using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class BrukerRepo: IBrukerRepo
    {
        private readonly MyDBContext _db;
        private readonly ILogRepo _log;

        public BrukerRepo(MyDBContext db, ILogRepo log)
        {
            _db = db;
            _log = log;
        }


        public async Task<List<BrukerDTO>> HentAlleBrukereAsync()
        {
            return (await _db.Brukere.ToListAsync()).Select(b => new BrukerDTO{Id = b.Id, Brukernavn = b.Brukernavn}).ToList();
        }

        public async Task<BrukerDTO> HentEnBrukerByIdAsync(int id)
        {
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Id == id);
            if (bruker is null)
            {
                return null;
            }
            return new BrukerDTO{Id = bruker.Id, Brukernavn = bruker.Brukernavn};
        }

        public async Task<Bruker> LeggTilBrukerAsync(BrukerDTO bruker)
        {
            var salt = LagSalt();
            var hash = HashPassord(bruker.Passord, salt);

            Bruker b = new Bruker
            {
                Brukernavn = bruker.Brukernavn,
                PassordHash = hash,
                Salt = salt
            };

            bruker.Passord = "";

            await _log.LogAsync($"Ny bruker opprettet med brukernavn; {bruker.Brukernavn}");

            _db.Brukere.Add(b);
            await _db.SaveChangesAsync();
            return b;
        }

        public async Task<BrukerDTO> VerifiserBrukerAsync(BrukerDTO bruker)
        {
            var dbBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
            if (dbBruker is null)
            {
                return null;
            }

            var hash = HashPassord(bruker.Passord, dbBruker.Salt);

            if (hash.SequenceEqual(dbBruker.PassordHash))
            {
                bruker.Passord = "";
                _db.Logs.Add(new Log
                {
                    Beskrivelse = "Bruker logget inn",
                    Bruker = dbBruker,
                    DatoEndret = DateTime.Now
                });
                await _db.SaveChangesAsync();
                Console.WriteLine("--> logget login");
                return bruker;
            }

            return null;
        }

        public async Task<BrukerDTO> EndreBrukerAsync(int id, BrukerUpdateDTO innBruker)
        {
            var dbBruker = await _db.Brukere.FindAsync(id);

            if (dbBruker is null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var hash = HashPassord(innBruker.Passord, dbBruker.Salt);

            if (hash.SequenceEqual(dbBruker.PassordHash))
            {
                dbBruker.Salt = LagSalt();
                dbBruker.PassordHash = HashPassord(innBruker.NyttPassord, dbBruker.Salt);
                await _db.SaveChangesAsync();
                
                await _log.LogAsync($"Endret passord for bruker: {dbBruker.Brukernavn}");

                return new BrukerDTO {Id = dbBruker.Id, Brukernavn = dbBruker.Brukernavn};
            }

            return null;

        }

        public async Task<int> SlettBrukerAsync(int id)
        {
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Id == id);
            _db.Brukere.Remove(bruker);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> LagreAsync()
        {
            return await _db.SaveChangesAsync();
        }
        
        public byte[] HashPassord(string uhashet, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(password: uhashet, salt: salt, prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000, numBytesRequested: 32);
        }

        public byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }
    }
}
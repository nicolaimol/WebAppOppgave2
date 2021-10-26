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

        public BrukerRepo(MyDBContext db)
        {
            _db = db;
        }


        public async Task<List<BrukerDTO>> HentAlle()
        {
            return (await _db.Brukere.ToListAsync()).Select(b => new BrukerDTO{Id = b.Id, Brukernavn = b.Brukernavn}).ToList();
        }

        public async Task<BrukerDTO> HentEn(int id)
        {
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Id == id);
            if (bruker is null)
            {
                return null;
            }
            return new BrukerDTO{Id = bruker.Id, Brukernavn = bruker.Brukernavn};
        }

        public async Task<Bruker> LeggTil(BrukerDTO bruker)
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

            _db.Brukere.Add(b);
            await _db.SaveChangesAsync();
            return b;
        }

        public async Task<BrukerDTO> VerifiserBruker(BrukerDTO bruker)
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
                return bruker;
            }

            return null;
        }

        public async Task Endre(int id, BrukerDTO bruker)
        {
            var brukeren = await _db.Bestillinger.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> Slett(int id)
        {
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Id == id);
            _db.Brukere.Remove(bruker);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> Lagre()
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
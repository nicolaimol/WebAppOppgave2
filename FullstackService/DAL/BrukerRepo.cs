using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;
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


        public async Task<List<Bruker>> HentAlle()
        {
            return await _db.Brukere.ToListAsync();
        }

        public async Task<Bruker> HentEn(int id)
        {
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Id == id);
            return bruker;
        }

        public async Task<Bruker> LeggTil(Bruker bruker)
        {
            _db.Brukere.Add(bruker);
            await _db.SaveChangesAsync();
            return bruker;
        }

        public async Task Endre(int id, Bruker bruker)
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
    }
}
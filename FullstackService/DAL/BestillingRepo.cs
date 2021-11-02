using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class BestillingRepo: IBestillingRepo
    {
        private readonly MyDBContext _db;
        private readonly ILogRepo _log;

        public BestillingRepo(MyDBContext db, ILogRepo log)
        {
            _db = db;
            _log = log;
        }
        
        public async Task<List<Bestilling>> HentAlleBestillingerAsync()
        {
            return await _db.Bestillinger.ToListAsync();
        }

        public async Task<Bestilling> HentEnBestillingAsync(int id)
        {
            var bestilling = await _db.Bestillinger.FirstOrDefaultAsync(k => k.Id == id);

            //await _log.LogAsync($"Hentet bestilling på id: {id}");

            return bestilling;
        }

        public async Task<Bestilling> HentEnBestillingByRefAsync(string referanse)
        {
            var bestilling = await _db.Bestillinger
                .FirstOrDefaultAsync(b => b.Referanse == referanse);

            return bestilling;

        }
        
        public async Task<Bestilling> LeggTilBestillingAsync(Bestilling bestilling)
        {
            // teste for unik Refereanse
            bool uniq = false;
            int teller = 0;
            // uses 8 first chars in new GUID to generate ref
            // uses do while to ensure unique ref 
            do
            {
                uniq = _db.Bestillinger
                    .FirstOrDefaultAsync(b => b.Referanse == bestilling.Referanse).Result == null;
                if (!uniq)
                {
                    bestilling.Referanse = Guid.NewGuid().ToString().Split("-")[0];
                    teller++;
                }
            } while (!uniq);

            // gets lugar and post from db to have right foreign key
            if (bestilling.LugarType != null)
            {
                bestilling.LugarType = await _db.Lugarer.FirstOrDefaultAsync(l => l.Id == bestilling.LugarType.Id);
            }
            bestilling.KontaktPerson.Post = await _db.PostSteder.FindAsync(bestilling.KontaktPerson.Post.PostNummer);
            bestilling.Reise = await _db.Reiser.FindAsync(bestilling.ReiseId);
            
            _db.Bestillinger.Add(bestilling);
            await _db.SaveChangesAsync();
            return bestilling;
        }

        public async Task<Bestilling> EndreBestillingAsync(int id, Bestilling bestilling)
        {
            var dbBesilling = await _db.Bestillinger.FindAsync(id);
            dbBesilling.UtreiseDato = bestilling.UtreiseDato;
            dbBesilling.HjemreiseDato = bestilling.HjemreiseDato;
            dbBesilling.Registreringsnummer = bestilling.Registreringsnummer;

            await _log.LogAsync($"endret bestilling med id: {id}");

            return dbBesilling;
        }

        public async Task<int> LagreBestillingAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<int> SlettBestillingAsync(int id)
        {
            var bestilling = await _db.Bestillinger.FirstOrDefaultAsync(b => b.Id == id);
            _db.Bestillinger.Remove(bestilling);

            await _log.LogAsync($"SLettet bestilling med id: {id}");

            return await _db.SaveChangesAsync();
        }
    }
}
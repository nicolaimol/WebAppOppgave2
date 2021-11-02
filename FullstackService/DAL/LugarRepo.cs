using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullstackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class LugarRepo: ILugarRepo
    {
        private readonly MyDBContext _db;
        private readonly ILogRepo _log;

        public LugarRepo(MyDBContext db, ILogRepo log)
        {
            _db = db;
            _log = log;
        }
        
        public async Task<List<Lugar>> HentLugerByReiseAsync(int reiseId)
        {
            return await _db.Lugarer.Where(l => l.Reise.Id == reiseId).ToListAsync();
        }

        public async Task<Lugar> CreateLugarAsync(Lugar lugar)
        {
            _db.Lugarer.Add(lugar);
            await _db.SaveChangesAsync();

            await _log.LogAsync($"Laget ny lugar til id: {lugar.ReiseId}");

            return lugar;
        }

        public async Task<Lugar> UpdateLugarAsync(Lugar lugar)
        {
            if (!_db.Lugarer.Any(l => l.Id == lugar.Id))
            {
                return null;
            }

            var dbLugar = _db.Lugarer.Find(lugar.Id);
            dbLugar.Antall = lugar.Antall;
            dbLugar.Pris = lugar.Pris;
            dbLugar.Type = lugar.Type;

            await _log.LogAsync($"Endret lugar med id: {lugar.Id}");

            await _db.SaveChangesAsync();
            return dbLugar;
        }

        public async Task<Lugar> DeleteLugarAsync(int id)
        {
            var dbLugar = await _db.Lugarer.FindAsync(id);
            if (dbLugar is null)
            {
                return null;
            }

            var lugar = _db.Lugarer.Remove(dbLugar);
            await _db.SaveChangesAsync();

            await _log.LogAsync($"Slettet lugar med id: {id}");
            
            return lugar.Entity;
        }
    }
}
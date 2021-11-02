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

        public LugarRepo(MyDBContext db)
        {
            _db = db;
        }
        
        public async Task<List<Lugar>> HentLugerByReiseAsync(int reiseId)
        {
            return await _db.Lugarer.Where(l => l.Reise.Id == reiseId).ToListAsync();
        }

        public async Task<Lugar> CreateLugarAsync(Lugar lugar)
        {
            _db.Lugarer.Add(lugar);
            await _db.SaveChangesAsync();

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
            
            return lugar.Entity;
        }
    }
}
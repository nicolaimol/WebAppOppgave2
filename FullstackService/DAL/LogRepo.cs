using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class LogRepo: ILogRepo
    {
        private readonly MyDBContext _db;
        private readonly IHttpContextAccessor _http;

        public LogRepo(MyDBContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }
        
        public async Task LogAsync(string beskrivelse)
        {
            string username = _http.HttpContext?.Session.GetString("logget inn");
            var bruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == username);

            if (bruker is null)
            {
                /*
                _db.Logs.Add(new Log
                {
                    Beskrivelse = "ENDRING UTEN SESSION!!!!",
                    DatoEndret = DateTime.Now,
                    Bruker = await _db.Brukere.FindAsync(1)
                });
                */
            }
            else
            {
                _db.Logs.Add(new Log
                {
                    Beskrivelse = beskrivelse,
                    DatoEndret = DateTime.Now,
                    Bruker = bruker
                });
            }

            
            await _db.SaveChangesAsync();
        }

        public async Task<List<Log>> HentLogAsync()
        {
            return (await _db.Logs.ToListAsync()).Select(l => new Log
            {
                Id = l.Id,
                Bruker = new Bruker
                {
                    Brukernavn = l.Bruker.Brukernavn,
                    Id = l.Bruker.Id
                },
                Beskrivelse = l.Beskrivelse,
                DatoEndret = l.DatoEndret
            }).ToList();
        }
    }
}
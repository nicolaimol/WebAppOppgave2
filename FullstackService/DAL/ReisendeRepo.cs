using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public class ReisendeRepo: IReisendeRepo
    {
        private readonly MyDBContext _db;

        public ReisendeRepo(MyDBContext db)
        {
            _db = db;
        }
        
        public async Task<KontaktPerson> ChangeKontaktPersonAsync(int id, KontaktPerson kontaktPerson)
        {
            var dbKontaktPerson = await _db.FindAsync<KontaktPerson>(id);
            if (dbKontaktPerson is null)
            {
                return null;
            }

            dbKontaktPerson.Fornavn = kontaktPerson.Fornavn;
            dbKontaktPerson.Etternavn = kontaktPerson.Etternavn;
            dbKontaktPerson.Epost = kontaktPerson.Epost;
            dbKontaktPerson.Post = await _db.PostSteder.FindAsync(kontaktPerson.Post.PostNummer);
            dbKontaktPerson.Adresse = kontaktPerson.Adresse;
            dbKontaktPerson.Telefon = kontaktPerson.Telefon;
            dbKontaktPerson.Foedselsdato = kontaktPerson.Foedselsdato;

            var antall = await _db.SaveChangesAsync();

            return antall > 0 ? dbKontaktPerson : null;
        }

        public async Task<Voksen> ChangeVoksenAsync(int id, Voksen voksen)
        {
            var dbVoksen = await _db.Voksne.FindAsync(id);
            if (dbVoksen is null)
            {
                return null;
            }

            dbVoksen.Fornavn = voksen.Fornavn;
            dbVoksen.Etternavn = voksen.Etternavn;
            dbVoksen.Foedselsdato = voksen.Foedselsdato;

            var antall = await _db.SaveChangesAsync();

            return antall > 0 ? dbVoksen : null;
        }
        
        public async Task<Barn> ChangeBarnAsync(int id, Barn barn)
        {
            var dbBarn = await _db.Barn.FindAsync(id);
            if (dbBarn is null)
            {
                return null;
            }

            dbBarn.Fornavn = barn.Fornavn;
            dbBarn.Etternavn = barn.Etternavn;
            dbBarn.Foedselsdato = barn.Foedselsdato;

            var antall = await _db.SaveChangesAsync();

            return antall > 0 ? dbBarn : null;
        }
    }
}
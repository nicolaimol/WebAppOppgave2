using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IReisendeRepo
    {
        Task<KontaktPerson> ChangeKontaktPersonAsync(int id, KontaktPerson kontaktPerson);
        Task<Voksen> ChangeVoksenAsync(int id, Voksen voksen);
        Task<Barn> ChangeBarnAsync(int id, Barn barn);

    }
}
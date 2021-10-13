using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IReiseRepo
    {
        Task<bool> AddOne(Reise reise);

        Task<List<Reise>> GetAll();

        Task<List<Lugar>> HentLugerByReise(int reiseId);

        Task<Post> HentPoststedByPostnummer(string postnummer);
    }
}
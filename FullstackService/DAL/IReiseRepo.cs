using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IReiseRepo
    {
        Task<bool> AddOne(Reise reise);

        Task<Reise> GetOneById(int id);

        Task<List<Reise>> GetAll();

        Task<List<Lugar>> HentLugerByReise(int reiseId);
        Task<Lugar> CreateLugar(Lugar lugar);

        Task<Lugar> UpdateLugar(Lugar lugar);

        Task<Post> HentPoststedByPostnummer(string postnummer);

        Task<Reise> UpdateReise(int reiseId, Reise reise);

        Task<Reise> DeleteReise(int reiseId);

        Task<List<Bilde>> GetAlleBilder();
        Task InsertBilde(Bilde bilde);
    }
}
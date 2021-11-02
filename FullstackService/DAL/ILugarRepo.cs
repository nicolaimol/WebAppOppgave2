using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface ILugarRepo
    {
        Task<List<Lugar>> HentLugerByReiseAsync(int reiseId);
        
        Task<Lugar> CreateLugarAsync(Lugar lugar);

        Task<Lugar> UpdateLugarAsync(Lugar lugar);

        Task<Lugar> DeleteLugarAsync(int id);
    }
}
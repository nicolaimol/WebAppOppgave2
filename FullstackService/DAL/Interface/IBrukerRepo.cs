using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FullstackService.DAL
{
    public interface IBrukerRepo
    {
        Task<List<BrukerDTO>> HentAlleBrukereAsync();
        
        Task<BrukerDTO> HentEnBrukerByIdAsync(int id);

        Task<Bruker> LeggTilBrukerAsync(BrukerDTO bruker);

        Task<BrukerDTO> VerifiserBrukerAsync(BrukerDTO bruker);

        Task<BrukerDTO> EndreBrukerAsync(int id, BrukerUpdateDTO bruker);

        Task<int> SlettBrukerAsync(int id);

        Task<int> LagreAsync();

        
    }
}
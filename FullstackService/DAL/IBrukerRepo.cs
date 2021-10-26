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
        Task<List<BrukerDTO>> HentAlle();
        
        Task<BrukerDTO> HentEn(int id);

        Task<Bruker> LeggTil(BrukerDTO bruker);

        Task<BrukerDTO> VerifiserBruker(BrukerDTO bruker);

        Task Endre(int id, BrukerDTO bruker);

        Task<int> Slett(int id);

        Task<int> Lagre();

        
    }
}
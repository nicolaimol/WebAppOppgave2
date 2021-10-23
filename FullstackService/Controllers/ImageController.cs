using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FullstackService.Controllers
{
    [Route("api/image")]
    //[ApiController]
    public class ImageController: ControllerBase
    {
        public static IWebHostEnvironment _env;
        private readonly IReiseRepo _repo;

        public ImageController(IWebHostEnvironment env, IReiseRepo repo)
        {
            _env = env;
            _repo = repo;
        }

        public class FileUploadApi
        {
            public IFormFile files { get; set; }
        }
        
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> Post()
        {
            
            try
            {
                var request = HttpContext.Request.Form.Files;
                if (request.Count > 0)
                {
                    if (! Directory.Exists(_env.WebRootPath + "/res/"))
                    {
                        Directory.CreateDirectory(_env.WebRootPath + "/res/");
                    }

                    using (FileStream fileStream =
                        System.IO.File.Create(_env.WebRootPath + "/res/" + request[0].FileName))
                    {
                        request[0].CopyTo(fileStream);
                        fileStream.Flush();

                        var bilde = new Bilde
                        {
                            Url = "./res/" + request[0].FileName
                        };

                        await _repo.InsertBilde(bilde);
                        
                        return Ok(bilde);
                    }
                }
                else
                {
                    return BadRequest("Failed to upload");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message.ToString());
            }
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleBilder()
        {
            return Ok(await _repo.GetAlleBilder());
        }
        
        
    }
}
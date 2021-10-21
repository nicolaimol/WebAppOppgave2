using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
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
                        return Ok(new {url = "/res/" + request[0].FileName});
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
    }
}
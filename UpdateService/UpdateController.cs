using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace UpdateService
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        public UpdateController()
        {

        }

        [HttpGet(Name = "GetUpdate")]
        public IActionResult GetUpdate()
        {
            string filePath = @"C:\Users\Андрей\Desktop\mods.zip";
            string fileName = "mods.zip";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }


    }
    
}

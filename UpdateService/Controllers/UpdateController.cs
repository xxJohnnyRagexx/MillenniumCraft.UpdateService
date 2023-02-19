using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using UpdateService.Models;

namespace UpdateService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly UpdatesRepository _updatesRepository;

        public UpdateController(UpdatesRepository updatesRepository)
        {
            _updatesRepository = updatesRepository;
        }

        [HttpGet(Name = "GetUpdate")]
        public async Task<IActionResult> GetUpdate()
        {
            string filePath = @"C:\Users\Андрей\Desktop\mods.zip";
            string fileName = "mods.zip";

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }

        [HttpPost]
        [Route("update")]
        public async Task AddUpdate([FromBody] UpdateRequest request)
        {
            _updatesRepository.AddNewUpdate(new Data.Models.UpdateItemEntity
            {
                Description = request.Description,
                GameVersion = request.GameVersion,
                Version = request.Version,
                Path = request.Path,
            });
        }

        [HttpGet]
        [Route("FetchUpdates")]
        public async Task<IEnumerable<UpdateItemEntity>> FetchUpdates()
        {
            return await _updatesRepository.FetchUpdatesData();
        }

    }

}

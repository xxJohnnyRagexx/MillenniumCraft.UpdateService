using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using UpdateService.Models;

namespace UpdateService.Controllers
{
    [ApiController]
    [Route("api/updates-service")]
    public class UpdateController : ControllerBase
    {
        private readonly UpdatesRepository _updatesRepository;

        public UpdateController(UpdatesRepository updatesRepository)
        {
            _updatesRepository = updatesRepository;
        }

        [HttpGet]
        [Route("update")]
        public async Task<IActionResult> GetUpdate(string gameVersion)
        {
            string fileName = string.Format($"{gameVersion}.zip");

            var data = _updatesRepository.FetchUpdate(gameVersion);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(data.Path);

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
        [Route("fetch-updates")]
        public async Task<IEnumerable<UpdateItemEntity>> FetchUpdates()
        {
            return await _updatesRepository.FetchUpdatesData();
        }

    }

}

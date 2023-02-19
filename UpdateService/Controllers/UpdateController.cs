using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using UpdateService.Filters;
using UpdateService.Models;

namespace UpdateService.Controllers
{
    [GeneralExceptionFiler]
    [ApiController]
    [Route("api/updates-service")]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdatesRepository _updatesRepository;

        public UpdateController(IUpdatesRepository updatesRepository)
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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("fetch-updates")]
        public async Task<IEnumerable<UpdateResponse>> FetchUpdates()
        {
            var r = _updatesRepository.FetchUpdatesData();
            return r.Select(x => x.ToResponse()).ToList();
        }

    }

}

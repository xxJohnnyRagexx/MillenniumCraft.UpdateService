using Data.Models;

namespace UpdateService.Models
{
    public static class MappingExtensions
    {
        public static UpdateItemEntity ToEntity(this UpdateRequest source)
        {
            return new UpdateItemEntity
            {
                GameVersion = source.GameVersion,
                Description = source.Description,
                Path = source.Path,
            };
        }

        public static UpdateResponse ToResponse(this UpdateItemEntity source)
        {
            return new UpdateResponse
            {
                Version = source.Version,
                GameVersion = source.GameVersion,
                Description = source.Description,
                Path = source.Path,
            };
        }
    }
}

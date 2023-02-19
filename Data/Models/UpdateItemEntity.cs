using LiteDB;

namespace Data.Models
{
    public class UpdateItemEntity
    {
        /// <summary>
        /// Gets or sets PK represented as integer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets version of package.
        /// </summary>
        public int Version { get; set; } = 0;

        /// <summary>
        /// Gets or sets version of game, which would be modified.
        /// </summary>
        public string GameVersion { get; set; }

        /// <summary>
        /// Gets or sets description of update.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the final Path, that can be used for downloading of package by client.
        /// </summary>
        public string Path { get; set; }
    }
}

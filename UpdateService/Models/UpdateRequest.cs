namespace UpdateService.Models
{
    public class UpdateRequest
    {
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

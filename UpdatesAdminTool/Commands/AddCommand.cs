using Data.Models;
using Data.Repositories;
using LiteDB;
using Shared;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace UpdatesAdminTool.Commands
{
    public class AddCommand : Command<AddCommand.Settings>
    {
        public sealed class Settings : CommandSettings
        {
            [Description("Версия minecraft для которой распространяется обновление")]
            [CommandOption("-v|--game-version")]
            public string GameVersion { get; set; }

            [Description("Путь к файлу с текстом описания обновления")]
            [CommandOption("-f|--description-file")]
            public string DescriptionFilePath { get; set; }

            [Description("Путь к файлам обновления")]
            [CommandOption("-p|--path")]
            public string Path { get; set; }
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            var item = new UpdateItemEntity();

            item.GameVersion = settings.GameVersion;
            item.Path = settings.Path;
            if (!string.IsNullOrEmpty(settings.DescriptionFilePath))
            {
                using (var sr = new StreamReader(settings.DescriptionFilePath))
                {
                    item.Description = sr.ReadToEnd();
                }
            }

            var _repo = new UpdatesRepository(
                new LiteDatabase(
                    SettingsExtension
                    .BuildDatabaseConnectionString(
                        "updates.db", "shared")));
            _repo.AddNewUpdate(item);
            return 0;
        }
    }
}

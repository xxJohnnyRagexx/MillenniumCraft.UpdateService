using Spectre.Console.Cli;
using UpdatesAdminTool.Commands;

namespace UpdatesAdminTool
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<AddCommand>("add")
                .WithDescription("Добавляет пакет обновления к раздаче");
            });
            return app.Run(args);
 
        }
    }

}
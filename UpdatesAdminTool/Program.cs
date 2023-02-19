using CliFx;

namespace UpdatesAdminTool
{
    internal class Program
    {
        public static async Task<int> Main()
        {
            await new CliApplicationBuilder()
             .AddCommandsFromThisAssembly()
             .Build()
             .RunAsync();

        }
    }
    public class AddUpdate : ICommand
    {
        public ValueTask ExecuteAsync()
        {

        }
    }
}
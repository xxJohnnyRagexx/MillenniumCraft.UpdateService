using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace UpdatesAdminTool
{
    internal class Program
    {
        public static async Task<int> Main()
        {
             await new CliApplicationBuilder()
             .AddCommandsFromThisAssembly()
             .Build().RunAsync();
            return default;
        }
    }
    [Command]
    public class AddUpdate : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            console.Output.WriteLine("hello mazafaka");
            return default;
        }
    }
}
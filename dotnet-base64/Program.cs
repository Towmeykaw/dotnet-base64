using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
namespace dotnet_base64
{
    [Command(
        Name = "base64",
        FullName = "A .NET Core global tool to base64 encode/decode strings.")]
    class Program
    {
        [Required(ErrorMessage = "You must add a string")]
        [Argument(0, Description = "The string to encode/decode")]
        public string StringToProcess { get; }
        [Option(CommandOptionType.NoValue, Description = "Decode data. ",
            ShortName = "d", LongName = "decode")]
        public bool Decode { get; } = false;
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        
        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            try
            {   
                if (!console.IsOutputRedirected)
                    ClearCurrentConsoleLine();
                if (Decode)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(StringToProcess)));
                }
                else
                {                    
                    Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(StringToProcess)));
                }                
                return 0;
            }
            catch (Exception e)
            {
                return 1;
            }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}

using System;
using System.IO;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
namespace dotnet_base64
{
    [Command(
        Name = "base64",
        FullName = "A .NET Core global tool to base64 encode/decode strings.")]
    internal class Program
    {
        [Argument(0, Description = "The string to encode/decode")]
        public string StringToProcess { get; } = "";

        [Option(CommandOptionType.NoValue, Description = "Decode data. ",
            ShortName = "d", LongName = "decode")]
        public bool Decode { get; } = false;

        [Option(CommandOptionType.SingleValue, Description = "Input file to read data from.",
            ShortName = "f", LongName = "inputfile")]
        public string InputFilePath { get; }

        [Option(CommandOptionType.SingleValue, Description = "Output file to write data to.",
            ShortName = "o", LongName = "outputfile")]
        public string OutputFilePath { get; }

        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        
        private int OnExecute(IConsole console)
        {
            try
            {
                string data;
                if (InputFilePath != null && File.Exists(InputFilePath))
                {
                    data = File.ReadAllText(InputFilePath);
                }
                else
                {
                    data = !console.IsInputRedirected ? StringToProcess : Console.In.ReadLine();
                }

                if (OutputFilePath != null)
                {   
                    File.AppendAllText(OutputFilePath, Decode
                        ? Encoding.UTF8.GetString(Convert.FromBase64String(data))
                        : Convert.ToBase64String(Encoding.UTF8.GetBytes(data)));
                }
                else
                {
                    Console.WriteLine(Decode
                        ? Encoding.UTF8.GetString(Convert.FromBase64String(data))
                        : Convert.ToBase64String(Encoding.UTF8.GetBytes(data)));
                }

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}

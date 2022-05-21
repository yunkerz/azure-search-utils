// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;

public partial class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Hello!" + Environment.NewLine);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

            if (args.Length == 0)
            {
                Console.WriteLine("If execute code you want, input command you must." + Environment.NewLine);
                Console.WriteLine("Current commands include: saveindexdef (or sid) ... and that's it.");
            }
            else
            {
                var command = args[0];

                switch (command.ToLower())
                {
                    case "sid":
                    case "saveindexdef":
                        // validate that we have the right number of arguments
                        // args[1] == indexName
                        // args[2] == filePath including fileName
                        if (args.Length != 3)
                        {
                            Console.WriteLine("BRAAAPPPP. Failure to compute...." + Environment.NewLine);
                            Console.WriteLine("Incorrect command-line argument input. SaveIndexDefinition requires the command and two additional arguments.");
                            Console.WriteLine(@"Example:  sid imdbbasicindex C:\Temp\ImdbBasicsIndexDefinition.json");
                            return;
                        }

                        var w = new AzureSearchUtils.SaveIndexDefinition.SaveIndexDefinitionUtil(config);
                        w.SaveIndexDefinitionFile(args[1].ToString(), args[2].ToString());
                        break;

                    default:
                        Console.WriteLine("I know not the command of which you type.");
                        break;
                }
            }

            Console.WriteLine(Environment.NewLine + "Press any key to exit...");
            Console.ReadKey();

        }
        catch (Exception ex)
        {
            var exString = "Yonkleblatt! There was an unhandled exception." + Environment.NewLine +
                            ex.ToString() + Environment.NewLine +
                            ex.StackTrace;
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using BasicBot.Properties;
using Newtonsoft.Json;
using SpaceInvaders;
using SpaceInvaders.Command;
using SpaceInvaders.Core;

namespace BasicBot
{
    public class BasicBot
    {
        public BasicBot(string outputPath)
        {
            OutputPath = outputPath;
        }

        protected string OutputPath { get; private set; }

        public void Execute()
        {
            var match = LoadState();
            LogMatchState(match);

            var map = LoadMap();
            Log(String.Format("Map:{0}{1}", Environment.NewLine, map));

            var shipCommand = GetRandomShipCommand();
            SaveShipCommand(shipCommand);
        }

        private Match LoadState()
        {
            var filename = Path.Combine(OutputPath, Settings.Default.StateFile);
            try
            {
                string jsonText;
                using (var file = new StreamReader(filename))
                {
                    jsonText = file.ReadToEnd();
                }

                return DeserializeState(jsonText);
            }
            catch (IOException e)
            {
                Log(String.Format("Unable to read state file: {0}", filename));
                var trace = new StackTrace(e);
                Log(String.Format("Stacktrace: {0}", trace));
                return null;
            }
        }

        private static Match DeserializeState(string jsonText)
        {
            var match = JsonConvert.DeserializeObject<Match>(jsonText,
                new JsonSerializerSettings
                {
                    Converters = {new EntityConverter()},
                    NullValueHandling = NullValueHandling.Ignore
                });
            return match;
        }

        private void LogMatchState(Match match)
        {
            Log("Game state:");
            Log(String.Format("\tRound Number: {0}", match.RoundNumber));

            foreach (var player in match.Players)
            {
                LogPlayerState(player);
            }
        }

        private void LogPlayerState(Player player)
        {
            Log(String.Format("\tPlayer {0} Kills: {1}", player.PlayerNumber, player.Kills));
            Log(String.Format("\tPlayer {0} Lives: {1}", player.PlayerNumber, player.Lives));
            Log(String.Format("\tPlayer {0} Missiles: {1}/{2}", player.PlayerNumber,
                player.Missiles.Count, player.MissileLimit));
        }

        private string LoadMap()
        {
            var filename = Path.Combine(OutputPath, Settings.Default.MapFile);
            try
            {
                using (var file = new StreamReader(filename))
                {
                    return file.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Log(String.Format("Unable to read map file: {0}", filename));
                var trace = new StackTrace(e);
                Log(String.Format("Stacktrace: {0}", trace));
                return "Failed to load map!";
            }
        }

        private ShipCommand GetRandomShipCommand()
        {
            var random = new Random();
            var possibleShipCommands = Enum.GetValues(typeof (ShipCommand));
            return (ShipCommand) possibleShipCommands.GetValue(random.Next(0, possibleShipCommands.Length));
        }

        private void SaveShipCommand(ShipCommand shipCommand)
        {
            var shipCommandString = shipCommand.ToString();
            var filename = Path.Combine(OutputPath, Settings.Default.OutputFile);
            try
            {
                using (var file = new StreamWriter(filename))
                {
                    file.WriteLine(shipCommandString);
                }

                Log("Command: " + shipCommandString);
            }
            catch (IOException e)
            {
                Log(String.Format("Unable to write command file: {0}", filename));

                var trace = new StackTrace(e);
                Log(String.Format("Stacktrace: {0}", trace));
            }
        }

        private void Log(string message)
        {
            Console.WriteLine("[BOT]\t{0}", message);
        }
    }
}
using System.Collections.Immutable;
using TelstraPurple.Robot;

namespace TelstraPurple.ConsoleSample // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new RobotBuilder();

            var robot = builder.Build(() =>
            new RobotOptions
            {
                BlockedCells = ImmutableList.CreateRange<int[]>(
                    new List<int[]>
                    {
                        new int[] {1, 1 },
                        new int[] {2, 9},
                        new int[] {0, 1},
                    }),
                TableTopSize = ImmutableSortedDictionary.CreateRange<string, int>(
                    new List<KeyValuePair<string, int>>
                    {
                        new KeyValuePair<string, int>("X", 10),
                        new KeyValuePair<string, int>("Y", 10),
                    }),
                Reporter = Console.WriteLine
            });

            robot.CommandExecuted += Robot_CommandExecuted;

            do
            {
                var input = Console.ReadLine();
                robot.Command(input);
            }
            while (true);
        }

        private static void Robot_CommandExecuted(object? sender, RobotCommandEventArgs e)
        {
            // event to handle commands callback
        }
    }
}
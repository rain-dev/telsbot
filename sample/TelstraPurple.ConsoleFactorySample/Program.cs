using System.Collections.Immutable;
using TelstraPurple.Robot;

namespace TelstraPurple.ConsoleFactorySample // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var factory = new RoboFactory();

            factory.CreateRobot("Zena", new RobotOptions
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
            factory.CreateRobot("Otter", new RobotOptions
            {
                BlockedCells = ImmutableList.CreateRange<int[]>(
                    new List<int[]>
                    {
                        new int[] {8, 1 },
                        new int[] {1, 5},
                        new int[] {5, 5},
                    }),
                TableTopSize = ImmutableSortedDictionary.CreateRange<string, int>(
                    new List<KeyValuePair<string, int>>
                    {
                        new KeyValuePair<string, int>("X", 20),
                        new KeyValuePair<string, int>("Y", 20),
                    }),
                Reporter = Console.WriteLine
            });

            Robot.Robot robot = default;
            do
            {
                Console.Write("Select a robot name (Zena or Otter): ");
                var robotName = Console.ReadLine();


                robot = factory.GetRobot(robotName);
            }
            while (robot == default);

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
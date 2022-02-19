using System.Collections.Immutable;

namespace TelstraPurple.Robot
{
    public class RobotOptions
    {
        /// <summary>
        ///    configure the size of the robot playground. default size is 6x6.
        /// </summary>
        public ImmutableSortedDictionary<string, int> TableTopSize { get; set; } =
            ImmutableSortedDictionary.CreateRange<string, int>(new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("X", 6),
                new KeyValuePair<string, int>("Y", 6)
            });

        /// <summary>
        ///    configure blocked cells where the robot can't go. default is empty
        /// </summary>
        public ImmutableList<int[]> BlockedCells { get; set; } = ImmutableList<int[]>.Empty;

        /// <summary>
        ///    a delegate action for robot invocation of report. default is Console.Writeline
        /// </summary>
        public Action<string, object[]> Reporter { get; set; } = Console.WriteLine;


    }
}

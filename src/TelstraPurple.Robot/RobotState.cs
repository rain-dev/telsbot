using TelstraPurple.Robot.Enums;

namespace TelstraPurple.Robot
{
    public class RobotState
    {

        /// <summary>
        ///    robot current position. [x, y] | [row, column]
        /// </summary>
        public int[] Position { get; set; }

        /// <summary>
        ///    the current direction of the robot. NORTH | EAST | WEST | EAST
        /// </summary>
        public RobotFrontDirections Direction { get; set; } = RobotFrontDirections.NORTH;

        /// <summary>
        ///    flag to check if the Robot has been placed already.
        /// </summary>
        public bool IsPlaced { get; set; } = false;

        /// <summary>
        ///    read only property for X-Axis address
        /// </summary>
        public int X { get => Position[0]; }

        /// <summary>
        ///    read only property for Y-Axis address
        /// </summary>
        public int Y { get => Position[1]; }
    }
}

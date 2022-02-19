using TelstraPurple.Robot;

namespace TelstraPurple.ConsoleFactorySample
{
    public class RoboFactory : IRobotFactory<Robot.Robot>
    {
        private Lazy<IDictionary<string, Robot.Robot>> robots = new Lazy<IDictionary<string, Robot.Robot>>(() => new Dictionary<string, Robot.Robot>());
        /// <summary>
        ///    create and stores robot.
        /// </summary>
        /// <param name="name">name of the robot</param>
        /// <param name="options">robot setup</param>
        /// <returns></returns>
        public Robot.Robot CreateRobot(string name, RobotOptions options)
        {
            var robotBuilder = new Robot.RobotBuilder();
            var robot = robotBuilder.Build(() => options);

            robots.Value.TryAdd(name, robot);

            return robot;
        }
        /// <summary>
        ///    select a robot given by robot name
        /// </summary>
        /// <param name="robotName">name of the robot</param>
        /// <returns></returns>
        public Robot.Robot GetRobot(string robotName)
        {
            if (robots.Value.TryGetValue(robotName, out var robot))
                return robot;

            return default;
        }
    }
}

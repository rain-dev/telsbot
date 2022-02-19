namespace TelstraPurple.Robot
{
    public interface IRobotBuilder<TRobot>
        where TRobot : Robot, new()
    {
        /// <summary>
        ///   combines setup and returns the robot object
        /// </summary>
        /// <returns></returns>
        public TRobot Build(Func<RobotOptions> options);
    }
}

namespace TelstraPurple.Robot
{
    public class RobotBuilder : IRobotBuilder<Robot>
    {

        private Robot _robot;
        public Robot Build(Func<RobotOptions> options)
        {
            this.BuildRobotGeneral();
            this.BuildRobotCapabilities(options);
            return _robot;
        }

        #region Protected Methods 

        protected virtual void BuildRobotCapabilities(Func<RobotOptions> options) =>
            _robot.Setup(options());

        protected virtual void BuildRobotGeneral() => _robot = new();

        #endregion
    }
}

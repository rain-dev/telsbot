using TelstraPurple.Robot.Enums;

namespace TelstraPurple.Robot
{
    public class RobotCommandEventArgs : EventArgs
    {
        public Commands Command { get; set; }
        public string Output { get; set; }
    }
}

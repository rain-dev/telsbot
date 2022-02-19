namespace TelstraPurple.Robot
{
    public interface IRobotFactory<TRobot>
        where TRobot : Robot, new()
    {
        TRobot CreateRobot(string name, RobotOptions options);
        TRobot GetRobot(string robotName);
       
    }
}

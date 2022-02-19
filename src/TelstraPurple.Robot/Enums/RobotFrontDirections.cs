namespace TelstraPurple.Robot.Enums
{
    /// <summary>
    /// robot direction for movement
    /// </summary>
    [Flags]
    public enum RobotFrontDirections : short
    {
        NORTH = 0,
        SOUTH = 1,
        EAST = 2,
        WEST = 3,
        // Z MOVEMENT
        NORTHWEST = NORTH | WEST,
        NORTHEAST = NORTH | EAST,
        SOUTHWEST = SOUTH | WEST,
        SOUTHEAST = SOUTH | EAST,

    }
}

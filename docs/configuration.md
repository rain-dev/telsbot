# CONFIGURATION GUIDE #

The app configuration guide and how to integrate the library

- - - -

#### Robot Commands ####

Command | Description
------------- | -------------
PLACE   | Moves the robot in the exact cell address given. If direction is given then the robot will face that direct. Default direction is NORTH.
MOVE    | Moves the robot cell by 1 based on what direction
LEFT \| RIGHT | Rotates the robots 90 degree.
REPORT  | Robot will broadcast a report of its position.


- - - -

#### [RobotOptions.cs](./../src/TelstraPurple.Robot/RobotOptions.cs "RobotOptions.cs") ####

This object represents the capabilities of the Robot and its environment. Below are the properties.

Name  | Description | Type
------------- | ------------- | -------------
BlockedCells  | Contains all the blocked cell addresses the robot can't pass on.  |  ImmutableList<int[]>
TableTopSize  | The size of the table top. | ImmutableDictionary<string, int>
Reporter      | A delegate method that the robot invokes when reporting. | Action<string, object[]>







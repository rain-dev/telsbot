using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TelstraPurple.Robot.Test
{
    [TestFixture]
    public class RobotTest
    {
        private Robot _robot;


        [OneTimeSetUp]
        public void Setup()
        {
            _robot = new Robot(new
            RobotOptions
            {
                BlockedCells = null,
                TableTopSize = ImmutableSortedDictionary.CreateRange<string, int>
                (new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("X", 6),
                    new KeyValuePair<string, int>("Y", 6)
                })
            });
        }


        [TestCase]
        public void When_Robot_Placed()
        {
            _robot.SendCommand(Enums.Commands.PLACE, new[] { 0, 0 }, Enums.RobotFrontDirections.NORTH);

            Assert.AreEqual(_robot.State.X, 0);
            Assert.AreEqual(_robot.State.Y, 0);
        }


        [TestCase]
        public void When_Robot_Moved_Without_Placing()
        {

            _robot.SendCommand(Enums.Commands.MOVE);

            Assert.IsNull(_robot.State.X);
            Assert.IsNull(_robot.State.Y);
        }

        [TestCase]
        public void When_Robot_Simulate()
        {

            _robot.SendCommand(Enums.Commands.PLACE, new int[2], Enums.RobotFrontDirections.NORTH);

            _robot.SendCommand(Enums.Commands.MOVE);
            _robot.SendCommand(Enums.Commands.MOVE);
            Assert.AreEqual(_robot.State.X, 2);
            Assert.AreEqual(_robot.State.Y, 0);


            _robot.SendCommand(Enums.Commands.RIGHT);
            _robot.SendCommand(Enums.Commands.MOVE);

            Assert.AreEqual(_robot.State.Y, 1);
            Assert.AreEqual(_robot.State.X, 2);
        }

        [TestCase]
        public void When_Robot_Simulate_And_Report()
        {

            _robot.SendCommand(Enums.Commands.PLACE, new int[2], Enums.RobotFrontDirections.NORTH);

            _robot.SendCommand(Enums.Commands.MOVE);
            _robot.SendCommand(Enums.Commands.MOVE);
            Assert.AreEqual(_robot.State.X, 2);
            Assert.AreEqual(_robot.State.Y, 0);


            _robot.SendCommand(Enums.Commands.RIGHT);
            _robot.SendCommand(Enums.Commands.MOVE);

            Assert.AreEqual(_robot.State.X, 1);
            Assert.AreEqual(_robot.State.Y, 2);

        }
    }
}
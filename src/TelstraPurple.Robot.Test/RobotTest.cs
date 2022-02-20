using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TelstraPurple.Robot.Test
{
    [TestFixture]
    public class RobotTest
    {
        private Robot _robot;
        private string _reporter;

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = new RobotBuilder();
            _robot = builder.Build(() => new RobotOptions
                {
                    BlockedCells = ImmutableList.CreateRange<int[]>
                    (
                        new List<int[]>
                        {
                            new int[] { 5,5},
                            new int[] { 0, 0},
                        }                        
                    ),
                    TableTopSize = ImmutableSortedDictionary.CreateRange<string, int>
                    (new List<KeyValuePair<string, int>>
                    {
                        new KeyValuePair<string, int>("X", 10),
                        new KeyValuePair<string, int>("Y", 10)
                    }),
                    Reporter = (format, args) => _reporter = string.Format(format, args)
                });
            
        }

        [Test]
        [TestCase(new object[] { "PLACE 1,1,SOUTH" , "LEFT", "MOVE", "MOVE"})]
        public void When_Robot_Is_Placed_Should_Move_Accordingly(object[] args)
        {
            foreach (var arg in args) _robot.Command(Convert.ToString(arg));


            Assert.AreEqual(_robot.State.X, 1);
            Assert.AreEqual(_robot.State.Y, 3);

            _robot.Command("REPORT");

            Assert.AreEqual(_reporter, "1,3, WEST");
        }

        [Test]
        [TestCase(new object[] { "LEFT", "MOVE", "MOVE" })]
        public void When_Robot_Is_Not_Placed_Commands_Should_Be_Ignored(object[] args)
        {
            foreach (var arg in args) _robot.Command(Convert.ToString(arg));

            _robot.Command("REPORT");

            Assert.IsNull(_reporter);
        }
        [Test]
        [TestCase(new object[] { "PLACE 4,4,NORTH", "MOVE", "RIGHT", "MOVE", "MOVE" })]
        public void When_Robot_Traversed_Into_Blocked_Cell_It_Should_Ignore(object[] args)
        {
            foreach (var arg in args) _robot.Command(Convert.ToString(arg));

            Assert.AreEqual(_robot.State.X, 5);
            Assert.AreEqual(_robot.State.Y, 4);

            _robot.Command("REPORT");

            Assert.AreEqual(_reporter, "5,4, WEST");
        }

    }
}
using Microsoft.Extensions.Options;
using TelstraPurple.Robot.Enums;

namespace TelstraPurple.Robot
{
    public class Robot : IRobot, IDisposable
    {
        public event EventHandler<RobotCommandEventArgs> CommandExecuted;


        #region Public Methods 

        /// <summary>
        ///    execute command to be parsed by the robot.
        ///    format: (MOVE|LEFT|RIGHT|PLACE|REPORT) [0-9]{1,2},[0-9]{1,2},(NORTH|SOUTH|WEST|EAST)
        /// </summary>
        /// <param name="input"></param>
        public void Command(string input)
        {
            if (string.IsNullOrEmpty(input)) return;

            string[] inputs = input.Split(' ');

            if (!Enum.TryParse<Commands>(inputs[0], true, out var command)) return;

            // if command is place, should expect input
            if (command == Commands.PLACE && inputs.Length < 2) return;

            if (command != Commands.PLACE) { ExecuteCommand(command); return; }

            var commandArgs = inputs[1].Split(',');
            var hasDirection = false;
            var direction = RobotFrontDirections.NORTH;

            // command args is in minimum of 2;
            if (commandArgs.Length < 2) return;

            if (commandArgs.Length == 3)
                hasDirection = Enum.TryParse<RobotFrontDirections>(commandArgs[2], true, out direction);


            // invalid command args
            if (!int.TryParse(commandArgs[0], out var x) ||
                !int.TryParse(commandArgs[1], out var y))
                return;


            if (command != Commands.PLACE)
            {
                ExecuteCommand(command);
                return;
            }

            if (hasDirection)
            {
                ExecuteCommand(command, new int[] { x, y }, direction);
                return;
            }
            ExecuteCommand(command, new int[] { x, y, });
        }
        
        public void Dispose()
        {
            State = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Internal Methods

        internal void Setup(RobotOptions options)
        {
            _options = options;
        }

        #endregion

        #region Protected Properties

        public RobotState? State { get; set; } = new();

        #endregion

        #region Protected Methods 

        /// <summary>
        ///   sends a command by the given cell and direction.
        /// </summary>
        /// <param name="command">Type of command. PLACE | MOVE | LEFT | RIGHT | REPORT</param>
        protected virtual void ExecuteCommand(Commands command)
        {
            if (State.Position == null) return;

            if (!State.IsPlaced) return;
            void SwitchDirection(Commands commands = Commands.LEFT | Commands.RIGHT)
            {
                switch (State.Direction)
                {
                    case RobotFrontDirections.NORTH:
                        State.Direction = commands == Commands.LEFT ? RobotFrontDirections.EAST : RobotFrontDirections.WEST;
                        break;
                    case RobotFrontDirections.SOUTH:
                        State.Direction = commands == Commands.LEFT ? RobotFrontDirections.WEST : RobotFrontDirections.EAST;
                        break;
                    case RobotFrontDirections.EAST:
                        State.Direction = commands == Commands.LEFT ? RobotFrontDirections.SOUTH : RobotFrontDirections.NORTH;
                        break;
                    case RobotFrontDirections.WEST:
                        State.Direction = commands == Commands.LEFT ? RobotFrontDirections.NORTH : RobotFrontDirections.SOUTH;
                        break;
                    default:
                        break;
                }
            }
            var eventArgs = new RobotCommandEventArgs
            {
                Command = command,
            };
            switch (command)
            {
                case Commands.MOVE:
                    Move();
                    break;
                case Commands.LEFT:
                case Commands.RIGHT:
                    SwitchDirection(command);
                    break;
                case Commands.REPORT:
                    var reportTemplate = "{0},{1}, {2}";
                    var args = new object[] { State.Position[0], State.Position[1], State.Direction };
                    eventArgs.Output = String.Format(reportTemplate, args);
                    _options.Reporter(reportTemplate, args);
                    break;

                default:
                    break;
            }
            OnCommandExecuted(eventArgs);
        }

        /// <summary>
        ///   sends a command by the given cell and direction.
        /// </summary>
        /// <param name="command">Type of command. PLACE | MOVE | LEFT | RIGHT | REPORT</param>
        /// <param name="cell">the address of the cell the robot should be placed. </param>
        protected virtual void ExecuteCommand(Commands command, int[] cell)
        {
            if (!IsWithinBorder(cell)) return;

            if (!IsNotBlocked(cell)) return;

            if (command == Commands.PLACE && !State.IsPlaced) State.IsPlaced = true;

            State.Position = cell;
            ExecuteCommand(command);
        }

        /// <summary>
        ///   sends a command by the given cell and direction.
        /// </summary>
        /// <param name="command">Type of command. PLACE | MOVE | LEFT | RIGHT | REPORT</param>
        /// <param name="cell">the address of the cell the robot should be placed. </param>
        /// <param name="direction">the direction the robot would face after command execution</param>
        protected virtual void ExecuteCommand(Commands command, int[] cell, RobotFrontDirections direction)
        {
            if (!IsWithinBorder(cell)) return;

            if (!IsNotBlocked(cell)) return;

            if (command == Commands.PLACE && !State.IsPlaced) State.IsPlaced = true; 
            
            State.Direction = direction;
            ExecuteCommand(command, cell);
        }

        /// <summary>
        ///    invokes event to send trigger 
        /// </summary>
        /// <param name="args">the command args after execution</param>
        protected virtual void OnCommandExecuted(RobotCommandEventArgs args) =>
            CommandExecuted?.Invoke(this, args);

        
        /// <summary>
        ///    executes and calculate move command.
        /// </summary>
        /// <returns></returns>
        protected virtual int[] Move()
        {
            var tableTopSize = new int[] { _options.TableTopSize["X"], _options.TableTopSize["Y"] };
            var axis = RobotFrontDirections.NORTH == State.Direction || RobotFrontDirections.SOUTH == State.Direction ? 0 : 1;
            var blockedRowAddresses = _options.BlockedCells.Select(o => o[0]).ToArray();
            var blockedColumnAddresses = _options.BlockedCells.Select(o => o[1]).ToArray();

            var move = new int[2];
            Array.Copy(State.Position, move, 2);
            // check if the move is north or west then its positive axis.
            if (axis == 0 && State.Direction == RobotFrontDirections.NORTH ||
                axis == 1 && State.Direction == RobotFrontDirections.WEST)
            {
                move[axis] = State.Position[axis] + 1;
            }
            else
            {
                move[axis] = State.Position[axis] - 1;
            } 
            if (!IsNotBlocked(move)) return State.Position;

            if (move[axis] == tableTopSize[axis] || move[axis] == -1) return State.Position;

            State.Position[axis] = move[axis];

            return State.Position;
        }
        #endregion

        #region Private Fields

        private RobotOptions _options;

        #endregion

        #region Private Methods

        private bool IsNotBlocked(int[] targetCell)
        {
            if (_options.BlockedCells.Any(o => o[0] == targetCell[0] && o[1] == targetCell[1])) return false;

            return true;
        }

        /// <summary>
        ///    validate if target cell is within border.
        /// </summary>
        /// <param name="cell">the target cell.</param>
        /// <returns>returns true if the robot is ok to move, will return false if the robot might crash using the target cell.</returns>
        private bool IsWithinBorder(int[] cell)
        {
            if (cell.Any(o => o == -1)) return false;

            if (State.Position == null) return true;

            if (State.Position[0] >= _options.TableTopSize["X"]) return false;
            if (State.Position[1] >= _options.TableTopSize["Y"]) return false;


            return true;
        }
        #endregion

    }
}

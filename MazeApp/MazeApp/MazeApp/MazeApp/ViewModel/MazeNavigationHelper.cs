using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeApp.Models;
using MazeApp.ViewModel.Core;

namespace MazeApp.ViewModel
{
    internal enum Navigation
    {
        Right,
        Left,
        Up,
        Down,
        None
    }
    public class MazeNavigationHelper : IMazeNavigationHelper
    {
        private Navigation PreviousNavigationPoint = Navigation.None;
        private IList<Position> NavigatedPath = new List<Position>();
        private const char FinishedSymbol = 'F';
        private const char InvalidChar = 'C';
        private const char WallChar = 'X';
        private IMaze myMaze;

        public MazeNavigationHelper(IMaze maze)
        {
            myMaze = maze;
        }

        public bool TryAndMoveRight(Position currentPosition, out Position newPosition)
        {
            newPosition = null;
            var nextPosition = new Position(currentPosition.X, currentPosition.Y + 1);
            char item;
            var canNavigate = TryGetItemWhileNavigating(nextPosition,out item);
            if (canNavigate)
            {
                bool removeItem = false;
                if (PreviousNavigationPoint.Equals(Navigation.Left))
                {
                    removeItem = true;
                }
                UpdateNavigationPath(nextPosition, removeItem,item);
                newPosition = nextPosition;
                PreviousNavigationPoint = Navigation.Right;
            }

            return canNavigate;
        }

        public bool TryAndMoveLeft(Position currentPosition, out Position newPosition)
        {
            newPosition = null;
            char item;
            var nextPosition = new Position(currentPosition.X, currentPosition.Y - 1);
            var canNavigate = TryGetItemWhileNavigating(nextPosition,out item);
            if (canNavigate)
            {
                bool removeItem = false;
                if (PreviousNavigationPoint.Equals(Navigation.Right))
                {
                    removeItem = true;
                }
                UpdateNavigationPath(nextPosition, removeItem,item);
                newPosition = nextPosition;
                PreviousNavigationPoint = Navigation.Left;
            }

            return canNavigate;
        }

        public bool TryAndMoveUp(Position currentPosition, out Position newPosition)
        {
            newPosition = null;
            char item;
            var nextPosition = new Position(currentPosition.X - 1, currentPosition.Y);
            var canNavigate = TryGetItemWhileNavigating(nextPosition,out item);
            if (canNavigate)
            {
                bool removeItem = false;
                if (PreviousNavigationPoint.Equals(Navigation.Down))
                {
                    removeItem = true;
                }
                UpdateNavigationPath(nextPosition, removeItem,item);
                newPosition = nextPosition;
                PreviousNavigationPoint = Navigation.Up;
            }

            return canNavigate;
        }

        public bool TryAndMoveDown(Position currentPosition, out Position newPosition)
        {
            newPosition = null;
            char item;
            var nextPosition = new Position(currentPosition.X + 1, currentPosition.Y);
            var canNavigate = TryGetItemWhileNavigating(nextPosition,out item);
            if (canNavigate)
            {
                bool removeItem = false;
                if (PreviousNavigationPoint.Equals(Navigation.Up))
                {
                    removeItem = true;
                }
                UpdateNavigationPath(nextPosition, removeItem,item);
                newPosition = nextPosition;
                PreviousNavigationPoint = Navigation.Down;
            }
            return canNavigate;
        }

        public event EventHandler<MazeCompletedEvent> MaizeCompleted;

        private void UpdateNavigationPath(Position position, bool removeLastItem,char c)
        {
            if (removeLastItem)
            {
                NavigatedPath.RemoveAt(NavigatedPath.Count() - 1);
            }
            if (!NavigatedPath.Contains(position))
            {
                NavigatedPath.Add(position);
            }
            if(c.Equals(FinishedSymbol))
            {
                MaizeCompleted?.Invoke(this, new MazeCompletedEvent(NavigatedPath, true));
            }
        }

        private bool TryGetItemWhileNavigating(Position newPosition,out char item)
        {
            item =  myMaze.GetItemAtPosition(newPosition);

            if (char.IsWhiteSpace(item) || item.Equals(FinishedSymbol))
            {
                return true;
            }

            return false;
        }
             

        public void AutoSolveMaze()
        {
            var visited = new bool[myMaze.NumberOfWalls,myMaze.NumberOfSpaces];
            NavigatedPath.Clear();
            var solved = TryNavigate(myMaze.StartPosition.X,myMaze.StartPosition.Y, visited);        
            MaizeCompleted?.Invoke(this, new MazeCompletedEvent(NavigatedPath, solved));           
            
        }

        private bool TryNavigate(int x,int y,bool[,] visited )
        {
            bool navigate = true;
            bool rightPath = false;
            var symbol = myMaze.GetItemAtPosition(new Position(x, y));
            //We have reached end and not found anything 
            if (symbol.Equals(InvalidChar))
            {
                return false;
            }
            else
            {
                if(symbol.Equals(FinishedSymbol))
                {
                    rightPath = true;
                    navigate = false;
                }
                else if(symbol.Equals(WallChar) || visited[x,y])
                {
                    navigate = false;
                }
            }
            if(navigate)
            {
                visited[x, y] = true;
                rightPath = rightPath || TryNavigate(x + 1, y,visited);
                rightPath = rightPath || TryNavigate(x, y + 1, visited);
                rightPath = rightPath || TryNavigate(x-1, y, visited);
                rightPath = rightPath || TryNavigate(x, y - 1, visited);
            }
            if(rightPath)
            {
                UpdateNavigationPath(new Position(x, y), false,symbol);
            }

            return rightPath;
        }   


    }
}

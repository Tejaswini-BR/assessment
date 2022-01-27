using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeApp.Models
{
    public class Maze : IMaze,INotifyPropertyChanged
    {
        private Position myStartPosition;
        private Position myEndPosition;
        private IList<Tuple<Position, char>> Positions { get; set; }
        private const char FinishedSymbol = 'F';
        private const char InvalidChar = 'C';
        private const char WallChar = 'X';
        private const char WhiteSpace = ' ';
        private const char StartSymbol = 'S';
        private int myNumberOfWalls;
        private int myNumberOfSpaces;
        private bool myMazeIsValid;

        public Maze()
        {
            Positions = new List<Tuple<Position, char>>();
        }
        public int NumberOfWalls
        {
            get
            {
                return myNumberOfWalls;
            }
            set
            {
                myNumberOfWalls = value;
                OnPropertyChanged("NumberOfWalls");
            }
        }

        public int NumberOfSpaces
        {
            get
            {
                return myNumberOfSpaces;
            }
            set
            {
                myNumberOfSpaces = value;
                OnPropertyChanged("NumberOfSpaces");
            }
        }

        public bool IsValidMaze
        {
            get
            {
                return myMazeIsValid;
            }
            private set
            {
                myMazeIsValid = value;
                OnPropertyChanged("IsValidMaze");
            }
        }

        public void  ValidateMaze()
        {
            bool isValid = true ;
            var startItems = Positions.Where(x => x.Item2.Equals(StartSymbol)).ToList();
            var endItems = Positions.Where(x => x.Item2.Equals(StartSymbol)).ToList();
            if(startItems.Count()>1 || endItems.Count()>1)
            {
                isValid =  false;
            }
            IsValidMaze = isValid;            
        }

        public void UpdateMaze(int x, int y,char c)
        {
            var position = new Position(x, y);
            Positions.Add(new Tuple<Position, char>(position, c));

            switch (c)
            {
                case WallChar:
                                {
                                    NumberOfWalls++;
                                    break;
                                }

                case WhiteSpace:
                                {
                                    NumberOfSpaces++;
                                    break;
                                }

                case FinishedSymbol:
                                    {
                                        EndPosition = position;
                                        break;
                                    }

                case StartSymbol:
                                    {
                                        StartPosition = position;
                                        break;
                                    }
                 default: break;
            }           
            
        }
        
        public Position StartPosition
        {
            get { return myStartPosition; }
            set
            {
                myStartPosition = value;
                OnPropertyChanged("StartPosition");
            }
        }

        public Position EndPosition
        {
            get { return myEndPosition; }
            set
            {
                myEndPosition = value;
                OnPropertyChanged("EndPosition");
            }
        }

        public char GetItemAtPosition(Position position)
        {
            foreach (var item in Positions)
            {
                if (item.Item1.Equals(position))
                {
                    return item.Item2;
                }
            }
            return InvalidChar;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeApp.Models;
using MazeApp.ViewModel.Core;

namespace MazeApp.ViewModel
{
    public class MazeViewModel : IMazeViewModel,INotifyPropertyChanged
    {
        private bool myMaizeCompleted;
        private IMazeNavigationHelper myMaizeNavigationHelper;
        private char myGridValue;
        private Position myEnteredPosition;
       
        public IMaze Maze { get; }

        public AutoSolveCommand AutoSolveCommand { get; set; }

        public MazeViewModel(IMaze maze,IMazeNavigationHelper helper)
        {
            Maze = maze;
            myMaizeNavigationHelper = helper;
            myMaizeNavigationHelper.MaizeCompleted += OnMaizeCompleted;
            AutoSolveCommand = new AutoSolveCommand(this);
        }

        private void OnMaizeCompleted(object sender, MazeCompletedEvent e)
        {
            MaizeCompletedSuccessfully = e.MaizeSolved;
            MaizeCompleted?.Invoke(this, new MazeCompletedEvent(e.Positions,e.MaizeSolved));
        }

        public event EventHandler<MazeCompletedEvent> MaizeCompleted;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool MaizeCompletedSuccessfully
        {
            get
            {
                return myMaizeCompleted;
            }

            private set
            {
                myMaizeCompleted = value;
                OnPropertyChanged("MaizeCompletedSuccessfully");
            }
        }

        public Position Position
        {
            get { return myEnteredPosition; }
            set
            {
                myEnteredPosition = value;                
                OnPropertyChanged("Position");
                GridValue = Maze.GetItemAtPosition(Position);
            }
        }
        public char GridValue
        {
            get { return myGridValue; }
            set
            {
                myGridValue = value;
                OnPropertyChanged("GridValue");
            }
        }       
        
        public bool TryAndMoveRight(Position currentPosition, out Position newPosition)
        {
            return myMaizeNavigationHelper.TryAndMoveRight(currentPosition,out newPosition);
        }

        public bool TryAndMoveLeft(Position currentPosition, out Position newPosition)
        {
            return myMaizeNavigationHelper.TryAndMoveLeft(currentPosition, out newPosition);
        }

        public bool TryAndMoveDown(Position currentPosition, out Position newPosition)
        {
            return myMaizeNavigationHelper.TryAndMoveDown(currentPosition, out newPosition);
        }

        public bool TryAndMoveUp(Position currentPosition, out Position newPosition)
        {
            return myMaizeNavigationHelper.TryAndMoveUp(currentPosition, out newPosition);
        }

        public void AutoSolveMaze()
        { 
            myMaizeNavigationHelper.AutoSolveMaze();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}

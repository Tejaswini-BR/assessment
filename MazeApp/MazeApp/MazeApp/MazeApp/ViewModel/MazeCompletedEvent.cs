using System;
using System.Collections.Generic;
using MazeApp.Models;

namespace MazeApp.ViewModel
{
    public class MazeCompletedEvent : EventArgs
    {
        public IList<Position> Positions { get; }
        public bool MaizeSolved { get; }

        public MazeCompletedEvent(IList<Position> positions,bool isSolved)
        {
            Positions = positions;
            MaizeSolved = isSolved;
        }
    }
}

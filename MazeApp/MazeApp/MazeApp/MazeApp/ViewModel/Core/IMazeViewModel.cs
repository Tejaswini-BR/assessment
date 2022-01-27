using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeApp.Models;

namespace MazeApp.ViewModel.Core
{
    public interface IMazeViewModel
    {
        IMaze Maze { get; }

        AutoSolveCommand AutoSolveCommand { get; set; }

        event EventHandler<MazeCompletedEvent> MaizeCompleted;

        bool MaizeCompletedSuccessfully { get;  }

        Position Position { get; set; }

        char GridValue { get; set; }

        bool TryAndMoveRight(Position currentPosition, out Position newPosition);

        bool TryAndMoveLeft(Position currentPosition, out Position newPosition);

        bool TryAndMoveDown(Position currentPosition, out Position newPosition);

        bool TryAndMoveUp(Position currentPosition, out Position newPosition);

        void AutoSolveMaze();
    }
}

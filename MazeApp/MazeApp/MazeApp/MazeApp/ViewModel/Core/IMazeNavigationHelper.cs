using System;
using MazeApp.Models;

namespace MazeApp.ViewModel.Core
{
    public interface IMazeNavigationHelper
    {
        bool TryAndMoveRight(Position currentPosition, out Position newPosition);

        bool TryAndMoveUp(Position currentPosition, out Position newPosition);

        bool TryAndMoveLeft(Position currentPosition, out Position newPosition);

        bool TryAndMoveDown(Position currentPosition, out Position newPosition);

        event EventHandler<MazeCompletedEvent> MaizeCompleted;

        void AutoSolveMaze();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeApp.Models
{
    public interface IMaze
    {
        void UpdateMaze(int x,int y, char c);

        int NumberOfSpaces { get; set; }

        int NumberOfWalls { get; set; }

        Position EndPosition { get; set; }

        Position StartPosition { get; set; }

        char GetItemAtPosition(Position position);

        void ValidateMaze();

        bool IsValidMaze { get;}
    }
}

using MazeApp.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MazeApp.ViewModel
{
    public class AutoSolveCommand : ICommand
    {
        private IMazeViewModel myViewModel;
        public event EventHandler CanExecuteChanged;

        public AutoSolveCommand(IMazeViewModel vm)
        {
            myViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            myViewModel.AutoSolveMaze();
        }
    }
}

using MazeApp.Models;
using MazeApp.ViewModel;
using MazeApp.ViewModel.Core;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MazeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMazeViewModel myViewModel;
        public MainWindow(IMazeViewModel model)
        {
            InitializeComponent();
            myViewModel = model;
            DataContext = model;
            this.Loaded += OnLoaded;
            this.Unloaded += OnUnLoaded;
        }

        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            this.Unloaded -= OnUnLoaded;
            myViewModel.MaizeCompleted -= OnMaizeCompletedSuccessfully;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadMaze();
        }

        private void LoadMaze()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = string.Format("{0}Resources\\ExampleMaze.txt",
                                        System.IO.Path.GetFullPath(System.IO.Path.Combine(RunningPath,@"..\..\")));

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("MazeFile not found");
            }

            var gridData = File.ReadAllLines(filePath);

            for (int i = 0; i < gridData.Length; i++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = GridLength.Auto;
                MazeGrid.RowDefinitions.Add(rowDef);
                var row = gridData[i];

                for (int j = 0; j < row.Length; j++)
                {
                    myViewModel.Maze.UpdateMaze(i,j, row[j]);
                    var text = row[j];

                    var colDef = new ColumnDefinition();
                    colDef.Width = GridLength.Auto;
                    MazeGrid.ColumnDefinitions.Add(colDef);

                    var textControl = new TextBox();
                    textControl.Text = text.ToString();
                    textControl.IsReadOnly = true;
                    textControl.PreviewKeyDown += OnKeyDown;

                    if (text.Equals('S'))
                    {
                        textControl.Focusable = true;
                        textControl.Focus();
                        textControl.SelectAll();
                        
                        Keyboard.Focus(textControl);
                    }
                    Grid.SetColumn(textControl, j);
                    Grid.SetRow(textControl, i);
                    MazeGrid.Children.Add(textControl);
                }
            }

            myViewModel.Maze.ValidateMaze();
            myViewModel.MaizeCompleted += OnMaizeCompletedSuccessfully;


        }

        private void OnMaizeCompletedSuccessfully(object sender, MazeCompletedEvent e)
        {
            if (!e.MaizeSolved)
            {
                DisplayFailureLabel.Visibility = Visibility.Visible;
                return;
            }
            foreach (var position in e.Positions)
            {
                var control = GetGridControl(position.X, position.Y);
                ((TextBox)control).Background = Brushes.Aqua;
            }
            DisplayFailureLabel.Visibility = Visibility.Hidden;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var control = sender as TextBox;
            var x = Grid.GetRow(control);
            var y = Grid.GetColumn(control);
            var currentPosition = new Position(x, y);
            Position newPosition;

            if(!control.Text.Equals("S"))
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Right:
                    {
                        if (myViewModel.TryAndMoveRight(currentPosition, out newPosition))
                        {
                            Move(control, currentPosition, newPosition);
                        }
                        break;
                    }

                case Key.Left:
                    {
                        if (myViewModel.TryAndMoveLeft(currentPosition, out newPosition))
                        {
                            Move(control, currentPosition, newPosition);
                        }
                        break;
                    }
                case Key.Up:
                    {
                        if (myViewModel.TryAndMoveUp(currentPosition, out newPosition))
                        {
                            Move(control, currentPosition, newPosition);
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if (myViewModel.TryAndMoveDown(currentPosition, out newPosition))
                        {
                            Move(control, currentPosition, newPosition);
                        }
                        break;
                    }
            }
        }

        private void Move(UIElement currentControl, Position currentPosition, Position nextControlPosition)
        {
            var nextControl = GetGridControl(nextControlPosition.X, nextControlPosition.Y);
            if (null != nextControl)
            {
                SwapControls(currentControl, nextControl, currentPosition, nextControlPosition);
            }
        }


        private void SwapControls(UIElement control, UIElement nextControl,
                                    Position controlPosition, Position nextControlPosition)
        {
            Grid.SetRow(control, nextControlPosition.X);
            Grid.SetColumn(control, nextControlPosition.Y);
            Grid.SetColumn(nextControl, controlPosition.Y);
            Grid.SetRow(nextControl, controlPosition.X);
        }

        private UIElement GetGridControl(int row, int col)
        {
            var controls = MazeGrid.Children.Cast<UIElement>();
            var control = controls.Where(c => Grid.GetRow(c) == row && Grid.GetColumn(c) == col).FirstOrDefault();
            return control;

        }
    
    }
}

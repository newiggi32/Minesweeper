using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes; 

namespace MinesweeperWpf
{  
    public partial class wGameScene : Window
    {
        public struct Cell
        {
            public bool IsClicked { get; set; }
            public bool IsMine { get; set; }
            public int ButtonIndex { get; set; }

            public Cell(bool IsClickedCell = false, bool IsMineCell = false)
            {
                this.IsClicked = IsClickedCell;
                this.IsMine = IsMineCell;
                this.ButtonIndex = 0;
            }
        }

        public static Cell[,] cells;

        public wGameScene()
        {
            InitializeComponent();                                             
        }

        public void CreateMap(int LevelNumber)
        {            
            int Columns, Rows, Mines;
            switch (LevelNumber)
            {
                case 0:
                    Columns = 9;
                    Rows = 9;
                    Mines = 10;
                    break;
                case 1:
                    Columns = 16;
                    Rows = 16;
                    Mines = 40;
                    break;
                case 2:
                    Columns = 16;
                    Rows = 30;
                    Mines = 99;
                    break;
                default:
                    Columns = 0;
                    Rows = 0;
                    Mines = 0;
                    break;
            }

            txtMinesCount.Text = Mines.ToString();            

            cells = new Cell[Columns, Rows];

            Random r = new Random();

            for (int i = 0; i < Mines;)
            {
                int rColumn = r.Next(0, Columns);
                int rRow = r.Next(0, Rows);
                if (cells[rColumn, rRow].IsMine == false)
                {
                    cells[rColumn, rRow].IsMine = true;
                    i++;
                }
            }
                       
            for (int i = 0; i < Columns; i++) //разметка grid
            {
                SceneDG.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < Rows; i++)
            {
                SceneDG.RowDefinitions.Add(new RowDefinition());
            }


            Button[,] buttons = new Button[Columns, Rows]; //button в ячейках grid

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Button button = new Button();
                    
                    button.PreviewMouseLeftButtonDown += Button_PreviewMouseLeftButtonDown;
                    button.MouseRightButtonDown += Button_MouseRightButtonDown;
                    
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);

                    buttons[i, j] = button;
                    SceneDG.Children.Add(buttons[i, j]);

                    cells[i, j].ButtonIndex = SceneDG.Children.IndexOf(button);
                }
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            Button pressButton = sender as Button;
            Cell pressCell = new Cell();
            

            if (pressCell.IsClicked == false)
            {
                pressCell.IsClicked = true;
                if(pressCell.IsMine == true)
                {
                    GameOver();
                }
                else
                {
                    OpenCell();
                }
            }
            Console.WriteLine("---");
            

            Console.WriteLine(SceneDG.Children.IndexOf(pressButton));
            pressButton.Background = Brushes.Red;
        }

        private void OpenCell()
        {

        }

        private void GameOver()
        {

        }

        private void Button_MouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("---");
            Button pressButton = sender as Button;
            pressButton.Background = Brushes.Black;
            
            Console.WriteLine(SceneDG.Children.IndexOf(pressButton));
        }

        private void MenuRestart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuEasySize_Click(object sender, RoutedEventArgs e)
        {
            CreateMap(0);
        }

        private void MenuMediumSize_Click(object sender, RoutedEventArgs e)
        {
            CreateMap(1);
        }

        private void MenuHardSize_Click(object sender, RoutedEventArgs e)
        {
            CreateMap(2);
        }
    }


}

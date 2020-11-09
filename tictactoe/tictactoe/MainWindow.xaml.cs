using System;
using System.CodeDom;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private Members
        /// <summary>
        /// Holds current values of cells in the active game
        /// </summary>
        private MarkType[] Results;
        /// <summary>
        /// true if its players1 turn (x) or player2 turns (o)
        /// </summary>
        private bool Player1turn;
        /// <summary>
        /// true if game has ended
        /// </summary>
        private bool GameEnded;
        #endregion
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion
        /// <summary>
        /// starts a new game and clear all value to the start
        /// </summary>
        private void NewGame()
        {
            //ceate new array of cells
            Results = new MarkType[9];
            for(var i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Free;
                //make sure player1 starts game
                Player1turn = true;

                container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                });

                //make sure game hasnt finished
                GameEnded = false;
            }
        }
        /// <summary>
        /// Handles a button clicked event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the clicked</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //start a new game on click after its has finished
            if(GameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;
            //find the button position in an array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);
            //dont do anything if the cell already has a value in it
            if (Results[index] != MarkType.Free)
            {
                return;
            }
            //set the cell value based on players turn
            Results[index] = Player1turn ? MarkType.Cross : MarkType.Nought;
            //setbutton text to result
            button.Content = Player1turn ? "X" : "O";
            //cahnge the color of nought to green
            if (Player1turn)
            {
                button.Foreground = Brushes.Green;
            }
            else if (!Player1turn)
            {
                button.Foreground = Brushes.Yellow;
            }
            //toggle the players turn
            Player1turn ^= true;
            //check for a winner
            checkingWinner();
        }
        private void checkingWinner()
        {
            #region Horizontal wins
            //check for horizontal win
            // for first row
            if (Results[0] != MarkType.Free && (Results[0] & Results[1] & Results[2]) == Results[0]) 
            {
                GameEnded = true;

                //change Background color
                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Aquamarine;
            }

            //for second row
            if (Results[3] != MarkType.Free && (Results[3] & Results[4] & Results[5]) == Results[3])
            {
                GameEnded = true;

                //change Background color
                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Aquamarine;
            }

            // for third row
            if (Results[6] != MarkType.Free && (Results[6] & Results[7] & Results[8]) == Results[6])
            {
                GameEnded = true;

                //change Background color
                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Aquamarine;
            }
            #endregion

            #region Vertical wins
            // for first column
            if (Results[0] != MarkType.Free && (Results[3] & Results[1] & Results[6]) == Results[0])
            {
                GameEnded = true;

                //change Background color
                button0_0.Background = button0_1.Background = button0_2.Background = Brushes.Aquamarine;
            }

            //second column
            if (Results[1] != MarkType.Free && (Results[1] & Results[4] & Results[7]) == Results[1])
            {
                GameEnded = true;

                //change Background color
                button1_0.Background = button1_1.Background = button1_2.Background = Brushes.Aquamarine;
            }

            // third column
            if (Results[2] != MarkType.Free && (Results[2] & Results[5] & Results[8]) == Results[2])
            {
                GameEnded = true;

                //change Background color
                button2_0.Background = button2_1.Background = button2_2.Background = Brushes.Aquamarine;
            }
            #endregion
            #region Diagonal wins
            // first diagonal wins
            if (Results[0] != MarkType.Free && (Results[0] & Results[4] & Results[8]) == Results[0])
            {
                GameEnded = true;

                //change Background color
                button0_0.Background = button1_1.Background = button2_2.Background = Brushes.Aquamarine;
            }

            // second diagonal win
            if (Results[2] != MarkType.Free && (Results[2] & Results[4] & Results[6]) == Results[2])
            {
                GameEnded = true;

                //change Background color
                button0_2.Background = button1_1.Background = button2_0.Background = Brushes.Aquamarine;
            }
            #endregion
            //check for no winner and full board
            if (!Results.Any(Results => Results == MarkType.Free))
            {
                GameEnded = true;
                //changing color
                container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Red;
                });
                   
            }
        }
    }
}

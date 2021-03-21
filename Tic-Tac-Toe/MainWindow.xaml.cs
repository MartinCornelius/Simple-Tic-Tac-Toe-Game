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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
	public partial class MainWindow : Window
	{
		private string player = "O";
		private int xWins = 0;
		private int oWins = 0;
		private static readonly Brush DEFAULTBRUSH = new SolidColorBrush(Color.FromArgb(255, 142, 142, 166));
		
		public MainWindow()
		{
			InitializeComponent();
		}

		private void restartClick(object sender, RoutedEventArgs e)
		{
			xWins = 0;
			oWins = 0;
			lblXWins.Content = "X: 0";
			lblOWins.Content = "O: 0";
			ResetTiles();
		}

		private void exitClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void tileClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			btn.Foreground = Brushes.Black;
			btn.IsEnabled = false;
			
			//Check Row for win
			if (IsWin(tileA1, tileA2, tileA3)) GameOver(tileA1.Content.ToString());
			if (IsWin(tileB1, tileB2, tileB3)) GameOver(tileB1.Content.ToString());
			if (IsWin(tileC1, tileC2, tileC3)) GameOver(tileC1.Content.ToString());

			//Check Column for win
			if (IsWin(tileA1, tileB1, tileC1)) GameOver(tileA1.Content.ToString());
			if (IsWin(tileA2, tileB2, tileC2)) GameOver(tileA2.Content.ToString());
			if (IsWin(tileA3, tileB3, tileC3)) GameOver(tileA3.Content.ToString());

			//Check Diagonal for win
			if (IsWin(tileA1, tileB2, tileC3)) GameOver(tileA1.Content.ToString());
			if (IsWin(tileA3, tileB2, tileC1)) GameOver(tileA3.Content.ToString());

			//Check for draw (no tiles left)
			if (!tileA1.IsEnabled && !tileA2.IsEnabled && !tileA3.IsEnabled &&
				!tileB1.IsEnabled && !tileB2.IsEnabled && !tileB3.IsEnabled &&
				!tileC1.IsEnabled && !tileC2.IsEnabled && !tileC3.IsEnabled)
			{
				GameOver("");
			}

			//Change player
			if (player == "X")
			{
				player = "O";
			}
			else
			{
				player = "X";
			}
		}

		private void GameOver(string player)
		{
			if (lblWinner.Visibility == Visibility.Visible)
			{
				return;
			}

			if (player == "X")
			{
				lblWinner.Content = "Player X Wins!";
				lblXWins.Content = $"X: {++xWins}";
			}
			else if (player == "O")
			{
				lblWinner.Content = "Player O Wins!";
				lblOWins.Content = $"O: {++oWins}";
			}
			else
			{
				lblWinner.Content = "DRAW!!!";
			}
			lblWinner.Visibility =  Visibility.Visible;
			DelayedRestart();
		}

		private async void DelayedRestart()
		{
			ResetTiles();
			await Task.Delay(1000);
		}

		private void ResetTiles()
		{
			ResetTile(tileA1);
			ResetTile(tileA2);
			ResetTile(tileA3);
			ResetTile(tileB1);
			ResetTile(tileB2);
			ResetTile(tileB3);
			ResetTile(tileC1);
			ResetTile(tileC2);
			ResetTile(tileC3);
		}

		private void ResetTile(Button tile)
		{
			tile.Content = "";
			tile.IsEnabled = true;
			tile.Foreground = DEFAULTBRUSH;
		}

		private bool IsWin(Button btn1, Button btn2, Button btn3) =>
			!btn1.IsEnabled && btn1.Content == btn2.Content && btn1.Content == btn3.Content;

		private void tileEnter(object sender, MouseEventArgs e)
		{
			Button tile = (Button)sender;
			tile.Content = player;
		}

		private void tileLeave(object sender, MouseEventArgs e)
		{
			Button tile = (Button)sender;
			if (tile.IsEnabled)
			{
				tile.Content = "";
			}
		}

		private void lblWinner_MouseDown(object sender, MouseButtonEventArgs e)
		{
			ResetTiles();
			lblWinner.Visibility = Visibility.Hidden;
		}
	}
}

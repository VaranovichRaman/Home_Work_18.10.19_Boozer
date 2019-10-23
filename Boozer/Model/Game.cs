using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Boozer.Model
{
    public class Game
    {
        public List<Card> CardDeck = new List<Card>(36);
        public List<Card> Player1Cards = new List<Card>();
        public List<Card> Player2Cards = new List<Card>();
        public List<Card> TempCards = new List<Card>();
        int countOfRepeats = 0;
        public void SetPlayers()
        {            
            if (countOfRepeats == 0)
            {
                Console.WriteLine($"Welcome to the game \"Boozer\". This game helps to resolve any conflicts without killing " +
                    $"and destroying cities! Let's start Boozer game!\n");
                countOfRepeats = 1;
            }
            else
            {
                Console.WriteLine($"Who will be next?\n");
            }
            Console.WriteLine($"Choose Player's 1 name:\n");
            string player1Name = Console.ReadLine();
            Console.WriteLine($"Choose Player's 2 name:\n");
            string player2Name = Console.ReadLine();
            GameStart(player1Name, player2Name);
        }
        public void CreatingDeck()
        {
            CardDeck = new List<Card>();
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (Values value in Enum.GetValues(typeof(Values)))
                {
                    CardDeck.Add(new Card { CardSuit = suit, CardValue = value });
                }
            }
        }
        public void Shuffle()
        {
            CardDeck = CardDeck.OrderBy(x => Guid.NewGuid()).ToList();
        }
        public void SplitCardDeck()
        {
            Player1Cards = CardDeck.GetRange(0, CardDeck.Count / 2);
            Player2Cards = CardDeck.GetRange(18, CardDeck.Count / 2);
        }
        public void GameStart(string player1, string player2)
        {
            int counter = 0;
            while (Player1Cards.Count != 0 && Player2Cards.Count != 0)
            {
                Console.WriteLine($"{player1}'s 1 card: {Player1Cards[0].CardValue} {Player1Cards[0].Mark} {Player1Cards[0].CardSuit}, " +
                    $"and {player2}'s 2 card: {Player2Cards[0].CardValue} {Player2Cards[0].Mark} {Player2Cards[0].CardSuit}");
                Thread.Sleep(10);
                if ((int)Player1Cards[0].CardValue > (int)Player2Cards[0].CardValue)
                {
                    if (Player1Cards[0].CardValue == Values.Ace && Player2Cards[0].CardValue == Values.Six)
                    {
                        Player2Cards.Add(Player2Cards[0]);
                        Player2Cards.Add(Player1Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player1Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"{player2} take cards!\n");
                        Console.WriteLine($"{player1} have {Player1Cards.Count} cards in his deck | " +
                           $"{player2} have {Player2Cards.Count} cards in his deck!\n");
                        //Console.ReadLine(); //for game controlling & debugging 
                    }
                    else
                    {
                        Player1Cards.Add(Player1Cards[0]);
                        Player1Cards.Add(Player2Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player1Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"{player1} take cards!\n");
                        Console.WriteLine($"{player1} have {Player1Cards.Count} cards in his deck | " +
                            $"{player2} have {Player2Cards.Count} cards in his deck!\n");
                        //Console.ReadLine(); //for game controlling & debugging 
                    }
                }
                else if ((int)Player1Cards[0].CardValue < (int)Player2Cards[0].CardValue)
                {
                    if (Player2Cards[0].CardValue == Values.Ace && Player1Cards[0].CardValue == Values.Six)
                    {
                        Player1Cards.Add(Player1Cards[0]);
                        Player1Cards.Add(Player2Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player1Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"{player1} take cards!\n");
                        Console.WriteLine($"{player1} have {Player1Cards.Count} cards in his deck | " +
                            $"{player2} have {Player2Cards.Count} cards in his deck!\n");
                        //Console.ReadLine(); //for game controlling & debugging 
                    }
                    else
                    {
                        Player2Cards.Add(Player2Cards[0]);
                        Player2Cards.Add(Player1Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player2Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"{player2} take cards!\n");
                        Console.WriteLine($"{player1} have {Player1Cards.Count} cards in his deck | " +
                           $"{player2} have {Player2Cards.Count} cards in his deck!\n");
                        // Console.ReadLine(); //for game controlling & debugging 
                    }
                }
                else
                {
                    TempCards.Add(Player1Cards[0]);
                    TempCards.Add(Player2Cards[0]);
                    Player1Cards.Remove(Player1Cards[0]);
                    Player2Cards.Remove(Player2Cards[0]);
                    Console.WriteLine($"Draw! This cards will be taken by player who win next round!\n");
                    //Console.ReadLine();//for game controlling & debugging 
                }
                counter++;
            }
            if (Player1Cards.Count == 0)
            {
                Console.WriteLine($"{player2} WON in {counter} steps!!!");
                WriteHistory(player2, player1, counter);
            }
            else
            {
                Console.WriteLine($"{player1} WON in {counter} steps!!!");
                WriteHistory(player1, player2, counter);
            }
            counter = 0;
        }
        public void RepeatGame()
        {
            bool flagForRepeat = true;
            Console.WriteLine($"Do you want play again?(type \"y\" if yes, \"n\" if no, " +
                $"\"h\" if you want see game history, \"l\" if you want see game leaderboard):\n");
            while (flagForRepeat)
            {
                string answerForRepeat = Console.ReadLine().ToLower();
                if (answerForRepeat == "y")
                {
                    Console.WriteLine($"\nNice, let's go!\n");
                    Boozer();
                    flagForRepeat = false;
                }
                else if (answerForRepeat == "n")
                {
                    Console.WriteLine($"\nSee you next time!\n");
                    flagForRepeat = false;
                }
                else if (answerForRepeat == "h")
                {
                    ShowHistory();
                    Console.WriteLine($"\nDo you want play again?(type \"y\" if yes, \"n\" if no, " +
                $"\"h\" if you want see game history, \"l\" if you want see game leaderboard):\n");
                }
                else if (answerForRepeat == "l")
                {
                    SortAndShowLeaderboard();
                    Console.WriteLine($"\nDo you want play again?(type \"y\" if yes, \"n\" if no, " +
                $"\"h\" if you want see game history, \"l\" if you want see game leaderboard):\n");
                }
                else
                {
                    Console.WriteLine($"\nWrong key! Type \"y\" if yes, \"n\" if no, " +
                $"\"h\" if you want see game history, \"l\" if you want see game leaderboard):\n");
                }
            }
        }
        public static void WriteHistory(string winner, string looser, int steps)
        {
            string addHistory = $"Battle {winner} VS {looser}, and {winner} won in {steps} steps" + Environment.NewLine;
            File.AppendAllText("../../history.txt", addHistory, Encoding.Default);
            WriteLeaderboard(winner, steps);
        }
        public static void ShowHistory()
        {
            Console.WriteLine($"-------[GAME HISTORY]-------");
            using (StreamReader readHistory = new StreamReader("../../history.txt", Encoding.Default))
            {
                string historyList = readHistory.ReadToEnd();
                Console.WriteLine(historyList);
            }
        }
        public static void WriteLeaderboard(string winner, int steps)
        {
            string addLeaderboard = $"{steps}:{winner}'s steps" + Environment.NewLine;
            File.AppendAllText("../../leaderboard.txt", addLeaderboard, Encoding.Default);
        }
        public static void SortAndShowLeaderboard()
        {
            string[] sortedScoreLines =
            File.ReadAllLines("../../leaderboard.txt", Encoding.Default);
            var parsedPersons = from s in sortedScoreLines
                                select new
                                {
                                    Steps = int.Parse(s.Split(':')[0]),
                                    Name = s.Split(':')[1]
                                };
            var sortedPersons = parsedPersons.OrderBy(o => o.Steps).ThenBy(i => i.Name);
            var result = (from s in sortedPersons select s.Steps + ":" + s.Name).ToArray();
            Console.WriteLine($"-------[LEADERBOARD]-------");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
        public void Boozer()
        {
            CreatingDeck();
            Shuffle();
            SplitCardDeck();
            SplitCardDeck();
            SetPlayers();
            RepeatGame();
        }
    }

}

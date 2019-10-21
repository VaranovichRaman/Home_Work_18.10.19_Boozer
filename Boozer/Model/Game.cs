using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Player1Cards = CardDeck.GetRange(0, CardDeck.Count/2);
            Player2Cards = CardDeck.GetRange(18, CardDeck.Count/2);
        }
        public void GameStart()
        {
            int counter = 0;
            while (Player1Cards.Count != 0 && Player2Cards.Count != 0)
            {
                Console.WriteLine($"Player's 1 card: {Player1Cards[0].CardValue} {Player1Cards[0].Mark} {Player1Cards[0].CardSuit}, " +
                    $"and Player's 2 card: {Player2Cards[0].CardValue} {Player2Cards[0].Mark} {Player2Cards[0].CardSuit}");
                Thread.Sleep(300);
                if ( (int) Player1Cards[0].CardValue > (int) Player2Cards[0].CardValue)
                {
                    if (Player1Cards[0].CardValue == Values.Ace && Player2Cards[0].CardValue == Values.Six)
                    {
                        Player2Cards.Add(Player2Cards[0]);
                        Player2Cards.Add(Player1Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player1Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"Player 2 take cards!\n");
                        Console.WriteLine($"Player 1 have {Player1Cards.Count} cards in his deck | " +
                           $"Player2 have {Player2Cards.Count} cards in his deck!\n");
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
                        Console.WriteLine($"Player 1 take cards!\n");
                        Console.WriteLine($"Player 1 have {Player1Cards.Count} cards in his deck | " +
                            $"Player2 have {Player2Cards.Count} cards in his deck!\n");
                        //Console.ReadLine(); //for game controlling & debugging 
                    }
                }
                else if((int)Player1Cards[0].CardValue < (int)Player2Cards[0].CardValue)
                {
                    if (Player2Cards[0].CardValue == Values.Ace && Player1Cards[0].CardValue == Values.Six)
                    {
                        Player1Cards.Add(Player1Cards[0]);
                        Player1Cards.Add(Player2Cards[0]);
                        Player1Cards.Remove(Player1Cards[0]);
                        Player2Cards.Remove(Player2Cards[0]);
                        Player1Cards.AddRange(TempCards.GetRange(0, TempCards.Count));
                        TempCards.Clear();
                        Console.WriteLine($"Player 1 take cards!\n");
                        Console.WriteLine($"Player 1 have {Player1Cards.Count} cards in his deck | " +
                            $"Player2 have {Player2Cards.Count} cards in his deck!\n");
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
                        Console.WriteLine($"Player 2 take cards!\n");
                        Console.WriteLine($"Player 1 have {Player1Cards.Count} cards in his deck | " +
                           $"Player2 have {Player2Cards.Count} cards in his deck!\n");
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
                Console.WriteLine($"Player 2 WIN for {counter} steps!!!");
            }
            else
            {
                Console.WriteLine($"Player 1 WIN for {counter} steps!!!");
            }
            counter = 0;
        }
        public void Boozer()
        {
            CreatingDeck();
            Shuffle();
            SplitCardDeck();
            SplitCardDeck();
            GameStart();
        }
    }

}

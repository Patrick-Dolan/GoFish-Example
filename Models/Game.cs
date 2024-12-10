using System.Collections.Generic;

namespace GoFish.Models
{
  public class Game
  {
    private static Game _instance;
    public string Name { get; } = "Go Fish";
    public Deck Deck { get; set; } = new Deck();
    public Player Player1 { get; set; } = new Player();
    public Player Player2 { get; set; } = new Player();
    public bool CurrentPlayerDrawCard { get; set; } = false;
    private int MinHandSize { get; } = 5;
    public string CurrentPlayer { get; set; } = "Player1";

    public static Game GetInstance()
    {
      if (_instance == null)
      {
        _instance = new Game();
        _instance.StartGame();
      }
      return _instance;
    }

    public void StartGame()
    {
      Player1.Hand = Deck.DrawCards(MinHandSize);
      Player2.Hand = Deck.DrawCards(MinHandSize);
    }

    public void ResetGame()
    {
      Deck = new Deck();
      Player1 = new Player();
      Player2 = new Player();
      CurrentPlayerDrawCard = false;
      StartGame();
    }

    public void SwitchPlayerTurn()
    {
      CurrentPlayer = CurrentPlayer == "Player1" ? "Player2" : "Player1";
    }

    public void AskCard(string cardValue, string cardSuit)
    {
      // Loop through computer hand checking for match
      for (int i = 0; i < Player2.Hand.Count; i++)
      {
        if (Player2.Hand[i].Value == cardValue)
        {
          Player2.Hand.RemoveAt(i);
          // Loop through player hand and remove matched card
          for (int j = 0; j < Player1.Hand.Count; j++)
          {
            if (Player1.Hand[j].Value == cardValue && Player1.Hand[j].Suit == cardSuit)
            {
              Player1.Hand.RemoveAt(j);
            }
          }
          // Increase pairs if found
          Player1.Pairs++;
          break;
        }
      }
      // Set GoFish so user must draw cards
      if (Player1.Hand.Count < MinHandSize)
      {
        CurrentPlayerDrawCard = true;
      }
    }

    public void DrawToMinHandSize()
    {
      if (Player1.Hand.Count < MinHandSize)
      {
        List<Card> drawnCards = Deck.DrawCards(MinHandSize - Player1.Hand.Count);
        Player1.Hand.AddRange(drawnCards);
        CurrentPlayerDrawCard = false;
      }
    }
  }
}
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
    public readonly int MinHandSize = 5;
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
      CurrentPlayer = "Player1";
      StartGame();
    }

    public void SwitchPlayerTurn()
    {
      CurrentPlayer = CurrentPlayer == "Player1" ? "Player2" : "Player1";
    }

    // AskCard determines current player, and looks for pairs in opposing player hand.
    // If a pair is found the cards are removed from players hands and pairs prop is
    // increased for asking player.
    public void AskCard(string cardValue, string cardSuit)
    {
      Player currentPlayer = CurrentPlayer == "Player1" ? Player1 : Player2;
      Player askedPlayer = CurrentPlayer == "Player1" ? Player2 : Player1;

      bool matchFound = false;
      // Loop through askedPlayer hand checking for match
      for (int i = 0; i < askedPlayer.Hand.Count; i++)
      {
        if (askedPlayer.Hand[i].Value == cardValue)
        {
          matchFound = true;
          currentPlayer.Pairs++;
          // Remove cards from askedPlayer and currentPlayer
          askedPlayer.Hand.RemoveAt(i);
          for (int j = 0; j < currentPlayer.Hand.Count; j++)
          {
            if (currentPlayer.Hand[j].Value == cardValue && currentPlayer.Hand[j].Suit == cardSuit)
            {
              currentPlayer.Hand.RemoveAt(j);
            }
          }
          break;
        }
      }
      // Set CurrentPlayerDrawCard so user must draw cards
      if (currentPlayer.Hand.Count < MinHandSize || !matchFound)
      {
        CurrentPlayerDrawCard = true;
      }
    }

    // TODO fix so that if the deck is empty the change turns instead?
    // Might make more sense todo that logic in controller.
    public void DrawToMinHandSize()
    {
      if (Player1.Hand.Count < MinHandSize)
      {
        List<Card> drawnCards = Deck.DrawCards(MinHandSize - Player1.Hand.Count);
        Player1.Hand.AddRange(drawnCards);
        CurrentPlayerDrawCard = false;
      }
      if (Player2.Hand.Count < MinHandSize)
      {
        List<Card> drawnCards = Deck.DrawCards(MinHandSize - Player2.Hand.Count);
        Player2.Hand.AddRange(drawnCards);
        CurrentPlayerDrawCard = false;
      }
    }

    // Draws a card for the current player
    public void DrawCard()
    {
      Player currentPlayer = CurrentPlayer == "Player1" ? Player1 : Player2;
      Card drawnCard = Deck.DrawCards(1)[0];
      currentPlayer.Hand.Add(drawnCard);
      CurrentPlayerDrawCard = false;
    }
  }
}
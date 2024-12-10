using System.Collections.Generic;

namespace GoFish.Models
{
  public class Game
  {
    private static Game _instance;
    public string Name {get; set;} = "My Game:";
    public string Test {get; set;} = "WOOOO";
    public Deck Deck { get; set; } = new Deck();
    public Player Player { get; set; } = new Player();
    public Player Computer { get; set; } = new Player();
    public bool GoFish { get; set; } = false;
    private int MinHandSize { get; } = 5;

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
      Player.Hand = Deck.DrawCards(MinHandSize);
      Computer.Hand = Deck.DrawCards(MinHandSize);
    }

    public void AskCard(string cardValue, string cardSuit)
    {
      // Loop through computer hand checking for match
      for (int i = 0; i < Computer.Hand.Count; i++)
      {
        if (Computer.Hand[i].Value == cardValue)
        {
          Computer.Hand.RemoveAt(i);
          // Loop through player hand and remove matched card
          for (int j = 0; j < Player.Hand.Count; j++)
          {
            if (Player.Hand[j].Value == cardValue && Player.Hand[j].Suit == cardSuit)
            {
              Player.Hand.RemoveAt(j);
            }
          }
          // Increase pairs if found
          Player.Pairs++;
          break;
        }
      }
      // Set GoFish so user must draw cards
      if (Player.Hand.Count < MinHandSize)
      {
        GoFish = true;
      }
    }

    public void DrawToMinHandSize()
    {
      if (Player.Hand.Count < MinHandSize)
      {
        List<Card> drawnCards = Deck.DrawCards(MinHandSize - Player.Hand.Count);
        Player.Hand.AddRange(drawnCards);
        GoFish = false;
      }
    }
  }
}
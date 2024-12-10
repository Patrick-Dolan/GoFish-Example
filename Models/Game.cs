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
      Player.Hand = Deck.DrawCards(5);
      Computer.Hand = Deck.DrawCards(5);
    }

    public void AskCard(string cardValue)
    {
      for (int i = 0; i < Computer.Hand.Count; i++)
      {
        if (Computer.Hand[i].Value == cardValue)
        {
          Player.Hand.RemoveAt(i);
          Computer.Hand.RemoveAt(i);
          Player.Pairs++;
          i--; // Adjust index after removal
        }
      }
    }
  }
}
namespace GoFish.Models
{
  public class Card
  {
    public string Value { get; set; }
    public string Suit { get; set; }

    public Card(string value, string suit)
    {
      Value = value;
      Suit = suit;
    }
  }
}
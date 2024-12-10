using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Deck
  {
    public List<Card> Cards { get; set; } = new List<Card> {};
    public List<Card> ShuffledCards { get; set; } = new List<Card> {};

    public Deck()
    {
      GenerateDeck();
      ShuffledCards = new List<Card>(Cards);
      ShuffleDeck();
    }

    private void GenerateDeck()
    {
      List<string> suits = new List<string> {"Hearts", "Diamonds", "Clubs", "Spades"};
      List<string> values = new List<string> {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};

      foreach (string suit in suits)
      {
        foreach (string value in values)
        {
          Card newCard = new Card(value, suit);
          Cards.Add(newCard);
        }
      }
    }

    // Wikipedia for Fisher Yates algo used to shuffle a finite list: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
    // Article Dan shared of Fisher Yates algo in C#: https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/
    private void ShuffleDeck()
    {
      Random r = new Random();
      // Count down from the end of the list given
      for (int n = ShuffledCards.Count - 1; n > 0; --n)
      {
        // Randomly pick an item index that hasn't been shuffled yet
        int k = r.Next(n + 1);

        // Swap the selected Card with the currently evaluated index in the loop 
        Card temp = ShuffledCards[n];
        ShuffledCards[n] = ShuffledCards[k];
        ShuffledCards[k] = temp;
      }
    }

    public List<Card> DrawCards(int num)
    {
      List<Card> drawnCards = new List<Card>();
      if (num <= 0)
      {
        return drawnCards;
      }
      for (int i = 0; num > i; i++)
      {
        drawnCards.Add(ShuffledCards[0]);
        ShuffledCards.RemoveAt(0);
      }
      return drawnCards;
    }
  }
}
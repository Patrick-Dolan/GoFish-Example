using System.Collections.Generic;
using GoFish.Models;

namespace GoFish.Models
{
  public class Player
  {
    public List<Card> Hand { get; set; } 
    public int Pairs { get; set; } = 0;
  }
}
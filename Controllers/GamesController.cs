using Microsoft.AspNetCore.Mvc;
using GoFish.Models;

namespace GoFish.Controllers
{
  public class GamesController : Controller
  {
    [HttpGet("/games")]
    public ActionResult Index()
    {
      Game gameObj = Game.GetInstance();
      return View(gameObj);
    }

    [HttpPost("/take-turn")]
    public ActionResult TakeTurn(string cardValue, string cardSuit)
    {
      Game gameObj = Game.GetInstance();
      gameObj.AskCard(cardValue, cardSuit);
      return RedirectToAction("Index");
    }

    [HttpPost("/draw")]
    public ActionResult DrawCards()
    {
      Game gameObj = Game.GetInstance();
      if (gameObj.Player1.Hand.Count < gameObj.MinHandSize || gameObj.Player1.Hand.Count < gameObj.MinHandSize)
      {
        gameObj.DrawToMinHandSize();
      }
      else
      {
        gameObj.DrawCard();
        gameObj.SwitchPlayerTurn();
      }
      return RedirectToAction("Index");
    }

    [HttpPost("/reset-game")]
    public ActionResult ResetGame()
    {
      Game gameObj = Game.GetInstance();
      gameObj.ResetGame();
      return RedirectToAction("Index");
    }
  }
}
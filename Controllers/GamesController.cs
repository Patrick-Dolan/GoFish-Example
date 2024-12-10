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
      gameObj.AskCard(cardValue);
      return RedirectToAction("Index");
    }
  }
}
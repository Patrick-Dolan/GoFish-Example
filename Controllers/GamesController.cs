using Microsoft.AspNetCore.Mvc;
using GoFish.Models;
using System.Collections.Generic;

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
      gameObj.DrawToMinHandSize();
      return RedirectToAction("Index");
    }
  }
}
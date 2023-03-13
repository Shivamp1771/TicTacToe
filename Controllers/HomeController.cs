using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Module101TicTacToeApp.Models;
using System.Diagnostics;
using System.Web;


namespace Module101TicTacToeApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            var session = new AppSession(HttpContext.Session);

            List<string> blocks = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                blocks.Add("-");
            }
            if (session.GetCurrentTurn() == "O")
            {
                session.SetCurrentTurn("X");
            }
            else if (session.GetCurrentTurn() == "X")
            {
                session.SetCurrentTurn("O");
            }
            else
            {
                session.SetCurrentTurn("X");
            }
            if (session.GetBlocks() == null || session.GetBlocks()?.Count == 0)
            {
                session.SetBlocks(blocks);
            }
            var model = new TicTacToe
            {
                result = null,
                currentTurn = session.GetCurrentTurn(),
                currentTurnLabel = session.GetCurrentTurn() == "X" ? "X's turn" : "O's turn",
                blocks = session.GetBlocks()
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult Index(string id)
        {

            string[] newValue = id.Split('-');
            var session = new AppSession(HttpContext.Session);
            var tempBlocks = session.GetBlocks();


            if (tempBlocks?.Count > 0)
            {
                tempBlocks[Int16.Parse(newValue[0])] = newValue[1];
                session.SetBlocks(tempBlocks);
            }
            session.SetCurrentTurn(newValue[1] == "X" ? "O" : "X");

            var model = new TicTacToe
            {
                result = null,
                currentTurn = session.GetCurrentTurn(),
                currentTurnLabel = session.GetCurrentTurn() == "X" ? "X's turn" : "O's turn",
                blocks = session.GetBlocks()
            };


            if (tempBlocks?.Count > 0)
            {
                int[][] checks = new int[8][] {
                    new int[3] { 0, 1, 2 },
                    new int[3] { 3, 4, 5},
                    new int[3] { 6, 7, 8},
                    new int[3] { 0, 3, 6},
                    new int[3] { 1, 4, 7},
                    new int[3] { 2, 5, 8},
                    new int[3] { 0, 4, 8},
                    new int[3] { 2, 4, 6},
                };

                bool isMatched = false;
                int x = 0;
                int o = 0;
                for (int i = 0; i < checks.Length; i++)
                {
                    x = 0;
                    o = 0;
                    for (int j = 0; j < checks[i].Length; j++)
                    {
                        if (tempBlocks[checks[i][j]] == "X")
                        {
                            x++;
                        }
                        if (tempBlocks[checks[i][j]] == "O")
                        {
                            o++;
                        }
                        if (x >= 3 || o >= 3)
                        {
                            isMatched = true;
                            break;
                        }
                    }
                    if (isMatched)
                    {
                        break;
                    }
                }

                if (isMatched)
                {

                    model.result = x >= 3 ? "X wins!" : "O wins!";
                    model.currentTurn = session.GetCurrentTurn();
                    model.currentTurnLabel = session.GetCurrentTurn() == "X" ? "X's turn" : "O's turn";
                    model.blocks = session.GetBlocks();

                }
                else if (tempBlocks.FindAll(b => b == "-").Count == 0)
                {
                    model.result = "It's a Tie";
                    model.currentTurn = session.GetCurrentTurn();
                    model.currentTurnLabel = session.GetCurrentTurn() == "X" ? "X's turn" : "O's turn";
                    model.blocks = session.GetBlocks();

                }
            }

            return View(model);
        }

        [HttpGet]
        public RedirectToActionResult Reset(string id)
        {
            var session = new AppSession(HttpContext.Session);

            if (id == "reset")
            {
                session.ClearSession();
            }
            return RedirectToAction("Index");
        }
    }
}
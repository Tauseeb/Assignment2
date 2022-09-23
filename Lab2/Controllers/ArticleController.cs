using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Assignment2.Controllers
{
    public class ArticleController : Controller
    {
        public const string cmd = "Comments";

        public IActionResult Index()
        {
            ViewBag.CommentCount = GetComments().Count();

            var message = TempData["IsCommentSaved"];
            if (message != null)
            {
                ViewData["IsCommentSaved"] = (bool?)message;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Comments(string Comment)
        {
            if (string.IsNullOrWhiteSpace(Comment))
            {
                TempData["IsCommentSaved"] = false;
            }
            else
            {
                string sessionString = HttpContext.Session.GetString(cmd);
                List<string> comments = GetComments();
                comments.Add(Comment);
                HttpContext.Session.SetString("Comments", JsonSerializer.Serialize(comments));
                TempData["IsCommentSaved"] = true;
            }

            return RedirectToAction("Index");
        }

        private List<string> GetComments()
        {
            string sessionString = HttpContext.Session.GetString(cmd);

            List<string> comments = new List<string>();
            if (!string.IsNullOrWhiteSpace(sessionString))
            {
                comments = JsonSerializer.Deserialize<List<string>>(sessionString);
            }

            return comments;
        }
    }
}

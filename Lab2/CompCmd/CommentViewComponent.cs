using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lab2.Components
{
    public class CommentViewComponent : ViewComponent
    {
        public const string comment = "Comments";

        public IViewComponentResult Invoke()
        {
            string sessionString = HttpContext.Session.GetString("Comments");
            List<string> comments = new List<string>();
            if (!string.IsNullOrWhiteSpace(sessionString))
            {
                comments = JsonSerializer.Deserialize<List<string>>(sessionString);
            }

            return View("~/views/Comp/_commentss.cshtml", comments);
        }
    }
}

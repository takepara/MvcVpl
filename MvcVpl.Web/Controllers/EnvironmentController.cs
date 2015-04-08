using System.Web.Mvc;
using MvcVpl.Roslyn;

namespace MvcVpl.Web.Controllers
{
    public class EnvironmentController : Controller
    {
        [HttpPost]
        public ActionResult JavaScript(string source)
        {
            ViewBag.Response = source;
            return View();
        }

        [HttpPost]
        public ActionResult CSharp(string source)
        {
            var env = new RoslynEnvironment();
            ViewBag.Response = env.Execute(source);
            return View();
        }
    }
}
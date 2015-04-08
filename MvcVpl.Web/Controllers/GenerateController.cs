using System.Web.Mvc;
using MvcVpl.Ast;
using MvcVpl.Generators;

namespace MvcVpl.Web.Controllers
{
    public class GenerateController : Controller
    {
        [HttpPost]
        public ActionResult JavaScript(Block block)
        {
            var generator = new JavaScriptGenerator();
            var generated = generator.Generate(block);

            return Content(generated);
        }

        [HttpPost]
        public ActionResult CSharp(Block block)
        {
            var generator = new CSharpGenerator();
            var generated = generator.Generate(block);

            return Content(generated);
        }
    }
}
using System;
using System.Linq;
using RazorEngine.Configuration;
using RazorEngine.Roslyn;
using RazorEngine.Templating;

namespace MvcVpl.Roslyn
{
    public class RoslynTemplateBase : TemplateBase
    {
        public int Length(string str)
        {
            return str.Length;
        }

        public string CharExchange(string str, int src, int dst)
        {
            var chars = str.ToCharArray();
            var charTemp = chars[src];
            chars[src] = chars[dst];
            chars[dst] = charTemp;
            return new String(chars);
        }
    }
    public class RoslynEnvironment : IEnvironment
    {
        private const string _compileTemplate = @"
@functions {{
  object Render(){{
    {0}
  }}
}}
@string.Format(""{{0}}"", Render())
";
        private readonly bool _initialized = false;
        private readonly object _initializLock = new object();
        private static IRazorEngineService _service;
        public RoslynEnvironment()
        {
            if (_initialized)
                return;

            lock (_initializLock)
            {
                if (_initialized)
                    return;

                var roslynConfig = new TemplateServiceConfiguration
                {
                    BaseTemplateType = typeof(RoslynTemplateBase),
                    CompilerServiceFactory = new RoslynCompilerServiceFactory()
                };
                _service = RazorEngineService.Create(roslynConfig);
                _initialized = true;
            }
        }

        public string Execute(string source)
        {
            var compileSource = string.Format(_compileTemplate, string.IsNullOrWhiteSpace(source) ? "return null;" : source);
            var templateKey = "Roslyn" + source.GetHashCode().ToString().Replace("-", "_");
            var result = "";
            try
            {
                result = _service.RunCompile(compileSource, templateKey, null, new { Name = templateKey });
            }
            catch (TemplateCompilationException tcex)
            {
                result = tcex.CompilerErrors.Aggregate(result, (current, error) => current + ("<b>" + error.ErrorNumber + "</b><br />" + error.ErrorText + "<br />"));
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}

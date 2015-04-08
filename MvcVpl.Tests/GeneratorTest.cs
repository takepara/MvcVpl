using System;
using Jil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcVpl.Ast;
using MvcVpl.Generators;

namespace MvcVpl.Tests
{
    [TestClass]
    public class GeneratorTest
    {
        string JsonSerialize(Block root)
        {
            var options = new Options(prettyPrint: true, excludeNulls: true);
            return JSON.Serialize(root, options);

        }

        [TestMethod]
        public void Json_Complex()
        {
            Console.WriteLine(JsonSerialize(Ast.Complex()));
        }

        [TestMethod]
        public void CSharp_Complex()
        {
            var generator = new CSharpGenerator();
            var generated = generator.Generate(Ast.Complex());

            Console.WriteLine(generated);
        }
        
        [TestMethod]
        public void JavaScript_Complex()
        {
            var generator = new JavaScriptGenerator();
            var generated = generator.Generate(Ast.Complex());

            Console.WriteLine(generated);
        }
    }
}

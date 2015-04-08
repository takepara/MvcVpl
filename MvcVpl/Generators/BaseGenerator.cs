using System;
using System.IO;
using MvcVpl.Ast;
using TypeCode = MvcVpl.Ast.TypeCode;

namespace MvcVpl.Generators
{
    public class GenerateContext : IDisposable
    {
        public Block Root { get; private set; }
        public TextWriter Response { get; private set; }
        public int Indent { get; set; }

        public string IndentString
        {
            get { return new string(' ', Indent*2); }
            
        }
        public GenerateContext(Block root)
        {
            Root = root;
            Response = new StringWriter();
            Indent = 0;
        }

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Response.Dispose();
            }

            _disposed = true;
        }
    }

    public abstract class BaseGenerator : IGenerator
    {
        protected abstract string Visit(GenerateContext context, Syntax syntax);

        protected string ValueReference(Syntax syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.Constant:
                    return syntax.Type == TypeCode.String
                        ? string.Format("\"{0}\"", syntax.Value)
                        : string.Format("{0}", syntax.Value);
                case SyntaxKind.VariableDeclaration:
                    return syntax.Name;
            }

            return null;
        }

        protected string Operation(Syntax syntax)
        {
            switch (syntax.OpCode)
            {
                case OperationCode.Addition:
                    return "+";
                case OperationCode.Subtraction:
                    return "-";
                case OperationCode.Multiplication:
                    return "*";
                case OperationCode.Division:
                    return "/";
                case OperationCode.Equal:
                    return "==";
                case OperationCode.GraterThan:
                    return ">";
                case OperationCode.GraterThanOrEqual:
                    return ">=";
                case OperationCode.LessThan:
                    return "<";
                case OperationCode.LessThanOrEqual:
                    return "<=";
            }

            return null;
        }

        void Visitor(GenerateContext context, Block block)
        {
            foreach (var syntax in block.SyntaxTree)
            {
                // 生成
                var generated = Visit(context, syntax);
                context.Response.Write(context.IndentString + generated);

                // 小ブロック再帰
                if (syntax.Block == null) 
                    continue;
                
                context.Response.WriteLine(context.IndentString + "{");

                context.Indent++;
                Visitor(context, syntax.Block);
                context.Indent--;

                context.Response.WriteLine(context.IndentString + "}");
            }
        }

        public string Generate(Block root)
        {
            using (var context = new GenerateContext(root))
            {
                Visitor(context, root);

                return context.Response.ToString();
            }
        }
    }
}

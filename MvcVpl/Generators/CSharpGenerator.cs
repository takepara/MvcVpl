using MvcVpl.Ast;

namespace MvcVpl.Generators
{
    public class CSharpGenerator : BaseGenerator
    {
        protected override string Visit(GenerateContext context, Syntax syntax)
        {
            var generated = default(string);
            string primary;
            string secondary;

            switch (syntax.Kind)
            {
                case SyntaxKind.Constant:
                    generated = string.Format("{0}", syntax.Value);
                    break;
                case SyntaxKind.Asignment:
                    if (string.IsNullOrEmpty(syntax.OpCode))
                    {
                        primary = ValueReference(syntax.Primary) ?? Visit(context, syntax.Primary);
                        generated = string.Format("{0} = {1};\r\n",
                            syntax.Name,
                            primary);
                    }
                    else
                    {
                        generated = Visit(context,new Syntax
                        {
                            Kind = SyntaxKind.Operation,
                            OpCode = syntax.OpCode,
                            Primary = syntax.Primary,
                            Secondary = syntax.Secondary
                        });
                    }
                    break;
                case SyntaxKind.Operation:
                    primary = ValueReference(syntax.Primary) ?? Visit(context, syntax.Primary);
                    secondary = ValueReference(syntax.Secondary) ?? Visit(context, syntax.Secondary);
                    generated = string.Format("({0} {1} {2})", 
                        primary, 
                        Operation(syntax),
                        secondary);
                    break;
                case SyntaxKind.VariableReference:
                    generated = string.Format("({0})",
                        syntax.Name);
                    break;
                case SyntaxKind.VariableDeclaration:
                    generated = string.Format("{0} {1};\r\n",
                        syntax.Type,
                        syntax.Name);
                    break;
                case SyntaxKind.If:
                    primary = ValueReference(syntax.Primary) ?? Visit(context, syntax.Primary);
                    generated = string.Format("if ({0})\r\n", 
                        primary);
                    break;
                case SyntaxKind.While:
                    primary = ValueReference(syntax.Primary) ?? Visit(context, syntax.Primary);
                    generated = string.Format("while ({0})\r\n", 
                        primary);
                    break;
                case SyntaxKind.Return:
                    primary = ValueReference(syntax.Primary) ?? Visit(context, syntax.Primary);
                    generated = string.Format("return ({0});\r\n", 
                        primary);
                    break;
                case SyntaxKind.Break:
                    generated = string.Format("break;\r\n");
                    break;
                case SyntaxKind.Continue:
                    generated = string.Format("continue;\r\n");
                    break;
            }

            return generated;
        }
    }
}

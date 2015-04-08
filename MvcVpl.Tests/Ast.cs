using MvcVpl.Ast;

namespace MvcVpl.Tests
{
    public class Ast
    {
        public static Block Complex()
        {
            var context = new Block();
            var var1 = Syntax.VariableDeclaration(TypeCode.String, "foo");
            var ref1 = Syntax.VariableReference("foo");
            var var2 = Syntax.VariableDeclaration(TypeCode.Integer, "woo");
            var ref2 = Syntax.VariableReference("woo");
            var ret = Syntax.Return(var1);
            var str1 = Syntax.Constant(TypeCode.String, "takepara");
            var num1 = Syntax.Constant(TypeCode.Integer, "5");
            var num2 = Syntax.Constant(TypeCode.Integer, "8");
            var num3 = Syntax.Constant(TypeCode.Integer, "10");
            context.SyntaxTree.Add(var1);
            context.SyntaxTree.Add(var2);
            context.SyntaxTree.Add(Syntax.Asignment("foo", str1));
            context.SyntaxTree.Add(Syntax.Asignment("woo", Syntax.Operation(num1, num2, OperationCode.Addition)));
            var ifBlock = new Block();
            ifBlock.SyntaxTree.Add(Syntax.Break());
            var if1 = Syntax.If(Syntax.Operation(ref2, num3, OperationCode.GraterThan), ifBlock);
            var if2 = Syntax.If(Syntax.Constant(TypeCode.Boolean, "true"), ifBlock);
            context.SyntaxTree.Add(if1);
            context.SyntaxTree.Add(if2);
            var loopBlock = new Block();
            loopBlock.SyntaxTree.Add(Syntax.Break());
            loopBlock.SyntaxTree.Add(Syntax.Continue());
            var loop1 = Syntax.While(Syntax.Operation(Syntax.Constant(TypeCode.Boolean, "true"), Syntax.Constant(TypeCode.Boolean, "false"), OperationCode.Equal), loopBlock);
            context.SyntaxTree.Add(loop1);
            context.SyntaxTree.Add(ret);
            
            return context;
        }
    }
}

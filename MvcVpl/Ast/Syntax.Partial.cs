namespace MvcVpl.Ast
{
    public partial class Syntax
    {
        public static Syntax Constant(string type, object value)
        {
            return new Syntax
            {
                Kind = SyntaxKind.Constant,
                Type = type,
                Value = value
            };
        }

        public static Syntax VariableDeclaration(string type, string name)
        {
            return new Syntax
            {
                Kind = SyntaxKind.VariableDeclaration,
                Type = type,
                Name = name
            };
        }

        public static Syntax VariableReference(string name)
        {
            return new Syntax
            {
                Kind = SyntaxKind.VariableReference,
                Name = name
            };
        }

        public static Syntax Operation(Syntax left, Syntax right, string operation)
        {
            return new Syntax
            {
                Kind = SyntaxKind.Operation,
                OpCode = operation,
                Primary = left,
                Secondary = right
            };
        }

        public static Syntax Asignment(string name, Syntax primary)
        {
            return new Syntax
            {
                Kind = SyntaxKind.Asignment,
                Name = name,
                Primary = primary
            };
        }

        public static Syntax If(Syntax expression, Block block)
        {
            return new Syntax
            {
                Kind = SyntaxKind.If,
                Primary = expression,
                Block = block
            };
        }

        public static Syntax While(Syntax expression, Block block)
        {
            return new Syntax
            {
                Kind = SyntaxKind.While,
                Primary = expression,
                Block = block
            };
        }

        public static Syntax Return(Syntax expression)
        {
            return new Syntax
            {
                Kind = SyntaxKind.Return,
                Primary = expression
            };
        }

        public static Syntax Break()
        {
            return new Syntax
            {
                Kind = SyntaxKind.Break
            };
        }

        public static Syntax Continue()
        {
            return new Syntax
            {
                Kind = SyntaxKind.Continue
            };
        }
    }
}

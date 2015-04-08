namespace MvcVpl.Ast
{
    public class TypeCode
    {
        public const string Boolean = "bool";
        public const string String = "string";
        public const string Integer = "int";
    }

    public class OperationCode
    {
        // Arithmetic
        public const string Addition = "add";
        public const string Subtraction = "sub";
        public const string Multiplication = "mul";
        public const string Division = "div";

        // Relational
        public const string Equal = "eq";
        public const string LessThan = "lt";
        public const string LessThanOrEqual = "lte";
        public const string GraterThan = "gt";
        public const string GraterThanOrEqual = "gte";
    }

    public class SyntaxKind
    {
        // Expression
        public const string Constant = "constant";
        public const string Operation = "operation";
        public const string Asignment = "assign";
        public const string VariableReference = "ref";

        // Statement
        public const string VariableDeclaration = "var";
        public const string If = "if";
        public const string While = "while";
        public const string Return = "return";
        public const string Break = "break";
        public const string Continue = "continue";

    }
}

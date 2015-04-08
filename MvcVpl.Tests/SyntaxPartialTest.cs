using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcVpl.Ast;

namespace MvcVpl.Tests
{
    [TestClass]
    public class SyntaxPartialTest
    {
        [TestMethod]
        public void Constant_Boolean()
        {
            var syntax = Syntax.Constant(TypeCode.Boolean, true);

            Assert.AreEqual(SyntaxKind.Constant, syntax.Kind);
            Assert.AreEqual(true, syntax.Value);
        }

        [TestMethod]
        public void Constant_Integer()
        {
            var syntax = Syntax.Constant(TypeCode.Integer , 123);

            Assert.AreEqual(SyntaxKind.Constant, syntax.Kind);
            Assert.AreEqual(123, syntax.Value);
        }

        [TestMethod]
        public void Constant_String()
        {
            var syntax = Syntax.Constant(TypeCode.Integer, "takepara");

            Assert.AreEqual(SyntaxKind.Constant, syntax.Kind);
            Assert.AreEqual("takepara", syntax.Value);
        }

        [TestMethod]
        public void VariableDeclaration_Boolean()
        {
            var syntax = Syntax.VariableDeclaration(TypeCode.Boolean, "foo");

            Assert.AreEqual(SyntaxKind.VariableDeclaration, syntax.Kind);
            Assert.AreEqual("foo", syntax.Name);
            Assert.AreEqual(TypeCode.Boolean, syntax.Type);
        }

        [TestMethod]
        public void VariableDeclaration_Integer()
        {
            var syntax = Syntax.VariableDeclaration(TypeCode.Integer, "foo");

            Assert.AreEqual(SyntaxKind.VariableDeclaration, syntax.Kind);
            Assert.AreEqual("foo", syntax.Name);
        }

        [TestMethod]
        public void Operation_IntInt()
        {
            var left = Syntax.Constant(TypeCode.Integer, 123);
            var right = Syntax.Constant(TypeCode.Integer, 123);
            var syntax = Syntax.Operation(left,right,OperationCode.Addition);

            Assert.AreEqual(SyntaxKind.Operation, syntax.Kind);
            Assert.AreEqual(OperationCode.Addition, syntax.OpCode);
            Assert.AreEqual(123, syntax.Primary.Value);
            Assert.AreEqual(TypeCode.Integer, syntax.Primary.Type);
            Assert.AreEqual(123, syntax.Secondary.Value);
            Assert.AreEqual(TypeCode.Integer, syntax.Secondary.Type);
        }

        [TestMethod]
        public void Operation_VarInt()
        {
            var left = Syntax.VariableReference("foo");
            var right = Syntax.Constant(TypeCode.Integer, 123);
            var syntax = Syntax.Operation(left, right, OperationCode.Addition);

            Assert.AreEqual(SyntaxKind.Operation, syntax.Kind);
            Assert.AreEqual(OperationCode.Addition, syntax.OpCode);
            Assert.AreEqual("foo", syntax.Primary.Name);
            Assert.AreEqual(123, syntax.Secondary.Value);
            Assert.AreEqual(TypeCode.Integer, syntax.Secondary.Type);
        }

        [TestMethod]
        public void Assign_Int()
        {
            var expression = Syntax.Constant(TypeCode.Integer, 123);
            var syntax = Syntax.Asignment("foo", expression);

            Assert.AreEqual(SyntaxKind.Asignment, syntax.Kind);
            Assert.AreEqual("foo", syntax.Name);
            Assert.AreEqual(123, syntax.Primary.Value);
        }

        [TestMethod]
        public void Assign_Complex()
        {
            var left = Syntax.Constant(TypeCode.Integer, 123);
            var right = Syntax.Constant(TypeCode.Integer, 123);
            var expression = Syntax.Operation(left, right, OperationCode.Addition);
            var syntax = Syntax.Asignment("foo", expression);

            Assert.AreEqual(SyntaxKind.Asignment, syntax.Kind);
            Assert.AreEqual("foo", syntax.Name);
            Assert.AreEqual(123, syntax.Primary.Primary.Value);
            Assert.AreEqual(123, syntax.Primary.Secondary.Value);
        }

        [TestMethod]
        public void If_True()
        {
            var expression = Syntax.Constant(TypeCode.Boolean, true);
            var syntax = Syntax.If(expression, new Block());

            Assert.AreEqual(SyntaxKind.If, syntax.Kind);
            Assert.AreEqual(true, syntax.Primary.Value);
        }

        [TestMethod]
        public void While_True()
        {
            var expression = Syntax.Constant(TypeCode.Boolean, true);
            var syntax = Syntax.While(expression, new Block());

            Assert.AreEqual(SyntaxKind.While, syntax.Kind);
            Assert.AreEqual(true, syntax.Primary.Value);
        }

        [TestMethod]
        public void Return_True()
        {
            var expression = Syntax.Constant(TypeCode.Boolean, true);
            var syntax = Syntax.Return(expression);

            Assert.AreEqual(SyntaxKind.Return, syntax.Kind);
            Assert.AreEqual(true, syntax.Primary.Value);
        }

        [TestMethod]
        public void Break()
        {
            var syntax = Syntax.Break();

            Assert.AreEqual(SyntaxKind.Break, syntax.Kind);
        }

        [TestMethod]
        public void Continue()
        {
            var syntax = Syntax.Continue();

            Assert.AreEqual(SyntaxKind.Continue, syntax.Kind);
        }
    }
}

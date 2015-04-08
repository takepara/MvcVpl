using System.Collections.Generic;

namespace MvcVpl.Ast
{
    public class Block
    {
        public Block()
        {
            SyntaxTree = new List<Syntax>();
        }

        public IList<Syntax> SyntaxTree { get; set; }
    }
}

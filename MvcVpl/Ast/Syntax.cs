namespace MvcVpl.Ast
{
    public partial class Syntax
    {
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public Syntax Primary { get; set; }
        public Syntax Secondary { get; set; }
        public string OpCode { get; set; }
        public Block Block { get; set; }

    }
}

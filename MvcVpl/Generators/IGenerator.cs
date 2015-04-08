using MvcVpl.Ast;

namespace MvcVpl.Generators
{
    public interface IGenerator
    {
        string Generate(Block root);
    }
}

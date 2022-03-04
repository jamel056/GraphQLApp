using GraphQL.Types;

namespace GraphQLApp.GraphQL.Mutations
{
    public class CommandInputType : InputObjectGraphType
    {
        public CommandInputType()
        {
            Name = "CommandInput";
            Field<NonNullGraphType<StringGraphType>>("howTo");
            Field<NonNullGraphType<StringGraphType>>("commandLine");
            Field<NonNullGraphType<IntGraphType>>("platformId");
        }
    }
}

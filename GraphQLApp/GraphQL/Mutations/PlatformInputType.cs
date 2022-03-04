using GraphQL.Types;
using GraphQLApp.GraphQL.Types;

namespace GraphQLApp.GraphQL.Mutations
{
    public class PlatformInputType : InputObjectGraphType
    {
        public PlatformInputType()
        {
            Name = "PlatformInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<PlatformTypeEnumType>>("typeEnum");
        }
    }
}

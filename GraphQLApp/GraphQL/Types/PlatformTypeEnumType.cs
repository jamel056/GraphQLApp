using GraphQL.Types;
using GraphQLApp.Enums;

namespace GraphQLApp.GraphQL.Types
{
    public class PlatformTypeEnumType : EnumerationGraphType<PlatformTypeEnum>
    {
        public PlatformTypeEnumType()
        {
            Name = "Type";
            Description = "Enumeration of platform type";
        }
    }
}

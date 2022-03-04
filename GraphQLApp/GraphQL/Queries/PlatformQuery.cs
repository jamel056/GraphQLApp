using GraphQL;
using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraphQLApp.GraphQL.Queries
{
    public class PlatformQuery : ObjectGraphType
    {
        public PlatformQuery(AppDbContext context)
        {
            Name = "Query";
            
            Field<StringGraphType>("item", resolve: con => "helloooooooo");
            
            Field<ListGraphType<PlatformGraphType>>("Platforms", resolve: con => context.Platforms);

            Field<PlatformGraphType>("Platform",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Platform Id" })
                , resolve: con =>
                {
                    int id = con.GetArgument<int>("id");
                    return context.Platforms.FirstOrDefault(c => c.Id == id);
                });
        }
    }
}

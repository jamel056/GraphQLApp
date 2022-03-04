using GraphQL;
using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.GraphQL.Types;
using System.Linq;


namespace GraphQLApp.GraphQL.Queries
{
    public class CommandQuery : ObjectGraphType
    {
        public CommandQuery(AppDbContext context)
        {
            Name = "Query";
            
            Field<ListGraphType<CommandGraphType>>("Commands", resolve: con => context.Commands);
            
            Field<CommandGraphType>("Command",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Command Id" })
                , resolve: con =>
                {
                    int id = con.GetArgument<int>("id");
                    if ( id == null)
                    {
                        con.Errors.Add(new ExecutionError("wrong value for id"));
                    }

                    return context.Commands.FirstOrDefault(c => c.Id == id);
                }); 
        } 
    }
}

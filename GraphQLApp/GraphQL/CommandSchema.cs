using GraphQL.Types;
using GraphQLApp.GraphQL.Mutations;
using GraphQLApp.GraphQL.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQLApp.GraphQL
{
    public class CommandSchema : Schema
    {
        public CommandSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
            Query = serviceProvider.GetRequiredService<CommandQuery>();
            Mutation = serviceProvider.GetRequiredService<CommandMutation>();
        }
    }
}

using GraphQL.Types;
using GraphQLApp.GraphQL.Mutations;
using GraphQLApp.GraphQL.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQLApp.GraphQL
{
    public class PlatformSchema : Schema
    {
        public PlatformSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<PlatformQuery>();
            Mutation = serviceProvider.GetRequiredService<PlatformMutation>();
        }
    }
}

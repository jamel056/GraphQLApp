using GraphQL;
using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.GraphQL.Types;
using GraphQLApp.Models;
using System.Linq;

namespace GraphQLApp.GraphQL.Mutations
{
    public class PlatformMutation : ObjectGraphType
    {
        public PlatformMutation(AppDbContext context)
        {
            FieldAsync<PlatformGraphType>(
           "createPlatform",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<PlatformInputType>> { Name = "platform" }
           ),
           resolve: async con =>
           {
               var platform = con.GetArgument<Platform>("platform");
               await context.Platforms.AddAsync(platform);
               await context.SaveChangesAsync();
               return platform;
           });

            FieldAsync<PlatformGraphType>(
           "updatePlatform",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
               new QueryArgument<NonNullGraphType<PlatformInputType>> { Name = "platform" }
           ),
           resolve: async con =>
           {
               var platformRequest = con.GetArgument<Platform>("platform");
               int id = con.GetArgument<int>("id");
               var platformFromDb = context.Platforms.FirstOrDefault(x => x.Id == id);
               if (platformFromDb == null)
               {
                   con.Errors.Add(new ExecutionError("platform not found"));
                   return platformFromDb;
               }

               platformFromDb.Name = platformRequest.Name;
               platformFromDb.TypeEnum = platformRequest.TypeEnum;
               context.Platforms.Update(platformFromDb);
               await context.SaveChangesAsync();
               return platformFromDb;
           });

            FieldAsync<StringGraphType>(
           "deletePlatform",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
           ),
           resolve: async con =>
           {
               int id = con.GetArgument<int>("id");
               var platform = context.Platforms.FirstOrDefault(x => x.Id == id);
               if (platform == null)
               {
                   con.Errors.Add(new ExecutionError("platform not found"));
               }
               context.Platforms.Remove(platform);
               await context.SaveChangesAsync();
               return $"the platform in Id: {id} has been deleted successfully!";
           });
        }
    }
}

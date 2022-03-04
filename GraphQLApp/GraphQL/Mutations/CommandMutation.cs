using GraphQL;
using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.GraphQL.Types;
using GraphQLApp.Models;
using System.Linq;

namespace GraphQLApp.GraphQL.Mutations
{
    public class CommandMutation : ObjectGraphType
    {
        public CommandMutation(AppDbContext context)
        {
            FieldAsync<CommandGraphType>(
           "createCommand",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<CommandInputType>> { Name = "command" }
           ),
           resolve: async con =>
           {
               var command = con.GetArgument<Command>("command");

               var platformFromDb = context.Platforms.FirstOrDefault(x => x.Id == command.PlatformId);
               if (platformFromDb == null)
               {
                   con.Errors.Add(new ExecutionError("platform that you assigned not found"));
               }

               await context.Commands.AddAsync(command);
               await context.SaveChangesAsync();
               return command;
           });

            FieldAsync<CommandGraphType>(
           "updateCommand",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
               new QueryArgument<NonNullGraphType<CommandInputType>> { Name = "command" }
           ),
           resolve: async con =>
           {
               var commandRequest = con.GetArgument<Command>("command");
               int id = con.GetArgument<int>("id");
               var commandFromDb = context.Commands.FirstOrDefault(x => x.Id == id);
               if (commandFromDb == null)
               {
                   con.Errors.Add(new ExecutionError("command not found"));
                   return commandFromDb;
               }

               var platformFromDb = context.Platforms.FirstOrDefault(x => x.Id == commandRequest.PlatformId);
               if (platformFromDb == null)
               {
                   con.Errors.Add(new ExecutionError("platform that you assigned not found"));
                   return commandFromDb;
               }

               commandFromDb.CommandLine = commandRequest.CommandLine;
               commandFromDb.PlatformId = commandRequest.PlatformId;
               commandFromDb.HowTo = commandRequest.HowTo;

               context.Commands.Update(commandFromDb);
               await context.SaveChangesAsync();
               return commandFromDb;
           });

            FieldAsync<StringGraphType>(
           "deleteCommand",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
           ),
           resolve: async con =>
           {
               int id = con.GetArgument<int>("id");
               var command = context.Commands.FirstOrDefault(x => x.Id == id);
               if (command == null)
               {
                   con.Errors.Add(new ExecutionError("command not found"));
               }
               context.Commands.Remove(command);
               await context.SaveChangesAsync();
               return $"the command in Id: {id} has been deleted successfully!";
           });
        }
    }
}

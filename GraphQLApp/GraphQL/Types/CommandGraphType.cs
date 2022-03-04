using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLApp.GraphQL.Types
{
    public class CommandGraphType : ObjectGraphType<Command>
    {
        private readonly AppDbContext _context;

        public CommandGraphType(AppDbContext context, IDataLoaderContextAccessor dataLoader)
        {
            _context = context;

            Field(x => x.Id);
            Field(x => x.HowTo, false).Description("how to use it");
            Field(x => x.PlatformId, false).Description("parent platform");
            Field(x => x.CommandLine, false).Description("command type");

            FieldAsync<PlatformGraphType, Platform>("platform", resolve: async con =>
            {
                return await context.Platforms.FirstOrDefaultAsync(x => x.Id == con.Source.PlatformId);
            });

            Field<CommandGraphType, Command>()
                .Name("commandDict")
                .ResolveAsync(con =>
                {
                    var loader = dataLoader.Context.GetOrAddBatchLoader<int, Command>("GetCommandById", GetCommandById
                        );
                    return loader.LoadAsync(con.Source.Id);
                });
            Field<ListGraphType<CommandGraphType>, IEnumerable<Command>>()
                .Name("commandDict2")
                .ResolveAsync(async ctx =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, Command>("GetCommandById2", GetCommandById2);
                    return loader.LoadAsync(ctx.Source.Id);
                });
        }

        public async Task<IDictionary<int, Command>> GetCommandById(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            return await _context.Commands.Where(c => ids.Contains(c.Id)).AsNoTracking().ToDictionaryAsync(c => c.Id);
        }

        public async Task<ILookup<int, Command>> GetCommandById2(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            var commands = await _context.Commands.Where(i => ids.Contains(i.Id)).ToListAsync();
            return commands.ToLookup(i => i.Id);
        }
    }
}

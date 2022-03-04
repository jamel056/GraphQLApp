using GraphQL.Types;
using GraphQLApp.Data;
using GraphQLApp.Enums;
using GraphQLApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLApp.GraphQL.Types
{
    public class PlatformGraphType : ObjectGraphType<Platform>
    {
        public PlatformGraphType(AppDbContext context)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the Platform object.");
            
            Field(x => x.Name, false)
                .Name("Name")
                .Description("name of platform");

            Field(x => x.LicenseKey, true)
                .Name("LicenseKey")
                .Description("license key of platform");

            Field<PlatformTypeEnumType>("TypeEnum","Enum of platform types");
            
            FieldAsync<ListGraphType<CommandGraphType>, IReadOnlyCollection<Command>>(
            "commands",
            resolve: async con =>
            {
                return await context.Commands.Where(x => x.PlatformId == con.Source.Id).ToListAsync();
            });
        }
    }
}

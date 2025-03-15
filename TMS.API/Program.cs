using Microsoft.EntityFrameworkCore;
using TMS.APPLICATION.Common.Mappings;
using static TMS.API.Configuration.Database;
using static TMS.API.Configuration.MediatR;
using static TMS.API.Configuration.DependencyInjection;
using static TMS.API.Configuration.Swash;
using static TMS.API.Configuration.Endpoints;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
AddControllers(builder);
RegisterSwagger(builder);
RegisterDatabase(builder);
RegisterMediatR(builder);
RegisterAutomapper(builder);

RegisterDI(builder);


var app = builder.Build();

UseSwagger(app);
//Apply pending migration
ApplyMigrations(app);


app.UseHttpsRedirection();
app.MapControllers();
app.Run();

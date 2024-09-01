using System.Text.Json;
using System.Text.Json.Serialization;
using BLL;
using DAL.Configurations;
using DAL.Configurations.DepedencyConf;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace LibraryApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDepedencies(builder.Configuration);

        builder.Services.AddMediatR(typeof(DemoEntry).Assembly);

        var app = builder.Build();
     
        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
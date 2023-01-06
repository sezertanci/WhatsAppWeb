using Application.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Persistence.Context;
using Persistence.Extensions;
using WebApi.Hubs;
using WebApi.Infrastructure.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())//Controller'lara gelen istekleri filtreler
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;//Json çýktýsýnýn baþ harflerinde deðiþiklik yapmaz
    })
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new DefaultContractResolver();//Json çýktýsýnýn baþ harflerinde deðiþiklik yapmaz
    })
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);


builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyHeader().AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(x => true);
}));

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/ChatHub");
});

using(var context = new WhatsAppWebContext())
{
    context.Database.Migrate();
}

app.Run();

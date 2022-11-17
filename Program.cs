using BookAPI.Models;
using BookAPI.Storage;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

switch (builder.Configuration["Storage:Type"].ToStorageEnum())
{
    case StorageEnum.MemCache:
        builder.Services.AddSingleton<IStorage<Book>, MemCache>();
        break;
    case StorageEnum.FileStorage:
        builder.Services.AddSingleton<IStorage<Book>>(
            x => new FileStorage(builder.Configuration["Storage:FileStorage:Filename"], int.Parse(builder.Configuration["Storage:FileStorage:FlushPeriod"])));
        break;
    default:
        throw new IndexOutOfRangeException($"Storage type '{builder.Configuration["Storage:Type"]}' is unknown");
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

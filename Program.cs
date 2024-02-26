using minimalAPI_webbutveckling_labb2.Data;
using minimalAPI_webbutveckling_labb2.Models;

namespace minimalAPI_webbutveckling_labb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();




            app.MapGet("/car", async (DataContext context) =>
            {
                return await context.Cars.ToListAsync();
            });



            app.MapGet("/car/{id}", async (DataContext context, int id) =>

                await context.Cars.FindAsync(id) is Car car ?
                Results.Ok(car) :
                Results.NotFound("Does not exist")
            );





            app.MapPost("/car", async (DataContext context, Car car) =>
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Cars.ToListAsync());

            });

            app.MapPut("/car/{id}", async (DataContext context, Car updateCar, int id) =>
            {
                var car = await context.Cars.FindAsync(id);

                if (car == null)
                {
                    return Results.NotFound("Sorry, this car doesnt exists");


                }

                car.Make = updateCar.Make;
                car.Model = updateCar.Model;

                await context.SaveChangesAsync();

                return Results.Ok(await context.Cars.ToListAsync());
            });

            app.MapDelete("/car/{id}", async (DataContext context, int id) =>
            {
                var car = context.Cars.FindAsync(id);

                if (car == null)

                    return Results.NotFound("Sorry, this car doesnt exists");




                context.Cars.Remove(await car);
                await context.SaveChangesAsync();

                return Results.Ok(await context.Cars.ToListAsync());
            });

            app.Run();

        }


    }
}










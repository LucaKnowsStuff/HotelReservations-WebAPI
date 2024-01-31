using HotelReservation.Data;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Repositories;
using HotelReservation.UnitOfWork;
using HotelReservation.Mapping;
using FluentValidation;
using HotelReservation.Models.Domain;
using HotelReservation.Validations;
using HotelReservation.Models.DTOs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<HotelReservationDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("HotelReservationConnectionString"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("HotelReservationConnectionString"))));



builder.Services.AddScoped<IUOWork, UOWork>();



builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();


builder.Services.AddScoped<IValidator<AddGuestDTO>, AddGuestDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateGuestDTO>, UpdateGuestDTOValidator>();
builder.Services.AddScoped<IValidator<AddRoomDTO>, AddRoomDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateRoomDTO>, UpdateRoomDTOValidator>();
builder.Services.AddScoped<IValidator<AddReservationDTO>, AddReservationDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateReservationDTO>, UpdateReservationDTOValidator>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

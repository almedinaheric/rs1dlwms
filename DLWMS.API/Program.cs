using DLWMS.Repository;
using DLWMS.Servis;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string konekcijskiString = builder.Configuration.GetConnectionString("DLWMSdev");

builder.Services.AddDbContext<DLWMSDBContext>(
    dbContextOpcije => dbContextOpcije.UseSqlServer(konekcijskiString) );


//ovo je linija dependency injectiona
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
//ova linija koda-> kad neko bude zahtijevao tip Irepository (<,> zato sto su unutra 2 tipa), ti ces mu isporuciti objekat tipa
//repository za ista ta dva tipa (Tentity, Tkey)
//npr neko zahtijeva neszo za studenta i int, ovaj mu vrati rpeository klasu sa studen int
builder.Services.AddScoped<IStudentService, StudentServis>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

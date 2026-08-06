using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Render/Docker Port Yapılandırması
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection String ve PostgreSQL Yapılandırması
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
string npgsqlConnStr = connStr ?? "";

if (!string.IsNullOrEmpty(connStr) && connStr.StartsWith("postgres"))
{
    var uri = new Uri(connStr);
    var userInfo = uri.UserInfo.Split(':');
    var user = userInfo[0];
    var pass = userInfo.Length > 1 ? userInfo[1] : "";
    var host = uri.Host;
    var portNum = uri.Port > 0 ? uri.Port : 5432;
    var db = uri.AbsolutePath.TrimStart('/');
    npgsqlConnStr = $"Host={host};Port={portNum};Database={db};Username={user};Password={pass};Ssl Mode=Require;Trust Server Certificate=true";
}

if (!string.IsNullOrEmpty(npgsqlConnStr))
{
    builder.Services.AddDbContext(options =>
        options.UseNpgsql(npgsqlConnStr));
}

var app = builder.Build();

// Otomatik Veritabanı Tablosu Oluşturma
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService();
    dbContext?.Database.EnsureCreated();
}

// Frontend ve Statik Dosya Desteği
app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger Yapılandırması
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Girisim & Yatirim API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();
app.Run();

// Veritabanı Yapısı ve Modeller
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
    public DbSet<Girisim> Girisimler => Set<Girisim>();
    public DbSet<Teklif> Teklifler => Set<Teklif>();
}
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet Kullanicilar => Set();
    public DbSet Girisimler => Set();
    public DbSet Teklifler => Set();
}

public class Kullanici
{
    public int Id { get; set; }
    [Required] public string AdSoyad { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = "Girisimci";
}

public class Girisim
{
    public int Id { get; set; }
    [Required] public string Baslik { get; set; } = string.Empty;
    public string Aciklama { get; set; } = string.Empty;
    public string Sektor { get; set; } = string.Empty;
    public decimal ArananYatirim { get; set; }
    public decimal TeklifEdilenHisseYuzdesi { get; set; }
    public int KullaniciId { get; set; }
}

public class Teklif
{
    public int Id { get; set; }
    public int GirisimId { get; set; }
    public int YatirimciId { get; set; }
    public decimal TeklifMiktari { get; set; }
    public string Mesaj { get; set; } = string.Empty;
    public DateTime Tarih { get; set; } = DateTime.UtcNow;
}
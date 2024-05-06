using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using System.Data;

namespace MultiShop.Discount.Context;

public class DapperContext:DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //tablomu veritabanına yansıtmak için baglantıyı burada yapacagım
        //ilerleyen sureclerde bu baglantıyı appsettings.json 'a tasıyacagım.
        optionsBuilder.UseSqlServer("Server=DESKTOP-345EPP4\\MSSQLSERVEREMRE;initial Catalog=MultiShopDiscountDb;" +
            "integrated security=true");
    }
    public DbSet<Coupon> Coupons { get; set; }
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}

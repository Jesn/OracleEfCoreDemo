using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using OracleEfCoreDemo.Model;
using OracleEfCoreDemo.Repository;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Oracle 数据库连接配置
OracleConfiguration.TnsAdmin = configuration.GetSection("TnsAdminDirectory").Value;
OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
OracleConfiguration.TraceLevel = 7;
// OracleConfiguration.TraceFileLocation = @"/Users/darren/coding/tool/oracledb_Wallet_RichDB/traces";


// 参考地址： https://www.oracle.com/database/technologies/appdev/dotnet/adbdotnetquickstarts.html
builder.Services.AddDbContext<OracleDBContext>(options =>
{
    options.UseOracle(configuration.GetConnectionString("OracleConnString"));
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapGet("/test", (OracleDBContext oracleDbContext) =>
{
    var students = oracleDbContext.Students.ToList();
    return Task.FromResult(students);
});


app.MapGet("/add", (OracleDBContext oracleDbContext) =>
{
    var count = oracleDbContext.Students.Count();

    var newStudent = new Student()
    {
        Id = count + 1,
        Name = "王五",
        Code = "003"
    };
    
    var stu=  oracleDbContext.Students.Add(newStudent);
    oracleDbContext.SaveChanges();
    var students = oracleDbContext.Students.ToList();
    return Task.FromResult(students);
});

app.Run();
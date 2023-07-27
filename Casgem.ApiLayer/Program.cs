using Amazon.Runtime;
using Casgem.ApiLayer.Models;
using Casgem.BusinessLayer.Abstract;
using Casgem.BusinessLayer.Concrete;
using Casgem.BusinessLayer.MongoConcrete;
using Casgem.DataAccessLayer.Abstract;
using Casgem.DataAccessLayer.Concrete;
using Casgem.DataAccessLayer.EntityFramework;
using Casgem.EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Settings.Abstract;
using EntityLayer.Settings.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using CategoryManager = Casgem.BusinessLayer.Concrete.CategoryManager;
using CustomerManager = Casgem.BusinessLayer.Concrete.CustomerManager;
using ProductManager = Casgem.BusinessLayer.Concrete.ProductManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
});
builder.Services.AddMvc();



//builder.Services.AddS
/*
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});*/

// Add services to the container.

//builder.Services.AddHttpClient();
//builder.Services.AddHttpClient<IHttpClientFactory, HttpC>();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);




builder.Services.AddScoped<IEstateOfficeDatabaseSetting, EstateOfficeDatabaseSetting>();
builder.Services.Configure<EstateOfficeDatabaseSetting>(builder.Configuration.GetSection("EstateOfficeDatabaseSetting"));
builder.Services.AddSingleton<IEstateOfficeDatabaseSetting>(sp =>
{
    return sp.GetRequiredService<IOptions<EstateOfficeDatabaseSetting>>().Value;
});
builder.Services.Configure<EstateOfficeDatabaseSetting>(
    builder.Configuration.GetSection(nameof(EstateOfficeDatabaseSetting)));

builder.Services.AddSingleton<EstateOfficeDatabaseSetting>(sp =>
    sp.GetRequiredService<IOptions<EstateOfficeDatabaseSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("EstateOfficeDatabaseSetting:ConnectionString")));


builder.Services.AddScoped<IMongoEstateService, EstateMongoManager>();
builder.Services.AddScoped<IMongoEstateDal, MongoEstateDal>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Selamun Aleyküm Mübarek <br />! Hüseyin Aydýn MongoDB Swagger Dashboard", Version = "v1" });
});



builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSession();

app.UseHttpsRedirection();

app.UseAuthentication(); //Kimlik doðrulamasý kullan
app.UseAuthorization();

app.MapControllers();

app.Run();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
builder.Logging.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Config/log4net.config"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule<AutofacModule>();
//});
builder.Services.AddIOC();
builder.Services.AddAutoIOC();
builder.Services.AddConfig(configuration);
builder.Services.AddFilter();
builder.Services.AddJWT(configuration);
builder.Services.AddRedis(configuration);
builder.Services.AddCors(options => options
.AddPolicy("All", builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //��Ȩ
app.UseAuthorization();//��Ȩ

app.UseRouting();     //·��
app.UseCors("All");       //����
app.MapControllers();

app.Run();


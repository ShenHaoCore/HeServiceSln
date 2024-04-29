using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using He.BaseService.Api.Common;
using He.BaseService.Model.Common;
using He.Business.Common;
using He.Framework.Common;
using He.Framework.Extension.Consul;
using He.Framework.Extension.Cors;
using He.Framework.Extension.Jwt;
using He.Framework.Extension.RabbitMQ;
using He.Framework.Extension.Redis;
using He.Framework.Extension.Serilog;
using He.Framework.Extension.SqlSugar;
using He.Framework.Extension.Swagger;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule<AutofacRegisterModule>(); });
builder.Host.AddSerilog();

var setting = builder.Configuration.GetSection(ServiceConfig.KEY).Get<ServiceConfig>();
ArgumentNullException.ThrowIfNull(setting);
List<string> xmlNames = [$"{ServiceHelper.AssemblyName}.XML", $"{ModelHelper.AssemblyName}.XML", $"{BusinessHelper.AssemblyName}.XML", $"{FrameworkHelper.AssemblyName}.XML"];
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>()); // 属性注入必须

builder.Services.AddSingleton(new AppSettings(builder.Configuration));
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
builder.Services.AddRabbitMQ();
builder.Services.AddRedis();
builder.Services.AddSqlSugar();
builder.Services.AddController(setting.PrefixName);
builder.Services.AddApiVersion();
builder.Services.AddRateLimiting();
builder.Services.AddCorsPolicy();
builder.Services.AddJwt();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiSwagger(xmlNames);

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
app.UseApiSwagger();
app.UseSerilog();
app.UseHealthCheckMiddle();
app.UseCorsPolicy();
app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseAuthentication(); // 启用身份验证中间件
app.UseAuthorization();
app.MapControllers();
app.Run();

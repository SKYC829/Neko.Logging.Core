# Neko.Logging.Core
一个让Asp.NetCore的日志可以输出到文件的超轻型中间件
### 项目已整合至[Neko.Utility](https://github.com/SKYC829/Neko.Utility)建议使用该项目的日志功能
------
使用方法:

- 最偷懒的方法
```c#
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //使用BuildNekoLog配置NekoLog中间件
                .BuildNekoLog(op=>
                {
                    // op是NekoLogConfiguration
                    op.LogLevel = LogLevel.Trace;
                })
                //...);
```
- 不会覆盖原有的ILog<TCategoryName>的方法
 Startup.cs:
 ```c#
        public void ConfigureServices(IServiceCollection services)
        {
            //...
            services.AddNekoLog(); //添加INekoLog的依赖注入
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //...
            //使用NekoLog的中间件
            app.UseNekoLog(op=>
                {
                    // op是NekoLogConfiguration
                    op.LogLevel = LogLevel.Trace;
                });
            //...
        }
 ```
 
 WeatherForecastController.cs:
 ```c#
        //重载构造函数接受INekoLog的依赖注入
        public WeatherForecastController(INekoLog log)
        {
            
        }
 ```

using FlexLink.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

var builder = WebApplication.CreateBuilder(args);

var redirectionSettings = builder.Configuration
                                  .GetSection("RedirectionSettings:Redirects")
                                  .Get<Dictionary<string, RedirectSettings>>(); 

var app = builder.Build();



app.Map("/{keyword}", async context =>
{
    var path = context.Request.RouteValues["keyword"] as string; 
    var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower();
    var redirectionUrl = string.Empty;

    if (redirectionSettings.TryGetValue(path, out var redirectSetting))
    {  
        if (redirectSetting.Type == "normal")
        {
            redirectionUrl = redirectSetting.Url;
        }
        else if (redirectSetting.Type == "multiple")
        {
            if (userAgent.Contains("android"))
            {
                redirectionUrl = redirectSetting.Urls["Android"];
            }
            else if (userAgent.Contains("iphone") || userAgent.Contains("ipad"))
            {
                redirectionUrl = redirectSetting.Urls["iOS"];
            }
            else if (userAgent.Contains("huawei"))
            {
                redirectionUrl = redirectSetting.Urls["Huawei"];
            }
            else
            {
                redirectionUrl = redirectSetting.Urls["Others"];
            }
        }

        if (!string.IsNullOrEmpty(redirectionUrl))
        {
            context.Response.Redirect(redirectionUrl);
            return;
        } 
    }
    await HttpContextExtensions.NotFoundResponseAsync(context, "Aradýðýnýz sayfa bulunamadý.");

});

app.MapFallback(async context =>
{
    await HttpContextExtensions.NotFoundResponseAsync(context, "Aradýðýnýz sayfa bulunamadý.");
});  

app.Run(); 
public static class HttpContextExtensions
{
    public static Task NotFoundResponseAsync(HttpContext context, string message)
    {
        context.Response.ContentType = "text/plain; charset=utf-8";
        context.Response.StatusCode = 404;
        return context.Response.WriteAsync(message);
    }
}
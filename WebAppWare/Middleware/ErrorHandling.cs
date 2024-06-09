
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NuGet.Protocol;

namespace WebAppWare.Middleware;

public class ErrorHandling : IMiddleware
{
	private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

	public ErrorHandling(ITempDataDictionaryFactory tempDataDictionaryFactory)
    {
		_tempDataDictionaryFactory = tempDataDictionaryFactory;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next.Invoke(context);
		}
		catch (Exception ex)
		{
			context.Response.StatusCode = 500;
			
			//context.Items["ExceptionMessage"] = ex.Message;
			var tempData = _tempDataDictionaryFactory.GetTempData(context);
			tempData["ExceptionMessage"] = ex.Message;
			context.Response.Redirect(@"/Home/Error");
			//await context.Response.WriteAsJsonAsync(ex.ToString());
		}
	}
}


using Domain.Excaptions;
using Shared.ErorrModels;
using System.Net;

namespace WebApplication5.Middelware
{
	public class GlobalerorrhandlingMiddelWares 
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalerorrhandlingMiddelWares> _logger;
        public GlobalerorrhandlingMiddelWares(RequestDelegate next,ILogger<GlobalerorrhandlingMiddelWares> logger)
        {
			_logger=logger;
			_next=next;	
		}

		public async Task Inovokeasync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
				  
			}

			catch(Exception ex) 
			{
				_logger.LogError($"Something go wrong");
				await HandleException(httpContext,ex);
			}
		}

		private async Task HandleException(HttpContext httpContext, Exception ex)
		{
			//httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			httpContext.Response.ContentType = "application/json";

			httpContext.Response.StatusCode = ex switch
			{
				NotFound_Excaptions => (int)HttpStatusCode.NotFound,
				_ => (int)HttpStatusCode.InternalServerError,
			};

			var response = new ErorrDetails
			{
				ErrorMessage = ex.Message
			};

			response.StatusCode = httpContext.Response.StatusCode;

			await httpContext.Response.WriteAsync(response.ToString());
		}
	}
}

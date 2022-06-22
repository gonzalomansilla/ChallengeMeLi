using ChallengeMeLi.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace ChallengeMeLi.Extensions
{
    /*
		Clase extensora que registra los servicios utilziados en la capa Presentacion
	 */

    public static class AppExtensions
    {
        public static void UseErrorHandlindMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
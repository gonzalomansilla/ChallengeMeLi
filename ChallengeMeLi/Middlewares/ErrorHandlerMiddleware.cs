using ChallengeMeLi.Application.Exceptions;
using ChallengeMeLi.Application.Wrappers;

using Microsoft.AspNetCore.Http;

using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChallengeMeLi.Middlewares
{
    /*
		Clase que maneja los errores provenientes de la capa de negocios

		Se intermedia la peticion http para dar un response personalizado
	*/

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new ResponseWrapper<string>()
                {
                    Success = false,
                    Message = error?.Message,
                };

                switch (error)
                {
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Errors = e.Errors;
                        break;

                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Application.Wrappers
{
    /*
		Clase generica que representa la respuesta estandar de todas las apis
	*/

    [ExcludeFromCodeCoverage]
    public class ResponseWrapper<T>
    {
        public ResponseWrapper()
        {
        }

        public ResponseWrapper(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
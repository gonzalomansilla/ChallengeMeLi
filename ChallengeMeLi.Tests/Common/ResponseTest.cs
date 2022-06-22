using ChallengeMeLi.Application.Wrappers;

using System.Net;

namespace ChallengeMeLi.Tests.Common
{
	public class ResponseTest<T> where T : class
	{
		public ResponseWrapper<T> ResponseWrapper { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}
}

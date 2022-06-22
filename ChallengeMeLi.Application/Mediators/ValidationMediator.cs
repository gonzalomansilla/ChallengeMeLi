using FluentValidation;

using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/*
	Comportamiento de las validaciones dentro flujo del request

	Ejecuta las validaciones del request antes de llegar a la capa de negocio
 */

namespace ChallengeMeLi.Application.Mediators
{
	public class ValidationMediator<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validations;

		public ValidationMediator(IEnumerable<IValidator<TRequest>> validations)
		{
			_validations = validations;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			// Verifique si hay algún validador implementado a través del pipeline
			if (_validations.Any())
			{
				// Obtener el request que esta entrando (context)
				var context = new ValidationContext<TRequest>(request);

				// Obtener resultados de las validaciones
				var resultValidations = await Task.WhenAll(_validations.Select(v => v.ValidateAsync(context, cancellationToken)));

				// Recolectar los errores que pudieron haberse producido en la validacion
				var validationFailures = resultValidations
					.SelectMany(x => x.Errors)
					.Where(f => f != null).ToList();

				if (validationFailures.Count != 0)
				{
					throw new Exceptions.ValidationException(validationFailures);
				}
			}

			// Seguir con el request
			return await next();
		}
	}
}

using ChallengeMeLi.Application.Mediators;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

/*
	Clase extensora que registra los servicios utilziados en la capa Application
 */

namespace ChallengeMeLi.Application
{
	public static class ServicesExtension
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			// Registra automáticamente todos los validadores creados
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			// Registra el pipeline que realiza las validaciones del request antes de llegar a la
			// capa de dominio
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationMediator<,>));
			services.AddMediatR(Assembly.GetExecutingAssembly());
		}
	}
}

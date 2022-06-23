
# Challenge MeLi

Proyecto para el challenge de MercadoLibre para la posición de Backend Developer.

### Catacteristicas
#### Tecnlogías
* [ASP.NET Core (.NET Core 5)](https://dotnet.microsoft.com/en-us/apps/aspnet/microservices)
* [Entity Framework 5](https://docs.microsoft.com/en-us/ef/)
* [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
* [Docker](https://www.docker.com/)
* Unit Test y Integration Test con [XUnit](https://xunit.net/)
* [Swagger](https://swagger.io/) como explorador y documentador de APIs

#### Servicios de Azure Portal
* [Azure DevOps](https://azure.microsoft.com/es-es/services/devops/)
	
	Plataforma para crear los pipelines que se encargaran de ejecutar automáticamente las tareas de CI (Continous Integration) y CD (Continous Delivery)
* [Azure SQL Database](https://azure.microsoft.com/es-es/products/azure-sql/database/#overview)
	
	Base de datos en la nube para persistir los datos

* [Azure Container Registry](https://azure.microsoft.com/es-es/services/container-registry/)
	
	Servicio para registrar las imágenes de docker
	
* [Azure App Service](https://azure.microsoft.com/es-es/services/app-service/)
	
	Servicio en la nube para hospedar microservicios

## Instalación
1. Instalar el SDK de [.NET Core 5](https://download.visualstudio.microsoft.com/download/pr/14ccbee3-e812-4068-af47-1631444310d1/3b8da657b99d28f1ae754294c9a8f426/dotnet-sdk-5.0.408-win-x64.exe)
	> Para más informacion revisar la [documentación oficial](https://dotnet.microsoft.com/en-us/learn/dotnet/hello-world-tutorial/install) de Microsoft

2. Dado que se usa SQL Server para persistir datos es requerido tener instalado:
	* [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
	* [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver16) como IDE para interactuar con la base de datos (Opcional)
3. Clonar repositorio:
  ```
  git clone https://github.com/gonzalomansilla/ChallengeMeLi.git
  ```
4. Modificar el ConnectionString con el host local

  ![connection string](https://i.imgur.com/3arqmTY.png)
  
5. Restaurar los paquetes y generar las dependencias:
  ```
  dotnet restore
  ```
6. Ejecutar el microservicio:
  ```
  dotnet run
  ```
7. Probar las apis:

    La URL base es https://localhost:5001/ seguido de los distintos endpoints ya conocidos

## Consideraciones
Una vez ejecutado el proyecto, este realizara automáticamente las migraciones iniciales que crearan las tablas, sus relaciones y los datos iniciales requeridos por el microservicio.

## Características del proyecto
En el proyecto se utilizo diferentes patrones de diseño y de arquitectura que proporcionaron mantenimiento, independencia entre capas y principalmente escalabilidad.

A nivel arquitectura se opto por el patrón **Clean Architecture**, donde su principal benéfico es la aislación de nuestra lógica de dominio haciendo que esta sea independiente de la UI, la base de datos o cualquier otro framework que se utilice.

![clean architecture diagram](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

A nivel diseño se utilizo principalmente el patrón **Mediator**, ya que con este es posible reducir las dependencias entre objetos y capas. En este proyecto concretamente, permitió que la capa de los Controllers no se comuniquen directamente con la de Application, que es donde reside toda la lógica.

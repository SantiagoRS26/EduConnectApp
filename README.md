# EduConnect

Este repositorio contiene un proyecto completo de EduConnect, que incluye tanto el backend como el frontend. EduConnect es una plataforma que permite la interacción entre usuarios y colegios, incluyendo funcionalidades de mensajería en tiempo real mediante SignalR.

## Características

- **ASP.NET Core Backend**: API desarrollada en ASP.NET Core con autenticación JWT, integración de Google y manejo de SignalR para mensajería en tiempo real.
- **SignalR para Chat en Tiempo Real**: Implementación de un sistema de chat en tiempo real utilizando SignalR.
- **Autenticación con JWT y Google**: Autenticación segura basada en JWT y opción de iniciar sesión con Google.
- **Arquitectura de Capas**: Separación de responsabilidades en diferentes capas: Data Access Layer (DAL), Business Logic Layer (BLL) y API Layer.
- **Carga de Archivos**: Funcionalidad para cargar y gestionar imágenes de perfil de usuario.
- **NetTopologySuite**: Uso de NetTopologySuite para operaciones geoespaciales, como la búsqueda de colegios en un radio específico.

## Estructura del Proyecto

El proyecto está dividido en las siguientes capas:

- **EduConnect.API**: Contiene la API ASP.NET Core que expone los servicios para ser consumidos por el frontend.
- **EduConnect.BLL (Business Logic Layer)**: Contiene la lógica de negocio de la aplicación.
- **EduConnect.DAL (Data Access Layer)**: Maneja el acceso a la base de datos y las operaciones CRUD.
- **EduConnect.Models**: Definición de los modelos de datos utilizados en la aplicación.

## Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Node.js](https://nodejs.org/) (para ejecutar el frontend)
- [Visual Studio Code](https://code.visualstudio.com/) o [Visual Studio](https://visualstudio.microsoft.com/)

## Configuración del Proyecto

### Backend

1. Clona el repositorio:
   ```sh
   git clone https://github.com/tu_usuario/educonnect.git
   cd educonnect
   ```

2. Configura la cadena de conexión en `appsettings.json` dentro del proyecto EduConnect.API:
   ```json
   "ConnectionStrings": {
     "ConnectionDB": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   },
   "Jwt": {
     "Key": "your_secret_key",
     "Issuer": "your_issuer",
     "Audience": "your_audience"
   },
   "Authentication:Google": {
     "ClientId": "your_google_client_id",
     "ClientSecret": "your_google_client_secret"
   }
   ```

3. Aplica las migraciones para crear la base de datos:
   ```sh
   dotnet ef database update --project EduConnect.DAL
   ```

4. Ejecuta la aplicación backend:
   ```sh
   dotnet run --project EduConnect.API
   ```

### Frontend

1. Navega al directorio del frontend:
   ```sh
   cd frontend
   ```

2. Instala las dependencias:
   ```sh
   npm install
   ```

3. Ejecuta el servidor de desarrollo:
   ```sh
   npm start
   ```

## Uso de la API

Una vez que el backend esté en funcionamiento, puedes utilizar herramientas como Postman, Swagger o el frontend de la aplicación para interactuar con los diferentes endpoints y realizar operaciones como registro, inicio de sesión, búsqueda de colegios, y mensajería en tiempo real.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue para discutir cualquier cambio que desees realizar.

## Contacto

Si tienes alguna pregunta o sugerencia, no dudes en contactarme a través de [santiagors2611@gmail.com](mailto:santiagors2611@gmail.com).

---

¡Gracias por visitar mi repositorio y espero que encuentres útil este proyecto!

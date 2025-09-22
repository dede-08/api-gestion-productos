
Una API REST completa para la gestión de inventario con autenticación JWT, desarrollada en ASP.NET Core 8.0.

## Características

- **Autenticación JWT**: Sistema de autenticación seguro con tokens JWT
- **CRUD Completo**: Operaciones Create, Read, Update, Delete para productos
- **Validaciones**: Validaciones robustas con Data Annotations
- **DTOs**: Separación de modelos de entrada y salida
- **Servicios**: Arquitectura en capas con servicios de negocio
- **Estadísticas**: Endpoints para reportes y estadísticas
- **Documentación**: Swagger UI integrado con documentación completa
- **Logging**: Middleware para logging de requests
- **CORS**: Configuración para desarrollo frontend
- **Soft Delete**: Eliminación lógica de productos

## Tecnologías

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **PostgreSQL**
- **JWT Authentication**
- **Swagger/OpenAPI**
- **CORS**

## Requisitos Previos

- .NET 8.0 SDK
- PostgreSQL
- Visual Studio 2022 o VS Code


## Arquitectura

```
api-gestion-productos/
├── Controllers/          # Controladores de la API
├── Data/                # Contexto de Entity Framework
├── Models/              # Modelos de datos y DTOs
├── Services/            # Servicios de negocio
├── Middleware/          # Middleware personalizado
├── Program.cs           # Configuración de la aplicación
└── README.md           # Documentación
```



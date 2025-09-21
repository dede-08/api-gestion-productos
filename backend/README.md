# API Gestión de Productos

Una API REST completa para la gestión de productos con autenticación JWT, desarrollada en ASP.NET Core 8.0.

## 🚀 Características

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

## 🛠️ Tecnologías

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **PostgreSQL**
- **JWT Authentication**
- **Swagger/OpenAPI**
- **CORS**

## 📋 Requisitos Previos

- .NET 8.0 SDK
- PostgreSQL
- Visual Studio 2022 o VS Code

## ⚙️ Configuración

### 1. Clonar el repositorio
```bash
git clone <url-del-repositorio>
cd api-gestion-productos
```

### 2. Configurar la base de datos
- Crear una base de datos PostgreSQL
- Actualizar la cadena de conexión en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=products_db;Username=tu_usuario;Password=tu_password"
  }
}
```

### 3. Configurar variables de entorno
Crear un archivo `.env` en la raíz del proyecto:
```
API_KEY=tu_clave_secreta_muy_larga_y_segura
```

### 4. Ejecutar migraciones
```bash
dotnet ef database update
```

### 5. Ejecutar la aplicación
```bash
dotnet run
```

La API estará disponible en: `https://localhost:7001`
Swagger UI estará disponible en: `https://localhost:7001`

## 🔐 Autenticación

### Registro de Usuario
```http
POST /api/auth/add-user
Content-Type: application/json

{
  "name": "Juan",
  "lastname": "Pérez",
  "age": 25,
  "email": "juan@ejemplo.com",
  "telephone": "123456789",
  "password": "123456"
}
```

### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "juan@ejemplo.com",
  "password": "123456"
}
```

### Usar el Token
Incluir el token en el header de las requests:
```
Authorization: Bearer <tu_token_jwt>
```

## 📚 Endpoints

### Productos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/products` | Obtener todos los productos |
| GET | `/api/products/{id}` | Obtener producto por ID |
| POST | `/api/products` | Crear nuevo producto |
| PUT | `/api/products/{id}` | Actualizar producto |
| DELETE | `/api/products/{id}` | Eliminar producto (soft delete) |
| GET | `/api/products/category/{category}` | Productos por categoría |
| GET | `/api/products/search?q={term}` | Buscar productos |
| GET | `/api/products/low-stock?threshold={number}` | Productos con stock bajo |

### Estadísticas

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/stats/products` | Estadísticas generales |
| GET | `/api/stats/products/by-category` | Productos por categoría |
| GET | `/api/stats/products/most-expensive` | Productos más caros |
| GET | `/api/stats/products/highest-stock` | Productos con más stock |
| GET | `/api/stats/products/recent` | Productos recientes |

## 📝 Ejemplos de Uso

### Crear un Producto
```http
POST /api/products
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Laptop Gaming",
  "description": "Laptop para gaming de alto rendimiento",
  "price": 1299.99,
  "stock": 15,
  "category": "Electrónicos"
}
```

### Buscar Productos
```http
GET /api/products/search?q=gaming
Authorization: Bearer <token>
```

### Obtener Estadísticas
```http
GET /api/stats/products
Authorization: Bearer <token>
```

## 🏗️ Arquitectura

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

## 🔧 Configuración Avanzada

### Logging
La aplicación incluye logging automático de todas las requests con tiempo de respuesta.

### CORS
Configurado para permitir requests desde cualquier origen (configurar apropiadamente para producción).

### Validaciones
Todos los modelos incluyen validaciones robustas con mensajes de error personalizados.

## 🚀 Despliegue

### Docker (Opcional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["api-gestion-productos.csproj", "./"]
RUN dotnet restore "api-gestion-productos.csproj"
COPY . .
RUN dotnet build "api-gestion-productos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api-gestion-productos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api-gestion-productos.dll"]
```

## 🤝 Contribuir

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 📞 Contacto

- Email: tu.email@ejemplo.com
- Proyecto: [https://github.com/tu-usuario/api-gestion-productos](https://github.com/tu-usuario/api-gestion-productos)

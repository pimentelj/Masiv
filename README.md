# Prerequisitos
- Tener instalado .NET Core.
- Tener instalado SQL Server

# Configuración
Para configurar la base de datos en la aplicación se debe ir al archivo **appsettings.json** que se encuentra en la raíz y editar el campo "Connection" (Línea 10), que debe reemplazar por la conexión y la base de datos configurada en su equipo, así como el usuario y contraseña.

Tambien se recomienda editar la llave secreta para el cifrado del token JWT. Para realizar esta acción se debe reemplazar el texto que se encuentra en el campo "Secret" (Línea 13). Existen páginas que generan token random como por ejemplo: https://randomkeygen.com/

Luego de hacer todo lo anterior, se procede a correr la migración para generar las tablas en la base de datos. Para realizar esta acción, se debe abrir la consola de comandos del sistema operativo e ir a la carpeta del proyecto y ejecutar el comando `dotnet ef database update  --context DataContext `, de esta forma se comienzan ejecutar la migración. Al finalizar la migración mostrará el siguiente mensaje `Applying migration '20210408025541_InitialCreate'.`.

Finalizado todo lo anterior, se puede iniciar el servicio que provee .NET Core para consumirlo desde postman con el comando `dotnet run`. El servicio se inicia en los puertos 5000 (http) y 5001(https). 

Para consultar la api del proyecto ir a la url https://localhost:5001/swagger/index.html si se subio el servicio por los puertos mencionados y se encuentra consultando de manera local.

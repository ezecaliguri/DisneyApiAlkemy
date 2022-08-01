Proyecto para ingresar a la aceleración de Alkemy con C# .net

El proyecto cuenta con un CRUD, implementación de JWT, Envío de Emails Al registrarse y Loguearse y un poco de testing.

Para la realización de la base de datos se utilizó CodeFirst. También agregue un inyección de datos por defecto para que al crear la base se pueda ya probar con unos datos Pre armados.

Para la creación de la base de datos, se debe quitar la carpeta de Migrations, y actualizar los datos en el appsettings.json con las conexiones.

![image](https://user-images.githubusercontent.com/74575681/181801250-a9acd592-ce50-456e-b316-87fe5fd673b0.png)


En la terminal de la consola de paquetes se realizan las Solicitudes:

- add-migration InitialDbAndSeed -Context DisneyConnection
- update-database -Context DisneyConnection

Creación de la base de datos para la autentificación de usuarios:

- add-migration UserDb -Context UserConnection
- update-database -Context UserConnection

-- Datos para SendGrid --

En el archivo appsettings.json deben colocar su Key proporcionada por la api en "Key_SendGrid" y el email que se tomará como el destinatario en  "Email_from_SendGrid".
Si el email que van a utilizar es de Gmail, deben ir a la configuración en el email, al apartado de "reenvío y correo POP/IMAP" y habilitar la opción de "Habilitar acceso IMAP
".

![image](https://user-images.githubusercontent.com/74575681/181801656-ccf59591-67d1-4e10-b5b8-4add36639106.png)


-------- Autentificación de Usuarios --------

* Aclaración: Al subir el repositorio a GitHub olvide quitar la Key de SendGrid y al realizar la documentación, se bloqueo mi usuario temporalmente, al disponer del acceso al a cuenta nuevamente actualizo el readmi.
  Al final dejo una imagen de cómo era la recepción del email cuando uno se logeaba.

-- Registro --

Al registrarse verifica si el nombre de usuario no se encuentra en uso, y al crearlo envía un Email con la información.

![image](https://user-images.githubusercontent.com/74575681/181802693-6213cd9c-4b3f-4fc7-ac17-d90972f86f7d.png)
![image](https://user-images.githubusercontent.com/74575681/181802714-b8286978-183f-4c0d-a3ba-c5b3e9bc41ed.png)

-- Login --

Al loguearse se genera un token el cual es enviado también por email junto a la fecha de expiración.

![image](https://user-images.githubusercontent.com/74575681/182163996-dbde01d8-e939-4df2-b253-2a802655e549.png)


-------- Comprobación del token --------

Se Realiza la confirmación del token para poder utilizar el crud en su totalidad

![image](https://user-images.githubusercontent.com/74575681/182164105-c0802ea6-58dd-4b54-b537-f5fa780a68b3.png)
![image](https://user-images.githubusercontent.com/74575681/182164139-b7318c3c-8d48-4f61-ae45-caf27be1c8cc.png)


-------- Ya se pueden realizar las peticiones --------

Algunos Ejemplos:

* Listado de Personajes:

![image](https://user-images.githubusercontent.com/74575681/182165326-737ede66-e146-4964-82f3-66ac051781a0.png)

* Detalle de Personajes:

![image](https://user-images.githubusercontent.com/74575681/182165572-ff735ab4-12c7-42e8-a397-1e0a97c9401e.png)

![image](https://user-images.githubusercontent.com/74575681/182165616-105a38a6-1283-4348-934b-db2ca4db0897.png)

* Error de personaje ya existente:

![image](https://user-images.githubusercontent.com/74575681/182165893-8b11d03a-e3c4-4a58-8771-4f9ab221b5c4.png)

* Detalles de Peliculas:

![image](https://user-images.githubusercontent.com/74575681/182166023-0aac0deb-33a0-4ab8-a420-4ad71ee0c0f7.png)

* Orden de peliculas por la Fecha:

- Ascendente:

![image](https://user-images.githubusercontent.com/74575681/182166349-588a1f82-3134-41c5-a149-5afd4ebf0315.png)

- Descendente:

![image](https://user-images.githubusercontent.com/74575681/182166427-e4a0f96f-df4f-49e6-97b9-31468ebaf0f1.png)

-------- Testing --------

Para la realización del testing utilice MSTest y un paquete de EFC para el manejo de memoria.

![image](https://user-images.githubusercontent.com/74575681/182168285-0dc4ca89-b2af-4c1f-8c1b-82015c917371.png)

Cree dos constructores para probar el testing, una función que solo emula la base de dato sin ninguna carga y otra que ya carga una base de pruebas. Las dos se encuentran en el archivo ContextBase.cs

* Constructor base:

![image](https://user-images.githubusercontent.com/74575681/182168005-cf739703-5546-4cda-9a44-d74839daf576.png)

* Constructor base con carga de datos:

![image](https://user-images.githubusercontent.com/74575681/182168130-c459ce97-98fb-48a7-994d-ecabc8a68e6b.png)

* Ejecuciones:

![image](https://user-images.githubusercontent.com/74575681/182168656-24313837-2437-43bc-b5a3-d338ae21bf0b.png)


-------- Imagen de muestra del envío del email al Loguearse -------- 


![image](https://user-images.githubusercontent.com/74575681/182168894-da2c6e4d-cd84-413c-8e00-c6476619a88f.png)







# Technical Test 

Para instalar docker en window visita el siguiente link : https://docs.docker.com/desktop/windows/install/


### Run

Asegurarse de instalar el dotnet-ef 
*** dotnet tool install --global dotnet-ef --version 7.0.4 ***

Levantamos el servicio de base de datos
*** docker compose up --build -d technical.test.mssql ***

entrar al projecto de backend y ejecutar las migraciones correspondientes
*** dotnet ef database update --project .\Service.Categories.Api\ ***
*** dotnet ef database update --project .\Service.Security.Api\ ***

Ejecutar todos los servicios
*** docker compose up --build -d *** 


*** 
### Base de datos SQL SERVER 
Descargamos la imagen
  * ***docker pull mcr.microsoft.com/mssql/server***
  
⁠Creamos la instancia de SQL Server en un container
* ***docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Jerios@091311" -p 1433:1433 -d mcr.microsoft.com/mssql/server⁠*** 

Nos conectamos a nuestra base de datos por medio de cualquier cliente que acepte conexión a SQLSERVER EX: SSMS, DBeaver. Creamos 2 bases de datos. 

* Security
* Categiries

***

### Clonamos el repositorio

git clone https://github.com/Julianrios84/technical-test.git


***

### Backend

Abrimos el editor visual studio comunity, en el lado derecho en la parte de ***Soluction Explore*** en la solucion damos click derecho seleccionamos propiedades, configuramos el ***Multiple stratup project*** ponemos los 3 projectos en ***start***

Antes de correr nuestros projectos tenemos que correr las migraciones con el ***ENTITY FRAMEWORK***, para esto usamos el ***Package Manager Console***. Esta consola la encontramos en la parte superior del edito en ***Tools -> NuGet Package Manager -> Package Manager Console***.

Se nos abrira una consola, en la parte superior derecha ***Default Project*** selecionamos los projectos:

1) Selecionamos ***Service.Security.Api*** corremos ***Add-Migration*** Initial si no hay ninguna migracion, de lo contrario usamos ***Update-Database*** generar las tablas.

2) Selecionamos ***Service.Categories.Api*** corremos ***Add-Migration*** Initial si no hay ninguna migracion, de lo contrario usamos ***Update-Database*** generar las tablas.

***

### Frontend


Abrimos el editor visual stuido code. precionamos ***ctrl + ñ***, verificamos que la consola este apuntando a la carpeta de frontend del projecto.

Para correr el projecto de angular seguimos los siguiente pasos:

* **npm install**
* **npm run start**

***



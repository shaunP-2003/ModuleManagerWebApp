Read Me file for Module web app 



Information to run the app

- Create the database in the sql server manager studio with the scripts provided in the folder database scripts
Copy paste the script to create the database. 

- Then go to the connection string in the files appsettings.json to change the connection string to your server 
and initial catolog name(Database name) Example: My connection string 
:"ModAppIdentityDbCConnection": "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=ModuleWebAppDb;Integrated Security=True"

My servername : lab000000\SQLEXPRESS
 Aunthetication : Windows Authentication 

Then having a successful connection string will have the application working after the database has been 
successfully linked to visual studio. 
To check for successful connection navigate to the Server Explorer then check if the database name is there. ModuleWebAppDb.

OPTION 

- If you want to create a new database name : 
then you must got to the sql server management studio create a new database then excute a new query inside copy paste 
the script and remove the very top of the script : USE [ModuleWebAppDb] remove that line to successfully create the database.

Then once theres a successful connection run the application and follow the user manual to use the application. 


Demonstration Video of the application : https://youtu.be/p6MJTe6YSKk
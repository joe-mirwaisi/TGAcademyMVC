# TGAcademyMVC
Repository for the Techtonic Group - Academy | Portal

CreateRoles method in Startup function makes sure that the base roles of "Prospective_Student", "Current_Student", "Mentor", and "Admin" exists in the ASPNetRoles table of the database. It then checks for or creates the test users and makes sure they are assigned to the correct roles. This is done using the built in ASPNet UserManager and RoleManager.

Updating the Database:
If you need to add a Table or Form to the Database you will have to run a migration from the command line.
1) Go to to Tools -> NuGetPackage Manager -> Package Manger Console. This will open up the console in Visual Studio for you.
2) Type in Add-Migration
3) You will be prompted for a name, this can't be the name of any of the exsisting migrations in the Data/Migrations folder in the SolutionExplorer. If you have done a recent Migration that you do not need anymore you can delete that file and then use the same name.
4) Attempt to update the database with the update-database command.
5) If the change you were expecting does not happen, go to the Migration file you creaeted and remove all the text in the Up() function leaving only the function call behind. Then run update-database again.
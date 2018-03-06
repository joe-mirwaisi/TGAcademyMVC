# TGAcademyMVC
Repository for the Techtonic Group - Academy | Portal

CreateRoles method in Startup function makes sure that the base roles of "Prospective_Student", "Current_Student", "Mentor", and "Admin" exists in the ASPNetRoles table of the database. It then checks for or creates the test users and makes sure they are assigned to the correct roles. This is done using the built in ASPNet UserManager and RoleManager.
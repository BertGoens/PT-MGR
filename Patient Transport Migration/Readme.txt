##Installs
Install Entity Framework from Nuget Packages Manager and add a reference to the Model project 
install-package entityframework -version 5.0.0.0

##Structure

/Migrations
	Configuration.cs

/Models
	Context.cs

	/DAL (Data-Acess-Layer)
		Database Classes

/Controllers
	Template Create
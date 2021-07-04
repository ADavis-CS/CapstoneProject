PRODUCTION NOTES: 


1. Had issues with adding roles into DB from the Seed Method. 
		Resolved by running the following commands: 
			add-migration Initial -configurationtypename membershipconfiguration -ignorechanges
			update-database -configurationtypename membershipconfiguration -force


2. When encountering an issue with new model columns not being added to database on update-database command
	a. Need to delete all previous migration history files 
	b. need to delete from _migrationHistory table by running SQL query
	c. Then delete the actual tables i.e. collectionobjects and collectionobjectimages
	d. run the following NPM command (remember in the add migration you're not adding -ignorechanges:  
			Add-Migration Initial -configurationtypename configuration
			update-database -configurationtypename configuration -force


3. When updating the database to apply changes in model to sql server
	run the following command in the Visual Studio NPM console to generate a sql script 
		update-database -configurationtypename configuration -script
	execute the script in production environment against the db
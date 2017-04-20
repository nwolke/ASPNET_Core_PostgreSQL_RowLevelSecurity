# ASPNET_Core_PostgreSQL_RowLevelSecurity

## Purpose

This project is to showcase my solution for creating dynamic connection strings needed for connecting to a PostgreSQL database that is setup for Row Level Security.

Unfortunately, there is no easy way to unit or supply an integration test for this solution as it requires a PostgreSQL database with Row Level Security setup on it. I will provide some simple instructions though on how to set up one.

#### PostgreSQL SETUP

Follow the instructions at this link to setup a PostgreSQL DB on Windows: https://www.labkey.org/Documentation/wiki-page.view?name=installPostgreSQLWindows

#### Row Level Security Setup

Before this can happen, please ensure that the table to have this policy has already been migrated from the project or created otherwise in the database.

See the following documentation on Row Level Security: https://www.postgresql.org/docs/9.5/static/ddl-rowsecurity.html

Example code for this project:

```SQL
	ALTER TABLE testmodel ENABLE ROW LEVEL SECURITY

	CREATE POLICY testmodel_rls ON testmodel
		using (rowsecuritycolumn = current_user)
```
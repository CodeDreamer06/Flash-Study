# Flash Study
A simple application that helps you study using spaced repetition in fully featured stacks
## Usage and help
*note: type help to display this message again*

 - **1 - Study -** Learn or revise from your flashcards
 - **2 - Manage flashcards** - Create, add or edit flashcards and stacks
 - **0 - Exit -** Leave the application

## Instructions
To get started, create an `app.config` file in the root of the project. Then paste in the following XML along with the connection string for your SQL.

<?xml version="1.0"?>
<configuration>
    <connectionStrings>
        <add name="defaultConnStr"
            connectionString="server=localhost;user=root;port=3306;password=abhikij761"
            providerName="MySql.Data.MySqlClient"/>
        <add name="connStr"
             connectionString="server=localhost;user=root;database=study;port=3306;password=abhikij761"
             providerName="MySql.Data.MySqlClient"/>
    </connectionStrings>
</configuration>

To import the database, enter the following command in your terminal:
  mysql -u [your username] -p study < study.sql

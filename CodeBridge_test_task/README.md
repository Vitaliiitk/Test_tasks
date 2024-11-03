MY COMMENT

I have created a sample REST API application that allows to work with a database (extracting items from the database, adding items to the database). 
I tried to implement every single condition of the task (look below "SETTING OF THE PROBLEM" description). For the database, I used the MS SQL database. 
I do not attach here a backup file of the database, so if you want to run the application on your PC you will need to have the SQL Server Management studio 
to be installed. Also, you will need to change a ConnectionString parameter in the appsettings.json file. On your PC you have to create it by yourself and 
substitute my connection string in folder Dogs.Host/appsettings.json.

In addition to fulfilling all the task's requirements, I added testing to the project (xUnit project). 
I have added a validation where it is necessary, in my opinion.

===================================================================================================================================================

SETTING OF THE PROBLEM

Necessary to develop a sample REST API using C#. There is an MS SQL database with just one table: select top(2) * from dogs limit 2;

|name| |color|          |tail_length| |weight| 
|Neo| |red & amber|     |22|          |32| 
|Jessy| |black & white| |7|           |14| 
(2 rows)

Need to develop the following API on top of it:

1. Your application should have an action called "Ping". It should work in the following way:
curl -X GET http://localhost/ping should return the following message: "Dogshouseservice.Version1.0.1"

2. Your application should have an action that allows querying dogs. It should work in the following way: 
curl -X GET http://localhost/dogs should return the following:

[ { "name": "Neo", "color": "red&amber", "tail_length": 22, "weight": 32 },

{ "name": "Jessy", "color": "black&white", "tail_length": 7, "weight": 14 } ]

API should support sorting by attribute, for example, 
curl -X GET http://localhost/dogs?attribute=weight&order=desc 
API should support pagination, for example,
curl -X GET http://localhost/dogs?pageNumber=3&pageSize=10 Both sorting and pagination should work together.

3. Application should have an action that allows creating dogs. It should work in the following way: curl -X POST http://localhost/dog
-d "{"name": "Doggy","color":"red","tail_length": 173,"weight": 33}"

As a result, a new dog should be created. Please think about the following cases:

Dog with the same name already exists in DB.
Tail height is a negative number or is not a number. 
Invalid JSON is passed in a request body. 
Other cases that need to be handled in order for API to work properly.

4. Need to implement logic that handles situations when there are too many incoming requests to the application, so those could not be handled. 
There should be a setting that says how many requests the service can handle, for example, 10 requests per second. In case there are more incoming
requests than in configuration application should return HTTP status code "429TooManyRequests".

Here are a few additional requirements for the task:

Use ASP .Net Core Web API. 
Use EF Core code first for the database creation (any database).
Use async-await where it's possible. 
All logic in the application should be covered by unit tests. 
Please show knowledge of different software development patterns.

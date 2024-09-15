
The main idea of the project is to train how to write real-time chat application with sentiment analysis. Link to deployed program on Azure: 
https://reenbitchat.azurewebsites.net/

The demo project was taken as a basis, on the basis on which the software application was built. Link to the project: 
https://github.com/aspnet/AzureSignalR-samples/tree/main/samples/ChatRoomLocal

The entire program was written within the framework of one ecosystem - .NET, for part of the web it was used a razor page approach.

The program uses Azure cloud technologies: Azure SignalR Service, Azure SQL database, Azure WEB App Service, Azure AI Text Analytics.

SignalR is a library for .NET developers that simplifies the process of adding real-time web functionality to applications. It allows server-side
code to push content to connected clients instantly as it becomes available, without the client needing to request it explicitly. Azure SQL 
database is used to store messages. Azure WEB App Service allows us to deploy application/publish it. Azure Cognitive Services Text Analytics is
a cloud-based service provided by Microsoft Azure that allows developers to integrate powerful natural language processing (NLP) capabilities
into their applications. This service offers a range of pre-built models to extract insights from text, including sentiment analysis.

On my part, work was done to integrate all these parts into the real-time chat. In case you want to download the sample, and run it under your
Azure account, you will need to go through the steps to get the program working:

1. Register an account on the Azure portal.
2. Create an Azure Web App for .NET Core.
3. Create an Azure SignalR Services, need to pay attention to connection strings after creation, under your Azure account you will have to change it.
4. Create an Azure SQL database, again, need to pay attention to the connection string. To work with databases I use the Entity Framework library.
   Need to keep in mind migrations after database is created and connected to the project, without migrations there will be no tables.
5. Create Azure Cognititve Services Text Analytics, remember about connection strings.

# City of Pacopolis Online Voting

Colman Scharff, 
Mac DePriest, 
Keyik Annagulyyeva,
Mason Shimek,
Brandon Clear,
Joseph Allen

This is the capstone project for Group 10 in CSCE 361

The API directory contains all the backend code in c#. The file system is self explanatory to anyone familiar with MVC or iDesign. It depends on a local Mircosoft SQL server. Simply starting the program will create the necessary tables and populate them. 
*Note*: If you sign up, kill the dotnet program, and start it again, your account will be deleted because the DB is deleted and recreated each time the program is ran. 


The website has three primary pages which are quite similar: Past, Present, and Future Elections. Each election has multiple positions to vote for, with information about the candidate available if you click on them. You can also filter past and future elections by year. Of course, you can vote for the current election by hovering over the top right of a candidate, clicking "vote now", and confirming your vote. You will then see the candidate card color change, indicating who you voted for, and you will no longer be able to vote for that position. 

You can sign in with email 'john.doe@example.com' and password 'Password123', or create an account with your real email, then click on "confirm email", and you should be sent an email with a 10 character code.

There's also an account page where you can view and edit your account information.

To run the backend:
```
cd API
dotnet add package MailKit --version 4.5.0
dotnet add package Microsoft.Extensions.Configuration
dotnet run
```

To run the frontend:
```
cd view/src
npm install
npm start
```



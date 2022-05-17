# EmailApi
The project has a class library (DLL) which can be reused for sending email. It features 2 servers, 1 for the back-end and 1 for the front-end. This architecture essentially simulates what it would be like to operate a 3rd party public API call.

## Overview
The project was created with the following constraints in mind:
 - The user must not be interrupted/delayed in navigating the website because of an email failing to send.
 - Email method should be in a class library (DLL) which can be reused through different apps/entry points.
 - To, from, subject, body, attempt status and the date must be stored with each attempt.
 - Email should be retried until success or a maximum of 3 times, whichever comes first.
 - All credentials should be stored in appsettings, rather than hardcoded.
 - Bonus for having a front-end call the API to send the e-mail.

## Technologies
The project uses the following tech:
 - Back-end built with C# using an ASP.NET Web API template.
 - Front-end built with C# using an ASP.NET MVC template with Razor pages.
 - SQLite database, initially created with Entity Framework (migrations).

## Setup
Download DB Browser for SQLite (https://sqlitebrowser.org/dl/).
  - This will allow us to quickly & easily view logging to the database.

Download Postman (https://www.postman.com/downloads/).
  - This will allow API testing. Note that Swagger API testing is part of the back-end already.

Download Visual Studio Code (https://visualstudio.microsoft.com/vs/community/).
  - This will allow us to use an IDE to examine the code & easily clone the project.


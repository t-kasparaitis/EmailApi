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
 - Back-end built with C# using an ASP.NET Core Web API template.
 - Front-end built with C# using an ASP.NET Core Web App template with Razor Pages (view engine).
 - SQLite database, initially created with Entity Framework (migrations).

## Setup
Here's what to do to run this project on your local machine.

### Preparation
Download DB Browser for SQLite (https://sqlitebrowser.org/dl/).
  - This will allow us to quickly & easily view logging to the database.

Download Postman (https://www.postman.com/downloads/).
  - This will allow API testing. Note that this is optional as Swagger API testing is part of the back-end already.

Download Visual Studio Code (https://visualstudio.microsoft.com/vs/community/).
  - This will allow us to use an IDE to examine the code & easily clone the project.

Register with Mailtrap.io or similar service (https://mailtrap.io/register/signup).
  - This will allow us to catch e-mails without sending them from/to real inboxes.

### Chronological Steps
  1. After installing Visual Studio Code, clone this repository.
  2. We have one solution with multiple projects - configure multiple startup projects.
     - Right click on the solution, select "Set Startup Projects".
     - Choose "Multiple startup projects".
     - Set Action to "Start" for "FrontEnd" & "WebApi". Click "Apply" & "OK".
  3. The front-end will start on https://localhost:5003/. This is the website for using an email form.
  4. Swagger (back-end API documentation/testing) will start on https://localhost:5001/swagger/index.html.
     - The back-end is listening for API calls on http://localhost:5000/.
  5. Note: Closing either one of the start-up browser windows will stop the program.
     - There is a bug with either Chrome or Visual Studio Code, where debugging exits (program appears to crash) if tabs are merged. The pages have to stay as single pages or else the program crashes/exits unexpectedly.

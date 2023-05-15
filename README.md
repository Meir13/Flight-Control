# Flight Management System

This is a flight management system that simulates airport operations. It includes features such as flight scheduling, leg occupation tracking, and multi-threaded processing. The system is built using C# and ASP.NET Core on the server side, with a React.js client-side application.
![Screenshot (113)](https://github.com/Meir13/Flight-Control/assets/133117732/0012364a-b1b6-43d8-9a2b-6942552795ee)


## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Technologies](#technologies)

## Installation

To run the flight management system locally, follow these steps:

1. Clone the repository to your local machine.
2. Open the server project in your preferred development environment.
3. Set up the database connection and configure the necessary environment variables (You can see in the models how it should be like).
![Screenshot 2023-05-15 122328](https://github.com/Meir13/Flight-Control/assets/133117732/463afe32-2623-4867-b4c5-49f267588259)

5. Run the server project.
6. Run the simulator project.
7. Open the client project in a separate terminal or development environment.
8. Install the project dependencies using `npm install`.
9. Start the React client application using `npm start`.

## Usage

The flight management system provides the following functionalities:

- Adding new flights and scheduling them.
- Tracking the occupation status of different legs of the airport.
- Simulating flight movements and updating their status.
- managing flights to avoid collision.
- Real-time updates through WebSocket using SignalR library.

## Technologies

The flight management system is built using the following technologies:

- C# and ASP.NET Core for the server-side application.
- React.js for the client-side application.
- Entity Framework Core for database access.
- SignalR for real-time communication.
- SQL Server for data storage.

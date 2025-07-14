# ✈️ OnTheSky - Flight Management System

**OnTheSky** is a full-stack web application for managing flights and flight bookings.  
The system is designed for both regular users, who can view and book flights, and administrators, who can manage flight and booking data.  
The project consists of a **client-side built in Angular** and a **server-side built in ASP.NET Core Web API**.

---

## 🧭 Project Overview

- Display a list of flights by country
- Allow registered users to book flights
- Enable administrators to manage flights and bookings
- Role-based access control (User / Admin)
- User-friendly UI with search, filters, and forms

---

## 🖥️ Client-Side (Angular)

- Developed using **Angular**
- Login and registration forms with input validation
- Flight list view available to all logged-in users:
  - Flights are grouped and displayed by **country**
  - Users can filter/search flights by country name
- Flight booking form (Travel) allowing users to select a flight and enter personal details
- Admin-only features:
  - **Flight management** page (Full CRUD) – including assignment to countries
  - **Booking management** – approve, decline, or extend existing bookings
- Uses Angular `HttpClient` to communicate with the ASP.NET Core backend via RESTful APIs
- Routing and access control based on user role (`User` / `Admin`)
- Data models (interfaces) for `User`, `Flight`, `Travel`, and `Country`
- Styled with CSS and optionally Tailwind for a responsive, clean UI

---

## 🧑‍💻 Server-Side (ASP.NET Core Web API)

- Built using **ASP.NET Core Web API**
- Uses **Entity Framework Core** for data access and database interactions
- Main models:
  - `User`: Represents the application user with `Id`, `UserName`, `Password`, and `Role` (User/Admin)
  - `Flight`: Represents a flight with fields like `Id`, `Date`, `Price`, and a link to a country
  - `Travel`: Represents a booking made by a user for a flight
  - `Country` (formerly `Places`): Represents a country with:
    - `countryid`: Primary key
    - `country`: Country name (e.g., "France", "USA")
    - `flight_list`: A list of all flights associated with that country
- Entity relationships:
  - One `Country` has many `Flights`
  - One `Flight` belongs to one `Country`
  - One `Travel` is linked to one `Flight` and one `User`

- DTOs used to transfer only the necessary data via API
- Controllers:
  - `AuthController` – Handles user registration and login
  - `FlightController` – Handles CRUD operations for flights
  - `TravelController` – Handles creation and management of bookings
  - (Optional) `CountryController` – Can be added to manage/view countries
- Configured with CORS and ready for JWT authentication if needed

---

## 🗂️ Project Structure
 OnTheSky/  
      ├── OnTheSky-Client/       # Angular frontend application  
      └── OnTheSky-Server/       # ASP.NET Core backend API

## 🚀 Getting Started

### Server (ASP.NET Core)
cd OnTheSky-Server  
dotnet restore  
dotnet run  
### Client (Angular)
cd OnTheSky-Client/OnTheSky  
npm install  
ng serve  

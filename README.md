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
- Login and registration forms with field validations
- Flight list view available to all logged-in users
- Flight booking form (Travel) allowing users to select a flight and enter details
- Admin-only features:
  - Flight management page (Full CRUD)
  - Booking management with approval/decline/extension functionality
- Uses Angular `HttpClient` to communicate with the backend
- Routing based on user role (`Role`)
- Styled with CSS and Tailwind for responsive and clean UI

---

## 🧑‍💻 Server-Side (ASP.NET Core Web API)

- Built using **ASP.NET Core Web API**
- Uses **Entity Framework Core** for data access and database interactions
- Main models:
  - `User`: `Id`, `UserName`, `Password`, `Role` (enum)
  - `Flight`: destination, date, price, etc.
  - `Travel`: represents a flight booking by a user
- DTOs used to transfer only necessary data via API
- Controllers:
  - `AuthController` – handles login and registration
  - `FlightController` – manages flight data
  - `TravelController` – handles flight bookings
- Configured with CORS and ready for JWT integration if needed

---

## 🗂️ Project Structure
OnTheSky/
├── OnTheSky-Client/       # Angular frontend application
└── OnTheSky-Server/       # ASP.NET Core backend API


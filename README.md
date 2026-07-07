#TechnoLab Booking Management System

A C# console application built to manage equipment booking requests for Belgium Campus's Technology Exploratorium (TechnoLab), where students can book equipment such as drones, VR headsets, microcontrollers, and 3D printers.

This was a group project for Programming 261 (Belgium Campus iTversity).

## Features
Capture Booking Requests — Records student details (name, student number, year of study, equipment type, booking duration, training status, and active bookings) and stores them for processing.
Evaluate Booking Eligibility— Applies business rules to determine whether each booking is approved, conditionally approved, or rejected, based on:
  - Completion of required training
  - Requested booking duration
  - Number of currently active bookings
Display Booking Statistics— Shows total, approved, and rejected booking counts. Approved bookings are displayed in priority order based on year of study, active booking count, and booking duration.
Menu-driven interface— Built using a C# enum to navigate between the above options.

## Tech Stack

Language: C#
Platform: .NET Console Application

## How to Run
1. Clone or download this repository.
2. Open `TechnoLabBookingManagementSystem.sln` in Visual Studio.
3. Build and run the project.
4. Use the on-screen menu to capture bookings, evaluate eligibility, and view statistics.

## Project Context
Developed as part of a 3-member group project for the Programming 261 module, focusing on collections, conditional logic, and multi-criteria sorting in C#.

# Nawras Project 🌊

## OverView

- Nawras is a cross-platform application designed for tourists and boat owners. It allows tourists to browse and book sea trips while giving boat owners the ability to manage trips and list their boats. The system is inspired by social media but focused on sea travel experiences.

## Requirements (or Features / Functional Requirements) 📌

- baisiclly it's a social media but for boats and sea vehicls
- you can register/login as:
  - a tourst and a tourist can do is:
    - register
    - login
    - see available trips (you can only book one trip at a time)
    - when you book the trip it has a page that describe the trip like:
      - where it's going
      - start and end dates
      - captain
      - owner of the boat (sometimes he is the captain specially in small boates senarios)
      - rating (after the trip end and of course it can be a separate system by itself if i want to expand in the future)
  - a boat owner
    - register
    - login
    - apply to list your boat in the system for the people to book (the system support the scenario of many owners and support if they are co-own the vessel as in amature relationship like family or friends and or if are a comercial entity)

    - see your boats list
    - create trips (You can only book one trip at a time and when you want to book more than one trip at the same time you must hire a captain to do that)

## coding standards 🟡

### naming conventions

- Element Convention Example
- Class Name PascalCase Employee, Department prefixed with (cls)
- Controls Name PascalCase frmEmployee, ctrlDepartment prefixed with abreviation from the control name
- Property Name PascalCase FirstName, HireDate
- Private Fields _camelCase_firstName
- Methods PascalCase GetFullName()
- Local variables / parameters camelCase employee, hireDate
- Tables: use snake_case, plural form → employees, departments (in the schema design write it singular like employee and in the database itself writet in a group manner like employees)
- Columns: use snake_case → employee_id, first_name
- Primary keys: id, or tablename_id → employee_id
- Foreign keys: reference original table name → department_id, country_id

## Tech Stack

- Frontend: Angular
- Backend: ASP.NET Web API (C#)
- Database: SQL Server
- UI Framework: Bootstrap

## Future Expansion Ideas 🧠

- Dedicated captain and boat rating system
- Admin dashboard for managing users and trips
- Messaging system between tourists and boat owners

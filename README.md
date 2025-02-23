## Description

---

A basic Quotation and Order Management Web App for Enterprise Operations Management using ASP.Net, Vue JS and MySQL.

![Static Badge](https://img.shields.io/badge/Backend-purple?logo=dotnet)
![Static Badge](https://img.shields.io/badge/Frontend-black?logo=vuedotjs)
![Static Badge](https://img.shields.io/badge/Components_Framework-blue?logo=vuetify)

## Features

---

- CRUD Operations on Models
- Scalable Model Addition Approach
- DTO Validation for POST and PUT Operations on backend
- Generic Repository and Controller - Easier to add more models
- Pagination with Filtering and Sorting
- Pre-fill forms when updating records on front-end
- Autocomplete fields when filling forms for Post and Put Operations

## Packages Used / Dependencies

---

- Fluent Validation - For PostDTO Backend Validation
- AutoMapper - For Model <-> DTO mapping
- Vuetify - Provides Material Design and Responsive Components on frontend

## TODO

---

- Caching
- Orders Model
- Front-end Dashboard
- Middleware Exception Handler
- Quotation Price Suggestion on Front-end

## Backend Directory Structure

---

/Controllers
: Handles the endpoints for a specific model (Table in the database)

/Data
: Contains the Project Specific DbContext Class and DateOnly Type Converter for DbContext use

/DTOs
: Stores the specific data structure and format related to a model (in most cases) that need to be passed between frontend and backend for CRUD Operations.

/Migrations
: The scripts run to update the database based on changes made to models

/Models
: The data structure of the records for each specific tables in the database

/Services
: Class Objects injected into other objects for:
- Model and database operations, in the case of repositories.
- Post and Put DTO Validation for Validators.
- Object Converters (DateOnly Converter Class in the JSON Parser for this project)

MappingProfile.cs
: Defines what class object can be mapped to the other and how to map properties if not directly convertable.

## Frontend Directory Structure

---

src/components
: Vue Components to be re-used in the template tags of the parent components

src/services
: Utility functions to format strings and make API Requests to the backend, in this case.
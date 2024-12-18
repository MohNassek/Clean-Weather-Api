# Weather API - Clean Architecture

This is a simple Weather API project built with .NET Core, implementing clean architecture principles. The API fetches weather data from an external weather API, stores it in a PostgreSQL database, and provides CRUD operations via HTTP endpoints.

## Project Structure

This project follows a clean architecture approach with the following folder structure:


### Key Components:
- **WeatherApi.Application**: Contains service classes and interfaces that implement business logic for fetching and managing weather data.
- **WeatherApi.Domain**: Contains entities representing the application's core objects and interfaces for repositories.
- **WeatherApi.Infrastructure**: Contains repository classes and database context that interact with PostgreSQL.
- **WeatherApi.API**: Contains the API controllers that expose endpoints to interact with the application.

## Prerequisites

To run this project, you need the following:

- .NET 8.0 SDK or later
- PostgreSQL database
- API key for weather data (e.g., OpenWeatherMap or similar service)

## Setup Instructions

### 1. Clone the repository

Clone this repository to your local machine:

```bash
git clone https://github.com/yourusername/weather-api-clean-architecture.git
cd weather.APi

 

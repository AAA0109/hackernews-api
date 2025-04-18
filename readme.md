# Hacker News API

A robust .NET Core API that provides a clean interface to the Hacker News API with caching, pagination, and search capabilities.

## Overview

This project is a RESTful API service built with ASP.NET Core that wraps the official Hacker News API. It enhances the original API by adding:

- In-memory caching to improve performance
- Pagination for better data consumption
- Search functionality to filter stories
- Standardized response format

## Architecture

The application follows a clean architecture pattern with the following components:

### Core Components

- **Controllers**: Handle HTTP requests and responses
- **Services**: Implement business logic and external API communication
- **Models**: Define data structures used throughout the application

### Key Features

- **Caching**: Uses ASP.NET Core's `IMemoryCache` to cache story data for 30 minutes
- **CORS Support**: Configured to allow cross-origin requests from any origin
- **Error Handling**: Standardized error responses
- **TLS Security**: Enforces TLS 1.2 for all communications

## API Endpoints

### Get Stories

```
GET /api/stories
```

#### Query Parameters

| Parameter | Type   | Description                                  | Default |
|-----------|--------|----------------------------------------------|---------|
| query     | string | Optional search term to filter story titles  | ""      |
| page      | int    | Page number for pagination                   | 1       |
| pageSize  | int    | Number of stories per page                   | 10      |

#### Response Format

```json
{
  "message": "",
  "success": true,
  "data": {
    "page": 1,
    "total": 100,
    "pageSize": 10,
    "stories": [
      {
        "id": 1,
        "title": "Example Story",
        "url": "https://example.com",
        "score": 100,
        "time": 1609459200,
        "by": "username"
      }
    ]
  }
}
```

## Technical Implementation

### Dependency Injection

The application uses ASP.NET Core's built-in dependency injection container to manage service lifetimes:

- `ICacheService`: Scoped service for story retrieval and caching
- `HttpClient`: Configured with base URL from configuration and custom TLS handling

### Caching Strategy

Stories are cached individually by ID to optimize memory usage and allow for partial cache invalidation. The cache service implements:

- Cache key generation based on story ID
- Automatic cache expiration after 30 minutes
- Fallback retry mechanism for API failures

### Error Handling

The application provides standardized error responses through the `Response` class, which encapsulates:

- Success/failure status
- Error messages
- Structured data payload

## Development Setup

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code with C# extensions

### Clone the repository:

```bash
git clone https://github.com/aaa0109/hackernews-api.git
cd hackernews-api
```

### Configuration

Add the following to your `appsettings.json`:

```json
{
  "HackerNewsApi": {
    "BaseUrl": "https://hacker-news.firebaseio.com/v0/"
  }
}
```

### Running the Application

```bash
dotnet restore
```

```bash
dotnet build
```

```bash
dotnet run --project HackerNewsApi
```

The API will be available at `https://localhost:5001` and `http://localhost:5000`.

### Running Tests

```bash
cd .\HackerNewsApi.Tests\
dotnet test
```

## Testing

The project includes unit tests for controllers and services using:

- xUnit as the test framework
- Moq for mocking dependencies

Key test scenarios include:
- Successful story retrieval
- Error handling
- Parameter validation

## Performance Considerations

- In-memory caching reduces load on the Hacker News API
- Pagination limits response size for better network performance
- Asynchronous processing with `Task.WhenAll` for parallel story retrieval

## Security

- HTTPS redirection enabled
- TLS 1.2 enforced
- CORS policy configured to allow specific origins

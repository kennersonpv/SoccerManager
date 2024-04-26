# Soccer Manager API

## Goal

The goal of this project is to create a RESTful API for a fantasy football application. Football/soccer fans will be able to create fantasy teams and perform player transactions like buying or selling players.

## How to Run

### Using Docker

To run the application using Docker, follow these steps:

1. Build the Docker image:

    ```bash
    docker-compose build --no-cache
    ```

2. Run the Docker containers in the background:

    ```bash
    docker-compose up -d
    ```

### Running Migrations

To apply database migrations, use the following command:

```bash
dotnet ef database update

### Improvements

Here's a few improvements I would do if i got more time:
- Get list of players of users team
- Create Unit tests
- Create validation for inputs

@echo off
setlocal

REM Check if Docker is installed
docker --version >nul 2>&1
if errorlevel 1 (
    echo Docker is not installed. Opening Docker installation documentation...
    start https://docs.docker.com/get-docker/
    echo Please install Docker and try again.
    pause
    exit /b 1
)

REM Civic Integrity Hub API Runner Script
REM Usage: civic start [mysql]  - Start MySQL only or all services
REM        civic stop  [api]       - Stop all services
REM        civic build          - Build all services
REM        civic logs           - View logs
REM        civic clean          - Stop and remove volumes

if "%1"=="start" (
    cd src\Infrastructure
    if "%2"=="mysql" (
        echo Starting MySQL...
        docker-compose up mysql -d
    ) else (
        echo Starting all services...
        docker-compose up --build
    )
    cd ..\..
) else if "%1"=="stop" (
    cd src\Infrastructure
    if "%2"=="api" (
        echo Stopping API service...
        docker-compose stop api
        cd ..\..
        exit /b 0
    ) else (
    
    echo Stopping all services...
    docker-compose down
    cd ..\..
    )
) else if "%1"=="build" (
    cd src\Infrastructure
    echo Building all services...
    docker-compose build
    cd ..\..
) else if "%1"=="logs" (
    cd src\Infrastructure
    echo Viewing logs...
    docker-compose logs -f
    cd ..\..
) else if "%1"=="clean" (
    cd src\Infrastructure
    echo Stopping and removing volumes...
    docker-compose down -v
    cd ..\..
) else (
    echo Usage: civic start [mysql] ^| stop ^| build ^| logs ^| clean
    echo.
    echo Examples:
    echo   civic start       - Start all services with build
    echo   civic start mysql - Start MySQL only
    echo   civic stop api    - Stop all services or just api
    echo   civic build       - Build services without starting
    echo   civic logs        - View real-time logs
    echo   civic clean       - Stop and remove all data
)

endlocal
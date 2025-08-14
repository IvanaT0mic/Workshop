@echo off
REM ==================================================================
REM OrderManagement Test Coverage Script
REM Runs all tests with coverage and generates HTML report
REM ==================================================================

REM Check if we're in the right directory
if not exist "OrderManagement.sln" (
    echo ERROR: This script must be run from the OrderManagement directory!
    echo Current directory: %CD%
    pause
    exit /b 1
)

REM Clean up previous coverage files
echo Cleaning up previous coverage files...
if exist "coverage-html" rmdir /s /q "coverage-html"
if exist "OrderManagement.UnitTests\coverage.opencover.xml" del "OrderManagement.UnitTests\coverage.opencover.xml"
if exist "OrderManagement.UnitTests\coverage" rmdir /s /q "OrderManagement.UnitTests\coverage"
if exist "OrderManagement.IntegrationTests\coverage.opencover.xml" del "OrderManagement.IntegrationTests\coverage.opencover.xml"
if exist "OrderManagement.IntegrationTests\coverage" rmdir /s /q "OrderManagement.IntegrationTests\coverage"
echo.

REM Run Unit Tests with Coverage
echo ================================
echo Running Unit Tests with Coverage
echo ================================
dotnet test OrderManagement.UnitTests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml
if errorlevel 1 (
    echo ERROR: Unit tests failed!
    pause
    exit /b 1
)
echo.

REM Run Integration Tests with Coverage
echo ========================================
echo Running Integration Tests with Coverage
echo ========================================
dotnet test OrderManagement.IntegrationTests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml
if errorlevel 1 (
    echo ERROR: Integration tests failed!
    pause
    exit /b 1
)
echo.

REM Generate combined HTML report
echo ===========================
echo Generating HTML Coverage Report
echo ===========================
reportgenerator ^
    -reports:"OrderManagement.UnitTests\coverage.opencover.xml;OrderManagement.IntegrationTests\coverage.opencover.xml" ^
    -targetdir:"coverage-html" ^
    -reporttypes:Html ^
    -title:"OrderManagement Coverage Report"

if errorlevel 1 (
    echo ERROR: Failed to generate HTML report!
    echo Make sure ReportGenerator is installed: dotnet tool install -g dotnet-reportgenerator-globaltool
    pause
    exit /b 1
)

echo.
echo ===========================
echo Coverage Report Generated!
echo ===========================
echo Report location: %CD%\coverage-html\index.html
echo Opening coverage report in browser...
start coverage-html\index.html

echo.
echo ================================
echo Coverage analysis complete!
echo ================================
pause

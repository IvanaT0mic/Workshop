#!/bin/bash

# check if we're in the right directory
if [[ ! -f ./OrderManagement.sln ]]; then
  echo "ERROR: Script must be run from the OrderManagement directory!"
  echo "Current directory is ${PWD}"
  exit 1
fi

# checking for report generator tool
#
echo "Checking for reportgenerator in PATH"
if ! command -v reportgenerator >/dev/null 2>&1; then
  echo "reportgenerator not found!"
  echo "please install reportgenerator!"
  exit 1
else
  echo "reportgenerator already installed"
fi

# clean up previous coverage files
echo ===========================
echo "cleaning up previous coverage files..."
echo ===========================
rm -rf ./coverage-html
rm -rf ./OrderManagement.UnitTests/coverage.opencover.xml
rm -rf ./OrderManagement.UnitTests/coverage
rm -rf ./OrderManagement.IntegrationTests/coverage.opencover.xml
rm -rf ./OrderManagement.IntegrationTests/coverage

echo "cleaned up previous files"

echo "Running Unit Tests with Coverage..."
dotnet test OrderManagement.UnitTests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml;
RESULT=$?
if [ $RESULT -ne 0 ]; then
  echo "ERROR: Unit tests failed!"
  exit 1
fi

echo ===========================
echo "Running Integration Tests with Coverage..."
echo ===========================
dotnet test OrderManagement.IntegrationTests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml;
RESULT=$?
if [ $RESULT -ne 0 ]; then
  echo "ERROR: Unit tests failed!"
  exit 1
fi

echo ===========================
echo Generating HTML Coverage Report
echo ===========================
reportgenerator \
    -reports:"OrderManagement.UnitTests/coverage.opencover.xml;OrderManagement.IntegrationTests/coverage.opencover.xml" \
    -targetdir:"coverage-html" \
    -reporttypes:Html \
    -title:"OrderManagement Coverage Report"

RESULT=$?
if [ $RESULT -ne 0 ]; then
  echo ERROR: Failed to generate HTML report!
  echo Make sure ReportGenerator is installed: dotnet tool install -g dotnet-reportgenerator-globaltool
  exit 1
fi


echo ===========================
echo Coverage Report Generated!
echo ===========================
echo Report location: ${PWD}/coverage-html/index.html
if command -v xdg-open >/dev/null 2>&1; then
  echo Opening coverage report in browser...
  xdg-open ./coverage-html/index.html
else
  echo "warning: xdg-open not installed"
  echo "you have to open the file manually."
fi

echo ================================
echo Coverage analysis complete!
echo ================================

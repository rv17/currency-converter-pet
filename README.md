# Currency Converter

This is a pet project created for purposes of practicing TDD and demonstrating
unit testing best practices.

# Features

Core feature is to convert values from one currency to another. 
Library loads exchange ratios from a local `.json`-file. 
Path to file should be specified via client's configuration.

# Usage

## Core library

Add library services via dependency injection:

    public void ConfigureServices(IServiceCollection services) {
        /* ... */
        services.AddCurrencyConverter();
        /* ... */
    }

and then inject `ICurrencyConverter` as a dependency for your services.

**Note: library requires `IConfiguration` service to be available via DI.**

## Client

There will be an interactive console app which will prompt for source and 
target currencies and source value, and then will return converted value.

# Building

    dotnet build

# Testing

    dotnet test

# Deployment

Use `dotnet publish` to create a distribution.

# Technologies

The app is written in C# 8 using .NEt Core 3.1. 
Tests are using `NUnit v3.15.0` and `NSubstitute v4.2.1` frameworks.
Both library and tests are using `Newtonsoft.Json v.12.0.3` to deal with JSON
data.

# Contribution

**TODO: describe contribution options**
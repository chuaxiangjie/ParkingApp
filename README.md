# ParkingApp

A smart parking app that automatically issue slot reservation for incoming cars. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

#### Windows Environment

1. Install latest .NET Core
(https://dotnet.microsoft.com/download)

2. Visual Studio 2019 [Optional]

```
Build and Run using Visual Studio 2019

-> Clone repository using VS, build and run application

Build and Run using .NET CLI

Navigate to source directory and execute the following commands
-> dotnet build
-> dotnet test
-> dotnet run

```
#### Linux Environment

1. Install latest .NET Core through bash CLI
https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904

Please install .Net Core v2.2, ensure wget command is installed

2. Visual Studio Code [Optional]

```
Build and Run using bash

Navigate to source directory and execute the following commands
-> dotnet build
-> dotnet test
-> dotnet run

```

## Running the tests

Execute dotnet test
![unit_test](https://user-images.githubusercontent.com/5947398/73134025-53839780-406c-11ea-8f93-64b47e393bf6.PNG)
### Break down into end to end tests

Explain what these tests test and why

```
Test focus on each of the allowable commands

1. Create Parking Lot
2. Allocate Parking Lot
3. Leave Parking Slot
4. Parking lot status
5. Search parameters
6. Test Drive application with multiple commands

```

## Built With

* [.NET Core](https://dotnet.microsoft.com/download) - The library used
* [C#] - Programming language used


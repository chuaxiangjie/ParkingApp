# ParkingApp

A smart parking app that automatically issue slot reservation for incoming cars. 

## Application Logic

The Parking System handles two logics (Available and Occupied Slots) represented by two different collection objects
1. Min Heap - Represents the available parking slots
2. Dictionary - <K,V> where K refers to slot no and V as slot object

Parking lot of 6 is represented by a min heap tree where the root contain slot no 1. 
The rest of the child nodes are greater than their parent node.

Operation with Time complexity

### Min Heap (Available Slots)

| Process | Action | Time Complexity |
| :---:       |     :---:      |          :---: |
| Issue of slot ticket to incoming Car   | PopMin()    | O(1)    |
| Release of car slot    | Add()       | O(1)    |

### Dictionary (Occupied Slots)

| Process | Action | Time Complexity |
| :---:       |     :---:      |          :---: |
| Denote slot as occupied with vehicle information   | Add()   | O(1)    |
| Search via slot number    | Search()      | O(1)    |
| Search via colour    | Search()      | O(n)    |
| Search via registration number    | Search()      | O(n)    |

When a slot is assigned to a car, the slot will be removed from the heap and added to the occupied collection (Dictionary).
It provides information on the current slots that are occupied.

When a car leaves the carpark, the slot is removed from the collection and added back to the heap.

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


1. Execute the following bash scripts in sequence
   * apt-get update
   * apt install wget
   * wget -q https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   * dpkg -i packages-microsoft-prod.deb
   * apt-get install apt-transport-https
   * apt-get update
   * apt-get install dotnet-sdk-2.2


Please install .Net Core v2.2, ensure wget command is installed

After installation, execute dotnet --version, verify version states 2.2

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
Test focus on each of the following commands

1. Create Parking Lot
2. Allocate Parking Lot
3. Leave Parking Slot
4. Parking lot status
5. Search parameters
6. Test Drive application with multiple commands

```

## Built With

* [.NET Core](https://dotnet.microsoft.com/download) - Microsoft Technology
* [C#] - Programming language used


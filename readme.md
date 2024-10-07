# SpanLeague Ranking Application

This is a .NET Core command-line application that calculates a ranking table for a league based on game results. It follows the Strategy Pattern for handling variations in ranking rules and utilizes Dependency Injection, logging, and error handling for scalability and maintainability.

## Features

- Reads game results from an input file.
- Calculates team rankings based on game outcomes.
- Allows for future expansion with different ranking strategies via the Strategy Pattern.
- Logs output and errors using the built-in ILogger interface.

---

## Prerequisites

Before running or deploying the application, ensure you have:

- .NET Core SDK (v6 or above)
- A file with game results to be processed.

Example file to process:

```
Lions 3, Snakes 3
Tarantulas 1, FC Awesome 0
Lions 1, FC Awesome 1
Tarantulas 3, Snakes 1
Lions 4, Grouches 0 
```

---

## Installation

Clone the repository or copy the project to your local environment:

```bash
git clone <repository_url>
```

Navigate to the project directory:

```bash
cd SpanLeague
```

---

## Running the Application

### Windows
1. Open a command prompt.
2. Navigate to the project folder.
3. Run the following command:

```cmd
dotnet run <path_to_input_file>
```

For example:

```cmd
dotnet run C:\path\to\inputfile.txt
```

### Linux / macOS

1. Open a terminal.
2. Navigate to the project folder.
3. Run the following command:

```bash
dotnet run <path_to_input_file>
```

For example:

```bash
dotnet run /path/to/inputfile.txt
```

---

## Testing

### Running Unit Test

Ensure that you have a test project set up in the solution. To run the unit tests:

```bash
dotnet test
```

---

## Deploying the Application

### Self-Contained Deployment

1. Publish the application as a self-contained package. This allows running it without needing to have .NET Core installed on the target machine.

#### Windows

```cmd
dotnet publish -c Release -r win-x64 --self-contained
```
#### Linux
```bash
dotnet publish -c Release -r linux-x64 --self-contained
```

#### macOS
```bash
dotnet publish -c Release -r osx-x64 --self-contained
```

2. The output will be available in the /bin/Release/net6.0/<runtime>/publish folder.

3. You can run the executable directly:


For Windows:
```bash
./publish/SpanLeague.exe <path_to_input_file>
```

For Linux/macOS:
```bash
./publish/SpanLeague <path_to_input_file>
```

---

## Contact

For any queries or support, feel free to reach out at david.mesm@gmail.com.


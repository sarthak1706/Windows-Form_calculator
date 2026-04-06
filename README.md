# Calculator Windows Forms Application

A simple calculator application built with Windows Forms in C#.

## Features

- Basic arithmetic operations: addition, subtraction, multiplication, division
- Decimal point support
- Clear function
- Error handling for division by zero

## Usage

Run the application: `dotnet run --project Calculator`

- Use the number buttons (0-9) to input numbers
- Use the operator buttons (+, -, *, /) to select operations
- Press `=` to calculate the result
- Press `C` to clear the display and reset

## Building

To build the project:

```
dotnet build Calculator
```

## Running

To run the application:

```
dotnet run --project Calculator
```

## Troubleshooting

- **Application doesn't start:** Ensure you have .NET 8.0 SDK installed.
- **Division by zero error:** The calculator displays `Error` when attempting to divide by zero.
- **Build warnings:** The code has nullability warnings which are harmless and don't affect functionality.

## Requirements

- .NET 8.0 SDK
- Windows OS (for Windows Forms)

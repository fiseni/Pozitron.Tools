# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Build
- Build the entire solution: `dotnet build`
- Build a specific project: `dotnet build <path_to_csproj>`

### Test
- Run all tests: `dotnet test`
- Run tests for a specific project: `dotnet test <path_to_csproj>`

### Clean
- Delete `bin` and `obj` directories: `./clean.sh obj`
- Delete all build artifacts (bin, obj, .vs, logs, etc.): `./clean.sh all`

## Architecture

This repository contains a collection of .NET libraries and their corresponding test projects.

### Project Structure
- `src/`: Contains the library source code.
  - `Pozitron.SharedKernel/`: Core utilities and common classes.
  - `Pozitron.Extensions.EntityFrameworkCore/`: Extensions for Entity Framework Core.
- `tests/`: Contains the test projects.
  - `Pozitron.SharedKernel.Tests/`: Unit tests for SharedKernel.
  - `Pozitron.Extensions.EntityFrameworkCore.Tests/`: Unit tests for Extensions.EFCore.

### Key Components
- **Pozitron.SharedKernel**: Provides foundational elements used across other projects.
- **Pozitron.Extensions.EntityFrameworkCore**: Focuses on enhancing EF Core capabilities through specialized extensions (e.g., `DbContextExtensions`, `IQueryableExtensions`, `ModelBuilderExtensions`).

## Tooling
- Uses `.slnx` solution files for project management.
- Uses `Directory.Packages.props` for centralized dependency management.
- `clean.sh` is a utility script for cleaning up build artifacts and managing the development environment.

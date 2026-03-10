# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

## Repository Structure

Examples are organized by feature category:
- `Conversion/` - 230 example(s)
- `Document/` - 72 example(s)
- `JavaScript/` - 9 example(s)
- `Operators/` - 7 example(s)
- `Pages/` - 45 example(s)
- `Stamping/` - 17 example(s)
- `accessibility-and-tagged-pdfs/` - 70 example(s)
- `basic-operations/` - 78 example(s)
- `compare-pdf/` - 30 example(s)
- `facades-acroforms/` - 35 example(s)
- `facades-texts-and-images/` - 22 example(s)
- `graphs-zugferd-operators/` - 9 example(s)
- `parse-pdf/` - 76 example(s)
- `securing-and-signing-pdf/` - 25 example(s)
- `working-with-annotations/` - 252 example(s)
- `working-with-attachments/` - 95 example(s)
- `working-with-forms/` - 74 example(s)
- `working-with-graphs/` - 33 example(s)
- `working-with-images/` - 27 example(s)
- `working-with-tables/` - 118 example(s)
- `working-with-text/` - 62 example(s)
- `working-with-xml/` - 16 example(s)

Each category contains standalone `.cs` files that can be compiled and run independently.

## Getting Started

### Prerequisites
- .NET SDK (net10.0 or compatible version)
- Aspose.PDF for .NET NuGet package
- Valid Aspose license (for production use)

### Running Examples

Each example is a self-contained C# file. To run an example:

```bash
cd <CategoryFolder>
dotnet new console -o ExampleProject
cd ExampleProject
dotnet add package Aspose.PDF
# Copy the example .cs file as Program.cs
dotnet run
```

## Code Patterns

### Loading a PDF
```csharp
using (Document pdfDoc = new Document("input.pdf"))
{
    // Work with document
}
```

### Error Handling
```csharp
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Error: File not found – {inputPath}");
    return;
}

try
{
    // Operations
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
}
```

### Important Notes
- **One-based indexing**: Aspose.PDF uses 1-based page indexing (`Pages[1]` = first page)
- **Deterministic cleanup**: All IDisposable objects wrapped in `using` blocks
- **Console output**: Success/error messages written to Console.WriteLine/Console.Error

## Contributing

Examples in this repository are **automatically generated**. To suggest new examples:
1. Submit tasks to the Aspose.PDF Examples Generator
2. Generated examples are validated via compilation
3. Passing examples are included in monthly batches

## Related Resources

- [Aspose.PDF for .NET Documentation](https://docs.aspose.com/pdf/net/)
- [API Reference](https://reference.aspose.com/pdf/net/)
- [Aspose Forum](https://forum.aspose.com/c/pdf/10)
- [AI Agent Guide](./agents.md) - For AI agents and code generation tools

## License

All examples use Aspose.PDF for .NET and require a valid license for production use. See [licensing](https://purchase.aspose.com/pdf/net).

---

*This repository is maintained by automated code generation. For AI-friendly guidance, see [agents.md](./agents.md).*

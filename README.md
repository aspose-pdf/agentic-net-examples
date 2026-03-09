# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

## Repository Structure

Examples are organized by feature category:
- `Accessibility and Tagged PDFs/` - 70 example(s)
- `Basic Operations/` - 78 example(s)
- `Compare PDF/` - 30 example(s)
- `Conversion/` - 230 example(s)
- `Document/` - 72 example(s)
- `Facades - AcroForms/` - 35 example(s)
- `Working with Annotations/` - 252 example(s)
- `Working with Attachments/` - 95 example(s)

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

# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

| Metric | Value |
|--------|-------|
| Total examples | 2617 |
| Categories | 31 |
| Target framework | net10.0 |
| Aspose.PDF version | 26.3.0 |
| Last updated | 2026-04-07 |

## Repository Structure

Examples are organized by feature category:
- `accessibility-and-tagged-pdfs/` - 45 example(s)
- `basic-operations/` - 56 example(s)
- `compare-pdf/` - 27 example(s)
- `conversion/` - 100 example(s)
- `document/` - 111 example(s)
- `facades-acroforms/` - 40 example(s)
- `facades-annotations/` - 105 example(s)
- `facades-bookmarks/` - 34 example(s)
- `facades-convert-documents/` - 40 example(s)
- `facades-documents/` - 100 example(s)
- `facades-edit-document/` - 208 example(s)
- `facades-extract-images-and-text/` - 81 example(s)
- `facades-fill-forms/` - 32 example(s)
- `facades-forms/` - 83 example(s)
- `facades-metadata/` - 40 example(s)
- `facades-pages/` - 116 example(s)
- `facades-secure-documents/` - 40 example(s)
- `facades-sign-documents/` - 32 example(s)
- `facades-stamps/` - 46 example(s)
- `facades-texts-and-images/` - 29 example(s)
- `facades-xmp-metadata/` - 43 example(s)
- `pages/` - 85 example(s)
- `stamping/` - 49 example(s)
- `working-with-annotations/` - 163 example(s)
- `working-with-attachments/` - 50 example(s)
- `working-with-forms/` - 450 example(s)
- `working-with-graphs/` - 85 example(s)
- `working-with-images/` - 72 example(s)
- `working-with-tables/` - 105 example(s)
- `working-with-text/` - 76 example(s)
- `working-with-xml/` - 74 example(s)

Each category contains standalone `.cs` files that can be compiled and run independently.

## Getting Started

### Prerequisites
- .NET SDK (net10.0 or compatible version)
- Aspose.PDF for .NET NuGet package (26.3.0)
- Valid Aspose license (for production use)

### Running Examples

Each example is a self-contained C# file. To run an example:

```bash
cd <CategoryFolder>
dotnet new console -o ExampleProject
cd ExampleProject
dotnet add package Aspose.PDF --version 26.3.0
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
    Console.Error.WriteLine($"Error: File not found - {inputPath}");
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
- **Fully qualified types**: Use `Aspose.Pdf.Drawing.Path` (not bare `Path`) to avoid ambiguity with `System.IO.Path`

## Documentation

- Each category folder contains an [`agents.md`](./agents.md) with category-specific guidance
- Each category folder contains an `index.json` with per-example metadata
- Root [`agents.md`](./agents.md) provides cumulative guidance across all categories
- Root [`index.json`](./index.json) provides a machine-readable manifest of all examples

## Contributing

Examples in this repository are **automatically generated**. To suggest new examples:
1. Submit tasks to the Aspose.PDF Examples Generator
2. Generated examples are validated via compilation
3. Passing examples are included in version release batches

## Related Resources

- [Aspose.PDF for .NET Documentation](https://docs.aspose.com/pdf/net/)
- [API Reference](https://reference.aspose.com/pdf/net/)
- [NuGet Package](https://www.nuget.org/packages/Aspose.PDF)
- [Aspose Forum](https://forum.aspose.com/c/pdf/10)
- [AI Agent Guide](./agents.md) - For AI agents and code generation tools

## License

All examples use Aspose.PDF for .NET and require a valid license for production use. See [licensing](https://purchase.aspose.com/).

---

*This repository is maintained by automated code generation. For AI-friendly guidance, see [agents.md](./agents.md). Last updated: 2026-04-07*

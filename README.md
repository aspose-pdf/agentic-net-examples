# Aspose.PDF for .NET — Agentic Examples

Agentic, build-validated C# code examples for **Aspose.PDF for .NET** covering PDF creation, conversion, editing, annotations, forms, digital signatures, and text extraction. Every example compiles and runs successfully. Includes `AGENTS.md` guides optimized for AI coding agents.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

| Metric | Value |
|--------|-------|
| Total examples | 2684 |
| Categories | 34 |
| Target framework | net10.0 |
| Aspose.PDF version | 26.6.0 |
| Last updated | 2026-07-06 |

## For AI Coding Agents

This repository is structured for direct use by AI coding agents and LLM-powered tools:

- **[`AGENTS.md`](./AGENTS.md)** — root-level guide covering API surface, anti-patterns, and category tips
- **Per-category `AGENTS.md`** — targeted guidance inside each category folder
- **[`index.json`](./index.json)** — machine-readable manifest of all 2684 examples with metadata
- **MCP-compatible** — integrate with any MCP client (Claude Desktop, Cursor, Continue.dev)

## Categories

| Category | Examples | Agent Guide |
|----------|----------|-------------|
| `accessibility-and-tagged-pdfs` | 45 | [AGENTS.md](./accessibility-and-tagged-pdfs/AGENTS.md) |
| `basic-operations` | 57 | [AGENTS.md](./basic-operations/AGENTS.md) |
| `compare-pdf` | 28 | [AGENTS.md](./compare-pdf/AGENTS.md) |
| `conversion` | 102 | [AGENTS.md](./conversion/AGENTS.md) |
| `document` | 122 | [AGENTS.md](./document/AGENTS.md) |
| `facades-acroforms` | 41 | [AGENTS.md](./facades-acroforms/AGENTS.md) |
| `facades-annotations` | 107 | [AGENTS.md](./facades-annotations/AGENTS.md) |
| `facades-bookmarks` | 35 | [AGENTS.md](./facades-bookmarks/AGENTS.md) |
| `facades-convert-documents` | 40 | [AGENTS.md](./facades-convert-documents/AGENTS.md) |
| `facades-documents` | 101 | [AGENTS.md](./facades-documents/AGENTS.md) |
| `facades-edit-document` | 213 | [AGENTS.md](./facades-edit-document/AGENTS.md) |
| `facades-extract-images-and-text` | 84 | [AGENTS.md](./facades-extract-images-and-text/AGENTS.md) |
| `facades-fill-forms` | 35 | [AGENTS.md](./facades-fill-forms/AGENTS.md) |
| `facades-forms` | 88 | [AGENTS.md](./facades-forms/AGENTS.md) |
| `facades-metadata` | 40 | [AGENTS.md](./facades-metadata/AGENTS.md) |
| `facades-pages` | 117 | [AGENTS.md](./facades-pages/AGENTS.md) |
| `facades-secure-documents` | 39 | [AGENTS.md](./facades-secure-documents/AGENTS.md) |
| `facades-sign-documents` | 35 | [AGENTS.md](./facades-sign-documents/AGENTS.md) |
| `facades-stamps` | 47 | [AGENTS.md](./facades-stamps/AGENTS.md) |
| `facades-texts-and-images` | 29 | [AGENTS.md](./facades-texts-and-images/AGENTS.md) |
| `facades-xmp-metadata` | 45 | [AGENTS.md](./facades-xmp-metadata/AGENTS.md) |
| `graphs-zugferd-operators` | 89 | [AGENTS.md](./graphs-zugferd-operators/AGENTS.md) |
| `pages` | 99 | [AGENTS.md](./pages/AGENTS.md) |
| `parse-pdf` | 64 | [AGENTS.md](./parse-pdf/AGENTS.md) |
| `securing-and-signing-pdf` | 84 | [AGENTS.md](./securing-and-signing-pdf/AGENTS.md) |
| `stamping` | 50 | [AGENTS.md](./stamping/AGENTS.md) |
| `working-with-annotations` | 162 | [AGENTS.md](./working-with-annotations/AGENTS.md) |
| `working-with-attachments` | 49 | [AGENTS.md](./working-with-attachments/AGENTS.md) |
| `working-with-forms` | 239 | [AGENTS.md](./working-with-forms/AGENTS.md) |
| `working-with-graphs` | 79 | [AGENTS.md](./working-with-graphs/AGENTS.md) |
| `working-with-images` | 72 | [AGENTS.md](./working-with-images/AGENTS.md) |
| `working-with-tables` | 98 | [AGENTS.md](./working-with-tables/AGENTS.md) |
| `working-with-text` | 76 | [AGENTS.md](./working-with-text/AGENTS.md) |
| `working-with-xml` | 73 | [AGENTS.md](./working-with-xml/AGENTS.md) |

Each category contains standalone `.cs` files that can be compiled and run independently.

## Getting Started

### Prerequisites
- .NET SDK (net10.0 or compatible version)
- Aspose.PDF for .NET NuGet package (26.6.0)
- Valid Aspose license (for production use)

### Running Examples

Each example is a self-contained C# file. To run an example:

```bash
cd <CategoryFolder>
dotnet new console -o ExampleProject
cd ExampleProject
dotnet add package Aspose.PDF --version 26.6.0
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

## Agentic .NET Ecosystem

Other Aspose products with agentic, build-validated example repositories:

| Product | Repository | Focus |
|---------|-----------|-------|
| Aspose.Words for .NET | [aspose-words/agentic-net-examples](https://github.com/aspose-words/agentic-net-examples) | Word processing, DOCX, mail merge |
| Aspose.Cells for .NET | [aspose-cells/agentic-net-examples](https://github.com/aspose-cells/agentic-net-examples) | Spreadsheets, Excel, charts |
| Aspose.HTML for .NET | [aspose-html/agentic-net-examples](https://github.com/aspose-html/agentic-net-examples) | HTML conversion, DOM editing |
| Aspose.Imaging for .NET | [aspose-imaging/agentic-net-examples](https://github.com/aspose-imaging/agentic-net-examples) | Image conversion, manipulation |
| Aspose.Slides for .NET | [aspose-slides/agentic-net-examples](https://github.com/aspose-slides/agentic-net-examples) | Presentations, PowerPoint |
| Aspose.Email for .NET | [aspose-email/agentic-net-examples](https://github.com/aspose-email/agentic-net-examples) | Email, calendars, messaging |
| Aspose.BarCode for .NET | [aspose-barcode/agentic-net-examples](https://github.com/aspose-barcode/agentic-net-examples) | Barcode generation and recognition |

## Related Resources

### Official Documentation
- [Aspose.PDF for .NET Documentation](https://docs.aspose.com/pdf/net/) — Guides, tutorials, and feature overviews
- [API Reference](https://reference.aspose.com/pdf/net/) — Complete class/method reference
- [Release Notes](https://releases.aspose.com/pdf/net/release-notes/) — Version history and changelogs

### Downloads & Packages
- [NuGet Package](https://www.nuget.org/packages/Aspose.PDF) — Install via `dotnet add package Aspose.PDF`
- [Direct Downloads](https://releases.aspose.com/pdf/net/) — MSI/ZIP installers and DLLs

### Community & Support
- [Aspose.PDF Forum](https://forum.aspose.com/c/pdf/10) — Community Q&A and official support
- [Aspose Blog - PDF](https://blog.aspose.com/category/pdf/) — Tutorials, tips, and product updates
- [GitHub Issues](https://github.com/aspose-pdf/agentic-net-examples/issues) — Bug reports and feature requests

### Licensing & Purchase
- [Purchase](https://purchase.aspose.com/buy) — Commercial license options
- [Temporary License](https://purchase.aspose.com/temporary-license/) — Full-feature evaluation license

## License

All examples use [Aspose.PDF for .NET](https://products.aspose.com/pdf/net/) and require a valid license for production use. See [licensing options](https://purchase.aspose.com/buy).

---

*Maintained by an [agentic example generation workflow](https://metrics.aspose.com/agents/sections/examples) | For AI-friendly guidance, see [AGENTS.md](./AGENTS.md) | Last updated: 2026-07-06*

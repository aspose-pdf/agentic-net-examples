# Aspose.PDF for .NET — Agentic Examples

Agentic, build-validated C# code examples for **Aspose.PDF for .NET** covering PDF creation, conversion, editing, annotations, forms, digital signatures, and text extraction. Every example compiles and runs successfully. Includes `agents.md` guides optimized for AI coding agents.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

| Metric | Value |
|--------|-------|
| Total examples | 2631 |
| Categories | 34 |
| Target framework | net10.0 |
| Aspose.PDF version | 26.4.0 |
| Last updated | 2026-05-08 |

## For AI Coding Agents

This repository is structured for direct use by AI coding agents and LLM-powered tools:

- **[`agents.md`](./agents.md)** — root-level guide covering API surface, anti-patterns, and category tips
- **Per-category `agents.md`** — targeted guidance inside each category folder
- **[`index.json`](./index.json)** — machine-readable manifest of all 2,631 examples with metadata
- **MCP-compatible** — integrate with any MCP client (Claude Desktop, Cursor, Continue.dev) via the [Aspose PDF Examples Generator](https://github.com/aspose-pdf/agentic-net-examples)

## Categories

| Category | Examples | Agent Guide |
|----------|----------|-------------|
| `accessibility-and-tagged-pdfs` | 45 | [agents.md](./accessibility-and-tagged-pdfs/agents.md) |
| `basic-operations` | 56 | [agents.md](./basic-operations/agents.md) |
| `compare-pdf` | 27 | [agents.md](./compare-pdf/agents.md) |
| `conversion` | 102 | [agents.md](./conversion/agents.md) |
| `document` | 118 | [agents.md](./document/agents.md) |
| `facades-acroforms` | 40 | [agents.md](./facades-acroforms/agents.md) |
| `facades-annotations` | 106 | [agents.md](./facades-annotations/agents.md) |
| `facades-bookmarks` | 35 | [agents.md](./facades-bookmarks/agents.md) |
| `facades-convert-documents` | 40 | [agents.md](./facades-convert-documents/agents.md) |
| `facades-documents` | 101 | [agents.md](./facades-documents/agents.md) |
| `facades-edit-document` | 209 | [agents.md](./facades-edit-document/agents.md) |
| `facades-extract-images-and-text` | 82 | [agents.md](./facades-extract-images-and-text/agents.md) |
| `facades-fill-forms` | 28 | [agents.md](./facades-fill-forms/agents.md) |
| `facades-forms` | 90 | [agents.md](./facades-forms/agents.md) |
| `facades-metadata` | 40 | [agents.md](./facades-metadata/agents.md) |
| `facades-pages` | 117 | [agents.md](./facades-pages/agents.md) |
| `facades-secure-documents` | 40 | [agents.md](./facades-secure-documents/agents.md) |
| `facades-sign-documents` | 34 | [agents.md](./facades-sign-documents/agents.md) |
| `facades-stamps` | 45 | [agents.md](./facades-stamps/agents.md) |
| `facades-texts-and-images` | 27 | [agents.md](./facades-texts-and-images/agents.md) |
| `facades-xmp-metadata` | 43 | [agents.md](./facades-xmp-metadata/agents.md) |
| `graphs-zugferd-operators` | 83 | [agents.md](./graphs-zugferd-operators/agents.md) |
| `pages` | 99 | [agents.md](./pages/agents.md) |
| `parse-pdf` | 65 | [agents.md](./parse-pdf/agents.md) |
| `securing-and-signing-pdf` | 83 | [agents.md](./securing-and-signing-pdf/agents.md) |
| `stamping` | 50 | [agents.md](./stamping/agents.md) |
| `working-with-annotations` | 160 | [agents.md](./working-with-annotations/agents.md) |
| `working-with-attachments` | 50 | [agents.md](./working-with-attachments/agents.md) |
| `working-with-forms` | 239 | [agents.md](./working-with-forms/agents.md) |
| `working-with-graphs` | 70 | [agents.md](./working-with-graphs/agents.md) |
| `working-with-images` | 70 | [agents.md](./working-with-images/agents.md) |
| `working-with-tables` | 91 | [agents.md](./working-with-tables/agents.md) |
| `working-with-text` | 72 | [agents.md](./working-with-text/agents.md) |
| `working-with-xml` | 74 | [agents.md](./working-with-xml/agents.md) |

## Getting Started

### Prerequisites
- .NET SDK (net10.0 or compatible version)
- Aspose.PDF for .NET NuGet package (26.4.0)
- Valid Aspose license (for production use)

### Running Examples

Each example is a self-contained C# file. To run an example:

```bash
cd <CategoryFolder>
dotnet new console -o ExampleProject
cd ExampleProject
dotnet add package Aspose.PDF --version 26.4.0
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

*This repository is maintained by automated code generation. For AI-friendly guidance, see [agents.md](./agents.md). Last updated: 2026-05-08*

# Aspose.PDF for .NET — Agentic Examples

<script type="application/ld+json">
{
  "@context": "https://schema.org",
  "@graph": [
    {
      "@type": "SoftwareSourceCode",
      "name": "Aspose.PDF for .NET -- Agentic Examples",
      "description": "AI-generated, compile-validated C# code examples for Aspose.PDF for .NET 26.7.0. 2,636 verified examples across 35 categories.",
      "programmingLanguage": "C#",
      "runtimePlatform": ".NET",
      "targetProduct": {
        "@type": "SoftwareApplication",
        "name": "Aspose.PDF for .NET",
        "operatingSystem": "Cross-platform",
        "applicationCategory": "DeveloperApplication",
        "softwareVersion": "26.7.0"
      },
      "codeRepository": "https://github.com/aspose-pdf/agentic-net-examples.git",
      "license": "https://opensource.org/licenses/MIT",
      "keywords": [
        "PDF",
        "C#",
        ".NET",
        "Aspose.PDF",
        "PDF conversion",
        "PDF forms",
        "digital signatures",
        "text extraction",
        "MCP server",
        "agentic AI",
        "code examples",
        "net10.0",
        "Aspose.PDF 26.7.0"
      ],
      "dateModified": "2026-07-21",
      "author": {
        "@type": "Organization",
        "name": "Aspose Pty Ltd",
        "url": "https://www.aspose.com"
      }
    },
    {
      "@type": "FAQPage",
      "mainEntity": [
        {
          "@type": "Question",
          "name": "How do I create a new PDF from scratch in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Instantiate `Aspose.Pdf.Document`, add pages via `doc.Pages.Add()`, then `doc.Save(\"output.pdf\")`. See the `basic-operations` and `document` category folders for working examples."
          }
        },
        {
          "@type": "Question",
          "name": "How do I convert a PDF to DOCX, HTML, or an image in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Load the source with `new Document(path)`, then `doc.Save(target, SaveFormat.DocX)` (or `Html` / `Xps` / `Tiff` / etc.). Per-format examples live in the `conversion` and `facades-convert-documents` folders."
          }
        },
        {
          "@type": "Question",
          "name": "How do I extract text from a PDF in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Use `TextAbsorber` (page-level) or `TextFragmentAbsorber` (search-based). Examples of both patterns live in `working-with-text` and `facades-extract-images-and-text`."
          }
        },
        {
          "@type": "Question",
          "name": "How do I add form fields (text boxes, checkboxes, radio buttons) to a PDF?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Use `TextBoxField`, `CheckboxField` (lowercase 'b'!), `RadioButtonField`, `ButtonField` -- all in the `Aspose.Pdf.Forms` namespace. Attach with `doc.Form.Add(field, pageNum)`. See `working-with-forms` and `facades-forms` for complete examples."
          }
        },
        {
          "@type": "Question",
          "name": "How do I digitally sign a PDF in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Use `PdfFileSignature` (facades) or `SignatureField` + `PkcsSignature` (core API). Load a PFX certificate with `new PdfFileSignature(document)`, then `Sign(...)`. See `securing-and-signing-pdf` and `facades-sign-documents` for working end-to-end examples."
          }
        },
        {
          "@type": "Question",
          "name": "How do I encrypt or decrypt a PDF in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Call `doc.Encrypt(userPass, ownerPass, DocumentPrivilege.ForbidAll, CryptoAlgorithm.AESx256, false)` before Save, or `new Document(path, password)` to open an encrypted PDF. Examples in `securing-and-signing-pdf` and `facades-secure-documents`."
          }
        },
        {
          "@type": "Question",
          "name": "How do I add bookmarks or an outline / TOC to a PDF?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Create `OutlineItemCollection` (the type doubles as both node and child container -- there is no separate `OutlineItem` singular type in Aspose.PDF), set Title / Action / Open, then `doc.Outlines.Add(item)` or `parent.Add(child)`. Examples in `facades-bookmarks`."
          }
        },
        {
          "@type": "Question",
          "name": "How do I add annotations (highlights, comments, stamps) to a PDF?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Annotation types live in `Aspose.Pdf.Annotations` (`HighlightAnnotation`, `TextAnnotation`, `StampAnnotation`, `LinkAnnotation`, `SquareAnnotation`, etc.). Attach via `page.Annotations.Add(annotation)`. See `working-with-annotations` and `facades-annotations`."
          }
        },
        {
          "@type": "Question",
          "name": "How do I merge or split PDFs in Aspose.PDF for .NET?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Merge with `PdfFileEditor.Concatenate(...)`. Split by page range with `PdfFileEditor.SplitFromFirst / SplitToEnd / SplitFromFile`. See `facades-edit-document` for both patterns."
          }
        },
        {
          "@type": "Question",
          "name": "How do I convert a PDF to PDF/A for archival compliance?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Load the document, then `doc.Convert(logStream, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete)` (or PDF_A_2B / PDF_A_3B). Save and the resulting file is PDF/A-compliant. See `conversion` for full samples."
          }
        },
        {
          "@type": "Question",
          "name": "Can these examples be used by AI coding agents like Claude, Copilot, or Cursor?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "Yes -- the repository ships an `AGENTS.md` at root and one per category, plus `.well-known/agent.json`, `llm.txt`, `mcp.json`, and a machine-readable `index.json`. Point your agent at the repo's raw URLs (or install the MCP server) and it can browse examples programmatically."
          }
        },
        {
          "@type": "Question",
          "name": "Do I need an Aspose.PDF license to run these examples?",
          "acceptedAnswer": {
            "@type": "Answer",
            "text": "The library runs in evaluation mode without a license (with page-count and collection-size limits). For production use, apply a license via `new License().SetLicense(path)`. Purchase / trial options: https://purchase.aspose.com/buy."
          }
        }
      ]
    }
  ]
}
</script>


Agentic, build-validated C# code examples for **Aspose.PDF for .NET** covering PDF creation, conversion, editing, annotations, forms, digital signatures, and text extraction. Every example compiles and runs successfully. Includes `AGENTS.md` guides optimized for AI coding agents.

## Overview

This repository provides working code examples demonstrating Aspose.PDF for .NET capabilities. All examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

| Metric | Value |
|--------|-------|
| Total examples | 2636 |
| Categories | 35 |
| Target framework | net10.0 |
| Aspose.PDF version | 26.7.0 |
| Last updated | 2026-07-21 |

## For AI Coding Agents

This repository is structured for direct use by AI coding agents and LLM-powered tools:

- **[`AGENTS.md`](./AGENTS.md)** — root-level guide covering API surface, anti-patterns, and category tips
- **Per-category `AGENTS.md`** — targeted guidance inside each category folder
- **[`index.json`](./index.json)** — machine-readable manifest of all 2636 examples with metadata
- **MCP-compatible** — integrate with any MCP client (Claude Desktop, Cursor, Continue.dev)

## Categories

| Category | Examples | Agent Guide |
|----------|----------|-------------|
| `accessibility-and-tagged-pdfs` | 45 | [AGENTS.md](./accessibility-and-tagged-pdfs/AGENTS.md) |
| `basic-operations` | 57 | [AGENTS.md](./basic-operations/AGENTS.md) |
| `compare-pdf` | 29 | [AGENTS.md](./compare-pdf/AGENTS.md) |
| `conversion` | 102 | [AGENTS.md](./conversion/AGENTS.md) |
| `document` | 117 | [AGENTS.md](./document/AGENTS.md) |
| `facades-acroforms` | 41 | [AGENTS.md](./facades-acroforms/AGENTS.md) |
| `facades-annotations` | 106 | [AGENTS.md](./facades-annotations/AGENTS.md) |
| `facades-bookmarks` | 35 | [AGENTS.md](./facades-bookmarks/AGENTS.md) |
| `facades-convert-documents` | 36 | [AGENTS.md](./facades-convert-documents/AGENTS.md) |
| `facades-documents` | 101 | [AGENTS.md](./facades-documents/AGENTS.md) |
| `facades-edit-document` | 213 | [AGENTS.md](./facades-edit-document/AGENTS.md) |
| `facades-extract-images-and-text` | 83 | [AGENTS.md](./facades-extract-images-and-text/AGENTS.md) |
| `facades-fill-forms` | 35 | [AGENTS.md](./facades-fill-forms/AGENTS.md) |
| `facades-forms` | 85 | [AGENTS.md](./facades-forms/AGENTS.md) |
| `facades-metadata` | 40 | [AGENTS.md](./facades-metadata/AGENTS.md) |
| `facades-pages` | 117 | [AGENTS.md](./facades-pages/AGENTS.md) |
| `facades-secure-documents` | 38 | [AGENTS.md](./facades-secure-documents/AGENTS.md) |
| `facades-sign-documents` | 35 | [AGENTS.md](./facades-sign-documents/AGENTS.md) |
| `facades-stamps` | 48 | [AGENTS.md](./facades-stamps/AGENTS.md) |
| `facades-texts-and-images` | 28 | [AGENTS.md](./facades-texts-and-images/AGENTS.md) |
| `facades-xmp-metadata` | 44 | [AGENTS.md](./facades-xmp-metadata/AGENTS.md) |
| `graphs-zugferd-operators` | 82 | [AGENTS.md](./graphs-zugferd-operators/AGENTS.md) |
| `pages` | 100 | [AGENTS.md](./pages/AGENTS.md) |
| `parse-pdf` | 63 | [AGENTS.md](./parse-pdf/AGENTS.md) |
| `securing-and-signing-pdf` | 78 | [AGENTS.md](./securing-and-signing-pdf/AGENTS.md) |
| `stamping` | 50 | [AGENTS.md](./stamping/AGENTS.md) |
| `uncategorized` | 1 | [AGENTS.md](./uncategorized/AGENTS.md) |
| `working-with-annotations` | 156 | [AGENTS.md](./working-with-annotations/AGENTS.md) |
| `working-with-attachments` | 50 | [AGENTS.md](./working-with-attachments/AGENTS.md) |
| `working-with-forms` | 230 | [AGENTS.md](./working-with-forms/AGENTS.md) |
| `working-with-graphs` | 77 | [AGENTS.md](./working-with-graphs/AGENTS.md) |
| `working-with-images` | 72 | [AGENTS.md](./working-with-images/AGENTS.md) |
| `working-with-tables` | 96 | [AGENTS.md](./working-with-tables/AGENTS.md) |
| `working-with-text` | 73 | [AGENTS.md](./working-with-text/AGENTS.md) |
| `working-with-xml` | 73 | [AGENTS.md](./working-with-xml/AGENTS.md) |

Each category contains standalone `.cs` files that can be compiled and run independently.

## Frequently Asked Questions

### How do I create a new PDF from scratch in Aspose.PDF for .NET?

Instantiate `Aspose.Pdf.Document`, add pages via `doc.Pages.Add()`, then `doc.Save("output.pdf")`. See the `basic-operations` and `document` category folders for working examples.

### How do I convert a PDF to DOCX, HTML, or an image in Aspose.PDF for .NET?

Load the source with `new Document(path)`, then `doc.Save(target, SaveFormat.DocX)` (or `Html` / `Xps` / `Tiff` / etc.). Per-format examples live in the `conversion` and `facades-convert-documents` folders.

### How do I extract text from a PDF in Aspose.PDF for .NET?

Use `TextAbsorber` (page-level) or `TextFragmentAbsorber` (search-based). Examples of both patterns live in `working-with-text` and `facades-extract-images-and-text`.

### How do I add form fields (text boxes, checkboxes, radio buttons) to a PDF?

Use `TextBoxField`, `CheckboxField` (lowercase 'b'!), `RadioButtonField`, `ButtonField` -- all in the `Aspose.Pdf.Forms` namespace. Attach with `doc.Form.Add(field, pageNum)`. See `working-with-forms` and `facades-forms` for complete examples.

### How do I digitally sign a PDF in Aspose.PDF for .NET?

Use `PdfFileSignature` (facades) or `SignatureField` + `PkcsSignature` (core API). Load a PFX certificate with `new PdfFileSignature(document)`, then `Sign(...)`. See `securing-and-signing-pdf` and `facades-sign-documents` for working end-to-end examples.

### How do I encrypt or decrypt a PDF in Aspose.PDF for .NET?

Call `doc.Encrypt(userPass, ownerPass, DocumentPrivilege.ForbidAll, CryptoAlgorithm.AESx256, false)` before Save, or `new Document(path, password)` to open an encrypted PDF. Examples in `securing-and-signing-pdf` and `facades-secure-documents`.

### How do I add bookmarks or an outline / TOC to a PDF?

Create `OutlineItemCollection` (the type doubles as both node and child container -- there is no separate `OutlineItem` singular type in Aspose.PDF), set Title / Action / Open, then `doc.Outlines.Add(item)` or `parent.Add(child)`. Examples in `facades-bookmarks`.

### How do I add annotations (highlights, comments, stamps) to a PDF?

Annotation types live in `Aspose.Pdf.Annotations` (`HighlightAnnotation`, `TextAnnotation`, `StampAnnotation`, `LinkAnnotation`, `SquareAnnotation`, etc.). Attach via `page.Annotations.Add(annotation)`. See `working-with-annotations` and `facades-annotations`.

### How do I merge or split PDFs in Aspose.PDF for .NET?

Merge with `PdfFileEditor.Concatenate(...)`. Split by page range with `PdfFileEditor.SplitFromFirst / SplitToEnd / SplitFromFile`. See `facades-edit-document` for both patterns.

### How do I convert a PDF to PDF/A for archival compliance?

Load the document, then `doc.Convert(logStream, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete)` (or PDF_A_2B / PDF_A_3B). Save and the resulting file is PDF/A-compliant. See `conversion` for full samples.

### Can these examples be used by AI coding agents like Claude, Copilot, or Cursor?

Yes -- the repository ships an `AGENTS.md` at root and one per category, plus `.well-known/agent.json`, `llm.txt`, `mcp.json`, and a machine-readable `index.json`. Point your agent at the repo's raw URLs (or install the MCP server) and it can browse examples programmatically.

### Do I need an Aspose.PDF license to run these examples?

The library runs in evaluation mode without a license (with page-count and collection-size limits). For production use, apply a license via `new License().SetLicense(path)`. Purchase / trial options: https://purchase.aspose.com/buy.

## Getting Started

### Prerequisites
- .NET SDK (net10.0 or compatible version)
- Aspose.PDF for .NET NuGet package (26.7.0)
- Valid Aspose license (for production use)

### Running Examples

Each example is a self-contained C# file. To run an example:

```bash
cd <CategoryFolder>
dotnet new console -o ExampleProject
cd ExampleProject
dotnet add package Aspose.PDF --version 26.7.0
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

*Maintained by an [agentic example generation workflow](https://metrics.aspose.com/agents/sections/examples) | For AI-friendly guidance, see [AGENTS.md](./AGENTS.md) | Last updated: 2026-07-21*

---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **basic-operations** category.
This folder contains standalone C# examples for basic-operations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **basic-operations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (78/78 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/78 files)
- `using Aspose.Pdf.Facades;` (3/78 files)
- `using Aspose.Pdf.Annotations;` (1/78 files)
- `using System;` (78/78 files)
- `using System.IO;` (78/78 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [Basic-operations-with-PDF-documents-using-HTML-format](./Basic-operations-with-PDF-documents-using-HTML-format.cs) |  | Basic operations with PDF documents using HTML format |
| [Basic-operations-with-PDF-documents-using-PDF-format](./Basic-operations-with-PDF-documents-using-PDF-format.cs) | `TextAbsorber`, `TextAnnotation` | Basic operations with PDF documents using PDF format |
| [Convert-PDF-document-from-HTML-to-PDF-format](./Convert-PDF-document-from-HTML-to-PDF-format.cs) | `HtmlLoadOptions` | Convert PDF document from HTML to PDF format |
| [Convert-PDF-document-from-HTML-to-SVG-format](./Convert-PDF-document-from-HTML-to-SVG-format.cs) | `HtmlLoadOptions`, `SvgSaveOptions` | Convert PDF document from HTML to SVG format |
| [Convert-PDF-document-from-PDF-to-PDF-format](./Convert-PDF-document-from-PDF-to-PDF-format.cs) |  | Convert PDF document from PDF to PDF format |
| [Convert-PDF-document-from-PDF-to-SVG-format](./Convert-PDF-document-from-PDF-to-SVG-format.cs) | `SvgSaveOptions` | Convert PDF document from PDF to SVG format |
| [Convert-PDF-document-from-PDF-to-XML-format](./Convert-PDF-document-from-PDF-to-XML-format.cs) | `XmlSaveOptions` | Convert PDF document from PDF to XML format |
| [Convert-a-document-to-a-PDF-X-3-format-document-from-HTML-to...](./Convert-a-document-to-a-PDF-X-3-format-document-from-HTML-to-SVG-format.cs) | `HtmlLoadOptions`, `SvgSaveOptions` | Convert a document to a PDF X 3 format document from HTML to SVG format |
| [Convert-a-document-to-a-PDF-X-3-format-document-from-PDF-to-...](./Convert-a-document-to-a-PDF-X-3-format-document-from-PDF-to-PDF-format.cs) |  | Convert a document to a PDF X 3 format document from PDF to PDF format |
| [Convert-a-document-to-a-PDF-X-3-format-document-from-PDF-to-...](./Convert-a-document-to-a-PDF-X-3-format-document-from-PDF-to-XML-format.cs) | `XmlSaveOptions` | Convert a document to a PDF X 3 format document from PDF to XML format |
| [Create-PDF-document-programmatically-using-HTML-format](./Create-PDF-document-programmatically-using-HTML-format.cs) | `HtmlLoadOptions` | Create PDF document programmatically using HTML format |
| [Create-PDF-document-programmatically-using-SVG-format](./Create-PDF-document-programmatically-using-SVG-format.cs) | `SvgLoadOptions` | Create PDF document programmatically using SVG format |
| [Encrypt-and-Decrypt-PDF-File-using-CGM-format](./Encrypt-and-Decrypt-PDF-File-using-CGM-format.cs) | `CgmLoadOptions` | Encrypt and Decrypt PDF File using CGM format |
| [Encrypt-and-Decrypt-PDF-File-using-HTML-format](./Encrypt-and-Decrypt-PDF-File-using-HTML-format.cs) |  | Encrypt and Decrypt PDF File using HTML format |
| [Encrypt-and-Decrypt-PDF-File-using-PDF-format](./Encrypt-and-Decrypt-PDF-File-using-PDF-format.cs) |  | Encrypt and Decrypt PDF File using PDF format |
| [Encrypt-and-Decrypt-PDF-File-using-SVG-format](./Encrypt-and-Decrypt-PDF-File-using-SVG-format.cs) | `SvgSaveOptions` | Encrypt and Decrypt PDF File using SVG format |
| [How-to-Create-PDF-File-using-C-using-HTML-format](./How-to-Create-PDF-File-using-C-using-HTML-format.cs) | `HtmlLoadOptions` | How to Create PDF File using C using HTML format |
| [How-to-Create-PDF-File-using-C-using-PDF-format](./How-to-Create-PDF-File-using-C-using-PDF-format.cs) | `TextFragment` | How to Create PDF File using C using PDF format |
| [How-to-Create-PDF-File-using-C-using-SVG-format](./How-to-Create-PDF-File-using-C-using-SVG-format.cs) | `SvgLoadOptions` | How to Create PDF File using C using SVG format |
| [Merge-PDF-using-CGM-format](./Merge-PDF-using-CGM-format.cs) | `CgmLoadOptions` | Merge PDF using CGM format |
| [Merge-PDF-using-HTML-format](./Merge-PDF-using-HTML-format.cs) |  | Merge PDF using HTML format |
| [Merge-PDF-using-PDF-format](./Merge-PDF-using-PDF-format.cs) |  | Merge PDF using PDF format |
| [Open-PDF-document-programmatically-using-CGM-format](./Open-PDF-document-programmatically-using-CGM-format.cs) | `CgmLoadOptions` | Open PDF document programmatically using CGM format |
| [Open-PDF-document-programmatically-using-EPUB-format](./Open-PDF-document-programmatically-using-EPUB-format.cs) | `EpubLoadOptions` | Open PDF document programmatically using EPUB format |
| [Open-PDF-document-programmatically-using-HTML-format](./Open-PDF-document-programmatically-using-HTML-format.cs) | `HtmlLoadOptions` | Open PDF document programmatically using HTML format |
| [Open-PDF-document-programmatically-using-MHT-format](./Open-PDF-document-programmatically-using-MHT-format.cs) | `MhtLoadOptions` | Open PDF document programmatically using MHT format |
| [Open-PDF-document-programmatically-using-PCL-format](./Open-PDF-document-programmatically-using-PCL-format.cs) | `PclLoadOptions` | Open PDF document programmatically using PCL format |
| [Open-PDF-document-programmatically-using-PDF-format](./Open-PDF-document-programmatically-using-PDF-format.cs) |  | Open PDF document programmatically using PDF format |
| [Open-PDF-document-programmatically-using-PS-format](./Open-PDF-document-programmatically-using-PS-format.cs) | `PsLoadOptions` | Open PDF document programmatically using PS format |
| [Open-PDF-document-programmatically-using-TeX-format](./Open-PDF-document-programmatically-using-TeX-format.cs) |  | Open PDF document programmatically using TeX format |
| ... | | *and 48 more files* |

## Category Statistics
- Total examples: 78

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-11 | Run: `20260311_113226_362e59`
<!-- AUTOGENERATED:END -->

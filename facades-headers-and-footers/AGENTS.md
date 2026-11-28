---
name: facades-headers-and-footers
description: C# examples for facades-headers-and-footers using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-headers-and-footers

> **Facades headers and footers** in PDF using C# / .NET -- **34** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-headers-and-footers** category.
This folder contains standalone C# examples for facades-headers-and-footers operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-headers-and-footers**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (28/34 files) ← category-specific
- `using Aspose.Pdf;` (21/34 files) ← category-specific
- `using Aspose.Pdf.Text;` (15/34 files)
- `using Aspose.Pdf.Drawing;` (4/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (34/34 files)
- `using System.Drawing;` (10/34 files)
- `using System.Linq;` (1/34 files)
- `using System.Text;` (1/34 files)

## Common Code Pattern

Most files in this category use `PdfFileStamp` from `Aspose.Pdf.Facades`:

```csharp
PdfFileStamp tool = new PdfFileStamp();
tool.BindPdf("input.pdf");
// ... PdfFileStamp operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-arabic-footer-to-pdf](./add-arabic-footer-to-pdf.cs) | Add Arabic Footer to PDF with PdfFileStamp | `PdfFileStamp`, `BindPdf`, `FormattedText` | Demonstrates loading a PDF using PdfFileStamp, creating a FormattedText object with Arabic charac... |
| [add-blue-times-new-roman-footer](./add-blue-times-new-roman-footer.cs) | Add Blue Times New Roman Footer to PDF | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates binding an existing PDF with PdfFileStamp, creating a blue Times New Roman footer us... |
| [add-confidential-footer-to-pdf](./add-confidential-footer-to-pdf.cs) | Add Confidential Footer to PDF using TextStamp | `Document`, `TextStamp`, `FindFont` | Shows how to load a PDF, create a black "Company Confidential" footer with a 20‑point bottom marg... |
| [add-dark-green-georgia-header](./add-dark-green-georgia-header.cs) | Add Dark Green Georgia Header to PDF using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates loading a PDF with PdfFileStamp, creating a FormattedText header in dark‑green Georg... |
| [add-dark-red-footer-pdffilestamp](./add-dark-red-footer-pdffilestamp.cs) | Add Dark Red Footer to PDF with PdfFileStamp | `Document`, `PdfFileStamp`, `FormattedText` | Shows how to load a PDF, create a dark‑red FormattedText footer with a 14‑point bottom margin, an... |
| [add-footer-image-to-pdf](./add-footer-image-to-pdf.cs) | Add Footer Image to PDF Using ImageStamp | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp positioned at the bottom center with zero marg... |
| [add-footer-image-to-pdf__v2](./add-footer-image-to-pdf__v2.cs) | Add Footer Image to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `Stamp` | Demonstrates binding a PDF, creating a stamp from an image stream, positioning it 30 points above... |
| [add-footer-image-to-pdf__v3](./add-footer-image-to-pdf__v3.cs) | Add Footer Image to PDF Using ImageStamp | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF, create an ImageStamp from a file stream, position it as a bottom‑... |
| [add-footer-to-pdf-using-pdffilestamp](./add-footer-to-pdf-using-pdffilestamp.cs) | Add Footer to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddFooter` | Shows how to bind an existing PDF, create a multi‑line formatted footer, add it with a bottom mar... |
| [add-gray-helvetica-footer-to-pdf](./add-gray-helvetica-footer-to-pdf.cs) | Add Gray Helvetica Footer to PDF Using PdfFileStamp | `PdfFileStamp`, `FormattedText`, `FontStyle` | Demonstrates loading a PDF with PdfFileStamp, creating a gray Helvetica footer via FormattedText,... |
| [add-green-helvetica-header-to-pdf](./add-green-helvetica-header-to-pdf.cs) | Add Green Helvetica Header to PDF using PdfFileStamp | `PdfFileStamp`, `FormattedText`, `BindPdf` | Demonstrates loading a PDF with PdfFileStamp, creating a green Helvetica header using FormattedTe... |
| [add-green-times-new-roman-header](./add-green-times-new-roman-header.cs) | Add Green Times New Roman Header to PDF | `Document`, `PdfFileStamp`, `FormattedText` | Shows how to load a PDF with Aspose.Pdf, create a green Times New Roman header using FormattedTex... |
| [add-header-image-to-pdf-using-pdffilestamp](./add-header-image-to-pdf-using-pdffilestamp.cs) | Add Header Image to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to bind an existing PDF, insert a header image from a TIFF file via a FileStream with a... |
| [add-header-image-to-pdf-using-pdffilestamp__v2](./add-header-image-to-pdf-using-pdffilestamp__v2.cs) | Add Header Image to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to bind a PDF, add a header image from a TIFF file via FileStream with a top mar... |
| [add-header-image-to-pdf](./add-header-image-to-pdf.cs) | Add Header Image to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates binding a PDF, inserting a header image from a FileStream with a top margin, and sav... |
| [add-header-image-to-pdf__v2](./add-header-image-to-pdf__v2.cs) | Add Header Image to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp for a header image, applying i... |
| [add-header-image-to-pdf__v3](./add-header-image-to-pdf__v3.cs) | Add Header Image to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddStamp` | Shows how to bind a PDF, create a header stamp from an image stream with a top margin, apply it t... |
| [add-header-image-to-pdf__v4](./add-header-image-to-pdf__v4.cs) | Add Header Image to PDF Using ImageStamp | `Document`, `ImageStamp`, `AddStamp` | Loads a PDF, creates an ImageStamp from a FileStream, positions it as a top‑centered header with ... |
| [add-header-to-pdf-using-pdffilestamp](./add-header-to-pdf-using-pdffilestamp.cs) | Add Header to PDF Using PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to bind an existing PDF, create formatted header lines with specific margins, ad... |
| [add-image-footer-to-pdf](./add-image-footer-to-pdf.cs) | Add Image Footer to PDF Using PdfFileStamp | `PdfFileStamp`, `Stamp`, `BindImage` | Shows how to bind a PDF, create an image stamp from a FileStream, position it 40 points above the... |
| [add-image-header-to-pdf](./add-image-header-to-pdf.cs) | Add Image Header to PDF Using PdfFileStamp | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to bind a PDF, load a logo image via FileStream, configure its size and top‑margin plac... |
| [add-image-header-to-pdf__v2](./add-image-header-to-pdf__v2.cs) | Add Image Header to PDF with ImageStamp | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp from a logo file using a FileStream, set a top marg... |
| [add-image-header-to-pdf__v3](./add-image-header-to-pdf__v3.cs) | Add Image Header to PDF Using ImageStamp | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp from a PNG, positioning it as a top header wit... |
| [add-japanese-header-to-pdf](./add-japanese-header-to-pdf.cs) | Add Japanese Header to PDF with PdfFileStamp | `Document`, `FormattedText`, `PdfFileStamp` | Demonstrates loading a PDF, creating a FormattedText header containing Japanese characters, apply... |
| [add-magenta-draft-footer-to-pdf](./add-magenta-draft-footer-to-pdf.cs) | Add Magenta Draft Footer to PDF with PdfFileStamp | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates how to bind an existing PDF, create a magenta "Draft" footer using FormattedText, ad... |
| [add-maroon-verdana-footer-to-pdf](./add-maroon-verdana-footer-to-pdf.cs) | Add Maroon Verdana Footer to PDF using PdfFileStamp | `PdfFileStamp`, `FormattedText`, `BindPdf` | Demonstrates loading a PDF with PdfFileStamp, creating a maroon Verdana footer via FormattedText,... |
| [add-navy-calibri-header-to-pdf](./add-navy-calibri-header-to-pdf.cs) | Add Navy Calibri Header to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Loads an existing PDF, creates a navy Calibri header using FormattedText, adds it with a top marg... |
| [add-orange-arial-footer-to-pdf](./add-orange-arial-footer-to-pdf.cs) | Add Orange Arial Footer to PDF | `PdfFileStamp`, `BindPdf`, `FormattedText` | Demonstrates loading a PDF with PdfFileStamp, creating an orange Arial footer using FormattedText... |
| [add-orange-symbol-header-pdf](./add-orange-symbol-header-pdf.cs) | Add Orange Symbol Header to PDF with PdfFileStamp | `Document`, `PdfFileStamp`, `FormattedText` | Demonstrates loading a PDF, creating a formatted orange "Symbol" header using FormattedText, addi... |
| [add-page-footer-with-pdffilemend](./add-page-footer-with-pdffilemend.cs) | Add Page Footer with Formatted Text Using PdfFileMend | `Document`, `BindPdf`, `AddText` | Shows how to load a PDF, create a blue "Page 1 of 10" footer with FormattedText, add it to all pa... |
| ... | | | *and 4 more files* |

## Category Statistics
- Total examples: 34

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-headers-and-footers patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-28 | Run: `20261128_004000_7c71f0`
<!-- AUTOGENERATED:END -->

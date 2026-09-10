---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **51** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (29/51 files) ← category-specific
- `using Aspose.Pdf;` (27/51 files) ← category-specific
- `using Aspose.Pdf.Devices;` (18/51 files)
- `using Aspose.Pdf.Text;` (4/51 files)
- `using System;` (37/51 files)
- `using System.IO;` (37/51 files)
- `using System.Drawing.Imaging;` (15/51 files)
- `using System.Threading.Tasks;` (1/51 files)

## Common Code Pattern

Most files in this category use `PdfConverter` from `Aspose.Pdf.Facades`:

```csharp
PdfConverter tool = new PdfConverter();
tool.BindPdf("input.pdf");
// ... PdfConverter operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to iterate over PDF files in a folder and convert each page of every PDF into separate ... |
| [convert-odd-pdf-pages-to-png](./convert-odd-pdf-pages-to-png.cs) | Convert odd pdf pages to png |  | Convert odd pdf pages to png |
| [convert-pdf-odd-pages-to-png](./convert-pdf-odd-pages-to-png.cs) | Convert PDF Odd Pages to PNG Images | `Document`, `PdfConverter`, `BindPdf` | Shows how to extract only the odd‑numbered pages from a PDF and save each page as a separate PNG ... |
| [convert-pdf-pages-3-8-to-bmp](./convert-pdf-pages-3-8-to-bmp.cs) | Convert PDF Pages 3‑8 to BMP Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 3 to 8) ... |
| [convert-pdf-pages-3-8-to-multi-page-tiff](./convert-pdf-pages-3-8-to-multi-page-tiff.cs) | Convert PDF Pages 3‑8 to Multi‑Page TIFF | `Document`, `PdfConverter`, `BindPdf` | Shows how to load a PDF, select a specific page range, and save those pages as a single multi‑pag... |
| [convert-pdf-pages-3-8-to-tiff](./convert-pdf-pages-3-8-to-tiff.cs) | Convert pdf pages 3 8 to tiff |  | Convert pdf pages 3 8 to tiff |
| [convert-pdf-pages-4-9-to-multi-page-tiff](./convert-pdf-pages-4-9-to-multi-page-tiff.cs) | Convert pdf pages 4 9 to multi page tiff |  | Convert pdf pages 4 9 to multi page tiff |
| [convert-pdf-pages-to-bmp-150-dpi](./convert-pdf-pages-to-bmp-150-dpi.cs) | Convert PDF Pages to BMP Images (150 DPI) | `Document`, `Resolution`, `BmpDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 150 DPI resolution, and converting the firs... |
| [convert-pdf-pages-to-bmp-helvetica-arial](./convert-pdf-pages-to-bmp-helvetica-arial.cs) | Convert pdf pages to bmp helvetica arial |  | Convert pdf pages to bmp helvetica arial |
| [convert-pdf-pages-to-bmp-images](./convert-pdf-pages-to-bmp-images.cs) | Convert PDF Pages to BMP Images (Partial Range) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a specific page range of a PDF into... |
| [convert-pdf-pages-to-bmp](./convert-pdf-pages-to-bmp.cs) | Convert pdf pages to bmp |  | Convert pdf pages to bmp |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1-10 to JPEG Images with 150 DPI | `Document`, `PdfConverter`, `BindPdf` | Demonstrates using Aspose.Pdf's PdfConverter to convert the first ten pages of a PDF into JPEG fi... |
| [convert-pdf-pages-to-jpeg](./convert-pdf-pages-to-jpeg.cs) | Convert pdf pages to jpeg |  | Convert pdf pages to jpeg |
| [convert-pdf-pages-to-multi-page-tiff](./convert-pdf-pages-to-multi-page-tiff.cs) | Convert PDF Pages 4-9 to Multi-Page TIFF | `Document`, `PdfConverter`, `StartPage` | Demonstrates extracting pages 4 through 9 from a PDF and saving them as a single multi-page TIFF ... |
| [convert-pdf-pages-to-png-reverse-order](./convert-pdf-pages-to-png-reverse-order.cs) | Convert PDF Pages to PNG in Reverse Order | `Document`, `PdfConverter`, `BindPdf` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF document to a PN... |
| [convert-pdf-pages-to-png](./convert-pdf-pages-to-png.cs) | Convert pdf pages to png |  | Convert pdf pages to png |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Individual TIFF Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to extract each page of a PDF and save it as a s... |
| [convert-pdf-to-bmp-200-dpi](./convert-pdf-to-bmp-200-dpi.cs) | Convert PDF to BMP Images with 200 DPI | `Document`, `Resolution`, `BmpDevice` | Shows how to load a PDF using Aspose.Pdf, set a 200 DPI resolution, and convert each page to a BM... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert the first ten pages of a PDF i... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Resolution Settings | `PdfConverter`, `Document`, `Resolution` | Demonstrates how to convert each page of a PDF document to BMP images using Aspose.Pdf.Facades.Pd... |
| [convert-pdf-to-bmp-images__v2](./convert-pdf-to-bmp-images__v2.cs) | Convert pdf to bmp images__v2 |  | Convert pdf to bmp images__v2 |
| [convert-pdf-to-bmp-partial-range](./convert-pdf-to-bmp-partial-range.cs) | Convert pdf to bmp partial range |  | Convert pdf to bmp partial range |
| [convert-pdf-to-bmp-with-cropbox](./convert-pdf-to-bmp-with-cropbox.cs) | Convert PDF to BMP Images Using CropBox | `Document`, `BmpDevice`, `Resolution` | Demonstrates loading a PDF (creating a placeholder if missing) and rendering each page to a BMP i... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF Pages to BMP Images with Font Substitution | `Document`, `PdfConverter`, `BindPdf` | Shows how to convert each page of a PDF document to BMP images using Aspose.Pdf's PdfConverter, w... |
| [convert-pdf-to-bmp-with-font-substitution__v2](./convert-pdf-to-bmp-with-font-substitution__v2.cs) | Convert pdf to bmp with font substitution__v2 |  | Convert pdf to bmp with font substitution__v2 |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution Multi-Page TIFF | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert a PDF file into a single multi... |
| [convert-pdf-to-jpeg-300dpi-cropbox](./convert-pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF to JPEG Images with 300 DPI and CropBox | `PdfConverter`, `Resolution`, `PageCoordinateType` | Demonstrates converting each page of a PDF to high‑resolution JPEG images (300 DPI) using the Cro... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF to JPEG Images at 96 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates converting each page of a PDF into separate JPEG files with a web‑friendly 96 DPI re... |
| [convert-pdf-to-jpeg-first-5-pages-200-dpi](./convert-pdf-to-jpeg-first-5-pages-200-dpi.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to convert a PDF document to JPEG images using Aspose.Pdf, limiting the conversi... |
| [convert-pdf-to-jpeg-first-5-pages](./convert-pdf-to-jpeg-first-5-pages.cs) | Convert pdf to jpeg first 5 pages |  | Convert pdf to jpeg first 5 pages |
| ... | | | *and 21 more files* |

## Category Statistics
- Total examples: 51

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

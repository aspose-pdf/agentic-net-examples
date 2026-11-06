---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Devices;` (40/40 files) ← category-specific
- `using Aspose.Pdf;` (38/40 files) ← category-specific
- `using Aspose.Pdf.Facades;` (11/40 files)
- `using Aspose.Pdf.Text;` (9/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Threading.Tasks;` (1/40 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [batch-convert-pdfs-to-jpeg__v2](./batch-convert-pdfs-to-jpeg__v2.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through a folder of PDF files and convert each page to a JPEG image u... |
| [convert-pdf-odd-pages-to-png](./convert-pdf-odd-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `Document`, `PageCollection`, `PngDevice` | Loads a PDF file, iterates over its odd‑numbered pages, and saves each page as a separate PNG ima... |
| [convert-pdf-pages-3-8-to-bmp](./convert-pdf-pages-3-8-to-bmp.cs) | Convert PDF Pages to BMP Images (Pages 3-8) | `Document`, `Resolution`, `BmpDevice` | Shows how to convert a specific range of PDF pages to BMP images using Aspose.Pdf, including page... |
| [convert-pdf-pages-3-8-to-multi-page-tiff](./convert-pdf-pages-3-8-to-multi-page-tiff.cs) | Convert PDF Pages 3‑8 to TIFF Images | `Document`, `TiffDevice`, `Resolution` | Shows how to convert a specific range of PDF pages (pages 3 through 8) into separate TIFF files u... |
| [convert-pdf-pages-to-bmp-150-dpi](./convert-pdf-pages-to-bmp-150-dpi.cs) | Convert PDF Pages to BMP Images (1‑20) at 150 DPI | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert the first 20 pages of a PDF in... |
| [convert-pdf-pages-to-bmp-helvetica-to-arial](./convert-pdf-pages-to-bmp-helvetica-to-arial.cs) | Convert PDF Pages 5-7 to BMP with Helvetica to Arial Substit... | `Document`, `FontRepository`, `SimpleFontSubstitution` | The example loads a PDF, applies a Helvetica‑to‑Arial font substitution, and renders pages 5 thro... |
| [convert-pdf-pages-to-bmp-images](./convert-pdf-pages-to-bmp-images.cs) | Convert PDF Pages 2-6 to BMP Images | `Document`, `BmpDevice`, `Resolution` | Demonstrates loading a PDF with Aspose.Pdf, selecting a page range (2 to 6), and saving each page... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages to JPEG Images with 150 DPI | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to load a PDF, select pages 1‑10, and convert each page to a JPEG image at 150 D... |
| [convert-pdf-pages-to-multi-page-tiff](./convert-pdf-pages-to-multi-page-tiff.cs) | Convert PDF Pages 4‑9 to Multi‑Page TIFF | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to extract a specific page range from a PDF and save it as a multi‑page TIFF using Aspo... |
| [convert-pdf-pages-to-png-reverse-order](./convert-pdf-pages-to-png-reverse-order.cs) | Convert PDF Pages to PNG Images in Reverse Order | `Document`, `PdfConverter`, `BindPdf` | Shows how to load a PDF with Aspose.Pdf, iterate its pages from the last to the first, and render... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Separate TIFF Images | `Document`, `Resolution`, `TiffDevice` | Demonstrates loading a PDF with Aspose.Pdf, iterating through each page, and saving each page as ... |
| [convert-pdf-to-bmp-200-dpi](./convert-pdf-to-bmp-200-dpi.cs) | Convert PDF Pages to BMP Images at 200 DPI | `Document`, `Resolution`, `BmpDevice` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, and saving each page as ... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `Document`, `BmpDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, render up to the first ten pages to BMP format, and save... |
| [convert-pdf-to-bmp-images__v2](./convert-pdf-to-bmp-images__v2.cs) | Convert PDF to BMP Images with Resolution Settings | `Document`, `BmpDevice`, `Resolution` | Shows how to load a PDF using Aspose.Pdf, set a BmpDevice with a specific resolution, and convert... |
| [convert-pdf-to-bmp-with-cropbox](./convert-pdf-to-bmp-with-cropbox.cs) | Convert PDF to BMP Images Using CropBox | `Document`, `Resolution`, `BmpDevice` | Shows how to convert each page of a PDF to BMP files with Aspose.Pdf, automatically trimming marg... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `FontRepository`, `SimpleFontSubstitution` | Shows how to load a PDF, set up font substitution for missing fonts, and convert each page to a B... |
| [convert-pdf-to-bmp-with-font-substitution__v2](./convert-pdf-to-bmp-with-font-substitution__v2.cs) | Convert PDF to BMP with Font Substitution | `Document`, `Substitutions`, `SimpleFontSubstitution` | Demonstrates loading a PDF, registering a Times New Roman to Calibri font substitution, and conve... |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution TIFF Images | `Document`, `PdfConverter`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and save each page as a separ... |
| [convert-pdf-to-jpeg-300dpi-cropbox](./convert-pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF to JPEG Images with 300 DPI | `Document`, `JpegDevice`, `Resolution` | Shows how to load a PDF using Aspose.Pdf, configure a JpegDevice at 300 DPI, and save each page a... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF Pages to JPEG Images at 96 DPI | `Document`, `JpegDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, create a JpegDevice with 96 DPI and 80% quality, and sav... |
| [convert-pdf-to-jpeg-first-5-pages-200-dpi](./convert-pdf-to-jpeg-first-5-pages-200-dpi.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `JpegDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, iterate over up to five pages, and save each page as a 2... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF Pages to JPEG Images | `Document`, `PageCollection`, `JpegDevice` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, and saving each page as ... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images | `Document`, `JpegDevice`, `Pages` | Shows how to load a PDF with Aspose.Pdf and convert each page to a JPEG file using the JpegDevice... |
| [convert-pdf-to-jpeg-images__v3](./convert-pdf-to-jpeg-images__v3.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Page` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, and save each page as an indi... |
| [convert-pdf-to-jpeg-preview-images](./convert-pdf-to-jpeg-preview-images.cs) | Convert PDF Pages to JPEG Preview Images | `Document`, `JpegDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, define a page range (1‑3), and convert each page to a JP... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `Substitutions`, `SimpleFontSubstitution` | Shows how to replace missing fonts in a PDF using a custom TrueType font and then render each pag... |
| [convert-pdf-to-multi-page-tiff-300-dpi](./convert-pdf-to-multi-page-tiff-300-dpi.cs) | Convert PDF to TIFF Images with 300 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates converting each page of a PDF to separate TIFF files using Aspose.Pdf.Facades.PdfCon... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi‑Page TIFF at 600 DPI | `PdfConverter`, `Resolution`, `CompressionType` | Shows how to convert a PDF document into a single multi‑page TIFF image with a resolution of 600 ... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Font Substitution | `Document`, `PdfConverter`, `BindPdf` | Demonstrates converting a PDF document to a multi‑page TIFF image while substituting the Courier ... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution__v2.cs) | Convert PDF to Multi-Page TIFF with Font Substitution | `Document`, `Add`, `Resolution` | Shows how to convert a PDF document to a multi-page TIFF image using Aspose.Pdf, applying Symbol ... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-06 | Run: `20261106_033110_dd579b`
<!-- AUTOGENERATED:END -->

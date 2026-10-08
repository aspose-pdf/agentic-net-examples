---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **34** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Devices;` (32/34 files) ← category-specific
- `using Aspose.Pdf;` (31/34 files) ← category-specific
- `using Aspose.Pdf.Facades;` (10/34 files)
- `using Aspose.Pdf.Text;` (5/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (34/34 files)
- `using System.Threading.Tasks;` (1/34 files)

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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Process` | Shows how to iterate through PDF files in a folder and convert each page of every PDF into separa... |
| [convert-pdf-odd-pages-to-png](./convert-pdf-odd-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `Document`, `PngDevice`, `Resolution` | Demonstrates loading a PDF with Aspose.Pdf, iterating over only the odd‑numbered pages, and conve... |
| [convert-pdf-pages-3-8-to-bmp](./convert-pdf-pages-3-8-to-bmp.cs) | Convert PDF Pages to BMP Images with Page Range | `Document`, `BmpDevice`, `Resolution` | Shows how to convert a specific range of PDF pages (pages 3‑8) to BMP images using Aspose.Pdf, wi... |
| [convert-pdf-pages-3-8-to-multi-page-tiff](./convert-pdf-pages-3-8-to-multi-page-tiff.cs) | Convert PDF Pages 3‑8 to TIFF Images | `PdfConverter`, `BindPdf`, `SaveAsTIFF` | Demonstrates converting a specific range of PDF pages (pages 3 through 8) to separate TIFF files ... |
| [convert-pdf-pages-to-bmp-150-dpi](./convert-pdf-pages-to-bmp-150-dpi.cs) | Convert PDF Pages 1‑20 to BMP Images | `PdfConverter`, `Document`, `Resolution` | Demonstrates converting the first up to 20 pages of a PDF to BMP images at 150 DPI using Aspose.P... |
| [convert-pdf-pages-to-bmp-helvetica-to-arial](./convert-pdf-pages-to-bmp-helvetica-to-arial.cs) | Convert PDF Pages 5‑7 to BMP with Helvetica‑to‑Arial Substit... | `Document`, `FontRepository`, `SimpleFontSubstitution` | The example loads a PDF, substitutes the Helvetica font with Arial, and converts pages 5 through ... |
| [convert-pdf-pages-to-bmp-images](./convert-pdf-pages-to-bmp-images.cs) | Convert PDF Pages to BMP Images (Partial Range) | `Document`, `BmpDevice`, `Resolution` | Demonstrates how to load a PDF with Aspose.Pdf, select a page range (pages 2‑6), and render each ... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1-10 to JPEG Images at 150 DPI | `Document`, `JpegDevice`, `Resolution` | Loads a PDF document, renders pages 1 through 10 to JPEG files at 150 DPI using the CropBox, and ... |
| [convert-pdf-pages-to-multi-page-tiff](./convert-pdf-pages-to-multi-page-tiff.cs) | Convert PDF Pages 4-9 to Multi-Page TIFF | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates extracting pages 4 through 9 from a PDF and saving them as a multi-page TIFF file us... |
| [convert-pdf-pages-to-png-reverse-order](./convert-pdf-pages-to-png-reverse-order.cs) | Convert PDF Pages to PNG Images in Reverse Order | `Document`, `Page`, `PngDevice` | Demonstrates loading a PDF with Aspose.Pdf, iterating its pages from last to first, and saving ea... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Separate TIFF Images | `Document`, `Page`, `TiffDevice` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through each page, and render each page a... |
| [convert-pdf-to-bmp-200-dpi](./convert-pdf-to-bmp-200-dpi.cs) | Convert PDF to BMP Images at 200 DPI | `Document`, `Resolution`, `BmpDevice` | Shows how to load a PDF with Aspose.Pdf, render each page to a BMP image using a 200 DPI Resoluti... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `Document`, `BmpDevice`, `Resolution` | Demonstrates how to convert the first ten pages of a PDF document to BMP images using Aspose.Pdf. |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Custom Resolution | `Document`, `BmpDevice`, `Resolution` | Shows how to convert each page of a PDF into separate BMP files using Aspose.Pdf, setting the DPI... |
| [convert-pdf-to-bmp-with-cropbox](./convert-pdf-to-bmp-with-cropbox.cs) | Convert PDF Pages to BMP Using CropBox | `Document`, `Page`, `Resolution` | Loads a PDF, iterates through its pages and renders each page to a BMP image using the page's Cro... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `FontRepository`, `SimpleFontSubstitution` | Shows how to convert each page of a PDF to BMP images while applying font substitution for missin... |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution TIFF Images | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates converting each page of a PDF into separate TIFF files at 400 DPI using Aspose.Pdf's... |
| [convert-pdf-to-jpeg-300dpi-cropbox](./convert-pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF to JPEG with 300 DPI and CropBox Cropping | `Document`, `Page`, `JpegDevice` | Demonstrates loading a PDF, applying a uniform CropBox to all pages, and converting each page to ... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF to JPEG Images with 96 DPI | `Document`, `Resolution`, `JpegDevice` | Demonstrates how to load a PDF with Aspose.Pdf, set a 96 DPI resolution, and export each page as ... |
| [convert-pdf-to-jpeg-first-5-pages-200-dpi](./convert-pdf-to-jpeg-first-5-pages-200-dpi.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `Resolution`, `JpegDevice` | Shows how to convert up to the first five pages of a PDF document into JPEG images at a resolutio... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, and save each page as an indi... |
| [convert-pdf-to-jpeg-preview-images](./convert-pdf-to-jpeg-preview-images.cs) | Convert PDF Pages to JPEG Images | `Document`, `Page`, `JpegDevice` | Shows how to load a PDF with Aspose.Pdf, validate a page range, and render each selected page to ... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF Pages to JPEG with Font Substitution | `Document`, `FontRepository`, `SimpleFontSubstitution` | Loads a PDF, substitutes missing fonts with available ones, and converts each page to a JPEG imag... |
| [convert-pdf-to-multi-page-tiff-300-dpi](./convert-pdf-to-multi-page-tiff-300-dpi.cs) | Convert PDF to Multi‑Page TIFF with 300 DPI | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a PDF document into a single multi‑pa... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi‑Page TIFF at 600 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates how to use Aspose.Pdf's PdfConverter to convert a PDF document into a multi‑page TIF... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi-Page TIFF with Symbol Font Substitution | `PdfConverter`, `BindPdf`, `SaveAsTIFF` | Demonstrates converting a PDF to a multi-page TIFF at 300 DPI using Aspose.Pdf, while substitutin... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi-Page TIFF with Symbol-to-Arial Unicode ... | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to convert a PDF document to a multi-page TIFF image while substituting the Symb... |
| [convert-pdf-to-png-300-dpi](./convert-pdf-to-png-300-dpi.cs) | Convert PDF to PNG Images at 300 DPI | `Document`, `PngDevice`, `Resolution` | Loads a PDF document and renders each page to a PNG file at 300 DPI using Aspose.Pdf's PngDevice ... |
| [convert-pdf-to-png-72-dpi](./convert-pdf-to-png-72-dpi.cs) | Convert PDF Pages to PNG Images (72 DPI) | `Document`, `Page`, `PngDevice` | Loads a PDF document, iterates through each page, and renders the pages to PNG files at 72 DPI wh... |
| [convert-pdf-to-png-cropbox](./convert-pdf-to-png-cropbox.cs) | Convert PDF Pages to PNG Using CropBox | `Document`, `Page`, `PngDevice` | Loads a PDF document, iterates through each page, and saves each page as a PNG image using the pa... |
| ... | | | *and 4 more files* |

## Category Statistics
- Total examples: 34

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

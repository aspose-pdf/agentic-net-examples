---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (29/40 files) ← category-specific
- `using Aspose.Pdf.Facades;` (27/40 files) ← category-specific
- `using Aspose.Pdf.Devices;` (23/40 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Drawing.Imaging;` (15/40 files)
- `using System.Drawing;` (1/40 files)
- `using System.Threading.Tasks;` (1/40 files)

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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Shows how to loop through a folder of PDF files and convert each page of every PDF into separate ... |
| [convert-odd-pdf-pages-to-png](./convert-odd-pdf-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `Document`, `Extract`, `BindPdf` | Shows how to extract odd‑numbered pages from a PDF, create a temporary PDF, and convert each extr... |
| [convert-pdf-pages-1-3-to-jpeg](./convert-pdf-pages-1-3-to-jpeg.cs) | Convert PDF Pages 1‑3 to JPEG Images | `Document`, `PdfConverter`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 1 to 3) ... |
| [convert-pdf-pages-3-8-to-multi-page-tiff](./convert-pdf-pages-3-8-to-multi-page-tiff.cs) | Convert PDF Pages 3‑8 to Multi‑Page TIFF | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a specific page range of a PDF into a... |
| [convert-pdf-pages-4-9-to-multi-page-tiff](./convert-pdf-pages-4-9-to-multi-page-tiff.cs) | Convert PDF Pages 4‑9 to Multi‑Page TIFF | `PdfConverter`, `Document`, `BindPdf` | Shows how to extract pages 4 through 9 from a PDF and save them as a single multi‑page TIFF using... |
| [convert-pdf-pages-5-7-to-bmp-helvetica-to-arial](./convert-pdf-pages-5-7-to-bmp-helvetica-to-arial.cs) | Convert PDF Pages 5‑7 to BMP with Helvetica‑to‑Arial Substit... | `Document`, `PdfConverter`, `FontRepository` | Loads a PDF, substitutes missing Helvetica fonts with Arial, and converts pages 5 through 7 into ... |
| [convert-pdf-pages-to-bmp-images](./convert-pdf-pages-to-bmp-images.cs) | Convert PDF Pages to BMP Images (Pages 3‑8) | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a specific page range (3 to 8) of a P... |
| [convert-pdf-pages-to-bmp-images__v2](./convert-pdf-pages-to-bmp-images__v2.cs) | Convert PDF Pages to BMP Images (Partial Range) | `Document`, `PdfConverter`, `BindPdf` | Shows how to convert a specific page range of a PDF document to BMP image files using Aspose.Pdf'... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1-10 to JPEG Images at 150 DPI | `Document`, `PdfConverter`, `Resolution` | Demonstrates how to convert the first ten pages of a PDF document to JPEG images using a 150 DPI ... |
| [convert-pdf-pages-to-png](./convert-pdf-pages-to-png.cs) | Convert PDF Pages to PNG Images | `Document`, `Resolution`, `PngDevice` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, and render them as separate P... |
| [convert-pdf-to-bmp-150-dpi](./convert-pdf-to-bmp-150-dpi.cs) | Convert PDF Pages to BMP Images (1‑20) at 150 DPI | `Document`, `Resolution`, `BmpDevice` | Demonstrates how to load a PDF with Aspose.Pdf, set a 150 DPI resolution, and render pages 1‑20 (... |
| [convert-pdf-to-bmp-cropbox](./convert-pdf-to-bmp-cropbox.cs) | Convert PDF Pages to BMP Using CropBox | `Document`, `BmpDevice`, `Resolution` | Shows how to convert each page of a PDF document to BMP images while automatically respecting the... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert the first ten pages of a PDF document... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Resolution and Coordinate Set... | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates converting each page of a PDF to BMP files using Aspose.Pdf.Facades.PdfConverter, co... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `PdfConverter`, `Add` | Demonstrates converting each page of a PDF to BMP images while substituting Times New Roman with ... |
| [convert-pdf-to-bmp-with-font-substitution__v2](./convert-pdf-to-bmp-with-font-substitution__v2.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `FontRepository`, `BmpDevice` | Demonstrates how to convert each page of a PDF document to BMP images using Aspose.Pdf, applying ... |
| [convert-pdf-to-bmp](./convert-pdf-to-bmp.cs) | Convert PDF Pages to BMP Images | `Document`, `Resolution`, `BmpDevice` | Shows how to load a PDF with Aspose.Pdf, set a 200 DPI resolution, and render each page to a BMP ... |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution Multi-Page TIFF | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert a PDF document into a single m... |
| [convert-pdf-to-jpeg-300dpi-cropbox](./convert-pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF to JPEG Images with 300 DPI and CropBox | `Document`, `Resolution`, `JpegDevice` | This example loads a PDF document and converts each page to a JPEG image using a 300 DPI resoluti... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF Pages to JPEG Images at 96 DPI | `Document`, `Resolution`, `JpegDevice` | Shows how to load a PDF with Aspose.Pdf, set a 96 DPI resolution, and render each page to a JPEG ... |
| [convert-pdf-to-jpeg-first-5-pages-200-dpi](./convert-pdf-to-jpeg-first-5-pages-200-dpi.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `Resolution`, `JpegDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 200 DPI resolution, and converting up to th... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images (One per Page) | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `PdfSaveOptions`, `PdfConverter` | Shows how to apply a fallback font for missing fonts using PdfSaveOptions and then convert each p... |
| [convert-pdf-to-jpeg](./convert-pdf-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `Document`, `Page`, `Resolution` | Shows how to load a PDF with Aspose.Pdf, set a 72 DPI resolution, and save each page as a JPEG fi... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi-Page TIFF with Symbol-to-Arial Unicode ... | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates registering a Symbol‑to‑Arial Unicode MS font substitution and converting a PDF into... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution__v2.cs) | Convert PDF to Multi-Page TIFF with Font Substitution | `Document`, `PdfConverter`, `BindPdf` | Demonstrates converting a PDF to a single multi-page TIFF file while substituting missing Courier... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi-Page TIFF with 300 DPI | `Document`, `PdfConverter`, `BindPdf` | Demonstrates using Aspose.Pdf's PdfConverter facade to convert a PDF document into a single multi... |
| [convert-pdf-to-png-300-dpi](./convert-pdf-to-png-300-dpi.cs) | Convert PDF to PNG Images at 300 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separa... |
| [convert-pdf-to-png-72-dpi](./convert-pdf-to-png-72-dpi.cs) | Convert PDF to PNG Images with 72 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Shows how to convert each page of a PDF into PNG images at 72 DPI using Aspose.Pdf's PdfConverter... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-05 | Run: `20260505_160701_d75864`
<!-- AUTOGENERATED:END -->

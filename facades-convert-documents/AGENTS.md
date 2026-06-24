---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (28/40 files) ← category-specific
- `using Aspose.Pdf;` (24/40 files) ← category-specific
- `using Aspose.Pdf.Devices;` (22/40 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (39/40 files)
- `using System.Drawing.Imaging;` (11/40 files)
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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to process all PDF files in a directory, creating a sub‑folder for each PDF and convert... |
| [convert-pdf-pages-2-6-to-bmp](./convert-pdf-pages-2-6-to-bmp.cs) | Convert PDF Pages 2-6 to BMP Images | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 2 thro... |
| [convert-pdf-pages-3-8-to-multi-page-tiff](./convert-pdf-pages-3-8-to-multi-page-tiff.cs) | Convert PDF Pages 3-8 to Multi-Page TIFF | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 3‑8) of ... |
| [convert-pdf-pages-4-9-to-multi-page-tiff](./convert-pdf-pages-4-9-to-multi-page-tiff.cs) | Convert PDF Pages 4‑9 to Multi‑Page TIFF | `Document`, `TiffDevice`, `Resolution` | Shows how to extract a specific page range from a PDF and convert it into a single multi‑page TIF... |
| [convert-pdf-pages-to-bmp-images](./convert-pdf-pages-to-bmp-images.cs) | Convert PDF Pages to BMP Images (1‑20, 150 DPI) | `Document`, `Resolution`, `BmpDevice` | Shows how to load a PDF with Aspose.Pdf, set a 150 DPI resolution, and convert the first up to 20... |
| [convert-pdf-pages-to-bmp-with-font-substitution](./convert-pdf-pages-to-bmp-with-font-substitution.cs) | Convert PDF Pages 5-7 to BMP with Helvetica-to-Arial Substit... | `FontRepository`, `SimpleFontSubstitution`, `Document` | The example loads a PDF, substitutes missing Helvetica font with Arial, and converts pages 5 thro... |
| [convert-pdf-pages-to-bmp](./convert-pdf-pages-to-bmp.cs) | Convert PDF Pages to BMP Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to convert a specific page range (pages 3‑8) of a PDF document into separate BMP image ... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1-10 to JPEG with 150 DPI | `Document`, `JpegDevice`, `Resolution` | Shows how to render the first ten pages of a PDF document to JPEG images at 150 DPI, using the pa... |
| [convert-pdf-pages-to-jpeg-preview](./convert-pdf-pages-to-jpeg-preview.cs) | Convert PDF Pages 1-3 to JPEG Preview Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to generate JPEG images for the first three page... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Separate TIFF Images | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into indivi... |
| [convert-pdf-to-bmp-200-dpi](./convert-pdf-to-bmp-200-dpi.cs) | Convert PDF to BMP Images with 200 DPI | `Document`, `Resolution`, `BmpDevice` | Demonstrates loading a PDF using Aspose.Pdf, setting a 200 DPI resolution, and converting each pa... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `Document`, `BmpDevice`, `Resolution` | Demonstrates how to load a PDF with Aspose.Pdf, convert up to the first ten pages to BMP images, ... |
| [convert-pdf-to-bmp-using-pdfconverter](./convert-pdf-to-bmp-using-pdfconverter.cs) | Convert PDF to BMP Images with PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF document in... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `FontRepository`, `SimpleFontSubstitution`, `BmpDevice` | Demonstrates how to convert each page of a PDF to BMP images while substituting Times New Roman w... |
| [convert-pdf-to-bmp-with-font-substitution__v2](./convert-pdf-to-bmp-with-font-substitution__v2.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `Substitutions`, `SimpleFontSubstitution` | Demonstrates how to convert each page of a PDF document to BMP images while substituting missing ... |
| [convert-pdf-to-bmp](./convert-pdf-to-bmp.cs) | Convert PDF to BMP Images with Resolution and Coordinate Typ... | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to convert each page of a PDF to BMP images, setting a custom resolution and coo... |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution Multi-Page TIFF | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates converting a PDF file to a single multi-page TIFF image with 400 DPI resolution usin... |
| [convert-pdf-to-jpeg-300dpi](./convert-pdf-to-jpeg-300dpi.cs) | Convert PDF Pages to JPEG Images at 300 DPI | `Document`, `Page`, `JpegDevice` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, and saving each page as ... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF to JPEG Images at 96 DPI | `Document`, `Resolution`, `JpegDevice` | Shows how to load a PDF with Aspose.Pdf, set a 96‑DPI resolution, and save each page as a JPEG im... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF to JPEG Images Using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF into separate JPEG files while preserving the original pa... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF document into sepa... |
| [convert-pdf-to-jpeg-images__v3](./convert-pdf-to-jpeg-images__v3.cs) | Convert PDF to JPEG Images (One Image per Page) | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `PdfSaveOptions`, `PdfConverter` | Demonstrates how to substitute missing fonts in a PDF and then convert each page to JPEG images u... |
| [convert-pdf-to-multi-page-tiff-300-dpi](./convert-pdf-to-multi-page-tiff-300-dpi.cs) | Convert PDF to Multi‑Page TIFF with 300 DPI | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates converting a PDF file into a single multi‑page TIFF image at 300 DPI using Aspose.Pd... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi-Page TIFF at 600 DPI | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates how to use Aspose.Pdf's PdfConverter to convert a PDF document into a single multi-p... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Symbol‑to‑Arial Unicode ... | `PdfConverter`, `FontRepository`, `SimpleFontSubstitution` | Demonstrates how to globally substitute the Symbol font with Arial Unicode MS and convert a PDF d... |
| [convert-pdf-to-multipage-tiff-with-font-substituti...](./convert-pdf-to-multipage-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Symbol to Arial Unicode ... | `PdfConverter`, `RenderingOptions`, `BindPdf` | Demonstrates how to convert a PDF document to a single multi‑page TIFF image while substituting S... |
| [convert-pdf-to-png-72-dpi](./convert-pdf-to-png-72-dpi.cs) | Convert PDF to PNG Images with 72 DPI Preview | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into PNG im... |
| [convert-pdf-to-png-72-dpi__v2](./convert-pdf-to-png-72-dpi__v2.cs) | Convert PDF to PNG Images at 72 DPI Using CropBox | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF to PNG images at 72 DPI while preserving the visible cont... |
| [convert-pdf-to-png-cropbox](./convert-pdf-to-png-cropbox.cs) | Convert PDF Pages to PNG Using CropBox | `Document`, `Page`, `PngDevice` | Shows how to convert each PDF page to a PNG image while using the page's CropBox to render only t... |
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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

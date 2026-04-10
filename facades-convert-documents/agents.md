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

- `using Aspose.Pdf.Facades;` (36/40 files) ← category-specific
- `using Aspose.Pdf;` (28/40 files) ← category-specific
- `using Aspose.Pdf.Devices;` (21/40 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Drawing.Imaging;` (18/40 files)
- `using System.Collections.Generic;` (2/40 files)
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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of multiple PDF files in a folder to JPEG images using Aspose.Pdf'... |
| [convert-odd-pdf-pages-to-png](./convert-odd-pdf-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `Document`, `BindPdf`, `DoConvert` | Demonstrates extracting only the odd‑numbered pages from a PDF and saving each page as a separate... |
| [convert-pdf-pages-3-8-to-tiff](./convert-pdf-pages-3-8-to-tiff.cs) | Convert PDF Pages 3‑8 to a Single TIFF Image | `Document`, `TiffDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf and convert pages 3 through 8 into one TIFF file using th... |
| [convert-pdf-pages-4-9-to-multi-page-tiff](./convert-pdf-pages-4-9-to-multi-page-tiff.cs) | Convert PDF Pages 4‑9 to Multi‑Page TIFF | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to extract a specific page range from a PDF and save it as a single multi‑page T... |
| [convert-pdf-pages-to-bmp-150-dpi](./convert-pdf-pages-to-bmp-150-dpi.cs) | Convert PDF Pages 1‑20 to BMP Images at 150 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert the first 20 pages of a PDF into BMP ... |
| [convert-pdf-pages-to-bmp-helvetica-to-arial](./convert-pdf-pages-to-bmp-helvetica-to-arial.cs) | Convert PDF Pages 5‑7 to BMP with Helvetica to Arial Substit... | `Document`, `PdfConverter`, `DoConvert` | Demonstrates converting pages 5 through 7 of a PDF to BMP images using PdfConverter and applying ... |
| [convert-pdf-pages-to-bmp](./convert-pdf-pages-to-bmp.cs) | Convert PDF Pages 3-8 to BMP Images | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert a specific page range of a PD... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1-10 to JPEG with 150 DPI | `PdfConverter`, `Resolution` | Demonstrates converting the first ten pages of a PDF to JPEG images at 150 DPI using the PdfConve... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Separate TIFF Images | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to render each PDF page as an individual ... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert the first ten pages of a PDF i... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Resolution and Coordinate Set... | `PdfConverter`, `Resolution`, `PageCoordinateType` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert each page of a PDF into BMP f... |
| [convert-pdf-to-bmp-images__v2](./convert-pdf-to-bmp-images__v2.cs) | Convert PDF Pages to BMP Images | `Document`, `PdfConverter`, `BindPdf` | Shows how to convert each page of a PDF document to BMP files at 200 DPI using Aspose.Pdf's PdfCo... |
| [convert-pdf-to-bmp-using-cropbox](./convert-pdf-to-bmp-using-cropbox.cs) | Convert PDF to BMP Images Using CropBox Cropping | `Document`, `Resolution`, `BmpDevice` | Demonstrates converting each page of a PDF to BMP images at 300 DPI while automatically cropping ... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `BindPdf`, `DoConvert` | Demonstrates converting each page of a PDF to BMP files using Aspose.Pdf's PdfConverter, applying... |
| [convert-pdf-to-high-resolution-multi-page-tiff](./convert-pdf-to-high-resolution-multi-page-tiff.cs) | Convert PDF to High-Resolution Multi-Page TIFF | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf's PdfConverter to bind a PDF, set a 400 DPI resolution, and sa... |
| [convert-pdf-to-jpeg-300dpi-cropbox](./convert-pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF to JPEG Images with 300 DPI and CropBox | `Document`, `PdfConverter`, `Resolution` | Demonstrates converting each page of a PDF to high‑resolution JPEG images using 300 DPI and the C... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF to JPEG Images with 96 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert each page of a PDF into JPEG ... |
| [convert-pdf-to-jpeg-first-five-pages-200dpi](./convert-pdf-to-jpeg-first-five-pages-200dpi.cs) | Convert PDF to JPEG with 200 DPI and First Five Pages | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to convert a PDF document to JPEG images using Aspose.Pdf, limiting the conversi... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF to JPEG Images using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF into separate JPEG files using Aspose.Pdf's PdfConverter ... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images Page by Page | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-to-jpeg-preview](./convert-pdf-to-jpeg-preview.cs) | Convert PDF Pages to JPEG Preview Images | `Document`, `StartPage`, `EndPage` | Demonstrates how to convert the first three pages of a PDF into separate JPEG images using Aspose... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `Page`, `TextFragment` | Demonstrates converting each page of a PDF to JPEG images while applying a custom font substituti... |
| [convert-pdf-to-jpeg](./convert-pdf-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, create a JPEG device at 72 DPI, and save each page as a ... |
| [convert-pdf-to-multi-page-tiff-300-dpi](./convert-pdf-to-multi-page-tiff-300-dpi.cs) | Convert PDF to Multi-Page TIFF at 300 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates converting a PDF document to a single multi-page TIFF image using Aspose.Pdf with th... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi-Page TIFF at 600 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert a PDF into a single multi-pag... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Font Substitution | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to replace the Symbol font with Arial Unicode MS during PDF conversion and save ... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution__v2.cs) | Convert PDF to Multi-Page TIFF with Symbol-to-Arial Unicode ... | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to use Aspose.Pdf's PdfConverter to transform a PDF into a single multi-page TIF... |
| [convert-pdf-to-png-300-dpi](./convert-pdf-to-png-300-dpi.cs) | Convert PDF to PNG Images at 300 DPI | `Document`, `Resolution`, `PngDevice` | Shows how to convert each page of a PDF into separate PNG files at 300 DPI using Aspose.Pdf. |
| [convert-pdf-to-png-72-dpi](./convert-pdf-to-png-72-dpi.cs) | Convert PDF to PNG Images with 72 DPI Preview | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates using Aspose.Pdf's PdfConverter facade to convert each page of a PDF into low‑resolu... |
| [convert-pdf-to-png-72-dpi__v2](./convert-pdf-to-png-72-dpi__v2.cs) | Convert PDF to PNG Images with 72 DPI Using CropBox | `Document`, `Resolution`, `PngDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 72 DPI resolution, and converting each page... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

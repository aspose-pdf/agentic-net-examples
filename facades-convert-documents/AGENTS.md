---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **36** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (29/36 files) ← category-specific
- `using Aspose.Pdf;` (26/36 files) ← category-specific
- `using Aspose.Pdf.Devices;` (17/36 files)
- `using Aspose.Pdf.Text;` (5/36 files)
- `using System;` (36/36 files)
- `using System.IO;` (36/36 files)
- `using System.Drawing.Imaging;` (17/36 files)
- `using System.Threading.Tasks;` (1/36 files)

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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to iterate over PDF files in a folder and convert each page to separate JPEG images usi... |
| [convert-odd-pdf-pages-to-png](./convert-odd-pdf-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `Document`, `PdfConverter`, `Count` | Demonstrates extracting only odd‑numbered pages from a PDF and saving each page as a separate PNG... |
| [convert-pdf-pages-3-8-to-bmp](./convert-pdf-pages-3-8-to-bmp.cs) | Convert PDF Pages 3‑8 to BMP Images | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert a specific page range (pages ... |
| [convert-pdf-pages-3-8-to-tiff](./convert-pdf-pages-3-8-to-tiff.cs) | Convert PDF Pages 3-8 to TIFF | `Document`, `TiffDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf and use TiffDevice to convert pages 3 through 8 into a si... |
| [convert-pdf-pages-4-9-to-multi-page-tiff](./convert-pdf-pages-4-9-to-multi-page-tiff.cs) | Convert PDF Pages 4-9 to Multi-Page TIFF | `Document`, `PdfConverter`, `BindPdf` | Shows how to extract pages 4 through 9 from a PDF and save them as a single multi-page TIFF using... |
| [convert-pdf-pages-to-bmp-helvetica-arial](./convert-pdf-pages-to-bmp-helvetica-arial.cs) | Convert PDF Pages 5‑7 to BMP with Helvetica‑to‑Arial Substit... | `Document`, `PdfConverter`, `Add` | Shows how to replace Helvetica with Arial using font substitution and convert pages 5 to 7 of a P... |
| [convert-pdf-pages-to-bmp](./convert-pdf-pages-to-bmp.cs) | Convert PDF Pages to BMP Images (150 DPI) | `Document`, `Resolution`, `BmpDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 150 DPI resolution, and converting the firs... |
| [convert-pdf-pages-to-jpeg](./convert-pdf-pages-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-pages-to-png-reverse-order](./convert-pdf-pages-to-png-reverse-order.cs) | Convert PDF Pages to PNG Images in Reverse Order | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to load a PDF with Aspose.Pdf, iterate its pages from last to first, and save ea... |
| [convert-pdf-pages-to-png](./convert-pdf-pages-to-png.cs) | Convert PDF Pages to PNG Images | `Document`, `PdfConverter`, `BindPdf` | Shows how to use Aspose.Pdf's PdfConverter facade to convert each page of a PDF document into sep... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Separate TIFF Images | `Document`, `PpdfConverter`, `BindPdf` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to extract each page of a PDF as an indi... |
| [convert-pdf-to-bmp-first-10-pages](./convert-pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert the first ten pages of a PDF docume... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Resolution Settings | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates how to convert each page of a PDF document to BMP images using the PdfConverter faca... |
| [convert-pdf-to-bmp-images__v2](./convert-pdf-to-bmp-images__v2.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf, rasterize each page to a BMP file at 200 DPI using BmpDe... |
| [convert-pdf-to-bmp-partial-range](./convert-pdf-to-bmp-partial-range.cs) | Convert PDF Pages to BMP Images (Partial Range) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 2‑6) o... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Times New Roman to Calibri Su... | `Document`, `PdfConverter`, `DoConvert` | Loads a PDF, replaces Times New Roman with Calibri via custom font substitution, and converts eac... |
| [convert-pdf-to-bmp-with-font-substitution__v2](./convert-pdf-to-bmp-with-font-substitution__v2.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `PdfConverter`, `BindPdf` | Demonstrates converting each page of a PDF to BMP images using PdfConverter and applying a global... |
| [convert-pdf-to-jpeg-300-dpi](./convert-pdf-to-jpeg-300-dpi.cs) | Convert PDF to JPEG Images at 300 DPI Using CropBox | `PdfConverter`, `Resolution`, `BindPdf` | Shows how to convert each page of a PDF into high‑resolution JPEG images (300 DPI) using Aspose.P... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF to JPEG Images at 96 DPI | `Document`, `Resolution`, `JpegDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 96 DPI resolution, and converting each page... |
| [convert-pdf-to-jpeg-first-5-pages](./convert-pdf-to-jpeg-first-5-pages.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `Resolution`, `JpegDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 200 DPI resolution, and converting up to th... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF to JPEG Images Using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF into separate JPEG files while preserving the original pa... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF into separate JPEG files using Aspose.Pdf's PdfConverter ... |
| [convert-pdf-to-jpeg-preview](./convert-pdf-to-jpeg-preview.cs) | Convert PDF Pages to JPEG Images (Preview) | `Document`, `PdfConverter`, `StartPage` | Shows how to convert the first three pages of a PDF document into separate JPEG files using Aspos... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `PdfSaveOptions`, `PdfConverter` | Shows how to load a PDF, apply a default font for missing fonts, and convert each page to JPEG im... |
| [convert-pdf-to-multi-page-tiff-300-dpi](./convert-pdf-to-multi-page-tiff-300-dpi.cs) | Convert PDF to Multi‑Page TIFF at 300 DPI | `Document`, `PdfConverter`, `Resolution` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert a PDF document into a single ... |
| [convert-pdf-to-multi-page-tiff-400-dpi](./convert-pdf-to-multi-page-tiff-400-dpi.cs) | Convert PDF to Multi-Page TIFF at 400 DPI | `PdfConverter`, `Resolution`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert a PDF into a single multi-page... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi‑Page TIFF at 600 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates how to use Aspose.Pdf's PdfConverter to bind a PDF, set a 600 DPI resolution, and sa... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Symbol to Arial Font Sub... | `Document`, `PdfSaveOptions`, `PdfConverter` | Demonstrates loading a PDF, applying a fallback Arial font for missing Symbol characters, and con... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution__v2.cs) | Convert PDF to Multi‑Page TIFF with Font Substitution | `Document`, `PdfSaveOptions`, `PdfConverter` | Demonstrates loading a PDF, substituting missing Symbol fonts with Arial Unicode MS, and converti... |
| [convert-pdf-to-png-72-dpi](./convert-pdf-to-png-72-dpi.cs) | Convert PDF to PNG Images with 72 DPI Preview | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to convert each page of a PDF into separate PNG files using Aspose.Pdf's PdfConv... |
| ... | | | *and 6 more files* |

## Category Statistics
- Total examples: 36

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

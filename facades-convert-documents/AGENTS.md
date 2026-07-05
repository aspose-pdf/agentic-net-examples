---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

> **Facades convert documents** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (24/40 files) ← category-specific
- `using Aspose.Pdf.Devices;` (18/40 files)
- `using Aspose.Pdf.Text;` (7/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Drawing.Imaging;` (19/40 files)
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
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert every PDF in a directory to JPEG images, creating a subfolder per document a... |
| [convert-first-10-pdf-pages-to-bmp](./convert-first-10-pdf-pages-to-bmp.cs) | Convert First 10 PDF Pages to BMP Images | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf's PdfConverter facade to convert the first ten pages of a PDF ... |
| [convert-first-five-pdf-pages-to-jpeg](./convert-first-five-pdf-pages-to-jpeg.cs) | Convert First Five PDF Pages to JPEG at 200 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert the first five pages of a PDF into ... |
| [convert-odd-pdf-pages-to-png](./convert-odd-pdf-pages-to-png.cs) | Convert Odd PDF Pages to PNG Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfConverter to extract only odd-numbered pages from a PDF an... |
| [convert-pdf-pages-3-8-to-bmp](./convert-pdf-pages-3-8-to-bmp.cs) | Convert PDF Pages 3‑8 to BMP Images | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a specific page range (pages 3 to 8... |
| [convert-pdf-pages-3-8-to-tiff](./convert-pdf-pages-3-8-to-tiff.cs) | Convert PDF Pages 3‑8 to TIFF Images | `Document`, `TiffDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf, iterate pages 3 through 8, and convert each page to a TI... |
| [convert-pdf-pages-5-7-to-bmp-helvetica-to-arial](./convert-pdf-pages-5-7-to-bmp-helvetica-to-arial.cs) | Convert PDF Pages 5‑7 to BMP with Helvetica to Arial Substit... | `Document`, `Add`, `SimpleFontSubstitution` | Demonstrates converting specific PDF pages (5‑7) to BMP images while replacing the Helvetica font... |
| [convert-pdf-pages-to-individual-tiff](./convert-pdf-pages-to-individual-tiff.cs) | Convert PDF Pages to Individual TIFF Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf's PdfConverter to extract each page of a PDF and save it as a separat... |
| [convert-pdf-pages-to-jpeg-150-dpi](./convert-pdf-pages-to-jpeg-150-dpi.cs) | Convert PDF Pages 1‑10 to JPEG with 150 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to convert the first ten pages of a PDF document to JPEG images at 150 DPI using... |
| [convert-pdf-pages-to-jpeg-preview](./convert-pdf-pages-to-jpeg-preview.cs) | Convert PDF Pages 1-3 to JPEG Preview Images | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to generate JPEG images for the first thr... |
| [convert-pdf-pages-to-jpeg](./convert-pdf-pages-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-pages-to-multi-page-tiff](./convert-pdf-pages-to-multi-page-tiff.cs) | Convert PDF Pages 4-9 to Multi-Page TIFF | `Document`, `PdfConverter`, `BindPdf` | Demonstrates extracting a specific page range from a PDF and saving it as a single multi-page TIF... |
| [convert-pdf-pages-to-png](./convert-pdf-pages-to-png.cs) | Convert PDF Pages to PNG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates using Aspose.Pdf's PdfConverter facade to convert each page of a PDF into separate P... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to Individual TIFF Images | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to split a PDF into separate TIFF files, ... |
| [convert-pdf-to-bmp-images-200-dpi](./convert-pdf-to-bmp-images-200-dpi.cs) | Convert PDF to BMP Images with 200 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates converting each page of a PDF to BMP images at 200 DPI using Aspose.Pdf.Facades.PdfC... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images with Resolution Settings | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates using Aspose.Pdf's PdfConverter facade to convert each page of a PDF document into B... |
| [convert-pdf-to-bmp-images__v2](./convert-pdf-to-bmp-images__v2.cs) | Convert PDF to BMP Images with Font Substitution | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to convert each page of a PDF document to BMP images using Aspose.Pdf, with auto... |
| [convert-pdf-to-bmp-pages-150-dpi](./convert-pdf-to-bmp-pages-150-dpi.cs) | Convert PDF Pages to BMP Images (1‑20) at 150 DPI | `Document`, `Resolution`, `BmpDevice` | Shows how to load a PDF with Aspose.Pdf, iterate over the first 20 pages (or up to the document l... |
| [convert-pdf-to-bmp-partial-range](./convert-pdf-to-bmp-partial-range.cs) | Convert PDF Pages to BMP Images (Partial Range) | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a specific page range of a PDF into... |
| [convert-pdf-to-bmp-using-cropbox](./convert-pdf-to-bmp-using-cropbox.cs) | Convert PDF to BMP Images Using CropBox Cropping | `Document`, `PdfConverter`, `DoConvert` | Demonstrates how to convert each page of a PDF to BMP images with automatic margin cropping using... |
| [convert-pdf-to-bmp-with-font-substitution](./convert-pdf-to-bmp-with-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `PdfConverter`, `Substitutions` | Demonstrates how to convert each page of a PDF document to BMP images while substituting Times Ne... |
| [convert-pdf-to-jpeg-300-dpi](./convert-pdf-to-jpeg-300-dpi.cs) | Convert PDF to JPEG Images at 300 DPI | `PdfConverter`, `Resolution`, `BindPdf` | Shows how to convert each page of a PDF into separate JPEG files using Aspose.Pdf's PdfConverter ... |
| [convert-pdf-to-jpeg-96-dpi](./convert-pdf-to-jpeg-96-dpi.cs) | Convert PDF Pages to JPEG Images at 96 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF into separate JPEG... |
| [convert-pdf-to-jpeg-images](./convert-pdf-to-jpeg-images.cs) | Convert PDF to JPEG Images Using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to convert each page of a PDF into separate JPEG files while preserving the original pa... |
| [convert-pdf-to-jpeg-images__v2](./convert-pdf-to-jpeg-images__v2.cs) | Convert PDF to JPEG Images | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to use Aspose.Pdf.Facades.PdfConverter to convert each page of a PDF document into sepa... |
| [convert-pdf-to-jpeg-with-font-substitution](./convert-pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG with Font Substitution | `Document`, `Page`, `TextFragment` | Demonstrates how to convert each page of a PDF to JPEG images while providing a fallback font for... |
| [convert-pdf-to-multi-page-tiff-400-dpi](./convert-pdf-to-multi-page-tiff-400-dpi.cs) | Convert PDF to Multi-Page TIFF with 400 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates using Aspose.Pdf.Facades.PdfConverter to convert a PDF into a single multi-page TIFF... |
| [convert-pdf-to-multi-page-tiff-600-dpi](./convert-pdf-to-multi-page-tiff-600-dpi.cs) | Convert PDF to Multi-Page TIFF at 600 DPI | `PdfConverter`, `BindPdf`, `Resolution` | Demonstrates how to use Aspose.Pdf.Facades.PdfConverter to convert a PDF file into a single multi... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution.cs) | Convert PDF to Multi‑Page TIFF with Symbol to Arial Unicode ... | `PdfConverter`, `Document`, `FontRepository` | Demonstrates how to convert a PDF document to a single multi‑page TIFF file using Aspose.Pdf, whi... |
| [convert-pdf-to-multi-page-tiff-with-font-substitut...](./convert-pdf-to-multi-page-tiff-with-font-substitution__v2.cs) | Convert PDF to Multi-Page TIFF with Font Substitution | `Document`, `PdfConverter`, `Resolution` | Demonstrates converting a PDF document to a multi-page TIFF image while substituting the Courier ... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

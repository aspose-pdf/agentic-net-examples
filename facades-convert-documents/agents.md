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

- `using Aspose.Pdf;` (34/40 files) ← category-specific
- `using Aspose.Pdf.Devices;` (34/40 files) ← category-specific
- `using Aspose.Pdf.Facades;` (13/40 files)
- `using Aspose.Pdf.Text;` (9/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Drawing.Imaging;` (3/40 files)
- `using System.Runtime.InteropServices;` (1/40 files)
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
| [convert-pdf-pages-to-bmp](./convert-pdf-pages-to-bmp.cs) | Convert PDF Pages 5‑7 to BMP Images with Font Substitution | `Document`, `FontSubstitution`, `Add` | Shows how to replace Helvetica with Arial in a PDF and export pages 5 to 7 as BMP images. |
| [convert-pdf-pages-to-jpeg](./convert-pdf-pages-to-jpeg.cs) | Convert PDF Pages 1-10 to JPEG Images with 150 DPI | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to convert the first ten pages of a PDF to JPEG images at 150 DPI using the Crop... |
| [convert-pdf-pages-to-tiff](./convert-pdf-pages-to-tiff.cs) | Convert PDF Pages to TIFF Using CropBox | `Document`, `PdfConverter`, `StartPage` | Demonstrates converting pages 3 through 8 of a PDF to a single multi‑page TIFF image using the de... |
| [convert-pdf-to-bmp-range](./convert-pdf-to-bmp-range.cs) | Convert PDF Pages to BMP Images (Partial Range) | `Document`, `BmpDevice`, `Resolution` | Demonstrates how to convert specific pages of a PDF to BMP images using Aspose.Pdf. |
| [convert-pdf-to-bmp](./convert-pdf-to-bmp.cs) | Convert PDF Pages to BMP Images | `Document`, `BmpDevice`, `Resolution` | Converts the first 20 pages of a PDF to BMP images at 150 DPI resolution. |
| [convert-pdf-to-jpeg](./convert-pdf-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Converts each page of a PDF document into separate JPEG image files, naming each file with a page... |
| [convert-pdf-to-tiff](./convert-pdf-to-tiff.cs) | Convert PDF to TIFF Image | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates converting a PDF document to a TIFF image using Aspose.Pdf.Facades.PdfConverter with... |
| [convert-pdfs-to-jpeg](./convert-pdfs-to-jpeg.cs) | Convert PDFs to JPEG Images in Batch | `BindPdf`, `DoConvert`, `HasNextImage` | Converts each PDF file in a folder to separate JPEG images, one per page, using Aspose.Pdf's PdfC... |
| [pdf-to-bmp-200dpi](./pdf-to-bmp-200dpi.cs) | Convert PDF to BMP Images with 200 DPI | `Document`, `BmpDevice`, `Resolution` | Converts each page of a PDF document to a BMP image using 200 DPI resolution and the default Crop... |
| [pdf-to-bmp-conversion](./pdf-to-bmp-conversion.cs) | Convert PDF to BMP Images with Resolution and Coordinate Typ... | `Document`, `PdfConverter`, `Resolution` | Demonstrates converting each page of a PDF to BMP images using PdfConverter, setting custom resol... |
| [pdf-to-bmp-cropbox](./pdf-to-bmp-cropbox.cs) | Convert PDF Pages to BMP Images with CropBox Cropping | `Document`, `BmpDevice`, `Resolution` | Demonstrates converting each page of a PDF to BMP images using BmpDevice and setting the coordina... |
| [pdf-to-bmp-first-10-pages](./pdf-to-bmp-first-10-pages.cs) | Convert PDF to BMP Images (First 10 Pages) | `Document`, `BmpDevice`, `Resolution` | Demonstrates converting the first ten pages of a PDF document to separate BMP image files using A... |
| [pdf-to-bmp-font-substitution](./pdf-to-bmp-font-substitution.cs) | Convert PDF to BMP Images with Font Substitution | `Document`, `Resolution`, `BmpDevice` | Demonstrates converting each page of a PDF to BMP images while substituting Times New Roman with ... |
| [pdf-to-bmp-with-font-substitution](./pdf-to-bmp-with-font-substitution.cs) | Convert PDF Pages to BMP Images with Font Substitution | `Document`, `BmpDevice`, `Resolution` | Demonstrates converting each page of a PDF to BMP images while substituting missing fonts with a ... |
| [pdf-to-bmp](./pdf-to-bmp.cs) | Convert PDF Pages to BMP Images | `Document`, `BmpDevice`, `Process` | Converts pages 3 through 8 of a PDF document to separate BMP image files. |
| [pdf-to-jpeg-300dpi-cropbox](./pdf-to-jpeg-300dpi-cropbox.cs) | Convert PDF Pages to JPEG Images with 300 DPI and CropBox | `Document`, `JpegDevice`, `Resolution` | Converts each page of a PDF document to a JPEG image using 300 DPI resolution and the CropBox coo... |
| [pdf-to-jpeg-96dpi](./pdf-to-jpeg-96dpi.cs) | Convert PDF to JPEG Images with 96 DPI | `Document`, `Resolution`, `JpegDevice` | Converts each page of a PDF document into separate JPEG files using a 96 DPI resolution suitable ... |
| [pdf-to-jpeg-first5-200dpi](./pdf-to-jpeg-first5-200dpi.cs) | Convert PDF to JPEG Images (First 5 Pages, 200 DPI) | `Document`, `JpegDevice`, `Resolution` | Converts the first five pages of a PDF document to JPEG images at a resolution of 200 DPI. |
| [pdf-to-jpeg-preview](./pdf-to-jpeg-preview.cs) | Convert PDF Pages to JPEG Images (Preview) | `Document`, `JpegDevice`, `Resolution` | Converts the first three pages of a PDF into separate JPEG images using Aspose.Pdf. |
| [pdf-to-jpeg-with-font-substitution](./pdf-to-jpeg-with-font-substitution.cs) | Convert PDF to JPEG Images with Font Substitution | `Document`, `FontRepository`, `Save` | Demonstrates converting each page of a PDF to JPEG images while substituting missing fonts with a... |
| [pdf-to-jpeg](./pdf-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Converts each page of a PDF document to separate JPEG images while preserving the original page s... |
| [pdf-to-jpeg__v2](./pdf-to-jpeg__v2.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to convert each page of a PDF document into separate JPEG image files using Aspo... |
| [pdf-to-png-300dpi](./pdf-to-png-300dpi.cs) | Convert PDF Pages to PNG Images at 300 DPI | `Document`, `PngDevice`, `Resolution` | Converts each page of a PDF document to separate PNG images using a 300 DPI resolution. |
| [pdf-to-png-72dpi](./pdf-to-png-72dpi.cs) | Convert PDF Pages to PNG Images with 72 DPI using CropBox | `Document`, `Resolution`, `PngDevice` | Demonstrates converting each page of a PDF to separate PNG images at 72 DPI while preserving the ... |
| [pdf-to-png-cropbox](./pdf-to-png-cropbox.cs) | Convert PDF Pages to PNG Images Using CropBox | `Document`, `Resolution`, `PngDevice` | Demonstrates converting each page of a PDF to PNG images while using the CropBox coordinate type ... |
| [pdf-to-png-font-substitution](./pdf-to-png-font-substitution.cs) | Convert PDF to PNG Images with Font Substitution | `Document`, `FontRepository`, `PngDevice` | Converts each page of a PDF to PNG images while substituting missing Helvetica fonts with Times N... |
| [pdf-to-png-odd-pages](./pdf-to-png-odd-pages.cs) | Convert PDF to PNG Images (Odd Pages Only) | `Document`, `PngDevice`, `Resolution` | Extracts odd-numbered pages from a PDF and saves each as a separate PNG image. |
| [pdf-to-png-parallel](./pdf-to-png-parallel.cs) | Convert PDF Pages to PNG Images in Parallel | `Document`, `PngDevice`, `Resolution` | Demonstrates converting each page of a PDF to a PNG image using Aspose.Pdf with parallel processi... |
| [pdf-to-png-preview](./pdf-to-png-preview.cs) | Convert PDF Pages to PNG Images (72 DPI Preview) | `Document`, `PngDevice`, `Resolution` | Demonstrates how to convert each page of a PDF document to separate PNG images using a 72 DPI res... |
| [pdf-to-png-reverse](./pdf-to-png-reverse.cs) | Convert PDF to PNG Images in Reverse Page Order | `Document`, `PngDevice`, `Resolution` | Demonstrates converting each page of a PDF to separate PNG images, processing pages from the last... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-31 | Run: `20260331_170310_4f6364`
<!-- AUTOGENERATED:END -->

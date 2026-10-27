---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **69** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-images** category.
This folder contains standalone C# examples for working-with-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (69/69 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (12/69 files)
- `using Aspose.Pdf.Devices;` (9/69 files)
- `using Aspose.Pdf.Text;` (9/69 files)
- `using Aspose.Pdf.Annotations;` (6/69 files)
- `using Aspose.Pdf.Optimization;` (2/69 files)
- `using Aspose.Pdf.Facades;` (1/69 files)
- `using Aspose.Pdf.Operators;` (1/69 files)
- `using System;` (69/69 files)
- `using System.IO;` (69/69 files)
- `using System.Collections.Generic;` (10/69 files)
- `using System.Drawing.Imaging;` (4/69 files)
- `using System.Reflection;` (3/69 files)
- `using System.Text.Json;` (3/69 files)
- `using System.Drawing;` (2/69 files)
- `using System.Linq;` (1/69 files)
- `using System.Security.Cryptography;` (1/69 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-alternative-text-to-pdf-images](./add-alternative-text-to-pdf-images.cs) | Add Alternative Text to PDF Images | `Document`, `Page`, `XImage` | The example loads a PDF, iterates through each page and its image resources, assigns alternative ... |
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages | `Document`, `ImageStamp`, `Page` | Demonstrates how to overlay a semi‑transparent background image on every page of a PDF using Aspo... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, iterating its pages, and adding a background image using ImageStamp w... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent pattern image as a background on every page of a P... |
| [add-centered-logo-to-first-pdf-page](./add-centered-logo-to-first-pdf-page.cs) | Add Company Logo to First Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to place a centered image stamp (logo) on the first page of a PDF using Aspose.Pdf. |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Footer Image to Each PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to insert a decorative footer image on every page of a PDF with Aspose.Pdf, scaling the... |
| [add-diagonal-image-watermark-to-pdf__v2](./add-diagonal-image-watermark-to-pdf__v2.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image watermark on every page of a PDF and rotate it 45 degrees for diagona... |
| [add-dicom-image-to-pdf-page](./add-dicom-image-to-pdf-page.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Page`, `Image` | Demonstrates loading a DICOM medical image from a FileStream and inserting it into a PDF page wit... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Header Image to Each PDF Page | `Document`, `ImageStamp`, `PageCollection` | Shows how to place a decorative header image at the top of every page in a PDF using Aspose.Pdf, ... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alt Text to PDF | `Document`, `Page`, `ImageStamp` | Shows how to insert an image into a PDF using Aspose.Pdf and assign alternative text for accessib... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp from a PNG file, positioning i... |
| [add-raster-image-to-pdf-page](./add-raster-image-to-pdf-page.cs) | Add Raster Image to a New PDF Page | `Document`, `Page`, `Image` | Shows how to load a raster image from a file stream and insert it onto a newly added PDF page usi... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a PNG watermark with configurable opacity on each page of a PDF using... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to overlay a semi‑transparent PNG across all pages of a PDF using Aspose.Pdf's I... |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Theme‑Based Semi‑Transparent Overlay to PDF Pages | `Document`, `Page`, `Graph` | The example reads a JSON theme configuration to obtain RGBA values, then uses Aspose.Pdf to add a... |
| [add-subtle-background-pattern-to-pdf-pages](./add-subtle-background-pattern-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `ImageStamp` | This example shows how to overlay a semi‑transparent pattern image as a background on every page ... |
| [add-texture-background-image-to-pdf-pages](./add-texture-background-image-to-pdf-pages.cs) | Add Background Texture Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent background image on each page of a PDF using Aspos... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a transparent PNG image on every page of a PDF using Aspose.Pdf, ensu... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract PDF Pages to SVG Files | `Document`, `SvgSaveOptions`, `Pages` | Loads each PDF from an input folder, creates a dedicated sub‑folder for the file, and saves every... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Pages`, `Page` | Shows how to load a PDF, iterate through each page's image collection, and gather width, height, ... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF Using OptimizationOptions | `Document`, `OptimizationOptions`, `All` | Demonstrates how to reduce PDF size by compressing images (especially those larger than 2 MB) usi... |
| [compress-pdf-images-using-optimization](./compress-pdf-images-using-optimization.cs) | Compress Large Images in PDF | `Document`, `Page`, `XImage` | Demonstrates detecting images larger than 1 MB in a PDF and applying Aspose.Pdf optimization to r... |
| [convert-pdf-images-to-monochrome](./convert-pdf-images-to-monochrome.cs) | Convert PDF Images to Monochrome | `Document`, `Page`, `XImage` | Demonstrates how to iterate through a PDF, extract each embedded image, convert it to a grayscale... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font Arial | `Document`, `PngDevice`, `Resolution` | Shows how to load a PDF, set RenderingOptions.DefaultFontName to "Arial", and render each page as... |
| [convert-pdf-pages-to-png-with-default-font__v2](./convert-pdf-pages-to-png-with-default-font__v2.cs) | Render PDF Pages to PNG with Custom Font | `RenderingOptions`, `Resolution`, `PngDevice` | Shows how to set a default font for rendering and convert each page of a PDF into PNG images usin... |
| [convert-pdf-to-multi-page-tiff-default-font](./convert-pdf-to-multi-page-tiff-default-font.cs) | Convert PDF to Multi-Page TIFF with Default Font Substitutio... | `Document`, `Add`, `SimpleFontSubstitution` | Shows how to convert an entire PDF document to a multi-page TIFF file using Aspose.Pdf while appl... |
| [copy-vector-graphics-between-pdf-pages](./copy-vector-graphics-between-pdf-pages.cs) | Insert Extracted SVG Vector Graphic into PDF Page | `Document`, `SvgSaveOptions`, `Page` | Demonstrates extracting a vector graphic from a source PDF as SVG, loading it as an Image, and in... |
| [correct-pdf-image-orientation](./correct-pdf-image-orientation.cs) | Correct EXIF Orientation of Images in PDF | `Document`, `Page`, `XImage` | The example loads a PDF, scans each embedded image for EXIF orientation metadata, rotates/flips t... |
| [delete-raster-image-from-pdf-page](./delete-raster-image-from-pdf-page.cs) | Remove Specific Raster Image from PDF Page | `Document`, `Page`, `XImage` | Demonstrates how to delete a particular raster image from a PDF page by removing it from the page... |
| [export-pdf-pages-to-bmp-300-dpi](./export-pdf-pages-to-bmp-300-dpi.cs) | Export PDF Pages to BMP Images at 300 DPI | `Document`, `BmpDevice`, `Resolution` | Shows how to convert each page of a PDF into a high‑resolution BMP image (300 DPI) using Aspose.P... |
| ... | | | *and 39 more files* |

## Category Statistics
- Total examples: 69

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Devices.BmpDevice`
- `Aspose.Pdf.Devices.ColorDepth`
- `Aspose.Pdf.Devices.CompressionType`
- `Aspose.Pdf.Devices.EmfDevice`
- `Aspose.Pdf.Devices.PngDevice`
- `Aspose.Pdf.Devices.Resolution`
- `Aspose.Pdf.Devices.ShapeType`
- `Aspose.Pdf.Devices.TiffDevice`
- `Aspose.Pdf.Devices.TiffSettings`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfProducer`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.ImportFormat`
- `Aspose.Pdf.Optimization.ImageCompressionOptions`

### Rules
- Load a PDF document: Document {doc} = new Document("{input_pdf}");
- Iterate over pages using 1‑based index: for (int {page}=1; {page} <= {doc}.Pages.Count; {page}++) { ... }
- Create a Resolution object for desired DPI: Resolution {resolution} = new Resolution({int});
- Instantiate a PngDevice with the resolution: PngDevice pngDevice = new PngDevice({resolution});
- Render a page to an output stream: pngDevice.Process({doc}.Pages[{page}], {output_stream});

### Warnings
- PdfProducer resides in the Aspose.Pdf.Facades namespace and may be deprecated in newer library versions; ensure the correct version is referenced.
- A valid Aspose.PDF license is required for production use.
- Assumes Aspose.Pdf.Devices.EmfDevice and Resolution are the correct fully qualified types; if the library version changes the namespace may differ.
- The example manually closes the MemoryStream; using a 'using' statement is recommended to ensure proper disposal.
- The code reads the entire file into a byte array before creating the MemoryStream; for large images a direct stream copy may be more efficient.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-25 | Run: `20261025_232257_0da171`
<!-- AUTOGENERATED:END -->

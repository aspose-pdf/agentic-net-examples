---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **70** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-images** category.
This folder contains standalone C# examples for working-with-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (70/70 files) ← category-specific
- `using Aspose.Pdf.Devices;` (10/70 files)
- `using Aspose.Pdf.Facades;` (7/70 files)
- `using Aspose.Pdf.Drawing;` (4/70 files)
- `using Aspose.Pdf.Vector;` (3/70 files)
- `using Aspose.Pdf.Annotations;` (2/70 files)
- `using Aspose.Pdf.Operators;` (2/70 files)
- `using Aspose.Pdf.Optimization;` (2/70 files)
- `using Aspose.Pdf.Text;` (2/70 files)
- `using System;` (70/70 files)
- `using System.IO;` (70/70 files)
- `using System.Collections.Generic;` (5/70 files)
- `using System.Drawing.Imaging;` (3/70 files)
- `using System.Text.Json;` (3/70 files)
- `using System.Drawing;` (2/70 files)
- `using System.Globalization;` (1/70 files)
- `using System.Linq;` (1/70 files)
- `using System.Security.Cryptography;` (1/70 files)

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
| [add-background-image-opacity-to-pdf-pages](./add-background-image-opacity-to-pdf-pages.cs) | Add Background Image with Opacity to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent background image on each page of an existing PDF using Aspo... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF with Subtle Multiply Effect | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to load an existing PDF, add a full‑page background image using a BackgroundArti... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent pattern image as a background on every page of a PDF using ... |
| [add-background-pattern-to-pdf-pages](./add-background-pattern-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to apply a semi‑transparent background pattern image to each page of a PDF document usi... |
| [add-company-logo-to-first-page](./add-company-logo-to-first-page.cs) | Add Company Logo to First Page of PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp for a logo, center it on the first page, and save t... |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Decorative Footer Image to PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to insert a footer image on every page of a PDF and scale it proportionally to the page... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp from a PNG, rotate it 45° for diagonal placement, s... |
| [add-dicom-image-to-pdf-using-filestream](./add-dicom-image-to-pdf-using-filestream.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Image`, `Add` | Demonstrates loading a DICOM medical image from a FileStream and embedding it into a PDF page usi... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Header Image to PDF Pages | `Document`, `Page`, `GSave` | Shows how to place a decorative header image on every page of an existing PDF and shift the origi... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `Add` | Demonstrates embedding an image into a PDF document and assigning alternative (alt) text for acce... |
| [add-png-logo-to-first-pdf-page](./add-png-logo-to-first-pdf-page.cs) | Add PNG Logo to First PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF with Aspose.Pdf, inserting a PNG image at specific coordinates on the ... |
| [add-raster-image-to-new-pdf-page](./add-raster-image-to-new-pdf-page.cs) | Add Raster Image to New PDF Page | `Document`, `Page`, `Image` | Shows how to create a PDF document, add a blank page, load a raster image, and place it on the pa... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp, setting its opacity, and appl... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates applying a semi‑transparent image overlay to every page of a PDF using Aspose.Pdf. |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Semi-Transparent Overlay to PDF Pages | `Document`, `Page`, `Color` | Demonstrates reading a theme JSON file, converting a hex color with opacity to an Aspose.Pdf.Colo... |
| [add-texture-background-image-to-pdf-pages](./add-texture-background-image-to-pdf-pages.cs) | Add Texture Background Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a subtle texture image as a background on every PDF page and adjust its opacit... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, overlay a transparent PNG on each page using an ImageStamp, and e... |
| [add-vector-graphics-collection-to-new-pdf-page](./add-vector-graphics-collection-to-new-pdf-page.cs) | Add Vector Graphics Collection to New PDF Page Using AddRang... | `Document`, `GraphicsAbsorber`, `Graph` | The example extracts vector graphics from an existing PDF page with GraphicsAbsorber, creates pla... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs | `Document`, `Page`, `XImage` | Shows how to process multiple PDF files in a folder, iterate through each page, and save all embe... |
| [batch-extract-vector-graphics](./batch-extract-vector-graphics.cs) | Batch Extract Vector Graphics from PDFs | `Document`, `Page`, `Pages` | Shows how to iterate over PDF files in a directory, extract each page's vector graphics as SVG fi... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF to Reduce Size | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates using Aspose.Pdf's OptimizationOptions to recompress images larger than 2 MB into JP... |
| [compress-pdf-images](./compress-pdf-images.cs) | Compress PDF Images to Reduce File Size | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to use Aspose.Pdf's OptimizationOptions to compress images in a PDF, lowering the overa... |
| [convert-even-pdf-pages-to-grayscale](./convert-even-pdf-pages-to-grayscale.cs) | Convert Even PDF Pages to Grayscale | `Document`, `Page`, `Pages` | Shows how to open a PDF with Aspose.Pdf, iterate through its pages, and apply a grayscale convers... |
| [convert-pdf-images-to-grayscale](./convert-pdf-images-to-grayscale.cs) | Convert PDF Images to Grayscale for Uniform Look | `Document`, `Page`, `XImageCollection` | Loads a PDF, scans each page for images, converts any matching images to grayscale using System.D... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font Setting | `Document`, `PngDevice`, `Resolution` | Demonstrates how to load a PDF, set RenderingOptions.DefaultFontName to "Arial", and convert each... |
| [convert-pdf-pages-to-png-with-default-font__v2](./convert-pdf-pages-to-png-with-default-font__v2.cs) | Render PDF Pages to PNG with Default Font Substitution | `Document`, `PngDevice`, `Process` | Loads a PDF document and converts each page to a PNG image while setting RenderingOptions.Default... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Demonstrates loading a PDF, configuring resolution and TIFF settings, applying a default font for... |
| [correct-image-orientation-in-pdf](./correct-image-orientation-in-pdf.cs) | Correct Image EXIF Orientation in PDF | `Document`, `Page`, `XImage` | Demonstrates how to detect EXIF orientation metadata in images embedded in a PDF, rotate the imag... |
| [delete-raster-image-from-pdf-page](./delete-raster-image-from-pdf-page.cs) | Delete Raster Image from PDF Page | `Document`, `Page`, `ImageDeleteAction` | Shows how to remove a specific raster image from a PDF page by deleting it from the page's image ... |
| [export-pdf-pages-to-bmp-300-dpi](./export-pdf-pages-to-bmp-300-dpi.cs) | Export PDF Pages to BMP at 300 DPI | `Document`, `Resolution`, `BmpDevice` | Demonstrates how to convert each page of a PDF into a 300‑DPI BMP image using Aspose.Pdf with cus... |
| ... | | | *and 40 more files* |

## Category Statistics
- Total examples: 70

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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

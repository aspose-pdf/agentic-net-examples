---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **69** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Devices;` (9/69 files)
- `using Aspose.Pdf.Annotations;` (8/69 files)
- `using Aspose.Pdf.Text;` (8/69 files)
- `using Aspose.Pdf.Drawing;` (6/69 files)
- `using Aspose.Pdf.Operators;` (2/69 files)
- `using Aspose.Pdf.Optimization;` (2/69 files)
- `using Aspose.Pdf.Vector;` (2/69 files)
- `using Aspose.Pdf.Forms;` (1/69 files)
- `using System;` (69/69 files)
- `using System.IO;` (69/69 files)
- `using System.Collections.Generic;` (11/69 files)
- `using System.Drawing;` (7/69 files)
- `using System.Drawing.Imaging;` (5/69 files)
- `using System.Text.Json;` (3/69 files)
- `using System.Drawing.Drawing2D;` (2/69 files)
- `using System.Linq;` (2/69 files)
- `using System.Globalization;` (1/69 files)
- `using System.Net.Http;` (1/69 files)
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
| [add-alternative-text-to-pdf-images](./add-alternative-text-to-pdf-images.cs) | Add Alternative Text to PDF Images | `Document`, `Page`, `XImage` | Demonstrates how to load a PDF, iterate through its pages and image resources, and set alternativ... |
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Opacity | `Document`, `ImageStamp`, `Page` | Shows how to place a semi‑transparent background image on each page of a PDF using Aspose.Pdf's I... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF with Opacity | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp for a background image, set it behind page content ... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent pattern image as a background on each page of a PD... |
| [add-centered-logo-to-first-pdf-page](./add-centered-logo-to-first-pdf-page.cs) | Add Company Logo to First Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to place a centered image stamp (logo) on the first page of a PDF using Aspose.Pdf. |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Scalable Footer Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to insert a footer image on every page of a PDF and scale it proportionally to match th... |
| [add-diagonal-image-watermark-to-pdf__v2](./add-diagonal-image-watermark-to-pdf__v2.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp, rotate it 45°, center it on each page with semi‑tr... |
| [add-dicom-image-to-pdf-page](./add-dicom-image-to-pdf-page.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Page`, `Image` | Shows how to open a DICOM medical image via a FileStream and embed it into a PDF page using Aspos... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Decorative Header Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to insert a decorative header image on each page of a PDF using Aspose.Pdf while... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates inserting an image into a PDF using Aspose.Pdf and assigning the AlternativeText pro... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `ImageStamp`, `Page` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp from a PNG file, positioning i... |
| [add-raster-image-to-pdf-page](./add-raster-image-to-pdf-page.cs) | Add Raster Image to PDF Page | `Document`, `Page`, `Image` | Shows how to insert a raster image into a newly created PDF page using Aspose.Pdf's Image class a... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to apply a semi‑transparent image watermark to every page of a PDF using Aspose.Pdf, wi... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image across all pages of a PDF using Aspose.Pdf b... |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Semi-Transparent Overlay to PDF Pages | `Document`, `Page`, `SquareAnnotation` | Shows how to read a theme JSON file and apply a full‑page semi‑transparent color overlay to each ... |
| [add-subtle-background-pattern-to-pdf-pages](./add-subtle-background-pattern-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a low‑opacity pattern image as a background on every page of a PDF us... |
| [add-texture-background-image-to-pdf-pages](./add-texture-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Transparency | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, place a semi‑transparent background image on each page u... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, create an ImageStamp from a transparent PNG, and add it ... |
| [add-vector-graphics-to-pdf-page](./add-vector-graphics-to-pdf-page.cs) | Add Extracted Vector Graphics to a New PDF Page | `Document`, `Page`, `XForm` | Shows how to collect XForm vector graphics from an existing PDF and insert them into a newly crea... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract PDF Vector Graphics to SVG | `Document`, `Page`, `SvgExtractor` | Demonstrates how to process multiple PDF files, extract each page's vector graphics as SVG files,... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Page`, `XImage` | Shows how to load a PDF with Aspose.Pdf, iterate through each page's image resources, and retriev... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF Using OptimizationOptions | `Document`, `OptimizationOptions`, `All` | Shows how to reduce PDF size by compressing images larger than 2 MB with Aspose.Pdf's Optimizatio... |
| [compress-pdf-images-using-optimization](./compress-pdf-images-using-optimization.cs) | Compress Large Images in PDF | `Document`, `Page`, `XImage` | The example scans a PDF for images larger than 1 MB and applies Aspose.Pdf optimization to compre... |
| [convert-even-pdf-pages-to-grayscale](./convert-even-pdf-pages-to-grayscale.cs) | Replace Images on Even PDF Pages with Grayscale | `Document`, `Page`, `XImageCollection` | Loads a PDF, iterates over even‑numbered pages, converts each embedded image to a grayscale PNG, ... |
| [convert-pdf-images-to-monochrome](./convert-pdf-images-to-monochrome.cs) | Replace PDF Images with Monochrome Version | `Document`, `Page`, `XImage` | Shows how to iterate through each page of a PDF using Aspose.Pdf, remove existing images, and rep... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Arial Font Rendering | `Document`, `PngDevice`, `Resolution` | Demonstrates how to render each page of a PDF as a PNG image while forcing the default font to Ar... |
| [convert-pdf-pages-to-png-with-default-font__v2](./convert-pdf-pages-to-png-with-default-font__v2.cs) | Convert PDF Pages to PNG Images with Default Font | `Document`, `PngDevice`, `RenderingOptions` | Shows how to load a PDF, set RenderingOptions.DefaultFontName to "Times New Roman", and render ea... |
| [convert-pdf-to-multi-page-tiff-default-font](./convert-pdf-to-multi-page-tiff-default-font.cs) | Convert PDF to Multi‑Page TIFF with Default Font | `Document`, `FontRepository`, `SimpleFontSubstitution` | Demonstrates loading a PDF, substituting missing fonts with a default font, and converting the en... |
| [copy-vector-graphics-between-pdf-pages](./copy-vector-graphics-between-pdf-pages.cs) | Insert Extracted Vector Graphic Page into PDF | `Document`, `Page`, `Insert` | Demonstrates how to extract a page containing a vector graphic from one PDF and insert it into an... |
| [correct-pdf-image-orientation](./correct-pdf-image-orientation.cs) | Correct Image Orientation in PDF Using EXIF Data | `Document`, `Page`, `XImage` | The example loads a PDF, extracts each embedded image, checks its EXIF orientation tag, rotates o... |
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
Updated: 2026-11-17 | Run: `20261117_230320_191268`
<!-- AUTOGENERATED:END -->

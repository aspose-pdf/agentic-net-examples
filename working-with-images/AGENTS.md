---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **72** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-images** category.
This folder contains standalone C# examples for working-with-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (72/72 files) ← category-specific
- `using Aspose.Pdf.Devices;` (10/72 files)
- `using Aspose.Pdf.Text;` (6/72 files)
- `using Aspose.Pdf.Vector;` (6/72 files)
- `using Aspose.Pdf.Facades;` (5/72 files)
- `using Aspose.Pdf.Annotations;` (3/72 files)
- `using Aspose.Pdf.Drawing;` (2/72 files)
- `using Aspose.Pdf.Optimization;` (2/72 files)
- `using Aspose.Pdf.LogicalStructure;` (1/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (72/72 files)
- `using System.Collections.Generic;` (7/72 files)
- `using System.Drawing.Imaging;` (4/72 files)
- `using System.Drawing;` (3/72 files)
- `using System.Text.Json;` (3/72 files)
- `using System.Linq;` (1/72 files)

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
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Opacity | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a background image on every page of a PDF and set its opacity to 30% using Asp... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates loading an existing PDF, adding a background image to each page using a BackgroundAr... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to place a semi‑transparent pattern image as a background on every page of a PDF... |
| [add-background-pattern-image-to-pdf-pages__v2](./add-background-pattern-image-to-pdf-pages__v2.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent background pattern image on every page of a PDF using Aspos... |
| [add-background-texture-overlay-to-pdf-pages](./add-background-texture-overlay-to-pdf-pages.cs) | Add Background Texture Overlay to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to add a semi‑transparent background image to each page of a PDF using Aspose.Pd... |
| [add-company-logo-to-first-page](./add-company-logo-to-first-page.cs) | Add Company Logo to First Page of PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create a centered ImageStamp for a logo, add it only to the first page, ... |
| [add-decorative-header-image-to-pdf-pages](./add-decorative-header-image-to-pdf-pages.cs) | Add Decorative Header Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place a header image at the top of each page in a PDF using Aspose.Pdf, calculating ... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `RotateAngle` | Demonstrates how to overlay a semi‑transparent image watermark on every page of a PDF and rotate ... |
| [add-dicom-image-to-pdf-using-filestream](./add-dicom-image-to-pdf-using-filestream.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Image`, `ImageStream` | Shows how to load a DICOM file via a FileStream, create an Aspose.Pdf.Image, and embed it on a PD... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `Image` | Shows how to insert an image into a PDF using Aspose.Pdf and assign alternative (alt) text for ac... |
| [add-png-logo-to-first-pdf-page](./add-png-logo-to-first-pdf-page.cs) | Add PNG Logo to First PDF Page | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, place a PNG logo at specific coordinates on the first pa... |
| [add-raster-image-to-new-pdf-page](./add-raster-image-to-new-pdf-page.cs) | Add Raster Image to a New PDF Page | `Document`, `Image`, `Pages` | Shows how to place a raster image (PNG, JPEG, etc.) on a newly added PDF page using Aspose.Pdf's ... |
| [add-scalable-footer-image-to-pdf-pages](./add-scalable-footer-image-to-pdf-pages.cs) | Add Scalable Footer Image to PDF Pages | `Document`, `Page`, `Image` | Shows how to insert a decorative footer image on every page of a PDF and scale it proportionally ... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent PNG image as a watermark on every page of a PDF us... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply a semi‑transparent image overlay to every page of a PDF using Aspose.Pd... |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Semi-Transparent Color Overlay to PDF Pages | `Document`, `Page`, `Graph` | Shows how to load a theme configuration from a JSON file and apply a semi‑transparent color overl... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, iterating through its pages, and applying a transparent PNG overlay u... |
| [add-vector-graphics-collection-to-pdf-page](./add-vector-graphics-collection-to-pdf-page.cs) | Add Vector Graphics Collection to PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to create a collection of vector shapes (line, rectangle, ellipse) and add them ... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs | `Document`, `Page`, `XImage` | Shows how to process multiple PDF files in a folder, load each with Aspose.Pdf, and extract all e... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `SvgExtractor` | Demonstrates how to iterate through multiple PDF files, detect pages with vector graphics, and ex... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to reduce PDF size by compressing images (e.g., those over 1 MB) using Aspose.Pdf's opt... |
| [compress-large-images-in-pdf__v2](./compress-large-images-in-pdf__v2.cs) | Compress Large Images in PDF with JPEG Optimization | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates loading a PDF, configuring image compression options to replace images larger than 2... |
| [convert-pdf-images-to-monochrome](./convert-pdf-images-to-monochrome.cs) | Convert PDF Images to Monochrome | `Document`, `Page`, `ImageCollection` | Shows how to replace each image in a PDF with its grayscale version using Aspose.Pdf, resulting i... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG Images with Default Font Arial | `RenderingOptions`, `PngDevice`, `Document` | Demonstrates setting RenderingOptions.DefaultFontName to Arial and converting each page of a PDF ... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Demonstrates converting an entire PDF document to a multi‑page TIFF file using Aspose.Pdf, config... |
| [convert-pdf-to-png-with-default-font](./convert-pdf-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font Override | `Document`, `RenderingOptions`, `PngDevice` | Loads a PDF, sets RenderingOptions.DefaultFontName to "Times New Roman" to replace missing fonts,... |
| [copy-vector-graphics-between-pdf-pages](./copy-vector-graphics-between-pdf-pages.cs) | Copy Vector Graphics Between PDF Pages | `Document`, `Page`, `GraphicsAbsorber` | Shows how to extract vector graphics from a source PDF page using GraphicsAbsorber and insert eac... |
| [correct-exif-orientation-in-pdf](./correct-exif-orientation-in-pdf.cs) | Correct EXIF Orientation of Images in PDF | `Document`, `Page`, `XImage` | The example loads a PDF, detects EXIF orientation metadata in each embedded image, rotates/flips ... |
| [delete-low-resolution-images-from-pdf](./delete-low-resolution-images-from-pdf.cs) | Delete Low-Resolution Images from PDF | `Document`, `Page`, `ImagePlacementAbsorber` | Shows how to remove images with a DPI lower than 72 from a PDF using ImagePlacementAbsorber, hide... |
| ... | | | *and 42 more files* |

## Category Statistics
- Total examples: 72

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

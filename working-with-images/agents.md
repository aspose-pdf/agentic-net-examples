---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

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
- `using Aspose.Pdf.Devices;` (11/70 files)
- `using Aspose.Pdf.Vector;` (6/70 files)
- `using Aspose.Pdf.Annotations;` (4/70 files)
- `using Aspose.Pdf.Text;` (4/70 files)
- `using Aspose.Pdf.Drawing;` (3/70 files)
- `using Aspose.Pdf.Facades;` (2/70 files)
- `using Aspose.Pdf.Optimization;` (2/70 files)
- `using Aspose.Pdf.Tagged;` (2/70 files)
- `using Aspose.Pdf.LogicalStructure;` (1/70 files)
- `using System;` (70/70 files)
- `using System.IO;` (70/70 files)
- `using System.Drawing;` (4/70 files)
- `using System.Drawing.Imaging;` (4/70 files)
- `using System.Collections.Generic;` (3/70 files)
- `using System.Text.Json;` (3/70 files)
- `using System.Runtime.InteropServices;` (1/70 files)

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
| [add-background-image-multiply-blend-mode](./add-background-image-multiply-blend-mode.cs) | Add Background Image with Multiply Blend Mode to PDF | `Document`, `Save`, `BackgroundArtifact` | Shows how to load an existing PDF, attach a background image artifact to a page, optionally set i... |
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Opacity | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a background image on every page of a PDF and set its opacity to 30 % using As... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent pattern image as a background on every page of a PDF using ... |
| [add-background-pattern-to-pdf-pages](./add-background-pattern-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent background pattern image on every page of a PDF using Aspos... |
| [add-background-texture-to-pdf-pages](./add-background-texture-to-pdf-pages.cs) | Add Background Texture Image to PDF Pages | `Document`, `Page`, `Image` | Shows how to place a subtle texture image as a background on every page of an existing PDF and si... |
| [add-company-logo-to-first-page](./add-company-logo-to-first-page.cs) | Add Company Logo to First Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, create an ImageStamp for a logo, center it on the first ... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF with Aspose.Pdf, add an image stamp as a watermark on each page, r... |
| [add-dicom-image-to-pdf-using-filestream](./add-dicom-image-to-pdf-using-filestream.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Image`, `Add` | Shows how to embed a DICOM medical image into a PDF page by reading the image from a FileStream a... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Header Image to Each PDF Page | `Document`, `Page`, `PageInfo` | Shows how to load a PDF with Aspose.Pdf, calculate a top‑margin rectangle for each page, insert a... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `Image` | Demonstrates inserting an image into a PDF and assigning alternative text for accessibility using... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, place a PNG logo at specific coordinates on the first pa... |
| [add-raster-image-to-pdf-page](./add-raster-image-to-pdf-page.cs) | Add Raster Image to PDF Page | `Document`, `Image`, `Add` | Demonstrates how to insert a PNG/JPEG raster image onto a new PDF page using Aspose.Pdf's Image c... |
| [add-scalable-footer-image-to-pdf-pages](./add-scalable-footer-image-to-pdf-pages.cs) | Add Scalable Footer Image to PDF Pages | `Document`, `Page`, `Image` | Shows how to insert a decorative footer image on every page of a PDF and scale it proportionally ... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp, setting its opacity for a sem... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to place a semi‑transparent PNG overlay on every page of a PDF using Aspose.Pdf's Image... |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Semi-Transparent Color Overlay to PDF Pages | `Document`, `Page`, `Graph` | The example reads a theme configuration (hex color and opacity) from a JSON file and applies a se... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place a transparent PNG image over every page of a PDF as a foreground stamp using A... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs | `Document`, `Page`, `XImage` | Demonstrates how to iterate over PDF files in a folder, access each page's image resources with A... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract Vector Graphics from PDFs | `Document`, `Page`, `SvgExtractor` | Shows how to process multiple PDF files, detect pages containing vector graphics, and extract eac... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Page`, `ImagePlacementAbsorber` | Loads a PDF, iterates through each page, extracts image placements with ImagePlacementAbsorber, a... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF to Reduce File Size | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to replace images larger than 1 MB with compressed JPEG versions using Aspose.Pdf optim... |
| [compress-large-images-in-pdf__v2](./compress-large-images-in-pdf__v2.cs) | Compress Large Images in PDF to Reduce File Size | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates loading a PDF, configuring image compression options to downsize images, optimizing ... |
| [convert-even-pages-to-grayscale](./convert-even-pages-to-grayscale.cs) | Convert Even Pages to Grayscale in PDF | `Document`, `Page`, `MakeGrayscale` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, apply a grayscale conversion ... |
| [convert-pdf-pages-to-jpeg-with-default-font](./convert-pdf-pages-to-jpeg-with-default-font.cs) | Convert PDF Pages to JPEG with Default Font Override | `Document`, `JpegDevice`, `Resolution` | Demonstrates loading a PDF, setting the default font to Arial for rendering, and converting each ... |
| [convert-pdf-to-multi-page-tiff-default-font](./convert-pdf-to-multi-page-tiff-default-font.cs) | Convert PDF to Multi-Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Loads a PDF document and converts it into a multi‑page TIFF image using Aspose.Pdf, applying a sp... |
| [convert-pdf-to-png-with-default-font](./convert-pdf-to-png-with-default-font.cs) | Convert PDF Pages to PNG Images with Default Font Substituti... | `Document`, `Resolution`, `PngDevice` | Loads a PDF document, sets the rendering default font to Times New Roman to handle missing fonts,... |
| [correct-pdf-image-orientation](./correct-pdf-image-orientation.cs) | Correct PDF Image Orientation Using EXIF Data | `Document`, `Page`, `XImageCollection` | Loads a PDF, reads EXIF orientation from each embedded image, rotates the image when needed, and ... |
| [delete-raster-image-from-pdf-page](./delete-raster-image-from-pdf-page.cs) | Delete Raster Image from PDF Page | `Document`, `Page`, `ImageDeleteAction` | Demonstrates how to remove a specific raster image from a PDF page by deleting its reference from... |
| [export-pdf-pages-to-bmp-300-dpi](./export-pdf-pages-to-bmp-300-dpi.cs) | Export PDF Pages as BMP Images (300 DPI) | `Document`, `Resolution`, `BmpDevice` | Loads a PDF document, sets a 300 DPI resolution, and renders each page to a separate BMP file usi... |
| [extract-raster-images-from-pdf-preserve-format](./extract-raster-images-from-pdf-preserve-format.cs) | Extract Raster Images from PDF Preserving Original Format | `Document`, `Page`, `XImage` | The example loads a PDF with Aspose.Pdf, iterates through each page's image resources, determines... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

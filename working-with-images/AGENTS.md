---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **72** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Facades;` (6/72 files)
- `using Aspose.Pdf.Vector;` (6/72 files)
- `using Aspose.Pdf.Text;` (5/72 files)
- `using Aspose.Pdf.Annotations;` (2/72 files)
- `using Aspose.Pdf.Drawing;` (2/72 files)
- `using Aspose.Pdf.Optimization;` (1/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (71/72 files)
- `using System.Drawing;` (5/72 files)
- `using System.Drawing.Imaging;` (5/72 files)
- `using System.Collections.Generic;` (4/72 files)
- `using System.Text.Json;` (2/72 files)
- `using System.Security.Cryptography;` (1/72 files)

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
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Opacity | `Document`, `Page`, `ImageStamp` | Demonstrates how to place a semi‑transparent background image on every page of a PDF using Aspose... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF Pages | `Document`, `Page`, `Image` | Demonstrates loading an existing PDF, adding a semi‑transparent background image to each page usi... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to apply a semi‑transparent background pattern image to every page of an existing PDF u... |
| [add-background-pattern-image-to-pdf-pages__v2](./add-background-pattern-image-to-pdf-pages__v2.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to apply a semi‑transparent background pattern image to every page of a PDF usin... |
| [add-background-texture-overlay](./add-background-texture-overlay.cs) | Add Background Texture Image with Overlay Blend to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates adding a semi‑transparent background image to each PDF page and setting its blend mo... |
| [add-company-logo-to-first-page](./add-company-logo-to-first-page.cs) | Add Company Logo to First Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF, create an ImageStamp for a logo, center it on the first page, and save t... |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Decorative Footer Image to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to insert a footer image on each page of a PDF and scale it proportionally to the page ... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay an image watermark on every page of a PDF and rotate it 45 degrees fo... |
| [add-dicom-image-to-pdf-using-filestream](./add-dicom-image-to-pdf-using-filestream.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Image`, `Page` | Demonstrates loading a DICOM medical image via a FileStream and inserting it into a PDF page usin... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Header Image to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and place a decorative header... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, place a PNG logo at specific coordinates on the first pa... |
| [add-raster-image-to-pdf-page](./add-raster-image-to-pdf-page.cs) | Add Raster Image to PDF Page | `Document`, `Image`, `Page` | Demonstrates creating an in‑memory PNG raster image and inserting it onto a new PDF page using As... |
| [add-semi-transparent-color-overlay-to-pdf-pages](./add-semi-transparent-color-overlay-to-pdf-pages.cs) | Add Semi-Transparent Color Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates reading a theme configuration file and applying a semi‑transparent colored overlay t... |
| [add-semi-transparent-image-watermark-to-pdf](./add-semi-transparent-image-watermark-to-pdf.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with configurable opacity, apply it to each page, a... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to apply a semi‑transparent overlay image to every page of a PDF using Aspose.Pdf. |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a transparent PNG image on each page of a PDF and set its Z‑order so it... |
| [assign-alt-text-to-embedded-image](./assign-alt-text-to-embedded-image.cs) | Assign Alternative Text to an Embedded Image in PDF | `Document`, `Page`, `Image` | Demonstrates how to embed an image into a PDF using Aspose.Pdf and assign alternative text for ac... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDF Files | `Document`, `Page`, `XImage` | Shows how to iterate over PDF files in a folder, extract each embedded image from every page usin... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Pages`, `Page` | Shows how to process multiple PDF files, create per‑file and per‑page folders, and use Aspose.Pdf... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Page`, `ImagePlacementAbsorber` | The example loads a PDF, extracts all image placements on each page, and reports each image's vis... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to replace images exceeding 1 MB with compressed JPEG versions by configuring Aspose.Pd... |
| [compress-large-images-in-pdf__v2](./compress-large-images-in-pdf__v2.cs) | Compress Large Images in PDF | `Document`, `Page`, `XImage` | Demonstrates iterating through PDF pages, detecting images larger than 2 MB, and replacing them w... |
| [convert-even-pdf-pages-to-grayscale](./convert-even-pdf-pages-to-grayscale.cs) | Convert Even PDF Pages to Grayscale | `Document`, `Page`, `MakeGrayscale` | Shows how to load a PDF, turn only the even-numbered pages into grayscale, and save the modified ... |
| [convert-pdf-images-to-monochrome](./convert-pdf-images-to-monochrome.cs) | Convert PDF Images to Monochrome | `Document`, `Page`, `XImage` | Shows how to replace each image in a PDF with a grayscale version using Aspose.Pdf, resulting in ... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font Arial | `Document`, `Resolution`, `PngDevice` | Shows how to render each page of a PDF to PNG images using Aspose.Pdf while setting RenderingOpti... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Demonstrates converting an entire PDF document to a multi‑page TIFF file using Aspose.Pdf while s... |
| [convert-pdf-to-png-with-default-font](./convert-pdf-to-png-with-default-font.cs) | Convert PDF Pages to PNG Images with Custom Default Font | `Document`, `RenderingOptions`, `Resolution` | The example loads a PDF document, sets RenderingOptions.DefaultFontName to "Times New Roman" to h... |
| [copy-first-page-to-new-pdf](./copy-first-page-to-new-pdf.cs) | Copy First Page to a New PDF Document | `Document`, `PageCollection`, `Add` | Shows how to load an existing PDF, copy its first page into a new document using the Add method, ... |
| [correct-exif-orientation-in-pdf](./correct-exif-orientation-in-pdf.cs) | Correct EXIF Orientation of Images in PDF | `Document`, `Page`, `XImage` | Shows how to read EXIF orientation from images embedded in a PDF, rotate them accordingly, and re... |
| [delete-raster-image-reference-from-pdf-page](./delete-raster-image-reference-from-pdf-page.cs) | Delete Raster Image Reference from PDF Page | `Document`, `Page`, `Images` | Shows how to remove a specific raster image reference from a PDF page using Aspose.Pdf while keep... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

> **Working with images** in PDF using C# / .NET -- **72** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Drawing;` (4/72 files)
- `using Aspose.Pdf.Annotations;` (3/72 files)
- `using Aspose.Pdf.Optimization;` (2/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (72/72 files)
- `using System.Collections.Generic;` (5/72 files)
- `using System.Text.Json;` (3/72 files)
- `using System.Drawing.Imaging;` (2/72 files)
- `using System.Drawing;` (1/72 files)
- `using System.Linq;` (1/72 files)
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
| [add-alternative-text-to-pdf-images](./add-alternative-text-to-pdf-images.cs) | Add Alternative Text to PDF Images | `Document`, `Page`, `XImage` | Demonstrates how to load a PDF, iterate through its pages and images, set alternative text for ea... |
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to PDF Pages with Opacity | `Document`, `ImageStamp`, `AddStamp` | Shows how to place a semi‑transparent background image on every page of a PDF using Aspose.Pdf's ... |
| [add-background-image-to-pdf](./add-background-image-to-pdf.cs) | Add Background Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a background image on each page of an existing PDF using Aspose.Pdf, configuri... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a semi‑transparent background pattern image on each page of a PDF document usi... |
| [add-centered-logo-to-first-pdf-page](./add-centered-logo-to-first-pdf-page.cs) | Add Centered Logo to First PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF, create an ImageStamp for a logo, center it on the first page, and save t... |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Decorative Footer Image to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to insert a footer image on every page of a PDF, scaling it proportionally to the page ... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with a 45° rotation and semi‑transparent opacity, a... |
| [add-dicom-image-to-pdf-page](./add-dicom-image-to-pdf-page.cs) | Add DICOM Image to PDF Page using FileStream | `Document`, `Page`, `Image` | Shows how to load an existing PDF, read a DICOM image via a FileStream, and place the image on a ... |
| [add-header-image-to-pdf-pages](./add-header-image-to-pdf-pages.cs) | Add Header Image to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to place a decorative header image at the top of every page in a PDF using Aspose.Pdf, ... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert an image into an existing PDF document and assign alternative (alt) text for ... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, place a PNG logo at specific coordinates on the first pa... |
| [add-raster-image-to-pdf-page](./add-raster-image-to-pdf-page.cs) | Add Raster Image to PDF Page | `Document`, `Image`, `Add` | Demonstrates how to insert a raster image (PNG, JPEG, etc.) onto a newly created PDF page using A... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi-transparent PNG watermark on every page of a PDF using Aspose.... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to apply a semi‑transparent PNG overlay to every page of a PDF using Aspose.Pdf,... |
| [add-semi-transparent-overlay-to-pdf-pages](./add-semi-transparent-overlay-to-pdf-pages.cs) | Add Semi-Transparent Overlay to PDF Pages from Theme Config | `Document`, `Page`, `Graph` | Shows how to read a JSON theme file, parse a hex color with opacity, and draw a full‑page semi‑tr... |
| [add-subtle-background-pattern-to-pdf-pages](./add-subtle-background-pattern-to-pdf-pages.cs) | Add Subtle Background Pattern to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to apply a low‑opacity pattern image as a background artifact on every page of a... |
| [add-texture-background-image-to-pdf-pages](./add-texture-background-image-to-pdf-pages.cs) | Add Texture Background Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to place a subtle texture image as a background on every page of a PDF using Aspose.Pdf... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, iterate through each page, and place a full‑page transparent PNG overlay... |
| [add-vector-graphics-to-pdf-page](./add-vector-graphics-to-pdf-page.cs) | Add Vector Graphics to a PDF Page | `Document`, `Page`, `Graph` | Shows how to create a new PDF page, build a Graph with rectangle, ellipse, and line shapes, and a... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract Vector Graphics from PDFs | `Document`, `Page`, `SvgExtractor` | Loads all PDF files from a specified input folder, iterates through each page, and extracts any v... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Page`, `ImagePlacementAbsorber` | Loads a PDF, iterates through each page, finds all images using ImagePlacementAbsorber, and repor... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to load a PDF, configure image compression to replace images larger than 2 MB with comp... |
| [compress-pdf-images-using-optimization](./compress-pdf-images-using-optimization.cs) | Compress Images in PDF Using Aspose.Pdf Optimization | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | The example loads a PDF, configures image compression options (quality and resolution), applies r... |
| [convert-even-pdf-pages-to-grayscale](./convert-even-pdf-pages-to-grayscale.cs) | Convert Even PDF Pages to Grayscale | `Document`, `PageCollection`, `Page` | Shows how to load a PDF, iterate through its pages, apply a grayscale conversion only to even-num... |
| [convert-pdf-images-to-monochrome](./convert-pdf-images-to-monochrome.cs) | Convert PDF Images to Monochrome | `Document`, `Page`, `XImageCollection` | Shows how to open a PDF, iterate through each page's images, and replace them with a black‑and‑wh... |
| [convert-pdf-pages-to-png-with-default-font](./convert-pdf-pages-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font | `Document`, `Page`, `RenderingOptions` | Shows how to load a PDF, set RenderingOptions.DefaultFontName to Arial, and convert each page to ... |
| [convert-pdf-pages-to-png-with-default-font__v2](./convert-pdf-pages-to-png-with-default-font__v2.cs) | Convert PDF Pages to PNG with Default Font Override | `Document`, `Resolution`, `PngDevice` | Demonstrates how to set RenderingOptions.DefaultFontName to "Times New Roman" and convert each pa... |
| [convert-pdf-to-multi-page-tiff-default-font](./convert-pdf-to-multi-page-tiff-default-font.cs) | Convert PDF to Multi-Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Demonstrates converting an entire PDF document into a multi‑page TIFF file using Aspose.Pdf, sett... |
| [copy-vector-graphics-between-pdf-pages](./copy-vector-graphics-between-pdf-pages.cs) | Copy Vector Graphics Between PDF Pages | `Document`, `Page`, `GraphicsAbsorber` | Demonstrates how to extract vector graphic objects from a source PDF page using GraphicsAbsorber ... |
| [correct-pdf-image-orientation](./correct-pdf-image-orientation.cs) | Correct PDF Image Orientation Using EXIF Data | `Document`, `Page`, `XImageCollection` | Loads a PDF, reads each embedded image's EXIF orientation, rotates or flips the image accordingly... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

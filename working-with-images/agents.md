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

- `using Aspose.Pdf;` (72/72 files) ← category-specific
- `using Aspose.Pdf.Devices;` (11/72 files)
- `using Aspose.Pdf.Vector;` (6/72 files)
- `using Aspose.Pdf.Drawing;` (4/72 files)
- `using Aspose.Pdf.Text;` (3/72 files)
- `using Aspose.Pdf.Annotations;` (2/72 files)
- `using Aspose.Pdf.Facades;` (2/72 files)
- `using Aspose.Pdf.Optimization;` (2/72 files)
- `using Aspose.Pdf.Tagged;` (2/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (71/72 files)
- `using System.Drawing;` (7/72 files)
- `using System.Collections.Generic;` (6/72 files)
- `using System.Drawing.Imaging;` (5/72 files)
- `using System.Text.Json;` (3/72 files)
- `using System.Linq;` (2/72 files)
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
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to All PDF Pages with Opacity | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF, place a semi‑transparent background image on every page, and save the mo... |
| [add-background-image-to-pdf-pages__v2](./add-background-image-to-pdf-pages__v2.cs) | Add Background Image to PDF Pages | `Document`, `Save`, `Page` | Shows how to embed a background image on every page of a PDF with Aspose.Pdf and adjust its opaci... |
| [add-background-pattern-image-to-pdf-pages](./add-background-pattern-image-to-pdf-pages.cs) | Add Background Pattern Image to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to overlay a semi‑transparent pattern image as a background on every page of an ... |
| [add-background-pattern-image-to-pdf-pages__v2](./add-background-pattern-image-to-pdf-pages__v2.cs) | Add Background Pattern Image with Low Opacity to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to overlay a semi‑transparent pattern image as a background on every page of a P... |
| [add-background-texture-to-pdf-pages](./add-background-texture-to-pdf-pages.cs) | Add Background Texture Image to PDF Pages | `Document`, `Page`, `Image` | Shows how to place a texture image as the background of every page in a PDF using Aspose.Pdf's Ba... |
| [add-centered-company-logo-to-first-pdf-page](./add-centered-company-logo-to-first-pdf-page.cs) | Add Centered Company Logo to First PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF with Aspose.Pdf, placing a logo image at the center of the first page ... |
| [add-decorative-footer-image-to-pdf-pages](./add-decorative-footer-image-to-pdf-pages.cs) | Add Decorative Footer Image to Each PDF Page | `Document`, `Save`, `Page` | Shows how to load a PDF with Aspose.Pdf, loop through all pages, and add a footer image that scal... |
| [add-decorative-header-image-to-pdf-pages](./add-decorative-header-image-to-pdf-pages.cs) | Add Decorative Header Image to Each PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a header image at the top of every page in a PDF using Aspose.Pdf, pos... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent image watermark rotated 45° on each page of a PDF ... |
| [add-dicom-image-to-pdf-using-filestream](./add-dicom-image-to-pdf-using-filestream.cs) | Add DICOM Image to PDF Using FileStream | `Document`, `Image`, `Save` | Demonstrates loading a DICOM medical image from a FileStream and inserting it into a PDF page wit... |
| [add-extracted-vector-graphics-to-new-pdf-page](./add-extracted-vector-graphics-to-new-pdf-page.cs) | Add Extracted Vector Graphics to a New PDF Page | `Document`, `Save`, `Page` | Demonstrates extracting vector graphics from an existing PDF page using GraphicsAbsorber and addi... |
| [add-image-with-alt-text-to-pdf](./add-image-with-alt-text-to-pdf.cs) | Add Image with Alternative Text to PDF | `Document`, `Page`, `Image` | Demonstrates inserting an image onto a PDF page and assigning alternative text for accessibility ... |
| [add-png-logo-to-first-page](./add-png-logo-to-first-page.cs) | Add PNG Logo to First Page of PDF | `Document`, `Rectangle`, `AddImage` | Shows how to load a PDF, ensure source files exist, and insert a PNG logo onto the first page at ... |
| [add-raster-image-to-new-pdf-page](./add-raster-image-to-new-pdf-page.cs) | Add Raster Image to a New PDF Page | `Document`, `Image`, `Save` | Demonstrates inserting a raster image onto a newly added PDF page using Aspose.Pdf's Image class ... |
| [add-semi-transparent-image-watermark-to-pdf-pages](./add-semi-transparent-image-watermark-to-pdf-pages.cs) | Add Semi-Transparent Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a PNG watermark image on each page of a PDF with configurable opacity... |
| [add-semi-transparent-overlay-image-to-pdf](./add-semi-transparent-overlay-image-to-pdf.cs) | Add Semi-Transparent Overlay Image to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to apply a semi‑transparent PNG overlay to every page of a PDF using Aspose.Pdf,... |
| [add-theme-based-overlay-to-pdf-pages](./add-theme-based-overlay-to-pdf-pages.cs) | Add Theme‑Based Overlay to PDF Pages | `Document`, `Page`, `Color` | Demonstrates loading a JSON theme configuration, parsing default and per‑page colors, and applyin... |
| [add-transparent-png-overlay-to-pdf-pages](./add-transparent-png-overlay-to-pdf-pages.cs) | Add Transparent PNG Overlay to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a transparent PNG image on every page of a PDF using Aspose.Pdf, ensu... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs using Aspose.Pdf | `Document`, `Page`, `XImage` | Demonstrates how to iterate through PDF files in a folder, extract each embedded image from every... |
| [batch-extract-vector-graphics-from-pdfs](./batch-extract-vector-graphics-from-pdfs.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `HasVectorGraphics`, `TrySaveVectorGraphics` | Demonstrates how to iterate over multiple PDF files, detect pages with vector graphics, and save ... |
| [collect-image-resolution-metadata](./collect-image-resolution-metadata.cs) | Collect Image Resolution Metadata from PDF Pages | `Document`, `Accept`, `ImagePlacementAbsorber` | The example opens a PDF, iterates through each page, extracts all images using ImagePlacementAbso... |
| [compress-large-images-in-pdf](./compress-large-images-in-pdf.cs) | Compress Large Images in PDF to Reduce File Size | `Document`, `OptimizationOptions`, `OptimizeResources` | Shows how to use Aspose.Pdf's optimization features to compress images (e.g., those over 1 MB) by... |
| [compress-pdf-images](./compress-pdf-images.cs) | Compress Images in PDF to Reduce File Size | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates how to use Aspose.Pdf to compress images within a PDF by applying JPEG compression a... |
| [convert-even-pdf-pages-to-grayscale](./convert-even-pdf-pages-to-grayscale.cs) | Convert Even PDF Pages to Grayscale | `Document`, `MakeGrayscale`, `Save` | Loads a PDF, converts each even‑numbered page to grayscale using Aspose.Pdf, and saves the modifi... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF with Default Font | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to convert an entire PDF document into a multi‑page TIFF file using Aspose.Pdf, ... |
| [convert-pdf-to-png-using-arial-font](./convert-pdf-to-png-using-arial-font.cs) | Render PDF Pages to PNG Using Arial as Default Font | `Document`, `PngDevice`, `Resolution` | The example loads a PDF document and converts each page to a PNG image, configuring the rendering... |
| [convert-pdf-to-png-with-default-font](./convert-pdf-to-png-with-default-font.cs) | Convert PDF Pages to PNG with Default Font | `Document`, `PngDevice`, `Resolution` | Loads a PDF, sets RenderingOptions.DefaultFontName to "Times New Roman", and converts each page t... |
| [copy-vector-graphics-between-pdf-pages](./copy-vector-graphics-between-pdf-pages.cs) | Copy Vector Graphics Between PDF Pages | `Document`, `Page`, `GraphicsAbsorber` | Shows how to extract vector graphic elements from a source PDF page using GraphicsAbsorber and in... |
| [correct-exif-orientation-in-pdf](./correct-exif-orientation-in-pdf.cs) | Correct EXIF Orientation of Images in PDF | `Document`, `Save`, `Page` | Iterates through all images in a PDF, detects EXIF orientation metadata, rotates or flips the ima... |
| [delete-specific-raster-image-from-pdf-page](./delete-specific-raster-image-from-pdf-page.cs) | Delete Specific Raster Image from PDF Page | `Document`, `ImageDeleteAction`, `Delete` | Shows how to remove a raster image from a PDF page by deleting it from the page's image resources... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

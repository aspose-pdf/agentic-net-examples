---
name: facades-images
description: C# examples for facades-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-images

> **Facades images** in PDF using C# / .NET -- **37** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-images** category.
This folder contains standalone C# examples for facades-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/37 files) ← category-specific
- `using Aspose.Pdf;` (18/37 files)
- `using Aspose.Pdf.Devices;` (5/37 files)
- `using Aspose.Pdf.Text;` (1/37 files)
- `using System;` (37/37 files)
- `using System.IO;` (37/37 files)
- `using System.Drawing;` (4/37 files)
- `using System.Drawing.Imaging;` (3/37 files)
- `using System.Collections.Generic;` (1/37 files)
- `using System.Linq;` (1/37 files)
- `using System.Text;` (1/37 files)

## Common Code Pattern

Most files in this category use `PdfFileMend` from `Aspose.Pdf.Facades`:

```csharp
PdfFileMend tool = new PdfFileMend();
tool.BindPdf("input.pdf");
// ... PdfFileMend operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-and-replace-image-in-pdf](./add-and-replace-image-in-pdf.cs) | Add and Replace Image in PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind a PDF, add an image at specific page coordinates, and replace it by adding anot... |
| [add-bmp-image-to-pdf-page](./add-bmp-image-to-pdf-page.cs) | Add BMP Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF, insert a BMP image onto page 16 at coordinates (50,50) ... |
| [add-gif-image-to-pdf-page](./add-gif-image-to-pdf-page.cs) | Add GIF Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF, inserting a GIF image onto page 17 at given coordinates, an... |
| [add-gif-image-to-pdf-page__v2](./add-gif-image-to-pdf-page__v2.cs) | Add GIF Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF, insert a GIF image onto page 44 at coordinates (180,180) with ... |
| [add-image-and-caption-to-pdf-page](./add-image-and-caption-to-pdf-page.cs) | Add Image and Caption to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF, insert an image on page 11 at given coordinates, add formatted... |
| [add-image-and-text-overlay-to-pdf-page](./add-image-and-text-overlay-to-pdf-page.cs) | Add Image and Text Overlay to PDF Page with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF, place an image on page 33 at (250,250) and add formatted text ... |
| [add-image-to-pdf-page-using-pdffilemend](./add-image-to-pdf-page-using-pdffilemend.cs) | Add Image to Specific PDF Page Using PdfFileMend | `Document`, `PdfFileMend`, `BindPdf` | Demonstrates loading a PDF, binding it to PdfFileMend, adding an image to page 2 at coordinates (... |
| [add-image-to-pdf-page-using-pdffilemend__v2](./add-image-to-pdf-page-using-pdffilemend__v2.cs) | Add Image to Specific PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates loading a PDF, inserting an image onto page six with PdfFileMend while specifying a ... |
| [add-image-to-specific-page-using-pdffilemend](./add-image-to-specific-page-using-pdffilemend.cs) | Add Image to Specific Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF, insert an image into a defined rectangle on page 27, an... |
| [add-image-to-specific-pdf-page](./add-image-to-specific-pdf-page.cs) | Add Image to Specific PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend, inserting an image into a defined rectangl... |
| [add-jpg-image-to-pdf-page](./add-jpg-image-to-pdf-page.cs) | Add JPG Image to Specific PDF Page Using PdfFileMend | `PdfFileMend`, `Document`, `BindPdf` | Shows how to bind an existing PDF with PdfFileMend, insert a JPG image onto page two at given coo... |
| [add-jpg-image-to-pdf-page__v2](./add-jpg-image-to-pdf-page__v2.cs) | Add JPG Image to Specific PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, insert a JPG image onto page 19 at spe... |
| [add-jpg-image-to-pdf-page__v3](./add-jpg-image-to-pdf-page__v3.cs) | Add JPG Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF, insert a JPG image onto page 25 at coordinates (250,250... |
| [add-multiple-images-to-pdf-pages](./add-multiple-images-to-pdf-pages.cs) | Add Multiple Images to Specific PDF Pages using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF, inserting different images onto separate pages at specified... |
| [add-png-image-to-first-page-pdf](./add-png-image-to-first-page-pdf.cs) | Add PNG Image to First Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF, inserting a PNG image from a stream onto page 1 within a sp... |
| [add-png-image-to-pdf-page__v2](./add-png-image-to-pdf-page__v2.cs) | Add PNG Image to a Specific PDF Page with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF, insert a PNG image onto page 18 at given coordinates, a... |
| [add-png-image-to-specific-pdf-page](./add-png-image-to-specific-pdf-page.cs) | Add PNG Image to Specific PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF, inserting a PNG image from a memory stream onto page 24 at ... |
| [add-scaled-image-to-pdf-page](./add-scaled-image-to-pdf-page.cs) | Add Scaled Image to Specific PDF Page | `Document`, `PdfFileMend`, `BindPdf` | Demonstrates loading a PDF, inserting an image onto page four with custom width and height using ... |
| [bind-pdf-convert-first-page-to-png](./bind-pdf-convert-first-page-to-png.cs) | Bind PDF and Convert First Page to PNG | `PdfFileMend`, `BindPdf`, `Save` | Shows how to linearize a PDF using PdfFileMend and then render its first page as a PNG image with... |
| [convert-pdf-page-41-to-bmp](./convert-pdf-page-41-to-bmp.cs) | Convert PDF Page 41 to BMP using PdfConverter | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to bind a PDF with Aspose.Pdf.Facades.PdfConverter, render page 41 to a TIFF at 150 DPI... |
| [convert-pdf-pages-to-png__v12](./convert-pdf-pages-to-png__v12.cs) | Convert Specific PDF Pages to PNG Images | `Document`, `PngDevice`, `Resolution` | Demonstrates loading a PDF with Aspose.Pdf, extracting pages 42 and 43, and saving each as a sepa... |
| [convert-pdf-to-multi-page-tiff__v7](./convert-pdf-to-multi-page-tiff__v7.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to bind a PDF document, configure resolution and TIFF settings, and render the e... |
| [extract-image-add-to-pdf-page](./extract-image-add-to-pdf-page.cs) | Extract Image and Add to Specific PDF Page | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract the first image from a PDF using PdfExtractor and then insert that im... |
| [extract-images-from-pdf__v16](./extract-images-from-pdf__v16.cs) | Extract Images from PDF using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to bind a PDF with Aspose.Pdf.Facades.PdfExtractor, extract all embedded images, and sa... |
| [extract-images-pdf-csv-manifest__v2](./extract-images-pdf-csv-manifest__v2.cs) | Extract PDF Images and Generate CSV Manifest with PdfFileMen... | `PdfFileMend`, `BindPdf`, `Document` | The example binds a PDF using PdfFileMend, iterates through each page to extract embedded images,... |
| [insert-image-into-pdf-using-pdffilemend](./insert-image-into-pdf-using-pdffilemend.cs) | Insert Image into PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF, insert an image onto a specific page using PdfFileMend,... |
| [insert-image-into-pdf-using-pdffilemend__v2](./insert-image-into-pdf-using-pdffilemend__v2.cs) | Insert Image into PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates loading a PDF, inserting an image at specific coordinates with PdfFileMend, and savi... |
| [insert-multiple-images-into-pdf](./insert-multiple-images-into-pdf.cs) | Insert Multiple Images into PDF Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF, add images to specific pages at defined coordinates, and save ... |
| [overlay-gif-on-png-background-pdf](./overlay-gif-on-png-background-pdf.cs) | Overlay GIF on PNG Background in PDF using PdfFileMend | `PdfFileMend`, `CompositingParameters`, `BlendMode` | Demonstrates how to bind an existing PDF, add a PNG as a background on page three, and overlay a ... |
| [overlay-png-on-pdf-page](./overlay-png-on-pdf-page.cs) | Overlay PNG on Specific PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend, creating compositing parameters, and overl... |
| ... | | | *and 7 more files* |

## Category Statistics
- Total examples: 37

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.CgmImportOptions`
- `Aspose.Pdf.Facades.ExtractImageMode`
- `Aspose.Pdf.Facades.ImportFormat`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfContentEditor.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfContentEditor.DeleteImage()`
- `Aspose.Pdf.Facades.PdfContentEditor.DeleteImage(int, int[])`
- `Aspose.Pdf.Facades.PdfContentEditor.Save(string)`
- `Aspose.Pdf.Facades.PdfConverter`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfFileMend`
- `Aspose.Pdf.Facades.PdfPageEditor`
- `Aspose.Pdf.Facades.PdfProducer`

### Rules
- Bind a PDF file to a PdfConverter with BindPdf({input_pdf}) before any conversion.
- Invoke DoConvert() on the PdfConverter to initialize the conversion process.
- Export the bound PDF to a TIFF image using SaveAsTIFF({output_tiff}) after DoConvert() has been called.
- Always release resources by calling Close() on the PdfConverter when finished.
- Bind the PDF document to a PdfExtractor instance using BindPdf({input_pdf}) before any extraction operation.

### Warnings
- PdfConverter belongs to the Aspose.Pdf.Facades namespace, which may be considered legacy in newer SDK versions; verify compatibility.
- The example uses System.Drawing.Imaging.ImageFormat for the output format, which may require additional NuGet packages (e.g., System.Drawing.Common) on non‑Windows platforms.
- GetNextImage overwrites files if the generated {output_image_path} collides; ensure unique filenames.
- GetNextImage writes the image data to the provided stream; the stream should be positioned appropriately before further use.
- The example writes images to files using DateTime.Now.Ticks for naming, which may cause naming collisions in rapid successive runs.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-28 | Run: `20261128_020000_8f409b`
<!-- AUTOGENERATED:END -->

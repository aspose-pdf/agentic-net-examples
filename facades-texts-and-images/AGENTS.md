---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **28** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (28/28 files) ← category-specific
- `using Aspose.Pdf;` (14/28 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (27/28 files)
- `using System.Drawing.Imaging;` (3/28 files)
- `using System.Drawing;` (2/28 files)
- `using System.Linq;` (2/28 files)
- `using Microsoft.VisualStudio.TestTools.UnitTesting;` (1/28 files)
- `using NUnit.Framework;` (1/28 files)
- `using System.Threading;` (1/28 files)
- `using System.Threading.Tasks;` (1/28 files)

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
| [add-background-png-to-all-pdf-pages](./add-background-png-to-all-pdf-pages.cs) | Add Background PNG to All PDF Pages | `Document`, `Rect`, `BindPdf` | Shows how to load a PDF, retrieve each page's dimensions, and overlay a PNG image as a background... |
| [add-header-image-to-pdf-documents](./add-header-image-to-pdf-documents.cs) | Add Header Image to PDF Documents | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to insert a header image into one or more PDF files using Aspose.Pdf.Facades.PdfFileSta... |
| [add-image-and-text-to-pdf-with-audit-logging](./add-image-and-text-to-pdf-with-audit-logging.cs) | Add Image and Text to PDF with Audit Logging | `Document`, `PdfFileMend`, `AddImage` | Loads a PDF, adds an image and a formatted text watermark to page 1, logs each AddImage and AddTe... |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Semi‑Transparent Text Watermark to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to combine an image and semi‑transparent text into a single stamp and apply it a... |
| [add-image-to-each-pdf-page-dynamic-position](./add-image-to-each-pdf-page-dynamic-position.cs) | Add Image to Each PDF Page with Dynamic Positioning | `Document`, `Page`, `PdfFileMend` | Loads a PDF, calculates each page's dimensions, and places a PNG image in the bottom‑right corner... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to use Aspose.Pdf.Facades to insert an image onto a chosen page of a PDF at given recta... |
| [add-image-to-pdf-using-pdffilemend](./add-image-to-pdf-using-pdffilemend.cs) | Add Image to PDF Using PdfFileMend with Try‑Finally | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind a PDF, insert an image on a page, save the result, and guarantee that Pd... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image to PDF with Format Validation | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to validate an image's file format and then add the image to a PDF page using As... |
| [add-image-verify-pdf-byte-size](./add-image-verify-pdf-byte-size.cs) | Add Image to PDF and Verify Byte Size Increase | `Document`, `PdfFileMend`, `BindPdf` | Creates a minimal PDF, binds it with PdfFileMend, adds a PNG image, saves the modified document, ... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Demonstrates how to insert multiple images into a PDF using Aspose.Pdf's PdfFileMend facade while... |
| [add-multi-line-text-block-page-3](./add-multi-line-text-block-page-3.cs) | Add Multi‑Line Text Block with Custom Line Spacing to Page 3 | `PdfFileMend`, `Document`, `Page` | Demonstrates how to use the PdfFileMend facade to bind an existing PDF, create a multi‑line TextP... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, insert a PNG image onto page two at de... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Shows how to insert the same promotional message on pages 3, 5, and 7 of a PDF using Aspose.Pdf's... |
| [add-text-at-coordinates-verify-position](./add-text-at-coordinates-verify-position.cs) | Add Text at Specific Coordinates and Verify Position | `Document`, `Page`, `TextFragment` | Creates a blank PDF, adds a text fragment at defined X/Y coordinates using TextBuilder, saves and... |
| [add-tiff-image-to-last-page-pdf](./add-tiff-image-to-last-page-pdf.cs) | Add TIFF Image to Last Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileMend to embed a TIFF image onto the final page ... |
| [add-wrapped-footer-to-pdf-pages](./add-wrapped-footer-to-pdf-pages.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `Document`, `PageInfo`, `FormattedText` | Shows how to use Aspose.Pdf Facade API to add a formatted footer that wraps word‑by‑word across t... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Image to PDF Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to modify a PDF on a background thread by adding an image to the first page with... |
| [batch-add-company-logo-to-pdfs](./batch-add-company-logo-to-pdfs.cs) | Batch Add Company Logo to PDFs | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to process all PDF files in a folder and overlay a PNG company logo at the top‑right co... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page and Index Naming | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to process multiple PDF files, extract images from each page using Aspose.Pdf.Facades.P... |
| [configure-word-wrapping-bywords-add-long-text](./configure-word-wrapping-bywords-add-long-text.cs) | Configure Word Wrapping ByWords and Add Long Text to PDF | `Document`, `PdfFileMend`, `IsWordWrap` | The example shows how to enable word wrapping on a PDF using PdfFileMend, set the wrap mode to By... |
| [extract-images-from-pdf-pages](./extract-images-from-pdf-pages.cs) | Extract Images from Specific PDF Pages | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from pages 2‑5 of a PDF an... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF document and s... |
| [insert-png-signature-image-on-every-pdf-page](./insert-png-signature-image-on-every-pdf-page.cs) | Insert PNG Signature Image on Every PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use PdfFileMend to add a PNG signature image to the bottom‑left corner of eac... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF on PDF | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to place a semi‑transparent GIF over an existing PNG in a PDF using CompositingP... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete every image in a PDF using Aspose.Pdf's PdfContentEditor facade and save the ... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from Specific PDF Page | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a particular image identified by its object ID from page four of a PDF... |
| [replace-jpeg-with-bmp-first-page](./replace-jpeg-with-bmp-first-page.cs) | Replace JPEG with High‑Resolution BMP on First PDF Page | `Document`, `PdfContentEditor`, `BindPdf` | Demonstrates how to replace the first image on page one of a PDF with a higher‑resolution BMP usi... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low-Resolution Images with High-Resolution PNGs in P... | `Document`, `PdfContentEditor`, `PdfExtractor` | Demonstrates how to iterate through each page of a PDF, detect embedded images, and replace low‑r... |

## Category Statistics
- Total examples: 28

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
- Review code examples in this folder for facades-texts-and-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

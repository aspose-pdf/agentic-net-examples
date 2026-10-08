---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **29** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (24/29 files) ← category-specific
- `using Aspose.Pdf.Facades;` (12/29 files)
- `using Aspose.Pdf.Text;` (8/29 files)
- `using Aspose.Pdf.Drawing;` (3/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using NUnit.Framework;` (1/29 files)
- `using StubHttp;` (1/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.Threading.Tasks;` (1/29 files)

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
| [add-background-png-to-all-pdf-pages](./add-background-png-to-all-pdf-pages.cs) | Add Background Image to All PDF Pages | `Document`, `Page`, `Image` | Shows how to load a PDF with Aspose.Pdf, loop through each page, and insert a PNG image that cove... |
| [add-header-image-to-pdf-documents](./add-header-image-to-pdf-documents.cs) | Add Header Image to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load PDF files with Aspose.Pdf, create an ImageStamp for a header image, apply the s... |
| [add-image-and-text-to-pdf-with-audit-logging](./add-image-and-text-to-pdf-with-audit-logging.cs) | Add Image and Text to PDF with Audit Logging | `Document`, `ImageStamp`, `TextFragment` | The example loads a PDF, inserts an image and a text fragment on the first page, logs each operat... |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Semi‑Transparent Text Watermark to PDF | `Document`, `ImageStamp`, `TextStamp` | Demonstrates how to place an image stamp as a background and overlay it with a semi‑transparent t... |
| [add-image-to-each-pdf-page-dynamic-position](./add-image-to-each-pdf-page-dynamic-position.cs) | Dynamic Image Placement on PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, retrieving page dimensions, calculating coordinates, and adding an Im... |
| [add-image-to-pdf-and-stream](./add-image-to-pdf-and-stream.cs) | Add Image to PDF and Stream via HTTP | `Document`, `Page`, `ImageStamp` | Loads a PDF, places an image on the first page using an ImageStamp, saves the modified document t... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to insert an image onto a chosen PDF page at given coordinates using Aspose.Pdf's Image... |
| [add-image-to-pdf-using-pdffilemend](./add-image-to-pdf-using-pdffilemend.cs) | Ensure PDF Changes Are Saved Using PdfFileMend and Try-Final... | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileMend to modify a PDF and guarantee that changes... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Images to PDF with Extension Validation | `Document`, `Page`, `Image` | Demonstrates how to validate image file extensions (JPG, PNG, GIF, BMP, TIFF) before inserting th... |
| [add-image-verify-pdf-byte-size](./add-image-verify-pdf-byte-size.cs) | Verify PDF Size Increases After Adding Image | `Document`, `ImageStamp`, `AddStamp` | Creates a simple PDF, adds a PNG image as an ImageStamp, and asserts that the PDF byte size grows... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `Document`, `Page`, `Image` | Demonstrates inserting multiple images into a PDF using Aspose.Pdf while handling missing files a... |
| [add-multi-line-text-block-page-3](./add-multi-line-text-block-page-3.cs) | Add Multi-Line Text with Custom Line Spacing to Page 3 | `Document`, `TextFragment`, `Position` | Demonstrates how to insert a multi-line text fragment with a specific line spacing into the left ... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend, inserting a PNG image onto page two at spe... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Multiple PDF Pages | `PdfFileMend`, `FormattedText`, `EncodingType` | Demonstrates inserting the same promotional message on pages 3, 5, and 7 of a PDF using Aspose.Pd... |
| [add-text-at-coordinates-verify-position](./add-text-at-coordinates-verify-position.cs) | Verify Text Position in PDF with Aspose | `Document`, `Page`, `TextFragment` | Creates a PDF in memory, adds a text fragment at specific X and Y coordinates, and uses a unit te... |
| [add-tiff-image-to-last-page-pdf](./add-tiff-image-to-last-page-pdf.cs) | Add TIFF Image to the Last Page of a PDF | `Document`, `PdfFileMend`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileMend to bind an existing PDF, add a TIFF image ... |
| [add-wrapped-footer-to-pdf-pages](./add-wrapped-footer-to-pdf-pages.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to add a footer with word‑by‑word wrapping to every page of a PDF using Aspose.P... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Text Stamp to PDF | `Document`, `TextStamp`, `TextState` | Demonstrates how to add a text stamp to every page of a PDF using Aspose.Pdf while keeping the op... |
| [batch-add-company-logo-to-pdfs](./batch-add-company-logo-to-pdfs.cs) | Add Company Logo Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load each PDF in a folder, place a PNG logo as an ImageStamp in the top‑right... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page and Index Naming | `Document`, `PdfExtractor`, `BindPdf` | Shows how to process all PDF files in a directory, extract each image per page using Aspose.Pdf, ... |
| [configure-word-wrapping-bywords-add-long-text](./configure-word-wrapping-bywords-add-long-text.cs) | Word‑by‑Word Text Wrapping in a PDF Rectangle | `Document`, `Page`, `Rectangle` | Demonstrates how to place a long paragraph inside a defined rectangle and enable word‑by‑word wra... |
| [extract-images-from-pdf-pages](./extract-images-from-pdf-pages.cs) | Extract Images from Specific PDF Pages | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from pages 2 through 5 of a PDF and save them to a temporary dire... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF document and sa... |
| [insert-png-signature-image-on-every-pdf-page](./insert-png-signature-image-on-every-pdf-page.cs) | Insert PNG Signature on Every PDF Page | `Document`, `ImageStamp`, `AddStamp` | Shows how to add a PNG signature image to the bottom‑left corner of each page in a PDF using Aspo... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF on PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a semi‑transparent GIF over an existing PNG in a PDF by using an ImageS... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from a PDF | `Document`, `Page`, `Images` | Shows how to delete every image in a PDF document using Aspose.Pdf and save the modified file to ... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image by Object ID from PDF Page | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete a specific image from a PDF page using its object ID with Aspose.Pdf.Facades.... |
| [replace-jpeg-with-bmp-first-page](./replace-jpeg-with-bmp-first-page.cs) | Replace JPEG Image on First Page with High‑Resolution BMP | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Demonstrates how to replace the first JPEG image on page 1 of a PDF with a higher‑resolution BMP ... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low‑Resolution Images with High‑Resolution PNGs | `Document`, `Page`, `PdfContentEditor` | Demonstrates how to iterate through all images in a PDF and replace each low‑resolution image wit... |

## Category Statistics
- Total examples: 29

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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

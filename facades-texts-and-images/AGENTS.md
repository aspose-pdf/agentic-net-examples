---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **29** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (25/29 files) ← category-specific
- `using Aspose.Pdf.Facades;` (16/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/29 files)
- `using Aspose.Pdf.Drawing;` (3/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using System.Drawing;` (3/29 files)
- `using System.Collections.Generic;` (1/29 files)
- `using System.Text;` (1/29 files)
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
| [add-background-png-to-all-pdf-pages](./add-background-png-to-all-pdf-pages.cs) | Add Background PNG Image to All PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, iterate through each page, and add a full‑page PNG background usi... |
| [add-header-image-to-pdf-documents](./add-header-image-to-pdf-documents.cs) | Add Header Image to PDF Pages | `Document`, `ImageStamp`, `Page` | Shows how to load PDF files with Aspose.Pdf, create an ImageStamp for a header graphic, place it ... |
| [add-image-and-text-to-pdf-with-audit-logging](./add-image-and-text-to-pdf-with-audit-logging.cs) | Add Images and Text to PDF with Audit Logging | `Document`, `ImageStamp`, `TextFragment` | Demonstrates inserting image stamps and text fragments onto specific PDF pages using Aspose.Pdf a... |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Text Watermark to PDF | `Document`, `ImageStamp`, `TextStamp` | Demonstrates how to load a PDF, overlay a semi‑transparent image stamp and a rotated semi‑transpa... |
| [add-image-to-each-pdf-page-dynamic-position](./add-image-to-each-pdf-page-dynamic-position.cs) | Add Centered Image to Each PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF with Aspose.Pdf, retrieving each page's dimensions, and dynamically pl... |
| [add-image-to-pdf-and-send-response](./add-image-to-pdf-and-send-response.cs) | Add Image to PDF and Send as HTTP Response | `Document`, `ImageStamp`, `SaveFormat` | Demonstrates loading a PDF, stamping an image onto its first page, and streaming the modified doc... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to insert an image onto a chosen page of a PDF at exact coordinates using Aspose.Pdf's ... |
| [add-image-to-pdf-using-pdffilemend](./add-image-to-pdf-using-pdffilemend.cs) | Modify PDF with PdfFileMend and Ensure Proper Cleanup | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates loading, modifying, and saving a PDF using Aspose.Pdf.Facades.PdfFileMend while guar... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image Stamp to PDF with Format Validation | `Document`, `ImageStamp`, `Page` | Demonstrates how to validate an image file's format and then add it as a stamp to a PDF using Asp... |
| [add-image-verify-pdf-byte-size](./add-image-verify-pdf-byte-size.cs) | Add Image Stamp to PDF and Verify Size Increase | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF with Aspose.Pdf, add an image stamp to each page, save the modified docum... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, add multiple images to a page using Aspose.Pdf, and catch exceptions to ... |
| [add-multi-line-text-block-page-3](./add-multi-line-text-block-page-3.cs) | Add Multi‑Line Text with Custom Line Spacing to Page 3 | `Document`, `Page`, `TextFragment` | Demonstrates how to insert a multi‑line text block with custom line spacing into the left margin ... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF with Aspose.Pdf.Facades.PdfFileMend, insert a PNG image onto pa... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileMend`, `FormattedText`, `AddText` | Demonstrates how to insert the same promotional message on pages 3, 5, and 7 of a PDF using Aspos... |
| [add-text-at-coordinates-verify-position](./add-text-at-coordinates-verify-position.cs) | Verify Text Position in PDF Using Aspose.Pdf | `Document`, `Page`, `TextFragment` | Creates a PDF, places a text fragment at specific X/Y coordinates, then loads the document and va... |
| [add-tiff-image-to-last-page-pdf](./add-tiff-image-to-last-page-pdf.cs) | Add TIFF Image to Last PDF Page with PdfFileMend | `PdfFileInfo`, `NumberOfPages`, `BindPdf` | Shows how to use Aspose.Pdf.Facades.PdfFileMend to insert a TIFF image onto the last page of an e... |
| [add-wrapped-footer-to-pdf-pages](./add-wrapped-footer-to-pdf-pages.cs) | Add Word-Wrapped Footer to PDF Pages | `PdfFileInfo`, `PdfFileMend`, `AddText` | Shows how to insert a formatted, word‑by‑word wrapped footer on every page of a PDF using PdfFile... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Text Watermark to PDF | `Document`, `TextStamp`, `FontStyles` | Shows how to apply a semi‑transparent "Confidential" text stamp to every page of a PDF using Aspo... |
| [batch-add-company-logo-to-pdfs](./batch-add-company-logo-to-pdfs.cs) | Add Company Logo to PDFs in Batch | `Document`, `ImageStamp`, `Page` | Demonstrates how to iterate through a folder of PDF files and stamp a PNG logo onto the top‑right... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Extract Images from PDFs and Rename by Page and Index | `Document`, `PdfExtractor`, `BindPdf` | The example iterates through all PDF files in a folder, extracts every image from each page, and ... |
| [configure-word-wrapping-bywords-add-long-text](./configure-word-wrapping-bywords-add-long-text.cs) | Add Word‑Wrapped Text to PDF Using PdfFileMend | `Document`, `PdfFileMend`, `PdfFileInfo` | Demonstrates how to enable word‑wrap mode ByWords with PdfFileMend and insert a long paragraph th... |
| [extract-images-from-pdf-pages](./extract-images-from-pdf-pages.cs) | Extract Images from Specific PDF Pages to a Temporary Folder | `PdfExtractor`, `StartPage`, `EndPage` | Demonstrates how to extract all images from pages 2 through 5 of a PDF using Aspose.Pdf.Facades a... |
| [extract-images-from-pdf-to-png__v2](./extract-images-from-pdf-to-png__v2.cs) | Extract Images from PDF to PNG Files | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract every image from a PDF document and s... |
| [insert-png-signature-image-on-every-pdf-page](./insert-png-signature-image-on-every-pdf-page.cs) | Insert PNG Signature on Every PDF Page | `Document`, `PdfFileMend`, `BindPdf` | Demonstrates how to use PdfFileMend to add a PNG signature image to the bottom‑left corner of eac... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF on PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to place a semi‑transparent GIF over an existing PNG in a PDF by using an ImageStamp wi... |
| [remove-all-images-from-pdf__v2](./remove-all-images-from-pdf__v2.cs) | Remove All Images from a PDF | `Document`, `Page`, `Resources` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, clear the image resources, an... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image by Object ID from PDF Page | `Document`, `Page`, `ImageCollection` | Demonstrates how to delete a specific image from a given PDF page using its object ID with Aspose... |
| [replace-jpeg-with-bmp-first-page](./replace-jpeg-with-bmp-first-page.cs) | Replace JPEG with High‑Resolution BMP on First PDF Page | `Document`, `Page`, `XImageCollection` | Demonstrates loading a PDF, locating the first image on the first page, and replacing it with a h... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low‑Resolution Images with High‑Resolution PNGs | `Document`, `Page`, `XImage` | Demonstrates how to iterate through PDF pages and image resources, locate matching high‑resolutio... |

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
Updated: 2026-11-12 | Run: `20261111_235959_bd8254`
<!-- AUTOGENERATED:END -->

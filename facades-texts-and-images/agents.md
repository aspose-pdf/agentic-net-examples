---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (27/27 files) ← category-specific
- `using Aspose.Pdf;` (14/27 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/27 files)
- `using Aspose.Pdf.Devices;` (1/27 files)
- `using System;` (27/27 files)
- `using System.IO;` (27/27 files)
- `using System.Drawing.Imaging;` (2/27 files)
- `using System.Linq;` (2/27 files)
- `using NUnit.Framework;` (1/27 files)
- `using System.Drawing;` (1/27 files)
- `using System.Text;` (1/27 files)
- `using System.Threading;` (1/27 files)
- `using System.Threading.Tasks;` (1/27 files)

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
| [add-background-image-to-all-pdf-pages](./add-background-image-to-all-pdf-pages.cs) | Add Background Image to All PDF Pages | `Document`, `PdfFileMend`, `BindPdf` | Shows how to overlay a PNG background image on every page of a PDF by retrieving page dimensions ... |
| [add-header-image-to-pdf](./add-header-image-to-pdf.cs) | Add Header Image to PDF Files | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp in a console app to insert a header image into o... |
| [add-image-and-text-to-pdf-with-logging](./add-image-and-text-to-pdf-with-logging.cs) | Add Image and Text to PDF with Audit Logging | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to use Aspose.Pdf.Facades.PdfFileMend to insert an image and formatted text onto specif... |
| [add-image-stamp-to-pdf-with-cleanup](./add-image-stamp-to-pdf-with-cleanup.cs) | Add Image Stamp to PDF with Proper Resource Cleanup | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind a PDF, add an image stamp to a page, save the changes, and guarantee res... |
| [add-image-to-all-pdf-pages-dynamic-positioning](./add-image-to-all-pdf-pages-dynamic-positioning.cs) | Add Image to All PDF Pages with Dynamic Positioning | `Document`, `Page`, `PdfFileMend` | Loads a PDF, calculates image placement coordinates based on each page's dimensions, and inserts ... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to insert an image onto a chosen page of a PDF document at specified coordinates... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image to PDF with Format Validation | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to validate an image's file format before using Aspose.Pdf.Facades.PdfFileMend t... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Demonstrates how to add multiple images to a PDF using Aspose.Pdf's PdfFileMend facade while hand... |
| [add-multi-line-text-custom-spacing-page-3](./add-multi-line-text-custom-spacing-page-3.cs) | Add Multi‑Line Text with Custom Line Spacing to Page 3 | `PdfFileMend`, `BindPdf`, `Pages` | Demonstrates how to use Aspose.Pdf Facades to insert a multi‑line text paragraph with custom line... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, insert a PNG image onto a specific pag... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileMend`, `BindPdf`, `AddText` | Shows how to insert the same promotional message on pages 3, 5, and 7 of a PDF using Aspose.Pdf.F... |
| [add-semi-transparent-image-text-watermark](./add-semi-transparent-image-text-watermark.cs) | Add Semi-Transparent Image and Text Watermark to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to combine an image stamp and a formatted text stamp using Aspose.Pdf.Facades to... |
| [add-tiff-image-to-last-pdf-page](./add-tiff-image-to-last-pdf-page.cs) | Add TIFF Image to Last PDF Page with PdfFileMend | `Document`, `Page`, `PdfFileMend` | Demonstrates using Aspose.Pdf's PdfFileMend to insert a TIFF image onto the last page of an exist... |
| [add-word-wrap-footer-to-pdf-pages](./add-word-wrap-footer-to-pdf-pages.cs) | Add Word‑Wrap Footer to All PDF Pages | `PdfFileMend`, `BindPdf`, `IsWordWrap` | Shows how to use PdfFileMend with FormattedText to add a word‑by‑word wrapped footer to every pag... |
| [add-word-wrapped-paragraph-to-pdf](./add-word-wrapped-paragraph-to-pdf.cs) | Add Word‑Wrapped Paragraph to PDF Using PdfFileMend | `Document`, `PdfFileMend`, `IsWordWrap` | Demonstrates how to enable the ByWords word‑wrap mode with PdfFileMend and add a long paragraph t... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Image to PDF Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to modify a PDF on a background thread by adding an image to the first page with PdfFil... |
| [batch-add-company-logo-to-pdfs](./batch-add-company-logo-to-pdfs.cs) | Batch Add Company Logo to PDFs | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to process a folder of PDF files and stamp a company logo PNG onto the top‑right... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page and Index Naming | `Document`, `PdfExtractor`, `BindPdf` | Demonstrates how to iterate through multiple PDF files, extract all images per page, and save the... |
| [extract-images-from-pdf-pages](./extract-images-from-pdf-pages.cs) | Extract Images from Specific PDF Pages | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract images from pages 2 through 5 of a PDF and save them as PNG files in a tempo... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save each a... |
| [insert-png-signature-on-every-pdf-page](./insert-png-signature-on-every-pdf-page.cs) | Insert PNG Signature on Every PDF Page | `Document`, `PdfFileMend`, `Count` | Demonstrates how to use PdfFileMend to add a PNG signature image to the bottom‑left corner of eac... |
| [overlay-semi-transparent-gif-onto-pdf](./overlay-semi-transparent-gif-onto-pdf.cs) | Overlay Semi-Transparent GIF onto PDF using CompositingParam... | `Document`, `PdfFileMend`, `CompositingParameters` | Shows how to place a semi‑transparent GIF over an existing PNG in a PDF by using PdfFileMend with... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from a PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete every image from a PDF using Aspose.Pdf's PdfContentEditor and save the modif... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from Specific PDF Page | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a particular image on page four of a PDF using Aspose.Pdf's PdfContent... |
| [replace-jpeg-with-bmp](./replace-jpeg-with-bmp.cs) | Replace JPEG Image with BMP Using PdfContentEditor | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to replace an existing image on the first page of a PDF with a higher‑resolution BMP fi... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low-Resolution Images with High-Resolution PNGs in P... | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Shows how to loop through PDF pages and images using Aspose.Pdf.Facades and replace each low‑reso... |
| [verify-pdf-size-increase-after-adding-image](./verify-pdf-size-increase-after-adding-image.cs) | Verify PDF Size Increase After Adding Image | `Document`, `PdfFileMend`, `AddImage` | The example creates a blank PDF, adds a PNG image to the first page using Aspose.Pdf.Facades.PdfF... |

## Category Statistics
- Total examples: 27

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-texts-and-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_144637_f72c91`
<!-- AUTOGENERATED:END -->

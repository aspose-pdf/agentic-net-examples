---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **28** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (26/28 files) ← category-specific
- `using Aspose.Pdf;` (16/28 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/28 files)
- `using Aspose.Pdf.Drawing;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (26/28 files)
- `using System.Drawing;` (4/28 files)
- `using System.Drawing.Imaging;` (2/28 files)
- `using System.Linq;` (1/28 files)
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
| [add-background-png-to-all-pdf-pages](./add-background-png-to-all-pdf-pages.cs) | Add Background PNG to All PDF Pages | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to use Aspose.Pdf.Facades.PdfFileMend to overlay a full‑page PNG image as a background ... |
| [add-centered-image-to-pdf](./add-centered-image-to-pdf.cs) | Add Centered Image to PDF Using Page Dimensions | `Document`, `Page`, `PdfFileMend` | Demonstrates retrieving a PDF page's width and height with Aspose.Pdf, calculating coordinates, a... |
| [add-header-image-to-pdf](./add-header-image-to-pdf.cs) | Add Header Image to PDF Documents | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to insert a header image on every page of one or more PDF files using Aspose.Pdf.Facades. |
| [add-image-and-text-with-audit-logging](./add-image-and-text-with-audit-logging.cs) | Add Image and Text to PDF with Audit Logging | `Document`, `BindPdf`, `AddImage` | Demonstrates using Aspose.Pdf facades to insert an image and a text annotation into a PDF and log... |
| [add-image-http-response](./add-image-http-response.cs) | Add Image to PDF and Return via HTTP Response | `Document`, `PdfFileMend`, `AddImage` | Creates a PDF, adds an image using PdfFileMend, saves the result to a memory stream, and writes i... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to insert an image onto a chosen page of a PDF document at given rectangle coordinates ... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image to PDF with Format Validation | `PdfFileMend`, `BindPdf`, `AddImage` | The example validates that an image file is of an allowed type (JPG, PNG, GIF, BMP, or TIFF) befo... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Demonstrates how to insert multiple images into a PDF using Aspose.Pdf.Facades.PdfFileMend while ... |
| [add-multi-line-text-custom-spacing-page-3](./add-multi-line-text-custom-spacing-page-3.cs) | Add Multi‑Line Text with Custom Line Spacing to Page 3 | `PdfFileMend`, `Document`, `Page` | Shows how to bind an existing PDF with PdfFileMend, create a TextParagraph with custom line spaci... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend and insert a PNG image onto page two at... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileMend`, `BindPdf`, `AddText` | Shows how to insert the same promotional message on pages 3, 5, and 7 of a PDF using Aspose.Pdf.F... |
| [add-tiff-image-to-last-page](./add-tiff-image-to-last-page.cs) | Add TIFF Image to Last Page of PDF | `Document`, `PdfFileMend`, `BindPdf` | Shows how to use PdfFileMend to insert a TIFF image onto the final page of an existing PDF withou... |
| [add-word-wrapped-footer-to-pdf-pages](./add-word-wrapped-footer-to-pdf-pages.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `PdfFileStamp`, `AddFooter`, `FormattedText` | Shows how to create a FormattedText object and use PdfFileStamp to add a word‑by‑word wrapped foo... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Image to PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to wrap the synchronous PdfFileMend operations in Task.Run to add an image to a specifi... |
| [batch-add-logo-to-pdf-pages](./batch-add-logo-to-pdf-pages.cs) | Batch Add Logo to PDF Pages | `Document`, `PdfFileMend`, `BindPdf` | Shows how to iterate through a folder of PDF files and overlay a company logo PNG in the top‑righ... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page‑Based Naming | `Document`, `PdfExtractor`, `BindPdf` | Demonstrates how to iterate through multiple PDF files, extract all images from each page, and sa... |
| [configure-word-wrapping-bywords](./configure-word-wrapping-bywords.cs) | Configure Word Wrapping ByWords for PDF Paragraph | `Document`, `Page`, `Rectangle` | Demonstrates setting the TextParagraph formatting to wrap whole words (ByWords) and adding a long... |
| [extract-images-from-pdf-as-png](./extract-images-from-pdf-as-png.cs) | Extract Images from PDF as PNG Files | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF and save each ... |
| [extract-images-from-pdf-pages-2-5](./extract-images-from-pdf-pages-2-5.cs) | Extract Images from PDF Pages 2‑5 | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a specific page range... |
| [insert-png-signature-on-every-pdf-page](./insert-png-signature-on-every-pdf-page.cs) | Insert PNG Signature on Every PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to use Aspose.Pdf.Facades.PdfFileMend to bind an existing PDF, add a PNG signature imag... |
| [modify-pdf-with-pdffilemend-ensure-close](./modify-pdf-with-pdffilemend-ensure-close.cs) | Modify PDF with PdfFileMend and Ensure Proper Closure | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates binding a PDF, optionally adding an image, saving the modified document, and guarant... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF onto PDF Image | `PdfFileMend`, `CompositingParameters`, `BlendMode` | Demonstrates how to place a semi‑transparent GIF over an existing PNG in a PDF using Aspose.Pdf's... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from a PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete every image in a PDF using Aspose.Pdf's PdfContentEditor and save the modifie... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from Specific PDF Page | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a particular image, identified by its object ID, from page four of a P... |
| [replace-jpeg-image-with-bmp](./replace-jpeg-image-with-bmp.cs) | Replace JPEG Image on First Page with BMP | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Demonstrates how to replace the first JPEG image on page 1 of a PDF with a higher‑resolution BMP ... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low‑Resolution Images with High‑Resolution PNGs in a... | `Document`, `PdfContentEditor`, `BindPdf` | Demonstrates how to iterate through each page and image in a PDF and replace low‑resolution image... |
| [verify-pdf-size-after-image](./verify-pdf-size-after-image.cs) | Verify PDF Size Increase After Adding an Image | `Document`, `PdfFileMend`, `AddImage` | Creates a sample PDF, adds an image using PdfFileMend, and checks that the file size grows, servi... |
| [verify-text-position-in-pdf](./verify-text-position-in-pdf.cs) | Verify Text Position in PDF | `Document`, `Page`, `TextFragment` | Demonstrates adding a text fragment at specific X/Y coordinates in a PDF and using a TextFragment... |

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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **29** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (29/29 files) ← category-specific
- `using Aspose.Pdf;` (17/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (29/29 files)
- `using System.Drawing;` (5/29 files)
- `using System.Drawing.Imaging;` (3/29 files)
- `using NUnit.Framework;` (2/29 files)
- `using System.Linq;` (1/29 files)
- `using System.Threading;` (1/29 files)
- `using System.Threading.Tasks;` (1/29 files)

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
| [add-background-image-to-all-pdf-pages](./add-background-image-to-all-pdf-pages.cs) | Add Background Image to All PDF Pages | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use the PdfFileMend facade to overlay a PNG image as a full‑page background o... |
| [add-company-logo-to-pdf-pages](./add-company-logo-to-pdf-pages.cs) | Add Company Logo to All PDF Pages | `Document`, `Page`, `PageInfo` | Shows how to batch‑process a folder of PDFs and overlay a PNG company logo in the top‑right corne... |
| [add-header-image-to-pdf](./add-header-image-to-pdf.cs) | Add Header Image to PDF Files | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to insert a header image into one or more PDF documents using Aspose.Pdf.Facades.PdfFil... |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Text Watermark to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to combine an image stamp and a semi‑transparent text stamp using Aspose.Pdf Fac... |
| [add-image-and-text-with-audit-logging](./add-image-and-text-with-audit-logging.cs) | Add Image and Text to PDF with Audit Logging | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates using Aspose.Pdf.Facades.PdfFileMend to bind a PDF, insert an image and formatted te... |
| [add-image-to-pdf-memory-stream](./add-image-to-pdf-memory-stream.cs) | Add Image to PDF and Return as MemoryStream | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to insert an image onto the first page of an existing PDF using Aspose.Pdf.Facades and ... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to insert an image onto a chosen page of a PDF document at given rectangle coordinates ... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image to PDF with Format Validation | `Document`, `Save`, `PdfFileMend` | Demonstrates how to validate an image's file format and then add the image to a specific page of ... |
| [add-image-verify-pdf-byte-size](./add-image-verify-pdf-byte-size.cs) | Add Image to PDF and Verify Byte Size Increase | `Document`, `PdfFileMend`, `BindPdf` | Creates a blank PDF, inserts a PNG image using PdfFileMend, saves the result, and asserts that th... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Demonstrates how to insert multiple images into a PDF using the PdfFileMend facade and handle mis... |
| [add-multi-line-text-with-custom-spacing](./add-multi-line-text-with-custom-spacing.cs) | Add Multi‑Line Text with Custom Spacing to PDF Page | `PdfFileMend`, `AddText`, `Save` | Shows how to create a FormattedText block, add lines with custom line spacing, and place the bloc... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, insert a PNG image onto page two at sp... |
| [add-promotional-text-multiple-pdf-pages](./add-promotional-text-multiple-pdf-pages.cs) | Add Promotional Text to Multiple PDF Pages | `PdfFileMend`, `BindPdf`, `AddText` | Demonstrates inserting the same promotional message on pages 3, 5, and 7 of a PDF using Aspose.Pd... |
| [add-tiff-image-to-last-pdf-page](./add-tiff-image-to-last-pdf-page.cs) | Add TIFF Image to Last PDF Page with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use PdfFileMend to insert a TIFF image onto the final page of an existing PDF... |
| [add-word-wrapped-footer-to-pdf-pages](./add-word-wrapped-footer-to-pdf-pages.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `PdfFileStamp`, `BindPdf`, `FormattedText` | Shows how to use Aspose.Pdf.Facades to add a formatted footer with automatic word‑by‑word wrappin... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Image to PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to wrap PdfFileMend operations in Task.Run to add an image to the first page of a PDF w... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page and Index Naming | `Document`, `PdfExtractor`, `BindPdf` | Shows how to process multiple PDF files, extract images from each page using PdfExtractor, and sa... |
| [dynamic-image-placement](./dynamic-image-placement.cs) | Dynamic Image Placement on PDF Page | `Document`, `Page`, `PdfFileMend` | Demonstrates calculating image coordinates from a PDF page's dimensions and adding the image usin... |
| [extract-images-from-pdf-pages-2-5](./extract-images-from-pdf-pages-2-5.cs) | Extract Images from PDF Pages 2‑5 | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF within a specif... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF document and sa... |
| [insert-png-signature-on-all-pdf-pages](./insert-png-signature-on-all-pdf-pages.cs) | Insert PNG Signature on All PDF Pages with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use the PdfFileMend facade to add a PNG signature image to the bottom‑left co... |
| [modify-pdf-with-pdffilemend-try-finally](./modify-pdf-with-pdffilemend-try-finally.cs) | Modify PDF Using PdfFileMend with Try-Finally Cleanup | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates binding a PDF, optionally modifying it, saving the result, and guaranteeing that the... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF on PDF | `Document`, `PdfFileMend`, `CompositingParameters` | Demonstrates how to place a semi‑transparent GIF over an existing PNG image in a PDF using Aspose... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete every image in a PDF using Aspose.Pdf's PdfContentEditor and save the modifie... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from PDF Page | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to delete a specific image on a given page of a PDF using Aspose.Pdf.Facades.PdfContent... |
| [replace-image-with-bmp](./replace-image-with-bmp.cs) | Replace PDF Image with High‑Resolution BMP | `Document`, `PdfContentEditor`, `BindPdf` | Demonstrates how to replace the first image on the first page of a PDF with a higher‑resolution B... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low-Resolution Images with High-Resolution PNGs in P... | `Document`, `PdfContentEditor`, `Pages` | Loads a PDF, iterates through each page and its images, and replaces every low‑resolution image w... |
| [verify-added-text-coordinates](./verify-added-text-coordinates.cs) | Verify Added Text Coordinates with PdfContentEditor | `Document`, `PdfContentEditor`, `BindPdf` | Creates a blank PDF, adds a free‑text annotation at a specific rectangle using PdfContentEditor, ... |
| [wrap-text-by-words-using-pdffilemend](./wrap-text-by-words-using-pdffilemend.cs) | Wrap Text by Words Using PdfFileMend | `Document`, `PdfFileMend`, `FormattedText` | Demonstrates how to enable the ByWords word‑wrapping mode with PdfFileMend and add a long paragra... |

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

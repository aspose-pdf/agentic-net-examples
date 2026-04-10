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

- `using Aspose.Pdf.Facades;` (28/29 files) ← category-specific
- `using Aspose.Pdf;` (16/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using System.Drawing.Imaging;` (2/29 files)
- `using System.Linq;` (2/29 files)
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
| [add-background-png-image-to-all-pdf-pages](./add-background-png-image-to-all-pdf-pages.cs) | Add Background PNG Image to All PDF Pages | `Document`, `AddImage` | Shows how to insert a PNG background image on every page of a PDF using the PdfFileMend facade. |
| [add-header-image-to-pdf-documents](./add-header-image-to-pdf-documents.cs) | Add Header Image to PDF Documents | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to insert a header image on every page of one or more PDF files using Aspose.Pdf.Facades. |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Semi‑Transparent Text Watermark to PDF | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates using Aspose.Pdf.Facades to stamp an image as a background and then overlay a semi‑t... |
| [add-image-and-text-with-audit-logging](./add-image-and-text-with-audit-logging.cs) | Add Image and Text to PDF with Audit Logging | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates using PdfFileMend to insert an image and formatted text into a PDF page while loggin... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to insert an image into a specified page of an existing PDF document using Aspose.Pdf's... |
| [add-image-to-pdf-return-bytes](./add-image-to-pdf-return-bytes.cs) | Add Image to PDF and Return as Byte Array | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to use Aspose.Pdf.Facades to load a PDF, place an image on the first page, and return t... |
| [add-image-to-pdf-with-format-validation](./add-image-to-pdf-with-format-validation.cs) | Add Image to PDF with Format Validation | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to validate an image's file format before using Aspose.Pdf.Facades to insert it ... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Shows how to insert multiple images into a PDF using Aspose.Pdf.Facades.PdfFileMend and gracefull... |
| [add-multi-line-text-custom-spacing-page-3](./add-multi-line-text-custom-spacing-page-3.cs) | Add Multi‑Line Text with Custom Line Spacing to Page 3 | `Document`, `Page`, `TextParagraph` | Demonstrates how to insert a multi‑line text block with custom line spacing into the left margin ... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to Specific PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend, inserting a PNG image onto page two at giv... |
| [add-promotional-text-to-specific-pdf-pages](./add-promotional-text-to-specific-pdf-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileMend`, `FormattedText`, `BindPdf` | Demonstrates how to insert the same promotional message on pages 3, 5, and 7 of a PDF using PdfFi... |
| [add-tiff-image-to-last-page-pdf](./add-tiff-image-to-last-page-pdf.cs) | Add TIFF Image to Last Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind a PDF with PdfFileMend, determine the last page, and insert a TIFF image onto t... |
| [add-word-wrap-footer-to-pdf-pages](./add-word-wrap-footer-to-pdf-pages.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `PdfFileStamp`, `BindPdf`, `AddFooter` | Shows how to place a formatted footer that automatically wraps word‑by‑word across the page width... |
| [add-word-wrapped-paragraph-to-pdf](./add-word-wrapped-paragraph-to-pdf.cs) | Add Word‑Wrapped Paragraph to PDF | `Document`, `PdfFileMend`, `WordWrapMode` | Demonstrates how to enable the ByWords word‑wrap mode with PdfFileMend and TextParagraph, then ad... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronous Add Image to PDF with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to add an image to a specific PDF page asynchronously by wrapping PdfFileMend op... |
| [batch-add-logo-to-pdf-pages](./batch-add-logo-to-pdf-pages.cs) | Batch Add Logo to PDF Pages | `Document`, `Page`, `PdfFileMend` | Demonstrates how to process a folder of PDF files and add a company logo PNG to the top‑right cor... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page and Index Naming | `Document`, `BindPdf`, `ExtractImage` | Shows how to process all PDF files in a folder, extract images from each page using Aspose.Pdf.Fa... |
| [dynamic-image-placement-on-pdf](./dynamic-image-placement-on-pdf.cs) | Dynamic Image Placement on PDF Page | `Document`, `Page`, `BindPdf` | Demonstrates how to retrieve a PDF page's dimensions, calculate image coordinates proportionally,... |
| [extract-images-from-pdf-pages](./extract-images-from-pdf-pages.cs) | Extract Images from Specific PDF Pages | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from pages 2‑5 of a PDF and save them to a temporary directory us... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF file and save ... |
| [insert-png-signature-on-every-pdf-page](./insert-png-signature-on-every-pdf-page.cs) | Insert PNG Signature on Every PDF Page | `Document`, `BindPdf`, `AddImage` | Demonstrates how to use Aspose.Pdf's PdfFileMend facade to place a PNG signature image at the bot... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF on PDF Using CompositingParamet... | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to place a semi‑transparent GIF onto a specific page of an existing PDF by using PdfFil... |
| [pdffilemend-try-finally-save](./pdffilemend-try-finally-save.cs) | Modify PDF with PdfFileMend and Ensure Save Using Try‑Finall... | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates how to bind, modify, and save a PDF using Aspose.Pdf.Facades.PdfFileMend while guara... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfContentEditor to delete every image from an existin... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from PDF Page Using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a specific image, identified by its object ID, from a given page of a ... |
| [replace-image-with-bmp](./replace-image-with-bmp.cs) | Replace Image on PDF Page with BMP | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Shows how to replace the first image on the first page of a PDF with a higher‑resolution BMP usin... |
| [replace-low-res-images-with-high-res-pngs](./replace-low-res-images-with-high-res-pngs.cs) | Replace Low-Resolution Images with High-Resolution PNGs | `Document`, `PdfContentEditor`, `BindPdf` | Demonstrates how to iterate through PDF pages and replace each low‑resolution image with a corres... |
| [verify-pdf-size-after-adding-image](./verify-pdf-size-after-adding-image.cs) | Verify PDF Size Increase After Adding Image | `Document`, `Save`, `PdfFileMend` | Creates a blank PDF, adds an image using PdfFileMend, and includes unit tests that confirm the PD... |
| [verify-text-position-in-pdf](./verify-text-position-in-pdf.cs) | Verify Text Position in PDF | `Document`, `TextBuilder`, `TextFragment` | Creates a PDF, adds a text fragment at specific X/Y coordinates, saves and reloads the document, ... |

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-texts-and-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

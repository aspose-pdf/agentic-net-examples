---
name: Facades - Texts and Images
description: C# examples for Facades - Texts and Images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - Texts and Images

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - Texts and Images** category.
This folder contains standalone C# examples for Facades - Texts and Images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - Texts and Images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (36/49 files) ← category-specific
- `using Aspose.Pdf;` (34/49 files) ← category-specific
- `using Aspose.Pdf.Text;` (10/49 files)
- `using Aspose.Pdf.Drawing;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (47/49 files)
- `using System.Drawing;` (2/49 files)
- `using System.Drawing.Imaging;` (1/49 files)
- `using System.Linq;` (1/49 files)
- `using System.Runtime.InteropServices;` (1/49 files)
- `using System.Text;` (1/49 files)
- `using System.Threading.Tasks;` (1/49 files)

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
| [add-background-png-image](./add-background-png-image.cs) | Add Background PNG Image to All PDF Pages | `PdfFileMend`, `AddImage`, `Document` | Demonstrates how to add a PNG image as a background to every page of a PDF by iterating through p... |
| [add-background-png](./add-background-png.cs) | Add Background PNG Image to All PDF Pages | `Document`, `PdfFileMend`, `AddImage` | Demonstrates how to add a PNG image as a background to every page of a PDF by iterating through p... |
| [add-company-logo-batch](./add-company-logo-batch.cs) | Add Company Logo to PDFs in Batch | `Document`, `ImageStamp`, `AddStamp` | Processes all PDF files in a folder and adds a PNG logo to the top‑right corner of each page. |
| [add-footer-word-wrap](./add-footer-word-wrap.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `PdfFileStamp`, `FormattedText`, `AddFooter` | Demonstrates adding a formatted text footer with automatic word‑by‑word wrapping to every page of... |
| [add-formatted-text-footer](./add-formatted-text-footer.cs) | Add Formatted Text Footer to PDF Pages | `PdfFileStamp`, `FormattedText`, `AddFooter` | Demonstrates adding a word‑by‑word wrapped formatted text footer to every page of a PDF using Pdf... |
| [add-header-image-multiple-pdfs](./add-header-image-multiple-pdfs.cs) | Add Header Image to Multiple PDFs | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to iterate over a set of PDF files and add a header image to each using Aspose.P... |
| [add-header-image-multiple-pdfs__v2](./add-header-image-multiple-pdfs__v2.cs) | Add Header Image to Multiple PDFs | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to iterate over a list of PDF files and add a header image to each using PdfFile... |
| [add-image-dynamic-coordinates](./add-image-dynamic-coordinates.cs) | Add Image to PDF Page with Dynamic Coordinates | `Document`, `Page`, `PdfFileMend` | Demonstrates retrieving page dimensions from a PDF and placing an image at a calculated position ... |
| [add-image-http-response](./add-image-http-response.cs) | Add Image to PDF and Return via HTTP Response | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates adding an image to a PDF using PdfFileMend, saving the result to a memory stream, an... |
| [add-image-text-audit](./add-image-text-audit.cs) | Add Image and Text to PDF with Audit Logging | `PdfFileMend`, `Document`, `AddImage` | Demonstrates adding an image to a PDF using PdfFileMend and logs each AddImage and AddText operat... |
| [add-image-to-pdf-memory-stream](./add-image-to-pdf-memory-stream.cs) | Add Image to PDF and Return as Memory Stream | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates adding an image to a PDF using PdfFileMend, saving the result into a MemoryStream, a... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to PDF Page Helper | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates a reusable helper method that inserts an image onto a specified page of a PDF at giv... |
| [add-image-to-pdf-page__v2](./add-image-to-pdf-page__v2.cs) | Add Image to PDF Page Using PdfFileMend | `PdfFileMend`, `AddImage` | Demonstrates a reusable helper method that inserts an image onto a specified page of a PDF at giv... |
| [add-image-with-format-validation](./add-image-with-format-validation.cs) | Add Image to PDF with Format Validation | `Document`, `AddImage`, `Rectangle` | Demonstrates how to validate an image file's format before adding it to a PDF page using Aspose.Pdf. |
| [add-images-with-error-handling](./add-images-with-error-handling.cs) | Add Images to PDF with Error Handling | `Document`, `AddImage`, `Rectangle` | Demonstrates adding images to a PDF page while catching exceptions from AddImage and logging the ... |
| [add-images-with-error-handling__v2](./add-images-with-error-handling__v2.cs) | Add Images to PDF with Error Handling | `Document`, `AddImage`, `Rectangle` | Demonstrates adding images to a PDF page while catching exceptions from AddImage and logging the ... |
| [add-multi-line-text-page-three](./add-multi-line-text-page-three.cs) | Add Multi-Line Text Block with Custom Line Spacing to Page T... | `Document`, `Page`, `TextParagraph` | Demonstrates adding a left-aligned multi-line text paragraph with custom line spacing to the thir... |
| [add-multi-line-text-page3](./add-multi-line-text-page3.cs) | Add Multi‑Line Text Block with Custom Line Spacing to Page 3 | `Document`, `Page`, `TextParagraph` | Demonstrates adding a multi‑line text paragraph with custom line spacing to the left margin of th... |
| [add-paragraph-word-wrap-bywords](./add-paragraph-word-wrap-bywords.cs) | Add Paragraph with Word Wrap ByWords | `Document`, `Page`, `TextParagraph` | Demonstrates configuring WordWrapMode.ByWords for a TextParagraph before adding long text that ex... |
| [add-paragraph-wordwrap-bywords](./add-paragraph-wordwrap-bywords.cs) | Add Paragraph with WordWrap ByWords | `Document`, `Page`, `TextParagraph` | Demonstrates configuring WordWrapMode.ByWords for a text paragraph that exceeds its rectangle wid... |
| [add-png-image-pdf-filemend](./add-png-image-pdf-filemend.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend and inserting a PNG image onto page two at ... |
| [add-png-image-pdffilemend](./add-png-image-pdffilemend.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend and inserting a PNG image onto page two at ... |
| [add-promotional-text-pages](./add-promotional-text-pages.cs) | Add Promotional Text to Specific PDF Pages | `PdfFileStamp`, `BindPdf`, `AddText` | Demonstrates how to insert the same promotional message on pages 3, 5, and 7 of a PDF using PdfFi... |
| [add-promotional-text](./add-promotional-text.cs) | Add Promotional Text to Specific Pages | `PdfFileStamp`, `FormattedText`, `AddText` | Demonstrates adding the same promotional message to pages 3, 5, and 7 of a PDF using PdfFileStamp... |
| [add-text-position-test](./add-text-position-test.cs) | Verify Text Position on PDF Page | `Document`, `Page`, `TextFragment` | Adds text at specific X/Y coordinates in a PDF and checks that the coordinates match using Aspose... |
| [add-tiff-image-last-page](./add-tiff-image-last-page.cs) | Add TIFF Image to Last PDF Page using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use PdfFileMend to add a TIFF image to the last page of an existing PDF witho... |
| [add-tiff-image-last-page__v2](./add-tiff-image-last-page__v2.cs) | Add TIFF Image to Last Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, locate its last page, and add a TIFF i... |
| [async-pdf-modification](./async-pdf-modification.cs) | Asynchronous PDF Modification with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to modify a PDF asynchronously by adding an image using PdfFileMend wrapped in T... |
| [batch-add-company-logo](./batch-add-company-logo.cs) | Batch Add Company Logo to PDFs | `Document`, `ImageStamp`, `AddStamp` | Processes all PDF files in a folder and adds a PNG logo to the top‑right corner of each page usin... |
| [batch-extract-images](./batch-extract-images.cs) | Batch Extract Images from PDFs with Page and Index Naming | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from each PDF in a folder and saves them using the source PDF name, page numb... |
| ... | | | *and 19 more files* |

## Category Statistics
- Total examples: 49

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
- Review code examples in this folder for Facades - Texts and Images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-01 | Run: `20260401_150821_8989ce`
<!-- AUTOGENERATED:END -->

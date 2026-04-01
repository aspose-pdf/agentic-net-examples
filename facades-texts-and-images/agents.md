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

- `using Aspose.Pdf.Facades;` (22/29 files) ← category-specific
- `using Aspose.Pdf;` (18/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (6/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using System.Drawing;` (3/29 files)
- `using System.Drawing.Imaging;` (1/29 files)
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
| [add-background-png-image](./add-background-png-image.cs) | Add Background PNG Image to Every PDF Page | `Document`, `PdfFileMend`, `AddImage` | Demonstrates how to add a PNG background image to each page of a PDF by iterating through all pag... |
| [add-header-image-multiple-pdfs](./add-header-image-multiple-pdfs.cs) | Add Header Image to Multiple PDFs | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to add a header image to each PDF in a list using Aspose.Pdf's PdfFileStamp. |
| [add-image-text-watermark](./add-image-text-watermark.cs) | Add Image and Text Watermark to PDF | `Document`, `WatermarkArtifact`, `TextState` | Demonstrates adding a semi‑transparent text overlay on top of an image as a watermark to each pag... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to PDF Page Helper | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates a reusable helper method that inserts an image onto a specified page of a PDF at giv... |
| [add-image-with-error-handling](./add-image-with-error-handling.cs) | Add Images to PDF with Error Handling | `Document`, `AddImage`, `Rectangle` | Demonstrates adding images to a PDF page while catching exceptions from AddImage and logging the ... |
| [add-image-with-format-validation](./add-image-with-format-validation.cs) | Add Image to PDF with Format Validation | `Document`, `AddImage`, `Rectangle` | Demonstrates how to validate an image's format before adding it to a PDF page using Aspose.Pdf. |
| [add-images-pdf-http-response](./add-images-pdf-http-response.cs) | Add Images to PDF and Return via HTTP Response | `PdfFileMend`, `AddImage`, `Save` | Demonstrates adding an image to a PDF using PdfFileMend, saving the result to a memory stream, an... |
| [add-multiline-text-block](./add-multiline-text-block.cs) | Add Multi-line Text Block with Custom Line Spacing to Page T... | `Document`, `Page`, `TextParagraph` | Demonstrates adding a multi‑line text paragraph with custom line spacing to the left margin of th... |
| [add-png-image-pdffilemend](./add-png-image-pdffilemend.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend and inserting a PNG image onto page two at ... |
| [add-promotional-text-multiple-pages](./add-promotional-text-multiple-pages.cs) | Add Promotional Text to Specific Pages Using PdfFileStamp | `PdfFileStamp`, `FormattedText`, `AddText` | Demonstrates inserting the same promotional message on pages 3, 5, and 7 of a PDF using PdfFileSt... |
| [add-tiff-image-last-page](./add-tiff-image-last-page.cs) | Add TIFF Image to Last Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to use PdfFileMend to insert a TIFF image onto the last page of an existing PDF ... |
| [add-word-wrapped-footer](./add-word-wrapped-footer.cs) | Add Word-Wrapped Footer to PDF Pages | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates adding a formatted, word‑wrapped footer to every page of a PDF using PdfFileStamp. |
| [async-pdf-modification](./async-pdf-modification.cs) | Asynchronous PDF Modification with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to perform non‑blocking PDF modifications, such as adding an image, by wrapping ... |
| [batch-add-company-logo](./batch-add-company-logo.cs) | Batch Add Company Logo to PDFs | `Document`, `ImageStamp`, `AddStamp` | Processes all PDF files in a folder and adds a PNG logo to the top‑right corner of each page. |
| [extract-images-from-pages](./extract-images-from-pages.cs) | Extract Images from Specific PDF Pages | `Extract`, `PdfExtractor`, `ExtractImage` | Extracts all images from pages 2 through 5 of a PDF and saves them to a temporary directory. |
| [extract-images-from-pdfs](./extract-images-from-pdfs.cs) | Extract Images from PDFs and Rename Using Page Number and Im... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to batch‑extract all images from multiple PDF files and save each image with a f... |
| [extract-images-pdf-png](./extract-images-pdf-png.cs) | Extract Images from PDF to PNG Files | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting images from a PDF and saving each as a PNG using Aspose.Pdf's PdfExtractor. |
| [insert-png-signature-pdfmend](./insert-png-signature-pdfmend.cs) | Insert PNG Signature on All PDF Pages using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to add a PNG signature image to the bottom‑left corner of every page of a PDF us... |
| [log-pdf-add-operations](./log-pdf-add-operations.cs) | Log AddImage and AddText Operations in PDF | `PdfFileMend`, `AddImage`, `AddText` | Demonstrates adding an image and text to a PDF using PdfFileMend while logging each operation wit... |
| [overlay-gif-onto-png](./overlay-gif-onto-png.cs) | Overlay Semi-Transparent GIF onto PNG in PDF using Compositi... | `PdfFileMend`, `CompositingParameters`, `BlendMode` | Demonstrates how to overlay a semi‑transparent GIF image onto an existing PNG image on the same P... |
| [pdf-file-mend-try-finally](./pdf-file-mend-try-finally.cs) | Add Image to PDF with PdfFileMend and Ensure Close via try-f... | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates using PdfFileMend to modify a PDF, saving changes, and guaranteeing resources are re... |
| [place-image-dynamic](./place-image-dynamic.cs) | Place Image Dynamically Based on Page Size | `Document`, `PdfFileMend`, `Page` | Demonstrates how to retrieve a PDF page's dimensions and calculate image placement coordinates be... |
| [remove-all-images](./remove-all-images.cs) | Remove All Images from PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete every image from a PDF using Aspose.Pdf.Facades.PdfContentEditor and s... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Specific Image from PDF Page Using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a particular image identified by its object ID from page four of a PDF... |
| [replace-images-highres](./replace-images-highres.cs) | Replace Images in PDF with High‑Resolution PNGs | `Document`, `Page`, `Replace` | Demonstrates how to replace each image in a PDF with a corresponding high‑resolution PNG using a ... |
| [replace-jpeg-with-bmp](./replace-jpeg-with-bmp.cs) | Replace JPEG Image on First Page with BMP Using PdfContentEd... | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Demonstrates how to replace a JPEG image on the first page of a PDF with a higher‑resolution BMP ... |
| [verify-pdf-modification-byte-size](./verify-pdf-modification-byte-size.cs) | Verify PDF Modification by Comparing Byte Size After Adding ... | `Document`, `PdfFileMend`, `BindPdf` | Creates a PDF, records its byte size, adds an image using PdfFileMend, saves the modified PDF, an... |
| [verify-text-position](./verify-text-position.cs) | Verify Text Position on PDF Page | `Document`, `Page`, `TextFragment` | Adds text at specific coordinates to a PDF and verifies the X/Y position using Aspose.Pdf. |
| [word-wrap-bywords](./word-wrap-bywords.cs) | Word Wrap ByWords for Text Paragraph | `Document`, `Page`, `TextParagraph` | Demonstrates setting the WordWrapMode to ByWords on a TextParagraph before adding long text that ... |

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
- Review code examples in this folder for Facades - Texts and Images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-01 | Run: `20260401_083243_90e036`
<!-- AUTOGENERATED:END -->

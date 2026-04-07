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

- `using Aspose.Pdf.Facades;` (22/29 files) ← category-specific
- `using Aspose.Pdf;` (19/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/29 files)
- `using Aspose.Pdf.Drawing;` (1/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.Drawing.Imaging;` (1/29 files)
- `using System.Runtime.InteropServices;` (1/29 files)
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
| [add-background-png](./add-background-png.cs) | Add Background PNG Image to All PDF Pages | `Document`, `PdfFileMend`, `AddImage` | Demonstrates how to add a PNG image as a background to every page of a PDF by iterating through p... |
| [add-company-logo-batch](./add-company-logo-batch.cs) | Add Company Logo to PDFs in Batch | `Document`, `ImageStamp`, `AddStamp` | Processes all PDF files in a folder and adds a PNG logo to the top‑right corner of each page. |
| [add-formatted-text-footer](./add-formatted-text-footer.cs) | Add Formatted Text Footer to PDF Pages | `PdfFileStamp`, `FormattedText`, `AddFooter` | Demonstrates adding a word‑by‑word wrapped formatted text footer to every page of a PDF using Pdf... |
| [add-header-image-multiple-pdfs__v2](./add-header-image-multiple-pdfs__v2.cs) | Add Header Image to Multiple PDFs | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to iterate over a list of PDF files and add a header image to each using PdfFile... |
| [add-image-dynamic-coordinates](./add-image-dynamic-coordinates.cs) | Add Image to PDF Page with Dynamic Coordinates | `Document`, `Page`, `PdfFileMend` | Demonstrates retrieving page dimensions from a PDF and placing an image at a calculated position ... |
| [add-image-text-audit](./add-image-text-audit.cs) | Add Image and Text to PDF with Audit Logging | `PdfFileMend`, `Document`, `AddImage` | Demonstrates adding an image to a PDF using PdfFileMend and logs each AddImage and AddText operat... |
| [add-image-to-pdf-memory-stream](./add-image-to-pdf-memory-stream.cs) | Add Image to PDF and Return as Memory Stream | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates adding an image to a PDF using PdfFileMend, saving the result into a MemoryStream, a... |
| [add-image-to-pdf-page__v2](./add-image-to-pdf-page__v2.cs) | Add Image to PDF Page Using PdfFileMend | `PdfFileMend`, `AddImage` | Demonstrates a reusable helper method that inserts an image onto a specified page of a PDF at giv... |
| [add-images-with-error-handling__v2](./add-images-with-error-handling__v2.cs) | Add Images to PDF with Error Handling | `Document`, `AddImage`, `Rectangle` | Demonstrates adding images to a PDF page while catching exceptions from AddImage and logging the ... |
| [add-multi-line-text-page3](./add-multi-line-text-page3.cs) | Add Multi‑Line Text Block with Custom Line Spacing to Page 3 | `Document`, `Page`, `TextParagraph` | Demonstrates adding a multi‑line text paragraph with custom line spacing to the left margin of th... |
| [add-paragraph-wordwrap-bywords](./add-paragraph-wordwrap-bywords.cs) | Add Paragraph with WordWrap ByWords | `Document`, `Page`, `TextParagraph` | Demonstrates configuring WordWrapMode.ByWords for a text paragraph that exceeds its rectangle wid... |
| [add-png-image-pdffilemend](./add-png-image-pdffilemend.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates binding an existing PDF with PdfFileMend and inserting a PNG image onto page two at ... |
| [add-promotional-text](./add-promotional-text.cs) | Add Promotional Text to Specific Pages | `PdfFileStamp`, `FormattedText`, `AddText` | Demonstrates adding the same promotional message to pages 3, 5, and 7 of a PDF using PdfFileStamp... |
| [add-text-position-test](./add-text-position-test.cs) | Verify Text Position on PDF Page | `Document`, `Page`, `TextFragment` | Adds text at specific X/Y coordinates in a PDF and checks that the coordinates match using Aspose... |
| [add-tiff-image-last-page__v2](./add-tiff-image-last-page__v2.cs) | Add TIFF Image to Last Page of PDF using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, locate its last page, and add a TIFF i... |
| [async-pdf-modification](./async-pdf-modification.cs) | Asynchronous PDF Modification with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to modify a PDF asynchronously by adding an image using PdfFileMend wrapped in T... |
| [batch-extract-images](./batch-extract-images.cs) | Batch Extract Images from PDFs with Page and Index Naming | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from each PDF in a folder and saves them using the source PDF name, page numb... |
| [compare-pdf-size-after-image](./compare-pdf-size-after-image.cs) | Compare PDF Size Before and After Adding Image | `Document`, `BindPdf`, `AddImage` | Loads a PDF, records its byte size, adds an image to the first page, then compares the byte sizes... |
| [extract-images-from-pages](./extract-images-from-pages.cs) | Extract Images from Specific PDF Pages | `Extract`, `PdfExtractor`, `ExtractImage` | Extracts all images from pages 2 through 5 of a PDF and saves them into a temporary directory. |
| [extract-images-pdf-png](./extract-images-pdf-png.cs) | Extract Images from PDF to PNG Files | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF document and save each as a separate PNG file u... |
| [image-text-watermark__v2](./image-text-watermark__v2.cs) | Add Semi-Transparent Text Watermark Over Image | `WatermarkArtifact`, `Document`, `Page` | Demonstrates adding a semi‑transparent text watermark on top of an image using WatermarkArtifact. |
| [insert-png-signature-pdf__v2](./insert-png-signature-pdf__v2.cs) | Insert PNG Signature on All PDF Pages using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to add a PNG signature image to the bottom‑left corner of each page of a PDF usi... |
| [overlay-gif-on-png](./overlay-gif-on-png.cs) | Overlay Semi-Transparent GIF onto PNG in PDF using Compositi... | `Document`, `Page`, `Image` | Demonstrates creating a PDF with a PNG image and then overlaying a semi‑transparent GIF on the sa... |
| [pdf-file-mend-close__v2](./pdf-file-mend-close__v2.cs) | Add Image to PDF using PdfFileMend with guaranteed Close | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to modify a PDF with PdfFileMend and ensure the facade is closed using a try‑fin... |
| [remove-all-images](./remove-all-images.cs) | Remove All Images from PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete every image from a PDF using Aspose.Pdf.Facades.PdfContentEditor and s... |
| [remove-image-from-page](./remove-image-from-page.cs) | Remove Specific Image from PDF Page Using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a specific image identified by its object ID from page four of a PDF u... |
| [replace-image-bmp__v2](./replace-image-bmp__v2.cs) | Replace JPEG Image on First Page with BMP Using PdfContentEd... | `PdfContentEditor`, `BindPdf`, `ReplaceImage` | Demonstrates how to replace the first JPEG image on page one of a PDF with a higher‑resolution BM... |
| [replace-low-res-images](./replace-low-res-images.cs) | Replace Low-Resolution Images with High-Resolution PNGs in P... | `Document`, `Images`, `Replace` | Demonstrates how to loop through all images in a PDF and replace each with a higher‑resolution PN... |
| [validate-image-format-add-image](./validate-image-format-add-image.cs) | Validate Image Format Before Adding to PDF | `Document`, `AddImage`, `Rectangle` | Demonstrates checking an image file's format (JPG, PNG, GIF, BMP, TIFF) before adding it to a PDF... |

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

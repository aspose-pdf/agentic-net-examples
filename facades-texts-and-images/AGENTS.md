---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

> **Facades texts and images** in PDF using C# / .NET -- **28** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (27/28 files) ← category-specific
- `using Aspose.Pdf;` (17/28 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/28 files)
- `using Aspose.Pdf.Annotations;` (1/28 files)
- `using Aspose.Pdf.Drawing;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (26/28 files)
- `using System.Drawing;` (5/28 files)
- `using System.Drawing.Imaging;` (2/28 files)
- `using System.Threading.Tasks;` (2/28 files)
- `using NUnit.Framework;` (1/28 files)
- `using System.Runtime.Versioning;` (1/28 files)
- `using System.Threading;` (1/28 files)

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
| [add-background-png-to-all-pdf-pages](./add-background-png-to-all-pdf-pages.cs) | Add Background PNG to All PDF Pages | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to overlay a PNG image as a full‑page background on every page of a PDF using th... |
| [add-header-image-to-pdfs](./add-header-image-to-pdfs.cs) | Add Header Image to PDFs | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to attach a header image to each page of multiple PDF documents and save the modified f... |
| [add-image-and-text-audit-pdf](./add-image-and-text-audit-pdf.cs) | Add Image and Text to PDF with Audit Logging | `Document`, `PdfFileMend`, `AddImage` | Demonstrates how to insert an image and a text annotation into a PDF using Aspose.Pdf facades whi... |
| [add-image-and-text-watermark-to-pdf](./add-image-and-text-watermark-to-pdf.cs) | Add Image and Text Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image and centered text as a watermark on each pag... |
| [add-image-to-all-pdf-pages-dynamic-position](./add-image-to-all-pdf-pages-dynamic-position.cs) | Add Image to All PDF Pages with Dynamic Positioning | `Document`, `PdfFileMend`, `Page` | Demonstrates loading a PDF, calculating each page's dimensions, and placing an image at the botto... |
| [add-image-to-pdf-get-memorystream](./add-image-to-pdf-get-memorystream.cs) | Add Image to PDF and Return MemoryStream | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to bind an existing PDF with Aspose.Pdf.Facades.PdfFileMend, add an image to the first ... |
| [add-image-to-pdf-page](./add-image-to-pdf-page.cs) | Add Image to Specific PDF Page | `Document`, `PdfFileMend`, `AddImage` | Shows how to insert an image onto a chosen PDF page at given rectangle coordinates using Aspose.P... |
| [add-image-to-pdf-with-pdffilemend](./add-image-to-pdf-with-pdffilemend.cs) | Add Image to PDF Using PdfFileMend with Try‑Finally | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind a PDF, add an image stamp to a page, save the modified document, and gua... |
| [add-images-to-pdf-with-error-handling](./add-images-to-pdf-with-error-handling.cs) | Add Images to PDF with Error Handling | `PdfFileMend`, `AddImage`, `Close` | Demonstrates how to insert multiple images into a PDF using Aspose.Pdf's PdfFileMend class while ... |
| [add-multi-line-text-left-margin-page-3](./add-multi-line-text-left-margin-page-3.cs) | Add Multi‑Line Text with Custom Line Spacing to Left Margin ... | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates how to insert a multi‑line text block with custom line spacing into the left margin ... |
| [add-png-image-to-pdf-page](./add-png-image-to-pdf-page.cs) | Add PNG Image to PDF Page Using PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates how to bind an existing PDF with PdfFileMend, insert a PNG image onto page two at sp... |
| [add-promotional-text-multiple-pdf-pages](./add-promotional-text-multiple-pdf-pages.cs) | Add Promotional Text to Multiple PDF Pages | `PdfFileMend`, `BindPdf`, `AddText` | Demonstrates inserting the same promotional text on pages 3, 5, and 7 of a PDF using PdfFileMend.... |
| [add-tiff-image-to-last-pdf-page](./add-tiff-image-to-last-pdf-page.cs) | Add TIFF Image to Last Page of PDF with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Demonstrates using PdfFileMend to bind an existing PDF, determine its last page, and embed a TIFF... |
| [add-word-wrapped-footer-to-pdf](./add-word-wrapped-footer-to-pdf.cs) | Add Word‑by‑Word Wrapped Footer to PDF Pages | `PdfFileStamp`, `FormattedText`, `BindPdf` | Shows how to add a formatted text footer that automatically wraps word‑by‑word across the page wi... |
| [async-add-image-to-pdf](./async-add-image-to-pdf.cs) | Asynchronously Add Image to PDF with PdfFileMend | `PdfFileMend`, `BindPdf`, `AddImage` | Shows how to add an image to a specific page of a PDF file asynchronously by wrapping the synchro... |
| [batch-add-logo-to-pdf-pages](./batch-add-logo-to-pdf-pages.cs) | Batch Add Logo to PDF Pages | `Document`, `PdfFileMend`, `BindPdf` | Shows how to iterate through a folder of PDF files and overlay a PNG company logo at the top‑righ... |
| [batch-extract-images-from-pdfs](./batch-extract-images-from-pdfs.cs) | Batch Extract Images from PDFs with Page‑Based Naming | `SplitToPages`, `BindPdf`, `ExtractImage` | The example splits each PDF into single‑page files, extracts all images from each page, and saves... |
| [configure-word-wrapping-bywords](./configure-word-wrapping-bywords.cs) | Configure Word Wrapping ByWords and Add Wrapped Text | `Document`, `PdfFileMend`, `FormattedText` | Demonstrates enabling word‑wrap, setting the wrap mode to ByWords, and adding a long paragraph th... |
| [extract-images-from-pdf-pages-2-5](./extract-images-from-pdf-pages-2-5.cs) | Extract Images from PDF Pages 2‑5 | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a specific page range in a PDF using Aspose.Pdf.Facades.PdfE... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF to PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract every image from a PDF document and s... |
| [insert-png-signature-image-every-pdf-page](./insert-png-signature-image-every-pdf-page.cs) | Insert PNG Signature Image on Every PDF Page | `Document`, `PdfFileMend`, `BindPdf` | Shows how to use PdfFileMend to add a PNG signature image to the bottom‑left corner of each page ... |
| [overlay-semi-transparent-gif-on-pdf](./overlay-semi-transparent-gif-on-pdf.cs) | Overlay Semi-Transparent GIF onto PDF Image | `PdfFileMend`, `CompositingParameters`, `BlendMode` | Demonstrates how to overlay a semi‑transparent GIF onto an existing PDF page that already contain... |
| [remove-all-images-from-pdf](./remove-all-images-from-pdf.cs) | Remove All Images from a PDF | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Shows how to load a PDF, delete every image on all pages using Aspose.Pdf.Facades, and save the m... |
| [remove-image-from-pdf-page](./remove-image-from-pdf-page.cs) | Remove Image from PDF Page using Aspose.Pdf | `PdfContentEditor`, `BindPdf`, `DeleteImage` | Demonstrates how to delete a specific image identified by its object ID from a given page of a PD... |
| [replace-jpeg-with-bmp-using-replaceimage](./replace-jpeg-with-bmp-using-replaceimage.cs) | Replace JPEG with Higher‑Resolution BMP Using ReplaceImage | `Document`, `Page`, `Image` | Demonstrates how to create a PDF with an embedded JPEG, generate a higher‑resolution BMP in memor... |
| [replace-lowres-images-with-highres-png](./replace-lowres-images-with-highres-png.cs) | Replace Low-Resolution Images with High-Resolution PNGs in P... | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to iterate through PDF pages and replace each embedded image with a high‑resolution PNG... |
| [validate-image-format-before-calling-addimage-to-e...](./validate-image-format-before-calling-addimage-to-ensure-only-jpg-png-gif-bmp-or-tiff-files-are-used.cs) | Validate Image Format Before Calling Addimage To Ensure Only... | `PdfFileMend` | Validate Image Format Before Calling Addimage To Ensure Only Jpg Png Gif Bmp Or Tiff Files Are Used |
| [verify-free-text-annotation-coordinates](./verify-free-text-annotation-coordinates.cs) | Verify Free Text Annotation Coordinates with PdfContentEdito... | `PdfContentEditor`, `Document`, `BindPdf` | Shows how to add a free‑text annotation to a PDF using PdfContentEditor and unit‑test that its re... |

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

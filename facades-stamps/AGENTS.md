---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **48** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (37/48 files) ← category-specific
- `using Aspose.Pdf;` (35/48 files) ← category-specific
- `using Aspose.Pdf.Text;` (14/48 files)
- `using Aspose.Pdf.Annotations;` (2/48 files)
- `using Aspose.Pdf.Drawing;` (1/48 files)
- `using Aspose.Pdf.Forms;` (1/48 files)
- `using System;` (48/48 files)
- `using System.IO;` (47/48 files)
- `using System.Drawing;` (11/48 files)
- `using System.Text;` (1/48 files)

## Common Code Pattern

Most files in this category use `PdfFileStamp` from `Aspose.Pdf.Facades`:

```csharp
PdfFileStamp tool = new PdfFileStamp();
tool.BindPdf("input.pdf");
// ... PdfFileStamp operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-background-watermark-to-specific-pdf-pages](./add-background-watermark-to-specific-pdf-pages.cs) | Add Background Watermark to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to apply a light‑gray background watermark to pages 2‑5 of a PDF using Aspose.Pd... |
| [add-confidential-text-and-logo-stamp](./add-confidential-text-and-logo-stamp.cs) | Add Confidential Text and Logo Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates using Aspose.Pdf.Facades to place a combined formatted text stamp and a logo image o... |
| [add-creation-date-stamp](./add-creation-date-stamp.cs) | Add Creation Date Stamp to PDF | `Document`, `DocumentInfo`, `TextStamp` | Shows how to read a PDF's creation date and place it as a text stamp in the top‑left corner of ev... |
| [add-custom-border-stamp-to-pdf](./add-custom-border-stamp-to-pdf.cs) | Add Custom Border Stamp Annotation to PDF | `Document`, `StampAnnotation`, `Border` | Demonstrates how to load a PDF, create a rubber‑stamp annotation with a custom red border and thi... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `Document`, `Page`, `TextStamp` | Demonstrates creating a TextStamp with interpolated author and date values, applying it to each p... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates creating a TextStamp that shows the PDF's file name in the header of each page and a... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Page`, `TextStamp` | Demonstrates how to insert a footer stamp containing the current date (MM-dd-yyyy) on the last pa... |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Stamp 10 Points Above Bottom Edge | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates how to use PdfFileStamp to add a footer stamp positioned exactly 10 points above the... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer with Page Count to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a footer that displays the current page n... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF with Aspose.Pdf, iterate through its Pages collection, and add a centered... |
| [add-image-stamp-to-pdfs-preserve-filenames](./add-image-stamp-to-pdfs-preserve-filenames.cs) | Add Image Stamp to PDFs and Preserve Filenames | `PdfFileStamp`, `Stamp`, `BindPdf` | Iterates over PDF files in a source folder, applies an image stamp to each page using the PdfFile... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower‑Roman Page Numbers to Odd Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to add page numbers in lower‑case Roman numerals to only the odd pages of a PDF ... |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi-Line Colored Header Stamp to PDF | `BindPdf`, `AddStamp`, `Save` | Demonstrates how to apply a multi‑line header stamp with different font colors to each line using... |
| [add-multi-line-text-watermark-facade](./add-multi-line-text-watermark-facade.cs) | Add Multi-Line Text Watermark to PDF using Facades | `PdfFileStamp`, `FormattedText`, `AddNewLineText` | Shows how to create a multi-line text watermark with Aspose.Pdf.Facades by using FormattedText, a... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi‑Line Text Watermark with Varying Font Sizes | `Document`, `Page`, `FindFont` | Demonstrates how to apply a multi‑line watermark to each page of a PDF, using different font size... |
| [add-multi-line-watermark-custom-font](./add-multi-line-watermark-custom-font.cs) | Add Multi‑Line Watermark with Custom Font to PDF | `Document`, `FormattedText`, `BindLogo` | Demonstrates loading a PDF from a memory stream and applying a multi‑line, custom‑styled watermar... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `Document`, `PdfFileStamp`, `NumberingStyle` | Shows how to use Aspose.Pdf to stamp Arabic numeral page numbers with leading zeros on every page... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Page Number Stamp to PDF | `PdfFileStamp`, `AddPageNumber`, `StartingNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a dynamic page‑number stamp (using the "#... |
| [add-qr-code-stamp-from-pdf-metadata](./add-qr-code-stamp-from-pdf-metadata.cs) | Add QR Code Stamp from PDF Metadata | `Document`, `Stamp`, `PdfFileStamp` | Demonstrates how to generate a QR code from a PDF's metadata and apply it as a stamp on the docum... |
| [add-repeating-background-watermark-image](./add-repeating-background-watermark-image.cs) | Add Repeating Background Watermark Image to PDF Pages | `PdfFileStamp`, `Stamp`, `BindImage` | Shows how to use PdfFileStamp with a single Stamp instance to place a semi‑transparent background... |
| [add-right-aligned-logo-stamp-to-pdf](./add-right-aligned-logo-stamp-to-pdf.cs) | Add Right-Aligned Logo Stamp to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp (logo) at the right margin of each page in a PDF using Aspose.Pdf. |
| [add-semi-transparent-background-stamp](./add-semi-transparent-background-stamp.cs) | Add Semi-Transparent Background Stamp to PDF | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to apply an image as a background stamp on all pages of a PDF with 30% opacity u... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent Confidential Text Stamp to Selected PDF Page... | `Document`, `PdfFileStamp`, `FormattedText` | Demonstrates applying a 70% opaque red "CONFIDENTIAL" text stamp to specific pages of a PDF using... |
| [apply-custom-line-height-stamp](./apply-custom-line-height-stamp.cs) | Apply Custom Line-Height Stamp to PDF | `Stamp`, `PdfFileStamp`, `FormattedText` | Demonstrates how to create a multi‑line watermark with custom line spacing using a formatted text... |
| [apply-external-pdf-template-stamp](./apply-external-pdf-template-stamp.cs) | Apply External PDF Template as Stamp on a Specific Page | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to create a PdfFileStamp from an external template PDF and apply it only to the third p... |
| [apply-image-stamp-to-all-pdf-pages](./apply-image-stamp-to-all-pdf-pages.cs) | Apply Image Stamp to All PDF Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates using PdfFileStamp and Stamp to add a semi‑transparent image stamp to every page of ... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply PDF Page Stamp as Background to All Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades to stamp every page of a PDF with the first page of an... |
| [apply-rotated-image-stamp-to-pdf-pages](./apply-rotated-image-stamp-to-pdf-pages.cs) | Apply Rotated Image Stamp to PDF Pages | `PdfFileStamp`, `Stamp`, `Document` | Demonstrates how to add an image stamp rotated by 30° to the bottom‑right corner of every page in... |
| [apply-rotated-text-stamp-odd-pages](./apply-rotated-text-stamp-odd-pages.cs) | Apply Rotated Text Stamp to Odd Pages | `Document`, `TextStamp`, `FindFont` | Demonstrates adding a 30‑degree rotated text stamp to the left margin of every odd‑numbered page ... |
| [apply-rotating-stamps-to-pdf-pages](./apply-rotating-stamps-to-pdf-pages.cs) | Apply Rotating Stamps to Each PDF Page | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to add a text stamp to every page of a PDF and rotate each stamp based on the pa... |
| ... | | | *and 18 more files* |

## Category Statistics
- Total examples: 48

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.EncodingType`
- `Aspose.Pdf.Facades.FontStyle`
- `Aspose.Pdf.Facades.FormattedText`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfFileStamp`
- `Aspose.Pdf.Facades.PdfFileStamp.AddHeader(System.IO.Stream, int)`
- `Aspose.Pdf.Facades.PdfFileStamp.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfFileStamp.Close()`
- `Aspose.Pdf.Facades.PdfFileStamp.Save(string)`
- `Aspose.Pdf.Facades.Stamp`
- `Aspose.Pdf.Facades.StampInfo`
- `System.Drawing.Color`
- `System.Drawing.Image`

### Rules
- Create a PdfFileStamp instance, bind it to {input_pdf} with BindPdf, then call AddFooter({image_stream}, {int}) to place the image footer on each page, finally Save({output_pdf}) and Close() the stamp object.
- The image for the footer must be provided as a readable Stream (e.g., FileStream opened with FileMode.Open); the integer argument specifies the vertical offset (in points) from the bottom edge of the page.
- Instantiate a PdfFileStamp object, then call BindPdf({input_pdf}) to load the source document.
- Call AddHeader({image_stream}, {int}) on the bound PdfFileStamp to place an image header on each page, where the integer specifies the vertical offset from the top.
- Save the modified document with Save({output_pdf}) and release resources with Close().

### Warnings
- PdfFileStamp belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in newer releases; consider using the Document class with Stamp objects for future compatibility.
- The example does not explicitly dispose the FileStream; callers should ensure proper disposal of streams to avoid resource leaks.
- The example uses a raw FileStream without a using statement; callers should ensure the stream is disposed.
- AddHeader expects the image stream to be positioned at the beginning; callers must reset the stream if reused.
- Method signatures (e.g., SetOrigin) may accept double rather than float; adjust types accordingly.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-stamps patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **47** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (41/47 files) ← category-specific
- `using Aspose.Pdf;` (31/47 files) ← category-specific
- `using Aspose.Pdf.Text;` (13/47 files)
- `using Aspose.Pdf.Annotations;` (1/47 files)
- `using System;` (47/47 files)
- `using System.IO;` (46/47 files)
- `using System.Drawing;` (10/47 files)
- `using System.Collections.Generic;` (1/47 files)
- `using System.Linq;` (1/47 files)

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
| [add-background-image-watermark-to-specific-pdf-pag...](./add-background-image-watermark-to-specific-pdf-pages.cs) | Add Background Image Watermark to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `BindImage` | Demonstrates how to apply an image watermark as a background stamp to pages 2‑5 of a PDF using As... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add Creation Date Stamp to PDF Pages | `Document`, `TextStamp`, `FindFont` | Shows how to read a PDF's creation date (or use the current date) and add it as a text stamp in t... |
| [add-dynamic-page-number-stamp](./add-dynamic-page-number-stamp.cs) | Add Dynamic Page Number Stamp to PDF | `PdfFileStamp`, `AddPageNumber`, `Close` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a text stamp that displays the current pa... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates creating a text stamp with interpolated date and author information and applying it ... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to use Aspose.Pdf.Facades to add a header containing the {file_name} placeholder to eve... |
| [add-footer-date-stamp-to-last-page](./add-footer-date-stamp-to-last-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `TextStamp`, `FindFont` | Shows how to create a text stamp with the current date and place it as a footer on the last page ... |
| [add-footer-stamp-with-bottom-margin](./add-footer-stamp-with-bottom-margin.cs) | Add Footer Stamp with Bottom Margin to PDF | `PdfFileStamp`, `FormattedText`, `EncodingType` | Demonstrates how to add a footer stamp to a PDF using PdfFileStamp and position it 10 points abov... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer with Page Count to PDF | `PdfFileStamp`, `AddFooter`, `Close` | Shows how to use Aspose.Pdf's PdfFileStamp facade to add a footer containing the {page_count} pla... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF, iterate through its pages, and add a centered header stamp containing a ... |
| [add-image-and-text-stamp-to-pdf](./add-image-and-text-stamp-to-pdf.cs) | Add Image and Text Stamp to PDF | `Document`, `Stamp`, `SetOrigin` | Demonstrates how to create a stamp that combines a logo image with custom formatted text and appl... |
| [add-image-and-text-stamp-to-pdf__v2](./add-image-and-text-stamp-to-pdf__v2.cs) | Add Image and Text Stamp to PDF | `PdfFileStamp`, `BindImage`, `BindLogo` | Demonstrates how to use Aspose.Pdf.Facades to place a company logo image and a bold "Confidential... |
| [add-lower-roman-page-numbers-to-odd-pages](./add-lower-roman-page-numbers-to-odd-pages.cs) | Add Lower Roman Page Numbers to Odd Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Shows how to apply lower‑roman page number stamps to odd pages of a PDF using Aspose.Pdf. |
| [add-multi-line-text-watermark-from-memory-stream](./add-multi-line-text-watermark-from-memory-stream.cs) | Add Multi‑Line Text Watermark to PDF from Memory Stream | `Document`, `PdfFileMend`, `FormattedText` | Demonstrates how to create a PDF in memory, load it from a MemoryStream, and apply a multi‑line t... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi-Line Text Watermark Using FormattedText | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to build a FormattedText object with multiple lines using AddNewLineText and apply it a... |
| [add-multi-line-text-watermark__v2](./add-multi-line-text-watermark__v2.cs) | Add Multi-Line Text Watermark with Varying Font Sizes | `Document`, `PdfFileMend`, `FormattedText` | Demonstrates how to use Aspose.Pdf.Facades to add a multi-line text watermark to all pages of a P... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros | `PdfFileStamp`, `BindPdf`, `NumberingStyle` | Demonstrates how to add Arabic numeral page numbers with leading zeros to every page of a PDF usi... |
| [add-qr-code-and-text-stamp-to-pdf](./add-qr-code-and-text-stamp-to-pdf.cs) | Add QR Code and Text Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Shows how to use Aspose.Pdf.Facades to stamp a PDF with a QR code image and a descriptive text la... |
| [add-repeating-background-watermark-to-pdf-pages](./add-repeating-background-watermark-to-pdf-pages.cs) | Add Repeating Background Watermark to PDF Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to use a single Stamp instance as a background watermark that repeats on every p... |
| [add-right-aligned-logo-stamp-to-pdf](./add-right-aligned-logo-stamp-to-pdf.cs) | Add Right-Aligned Logo Stamp to PDF | `Document`, `ImageStamp`, `HorizontalAlignment` | Shows how to place an image stamp (logo) on every page of a PDF and align it to the right margin ... |
| [add-semi-transparent-background-text-stamp](./add-semi-transparent-background-text-stamp.cs) | Add Semi-Transparent Background Text Stamp | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to use Aspose.Pdf.Facades to add a semi‑transparent background text stamp (e.g.,... |
| [add-semi-transparent-png-background-stamp](./add-semi-transparent-png-background-stamp.cs) | Add Semi-Transparent PNG Background Stamp to PDF | `Document`, `Page`, `Stamp` | Demonstrates how to apply a semi‑transparent PNG image as a background stamp that covers the enti... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent Confidential Text Stamp to Selected PDF Page... | `PdfFileStamp`, `Stamp`, `FormattedText` | Shows how to place a semi‑transparent CONFIDENTIAL text stamp on specific pages of a PDF using As... |
| [apply-custom-stamp-annotation](./apply-custom-stamp-annotation.cs) | Apply Custom Stamp Annotation with Border to PDF | `Document`, `Rectangle`, `StampAnnotation` | Demonstrates adding a yellow stamp annotation with a 3‑point border to the first page of a PDF us... |
| [apply-image-stamp-to-all-pdf-pages](./apply-image-stamp-to-all-pdf-pages.cs) | Apply Image Stamp to All PDF Pages Efficiently | `Document`, `PdfFileStamp`, `Stamp` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp with a Stamp object to add a single image stamp ... |
| [apply-multi-line-colored-header-stamp](./apply-multi-line-colored-header-stamp.cs) | Apply Multi‑Line Colored Header Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to add several header lines with different font colors to a PDF document using Aspose.P... |
| [apply-multi-line-text-stamp-1-5-spacing](./apply-multi-line-text-stamp-1-5-spacing.cs) | Apply Multi-Line Text Stamp with 1.5 Line Spacing | `FormattedText`, `AddNewLineText`, `Stamp` | Demonstrates creating a formatted multi-line text watermark with custom line spacing, binding it ... |
| [apply-multi-line-watermark-custom-line-height](./apply-multi-line-watermark-custom-line-height.cs) | Apply Multi-Line Watermark with Custom Line Height | `FormattedText`, `Stamp`, `PdfFileStamp` | Demonstrates creating a formatted text watermark with custom line spacing and applying it as a ba... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply PDF Page Stamp as Background to All Pages | `PdfFileStamp`, `BindPdf`, `AddStamp` | Shows how to use Aspose.Pdf.Facades to add a page from another PDF as a background stamp to every... |
| [apply-pdf-stamp-and-save-to-new-directory](./apply-pdf-stamp-and-save-to-new-directory.cs) | Apply PDF Stamp and Save to New Directory | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates how to apply a stamp from another PDF to each document in a folder and save the stam... |
| [apply-rotated-image-stamp-bottom-right](./apply-rotated-image-stamp-bottom-right.cs) | Apply Rotated Image Stamp to Bottom Right of PDF Pages | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to add a 30‑degree rotated image stamp to the bottom‑right corner of each page i... |
| ... | | | *and 17 more files* |

## Category Statistics
- Total examples: 47

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

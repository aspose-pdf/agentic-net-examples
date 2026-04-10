---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/50 files) ← category-specific
- `using Aspose.Pdf;` (34/50 files) ← category-specific
- `using Aspose.Pdf.Text;` (16/50 files)
- `using Aspose.Pdf.Forms;` (2/50 files)
- `using Aspose.Pdf.Annotations;` (1/50 files)
- `using Aspose.Pdf.Drawing;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (49/50 files)
- `using System.Drawing;` (8/50 files)
- `using System.Text;` (2/50 files)
- `using System.Collections.Generic;` (1/50 files)
- `using System.Linq;` (1/50 files)

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
| [add-background-text-watermark-to-specific-pdf-page...](./add-background-text-watermark-to-specific-pdf-pages.cs) | Add Background Text Watermark to Specific PDF Pages | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates how to use Aspose.Pdf.Facades to place a semi‑transparent background text watermark ... |
| [add-barcode-header-stamp-to-pdf-pages](./add-barcode-header-stamp-to-pdf-pages.cs) | Add Barcode Header Stamp to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates how to add a barcode field as a header on each page of a PDF using Aspose.Pdf, gener... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add Creation Date Stamp to PDF | `PdfFileStamp`, `BindPdf`, `PageHeight` | Demonstrates how to use Aspose.Pdf.Facades to add a formatted date stamp at the top‑left corner o... |
| [add-dynamic-barcode-stamp-to-pdf](./add-dynamic-barcode-stamp-to-pdf.cs) | Add Dynamic Barcode Stamp to PDF | `Document`, `Page`, `Rectangle` | Generates a barcode from a document's unique identifier and stamps it onto the original PDF using... |
| [add-dynamic-page-numbers-to-pdf](./add-dynamic-page-numbers-to-pdf.cs) | Add Dynamic Page Numbers to PDF | `PdfFileStamp`, `AddPageNumber`, `Close` | Demonstrates stamping each page of a PDF with the current page number using Aspose.Pdf's PdfFileS... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF Using Aspose.Pdf Facade | `PdfFileStamp`, `BindPdf`, `FormattedText` | Shows how to create a text stamp with interpolated author and date, set its visual properties, an... |
| [add-file-name-header-stamp](./add-file-name-header-stamp.cs) | Add File Name Header Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to insert a header that automatically displays t... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Save`, `Page` | Demonstrates how to insert a centered footer containing the current date (MM-dd-yyyy) on the last... |
| [add-footer-stamp-10pt-bottom-margin](./add-footer-stamp-10pt-bottom-margin.cs) | Add Footer Stamp with 10‑Point Bottom Margin | `FormattedText`, `BindPdf`, `AddFooter` | Shows how to use PdfFileStamp to add a formatted footer positioned exactly 10 points above the bo... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer with Page Count to PDF | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates stamping a PDF with a footer that automatically displays the current page number and... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to iterate through each page of a PDF document and add a centered header text stamp con... |
| [add-image-and-bold-text-stamp](./add-image-and-bold-text-stamp.cs) | Add Image and Bold Text Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to create a stamp that combines a company logo image with bold "Confidential" te... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower‑Roman Page Numbers to Odd PDF Pages | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to stamp lower‑case Roman numerals on odd‑numbered pages of a PDF using Aspose.P... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi-Line Text Watermark to PDF | `Document`, `PdfFileMend`, `AddText` | Demonstrates how to create a multi‑line text watermark using FormattedText and PdfFileMend, and a... |
| [add-multi-line-watermark-custom-font](./add-multi-line-watermark-custom-font.cs) | Add Multi‑Line Watermark with Custom Font Size and Color | `PdfFileStamp`, `BindPdf`, `AddHeader` | Shows how to load a PDF from a memory stream and apply a semi‑transparent multi‑line text waterma... |
| [add-multi-line-watermark-varying-font-sizes](./add-multi-line-watermark-varying-font-sizes.cs) | Add Multi-Line Watermark with Varying Font Sizes | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to apply a multi-line watermark to a PDF using PdfFileStamp, assigning a differe... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Demonstrates how to add Arabic numeral page numbers with leading zeros to every page of a PDF usi... |
| [add-qr-code-and-text-stamp-to-pdf](./add-qr-code-and-text-stamp-to-pdf.cs) | Add QR Code and Text Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddStamp` | Shows how to create a stamp that combines a QR code image with formatted descriptive text and app... |
| [add-qr-code-stamp-from-pdf-metadata](./add-qr-code-stamp-from-pdf-metadata.cs) | Add QR Code Stamp from PDF Metadata | `Document`, `BindImage`, `SetOrigin` | Demonstrates how to read a PDF's metadata, generate a QR‑code image, and stamp it onto each page ... |
| [add-repeating-background-image-stamp](./add-repeating-background-image-stamp.cs) | Add Repeating Background Image Stamp to PDF | `PdfFileStamp`, `AddStamp`, `Close` | Demonstrates how to use Aspose.Pdf.Facades to apply a single background image stamp as a watermar... |
| [add-rotated-text-stamp-verify](./add-rotated-text-stamp-verify.cs) | Add Rotated Text Stamp and Verify Readability | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates creating a text stamp, rotating it 90 degrees, applying it to all pages of a PDF, sa... |
| [add-translucent-text-watermark-to-pdf-pages](./add-translucent-text-watermark-to-pdf-pages.cs) | Add Translucent Text Watermark to PDF Pages | `Document`, `TextStamp`, `TextState` | Demonstrates how to create a semi‑transparent text stamp and apply it as a watermark to every pag... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent Confidential Text Stamp to Selected PDF Page... | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to apply a 70% opaque red "CONFIDENTIAL" text stamp to specific pages of a PDF u... |
| [align-logo-stamp-to-right-margin](./align-logo-stamp-to-right-margin.cs) | Align Logo Stamp to Right Margin | `Document`, `ImageStamp`, `HorizontalAlignment` | Demonstrates how to add an image stamp (logo) to each page of a PDF and align it to the right mar... |
| [apply-background-image-stamp-30-opacity](./apply-background-image-stamp-30-opacity.cs) | Apply Background Image Stamp with 30% Opacity | `PdfFileStamp`, `Stamp` | Demonstrates how to add a background image stamp to all pages of a PDF with 30% opacity using Asp... |
| [apply-custom-stamp-with-border](./apply-custom-stamp-with-border.cs) | Apply Custom Stamp with Border to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to add a stamp annotation with a red border and custom thickness to every page of a PDF... |
| [apply-multi-line-text-stamp-1-5-spacing](./apply-multi-line-text-stamp-1-5-spacing.cs) | Apply Multi-Line Text Stamp with 1.5 Line Spacing | `PdfFileStamp`, `AddStamp`, `Save` | Demonstrates how to add a multi-line text watermark to a PDF using Aspose.Pdf.Facades, setting li... |
| [apply-multi-line-text-stamp](./apply-multi-line-text-stamp.cs) | Apply Multi-Line Text Stamp to PDF | `PdfFileStamp`, `FormattedText`, `Stamp` | Shows how to create a multi‑line text watermark using FormattedText and Stamp, then apply it to a... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply PDF Page Stamp as Background to All Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades to add a PDF page as a background stamp to every page ... |
| [apply-pdf-stamp-to-multiple-pdfs](./apply-pdf-stamp-to-multiple-pdfs.cs) | Apply PDF Stamp to Multiple PDFs and Save to Output Folder | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to apply a PDF page as a semi‑transparent background stamp to each PDF in a fold... |
| ... | | | *and 20 more files* |

## Category Statistics
- Total examples: 50

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-stamps patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

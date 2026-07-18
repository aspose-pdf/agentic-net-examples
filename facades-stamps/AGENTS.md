---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **48** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (38/48 files) ← category-specific
- `using Aspose.Pdf;` (33/48 files) ← category-specific
- `using Aspose.Pdf.Text;` (19/48 files)
- `using Aspose.Pdf.Drawing;` (2/48 files)
- `using Aspose.Pdf.Annotations;` (1/48 files)
- `using Aspose.Pdf.Forms;` (1/48 files)
- `using System;` (48/48 files)
- `using System.IO;` (44/48 files)
- `using System.Drawing;` (9/48 files)
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
| [add-background-image-watermark-to-specific-pdf-pag...](./add-background-image-watermark-to-specific-pdf-pages.cs) | Add Background Image Watermark to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Demonstrates how to apply an image watermark as a background stamp to pages 2‑5 of a PDF using As... |
| [add-barcode-header-to-pdf-pages](./add-barcode-header-to-pdf-pages.cs) | Add Barcode Header to PDF Pages | `PdfFileStamp`, `AddHeader`, `Close` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to place a pre‑generated barcode image as a head... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add Creation Date Stamp to PDF Pages | `Document`, `TextStamp`, `FindFont` | Demonstrates how to add a text stamp that displays the document's creation date (formatted as yyy... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `FormattedText`, `Stamp`, `BindLogo` | Demonstrates creating a formatted text stamp with dynamic author and date values using string int... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `PdfFileStamp`, `AddHeader`, `Close` | Demonstrates how to use Aspose.Pdf's PdfFileStamp facade to add a header containing the source PD... |
| [add-footer-date-stamp-to-last-page](./add-footer-date-stamp-to-last-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Page`, `TextStamp` | Shows how to add a footer stamp with the current date (MM-dd-yyyy) to only the last page of a PDF... |
| [add-footer-page-count-stamp](./add-footer-page-count-stamp.cs) | Add Footer Page Count Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a footer that automatically displays the ... |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Stamp 10 Points Above Bottom Edge | `PdfFileStamp`, `FormattedText`, `EncodingType` | Demonstrates how to place a footer stamp exactly 10 points above the page bottom using Aspose.Pdf... |
| [add-header-stamp-to-all-pdf-pages](./add-header-stamp-to-all-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `PdfFileStamp`, `Document`, `FormattedText` | Demonstrates iterating through a PDF document and adding a header stamp with a company name to ea... |
| [add-image-and-text-stamp-to-pdf](./add-image-and-text-stamp-to-pdf.cs) | Add Image and Text Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to use Aspose.Pdf.Facades to place a company logo image and a bold "Confidential... |
| [add-lowercase-roman-page-numbers-to-odd-pages](./add-lowercase-roman-page-numbers-to-odd-pages.cs) | Add Lowercase Roman Page Numbers to Odd Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to add page numbers in lowercase Roman numerals to only the odd pages of a PDF u... |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi‑Line Colored Header Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add a multi‑line header to each page of a PDF using Aspose.Pdf, with each lin... |
| [add-multi-line-text-watermark-pdf](./add-multi-line-text-watermark-pdf.cs) | Add Multi-Line Text Watermark to PDF | `BindPdf`, `AddHeader`, `Save` | Shows how to create a multi-line text watermark and add it as a header on every page of a PDF usi... |
| [add-multi-line-watermark-pdf](./add-multi-line-watermark-pdf.cs) | Add Multi-Line Watermark with Line Spacing to PDF | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates using Aspose.Pdf.Facades to apply a multi‑line text watermark with opacity and backg... |
| [add-multi-line-watermark-to-pdf](./add-multi-line-watermark-to-pdf.cs) | Add Multi‑Line Watermark to PDF from Memory Stream | `PdfFileStamp`, `FormattedText`, `Stamp` | Shows how to create a PDF in memory and apply a multi‑line, colored, custom‑size watermark using ... |
| [add-multi-line-watermark-varying-font-sizes](./add-multi-line-watermark-varying-font-sizes.cs) | Add Multi-Line Watermark with Varying Font Sizes | `Document`, `PdfFileMend`, `BindPdf` | Creates a PDF and uses PdfFileMend to add three watermark lines at the top of each page, each wit... |
| [add-page-number-stamp-with-leading-zeros](./add-page-number-stamp-with-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `Document`, `Page`, `TextStamp` | Shows how to add a page‑number stamp formatted with leading zeros to every page of a PDF using As... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers to PDF using PdfFileStamp | `PdfFileStamp`, `AddPageNumber`, `Close` | Demonstrates stamping a PDF with dynamic page numbers using the PdfFileStamp facade and the '#' p... |
| [add-qr-code-verification-stamp](./add-qr-code-verification-stamp.cs) | Add QR Code and Description Stamp to PDF | `Document`, `ImageStamp`, `TextStamp` | Demonstrates how to load a PDF, create an image stamp for a QR code and a text stamp with verific... |
| [add-repeating-background-image-stamp](./add-repeating-background-image-stamp.cs) | Add Repeating Background Image Stamp to PDF | `PdfFileStamp`, `Stamp`, `BindImage` | Demonstrates how to use Aspose.Pdf.Facades to apply a single image stamp as a background watermar... |
| [add-right-aligned-image-stamp-to-pdf](./add-right-aligned-image-stamp-to-pdf.cs) | Add Right-Aligned Image Stamp to PDF | `Document`, `ImageStamp`, `HorizontalAlignment` | Shows how to load a PDF, create an ImageStamp, set its HorizontalAlignment to Right, and apply th... |
| [add-semi-transparent-background-stamp](./add-semi-transparent-background-stamp.cs) | Add Semi-Transparent Background Stamp to PDF | `Stamp`, `PdfFileStamp`, `FormattedText` | Demonstrates creating a formatted text stamp, setting it as a background with 30% opacity, and ap... |
| [add-semi-transparent-png-background-stamp](./add-semi-transparent-png-background-stamp.cs) | Add Semi-Transparent PNG Background Stamp to PDF | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to apply a semi‑transparent PNG image as a background stamp that covers the enti... |
| [add-text-stamp-overlay-to-pdf-pages](./add-text-stamp-overlay-to-pdf-pages.cs) | Add Text Stamp Overlay to PDF Pages | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates how to use the PdfFileStamp facade to place a formatted text stamp on top of existin... |
| [add-transparent-text-stamp-to-selected-pdf-pages](./add-transparent-text-stamp-to-selected-pdf-pages.cs) | Add Transparent Text Stamp to Selected PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to apply a semi‑transparent red text stamp labeled 'CONFIDENTIAL' to specific pa... |
| [apply-custom-colored-stamp-with-border](./apply-custom-colored-stamp-with-border.cs) | Apply Custom Colored Stamp with Border to PDF Pages | `Document`, `Page`, `StampAnnotation` | Demonstrates how to add a rubber‑stamp annotation with custom text, color, and border thickness t... |
| [apply-multi-line-text-stamp-1-5-line-spacing](./apply-multi-line-text-stamp-1-5-line-spacing.cs) | Apply Multi-Line Text Stamp with 1.5 Line Spacing | `FormattedText`, `AddNewLineText`, `Stamp` | Demonstrates creating a multi-line text watermark with 1.5 line spacing using FormattedText, bind... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply PDF Page Stamp as Background to All Pages | `PdfFileStamp`, `BindPdf`, `Stamp` | Shows how to use Aspose.Pdf.Facades to bind a source PDF, create a background stamp from another ... |
| [apply-pdf-stamp-preserve-filenames](./apply-pdf-stamp-preserve-filenames.cs) | Apply PDF Stamp and Preserve Original Filenames | `PdfFileStamp`, `Stamp`, `InputFile` | Demonstrates how to batch‑apply a PDF stamp to each document in a folder using Aspose.Pdf.Facades... |
| [apply-pdf-template-stamp-to-page](./apply-pdf-template-stamp-to-page.cs) | Apply PDF Template Stamp to Specific Page | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp and Stamp to bind a page from an external templa... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

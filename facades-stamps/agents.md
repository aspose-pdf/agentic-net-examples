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

- `using Aspose.Pdf;` (39/46 files) ← category-specific
- `using Aspose.Pdf.Facades;` (31/46 files) ← category-specific
- `using Aspose.Pdf.Text;` (17/46 files)
- `using Aspose.Pdf.Drawing;` (1/46 files)
- `using System;` (46/46 files)
- `using System.IO;` (45/46 files)
- `using System.Drawing;` (9/46 files)
- `using System.Collections.Generic;` (1/46 files)
- `using System.Runtime.InteropServices;` (1/46 files)

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
| [add-background-watermark](./add-background-watermark.cs) | Add Background Watermark to Selected PDF Pages | `PdfFileStamp`, `BindPdf`, `Stamp` | Demonstrates adding a text watermark as a background on pages 2 through 5 of a PDF using Aspose.Pdf. |
| [add-background-watermark__v2](./add-background-watermark__v2.cs) | Add Repeating Background Watermark Image to PDF | `PdfFileStamp`, `BindImage`, `SetImageSize` | Demonstrates how to add a background image watermark to every page of a PDF using a single Stamp ... |
| [add-company-header](./add-company-header.cs) | Add Company Header to Every PDF Page | `PdfFileStamp`, `FormattedText`, `BindPdf` | Demonstrates adding a header stamp with the company name to each page of a PDF using Aspose.Pdf. |
| [add-creation-date-stamp](./add-creation-date-stamp.cs) | Add Creation Date Stamp to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates adding a text stamp with the document's creation date (yyyy‑MM‑dd) to the top‑left c... |
| [add-date-footer-last-page](./add-date-footer-last-page.cs) | Add Date Footer to Last Page of PDF | `Document`, `TextStamp`, `AddStamp` | Demonstrates adding a footer containing the current date (MM-dd-yyyy) to only the last page of a ... |
| [add-dynamic-barcode-header](./add-dynamic-barcode-header.cs) | Add Dynamic Barcode Header to Each PDF Page | `Document`, `AddStamp`, `ImageStamp` | Demonstrates adding a header stamp with a barcode image generated per page using Aspose.Pdf. |
| [add-dynamic-text-stamp](./add-dynamic-text-stamp.cs) | Add Dynamic Text Stamp to PDF Pages | `Document`, `TextStamp`, `AddStamp` | Demonstrates creating a TextStamp with interpolated date and author, and applying it to all pages... |
| [add-file-name-header-stamp](./add-file-name-header-stamp.cs) | Add File Name Header Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates how to add a header stamp that displays the PDF file name using the {file_name} plac... |
| [add-footer-page-count](./add-footer-page-count.cs) | Add Footer with Page Count to PDF | `PdfFileStamp`, `FormattedText`, `BindPdf` | Demonstrates adding a footer stamp that automatically displays the total page count on each page ... |
| [add-footer-stamp-10pt-margin](./add-footer-stamp-10pt-margin.cs) | Add Footer Stamp with 10-Point Bottom Margin | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates how to add a footer stamp positioned exactly 10 points above the bottom edge of each... |
| [add-image-text-stamp](./add-image-text-stamp.cs) | Add Image and Text to a PDF Stamp | `Stamp`, `Document`, `AddStamp` | Demonstrates how to combine a logo image and custom text into a single stamp and apply it to a PD... |
| [add-image-text-stamp__v2](./add-image-text-stamp__v2.cs) | Add Image and Text Stamp to PDF | `Document`, `ImageStamp`, `TextStamp` | Demonstrates how to add a combined stamp containing a company logo image and a bold "Confidential... |
| [add-multi-line-colored-header](./add-multi-line-colored-header.cs) | Add Multi-Line Colored Header to PDF | `PdfFileStamp`, `BindPdf`, `AddHeader` | Demonstrates adding multiple header lines with different font colors to each page of a PDF using ... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi‑Line Text Watermark to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates adding a multi‑line text watermark to a PDF using FormattedText.AddNewLineText and P... |
| [add-multi-line-watermark](./add-multi-line-watermark.cs) | Add Multi-line Text Watermark to PDF from Memory Stream | `Document`, `WatermarkArtifact`, `TextState` | Demonstrates loading a PDF from a memory stream and adding a multi‑line text watermark with custo... |
| [add-multi-line-watermark__v2](./add-multi-line-watermark__v2.cs) | Add Multi‑Line Watermark with Varying Font Sizes | `Document`, `TextStamp`, `TextState` | Demonstrates adding a multi‑line watermark to each page of a PDF, using different font sizes for ... |
| [add-page-number-leading-zeros](./add-page-number-leading-zeros.cs) | Add Page Numbers with Leading Zeros to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Demonstrates adding a page‑number stamp formatted with leading zeros to every page of a PDF using... |
| [add-page-number-stamp](./add-page-number-stamp.cs) | Add Dynamic Page Number Stamp to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add a text stamp that shows the current page number using a placeholder. |
| [add-rotated-text-stamp](./add-rotated-text-stamp.cs) | Add Rotated Text Stamp to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates adding a text stamp rotated 90 degrees to a PDF page and verifies the rotation setting. |
| [add-translucent-watermark](./add-translucent-watermark.cs) | Add Translucent Watermark to PDF Using Stamp Opacity | `PdfFileStamp`, `Stamp` | Demonstrates how to apply a 50% opaque stamp as a translucent watermark on all pages of a PDF. |
| [add-transparent-text-stamp](./add-transparent-text-stamp.cs) | Add Transparent Text Stamp with Opacity to PDF | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates adding a semi‑transparent text stamp to selected pages of a PDF using Aspose.Pdf. |
| [align-logo-stamp-right](./align-logo-stamp-right.cs) | Align Logo Stamp to Right Margin | `ImageStamp`, `Document`, `AddStamp` | Demonstrates how to add an image stamp (logo) to a PDF and align it to the right margin using Asp... |
| [apply-background-stamp](./apply-background-stamp.cs) | Apply Background Stamp with 30% Opacity to PDF | `PdfFileStamp`, `BindImage`, `IsBackground` | Demonstrates adding a semi‑transparent background stamp to a PDF using Aspose.Pdf.Facades. |
| [apply-image-stamp](./apply-image-stamp.cs) | Apply Image Stamp to PDFs and Save to New Directory | `PdfFileStamp`, `AddStamp`, `Close` | Demonstrates adding an image stamp to each PDF in a source folder and saving the modified files t... |
| [apply-multi-line-text-stamp](./apply-multi-line-text-stamp.cs) | Apply Multi-line Text Stamp with Custom Line Spacing | `Document`, `Page`, `FormattedText` | Demonstrates adding a text stamp with multiple lines and equal custom spacing between them to eac... |
| [apply-multiline-text-stamp](./apply-multiline-text-stamp.cs) | Apply Multi-line Text Stamp with Custom Line Spacing | `Document`, `Page`, `TextStamp` | Demonstrates adding a multi‑line text stamp to each page of a PDF with a line spacing of 1.5 for ... |
| [apply-overlay-text-stamp](./apply-overlay-text-stamp.cs) | Apply Overlay Text Stamp to PDF | `PdfFileStamp`, `Stamp`, `IsBackground` | Demonstrates how to set a stamp's IsBackground property to false so it overlays existing PDF cont... |
| [apply-page-stamp-background](./apply-page-stamp-background.cs) | Apply PDF Page Stamp as Background to All Pages | `PdfFileStamp`, `BindPdf`, `IsBackground` | Demonstrates how to add a PDF page stamp as a background to every page of a document using Aspose... |
| [apply-rubber-stamp](./apply-rubber-stamp.cs) | Apply Rubber Stamp with Custom Border Thickness and Color | `PdfContentEditor`, `Document`, `Annotation` | Demonstrates adding a rubber stamp annotation with a custom color and then setting its border thi... |
| [apply-stamp-even-pages](./apply-stamp-even-pages.cs) | Apply Stamp to Even Pages of PDF | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates adding a text stamp only to even-numbered pages of a PDF using Aspose.Pdf. |
| ... | | | *and 16 more files* |

## Category Statistics
- Total examples: 46

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **82** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (37/82 files)
- `using Aspose.Pdf;` (35/82 files)
- `using Aspose.Pdf.Text;` (14/82 files)
- `using Aspose.Pdf.Annotations;` (2/82 files)
- `using Aspose.Pdf.Drawing;` (1/82 files)
- `using Aspose.Pdf.Forms;` (1/82 files)
- `using System;` (48/82 files)
- `using System.IO;` (47/82 files)
- `using System.Drawing;` (11/82 files)
- `using System.Text;` (1/82 files)

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
| [add-background-image-watermark-to-specific-pdf-pag...](./add-background-image-watermark-to-specific-pdf-pages.cs) | Add background image watermark to specific pdf pages |  | Add background image watermark to specific pdf pages |
| [add-background-watermark-to-specific-pdf-pages](./add-background-watermark-to-specific-pdf-pages.cs) | Add Background Watermark to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to apply a light‑gray background watermark to pages 2‑5 of a PDF using Aspose.Pd... |
| [add-barcode-header-to-pdf-pages](./add-barcode-header-to-pdf-pages.cs) | Add barcode header to pdf pages |  | Add barcode header to pdf pages |
| [add-confidential-text-and-logo-stamp](./add-confidential-text-and-logo-stamp.cs) | Add Confidential Text and Logo Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates using Aspose.Pdf.Facades to place a combined formatted text stamp and a logo image o... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add creation date stamp to pdf |  | Add creation date stamp to pdf |
| [add-creation-date-stamp](./add-creation-date-stamp.cs) | Add Creation Date Stamp to PDF | `Document`, `DocumentInfo`, `TextStamp` | Shows how to read a PDF's creation date and place it as a text stamp in the top‑left corner of ev... |
| [add-custom-border-stamp-to-pdf](./add-custom-border-stamp-to-pdf.cs) | Add Custom Border Stamp Annotation to PDF | `Document`, `StampAnnotation`, `Border` | Demonstrates how to load a PDF, create a rubber‑stamp annotation with a custom red border and thi... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `Document`, `Page`, `TextStamp` | Demonstrates creating a TextStamp with interpolated author and date values, applying it to each p... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates creating a TextStamp that shows the PDF's file name in the header of each page and a... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Page`, `TextStamp` | Demonstrates how to insert a footer stamp containing the current date (MM-dd-yyyy) on the last pa... |
| [add-footer-page-count-stamp](./add-footer-page-count-stamp.cs) | Add footer page count stamp |  | Add footer page count stamp |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Stamp 10 Points Above Bottom Edge | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates how to use PdfFileStamp to add a footer stamp positioned exactly 10 points above the... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer with Page Count to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a footer that displays the current page n... |
| [add-header-stamp-to-all-pdf-pages](./add-header-stamp-to-all-pdf-pages.cs) | Add header stamp to all pdf pages |  | Add header stamp to all pdf pages |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF with Aspose.Pdf, iterate through its Pages collection, and add a centered... |
| [add-image-and-text-stamp-to-pdf](./add-image-and-text-stamp-to-pdf.cs) | Add image and text stamp to pdf |  | Add image and text stamp to pdf |
| [add-image-stamp-to-pdfs-preserve-filenames](./add-image-stamp-to-pdfs-preserve-filenames.cs) | Add Image Stamp to PDFs and Preserve Filenames | `PdfFileStamp`, `Stamp`, `BindPdf` | Iterates over PDF files in a source folder, applies an image stamp to each page using the PdfFile... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower‑Roman Page Numbers to Odd Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to add page numbers in lower‑case Roman numerals to only the odd pages of a PDF ... |
| [add-lowercase-roman-page-numbers-to-odd-pages](./add-lowercase-roman-page-numbers-to-odd-pages.cs) | Add lowercase roman page numbers to odd pages |  | Add lowercase roman page numbers to odd pages |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi-Line Colored Header Stamp to PDF | `BindPdf`, `AddStamp`, `Save` | Demonstrates how to apply a multi‑line header stamp with different font colors to each line using... |
| [add-multi-line-text-watermark-facade](./add-multi-line-text-watermark-facade.cs) | Add Multi-Line Text Watermark to PDF using Facades | `PdfFileStamp`, `FormattedText`, `AddNewLineText` | Shows how to create a multi-line text watermark with Aspose.Pdf.Facades by using FormattedText, a... |
| [add-multi-line-text-watermark-pdf](./add-multi-line-text-watermark-pdf.cs) | Add multi line text watermark pdf |  | Add multi line text watermark pdf |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi‑Line Text Watermark with Varying Font Sizes | `Document`, `Page`, `FindFont` | Demonstrates how to apply a multi‑line watermark to each page of a PDF, using different font size... |
| [add-multi-line-watermark-custom-font](./add-multi-line-watermark-custom-font.cs) | Add Multi‑Line Watermark with Custom Font to PDF | `Document`, `FormattedText`, `BindLogo` | Demonstrates loading a PDF from a memory stream and applying a multi‑line, custom‑styled watermar... |
| [add-multi-line-watermark-pdf](./add-multi-line-watermark-pdf.cs) | Add multi line watermark pdf |  | Add multi line watermark pdf |
| [add-multi-line-watermark-to-pdf](./add-multi-line-watermark-to-pdf.cs) | Add multi line watermark to pdf |  | Add multi line watermark to pdf |
| [add-multi-line-watermark-varying-font-sizes](./add-multi-line-watermark-varying-font-sizes.cs) | Add multi line watermark varying font sizes |  | Add multi line watermark varying font sizes |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `Document`, `PdfFileStamp`, `NumberingStyle` | Shows how to use Aspose.Pdf to stamp Arabic numeral page numbers with leading zeros on every page... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Page Number Stamp to PDF | `PdfFileStamp`, `AddPageNumber`, `StartingNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a dynamic page‑number stamp (using the "#... |
| [add-page-number-stamp-with-leading-zeros](./add-page-number-stamp-with-leading-zeros.cs) | Add page number stamp with leading zeros |  | Add page number stamp with leading zeros |
| ... | | | *and 52 more files* |

## Category Statistics
- Total examples: 82

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

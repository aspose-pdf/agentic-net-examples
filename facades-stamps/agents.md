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

- `using Aspose.Pdf.Facades;` (34/45 files) ← category-specific
- `using Aspose.Pdf;` (28/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (15/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (45/45 files)
- `using System.Drawing;` (13/45 files)
- `using System.Collections.Generic;` (2/45 files)
- `using System.Linq;` (2/45 files)
- `using System.Runtime.InteropServices;` (2/45 files)
- `using System.Drawing.Imaging;` (1/45 files)

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
| [add-background-text-stamp-30-opacity](./add-background-text-stamp-30-opacity.cs) | Add Background Text Stamp with 30% Opacity | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to place a semi‑transparent text stamp behind the content of each page in a PDF ... |
| [add-background-watermark-stamp-to-pdf-pages](./add-background-watermark-stamp-to-pdf-pages.cs) | Add Background Watermark Stamp to Specific PDF Pages | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates how to create a background watermark stamp using Aspose.Pdf.Facades and apply it to ... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add Creation Date Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Shows how to read a PDF's creation date and place it as a text stamp in the top‑left corner of ea... |
| [add-custom-border-stamp-to-pdf-page](./add-custom-border-stamp-to-pdf-page.cs) | Add Custom Border Stamp to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateSquareCircle` | Shows how to use PdfContentEditor to create a square annotation with a custom border thickness an... |
| [add-disclaimer-stamp-to-first-page](./add-disclaimer-stamp-to-first-page.cs) | Add Disclaimer Stamp to First Page of PDF | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp and Stamp to overlay a disclaimer PDF onto the f... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Demonstrates loading a PDF, creating a TextStamp with interpolated date and author, configuring i... |
| [add-file-name-header-stamp](./add-file-name-header-stamp.cs) | Add File Name Header Stamp to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to place a text stamp in the header of each PDF page that automatically shows th... |
| [add-footer-date-stamp-to-last-page](./add-footer-date-stamp-to-last-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Page`, `TextStamp` | Shows how to place a footer stamp with the current date (MM-dd-yyyy) on the last page of a PDF us... |
| [add-footer-stamp-10pt-above-bottom](./add-footer-stamp-10pt-above-bottom.cs) | Add Footer Stamp 10 Points Above Bottom Edge | `PdfFileStamp`, `FormattedText`, `AddFooter` | Demonstrates how to place a footer stamp exactly 10 points above the bottom edge of each page by ... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer with Automatic Page Count to PDF | `PdfFileStamp`, `BindPdf`, `AddFooter` | Shows how to use PdfFileStamp to add a footer containing the {page_count} placeholder, which Aspo... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF, iterate through each page, and add a centered header stamp containing a ... |
| [add-logo-confidential-stamp-to-pdf](./add-logo-confidential-stamp-to-pdf.cs) | Add Logo and Confidential Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates using Aspose.Pdf.Facades to place a company logo image together with bold 'Confident... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower‑Roman Page Numbers to Odd Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to stamp lower‑case Roman numerals as page numbers on only the odd pages of a PD... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi-Line Text Watermark to PDF Using FormattedText | `FormattedText`, `PdfFileStamp`, `Document` | Demonstrates creating a multi-line formatted text watermark with Aspose.Pdf.Facades and applying ... |
| [add-multi-line-watermark-pdffilemend](./add-multi-line-watermark-pdffilemend.cs) | Add Multi‑Line Watermark with Different Font Styles using Pd... | `Document`, `PdfFileMend`, `FormattedText` | Demonstrates how to create a three‑line watermark on every PDF page using Aspose.Pdf.Facades.Form... |
| [add-page-number-stamp-arabic-numerals](./add-page-number-stamp-arabic-numerals.cs) | Add Page Number Stamp with Arabic Numerals | `PdfFileStamp`, `BindPdf`, `NumberingStyle` | Demonstrates how to use Aspose.Pdf's PdfFileStamp facade to add sequential page numbers formatted... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Page Number Stamp to PDF | `PdfFileStamp`, `BindPdf`, `AddPageNumber` | Shows how to use Aspose.Pdf.Facades.PdfFileStamp to add a dynamic page number stamp to each page ... |
| [add-qr-code-text-stamp-to-pdf](./add-qr-code-text-stamp-to-pdf.cs) | Add QR Code and Text Stamp to PDF | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to place a QR‑code image together with descriptive text on each page of a PDF us... |
| [add-repeating-background-image-watermark](./add-repeating-background-image-watermark.cs) | Add Repeating Background Image Watermark to PDF | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to use a single Stamp instance to place a semi‑transparent background image on e... |
| [add-right-aligned-logo-stamp-to-pdf-pages](./add-right-aligned-logo-stamp-to-pdf-pages.cs) | Add Right-Aligned Logo Stamp to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place an image stamp (logo) on every page of a PDF and align it to the right margin ... |
| [add-rotated-image-stamp-to-first-page](./add-rotated-image-stamp-to-first-page.cs) | Add Rotated Image Stamp to First Page of PDFs | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to batch‑process PDF files in a directory and apply a semi‑transparent, 45‑degree rotat... |
| [add-semi-transparent-png-background-stamp](./add-semi-transparent-png-background-stamp.cs) | Add Semi-Transparent PNG Background Stamp to PDF | `PdfFileStamp`, `BindPdf`, `Stamp` | Demonstrates how to use Aspose.Pdf.Facades to place a semi‑transparent PNG image as a background ... |
| [add-translucent-text-watermark-to-pdf-pages](./add-translucent-text-watermark-to-pdf-pages.cs) | Add Translucent Text Watermark to PDF Pages | `Document`, `TextStamp`, `FindFont` | Shows how to create a 50% opaque text stamp and apply it as a watermark on every page of a PDF do... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent CONFIDENTIAL Text Stamp to Selected PDF Page... | `FormattedText`, `Stamp`, `BindLogo` | Demonstrates how to place a semi‑transparent CONFIDENTIAL text stamp on specific pages of a PDF u... |
| [apply-image-stamp-to-all-pdf-pages](./apply-image-stamp-to-all-pdf-pages.cs) | Apply Image Stamp to All PDF Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to use PdfFileStamp and Stamp to add a single image stamp to every page of a PDF effici... |
| [apply-image-stamp-to-pdf](./apply-image-stamp-to-pdf.cs) | Apply Image Stamp to PDF with Overlay Control | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates creating a PDF in memory, generating an image stamp, binding both via streams, setti... |
| [apply-image-stamp-to-pdfs](./apply-image-stamp-to-pdfs.cs) | Apply Image Stamp to PDFs and Save to New Folder | `PdfFileStamp`, `Stamp`, `SetOrigin` | Demonstrates loading PDF files from a directory, applying an image stamp to every page with Aspos... |
| [apply-multi-line-colored-header-stamp](./apply-multi-line-colored-header-stamp.cs) | Apply Multi-Line Colored Header Stamp to PDF | `PdfFileStamp`, `FormattedText`, `EncodingType` | Demonstrates how to add a multi‑line header to a PDF using Aspose.Pdf.Facades, with each line ren... |
| [apply-multi-line-text-stamp](./apply-multi-line-text-stamp.cs) | Apply Multi-Line Text Stamp with Custom Line Spacing | `FormattedText`, `AddNewLineText`, `BindLogo` | Demonstrates creating a formatted text stamp with multiple lines and 1.5 point extra line spacing... |
| [apply-multi-line-watermark-custom-line-height](./apply-multi-line-watermark-custom-line-height.cs) | Apply Multi-Line Watermark with Custom Line Height | `PdfFileStamp`, `SetOrigin`, `BindLogo` | Demonstrates creating a stamp containing multiple lines of text with equal line spacing and apply... |
| ... | | | *and 15 more files* |

## Category Statistics
- Total examples: 45

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
Updated: 2026-05-08 | Run: `20260508_144637_f72c91`
<!-- AUTOGENERATED:END -->

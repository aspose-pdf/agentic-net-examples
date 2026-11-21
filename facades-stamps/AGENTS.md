---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (47/50 files) ← category-specific
- `using Aspose.Pdf.Text;` (27/50 files) ← category-specific
- `using Aspose.Pdf.Facades;` (18/50 files)
- `using Aspose.Pdf.Drawing;` (6/50 files)
- `using Aspose.Pdf.Forms;` (3/50 files)
- `using Aspose.Pdf.Annotations;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)
- `using System.Drawing;` (1/50 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-background-watermark-to-specific-pdf-pages](./add-background-watermark-to-specific-pdf-pages.cs) | Add Background Text Watermark to Selected PDF Pages | `TextStamp`, `Document`, `AddStamp` | Demonstrates how to create a semi‑transparent background text stamp and apply it to pages 2 throu... |
| [add-confidential-text-and-logo-stamp](./add-confidential-text-and-logo-stamp.cs) | Add Image and Text Stamps to PDF Pages | `Document`, `ImageStamp`, `TextStamp` | Demonstrates how to place a company logo image and a bold 'Confidential' text stamp on every page... |
| [add-creation-date-stamp](./add-creation-date-stamp.cs) | Add Creation Date Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to read a PDF's creation date and add it as a text stamp in the top‑left corner of each... |
| [add-custom-border-stamp-to-pdf](./add-custom-border-stamp-to-pdf.cs) | Apply Custom Border Stamp to PDF Pages | `Document`, `Page`, `TextAnnotation` | Shows how to add a text annotation with a colored, thick border to each page of a PDF to highligh... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF Pages | `Document`, `TextStamp`, `AddStamp` | Demonstrates creating a TextStamp with dynamic content using string interpolation and applying it... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to create a text stamp that shows the source PDF's file name and add it as a cen... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `TextStamp`, `TextState` | Shows how to insert a footer stamp with the current date (MM-dd-yyyy) on the last page of a PDF u... |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Image Stamp 10 Points Above Bottom | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp as a footer exactly 10 points above the bottom edge of e... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer Stamp with Page Count to PDF | `Document`, `Page`, `TextStamp` | Shows how to add a footer text stamp that automatically displays the current page number and tota... |
| [add-header-barcode-stamp](./add-header-barcode-stamp.cs) | Add Header Barcode Stamp to PDF Pages | `Document`, `Rectangle`, `TextBoxField` | Shows how to create a header stamp that inserts a dynamically generated barcode image on each pag... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF with Aspose.Pdf, create a TextStamp as a header, and apply it to every pa... |
| [add-image-stamp-to-pdfs-preserve-filenames](./add-image-stamp-to-pdfs-preserve-filenames.cs) | Add Image Stamp to PDFs and Save to Output Folder | `Document`, `ImageStamp`, `AddStamp` | The example loads each PDF from a source folder, applies an image stamp to every page, and saves ... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower-Roman Page Numbers to Odd Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add page numbers in lower‑case Roman numerals to only the odd pages of a PDF ... |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi-Line Colored Header Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Shows how to create a multi-line header stamp with different font colors using Aspose.Pdf and app... |
| [add-multi-line-text-watermark-facade](./add-multi-line-text-watermark-facade.cs) | Add Multi-Line Text Watermark to PDF | `FormattedText`, `AddNewLineText`, `Stamp` | Demonstrates how to create a multi‑line text watermark using FormattedText and apply it to every ... |
| [add-multi-line-text-watermark-with-line-spacing](./add-multi-line-text-watermark-with-line-spacing.cs) | Add Multi-Line Text Watermark with Custom Line Spacing | `Document`, `TextStamp`, `TextState` | Loads a PDF, creates a multi-line TextStamp, sets line spacing to 1.5 × font size, and applies a ... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi‑Line Watermark with Varying Font Sizes | `Document`, `Page`, `WatermarkArtifact` | Loads a PDF and adds three centered watermark lines—"CONFIDENTIAL", "Do Not Distribute", and "Int... |
| [add-multi-line-watermark-custom-font](./add-multi-line-watermark-custom-font.cs) | Add Multi‑Line Watermark with Custom Font Size and Color to ... | `Document`, `Page`, `TextStamp` | Demonstrates loading a PDF from a memory stream, creating a multi‑line text stamp with custom fon... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamps with Leading Zeros | `Document`, `Page`, `TextStamp` | Demonstrates how to add Arabic numeral page‑number stamps with leading zeros to every page of a P... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Dynamic Page Number Text Stamp to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to create a TextStamp with the {page_number} placeholder and apply it to every p... |
| [add-qr-code-stamp-from-pdf-metadata](./add-qr-code-stamp-from-pdf-metadata.cs) | Add QR Code Stamp from PDF Metadata | `Document`, `DocumentInfo`, `Page` | Shows how to extract a PDF's metadata, generate a QR‑code barcode from it, and place the barcode ... |
| [add-repeating-background-watermark-image](./add-repeating-background-watermark-image.cs) | Add Repeating Background Image Stamp to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to create a single ImageStamp and apply it as a semi‑transparent background wate... |
| [add-right-aligned-logo-stamp-to-pdf](./add-right-aligned-logo-stamp-to-pdf.cs) | Add Right-Aligned Logo Stamp to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF, create an image stamp for a logo, align it to the right margin, a... |
| [add-semi-transparent-background-stamp](./add-semi-transparent-background-stamp.cs) | Add Background Image Stamp with 30% Opacity | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp behind the content of each PDF page with 30% opacity, co... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent Text Stamp to Selected PDF Pages | `Document`, `TextStamp`, `FindFont` | Shows how to place a semi‑transparent text stamp (e.g., CONFIDENTIAL) on specific pages of a PDF ... |
| [apply-custom-line-height-stamp](./apply-custom-line-height-stamp.cs) | Apply Text Stamp with Custom Line Height to PDF | `Document`, `TextState`, `TextStamp` | Demonstrates adding a multi‑line text watermark to every page of a PDF using a TextStamp with cus... |
| [apply-external-pdf-template-stamp](./apply-external-pdf-template-stamp.cs) | Apply External PDF Stamp to Third Page | `Stamp`, `PdfFileStamp`, `BindPdf` | Demonstrates how to create a PdfFileStamp from an external PDF template and apply it only to the ... |
| [apply-image-stamp-to-all-pdf-pages__v2](./apply-image-stamp-to-all-pdf-pages__v2.cs) | Apply Image Stamp to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp, and efficiently apply the stamp to every page usin... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply PDF Page Stamp as Background to All Pages | `Document`, `Page`, `PdfPageStamp` | Shows how to load a source PDF and a stamp PDF, create a PdfPageStamp from the stamp page, set it... |
| [apply-rotated-image-stamp-to-pdf-pages](./apply-rotated-image-stamp-to-pdf-pages.cs) | Apply Rotated Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp rotated by 30 degrees to the bottom‑right corner of every ... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-stamps patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-11 | Run: `20261111_221500_fa849f`
<!-- AUTOGENERATED:END -->

---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **49** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (46/49 files) ← category-specific
- `using Aspose.Pdf.Text;` (25/49 files) ← category-specific
- `using Aspose.Pdf.Facades;` (18/49 files)
- `using Aspose.Pdf.Drawing;` (7/49 files)
- `using Aspose.Pdf.Annotations;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (49/49 files)

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
| [add-background-watermark-to-specific-pdf-pages](./add-background-watermark-to-specific-pdf-pages.cs) | Add Background Text Watermark to Selected PDF Pages | `TextStamp`, `TextState`, `FontRepository` | Demonstrates how to create a semi‑transparent background text stamp and apply it to pages 2‑5 of ... |
| [add-confidential-text-and-logo-stamp](./add-confidential-text-and-logo-stamp.cs) | Add Logo and Confidential Stamps to PDF | `Document`, `ImageStamp`, `TextStamp` | Loads a PDF, adds an image stamp for a company logo and a bold red "Confidential" text stamp to e... |
| [add-creation-date-stamp](./add-creation-date-stamp.cs) | Add Creation Date Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Shows how to read a PDF's creation date and place it as a text stamp on the top‑left corner of ev... |
| [add-custom-border-stamp-to-pdf](./add-custom-border-stamp-to-pdf.cs) | Apply Custom Border Stamp to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to add a text annotation with a custom border thickness and color to each page of a PDF... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Shows how to load a PDF, create a TextStamp with the current date and author using string interpo... |
| [add-file-name-header-stamp-to-pdf](./add-file-name-header-stamp-to-pdf.cs) | Add File Name Header Stamp to PDF | `Document`, `HeaderFooter`, `TextFragment` | Demonstrates how to add a header stamp that displays the PDF's file name using the {file_name} pl... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Page`, `TextStamp` | Demonstrates how to insert a footer stamp containing the current date (MM-dd-yyyy) on the last pa... |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Image Stamp 10 Points Above Bottom Edge | `Document`, `ImageStamp`, `AddStamp` | The example loads a PDF, creates an ImageStamp configured as a footer, sets its vertical alignmen... |
| [add-footer-with-page-count](./add-footer-with-page-count.cs) | Add Footer Stamp with Page Count to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to add a footer text stamp that automatically displays the current page number a... |
| [add-header-barcode-stamp-to-pdf-pages](./add-header-barcode-stamp-to-pdf-pages.cs) | Add Header Barcode Stamp to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to generate a barcode image and place it as a header stamp on each page of a PDF using ... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF, iterate through its Pages collection, and add a centered header TextStam... |
| [add-image-stamp-to-pdfs-preserve-filenames](./add-image-stamp-to-pdfs-preserve-filenames.cs) | Apply Image Stamp to PDFs and Save to New Directory | `Document`, `ImageStamp`, `AddStamp` | Shows how to load PDF files from a folder, add an image stamp to every page, and write the stampe... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower‑Roman Page Numbers to Odd Pages | `Document`, `TextStamp`, `TextState` | Demonstrates how to add lower‑case Roman numeral page numbers to only the odd pages of a PDF usin... |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi-Line Colored Header Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Shows how to apply a multi-line header stamp to each page of a PDF, with each line displayed in a... |
| [add-multi-line-text-stamp-with-line-spacing](./add-multi-line-text-stamp-with-line-spacing.cs) | Add Multi-Line Text Stamp with 1.5 Line Spacing | `TextStamp`, `TextState`, `FindFont` | Loads a PDF, creates a multi-line TextStamp, configures font, size, style, color and a line spaci... |
| [add-multi-line-text-watermark-facade](./add-multi-line-text-watermark-facade.cs) | Add Multi-Line Text Watermark to PDF Pages | `Document`, `WatermarkArtifact`, `TextState` | Shows how to create a multi-line text watermark using WatermarkArtifact and apply it to every pag... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi-Line Watermark with Varying Font Sizes | `Document`, `Page`, `TextFragment` | Loads a PDF, iterates over each page, and adds three centered text fragments as a watermark, each... |
| [add-multi-line-watermark-custom-font](./add-multi-line-watermark-custom-font.cs) | Add Multi‑Line Watermark with Custom Font to PDF | `Document`, `Page`, `WatermarkArtifact` | Demonstrates loading a PDF from a memory stream and applying a multi‑line watermark with a custom... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add a page‑number stamp formatted with leading zeros to every page of a PDF u... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Dynamic Page Number Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Demonstrates how to create a TextStamp with the {page_number} placeholder, apply it to every page... |
| [add-repeating-background-watermark-image](./add-repeating-background-watermark-image.cs) | Add Background Image Stamp to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to create a single ImageStamp and reuse it to apply a semi‑transparent backgroun... |
| [add-right-aligned-logo-stamp-to-pdf](./add-right-aligned-logo-stamp-to-pdf.cs) | Add Right-Aligned Logo Stamp to PDF Pages | `ImageStamp`, `Document`, `AddStamp` | Demonstrates how to place an image stamp (logo) at the right‑hand side of each page in a PDF usin... |
| [add-semi-transparent-background-stamp](./add-semi-transparent-background-stamp.cs) | Add Background Image Stamp with 30% Opacity | `ImageStamp`, `Document`, `AddStamp` | Demonstrates how to place an image stamp as a background on every page of a PDF using Aspose.Pdf,... |
| [add-transparent-confidential-text-stamp](./add-transparent-confidential-text-stamp.cs) | Add Transparent Confidential Text Stamp to Selected PDF Page... | `Document`, `TextStamp`, `FindFont` | Demonstrates how to apply a semi‑transparent 'CONFIDENTIAL' text stamp with 70% opacity to specif... |
| [apply-custom-line-height-stamp](./apply-custom-line-height-stamp.cs) | Apply Multi-Line Watermark with Custom Line Height | `Document`, `Page`, `TextStamp` | Shows how to add several TextStamp watermarks to a PDF page and control the vertical spacing betw... |
| [apply-external-pdf-template-stamp](./apply-external-pdf-template-stamp.cs) | Apply External PDF Stamp to Page 3 | `Stamp`, `PdfFileStamp`, `BindPdf` | Demonstrates creating a stamp from an external PDF template and overlaying it on the third page o... |
| [apply-image-stamp-to-all-pdf-pages](./apply-image-stamp-to-all-pdf-pages.cs) | Apply Image Stamp to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create a single ImageStamp, and efficiently add that stamp to every page... |
| [apply-pdf-page-stamp-as-background](./apply-pdf-page-stamp-as-background.cs) | Apply Image Stamp as Background to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp, set it as a background, and add it to every page b... |
| [apply-rotated-image-stamp-to-pdf-pages](./apply-rotated-image-stamp-to-pdf-pages.cs) | Add Rotated Image Stamp to Bottom-Right of PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp, rotating it 30°, aligning it ... |
| [apply-rotated-text-stamp-odd-pages](./apply-rotated-text-stamp-odd-pages.cs) | Apply Rotated Text Stamp to Odd Pages | `Document`, `TextStamp`, `FindFont` | Shows how to add a 30‑degree rotated text stamp to the left margin of every odd‑numbered page in ... |
| ... | | | *and 19 more files* |

## Category Statistics
- Total examples: 49

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
Updated: 2026-10-21 | Run: `20261021_060620_83f0b8`
<!-- AUTOGENERATED:END -->

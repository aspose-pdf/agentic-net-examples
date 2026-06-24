---
name: facades-stamps
description: C# examples for facades-stamps using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-stamps

> **Facades stamps** in PDF using C# / .NET -- **49** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-stamps** category.
This folder contains standalone C# examples for facades-stamps operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-stamps**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/49 files) ← category-specific
- `using Aspose.Pdf;` (35/49 files) ← category-specific
- `using Aspose.Pdf.Text;` (13/49 files)
- `using Aspose.Pdf.Annotations;` (1/49 files)
- `using Aspose.Pdf.Drawing;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (47/49 files)
- `using System.Drawing;` (10/49 files)
- `using System.Collections.Generic;` (2/49 files)
- `using System.Drawing.Imaging;` (1/49 files)
- `using System.Net.Http;` (1/49 files)
- `using System.Text;` (1/49 files)
- `using System.Web;` (1/49 files)

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
| [add-background-watermark-to-specific-pdf-pages](./add-background-watermark-to-specific-pdf-pages.cs) | Add Background Watermark to Specific PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to create a background text watermark using Aspose.Pdf.Facades and apply it to p... |
| [add-barcode-stamp](./add-barcode-stamp.cs) | Add Barcode Stamp to PDF | `Document`, `Barcode`, `PdfFileStamp` | Creates a sample PDF, generates a barcode from the document's unique identifier, and stamps the b... |
| [add-creation-date-stamp-to-pdf](./add-creation-date-stamp-to-pdf.cs) | Add Creation Date Stamp to PDF Pages | `Document`, `TextStamp`, `FindFont` | Demonstrates how to read a PDF's creation date (or use the current date) and add it as a text sta... |
| [add-dynamic-text-stamp-to-pdf](./add-dynamic-text-stamp-to-pdf.cs) | Add Dynamic Text Stamp to PDF Using Aspose.Pdf Facade | `PdfFileStamp`, `FormattedText`, `Stamp` | Shows how to create a text stamp that includes the current date and author via string interpolati... |
| [add-footer-date-stamp-to-last-pdf-page](./add-footer-date-stamp-to-last-pdf-page.cs) | Add Footer Date Stamp to Last PDF Page | `Document`, `Stamp`, `PdfFileStamp` | Demonstrates how to insert a footer containing the current date (MM-dd-yyyy) on the last page of ... |
| [add-footer-stamp-10-points-above-bottom](./add-footer-stamp-10-points-above-bottom.cs) | Add Footer Stamp 10 Points Above Bottom Edge | `PdfFileStamp`, `BindPdf`, `AddFooter` | Demonstrates using Aspose.Pdf.Facades.PdfFileStamp to add a footer stamp to each page of a PDF an... |
| [add-footer-with-automatic-page-count](./add-footer-with-automatic-page-count.cs) | Add Footer with Automatic Page Count to PDF | `PdfFileStamp`, `Document`, `FormattedText` | Shows how to use Aspose.Pdf.Facades to add a gray footer and dynamic page numbering (Page # of {p... |
| [add-header-barcode-image-to-pdf-pages](./add-header-barcode-image-to-pdf-pages.cs) | Add Header Barcode Image to Each PDF Page | `PdfFileStamp`, `Stamp`, `BindImage` | Demonstrates how to generate a barcode‑like image for each page and add it as a header stamp to a... |
| [add-header-stamp-to-pdf-pages](./add-header-stamp-to-pdf-pages.cs) | Add Header Stamp to All PDF Pages | `Document`, `TextStamp`, `FindFont` | Shows how to iterate through a PDF document's pages and add a centered header stamp containing a ... |
| [add-lower-roman-page-numbers-odd-pages](./add-lower-roman-page-numbers-odd-pages.cs) | Add Lower-Roman Page Numbers to Odd Pages | `PdfFileStamp`, `Document`, `NumberingStyle` | Demonstrates how to stamp lower‑case Roman numerals as page numbers on only the odd‑numbered page... |
| [add-multi-line-colored-header-stamp](./add-multi-line-colored-header-stamp.cs) | Add Multi‑Line Colored Header Stamp to PDF | `PdfFileStamp`, `FormattedText`, `Stamp` | Demonstrates how to create a multi‑line header stamp with different font colors using Aspose.Pdf.... |
| [add-multi-line-text-watermark](./add-multi-line-text-watermark.cs) | Add Multi‑Line Text Watermark to PDF | `Document`, `Page`, `FormattedText` | Creates a sample PDF and adds a multi‑line text watermark to each page using FormattedText and St... |
| [add-multi-line-text-watermark__v2](./add-multi-line-text-watermark__v2.cs) | Add Multi‑Line Text Watermark with Varying Font Sizes | `PdfFileStamp`, `Stamp`, `FormattedText` | Shows how to apply a multi‑line text watermark to a PDF using Aspose.Pdf.Facades, with each line ... |
| [add-multi-line-watermark](./add-multi-line-watermark.cs) | Add Multi‑Line Text Watermark to PDF from Memory Stream | `Document`, `WatermarkArtifact`, `Color` | Demonstrates creating a PDF, loading it from a memory stream, and adding a multi‑line text waterm... |
| [add-page-number-stamp-leading-zeros](./add-page-number-stamp-leading-zeros.cs) | Add Page Number Stamp with Leading Zeros to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to add a page‑number stamp formatted with leading zeros to every page of a PDF u... |
| [add-page-number-stamp-to-pdf](./add-page-number-stamp-to-pdf.cs) | Add Page Number Stamp to PDF | `TextStamp`, `HorizontalAlignment`, `VerticalAlignment` | Demonstrates creating a TextStamp with the {page_number} placeholder and applying it to every pag... |
| [add-qr-code-stamp-from-pdf-metadata](./add-qr-code-stamp-from-pdf-metadata.cs) | Add QR Code Stamp from PDF Metadata | `Document`, `Stamp`, `SetOrigin` | The example reads a PDF's metadata, generates a QR‑code image via an online service, and stamps t... |
| [add-qr-code-text-stamp-to-pdf](./add-qr-code-text-stamp-to-pdf.cs) | Add QR Code and Text Stamp to PDF for Product Verification | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates stamping a PDF with a QR code image and accompanying verification text using Aspose.... |
| [add-repeating-background-watermark-stamp](./add-repeating-background-watermark-stamp.cs) | Add Repeating Background Watermark Stamp to PDF | `PdfFileStamp`, `Stamp`, `BindImage` | Demonstrates how to use Aspose.Pdf.Facades to apply a single background stamp containing a waterm... |
| [add-right-aligned-logo-stamp-to-pdf-pages](./add-right-aligned-logo-stamp-to-pdf-pages.cs) | Add Right-Aligned Logo Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp (logo) on every page of a PDF and align it to the right ... |
| [add-rotated-text-stamp-to-pdf-pages](./add-rotated-text-stamp-to-pdf-pages.cs) | Add Rotated Text Stamp to PDF Pages | `Document`, `PdfFileStamp`, `FormattedText` | Shows how to create a formatted text stamp, rotate it 90 degrees, and apply it to all pages of a ... |
| [add-transparent-text-stamp-to-selected-pdf-pages](./add-transparent-text-stamp-to-selected-pdf-pages.cs) | Add Transparent Text Stamp to Selected PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Shows how to create a formatted text stamp with 70% opacity and apply it as a watermark to specif... |
| [apply-50-percent-opacity-text-watermark](./apply-50-percent-opacity-text-watermark.cs) | Apply 50% Opacity Text Watermark to PDF Pages | `PdfFileStamp`, `Stamp`, `FormattedText` | Demonstrates how to add a translucent text watermark to all pages of a PDF using PdfFileStamp and... |
| [apply-background-image-stamp-30-opacity](./apply-background-image-stamp-30-opacity.cs) | Apply Background Image Stamp with 30% Opacity | `PdfFileStamp`, `BindPdf`, `Stamp` | Demonstrates adding an image stamp as a background to all pages of a PDF and setting its opacity ... |
| [apply-background-stamp-to-all-pdf-pages](./apply-background-stamp-to-all-pdf-pages.cs) | Apply Background Stamp to All PDF Pages | `PdfFileStamp`, `BindPdf`, `AddStamp` | Demonstrates using Aspose.Pdf.Facades to add a stamp as a background on every page of a PDF docum... |
| [apply-custom-rubber-stamp-border](./apply-custom-rubber-stamp-border.cs) | Apply Custom Rubber Stamp with Border to PDF | `PdfContentEditor`, `BindPdf`, `CreateRubberStamp` | Shows how to add a rubber‑stamp annotation to a PDF page using PdfContentEditor and then customiz... |
| [apply-image-stamp-to-all-pdf-pages](./apply-image-stamp-to-all-pdf-pages.cs) | Apply Image Stamp to All PDF Pages | `PdfFileStamp`, `Stamp`, `BindPdf` | Shows how to use PdfFileStamp and Stamp from Aspose.Pdf.Facades to add a single image stamp to ev... |
| [apply-multi-line-watermark-custom-line-height](./apply-multi-line-watermark-custom-line-height.cs) | Apply Multi-Line Watermark with Custom Line Height | `PdfFileStamp`, `FormattedText`, `Stamp` | Demonstrates creating a multi-line text stamp with explicit line spacing and adding it as a semi-... |
| [apply-pdf-template-stamp-to-page](./apply-pdf-template-stamp-to-page.cs) | Apply PDF Template Stamp to Specific Page | `PdfFileStamp`, `Stamp`, `AddStamp` | Demonstrates how to use Aspose.Pdf.Facades to stamp a PDF with an external template PDF, applying... |
| [apply-rotated-image-stamp-bottom-right](./apply-rotated-image-stamp-bottom-right.cs) | Apply Rotated Image Stamp to Bottom Right of PDF Pages | `PdfFileInfo`, `GetPageWidth`, `GetPageHeight` | Demonstrates how to add an image stamp rotated 30° to the bottom‑right corner of each page in a P... |
| ... | | | *and 19 more files* |

## Category Statistics
- Total examples: 49

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Facades.EncodingType`
- `Aspose.Pdf.Facades.FormattedText`
- `Aspose.Pdf.Facades.PdfFileStamp`
- `Aspose.Pdf.Facades.Stamp`

### Rules
- When both 'using Aspose.Pdf;' and 'using Aspose.Pdf.Facades;' are imported, ALWAYS fully qualify Stamp as Aspose.Pdf.Facades.Stamp to avoid CS0104 ambiguity with Aspose.Pdf.Stamp.
- Aspose.Pdf.Facades.Stamp does NOT have SetText(), SetFont(), SetFontSize(), SetTextColor(), SetColor(), or SetOpacity() methods. To add text, create a FormattedText object and bind it using stamp.BindLogo(formattedText). For opacity, use the Opacity property directly: stamp.Opacity = 0.5f.
- FormattedText does NOT have settable properties (Text, Font, FontSize, TextState, TextColor, FontColor). ALL text content and styling must be set via the constructor: new FormattedText(text, System.Drawing.Color, fontName, EncodingType.Winansi, embedded, fontSize). The color parameter is System.Drawing.Color, NOT Aspose.Pdf.Color.
- To append additional lines to FormattedText after construction, use AddNewLineText(). There is no AddText() method.
- To bind formatted text to a Stamp, use stamp.BindLogo(formattedText). Stamp.SetText(FormattedText) does not exist.

### Warnings
- Do not confuse Aspose.Pdf.Facades.Stamp with Aspose.Pdf.Stamp — they are different classes in different namespaces.
- Do not confuse PdfPageStamp (core API) with Aspose.Pdf.Facades.Stamp (Facades API) — they are not interchangeable.
- FormattedText uses System.Drawing.Color for colors, not Aspose.Pdf.Color.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-stamps patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

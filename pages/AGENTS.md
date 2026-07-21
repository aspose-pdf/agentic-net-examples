---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

> **Pages** in PDF using C# / .NET -- **100** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **pages** category.
This folder contains standalone C# examples for pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (100/100 files) ← category-specific
- `using Aspose.Pdf.Text;` (17/100 files)
- `using Aspose.Pdf.Facades;` (7/100 files)
- `using Aspose.Pdf.Annotations;` (4/100 files)
- `using Aspose.Pdf.Drawing;` (1/100 files)
- `using System;` (100/100 files)
- `using System.IO;` (96/100 files)
- `using System.Collections.Generic;` (6/100 files)
- `using System.Linq;` (3/100 files)
- `using System.Text;` (1/100 files)
- `using System.Text.Json;` (1/100 files)

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
| [add-alphanumeric-bates-numbering-to-pdf](./add-alphanumeric-bates-numbering-to-pdf.cs) | Add Alphanumeric Bates Numbering to PDF Pages | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Shows how to apply Bates numbering with a custom alphanumeric prefix to every page of a PDF docum... |
| [add-bates-numbering-increment-5](./add-bates-numbering-increment-5.cs) | Add Bates Numbering with Increment of 5 to PDF Pages | `Document`, `Page`, `BatesNArtifact` | Shows how to batch‑process PDF files, adding a Bates numbering artifact to each page with a step ... |
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Demonstrates how to insert Bates numbering stamps starting at 1000 with a dash suffix on all page... |
| [add-bates-numbering-to-pdf](./add-bates-numbering-to-pdf.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Demonstrates loading a PDF with Aspose.Pdf, applying year‑based Bates numbering in the format "20... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `Pages`, `AddBatesNumbering` | Demonstrates how to apply Bates numbering to each page of a PDF using Aspose.Pdf, with a custom p... |
| [add-blank-front-matter-page-with-label-i](./add-blank-front-matter-page-with-label-i.cs) | Add Blank Front-Matter Page with Custom Label "i" | `Document`, `Page`, `PageLabel` | The example loads an existing PDF, inserts a blank page at the beginning, assigns a custom page l... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to load a PDF, iterate through its pages, and add a centered bold uppercase head... |
| [add-curved-text-watermark-to-pdf-page](./add-curved-text-watermark-to-pdf-page.cs) | Add Curved Text Watermark to PDF Page | `Document`, `Page`, `WatermarkArtifact` | Demonstrates opening a PDF, creating a WatermarkArtifact with styled text, positioning it at the ... |
| [add-custom-page-numbers-to-pdf](./add-custom-page-numbers-to-pdf.cs) | Add Custom Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to load a PDF, create a PageNumberStamp with a custom "Page X of Y" format, appl... |
| [add-diagonal-text-watermark-to-pdf-pages](./add-diagonal-text-watermark-to-pdf-pages.cs) | Add Diagonal Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to place a semi‑transparent diagonal text watermark on every page of a PDF using Aspose... |
| [add-dynamic-year-watermark-to-pdf-pages](./add-dynamic-year-watermark-to-pdf-pages.cs) | Add Dynamic Year Watermark to PDF Pages | `Document`, `AddStamp`, `TextStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating a TextStamp that includes the current year, ... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Save`, `Page` | Shows how to insert a text footer with the current generation date on every page of a PDF using A... |
| [add-header-with-logo-to-pdf-pages](./add-header-with-logo-to-pdf-pages.cs) | Add Header with Logo Image to PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to insert a left‑aligned header containing a logo image on every page of an exis... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HtmlFragment` | Shows how to insert an HTML fragment styled with CSS as a header on the first three pages of a PD... |
| [add-image-footer-30-opacity](./add-image-footer-30-opacity.cs) | Add Image Footer with 30% Opacity to PDF Pages | `Document`, `Page`, `FooterArtifact` | Shows how to add an image footer with 30% opacity to every page of a PDF document using Aspose.Pdf. |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with Opacity to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF, iterate through its pages, and apply an ImageStamp with 20% opacity as a... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to All PDF Pages | `Document`, `Page`, `Color` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, set a LightGray background co... |
| [add-multiple-empty-pages-sequentially](./add-multiple-empty-pages-sequentially.cs) | Add Multiple Empty Pages Sequentially | `Document`, `Pages`, `Add` | Demonstrates how to add a series of empty pages to a PDF document by iterating over a list of pag... |
| [add-page-numbers-leading-zeros](./add-page-numbers-leading-zeros.cs) | Add Page Numbers with Leading Zeros to PDF | `Document`, `TextStamp`, `TextState` | Shows how to insert sequential page numbers padded with a leading zero on each page of a PDF usin... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert page numbers only on even pages of a PDF using Aspose.Pdf's PageNumberStamp. |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `Page`, `PageNumberStamp` | Shows how to load a PDF with Aspose.Pdf, loop through its pages, and apply a page number stamp on... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Centered Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert a page number stamp that starts at 1 and is centered on every page of a PDF u... |
| [add-page-numbers-with-custom-embedded-font](./add-page-numbers-with-custom-embedded-font.cs) | Add Page Numbers with Custom Embedded Font | `Document`, `FindFont`, `Font` | Demonstrates how to insert page numbers on each PDF page using a TrueType font loaded from an ext... |
| [add-page-numbers-with-custom-font](./add-page-numbers-with-custom-font.cs) | Add Page Numbers with Custom Font to PDF | `Document`, `PageNumberStamp`, `FontRepository` | Demonstrates how to insert page numbers on every PDF page using a custom Arial 14‑point font with... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image watermark repeatedly in a grid across each p... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to apply a PNG image as a watermark on each page of a PDF, scaling it to half the page ... |
| [add-scaled-image-footer-to-pdf-pages](./add-scaled-image-footer-to-pdf-pages.cs) | Add Scaled Image Footer to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to place a footer image, scaled to a fixed height, at the bottom of every page in a PDF... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark with Outline to PDF Page... | `Document`, `TextStamp`, `FindFont` | Shows how to place a semi‑transparent text watermark with an outline on each page of a PDF using ... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Page Numbers to PDF Pages | `Document`, `Page`, `TextFragment` | Creates a PDF and adds a page‑number stamp to each page, formatted as superscript (smaller font s... |
| [add-text-header-to-first-pdf-page](./add-text-header-to-first-pdf-page.cs) | Add Text Header to First PDF Page | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a header with custom top margin to the first page of a PDF using Aspose.P... |
| ... | | | *and 70 more files* |

## Category Statistics
- Total examples: 100

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BackgroundArtifact`
- `Aspose.Pdf.ColorType`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Pages`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.Page.GetPageRect(bool)`
- `Aspose.Pdf.PageCollection`
- `Aspose.Pdf.PageCollection.Add`
- `Aspose.Pdf.Rotation`
- `Aspose.Pdf.Text.TextFragment`

### Rules
- Load a PDF into a {doc} using new Document({input_pdf}).
- Delete a particular page by invoking {doc}.Pages.Delete({int}) where the integer is the 1‑based page number.
- Persist the changes by calling {doc}.Save({output_pdf}).
- Instantiate a {doc} by calling new Document({input_pdf}) to load a PDF file.
- Read the total number of pages via {doc}.Pages.Count after the document is successfully loaded.

### Warnings
- The Delete method expects a 1‑based page index and will throw if the index is out of range.
- The helper method RunExamples.GetDataDir_AsposePdf_Pages() is external to this snippet and must provide a valid directory path.
- The added page inherits the default page size of the document; specify size explicitly if a different layout is required.
- The Add method copies all pages; selective page ranges require additional filtering.
- If {output_pdf} already exists it will be overwritten without warning.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

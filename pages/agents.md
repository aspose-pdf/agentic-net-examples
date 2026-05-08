---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **pages** category.
This folder contains standalone C# examples for pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (99/99 files) ← category-specific
- `using Aspose.Pdf.Text;` (18/99 files)
- `using Aspose.Pdf.Facades;` (6/99 files)
- `using Aspose.Pdf.Annotations;` (3/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (97/99 files)
- `using System.Collections.Generic;` (7/99 files)
- `using System.Linq;` (4/99 files)
- `using System.Text.Json;` (1/99 files)

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
| [add-bates-numbering-stamp-to-pdf-pages](./add-bates-numbering-stamp-to-pdf-pages.cs) | Add Bates Numbering Stamp to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Shows how to insert a Bates numbering stamp that starts at 1000 and adds a dash suffix on every p... |
| [add-bates-numbering-to-pdf](./add-bates-numbering-to-pdf.cs) | Add Bates Numbering to PDF Pages | `Document`, `Save`, `Pages` | Demonstrates how to load a PDF, apply year‑based Bates numbering in the format "2026-####" to eve... |
| [add-bates-numbering-with-alphanumeric-prefix](./add-bates-numbering-with-alphanumeric-prefix.cs) | Add Bates Numbering with Alphanumeric Prefix to PDF | `Document`, `AddBatesNumbering`, `BatesNumbering` | Shows how to load a PDF using Aspose.Pdf, apply Bates numbering that includes an alphanumeric pre... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to apply Bates numbering to every page of a PDF using Aspose.Pdf, with a custom ... |
| [add-blank-front-matter-page-with-custom-label](./add-blank-front-matter-page-with-custom-label.cs) | Add Blank Front-Matter Page with Custom Label to PDF | `Document`, `Insert`, `PageLabel` | Loads an existing PDF, inserts a blank page at the beginning, and assigns a custom page label "i"... |
| [add-bold-section-header-to-pdf-pages](./add-bold-section-header-to-pdf-pages.cs) | Add Bold Section Header to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to loop through each page of a PDF and insert a centered, bold, uppercase header using ... |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Insert Chapter-Numbered Page Stamps in PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add page numbers with a custom "Chapter" prefix to every page of a PDF using ... |
| [add-curved-text-watermark-to-pdf-pages](./add-curved-text-watermark-to-pdf-pages.cs) | Add Curved Text Watermark to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to place a repeated text watermark that follows a quadratic Bezier curve across ... |
| [add-diagonal-repeating-text-watermark](./add-diagonal-repeating-text-watermark.cs) | Add Diagonal Repeating Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to overlay a semi‑transparent, diagonal repeating text watermark on every page of a PDF... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to insert a text footer with the current date on every page of a PDF document using Asp... |
| [add-header-logo-to-pdf-pages](./add-header-logo-to-pdf-pages.cs) | Add Header Logo Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF with Aspose.Pdf, add a company logo as an image stamp aligned to t... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to insert an HTML‑styled header, including embedded CSS, on the first three page... |
| [add-image-footer-with-opacity-to-pdf-pages](./add-image-footer-with-opacity-to-pdf-pages.cs) | Add Image Footer with Opacity to PDF Pages | `Document`, `Page`, `FooterArtifact` | Demonstrates how to add an image footer with 30% opacity to every page of a PDF using Aspose.Pdf. |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with 20% opacity, center it on each page, apply it ... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to All PDF Pages | `Document`, `Page`, `Color` | Shows how to load a PDF, iterate through each page, set a LightGray background color for branding... |
| [add-multiple-empty-pages-sequentially](./add-multiple-empty-pages-sequentially.cs) | Add Multiple Empty Pages Sequentially | `Document`, `Pages`, `Add` | Demonstrates how to add a series of blank pages to a PDF document using Aspose.Pdf by iterating o... |
| [add-page-numbers-arial-font-pdf](./add-page-numbers-arial-font-pdf.cs) | Add Page Numbers with Arial Font to PDF | `Document`, `PageNumberStamp`, `FindFont` | Demonstrates how to insert sequential page numbers on each PDF page using a custom Arial font at ... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert page numbers only on even pages of a PDF using Aspose.Pdf in C#. |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert page numbers only on odd pages of a PDF using Aspose.Pdf's PageNumberStamp an... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers to PDF Using Aspose.Pdf | `Document`, `Page`, `PageNumberStamp` | Shows how to insert a centered page number stamp starting at 1 on every page of a PDF document wi... |
| [add-page-numbers-to-pdf__v2](./add-page-numbers-to-pdf__v2.cs) | Add Page Numbers (Page X of Y) to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to load a PDF, create a PageNumberStamp with a custom "Page # of #" format, appl... |
| [add-page-numbers-with-custom-separator](./add-page-numbers-with-custom-separator.cs) | Add Page Numbers with '/' Separator to PDF | `Document`, `Page`, `PageNumberStamp` | Loads a PDF, iterates through each page, and adds a page number stamp formatted as "current/total... |
| [add-page-numbers-with-custom-ttf-font](./add-page-numbers-with-custom-ttf-font.cs) | Add Page Numbers Using Custom TTF Font | `Document`, `FindFont`, `Font` | Loads a PDF, embeds an external TrueType font, and adds page numbers to each page using a PageNum... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, iterate over its pages, and tile a semi‑transparent imag... |
| [add-roman-numeral-page-numbers-to-intro-pages](./add-roman-numeral-page-numbers-to-intro-pages.cs) | Add Roman Numeral Page Numbers to Introductory PDF Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to load a PDF with Aspose.Pdf, apply a PageNumberStamp configured for uppercase ... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place a semi‑transparent image watermark, rotated 45°, scaled to half the page size ... |
| [add-scaled-image-footer-to-pdf-pages](./add-scaled-image-footer-to-pdf-pages.cs) | Add Scaled Image Footer to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to insert a footer image on every page of a PDF, scaling it to a fraction of the page w... |
| [add-semi-transparent-text-watermark-to-pdf-pages](./add-semi-transparent-text-watermark-to-pdf-pages.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add a semi‑transparent text watermark with an outline to each page of a PDF u... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Page Numbers to PDF Pages | `Document`, `PageNumberStamp`, `FindFont` | Loads a PDF, creates a PageNumberStamp with a smaller font to simulate superscript, places it at ... |
| [add-text-header-to-first-pdf-page](./add-text-header-to-first-pdf-page.cs) | Add Text Header to First PDF Page | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a text header to the first page of an existing PDF using Aspose.Pdf, conf... |
| ... | | | *and 69 more files* |

## Category Statistics
- Total examples: 99

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

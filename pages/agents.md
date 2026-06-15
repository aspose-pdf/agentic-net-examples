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

- `using Aspose.Pdf;` (100/100 files) ← category-specific
- `using Aspose.Pdf.Text;` (21/100 files)
- `using Aspose.Pdf.Facades;` (5/100 files)
- `using Aspose.Pdf.Annotations;` (2/100 files)
- `using Aspose.Pdf.Drawing;` (1/100 files)
- `using Aspose.Pdf.Operators;` (1/100 files)
- `using System;` (100/100 files)
- `using System.IO;` (98/100 files)
- `using System.Collections.Generic;` (5/100 files)
- `using System.Linq;` (2/100 files)
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
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `AddBatesNumbering`, `BatesNumberingArtifact` | Shows how to apply Bates numbering starting at 1000 with a dash suffix to every page of a PDF usi... |
| [add-bates-numbering-to-pdf-pages__v2](./add-bates-numbering-to-pdf-pages__v2.cs) | Add Bates Numbering (2026-####) to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Loads a PDF document, applies Bates numbering with the format "2026-####" to each page, and saves... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `Pages`, `AddBatesNumbering` | Shows how to load a PDF, apply Bates numbering with a custom "DOC" prefix and "-2026" suffix, and... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Alphanumeric Prefix to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Shows how to load a PDF, apply Bates numbering with a custom alphanumeric prefix, and save the up... |
| [add-blank-front-matter-page-with-custom-label](./add-blank-front-matter-page-with-custom-label.cs) | Add Blank Front-Matter Page with Custom Label | `Document`, `Insert`, `PageLabel` | Demonstrates how to insert a blank page at the beginning of a PDF and assign it a custom page lab... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to PDF Pages | `Document`, `Page`, `HeaderFooter` | Loads an existing PDF, iterates over each page, and adds a header containing bold, uppercase text... |
| [add-centered-page-numbers-to-pdf](./add-centered-page-numbers-to-pdf.cs) | Add Centered Page Numbers to PDF Pages | `Document`, `PageNumberStamp`, `HorizontalAlignment` | Shows how to insert a centered page number stamp that starts at 1 on every page of a PDF using As... |
| [add-chapter-prefixed-page-numbers](./add-chapter-prefixed-page-numbers.cs) | Add Chapter‑Prefixed Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to insert page numbers with a custom "Chapter" prefix into every page of a PDF u... |
| [add-date-footer-to-pdf-pages](./add-date-footer-to-pdf-pages.cs) | Add Date Footer to Each PDF Page | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a text footer containing the current date to every page of an existing PD... |
| [add-diagonal-text-watermark-to-pdf-pages](./add-diagonal-text-watermark-to-pdf-pages.cs) | Add Diagonal Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to place a semi‑transparent diagonal text watermark on every page of a PDF document usi... |
| [add-header-logo-image-to-pdf-pages](./add-header-logo-image-to-pdf-pages.cs) | Add Header Logo Image to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to insert a company logo as a left‑aligned header on every page of a PDF using A... |
| [add-header-to-first-pdf-page-using-margininfo](./add-header-to-first-pdf-page-using-margininfo.cs) | Add Header to First PDF Page Using MarginInfo | `Document`, `HeaderFooter`, `MarginInfo` | Loads an existing PDF, creates a HeaderFooter with a default MarginInfo, adds a text fragment as ... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to insert an HTML header with embedded CSS styling onto the first three pages of... |
| [add-image-footer-30-opacity-to-pdf-pages](./add-image-footer-30-opacity-to-pdf-pages.cs) | Add Image Footer with 30% Opacity to PDF Pages | `Document`, `Page`, `FooterArtifact` | Shows how to place an image footer with 30% opacity on every page of a PDF document using Aspose.... |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent image (e.g., a logo) on every page of a PDF using ... |
| [add-leading-zero-page-numbers-to-pdf](./add-leading-zero-page-numbers-to-pdf.cs) | Add Leading-Zero Page Numbers to PDF | `Document`, `PageNumberStamp`, `FontRepository` | Loads a PDF, creates a PageNumberStamp with a format that pads single‑digit page numbers with a l... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to PDF Pages | `Document`, `Page`, `Color` | Demonstrates how to set a LightGray background color for every page in a PDF using Aspose.Pdf. |
| [add-multiple-empty-pages-to-pdf](./add-multiple-empty-pages-to-pdf.cs) | Add Multiple Empty Pages to PDF Sequentially | `Document`, `PageCollection`, `Add` | Shows how to load an existing PDF with Aspose.Pdf, iterate over a list of page counts, add the sp... |
| [add-page-numbers-custom-format-pdf](./add-page-numbers-custom-format-pdf.cs) | Add Page Numbers (Page X of Y) to PDF | `Document`, `Page`, `PageNumberStamp` | Loads a PDF, creates a PageNumberStamp with the format "Page X of Y", positions it at the bottom ... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to load a PDF with Aspose.Pdf, iterate over even-numbered pages, and add a page-... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert page numbers only on odd-numbered pages of a PDF using Aspose.Pdf. The exampl... |
| [add-page-numbers-with-custom-font](./add-page-numbers-with-custom-font.cs) | Add Page Numbers with Custom Arial Font to PDF | `Document`, `PageNumberStamp`, `FontRepository` | Demonstrates how to load a PDF, create a PageNumberStamp, set the font to Arial size 14, and appl... |
| [add-page-numbers-with-custom-ttf-font](./add-page-numbers-with-custom-ttf-font.cs) | Add Page Numbers Using a Custom TTF Font | `Document`, `FontRepository`, `Font` | Loads a PDF, embeds an external TrueType font, and adds page numbers to every page using a PageNu... |
| [add-page-numbers-with-total-to-pdf](./add-page-numbers-with-total-to-pdf.cs) | Add Page Numbers with Total Count to PDF | `Document`, `PageNumberStamp`, `FontRepository` | Loads a PDF, creates a PageNumberStamp using a custom "current/total" format, applies it to every... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image watermark repeatedly in a grid pattern acros... |
| [add-rotated-image-watermark-to-pdf](./add-rotated-image-watermark-to-pdf.cs) | Add Rotated Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, create an ImageStamp, scale it to half the page size, rotate it 4... |
| [add-scaled-image-footer-to-pdf-pages](./add-scaled-image-footer-to-pdf-pages.cs) | Add Scaled Image Footer to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, iterate through its pages, and add a proportionally scaled footer image ... |
| [add-semi-transparent-text-watermark-outline](./add-semi-transparent-text-watermark-outline.cs) | Add Semi-Transparent Text Watermark with Outline to PDF Page... | `Document`, `Page`, `TextStamp` | The example loads a PDF, iterates through each page, and adds a centered text stamp as a watermar... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Page Numbers to PDF Pages | `Document`, `PageNumberStamp`, `FontRepository` | Loads a PDF, creates a PageNumberStamp with a smaller font and upward offset to simulate superscr... |
| [add-transparent-separator-page](./add-transparent-separator-page.cs) | Add Transparent Separator Page to PDF | `Document`, `Page`, `Transparent` | Shows how to append an empty page with a fully transparent background to an existing PDF document... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->

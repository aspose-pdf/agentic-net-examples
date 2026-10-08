---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

> **Pages** in PDF using C# / .NET -- **92** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **pages** category.
This folder contains standalone C# examples for pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (92/92 files) ← category-specific
- `using Aspose.Pdf.Text;` (26/92 files)
- `using Aspose.Pdf.Annotations;` (4/92 files)
- `using Aspose.Pdf.Drawing;` (3/92 files)
- `using System;` (92/92 files)
- `using System.IO;` (91/92 files)
- `using System.Collections.Generic;` (9/92 files)
- `using System.Drawing;` (2/92 files)
- `using System.Linq;` (1/92 files)
- `using System.Text;` (1/92 files)

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
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `AddStamp`, `TextStamp` | Shows how to insert year‑based Bates numbers (e.g., 2026‑0001) on each page of a PDF using Aspose... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Custom Bates Numbering to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Demonstrates applying sequential Bates numbers with a custom prefix and suffix to each page of a ... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Prefix to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to apply sequential Bates numbers with an alphanumeric prefix to each page of a ... |
| [add-blank-front-matter-page-roman-label](./add-blank-front-matter-page-roman-label.cs) | Add Blank Page and Custom Page Label to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a blank page into an existing PDF with Aspose.Pdf and explains how to assign ... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to Each PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through its pages, and add a centered bol... |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Add Chapter Page Numbers to PDF | `Document`, `Page`, `TextFragment` | Shows how to open a PDF with Aspose.Pdf, loop through each page, and insert a custom "Chapter" pr... |
| [add-curved-text-watermark-to-pdf-pages](./add-curved-text-watermark-to-pdf-pages.cs) | Add Curved Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to generate a quadratic Bézier curve and place semi‑transparent TextStamp object... |
| [add-custom-page-numbers-to-pdf](./add-custom-page-numbers-to-pdf.cs) | Insert Page Numbers in PDF (Page X of Y) | `Document`, `Page`, `TextFragment` | Shows how to add a custom footer with the format "Page X of Y" to every page of a PDF using Aspos... |
| [add-diagonal-text-watermark-to-pdf-pages](./add-diagonal-text-watermark-to-pdf-pages.cs) | Add Diagonal Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to place a semi‑transparent diagonal text watermark on every page of a PDF using Aspose... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Loads a PDF, creates a text fragment with the current date, places it as a footer on each page, a... |
| [add-header-logo-image-to-pdf-pages](./add-header-logo-image-to-pdf-pages.cs) | Add Header Logo to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to place a logo image as a header on every page of a PDF using Aspose.Pdf's ImageStamp ... |
| [add-header-to-first-pdf-page](./add-header-to-first-pdf-page.cs) | Add Header to First PDF Page Using MarginInfo | `Document`, `HeaderFooter`, `MarginInfo` | Demonstrates how to add a text header to the first page of a PDF document by configuring a Header... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HtmlFragment` | Demonstrates how to insert an HTML fragment with embedded CSS as a header on the first three page... |
| [add-image-footer-to-pdf-pages](./add-image-footer-to-pdf-pages.cs) | Add Image Footer with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to place a semi‑transparent image footer on every page of a PDF using Aspose.Pdf's Imag... |
| [add-image-footer-with-scaling-to-pdf-pages](./add-image-footer-with-scaling-to-pdf-pages.cs) | Add Scaled Image Footer to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add a footer image to every page of a PDF and scale it using Aspose.Pdf's Ima... |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF with Aspose.Pdf, create an ImageStamp with 20% opacity, and apply ... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to PDF Pages | `Document`, `Page`, `Graph` | Shows how to apply a LightGray background color to every page of a PDF by drawing a full‑page rec... |
| [add-multiple-empty-pages-to-pdf](./add-multiple-empty-pages-to-pdf.cs) | Add Multiple Empty Pages Sequentially to PDF | `Document`, `Pages`, `Add` | Shows how to add a series of empty pages to a PDF by iterating over a list of page counts and res... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to insert page numbers only on even pages of a PDF using Aspose.Pdf by iterating throug... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to insert page numbers only on odd-numbered pages of a PDF using Aspose.Pdf in C#. |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers to PDF Using Aspose.Pdf | `Document`, `Save`, `TextStamp` | Shows how to insert a centered page number stamp on each page of a PDF, starting at 1, using Aspo... |
| [add-page-numbers-with-custom-embedded-font](./add-page-numbers-with-custom-embedded-font.cs) | Add Page Numbers with Embedded Custom Font to PDF | `Document`, `OpenFont`, `Font` | Demonstrates how to load a PDF, embed an external TrueType font, and add page numbers using that ... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark Grid to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to overlay a semi‑transparent image watermark repeatedly across each page of a PDF by p... |
| [add-roman-numeral-page-numbers](./add-roman-numeral-page-numbers.cs) | Add Roman Numeral Page Numbers to Introductory PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to insert page numbers in Roman numerals on the first few pages of a PDF using Aspose.P... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent PNG watermark on each page of a PDF, rotating it 4... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to add a centered, semi‑transparent text watermark to each page of a PDF using A... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Footnote Page Numbers to PDF | `Document`, `Page`, `TextFragment` | Loads a PDF, iterates through each page, and inserts a small superscript‑styled page number near ... |
| [add-year-text-watermark-to-pdf](./add-year-text-watermark-to-pdf.cs) | Add Current Year Text Watermark to PDF | `Document`, `TextStamp`, `TextState` | Shows how to load a PDF with Aspose.Pdf, create a TextStamp that includes the current year, apply... |
| [adjust-pdf-bleedbox-for-printer-specs](./adjust-pdf-bleedbox-for-printer-specs.cs) | Adjust PDF BleedBox for Printer Specifications | `Document`, `Page`, `Rectangle` | Shows how to read each page's BleedBox, expand it by a margin to meet printer requirements, and s... |
| [append-empty-a4-page-to-pdf](./append-empty-a4-page-to-pdf.cs) | Append Empty A4 Page to PDF | `Document`, `Save`, `Add` | Shows how to load a PDF with Aspose.Pdf, add a new empty A4‑sized page at the end of the document... |
| ... | | | *and 62 more files* |

## Category Statistics
- Total examples: 92

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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

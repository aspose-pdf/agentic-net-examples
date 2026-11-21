---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

> **Pages** in PDF using C# / .NET -- **97** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **pages** category.
This folder contains standalone C# examples for pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (97/97 files) ← category-specific
- `using Aspose.Pdf.Text;` (26/97 files)
- `using Aspose.Pdf.Annotations;` (4/97 files)
- `using Aspose.Pdf.Drawing;` (1/97 files)
- `using Aspose.Pdf.Facades;` (1/97 files)
- `using System;` (97/97 files)
- `using System.IO;` (97/97 files)
- `using System.Collections.Generic;` (7/97 files)
- `using System.Linq;` (2/97 files)

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
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering Stamp to PDF Pages | `Document`, `PageNumberStamp`, `FontRepository` | Shows how to insert a Bates numbering stamp that starts at 1000 with a dash separator on every pa... |
| [add-bates-numbering-to-pdf-pages__v2](./add-bates-numbering-to-pdf-pages__v2.cs) | Add Bates Numbering to PDF Pages | `Document`, `AddStamp`, `TextStamp` | Shows how to insert year‑based Bates numbers (e.g., 2026‑0001) on each page of a PDF using Aspose... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates how to apply Bates numbering to every page of a PDF using Aspose.Pdf's TextStamp wit... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Prefix to PDF Pages | `Document`, `FindFont`, `TextStamp` | Shows how to load a PDF, iterate over its pages, and apply an alphanumeric Bates number stamp (e.... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Header to Each PDF Page | `Document`, `Page`, `TextFragment` | Shows how to open a PDF, loop through all pages, and insert a bold uppercase header near the top ... |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Insert Chapter‑Prefixed Page Numbers into PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through its pages, and add a custom "Chap... |
| [add-curved-text-watermark-to-pdf-pages](./add-curved-text-watermark-to-pdf-pages.cs) | Add Curved Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to place a watermark that follows a cubic Bézier curve across each page of a PDF using ... |
| [add-custom-page-numbers-to-pdf](./add-custom-page-numbers-to-pdf.cs) | Insert Custom Page Numbers into PDF | `Document`, `Page`, `TextFragment` | Shows how to add a "Page X of Y" footer to every page of a PDF using Aspose.Pdf. |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, create a TextFragment containing the current date, add i... |
| [add-header-logo-image-to-pdf-pages](./add-header-logo-image-to-pdf-pages.cs) | Add Header Logo to PDF Pages | `Document`, `ImageStamp`, `Page` | Shows how to insert a left‑aligned logo image as a header on every page of a PDF using Aspose.Pdf. |
| [add-header-to-first-pdf-page](./add-header-to-first-pdf-page.cs) | Add Text Header to First PDF Page Using MarginInfo | `Document`, `MarginInfo`, `Page` | Demonstrates how to configure page margins and place a styled text fragment as a header on the fi... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HtmlFragment` | Demonstrates how to insert an HTML fragment containing CSS‑styled header text into the first thre... |
| [add-image-footer-to-pdf-pages](./add-image-footer-to-pdf-pages.cs) | Add Image Footer with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image footer with 30% opacity to every page of a PDF using Aspose.Pdf. |
| [add-image-footer-with-scaling-to-pdf-pages](./add-image-footer-with-scaling-to-pdf-pages.cs) | Add Scaled Footer Image to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to insert a footer image on every page of a PDF, scaling it to 50% of the page width an... |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with 20% Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent PNG image as a watermark on every page of a PDF us... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to All PDF Pages | `Document`, `Page`, `Save` | Demonstrates how to apply a LightGray background color to every page of a PDF using Aspose.Pdf fo... |
| [add-multiple-empty-pages-to-pdf](./add-multiple-empty-pages-to-pdf.cs) | Add Multiple Empty Pages Sequentially to PDF | `Document`, `Document(string)`, `PageCollection` | Shows how to load a PDF with Aspose.Pdf, iterate over a list of page counts, add the specified nu... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and insert page numbers only ... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `TextFragment`, `Position` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and add a page number text fr... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Centered Page Numbers to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to add a centered page‑number stamp to each page of a PDF using Aspose.Pdf. |
| [add-page-numbers-with-custom-embedded-font](./add-page-numbers-with-custom-embedded-font.cs) | Insert Page Numbers with Custom Embedded Font | `Document`, `Page`, `TextFragment` | Demonstrates how to add page numbers to each PDF page using a TrueType font loaded from an extern... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an image, convert its size to PDF points, and stamp it repeatedly in a grid pat... |
| [add-roman-numeral-page-numbers](./add-roman-numeral-page-numbers.cs) | Add Roman Numeral Page Numbers to PDF | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, convert page indices to Roman numerals, and insert them ... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place a semi‑transparent image watermark, rotated 45°, scaled to half the page size ... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `TextStamp`, `TextState` | Shows how to open a PDF with Aspose.Pdf, create a semi‑transparent TextStamp, configure its font ... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Footnote Page Numbers to PDF | `Document`, `Page`, `TextFragment` | Loads a PDF, iterates through each page, and inserts a superscript‑styled page number as a footno... |
| [add-year-text-watermark-to-pdf](./add-year-text-watermark-to-pdf.cs) | Add Yearly Text Watermark to PDF Pages | `Document`, `TextStamp`, `AddStamp` | Shows how to create a text watermark that includes the current year and apply it to every page of... |
| [adjust-pdf-bleedbox-for-printer-specs](./adjust-pdf-bleedbox-for-printer-specs.cs) | Adjust BleedBox for Printer Bleed in PDF | `Document`, `Page`, `Rectangle` | Loads a PDF, expands each page's BleedBox by a 5 mm margin (converted to points) to meet printer ... |
| [append-empty-a4-page-to-pdf](./append-empty-a4-page-to-pdf.cs) | Append Empty A4 Page to PDF | `Document`, `Page`, `PageSize` | Shows how to load a PDF with Aspose.Pdf, add a new blank A4‑sized page at the end of the document... |
| [append-page-to-pdf](./append-page-to-pdf.cs) | Append a Page from One PDF to Another | `Document`, `PageCollection`, `Add` | Demonstrates loading two PDF files and appending a page from the source PDF to the end of the tar... |
| ... | | | *and 67 more files* |

## Category Statistics
- Total examples: 97

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
Updated: 2026-11-13 | Run: `20261113_000806_afb7ad`
<!-- AUTOGENERATED:END -->

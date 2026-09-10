---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

> **Pages** in PDF using C# / .NET -- **100** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Facades;` (3/100 files)
- `using Aspose.Pdf.Annotations;` (2/100 files)
- `using Aspose.Pdf.Drawing;` (2/100 files)
- `using System;` (100/100 files)
- `using System.IO;` (98/100 files)
- `using System.Collections.Generic;` (5/100 files)
- `using System.Linq;` (4/100 files)
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
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Shows how to insert Bates numbering stamps starting at 1000 with a dash separator on every page o... |
| [add-bates-numbering-to-pdf-pages__v2](./add-bates-numbering-to-pdf-pages__v2.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Loads a PDF document, applies year‑based Bates numbering (format "2026-####") to each page, and s... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `PageCollection`, `AddBatesNumbering` | Demonstrates loading a PDF with Aspose.Pdf, applying Bates numbering that includes a custom prefi... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Alphanumeric Prefix to PDF | `Document`, `Pages`, `AddBatesNumbering` | Shows how to load a PDF using Aspose.Pdf, apply Bates numbering with a custom alphanumeric prefix... |
| [add-blank-front-matter-page-roman-label](./add-blank-front-matter-page-roman-label.cs) | Add Blank Front‑Matter Page with Roman Numeral Label | `Document`, `Page`, `PageLabel` | Demonstrates how to insert a blank page at the beginning of an existing PDF and assign a custom p... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to insert a bold, uppercase header on every page of a PDF document using Aspose.Pdf. |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Add Chapter Prefix Page Numbers to PDF | `Document`, `PageNumberStamp`, `FindFont` | Demonstrates how to insert page numbers with a custom "Chapter" prefix on every page of a PDF usi... |
| [add-curved-text-watermark-to-pdf-pages](./add-curved-text-watermark-to-pdf-pages.cs) | Add Curved Text Watermark to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Shows how to place a repeated watermark that follows a curved arc across each page of a PDF using... |
| [add-custom-page-numbers-to-pdf](./add-custom-page-numbers-to-pdf.cs) | Add Custom Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to load a PDF with Aspose.Pdf, create a PageNumberStamp using the format "Page X... |
| [add-diagonal-text-watermark-to-pdf-pages](./add-diagonal-text-watermark-to-pdf-pages.cs) | Add Diagonal Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF, iterate through its pages, and apply a semi‑transparent diagonal text wa... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to Each PDF Page | `Document`, `Page`, `FooterArtifact` | Demonstrates how to add a text footer with the current generation date to every page of a PDF usi... |
| [add-header-logo-image-to-pdf-pages](./add-header-logo-image-to-pdf-pages.cs) | Add Header Logo Image to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp for a company logo, align it to the top‑left of eac... |
| [add-header-to-first-pdf-page](./add-header-to-first-pdf-page.cs) | Add Header Text to First PDF Page Using MarginInfo | `Document`, `HeaderFooter`, `MarginInfo` | Demonstrates how to add a text header to the first page of a PDF by configuring a HeaderFooter wi... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HtmlFragment` | Demonstrates how to embed a styled HTML header into the first three pages of a PDF using Aspose.P... |
| [add-image-footer-to-pdf-pages](./add-image-footer-to-pdf-pages.cs) | Add Image Footer with Opacity to PDF Pages | `Document`, `Page`, `FooterArtifact` | Demonstrates how to add an image footer with 30% opacity to every page of a PDF using Aspose.Pdf. |
| [add-image-footer-with-scaling-to-pdf-pages](./add-image-footer-with-scaling-to-pdf-pages.cs) | Add Image Footer with Scaling to PDF Pages | `Document`, `Page`, `Image` | Shows how to insert a scaled footer image on every page of a PDF document using Aspose.Pdf. |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay a semi‑transparent image (logo) on every page of a PDF using Aspose.P... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to PDF Pages | `Document`, `Page`, `Color` | Shows how to load a PDF with Aspose.Pdf, iterate over each page, set a LightGray background color... |
| [add-multiple-empty-pages-to-pdf](./add-multiple-empty-pages-to-pdf.cs) | Add Multiple Empty Pages Sequentially to PDF | `Document`, `Pages`, `PageCollection` | Demonstrates how to add a series of empty pages to a PDF by iterating over a list of page counts,... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `PageNumberStamp`, `HorizontalAlignment` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, and adding a page number... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `PageNumberStamp`, `Put` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, and applying a page numb... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers to PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert a centered page number stamp starting at 1 on every page of a PDF using Aspos... |
| [add-page-numbers-with-custom-embedded-font](./add-page-numbers-with-custom-embedded-font.cs) | Add Page Numbers with Custom Embedded Font to PDF | `Document`, `FindFont`, `Font` | Demonstrates how to insert page numbers on every PDF page using a TrueType font loaded from an ex... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image repeatedly in a grid pattern on every page o... |
| [add-roman-numeral-page-numbers](./add-roman-numeral-page-numbers.cs) | Add Roman Numeral Page Numbers to Introductory PDF Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to insert page numbers in uppercase Roman numeral format on the first few pages ... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a 45‑degree rotated, half‑size image as a watermark on every page of ... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark with Outline to PDF Page... | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to load a PDF, iterate through its pages, and apply a semi‑transparent text wate... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript Page Numbers to PDF Pages | `Document`, `Page`, `PageNumberStamp` | Shows how to insert page numbers with a superscript‑style appearance on every page of a PDF using... |
| [add-year-text-watermark-to-pdf](./add-year-text-watermark-to-pdf.cs) | Add Year-Based Text Watermark to PDF | `Document`, `AddStamp`, `TextStamp` | Shows how to load a PDF, create a TextStamp that includes the current year, apply it to every pag... |
| [adjust-pdf-bleedbox-for-printer-specs](./adjust-pdf-bleedbox-for-printer-specs.cs) | Adjust PDF BleedBox for Printer Specifications | `Document`, `Page`, `Rectangle` | Shows how to read each page's BleedBox, expand it by a margin, and save the modified PDF using As... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

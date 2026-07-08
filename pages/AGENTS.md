---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

> **Pages** in PDF using C# / .NET -- **99** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (22/99 files)
- `using Aspose.Pdf.Facades;` (3/99 files)
- `using Aspose.Pdf.Annotations;` (1/99 files)
- `using Aspose.Pdf.Drawing;` (1/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (97/99 files)
- `using System.Collections.Generic;` (5/99 files)
- `using System.Linq;` (2/99 files)
- `using System.Globalization;` (1/99 files)

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
| [add-bates-numbering-increment-5](./add-bates-numbering-increment-5.cs) | Add Bates Numbering with Increment of 5 to PDFs | `Document`, `BatesNArtifact`, `Page` | Shows how to batch‑process PDF files and apply Bates numbering that increments by 5 on each page ... |
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Shows how to apply Bates numbering starting at 1000 with a dash separator to every page of a PDF ... |
| [add-bates-numbering-to-pdf-pages__v2](./add-bates-numbering-to-pdf-pages__v2.cs) | Add Bates Numbering to PDF Pages | `Document`, `Pages`, `AddBatesNumbering` | Shows how to apply year‑based Bates numbering (format 2026‑####) to each page of a PDF document u... |
| [add-bates-numbering-with-alphanumeric-prefix](./add-bates-numbering-with-alphanumeric-prefix.cs) | Add Bates Numbering with Alphanumeric Prefix to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Shows how to load a PDF, apply Bates numbering with a custom alphanumeric prefix and zero‑padded ... |
| [add-bates-numbering-with-custom-prefix-suffix](./add-bates-numbering-with-custom-prefix-suffix.cs) | Add Bates Numbering with Custom Prefix and Suffix to PDF | `Document`, `Save`, `Pages` | Demonstrates how to apply Bates numbering to every page of a PDF using Aspose.Pdf, with a custom ... |
| [add-blank-front-matter-page-with-label-i](./add-blank-front-matter-page-with-label-i.cs) | Add Blank Front-Matter Page with Custom Label "i" to PDF | `Document`, `Insert`, `PageLabel` | Demonstrates inserting a blank page at the beginning of an existing PDF and assigning it a custom... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to Each PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, create a bold uppercase header using a TextFragment, and... |
| [add-centered-page-numbers-to-pdf](./add-centered-page-numbers-to-pdf.cs) | Add Centered Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Shows how to insert a page number stamp that starts at 1 and is centered on every page of a PDF u... |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Add Chapter Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to insert page numbers with a custom "Chapter" prefix on each page of a PDF usin... |
| [add-curved-text-watermark-to-pdf-pages](./add-curved-text-watermark-to-pdf-pages.cs) | Add Curved Text Watermark to PDF Pages | `Document`, `Page`, `TextState` | Demonstrates how to add a semi‑transparent, rotated text watermark that follows a curved path to ... |
| [add-diagonal-repeating-text-watermark-to-pdf-pages](./add-diagonal-repeating-text-watermark-to-pdf-pages.cs) | Add Diagonal Repeating Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to load a PDF, iterate through its pages, and apply a semi‑transparent diagonal ... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to load a PDF with Aspose.Pdf, create a footer containing the current date, and apply i... |
| [add-header-company-logo-to-pdf-pages](./add-header-company-logo-to-pdf-pages.cs) | Add Header with Company Logo to PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to load a PDF, create a header on each page, and insert a left‑aligned company l... |
| [add-html-header-with-css-to-first-three-pdf-pages](./add-html-header-with-css-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HtmlFragment` | Shows how to create an HTML fragment with embedded CSS and assign it as a header to the first thr... |
| [add-image-footer-30-opacity-to-pdf-pages](./add-image-footer-30-opacity-to-pdf-pages.cs) | Add Image Footer with 30% Opacity to PDF Pages | `Document`, `Page`, `FooterArtifact` | Demonstrates how to add an image footer with 30% opacity to every page of a PDF using Aspose.Pdf. |
| [add-image-footer-to-pdf-pages](./add-image-footer-to-pdf-pages.cs) | Add Image Footer to PDF Pages | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, iterate over each page, and add a scaled footer image at the bottom of e... |
| [add-image-watermark-with-opacity-to-pdf-pages](./add-image-watermark-with-opacity-to-pdf-pages.cs) | Add Image Watermark with Opacity to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to apply a semi‑transparent image watermark to every page of a PDF using Aspose.... |
| [add-leading-zero-page-numbers](./add-leading-zero-page-numbers.cs) | Insert Page Numbers with Leading Zeros | `Document`, `Page`, `TextStamp` | Loads a PDF, iterates through each page, and adds a centered page number stamp formatted with lea... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to All PDF Pages | `Document`, `Page`, `Color` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, set a LightGray background co... |
| [add-multiple-empty-pages-to-pdf](./add-multiple-empty-pages-to-pdf.cs) | Add Multiple Empty Pages to PDF Sequentially | `Document`, `Pages`, `Add` | Shows how to add a series of empty pages to a PDF by iterating over a list of page counts and res... |
| [add-page-numbers-custom-font](./add-page-numbers-custom-font.cs) | Add Page Numbers with Custom Font to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert sequential page numbers on each PDF page using Arial 14‑pt font with A... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to load a PDF with Aspose.Pdf, loop through its pages, and add a page number stamp only... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert page numbers only on odd-numbered pages of a PDF using Aspose.Pdf by iteratin... |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers (Page X of Y) to PDF | `Document`, `PageNumberStamp`, `FontRepository` | Demonstrates how to load a PDF, create a PageNumberStamp with a custom "Page # of #" format, and ... |
| [add-page-numbers-with-embedded-custom-font](./add-page-numbers-with-embedded-custom-font.cs) | Add Page Numbers with Embedded Custom Font to PDF | `Document`, `FindFont`, `Font` | Demonstrates how to insert page numbers on every PDF page using a custom TrueType font that is em... |
| [add-page-numbers-with-slash-separator](./add-page-numbers-with-slash-separator.cs) | Add Page Numbers with '/' Separator to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Loads a PDF, creates a PageNumberStamp using the "#/#" format to show current page and total page... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image repeatedly in a grid across each page of a P... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to place a 45° rotated, half‑size image watermark on every page of a PDF using A... |
| [add-semi-transparent-text-watermark-to-pdf-pages](./add-semi-transparent-text-watermark-to-pdf-pages.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add a semi‑transparent text watermark with an outline to each page of a PDF u... |
| [add-text-header-to-first-pdf-page](./add-text-header-to-first-pdf-page.cs) | Add Text Header to First PDF Page Using MarginInfo | `Document`, `HeaderFooter`, `MarginInfo` | Demonstrates how to add a text header to the first page of a PDF document and configure its margi... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

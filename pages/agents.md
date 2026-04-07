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

- `using Aspose.Pdf;` (85/85 files) ← category-specific
- `using Aspose.Pdf.Text;` (29/85 files)
- `using Aspose.Pdf.Drawing;` (5/85 files)
- `using Aspose.Pdf.Facades;` (1/85 files)
- `using Aspose.Pdf.Operators;` (1/85 files)
- `using System;` (84/85 files)
- `using System.Runtime.InteropServices;` (21/85 files)
- `using System.IO;` (20/85 files)
- `using System.Collections.Generic;` (5/85 files)
- `using System.Linq;` (2/85 files)

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
| [add-bates-numbering-increment-5](./add-bates-numbering-increment-5.cs) | Add Bates Numbering with Increment of 5 to PDFs | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to batch‑process PDF files and add Bates numbering starting at 5 (increment of 5... |
| [add-bates-numbering-stamp](./add-bates-numbering-stamp.cs) | Add Bates Numbering Stamp to PDF Pages | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to add Bates numbering to each page of a PDF, starting at 1000 and using a dash ... |
| [add-bates-numbering](./add-bates-numbering.cs) | Add Bates Numbering with Prefix and Suffix to PDF | `Document`, `Page`, `TextFragment` | Creates a sample PDF and adds Bates numbering with a custom prefix "DOC" and suffix "-2026" to ea... |
| [add-bates-numbering__v2](./add-bates-numbering__v2.cs) | Add Alphanumeric Bates Numbering to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to add Bates numbering with a custom alphanumeric prefix to each page of a PDF d... |
| [add-bates-numbering__v3](./add-bates-numbering__v3.cs) | Add Bates Numbering to PDF Pages | `Document`, `Page`, `AddBatesNumbering` | Demonstrates how to add year‑based Bates numbering (e.g., 2026‑0001) to each page of a PDF using ... |
| [add-blank-page-with-label](./add-blank-page-with-label.cs) | Add Blank Page with Custom Roman Numeral Page Label | `Document`, `Page`, `PageLabel` | Shows how to insert a blank page at the start of a PDF and assign a custom page label using lower... |
| [add-bold-uppercase-header](./add-bold-uppercase-header.cs) | Add Bold Uppercase Header to PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a bold, uppercase header to each page of a PDF using Aspose.Pdf for .NET. |
| [add-centered-page-numbers](./add-centered-page-numbers.cs) | Add Centered Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add a centered page number stamp starting at 1 to every page of a PDF document. |
| [add-curved-text-watermark](./add-curved-text-watermark.cs) | Add Curved Text Watermark to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add a text watermark that follows a curved path using WatermarkArtifact. |
| [add-custom-page-numbers](./add-custom-page-numbers.cs) | Add Custom Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to add a "Page X of Y" footer to each page of a PDF using Aspose.Pdf's PageNumbe... |
| [add-custom-page-numbers__v2](./add-custom-page-numbers__v2.cs) | Add Custom Page Numbers with Separator | `Document`, `PageNumber`, `PageNumberStamp` | Demonstrates how to insert page numbers in the format current/total on each page of a PDF. |
| [add-custom-prefix-page-numbers](./add-custom-prefix-page-numbers.cs) | Add Custom Prefix Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add page numbers with a custom "Chapter" prefix to each page of a PDF using A... |
| [add-diagonal-text-watermark](./add-diagonal-text-watermark.cs) | Add Diagonal Repeating Text Watermark to PDF | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to add a diagonal repeating text watermark to each page of a PDF using Watermark... |
| [add-dynamic-year-watermark](./add-dynamic-year-watermark.cs) | Add Dynamic Year Watermark to PDF | `Document`, `Page`, `WatermarkArtifact` | Demonstrates adding a text watermark that includes the current year to a PDF page using Aspose.Pdf. |
| [add-generation-date-footer](./add-generation-date-footer.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `FooterArtifact` | Demonstrates how to add a text footer with the current generation date to each page of a PDF usin... |
| [add-header-margininfo](./add-header-margininfo.cs) | Add Header to First PDF Page Using MarginInfo | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a text header to the first page of a PDF and configure its margins using ... |
| [add-html-header-first-three-pages](./add-html-header-first-three-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add an HTML header with embedded CSS styling to the first three pages of a PD... |
| [add-light-gray-background](./add-light-gray-background.cs) | Add Light Gray Background to All PDF Pages | `Document`, `Background`, `FromGray` | Demonstrates how to set a light gray background color for every page in a PDF using Aspose.PDF. |
| [add-multiple-empty-pages](./add-multiple-empty-pages.cs) | Add Multiple Empty Pages Sequentially | `Document`, `Add` | Demonstrates how to add several empty pages to a PDF by iterating over a list of page counts. |
| [add-odd-page-numbers](./add-odd-page-numbers.cs) | Add Page Numbers to Odd Pages in PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert page numbers only on odd-numbered pages of a PDF using Aspose.Pdf for ... |
| [add-page-numbers-custom-font](./add-page-numbers-custom-font.cs) | Add Page Numbers with Custom Font to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert page numbers using Arial 14pt font on each page of a PDF. |
| [add-page-numbers-even-pages](./add-page-numbers-even-pages.cs) | Add Page Numbers to Even Pages in PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert page numbers only on even-numbered pages of a PDF using a loop. |
| [add-page-numbers-leading-zeros](./add-page-numbers-leading-zeros.cs) | Add Page Numbers with Leading Zeros to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to insert page numbers padded with a leading zero for single‑digit pages in a PD... |
| [add-roman-numeral-page-numbers](./add-roman-numeral-page-numbers.cs) | Add Roman Numeral Page Numbers to Introductory Pages | `Document`, `PageNumberStamp`, `NumberingStyle` | Demonstrates how to add page numbers in Roman numerals to the first pages of a PDF using Aspose.Pdf. |
| [add-transparent-separator-page](./add-transparent-separator-page.cs) | Add Transparent Separator Page to PDF | `Document`, `Insert`, `Background` | Demonstrates inserting an empty page with a transparent background between existing pages in a PDF. |
| [adjust-bleedbox](./adjust-bleedbox.cs) | Adjust BleedBox of PDF Pages for Printer Specifications | `Document`, `BleedBox`, `Rectangle` | Loads a PDF, expands each page's BleedBox by a specified margin, and saves the updated document. |
| [alternate-page-background](./alternate-page-background.cs) | Alternate Page Background Colors in PDF | `Document`, `Page`, `Color` | Demonstrates how to set alternating background colors (LightGray and White) for each page in a PD... |
| [append-a4-page](./append-a4-page.cs) | Append an Empty A4 Page to PDF | `Document`, `Add`, `PageInfo` | Loads an existing PDF, appends an empty A4‑sized page at the end, and saves the result. |
| [append-page](./append-page.cs) | Append a Page from Another PDF to Existing PDF | `Document`, `Merge`, `TextFragment` | Demonstrates loading two PDFs, appending a page from the second PDF to the first, and saving the ... |
| [batch-resize-pages](./batch-resize-pages.cs) | Batch Resize PDF Pages to Fixed Width | `Document`, `SetPageSize` | Demonstrates how to resize all pages of a PDF to a fixed width of 800 points while preserving the... |
| ... | | | *and 55 more files* |

## Category Statistics
- Total examples: 85

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
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->

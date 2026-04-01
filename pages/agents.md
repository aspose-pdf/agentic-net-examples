---
name: Pages
description: C# examples for Pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Pages

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Pages** category.
This folder contains standalone C# examples for Pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (21/21 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/21 files)
- `using System;` (21/21 files)
- `using System.IO;` (13/21 files)
- `using System.Runtime.InteropServices;` (2/21 files)
- `using System.Collections.Generic;` (1/21 files)

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
| [add-multiple-empty-pages](./add-multiple-empty-pages.cs) | Add Multiple Empty Pages Sequentially | `Document`, `Add` | Demonstrates adding empty pages to a PDF by iterating over a list, respecting the evaluation mode... |
| [append-empty-a4-page](./append-empty-a4-page.cs) | Append Empty A4 Page to PDF | `Document`, `Add`, `SetPageSize` | Loads an existing PDF, appends an empty A4‑sized page at the end, and saves the result. |
| [append-page-from-another-pdf](./append-page-from-another-pdf.cs) | Append a Page from Another PDF | `Document`, `Page`, `Add` | Demonstrates how to load a PDF, copy a page from another PDF, and append it to the end of the tar... |
| [change-odd-pages-orientation](./change-odd-pages-orientation.cs) | Change Odd Pages from Landscape to Portrait | `Document`, `Page`, `IsLandscape` | Demonstrates how to convert odd-numbered landscape pages to portrait orientation by adjusting the... |
| [convert-portrait-to-landscape](./convert-portrait-to-landscape.cs) | Convert PDF Pages from Portrait to Landscape | `Document`, `Page`, `SetPageSize` | Demonstrates how to change each page orientation by swapping the width and height of the MediaBox... |
| [copy-mediabox](./copy-mediabox.cs) | Copy MediaBox from One Page to Another | `Document`, `MediaBox`, `Rectangle` | Copies the MediaBox rectangle from page 8 and applies it to page 12 of a PDF. |
| [delete-last-three-pages](./delete-last-three-pages.cs) | Delete Last Three Pages from PDF | `Document`, `Add`, `Delete` | Demonstrates how to remove the last three pages of a PDF using PageCollection.Delete. |
| [export-page-dimensions](./export-page-dimensions.cs) | Export PDF Page Dimensions to CSV | `Document`, `Page` | Extracts the width and height of each page in a PDF and writes them to a CSV file for external an... |
| [insert-blank-page-after-each](./insert-blank-page-after-each.cs) | Insert Blank Page After Each Existing Page | `Document`, `Insert`, `Count` | Demonstrates how to double a PDF's page count by inserting a blank page after every existing page. |
| [insert-blank-page](./insert-blank-page.cs) | Insert Blank Page at Specific Position | `Document`, `Insert`, `TextFragment` | Demonstrates inserting a blank page at index three in a PDF using Aspose.Pdf. |
| [insert-empty-page-custom-size](./insert-empty-page-custom-size.cs) | Insert Empty Page with Custom Size at Beginning | `Document`, `Insert`, `SetPageSize` | Demonstrates how to insert an empty page of 200 × 300 points at the start of a PDF document. |
| [insert-empty-pages-midpoint](./insert-empty-pages-midpoint.cs) | Insert Empty Pages at Document Midpoint | `Document`, `Insert`, `Count` | Shows how to calculate the middle of a PDF and insert empty pages there, limited to four pages du... |
| [insert-page-at-index-two](./insert-page-at-index-two.cs) | Insert Page from External PDF at Specific Position | `Document`, `Page`, `Insert` | Demonstrates loading two PDFs, extracting a page from the external PDF, and inserting it into ano... |
| [read-pdf-page-dimensions](./read-pdf-page-dimensions.cs) | Read PDF Page Dimensions | `Document`, `Page`, `Rectangle` | Loads a PDF and logs each page's width and height to the console. |
| [resize-page-seven-letter](./resize-page-seven-letter.cs) | Resize Page Seven to Letter Size | `Document`, `Page`, `PageSize` | Demonstrates how to change the size of page seven to Letter format using SetPageSize. |
| [resize-pages-a5](./resize-pages-a5.cs) | Resize PDF Pages to A5 Size | `Document`, `Page`, `PageSize` | Demonstrates how to loop through a PDF's pages and resize each one to A5 dimensions. |
| [rotate-all-pages-180](./rotate-all-pages-180.cs) | Rotate All PDF Pages 180 Degrees | `Document`, `Page`, `Rotate` | Demonstrates how to rotate every page of a PDF document by 180 degrees using Aspose.Pdf. |
| [rotate-page-four-90-degrees](./rotate-page-four-90-degrees.cs) | Rotate Fourth Page by 90 Degrees Clockwise | `Document`, `Rotate`, `TextFragment` | Creates a PDF with four pages, rotates the fourth page 90 degrees clockwise, and saves the result. |
| [rotate-pages-reverse-order](./rotate-pages-reverse-order.cs) | Rotate PDF Pages in Reverse Order | `Document`, `Rotate`, `Rotation` | Demonstrates rotating each page of a PDF 90 degrees clockwise, processing pages from the last to ... |
| [set-custom-page-size](./set-custom-page-size.cs) | Set Custom Page Size for First PDF Page | `Document`, `SetPageSize` | Demonstrates how to change the size of the first page of a PDF to custom dimensions (500 × 700 po... |
| [set-page-rotation-parity](./set-page-rotation-parity.cs) | Set Page Rotation Based on Page Number Parity | `Document`, `Page`, `Rotation` | Creates a PDF and rotates even-numbered pages by 90 degrees while leaving odd-numbered pages unro... |

## Category Statistics
- Total examples: 21

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
- Review code examples in this folder for Pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-01 | Run: `20260401_224557_2b5c5c`
<!-- AUTOGENERATED:END -->

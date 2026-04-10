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
- `using Aspose.Pdf.Text;` (23/99 files)
- `using Aspose.Pdf.Annotations;` (2/99 files)
- `using Aspose.Pdf.Facades;` (2/99 files)
- `using Aspose.Pdf.Drawing;` (1/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (97/99 files)
- `using System.Collections.Generic;` (6/99 files)
- `using System.Linq;` (2/99 files)

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
| [add-alphanumeric-bates-numbering-to-pdf-pages](./add-alphanumeric-bates-numbering-to-pdf-pages.cs) | Add Alphanumeric Bates Numbering to PDF Pages | `Document`, `AddBatesNumbering`, `HorizontalAlignment` | Demonstrates how to add Bates numbering with a custom alphanumeric prefix to each page of a PDF u... |
| [add-bates-numbering-to-pdf-pages](./add-bates-numbering-to-pdf-pages.cs) | Add Bates Numbering to PDF Pages | `Document`, `Save`, `AddBatesNumbering` | Shows how to insert year‑based Bates numbers in the format "2026-####" on every page of a PDF usi... |
| [add-blank-front-matter-page-with-custom-label](./add-blank-front-matter-page-with-custom-label.cs) | Add Blank Front‑Matter Page with Custom Label | `Document`, `Save`, `Insert` | Shows how to insert a blank page at the start of a PDF and assign a custom page label prefix "i" ... |
| [add-bleedbox-extend-cropbox](./add-bleedbox-extend-cropbox.cs) | Add BleedBox Extending 5 Points Beyond CropBox | `Document`, `Page`, `Rectangle` | Loads a PDF, iterates through each page, creates a BleedBox that extends 5 points beyond the exis... |
| [add-bold-uppercase-header-to-pdf-pages](./add-bold-uppercase-header-to-pdf-pages.cs) | Add Bold Uppercase Header to Each PDF Page | `Document`, `Page`, `HeaderFooter` | Demonstrates how to add a centered bold uppercase header to every page of an existing PDF using A... |
| [add-centered-page-numbers-to-pdf](./add-centered-page-numbers-to-pdf.cs) | Add Centered Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Shows how to insert a centered page number stamp on every page of a PDF document using Aspose.Pdf. |
| [add-chapter-page-numbers-to-pdf](./add-chapter-page-numbers-to-pdf.cs) | Add Chapter Page Numbers to PDF | `Document`, `Page`, `TextFragment` | Shows how to iterate through each page of a PDF and add a footer with a custom "Chapter" prefix a... |
| [add-diagonal-text-watermark-to-pdf-pages](./add-diagonal-text-watermark-to-pdf-pages.cs) | Add Diagonal Text Watermark to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to apply a semi‑transparent diagonal text watermark to every page of a PDF using... |
| [add-generation-date-footer-to-pdf-pages](./add-generation-date-footer-to-pdf-pages.cs) | Add Generation Date Footer to PDF Pages | `Document`, `Page`, `FooterArtifact` | Shows how to insert a text footer with the current date on every page of a PDF document using Asp... |
| [add-header-text-to-first-pdf-page](./add-header-text-to-first-pdf-page.cs) | Add Header Text to First PDF Page | `Document`, `HeaderFooter`, `MarginInfo` | Demonstrates how to add a text header to the first page of a PDF using Aspose.Pdf's HeaderFooter ... |
| [add-html-header-to-first-three-pdf-pages](./add-html-header-to-first-three-pdf-pages.cs) | Add HTML Header with CSS to First Three PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to insert an HTML‑styled header into the first three pages of a PDF document using Aspo... |
| [add-image-footer-30-opacity-pdf](./add-image-footer-30-opacity-pdf.cs) | Add Image Footer with 30% Opacity to All PDF Pages | `Document`, `Save`, `Page` | Demonstrates how to add a semi‑transparent image footer to every page of a PDF using Aspose.Pdf. |
| [add-image-watermark-20-opacity-pdf-pages](./add-image-watermark-20-opacity-pdf-pages.cs) | Add Image Watermark with 20% Opacity to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Shows how to apply an image watermark with 20% opacity to every page of a PDF document using Aspo... |
| [add-leading-zero-page-numbers](./add-leading-zero-page-numbers.cs) | Add Leading Zero Page Numbers to PDF | `Document`, `PageNumberStamp`, `FindFont` | Shows how to insert page numbers on every page of a PDF using Aspose.Pdf, formatting single‑digit... |
| [add-left-aligned-header-logo-to-pdf-pages](./add-left-aligned-header-logo-to-pdf-pages.cs) | Add Left-Aligned Header Logo to PDF Pages | `Document`, `Page`, `ImageStamp` | Loads an existing PDF, creates an ImageStamp from a logo image, adds it as a left‑aligned header ... |
| [add-lightgray-background-to-pdf-pages](./add-lightgray-background-to-pdf-pages.cs) | Add LightGray Background to All PDF Pages | `Document`, `Page`, `Color` | Demonstrates loading a PDF with Aspose.Pdf, applying a LightGray background color to each page, a... |
| [add-multiple-empty-pages](./add-multiple-empty-pages.cs) | Add Multiple Empty Pages Sequentially | `Document`, `Add`, `Save` | Demonstrates how to add a series of empty pages to a PDF by iterating over a list of page counts,... |
| [add-page-numbers-arial-font](./add-page-numbers-arial-font.cs) | Add Page Numbers with Arial Font to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert sequential page numbers on every page of a PDF using Aspose.Pdf, with ... |
| [add-page-numbers-to-even-pdf-pages](./add-page-numbers-to-even-pdf-pages.cs) | Add Page Numbers to Even PDF Pages | `Document`, `Page`, `PageNumberStamp` | Demonstrates loading a PDF with Aspose.Pdf, iterating over even-numbered pages, and stamping each... |
| [add-page-numbers-to-odd-pdf-pages](./add-page-numbers-to-odd-pdf-pages.cs) | Add Page Numbers to Odd PDF Pages | `Document`, `PageNumberStamp`, `Page` | Shows how to insert page numbers only on odd-numbered pages of a PDF using Aspose.Pdf. |
| [add-page-numbers-to-pdf](./add-page-numbers-to-pdf.cs) | Add Page Numbers in "Page X of Y" Format to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert page numbers with a custom "Page X of Y" format on every page of a PDF... |
| [add-repeating-image-watermark-to-pdf-pages](./add-repeating-image-watermark-to-pdf-pages.cs) | Add Repeating Image Watermark to PDF Pages | `Document`, `ImageStamp`, `Page` | Demonstrates how to tile a semi‑transparent image stamp across every page of a PDF, creating a gr... |
| [add-rotated-image-watermark-to-pdf-pages](./add-rotated-image-watermark-to-pdf-pages.cs) | Add Rotated Image Watermark to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add a PNG watermark image to each page of a PDF, scaling it to half the page ... |
| [add-scaled-image-footer-to-pdf-pages](./add-scaled-image-footer-to-pdf-pages.cs) | Add Scaled Image Footer to Each PDF Page | `Document`, `Page`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, iterate over all pages, and insert a proportionally scal... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark with Outline to PDF Page... | `Document`, `Save`, `AddStamp` | Demonstrates how to add a semi‑transparent text watermark with a colored outline to every page of... |
| [add-superscript-page-numbers-to-pdf](./add-superscript-page-numbers-to-pdf.cs) | Add Superscript-Style Page Numbers to PDF | `Document`, `Save`, `AddStamp` | Demonstrates how to insert page numbers at the bottom-center of each PDF page using a PageNumberS... |
| [add-text-watermark-to-pdf-pages](./add-text-watermark-to-pdf-pages.cs) | Add Text Watermark to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to add a semi‑transparent text watermark to each page of a PDF using Aspose.Pdf'... |
| [add-year-watermark-to-pdf-pages](./add-year-watermark-to-pdf-pages.cs) | Add Year Watermark to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to add a semi‑transparent text watermark that includes the current year to every... |
| [adjust-pdf-bleedbox-for-printer-specs](./adjust-pdf-bleedbox-for-printer-specs.cs) | Adjust PDF BleedBox for Printer Specifications | `Document`, `Page`, `Rectangle` | Loads a PDF, reads each page's BleedBox (or falls back to MediaBox), expands the box by a fixed o... |
| [alternating-page-background-colors](./alternating-page-background-colors.cs) | Set Alternating Page Background Colors in PDF | `Document`, `Page`, `Color` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, assign LightGray to odd pages... |
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
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

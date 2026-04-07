---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-text** category.
This folder contains standalone C# examples for working-with-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (76/76 files) ← category-specific
- `using Aspose.Pdf.Text;` (71/76 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (6/76 files)
- `using Aspose.Pdf.Facades;` (4/76 files)
- `using Aspose.Pdf.Forms;` (4/76 files)
- `using Aspose.Pdf.Drawing;` (2/76 files)
- `using System;` (76/76 files)
- `using System.IO;` (70/76 files)
- `using System.Collections.Generic;` (6/76 files)
- `using System.Text.RegularExpressions;` (4/76 files)
- `using System.Linq;` (1/76 files)
- `using System.Text;` (1/76 files)
- `using System.Text.Json;` (1/76 files)

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
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Hyperlink Text to PDF | `Document`, `Save`, `Page` | Demonstrates how to insert a text segment with a web hyperlink into an existing PDF using Aspose.... |
| [add-footnote-with-table-to-pdf](./add-footnote-with-table-to-pdf.cs) | Add Footnote with Table to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a 2×2 table inside a footnote and attach it to a text fragment in an existing... |
| [add-header-to-all-pdf-pages](./add-header-to-all-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Save`, `Page` | Shows how to iterate through a PDF document's pages and add a centered header text to each page u... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Specific Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate a text fragment in a PDF and overlay an invisible button field that displays ... |
| [add-page-number-footer-to-pdf](./add-page-number-footer-to-pdf.cs) | Add Page Number Footer to PDF | `Document`, `Page`, `PageNumberStamp` | Shows how to insert a footer containing page numbers on each page of a PDF using Aspose.Pdf's Pag... |
| [add-plain-text-to-pdf-page](./add-plain-text-to-pdf-page.cs) | Add Plain Text to a PDF Page Using TextFragment | `Document`, `Page`, `TextFragment` | Demonstrates opening a PDF, placing a plain text string at specific coordinates on the first page... |
| [add-rotated-mixed-style-text-paragraph](./add-rotated-mixed-style-text-paragraph.cs) | Add Rotated Mixed-Style Text Paragraph to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates creating a TextParagraph with bold, italic, and regular lines, rotating it, and inse... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom-Right Corner of Last PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated text paragraph at the bottom-... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated "CONFIDENTIAL" text fragment ... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to overlay a semi-transparent diagonal text watermark on each page of a PDF usin... |
| [add-styled-html-fragment-to-pdf-page](./add-styled-html-fragment-to-pdf-page.cs) | Add Styled HTML Fragment to PDF Page | `Document`, `Page`, `HtmlFragment` | Shows how to load an existing PDF, create an HtmlFragment with inline CSS, optionally override it... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads an existing PDF, adds a red "Confidential" text fragment to the first page, then encrypts t... |
| [add-text-to-pdf-using-memory-streams](./add-text-to-pdf-using-memory-streams.cs) | Add Text to PDF Using Memory Streams | `Document`, `Save`, `Page` | Demonstrates loading a PDF from a memory stream (or creating a new one), adding a text fragment t... |
| [add-text-with-automatic-ligatures-to-pdf](./add-text-with-automatic-ligatures-to-pdf.cs) | Add Text with Automatic Ligatures to PDF | `Document`, `TextFragment`, `FontRepository` | Shows how to load a PDF, add a TextFragment, and rely on the font rendering engine to handle liga... |
| [add-text-with-custom-line-spacing](./add-text-with-custom-line-spacing.cs) | Add Text with Custom Line Spacing to PDF | `Document`, `TextState`, `TextParagraph` | Shows how to insert a paragraph into a PDF page and control the line spacing by setting the TextS... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF using Aspose.Pdf | `Document`, `Page`, `TextFragment` | Shows how to create a PDF document, add a page, and insert an underlined text fragment by setting... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Adjust Word Spacing of Inserted Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to add a TextFragment to an existing PDF and change the spacing between words by settin... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to add a disclaimer as a TextFragment to the end of the first page's paragraph collecti... |
| [append-multi-line-textfragment-get-line-break-posi...](./append-multi-line-textfragment-get-line-break-positions.cs) | Append Multi‑Line TextFragment and Retrieve Line‑Break Posit... | `Document`, `Page`, `TextFragment` | Demonstrates how to add a multi‑line TextFragment to a PDF page and extract each line’s baseline ... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to set a TextFragment's background color via its TextState before adding the fragment t... |
| [apply-custom-otf-font-to-pdf](./apply-custom-otf-font-to-pdf.cs) | Apply Custom OTF Font to PDF TextFragment | `Document`, `Save`, `TextFragment` | Demonstrates loading an OTF font, embedding it, and applying it to a TextFragment to create style... |
| [batch-replace-text-in-pdfs](./batch-replace-text-in-pdfs.cs) | Batch Replace Text in PDF Files | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to iterate through a folder of PDF documents and replace multiple strings using ... |
| [batch-replace-text-pdf-config](./batch-replace-text-pdf-config.cs) | Batch Replace Text in PDF Using a Configuration File | `Document`, `Save`, `Accept` | Demonstrates how to load old‑new string pairs from a simple key=value file and replace all occurr... |
| [case-insensitive-keyword-highlighting](./case-insensitive-keyword-highlighting.cs) | Case‑Insensitive Keyword Highlighting in PDF | `Document`, `Save`, `Accept` | Demonstrates how to search a PDF for a keyword without regard to case and highlight each occurren... |
| [center-text-horizontally-in-pdf](./center-text-horizontally-in-pdf.cs) | Center Text Horizontally in a PDF | `Document`, `Page`, `TextFragment` | Shows how to center a TextFragment on a PDF page by setting TextState.HorizontalAlignment to Cent... |
| [create-clickable-text-launch-action](./create-clickable-text-launch-action.cs) | Create Clickable Text with Launch Action to Open a File | `Document`, `Save`, `Page` | Demonstrates adding a text fragment to a PDF and attaching a LaunchAction annotation so clicking ... |
| [create-footnote-with-image](./create-footnote-with-image.cs) | Create Footnote with Image in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add a footnote to a PDF page and insert an image into the footnote's paragrap... |
| [create-justified-text-pdf](./create-justified-text-pdf.cs) | Create Justified Text in a PDF using Aspose.Pdf | `Document`, `Save`, `Page` | Shows how to add a TextFragment to a PDF page, set its font, position, and apply HorizontalAlignm... |
| [create-multi-line-text-paragraph](./create-multi-line-text-paragraph.cs) | Create Multi‑Line Text Paragraph in PDF | `Document`, `Save`, `Page` | Shows how to create a multi‑line TextParagraph, configure its rectangle, wrapping, alignment, mar... |
| [create-rotated-text-paragraph](./create-rotated-text-paragraph.cs) | Create Rotated Text Paragraph with Multiple Fragments | `Document`, `Save`, `Page` | Demonstrates how to build a TextParagraph, add several TextFragment lines, set its bounding recta... |
| ... | | | *and 46 more files* |

## Category Statistics
- Total examples: 76

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Cell`
- `Aspose.Pdf.Cell.ColSpan`
- `Aspose.Pdf.Cell.Paragraphs`
- `Aspose.Pdf.Cell.VerticalAlignment`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Drawing.GradientAxialShading`
- `Aspose.Pdf.HtmlFragment`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.MarginInfo`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.PageCollection`
- `Aspose.Pdf.Rectangle`
- `Aspose.Pdf.Row`

### Rules
- Create a new Document instance ({doc}) and add a blank Page ({page}) using {doc}.Pages.Add().
- Instantiate an HtmlFragment ({html_fragment}) with an HTML string ({string_literal}) and insert it into the page via {page}.Paragraphs.Add({html_fragment}).
- Persist the PDF by calling {doc}.Save({output_pdf}).
- Create a new {doc} (Aspose.Pdf.Document) and add a {page} (Aspose.Pdf.Page) via doc.Pages.Add().
- Instantiate an {html_fragment} (Aspose.Pdf.Text.HtmlFragment) with an HTML {string_literal} and add it to the page using page.Paragraphs.Add({html_fragment}).

### Warnings
- The example assumes the Aspose.Pdf namespace is imported and the library is referenced.
- HtmlFragment requires well‑formed HTML; malformed markup may cause rendering issues.
- Margin values are in points; setting a large top margin (e.g., 400) may place the content outside the visible page area.
- HtmlFragment supports only a subset of HTML/CSS; complex layouts may not render as expected.
- SubsequentLinesIndent affects only lines that wrap within the same TextFragment; separate TextFragments are treated as separate paragraphs and will not inherit the indent.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_185541_4f51b3`
<!-- AUTOGENERATED:END -->

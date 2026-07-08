---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

> **Working with text** in PDF using C# / .NET -- **76** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (73/76 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (6/76 files)
- `using Aspose.Pdf.Drawing;` (2/76 files)
- `using Aspose.Pdf.Forms;` (2/76 files)
- `using Aspose.Pdf.Facades;` (1/76 files)
- `using Aspose.Pdf.Operators;` (1/76 files)
- `using System;` (76/76 files)
- `using System.IO;` (68/76 files)
- `using System.Collections.Generic;` (6/76 files)
- `using System.Text.RegularExpressions;` (3/76 files)
- `using System.Drawing;` (2/76 files)
- `using System.Drawing.Imaging;` (1/76 files)
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
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Hyperlink Text to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a TextFragment with a TextSegment that contains a web hyperlink and append it... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Add Footer Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert a footer with automatic page numbers on each page of a PDF using Aspos... |
| [add-header-to-all-pdf-pages](./add-header-to-all-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to iterate through a PDF document's pages and insert a header text fragment on each pag... |
| [add-html-fragment-with-styling-to-pdf-page](./add-html-fragment-with-styling-to-pdf-page.cs) | Add HTML Fragment with Styling to PDF Page | `Document`, `Page`, `HtmlFragment` | Demonstrates how to insert an HTML fragment containing bold and italic markup into an existing PD... |
| [add-internal-goto-link-to-pdf](./add-internal-goto-link-to-pdf.cs) | Add Internal GoTo Link to PDF Using TextFragment | `Document`, `Page`, `TextFragment` | Demonstrates how to create a clickable text fragment that navigates to a specific page within the... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate a specific text fragment in a PDF and place an invisible button field over it... |
| [add-javascript-action-to-text-annotation](./add-javascript-action-to-text-annotation.cs) | Add JavaScript Action to a PDF Text Annotation | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation, attach a JavaScript action to it, and save the modified PD... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Add Multi‑Line Text Paragraph to PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates how to create a TextParagraph, configure its layout, add multiple lines efficiently,... |
| [add-plain-text-to-pdf-page](./add-plain-text-to-pdf-page.cs) | Add Plain Text to a PDF Page using TextFragment | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its position on the first page, and save the ... |
| [add-rotated-mixed-style-text-paragraph-to-pdf-page...](./add-rotated-mixed-style-text-paragraph-to-pdf-page3.cs) | Add Rotated Mixed-Style Text Paragraph to PDF Page 3 | `Document`, `Page`, `TextParagraph` | Loads an existing PDF, creates a TextParagraph with mixed font styles, rotates it 45 degrees, and... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom-Right of Last PDF Page | `Document`, `Page`, `Rectangle` | Shows how to use Aspose.Pdf's TextBuilder and TextParagraph to place rotated text in a rectangle ... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder and TextFragment to place a rotated "CONFIDENTIA... |
| [add-semi-transparent-text-watermark-to-pdf-pages](./add-semi-transparent-text-watermark-to-pdf-pages.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to overlay a semi‑transparent text watermark on every page of a PDF by using a W... |
| [add-table-footnote-to-pdf](./add-table-footnote-to-pdf.cs) | Add Table Footnote to PDF Text | `Document`, `Page`, `TextFragment` | Shows how to create a footnote that contains a table and attach it to a text fragment in a PDF do... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Demonstrates loading an existing PDF, adding a text fragment, setting permissions, and encrypting... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF Using Aspose.Pdf | `Document`, `Page`, `TextFragment` | The example loads a PDF from a file into a MemoryStream (or creates a new PDF), adds a text fragm... |
| [add-text-with-ligatures-to-pdf](./add-text-with-ligatures-to-pdf.cs) | Add Text with Ligatures to PDF | `Document`, `TextFragment`, `Position` | Loads an existing PDF, creates a TextFragment, sets its font and size, and adds it to a page; lig... |
| [add-text-with-simulated-kerning-to-pdf](./add-text-with-simulated-kerning-to-pdf.cs) | Add Text with Simulated Kerning to PDF | `Document`, `Page`, `TextFragment` | Loads an existing PDF, creates a TextFragment, sets character spacing to simulate kerning, positi... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `TextFragment`, `Position` | Shows how to load an existing PDF, create a TextFragment, enable underlining via TextState, and s... |
| [adjust-word-spacing-inserted-text](./adjust-word-spacing-inserted-text.cs) | Adjust Word Spacing of Inserted Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set the TextState.WordSpacing property to control... |
| [append-disclaimer-text-to-first-pdf-page](./append-disclaimer-text-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to add a disclaimer as a TextFragment to the end of the paragraph collection on ... |
| [append-multiline-textfragment-linebreak-info](./append-multiline-textfragment-linebreak-info.cs) | Append Multi‑Line TextFragment and Retrieve Line‑Break Infor... | `Document`, `Page`, `TextFragment` | Demonstrates how to add a multi‑line TextFragment to a PDF page and iterate its TextSegment objec... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `TextFragment`, `TextState` | Shows how to set a background color for a TextFragment using its TextState before adding it to a ... |
| [apply-custom-otf-font-to-textfragment](./apply-custom-otf-font-to-textfragment.cs) | Apply Custom OpenType Font to TextFragment | `FontRepository`, `Font`, `Document` | Demonstrates loading an OpenType (OTF) font, embedding it, and applying it to a TextFragment to c... |
| [apply-font-size-and-color-to-textfragment](./apply-font-size-and-color-to-textfragment.cs) | Apply Font Size and Color to TextFragment | `Document`, `Page`, `TextFragment` | Shows how to set the font size and foreground color via TextState and assign the styled TextFragm... |
| [batch-replace-keywords-pdf](./batch-replace-keywords-pdf.cs) | Batch Replace Keywords in PDF Using a Configuration File | `Document`, `Page`, `TextFragmentAbsorber` | Shows how to load a CSV mapping of old and new strings and replace multiple keywords on every pag... |
| [batch-replace-text-in-pdf](./batch-replace-text-in-pdf.cs) | Batch Replace Text in PDF Using Aspose.Pdf | `Document`, `Page`, `TextFragmentAbsorber` | Demonstrates how to load a PDF, iterate over a dictionary of old‑new string pairs, and replace al... |
| [center-text-on-pdf-page](./center-text-on-pdf-page.cs) | Center Text on PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates adding a text fragment to a PDF and centering it horizontally by setting TextState.H... |
| [create-footnote-with-image](./create-footnote-with-image.cs) | Create Footnote with Image in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add a footnote to a PDF page and embed an in‑memory PNG image into the footno... |
| [create-pdf-launch-action-open-file](./create-pdf-launch-action-open-file.cs) | Create PDF with Launch Action to Open External File | `Document`, `Page`, `TextFragment` | Shows how to add a clickable text element to a PDF that launches an external file using Aspose.Pd... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

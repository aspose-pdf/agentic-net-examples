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

- `using Aspose.Pdf;` (74/75 files) ← category-specific
- `using Aspose.Pdf.Text;` (70/75 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (7/75 files)
- `using Aspose.Pdf.Drawing;` (3/75 files)
- `using Aspose.Pdf.Forms;` (2/75 files)
- `using Aspose.Pdf.Facades;` (1/75 files)
- `using System;` (75/75 files)
- `using System.IO;` (62/75 files)
- `using System.Runtime.InteropServices;` (8/75 files)
- `using System.Collections.Generic;` (5/75 files)
- `using System.Text.RegularExpressions;` (4/75 files)
- `using System.Text;` (2/75 files)
- `using System.Text.Json;` (2/75 files)
- `using System.Linq;` (1/75 files)

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
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Hyperlink Text to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a TextFragment with a WebHyperlink into an existing PDF, creating a clickable... |
| [add-clickable-hyperlink-to-text](./add-clickable-hyperlink-to-text.cs) | Add Clickable Hyperlink to Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to search for a specific phrase in a PDF and replace it with a clickable hyperlink usin... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Add Footer Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert a centered footer with page numbers on each page of a PDF using Aspose... |
| [add-formatted-endnote-to-pdf](./add-formatted-endnote-to-pdf.cs) | Add Formatted Endnote with Bold and Italic Text | `Document`, `TextFragment`, `Note` | Demonstrates how to insert an endnote into a PDF and apply bold and italic formatting using a Tex... |
| [add-header-to-all-pdf-pages](./add-header-to-all-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, iterate through each page, and insert a centered header text at the top ... |
| [add-html-fragment-to-pdf](./add-html-fragment-to-pdf.cs) | Add HTML Fragment with Styled Text to PDF | `Document`, `Page`, `HtmlFragment` | Shows how to insert an HTML fragment containing styled markup into a PDF page using HtmlFragment ... |
| [add-hyperlink-to-text-segment](./add-hyperlink-to-text-segment.cs) | Add Hyperlink to Text Segment in PDF | `Document`, `Page`, `TextFragment` | Shows how to create a clickable hyperlink inside a PDF by assigning a WebHyperlink to a TextSegme... |
| [add-internal-text-link-go-to-action](./add-internal-text-link-go-to-action.cs) | Add Internal Text Link to PDF Using GoToAction | `Document`, `Page`, `TextFragment` | Loads an existing PDF, inserts a visible text fragment as a hyperlink, and creates a LinkAnnotati... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate a specific text string in a PDF, obtain its rectangle, and place an invisible... |
| [add-launch-action-to-pdf](./add-launch-action-to-pdf.cs) | Add Launch Action to PDF Text | `Document`, `Page`, `Rectangle` | Demonstrates how to place clickable text in a PDF that opens an external file using a launch acti... |
| [add-left-aligned-text-to-pdf](./add-left-aligned-text-to-pdf.cs) | Add Left-Aligned Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates loading a PDF, creating a TextFragment, setting its horizontal alignment to left, po... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Add Multi‑Line Text Paragraph to PDF | `Document`, `Page`, `TextParagraph` | Demonstrates creating a multi‑line TextParagraph, configuring its rectangle, wrapping, alignment,... |
| [add-rotated-mixed-style-paragraph-to-pdf](./add-rotated-mixed-style-paragraph-to-pdf.cs) | Add Rotated Mixed‑Style Text Paragraph to PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates loading a PDF, creating a TextParagraph with mixed font styles, rotating it, and ins... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom‑Right of Last PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated text paragraph at the bottom‑... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder and TextFragment to place a semi‑transparent, ro... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextState` | Shows how to overlay a semi‑transparent text watermark on every page of a PDF using Aspose.Pdf by... |
| [add-table-inside-footnote-to-pdf](./add-table-inside-footnote-to-pdf.cs) | Add Table Inside Footnote to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to create a footnote (Note) for a TextFragment and populate its Paragraphs colle... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads an existing PDF, adds a red 'Confidential' text fragment to the first page, then encrypts t... |
| [add-text-fragment-to-pdf](./add-text-fragment-to-pdf.cs) | Add Text Fragment to PDF with Ligature Support | `Document`, `TextFragment`, `FindFont` | Demonstrates loading a PDF, creating a TextFragment, configuring its TextState (font and size), p... |
| [add-text-fragment-with-character-spacing](./add-text-fragment-with-character-spacing.cs) | Add Text Fragment with Character Spacing | `Document`, `TextFragment`, `TextState` | Demonstrates how to add a TextFragment to a PDF, set its font, size, position, and adjust charact... |
| [add-text-to-pdf-memory-stream](./add-text-to-pdf-memory-stream.cs) | Add Text to PDF Using Memory Streams | `Document`, `Page`, `TextFragment` | Demonstrates loading a PDF from a stream, inserting a text fragment on the first page, and return... |
| [add-text-to-pdf-page](./add-text-to-pdf-page.cs) | Add Text to a PDF Page Using TextFragment | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its position and appearance, append it to a p... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `TextFragment`, `TextState` | Loads an existing PDF, creates a TextFragment, sets its Underline property, positions it on the p... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Adjust Word Spacing of Inserted Text in PDF | `Document`, `TextFragment`, `TextState` | Shows how to set the TextState.WordSpacing property of a TextFragment to increase spacing between... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to add a disclaimer as a TextFragment to the end of the first page's paragraph collecti... |
| [append-multiple-textfragments-get-line-positions](./append-multiple-textfragments-get-line-positions.cs) | Append Multiple TextFragments and Get Line‑Break Positions | `Document`, `Page`, `TextFragment` | Demonstrates how to add several TextFragment objects to a PDF page in one operation and retrieve ... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `TextFragment`, `TextState` | Demonstrates how to set a background color for a text fragment by configuring its TextState befor... |
| [apply-custom-otf-font-to-pdf](./apply-custom-otf-font-to-pdf.cs) | Apply Custom OpenType Font to PDF Text | `OpenFont`, `Font`, `Document` | Shows how to load an OTF font, embed it in a PDF, and apply it to a TextFragment for styled text ... |
| [apply-custom-true-type-font-to-pdf](./apply-custom-true-type-font-to-pdf.cs) | Apply Custom TrueType Font to PDF Text | `Document`, `Page`, `TextFragment` | Shows how to load a TrueType font from a memory stream, embed it, and assign it to a TextState fo... |
| [attach-javascript-action-to-text-annotation](./attach-javascript-action-to-text-annotation.cs) | Attach JavaScript Action to a PDF Text Annotation | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a visible link annotation, attaches a JavaScript action that displays an ale... |
| ... | | | *and 45 more files* |

## Category Statistics
- Total examples: 75

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
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->

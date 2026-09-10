---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

> **Working with text** in PDF using C# / .NET -- **73** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-text** category.
This folder contains standalone C# examples for working-with-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (73/73 files) ← category-specific
- `using Aspose.Pdf.Text;` (71/73 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (5/73 files)
- `using Aspose.Pdf.Drawing;` (3/73 files)
- `using Aspose.Pdf.Forms;` (2/73 files)
- `using Aspose.Pdf.Facades;` (1/73 files)
- `using System;` (73/73 files)
- `using System.IO;` (65/73 files)
- `using System.Collections.Generic;` (5/73 files)
- `using System.Text.RegularExpressions;` (5/73 files)
- `using System.Linq;` (1/73 files)
- `using System.Text;` (1/73 files)
- `using System.Text.Json;` (1/73 files)

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
| [add-arabic-bidi-text-to-pdf](./add-arabic-bidi-text-to-pdf.cs) | Add Arabic Bidirectional Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to open an existing PDF, create a TextFragment with Arabic Unicode characters, c... |
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Hyperlink Text to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a TextSegment with a WebHyperlink and append it to a PDF page using Aspose.Pdf. |
| [add-endnote-bold-italic](./add-endnote-bold-italic.cs) | Add Endnote with Bold and Italic Formatting to PDF | `Document`, `TextFragment`, `Note` | Demonstrates how to insert an endnote into a PDF and apply bold and italic formatting using TextS... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Add Footer Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert a footer with automatic page numbers on every page of an existing PDF using A... |
| [add-footnote-with-image-to-pdf](./add-footnote-with-image-to-pdf.cs) | Add Footnote with Image to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a footnote (Note) for a TextFragment, insert an image into the footnote's Par... |
| [add-header-to-pdf-pages](./add-header-to-pdf-pages.cs) | Add Header to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to loop through a PDF's Pages collection and add a repeated header string to each page ... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate a specific text fragment in a PDF and overlay an invisible button field that ... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Add Multi‑Line Text Paragraph to PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates creating a TextParagraph, setting its rectangle and word‑wrap options, appending mul... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom‑Right of Last PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated text fragment at the bottom‑r... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextBuilder` | Shows how to use Aspose.Pdf's TextBuilder and TextFragment to place a rotated "CONFIDENTIAL" wate... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `TextState` | Demonstrates how to overlay a semi-transparent watermark text on each page of a PDF using Aspose.... |
| [add-styled-html-fragment-to-pdf-page](./add-styled-html-fragment-to-pdf-page.cs) | Add Styled HTML Fragment to PDF Page | `Document`, `HtmlFragment`, `TextState` | Shows how to insert a styled HTML fragment into an existing PDF document using Aspose.Pdf's HtmlF... |
| [add-table-to-pdf-footnote](./add-table-to-pdf-footnote.cs) | Insert a Table into a PDF Footnote | `Document`, `Page`, `TextFragment` | Demonstrates how to create a footnote for a text fragment and populate it with a table, then add ... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads an existing PDF, adds a red text fragment to the first page, then encrypts the document wit... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF from Memory Stream | `Document`, `Page`, `TextFragment` | Shows how to load a PDF from a Stream, insert a text fragment on the first page, and write the up... |
| [add-text-to-pdf-page-at-specific-coordinates](./add-text-to-pdf-page-at-specific-coordinates.cs) | Add Text to PDF Page at Specific Coordinates | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its position using X/Y coordinates, and save ... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `Page`, `TextFragment` | Creates a PDF document, adds a page, and inserts a text fragment with underlining by setting Text... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Adjust Word Spacing for Inserted Text in PDF | `Document`, `TextFragment`, `TextState` | Demonstrates how to load an existing PDF, create a TextFragment, set its WordSpacing property, an... |
| [append-a-multi-line-textfragment-to-a-page-and-ret...](./append-a-multi-line-textfragment-to-a-page-and-retrieve-its-line-break-information-for-custom-rendering.cs) | Append A Multi Line Textfragment To A Page And Retrieve Its ... | `TextFragment`, `TextBuilder` | Append A Multi Line Textfragment To A Page And Retrieve Its Line Break Information For Custom Ren... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a formatted TextFragment, and add it to the end of the first page... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to set a background color for a text fragment in a PDF using Aspose.Pdf by confi... |
| [apply-opentype-font-to-textfragment](./apply-opentype-font-to-textfragment.cs) | Apply OpenType Font to TextFragment in PDF | `Document`, `Page`, `TextFragment` | Demonstrates loading an OpenType (OTF) font, embedding it, and applying it to a TextFragment to c... |
| [batch-replace-keywords-in-pdf](./batch-replace-keywords-in-pdf.cs) | Batch Replace Keywords in PDF Using a Configuration File | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to read a mapping file and perform batch text replacements in a PDF document usi... |
| [batch-replace-multiple-strings-in-pdf](./batch-replace-multiple-strings-in-pdf.cs) | Batch Replace Multiple Strings in PDF | `Document`, `TextFragmentAbsorber`, `TextReplaceOptions` | Shows how to replace several text strings in a PDF by iterating a dictionary of old‑new pairs usi... |
| [center-text-horizontally-in-pdf](./center-text-horizontally-in-pdf.cs) | Center Text Horizontally in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to place a text fragment at the horizontal center of a PDF page using Aspose.Pdf... |
| [convert-modified-pdf-to-byte-array](./convert-modified-pdf-to-byte-array.cs) | Convert Modified PDF to Byte Array | `Document`, `Save`, `Pages` | Loads a PDF, adds a blank page, and returns the modified document as a byte array using a MemoryS... |
| [create-pdf-launch-action-open-external-file](./create-pdf-launch-action-open-external-file.cs) | Create PDF with Launch Action to Open External File | `Document`, `Page`, `TextFragment` | Demonstrates how to add clickable text in a PDF that launches an external file using Aspose.Pdf's... |
| [create-rotated-text-paragraph](./create-rotated-text-paragraph.cs) | Create Rotated Text Paragraph in PDF | `Document`, `Page`, `TextParagraph` | Demonstrates how to build a TextParagraph with multiple TextFragments, set its bounding rectangle... |
| [create-strikethrough-text-pdf](./create-strikethrough-text-pdf.cs) | Create Strikethrough Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to apply strikeout formatting to a TextFragment by setting TextState.StrikeOut and addi... |
| [create-text-link-goto-action](./create-text-link-goto-action.cs) | Create Text Link with GoTo Action in PDF | `Document`, `Page`, `TextFragment` | Shows how to add a clickable text fragment that jumps to another page by using a LinkAnnotation w... |
| ... | | | *and 43 more files* |

## Category Statistics
- Total examples: 73

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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

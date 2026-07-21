---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

> **Working with text** in PDF using C# / .NET -- **73** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (69/73 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (3/73 files)
- `using Aspose.Pdf.Drawing;` (2/73 files)
- `using Aspose.Pdf.Forms;` (2/73 files)
- `using Aspose.Pdf.Operators;` (1/73 files)
- `using System;` (73/73 files)
- `using System.IO;` (62/73 files)
- `using System.Collections.Generic;` (5/73 files)
- `using System.Text.RegularExpressions;` (3/73 files)
- `using System.Drawing;` (1/73 files)
- `using System.Drawing.Imaging;` (1/73 files)
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
| [add-arabic-bidi-text-to-pdf](./add-arabic-bidi-text-to-pdf.cs) | Add Arabic Bidirectional Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to insert right‑to‑left Arabic text into an existing PDF using Aspose.Pdf, relyi... |
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Hyperlink Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to insert a text fragment with a clickable segment that opens a web URL using As... |
| [add-clickable-hyperlink-to-pdf-text](./add-clickable-hyperlink-to-pdf-text.cs) | Add Clickable Hyperlink to PDF Text | `Document`, `Page`, `TextFragment` | Shows how to insert a clickable hyperlink into a PDF by creating a TextFragment, adding a TextSeg... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Add Footer Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Shows how to insert a footer containing page numbers on each page of a PDF using Aspose.Pdf's Pag... |
| [add-header-to-all-pdf-pages](./add-header-to-all-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to loop through a PDF's Pages collection and attach the same header text to each page u... |
| [add-html-fragment-to-pdf](./add-html-fragment-to-pdf.cs) | Add HTML Fragment with Styled Text to PDF | `Document`, `Page`, `HtmlFragment` | Shows how to insert a styled HTML fragment into a PDF page using Aspose.Pdf's HtmlFragment and cu... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Add Multi‑Line Text Paragraph to PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates creating a TextParagraph, configuring its rectangle, wrap mode, alignment, and margi... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom-Right Corner of Last PDF Page | `Document`, `Page`, `TextParagraph` | Demonstrates using Aspose.Pdf's TextBuilder to place a rotated text paragraph at the bottom‑right... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to All PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to use Aspose.Pdf's TextBuilder and TextFragment to place a rotated "CONFIDENTIAL" wate... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF | `Document`, `Page`, `TextState` | Demonstrates how to overlay a semi‑transparent text watermark on every page of a PDF using Aspose... |
| [add-table-to-footnote-in-pdf](./add-table-to-footnote-in-pdf.cs) | Add Table to Footnote in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to create a footnote for a text fragment and populate it with a table using Aspo... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads an existing PDF, adds a red text fragment, sets specific permissions, encrypts the document... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF from Memory Stream | `Document`, `Page`, `TextFragment` | Demonstrates loading a PDF from a Stream, inserting a text fragment on the first page, and return... |
| [add-text-to-pdf-page-at-specific-coordinates](./add-text-to-pdf-page-at-specific-coordinates.cs) | Add Text to PDF Page at Specific Coordinates | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its position using X/Y coordinates, and save ... |
| [add-text-with-font-settings-to-pdf](./add-text-with-font-settings-to-pdf.cs) | Add Text with Font Settings to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a PDF, add a page, insert a TextFragment, configure its font, size, and color... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `Page`, `TextFragment` | Creates a PDF document and inserts a text fragment with underlining by setting the TextState.Unde... |
| [adjust-word-spacing-inserted-text](./adjust-word-spacing-inserted-text.cs) | Adjust Word Spacing of Inserted Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to add a text fragment to a PDF page and customize the spacing between words using the ... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to add a disclaimer as a TextFragment to the end of the paragraph collection on ... |
| [append-multiline-textfragment-line-break-info](./append-multiline-textfragment-line-break-info.cs) | Append Multi-line TextFragment and Retrieve Line Break Infor... | `Document`, `Page`, `TextFragment` | Demonstrates adding a multi-line TextFragment to a PDF page with Aspose.Pdf and extracting each l... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `TextFragment`, `TextState` | Demonstrates how to set a background color on a TextFragment's TextState before adding it to a PD... |
| [apply-custom-otf-font-to-textfragment](./apply-custom-otf-font-to-textfragment.cs) | Apply Custom OTF Font to TextFragment | `OpenFont`, `Font`, `Document` | Demonstrates loading an OpenType (OTF) font, embedding it, and applying it to a TextFragment to c... |
| [batch-replace-text-in-pdfs](./batch-replace-text-in-pdfs.cs) | Batch Replace Text in PDFs Using Aspose.Pdf | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to process a folder of PDF files and replace multiple strings with new values us... |
| [batch-replace-text-pdf-config](./batch-replace-text-pdf-config.cs) | Batch Replace Text in PDF Using a Configuration File | `Document`, `Page`, `TextFragmentAbsorber` | Demonstrates how to load key‑value pairs from a config file and replace multiple text strings acr... |
| [center-text-on-pdf-page](./center-text-on-pdf-page.cs) | Center Text on PDF Page | `Document`, `Page`, `TextFragment` | Shows how to center‑align a text fragment on a PDF page using Aspose.Pdf by setting TextState.Hor... |
| [create-endnote-bold-italic](./create-endnote-bold-italic.cs) | Create Endnote with Bold and Italic Text in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add an endnote to a PDF and format its text using TextState to apply bold and... |
| [create-footnote-with-image](./create-footnote-with-image.cs) | Create Footnote with Image in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add a footnote to a PDF page and insert an in‑memory PNG image into the footn... |
| [create-internal-text-link-to-page](./create-internal-text-link-to-page.cs) | Create Internal Text Link to Specific Page in PDF | `Document`, `TextFragment`, `Position` | Shows how to add a clickable TextFragment that navigates to a given page within the same PDF usin... |
| [create-rotated-text-paragraph](./create-rotated-text-paragraph.cs) | Create Rotated Text Paragraph with Multiple Fragments | `Document`, `Page`, `TextParagraph` | Shows how to build a TextParagraph containing several TextFragments, apply a 30‑degree rotation, ... |
| [create-strikethrough-text](./create-strikethrough-text.cs) | Create Strikethrough Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to apply strikeout formatting to a TextFragment using the TextState property and save t... |
| [embed-css-in-htmlfragment](./embed-css-in-htmlfragment.cs) | Embed CSS in HtmlFragment for Styled PDF Text | `Document`, `Page`, `HtmlFragment` | Shows how to embed CSS rules inside an HtmlFragment to control font, color, margins, and line spa... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

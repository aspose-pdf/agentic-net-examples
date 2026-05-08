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

- `using Aspose.Pdf;` (74/74 files) ← category-specific
- `using Aspose.Pdf.Text;` (71/74 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (7/74 files)
- `using Aspose.Pdf.Drawing;` (2/74 files)
- `using Aspose.Pdf.Forms;` (2/74 files)
- `using System;` (74/74 files)
- `using System.IO;` (65/74 files)
- `using System.Runtime.InteropServices;` (10/74 files)
- `using System.Collections.Generic;` (8/74 files)
- `using System.Text.RegularExpressions;` (4/74 files)
- `using System.Linq;` (3/74 files)
- `using System.Text;` (1/74 files)
- `using System.Text.Json;` (1/74 files)

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
| [add-bidirectional-text-to-pdf](./add-bidirectional-text-to-pdf.cs) | Add Bidirectional Text to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert right‑to‑left (RTL) text such as Arabic into a PDF by using a Unicode RTL mar... |
| [add-clickable-text-hyperlink-to-pdf](./add-clickable-text-hyperlink-to-pdf.cs) | Add Clickable Text with Hyperlink to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a TextFragment, assign a WebHyperlink to a TextSegment, and append the linked... |
| [add-header-to-all-pdf-pages](./add-header-to-all-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `HeaderFooter` | Shows how to loop through a PDF's Pages collection and add a header string to each page using Asp... |
| [add-image-footnote-to-pdf](./add-image-footnote-to-pdf.cs) | Add Image Footnote to PDF Page | `Document`, `Page`, `TextFragment` | Shows how to create a footnote on a PDF page and insert an image into its Paragraphs collection u... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate specific text in a PDF and overlay an invisible button field that acts as a t... |
| [add-launch-action-to-text](./add-launch-action-to-text.cs) | Add Launch Action to Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a clickable text fragment into a PDF and attach a LaunchAction that opens an ... |
| [add-left-aligned-text-to-pdf](./add-left-aligned-text-to-pdf.cs) | Add Left-Aligned Text to PDF | `Document`, `TextFragment`, `TextState` | Demonstrates how to insert a text fragment into a PDF and align it to the left margin using Aspos... |
| [add-page-number-footer-to-pdf](./add-page-number-footer-to-pdf.cs) | Add Page Number Footer to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert a footer containing page numbers on each page of a PDF using Aspose.Pdf's Pag... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom‑Right Corner of Last PDF Page | `Document`, `Page`, `PageInfo` | The example loads a PDF, calculates a rectangle at the bottom‑right corner of the last page, and ... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated "CONFIDENTIAL" text fragment ... |
| [add-rtl-arabic-text-to-pdf](./add-rtl-arabic-text-to-pdf.cs) | Add Right-to-Left Arabic Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates inserting Arabic text with right‑to‑left direction into a PDF, creating the file if ... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to overlay a semi‑transparent text watermark on each page of a PDF using Aspose.... |
| [add-styled-html-fragment-to-pdf-page](./add-styled-html-fragment-to-pdf-page.cs) | Add Styled HTML Fragment to PDF Page | `Document`, `HtmlFragment`, `TextState` | Shows how to insert a styled HTML fragment into a PDF page using HtmlFragment and an optional Tex... |
| [add-table-inside-footnote](./add-table-inside-footnote.cs) | Add a Table Inside a Footnote in a PDF | `Document`, `Note`, `Table` | Shows how to create a footnote containing a table and attach it to a text fragment in a PDF using... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads an existing PDF, adds a text fragment to the first page, then encrypts the document with us... |
| [add-text-at-specific-coordinates-to-pdf-page](./add-text-at-specific-coordinates-to-pdf-page.cs) | Add Text at Specific Coordinates to a PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates loading an existing PDF, creating a TextFragment, positioning it at given X/Y coordi... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF from Memory Stream | `Document`, `Page`, `TextFragment` | Demonstrates loading a PDF from a MemoryStream, inserting a text fragment on the first page, and ... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `TextFragment`, `Position` | Shows how to load a PDF, create a TextFragment, enable underlining via TextState.Underline, and s... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Adjust Word Spacing of Inserted Text in PDF | `Document`, `Page`, `TextFragment` | Shows how to increase the spacing between words by setting the TextState.WordSpacing property of ... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a formatted TextFragment, and append it to the end of the first p... |
| [append-multiline-textfragment-line-break-info](./append-multiline-textfragment-line-break-info.cs) | Append Multi-line TextFragment and Retrieve Line Break Infor... | `Document`, `Page`, `TextFragment` | The example loads a PDF, appends a multi-line TextFragment to the first page, and iterates the fr... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to set a background color for a text fragment using the TextState property befor... |
| [apply-custom-opentype-font-to-pdf](./apply-custom-opentype-font-to-pdf.cs) | Apply Custom OpenType Font to PDF Text | `OpenFont`, `IsEmbedded`, `Document` | Shows how to load an OTF font, embed it, and apply it to a TextFragment for styled PDF output. |
| [attach-javascript-action-to-pdf-link-annotation](./attach-javascript-action-to-pdf-link-annotation.cs) | Attach JavaScript Action to PDF Link Annotation | `Document`, `LinkAnnotation`, `JavascriptAction` | Opens an existing PDF, creates a link annotation on the first page, assigns a JavaScript action t... |
| [batch-replace-multiple-keywords-pdf](./batch-replace-multiple-keywords-pdf.cs) | Batch Replace Multiple Keywords in PDF Using a Config File | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to load old‑new string mappings from a configuration file and replace all occurr... |
| [batch-replace-text-in-pdfs](./batch-replace-text-in-pdfs.cs) | Batch Replace Text in PDFs Using Aspose.Pdf | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to iterate through PDF files in a folder and replace multiple old strings with new stri... |
| [center-text-on-pdf-page](./center-text-on-pdf-page.cs) | Center Text on PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to center a text fragment on a PDF page by setting the TextState's HorizontalAli... |
| [create-goto-link-in-pdf](./create-goto-link-in-pdf.cs) | Create GoTo Link in PDF Using TextFragment | `Document`, `TextFragment`, `LocalHyperlink` | Shows how to add a text fragment that functions as a hyperlink to navigate to a specific page in ... |
| [create-multi-line-text-paragraph](./create-multi-line-text-paragraph.cs) | Create Multi-Line Text Paragraph in PDF | `Document`, `Page`, `TextParagraph` | Demonstrates how to build a multi‑line TextParagraph, configure its layout, and render it onto a ... |
| [create-rotated-text-paragraph](./create-rotated-text-paragraph.cs) | Create Rotated Text Paragraph with Multiple Fragments | `Document`, `Page`, `TextParagraph` | Shows how to build a TextParagraph with several TextFragments, apply a 30-degree rotation, and ad... |
| ... | | | *and 44 more files* |

## Category Statistics
- Total examples: 74

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
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

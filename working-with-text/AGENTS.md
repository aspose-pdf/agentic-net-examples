---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

> **Working with text** in PDF using C# / .NET -- **76** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Annotations;` (9/76 files)
- `using Aspose.Pdf.Forms;` (2/76 files)
- `using Aspose.Pdf.Drawing;` (1/76 files)
- `using Aspose.Pdf.LogicalStructure;` (1/76 files)
- `using Aspose.Pdf.Tagged;` (1/76 files)
- `using System;` (76/76 files)
- `using System.IO;` (73/76 files)
- `using System.Collections.Generic;` (6/76 files)
- `using System.Text.RegularExpressions;` (3/76 files)
- `using System.Text.Json;` (2/76 files)
- `using System.Text;` (1/76 files)

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
| [add-arabic-bidi-text-to-pdf](./add-arabic-bidi-text-to-pdf.cs) | Add Bidirectional Text to PDF using Aspose.PDF | `Document`, `Page`, `TextFragment` | Shows how to insert mixed left‑to‑right and right‑to‑left Unicode text (e.g., English, Arabic, He... |
| [add-arabic-rtl-text-to-pdf](./add-arabic-rtl-text-to-pdf.cs) | Add Arabic RTL Text to PDF | `Document`, `TextFragment`, `FontRepository` | Shows how to load an existing PDF, create a TextFragment with Arabic characters, set a Unicode fo... |
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Text Link to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a styled text fragment into a PDF and attach a URI link annotation so the tex... |
| [add-endnote-bold-italic](./add-endnote-bold-italic.cs) | Add Endnote with Bold and Italic Styling to PDF | `Document`, `TextAbsorber`, `Page` | Demonstrates loading a PDF, optionally extracting its text, adding a new page, creating a TextFra... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Add Footer with Page Numbers to PDF | `Document`, `Save`, `Page` | Shows how to insert a footer containing page numbers on each page of a PDF using Aspose.Pdf's Tex... |
| [add-footnote-with-image-to-pdf](./add-footnote-with-image-to-pdf.cs) | Add Footnote with Image to PDF Page | `Document`, `Page`, `TextFragment` | Shows how to create a footnote on a PDF page and insert an image into the page's Paragraphs colle... |
| [add-header-to-pdf-pages](./add-header-to-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to iterate through a PDF document's pages and insert a text header on each page using A... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Button Over Specific Text | `Document`, `TextFragmentAbsorber`, `TextSearchOptions` | Demonstrates locating a target string in a PDF with TextFragmentAbsorber, then overlaying an invi... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Add Multi‑Line Text to PDF | `Document`, `Page`, `TextFragment` | Demonstrates creating a PDF document, adding a page, inserting a multi‑line TextFragment with cus... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom‑Right Corner of Last PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextBuilder to place a rotated text fragment at the bottom‑r... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to use Aspose.Pdf's TextStamp and TextState to place a semi‑transparent, rotated... |
| [add-semi-transparent-text-to-pdf](./add-semi-transparent-text-to-pdf.cs) | Add Semi-Transparent Text to a PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to set text opacity by configuring the TextState's color with an alpha component... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark to PDF | `Document`, `TextStamp`, `TextState` | Demonstrates how to overlay a semi‑transparent text watermark on every page of a PDF using Aspose... |
| [add-styled-html-fragment-to-pdf-page](./add-styled-html-fragment-to-pdf-page.cs) | Add Styled HTML Fragment to PDF | `Document`, `Page`, `HtmlFragment` | Shows how to insert a styled HTML fragment into a PDF page using HtmlFragment and customize its a... |
| [add-table-to-pdf-footnote](./add-table-to-pdf-footnote.cs) | Add Table to Footnote in a Tagged PDF | `Document`, `ITaggedContent`, `NoteElement` | Demonstrates how to create a footnote (NoteElement) in a tagged PDF and populate it with a table ... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | Loads a PDF, adds a text fragment to the first page, then encrypts the document with user and own... |
| [add-text-annotation-to-pdf-page](./add-text-annotation-to-pdf-page.cs) | Add Text Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a text annotation with title, contents and icon, adds it to the first page, ... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF from Memory Stream | `Document`, `TextFragment`, `Position` | Shows how to create a PDF in memory, load it from a MemoryStream, insert a TextFragment, and save... |
| [add-text-to-pdf-page-at-specific-coordinates](./add-text-to-pdf-page-at-specific-coordinates.cs) | Add Text at Specific Coordinates to a PDF Page | `Document`, `TextFragment`, `Position` | Demonstrates loading a PDF, creating a TextFragment, positioning it using X/Y coordinates, adding... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `TextFragment`, `TextState` | Loads an existing PDF, creates a TextFragment, enables underline via TextState, and inserts the f... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Insert Text with Custom Word Spacing into PDF | `Document`, `TextFragment`, `TextState` | Shows how to add a TextFragment to a PDF page and adjust the spacing between words using the Text... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment with a disclaimer, append it to the first page's p... |
| [append-multiline-textfragment-line-breaks](./append-multiline-textfragment-line-breaks.cs) | Append Multi‑Line TextFragment and Retrieve Line Breaks | `Document`, `Page`, `TextFragment` | Loads a PDF, appends a multi‑line TextFragment to the first page, and shows how to extract line‑b... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `Page`, `TextFragment` | Creates a PDF document, adds a TextFragment, sets its TextState.BackgroundColor to Yellow, and sa... |
| [apply-opentype-font-to-textfragment](./apply-opentype-font-to-textfragment.cs) | Apply Custom OpenType Font to a TextFragment | `FontRepository`, `SimpleFontSubstitution`, `Document` | Demonstrates how to register an OTF font with Aspose.Pdf, create a TextFragment, apply the custom... |
| [batch-replace-keywords-in-pdf](./batch-replace-keywords-in-pdf.cs) | Batch Replace Text in PDF Using JSON Configuration | `Document`, `Page`, `TextFragmentAbsorber` | Demonstrates loading a PDF, reading a JSON map of old‑to‑new strings, and replacing all occurrenc... |
| [batch-replace-multiple-strings-in-pdf](./batch-replace-multiple-strings-in-pdf.cs) | Batch Replace Multiple Strings in PDFs | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to process a folder of PDF files and replace several specified strings with new ... |
| [center-text-horizontally-in-pdf](./center-text-horizontally-in-pdf.cs) | Center Text Horizontally in PDF | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its TextState.HorizontalAlignment to Center, ... |
| [convert-modified-pdf-to-byte-array](./convert-modified-pdf-to-byte-array.cs) | Save Modified PDF to Byte Array | `Document`, `Page`, `Rectangle` | The example loads a PDF, adds a text annotation, and saves the modified document into a byte arra... |
| [create-pdf-launch-action-open-external-file](./create-pdf-launch-action-open-external-file.cs) | Add Launch Action to PDF Text to Open External File | `Document`, `Page`, `TextFragment` | Demonstrates how to add a clickable text element in a PDF that launches an external file using a ... |
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
Updated: 2026-10-22 | Run: `20261022_001807_a68eac`
<!-- AUTOGENERATED:END -->

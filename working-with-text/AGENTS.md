---
name: working-with-text
description: C# examples for working-with-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-text

> **Working with text** in PDF using C# / .NET -- **76** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (70/76 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (10/76 files)
- `using Aspose.Pdf.Forms;` (3/76 files)
- `using Aspose.Pdf.Drawing;` (2/76 files)
- `using Aspose.Pdf.LogicalStructure;` (1/76 files)
- `using Aspose.Pdf.Tagged;` (1/76 files)
- `using System;` (76/76 files)
- `using System.IO;` (74/76 files)
- `using System.Collections.Generic;` (6/76 files)
- `using System.Text.RegularExpressions;` (3/76 files)
- `using System.Text.Json;` (2/76 files)
- `using System.Drawing;` (1/76 files)
- `using System.Linq;` (1/76 files)
- `using System.Reflection;` (1/76 files)
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
| [add-arabic-bidi-text-to-pdf](./add-arabic-bidi-text-to-pdf.cs) | Generate PDF with Mixed LTR and RTL Text | `Document`, `Page`, `TextFragment` | Demonstrates how to render left‑to‑right and right‑to‑left Unicode characters in a PDF using Aspo... |
| [add-arabic-rtl-text-to-pdf](./add-arabic-rtl-text-to-pdf.cs) | Add Right-to-Left Arabic Text to PDF | `Document`, `TextFragment`, `FontRepository` | Loads an existing PDF, inserts an Arabic text fragment with right‑to‑left direction using a Unico... |
| [add-clickable-hyperlink-text-to-pdf](./add-clickable-hyperlink-text-to-pdf.cs) | Add Clickable Text Link to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a TextFragment onto a PDF page and attach a URI link annotation so the text b... |
| [add-endnote-bold-italic](./add-endnote-bold-italic.cs) | Insert Endnote with Bold and Italic Formatting | `Document`, `Page`, `TextFragment` | Shows how to add an endnote to the last page of a PDF and apply bold and italic styles using a Te... |
| [add-footer-page-numbers-to-pdf](./add-footer-page-numbers-to-pdf.cs) | Insert Page Number Footer into PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to load an existing PDF, iterate through its pages, and add a footer with page n... |
| [add-footnote-with-image-to-pdf](./add-footnote-with-image-to-pdf.cs) | Insert Image into PDF Footnote | `Document`, `Page`, `TextFragment` | Demonstrates how to create a footnote on a PDF page, add descriptive text, and embed an image wit... |
| [add-header-to-pdf-pages](./add-header-to-pdf-pages.cs) | Add Header to All PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to iterate through a PDF document's pages and add a header text fragment to each page u... |
| [add-invisible-tooltip-button-over-text](./add-invisible-tooltip-button-over-text.cs) | Add Invisible Tooltip Buttons Over Text in PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to locate specific text in a PDF, overlay an invisible ButtonField on each occur... |
| [add-multi-line-text-paragraph](./add-multi-line-text-paragraph.cs) | Create Multi‑Line Text Paragraph in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to build a multi‑line paragraph using TextFragment, configure its formatting and... |
| [add-rotated-text-bottom-right-last-page](./add-rotated-text-bottom-right-last-page.cs) | Add Rotated Text to Bottom‑Right of Last PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to use Aspose.Pdf's TextFragment to place rotated text at the bottom‑right corne... |
| [add-rotated-text-watermark-to-pdf-pages](./add-rotated-text-watermark-to-pdf-pages.cs) | Add Rotated Text Watermark to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to use Aspose.Pdf's TextBuilder and TextFragment to place a rotated "CONFIDENTIAL" wate... |
| [add-semi-transparent-text-to-pdf](./add-semi-transparent-text-to-pdf.cs) | Add Semi‑Transparent Text to a PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to set text opacity by configuring the TextState's foreground color with an alph... |
| [add-semi-transparent-text-watermark__v2](./add-semi-transparent-text-watermark__v2.cs) | Add Semi-Transparent Text Watermark to PDF | `Document`, `TextStamp`, `TextState` | Demonstrates how to overlay a semi‑transparent text watermark on every page of a PDF using Aspose... |
| [add-styled-html-fragment-to-pdf-page](./add-styled-html-fragment-to-pdf-page.cs) | Add Styled HTML Fragment to PDF | `Document`, `Page`, `HtmlFragment` | Shows how to load an existing PDF, create an HtmlFragment with CSS styling, optionally set a Text... |
| [add-table-to-pdf-footnote](./add-table-to-pdf-footnote.cs) | Add a Table to a Footnote Using Tagged PDF Content | `Document`, `ITaggedContent`, `CreateDivElement` | Demonstrates how to use Aspose.Pdf's TaggedContent API to create a footnote-like DivElement, add ... |
| [add-text-and-encrypt-pdf](./add-text-and-encrypt-pdf.cs) | Add Text and Encrypt PDF with Password | `Document`, `Page`, `TextFragment` | The example loads a PDF, adds a "Confidential" text fragment to the first page, then encrypts the... |
| [add-text-annotation-to-pdf-page](./add-text-annotation-to-pdf-page.cs) | Create a Text Annotation in a PDF with Aspose.Pdf | `Document`, `Page`, `Rectangle` | The example loads a PDF, creates a TextAnnotation with title, contents and icon, adds it to the f... |
| [add-text-to-pdf-from-memory-stream](./add-text-to-pdf-from-memory-stream.cs) | Add Text to PDF Using Memory Streams | `Document`, `Save`, `Page` | Demonstrates loading a PDF from a MemoryStream, inserting a text fragment at a specific position,... |
| [add-text-to-pdf-page-at-specific-coordinates](./add-text-to-pdf-page-at-specific-coordinates.cs) | Add Text to PDF Page at Specific Coordinates | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, create a TextFragment, set its X/Y position, add it to the first page, a... |
| [add-underlined-text-to-pdf](./add-underlined-text-to-pdf.cs) | Add Underlined Text to PDF | `Document`, `Page`, `TextFragment` | Loads an existing PDF, creates a TextFragment, enables underline via TextState, positions the fra... |
| [adjust-word-spacing-in-pdf](./adjust-word-spacing-in-pdf.cs) | Adjust Word Spacing When Adding Text to PDF | `Document`, `TextFragment`, `FindFont` | Shows how to configure the TextState.WordSpacing property of a TextFragment before placing it on ... |
| [append-disclaimer-to-first-pdf-page](./append-disclaimer-to-first-pdf-page.cs) | Append Disclaimer Text to First PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, create a TextFragment containing a disclaimer, and add i... |
| [append-multiline-textfragment-line-breaks](./append-multiline-textfragment-line-breaks.cs) | Append Multi-line TextFragment and Retrieve Line Break Infor... | `Document`, `Page`, `TextFragment` | Demonstrates adding a multi-line TextFragment to a PDF page with Aspose.Pdf and inspecting its Te... |
| [apply-background-color-to-text](./apply-background-color-to-text.cs) | Apply Background Color to Text in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to set a background color for a text fragment by configuring the TextState.Backg... |
| [apply-opentype-font-to-textfragment](./apply-opentype-font-to-textfragment.cs) | Apply Custom OTF Font to TextFragment | `FontRepository`, `SimpleFontSubstitution`, `Document` | Shows how to load an OpenType (OTF) font, register it with Aspose.Pdf, and apply it to a TextFrag... |
| [batch-replace-keywords-in-pdf](./batch-replace-keywords-in-pdf.cs) | Batch Replace Text in PDF Using Aspose.Pdf and JSON Config | `Document`, `Page`, `TextFragmentAbsorber` | Demonstrates how to read a JSON mapping of old and new strings and replace all occurrences of eac... |
| [batch-replace-multiple-strings-in-pdf](./batch-replace-multiple-strings-in-pdf.cs) | Batch Replace Multiple Strings in PDFs | `Document`, `TextFragmentAbsorber`, `TextSearchOptions` | Shows how to iterate through PDF files in a folder and replace several specified strings with new... |
| [center-text-horizontally-in-pdf](./center-text-horizontally-in-pdf.cs) | Center Text Horizontally in a PDF | `Document`, `Page`, `TextFragment` | Shows how to load an existing PDF, create a TextFragment, set its HorizontalAlignment to Center, ... |
| [convert-modified-pdf-to-byte-array](./convert-modified-pdf-to-byte-array.cs) | Save Modified PDF to Byte Array | `Document`, `Page`, `Rectangle` | Loads a PDF, adds a text annotation to the first page, and saves the modified document into a byt... |
| [create-pdf-launch-action-open-external-file](./create-pdf-launch-action-open-external-file.cs) | Add Clickable Text to Open an External File | `Document`, `Page`, `TextFragment` | Demonstrates how to place a text fragment in a PDF and attach a LaunchAction via a LinkAnnotation... |
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
Updated: 2026-11-19 | Run: `20261119_000736_591f96`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf;` (62/62 files) ← category-specific
- `using Aspose.Pdf.Text;` (54/62 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (7/62 files)
- `using Aspose.Pdf.Facades;` (4/62 files)
- `using Aspose.Pdf.Drawing;` (1/62 files)
- `using System;` (62/62 files)
- `using System.IO;` (60/62 files)
- `using System.Collections.Generic;` (4/62 files)
- `using System.Linq;` (2/62 files)
- `using System.Text.RegularExpressions;` (1/62 files)

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

| File | Key APIs | Description |
|------|----------|-------------|
| [Adjust-the-font-size-of-text-within-a-PDF-document-while-pre...](./Adjust-the-font-size-of-text-within-a-PDF-document-while-preserving-the-original-layout-and-formatti.cs) | `TextFragmentAbsorber`, `PdfContentEditor` | Adjust the font size of text within a PDF document while preserving the origi... |
| [Apply-a-new-font-style-size-and-color-to-a-specific-text-fra...](./Apply-a-new-font-style-size-and-color-to-a-specific-text-fragment-within-a-PDF-document.cs) | `TextFragmentAbsorber` | Apply a new font style size and color to a specific text fragment within a PD... |
| [Apply-advanced-text-formatting-to-content-within-a-PDF-docum...](./Apply-advanced-text-formatting-to-content-within-a-PDF-document-while-maintaining-PDF-file-integrity.cs) | `TextFragmentAbsorber` | Apply advanced text formatting to content within a PDF document while maintai... |
| [Apply-underline-strikeout-and-other-text-effects-to-text-wit...](./Apply-underline-strikeout-and-other-text-effects-to-text-within-a-PDF-document-programmatically.cs) | `TextFragmentAbsorber` | Apply underline strikeout and other text effects to text within a PDF documen... |
| [Apply-visual-text-effects-to-PDF-documents-programmatically-...](./Apply-visual-text-effects-to-PDF-documents-programmatically-preserving-layout-and-rendering-fidelit.cs) | `TextFragmentAbsorber` | Apply visual text effects to PDF documents programmatically preserving layout... |
| [Attach-a-tooltip-to-a-specific-text-fragment-within-a-PDF-do...](./Attach-a-tooltip-to-a-specific-text-fragment-within-a-PDF-document-displaying-custom-hover-text.cs) | `TextFragmentAbsorber`, `TextAnnotation` | Attach a tooltip to a specific text fragment within a PDF document displaying... |
| [Attach-descriptive-tooltips-to-specific-text-elements-within...](./Attach-descriptive-tooltips-to-specific-text-elements-within-a-PDF-document-to-provide-contextual-in.cs) | `TextFragmentAbsorber`, `TextAnnotation` | Attach descriptive tooltips to specific text elements within a PDF document t... |
| [Calculate-appropriate-line-break-positions-for-a-text-string...](./Calculate-appropriate-line-break-positions-for-a-text-string-according-to-the-PDF-page-width-constra.cs) | `TextBuilder`, `TextFragmentAbsorber` | Calculate appropriate line break positions for a text string according to the... |
| [Calculate-line-breaks-by-comparing-element-width-against-the...](./Calculate-line-breaks-by-comparing-element-width-against-the-page-s-available-width-when-generating.cs) | `Rectangle`, `TextBuilder` | Calculate line breaks by comparing element width against the page s available... |
| [Configure-the-TextSearchOptions-for-PDF-processing-when-requ...](./Configure-the-TextSearchOptions-for-PDF-processing-when-required-to-customize-case-sensitivity-whol.cs) | `TextFragmentAbsorber` | Configure the TextSearchOptions for PDF processing when required to customize... |
| [Configure-the-position-of-a-floating-annotation-box-within-a...](./Configure-the-position-of-a-floating-annotation-box-within-a-PDF-document-using-precise-coordinate-s.cs) |  | Configure the position of a floating annotation box within a PDF document usi... |
| [Configure-tooltip-attributes-for-PDF-elements-to-display-cus...](./Configure-tooltip-attributes-for-PDF-elements-to-display-custom-hover-text-during-document-interacti.cs) | `TextAnnotation` | Configure tooltip attributes for PDF elements to display custom hover text du... |
| [Create-a-PDF-tooltip-annotation-that-displays-custom-text-wh...](./Create-a-PDF-tooltip-annotation-that-displays-custom-text-when-hovering-over-specified-document-elem.cs) | `TextAnnotation` | Create a PDF tooltip annotation that displays custom text when hovering over ... |
| [Create-a-new-PDF-document-and-insert-text-using-specified-fo...](./Create-a-new-PDF-document-and-insert-text-using-specified-font-size-and-color-settings.cs) | `TextFragment`, `TextBuilder` | Create a new PDF document and insert text using specified font size and color... |
| [Create-a-new-PDF-file-and-insert-specified-text-content-whil...](./Create-a-new-PDF-file-and-insert-specified-text-content-while-maintaining-PDF-compliance.cs) | `TextFragment`, `TextBuilder` | Create a new PDF file and insert specified text content while maintaining PDF... |
| [Determine-the-rendered-width-of-a-specified-text-string-with...](./Determine-the-rendered-width-of-a-specified-text-string-within-a-PDF-document-using-appropriate-meas.cs) |  | Determine the rendered width of a specified text string within a PDF document... |
| [Enable-the-PDF-document-to-accept-an-absorber-element-ensuri...](./Enable-the-PDF-document-to-accept-an-absorber-element-ensuring-proper-integration-during-processing.cs) | `TextAbsorber` | Enable the PDF document to accept an absorber element ensuring proper integra... |
| [Export-the-current-document-to-a-PDF-file-preserving-its-lay...](./Export-the-current-document-to-a-PDF-file-preserving-its-layout-fonts-and-formatting.cs) |  | Export the current document to a PDF file preserving its layout fonts and for... |
| [Extract-textual-content-from-a-PDF-document-using-the-TextAb...](./Extract-textual-content-from-a-PDF-document-using-the-TextAbsorber-utility-while-maintaining-layout.cs) | `TextAbsorber` | Extract textual content from a PDF document using the TextAbsorber utility wh... |
| [Identify-line-break-positions-within-a-PDF-document-for-accu...](./Identify-line-break-positions-within-a-PDF-document-for-accurate-text-extraction-and-processing.cs) | `TextFragmentAbsorber` | Identify line break positions within a PDF document for accurate text extract... |
| [Insert-a-FloatingBox-object-into-a-PDF-page-s-Paragraphs-col...](./Insert-a-FloatingBox-object-into-a-PDF-page-s-Paragraphs-collection-programmatically-using-the-API.cs) | `BorderInfo`, `TextFragment` | Insert a FloatingBox object into a PDF page s Paragraphs collection programma... |
| [Insert-a-TextFragment-object-into-the-target-page-s-Paragrap...](./Insert-a-TextFragment-object-into-the-target-page-s-Paragraphs-collection-when-generating-a-PDF-docu.cs) | `TextFragment` | Insert a TextFragment object into the target page s Paragraphs collection whe... |
| [Insert-a-TextFragment-object-onto-a-specific-page-within-a-P...](./Insert-a-TextFragment-object-onto-a-specific-page-within-a-PDF-document-using-the-API.cs) | `TextFragment`, `TextBuilder` | Insert a TextFragment object onto a specific page within a PDF document using... |
| [Insert-a-floating-annotation-at-designated-coordinates-on-a-...](./Insert-a-floating-annotation-at-designated-coordinates-on-a-PDF-page-and-populate-it-with-text-or-im.cs) | `FreeTextAnnotation`, `StampAnnotation` | Insert a floating annotation at designated coordinates on a PDF page and popu... |
| [Insert-colored-text-into-a-PDF-document-specifying-RGB-value...](./Insert-colored-text-into-a-PDF-document-specifying-RGB-values-and-preserving-existing-layout.cs) | `TextFragment`, `TextBuilder` | Insert colored text into a PDF document specifying RGB values and preserving ... |
| [Insert-content-into-a-floating-box-within-a-PDF-file-while-m...](./Insert-content-into-a-floating-box-within-a-PDF-file-while-maintaining-layout-integrity.cs) | `BorderInfo`, `TextFragment` | Insert content into a floating box within a PDF file while maintaining layout... |
| [Insert-custom-text-into-a-PDF-at-specified-coordinates-witho...](./Insert-custom-text-into-a-PDF-at-specified-coordinates-without-altering-the-existing-content.cs) | `TextFragment`, `TextBuilder` | Insert custom text into a PDF at specified coordinates without altering the e... |
| [Insert-custom-text-into-a-PDF-document-at-specified-X-Y-coor...](./Insert-custom-text-into-a-PDF-document-at-specified-X-Y-coordinates-while-preserving-existing-conten.cs) | `TextFragment`, `TextBuilder` | Insert custom text into a PDF document at specified X Y coordinates while pre... |
| [Insert-custom-text-into-a-PDF-document-programmatically-ensu...](./Insert-custom-text-into-a-PDF-document-programmatically-ensuring-correct-encoding-and-precise-place.cs) | `TextFragment`, `TextBuilder` | Insert custom text into a PDF document programmatically ensuring correct enco... |
| [Insert-custom-text-into-a-PDF-document-using-a-specified-fon...](./Insert-custom-text-into-a-PDF-document-using-a-specified-font-family-and-point-size.cs) | `TextFragment`, `TextBuilder` | Insert custom text into a PDF document using a specified font family and poin... |
| ... | | *and 32 more files* |

## Category Statistics
- Total examples: 62

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

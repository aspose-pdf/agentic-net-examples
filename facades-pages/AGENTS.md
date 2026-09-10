---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **185** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (115/185 files) ← category-specific
- `using Aspose.Pdf;` (78/185 files)
- `using Aspose.Pdf.Text;` (6/185 files)
- `using Aspose.Pdf.Annotations;` (1/185 files)
- `using Aspose.Pdf.Forms;` (1/185 files)
- `using System;` (117/185 files)
- `using System.IO;` (108/185 files)
- `using System.Collections.Generic;` (10/185 files)
- `using System.Linq;` (1/185 files)
- `using System.Reflection;` (1/185 files)

## Common Code Pattern

Most files in this category use `PdfPageEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfPageEditor tool = new PdfPageEditor();
tool.BindPdf("input.pdf");
// ... PdfPageEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to add a uniform 10 % margin on all side... |
| [add-15-percent-margins-to-pdf-pages](./add-15-percent-margins-to-pdf-pages.cs) | Add 15% Margins to PDF Pages for Booklet Layout | `PdfFileEditor`, `AddMarginsPct` | Shows how to apply a 15 % margin to selected or all pages of a PDF using Aspose.Pdf.Facades, impr... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add uniform 20 % margins around each page of a PDF using Aspose.Pdf.Facades.P... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margins and Print PDF | `PdfFileEditor`, `PdfViewer`, `AddMarginsPct` | Shows how to add a 5 % margin to every page of a PDF using PdfFileEditor and then print the modif... |
| [add-fade-transition-to-pdf-page](./add-fade-transition-to-pdf-page.cs) | Add Fade Transition to PDF Page | `Document`, `PdfPageEditor`, `TransitionType` | Demonstrates how to set a fade page transition with a two‑second duration on the first page of a ... |
| [add-percentage-margins-to-pdf-pages](./add-percentage-margins-to-pdf-pages.cs) | Add percentage margins to pdf pages |  | Add percentage margins to pdf pages |
| [add-transition-to-odd-pdf-pages](./add-transition-to-odd-pdf-pages.cs) | Add transition to odd pdf pages |  | Add transition to odd pdf pages |
| [adjust-page-zoom-based-on-word-count](./adjust-page-zoom-based-on-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `PdfPageEditor`, `TextAbsorber` | Demonstrates how to analyze each PDF page's word count and apply a dynamic zoom factor—higher zoo... |
| [adjust-pdf-page-zoom-by-word-count](./adjust-pdf-page-zoom-by-word-count.cs) | Adjust pdf page zoom by word count |  | Adjust pdf page zoom by word count |
| [align-page-three-vertically-top](./align-page-three-vertically-top.cs) | Align Page Three Vertically to Top Using PdfPageEditor | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to vertically align the content of page 3 of a PDF to the top using Aspose.Pdf's PdfPag... |
| [align-page-two-left](./align-page-two-left.cs) | Align Page Two Left in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to left‑justify the content of the second page of a PDF using Aspose.Pdf's PdfPa... |
| [align-vertical-content-page-three](./align-vertical-content-page-three.cs) | Align vertical content page three |  | Align vertical content page three |
| [apply-cover-transition-to-pdf-page](./apply-cover-transition-to-pdf-page.cs) | Apply cover transition to pdf page |  | Apply cover transition to pdf page |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different transition effects to each PDF page using Aspose.Pdf's PdfPageEdito... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to assign a distinct zoom factor to each page of a PDF using the Aspose.Pdf.Facades.Pdf... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a Specific PDF Page | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to set a three‑second Dissolve page transition on page 5 of a PDF using Aspose.P... |
| [apply-fade-transition-all-pdf-pages](./apply-fade-transition-all-pdf-pages.cs) | Apply fade transition all pdf pages |  | Apply fade transition all pdf pages |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade Transition to All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a uniform Fade (Dissolve) transition and duration for every page in a PDF using ... |
| [apply-horizontal-alignment-to-pdf-pages](./apply-horizontal-alignment-to-pdf-pages.cs) | Apply Horizontal Alignment to PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Shows how to set left‑justified horizontal alignment for all pages of a PDF using the PdfPageEdit... |
| [apply-page-settings-from-json](./apply-page-settings-from-json.cs) | Apply page settings from json |  | Apply page settings from json |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Different Page Transitions Based on Index | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to assign a different transition effect to each PDF page using Aspose.Pdf.Facade... |
| [apply-page-transitions-per-index](./apply-page-transitions-per-index.cs) | Apply page transitions per index |  | Apply page transitions per index |
| [apply-rotation-size-zoom-to-pdf-pages](./apply-rotation-size-zoom-to-pdf-pages.cs) | Apply Rotation, Size, and Zoom to PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates using Aspose.Pdf's PdfPageEditor facade to rotate pages, change the page size, apply... |
| [apply-rotation-zoom-transition-to-pdf-pages](./apply-rotation-zoom-transition-to-pdf-pages.cs) | Apply rotation zoom transition to pdf pages |  | Apply rotation zoom transition to pdf pages |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply sequential page transitions |  | Apply sequential page transitions |
| [apply-split-transition-to-pdf-page](./apply-split-transition-to-pdf-page.cs) | Apply split transition to pdf page |  | Apply split transition to pdf page |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose.Pdf's ... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to vertically align the content of specific pages in a PDF to the top using Aspo... |
| [apply-zoom-to-even-pdf-pages](./apply-zoom-to-even-pdf-pages.cs) | Apply zoom to even pdf pages |  | Apply zoom to even pdf pages |
| [apply-zoom-to-even-pdf-pages__v2](./apply-zoom-to-even-pdf-pages__v2.cs) | Apply zoom to even pdf pages__v2 |  | Apply zoom to even pdf pages__v2 |
| ... | | | *and 155 more files* |

## Category Statistics
- Total examples: 185

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.PdfFileEditor`
- `Aspose.Pdf.Facades.PdfFileEditor.Delete`
- `Aspose.Pdf.Facades.PdfFileEditor.Extract`
- `Aspose.Pdf.Facades.PdfFileEditor.SplitToEnd`

### Rules
- Instantiate Aspose.Pdf.Facades.PdfFileEditor and call Delete({input_pdf}, {int[]} pagesToDelete, {output_pdf}) to remove the specified pages.
- The page numbers in the array are 1‑based indices representing the pages to be removed.
- Use PdfFileEditor.Delete({input_pdf_stream}, {int[] pagesToDelete}, {output_pdf_stream}) to remove the specified pages (1‑based indices) from a PDF without loading it into a Document object.
- When working with streams, open the source PDF with FileMode.Open and create the destination PDF with FileMode.Create, then pass the streams to PdfFileEditor.Delete.
- Use PdfFileEditor.Extract({input_pdf}, new int[] {{int}, {int}, ...}, {output_pdf}) to create a new PDF containing only the listed pages.

### Warnings
- The example does not explicitly dispose the FileStream objects; callers should ensure streams are closed or wrapped in using statements.
- The output file will be created or overwritten; ensure the path is correct.
- The example assumes the input PDF exists at the specified location.
- Page numbers must be within the bounds of the source document; otherwise an exception will be thrown.
- Insert overwrites the output file if it already exists.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

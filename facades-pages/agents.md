---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (117/117 files) ← category-specific
- `using Aspose.Pdf;` (79/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/117 files)
- `using Aspose.Pdf.Annotations;` (1/117 files)
- `using System;` (116/117 files)
- `using System.IO;` (109/117 files)
- `using System.Collections.Generic;` (10/117 files)
- `using System.Drawing;` (1/117 files)
- `using System.Runtime.InteropServices;` (1/117 files)
- `using System.Text.Json;` (1/117 files)

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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to add a 10% margin on every side of each pag... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add a 20% whitespace margin around each page of a PDF for printing using Aspo... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margins and Print PDF with Auto‑Resize | `PdfFileEditor`, `AddMarginsPct`, `PdfViewer` | The example adds a 5 % margin to all pages of a PDF using PdfFileEditor, then prints the modified... |
| [adjust-page-zoom-based-on-word-count](./adjust-page-zoom-based-on-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `PdfPageEditor`, `TextAbsorber` | Shows how to extract text from each PDF page, count its words, and apply a higher zoom factor to ... |
| [adjust-pdf-page-properties-from-json](./adjust-pdf-page-properties-from-json.cs) | Adjust PDF Page Properties from JSON Configuration | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates reading a JSON file and applying rotation, zoom, alignment, size, and transition set... |
| [apply-15-percent-margins-booklet](./apply-15-percent-margins-booklet.cs) | Apply 15% Margins for Booklet Layout | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add 15 % margins to selected or all pages of a PDF using Aspose.Pdf.Facades, ... |
| [apply-boxout-transition-to-pdf-page](./apply-boxout-transition-to-pdf-page.cs) | Apply BoxOut Transition to PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set a BoxOut page transition with a three‑second duration on a specific page ... |
| [apply-conditional-page-transitions](./apply-conditional-page-transitions.cs) | Apply Conditional Page Transitions in PDF | `Document`, `PdfPageEditor`, `Page` | Demonstrates how to assign different page transition effects to each PDF page based on simple con... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to assign different transition effects to each PDF page by iterating through pages with... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to iterate through each page of a PDF and set a specific zoom factor using the PdfPageE... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a Specific PDF Page | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates how to set a Dissolve page transition with a three‑second duration on page 5 of a PD... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade (Dissolve) Transition to All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a dissolve (fade) page transition for every page in a PDF using the PdfPageEdito... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Page Transitions Based on Page Index | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to set different PDF page transition effects using PdfPageEditor, selecting the ... |
| [apply-rotation-size-zoom-to-pdf-pages](./apply-rotation-size-zoom-to-pdf-pages.cs) | Apply Rotation, Size, and Zoom to PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates how to use Aspose.Pdf's PdfPageEditor facade to rotate pages, set custom page dimens... |
| [apply-rotation-zoom-transition-to-pdf-pages](./apply-rotation-zoom-transition-to-pdf-pages.cs) | Apply Rotation, Zoom, and Transition Effects to PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to rotate, zoom and add slide transition effects to all pages of a PDF using Asp... |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply Sequential Page Transitions to PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different transition effects to each page of a PDF by using Aspose.Pdf's PdfP... |
| [apply-split-transition-to-pdf-page](./apply-split-transition-to-pdf-page.cs) | Apply Split Transition to PDF Page | `Document`, `PdfPageEditor`, `SPLITHIN` | Shows how to set a split page transition lasting two seconds on page three of a PDF using Aspose.... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose.Pdf's ... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to set top vertical alignment on specific pages... |
| [apply-zoom-to-odd-pdf-pages](./apply-zoom-to-odd-pdf-pages.cs) | Apply Zoom to Odd Pages in a PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a 1.2 zoom factor on all odd‑numbered pages of a PDF using Aspose.Pdf.Facades.Pd... |
| [apply-zoom-to-selected-pdf-pages](./apply-zoom-to-selected-pdf-pages.cs) | Apply Zoom to Selected PDF Pages | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to select non‑consecutive pages using PdfPageEditor.ProcessPages and apply a common zoo... |
| [batch-change-pdf-page-size](./batch-change-pdf-page-size.cs) | Batch Change PDF Page Size Based on File Name | `PdfPageEditor`, `BindPdf`, `PageSize` | Demonstrates how to process a folder of PDFs, determine a target page size from each file name, a... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfFileEditor`, `ResizeContents`, `PageSize` | Demonstrates how to resize multiple PDF documents to A4 dimensions using Aspose.Pdf's PdfFileEditor. |
| [batch-set-fade-transition-pdf](./batch-set-fade-transition-pdf.cs) | Batch Set Fade Transition for PDF Pages | `Document`, `PdfPageEditor`, `TransitionType` | Demonstrates how to apply a fade (dissolve) transition with a two‑second duration to all pages of... |
| [center-content-horizontally-page2](./center-content-horizontally-page2.cs) | Center Content Horizontally on Specific PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to use Aspose.Pdf's PdfPageEditor to horizontally center the existing content on page 2... |
| [center-page-content-set-display-duration](./center-page-content-set-display-duration.cs) | Center Page Content and Set Display Duration on Specific PDF... | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to center the content of page 5 horizontally and set its display duration to 4 s... |
| [chain-page-rotation-size-zoom](./chain-page-rotation-size-zoom.cs) | Chain Page Rotation, Size, and Zoom Modifications | `Document`, `PdfPageEditor`, `Rotation` | Shows how to apply multiple page property changes—rotation, page size, and zoom—sequentially usin... |
| [change-pdf-page-size-and-undo](./change-pdf-page-size-and-undo.cs) | Change PDF Page Size and Undo to Original | `PdfPageEditor`, `BindPdf`, `GetPageSize` | Shows how to set a custom page size for a PDF page, save the modified file, then revert to the or... |
| [convert-pdf-page-orientation-to-landscape](./convert-pdf-page-orientation-to-landscape.cs) | Convert PDF Page Orientation to Landscape (A4) | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to change a PDF's page orientation from portrait to landscape by setting the PageSize t... |
| [convert-pdf-portrait-to-landscape](./convert-pdf-portrait-to-landscape.cs) | Convert PDF Page from Portrait to Landscape | `PdfPageEditor`, `BindPdf`, `GetPageSize` | Shows how to change a PDF page's orientation from portrait to landscape by swapping the width and... |
| ... | | | *and 87 more files* |

## Category Statistics
- Total examples: 117

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_143841_4ab3c2`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf.Facades;` (114/117 files) ← category-specific
- `using Aspose.Pdf;` (79/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (9/117 files)
- `using Aspose.Pdf.Devices;` (1/117 files)
- `using Aspose.Pdf.Drawing;` (1/117 files)
- `using Aspose.Pdf.Printing;` (1/117 files)
- `using System;` (116/117 files)
- `using System.IO;` (104/117 files)
- `using System.Collections.Generic;` (13/117 files)
- `using System.Linq;` (2/117 files)
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
| [add-10-percent-margins-to-pdf](./add-10-percent-margins-to-pdf.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to add uniform 10% margins to every page of a... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add 20 % white margins around all pages of a PDF using the PdfFileEditor facade. |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margins and Print PDF with Auto‑Scaling | `PdfFileEditor`, `AddMarginsPct`, `PdfViewer` | Demonstrates how to add a 5 % margin to every page of a PDF using PdfFileEditor and then print th... |
| [add-fade-transition-to-first-pdf-page](./add-fade-transition-to-first-pdf-page.cs) | Add Fade Transition to First PDF Page | `Document`, `Save`, `PdfPageEditor` | Demonstrates how to set a fade transition of two seconds on the first page of a PDF using Aspose.... |
| [adjust-pdf-page-properties-from-json](./adjust-pdf-page-properties-from-json.cs) | Adjust PDF Page Properties from JSON Configuration | `Document`, `PdfPageEditor`, `PageSize` | Demonstrates loading a JSON file to configure rotation, zoom, size, and transition settings for s... |
| [adjust-pdf-page-zoom-based-on-word-count](./adjust-pdf-page-zoom-based-on-word-count.cs) | Adjust PDF Page Zoom Based on Word Count | `Document`, `TextAbsorber`, `PdfPageEditor` | The example counts words on each PDF page and applies a higher zoom factor to pages with fewer wo... |
| [apply-15-percent-margins-booklet-pdf](./apply-15-percent-margins-booklet-pdf.cs) | Apply 15% Margins for Booklet Layout to PDF | `PdfFileEditor`, `AddMarginsPct` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor.AddMarginsPct to add uniform 15% margins to a... |
| [apply-boxout-transition-to-pdf-page](./apply-boxout-transition-to-pdf-page.cs) | Apply BoxOut Transition to PDF Page | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates how to set a BoxOut page transition with a three‑second duration on a specific page ... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `ApplyChanges` | Shows how to set different transition effects for each PDF page using PdfPageEditor, selecting th... |
| [apply-cyclic-page-transitions](./apply-cyclic-page-transitions.cs) | Apply Cyclic Page Transitions Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `GetPages` | Demonstrates how to use Aspose.Pdf.Facades.PdfPageEditor to assign different transition effects t... |
| [apply-different-page-transitions](./apply-different-page-transitions.cs) | Apply Different Page Transitions Sequentially in a PDF | `Document`, `PdfPageEditor` | Shows how to assign distinct transition effects to individual pages of a PDF using Aspose.Pdf's P... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to assign individual zoom factors to each page of a PDF using Aspose.Pdf's PdfPageEditor. |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a PDF Page | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to add a three‑second dissolve transition to pa... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade Transition to All PDF Pages | `Document`, `PdfPageEditor` | Shows how to set a dissolve (fade) transition on every page of a PDF document using Aspose.Pdf's ... |
| [apply-left-horizontal-alignment-to-pdf-pages](./apply-left-horizontal-alignment-to-pdf-pages.cs) | Apply Left Horizontal Alignment to All PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to use PdfPageEditor to set the horizontal alignment of every page in a PDF to left and... |
| [apply-page-transitions-fade-boxout-cover](./apply-page-transitions-fade-boxout-cover.cs) | Apply Page Transition Effects (Fade, BoxOut, Cover) to PDF | `Document`, `PdfPageEditor`, `Save` | Demonstrates how to set different page transition effects (Fade, BoxOut, Cover) on individual PDF... |
| [apply-rotation-zoom-transition-to-pdf-slides](./apply-rotation-zoom-transition-to-pdf-slides.cs) | Apply Rotation, Zoom and Transition to PDF Slides | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates using PdfPageEditor to rotate, zoom and add slide transition effects to all pages of... |
| [apply-split-transition-to-pdf-page](./apply-split-transition-to-pdf-page.cs) | Apply Split Transition to PDF Page | `Document`, `PdfPageEditor` | Demonstrates how to set a split page transition with a 2‑second duration on page three of a PDF u... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to use PdfPageEditor to vertically align the content of specific pages to the top of th... |
| [assign-page-transitions-based-on-content](./assign-page-transitions-based-on-content.cs) | Assign Page Transitions Based on Content Type | `Document`, `PdfPageEditor`, `Page` | Demonstrates how to iterate through PDF pages and apply different transition effects depending on... |
| [batch-change-pdf-page-size](./batch-change-pdf-page-size.cs) | Batch Change PDF Page Size Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates how to process multiple PDF files in a folder and set a specific page size for each ... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to use Aspose.Pdf's PdfPageEditor facade to resize multiple PDF files to A4 dimensions ... |
| [batch-rotate-first-page-pdfs](./batch-rotate-first-page-pdfs.cs) | Batch Rotate First Page of PDFs | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to loop through a directory of PDF files and rotate the first page of each document by ... |
| [center-align-zoom-page-4](./center-align-zoom-page-4.cs) | Center Align and Zoom Page 4 of PDF | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to center‑align the content and apply a 150% zo... |
| [center-content-horizontally-on-second-page](./center-content-horizontally-on-second-page.cs) | Center Content Horizontally on Second PDF Page | `Document`, `TextFragment`, `Position` | Shows how to horizontally center a text fragment on the second page of a PDF using Aspose.Pdf's H... |
| [center-page-content-set-display-duration](./center-page-content-set-display-duration.cs) | Center Page Content and Set Display Duration on PDF Page | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to center the content of a specific PDF page and set its display duration to fou... |
| [chain-page-rotation-size-zoom](./chain-page-rotation-size-zoom.cs) | Chain Page Rotation, Size, and Zoom Modifications | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to use PdfPageEditor to rotate pages, set a new page size, and apply a zoom factor in a... |
| [change-pdf-page-orientation-to-landscape](./change-pdf-page-orientation-to-landscape.cs) | Change PDF Page Orientation to Landscape | `Document`, `Save`, `Page` | Loads a PDF, sets each page's dimensions to A4 landscape size, and saves the document, converting... |
| [check-page-rotation-after-resizing](./check-page-rotation-after-resizing.cs) | Check Page Rotation Before and After Resizing a PDF Page | `PdfPageEditor`, `BindPdf`, `GetPageRotation` | Demonstrates how to retrieve a PDF page's rotation, change the page size using PdfPageEditor, and... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

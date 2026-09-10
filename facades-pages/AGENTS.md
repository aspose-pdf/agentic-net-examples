---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (115/117 files) ← category-specific
- `using Aspose.Pdf;` (78/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (6/117 files)
- `using Aspose.Pdf.Annotations;` (1/117 files)
- `using Aspose.Pdf.Forms;` (1/117 files)
- `using System;` (117/117 files)
- `using System.IO;` (108/117 files)
- `using System.Collections.Generic;` (10/117 files)
- `using System.Linq;` (1/117 files)
- `using System.Reflection;` (1/117 files)

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
| [adjust-page-zoom-based-on-word-count](./adjust-page-zoom-based-on-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `PdfPageEditor`, `TextAbsorber` | Demonstrates how to analyze each PDF page's word count and apply a dynamic zoom factor—higher zoo... |
| [align-page-three-vertically-top](./align-page-three-vertically-top.cs) | Align Page Three Vertically to Top Using PdfPageEditor | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to vertically align the content of page 3 of a PDF to the top using Aspose.Pdf's PdfPag... |
| [align-page-two-left](./align-page-two-left.cs) | Align Page Two Left in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to left‑justify the content of the second page of a PDF using Aspose.Pdf's PdfPa... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different transition effects to each PDF page using Aspose.Pdf's PdfPageEdito... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to assign a distinct zoom factor to each page of a PDF using the Aspose.Pdf.Facades.Pdf... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a Specific PDF Page | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to set a three‑second Dissolve page transition on page 5 of a PDF using Aspose.P... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade Transition to All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a uniform Fade (Dissolve) transition and duration for every page in a PDF using ... |
| [apply-horizontal-alignment-to-pdf-pages](./apply-horizontal-alignment-to-pdf-pages.cs) | Apply Horizontal Alignment to PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Shows how to set left‑justified horizontal alignment for all pages of a PDF using the PdfPageEdit... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Different Page Transitions Based on Index | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to assign a different transition effect to each PDF page using Aspose.Pdf.Facade... |
| [apply-rotation-size-zoom-to-pdf-pages](./apply-rotation-size-zoom-to-pdf-pages.cs) | Apply Rotation, Size, and Zoom to PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates using Aspose.Pdf's PdfPageEditor facade to rotate pages, change the page size, apply... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose.Pdf's ... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to vertically align the content of specific pages in a PDF to the top using Aspo... |
| [apply-zoom-to-non-consecutive-pdf-pages](./apply-zoom-to-non-consecutive-pdf-pages.cs) | Apply Zoom to Non-Consecutive PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to select specific non‑consecutive pages using the ProcessPages array and apply a commo... |
| [assign-page-transitions-by-content](./assign-page-transitions-by-content.cs) | Assign Page Transitions Based on Content Type | `Document`, `Page`, `PdfPageEditor` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to set different transition effects for image a... |
| [audit-pdf-page-dimensions-rotation](./audit-pdf-page-dimensions-rotation.cs) | Audit PDF Page Dimensions and Rotation Before and After Edit... | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to log each PDF page's width, height, and rotation, apply a rotation to a page, and sav... |
| [batch-adjust-pdf-page-size](./batch-adjust-pdf-page-size.cs) | Batch Adjust PDF Page Size to A4 | `Document`, `PdfPageEditor`, `PageSize` | Shows how to process all PDFs in a folder and set each page to a specific size (e.g., A4) using A... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to iterate over PDF files in a directory and resize each document to A4 using Aspose.Pd... |
| [batch-rotate-first-page-pdfs](./batch-rotate-first-page-pdfs.cs) | Batch Rotate First Page of PDFs | `PdfPageEditor`, `BindPdf`, `PageRotations` | Shows how to process all PDF files in a folder and rotate the first page of each document by 90° ... |
| [batch-set-fade-transition-pdf-slideshow](./batch-set-fade-transition-pdf-slideshow.cs) | Batch Set Fade Transition for PDF Slideshow | `Document`, `PdfPageEditor`, `TransitionType` | Demonstrates how to apply a fade (dissolve) transition with a 2‑second duration to all pages of a... |
| [center-page-content-horizontally](./center-page-content-horizontally.cs) | Center Page Content Horizontally Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to center the existing content of a specific pa... |
| [center-page-content-set-display-duration](./center-page-content-set-display-duration.cs) | Center Page Content and Set Display Duration on a Specific P... | `PdfPageEditor`, `Document`, `BindPdf` | Demonstrates how to center the content of a specific PDF page and set its display duration using ... |
| [chain-page-rotation-size-zoom-modifications](./chain-page-rotation-size-zoom-modifications.cs) | Chain Page Rotation, Size, and Zoom Modifications | `Document`, `PdfPageEditor`, `Rotation` | Demonstrates how to use PdfPageEditor to rotate, resize, zoom, and reposition PDF pages in a sing... |
| [change-pdf-page-size-and-undo](./change-pdf-page-size-and-undo.cs) | Change PDF Page Size and Undo to Original | `PdfPageEditor`, `BindPdf`, `GetPageSize` | Shows how to capture a PDF's original page size, apply a custom size to all pages, save the modif... |
| [change-pdf-page-size-to-a3](./change-pdf-page-size-to-a3.cs) | Change PDF Page Size to A3 Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to resize all pages of a PDF to A3 dimensions and optionally increase resolution using ... |
| [combine-page-rotation-zoom-presentation](./combine-page-rotation-zoom-presentation.cs) | Combine Page Rotation, Zoom, and Transitions for PDF Present... | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to rotate, zoom, and apply slide transition effects to every page of a PDF using Aspose... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

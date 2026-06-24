---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (76/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/117 files)
- `using Aspose.Pdf.Annotations;` (1/117 files)
- `using Aspose.Pdf.Devices;` (1/117 files)
- `using System;` (116/117 files)
- `using System.IO;` (108/117 files)
- `using System.Collections.Generic;` (12/117 files)
- `using System.Linq;` (5/117 files)
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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add a 10 % margin on all four sides of every page in a PDF using Aspose.Pdf's... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Shows how to add a 20 % margin on all four sides of every page in a PDF document using Aspose.Pdf... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margins to PDF Pages and Print | `PdfFileEditor`, `AddMarginsPct`, `PdfViewer` | The example demonstrates how to add a uniform 5 % margin to every page of a PDF using PdfFileEdit... |
| [adjust-pdf-zoom-by-word-count](./adjust-pdf-zoom-by-word-count.cs) | Adjust PDF Page Zoom Based on Word Count | `Document`, `TextAbsorber`, `PdfPageEditor` | Demonstrates how to analyze each PDF page's word count and apply a per‑page zoom level, giving hi... |
| [align-page-3-vertically-to-top](./align-page-3-vertically-to-top.cs) | Align Page 3 Vertically to Top | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to align the content of a specific PDF page (pa... |
| [apply-boxout-transition-to-pdf-page](./apply-boxout-transition-to-pdf-page.cs) | Apply BoxOut Transition to PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to set a BoxOut page transition with a 3‑second duration on the second page of a PDF us... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to load a PDF, iterate through its pages, and assign a different transition effe... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to iterate through each page of a PDF and set a distinct zoom factor using the PdfPageE... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a Specific PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set a dissolve page transition with a three‑second duration on page 5 of a PD... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade Transition to All PDF Pages | `Document`, `PdfPageEditor`, `TransitionType` | Shows how to set a dissolve (fade) transition for every page in a PDF document using Aspose.Pdf's... |
| [apply-left-horizontal-alignment-to-pdf-pages](./apply-left-horizontal-alignment-to-pdf-pages.cs) | Apply Left Horizontal Alignment to All PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Demonstrates how to use PdfPageEditor to set a uniform left‑justified horizontal alignment for al... |
| [apply-page-settings-from-json](./apply-page-settings-from-json.cs) | Apply Page Settings from JSON Configuration to PDFs | `Document`, `Page`, `PageInfo` | Demonstrates how to read a JSON file and programmatically adjust rotation, size, and zoom of sele... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Page Transition Effects Based on Page Index | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different transition types to each PDF page using Aspose.Pdf's PdfPageEditor,... |
| [apply-rotation-zoom-transition-to-pdf-pages](./apply-rotation-zoom-transition-to-pdf-pages.cs) | Apply Rotation, Zoom, and Transition to PDF Pages | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Demonstrates using PdfPageEditor to rotate, zoom, and add a transition effect to all pages of a P... |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply Sequential Page Transitions to PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different transition effects to each page of a PDF using Aspose.Pdf.Facades.P... |
| [apply-split-transition-to-pdf-page](./apply-split-transition-to-pdf-page.cs) | Apply Split Transition to PDF Page | `Document`, `PdfPageEditor`, `SPLITHIN` | Demonstrates how to set a split page transition with a two‑second duration on a specific PDF page... |
| [apply-transitions-to-odd-pages](./apply-transitions-to-odd-pages.cs) | Apply Transitions to Odd Pages in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to add a page transition effect (e.g., dissolve) only to odd‑numbered pages of a PDF us... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to align the content of specific PDF pages to the top using Aspose.Pdf's PdfPage... |
| [apply-zoom-to-even-pages](./apply-zoom-to-even-pages.cs) | Apply Zoom to Even Pages in PDF | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to set a 1.2 zoom factor on all even‑numbered pages of a PDF using Aspose.Pdf.Facades.P... |
| [assign-page-transitions-based-on-content-type](./assign-page-transitions-based-on-content-type.cs) | Assign Page Transitions Based on Content Type | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to examine each PDF page for images and apply distinct page transition effects using As... |
| [audit-pdf-page-size-rotation](./audit-pdf-page-size-rotation.cs) | Audit PDF Page Size and Rotation Before and After Editing | `PdfPageEditor`, `BindPdf`, `GetPageSize` | Shows how to log each PDF page's width, height, and rotation, apply a rotation and size change us... |
| [batch-change-pdf-page-size](./batch-change-pdf-page-size.cs) | Batch Change PDF Page Size Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to iterate through PDFs in a folder and assign a specific page size to each document us... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `Save` | Shows how to process all PDF files in a directory and resize each document to A4 dimensions using... |
| [batch-rotate-first-page-pdf](./batch-rotate-first-page-pdf.cs) | Batch Rotate First Page of PDF Documents | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to rotate the first page of each PDF in a folde... |
| [batch-set-fade-transition-pdf-slideshow](./batch-set-fade-transition-pdf-slideshow.cs) | Batch Set Fade Transition for PDF Slideshow | `Document`, `PdfPageEditor`, `TransitionType` | Demonstrates how to apply a 2‑second fade (dissolve) transition to every page of a 200‑page PDF u... |
| [center-content-horizontally-page2](./center-content-horizontally-page2.cs) | Center Content Horizontally on Specific PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to center the existing content horizontally on ... |
| [center-page-alignment-set-display-duration](./center-page-alignment-set-display-duration.cs) | Center Page Content and Set Display Duration | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to center the content of a specific PDF page and set its slide‑show display dura... |
| [change-pdf-page-orientation-to-landscape](./change-pdf-page-orientation-to-landscape.cs) | Change PDF Page Orientation to Landscape (A4) | `PdfPageEditor`, `BindPdf`, `PageSize` | Demonstrates how to use Aspose.Pdf's PdfPageEditor facade to convert a PDF page from portrait to ... |
| [change-pdf-page-size-to-a3](./change-pdf-page-size-to-a3.cs) | Change PDF Page Size to A3 Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `PageSize` | Demonstrates how to resize all pages of an existing PDF to A3 dimensions using the Aspose.Pdf.Fac... |
| [convert-pdf-portrait-to-landscape](./convert-pdf-portrait-to-landscape.cs) | Convert PDF Page from Portrait to Landscape | `PdfPageEditor`, `BindPdf`, `GetPageSize` | Shows how to change a PDF page's orientation from portrait to landscape by swapping the PageSize ... |
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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

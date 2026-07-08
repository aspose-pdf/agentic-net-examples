---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (74/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/117 files)
- `using Aspose.Pdf.Annotations;` (1/117 files)
- `using System;` (117/117 files)
- `using System.IO;` (106/117 files)
- `using System.Collections.Generic;` (7/117 files)
- `using System.Linq;` (2/117 files)
- `using System.Collections;` (1/117 files)
- `using System.Drawing;` (1/117 files)
- `using System.Net.Http;` (1/117 files)
- `using System.Text.Json;` (1/117 files)
- `using System.Text.RegularExpressions;` (1/117 files)
- `using System.Threading.Tasks;` (1/117 files)

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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to add uniform 10% margins on every side of e... |
| [add-15-percent-margins-booklet](./add-15-percent-margins-booklet.cs) | Add 15% Margins for Booklet Layout | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to apply a 15 % margin to selected or all pages of a PDF using Aspose.Pdf.Facade... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Shows how to add a 20% whitespace margin around each page of a PDF using Aspose.Pdf.Facades.PdfFi... |
| [add-5-percent-margins-to-pdf-pages](./add-5-percent-margins-to-pdf-pages.cs) | Add 5% Margins to PDF Pages and Print | `PdfFileEditor`, `AddMarginsPct`, `PdfViewer` | Demonstrates how to add a uniform 5% margin to every page of a PDF using PdfFileEditor and then p... |
| [adjust-page-zoom-by-word-count](./adjust-page-zoom-by-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `PdfPageEditor`, `TextAbsorber` | Demonstrates how to extract text from each PDF page, count its words, and set a per‑page zoom lev... |
| [align-page-3-vertically-top](./align-page-3-vertically-top.cs) | Align Page 3 Vertically to Top Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to vertically align the content of the third page of a PDF to the top using the ... |
| [align-page-two-left](./align-page-two-left.cs) | Align Page Two Left Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to left‑justify the content of the second page of a PDF document using Aspose.Pdf.Facad... |
| [apply-boxout-transition-to-pdf-page](./apply-boxout-transition-to-pdf-page.cs) | Apply BoxOut Transition to a PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set a BoxOut page transition with a three‑second duration on a specific page ... |
| [apply-custom-page-transitions-to-pdf](./apply-custom-page-transitions-to-pdf.cs) | Apply Custom Page Transitions to PDF Pages | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to assign different transition effects to each page of a PDF using Aspose.Pdf's PdfPage... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to iterate through each page of a PDF and set a distinct zoom factor using PdfPageEdito... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to set a 3‑second dissolve page transition on page 5 of a PDF using Aspose.Pdf.Facades.... |
| [apply-fade-transition-to-first-pdf-page](./apply-fade-transition-to-first-pdf-page.cs) | Apply Fade Transition to First PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to set a Fade page transition with a 2‑second duration on the first page of a PDF using... |
| [apply-left-horizontal-alignment-to-pdf-pages](./apply-left-horizontal-alignment-to-pdf-pages.cs) | Apply Left Horizontal Alignment to All PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Demonstrates how to use PdfPageEditor to set a uniform left horizontal alignment for every page i... |
| [apply-page-settings-from-json](./apply-page-settings-from-json.cs) | Apply Page Settings from JSON to PDF | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to read a JSON configuration and use Aspose.Pdf's PdfPageEditor to rotate, zoom,... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Page Transition Effects Based on Page Index | `Document`, `PdfPageEditor`, `ApplyChanges` | Demonstrates how to loop through a PDF document and assign different transition types to each pag... |
| [apply-page-transitions-pdf](./apply-page-transitions-pdf.cs) | Apply Page Transition Effects with PdfPageEditor | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set different page transition effects (Fade, BoxOut, Cover) on individual PDF... |
| [apply-rotation-page-size-zoom-to-pdf](./apply-rotation-page-size-zoom-to-pdf.cs) | Apply Rotation, Page Size, and Zoom to PDF | `PdfPageEditor`, `BindPdf`, `Rotation` | Demonstrates using Aspose.Pdf.Facades.PdfPageEditor to rotate pages, change the page size, adjust... |
| [apply-rotation-zoom-transition-to-pdf-pages](./apply-rotation-zoom-transition-to-pdf-pages.cs) | Apply Rotation, Zoom, and Transition to PDF Pages | `Document`, `PdfPageEditor`, `Rotation` | Demonstrates using Aspose.Pdf's PdfPageEditor to rotate, zoom, and add a transition effect to all... |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply Sequential Page Transitions to PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to load a PDF, iterate through its pages, and assign different page transition effects ... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose.Pdf.Fa... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to set the vertical alignment of specific pages in a PDF to the top using PdfPag... |
| [assign-page-transitions-by-content](./assign-page-transitions-by-content.cs) | Assign Page Transitions Based on Content Type | `Document`, `PdfPageEditor`, `Page` | Shows how to loop through PDF pages, detect image‑heavy versus text‑only pages, and apply differe... |
| [backup-original-pdf-and-save-edited-copy](./backup-original-pdf-and-save-edited-copy.cs) | Backup Original PDF and Save Edited Copy | `PdfContentEditor`, `BindPdf`, `Save` | The example creates a backup of an existing PDF, then uses Aspose.Pdf.Facades.PdfContentEditor to... |
| [batch-change-pdf-page-size](./batch-change-pdf-page-size.cs) | Batch Change PDF Page Size Based on File Name | `Document`, `Save`, `Page` | Processes all PDFs in a folder, applying a specific page size to each document based on filename ... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to iterate over multiple PDF files and use Aspose.Pdf.Facades.PdfPageEditor to resize e... |
| [batch-rotate-first-page-pdfs](./batch-rotate-first-page-pdfs.cs) | Batch Rotate First Page of PDFs | `Document`, `Page`, `Rotation` | Demonstrates iterating over a folder of PDF files, loading each with Aspose.Pdf, rotating the fir... |
| [center-align-page-set-display-duration](./center-align-page-set-display-duration.cs) | Center Align Page Content and Set Display Duration | `Document`, `PdfPageEditor`, `HorizontalAlignment` | Loads a PDF, uses PdfPageEditor to center the content of page 5 and set its display duration to 4... |
| [center-page-content-horizontally](./center-page-content-horizontally.cs) | Center Page Content Horizontally Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to center the existing content of a specific PDF page horizontally by using the PdfPage... |
| [change-pdf-page-orientation-to-landscape](./change-pdf-page-orientation-to-landscape.cs) | Change PDF Page Orientation to Landscape (A4) | `PdfPageEditor`, `BindPdf`, `PageSize` | Demonstrates how to use Aspose.Pdf.Facades to convert a PDF page from portrait to landscape by se... |
| [change-pdf-page-size-and-revert](./change-pdf-page-size-and-revert.cs) | Change PDF Page Size and Revert Using PdfPageEditor | `Document`, `PdfPageEditor`, `PageSize` | Demonstrates how to modify a PDF page's dimensions with PdfPageEditor, save the changes, and then... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

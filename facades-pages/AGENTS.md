---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **114** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (97/114 files) ← category-specific
- `using Aspose.Pdf.Facades;` (79/114 files) ← category-specific
- `using Aspose.Pdf.Text;` (10/114 files)
- `using Aspose.Pdf.Annotations;` (3/114 files)
- `using Aspose.Pdf.Devices;` (2/114 files)
- `using System;` (114/114 files)
- `using System.IO;` (112/114 files)
- `using System.Linq;` (17/114 files)
- `using System.Collections.Generic;` (7/114 files)
- `using System.Drawing;` (1/114 files)
- `using System.Text;` (1/114 files)
- `using System.Text.Json;` (1/114 files)

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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Batch Resize PDF Pages by 10% Margin | `Document`, `PdfPageEditor`, `Pages` | Demonstrates how to use Aspose.Pdf's PdfPageEditor to scale the content of every page in a PDF to... |
| [add-15-percent-margins-to-pdf-pages](./add-15-percent-margins-to-pdf-pages.cs) | Apply 15% Margin Resize for Booklet Layout | `Document`, `PageInfo`, `MarginInfo` | Shows how to add a 15% margin on each side of selected PDF pages to create a booklet‑friendly lay... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margin to PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to create uniform 20% whitespace margins around every page of a PDF by scaling the page... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Resize PDF Pages by 5% to Add White Margin | `Document`, `PageInfo`, `Save` | Shows how to increase each PDF page size by 5 % using Aspose.Pdf, creating extra white space arou... |
| [add-fade-transition-to-pdf-page](./add-fade-transition-to-pdf-page.cs) | Apply Fade Transition to PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a fade transition and a two‑second duration for a specific page in a PDF using A... |
| [adjust-page-zoom-based-on-word-count](./adjust-page-zoom-based-on-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `PdfPageEditor`, `TextFragmentAbsorber` | Demonstrates how to extract text from each PDF page, compute a zoom factor from the word count, a... |
| [align-page-three-vertically-top](./align-page-three-vertically-top.cs) | Align Page 3 Content to Top Using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `AlignPageContent` | Demonstrates loading a PDF with PdfContentEditor, aligning the content of page 3 to the top using... |
| [align-page-two-left](./align-page-two-left.cs) | Left‑justify Text on Second PDF Page | `Document`, `Page`, `TextFragment` | Demonstrates how to add a left‑aligned text fragment to the second page of a PDF using Aspose.Pdf... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `ApplyChanges` | Shows how to assign different transition effects to each PDF page using Aspose.Pdf.Facades.PdfPag... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Per-Page Zoom Levels to PDF Using PdfPageEditor | `Document`, `PdfPageEditor`, `Save` | Shows how to set a different zoom factor for each page of a PDF by iterating through pages with P... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to set a Dissolve page transition with a three‑second duration on page 5 of a PD... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Set Fade Transition for All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to apply a Fade page transition to every page of a PDF using Aspose.Pdf's PdfPag... |
| [apply-horizontal-alignment-to-pdf-pages](./apply-horizontal-alignment-to-pdf-pages.cs) | Apply Left Horizontal Alignment to All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to left‑justify the content of every page in a PDF using Aspose.Pdf's PdfPageEditor and... |
| [apply-page-adjustments-from-json](./apply-page-adjustments-from-json.cs) | Apply Page Settings from JSON to PDF | `Document`, `Page`, `PageInfo` | Loads a JSON configuration that defines rotation, margins, and background color per page, then ap... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Page Transitions Based on Index Modulo Three | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to assign different page transition effects to each PDF page using PdfPageEditor, selec... |
| [apply-rotation-size-zoom-to-pdf-pages](./apply-rotation-size-zoom-to-pdf-pages.cs) | Apply Rotation, Resize, and Zoom to PDF Pages | `Document`, `Page`, `PdfPageEditor` | Shows how to rotate pages, change their dimensions, and zoom the page content of a PDF using Aspo... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Transition to Odd Pages in PDF | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to add a page transition effect only to odd‑numbered pages of a PDF using Aspose.Pdf.Fa... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `VerticalAlignmentType` | Shows how to set the vertical alignment of specific pages in a PDF to top using Aspose.Pdf's PdfP... |
| [apply-zoom-to-non-consecutive-pdf-pages](./apply-zoom-to-non-consecutive-pdf-pages.cs) | Apply Zoom to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to specify non‑consecutive pages with the ProcessPages array and apply a common zoom fa... |
| [assign-page-transitions-by-content](./assign-page-transitions-by-content.cs) | Set Page Transitions Based on Content Type | `Document`, `PdfPageEditor`, `Page` | The example loads a PDF, detects whether each page contains only images, only text, or a mix, and... |
| [audit-pdf-page-dimensions-rotation](./audit-pdf-page-dimensions-rotation.cs) | Log PDF Page Dimensions and Rotation Before and After Editin... | `Document`, `Page`, `PageInfo` | Demonstrates how to record each page's width, height, and rotation before and after adding a text... |
| [batch-adjust-pdf-page-size](./batch-adjust-pdf-page-size.cs) | Batch Set Individual Page Sizes for PDFs | `Document`, `PageInfo`, `Page` | Demonstrates how to process a folder of PDF files and assign a specific page size to each page of... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to iterate over PDF files in a directory and resize each document to A4 dimensions usin... |
| [batch-rotate-first-page-pdfs](./batch-rotate-first-page-pdfs.cs) | Rotate First Page of PDFs in a Folder | `Document`, `Rotation`, `Page` | Demonstrates how to batch‑process PDF files in a directory, rotating the first page of each docum... |
| [batch-set-fade-transition-pdf-slideshow](./batch-set-fade-transition-pdf-slideshow.cs) | Set Fade Transition for All PDF Pages | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to apply a 2‑second Fade transition to every page of a PDF using Aspose.Pdf's Pd... |
| [center-page-content-horizontally](./center-page-content-horizontally.cs) | Center Text on Second PDF Page | `Document`, `Page`, `TextFragment` | Shows how to load a PDF, access the second page, and add a text fragment that is horizontally cen... |
| [center-page-content-set-display-duration](./center-page-content-set-display-duration.cs) | Center Page Content and Set Display Duration on PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to center the content of a specific PDF page and set its display duration to fou... |
| [chain-page-rotation-size-zoom-modifications](./chain-page-rotation-size-zoom-modifications.cs) | Chain Page Rotation, Resize, and Zoom before Saving PDF | `Document`, `Page`, `PageInfo` | Shows how to rotate pages, change their size, and apply a viewer zoom using Aspose.Pdf Document a... |
| [change-pdf-page-size-and-undo](./change-pdf-page-size-and-undo.cs) | Change PDF Page Size and Revert to Original | `Document`, `Page`, `PageInfo` | Demonstrates how to capture a PDF page's original dimensions, apply a custom page size, and then ... |
| [change-pdf-page-size-to-a3](./change-pdf-page-size-to-a3.cs) | Resize PDF Pages to A3 Size | `Document`, `Page`, `PageInfo` | Shows how to load a PDF with Aspose.Pdf, set each page's width and height to A3 dimensions, and s... |
| ... | | | *and 84 more files* |

## Category Statistics
- Total examples: 114

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
Updated: 2026-11-11 | Run: `20261111_025854_ab1c98`
<!-- AUTOGENERATED:END -->

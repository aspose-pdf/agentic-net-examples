---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (80/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/117 files)
- `using Aspose.Pdf.Annotations;` (1/117 files)
- `using System;` (116/117 files)
- `using System.IO;` (108/117 files)
- `using System.Collections.Generic;` (15/117 files)
- `using System.Linq;` (3/117 files)
- `using System.Net.Http;` (1/117 files)
- `using System.Text.Json;` (1/117 files)
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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Add 10% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to add a 10 % margin on all sides of eve... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margins to PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add a 20 % margin around all pages of a PDF using Aspose.Pdf's PdfFileEditor ... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margins to PDF Pages and Print | `PdfFileEditor`, `AddMarginsPct`, `PdfViewer` | Demonstrates how to add a 5 % margin to all pages of a PDF using PdfFileEditor, then print the re... |
| [add-percentage-margins-to-pdf-pages](./add-percentage-margins-to-pdf-pages.cs) | Add 15% Margins to Selected PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to apply a 15 % margin on all sides of specific pages in a PDF using Aspose.Pdf.... |
| [add-transition-to-odd-pdf-pages](./add-transition-to-odd-pdf-pages.cs) | Add Transition Effect to Odd PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to apply a page transition only to odd‑numbered pages of a PDF using Aspose.Pdf.... |
| [adjust-pdf-page-zoom-by-word-count](./adjust-pdf-page-zoom-by-word-count.cs) | Adjust PDF Page Zoom Based on Word Count | `Document`, `TextAbsorber`, `TextExtractionOptions` | Shows how to count words on each PDF page and apply a dynamic zoom factor with PdfPageEditor to i... |
| [adjust-zoom-of-specific-pdf-page](./adjust-zoom-of-specific-pdf-page.cs) | Adjust Zoom of Specific PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to set a 150% zoom factor for page three of a PDF using the Aspose.Pdf.Facades P... |
| [align-vertical-content-page-three](./align-vertical-content-page-three.cs) | Vertically Align PDF Page Content to Center | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Demonstrates how to center the original content vertically on a specific page (page 3) of a PDF u... |
| [apply-boxout-transition-to-pdf-page](./apply-boxout-transition-to-pdf-page.cs) | Apply BoxOut Transition to PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to set a BoxOut (OUTBOX) page transition with a... |
| [apply-cover-transition-to-pdf-page](./apply-cover-transition-to-pdf-page.cs) | Apply Cover Transition to a PDF Page | `Document`, `PdfPageEditor`, `Save` | Creates a four‑page PDF and uses PdfPageEditor to set a Cover page transition with a one‑second d... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to use Aspose.Pdf's PdfPageEditor facade to assign different transition effects ... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to iterate through each page of a PDF and set a distinct zoom factor using Aspose.Pdf.F... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to a PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to set a dissolve page transition with a 3‑seco... |
| [apply-fade-transition-all-pdf-pages](./apply-fade-transition-all-pdf-pages.cs) | Apply Fade (Dissolve) Transition to All PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to set a fade-like (Dissolve) page transition for every page in a PDF using Aspose.Pdf'... |
| [apply-horizontal-alignment-to-pdf-pages](./apply-horizontal-alignment-to-pdf-pages.cs) | Apply Horizontal Alignment to PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Shows how to set left horizontal alignment for all pages of a PDF using Aspose.Pdf.Facades.PdfPag... |
| [apply-page-settings-from-json](./apply-page-settings-from-json.cs) | Apply Page Settings from JSON to PDF | `Document`, `PdfPageEditor`, `Rotation` | Shows how to read a JSON configuration file and apply per‑page properties such as rotation, zoom,... |
| [apply-page-transitions-per-index](./apply-page-transitions-per-index.cs) | Apply Different Page Transitions per PDF Page | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to iterate through PDF pages and assign different transition effects (vertical blinds, ... |
| [apply-rotation-zoom-transition-to-pdf-pages](./apply-rotation-zoom-transition-to-pdf-pages.cs) | Apply Rotation, Zoom, and Transition Effects to PDF Pages | `Document`, `PdfPageEditor`, `Rotation` | Demonstrates how to rotate and zoom PDF pages and add presentation transition effects using Aspos... |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply Sequential Page Transitions in a PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to assign different transition effects to consecutive pages of a PDF using Aspose.Pdf's... |
| [apply-split-transition-to-pdf-page](./apply-split-transition-to-pdf-page.cs) | Apply Split Transition to PDF Page | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set a split page transition and a 2‑second duration for a specific page using... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to set the vertical alignment of specific pages... |
| [apply-zoom-to-even-pdf-pages](./apply-zoom-to-even-pdf-pages.cs) | Apply Zoom to Even Pages in PDF | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to set a 1.2 (120%) zoom factor on all even‑numbered pages of a PDF using Aspose.Pdf.Fa... |
| [apply-zoom-to-even-pdf-pages__v2](./apply-zoom-to-even-pdf-pages__v2.cs) | Apply Zoom to Even PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to use Aspose.Pdf's PdfPageEditor to set a zoom factor of 0.8 on all even‑number... |
| [assign-page-transitions-by-content](./assign-page-transitions-by-content.cs) | Assign Page Transitions Based on Content Type | `Document`, `PdfPageEditor`, `Page` | Shows how to iterate through PDF pages, detect if a page contains images, and apply different pag... |
| [audit-pdf-page-dimensions-rotation](./audit-pdf-page-dimensions-rotation.cs) | Audit PDF Page Dimensions and Rotation Before and After Edit... | `PdfPageEditor`, `BindPdf`, `GetPages` | Shows how to log each page's width, height, and rotation before and after applying changes with P... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Convert PDFs to A4 Page Size | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to loop through multiple PDF files and use PdfPageEditor to resize every page to A4, sa... |
| [batch-resize-pdf-pages-by-filename](./batch-resize-pdf-pages-by-filename.cs) | Batch Resize PDF Pages Based on Filename Keywords | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to iterate through PDFs in a folder, pick a target page size from a keyword map derived... |
| [batch-set-fade-transition-for-pdf-slideshow](./batch-set-fade-transition-for-pdf-slideshow.cs) | Batch Set Fade Transition for PDF Slideshow | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to apply a Fade (Dissolve) page transition with a 2‑second duration to all pages... |
| [center-align-page-set-display-duration](./center-align-page-set-display-duration.cs) | Center Align PDF Page and Set Display Duration | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to center‑align the content of a specific PDF page and set its display duration ... |
| [center-content-on-page-two](./center-content-on-page-two.cs) | Center Content on Specific PDF Page | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use Aspose.Pdf.Facades.PdfPageEditor to horizontally center the existing content of ... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

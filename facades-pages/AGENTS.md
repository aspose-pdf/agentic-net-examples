---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

> **Facades pages** in PDF using C# / .NET -- **112** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (104/112 files) ← category-specific
- `using Aspose.Pdf.Facades;` (70/112 files) ← category-specific
- `using Aspose.Pdf.Text;` (6/112 files)
- `using Aspose.Pdf.Devices;` (5/112 files)
- `using Aspose.Pdf.Annotations;` (1/112 files)
- `using Aspose.Pdf.Drawing;` (1/112 files)
- `using System;` (112/112 files)
- `using System.IO;` (109/112 files)
- `using System.Linq;` (17/112 files)
- `using System.Collections.Generic;` (8/112 files)
- `using System.Text.Json;` (1/112 files)

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
| [add-10-percent-margins-to-pdf-pages](./add-10-percent-margins-to-pdf-pages.cs) | Resize PDF Pages with Uniform Margin | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to apply a uniform 80% zoom to all pages of a PDF using PdfPageEditor, creating a 10% m... |
| [add-15-percent-margins-to-pdf-pages](./add-15-percent-margins-to-pdf-pages.cs) | Apply 15% Margin Resize to Selected PDF Pages for Booklet La... | `Document`, `Page`, `PageInfo` | Loads a PDF, selects specific pages, applies a 15 % margin on all sides to improve readability fo... |
| [add-20-percent-margins-to-pdf-pages](./add-20-percent-margins-to-pdf-pages.cs) | Add 20% Margin to PDF Pages | `Document`, `Page`, `PageInfo` | Demonstrates how to increase each PDF page's size and add a uniform 20% whitespace margin around ... |
| [add-5-percent-margins-and-print-pdf](./add-5-percent-margins-and-print-pdf.cs) | Add 5% Margin to PDF Pages | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to enlarge each PDF page by 5 % to create a white margin and then scale the orig... |
| [add-fade-transition-to-pdf-page](./add-fade-transition-to-pdf-page.cs) | Apply Fade Transition to First PDF Page | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to add a 2‑second Fade (Dissolve) transition to the first page of a PDF using As... |
| [adjust-page-zoom-based-on-word-count](./adjust-page-zoom-based-on-word-count.cs) | Adjust PDF Page Zoom Based on Word Count | `Document`, `Page`, `TextAbsorber` | The example loads a PDF, extracts the plain text of each page to count words, and then scales the... |
| [align-page-three-vertically-top](./align-page-three-vertically-top.cs) | Align Page 3 Vertically to Top | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to use PdfPageEditor to align the content of page 3 to the top of the page in a PDF doc... |
| [align-page-two-left](./align-page-two-left.cs) | Left Align Content of PDF Page Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `Save` | Demonstrates how to left‑justify the content of a specific PDF page (page 2) using Aspose.Pdf's P... |
| [apply-custom-page-transitions](./apply-custom-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to assign different transition effects to each PDF page using PdfPageEditor, varying th... |
| [apply-different-zoom-levels-to-pdf-pages](./apply-different-zoom-levels-to-pdf-pages.cs) | Apply Different Zoom Levels to PDF Pages and Convert to TIFF | `Document`, `PdfPageEditor`, `PdfConverter` | Demonstrates iterating through each PDF page, applying a unique zoom factor with PdfPageEditor, a... |
| [apply-dissolve-transition-to-pdf-page](./apply-dissolve-transition-to-pdf-page.cs) | Apply Dissolve Transition to PDF Page | `Document`, `PdfPageEditor`, `ApplyChanges` | Shows how to set a Dissolve page transition with a 3‑second duration on a specific page using Asp... |
| [apply-fade-transition-to-all-pdf-pages](./apply-fade-transition-to-all-pdf-pages.cs) | Apply Fade Transition to All PDF Pages | `Document`, `PdfPageEditor`, `TransitionType` | Demonstrates how to set a Fade page transition for every page in a PDF using Aspose.Pdf's PdfPage... |
| [apply-horizontal-alignment-to-pdf-pages](./apply-horizontal-alignment-to-pdf-pages.cs) | Left‑justify Text on All PDF Pages | `PdfPageEditor`, `BindPdf`, `Save` | Shows how to use PdfPageEditor to iterate through each page of a PDF, set the HorizontalAlignment... |
| [apply-page-adjustments-from-json](./apply-page-adjustments-from-json.cs) | Apply Page Adjustments from JSON Config to PDFs | `Document`, `Page`, `Rotation` | Shows how to read a JSON configuration file and programmatically rotate, resize, and set backgrou... |
| [apply-page-transitions-by-index](./apply-page-transitions-by-index.cs) | Apply Rotating Page Transitions with PdfPageEditor | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates loading a PDF, iterating through its pages, and assigning different transition effec... |
| [apply-rotation-size-zoom-to-pdf-pages](./apply-rotation-size-zoom-to-pdf-pages.cs) | Rotate, Resize, and Zoom PDF Pages | `Document`, `PageInfo`, `Rotation` | Shows how to rotate all pages, set a custom page size, and apply a uniform zoom factor to a PDF u... |
| [apply-transition-to-odd-pages](./apply-transition-to-odd-pages.cs) | Apply Fade Transition to Odd Pages in PDF | `Document`, `PdfPageEditor`, `BindPdf` | Shows how to apply a Fade page transition only to odd‑numbered pages of a PDF using Aspose.Pdf.Fa... |
| [apply-vertical-alignment-to-selected-pdf-pages](./apply-vertical-alignment-to-selected-pdf-pages.cs) | Apply Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `VerticalAlignmentType` | Demonstrates how to align specific pages of a PDF to the top using PdfPageEditor’s VerticalAlignm... |
| [apply-zoom-to-non-consecutive-pdf-pages](./apply-zoom-to-non-consecutive-pdf-pages.cs) | Select Non-Consecutive PDF Pages and Apply Common Zoom | `Document`, `Add`, `Save` | Demonstrates how to extract specific non‑consecutive pages from a PDF, combine them into a new do... |
| [assign-page-transitions-by-content](./assign-page-transitions-by-content.cs) | Set Individual Page Transitions in PDF | `Document`, `PdfPageEditor`, `ProcessPages` | Shows how to assign different transition effects to specific PDF pages using Aspose.Pdf's PdfPage... |
| [audit-pdf-page-dimensions-rotation](./audit-pdf-page-dimensions-rotation.cs) | Audit PDF Page Dimensions and Rotation Before and After Edit... | `Document`, `Page`, `PageInfo` | Demonstrates how to log each page's width, height, and rotation angle before and after applying a... |
| [batch-adjust-pdf-page-size](./batch-adjust-pdf-page-size.cs) | Batch Set Individual Page Sizes in PDFs | `Document`, `PageInfo`, `Save` | Demonstrates how to process all PDF files in a folder, iterate through each page, assign differen... |
| [batch-convert-pdfs-to-a4](./batch-convert-pdfs-to-a4.cs) | Batch Resize PDFs to A4 Page Size | `Document`, `Page`, `PageInfo` | Shows how to process all PDF files in a folder, load each with Aspose.Pdf, change every page's di... |
| [batch-rotate-first-page-pdfs](./batch-rotate-first-page-pdfs.cs) | Batch Rotate First Page of PDFs | `Document`, `Save`, `Pages` | Shows how to loop through a directory of PDF files, rotate the first page of each document by 90 ... |
| [batch-set-fade-transition-pdf-slideshow](./batch-set-fade-transition-pdf-slideshow.cs) | Batch Apply Fade Transition to PDF Pages | `Document`, `BindPdf`, `ProcessPages` | Demonstrates how to load a PDF and use PdfPageEditor to set a Fade slide transition with a two‑se... |
| [center-page-content-horizontally](./center-page-content-horizontally.cs) | Center Text Horizontally on PDF Page 2 | `Document`, `TextFragment`, `FindFont` | Shows how to load a PDF, create a TextFragment, set its HorizontalAlignment to Center, and add it... |
| [center-page-content-set-display-duration](./center-page-content-set-display-duration.cs) | Center Align Page and Set Display Duration in PDF | `PdfPageEditor`, `BindPdf`, `ProcessPages` | Shows how to center‑align the content of a specific PDF page and set its automatic display durati... |
| [chain-page-rotation-size-zoom-modifications](./chain-page-rotation-size-zoom-modifications.cs) | Rotate, Resize and Zoom PDF Pages | `Document`, `Page`, `Rotation` | Demonstrates how to chain multiple page modifications—rotation, size change, and a simulated zoom... |
| [change-pdf-page-size-and-undo](./change-pdf-page-size-and-undo.cs) | Change PDF Page Size and Undo to Original Dimensions | `Document`, `PageInfo`, `Save` | Demonstrates how to record original page dimensions, apply a custom size to all pages, save the m... |
| [change-pdf-page-size-to-a3](./change-pdf-page-size-to-a3.cs) | Resize PDF Pages to A3 Size | `Document`, `Page`, `PageInfo` | Shows how to load a PDF with Aspose.Pdf, set each page's dimensions to A3, and save the modified ... |
| ... | | | *and 82 more files* |

## Category Statistics
- Total examples: 112

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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

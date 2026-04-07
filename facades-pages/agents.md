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

- `using Aspose.Pdf;` (103/116 files) ← category-specific
- `using Aspose.Pdf.Facades;` (76/116 files) ← category-specific
- `using Aspose.Pdf.Text;` (12/116 files)
- `using Aspose.Pdf.Annotations;` (3/116 files)
- `using Aspose.Pdf.Drawing;` (1/116 files)
- `using Aspose.Pdf.Printing;` (1/116 files)
- `using System;` (116/116 files)
- `using System.IO;` (105/116 files)
- `using System.Runtime.InteropServices;` (4/116 files)
- `using System.Collections.Generic;` (3/116 files)
- `using System.Linq;` (3/116 files)
- `using System.Text.Json;` (1/116 files)

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
| [add-5-percent-margins](./add-5-percent-margins.cs) | Add 5% Margins to All PDF Pages | `PdfFileEditor`, `AddMarginsPct` | Demonstrates how to add a uniform 5% margin to every page of a PDF using Aspose.Pdf's PdfFileEditor. |
| [adjust-zoom-by-word-count](./adjust-zoom-by-word-count.cs) | Adjust Page Zoom Based on Word Count | `Document`, `Page`, `TextAbsorber` | Demonstrates how to increase the zoom level on PDF pages that contain fewer words, improving read... |
| [align-page-two-left](./align-page-two-left.cs) | Align Page Two Content Left in PDF | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Demonstrates how to left‑justify the content of the second page of a PDF using PdfPageEditor. |
| [align-page-vertical-middle](./align-page-vertical-middle.cs) | Align Page Content Vertically to Middle on Page 3 | `PdfPageEditor`, `VerticalAlignment`, `HorizontalAlignment` | Demonstrates how to vertically center the content of the third page of a PDF using PdfPageEditor. |
| [align-page-vertical-top](./align-page-vertical-top.cs) | Align Page Content Vertically to Top | `PdfPageEditor`, `VerticalAlignmentType`, `VerticalAlignment` | Demonstrates aligning the content of the third page of a PDF to the top using PdfPageEditor.Verti... |
| [apply-different-zoom-levels](./apply-different-zoom-levels.cs) | Apply Different Zoom Levels to PDF Pages | `PdfPageEditor`, `BindPdf`, `GetPages` | Demonstrates how to set a distinct zoom factor for each page of a PDF using PdfPageEditor. |
| [apply-dissolve-transition](./apply-dissolve-transition.cs) | Apply Dissolve Transition to PDF Page | `Document`, `PdfPageEditor`, `ProcessPages` | Demonstrates how to set a dissolve page transition with a three‑second duration on page five of a... |
| [apply-double-zoom-stamp](./apply-double-zoom-stamp.cs) | Apply Double-Precision Zoom to a Stamp in PDF | `Document`, `AddStamp`, `BindImage` | Demonstrates how to set a double‑precision zoom factor on a stamp and apply it to a PDF page. |
| [apply-image-stamp-zoom](./apply-image-stamp-zoom.cs) | Apply Image Stamp to Selected Non-Consecutive Pages with Zoo... | `PdfFileStamp`, `ImageStamp`, `Zoom` | Demonstrates how to stamp an image on specific non-consecutive PDF pages and set a common zoom fa... |
| [apply-left-horizontal-alignment](./apply-left-horizontal-alignment.cs) | Apply Left Horizontal Alignment to All PDF Pages | `PdfPageEditor`, `BindPdf`, `HorizontalAlignment` | Demonstrates how to set a uniform left‑justified horizontal alignment for all pages of a PDF usin... |
| [apply-page-settings-from-json](./apply-page-settings-from-json.cs) | Apply Page Settings from JSON Configuration to PDF | `Document`, `Page`, `PageInfo` | Loads a PDF, reads page size and background color settings from a JSON file, and applies them to ... |
| [apply-page-transitions](./apply-page-transitions.cs) | Apply Custom Page Transitions Based on Index | `PdfPageEditor`, `Document`, `TransitionType` | Demonstrates how to set different transition effects for each PDF page using PdfPageEditor. |
| [apply-page-transitions__v2](./apply-page-transitions__v2.cs) | Apply Page Transition Effects Based on Page Index | `PdfPageEditor`, `Document`, `TransitionType` | Demonstrates how to set different transition effects for each PDF page using PdfPageEditor, cycli... |
| [apply-sequential-page-transitions](./apply-sequential-page-transitions.cs) | Apply Sequential Page Transitions in PDF | `PdfPageEditor`, `Document`, `BindPdf` | Demonstrates how to set different transition effects for each page of a PDF using PdfPageEditor. |
| [apply-transition-odd-pages](./apply-transition-odd-pages.cs) | Apply Transition Effect to Odd Pages in PDF | `PdfPageEditor`, `Document`, `ProcessPages` | Demonstrates how to set a page transition effect only on odd‑numbered pages of a PDF using Aspose... |
| [apply-vertical-alignment](./apply-vertical-alignment.cs) | Apply Top Vertical Alignment to Selected PDF Pages | `PdfPageEditor`, `BindPdf`, `PageNumbers` | Demonstrates how to set the vertical alignment of specific pages in a PDF to the top using PdfPag... |
| [apply-zoom-even-pages](./apply-zoom-even-pages.cs) | Apply Zoom to Even-Numbered PDF Pages | `Document`, `PdfPageEditor`, `File` | Demonstrates how to set a 1.2 zoom level on all even-numbered pages of a PDF using Aspose.Pdf. |
| [apply-zoom-to-image-pages](./apply-zoom-to-image-pages.cs) | Apply Zoom to Pages Containing Images | `Document`, `ImagePlacementAbsorber`, `PdfPageEditor` | Detects pages that contain images and applies a zoom factor only to those pages, leaving other pa... |
| [assign-page-transitions](./assign-page-transitions.cs) | Assign Page Transition Effects Based on Content Type | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates how to set different transition effects for individual PDF pages using PdfPageEditor. |
| [batch-convert-pdfs-a4](./batch-convert-pdfs-a4.cs) | Batch Convert PDFs to A4 Page Size | `Document`, `SetPageSize`, `A4` | Converts multiple PDF files to A4 page size for uniform printing across all documents. |
| [batch-resize-pdf-contents](./batch-resize-pdf-contents.cs) | Batch Resize PDF Contents with 10% Margins | `PdfFileEditor`, `ContentsResizeParameters`, `ContentsResizeValue` | Demonstrates how to shrink the contents of all pages in a PDF by adding a 10 % margin using Aspos... |
| [batch-set-page-sizes](./batch-set-page-sizes.cs) | Batch Process PDFs and Set Individual Page Sizes | `Document`, `Page`, `PageInfo` | Processes all PDF files in a folder, sets each page size based on the file name (e.g., A4 or Lett... |
| [booklet-margin-resize](./booklet-margin-resize.cs) | Create Booklet with 15% Margin Resize | `Document`, `PageInfo`, `Margin` | Demonstrates how to increase page margins by 15% and then generate a booklet layout using Aspose.... |
| [center-align-page-duration](./center-align-page-duration.cs) | Center Align Page and Set Display Duration | `PdfPageEditor`, `Document`, `BindPdf` | Demonstrates how to center‑align a specific PDF page and set its display duration to four seconds... |
| [center-align-second-page](./center-align-second-page.cs) | Center Align Text on Second Page | `Document`, `Page`, `TextFragment` | Demonstrates how to center-align text on the second page of a PDF using Aspose.Pdf. |
| [chain-page-modifications](./chain-page-modifications.cs) | Chain Page Rotation, Size, and Zoom Modifications | `PdfPageEditor`, `Document`, `Rotation` | Demonstrates how to rotate pages, change page size, and apply zoom using PdfPageEditor before sav... |
| [change-page-size-a3](./change-page-size-a3.cs) | Change PDF Page Size to A3 | `Document`, `SetPageSize`, `A3` | Demonstrates how to resize all pages of a PDF to A3 size, providing a larger canvas for high‑reso... |
| [change-page-size-revert](./change-page-size-revert.cs) | Change PDF Page Size and Revert to Original | `Document`, `SetPageSize`, `Save` | Demonstrates changing a PDF page to custom dimensions and then restoring the original size to ver... |
| [combine-rotation-zoom-transition](./combine-rotation-zoom-transition.cs) | Combine Page Rotation and Zoom with Transition Effects | `Document`, `PdfPageEditor`, `BindPdf` | Demonstrates using PdfPageEditor to rotate pages, apply zoom, and set a transition effect for pre... |
| [convert-portrait-to-landscape](./convert-portrait-to-landscape.cs) | Convert Portrait PDF Page to Landscape Orientation | `Document`, `SetPageSize`, `PageInfo` | Demonstrates how to change a PDF page from portrait to landscape by swapping its dimensions and v... |
| ... | | | *and 86 more files* |

## Category Statistics
- Total examples: 116

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

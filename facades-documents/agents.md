---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (49/56 files) ← category-specific
- `using Aspose.Pdf;` (21/56 files)
- `using Aspose.Pdf.Text;` (3/56 files)
- `using System;` (56/56 files)
- `using System.IO;` (56/56 files)
- `using System.Runtime.InteropServices;` (3/56 files)
- `using System.Collections.Generic;` (2/56 files)

## Common Code Pattern

Most files in this category use `PdfFileEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfFileEditor tool = new PdfFileEditor();
tool.BindPdf("input.pdf");
// ... PdfFileEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [append-a-loaded-pdf-to-another-document-concatenating-multip...](./append-a-loaded-pdf-to-another-document-concatenating-multiple-pdfs-while-preserving-original-conte.cs) | `PdfFileEditor` | Append a loaded pdf to another document concatenating multiple pdfs while pre... |
| [combine-multiple-pdf-files-using-various-loading-techniques-...](./combine-multiple-pdf-files-using-various-loading-techniques-to-create-a-single-concatenated-document.cs) | `PdfFileEditor`, `TextFragment` | Combine multiple pdf files using various loading techniques to create a singl... |
| [concatenation-loads-multiple-pdf-files-sequentially-merging-...](./concatenation-loads-multiple-pdf-files-sequentially-merging-them-into-a-single-continuous-in-memory.cs) | `PdfFileEditor` | Concatenation loads multiple pdf files sequentially merging them into a singl... |
| [configure-each-loaded-pdf-page-s-desiredsize-property-progra...](./configure-each-loaded-pdf-page-s-desiredsize-property-programmatically-to-uniformly-specify-its-targ.cs) | `PdfPageEditor` | Configure each loaded pdf page s desiredsize property programmatically to uni... |
| [configure-the-bookletlayout-property-while-loading-a-pdf-doc...](./configure-the-bookletlayout-property-while-loading-a-pdf-document-to-define-booklet-pagination-behav.cs) | `PdfFileEditor` | Configure the bookletlayout property while loading a pdf document to define b... |
| [create-a-booklet-version-of-a-loaded-pdf-document-reordering...](./create-a-booklet-version-of-a-loaded-pdf-document-reordering-pages-for-printed-folding.cs) | `PdfFileEditor` | Create a booklet version of a loaded pdf document reordering pages for printe... |
| [delete-selected-pages-from-a-loaded-pdf-document-while-maint...](./delete-selected-pages-from-a-loaded-pdf-document-while-maintaining-the-integrity-of-the-remaining-co.cs) | `PdfFileEditor` | Delete selected pages from a loaded pdf document while maintaining the integr... |
| [demonstrate-loading-a-pdf-document-into-memory-via-the-provi...](./demonstrate-loading-a-pdf-document-into-memory-via-the-provided-api-initializing-required-objects-a.cs) |  | Demonstrate loading a pdf document into memory via the provided api initializ... |
| [describe-pdf-splitting-functionality-for-loading-a-pdf-docum...](./describe-pdf-splitting-functionality-for-loading-a-pdf-document-and-dividing-it-into-separate-pages.cs) | `PdfFileEditor` | Describe pdf splitting functionality for loading a pdf document and dividing ... |
| [detect-page-break-positions-in-a-loaded-pdf-and-return-their...](./detect-page-break-positions-in-a-loaded-pdf-and-return-their-corresponding-page-numbers-for-further.cs) |  | Detect page break positions in a loaded pdf and return their corresponding pa... |
| [explain-the-n-up-layout-option-for-loading-a-pdf-describing-...](./explain-the-n-up-layout-option-for-loading-a-pdf-describing-how-pages-are-tiled-per-sheet.cs) | `PdfFileEditor` | Explain the n up layout option for loading a pdf describing how pages are til... |
| [generate-an-n-up-pdf-by-loading-an-existing-pdf-and-arrangin...](./generate-an-n-up-pdf-by-loading-an-existing-pdf-and-arranging-its-pages-in-a-grid-layout.cs) | `PdfFileEditor` | Generate an n up pdf by loading an existing pdf and arranging its pages in a ... |
| [implement-functionality-to-load-a-pdf-file-and-extract-speci...](./implement-functionality-to-load-a-pdf-file-and-extract-specified-pages-into-a-new-document.cs) | `PdfFileEditor` | Implement functionality to load a pdf file and extract specified pages into a... |
| [insert-a-new-page-into-a-loaded-pdf-at-a-specified-index-whi...](./insert-a-new-page-into-a-loaded-pdf-at-a-specified-index-while-maintaining-document-integrity.cs) | `PdfFileEditor` | Insert a new page into a loaded pdf at a specified index while maintaining do... |
| [insert-a-new-page-into-an-existing-pdf-document-by-loading-t...](./insert-a-new-page-into-an-existing-pdf-document-by-loading-the-source-pdf-and-specifying-the-inserti.cs) | `PdfFileEditor` | Insert a new page into an existing pdf document by loading the source pdf and... |
| [insert-a-page-break-into-a-loaded-pdf-document-while-preserv...](./insert-a-page-break-into-a-loaded-pdf-document-while-preserving-existing-content-integrity.cs) | `PdfFileEditor` | Insert a page break into a loaded pdf document while preserving existing cont... |
| [insert-a-page-break-into-an-opened-pdf-document-programmatic...](./insert-a-page-break-into-an-opened-pdf-document-programmatically-while-maintaining-existing-content.cs) | `PdfFileEditor` | Insert a page break into an opened pdf document programmatically while mainta... |
| [insert-additional-pages-into-an-existing-pdf-by-loading-the-...](./insert-additional-pages-into-an-existing-pdf-by-loading-the-document-and-appending-specified-page-co.cs) | `PdfFileEditor` | Insert additional pages into an existing pdf by loading the document and appe... |
| [insert-pages-from-one-pdf-into-another-to-concatenate-docume...](./insert-pages-from-one-pdf-into-another-to-concatenate-documents-after-loading-the-source-pdf.cs) | `PdfFileEditor` | Insert pages from one pdf into another to concatenate documents after loading... |
| [instantiate-a-new-pdf-document-object-and-append-the-specifi...](./instantiate-a-new-pdf-document-object-and-append-the-specified-pages-loaded-from-an-existing-pdf-fil.cs) | `PdfFileEditor` | Instantiate a new pdf document object and append the specified pages loaded f... |
| [instantiate-a-pdf-document-load-its-content-and-add-a-new-pa...](./instantiate-a-pdf-document-load-its-content-and-add-a-new-page-via-the-page-class.cs) | `PdfPageEditor` | Instantiate a pdf document load its content and add a new page via the page c... |
| [instantiate-a-pdfdocument-object-for-each-pdf-file-to-load-i...](./instantiate-a-pdfdocument-object-for-each-pdf-file-to-load-its-contents-into-memory.cs) |  | Instantiate a pdfdocument object for each pdf file to load its contents into ... |
| [instantiate-separate-pdf-document-objects-for-each-specified...](./instantiate-separate-pdf-document-objects-for-each-specified-range-loading-the-source-pdfs-accordin.cs) | `PdfFileEditor` | Instantiate separate pdf document objects for each specified range loading th... |
| [load-a-pdf-and-concatenate-its-pages-into-a-single-document-...](./load-a-pdf-and-concatenate-its-pages-into-a-single-document-using-the-merge-operation.cs) | `PdfFileEditor` | Load a pdf and concatenate its pages into a single document using the merge o... |
| [load-a-pdf-and-resize-its-pages-to-specified-dimensions-whil...](./load-a-pdf-and-resize-its-pages-to-specified-dimensions-while-maintaining-content-fidelity.cs) | `PdfPageEditor` | Load a pdf and resize its pages to specified dimensions while maintaining con... |
| [load-a-pdf-document-and-apply-an-n-up-layout-to-arrange-mult...](./load-a-pdf-document-and-apply-an-n-up-layout-to-arrange-multiple-pages-per-sheet.cs) | `PdfFileEditor` | Load a pdf document and apply an n up layout to arrange multiple pages per sheet |
| [load-a-pdf-document-and-convert-it-into-a-booklet-format-pre...](./load-a-pdf-document-and-convert-it-into-a-booklet-format-preserving-page-ordering-and-orientation.cs) | `PdfFileEditor` | Load a pdf document and convert it into a booklet format preserving page orde... |
| [load-a-pdf-document-and-define-specific-page-numbers-to-extr...](./load-a-pdf-document-and-define-specific-page-numbers-to-extract-from-for-processing.cs) | `PdfFileEditor` | Load a pdf document and define specific page numbers to extract from for proc... |
| [load-a-pdf-document-and-extract-selected-pages-into-a-new-pd...](./load-a-pdf-document-and-extract-selected-pages-into-a-new-pdf-while-maintaining-content-integrity.cs) | `PdfFileEditor` | Load a pdf document and extract selected pages into a new pdf while maintaini... |
| [load-a-pdf-document-and-extract-selected-pages-into-individu...](./load-a-pdf-document-and-extract-selected-pages-into-individual-pdf-files-while-preserving-content-in.cs) | `PdfFileEditor` | Load a pdf document and extract selected pages into individual pdf files whil... |
| ... | | *and 26 more files* |

## Category Statistics
- Total examples: 56

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

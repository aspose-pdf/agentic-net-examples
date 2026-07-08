---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

> **Facades documents** in PDF using C# / .NET -- **101** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (99/101 files) ← category-specific
- `using Aspose.Pdf;` (36/101 files)
- `using Aspose.Pdf.Text;` (6/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (98/101 files)
- `using System.Collections.Generic;` (15/101 files)
- `using System.Linq;` (5/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using NUnit.Framework;` (2/101 files)
- `using System.Threading.Tasks;` (2/101 files)
- `using System.IO.Compression;` (1/101 files)

## Common Code Pattern

Most files in this category use `PdfFileEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfFileEditor tool = new PdfFileEditor();
tool.BindPdf("input.pdf");
// ... PdfFileEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [append-page-ranges-multiple-pdfs](./append-page-ranges-multiple-pdfs.cs) | Append Page Ranges from Multiple PDFs Using TryAppend | `PdfFileEditor`, `TryAppend`, `LastException` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor.TryAppend to insert the same page range ... |
| [append-pdf-pages-to-another-document](./append-pdf-pages-to-another-document.cs) | Append PDF Pages to Another Document | `Document`, `PdfFileEditor`, `Append` | Demonstrates how to append all pages from a source PDF to the end of a destination PDF using Aspo... |
| [apply-4up-layout-to-pdf-via-stream](./apply-4up-layout-to-pdf-via-stream.cs) | Apply 4‑up Layout to PDF via Stream Overload | `PdfFileEditor`, `MakeNUp`, `Document` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to create a 4‑up (2×2) PDF by processing inpu... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to gather all PDF files from a directory and merge them into a single PDF using Aspose.... |
| [batch-create-3x2-nup-pdfs](./batch-create-3x2-nup-pdfs.cs) | Batch Create 3x2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Processes all PDF files in a folder and generates N‑up versions with three columns and two rows u... |
| [batch-delete-pages-from-pdfs](./batch-delete-pages-from-pdfs.cs) | Batch Delete Pages from Multiple PDFs | `PdfFileEditor`, `Delete` | Shows how to iterate over PDF files in a folder and remove specified pages from each document usi... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specified pages from several PDF files and then concatenate the cleaned PDFs ... |
| [batch-insert-page-ranges-into-pdf](./batch-insert-page-ranges-into-pdf.cs) | Batch Insert Page Ranges into PDF | `PdfFileEditor`, `Insert` | Demonstrates how to insert specific page ranges from multiple source PDFs into a single destinati... |
| [batch-resize-pdfs-a5-booklet](./batch-resize-pdfs-a5-booklet.cs) | Batch Resize PDFs to A5 and Create Booklets | `PdfFileEditor`, `ResizeContents`, `MakeBooklet` | Shows how to iterate over PDF files in a folder, resize each page to A5 dimensions, and then gene... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to iterate through a folder of PDF files and resize each document to A4 page size using... |
| [compare-file-path-and-stream-overloads-pdf-resizin...](./compare-file-path-and-stream-overloads-pdf-resizing.cs) | Compare File‑Path and Stream Overloads for PDF Resizing | `PdfPageEditor`, `BindPdf`, `Save` | Demonstrates how to resize a large PDF using Aspose.Pdf.Facades.PdfPageEditor via both file‑path ... |
| [compare-memory-usage-file-path-stream-pdf-concaten...](./compare-memory-usage-file-path-stream-pdf-concatenation.cs) | Compare Memory Usage of File-Path vs Stream Overloads for PD... | `Document`, `Page`, `TextFragment` | Creates two simple PDF files and concatenates them using Aspose.Pdf.Facades.PdfFileEditor, measur... |
| [concatenate-multiple-pdfs-measure-time](./concatenate-multiple-pdfs-measure-time.cs) | Concatenate Multiple PDFs and Measure Execution Time | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates how to concatenate fifty small PDF files using Aspose.Pdf.Facades.PdfFileEditor stre... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge several PDF files into one using Aspose.Pdf.Facades.PdfFileEditor. The ... |
| [concatenate-multiple-pdfs__v2](./concatenate-multiple-pdfs__v2.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge several PDF files into one PDF using Aspose.Pdf.Facades.PdfFileEditor in a con... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Shows how to merge multiple PDF files into a single document and automatically insert page number... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to a File | `Document`, `PdfFileEditor`, `Concatenate` | Shows how to merge multiple PDF documents held in memory streams into a single PDF file using Asp... |
| [concatenate-pdfs-from-zip](./concatenate-pdfs-from-zip.cs) | Concatenate PDFs from a ZIP Archive and Save Back | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | The example reads all PDF files stored in a ZIP archive, merges them into a single PDF using Aspo... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs and Preserve Original Metadata | `PdfFileEditor`, `Concatenate`, `PdfFileInfo` | Demonstrates how to merge multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor while copying... |
| [concatenate-pdfs-using-streams](./concatenate-pdfs-using-streams.cs) | Concatenate PDFs Using Streams | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge multiple PDF files into a single PDF by passing input and output streams to As... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Page Separator | `Document`, `PdfFileEditor`, `Add` | Demonstrates how to merge multiple PDF files using Aspose.Pdf while automatically inserting a bla... |
| [concatenate-pdfs-with-blank-separator](./concatenate-pdfs-with-blank-separator.cs) | Concatenate PDFs with Blank Separator Pages | `Document`, `PdfFileEditor`, `Add` | Shows how to create a one‑page blank PDF and insert it between multiple PDF documents while conca... |
| [concatenate-split-pdfs](./concatenate-split-pdfs.cs) | Concatenate Split PDFs into a Single Document | `PdfFileEditor`, `Insert`, `Document` | Shows how to merge a series of split PDF files into one PDF by iteratively inserting pages using ... |
| [concatenate-three-pdfs-pdffileeditor](./concatenate-three-pdfs-pdffileeditor.cs) | Concatenate Three PDFs with PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates merging three PDF files into a single document using Aspose.Pdf.Facades.PdfFileEdito... |
| [concatenate-two-pdfs-using-pdffileeditor](./concatenate-two-pdfs-using-pdffileeditor.cs) | Concatenate Two PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge two existing PDF files into a single PDF using Aspose.Pdf's PdfFileEdit... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2-up PDF Layout Using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to combine two pages side‑by‑side into a... |
| [create-a-function-that-generates-a-booklet-using-l...](./create-a-function-that-generates-a-booklet-using-left-pages-from-the-first-half-of-the-source-pdf.cs) | Create A Function That Generates A Booklet Using Left Pages ... | `PdfFileEditor` | Create A Function That Generates A Booklet Using Left Pages From The First Half Of The Source Pdf |
| [create-booklet-a5-page-size](./create-booklet-a5-page-size.cs) | Create Booklet with Custom A5 Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet PDF with A5 page dimensions from an existing PDF using Asp... |
| [create-booklet-custom-page-order](./create-booklet-custom-page-order.cs) | Create Booklet with Custom Left/Right Page Order | `Document`, `Page`, `TextFragment` | Demonstrates how to generate a simple PDF, define custom left and right page arrays, and use PdfF... |
| [create-booklet-from-second-half-right-pages](./create-booklet-from-second-half-right-pages.cs) | Create Booklet from Second Half Right Pages | `Document`, `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet PDF using only the right‑hand (odd) pages from the second half of... |
| ... | | | *and 71 more files* |

## Category Statistics
- Total examples: 101

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

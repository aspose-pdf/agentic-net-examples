---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

> **Facades documents** in PDF using C# / .NET -- **93** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (67/93 files) ← category-specific
- `using Aspose.Pdf.Facades;` (66/93 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/93 files)
- `using System;` (93/93 files)
- `using System.IO;` (93/93 files)
- `using System.Collections.Generic;` (16/93 files)
- `using System.Linq;` (9/93 files)
- `using NUnit.Framework;` (2/93 files)
- `using System.Diagnostics;` (2/93 files)
- `using System.Threading.Tasks;` (2/93 files)
- `using System.IO.Compression;` (1/93 files)
- `using System.Net.Http;` (1/93 files)

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
| [append-pages-from-multiple-pdfs](./append-pages-from-multiple-pdfs.cs) | Insert Pages from Multiple PDFs Using Ranges | `Document`, `Save`, `Add` | Demonstrates how to merge pages from several source PDFs into a single document by specifying pag... |
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another | `Document`, `Pages`, `Insert` | Demonstrates how to load two PDF files with Aspose.Pdf, append all pages from a source document t... |
| [audit-log-pdf-page-deletion](./audit-log-pdf-page-deletion.cs) | Audit Log Pdf Page Deletion | `PdfFileEditor` | Audit Log Pdf Page Deletion |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to gather all PDF files from a directory and merge them into a single PDF using Aspose.... |
| [batch-delete-pages-from-multiple-pdfs](./batch-delete-pages-from-multiple-pdfs.cs) | Batch Delete Specific Pages from PDFs | `Document`, `Pages`, `Delete` | Shows how to iterate over PDF files in a directory, delete selected pages from each document usin... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `Document`, `PdfFileEditor`, `Pages` | Demonstrates how to remove specified pages from several PDF files and then concatenate the cleane... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `Document`, `Pages`, `Insert` | Demonstrates merging specific page ranges from several source PDF files into a single destination... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 | `Document`, `Page`, `PageSize` | Shows how to iterate over PDF files in a folder, resize each page to A4 size using Aspose.Pdf, an... |
| [batch-resize-pdfs-to-a5-and-create-booklets](./batch-resize-pdfs-to-a5-and-create-booklets.cs) | Resize PDFs to A5 and Create Booklet | `Document`, `SetPageSize`, `PageSize` | Shows how to batch‑process PDF files by resizing each page to A5 size and then generating a bookl... |
| [compare-pdf-concatenation-paths-streams](./compare-pdf-concatenation-paths-streams.cs) | Compare PDF Concatenation via File Path and Stream Overloads | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to merge multiple PDFs using Aspose.Pdf's PdfFileEditor with both file‑path and stream ... |
| [concatenate-multiple-pdfs-measure-time](./concatenate-multiple-pdfs-measure-time.cs) | Concatenate Multiple PDFs and Measure Execution Time | `Document`, `PdfFileEditor`, `Concatenate` | Demonstrates how to create several small PDFs in memory, concatenate them using Aspose.Pdf's stre... |
| [concatenate-multiple-pdfs-with-logging](./concatenate-multiple-pdfs-with-logging.cs) | Merge Multiple PDFs with Step-by-Step Logging | `PdfFileEditor`, `Concatenate` | Shows how to sequentially concatenate several PDF files using Aspose.Pdf.Facades.PdfFileEditor wh... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Merge Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Shows how to concatenate several PDF files into one PDF using Aspose.Pdf.Facades.PdfFileEditor. |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `Concatenate`, `Document`, `Page` | Shows how to merge several PDF files into a single document and automatically insert sequential p... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to File | `PdfFileEditor`, `Concatenate` | Shows how to load PDF files into memory streams and merge them directly into an output file using... |
| [concatenate-pdfs-in-zip](./concatenate-pdfs-in-zip.cs) | Concatenate PDFs from a ZIP Archive and Save Merged PDF Back... | `PdfFileEditor`, `Concatenate` | The example extracts all PDF files from a ZIP archive, merges them into a single PDF using Aspose... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs and Preserve Metadata | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to merge multiple PDF files using PdfFileEditor and copy the original document metadata... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Streams | `PdfFileEditor`, `Concatenate` | Shows how to merge multiple PDF files into a single document by opening each file as a stream and... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages Between Documents | `Document`, `Add`, `Save` | Shows how to merge multiple PDF files and automatically insert a blank page between each document... |
| [concatenate-split-pdfs](./concatenate-split-pdfs.cs) | Concatenate Split PDFs into a Single Document | `Document`, `Pages`, `Insert` | Shows how to merge multiple PDF files (e.g., split pages) into one PDF by inserting each source d... |
| [concatenate-three-pdfs-into-one](./concatenate-three-pdfs-into-one.cs) | Concatenate Multiple PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates merging three PDF files into a single document using Aspose.Pdf.Facades.PdfFileEdito... |
| [concatenate-two-pdfs-using-pdffileeditor](./concatenate-two-pdfs-using-pdffileeditor.cs) | Merge Two PDFs Using PdfFileEditor Concatenate | `PdfFileEditor`, `Concatenate` | Demonstrates how to combine two PDF files on disk into a single PDF using Aspose.Pdf.Facades.PdfF... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to generate a 2‑up (two pages per sheet) PDF using Aspose.Pdf.Facades.PdfFileEdi... |
| [create-4up-pdf-with-pdffileeditor](./create-4up-pdf-with-pdffileeditor.cs) | Create 4‑up PDF Using Stream Overload | `PdfFileEditor`, `MakeNUp` | Demonstrates applying a 4‑up (N‑up) layout to an existing PDF by using Aspose.Pdf.Facades.PdfFile... |
| [create-a5-booklet-from-pdf](./create-a5-booklet-from-pdf.cs) | Create Booklet PDF with A5 Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet from an existing PDF using Aspose.Pdf.Facades.PdfFileEdito... |
| [create-booklet-from-second-half](./create-booklet-from-second-half.cs) | Generate Booklet from Second Half Right Pages | `Document`, `PdfFileEditor`, `Extract` | Demonstrates extracting the second half of a PDF, selecting only the odd‑numbered (right‑hand) pa... |
| [create-booklet-pdf-custom-page-order](./create-booklet-pdf-custom-page-order.cs) | Create Booklet PDF with Default Page Size | `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet PDF from an existing document using Aspose.Pdf.Facades.Pdf... |
| [create-booklet-pdf-custom-page-size](./create-booklet-pdf-custom-page-size.cs) | Create Booklet PDF with Custom Page Size from Stream | `PdfFileEditor`, `MakeBooklet`, `Document` | Demonstrates how to generate a booklet from a PDF stream, set each page to a custom 5.5×8.5‑inch ... |
| [create-booklet-pdf-delete-resize](./create-booklet-pdf-delete-resize.cs) | Create Booklet PDF with Page Deletion and Resize | `Document`, `SetPageSize`, `Delete` | Demonstrates loading a PDF from a stream, deleting a specified page range, resizing all pages, an... |
| [create-booklet-pdf-left-odd-pages](./create-booklet-pdf-left-odd-pages.cs) | Create Booklet PDF with Left Pages Odd Layout | `Document`, `PdfFileEditor`, `BookletOptions` | Shows how to use Aspose.Pdf's PdfFileEditor to generate a booklet PDF where left pages are odd-nu... |
| ... | | | *and 63 more files* |

## Category Statistics
- Total examples: 93

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
Updated: 2026-09-30 | Run: `20260930_043014_89dd90`
<!-- AUTOGENERATED:END -->

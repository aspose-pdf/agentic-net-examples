---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

> **Facades documents** in PDF using C# / .NET -- **101** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (98/101 files) ← category-specific
- `using Aspose.Pdf;` (37/101 files)
- `using Aspose.Pdf.Text;` (8/101 files)
- `using Aspose.Pdf.Devices;` (1/101 files)
- `using Aspose.Pdf.Drawing;` (1/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (99/101 files)
- `using System.Collections.Generic;` (11/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using System.Linq;` (3/101 files)
- `using NUnit.Framework;` (2/101 files)
- `using System.Threading.Tasks;` (2/101 files)
- `using System.Globalization;` (1/101 files)
- `using System.IO.Compression;` (1/101 files)
- `using System.Net.Http;` (1/101 files)

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
| [append-pages-from-multiple-pdfs](./append-pages-from-multiple-pdfs.cs) | Append Pages from Multiple PDFs to a Base PDF | `PdfFileEditor`, `TryAppend`, `LastException` | Demonstrates inserting a specific page range from several source PDFs into a base PDF in a single... |
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another | `Document`, `PdfFileEditor`, `Append` | Shows how to append all pages of a source PDF to the end of a destination PDF using Aspose.Pdf's ... |
| [audit-log-pdf-page-deletion](./audit-log-pdf-page-deletion.cs) | Audit Log for PDF Page Deletion | `Document`, `PdfFileEditor`, `TryDelete` | Demonstrates how to delete specific pages from a PDF using Aspose.Pdf's facade API and log the nu... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to gather all PDF files from a directory and merge them into a single PDF using Aspose.... |
| [batch-create-nup-pdfs-3x2](./batch-create-nup-pdfs-3x2.cs) | Batch Create N‑up PDFs with 3 Columns and 2 Rows | `PdfFileEditor`, `MakeNUp` | Shows how to iterate over a list of PDF files and generate N‑up versions using a 3‑column by 2‑ro... |
| [batch-delete-pages-from-multiple-pdfs](./batch-delete-pages-from-multiple-pdfs.cs) | Batch Delete Pages from Multiple PDFs | `PdfFileEditor`, `Delete` | Demonstrates how to loop through PDF files in a folder and delete specific pages from each using ... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specific pages from multiple PDF files and then concatenate the cleaned PDFs ... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `PdfFileEditor`, `TryInsert` | Shows how to insert selected pages from several source PDFs into a single destination PDF in a lo... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 | `PdfPageEditor`, `BindPdf`, `PageSize` | Iterates through all PDF files in a source folder, resizes each document to A4 page size using As... |
| [batch-resize-pdfs-to-a5-and-create-booklets](./batch-resize-pdfs-to-a5-and-create-booklets.cs) | Batch Resize PDFs to A5 and Create Booklets | `PdfFileEditor`, `ResizeContents`, `MakeBooklet` | Processes each PDF in a folder, resizes its pages to A5 dimensions, and then generates a booklet ... |
| [compare-pdf-concatenation-paths-streams](./compare-pdf-concatenation-paths-streams.cs) | Compare PDF Concatenation via File Paths and Streams | `PdfFileEditor`, `Concatenate(string, string, string)`, `Concatenate(Stream, Stream, Stream)` | Demonstrates how to concatenate two PDF files using Aspose.Pdf.Facades.PdfFileEditor with both fi... |
| [concatenate-multiple-pdfs-measure-time](./concatenate-multiple-pdfs-measure-time.cs) | Concatenate Multiple PDFs and Measure Execution Time | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates how to concatenate fifty small PDF files using Aspose.Pdf.Facades.PdfFileEditor stre... |
| [concatenate-multiple-pdfs-with-logging](./concatenate-multiple-pdfs-with-logging.cs) | Concatenate Multiple PDFs with Logging | `PdfFileEditor`, `Concatenate`, `CopyLogicalStructure` | Demonstrates how to merge several PDF files into a single document using Aspose.Pdf.Facades.PdfFi... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge several PDF files into a single document using Aspose.Pdf's PdfFileEditor facade. |
| [concatenate-multiple-pdfs__v2](./concatenate-multiple-pdfs__v2.cs) | Concatenate Multiple PDFs Using Aspose.Pdf | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates reading PDF files as streams, merging them with Aspose.Pdf.Facades.PdfFileEditor, an... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Shows how to merge multiple PDF files into a single document and automatically insert page number... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to File | `Document`, `Page`, `TextFragment` | Creates PDF documents in memory, concatenates them using Aspose.Pdf.Facades.PdfFileEditor, and wr... |
| [concatenate-pdfs-in-zip](./concatenate-pdfs-in-zip.cs) | Concatenate PDFs Inside a Zip Archive | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to read PDF files from a zip archive, merge them with Aspose.Pdf.Facades.PdfFileEditor,... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs While Preserving Original Metadata | `PdfFileInfo`, `PdfFileEditor`, `Concatenate` | Demonstrates how to merge multiple PDF files using PdfFileEditor and then copy the author, title,... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge multiple PDF files passed as command‑line arguments into a single PDF using As... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages | `Document`, `Add`, `Save` | Shows how to merge multiple PDF files and automatically insert a blank page between each document... |
| [concatenate-split-pdfs](./concatenate-split-pdfs.cs) | Concatenate Split PDFs into a Single Document | `PdfFileEditor`, `Insert`, `Document` | Shows how to merge a series of split PDF files into one PDF using Aspose.Pdf's PdfFileEditor.Inse... |
| [concatenate-three-pdfs-into-one](./concatenate-three-pdfs-into-one.cs) | Concatenate Three PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge three PDF files into one using Aspose.Pdf.Facades.PdfFileEditor and the... |
| [concatenate-two-pdfs-using-pdffileeditor](./concatenate-two-pdfs-using-pdffileeditor.cs) | Concatenate Two PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to merge two PDF files into a single document using the two‑file overload of PdfFileEdi... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2-up PDF Layout with Aspose PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to combine two PDF pages onto a single p... |
| [create-4up-pdf-with-pdffileeditor](./create-4up-pdf-with-pdffileeditor.cs) | Create 4‑up PDF using PdfFileEditor and MemoryStream | `PdfFileEditor`, `TryMakeNUp` | Demonstrates how to apply a 4‑up (2×2) layout to an existing PDF by using the PdfFileEditor.TryMa... |
| [create-a5-booklet-from-pdf](./create-a5-booklet-from-pdf.cs) | Create A5 Booklet from PDF | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet with A5 page size from an existing PDF using Aspose.Pdf's ... |
| [create-booklet-custom-page-order](./create-booklet-custom-page-order.cs) | Create Booklet with Custom Left/Right Page Order | `Document`, `TextFragment`, `TextAbsorber` | Demonstrates adding visible page numbers, defining custom left and right page arrays, and using A... |
| [create-booklet-from-second-half](./create-booklet-from-second-half.cs) | Create Booklet from Second Half of PDF | `Document`, `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet PDF where the left pages come from the first half of a sou... |
| [create-booklet-pdf-custom-page-order](./create-booklet-pdf-custom-page-order.cs) | Create Booklet PDF with Custom Page Order | `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet PDF by specifying left‑hand and right‑hand page sequences ... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

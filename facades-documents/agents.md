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

- `using Aspose.Pdf.Facades;` (96/101 files) ← category-specific
- `using Aspose.Pdf;` (38/101 files)
- `using Aspose.Pdf.Text;` (1/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (99/101 files)
- `using System.Collections.Generic;` (11/101 files)
- `using System.Linq;` (6/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using System.Threading.Tasks;` (2/101 files)
- `using Microsoft.VisualStudio.TestTools.UnitTesting;` (1/101 files)
- `using NUnit.Framework;` (1/101 files)
- `using System.IO.Compression;` (1/101 files)
- `using System.Net.Http;` (1/101 files)
- `using System.Runtime.InteropServices;` (1/101 files)

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
| [add-blank-separator-pages-when-merging-pdfs](./add-blank-separator-pages-when-merging-pdfs.cs) | Add Blank Separator Pages When Merging PDFs | `Document`, `Add`, `Save` | Shows how to insert a temporary blank page between PDF files and concatenate them into a single d... |
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another | `Document`, `PdfFileEditor`, `Append` | Shows how to append all pages of a source PDF to the end of a destination PDF using Aspose.Pdf's ... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to gather all PDF files from a directory and merge them into a single PDF using Aspose.... |
| [batch-delete-pages-from-multiple-pdfs](./batch-delete-pages-from-multiple-pdfs.cs) | Batch Delete Pages from Multiple PDFs | `PdfFileEditor`, `Delete` | Demonstrates how to iterate over PDF files in a folder and delete specified pages from each docum... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specified pages from several PDF files and then concatenate the cleaned PDFs ... |
| [batch-insert-page-ranges-into-pdf](./batch-insert-page-ranges-into-pdf.cs) | Batch Insert Page Ranges into PDF | `PdfFileEditor`, `Insert`, `Document` | Demonstrates how to sequentially insert specific page ranges from multiple source PDFs into a bas... |
| [batch-nup-pdf-3x2](./batch-nup-pdf-3x2.cs) | Batch Create 3‑by‑2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Demonstrates how to process multiple PDF files and generate N‑up versions with three columns and ... |
| [batch-resize-pdf-a5-booklet](./batch-resize-pdf-a5-booklet.cs) | Batch Resize PDFs to A5 and Create Booklet | `Document`, `PdfPageEditor`, `ApplyChanges` | Shows how to iterate over PDFs in a folder, resize each document to A5 using PdfPageEditor, and t... |
| [batch-resize-pdfs-concatenate](./batch-resize-pdfs-concatenate.cs) | Batch Resize PDFs and Concatenate into One Document | `Document`, `PdfFileEditor`, `ResizeContents` | Shows how to resize multiple PDF files to 1024 × 768 points using Aspose.Pdf.Facades.PdfFileEdito... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `ApplyChanges` | Shows how to loop through all PDF files in a folder and resize each document to A4 page size usin... |
| [compare-pdf-concatenate-memory-usage](./compare-pdf-concatenate-memory-usage.cs) | Memory Comparison of PdfFileEditor Concatenate Overloads | `PdfFileEditor`, `Concatenate(string,string,string)`, `Concatenate(Stream,Stream,Stream)` | Demonstrates how to concatenate PDFs using both file‑path and stream overloads of PdfFileEditor a... |
| [concatenate-multiple-pdfs-pdffileeditor](./concatenate-multiple-pdfs-pdffileeditor.cs) | Concatenate Multiple PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge three PDF files into a single document by using the three‑argument over... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate`, `LastException` | Demonstrates how to merge several PDF files into one using Aspose.Pdf's PdfFileEditor class from ... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Shows how to merge multiple PDF files into a single document and automatically insert page number... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to File | `PdfFileEditor`, `TryConcatenate`, `CloseConcatenatedStreams` | Demonstrates merging multiple PDF documents held in memory streams into a single PDF file directl... |
| [concatenate-pdfs-in-zip](./concatenate-pdfs-in-zip.cs) | Concatenate PDFs Inside a ZIP Archive | `PdfFileEditor`, `Concatenate` | Shows how to read PDF files from a zip archive, merge them using Aspose.Pdf.Facades, and write th... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs and Preserve Metadata with PdfFileEditor | `PdfFileEditor`, `Concatenate`, `PdfFileInfo` | Demonstrates how to merge multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor while keeping... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates how to merge multiple PDF files into a single document using Aspose.Pdf.Facades.PdfF... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages | `Document`, `Add`, `Save` | Shows how to merge multiple PDF files into a single document while automatically inserting a blan... |
| [concatenate-two-pdfs-using-pdffileeditor](./concatenate-two-pdfs-using-pdffileeditor.cs) | Concatenate Two PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Shows how to merge two PDF files on disk into a single PDF using Aspose.Pdf.Facades PdfFileEditor... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to generate a 2‑up (two pages per sheet) PDF using Aspose.Pdf's PdfFileEditor fa... |
| [create-4up-pdf-in-memory](./create-4up-pdf-in-memory.cs) | Create 4‑up PDF in Memory Using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates applying a 4‑up (2 × 2) layout to an existing PDF with Aspose.Pdf.Facades.PdfFileEdi... |
| [create-booklet-a5-pdf](./create-booklet-a5-pdf.cs) | Create Booklet PDF with Custom A5 Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates generating a booklet from an existing PDF using Aspose.Pdf.Facades.PdfFileEditor wit... |
| [create-booklet-from-second-half-right-pages](./create-booklet-from-second-half-right-pages.cs) | Create Booklet from Second Half Right Pages | `Document`, `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet PDF that includes only the right‑hand pages from the secon... |
| [create-booklet-pdf-custom-page-size](./create-booklet-pdf-custom-page-size.cs) | Create Booklet PDF with Custom Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Shows how to generate a booklet PDF with a 5.5×8.5‑inch custom page size from an input PDF stream... |
| [create-booklet-pdf-delete-resize](./create-booklet-pdf-delete-resize.cs) | Create Booklet PDF by Deleting Pages and Resizing Content | `PdfFileEditor` | Demonstrates how to take a PDF stream, remove specified pages, resize the remaining page contents... |
| [create-booklet-pdf-from-stream](./create-booklet-pdf-from-stream.cs) | Create Booklet PDF from Stream | `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet PDF using Aspose.Pdf.Facades.PdfFileEditor directly from an input... |
| [create-booklet-pdf-interleaving-pages](./create-booklet-pdf-interleaving-pages.cs) | Create Booklet PDF by Interleaving Specified Pages | `Document`, `PageInfo`, `Pages` | Demonstrates how to generate a booklet PDF by selecting left and right page ranges from an existi... |
| [create-booklet-pdf-left-odd-pages](./create-booklet-pdf-left-odd-pages.cs) | Create Booklet PDF with Left Pages as Odd Numbers | `PdfFileEditor`, `MakeBooklet` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to generate a booklet from a PDF stream, spec... |
| [create-booklet-pdf-left-pages](./create-booklet-pdf-left-pages.cs) | Create Booklet PDF Using Left Pages of First Half | `Document`, `Pages`, `PdfFileEditor` | Shows how to generate a booklet PDF by selecting the left pages from the first half of a source P... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-06 | Run: `20260506_111059_e831db`
<!-- AUTOGENERATED:END -->

---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

> **Facades documents** in PDF using C# / .NET -- **101** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (68/101 files) ← category-specific
- `using Aspose.Pdf.Facades;` (68/101 files) ← category-specific
- `using Aspose.Pdf.Text;` (6/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (101/101 files)
- `using System.Collections.Generic;` (23/101 files)
- `using System.Linq;` (9/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using System.Threading.Tasks;` (2/101 files)
- `using NUnit.Framework;` (1/101 files)
- `using System.IO.Compression;` (1/101 files)
- `using System.Reflection;` (1/101 files)

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
| [append-pages-from-multiple-pdfs](./append-pages-from-multiple-pdfs.cs) | Insert Selected Pages from Multiple PDFs into a Single Docum... | `Document`, `Pages`, `Add` | Shows how to merge specific page ranges from several source PDF files into one PDF using Aspose.Pdf. |
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another Using File Path Inserti... | `Document`, `PdfFileEditor`, `Insert` | Shows how to append all pages of a source PDF to the end of a destination PDF by using Aspose.Pdf... |
| [audit-log-pdf-page-deletion](./audit-log-pdf-page-deletion.cs) | Delete PDF Pages with Audit Logging | `PdfFileEditor`, `Delete`, `Document` | Demonstrates removing specific pages from a PDF using Aspose.Pdf.Facades.PdfFileEditor while logg... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to gather all PDF files from a directory and merge them into a single PDF using Aspose.... |
| [batch-create-nup-pdfs-3x2](./batch-create-nup-pdfs-3x2.cs) | Batch Create 3‑by‑2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Processes a list of PDF files and uses Aspose.Pdf.Facades.PdfFileEditor.MakeNUp to generate N‑up ... |
| [batch-delete-pages-from-multiple-pdfs](./batch-delete-pages-from-multiple-pdfs.cs) | Batch Delete Pages from PDFs in a Directory | `Document`, `Pages`, `Delete` | Demonstrates how to iterate over PDF files in a folder and remove specified pages from each docum... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `Document`, `PdfFileEditor`, `Delete` | Demonstrates how to remove specific pages from multiple PDF files, save the cleaned versions temp... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `Document`, `Pages`, `PageCollection` | Demonstrates how to merge specific page ranges from several source PDF files into a single destin... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 Size | `Document`, `Page`, `PageInfo` | Shows how to iterate over all PDF files in a source folder, resize each page to A4 dimensions usi... |
| [batch-resize-pdfs-to-a5-and-create-booklets](./batch-resize-pdfs-to-a5-and-create-booklets.cs) | Resize PDFs to A5 and Create Booklet | `Document`, `Page`, `PageInfo` | Shows how to batch‑process PDF files by resizing each page to A5 dimensions and then generating a... |
| [compare-pdf-concatenation-paths-streams](./compare-pdf-concatenation-paths-streams.cs) | Compare Memory Usage of PdfFileEditor Concatenate Overloads | `PdfFileEditor`, `Concatenate(String[],String)`, `Concatenate(Stream[],Stream)` | Merges multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor via the file‑path overload and t... |
| [concatenate-multiple-pdfs-measure-time](./concatenate-multiple-pdfs-measure-time.cs) | Measure PDF Concatenation Performance Using Streams | `Document`, `Page`, `TextFragment` | Demonstrates how to concatenate fifty small PDFs in memory using Aspose.Pdf stream overloads and ... |
| [concatenate-multiple-pdfs-with-logging](./concatenate-multiple-pdfs-with-logging.cs) | Merge Multiple PDFs with Logging Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates how to concatenate several PDF files into a single document while logging each merge... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Shows how to merge several PDF files into one output file using Aspose.Pdf's PdfFileEditor class. |
| [concatenate-multiple-pdfs__v2](./concatenate-multiple-pdfs__v2.cs) | Concatenate Multiple PDFs Using Aspose.Pdf | `PdfFileEditor`, `Concatenate` | Demonstrates merging several PDF files into a single document with Aspose.Pdf's PdfFileEditor in ... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `Document` | Demonstrates how to merge multiple PDF files into a single document using Aspose.Pdf.Facades.PdfF... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to File | `PdfFileEditor`, `Concatenate` | Shows how to merge multiple PDF documents that are loaded into memory streams directly into an ou... |
| [concatenate-pdfs-in-zip](./concatenate-pdfs-in-zip.cs) | Merge PDFs from a ZIP Archive and Save Back to ZIP | `PdfFileEditor`, `Concatenate` | Extracts all PDF files from a ZIP archive, concatenates them using Aspose.Pdf.Facades, and create... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs and Preserve Metadata with PdfFileEditor | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to merge multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor and retain the origi... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Streams | `PdfFileEditor`, `Concatenate` | Shows how to merge multiple PDF files passed as command‑line arguments into a single PDF using As... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Page Separator | `Document`, `Add`, `Save` | Shows how to merge multiple PDF files into one document and automatically insert a blank page bet... |
| [concatenate-split-pdfs](./concatenate-split-pdfs.cs) | Merge Split PDF Files into a Single Document | `Document`, `Page`, `Pages` | The example loads PDF files from a folder, appends each page to a base document using Aspose.Pdf,... |
| [concatenate-three-pdfs-into-one](./concatenate-three-pdfs-into-one.cs) | Concatenate Three PDFs with PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates merging three PDF files into a single document using Aspose.Pdf.Facades.PdfFileEdito... |
| [concatenate-two-pdfs-using-pdffileeditor](./concatenate-two-pdfs-using-pdffileeditor.cs) | Merge Two PDFs into a Single Document | `Document`, `Pages`, `Add` | Demonstrates how to combine two existing PDF files on disk into one PDF using Aspose.Pdf's two‑fi... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to arrange two PDF pages on a single sheet (2... |
| [create-4up-pdf-with-pdffileeditor](./create-4up-pdf-with-pdffileeditor.cs) | Create 4Up Pdf With Pdffileeditor | `PdfFileEditor` | Create 4Up Pdf With Pdffileeditor |
| [create-a5-booklet-from-pdf](./create-a5-booklet-from-pdf.cs) | Create Booklet with Custom A5 Page Size | `PdfFileEditor`, `BookletSettings`, `CreateBooklet` | Demonstrates generating a booklet PDF using Aspose.Pdf with A5 page dimensions by configuring Boo... |
| [create-booklet-custom-page-order](./create-booklet-custom-page-order.cs) | Create Booklet PDF with Custom Left/Right Page Order | `Document`, `Page`, `TextFragment` | Demonstrates how to generate a simple PDF, define custom left and right page sequences, and use A... |
| [create-booklet-from-second-half](./create-booklet-from-second-half.cs) | Generate Booklet with Right Pages from Second Half of PDF | `Document`, `PageCollection`, `PdfFileEditor` | Demonstrates how to create a booklet PDF that contains only the odd‑numbered (right) pages from t... |
| [create-booklet-pdf-custom-page-order](./create-booklet-pdf-custom-page-order.cs) | Create PDF Booklet by Interleaving Two PDFs | `Document`, `Pages`, `Insert` | Demonstrates how to combine two PDF files—left pages 1‑10 and right pages 11‑20—into a single boo... |
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
Updated: 2026-11-06 | Run: `20261106_081110_59ce08`
<!-- AUTOGENERATED:END -->

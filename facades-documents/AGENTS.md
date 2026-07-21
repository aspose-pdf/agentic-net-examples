---
name: facades-documents
description: C# examples for facades-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-documents

> **Facades documents** in PDF using C# / .NET -- **101** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-documents** category.
This folder contains standalone C# examples for facades-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (97/101 files) ← category-specific
- `using Aspose.Pdf;` (37/101 files)
- `using Aspose.Pdf.Text;` (6/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (99/101 files)
- `using System.Collections.Generic;` (16/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using System.Threading.Tasks;` (3/101 files)
- `using NUnit.Framework;` (2/101 files)
- `using System.Linq;` (2/101 files)
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
| [append-pdf-pages-using-pdffileeditor](./append-pdf-pages-using-pdffileeditor.cs) | Append PDF Pages Using PdfFileEditor | `Document`, `Pages`, `Count` | Demonstrates how to append all pages from a source PDF to the end of a destination PDF using Aspo... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to combine all PDF files from a specified directory into a single PDF document using As... |
| [batch-create-3x2-nup-pdfs](./batch-create-3x2-nup-pdfs.cs) | Batch Create 3‑by‑2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Demonstrates how to process multiple PDF files and generate N‑up versions with three columns and ... |
| [batch-create-pdf-booklets-audit-logging](./batch-create-pdf-booklets-audit-logging.cs) | Batch Create PDF Booklets with Audit Logging | `PdfFileEditor`, `MakeBooklet`, `ConversionLog` | Demonstrates processing multiple PDF files, converting each into a booklet using Aspose.Pdf.Facad... |
| [batch-delete-pages-from-pdfs](./batch-delete-pages-from-pdfs.cs) | Batch Delete Pages from Multiple PDFs | `PdfFileEditor`, `Delete` | Demonstrates how to loop through a directory of PDF files and delete specified pages from each do... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specified pages from several PDF files and then concatenate the cleaned PDFs ... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `PdfFileEditor`, `Insert` | Shows how to iteratively insert specific page ranges from several source PDFs into a base PDF usi... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `Save` | Shows how to loop through all PDF files in a folder and resize each document to A4 page size usin... |
| [compare-pdf-concatenation-overloads](./compare-pdf-concatenation-overloads.cs) | Compare PDF Concatenation Overloads (File Path vs Stream) | `Document`, `TextFragment`, `PdfFileEditor` | Demonstrates how to concatenate two PDFs using Aspose.Pdf's file‑path and stream overloads while ... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge several PDF files into one using Aspose.Pdf.Facades.PdfFileEditor. The ... |
| [concatenate-multiple-pdfs__v2](./concatenate-multiple-pdfs__v2.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge several PDF files into one using Aspose.Pdf.Facades.PdfFileEditor with stream ... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Shows how to merge multiple PDF files into a single document and automatically insert page number... |
| [concatenate-pdfs-from-zip](./concatenate-pdfs-from-zip.cs) | Concatenate PDFs from a ZIP Archive and Update the Archive | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | The example extracts all PDF files from a ZIP archive, merges them into a single PDF using Aspose... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs While Preserving Original Metadata | `PdfFileInfo`, `PdfFileEditor`, `Concatenate` | Demonstrates how to merge multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor and then copy... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge multiple PDF files into a single document by passing file streams to Aspose.Pd... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages | `Document`, `Add`, `Save` | Shows how to merge multiple PDF files into a single document while automatically inserting a blan... |
| [concatenate-split-pdfs-insertpages](./concatenate-split-pdfs-insertpages.cs) | Concatenate Split PDFs Using InsertPages | `Document`, `Page`, `TextFragment` | Demonstrates how to merge multiple split PDF files into a single document by iteratively insertin... |
| [concatenate-two-pdfs](./concatenate-two-pdfs.cs) | Concatenate Two PDFs into a New PDF | `PdfFileEditor`, `Concatenate` | Shows how to merge two existing PDF files into a single PDF document using the Aspose.Pdf.Facades... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout with Aspose.PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 2‑up (two pages per sheet)... |
| [create-4up-pdf-using-streams](./create-4up-pdf-using-streams.cs) | Create a 4‑up PDF using PdfFileEditor and streams | `PdfFileEditor`, `MakeNUp` | Demonstrates applying a 4‑up (2 × 2) layout to a PDF via the PdfFileEditor.MakeNUp stream overloa... |
| [create-a-function-that-generates-a-booklet-using-l...](./create-a-function-that-generates-a-booklet-using-left-pages-from-the-first-half-of-the-source-pdf.cs) | Create A Function That Generates A Booklet Using Left Pages ... | `PdfFileEditor` | Create A Function That Generates A Booklet Using Left Pages From The First Half Of The Source Pdf |
| [create-booklet-a5-pdf](./create-booklet-a5-pdf.cs) | Create Booklet PDF with Custom A5 Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet from an existing PDF using the Aspose.Pdf.Facades.PdfFileE... |
| [create-booklet-from-second-half-right-pages](./create-booklet-from-second-half-right-pages.cs) | Create Booklet from Second Half Right Pages | `Document`, `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet PDF that includes only the odd‑numbered (right‑hand) pages from t... |
| [create-booklet-left-odd-pages](./create-booklet-left-odd-pages.cs) | Create Booklet PDF with Left Pages as Odd Numbers | `Document`, `PdfFileEditor`, `MakeBooklet` | Shows how to use Aspose.Pdf.Facades.PdfFileEditor.MakeBooklet to reorder pages and generate a boo... |
| [create-booklet-pdf-custom-page-size](./create-booklet-pdf-custom-page-size.cs) | Create Booklet PDF with Custom Page Size Using Streams | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet PDF from an existing document using Aspose.Pdf's PdfFileEd... |
| [create-booklet-pdf-custom-page-size__v2](./create-booklet-pdf-custom-page-size__v2.cs) | Create Booklet PDF with Custom Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet PDF from an existing PDF stream using a custom 5.5×8.5‑inc... |
| [create-booklet-pdf-from-stream](./create-booklet-pdf-from-stream.cs) | Create Booklet PDF from Stream | `PdfFileEditor`, `MakeBooklet` | Shows how to use Aspose.Pdf.Facades to convert an input PDF stream into a booklet‑formatted PDF a... |
| [create-booklet-pdf-left-right-pages](./create-booklet-pdf-left-right-pages.cs) | Create Booklet PDF with Custom Left/Right Page Order | `Document`, `Pages`, `Count` | Shows how to split a source PDF into odd (right‑hand) and even (left‑hand) page collections and g... |
| [create-booklet-pdf-using-pdffileeditor](./create-booklet-pdf-using-pdffileeditor.cs) | Create Booklet PDF Using PdfFileEditor | `PdfFileEditor`, `MakeBooklet`, `Document` | Shows how to generate a booklet PDF from existing documents with Aspose.Pdf.Facades PdfFileEditor... |
| [create-booklet-pdf-with-page-deletion](./create-booklet-pdf-with-page-deletion.cs) | Create Booklet PDF with Page Deletion and Resizing | `PdfFileEditor`, `Delete`, `ResizeContents` | Demonstrates how to delete specific pages, resize page contents, and convert a PDF into a booklet... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

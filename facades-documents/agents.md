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

- `using Aspose.Pdf.Facades;` (98/101 files) ← category-specific
- `using Aspose.Pdf;` (28/101 files)
- `using Aspose.Pdf.Text;` (3/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (100/101 files)
- `using System.Collections.Generic;` (12/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using System.Linq;` (3/101 files)
- `using NUnit.Framework;` (2/101 files)
- `using System.Threading.Tasks;` (2/101 files)
- `using System.Drawing;` (1/101 files)
- `using System.Drawing.Imaging;` (1/101 files)
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
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another | `Document`, `Count`, `PdfFileEditor` | Shows how to append all pages of a source PDF to the end of an existing destination PDF using Asp... |
| [apply-4up-nup-layout-to-pdf](./apply-4up-nup-layout-to-pdf.cs) | Apply 4‑up N‑Up Layout to PDF Using Stream Overload | `PdfFileEditor`, `MakeNUp` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to create a 2 × 2 (4‑up) page grid on a PDF b... |
| [batch-concatenate-pdfs-in-folder](./batch-concatenate-pdfs-in-folder.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to merge all PDF files located in a specific directory into a single PDF document using... |
| [batch-create-nup-pdfs-3x2](./batch-create-nup-pdfs-3x2.cs) | Batch Create N‑up PDFs with 3 Columns and 2 Rows | `PdfFileEditor`, `MakeNUp` | Shows how to iterate over a list of PDF files and generate N‑up versions (3 × 2 layout) using Asp... |
| [batch-delete-pages-from-pdfs](./batch-delete-pages-from-pdfs.cs) | Batch Delete Pages from PDFs | `PdfFileEditor`, `Delete` | Shows how to loop through PDF files in a folder and remove specified pages using Aspose.Pdf.Facad... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specified pages from multiple PDF files and then concatenate the cleaned PDFs... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `PdfFileEditor`, `Insert`, `Document` | Demonstrates how to merge specific page ranges from several source PDFs into a single destination... |
| [batch-resize-pdfs-a5-create-booklet](./batch-resize-pdfs-a5-create-booklet.cs) | Batch Resize PDFs to A5 and Create Booklet | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to iterate through a folder of PDF files, resize each document to A5 using PdfPageEdito... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 Using PdfPageEditor | `PdfPageEditor`, `BindPdf`, `PageSize` | Shows how to loop through all PDF files in a folder and resize each document to A4 page size usin... |
| [compare-pdf-concatenation-memory-usage](./compare-pdf-concatenation-memory-usage.cs) | Compare Memory Usage of PDF Concatenation (Path vs Stream) | `PdfFileEditor`, `Concatenate(string,string,string)`, `Concatenate(Stream,Stream,Stream)` | Demonstrates how to concatenate two PDF files using Aspose.Pdf's file‑path and stream overloads w... |
| [concatenate-multiple-pdfs-from-streams](./concatenate-multiple-pdfs-from-streams.cs) | Concatenate Multiple PDFs from Streams | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge several PDF files supplied as streams using Aspose.Pdf.Facades.PdfFileEditor a... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single Document | `PdfFileEditor`, `Concatenate`, `LastException` | Demonstrates how to merge several PDF files into one using Aspose.Pdf.Facades.PdfFileEditor. The ... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Shows how to merge multiple PDF files into a single document and automatically insert page number... |
| [concatenate-pdfs-from-memory-streams](./concatenate-pdfs-from-memory-streams.cs) | Concatenate PDFs from Memory Streams to a File | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates how to load PDF files into memory streams, concatenate them using Aspose.Pdf.Facades... |
| [concatenate-pdfs-in-zip](./concatenate-pdfs-in-zip.cs) | Concatenate PDFs Inside a ZIP Archive | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to read PDF files from a zip archive, merge them using Aspose.Pdf.Facades.PdfFileEditor... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs and Preserve Metadata with PdfFileEditor | `PdfFileEditor`, `Concatenate`, `CopyOutlines` | Demonstrates how to merge multiple PDF files using Aspose.Pdf's PdfFileEditor while keeping outli... |
| [concatenate-pdfs-using-stream-overloads](./concatenate-pdfs-using-stream-overloads.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Shows how to merge multiple PDF files into a single PDF by opening each input as a FileStream and... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages | `PdfFileEditor`, `Concatenate`, `Document` | Shows how to merge multiple PDF files and automatically insert a blank page between each document... |
| [concatenate-three-pdfs-into-single-document](./concatenate-three-pdfs-into-single-document.cs) | Concatenate Three PDFs into a Single Document | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge three PDF files into one using Aspose.Pdf.Facades.PdfFileEditor with th... |
| [concatenate-two-pdfs](./concatenate-two-pdfs.cs) | Concatenate Two PDFs into a Single PDF | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge two existing PDF files on disk into a new PDF using the Aspose.Pdf.Faca... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 2‑up (two pages per sheet)... |
| [create-a5-booklet-from-pdf](./create-a5-booklet-from-pdf.cs) | Create A5 Booklet from PDF | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet with A5 page size from an existing PDF using Aspose.Pdf's ... |
| [create-booklet-custom-page-order](./create-booklet-custom-page-order.cs) | Create Booklet with Custom Left/Right Page Order | `Document`, `Pages`, `PdfFileEditor` | Demonstrates how to generate a booklet PDF by specifying odd (left) and even (right) page sequenc... |
| [create-booklet-custom-page-order__v2](./create-booklet-custom-page-order__v2.cs) | Create Booklet with Custom Left and Right Page Arrays | `PdfFileEditor`, `MakeBooklet`, `Document` | Demonstrates generating a booklet PDF using Aspose.Pdf's MakeBooklet method with custom left‑ and... |
| [create-booklet-from-second-half-right-pages](./create-booklet-from-second-half-right-pages.cs) | Create Booklet from Second Half Right Pages | `Document`, `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet PDF that contains only the right‑hand (odd‑numbered) pages... |
| [create-booklet-pdf-audit-log](./create-booklet-pdf-audit-log.cs) | Create Booklet PDF with Audit Logging | `PdfFileEditor`, `MakeBooklet`, `ConversionLog` | Demonstrates generating a booklet PDF using Aspose.Pdf's PdfFileEditor and logging each step of t... |
| [create-booklet-pdf-custom-page-size](./create-booklet-pdf-custom-page-size.cs) | Create Booklet PDF with Custom Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Shows how to generate a booklet PDF with a custom 5.5 × 8.5 inch page size using Aspose.Pdf's Pdf... |
| [create-booklet-pdf-delete-pages-resize](./create-booklet-pdf-delete-pages-resize.cs) | Create Booklet PDF with Page Deletion and Content Resize | `PdfFileEditor`, `Delete`, `ResizeContents` | Shows how to remove selected pages from a PDF, resize the remaining page contents, and produce a ... |
| [create-booklet-pdf-from-first-half-pages](./create-booklet-pdf-from-first-half-pages.cs) | Create Booklet PDF from First Half Pages | `Document`, `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet PDF by selecting even (left) and odd (right) pages from the first... |
| [create-booklet-pdf-from-stream](./create-booklet-pdf-from-stream.cs) | Create Booklet PDF from Stream | `PdfFileEditor`, `MakeBooklet` | Shows how to convert an input PDF stream into a booklet PDF using Aspose.Pdf.Facades.PdfFileEdito... |
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
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_013009_d919e8`
<!-- AUTOGENERATED:END -->

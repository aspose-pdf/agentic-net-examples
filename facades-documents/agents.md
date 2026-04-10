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
- `using Aspose.Pdf;` (34/101 files)
- `using Aspose.Pdf.Text;` (4/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (100/101 files)
- `using System.Collections.Generic;` (13/101 files)
- `using System.Diagnostics;` (3/101 files)
- `using NUnit.Framework;` (2/101 files)
- `using System.Linq;` (2/101 files)
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
| [append-pages-to-pdf](./append-pages-to-pdf.cs) | Append Pages from One PDF to Another | `Document`, `PdfFileEditor`, `Append` | Shows how to use Aspose.Pdf's PdfFileEditor to append all pages of a source PDF to the end of a d... |
| [batch-booklet-creation-audit-log](./batch-booklet-creation-audit-log.cs) | Batch Booklet Creation with Audit Logging | `PdfFileEditor`, `MakeBooklet`, `ConversionLog` | Demonstrates creating a booklet PDF using Aspose.Pdf.Facades.PdfFileEditor while logging each ste... |
| [batch-concatenate-pdfs-in-folder](./batch-concatenate-pdfs-in-folder.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Shows how to merge all PDF files from a given directory into a single PDF using Aspose.Pdf's PdfF... |
| [batch-create-3x2-nup-pdfs](./batch-create-3x2-nup-pdfs.cs) | Batch Create 3‑by‑2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Demonstrates how to process all PDF files in a folder and generate N‑up versions with three colum... |
| [batch-delete-pages-from-pdfs](./batch-delete-pages-from-pdfs.cs) | Batch Delete Pages from PDFs | `PdfFileEditor`, `Delete` | Shows how to loop through a folder of PDF files and delete specified pages from each document usi... |
| [batch-delete-pages-merge-pdfs](./batch-delete-pages-merge-pdfs.cs) | Batch Delete Pages and Merge PDFs | `PdfFileEditor`, `Delete`, `Concatenate` | Shows how to remove specified pages from several PDF files and then concatenate the cleaned PDFs ... |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `Document`, `Save`, `Add` | Demonstrates how to insert specific page ranges from several source PDFs into a single destinatio... |
| [batch-resize-pdfs-a5-booklet](./batch-resize-pdfs-a5-booklet.cs) | Batch Resize PDFs to A5 and Create Booklets | `PdfFileEditor`, `ResizeContents`, `MakeBooklet` | Shows how to resize every page of PDF files to A5 dimensions and then generate a booklet for each... |
| [batch-resize-pdfs-to-a4](./batch-resize-pdfs-to-a4.cs) | Batch Resize PDFs to A4 | `Document`, `PdfPageEditor`, `PageSize` | Demonstrates how to iterate through a folder of PDF files, resize each page to A4 using Aspose.Pd... |
| [compare-pdf-concatenation-path-vs-stream](./compare-pdf-concatenation-path-vs-stream.cs) | Compare PDF Concatenation Path vs Stream Overloads | `PdfFileEditor`, `Concatenate` | Demonstrates how to concatenate two PDFs using Aspose.Pdf's file‑path and stream overloads while ... |
| [concatenate-multiple-pdfs-pdffileeditor](./concatenate-multiple-pdfs-pdffileeditor.cs) | Concatenate Multiple PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge three PDF files into a single document using Aspose.Pdf.Facades.PdfFile... |
| [concatenate-multiple-pdfs](./concatenate-multiple-pdfs.cs) | Concatenate Multiple PDFs into a Single File | `PdfFileEditor`, `CloseConcatenatedStreams`, `Concatenate` | Shows how to merge several PDF documents into one output file using Aspose.Pdf.Facades.PdfFileEdi... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `PdfFileStamp` | Demonstrates how to merge multiple PDF files into a single document and automatically insert page... |
| [concatenate-pdfs-from-memory-to-file](./concatenate-pdfs-from-memory-to-file.cs) | Concatenate PDFs from Memory Streams to File | `Document`, `Page`, `TextFragment` | Shows how to merge multiple PDF documents held in memory using Aspose.Pdf.Facades.PdfFileEditor a... |
| [concatenate-pdfs-in-zip-archive](./concatenate-pdfs-in-zip-archive.cs) | Concatenate PDFs Inside a Zip Archive | `PdfFileEditor`, `Concatenate` | Demonstrates how to read PDF files from a ZIP archive, merge them using Aspose.Pdf.Facades, and w... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs While Preserving Metadata | `PdfFileEditor`, `PdfFileInfo`, `Concatenate` | Demonstrates how to merge multiple PDF files using Aspose.Pdf.Facades.PdfFileEditor and retain th... |
| [concatenate-pdfs-using-streams](./concatenate-pdfs-using-streams.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge multiple PDF files into a single document by opening each file as a str... |
| [concatenate-pdfs-with-blank-page](./concatenate-pdfs-with-blank-page.cs) | Concatenate PDFs with Blank Page Separator | `PdfFileEditor`, `Concatenate` | Shows how to merge multiple PDF documents into one file while automatically inserting a blank pag... |
| [concatenate-two-pdfs](./concatenate-two-pdfs.cs) | Concatenate Two PDFs into a Single PDF | `PdfFileEditor`, `Concatenate` | Shows how to merge two existing PDF files into a new PDF using Aspose.Pdf.Facades.PdfFileEditor’s... |
| [create-2up-pdf-layout](./create-2up-pdf-layout.cs) | Create 2‑up PDF Layout Using MakeNup | `PdfFileEditor`, `MakeNUp` | Shows how to generate a 2‑up (two pages per sheet) PDF by calling Aspose.Pdf.Facades.PdfFileEdito... |
| [create-4up-pdf-with-pdffileeditor](./create-4up-pdf-with-pdffileeditor.cs) | Create 4‑up PDF with PdfFileEditor and Streams | `PdfFileEditor`, `MakeNUp` | Demonstrates applying a 4‑up (2 × 2) layout to a PDF using Aspose.Pdf.Facades.PdfFileEditor.MakeN... |
| [create-booklet-from-right-pages](./create-booklet-from-right-pages.cs) | Create Booklet from Right‑Hand Pages of a PDF | `Document`, `Extract`, `MakeBooklet` | Demonstrates extracting the odd-numbered pages from the second half of a PDF and then generating ... |
| [create-booklet-left-odd-pages](./create-booklet-left-odd-pages.cs) | Create Booklet PDF with Left Pages as Odd Numbers | `Document`, `MakeBooklet` | Demonstrates how to use Aspose.Pdf's PdfFileEditor to generate a booklet PDF, assigning odd‑numbe... |
| [create-booklet-pdf-a5](./create-booklet-pdf-a5.cs) | Create Booklet PDF with Custom A5 Page Size | `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet from an existing PDF using Aspose.Pdf.Facades with a custom A5 pa... |
| [create-booklet-pdf-custom-page-order](./create-booklet-pdf-custom-page-order.cs) | Create Booklet PDF with Custom Page Order | `PdfFileEditor`, `MakeBooklet` | Shows how to generate a booklet PDF by specifying left and right page sequences using Aspose.Pdf.... |
| [create-booklet-pdf-custom-page-size](./create-booklet-pdf-custom-page-size.cs) | Create Booklet PDF with Custom Page Size Using Streams | `MakeBooklet`, `Document`, `Page` | Shows how to generate a booklet from an existing PDF via streams and then apply a custom page dim... |
| [create-booklet-pdf-custom-page-size__v2](./create-booklet-pdf-custom-page-size__v2.cs) | Create Booklet PDF with Custom Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet PDF with a custom 5.5×8.5‑inch page size using Aspose.Pdf'... |
| [create-booklet-pdf-custom-page-sizes](./create-booklet-pdf-custom-page-sizes.cs) | Create Booklet PDFs with Custom Page Sizes | `Document`, `PdfFileEditor`, `MakeBooklet` | Generates sample PDFs, then uses Aspose.Pdf.Facades.PdfFileEditor.MakeBooklet to produce booklet ... |
| [create-booklet-pdf-delete-resize](./create-booklet-pdf-delete-resize.cs) | Create Booklet PDF by Deleting Pages and Resizing Content | `PdfFileEditor`, `Delete`, `ResizeContents` | Demonstrates how to delete specific pages from a PDF stream, resize the remaining pages, and conv... |
| [create-booklet-pdf-from-stream](./create-booklet-pdf-from-stream.cs) | Create Booklet PDF from Stream | `PdfFileEditor`, `MakeBooklet` | Shows how to use Aspose.Pdf.Facades to convert an input PDF stream into a booklet PDF returned as... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

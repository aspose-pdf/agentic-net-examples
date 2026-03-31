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

- `using Aspose.Pdf.Facades;` (88/100 files) ← category-specific
- `using Aspose.Pdf;` (43/100 files)
- `using Aspose.Pdf.Annotations;` (1/100 files)
- `using Aspose.Pdf.Drawing;` (1/100 files)
- `using Aspose.Pdf.Text;` (1/100 files)
- `using System;` (100/100 files)
- `using System.IO;` (100/100 files)
- `using System.Collections.Generic;` (10/100 files)
- `using System.Diagnostics;` (3/100 files)
- `using System.Threading.Tasks;` (3/100 files)
- `using Microsoft.AspNetCore.Http;` (1/100 files)
- `using Microsoft.AspNetCore.Mvc;` (1/100 files)
- `using NUnit.Framework;` (1/100 files)
- `using System.IO.Compression;` (1/100 files)
- `using System.Linq;` (1/100 files)
- `using System.Net.Http;` (1/100 files)

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
| [append-pages-pdf](./append-pages-pdf.cs) | Append Pages from Source PDF to Destination PDF | `PdfFileEditor`, `Append` | Demonstrates how to append pages from a source PDF file to the end of a destination PDF using Asp... |
| [batch-booklet-creation](./batch-booklet-creation.cs) | Batch Booklet Creation with Audit Logging | `TryMakeBooklet`, `StreamWriter` | Creates booklets from multiple PDF files using PdfFileEditor and logs each step to a text file fo... |
| [batch-concatenate-pdfs](./batch-concatenate-pdfs.cs) | Batch Concatenate PDFs in a Folder | `PdfFileEditor`, `Concatenate` | Demonstrates how to concatenate all PDF files in a specified folder into a single PDF using Aspos... |
| [batch-delete-pages](./batch-delete-pages.cs) | Batch Delete Pages from Multiple PDFs | `Delete`, `GetFiles` | Deletes specified pages from every PDF file in a directory using PdfFileEditor in a loop. |
| [batch-insert-page-ranges](./batch-insert-page-ranges.cs) | Batch Insert Page Ranges from Multiple PDFs | `PdfFileEditor`, `Insert`, `Document` | Shows how to insert specific page ranges from several source PDFs into a destination PDF in a loo... |
| [batch-nup-pdf](./batch-nup-pdf.cs) | Batch Create 3‑by‑2 N‑up PDFs | `PdfFileEditor`, `MakeNUp` | Processes a list of PDF files and creates N‑up versions with three columns and two rows per page. |
| [batch-resize-pdfs-a4](./batch-resize-pdfs-a4.cs) | Batch Resize PDFs to A4 Size | `Document`, `Page`, `SetPageSize` | Resizes all PDF files in a folder to A4 page size and saves them to a target directory. |
| [combine-two-pdfs](./combine-two-pdfs.cs) | Combine Two PDFs into One | `PdfFileEditor`, `Concatenate` | Demonstrates how to concatenate two PDF files into a single PDF using Aspose.Pdf.Facades.PdfFileE... |
| [compare-concatenation-memory](./compare-concatenation-memory.cs) | Compare Memory Usage of PdfFileEditor Concatenation Overload... | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates how to concatenate PDFs using file‑path and stream overloads of PdfFileEditor and me... |
| [concatenate-pdf-streams](./concatenate-pdf-streams.cs) | Concatenate Multiple PDF Streams via HTTP | `PdfFileEditor`, `Concatenate(Stream[],Stream)` | Demonstrates an ASP.NET Core endpoint that receives PDF files as streams, concatenates them using... |
| [concatenate-pdfs-add-page-numbers](./concatenate-pdfs-add-page-numbers.cs) | Concatenate PDFs and Add Page Numbers | `PdfFileEditor`, `Concatenate`, `Document` | Combines multiple PDF files into a single document and inserts sequential page numbers on each pa... |
| [concatenate-pdfs-from-zip](./concatenate-pdfs-from-zip.cs) | Concatenate PDFs from a ZIP Archive and Store Result Back in... | `PdfFileEditor`, `Concatenate`, `ZipArchive` | Demonstrates reading PDF files from a ZIP archive, concatenating them using Aspose.Pdf.Facades.Pd... |
| [concatenate-pdfs-memory-streams](./concatenate-pdfs-memory-streams.cs) | Concatenate PDFs from Memory Streams | `PdfFileEditor`, `Concatenate`, `CloseConcatenatedStreams` | Demonstrates concatenating two PDF files loaded into memory streams and writing the merged result... |
| [concatenate-pdfs-preserve-metadata](./concatenate-pdfs-preserve-metadata.cs) | Concatenate PDFs while preserving metadata | `PdfFileEditor`, `Concatenate`, `Document` | Demonstrates how to concatenate multiple PDF files using PdfFileEditor and retain the original au... |
| [concatenate-pdfs-stream](./concatenate-pdfs-stream.cs) | Concatenate PDFs Using Stream Overloads | `PdfFileEditor`, `Concatenate(Stream[], Stream)` | Demonstrates how to concatenate multiple PDF files supplied via command‑line arguments using Aspo... |
| [concatenate-pdfs-with-blank-pages](./concatenate-pdfs-with-blank-pages.cs) | Concatenate PDFs with Blank Pages Between | `PdfFileEditor`, `Document`, `Add` | Shows how to merge multiple PDF files inserting a blank page between each document using Aspose.Pdf. |
| [concatenate-pdfs-with-blank-separator](./concatenate-pdfs-with-blank-separator.cs) | Concatenate PDFs with Blank Separator Page | `Document`, `Concatenate` | Demonstrates how to insert a blank page between PDFs during concatenation using Aspose.Pdf. |
| [concatenate-pdfs-with-logging](./concatenate-pdfs-with-logging.cs) | Concatenate Multiple PDFs with Logging | `PdfFileEditor`, `Concatenate`, `WriteLine` | Demonstrates merging several PDF files into one while logging each concatenation step. |
| [concatenate-pdfs](./concatenate-pdfs.cs) | Concatenate Multiple PDFs into One File | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge several PDF files into a single PDF using Aspose.Pdf's PdfFileEditor. |
| [concatenate-split-pdfs-insertpages](./concatenate-split-pdfs-insertpages.cs) | Concatenate Split PDFs Using InsertPages | `Document`, `Insert`, `Save` | Demonstrates how to merge multiple split PDF files into a single PDF by inserting pages from each... |
| [concatenate-three-pdfs](./concatenate-three-pdfs.cs) | Concatenate Three PDFs Using PdfFileEditor | `PdfFileEditor`, `Concatenate` | Demonstrates how to merge three PDF files into one by chaining the three‑argument Concatenate met... |
| [create-2up-pdf](./create-2up-pdf.cs) | Create 2-up PDF Layout Using MakeNup | `PdfFileEditor`, `MakeNup` | Demonstrates how to combine two PDF files into a side-by-side 2-up layout using PdfFileEditor.Mak... |
| [create-4up-pdf-memory](./create-4up-pdf-memory.cs) | Create 4‑up PDF using stream overload and memory output | `PdfFileEditor`, `MakeNUp`, `FileStream` | Demonstrates how to apply a 4‑up grid to a PDF using PdfFileEditor.MakeNUp with stream inputs and... |
| [create-booklet-a5](./create-booklet-a5.cs) | Create Booklet PDF with Custom A5 Page Size | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Creates a booklet from an existing PDF using the A5 page size via the PdfFileEditor.MakeBooklet o... |
| [create-booklet-custom-page-size](./create-booklet-custom-page-size.cs) | Create Booklet with Custom Page Size from PDF Stream | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate a booklet PDF with a custom 5.5×8.5‑inch page size from an input PDF... |
| [create-booklet-custom-page-sizes](./create-booklet-custom-page-sizes.cs) | Create Booklet PDFs with Custom Page Sizes | `PdfFileEditor`, `MakeBooklet`, `PageSize` | Demonstrates how to generate booklet PDFs from multiple source files using PdfFileEditor with var... |
| [create-booklet-from-stream](./create-booklet-from-stream.cs) | Create Booklet from PDF Stream after Deleting Pages and Resi... | `Document`, `PdfFileEditor`, `SetPageSize` | Demonstrates how to delete a page range, resize pages, and generate a booklet PDF using streams w... |
| [create-booklet-left-pages](./create-booklet-left-pages.cs) | Create Booklet Using Custom Left Pages | `MakeBooklet`, `Document`, `Count` | Demonstrates how to generate a booklet PDF by selecting left pages from the first half of a sourc... |
| [create-booklet-odd-left-pages](./create-booklet-odd-left-pages.cs) | Create Booklet from PDF Stream with Odd Left Pages | `PdfFileEditor`, `MakeBooklet`, `Document` | Shows how to use PdfFileEditor to generate a booklet from a PDF stream, specifying only odd-numbe... |
| [create-booklet-pdf](./create-booklet-pdf.cs) | Create Booklet PDF using PdfFileEditor | `PdfFileEditor`, `MakeBooklet` | Demonstrates how to generate a booklet from an existing PDF using Aspose.Pdf's PdfFileEditor with... |
| ... | | | *and 70 more files* |

## Category Statistics
- Total examples: 100

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
Updated: 2026-03-31 | Run: `20260331_170217_1ce39e`
<!-- AUTOGENERATED:END -->

---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **81** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (80/81 files) ← category-specific
- `using Aspose.Pdf;` (26/81 files)
- `using Aspose.Pdf.Text;` (4/81 files)
- `using Aspose.Pdf.AI;` (1/81 files)
- `using Aspose.Pdf.Drawing;` (1/81 files)
- `using Aspose.Pdf.Multithreading;` (1/81 files)
- `using System;` (81/81 files)
- `using System.IO;` (81/81 files)
- `using System.Text;` (23/81 files)
- `using System.Drawing.Imaging;` (20/81 files)
- `using System.Collections.Generic;` (18/81 files)
- `using System.Threading.Tasks;` (7/81 files)
- `using System.Drawing;` (6/81 files)
- `using System.Text.Json;` (4/81 files)
- `using Azure.Storage.Blobs;` (3/81 files)
- `using System.IO.Compression;` (3/81 files)
- `using System.Threading;` (3/81 files)
- `using NUnit.Framework;` (2/81 files)
- `using System.Security.Cryptography;` (2/81 files)
- `using Azure.Storage.Blobs.Models;` (1/81 files)
- `using Google.Apis.Storage.v1.Data;` (1/81 files)
- `using Google.Cloud.Storage.V1;` (1/81 files)
- `using Microsoft.Azure.WebJobs;` (1/81 files)
- `using Microsoft.Extensions.Logging;` (1/81 files)
- `using Npgsql;` (1/81 files)
- `using System.Collections;` (1/81 files)
- `using System.Diagnostics;` (1/81 files)
- `using System.Drawing.Drawing2D;` (1/81 files)

## Common Code Pattern

Most files in this category use `PdfExtractor` from `Aspose.Pdf.Facades`:

```csharp
PdfExtractor tool = new PdfExtractor();
tool.BindPdf("input.pdf");
// ... PdfExtractor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-watermark-extract-images-from-pdf](./add-watermark-extract-images-from-pdf.cs) | Add Watermark to PDF Pages and Extract Images | `Document`, `PdfFileMend`, `BindPdf` | Demonstrates how to overlay a PNG watermark on each page of a PDF using PdfFileMend, then extract... |
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text and Image Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text and images from a PDF file asynchronously using Aspose.Pdf's Pdf... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to iterate over a folder of PDF files, extract their text with Aspose.Pdf.Facades.PdfEx... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Extract Text from PDFs with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF file in a ... |
| [cancel-pdf-image-extraction](./cancel-pdf-image-extraction.cs) | Cancel PDF Image Extraction with InterruptMonitor | `PdfExtractor`, `InterruptMonitor`, `BindPdf` | Demonstrates how to use Aspose.Pdf's InterruptMonitor and a CancellationToken to abort a PdfExtra... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to bind a PDF to Aspose.Pdf.Facades.PdfExtractor, extract its text into a MemoryStream,... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Both Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF file contains ... |
| [check-pdf-text-only-by-detecting-images](./check-pdf-text-only-by-detecting-images.cs) | Check if PDF is Text‑Only by Detecting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF contains any images... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates reading a JSON configuration to enable or disable text, image, and attachment extrac... |
| [create-a-batch-job-that-processes-pdfs-from-an-azu...](./create-a-batch-job-that-processes-pdfs-from-an-azure-blob-container-extracting-text-and-uploading-results-back.cs) | Create A Batch Job That Processes Pdfs From An Azure Blob Co... | `PdfExtractor` | Create A Batch Job That Processes Pdfs From An Azure Blob Container Extracting Text And Uploading... |
| [create-contact-sheet-pdf-from-extracted-images](./create-contact-sheet-pdf-from-extracted-images.cs) | Create Contact Sheet PDF from Extracted Images | `PdfExtractor`, `Document`, `Page` | The example extracts all images from a source PDF using PdfExtractor, then arranges them as thumb... |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary from First Three Pages Text | `PdfExtractor`, `Document`, `TextFragment` | Demonstrates extracting text from the first three pages of a PDF using PdfExtractor and generatin... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor facade to extract every image from a PDF docume... |
| [extract-attachments-compute-sha256](./extract-attachments-compute-sha256.cs) | Extract PDF Attachments and Compute SHA-256 Hashes | `Document`, `FileSpecification`, `Add` | Demonstrates creating a PDF with an embedded file, extracting all attachments using Aspose.Pdf.Fa... |
| [extract-embedded-attachments-from-pdf](./extract-embedded-attachments-from-pdf.cs) | Extract Embedded Attachments from PDF | `PdfExtractor`, `BindPdf`, `ExtractAttachment` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to retrieve embedded file attachments fro... |
| [extract-images-and-compress-png-zip](./extract-images-and-compress-png-zip.cs) | Extract Images from PDF and Compress as PNG ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting embedded images from a PDF using Aspose.Pdf.Facades.PdfExtractor, saving ... |
| [extract-images-and-create-thumbnails](./extract-images-and-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract images from a PDF using Aspose.Pdf.Facades.PdfExtractor and generate PNG thu... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from PDF Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use PdfExtractor to scan each PDF page for a specific keyword and extract only the i... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Extract Images from PDF and Build an Image Portfolio | `PdfExtractor`, `ExtractImage`, `HasNextImage` | Demonstrates how to extract all images from an existing PDF using Aspose.Pdf.Facades.PdfExtractor... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create a Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and combi... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page into Byte Arrays | `Document`, `Page`, `Image` | The example creates a minimal PDF containing an in‑memory BMP image, then uses Aspose.Pdf.Facades... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF with Password | `PdfExtractor`, `Password`, `BindPdf` | Shows how to open an encrypted PDF by supplying the user password and extract all images using As... |
| [extract-images-from-pages-png](./extract-images-from-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract images from pages 5 through 10 of a... |
| [extract-images-from-pdf-to-temp-folder](./extract-images-from-pdf-to-temp-folder.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf's PdfExtractor with the default ex... |
| [extract-images-from-pdf-to-unc](./extract-images-from-pdf-to-unc.cs) | Extract Images from PDF to UNC Network Share | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF and save them as... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF and Create ZIP Archive | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf to extract all images from a PDF document and package them int... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates binding a PDF to Aspose.Pdf.Facades.PdfExtractor, extracting all images, and saving ... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save each a... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Specific PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a single PDF page by setting the StartPage and EndPage prope... |
| [extract-images-html-gallery](./extract-images-html-gallery.cs) | Extract Images from PDF and Create HTML Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and generatin... |
| ... | | | *and 51 more files* |

## Category Statistics
- Total examples: 81

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.ExtractImageMode`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfConverter`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfExtractor.BindPdf`
- `Aspose.Pdf.Facades.PdfExtractor.ExtractText`
- `Aspose.Pdf.Facades.PdfExtractor.GetNextPageText`
- `Aspose.Pdf.Facades.PdfExtractor.HasNextPageText`
- `Aspose.Pdf.Facades.PdfFileEditor`
- `Aspose.Pdf.Facades.PdfFileEditor.Extract`

### Rules
- BindPdf({input_pdf}) must be called on a PdfContentEditor instance before any editing methods such as ReplaceText.
- ReplaceText({text_fragment}, {page}, {text_fragment}) replaces all occurrences of the first text fragment on the specified 1‑based page with the second text fragment.
- Save({output_pdf}) persists the edited PDF; it should be invoked after all edit operations are completed.
- Use PdfFileEditor.Extract({input_pdf}, new int[] {{int}, {int}, ...}, {output_pdf}) to create a new PDF containing only the listed pages.
- Page numbers supplied in the int array are 1‑based and must exist in {input_pdf}.

### Warnings
- Page numbers are 1‑based; passing 0 will cause an error.
- ReplaceText operates only on the specified page and replaces every matching occurrence on that page.
- The output file will be created or overwritten; ensure the path is correct.
- The example assumes the input PDF exists at the specified location.
- The example does not explicitly dispose the FileStream objects; callers should ensure streams are closed or wrapped in using statements.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-extract-images-and-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

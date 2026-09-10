---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **125** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (80/125 files) ← category-specific
- `using Aspose.Pdf;` (26/125 files)
- `using Aspose.Pdf.Text;` (4/125 files)
- `using Aspose.Pdf.AI;` (1/125 files)
- `using Aspose.Pdf.Drawing;` (1/125 files)
- `using Aspose.Pdf.Multithreading;` (1/125 files)
- `using System;` (81/125 files)
- `using System.IO;` (81/125 files)
- `using System.Text;` (23/125 files)
- `using System.Drawing.Imaging;` (20/125 files)
- `using System.Collections.Generic;` (18/125 files)
- `using System.Threading.Tasks;` (7/125 files)
- `using System.Drawing;` (6/125 files)
- `using System.Text.Json;` (4/125 files)
- `using Azure.Storage.Blobs;` (3/125 files)
- `using System.IO.Compression;` (3/125 files)
- `using System.Threading;` (3/125 files)
- `using NUnit.Framework;` (2/125 files)
- `using System.Security.Cryptography;` (2/125 files)
- `using Azure.Storage.Blobs.Models;` (1/125 files)
- `using Google.Apis.Storage.v1.Data;` (1/125 files)
- `using Google.Cloud.Storage.V1;` (1/125 files)
- `using Microsoft.Azure.WebJobs;` (1/125 files)
- `using Microsoft.Extensions.Logging;` (1/125 files)
- `using Npgsql;` (1/125 files)
- `using System.Collections;` (1/125 files)
- `using System.Diagnostics;` (1/125 files)
- `using System.Drawing.Drawing2D;` (1/125 files)

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
| [async-extract-text-and-images-from-pdf](./async-extract-text-and-images-from-pdf.cs) | Async extract text and images from pdf |  | Async extract text and images from pdf |
| [async-pdf-text-extraction-with-cancellation](./async-pdf-text-extraction-with-cancellation.cs) | Async pdf text extraction with cancellation |  | Async pdf text extraction with cancellation |
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text and Image Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text and images from a PDF file asynchronously using Aspose.Pdf's Pdf... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to iterate over a folder of PDF files, extract their text with Aspose.Pdf.Facades.PdfEx... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Extract Text from PDFs with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF file in a ... |
| [batch-pdf-text-extraction-azure-blob](./batch-pdf-text-extraction-azure-blob.cs) | Batch pdf text extraction azure blob |  | Batch pdf text extraction azure blob |
| [cancel-pdf-image-extraction](./cancel-pdf-image-extraction.cs) | Cancel PDF Image Extraction with InterruptMonitor | `PdfExtractor`, `InterruptMonitor`, `BindPdf` | Demonstrates how to use Aspose.Pdf's InterruptMonitor and a CancellationToken to abort a PdfExtra... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to bind a PDF to Aspose.Pdf.Facades.PdfExtractor, extract its text into a MemoryStream,... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Both Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF file contains ... |
| [check-pdf-text-only-by-detecting-images](./check-pdf-text-only-by-detecting-images.cs) | Check if PDF is Text‑Only by Detecting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF contains any images... |
| [check-pdf-text-only-by-extracting-images](./check-pdf-text-only-by-extracting-images.cs) | Check pdf text only by extracting images |  | Check pdf text only by extracting images |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates reading a JSON configuration to enable or disable text, image, and attachment extrac... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert pdf to multi page tiff |  | Convert pdf to multi page tiff |
| [create-a-batch-job-that-processes-pdfs-from-an-azu...](./create-a-batch-job-that-processes-pdfs-from-an-azure-blob-container-extracting-text-and-uploading-results-back.cs) | Create A Batch Job That Processes Pdfs From An Azure Blob Co... | `PdfExtractor` | Create A Batch Job That Processes Pdfs From An Azure Blob Container Extracting Text And Uploading... |
| [create-contact-sheet-pdf-from-extracted-images](./create-contact-sheet-pdf-from-extracted-images.cs) | Create Contact Sheet PDF from Extracted Images | `PdfExtractor`, `Document`, `Page` | The example extracts all images from a source PDF using PdfExtractor, then arranges them as thumb... |
| [create-contact-sheet-pdf](./create-contact-sheet-pdf.cs) | Create contact sheet pdf |  | Create contact sheet pdf |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary from First Three Pages Text | `PdfExtractor`, `Document`, `TextFragment` | Demonstrates extracting text from the first three pages of a PDF using PdfExtractor and generatin... |
| [export-pdf-images-to-jpeg-quality-85](./export-pdf-images-to-jpeg-quality-85.cs) | Export pdf images to jpeg quality 85 |  | Export pdf images to jpeg quality 85 |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor facade to extract every image from a PDF docume... |
| [extract-attachments-compute-sha256](./extract-attachments-compute-sha256.cs) | Extract PDF Attachments and Compute SHA-256 Hashes | `Document`, `FileSpecification`, `Add` | Demonstrates creating a PDF with an embedded file, extracting all attachments using Aspose.Pdf.Fa... |
| [extract-embedded-attachments-from-pdf](./extract-embedded-attachments-from-pdf.cs) | Extract Embedded Attachments from PDF | `PdfExtractor`, `BindPdf`, `ExtractAttachment` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to retrieve embedded file attachments fro... |
| [extract-images-and-compress-png-zip](./extract-images-and-compress-png-zip.cs) | Extract Images from PDF and Compress as PNG ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting embedded images from a PDF using Aspose.Pdf.Facades.PdfExtractor, saving ... |
| [extract-images-and-create-thumbnails](./extract-images-and-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract images from a PDF using Aspose.Pdf.Facades.PdfExtractor and generate PNG thu... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from PDF Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use PdfExtractor to scan each PDF page for a specific keyword and extract only the i... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Extract Images from PDF and Build an Image Portfolio | `PdfExtractor`, `ExtractImage`, `HasNextImage` | Demonstrates how to extract all images from an existing PDF using Aspose.Pdf.Facades.PdfExtractor... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create a Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and combi... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page into Byte Arrays | `Document`, `Page`, `Image` | The example creates a minimal PDF containing an in‑memory BMP image, then uses Aspose.Pdf.Facades... |
| [extract-images-from-a-pdf-and-embed-them-into-an-h...](./extract-images-from-a-pdf-and-embed-them-into-an-html-report-using-base64-data-uris.cs) | Extract images from a pdf and embed them into an html report... |  | Extract images from a pdf and embed them into an html report using base64 data uris |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF with Password | `PdfExtractor`, `Password`, `BindPdf` | Shows how to open an encrypted PDF by supplying the user password and extract all images using As... |
| ... | | | *and 95 more files* |

## Category Statistics
- Total examples: 125

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

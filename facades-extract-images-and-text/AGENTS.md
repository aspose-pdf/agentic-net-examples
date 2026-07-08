---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **84** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (84/84 files) ← category-specific
- `using Aspose.Pdf;` (34/84 files)
- `using Aspose.Pdf.Text;` (7/84 files)
- `using Aspose.Pdf.AI;` (1/84 files)
- `using Aspose.Pdf.Devices;` (1/84 files)
- `using Aspose.Pdf.Drawing;` (1/84 files)
- `using Aspose.Pdf.Multithreading;` (1/84 files)
- `using System;` (84/84 files)
- `using System.IO;` (83/84 files)
- `using System.Text;` (27/84 files)
- `using System.Collections.Generic;` (17/84 files)
- `using System.Drawing.Imaging;` (17/84 files)
- `using System.Drawing;` (6/84 files)
- `using System.Threading.Tasks;` (6/84 files)
- `using System.Text.Json;` (4/84 files)
- `using System.Threading;` (3/84 files)
- `using Azure.Storage.Blobs;` (2/84 files)
- `using System.IO.Compression;` (2/84 files)
- `using System.Security.Cryptography;` (2/84 files)
- `using Amazon.S3.Model;` (1/84 files)
- `using Azure.Data.Tables;` (1/84 files)
- `using Azure.Storage.Blobs.Models;` (1/84 files)
- `using DocumentFormat.OpenXml;` (1/84 files)
- `using DocumentFormat.OpenXml.Drawing;` (1/84 files)
- `using DocumentFormat.OpenXml.Drawing.Pictures;` (1/84 files)
- `using DocumentFormat.OpenXml.Packaging;` (1/84 files)
- `using DocumentFormat.OpenXml.Wordprocessing;` (1/84 files)
- `using Microsoft.Extensions.Logging;` (1/84 files)
- `using Npgsql;` (1/84 files)
- `using System.Diagnostics;` (1/84 files)
- `using System.Drawing.Drawing2D;` (1/84 files)
- `using System.Net.NetworkInformation;` (1/84 files)
- `using System.Runtime.CompilerServices;` (1/84 files)
- `using System.Runtime.Versioning;` (1/84 files)

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
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text and Image Extraction with Aspose.Pdf | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract PDF text and images asynchronously using Aspose.Pdf.Facades.PdfExtrac... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs to Matching Text Files | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to loop through a directory of PDF documents, extract their text with Aspose.Pdf.Facade... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Extract Text from PDFs using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF in a folde... |
| [cancel-pdf-text-extraction](./cancel-pdf-text-extraction.cs) | Cancel PDF Text Extraction with CancellationToken | `Document`, `PdfExtractor`, `BindPdf` | Demonstrates extracting text from a PDF using Aspose.Pdf's PdfExtractor while allowing the operat... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text into a MemoryStream and determin... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Text and Images using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF contains both ... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text, images, and attachments from a PDF using Aspose.Pdf's PdfExtrac... |
| [create-contact-sheet-pdf](./create-contact-sheet-pdf.cs) | Create Contact Sheet PDF from Extracted Images | `PdfExtractor`, `ExtractImage`, `HasNextImage` | Extracts all images from an input PDF and generates a new PDF that shows those images as thumbnai... |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary from First Three Pages | `PdfExtractor`, `Document`, `TextFragment` | Extracts text from the first three pages of a PDF using PdfExtractor and generates a new PDF that... |
| [detect-text-only-pdf-by-extracting-images](./detect-text-only-pdf-by-extracting-images.cs) | Detect Text‑Only PDF by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract images from a PDF and determine if ... |
| [error-handling-pdfextractor-bindpdf](./error-handling-pdfextractor-bindpdf.cs) | Error Handling for PdfExtractor BindPdf | `PdfExtractor`, `BindPdf`, `InvalidPdfFileFormatException` | Demonstrates how to bind a PDF with PdfExtractor and catch specific Aspose.Pdf exceptions that oc... |
| [export-pdf-images-to-jpeg-quality-85](./export-pdf-images-to-jpeg-quality-85.cs) | Export PDF Images to JPEG with Quality 85 | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to extract all images from a PDF file and save them as JPEG files using Aspose.P... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract every image from a PDF file with Aspose.Pdf.Facades.PdfExtractor by setting ... |
| [extract-images-and-compress-png](./extract-images-and-compress-png.cs) | Extract Images from PDF and Compress as PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting rendered images from a PDF using Aspose.Pdf.Facades.PdfExtractor and re‑e... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates scanning each PDF page for a specific keyword and extracting only the images from pa... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and combining... |
| [extract-images-defined-in-resources](./extract-images-defined-in-resources.cs) | Extract Images Defined in PDF Resources | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Shows how to set the ImageExtractionMode to DefinedInResources and use PdfExtractor to retrieve i... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF | `PdfExtractor`, `Password`, `BindPdf` | Shows how to open a password‑protected PDF using Aspose.Pdf.Facades.PdfExtractor, extract all emb... |
| [extract-images-from-first-pdf-page](./extract-images-from-first-pdf-page.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades to extract all images from the first page of a PDF and... |
| [extract-images-from-pdf-pages-png](./extract-images-from-pdf-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use PdfExtractor to pull only the images from pages 5 through 10 of a PDF and save e... |
| [extract-images-from-pdf-to-csv](./extract-images-from-pdf-to-csv.cs) | Extract Images from PDF and Generate CSV Report | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull images from each PDF page, save them as ... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF and Save to ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF document and pa... |
| [extract-images-from-pdf-upload-to-s3](./extract-images-from-pdf-upload-to-s3.cs) | Extract Images from PDF and Upload to Amazon S3 | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting each image from a PDF using Aspose.Pdf's PdfExtractor and uploading the i... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates binding a PDF to Aspose.Pdf.Facades.PdfExtractor, extracting all images, and saving ... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF using the defa... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Specific PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a single PDF page using Aspose.Pdf.Facades.PdfExtractor by s... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images from PDF and Create Markdown Gallery | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Shows how to use Aspose.Pdf's PdfExtractor to pull images from a PDF, save them as files, and gen... |
| [extract-images-ocr-pdf](./extract-images-ocr-pdf.cs) | Extract Images from PDF and Perform OCR | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from a PDF using Aspose.Pdf.Facades and then runs OCR on each ima... |
| [extract-images-original-format](./extract-images-original-format.cs) | Extract Images from PDF in Original Format | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract images from a PDF while preserving th... |
| ... | | | *and 54 more files* |

## Category Statistics
- Total examples: 84

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

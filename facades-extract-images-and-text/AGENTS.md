---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **83** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (83/83 files) ← category-specific
- `using Aspose.Pdf;` (22/83 files)
- `using Aspose.Pdf.Text;` (4/83 files)
- `using Aspose.Pdf.Drawing;` (2/83 files)
- `using Aspose.Pdf.AI;` (1/83 files)
- `using Aspose.Pdf.Devices;` (1/83 files)
- `using System;` (83/83 files)
- `using System.IO;` (82/83 files)
- `using System.Text;` (19/83 files)
- `using System.Drawing.Imaging;` (17/83 files)
- `using System.Collections.Generic;` (15/83 files)
- `using System.Drawing;` (8/83 files)
- `using System.Threading.Tasks;` (7/83 files)
- `using System.Text.Json;` (4/83 files)
- `using Azure.Storage.Blobs;` (3/83 files)
- `using System.Threading;` (3/83 files)
- `using Amazon.S3;` (2/83 files)
- `using Amazon.S3.Model;` (2/83 files)
- `using NUnit.Framework;` (2/83 files)
- `using System.IO.Compression;` (2/83 files)
- `using System.Security.Cryptography;` (2/83 files)
- `using Amazon;` (1/83 files)
- `using Google.Cloud.Storage.V1;` (1/83 files)
- `using Microsoft.Azure.WebJobs;` (1/83 files)
- `using Microsoft.Extensions.Logging;` (1/83 files)
- `using System.Diagnostics;` (1/83 files)
- `using System.Net;` (1/83 files)

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
| [async-extract-text-and-images-from-pdf](./async-extract-text-and-images-from-pdf.cs) | Asynchronously Extract Text and Images from PDF | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor with async/await to extract all text, per... |
| [async-pdf-text-extraction-with-cancellation](./async-pdf-text-extraction-with-cancellation.cs) | Asynchronous PDF Text Extraction with Cancellation Support | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates extracting text from a PDF using Aspose.Pdf's PdfExtractor while honoring a Cancella... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs to Text Files | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to iterate over a directory of PDF files, extract their text using Aspose.Pdf.Facades.P... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Extract Text from PDFs using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF in a folder and w... |
| [batch-pdf-text-extraction-azure-blob](./batch-pdf-text-extraction-azure-blob.cs) | Batch PDF Text Extraction from Azure Blob Storage | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to enumerate PDF files in an Azure Blob container, extract their text using Aspo... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to bind a PDF to Aspose.Pdf.Facades.PdfExtractor, extract its text into a MemoryStream,... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Text and Images using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF file contains both te... |
| [check-pdf-text-only-by-extracting-images](./check-pdf-text-only-by-extracting-images.cs) | Check if PDF is Text‑Only by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to detect images in a PDF and determine w... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to read a JSON configuration file to enable or disable extraction of text, images, and ... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF for Archival | `PdfConverter`, `BindPdf`, `DoConvert` | Demonstrates how to bind a PDF, configure lossless TIFF settings, and save all pages as a single ... |
| [create-a-rest-api-endpoint-that-receives-a-pdf-byt...](./create-a-rest-api-endpoint-that-receives-a-pdf-byte-array-and-returns-extracted-text-as-json.cs) | Create A Rest Api Endpoint That Receives A Pdf Byte Array An... | `PdfExtractor` | Create A Rest Api Endpoint That Receives A Pdf Byte Array And Returns Extracted Text As Json |
| [create-contact-sheet-pdf](./create-contact-sheet-pdf.cs) | Create Contact Sheet PDF from Extracted Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a source PDF and assembles them into a contact sheet PDF with thumbnails... |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary from First Three Pages | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates extracting text from the first three pages of a PDF using PdfExtractor and generatin... |
| [export-pdf-images-to-jpeg-quality-85](./export-pdf-images-to-jpeg-quality-85.cs) | Export PDF Images to JPEG with Quality 85 | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to extract all images from a PDF and save them as JPEG files with a quality setting of ... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract images from every page of a PDF using Aspose.Pdf's PdfExtractor, sett... |
| [extract-embedded-attachments-from-pdf](./extract-embedded-attachments-from-pdf.cs) | Extract Embedded Attachments from PDF | `PdfExtractor`, `BindPdf`, `ExtractAttachment` | Demonstrates how to use Aspose.Pdf.Facades to extract all file attachments embedded in a PDF and ... |
| [extract-images-and-create-thumbnails](./extract-images-and-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and generatin... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from PDF Pages Containing a Keyword | `Document`, `PdfExtractor`, `TextFragmentAbsorber` | Shows how to search each PDF page for a specific keyword using TextFragmentAbsorber and then extr... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Create PDF Portfolio from Extracted Images | `PdfExtractor`, `Document`, `Page` | Demonstrates how to extract all images from an existing PDF using the PdfExtractor facade and ass... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a PDF into memory streams and merge them horizontally into a... |
| [extract-images-defined-in-resources](./extract-images-defined-in-resources.cs) | Extract Images Defined in Resources from PDF | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Demonstrates setting the ImageExtractionMode to DefinedInResources and extracting all resource‑ba... |
| [extract-images-from-a-pdf-and-embed-them-into-an-h...](./extract-images-from-a-pdf-and-embed-them-into-an-html-report-using-base64-data-uris.cs) | Extract Images From A Pdf And Embed Them Into An Html Report... | `StringBuilder`, `PdfExtractor` | Extract Images From A Pdf And Embed Them Into An Html Report Using Base64 Data Uris |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF | `PdfExtractor`, `Password`, `BindPdf` | Shows how to open a password‑protected PDF with Aspose.Pdf.Facades.PdfExtractor, extract all embe... |
| [extract-images-from-first-pdf-page](./extract-images-from-first-pdf-page.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from the first page of... |
| [extract-images-from-pdf-pages-png](./extract-images-from-pdf-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract only images from pages 5‑10 of a PDF... |
| [extract-images-from-pdf-png](./extract-images-from-pdf-png.cs) | Extract Images from PDF and Save as PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save each... |
| [extract-images-from-pdf-to-base64-json](./extract-images-from-pdf-to-base64-json.cs) | Extract Images from PDF to Base64 JSON | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example uses Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF, converts each ima... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a PDF file using Aspose.Pdf.Facades.PdfExtractor inside a us... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF and save each ... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF using the defa... |
| ... | | | *and 53 more files* |

## Category Statistics
- Total examples: 83

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

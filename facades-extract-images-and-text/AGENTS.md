---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (81/85 files) ← category-specific
- `using Aspose.Pdf;` (32/85 files)
- `using Aspose.Pdf.Text;` (7/85 files)
- `using Aspose.Pdf.AI;` (1/85 files)
- `using Aspose.Pdf.Devices;` (1/85 files)
- `using Aspose.Pdf.Drawing;` (1/85 files)
- `using System;` (85/85 files)
- `using System.IO;` (84/85 files)
- `using System.Drawing.Imaging;` (21/85 files)
- `using System.Text;` (21/85 files)
- `using System.Collections.Generic;` (16/85 files)
- `using System.Threading.Tasks;` (8/85 files)
- `using System.Drawing;` (4/85 files)
- `using System.Text.Json;` (4/85 files)
- `using System.IO.Compression;` (3/85 files)
- `using System.Threading;` (3/85 files)
- `using NUnit.Framework;` (2/85 files)
- `using System.Net.Http;` (2/85 files)
- `using System.Security.Cryptography;` (2/85 files)
- `using Amazon;` (1/85 files)
- `using Amazon.S3;` (1/85 files)
- `using Amazon.S3.Transfer;` (1/85 files)
- `using Azure.Storage.Blobs;` (1/85 files)
- `using System.Diagnostics;` (1/85 files)
- `using System.Drawing.Drawing2D;` (1/85 files)
- `using System.Linq;` (1/85 files)
- `using System.Net;` (1/85 files)
- `using System.Net.Http.Headers;` (1/85 files)
- `using System.Net.Sockets;` (1/85 files)

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
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text and Image Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract all text and images from a PDF file asynchronously using Aspose.Pdf's... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs to Files | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to iterate over a folder of PDF documents, extract their text using Aspose.Pdf.Facades.... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Text Extraction from PDFs with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to iterate over all PDF files in a folder... |
| [batch-ocr-extraction](./batch-ocr-extraction.cs) | Batch OCR Text Extraction from PDFs using Aspose.Pdf AI | `Document`, `OpenAIClient`, `OpenAIOcrCopilotOptions` | Demonstrates processing multiple PDF files, extracting their text with Aspose.Pdf AI OCR copilot,... |
| [cancel-pdf-text-extraction](./cancel-pdf-text-extraction.cs) | Cancel PDF Text Extraction with CancellationToken | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use a CancellationToken to abort Aspose.Pdf.Facades.PdfExtractor text extract... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text into a MemoryStream and determin... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Both Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf's PdfExtractor to verify that a PDF contains at least one piece of te... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates reading a JSON configuration to toggle extraction of text, images, and attachments f... |
| [create-pdf-summary-first-three-pages](./create-pdf-summary-first-three-pages.cs) | Create PDF Summary with First Three Pages Text | `Document`, `PdfExtractor`, `Page` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract text from the first three pages of ... |
| [detect-text-only-pdf-by-extracting-images](./detect-text-only-pdf-by-extracting-images.cs) | Detect Text‑Only PDF by Checking for Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to bind a PDF, extract any images into me... |
| [error-handling-corrupted-pdf-binding](./error-handling-corrupted-pdf-binding.cs) | Error Handling for Corrupted PDF Binding | `PdfExtractor`, `BindPdf`, `InvalidPdfFileFormatException` | Demonstrates how to safely bind a PDF using PdfExtractor and catch specific exceptions when the f... |
| [export-pdf-images-to-jpeg-quality-85](./export-pdf-images-to-jpeg-quality-85.cs) | Export PDF Images to JPEG with Quality 85 | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to extract all images from a PDF and save them as JPEG files with a quality setting of ... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF using PdfExtractor | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to extract every image from a PDF document using Aspose.Pdf's PdfExtractor facad... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to scan each PDF page for a specific keyword using TextAbsorber and then extract only t... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Create PDF Portfolio from Extracted Images | `BindPdf`, `ExtractImage`, `HasNextImage` | Demonstrates how to extract all images from an existing PDF using the PdfExtractor facade and ass... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create a Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a PDF into memory streams and merge them horizontally into a... |
| [extract-images-create-thumbnails](./extract-images-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF and generate PNG t... |
| [extract-images-defined-in-resources](./extract-images-defined-in-resources.cs) | Extract Images Defined in Resources from PDF | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Demonstrates how to use Aspose.Pdf's PdfExtractor facade to extract all images that are defined i... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from the first page of a PDF using Aspose.Pdf.Facades.PdfE... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF with Password | `PdfExtractor`, `Password`, `BindPdf` | Shows how to provide a user password for an encrypted PDF and extract all embedded images using A... |
| [extract-images-from-pdf-pages-png](./extract-images-from-pdf-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract images from pages 5‑10 of a PD... |
| [extract-images-from-pdf-to-base64-json](./extract-images-from-pdf-to-base64-json.cs) | Extract Images from PDF and Output as Base64 JSON | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example uses Aspose.Pdf.Facades.PdfExtractor to pull each image from a PDF, converts it to PN... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF and Save to ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF and bundle them in... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF using th... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Specific PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a single page of a PDF using Aspose.Pdf.Facades.PdfExtractor. |
| [extract-images-gcs](./extract-images-gcs.cs) | Extract Images from PDF and Upload to Google Cloud Storage | `Document`, `ExtractImage`, `GetNextImage` | Creates a sample PDF, extracts all images using Aspose.Pdf, and shows where to upload each image ... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images from PDF and Create Markdown Gallery | `PdfExtractor`, `BindPdf`, `Resolution` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF, save them to a ... |
| [extract-images-ocr-openai-copilot](./extract-images-ocr-openai-copilot.cs) | Extract Images from PDF and Perform OCR with OpenAI Copilot | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from a PDF using Aspose.Pdf.Facades.PdfExtractor, saves them as P... |
| [extract-images-original-format](./extract-images-original-format.cs) | Extract Images in Original Format with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF while preservi... |
| ... | | | *and 55 more files* |

## Category Statistics
- Total examples: 85

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
Updated: 2026-06-18 | Run: `20260618_032725_440ba6`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf.Facades;` (63/81 files) ← category-specific
- `using Aspose.Pdf;` (41/81 files) ← category-specific
- `using Aspose.Pdf.Text;` (11/81 files)
- `using Aspose.Pdf.AI;` (1/81 files)
- `using Aspose.Pdf.Devices;` (1/81 files)
- `using Aspose.Pdf.Multithreading;` (1/81 files)
- `using System;` (81/81 files)
- `using System.IO;` (81/81 files)
- `using System.Collections.Generic;` (18/81 files)
- `using System.Text;` (16/81 files)
- `using System.Drawing.Imaging;` (9/81 files)
- `using System.Threading.Tasks;` (6/81 files)
- `using System.IO.Compression;` (3/81 files)
- `using System.Threading;` (3/81 files)
- `using Amazon.S3;` (2/81 files)
- `using Amazon.S3.Transfer;` (2/81 files)
- `using Azure.Storage.Blobs;` (2/81 files)
- `using NUnit.Framework;` (2/81 files)
- `using System.Collections;` (2/81 files)
- `using System.Drawing;` (2/81 files)
- `using System.Security.Cryptography;` (2/81 files)
- `using System.Text.Json;` (2/81 files)
- `using Amazon.S3.Model;` (1/81 files)
- `using Azure.Storage.Blobs.Models;` (1/81 files)
- `using DocumentFormat.OpenXml;` (1/81 files)
- `using DocumentFormat.OpenXml.Wordprocessing;` (1/81 files)
- `using Microsoft.Azure.WebJobs;` (1/81 files)
- `using Microsoft.Extensions.Logging;` (1/81 files)
- `using System.Diagnostics;` (1/81 files)
- `using System.Linq;` (1/81 files)
- `using System.Runtime.CompilerServices;` (1/81 files)

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
| [async-pdf-text-extraction](./async-pdf-text-extraction.cs) | Asynchronous PDF Text Extraction with Aspose.Pdf | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text from a PDF file asynchronously using PdfExtractor and async/awai... |
| [azure-function-pdf-text-extraction](./azure-function-pdf-text-extraction.cs) | Scheduled Azure Function to Extract Text from PDFs in Queue | `Document`, `TextAbsorber`, `Accept` | An Azure Function triggered by a storage queue that downloads a PDF blob, extracts its text using... |
| [batch-extract-pdf-text](./batch-extract-pdf-text.cs) | Batch Extract Text from PDFs to Matching Text Files | `PdfExtractor`, `BindPdf`, `ExtractText` | Processes all PDF files in a folder, extracts their text using Aspose.Pdf.Facades.PdfExtractor, a... |
| [batch-extract-pdf-text__v2](./batch-extract-pdf-text__v2.cs) | Batch extract PDF text from Azure Blob storage | `Document`, `TextAbsorber`, `BlobServiceClient` | Downloads PDF files from an Azure Blob container, extracts their text using Aspose.Pdf, and uploa... |
| [batch-extract-text-pdf](./batch-extract-text-pdf.cs) | Batch Extract Text from PDFs Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF file in a directo... |
| [cancel-pdfextractor-extraction](./cancel-pdfextractor-extraction.cs) | Cancel PdfExtractor Extraction with InterruptMonitor | `PdfExtractor`, `InterruptMonitor`, `BindPdf` | Shows how to abort a PdfExtractor operation using an InterruptMonitor and its cancellation token. |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Any Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Extracts the PDF text to a MemoryStream using PdfExtractor and determines if any text is present ... |
| [check-pdf-text-and-images](./check-pdf-text-and-images.cs) | Check if PDF Contains Both Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use PdfExtractor to determine whether a PDF file contains any text and any im... |
| [compute-sha256-attachment-hashes](./compute-sha256-attachment-hashes.cs) | Compute SHA-256 Hashes for Extracted PDF Attachments | `PdfExtractor`, `BindPdf`, `ExtractAttachment` | Extracts all attachments from a PDF, saves them to files, and computes a SHA‑256 hash for each sa... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Extraction (Text, Images, Attachments) | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to toggle extraction of text, images, and attachments from a PDF using a JSON configura... |
| [determine-pdf-text-only](./determine-pdf-text-only.cs) | Determine if PDF is Text-Only by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a PDF using PdfExtractor and reports whether the document contains any i... |
| [export-pdf-images-to-jpeg](./export-pdf-images-to-jpeg.cs) | Export PDF Images to JPEG with Quality Setting | `PdfConverter`, `BindPdf`, `DoConvert` | Extracts each page of a PDF as a JPEG image using PdfConverter, setting the JPEG quality to 85. |
| [extract-all-images](./extract-all-images.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract images from every page of a PDF by setting StartPage to 1 and EndPage to 0 w... |
| [extract-images-bmp](./extract-images-bmp.cs) | Extract Images from PDF and Save as BMP | `PdfConverter`, `BindPdf`, `DoConvert` | Extracts all images from a PDF file and saves each as a BMP file, preserving the original resolut... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `ExtractText` | Scans each PDF page for a specific keyword and extracts images only from pages where the keyword ... |
| [extract-images-compress-png](./extract-images-compress-png.cs) | Extract Images from PDF and Compress PNGs | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting images from a PDF using Aspose.Pdf and then losslessly compressing each P... |
| [extract-images-contact-sheet](./extract-images-contact-sheet.cs) | Extract Images and Create Contact Sheet PDF | `Document`, `AddImage`, `Save` | Extracts all images from a PDF and generates a new PDF that displays the images as thumbnails arr... |
| [extract-images-csv-manifest](./extract-images-csv-manifest.cs) | Extract Images from PDF and Create CSV Manifest | `Document`, `Page`, `XImage` | Extracts all images from a PDF, saves them to files, and generates a CSV file listing each image'... |
| [extract-images-csv](./extract-images-csv.cs) | Extract Images from PDF and Generate CSV Report | `Document`, `Page`, `XImage` | Extracts all images from a PDF, saves them as separate files, and creates a CSV listing each imag... |
| [extract-images-definedinresources](./extract-images-definedinresources.cs) | Extract Images from PDF Using DefinedInResources Mode | `PdfExtractor`, `ExtractImageMode`, `BindPdf` | Demonstrates how to set PdfExtractor.ExtractImageMode to DefinedInResources and extract all resou... |
| [extract-images-encrypted-pdf](./extract-images-encrypted-pdf.cs) | Extract Images from Encrypted PDF Using Password | `PdfExtractor`, `Password`, `BindPdf` | Shows how to provide a user password for an encrypted PDF and extract all images using Aspose.Pdf... |
| [extract-images-first-page](./extract-images-first-page.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to extract all images from the first page of a PDF using PdfExtractor and store ... |
| [extract-images-guid](./extract-images-guid.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a PDF file and saves each image using a unique GUID filename to avoid na... |
| [extract-images-html-gallery](./extract-images-html-gallery.cs) | Extract Images from PDF and Create HTML Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a PDF using PdfExtractor, saves each image as a separate file, and gener... |
| [extract-images-html-report](./extract-images-html-report.cs) | Extract Images from PDF and Embed in HTML Report | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using PdfExtractor and embedding them as base64 dat... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images and Create Markdown Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a PDF using PdfExtractor and generates a markdown file with image links ... |
| [extract-images-ocr](./extract-images-ocr.cs) | Extract Images from PDF and Perform OCR using Aspose.Pdf AI | `Document`, `PdfExtractor`, `OpenAIClient` | Extracts all images from a PDF file, saves them as PNG files, and uses Aspose.Pdf AI OCR copilot ... |
| [extract-images-pages-png](./extract-images-pages-png.cs) | Extract Images from Specific Pages to PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to configure PdfExtractor to extract images only from pages 5 through 10 of a PD... |
| [extract-images-pdf-extractor](./extract-images-pdf-extractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF file using PdfExtractor inside a using block fo... |
| [extract-images-pdf-json](./extract-images-pdf-json.cs) | Extract Images from PDF and Output Base64 JSON | `Document`, `Page`, `XImage` | Extracts all images from a PDF, encodes them in Base64, and writes a JSON array with image metadata. |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-extract-images-and-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_213136_a66d65`
<!-- AUTOGENERATED:END -->

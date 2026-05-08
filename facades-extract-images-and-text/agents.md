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

- `using Aspose.Pdf.Facades;` (82/82 files) ← category-specific
- `using Aspose.Pdf;` (23/82 files)
- `using Aspose.Pdf.Text;` (4/82 files)
- `using Aspose.Pdf.AI;` (1/82 files)
- `using Aspose.Pdf.Drawing;` (1/82 files)
- `using System;` (82/82 files)
- `using System.IO;` (81/82 files)
- `using System.Text;` (25/82 files)
- `using System.Drawing.Imaging;` (20/82 files)
- `using System.Collections.Generic;` (16/82 files)
- `using System.Threading.Tasks;` (7/82 files)
- `using System.Text.Json;` (4/82 files)
- `using Azure.Storage.Blobs;` (3/82 files)
- `using System.IO.Compression;` (3/82 files)
- `using System.Threading;` (3/82 files)
- `using System.Drawing;` (2/82 files)
- `using System.Linq;` (2/82 files)
- `using System.Security.Cryptography;` (2/82 files)
- `using Azure.Storage.Blobs.Models;` (1/82 files)
- `using DocumentFormat.OpenXml;` (1/82 files)
- `using DocumentFormat.OpenXml.Drawing;` (1/82 files)
- `using DocumentFormat.OpenXml.Drawing.Pictures;` (1/82 files)
- `using DocumentFormat.OpenXml.Drawing.Wordprocessing;` (1/82 files)
- `using DocumentFormat.OpenXml.Packaging;` (1/82 files)
- `using DocumentFormat.OpenXml.Wordprocessing;` (1/82 files)
- `using Microsoft.Azure.WebJobs;` (1/82 files)
- `using Microsoft.Extensions.Logging;` (1/82 files)
- `using NUnit.Framework;` (1/82 files)
- `using System.Diagnostics;` (1/82 files)
- `using System.Runtime.CompilerServices;` (1/82 files)
- `using System.Runtime.InteropServices;` (1/82 files)
- `using System.Runtime.Versioning;` (1/82 files)

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
| [async-pdf-extraction](./async-pdf-extraction.cs) | Asynchronous PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text, images, and embedded attachments from a PDF file asynchronously... |
| [batch-extract-text-from-pdfs-azure-blob](./batch-extract-text-from-pdfs-azure-blob.cs) | Batch Extract Text from PDFs in Azure Blob Storage | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to enumerate PDF files in an Azure Blob container, extract their text using Aspo... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to loop through a folder of PDF files, use Aspose.Pdf.Facades.PdfExtractor to extract t... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch Extract Text from PDFs using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF in a directory an... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates using Aspose.Pdf's PdfExtractor to extract text into a MemoryStream and determine wh... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Both Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf's PdfExtractor to determine whether a PDF file contains at lea... |
| [check-pdf-text-only-by-detecting-images](./check-pdf-text-only-by-detecting-images.cs) | Check if PDF Is Text‑Only by Detecting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example uses Aspose.Pdf.Facades.PdfExtractor to bind a PDF, extract its images, and then chec... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates reading a JSON configuration to toggle extraction of text, images, and attachments f... |
| [count-pages-images-attachments-pdf](./count-pages-images-attachments-pdf.cs) | Count Pages, Images, and Attachments in a PDF | `Document`, `PdfExtractor`, `Count` | Demonstrates how to use Aspose.Pdf to obtain the total number of pages, embedded images, and file... |
| [create-contact-sheet-pdf](./create-contact-sheet-pdf.cs) | Create Contact Sheet PDF from Extracted Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a source PDF and generates a new PDF that displays those images as thumb... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to extract every image from a PDF document by setting the page range to all page... |
| [extract-first-three-pages-summary-pdf](./extract-first-three-pages-summary-pdf.cs) | Extract First Three Pages Text and Create Summary PDF | `PdfExtractor`, `Document`, `Page` | Shows how to extract text from the first three pages of a PDF using PdfExtractor (Facades API) an... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from Pages Containing a Keyword | `Document`, `PdfExtractor`, `BindPdf` | Shows how to scan each PDF page for a specific keyword and, when the keyword is found, extract al... |
| [extract-images-create-pdfa2b](./extract-images-create-pdfa2b.cs) | Extract Images and Create PDF/A‑2b Document | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract images from a PDF using PdfExtractor, embed each image as an XObject on a ne... |
| [extract-images-create-portfolio-pdf](./extract-images-create-portfolio-pdf.cs) | Extract Images from PDF and Create Portfolio PDF | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a source PDF using Aspose.Pdf's PdfExtractor and assembli... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create Sprite Sheet PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a PDF using PdfExtractor and merge them into a single horizo... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF using PdfExtractor | `PdfExtractor`, `Password`, `BindPdf` | Shows how to provide a user password to Aspose.Pdf.Facades.PdfExtractor and extract all images fr... |
| [extract-images-from-first-pdf-page](./extract-images-from-first-pdf-page.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf's PdfExtractor facade to pull all images from the first page o... |
| [extract-images-from-pdf-and-compress-png](./extract-images-from-pdf-and-compress-png.cs) | Extract Images from PDF and Compress PNGs | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract images from a PDF,... |
| [extract-images-from-pdf-csv](./extract-images-from-pdf-csv.cs) | Extract Images from PDF and Generate CSV Report | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract images from each PDF page, save them ... |
| [extract-images-from-pdf-pages-png](./extract-images-from-pdf-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to configure PdfExtractor to extract only images from pages 5‑10 of a PDF and sa... |
| [extract-images-from-pdf-to-gcs](./extract-images-from-pdf-to-gcs.cs) | Extract Images from PDF and Upload to Google Cloud Storage | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull images from a PDF and then upload each i... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF to ZIP Archive | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull all images out of a PDF and then package... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor inside a using block to extract all image... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to pull all images from a PDF and save them int... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Specific PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a single PDF page by setting the PdfExtractor's StartPage an... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images from PDF and Generate Markdown Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to pull images from a PDF, save them as P... |
| [extract-images-ocr-openai](./extract-images-ocr-openai.cs) | Extract Images from PDF and Perform OCR with OpenAI | `BindPdf`, `ExtractImage`, `HasNextImage` | Demonstrates extracting all images from a PDF using PdfExtractor and then applying Aspose.Pdf.AI ... |
| [extract-images-original-format](./extract-images-original-format.cs) | Extract Images from PDF in Original Format | `PdfExtractor`, `ExtractImageMode`, `BindPdf` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF while pr... |
| ... | | | *and 52 more files* |

## Category Statistics
- Total examples: 82

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
Updated: 2026-05-08 | Run: `20260508_144436_050a95`
<!-- AUTOGENERATED:END -->

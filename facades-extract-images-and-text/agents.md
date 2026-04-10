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

- `using Aspose.Pdf.Facades;` (83/83 files) ← category-specific
- `using Aspose.Pdf;` (28/83 files)
- `using Aspose.Pdf.Text;` (5/83 files)
- `using Aspose.Pdf.AI;` (1/83 files)
- `using Aspose.Pdf.Drawing;` (1/83 files)
- `using System;` (83/83 files)
- `using System.IO;` (81/83 files)
- `using System.Text;` (22/83 files)
- `using System.Drawing.Imaging;` (21/83 files)
- `using System.Collections.Generic;` (16/83 files)
- `using System.Threading.Tasks;` (6/83 files)
- `using System.Drawing;` (4/83 files)
- `using System.Text.Json;` (4/83 files)
- `using System.Threading;` (4/83 files)
- `using System.IO.Compression;` (3/83 files)
- `using Azure.Storage.Blobs;` (2/83 files)
- `using System.Security.Cryptography;` (2/83 files)
- `using Azure.Storage.Blobs.Models;` (1/83 files)
- `using Google.Apis.Storage.v1.Data;` (1/83 files)
- `using Google.Cloud.Storage.V1;` (1/83 files)
- `using Microsoft.Extensions.Logging;` (1/83 files)
- `using System.Collections;` (1/83 files)
- `using System.Collections.Concurrent;` (1/83 files)
- `using System.Diagnostics;` (1/83 files)
- `using System.Drawing.Drawing2D;` (1/83 files)
- `using System.Linq;` (1/83 files)
- `using System.Net;` (1/83 files)
- `using System.Net.Sockets;` (1/83 files)

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
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text and Image Extraction with Aspose.Pdf | `Document`, `BindPdf`, `ExtractText` | Shows how to extract all text and images from a PDF file asynchronously by wrapping Aspose.Pdf's ... |
| [azure-function-extract-pdf-text](./azure-function-extract-pdf-text.cs) | Azure Function to Extract Text from PDFs in a Queue | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows a scheduled Azure Function that reads a PDF blob name from an Azure Storage queue, extracts... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract text from every PDF in a folder and w... |
| [batch-pdf-text-extraction](./batch-pdf-text-extraction.cs) | Batch PDF Text Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Processes all PDF files in a specified folder, extracts their text using Aspose.Pdf.Facades.PdfEx... |
| [cancel-pdf-text-extraction](./cancel-pdf-text-extraction.cs) | Cancel PDF Text Extraction with CancellationToken | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text from each page of a PDF using Aspose.Pdf's PdfExtractor while al... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract text from a PDF into a MemoryS... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Text and Images | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF file contains both ... |
| [check-pdf-text-only-by-extracting-images](./check-pdf-text-only-by-extracting-images.cs) | Check if PDF Is Text‑Only by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to detect whether a PDF contains any imag... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `Document`, `PdfExtractor`, `BindPdf` | Shows how to extract text, images, and attachments from a PDF with Aspose.Pdf while toggling each... |
| [count-pages-images-attachments](./count-pages-images-attachments.cs) | Count Pages, Images, and Attachments in a PDF | `Document`, `PdfExtractor`, `FileSpecification` | Demonstrates how to use Aspose.Pdf to determine the total number of pages, embedded images, and f... |
| [create-contact-sheet-pdf-from-images](./create-contact-sheet-pdf-from-images.cs) | Create Contact Sheet PDF with Image Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from an existing PDF using PdfExtractor, arranges them as thumbna... |
| [create-pdf-summary-first-three-pages](./create-pdf-summary-first-three-pages.cs) | Create PDF Summary with First Three Pages Text | `PdfExtractor`, `BindPdf`, `ExtractText` | Extracts text from the first three pages of a PDF using PdfExtractor and builds a new PDF that co... |
| [export-pdf-images-to-jpeg-quality-85](./export-pdf-images-to-jpeg-quality-85.cs) | Export PDF Images to JPEG (Quality 85) | `Document`, `PdfConverter`, `BindPdf` | Demonstrates how to extract each page image from a PDF and save it as a JPEG file with a quality ... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates extracting images from every page of a PDF with Aspose.Pdf's PdfExtractor by setting... |
| [extract-attachments-from-pdf](./extract-attachments-from-pdf.cs) | Extract Attachments from PDF | `PdfExtractor`, `BindPdf`, `ExtractAttachment` | Demonstrates how to extract all embedded file attachments from a PDF using Aspose.Pdf.Facades and... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from PDF Pages Containing a Keyword | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to scan each PDF page for a specific keyword and, only when the keyword is found... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Extract Images and Build PDF Portfolio | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from an existing PDF, saves them as temporary PNG files, and crea... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and combine them... |
| [extract-images-defined-in-resources](./extract-images-defined-in-resources.cs) | Extract Images Defined in Resources from PDF | `PdfExtractor`, `BindPdf`, `ExtractImageMode` | Demonstrates how to set ImageExtractionMode to DefinedInResources and extract all resource‑based ... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from the first page... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF with Password | `PdfExtractor`, `Password`, `BindPdf` | Shows how to set the user password on a PdfExtractor and extract all images from an encrypted PDF... |
| [extract-images-from-pdf-csv-report](./extract-images-from-pdf-csv-report.cs) | Extract Images from PDF and Generate CSV Report | `Document`, `ImagePlacementAbsorber`, `ImagePlacement` | The example extracts all images from each page of a PDF, saves them as PNG files, and creates a C... |
| [extract-images-from-pdf-pages-png](./extract-images-from-pdf-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract images from pages 5 through 10 of a... |
| [extract-images-from-pdf-to-json](./extract-images-from-pdf-to-json.cs) | Extract Images from PDF and Output as Base64 JSON | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor, converting e... |
| [extract-images-from-pdf-to-unc-share](./extract-images-from-pdf-to-unc-share.cs) | Extract Images from PDF to UNC Network Share | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save them a... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF to Temporary Folder | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF using the default ext... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Specific PDF Page | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to extract all images from a single PDF page by setting StartPage and EndPage to the sa... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images from PDF and Generate a Markdown Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | The sample extracts all raster images from a PDF using Aspose.Pdf.Facades.PdfExtractor, saves the... |
| [extract-images-ocr-openai](./extract-images-ocr-openai.cs) | Extract Images from PDF and Perform OCR with OpenAI | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from a PDF using the Facades API, sends each image to the OpenAI ... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-extract-images-and-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

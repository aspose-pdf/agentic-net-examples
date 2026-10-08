---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **79** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (69/79 files) ← category-specific
- `using Aspose.Pdf;` (35/79 files)
- `using Aspose.Pdf.Text;` (8/79 files)
- `using Aspose.Pdf.Annotations;` (1/79 files)
- `using System;` (78/79 files)
- `using System.IO;` (78/79 files)
- `using System.Collections.Generic;` (10/79 files)
- `using System.Drawing.Imaging;` (10/79 files)
- `using System.Text;` (9/79 files)
- `using System.Drawing;` (7/79 files)
- `using System.IO.Compression;` (3/79 files)
- `using System.Reflection;` (3/79 files)
- `using System.Text.Json;` (3/79 files)
- `using System.Threading.Tasks;` (3/79 files)
- `using Amazon.S3;` (2/79 files)
- `using Azure.Storage.Blobs;` (2/79 files)
- `using System.Security.Cryptography;` (2/79 files)
- `using Amazon;` (1/79 files)
- `using Amazon.S3.Model;` (1/79 files)
- `using Amazon.S3.Transfer;` (1/79 files)
- `using Azure.Data.Tables;` (1/79 files)
- `using Azure.Storage.Blobs.Models;` (1/79 files)
- `using Google.Cloud.Storage.V1;` (1/79 files)
- `using Npgsql;` (1/79 files)
- `using System.Diagnostics;` (1/79 files)
- `using System.Drawing.Drawing2D;` (1/79 files)
- `using System.Linq;` (1/79 files)
- `using System.Threading;` (1/79 files)

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
| [add-watermark-extract-images-from-pdf](./add-watermark-extract-images-from-pdf.cs) | Extract PDF Images and Apply Watermark | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf's PdfExtractor and then overla... |
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronous PDF Text, Image, and Page Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text, images, and selected pages from a PDF file asynchronously using... |
| [batch-extract-text-from-pdfs-azure-blob](./batch-extract-text-from-pdfs-azure-blob.cs) | Batch Extract Text from PDFs in Azure Blob Storage | `Document`, `TextAbsorber`, `Pages` | A console app that iterates PDF blobs, downloads each to a temporary file, extracts all text usin... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to loop through a directory, use Aspose.Pdf.Facades.PdfExtractor to extract text from e... |
| [cancel-pdf-image-extraction](./cancel-pdf-image-extraction.cs) | Cancel PDF Extraction with CancellationToken | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use a CancellationToken to abort Aspose.Pdf.Facades.PdfExtractor operations s... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to extract text from a PDF into a MemoryStream with Aspose.Pdf.Facades.PdfExtractor and... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Check PDF for Text and Images using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to verify that a PDF contains both textual content and images by leveraging Aspose.Pdf.... |
| [check-pdf-text-only-by-detecting-images](./check-pdf-text-only-by-detecting-images.cs) | Detect Text‑Only PDF by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Extracts all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and checks if any image file... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates reading a JSON configuration to toggle extraction of text, images, and embedded atta... |
| [create-contact-sheet-pdf-from-extracted-images](./create-contact-sheet-pdf-from-extracted-images.cs) | Create Contact Sheet PDF from Extracted Images | `Document`, `ImagePlacementAbsorber`, `ImagePlacement` | The example extracts all images from a source PDF using ImagePlacementAbsorber and then builds a ... |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary from First Three Pages Text | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates extracting text from the first three pages of a PDF using PdfExtractor (Facades) and... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using ImageExtractor | `ImageExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract images from every page of a PDF with Aspose.Pdf.Facades.ImageExtracto... |
| [extract-attachments-compute-sha256](./extract-attachments-compute-sha256.cs) | Extract PDF Attachments and Compute SHA-256 Hashes | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | The example loads a PDF using Aspose.Pdf, extracts all embedded file attachments to a folder, and... |
| [extract-embedded-attachments-from-pdf](./extract-embedded-attachments-from-pdf.cs) | Extract Embedded Attachments from PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to enumerate embedded file attachments in a PDF with Aspose.Pdf and write each a... |
| [extract-images-and-compress-png-zip](./extract-images-and-compress-png-zip.cs) | Extract PDF Images and Compress into ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting images from a PDF using Aspose.Pdf.Facades.PdfExtractor, saving them as P... |
| [extract-images-and-create-thumbnails](./extract-images-and-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `Document`, `Page`, `XImage` | The example loads a PDF, extracts each embedded image, resizes it to a maximum of 200 pixels whil... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from PDF Pages Containing a Keyword | `Document`, `PdfExtractor`, `BindPdf` | Shows how to scan each PDF page for a specific keyword using PdfExtractor and then extract images... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Extract Images from PDF and Create a PDF Portfolio | `Document`, `Page`, `XImage` | Shows how to extract all images from a PDF, save them as PNG files, and embed each image as an at... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create a Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and combi... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from the first page of a PDF ... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF Using Password | `Document`, `PdfExtractor`, `BindPdf` | Demonstrates how to open an encrypted PDF by providing the user password and extract all embedded... |
| [extract-images-from-pages-png](./extract-images-from-pages-png.cs) | Extract Images from Specific PDF Pages | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates configuring PdfExtractor to extract only images from pages 5 through 10 of a PDF and... |
| [extract-images-from-pdf-to-temp-folder](./extract-images-from-pdf-to-temp-folder.cs) | Extract Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save... |
| [extract-images-from-pdf-to-unc](./extract-images-from-pdf-to-unc.cs) | Extract Images from PDF to UNC Network Share | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and saving th... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF and Create ZIP Archive | `Document`, `Page`, `XImage` | Shows how to iterate through PDF pages, extract each embedded image with Aspose.Pdf, and store th... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Using Block | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use a using block to automatically dispose a PdfExtractor while extracting al... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF and Rename with GUID | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF and save each... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Single PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Shows how to extract all images from a specific page of a PDF using Aspose.Pdf.Facades.PdfExtract... |
| [extract-images-html-gallery](./extract-images-html-gallery.cs) | Extract Images from PDF and Create HTML Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images from a PDF, save th... |
| [extract-images-markdown-gallery](./extract-images-markdown-gallery.cs) | Extract Images from PDF and Create Markdown Gallery | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf's PdfExtractor to pull all images from a PDF, save them as PNG files,... |
| ... | | | *and 49 more files* |

## Category Statistics
- Total examples: 79

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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

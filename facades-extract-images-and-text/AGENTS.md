---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

> **Facades extract images and text** in PDF using C# / .NET -- **84** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (76/84 files) ← category-specific
- `using Aspose.Pdf;` (31/84 files)
- `using Aspose.Pdf.Text;` (7/84 files)
- `using Aspose.Pdf.Devices;` (1/84 files)
- `using System;` (84/84 files)
- `using System.IO;` (84/84 files)
- `using System.Drawing.Imaging;` (24/84 files)
- `using System.Text;` (14/84 files)
- `using System.Collections.Generic;` (10/84 files)
- `using System.Drawing;` (7/84 files)
- `using System.Threading.Tasks;` (7/84 files)
- `using System.Text.Json;` (4/84 files)
- `using System.Threading;` (3/84 files)
- `using System.IO.Compression;` (2/84 files)
- `using System.Security.Cryptography;` (2/84 files)
- `using Amazon;` (1/84 files)
- `using Amazon.S3;` (1/84 files)
- `using Amazon.S3.Model;` (1/84 files)
- `using Google.Cloud.Storage.V1;` (1/84 files)
- `using System.Diagnostics;` (1/84 files)
- `using System.Drawing.Drawing2D;` (1/84 files)

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
| [add-watermark-extract-images-from-pdf](./add-watermark-extract-images-from-pdf.cs) | Extract PDF Images and Add Watermark | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor, overlay ... |
| [async-pdf-text-image-extraction](./async-pdf-text-image-extraction.cs) | Asynchronously Extract Text and Images from PDF | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf to extract all text and images from a PDF file asynchronously,... |
| [batch-extract-text-from-pdfs-azure-blob](./batch-extract-text-from-pdfs-azure-blob.cs) | Extract Text from PDFs and Save as Text Files | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to iterate over PDF files, use Aspose.Pdf.Facades.PdfExtractor to extract UTF‑8 ... |
| [batch-extract-text-from-pdfs](./batch-extract-text-from-pdfs.cs) | Batch Extract Text from PDFs | `PdfExtractor`, `BindPdf`, `ExtractText` | Shows how to iterate over a folder of PDF files, extract their text using Aspose.Pdf.Facades.PdfE... |
| [batch-extract-text-from-pdfs__v2](./batch-extract-text-from-pdfs__v2.cs) | Batch PDF Text Extraction via PowerShell Wrapper | `PdfExtractor`, `ExtractText`, `GetText` | Demonstrates a minimal C# entry point that prepares the environment for a PowerShell script which... |
| [cancel-pdf-image-extraction](./cancel-pdf-image-extraction.cs) | Cancel PDF Text Extraction with a CancellationToken | `Document`, `PdfExtractor`, `BindPdf` | Shows how to extract text from a PDF page‑by‑page using Aspose.Pdf and abort the operation on use... |
| [check-pdf-contains-text](./check-pdf-contains-text.cs) | Check if PDF Contains Text via MemoryStream | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract PDF text into a MemoryStream and de... |
| [check-pdf-for-text-and-images](./check-pdf-for-text-and-images.cs) | Detect Text and Images in a PDF with PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to determine whether a PDF contains at le... |
| [check-pdf-text-only-by-detecting-images](./check-pdf-text-only-by-detecting-images.cs) | Detect Text‑Only PDF by Extracting Images | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using PdfExtractor and determining if the document ... |
| [configurable-pdf-extraction](./configurable-pdf-extraction.cs) | Configurable PDF Text, Image, and Attachment Extraction | `PdfExtractor`, `BindPdf`, `ExtractText` | Demonstrates how to extract text, images, and embedded attachments from a PDF using Aspose.Pdf wi... |
| [create-contact-sheet-pdf-from-extracted-images](./create-contact-sheet-pdf-from-extracted-images.cs) | Create Contact Sheet PDF from Extracted Images | `Document`, `Page`, `XImage` | Extracts all images from a source PDF and assembles them into a new PDF that displays the images ... |
| [create-pdf-summary-from-first-three-pages](./create-pdf-summary-from-first-three-pages.cs) | Create PDF Summary with First Three Pages Text | `Document`, `PdfExtractor`, `TextFragment` | Shows how to extract text from the first three pages of a PDF and generate a new PDF that contain... |
| [extract-all-images-from-pdf](./extract-all-images-from-pdf.cs) | Extract All Images from PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract every image from a PDF document using Aspose.Pdf.Facades.PdfExtractor... |
| [extract-attachments-compute-sha256](./extract-attachments-compute-sha256.cs) | Extract PDF Attachments and Verify with SHA-256 Hashes | `Document`, `FileSpecification`, `PdfException` | Loads a PDF, extracts all embedded attachments to a directory, then computes a SHA‑256 hash for e... |
| [extract-embedded-attachments-from-pdf](./extract-embedded-attachments-from-pdf.cs) | Extract Embedded File Attachments from PDF | `Document`, `EmbeddedFiles`, `FileSpecification` | Demonstrates how to load a PDF with Aspose.Pdf, enumerate its embedded file attachments, and writ... |
| [extract-images-and-compress-png-zip](./extract-images-and-compress-png-zip.cs) | Extract Images from PDF and Save as Compressed PNG | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-and-create-thumbnails](./extract-images-and-create-thumbnails.cs) | Extract Images from PDF and Create Thumbnails | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and generatin... |
| [extract-images-by-keyword](./extract-images-by-keyword.cs) | Extract Images from Pages Containing a Keyword | `Document`, `TextAbsorber`, `PdfExtractor` | Demonstrates scanning a PDF for a specific keyword, identifying the pages that contain it, and ex... |
| [extract-images-create-pdf-portfolio](./extract-images-create-pdf-portfolio.cs) | Extract Images from PDF and Create Image Portfolio PDF | `PdfExtractor`, `BindPdf`, `ExtractImage` | The example extracts all images from an existing PDF using Aspose.Pdf.Facades.PdfExtractor, then ... |
| [extract-images-create-sprite-sheet](./extract-images-create-sprite-sheet.cs) | Extract Images from PDF and Create Sprite Sheet | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF with Aspose.Pdf.Facades.PdfExtractor, saving them a... |
| [extract-images-first-page-to-byte-arrays](./extract-images-first-page-to-byte-arrays.cs) | Extract Images from First PDF Page to Byte Arrays | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from the first page of... |
| [extract-images-from-encrypted-pdf](./extract-images-from-encrypted-pdf.cs) | Extract Images from Encrypted PDF | `Document`, `PdfExtractor`, `BindPdf` | Shows how to open an encrypted PDF with a user password and extract all embedded images to PNG fi... |
| [extract-images-from-pages-png](./extract-images-from-pages-png.cs) | Extract Images from Specific PDF Pages as PNG | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates configuring PdfExtractor to extract images only from pages 5 through 10 of a PDF and... |
| [extract-images-from-pdf-to-temp-folder](./extract-images-from-pdf-to-temp-folder.cs) | Extract Images from PDF Using Aspose.Pdf | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf's PdfExtractor to extract all images from a PDF and save them ... |
| [extract-images-from-pdf-to-unc](./extract-images-from-pdf-to-unc.cs) | Extract Images from PDF to UNC Network Share | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates extracting all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and saving th... |
| [extract-images-from-pdf-to-zip](./extract-images-from-pdf-to-zip.cs) | Extract Images from PDF and Save to ZIP | `PdfExtractor`, `BindPdf`, `ExtractImage` | Shows how to use Aspose.Pdf.Facades.PdfExtractor to pull all images from a PDF and store them as ... |
| [extract-images-from-pdf-using-pdfextractor](./extract-images-from-pdf-using-pdfextractor.cs) | Extract Images from PDF Using PdfExtractor with Automatic Di... | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF file using Aspose.Pdf.Facades.PdfExtractor wrap... |
| [extract-images-from-pdf-with-guid-filenames](./extract-images-from-pdf-with-guid-filenames.cs) | Extract Images from PDF with GUID Filenames | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to extract all images from a PDF using Aspose.Pdf.Facades.PdfExtractor and save ... |
| [extract-images-from-specific-pdf-page](./extract-images-from-specific-pdf-page.cs) | Extract Images from a Single PDF Page | `PdfExtractor`, `BindPdf`, `StartPage` | Demonstrates using Aspose.Pdf.Facades.PdfExtractor to extract all images from a specific page by ... |
| [extract-images-html-gallery](./extract-images-html-gallery.cs) | Extract Images from PDF and Create HTML Gallery | `Document`, `PdfExtractor`, `BindPdf` | The example extracts all images from each page of a PDF using Aspose.Pdf's PdfExtractor, saves th... |
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
Updated: 2026-11-09 | Run: `20261109_231947_5da9d9`
<!-- AUTOGENERATED:END -->

---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

> **Basic operations** in PDF using C# / .NET -- **56** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **basic-operations** category.
This folder contains standalone C# examples for basic-operations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **basic-operations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (56/56 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/56 files)
- `using Aspose.Pdf.Facades;` (1/56 files)
- `using Aspose.Pdf.Optimization;` (1/56 files)
- `using System;` (56/56 files)
- `using System.IO;` (55/56 files)
- `using System.Collections.Generic;` (3/56 files)
- `using System.Text;` (2/56 files)
- `using System.Net.Http;` (1/56 files)
- `using System.Security.Cryptography;` (1/56 files)
- `using System.Text.Json;` (1/56 files)
- `using System.Threading.Tasks;` (1/56 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-blank-page-to-pdf](./add-blank-page-to-pdf.cs) | Add Blank Page to PDF | `Document`, `Pages`, `Add` | Shows how to load an existing PDF with Aspose.Pdf, insert an empty page at the end, and save the ... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata`, `Save` | Shows how to load a PDF, create a custom XMP metadata packet, attach it to the document using Asp... |
| [batch-convert-pdf-to-pdfa-with-report](./batch-convert-pdf-to-pdfa-with-report.cs) | Batch Convert PDFs to PDF/A‑1b with Compression and Report | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Loads each PDF from a folder, converts it to PDF/A‑1b using high compression, saves the converted... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `Convert`, `PdfFormat` | Shows how to iterate through a folder of PDF files, convert each to PDF/A‑1b compliance with Aspo... |
| [batch-convert-pdfs-to-pdfa-with-csv-report](./batch-convert-pdfs-to-pdfa-with-csv-report.cs) | Batch Convert PDFs to PDF/A-1b with CSV Report | `Document`, `PdfFormat`, `ConvertErrorAction` | Processes all PDF files in a folder, converts each to PDF/A‑1b using Aspose.Pdf, writes individua... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Decrypt`, `Save` | Demonstrates how to iterate through a folder of encrypted PDF files, open each using a shared own... |
| [batch-encrypt-pdfs-in-directory](./batch-encrypt-pdfs-in-directory.cs) | Batch Encrypt PDFs in a Directory | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt all PDF files in a folder using a common user and owner password, app... |
| [batch-encrypt-pdfs-with-deterministic-passwords](./batch-encrypt-pdfs-with-deterministic-passwords.cs) | Batch Encrypt PDFs with Deterministic Passwords | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with User Passwords | `Document`, `Encrypt`, `Save` | Demonstrates reading a JSON configuration, encrypting multiple PDF files with individual user pas... |
| [batch-split-pdf-by-page-ranges](./batch-split-pdf-by-page-ranges.cs) | Batch Split PDF into Sections by Page Ranges | `Document`, `Pages`, `Save` | Shows how to read page range definitions from a configuration file and split a PDF into separate ... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF with Default Settings using Aspose.Pdf | `Document`, `Save` | Demonstrates loading a PDF, saving it with Aspose.Pdf's default compression, and comparing the fi... |
| [compress-pdf-high-optimization](./compress-pdf-high-optimization.cs) | Compress PDF with High Optimization and Compare File Size | `Document`, `OptimizationOptions`, `All` | Loads a PDF, applies high‑level compression using Aspose.Pdf optimization options, saves the resu... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b preserving structure | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, configuring PDF/A‑1b conversion options, performing t... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A‑1b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1b compliance while logging conversion errors, and saves t... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF, converting it to PDF/A-1b compliance while preserving metadata, and s... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X-3 with CMYK Color Space | `Document`, `OutputIntent`, `Convert` | Shows how to convert a PDF to PDF/X-3 compliance while forcing all colors to CMYK using an ICC pr... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 Preserving ICC Profile | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to PDF/X‑3 format while keeping the exi... |
| [convert-pdf-to-pdfx3](./convert-pdf-to-pdfx3.cs) | Convert PDF to PDF/X-3 while Preserving Color Profiles | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/X-3 compliance (which retains embedded ICC color profi... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A-1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | Creates a new PDF, adds a text paragraph to the first page, converts the document to PDF/A‑1b com... |
| [create-pdfx3-document-three-pages](./create-pdfx3-document-three-pages.cs) | Create PDF/X‑3 Document with Three Pages | `Document`, `Add`, `Convert` | Shows how to create a new PDF, add three blank pages, convert it to PDF/X‑3 compliance, and save ... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF with User Password | `Document`, `Decrypt`, `Save` | Demonstrates opening a password‑protected PDF, decrypting it, and saving an unprotected copy usin... |
| [disable-font-embedding-when-saving-pdf](./disable-font-embedding-when-saving-pdf.cs) | Disable Font Embedding When Saving PDF | `Document`, `PdfSaveOptions`, `Save` | The example opens an existing PDF, sets a default font in PdfSaveOptions to substitute missing fo... |
| [download-pdf-from-url-and-save](./download-pdf-from-url-and-save.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save`, `Stream)` | Demonstrates downloading a PDF via HttpClient, loading it into an Aspose.Pdf Document from a stre... |
| [encrypt-pdf-aes128-verify-print-permissions](./encrypt-pdf-aes128-verify-print-permissions.cs) | Encrypt PDF with AES‑128 and Verify Print Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads a PDF, applies AES‑128 encryption with user and owner passwords, sets printing ... |
| [encrypt-pdf-aes256-no-print](./encrypt-pdf-aes256-no-print.cs) | Encrypt PDF with AES‑256 and No Printing Permission | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to apply AES‑256 encryption to a PDF using Aspose.Pdf, set no permissions (disab... |
| [encrypt-pdf-allow-form-filling](./encrypt-pdf-allow-form-filling.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `Encrypt` | The example loads a PDF, encrypts it with AES‑256, and sets permissions so that only form filling... |
| [encrypt-pdf-form-fill-permission](./encrypt-pdf-form-fill-permission.cs) | Encrypt PDF and Allow Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF with user and owner passwords, restrict permissions to form fil... |
| [encrypt-pdf-form-filling-permission](./encrypt-pdf-form-filling-permission.cs) | Encrypt PDF with Form Filling Permission Only | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using Aspose.Pdf, allowing only form filling and disabling cont... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only | `Document`, `Permissions`, `Encrypt` | Shows how to encrypt a PDF using Aspose.Pdf by applying only an owner password, giving the owner ... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt a PDF using 128‑bit RC4 encryption with Aspose.Pdf and compare the or... |
| ... | | | *and 26 more files* |

## Category Statistics
- Total examples: 56

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

> **Basic operations** in PDF using C# / .NET -- **57** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **basic-operations** category.
This folder contains standalone C# examples for basic-operations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **basic-operations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (57/57 files) ← category-specific
- `using Aspose.Pdf.Optimization;` (2/57 files)
- `using Aspose.Pdf.Text;` (2/57 files)
- `using Aspose.Pdf.Facades;` (1/57 files)
- `using System;` (57/57 files)
- `using System.IO;` (56/57 files)
- `using System.Text;` (3/57 files)
- `using System.Collections.Generic;` (1/57 files)
- `using System.Net.Http;` (1/57 files)
- `using System.Security.Cryptography;` (1/57 files)
- `using System.Text.Json;` (1/57 files)
- `using System.Threading.Tasks;` (1/57 files)

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
| [add-blank-page-to-pdf-document](./add-blank-page-to-pdf-document.cs) | Add Blank Page to PDF Document | `Document`, `Pages`, `PageCollection` | Shows how to load an existing PDF with Aspose.Pdf, append a new blank page, and save the modified... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata`, `Save` | Demonstrates loading an existing PDF, inserting a custom XMP metadata packet, and saving the docu... |
| [append-page-incremental-save](./append-page-incremental-save.cs) | Append Page to PDF Using Incremental Save | `Document`, `Page`, `TextFragment` | Demonstrates opening an existing PDF with read/write access, adding a new blank page with a text ... |
| [batch-convert-pdfs-to-pdfa-1b-high-compression](./batch-convert-pdfs-to-pdfa-1b-high-compression.cs) | Batch Convert PDFs to PDF/A-1b with High Compression | `Document`, `PdfFormatConversionOptions`, `ConvertErrorAction` | Demonstrates how to batch process PDF files, convert each to PDF/A-1b format with high compressio... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A-1b | `Document`, `Convert`, `PdfFormat` | Shows how to iterate over PDF files in a folder, convert each to PDF/A‑1b compliance with Aspose.... |
| [batch-convert-pdfs-to-pdfa-1b__v2](./batch-convert-pdfs-to-pdfa-1b__v2.cs) | Batch Convert PDFs to PDF/A-1b with CSV Log | `Document`, `Convert`, `PdfFormat` | Demonstrates converting multiple PDF files to PDF/A-1b using Aspose.Pdf, saving the converted fil... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Decrypt`, `Save` | Shows how to open multiple password‑protected PDF files using a shared owner password, remove the... |
| [batch-encrypt-pdfs-from-filename](./batch-encrypt-pdfs-from-filename.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf, generating deterministic passwor... |
| [batch-encrypt-pdfs-with-configurable-passwords](./batch-encrypt-pdfs-with-configurable-passwords.cs) | Batch Encrypt PDFs with Configurable Passwords | `Document`, `CryptoAlgorithm`, `Permissions` | Demonstrates how to read a list of PDF encryption jobs from a JSON configuration file, apply user... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with a User Password | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a directory using the same user and owner passwords, applying s... |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF Passwords with Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Shows how to open a password‑protected PDF, modify its user and owner passwords, and save the doc... |
| [change-pdf-version-to-1-5-incremental-update](./change-pdf-version-to-1-5-incremental-update.cs) | Change PDF Version to 1.5 with Incremental Update | `Document`, `PdfVersion`, `Pages` | Loads an existing PDF, creates a new document with PDF version 1.5, copies all pages, and saves t... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF with Default Settings | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates how to reduce a PDF file size by applying Aspose.Pdf's default optimization (object ... |
| [compress-pdf-high-optimization](./compress-pdf-high-optimization.cs) | Compress PDF with High Optimization Settings | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates loading a PDF, applying high‑level compression via OptimizationOptions, and saving t... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b Preserving Structure and Fonts | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to load a PDF with Aspose.Pdf, set conversion options for PDF/A-1b while keeping the or... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A‑1b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1b compliance while logging conversion errors, and saves t... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A-1b compliance while preserving its metadata, and sav... |
| [convert-pdf-to-pdfa1b__v2](./convert-pdf-to-pdfa1b__v2.cs) | Convert PDF to PDF/A‑1b with Font Embedding | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance (automatically embedding missing fonts... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X-3 with CMYK Color Space | `Document`, `PdfFormatConversionOptions`, `OutputIntent` | Shows how to load a PDF, convert it to PDF/X‑3 compliance while forcing all colors to CMYK using ... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 Preserving ICC Profiles | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to PDF/X‑3 while keeping any existing I... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A-1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | The example creates a new PDF document, adds a text paragraph to the first page, converts the fil... |
| [create-pdfx3-document-three-pages](./create-pdfx3-document-three-pages.cs) | Create PDF/X‑3 Document with Three Pages | `Document`, `Pages`, `Convert` | Shows how to create a new PDF, add three blank pages, convert it to PDF/X‑3 compliance, and save ... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF with User Password | `Document`, `Decrypt`, `Save` | Shows how to open a password‑protected PDF using Aspose.Pdf, decrypt it, and save an unprotected ... |
| [download-pdf-from-url-and-save-locally](./download-pdf-from-url-and-save-locally.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save` | Shows how to download a PDF file from a network URL using HttpClient, load it into an Aspose.Pdf ... |
| [encrypt-pdf-aes128-high-quality-print](./encrypt-pdf-aes128-high-quality-print.cs) | Encrypt PDF with AES‑128 and Enable High‑Quality Printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using AES‑128, set high‑quality printing permission, and verify the pe... |
| [encrypt-pdf-aes256-disable-printing](./encrypt-pdf-aes256-disable-printing.cs) | Encrypt PDF with AES‑256 and Disable Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256, set custom permissions to disallow printing, and... |
| [encrypt-pdf-allow-form-fill-disable-extraction](./encrypt-pdf-allow-form-fill-disable-extraction.cs) | Encrypt PDF with Form Filling Permission Only | `Document`, `Permissions`, `Encrypt` | Loads an existing PDF, encrypts it with user and owner passwords, and sets permissions to allow o... |
| [encrypt-pdf-allow-form-fill](./encrypt-pdf-allow-form-fill.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `Encrypt` | Loads a PDF, applies AES‑256 encryption with permissions limited to form filling, and saves the p... |
| [encrypt-pdf-owner-password-aes256](./encrypt-pdf-owner-password-aes256.cs) | Encrypt PDF with Owner Password Only (AES-256) | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using Aspose.Pdf by setting only an owner password, disabling a... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using 128‑bit RC4 encryption with Aspose.Pdf, then checks that ... |
| ... | | | *and 27 more files* |

## Category Statistics
- Total examples: 57

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

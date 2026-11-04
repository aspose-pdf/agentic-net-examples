---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

> **Basic operations** in PDF using C# / .NET -- **56** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Security;` (6/56 files)
- `using Aspose.Pdf.Optimization;` (2/56 files)
- `using Aspose.Pdf.Text;` (2/56 files)
- `using System;` (56/56 files)
- `using System.IO;` (56/56 files)
- `using System.Collections.Generic;` (2/56 files)
- `using System.Net;` (1/56 files)
- `using System.Text;` (1/56 files)
- `using System.Text.Json;` (1/56 files)

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
| [add-blank-page-to-pdf](./add-blank-page-to-pdf.cs) | Add Blank Page to PDF | `Document`, `Pages`, `Add` | Shows how to load an existing PDF, append a new blank page, and save the modified document using ... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `Metadata`, `Save` | Loads a PDF file, inserts a custom XMP metadata entry, and saves the document while preserving al... |
| [batch-convert-pdf-to-pdfa-with-report](./batch-convert-pdf-to-pdfa-with-report.cs) | Batch Convert PDFs to PDF/A‑1b with Compression | `Document`, `OptimizeResources`, `Convert` | Demonstrates processing a folder of PDF files, applying resource optimization, converting each do... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `Convert`, `PdfFormat` | Shows how to iterate over PDF files in a folder, convert each to PDF/A‑1b compliance with Aspose.... |
| [batch-convert-pdfs-to-pdfa-with-csv-report](./batch-convert-pdfs-to-pdfa-with-csv-report.cs) | Batch Convert PDFs to PDF/A-1b with CSV Log | `Document`, `Convert`, `PdfFormat` | Demonstrates iterating over PDF files in a folder, converting each to PDF/A‑1b using Aspose.Pdf, ... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Decrypt Multiple PDFs Using Owner Password | `Document`, `Document(string, string)`, `Decrypt()` | Demonstrates opening encrypted PDF files with a shared owner password, removing their encryption,... |
| [batch-encrypt-pdfs-in-directory](./batch-encrypt-pdfs-in-directory.cs) | Batch Encrypt PDFs with Password | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt every PDF in a folder using a user and owner password, and save the e... |
| [batch-encrypt-pdfs-with-deterministic-passwords](./batch-encrypt-pdfs-with-deterministic-passwords.cs) | Batch Encrypt PDFs with File‑Name Based Passwords | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt multiple PDF files using Aspose.Pdf, generating a unique password from each ... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch PDF Encryption with Configurable Passwords | `Document`, `Encrypt`, `Permissions` | The example reads a JSON configuration file that defines multiple encryption jobs, encrypts each ... |
| [batch-split-pdf-by-page-ranges](./batch-split-pdf-by-page-ranges.cs) | Split PDF into Sections by Page Ranges | `Document`, `Pages`, `Save` | Demonstrates how to read page‑range definitions from a text file and create separate PDF files fo... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF Using Default Settings | `Document`, `Save` | Loads a PDF with Aspose.Pdf, saves it without explicit compression options (using default compres... |
| [compress-pdf-high-optimization](./compress-pdf-high-optimization.cs) | Save PDF with High Compression and Compare File Size | `Document`, `OptimizationOptions`, `All` | Demonstrates applying high‑level compression to a PDF using Aspose.Pdf's OptimizationOptions, sav... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b with Aspose.Pdf | `Document`, `PdfFormat`, `ConvertErrorAction` | Shows how to load a PDF, convert it to PDF/A‑1b compliance while preserving the original structur... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A-1b Compliance | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF with Aspose.Pdf, convert it to PDF/A‑1b compliance while generating a con... |
| [convert-pdf-to-pdfa1b-with-font-embedding](./convert-pdf-to-pdfa1b-with-font-embedding.cs) | Convert PDF to PDF/A‑1b with Embedded Fonts | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance (embedding any missing fonts), and sav... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Demonstrates converting an existing PDF to PDF/A-1b compliance using Aspose.Pdf, preserving docum... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X-3 with CMYK Color Spaces | `Document`, `PdfFormatConversionOptions`, `Convert` | Loads an existing PDF, converts it to PDF/X‑3 format to meet PDF/X‑3 compliance (including CMYK c... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 with ICC Profile Preservation | `Document`, `Convert`, `Save` | Shows how to load a PDF using Aspose.Pdf, convert it to PDF/X‑3 while retaining existing ICC colo... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A‑1b Document with Text Paragraph | `Document`, `TextFragment`, `PdfFormat` | Demonstrates how to create a PDF, add a text paragraph to the first page, and convert it to PDF/A... |
| [create-pdfx3-document-three-pages](./create-pdfx3-document-three-pages.cs) | Create PDF/X‑3 Document with Three Pages | `Document`, `Pages`, `Convert` | Demonstrates how to create a new PDF, add three blank pages, convert it to PDF/X‑3 format with a ... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt PDF with User Password using Aspose.Pdf | `Document`, `Decrypt`, `Save` | Shows how to open an encrypted PDF by supplying the user password, remove its encryption, and sav... |
| [disable-font-embedding-when-saving-pdf](./disable-font-embedding-when-saving-pdf.cs) | Disable Font Embedding in PDF and Save | `Document`, `OptimizationOptions`, `OptimizeResources` | Shows how to load a PDF, turn off font embedding using optimization options, and save the resulti... |
| [download-pdf-from-url-and-save](./download-pdf-from-url-and-save.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save` | Demonstrates how to fetch a PDF file from a network URL using a WebRequest, load it into an Aspos... |
| [encrypt-pdf-aes128-verify-print-permissions](./encrypt-pdf-aes128-verify-print-permissions.cs) | Encrypt PDF with AES‑128 and Verify Print Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑128, restrict permissions to high‑quality printing, a... |
| [encrypt-pdf-aes256-no-print](./encrypt-pdf-aes256-no-print.cs) | Encrypt PDF with AES-256 and No Printing Permission | `Document`, `Permissions`, `Encrypt` | The example encrypts a PDF using AES‑256 with user and owner passwords, disables all permissions ... |
| [encrypt-pdf-allow-form-filling](./encrypt-pdf-allow-form-filling.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates opening a PDF, applying AES‑256 encryption with permissions that restrict the docume... |
| [encrypt-pdf-form-fill-permission](./encrypt-pdf-form-fill-permission.cs) | Encrypt PDF with User Password and Form-Fill Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using user and owner passwords, allow only form filling, and disable p... |
| [encrypt-pdf-form-filling-permission](./encrypt-pdf-form-filling-permission.cs) | Encrypt PDF with Form Filling Permission Only | `Document`, `Permissions`, `Encrypt` | Shows how to encrypt a PDF using Aspose.Pdf, allowing only form filling (ModifyContent) and disab... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt a PDF using only an owner password, granting full rights to the owner... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to encrypt an existing PDF using 128‑bit RC4 encryption with Aspose.Pdf and then confir... |
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
Updated: 2026-11-03 | Run: `20261103_234303_6e2b26`
<!-- AUTOGENERATED:END -->

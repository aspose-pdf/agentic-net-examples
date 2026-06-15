---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

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
- `using Aspose.Pdf.Text;` (4/57 files)
- `using Aspose.Pdf.Optimization;` (2/57 files)
- `using System;` (57/57 files)
- `using System.IO;` (57/57 files)
- `using System.Collections.Generic;` (2/57 files)
- `using System.Linq;` (1/57 files)
- `using System.Net;` (1/57 files)
- `using System.Security.Cryptography;` (1/57 files)
- `using System.Text.Json;` (1/57 files)

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
| [add-blank-page-to-pdf](./add-blank-page-to-pdf.cs) | Add Blank Page to PDF Document | `Document`, `Pages`, `PageCollection` | Shows how to load an existing PDF, insert a new blank page at the end, and save the updated docum... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, creating a custom XMP metadata block, attaching it to... |
| [batch-convert-pdfa-compression](./batch-convert-pdfa-compression.cs) | Batch Convert PDFs to PDF/A-1b with Compression | `Document`, `Page`, `TextFragment` | Creates sample PDFs, converts each to PDF/A‑1b format with resource optimization for high compres... |
| [batch-convert-pdfa](./batch-convert-pdfa.cs) | Batch Convert PDFs to PDF/A-1b with CSV Log | `Document`, `Convert`, `Save` | Creates sample PDFs, converts each to PDF/A‑1b format, saves conversion logs, and records results... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `Convert`, `Save` | Demonstrates how to iterate over PDF files in a folder, convert each to PDF/A‑1b compliance with ... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Decrypt`, `Save` | Shows how to open multiple password‑protected PDF files using a shared owner password, remove the... |
| [batch-encrypt-pdfs-with-password](./batch-encrypt-pdfs-with-password.cs) | Batch Encrypt PDFs with a User Password | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf by applying the same user ... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with User and Owner Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Reads a JSON configuration file containing multiple PDF encryption jobs, applies user and owner p... |
| [batch-pdf-encrypt-with-secure-log](./batch-pdf-encrypt-with-secure-log.cs) | Batch Encrypt PDFs with File‑Name Derived Passwords and Secu... | `Document`, `Encrypt`, `Save` | The example encrypts all PDF files in a folder using passwords generated from each file name, sav... |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF User and Owner Passwords with Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Shows how to open a password‑protected PDF using the owner password, change the user and owner pa... |
| [change-pdf-version-to-1-5-incremental-update](./change-pdf-version-to-1-5-incremental-update.cs) | Change PDF Version to 1.5 with Incremental Update | `Document`, `Convert`, `Save` | Loads an existing PDF, converts it to PDF version 1.5 while logging conversion issues, and saves ... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF Using Default Settings and Compare File Size | `Document`, `Save`, `OptimizeResources` | Loads a PDF, saves it with Aspose.Pdf's default compression, then reports the original and compre... |
| [compress-pdf-high-compression](./compress-pdf-high-compression.cs) | Compress PDF with High Compression Level | `Document`, `OptimizationOptions`, `All` | Loads a PDF, applies full optimization (high compression) using OptimizationOptions, saves it wit... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to load a PDF with Aspose.Pdf, convert it to PDF/A-1b format while preserving the origi... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A-1b with Font Embedding | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF, converting it to PDF/A‑1b compliance (which embeds any missing fonts)... |
| [convert-pdf-to-pdfa-1b__v3](./convert-pdf-to-pdfa-1b__v3.cs) | Convert PDF to PDF/A-1b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1b compliance while logging any conversion errors, and sav... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance while preserving metadata, and save th... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X‑3 with CMYK Color Space | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/X‑3 compliance while forcing all colors to CMYK using ... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X-3 Preserving ICC Profiles | `Document`, `PdfFormatConversionOptions`, `Convert` | Shows how to convert a PDF to PDF/X-3 with Aspose.Pdf while retaining existing ICC color profiles... |
| [convert-pdf-to-pdfx3](./convert-pdf-to-pdfx3.cs) | Convert PDF to PDF/X‑3 with Color Profiles | `Document`, `Convert`, `PdfFormat` | Demonstrates converting an existing PDF to PDF/X‑3 compliance using Aspose.Pdf while preserving e... |
| [convert-pdf-to-version-1-4-from-memorystream](./convert-pdf-to-version-1-4-from-memorystream.cs) | Convert PDF to Version 1.4 from MemoryStream | `Document`, `Convert`, `PdfFormat` | The example loads a PDF file into a MemoryStream, creates an Aspose.Pdf Document from it, convert... |
| [create-pdfa1b-document-with-text](./create-pdfa1b-document-with-text.cs) | Create PDF/A‑1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | Demonstrates creating a PDF, adding a text paragraph to the first page, converting the document t... |
| [create-pdfx3-multi-page](./create-pdfx3-multi-page.cs) | Create PDF/X‑3 Document with Multiple Pages | `Document`, `Convert`, `PdfFormat` | Shows how to create a PDF, add three pages with simple text, convert the document to PDF/X‑3 comp... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF with Aspose.Pdf | `Document`, `Decrypt`, `Save` | Demonstrates opening a password‑protected PDF, removing its encryption, and saving an unprotected... |
| [download-pdf-from-url](./download-pdf-from-url.cs) | Download PDF from URL and Save Locally | `Document`, `ComHelper`, `WebRequest` | Demonstrates fetching a PDF via a WebRequest stream, loading it with Aspose.Pdf, and saving it to... |
| [encrypt-pdf-aes128-high-quality-printing](./encrypt-pdf-aes128-high-quality-printing.cs) | Encrypt PDF with AES-128 and High-Quality Printing Permissio... | `Document`, `Encrypt`, `Decrypt` | Demonstrates how to encrypt a PDF using AES‑128, set permissions for high‑quality printing, and v... |
| [encrypt-pdf-aes256-no-printing](./encrypt-pdf-aes256-no-printing.cs) | Encrypt PDF with AES‑256 and Disable Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply AES‑256 encryption with no permissions (including printing)... |
| [encrypt-pdf-allow-form-fill](./encrypt-pdf-allow-form-fill.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `Encrypt` | Demonstrates how to open a PDF with Aspose.Pdf, set permissions so only form filling is allowed, ... |
| [encrypt-pdf-form-filling-permission](./encrypt-pdf-form-filling-permission.cs) | Encrypt PDF with Form Filling Permission Only | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using Aspose.Pdf, allowing only form filling while disabling co... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf by setting only an owner password, granting full acce... |
| ... | | | *and 27 more files* |

## Category Statistics
- Total examples: 57

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-14 | Run: `20260614_232732_57e9be`
<!-- AUTOGENERATED:END -->

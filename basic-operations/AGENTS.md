---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

> **Basic operations** in PDF using C# / .NET -- **53** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **basic-operations** category.
This folder contains standalone C# examples for basic-operations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **basic-operations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (53/53 files) ← category-specific
- `using Aspose.Pdf.Security;` (6/53 files)
- `using Aspose.Pdf.Text;` (1/53 files)
- `using System;` (53/53 files)
- `using System.IO;` (53/53 files)
- `using System.Net.Http;` (2/53 files)
- `using System.Text;` (2/53 files)
- `using System.Threading.Tasks;` (2/53 files)
- `using System.Security.Cryptography;` (1/53 files)

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
| [add-blank-page-to-pdf](./add-blank-page-to-pdf.cs) | Add Blank Page to PDF Document | `Document`, `Pages`, `PageCollection` | Demonstrates loading an existing PDF, appending a blank page, and saving the updated file using A... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `Metadata`, `RegisterNamespaceUri` | Shows how to load a PDF, register a namespace, add custom XMP metadata properties, and save the d... |
| [batch-convert-pdf-to-pdfa-with-report](./batch-convert-pdf-to-pdfa-with-report.cs) | Batch Convert PDFs to PDF/A-1b with Compression | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates how to convert multiple PDF files to PDF/A-1b format with file-size optimization and... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A-1b | `Document`, `Convert`, `PdfFormat` | Shows how to iterate through a folder of PDF files, convert each to PDF/A‑1b compliance with Aspo... |
| [batch-convert-pdfs-to-pdfa-with-csv-report](./batch-convert-pdfs-to-pdfa-with-csv-report.cs) | Batch Convert PDFs to PDF/A-1b with CSV Logging | `Document`, `Convert`, `PdfFormat` | Demonstrates converting a collection of PDF files to PDF/A-1b format using Aspose.Pdf and recordi... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Decrypt`, `Save` | Demonstrates iterating over a folder of password‑protected PDF files, opening each with a shared ... |
| [batch-encrypt-pdfs-in-directory](./batch-encrypt-pdfs-in-directory.cs) | Batch Encrypt PDFs with a User Password | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a folder using Aspose.Pdf, applying the same user and owner pas... |
| [batch-encrypt-pdfs-with-deterministic-passwords](./batch-encrypt-pdfs-with-deterministic-passwords.cs) | Batch Encrypt PDFs with Filename‑Based Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt multiple PDF files using Aspose.Pdf, generating a deterministic password fro... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with User Passwords from Config | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates reading a configuration file to apply individual user passwords to multiple PDFs, en... |
| [batch-split-pdf-by-page-ranges](./batch-split-pdf-by-page-ranges.cs) | Batch Split PDF into Sections Using Page Ranges | `Document`, `Save`, `Pages` | Demonstrates how to read page‑range definitions from a text file and split a source PDF into mult... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF Using Default Settings | `Document`, `Save` | Loads a PDF file and saves it with Aspose.Pdf's default compression, then compares the original a... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1B Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1B compliance using Aspose.Pdf, and saves the compliant do... |
| [convert-pdf-to-pdfa1b-with-font-embedding](./convert-pdf-to-pdfa1b-with-font-embedding.cs) | Convert PDF to PDF/A-1b with Font Embedding | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance while embedding missing fonts, and sav... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A‑1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance while automatically preserving metadat... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X‑3 with CMYK Color Space | `Document`, `PdfFormatConversionOptions`, `Convert` | Demonstrates loading a PDF, converting it to PDF/X‑3 format and optionally forcing CMYK color spa... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 with ICC Profile Preservation | `Document`, `Convert`, `Save` | Demonstrates loading a PDF, converting it to PDF/X‑3 while retaining embedded ICC color profiles ... |
| [convert-pdf-to-pdfx3](./convert-pdf-to-pdfx3.cs) | Convert PDF to PDF/X‑3 while Preserving Color Profiles | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/X‑3 compliance (keeping embedded color profiles), and ... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A‑1b Document with Text Paragraph | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add a text paragraph, convert it to PDF/A‑1b compliance, and sa... |
| [create-pdfx3-document-three-pages](./create-pdfx3-document-three-pages.cs) | Create PDF/X‑3 Document with Multiple Pages | `Document`, `Pages`, `Add` | Shows how to create a new PDF, add three blank pages, convert it to PDF/X‑3 format, and save the ... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF with User Password | `Document`, `Decrypt()`, `Save()` | Loads an encrypted PDF using the provided user password, removes its encryption, and saves an unp... |
| [download-pdf-from-url-and-save](./download-pdf-from-url-and-save.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save` | Demonstrates how to download a PDF file from a network URL using HttpClient, load it into an Aspo... |
| [encrypt-pdf-aes128-verify-print-permissions](./encrypt-pdf-aes128-verify-print-permissions.cs) | Encrypt PDF with AES‑128 and Verify Print Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑128, set high‑quality printing permission, and then v... |
| [encrypt-pdf-aes256-no-print](./encrypt-pdf-aes256-no-print.cs) | Encrypt PDF with AES‑256 and Verify No Printing Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to apply AES‑256 encryption to a PDF with all permissions disabled (no printing)... |
| [encrypt-pdf-allow-form-filling](./encrypt-pdf-allow-form-filling.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to open a PDF, restrict permissions to form filling only, encrypt it with AES‑25... |
| [encrypt-pdf-form-fill-permission](./encrypt-pdf-form-fill-permission.cs) | Encrypt PDF with User Password and Restrict to Form Filling | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using a user and owner password, allow only form‑filling permis... |
| [encrypt-pdf-form-filling-permission](./encrypt-pdf-form-filling-permission.cs) | Encrypt PDF with Form Fill Permission Only | `Document`, `Permissions`, `Encrypt` | Demonstrates how to encrypt a PDF, allow only form filling, and disable content extraction using ... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf by setting an owner password, leaving the user passwo... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying 128‑bit RC4 encryption with specific permissions, saving the... |
| [encrypt-pdf-rc4-no-copy-permission](./encrypt-pdf-rc4-no-copy-permission.cs) | Encrypt PDF with RC4 and Disable Copying | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates encrypting a PDF using RC4 128‑bit, setting no permissions to block copying, and pro... |
| [encrypt-pdf-stream](./encrypt-pdf-stream.cs) | Encrypt PDF from URL and Save to Stream | `Document`, `Permissions`, `CryptoAlgorithm` | Downloads a PDF from a given URL, applies AES‑256 encryption with specified user and owner passwo... |
| ... | | | *and 23 more files* |

## Category Statistics
- Total examples: 53

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-24 | Run: `20260924_222140_ec8685`
<!-- AUTOGENERATED:END -->

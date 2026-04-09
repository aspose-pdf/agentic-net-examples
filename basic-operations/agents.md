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
- `using Aspose.Pdf.Optimization;` (1/57 files)
- `using Aspose.Pdf.Security;` (1/57 files)
- `using Aspose.Pdf.Tagged;` (1/57 files)
- `using Aspose.Pdf.Text;` (1/57 files)
- `using System;` (57/57 files)
- `using System.IO;` (56/57 files)
- `using System.Collections.Generic;` (1/57 files)
- `using System.Net.Http;` (1/57 files)
- `using System.Text;` (1/57 files)
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
| [add-blank-page-to-pdf-document](./add-blank-page-to-pdf-document.cs) | Add Blank Page to PDF Document | `Document`, `Add`, `Save` | Shows how to load an existing PDF with Aspose.Pdf, insert a new blank page, and save the modified... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata`, `Save` | Loads an existing PDF, injects a custom XMP metadata block, and saves the document while preservi... |
| [batch-convert-pdfs-to-pdfa-1b-with-compression](./batch-convert-pdfs-to-pdfa-1b-with-compression.cs) | Batch Convert PDFs to PDF/A‑1b with Compression and Reportin... | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates how to convert multiple PDF files to PDF/A‑1b format with high compression using Asp... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `Convert`, `Save` | Demonstrates how to process all PDF files in a folder, convert each to PDF/A‑1b format using Aspo... |
| [batch-convert-pdfs-to-pdfa1b-csv-report](./batch-convert-pdfs-to-pdfa1b-csv-report.cs) | Batch Convert PDFs to PDF/A-1b with CSV Report | `Document`, `Convert`, `Save` | Shows how to convert multiple PDF files to PDF/A-1b using Aspose.Pdf, save the converted files, a... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Decrypt`, `Save` | Shows how to open multiple password‑protected PDF files using a shared owner password, decrypt ea... |
| [batch-encrypt-pdfs-with-derived-passwords](./batch-encrypt-pdfs-with-derived-passwords.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf, generating a deterministic passw... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with a User Password | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a directory using Aspose.Pdf, applying the same user password a... |
| [batch-pdf-encryption-from-json-config](./batch-pdf-encryption-from-json-config.cs) | Batch PDF Encryption from JSON Config | `Document`, `Encrypt`, `Save` | Demonstrates reading a JSON configuration of encryption tasks, loading each PDF with Aspose.Pdf, ... |
| [batch-split-pdf-by-page-ranges](./batch-split-pdf-by-page-ranges.cs) | Batch Split PDF into Sections by Page Ranges | `Document`, `Pages`, `Save` | Shows how to read page range definitions from a configuration file and split a PDF into separate ... |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF Passwords Using Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Shows how to open a password‑protected PDF with the owner password, change both the user and owne... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF with Default Optimization | `Document`, `OptimizeResources`, `Save` | Shows how to load a PDF, apply Aspose.Pdf's default resource optimization, save the compressed fi... |
| [compress-pdf-high-compression](./compress-pdf-high-compression.cs) | Compress PDF with High Compression Level | `Document`, `OptimizeResources`, `Save` | Demonstrates how to load a PDF, apply high‑level compression using Aspose.Pdf optimization option... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b with Embedded Fonts Preservation | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to PDF/A-1b while preserving the origin... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A‑1b with Font Embedding | `Document`, `Convert`, `Save` | Shows how to load a PDF, convert it to PDF/A‑1b compliance (embedding missing fonts) using Aspose... |
| [convert-pdf-to-pdfa-1b__v3](./convert-pdf-to-pdfa-1b__v3.cs) | Convert PDF to PDF/A-1B Compliance | `Document`, `Convert`, `Save` | Loads a PDF file, converts it to PDF/A‑1B compliance using Aspose.Pdf, and saves the resulting do... |
| [convert-pdf-to-pdfa1b-preserve-metadata](./convert-pdf-to-pdfa1b-preserve-metadata.cs) | Convert PDF to PDF/A-1b while Preserving Metadata | `Document`, `Convert`, `Save` | Demonstrates loading an existing PDF, converting it to PDF/A‑1b compliance using Aspose.Pdf, and ... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X‑3 with CMYK Color Space | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF, converting it to PDF/X‑3 compliance while forcing all colors to CMYK ... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 while Preserving ICC Profile | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates how to convert a regular PDF to PDF/X‑3 using Aspose.Pdf, keeping the original ICC c... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A‑1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | Demonstrates creating a PDF, adding a wrapped text paragraph to a page, converting the document t... |
| [create-pdfx3-document-multiple-pages](./create-pdfx3-document-multiple-pages.cs) | Create PDF/X‑3 Document with Multiple Pages | `Document`, `Add`, `Convert` | Demonstrates creating a new PDF, adding three blank pages, converting it to PDF/X‑3 compliance, a... |
| [decrypt-pdf-with-user-password](./decrypt-pdf-with-user-password.cs) | Decrypt Encrypted PDF with User Password | `Document`, `Decrypt`, `Save` | Shows how to open a password‑protected PDF using Aspose.Pdf, decrypt it, and save an unprotected ... |
| [download-pdf-from-url-and-save-locally](./download-pdf-from-url-and-save-locally.cs) | Download PDF from URL and Save Locally | `Document`, `Save` | Demonstrates how to download a PDF from a network URL using HttpClient, load it into an Aspose.Pd... |
| [encrypt-pdf-aes128-verify-print-permissions](./encrypt-pdf-aes128-verify-print-permissions.cs) | Encrypt PDF with AES‑128 and Verify Print Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF using AES‑128, allowing high‑quality printing, and verifying the en... |
| [encrypt-pdf-aes256-disable-printing](./encrypt-pdf-aes256-disable-printing.cs) | Encrypt PDF with AES‑256 and Disable Printing | `Document`, `Encrypt`, `Save` | Demonstrates applying AES‑256 encryption to a PDF, restricting printing permissions, and verifyin... |
| [encrypt-pdf-form-fill-permission](./encrypt-pdf-form-fill-permission.cs) | Encrypt PDF with Form Fill Permission Only | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using Aspose.Pdf, allowing only form filling while disabling co... |
| [encrypt-pdf-form-fill-permission__v2](./encrypt-pdf-form-fill-permission__v2.cs) | Encrypt PDF with Form‑Fill Only Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying AES‑256 encryption that restricts permissions to only form f... |
| [encrypt-pdf-from-stream](./encrypt-pdf-from-stream.cs) | Encrypt PDF from Stream and Write to Output Stream | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF from a network stream, applying AES‑256 encryption with specific permi... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using Aspose.Pdf by providing only an owner password, leaving the user... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, applying 128‑bit RC4 encryption with user and owner p... |
| ... | | | *and 27 more files* |

## Category Statistics
- Total examples: 57

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-09 | Run: `20260409_101820_90465f`
<!-- AUTOGENERATED:END -->

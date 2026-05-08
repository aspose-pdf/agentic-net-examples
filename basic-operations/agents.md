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

- `using Aspose.Pdf;` (56/56 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/56 files)
- `using Aspose.Pdf.Optimization;` (1/56 files)
- `using Aspose.Pdf.Security;` (1/56 files)
- `using System;` (56/56 files)
- `using System.IO;` (54/56 files)
- `using System.Runtime.InteropServices;` (2/56 files)
- `using System.Text;` (2/56 files)
- `using System.Collections.Generic;` (1/56 files)
- `using System.Net.Http;` (1/56 files)
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
| [add-blank-page-to-pdf-document](./add-blank-page-to-pdf-document.cs) | Add Blank Page to PDF Document | `Document`, `Pages`, `PageCollection` | Demonstrates how to load an existing PDF, append a new blank page, and save the updated file usin... |
| [add-xmp-metadata-to-pdf](./add-xmp-metadata-to-pdf.cs) | Add XMP Metadata to PDF and Save | `Document`, `SetXmpMetadata`, `Save` | Loads a PDF, builds a custom XMP packet, attaches it to the document, and saves the file while pr... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A-1b | `Document`, `Convert`, `PdfFormat` | Shows how to iterate over PDF files in a folder, convert each to PDF/A‑1b using Aspose.Pdf, and w... |
| [batch-convert-pdfs-to-pdfa-1b__v2](./batch-convert-pdfs-to-pdfa-1b__v2.cs) | Batch Convert PDFs to PDF/A-1b with High Compression | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to iterate over PDF files in a folder, convert each to PDF/A‑1b using Aspose.Pdf with f... |
| [batch-convert-pdfs-to-pdfa-1b__v3](./batch-convert-pdfs-to-pdfa-1b__v3.cs) | Batch Convert PDFs to PDF/A-1b with CSV Log | `Document`, `Convert`, `Save` | Demonstrates how to process a folder of PDF files, convert each to PDF/A‑1b using Aspose.Pdf, and... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs | `Document`, `ctor(string, string)`, `Decrypt` | Shows how to open a collection of encrypted PDF files with a shared owner password, remove their ... |
| [batch-encrypt-pdfs-from-config](./batch-encrypt-pdfs-from-config.cs) | Batch Encrypt PDFs with Passwords from Configuration | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to read a JSON configuration file containing multiple encryption jobs and encryp... |
| [batch-encrypt-pdfs-with-filename-passwords](./batch-encrypt-pdfs-with-filename-passwords.cs) | Batch Encrypt PDFs with File‑Name Based Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt multiple PDF files using Aspose.Pdf, generate a unique password from each fi... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with a User Password | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a folder using Aspose.Pdf by applying the same user and owner p... |
| [compress-pdf-default-settings](./compress-pdf-default-settings.cs) | Compress PDF Using Default Settings | `Document`, `Optimize`, `Save` | Shows how to load a PDF with Aspose.Pdf, save it using the default compression options, and compa... |
| [compress-pdf-high-optimization](./compress-pdf-high-optimization.cs) | Compress PDF with High Optimization Level | `Document`, `OptimizationOptions`, `OptimizeResources` | Loads a PDF, applies aggressive optimization (object compression, font subsetting, unused object ... |
| [convert-pdf-to-pdfa-1b-embed-fonts](./convert-pdf-to-pdfa-1b-embed-fonts.cs) | Convert PDF to PDF/A-1b with Embedded Fonts | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance (embedding any missing fonts), and sav... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `Save` | Shows how to load a PDF, convert it to PDF/A‑1b compliance while preserving the document's metada... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A-1b | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF with Aspose.Pdf, convert it to PDF/A‑1b compliance, and save the resultin... |
| [convert-pdf-to-pdfa-1b__v3](./convert-pdf-to-pdfa-1b__v3.cs) | Convert PDF to PDF/A-1b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1b compliance while logging conversion errors, and saves t... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X-3 with CMYK Color Space | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to load a PDF, set up PdfFormatConversionOptions for PDF/X-3 compliance with a CMYK ICC... |
| [convert-pdf-to-pdfx3-preserving-icc-profiles](./convert-pdf-to-pdfx3-preserving-icc-profiles.cs) | Convert PDF to PDF/X‑3 Preserving ICC Profiles | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates how to convert an existing PDF to PDF/X‑3 format using Aspose.Pdf while keeping any ... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A-1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | The example creates a new PDF document, adds a text paragraph to the first page, and converts the... |
| [create-pdfx3-document-multiple-pages](./create-pdfx3-document-multiple-pages.cs) | Create PDF/X‑3 Document with Multiple Pages | `Document`, `Add`, `Convert` | Shows how to create a new PDF, add three blank pages, convert it to PDF/X‑3 format, and save the ... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF Using User Password | `Document`, `Decrypt`, `Save` | Demonstrates opening a password‑protected PDF with Aspose.Pdf, removing its encryption, and savin... |
| [download-pdf-from-url-and-save-locally](./download-pdf-from-url-and-save-locally.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save` | Demonstrates how to download a PDF file using HttpClient, load it into an Aspose.Pdf Document fro... |
| [encrypt-pdf-aes128-set-print-permissions](./encrypt-pdf-aes128-set-print-permissions.cs) | Encrypt PDF with AES‑128 and Set Printing Permissions | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using AES‑128 with Aspose.Pdf, assign printing and high‑quality... |
| [encrypt-pdf-aes256-disable-printing](./encrypt-pdf-aes256-disable-printing.cs) | Encrypt PDF with AES‑256 and Disable Printing | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using AES‑256, set no permissions to disable printing, and veri... |
| [encrypt-pdf-allow-form-filling](./encrypt-pdf-allow-form-filling.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, restrict permissions to form filling only, encrypt it with AES‑25... |
| [encrypt-pdf-form-fill-permission](./encrypt-pdf-form-fill-permission.cs) | Encrypt PDF with Password and Restrict to Form Filling | `Document`, `Permissions`, `Encrypt` | Demonstrates how to load an existing PDF, encrypt it with user and owner passwords using AES‑256,... |
| [encrypt-pdf-form-filling-permission](./encrypt-pdf-form-filling-permission.cs) | Encrypt PDF with Form Filling Permission Only | `Document`, `Permissions`, `Encrypt` | Demonstrates loading a PDF, applying AES‑256 encryption that permits only form filling (disabling... |
| [encrypt-pdf-owner-password-aes256](./encrypt-pdf-owner-password-aes256.cs) | Encrypt PDF with Owner Password Only (AES‑256) | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf by applying an owner password only, using AES‑256 enc... |
| [encrypt-pdf-rc4-128-verify-size](./encrypt-pdf-rc4-128-verify-size.cs) | Encrypt PDF with 128‑bit RC4 and Verify Size Increase | `Document`, `Permissions`, `CryptoAlgorithm` | Loads a PDF, applies 128‑bit RC4 encryption with specific permissions, saves the encrypted file, ... |
| [encrypt-pdf-rc4-verify-permissions](./encrypt-pdf-rc4-verify-permissions.cs) | Encrypt PDF with RC4 and Verify Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | The example encrypts a PDF using the RC4 128‑bit algorithm, restricts permissions to printing onl... |
| [encrypt-pdf-stream](./encrypt-pdf-stream.cs) | Encrypt PDF Stream with Passwords | `Document`, `Encrypt`, `Save` | Shows how to read a PDF from a stream, apply AES‑256 encryption with user and owner passwords and... |
| ... | | | *and 26 more files* |

## Category Statistics
- Total examples: 56

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for basic-operations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_135456_55d199`
<!-- AUTOGENERATED:END -->

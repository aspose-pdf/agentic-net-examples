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

- `using Aspose.Pdf;` (55/56 files) ← category-specific
- `using Aspose.Pdf.Security;` (3/56 files)
- `using Aspose.Pdf.Optimization;` (2/56 files)
- `using Aspose.Pdf.Text;` (2/56 files)
- `using Aspose.Pdf.Facades;` (1/56 files)
- `using System;` (56/56 files)
- `using System.IO;` (54/56 files)
- `using System.Text;` (3/56 files)
- `using System.Collections.Generic;` (2/56 files)
- `using System.Net.Http;` (2/56 files)
- `using System.Runtime.InteropServices;` (2/56 files)
- `using System.Threading.Tasks;` (2/56 files)
- `using System.Security.Cryptography;` (1/56 files)

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
| [add-blank-page](./add-blank-page.cs) | Add Blank Page to PDF | `Document`, `Add`, `Save` | Demonstrates how to open an existing PDF, add a new blank page, and save the updated document usi... |
| [add-xmp-metadata](./add-xmp-metadata.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata` | Loads a PDF, injects a custom XMP metadata block, and saves the document while preserving its met... |
| [batch-convert-pdf-to-pdfa](./batch-convert-pdf-to-pdfa.cs) | Batch Convert PDFs to PDF/A-1b with CSV Logging | `Document`, `Convert`, `Save` | Converts all PDF files in a folder to PDF/A-1b format, writes per‑file conversion logs, and recor... |
| [batch-convert-pdf-to-pdfa1b](./batch-convert-pdf-to-pdfa1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `PdfFormat`, `ConvertErrorAction` | Processes all PDF files in a folder, converts each to PDF/A‑1b compliance, and saves the results ... |
| [batch-convert-pdfa-compression](./batch-convert-pdfa-compression.cs) | Batch Convert PDFs to PDF/A‑1b with High Compression | `Document`, `Convert`, `OptimizeResources` | Converts all PDF files in a folder to PDF/A‑1b format, applies resource optimization for high com... |
| [batch-decrypt-pdfs](./batch-decrypt-pdfs.cs) | Batch Decrypt Encrypted PDFs | `Document`, `Decrypt`, `Save` | Decrypts all encrypted PDF files in a folder using a shared owner password and saves unprotected ... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch encrypt PDFs in a directory | `Document`, `Encrypt`, `Save` | Encrypts all PDF files in a given folder with the same user and owner passwords and saves the enc... |
| [batch-encrypt-pdfs__v2](./batch-encrypt-pdfs__v2.cs) | Batch Encrypt PDFs with Different Passwords | `Document`, `Encrypt`, `Permissions` | Encrypt multiple PDF files using individual user and owner passwords read from a configuration fi... |
| [batch-encrypt-pdfs__v3](./batch-encrypt-pdfs__v3.cs) | Batch Encrypt PDFs with File‑Based Passwords and Log | `Document`, `Permissions`, `CryptoAlgorithm` | Encrypts every PDF in a folder using a password derived from its file name and writes the passwor... |
| [batch-split-pdf-by-ranges](./batch-split-pdf-by-ranges.cs) | Batch split PDF into sections by page ranges | `Document`, `Add`, `Save` | Splits a PDF into multiple files based on page ranges defined in a text configuration file. |
| [batch-split-pdf-pages](./batch-split-pdf-pages.cs) | Batch Split PDF into Individual Page Folders | `Document`, `Add`, `Save` | Splits a large PDF into separate one‑page PDFs, each saved in its own folder. |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF Passwords | `Document`, `ChangePasswords` | Opens a password‑protected PDF, changes its user and owner passwords, and saves the updated docum... |
| [change-pdf-version-incremental](./change-pdf-version-incremental.cs) | Change PDF Version to 1.5 with Incremental Save | `Document`, `PdfVersion`, `Save` | Loads a PDF, sets its version to 1.5, and saves it using incremental update to preserve existing ... |
| [compress-pdf-compare-size](./compress-pdf-compare-size.cs) | Compress PDF and Compare File Sizes | `Document`, `OptimizeResources`, `Save` | Loads a PDF, applies default resource optimization to compress it, saves the result, and prints t... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b (Preserve Structure and Fonts) | `Document`, `Convert`, `Save` | Demonstrates converting an existing PDF to PDF/A-1b compliance while keeping the original documen... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A-1b Compliance | `Document`, `Convert`, `Save` | Loads a PDF, converts it to PDF/A‑1b compliance, and saves the resulting document. |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A‑1b with Metadata Preservation | `Document`, `PdfA1bSaveOptions`, `Save` | Demonstrates converting an existing PDF to PDF/A‑1b format while preserving document metadata usi... |
| [convert-pdf-to-pdfa1b__v2](./convert-pdf-to-pdfa1b__v2.cs) | Convert PDF to PDF/A-1b with Embedded Fonts | `Document`, `PdfA1bSaveOptions`, `Save` | Demonstrates converting a PDF to PDF/A-1b compliance while embedding any missing fonts using PdfA... |
| [convert-pdf-to-pdfx3](./convert-pdf-to-pdfx3.cs) | Convert PDF to PDF/X-3 preserving ICC profiles | `Document`, `Convert`, `PdfFormat` | Demonstrates converting a PDF to PDF/X-3 format while keeping existing ICC color profiles for acc... |
| [create-pdf-three-pages-pdfx3](./create-pdf-three-pages-pdfx3.cs) | Create PDF with Three Pages and Save as PDF/X‑3 | `Document`, `Add`, `Convert` | Creates a new PDF document, adds three blank pages, converts it to PDF/X‑3 compliance, and saves ... |
| [create-pdfa1b-with-paragraph](./create-pdfa1b-with-paragraph.cs) | Create PDF/A‑1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | Creates a new PDF, adds a text paragraph to the first page using TextBuilder, converts the docume... |
| [decrypt-pdf](./decrypt-pdf.cs) | Decrypt Encrypted PDF | `Document`, `Decrypt`, `Save` | Shows how to open an encrypted PDF with the user password, decrypt it, and save an unprotected copy. |
| [encrypt-aes128-high-quality-print](./encrypt-aes128-high-quality-print.cs) | Encrypt PDF with AES‑128 and enable high‑quality printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using AES‑128, set the printing permission to high quality, and verify... |
| [encrypt-allow-form-filling](./encrypt-allow-form-filling.cs) | Encrypt PDF allowing only form filling | `Document`, `Permissions`, `CryptoAlgorithm` | Opens a PDF, restricts permissions to only allow form filling, encrypts it with AES‑256, and save... |
| [encrypt-pdf-aes256-no-print](./encrypt-pdf-aes256-no-print.cs) | Encrypt PDF with AES‑256 and No Printing Permission | `Document`, `Encrypt`, `Save` | Demonstrates encrypting a PDF using AES‑256, removing all permissions (including printing), and v... |
| [encrypt-pdf-form-fill-only](./encrypt-pdf-form-fill-only.cs) | Encrypt PDF with Form Filling Only Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Encrypts a PDF allowing only form filling while disabling content extraction. |
| [encrypt-pdf-form-fill-only__v2](./encrypt-pdf-form-fill-only__v2.cs) | Encrypt PDF with User Password, Allow Form Filling Only | `Document`, `Encrypt`, `Save` | Encrypts a PDF using a user and owner password, permits only form filling and disables printing. |
| [encrypt-pdf-network-stream](./encrypt-pdf-network-stream.cs) | Encrypt PDF from Network Stream and Write Back to Stream | `Document`, `Encrypt`, `Save` | Loads a PDF from a network stream, encrypts it with user and owner passwords, and writes the secu... |
| [encrypt-pdf-owner-password](./encrypt-pdf-owner-password.cs) | Encrypt PDF with Owner Password Only | `Document`, `Encrypt`, `Save` | Encrypts a PDF using an owner password while leaving the user password empty, thereby granting fu... |
| [encrypt-pdf-rc4](./encrypt-pdf-rc4.cs) | Encrypt PDF with 128‑bit RC4 and compare file sizes | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF using the 128‑bit RC4 algorithm and verifies that the encrypted fil... |
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
Updated: 2026-04-07 | Run: `20260407_213136_a66d65`
<!-- AUTOGENERATED:END -->

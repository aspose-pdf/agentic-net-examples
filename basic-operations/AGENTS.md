---
name: basic-operations
description: C# examples for basic-operations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - basic-operations

> **Basic operations** in PDF using C# / .NET -- **57** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Facades;` (1/57 files)
- `using Aspose.Pdf.LogicalStructure;` (1/57 files)
- `using Aspose.Pdf.Optimization;` (1/57 files)
- `using Aspose.Pdf.Security;` (1/57 files)
- `using System;` (57/57 files)
- `using System.IO;` (54/57 files)
- `using System.Collections.Generic;` (2/57 files)
- `using System.Net.Http;` (1/57 files)
- `using System.Net.Sockets;` (1/57 files)
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
| [add-blank-page-to-pdf](./add-blank-page-to-pdf.cs) | Add Blank Page to PDF | `Document`, `Pages`, `Add` | Shows how to load an existing PDF, append a new blank page, and save the modified document using ... |
| [add-custom-xmp-metadata-to-pdf](./add-custom-xmp-metadata-to-pdf.cs) | Add Custom XMP Metadata to PDF | `Document`, `SetXmpMetadata`, `Save` | Loads a PDF, creates an XMP metadata packet, attaches it to the document using SetXmpMetadata, an... |
| [batch-convert-pdfs-to-pdfa-1b-csv-report](./batch-convert-pdfs-to-pdfa-1b-csv-report.cs) | Batch Convert PDFs to PDF/A-1b with CSV Report | `Document`, `Convert`, `PdfFormat` | Demonstrates how to convert multiple PDF files to PDF/A‑1b format using Aspose.Pdf, log each conv... |
| [batch-convert-pdfs-to-pdfa-1b](./batch-convert-pdfs-to-pdfa-1b.cs) | Batch Convert PDFs to PDF/A‑1b | `Document`, `Convert`, `Save` | Shows how to iterate through a folder of PDF files, convert each to PDF/A‑1b compliance with Aspo... |
| [batch-decrypt-encrypted-pdfs](./batch-decrypt-encrypted-pdfs.cs) | Batch Decrypt Encrypted PDFs with Owner Password | `Document`, `Document(string, string)`, `Decrypt()` | Shows how to open multiple password‑protected PDF files using a shared owner password, remove the... |
| [batch-encrypt-pdfs-common-password](./batch-encrypt-pdfs-common-password.cs) | Batch Encrypt PDFs with a Common Password | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt every PDF in a folder using the same user and owner passwords, set specific ... |
| [batch-encrypt-pdfs-with-derived-passwords](./batch-encrypt-pdfs-with-derived-passwords.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Save` | Shows how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique user pa... |
| [batch-pdf-encryption-from-config](./batch-pdf-encryption-from-config.cs) | Batch Encrypt PDFs with User Passwords from Config | `Document`, `Encrypt`, `CryptoAlgorithm` | The example reads a JSON configuration containing multiple PDF encryption tasks, applies user and... |
| [batch-pdf-to-pdfa-1b-conversion](./batch-pdf-to-pdfa-1b-conversion.cs) | Batch PDF to PDF/A‑1b Conversion with Compression | `Document`, `PdfFormatConversionOptions`, `Convert` | Demonstrates how to convert multiple PDF files to PDF/A‑1b format using Aspose.Pdf, applying high... |
| [batch-split-pdf-by-page-ranges](./batch-split-pdf-by-page-ranges.cs) | Batch Split PDF into Sections by Page Ranges | `Document`, `Pages`, `Add` | Shows how to split a PDF into multiple files using page ranges read from a configuration file wit... |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF Passwords with Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Shows how to open a password‑protected PDF, update its user and owner passwords, and save the doc... |
| [change-pdf-version-to-1-5-incremental-save](./change-pdf-version-to-1-5-incremental-save.cs) | Change PDF Version to 1.5 with Incremental Save | `Document`, `Convert`, `PdfFormat` | Shows how to open a PDF, convert its version to PDF 1.5 using Document.Convert, and save the file... |
| [compress-pdf-default-optimization](./compress-pdf-default-optimization.cs) | Compress PDF Using Default Optimization | `Document`, `OptimizeResources`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, applying default resource optimization, saving the co... |
| [compress-pdf-high-optimization](./compress-pdf-high-optimization.cs) | Compress PDF with High Optimization Settings | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates applying high compression to a PDF using Aspose.Pdf's OptimizationOptions and PdfSav... |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b with Font and Structure Preservation | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF using Aspose.Pdf, convert it to PDF/A‑1b while logging conversion issues ... |
| [convert-pdf-to-pdfa-1b__v2](./convert-pdf-to-pdfa-1b__v2.cs) | Convert PDF to PDF/A-1b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑1b compliance while logging conversion errors, and saves t... |
| [convert-pdf-to-pdfa1b](./convert-pdf-to-pdfa1b.cs) | Convert PDF to PDF/A-1b with Metadata Preservation | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A-1b compliance while preserving metadata, and save th... |
| [convert-pdf-to-pdfa1b__v2](./convert-pdf-to-pdfa1b__v2.cs) | Convert PDF to PDF/A‑1b with Font Embedding | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/A‑1b compliance (automatically embedding missing fonts... |
| [convert-pdf-to-pdfx3-cmyk](./convert-pdf-to-pdfx3-cmyk.cs) | Convert PDF to PDF/X‑3 with CMYK Color Space | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF, convert it to PDF/X‑3 (which enforces CMYK color conversion), and save t... |
| [convert-pdf-to-pdfx3-preserve-icc](./convert-pdf-to-pdfx3-preserve-icc.cs) | Convert PDF to PDF/X‑3 While Preserving ICC Profiles | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Shows how to convert a PDF to PDF/X‑3 using Aspose.Pdf and keep any embedded ICC color profiles f... |
| [create-pdfa1b-with-text-paragraph](./create-pdfa1b-with-text-paragraph.cs) | Create PDF/A-1b Document with Text Paragraph | `Document`, `Page`, `TextParagraph` | The example creates a new PDF document, adds a text paragraph to the first page, converts the fil... |
| [create-pdfx3-document-multiple-pages](./create-pdfx3-document-multiple-pages.cs) | Create PDF/X‑3 Document with Multiple Pages | `Document`, `Add`, `TextFragment` | Demonstrates creating a new PDF, adding three pages with simple text, converting it to PDF/X‑3 co... |
| [decrypt-encrypted-pdf](./decrypt-encrypted-pdf.cs) | Decrypt Encrypted PDF with User Password | `Document`, `Document(string, string)`, `Decrypt` | Demonstrates opening a password‑protected PDF, decrypting it, and saving an unprotected copy usin... |
| [download-pdf-from-url-and-save](./download-pdf-from-url-and-save.cs) | Download PDF from URL and Save Locally with Aspose.Pdf | `Document`, `Save` | Demonstrates how to download a PDF file from a network URL using HttpClient, load it into an Aspo... |
| [encrypt-pdf-aes128-high-quality-print](./encrypt-pdf-aes128-high-quality-print.cs) | Encrypt PDF with AES‑128 and High‑Quality Printing Permissio... | `Document`, `Encrypt`, `Permissions` | The example loads a PDF, applies AES‑128 encryption with user and owner passwords, enables high‑q... |
| [encrypt-pdf-aes256-no-printing](./encrypt-pdf-aes256-no-printing.cs) | Encrypt PDF with AES‑256 and Disable Printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using AES‑256, set no permissions to block printing, and verify the en... |
| [encrypt-pdf-allow-form-filling](./encrypt-pdf-allow-form-filling.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF with Aspose.Pdf, permitting only form filling and disabling content ex... |
| [encrypt-pdf-allow-form-filling__v2](./encrypt-pdf-allow-form-filling__v2.cs) | Encrypt PDF Allowing Only Form Filling | `Document`, `Permissions`, `Encrypt` | The example loads a PDF, restricts permissions to form filling only, encrypts the document with A... |
| [encrypt-pdf-on-network-stream](./encrypt-pdf-on-network-stream.cs) | Encrypt PDF from Network Stream and Return Encrypted Stream | `DocumentFactory`, `Document`, `Encrypt` | Shows how to read a PDF from a NetworkStream, apply AES‑256 encryption with user and owner passwo... |
| [encrypt-pdf-owner-password-only](./encrypt-pdf-owner-password-only.cs) | Encrypt PDF with Owner Password Only (No User Password) | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf by setting only an owner password, leaving the user p... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

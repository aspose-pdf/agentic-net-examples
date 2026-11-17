---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

> **Securing and signing PDF** in PDF using C# / .NET -- **87** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (86/87 files) ← category-specific
- `using Aspose.Pdf.Forms;` (57/87 files) ← category-specific
- `using Aspose.Pdf.Security;` (6/87 files)
- `using Aspose.Pdf.Text;` (6/87 files)
- `using Aspose.Pdf.Annotations;` (3/87 files)
- `using Aspose.Pdf.Drawing;` (3/87 files)
- `using Aspose.Pdf.Devices;` (1/87 files)
- `using Aspose.Pdf.Operators;` (1/87 files)
- `using Aspose.Pdf.Optimization;` (1/87 files)
- `using System;` (87/87 files)
- `using System.IO;` (87/87 files)
- `using System.Security.Cryptography.X509Certificates;` (19/87 files)
- `using System.Linq;` (10/87 files)
- `using System.Collections.Generic;` (6/87 files)
- `using System.Security.Cryptography;` (5/87 files)
- `using System.Reflection;` (2/87 files)
- `using System.Diagnostics.CodeAnalysis;` (1/87 files)
- `using System.Drawing;` (1/87 files)
- `using System.Drawing.Imaging;` (1/87 files)
- `using System.IO.Compression;` (1/87 files)
- `using System.Net.Http;` (1/87 files)
- `using System.Security.Cryptography.Pkcs;` (1/87 files)
- `using System.Text;` (1/87 files)

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
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Add Invisible Digital Signature to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to create an invisible signature field, apply a PKCS#7 digital signature, and sa... |
| [add-new-empty-signature-field](./add-new-empty-signature-field.cs) | Extract Signature Fields and Add a New Empty Signature Field | `Document`, `SignatureField`, `Rectangle` | The example opens a PDF, enumerates all existing signature fields, creates a new empty signature ... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Bottom‑Right of PDF Page 2 | `Document`, `Page`, `SignatureField` | Demonstrates how to place a visible digital signature field in the bottom‑right corner of the sec... |
| [add-watermark-and-encrypt-pdf](./add-watermark-and-encrypt-pdf.cs) | Add Watermark and Encrypt PDF | `Document`, `Page`, `TextFragment` | Demonstrates adding a centered semi‑transparent watermark to every page using TextStamp and then ... |
| [apply-pkcs7-detached-digital-signature](./apply-pkcs7-detached-digital-signature.cs) | Apply Detached PKCS#7 Signature to PDF | `Document`, `SignatureField`, `PKCS7Detached` | Demonstrates loading a PDF from a stream, adding an invisible signature field, and applying a PKC... |
| [batch-decrypt-pdfs-lookup-table](./batch-decrypt-pdfs-lookup-table.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Decrypt`, `Save` | Demonstrates iterating over a folder of encrypted PDFs, trying a list of possible passwords, decr... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Digitally Sign PDFs | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with AES‑256 and then apply a PKCS#7 digital signa... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Create ZIP Archive | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and bundle the enc... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Save` | Shows how to loop through a folder of PDFs, generate a password from each file name, encrypt each... |
| [batch-encrypt-pdfs-date-password](./batch-encrypt-pdfs-date-password.cs) | Batch Encrypt PDFs with Date‑Based Passwords | `Document`, `Encrypt`, `Save` | Demonstrates iterating over PDF files in a folder, generating a password from the file name and c... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `SignatureField`, `PKCS7` | Shows how to iterate through PDF files, add a digital signature field, sign it with a PKCS#7 cert... |
| [batch-sign-pdfs-title-based-certificate-selection](./batch-sign-pdfs-title-based-certificate-selection.cs) | Batch Sign PDFs Using Metadata-Driven Certificates | `Document`, `Page`, `Rectangle` | Shows how to process a folder of PDFs, select a PFX certificate based on each document's title me... |
| [batch-sign-pdfs-with-certificate-per-document-type](./batch-sign-pdfs-with-certificate-per-document-type.cs) | Batch Sign PDFs Using Document-Type Certificates | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to sign multiple PDF files in a folder, selecting a certificate based on the doc... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to iterate through a folder of PDF files and apply a visible digital signature u... |
| [batch-sign-pdfs-with-unique-visible-signature-imag...](./batch-sign-pdfs-with-unique-visible-signature-images.cs) | Batch Sign PDFs with Unique Visible Signature Images | `Document`, `SignatureField`, `PKCS7` | The example loads multiple PDF files, adds a digital signature field on the last page, signs it w... |
| [certified-pdf-signature-allow-add-pages](./certified-pdf-signature-allow-add-pages.cs) | Sign PDF with Certification Allowing Additional Pages | `Document`, `SignatureField`, `PKCS7` | The example opens an existing PDF, creates a signature field, signs it with a PKCS#7 certificate,... |
| [compute-verify-pdf-hash-before-after-signing](./compute-verify-pdf-hash-before-after-signing.cs) | Compute and Verify PDF Hash Before and After Signing | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to calculate a SHA‑256 hash of a PDF, apply a digital signature with Aspose.Pdf,... |
| [create-readonly-secured-signed-pdf](./create-readonly-secured-signed-pdf.cs) | Create Read‑Only Secured PDF and Add Digital Signature | `Document`, `Page`, `TextFragment` | The example builds a PDF in memory, applies AES‑256 encryption with read‑only permissions, adds a... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Log Access Attempts for Encrypted PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF with user and owner passwords, then opening it while logging each a... |
| [custom-security-handler-warning-overlay](./custom-security-handler-warning-overlay.cs) | Secure PDF with Copy‑Paste Disabled and Warning Overlay | `Document`, `Page`, `TextStamp` | Loads a PDF, adds a semi‑transparent warning stamp to every page, and applies AES‑256 encryption ... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Page`, `XImage` | The example opens an encrypted PDF with a user password, decrypts it, iterates through each page,... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Page` | The example opens an encrypted PDF using a user password, decrypts it, extracts all embedded imag... |
| [decrypt-pdf-sign-fields-multiple-certs](./decrypt-pdf-sign-fields-multiple-certs.cs) | Decrypt PDF and Re‑sign All Signature Fields with Different ... | `Document`, `SignatureField`, `PKCS7` | The example opens an encrypted PDF, extracts every signature field, and re‑signs each field using... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF with Owner Password and Update Metadata | `Document`, `Decrypt`, `Info` | Demonstrates opening an encrypted PDF using the owner password, removing its encryption, modifyin... |
| [decrypt-pdf-with-certificate-token](./decrypt-pdf-with-certificate-token.cs) | Decrypt PDF Encrypted with Certificate Using Hardware Token | `Document`, `Save`, `CertificateEncryptionOptions` | Shows how to locate a certificate on a hardware token, create certificate‑based encryption option... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF Using Owner Password | `Document`, `Decrypt`, `Save` | Shows how to open a password‑protected PDF with the owner password, decrypt the document, and sav... |
| [disable-content-extraction-form-filling](./disable-content-extraction-form-filling.cs) | Set PDF Privileges to Disable Extraction and Form Filling | `Document`, `Permissions`, `Encrypt` | Demonstrates how to apply document privileges to a signed PDF, disabling content extraction and f... |
| [download-sign-pdf-azure-blob](./download-sign-pdf-azure-blob.cs) | Download PDF, Add Placeholder Signature Field, and Save | `Document`, `Rectangle`, `SignatureField` | The example downloads a PDF from a URL, loads it with Aspose.Pdf, creates a signature field place... |
| [encrypt-pdf-aes256-user-owner-passwords](./encrypt-pdf-aes256-user-owner-passwords.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, applying AES‑256 encryption, setting user and owner p... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF and Allow Copying While Blocking Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256, set permissions to allow content extraction (cop... |
| ... | | | *and 57 more files* |

## Category Statistics
- Total examples: 87

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-17 | Run: `20261117_032517_7527fb`
<!-- AUTOGENERATED:END -->

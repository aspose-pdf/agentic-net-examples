---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

> **Securing and signing PDF** in PDF using C# / .NET -- **82** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (81/82 files) ← category-specific
- `using Aspose.Pdf.Forms;` (52/82 files) ← category-specific
- `using Aspose.Pdf.Security;` (6/82 files)
- `using Aspose.Pdf.Text;` (4/82 files)
- `using Aspose.Pdf.Annotations;` (3/82 files)
- `using Aspose.Pdf.Drawing;` (2/82 files)
- `using Aspose.Pdf.Optimization;` (1/82 files)
- `using System;` (82/82 files)
- `using System.IO;` (81/82 files)
- `using System.Security.Cryptography.X509Certificates;` (20/82 files)
- `using System.Collections.Generic;` (8/82 files)
- `using System.Linq;` (6/82 files)
- `using System.Security.Cryptography;` (6/82 files)
- `using System.Text;` (2/82 files)
- `using System.Collections;` (1/82 files)
- `using System.Data;` (1/82 files)
- `using System.Drawing.Imaging;` (1/82 files)
- `using System.IO.Compression;` (1/82 files)
- `using System.Security.Cryptography.Pkcs;` (1/82 files)

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
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Apply Invisible Digital Signature to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to add an invisible PKCS#7 digital signature to a PDF using Aspose.Pdf without a... |
| [add-new-empty-signature-field](./add-new-empty-signature-field.cs) | Extract Existing Signature Fields and Add a New Empty Signat... | `Document`, `Field`, `SignatureField` | The example loads a PDF, lists all existing signature fields, creates a uniquely‑named empty sign... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second PDF Page | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a visible signature field at the bottom‑right corner of the second page, sig... |
| [add-watermark-and-encrypt-pdf](./add-watermark-and-encrypt-pdf.cs) | Add Watermark and Encrypt PDF with Aspose.Pdf | `Document`, `Page`, `TextStamp` | Demonstrates how to create a PDF, apply a centered semi‑transparent watermark, and secure the doc... |
| [apply-pkcs7-detached-digital-signature](./apply-pkcs7-detached-digital-signature.cs) | Create PDF Signature Field from Stream | `Document`, `Rectangle`, `SignatureField` | Loads a PDF from a file stream, adds a placeholder signature field (for a PKCS#7 detached signatu... |
| [batch-decrypt-pdfs-lookup-table](./batch-decrypt-pdfs-lookup-table.cs) | Batch Decrypt Password-Protected PDFs Using a Password Looku... | `Document`, `Decrypt`, `Save` | The example iterates through PDF files in a folder, tries a list of possible passwords, and decry... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Sign PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with a user/owner password and then apply a PKCS#7... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and then pa... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch encrypt PDFs using file name passwords | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a folder with Aspose.Pdf, creating a user password from each fi... |
| [batch-encrypt-pdfs-date-password](./batch-encrypt-pdfs-date-password.cs) | Batch Encrypt PDFs with Hashed Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Loads each PDF from a source folder, creates a password from the file name and current UTC date, ... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `SignatureField`, `Signature` | Demonstrates how to load multiple PDF files, add a digital signature using a PFX certificate, com... |
| [batch-sign-pdfs-title-based-certificate-selection](./batch-sign-pdfs-title-based-certificate-selection.cs) | Batch Sign PDFs with Metadata‑Based Certificate Selection | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to process a folder of PDF files, choose a signing certificate based on each doc... |
| [batch-sign-pdfs-with-certificate-per-document-type](./batch-sign-pdfs-with-certificate-per-document-type.cs) | Batch Sign PDFs Based on Document Type Metadata | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to iterate over a set of PDF files, read a custom metadata key to determine the ... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to iterate through a folder of PDF files and apply a digital signature using a P... |
| [compute-verify-pdf-hash-before-after-signing](./compute-verify-pdf-hash-before-after-signing.cs) | Compute and Verify PDF Hash Before and After Signing | `Document`, `SignatureField`, `PKCS1` | Demonstrates how to calculate a SHA‑256 hash of a PDF, sign the document with a certificate, then... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Encrypt and Decrypt PDF with Logging | `Document`, `Encrypt`, `Decrypt` | Shows how to encrypt a PDF with user/owner passwords, log the encryption details, then open (decr... |
| [custom-security-handler-warning-overlay](./custom-security-handler-warning-overlay.cs) | Apply Security Handler with Warning Overlay and Copy‑Paste R... | `Document`, `Page`, `TextAnnotation` | The example loads a PDF, adds a visible warning text annotation to each page, and secures the doc... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt()`, `Page` | Shows how to open an encrypted PDF with a user password, remove its encryption, and extract all i... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Encrypt` | The example opens an encrypted PDF using a user password, extracts all embedded images to files, ... |
| [decrypt-pdf-sign-fields-multiple-certs](./decrypt-pdf-sign-fields-multiple-certs.cs) | Decrypt PDF and Re‑sign All Signature Fields with Different ... | `Document`, `Decrypt`, `SignatureField` | The example opens an encrypted PDF, decrypts it, locates every signature field, and applies a new... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF and Update Metadata with Aspose.PDF | `Document`, `Decrypt`, `Info` | Demonstrates opening an encrypted PDF using the owner password, removing its encryption, modifyin... |
| [decrypt-pdf-with-certificate-token](./decrypt-pdf-with-certificate-token.cs) | Decrypt PDF Using Certificate from Hardware Token | `Document`, `CertificateEncryptionOptions`, `Decrypt` | Shows how to load a certificate with a private key from a smart card or hardware token and use As... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password using Aspose.Pdf | `Document`, `Decrypt`, `Save` | Shows how to open a password‑protected PDF with the owner password, remove its encryption, and sa... |
| [disable-content-extraction-form-filling](./disable-content-extraction-form-filling.cs) | Disable Content Extraction and Form Filling in PDF | `Document`, `Permissions`, `Encrypt` | Demonstrates how to open a signed PDF, apply encryption with specific permissions to allow printi... |
| [encrypt-pdf-aes256-user-owner-passwords](./encrypt-pdf-aes256-user-owner-passwords.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Encrypt`, `CryptoAlgorithm` | Shows how to load a PDF with Aspose.Pdf, apply AES‑256 encryption, assign user and owner password... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF with Copy Permission and No Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf, permitting content extraction (copy) while disabling... |
| [encrypt-pdf-disable-content-extraction](./encrypt-pdf-disable-content-extraction.cs) | Encrypt PDF and Restrict Content Extraction | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply AES‑256 encryption, and set permissions that allow only pri... |
| [encrypt-pdf-disable-printing](./encrypt-pdf-disable-printing.cs) | Encrypt PDF with User Password and Disable Printing | `Document`, `Permissions`, `Encrypt` | Demonstrates how to load a PDF, apply AES‑256 encryption with a user password, restrict permissio... |
| [encrypt-pdf-owner-password-high-res-printing](./encrypt-pdf-owner-password-high-res-printing.cs) | Encrypt PDF with Owner Password and Restrict Copying | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt a PDF using Aspose.Pdf, set an owner password, allow high‑resolution printin... |
| [encrypt-pdf-print-only-permission](./encrypt-pdf-print-only-permission.cs) | Encrypt PDF with Print-Only Permission | `Document`, `Permissions`, `Encrypt` | Shows how to encrypt a PDF using Aspose.Pdf, set user and owner passwords, and restrict permissio... |
| ... | | | *and 52 more files* |

## Category Statistics
- Total examples: 82

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-26 | Run: `20261026_221959_83adb1`
<!-- AUTOGENERATED:END -->

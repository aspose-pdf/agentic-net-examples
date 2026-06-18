---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (77/77 files) ← category-specific
- `using Aspose.Pdf.Forms;` (46/77 files) ← category-specific
- `using Aspose.Pdf.Security;` (10/77 files)
- `using Aspose.Pdf.Text;` (6/77 files)
- `using Aspose.Pdf.Drawing;` (5/77 files)
- `using Aspose.Pdf.Annotations;` (3/77 files)
- `using System;` (77/77 files)
- `using System.IO;` (74/77 files)
- `using System.Security.Cryptography.X509Certificates;` (14/77 files)
- `using System.Collections.Generic;` (9/77 files)
- `using System.Linq;` (5/77 files)
- `using System.Security.Cryptography;` (4/77 files)
- `using System.Drawing.Imaging;` (3/77 files)
- `using System.Drawing;` (2/77 files)
- `using System.IO.Compression;` (1/77 files)
- `using System.Reflection;` (1/77 files)
- `using System.Text;` (1/77 files)

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
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Add Invisible Digital Signature to PDF | `Document`, `SignaturesAppendOnly`, `Rectangle` | Demonstrates how to apply an invisible PKCS#7 digital signature to a PDF using Aspose.Pdf, preser... |
| [add-new-empty-signature-field](./add-new-empty-signature-field.cs) | Add a New Empty Signature Field and List Existing Ones | `Document`, `SignatureField`, `Add` | The sample loads a PDF, lists any existing signature fields, creates a new empty signature field ... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second Page | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a visible signature field in the bottom‑right corner of the second page, and... |
| [apply-custom-security-handler-disable-copy-paste](./apply-custom-security-handler-disable-copy-paste.cs) | Apply Custom Security Handler to Disable Copy‑Paste in PDF | `Document`, `Permissions`, `ICustomSecurityHandler` | The example demonstrates implementing a custom ICustomSecurityHandler that disables the ExtractCo... |
| [batch-decrypt-pdf-lookup](./batch-decrypt-pdf-lookup.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Decrypt`, `Save` | Demonstrates how to decrypt multiple encrypted PDF files in a folder by retrieving each file's pa... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and store the encr... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf, generating a password fro... |
| [batch-encrypt-pdfs-with-date-based-passwords](./batch-encrypt-pdfs-with-date-based-passwords.cs) | Batch Encrypt PDFs with Date-Based Passwords | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-encrypt-sign-pdf](./batch-encrypt-sign-pdf.cs) | Batch Encrypt and Digitally Sign PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Creates sample PDFs, encrypts each file, then adds a digital signature using a self‑signed certif... |
| [batch-sign-pdfs-metadata-certificate](./batch-sign-pdfs-metadata-certificate.cs) | Batch Sign PDFs with Metadata‑Based Certificate Selection | `Document`, `DocumentInfo`, `PKCS7` | Demonstrates how to process a folder of PDF files, choose a signing certificate based on the docu... |
| [batch-sign-pdfs-with-certificates](./batch-sign-pdfs-with-certificates.cs) | Batch Sign PDFs Using Document-Type Certificates | `Document`, `SignatureField`, `PKCS7` | Demonstrates iterating over a list of PDFs, fetching a certificate per document type from a data ... |
| [certify-pdf-no-changes](./certify-pdf-no-changes.cs) | Certify PDF with No-Changes Permission | `Document`, `PKCS7`, `DocMDPSignature` | Shows how to apply a certification (DocMDP) signature to a PDF that prevents any further modifica... |
| [compute-pdf-hash-before-after-signing](./compute-pdf-hash-before-after-signing.cs) | Compute PDF Hash Before and After Signing | `Document`, `PKCS7`, `SignatureField` | Demonstrates how to compute a hash of a PDF document, apply a digital signature, and compare hash... |
| [custom-security-handler-disable-copy-paste-warning](./custom-security-handler-disable-copy-paste-warning.cs) | Apply Custom Security Handler to Disable Copy‑Paste and Add ... | `Document`, `Page`, `TextFragment` | Demonstrates implementing a simple ICustomSecurityHandler to encrypt a PDF, restrict permissions ... |
| [custom-security-watermark](./custom-security-watermark.cs) | Create PDF with Custom Security Handler and Watermark | `Document`, `Page`, `WatermarkArtifact` | Demonstrates creating a PDF, adding a watermark to each page using WatermarkArtifact, and encrypt... |
| [decrypt-extract-reencrypt](./decrypt-extract-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑Encrypt with New Owner P... | `Document`, `Image`, `XImage` | Demonstrates decrypting an encrypted PDF, extracting embedded images, and re‑encrypting the docum... |
| [decrypt-pdf-and-resign-multiple-certificates](./decrypt-pdf-and-resign-multiple-certificates.cs) | Decrypt Encrypted PDF and Re‑sign Signature Fields with Mult... | `Document`, `Decrypt`, `Page` | The example demonstrates opening an encrypted PDF with a password, removing its encryption, locat... |
| [decrypt-pdf-extract-images](./decrypt-pdf-extract-images.cs) | Decrypt PDF and Extract Images | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates decrypting a password‑protected PDF and extracting all embedded images to separate f... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF with Owner Password and Update Metadata | `Document`, `Decrypt`, `Info` | Demonstrates opening an encrypted PDF using the owner password, decrypting it, modifying document... |
| [decrypt-pdf-using-certificate-token](./decrypt-pdf-using-certificate-token.cs) | Decrypt PDF Using Certificate from Hardware Token | `Document`, `CertificateEncryptionOptions`, `Decrypt` | Shows how to open a PDF encrypted with a public certificate and automatically retrieve the privat... |
| [decrypt-pdf-using-owner-password](./decrypt-pdf-using-owner-password.cs) | Decrypt PDF Using Owner Password | `Document`, `Decrypt`, `Save` | Demonstrates opening an encrypted PDF with the owner password, removing its protection, and savin... |
| [encrypt-pdf-aes256](./encrypt-pdf-aes256.cs) | Encrypt PDF with AES‑256 and Set Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to load a PDF, apply AES‑256 encryption with user and owner passwords, define permissio... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF and Allow Copying While Blocking Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256, permit content extraction (copy) and disable pri... |
| [encrypt-pdf-disable-content-extraction](./encrypt-pdf-disable-content-extraction.cs) | Encrypt PDF and Disable Content Extraction | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF with Aspose.Pdf, applying AES‑256 encryption, setting permissions to a... |
| [encrypt-pdf-disable-printing](./encrypt-pdf-disable-printing.cs) | Encrypt PDF with Password and Disable Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply password protection with AES‑256 encryption, and restrict p... |
| [encrypt-pdf-log-access](./encrypt-pdf-log-access.cs) | Encrypt PDF and Log Access Attempts | `Document`, `Permissions`, `CryptoAlgorithm` | Creates a PDF, encrypts it with a password, then demonstrates logging of access attempts with wro... |
| [encrypt-pdf-owner-password-editing](./encrypt-pdf-owner-password-editing.cs) | Encrypt PDF with Owner Password for Editing Only | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt a PDF using Aspose.Pdf so it can be opened without a password for viewing, w... |
| [encrypt-pdf-owner-password-permissions](./encrypt-pdf-owner-password-permissions.cs) | Encrypt PDF with Owner Password and Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using an owner password, enable high‑resolution printing, and restrict... |
| [encrypt-pdf-print-only-permission](./encrypt-pdf-print-only-permission.cs) | Encrypt PDF with Print‑Only Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, encrypt it with AES‑256, and restrict permissions so that only pr... |
| [encrypt-pdf-rc4-128-bit](./encrypt-pdf-rc4-128-bit.cs) | Encrypt PDF with RC4 128‑bit and Restrict Editing | `Document`, `Permissions`, `Encrypt` | Shows how to load a PDF, apply RC4 128‑bit encryption with user and owner passwords, limit permis... |
| ... | | | *and 47 more files* |

## Category Statistics
- Total examples: 77

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

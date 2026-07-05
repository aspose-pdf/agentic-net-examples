---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

> **Securing and signing PDF** in PDF using C# / .NET -- **84** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (84/84 files) ← category-specific
- `using Aspose.Pdf.Forms;` (55/84 files) ← category-specific
- `using Aspose.Pdf.Security;` (13/84 files)
- `using Aspose.Pdf.Annotations;` (4/84 files)
- `using Aspose.Pdf.Drawing;` (4/84 files)
- `using Aspose.Pdf.Text;` (3/84 files)
- `using Aspose.Pdf.Signatures;` (1/84 files)
- `using System;` (84/84 files)
- `using System.IO;` (83/84 files)
- `using System.Security.Cryptography.X509Certificates;` (15/84 files)
- `using System.Collections.Generic;` (6/84 files)
- `using System.Linq;` (5/84 files)
- `using System.Security.Cryptography;` (3/84 files)
- `using System.Drawing.Imaging;` (2/84 files)
- `using System.Reflection;` (2/84 files)
- `using System.Text;` (2/84 files)
- `using Azure.Storage.Blobs;` (1/84 files)
- `using System.Collections;` (1/84 files)
- `using System.Data;` (1/84 files)
- `using System.Data.SqlClient;` (1/84 files)
- `using System.Drawing;` (1/84 files)
- `using System.IO.Compression;` (1/84 files)
- `using System.Net.Http;` (1/84 files)
- `using System.Threading.Tasks;` (1/84 files)
- `using System.Xml;` (1/84 files)

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
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to PDF Second Page | `Document`, `PageInfo`, `Rectangle` | The example loads a PDF, creates a visible signature field on the bottom‑right corner of the seco... |
| [apply-invisible-digital-signature-with-timestamp](./apply-invisible-digital-signature-with-timestamp.cs) | Apply Invisible Digital Signature with Timestamp to PDF | `Document`, `SignatureField`, `PKCS7` | Demonstrates adding an invisible PKCS#7 digital signature that records the signing time while pre... |
| [apply-pkcs7-detached-signature-to-pdf](./apply-pkcs7-detached-signature-to-pdf.cs) | Apply PKCS#7 Detached Signature to PDF | `Document`, `SignatureField`, `PKCS7Detached` | The example loads a PDF from a file stream, adds a signature field, and applies a PKCS#7 detached... |
| [batch-decrypt-pdf-lookup-table](./batch-decrypt-pdf-lookup-table.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Document(string, string)`, `Decrypt` | Demonstrates how to decrypt multiple password‑protected PDF files in a folder by using a dictiona... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Sign PDFs | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt multiple PDF files with AES‑256 and then apply a PKCS#7 digital signature to... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Encrypt`, `Save` | Shows how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and then package t... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Save` | The example iterates through all PDF files in a folder, derives a password from each file name, e... |
| [batch-encrypt-pdfs-with-date-hashed-passwords](./batch-encrypt-pdfs-with-date-hashed-passwords.cs) | Batch Encrypt PDFs with Date-Based Hashed Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `SignatureField`, `PKCS7` | Shows how to iterate through a folder of PDF files, add a visible digital signature using a PFX c... |
| [batch-sign-pdfs-by-doc-type](./batch-sign-pdfs-by-doc-type.cs) | Batch Sign PDFs Using Document-Type Certificates | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to sign multiple PDF files in a batch, selecting the appropriate X.509 certifica... |
| [batch-sign-pdfs-using-metadata](./batch-sign-pdfs-using-metadata.cs) | Batch Sign PDFs Using Document Metadata | `Document`, `Page`, `Rectangle` | Demonstrates how to iterate over PDF files, read a metadata value (Title) to choose a certificate... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `SignatureField`, `Rectangle` | Shows how to iterate over PDF files in a folder, add a signature field, apply a PKCS#7 digital si... |
| [batch-sign-pdfs-with-unique-visible-signature-imag...](./batch-sign-pdfs-with-unique-visible-signature-image.cs) | Batch Sign PDFs with Unique Visible Signature Image | `Document`, `SignatureField`, `PKCS1` | The example iterates through PDF files in a folder, adds a signature field to each document, and ... |
| [certify-pdf-append-only-signature](./certify-pdf-append-only-signature.cs) | Certify PDF with Append-Only Signature to Allow Adding Pages | `Document`, `Rectangle`, `SignatureField` | Shows how to create a DocMDP certification signature using Aspose.Pdf, set the document to append... |
| [certify-pdf-no-changes](./certify-pdf-no-changes.cs) | Certify PDF with No-Changes Permission | `Document`, `PKCS1`, `DocMDPSignature` | Shows how to apply a certification signature to a PDF that disallows any further modifications us... |
| [certify-pdf-prevent-further-signatures](./certify-pdf-prevent-further-signatures.cs) | Certify PDF with DocMDP and Prevent Further Signatures | `Document`, `PKCS1`, `DocMDPSignature` | Shows how to apply a DocMDP certification signature to a PDF using Aspose.Pdf, making the documen... |
| [compute-verify-pdf-hash-before-after-signing](./compute-verify-pdf-hash-before-after-signing.cs) | Compute and Verify PDF Hash Before and After Signing | `Document`, `PKCS7Detached`, `SignatureField` | Demonstrates how to calculate a SHA‑256 hash of a PDF, apply a PKCS#7 detached signature, extract... |
| [custom-pdf-security-disable-copy-paste-watermark](./custom-pdf-security-disable-copy-paste-watermark.cs) | Custom PDF Security with Disabled Copy‑Paste and Watermark | `Document`, `Encrypt`, `Permissions` | Demonstrates how to implement a simple custom security handler to disable copy‑paste via PDF perm... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Custom Security Handler Logging PDF Access | `Document`, `ICustomSecurityHandler`, `EncryptionParameters` | Demonstrates how to implement a custom ICustomSecurityHandler that logs encryption and decryption... |
| [decrypt-pdf-certificate-hardware-token](./decrypt-pdf-certificate-hardware-token.cs) | Decrypt PDF Encrypted with Certificate Using Hardware Token | `Document`, `CertificateEncryptionOptions`, `Decrypt` | Shows how to open a PDF that was encrypted with a public certificate and decrypt it by locating t... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt`, `Page` | Shows how to open an encrypted PDF with a user password, decrypt it, and extract all images from ... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Page` | Demonstrates opening an encrypted PDF with a user password, decrypting it, extracting all embedde... |
| [decrypt-pdf-multi-signature-re-sign](./decrypt-pdf-multi-signature-re-sign.cs) | Decrypt PDF and Re‑sign Multiple Signature Fields with Diffe... | `Document`, `Decrypt`, `SignatureField` | The example opens an encrypted PDF using a password, extracts all signature fields, and re‑signs ... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF with Owner Password and Update Metadata | `Document`, `DocumentInfo`, `Decrypt()` | The example demonstrates opening an encrypted PDF using the owner password, removing its encrypti... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password | `Document`, `Decrypt`, `Save` | Demonstrates opening an encrypted PDF using the owner password, decrypting it, and saving the unp... |
| [download-sign-upload-pdf-azure-blob](./download-sign-upload-pdf-azure-blob.cs) | Download PDF, Apply Digital Signature, and Upload to Azure B... | `Document`, `SignatureField`, `PKCS7` | The example downloads a PDF (or creates a blank one), adds a signature field, signs it with a PFX... |
| [encrypt-pdf-aes256-user-owner-passwords](./encrypt-pdf-aes256-user-owner-passwords.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply AES‑256 encryption, define user and owner passwords, set pe... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF and Allow Copying While Blocking Printing | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads a PDF, encrypts it with user and owner passwords, sets permissions to allow con... |
| [encrypt-pdf-allow-printing-copying](./encrypt-pdf-allow-printing-copying.cs) | Encrypt PDF and Allow Printing & Copying | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256 with user and owner passwords, while granting per... |
| [encrypt-pdf-disable-content-extraction](./encrypt-pdf-disable-content-extraction.cs) | Encrypt PDF and Disable Content Extraction | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF with user and owner passwords, set permissions to allow only printing ... |
| ... | | | *and 54 more files* |

## Category Statistics
- Total examples: 84

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

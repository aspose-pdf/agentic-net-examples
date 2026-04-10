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

- `using Aspose.Pdf;` (87/87 files) ← category-specific
- `using Aspose.Pdf.Forms;` (57/87 files) ← category-specific
- `using Aspose.Pdf.Security;` (14/87 files)
- `using Aspose.Pdf.Annotations;` (5/87 files)
- `using Aspose.Pdf.Text;` (5/87 files)
- `using Aspose.Pdf.Signatures;` (3/87 files)
- `using System;` (87/87 files)
- `using System.IO;` (87/87 files)
- `using System.Security.Cryptography.X509Certificates;` (15/87 files)
- `using System.Collections.Generic;` (8/87 files)
- `using System.Linq;` (4/87 files)
- `using System.Security.Cryptography;` (4/87 files)
- `using System.Drawing.Imaging;` (3/87 files)
- `using Azure.Storage.Blobs;` (1/87 files)
- `using System.Drawing;` (1/87 files)
- `using System.IO.Compression;` (1/87 files)
- `using System.Net.Http;` (1/87 files)
- `using System.Text;` (1/87 files)
- `using System.Threading.Tasks;` (1/87 files)
- `using System.Xml;` (1/87 files)

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
| [add-incremental-pdf-signature](./add-incremental-pdf-signature.cs) | Add Incremental PDF Signature with Aspose.Pdf | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to add a signature field to a PDF, apply a PKCS#7 signature, and enable the docu... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second Page of PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to place a visible PKCS#7 digital signature field at the bottom‑right corner of ... |
| [apply-invisible-digital-signature](./apply-invisible-digital-signature.cs) | Apply Invisible Digital Signature with Timestamp to PDF | `Document`, `Rectangle`, `SignatureField` | Shows how to add an invisible PKCS#7 digital signature that records the signing time to a PDF wit... |
| [apply-pkcs7-detached-signature-to-pdf](./apply-pkcs7-detached-signature-to-pdf.cs) | Apply PKCS#7 Detached Digital Signature to PDF from Stream | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF from a file stream, adding a visible signature field, and applying a P... |
| [batch-decrypt-pdfs-lookup](./batch-decrypt-pdfs-lookup.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Decrypt`, `Save` | Shows how to process a folder of encrypted PDFs, obtain passwords from a case‑insensitive diction... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Digitally Sign PDFs | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt multiple PDF files with AES‑256 and then apply a visible digital sign... |
| [batch-encrypt-pdfs-from-filename](./batch-encrypt-pdfs-from-filename.cs) | Batch Encrypt PDFs Using File Name Passwords | `Document`, `Encrypt`, `Save` | Shows how to encrypt every PDF in a directory with Aspose.Pdf, using the file name as both user a... |
| [batch-encrypt-pdfs-with-date-password](./batch-encrypt-pdfs-with-date-password.cs) | Batch Encrypt PDFs with Date-Based Passwords | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-encrypt-pdfs-zip-archive](./batch-encrypt-pdfs-zip-archive.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and then pa... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `Rectangle`, `SignatureField` | Shows how to process all PDF files in a folder, add a PKCS#7 digital signature from a PFX certifi... |
| [batch-sign-pdfs-by-document-type](./batch-sign-pdfs-by-document-type.cs) | Batch Sign PDFs Using Document-Type Certificates | `Document`, `Save`, `PKCS7` | Demonstrates how to load PDFs, retrieve a certificate based on document type, and sign all signat... |
| [batch-sign-pdfs-with-certificate-selection](./batch-sign-pdfs-with-certificate-selection.cs) | Batch Sign PDFs with Certificate Selection | `Document`, `DocumentInfo`, `Page` | Demonstrates how to process multiple PDF files, choose a signing certificate based on document me... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to iterate through a folder of PDF files and apply a digital signature using a P... |
| [batch-sign-pdfs-with-unique-visible-signature-imag...](./batch-sign-pdfs-with-unique-visible-signature-images.cs) | Batch Sign PDFs with Unique Visible Signature Images | `Document`, `Page`, `Rectangle` | The example iterates through PDF files in a folder, adds a visible signature field using a matchi... |
| [certify-pdf-no-changes](./certify-pdf-no-changes.cs) | Certify PDF with No-Changes Permission | `Document`, `PKCS7`, `DocMDPSignature` | Demonstrates how to apply a certification (DocMDP) signature to a PDF that prevents any further m... |
| [create-pdf-signature-sha512](./create-pdf-signature-sha512.cs) | Create PDF Signature with SHA‑512 Digest | `Document`, `Rectangle`, `SignatureField` | Shows how to add a signature field to a PDF and sign it using a PFX certificate with the SHA‑512 ... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Custom Security Handler Logging PDF Access | `Document`, `Permissions`, `ICustomSecurityHandler` | Demonstrates how to implement a custom ICustomSecurityHandler that logs each encryption, decrypti... |
| [custom-security-handler-with-warning-overlay](./custom-security-handler-with-warning-overlay.cs) | Apply Custom Security Handler with Warning Overlay | `Document`, `Encrypt`, `Save` | Loads a PDF, adds a semi‑transparent warning text annotation on each page, then encrypts the docu... |
| [decrypt-pdf-and-sign-fields-with-multiple-certs](./decrypt-pdf-and-sign-fields-with-multiple-certs.cs) | Decrypt PDF and Sign Each Signature Field with Different Cer... | `Document`, `Decrypt`, `Save` | The example opens an encrypted PDF, decrypts it, enumerates all signature fields, and signs each ... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt`, `Save` | Opens an encrypted PDF with a user password, decrypts it, extracts all embedded images to a speci... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Encrypt` | The example opens an encrypted PDF using a user password, decrypts it, extracts all embedded imag... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF with Owner Password and Update Metadata | `Document`, `Decrypt`, `Save` | Demonstrates opening an encrypted PDF using the owner password, removing its encryption, modifyin... |
| [decrypt-pdf-with-certificate-token](./decrypt-pdf-with-certificate-token.cs) | Decrypt PDF Encrypted with Certificate Using Hardware Token | `Document`, `CertificateEncryptionOptions`, `Decrypt` | Demonstrates loading a certificate (with private key) from a hardware token via X509Store and usi... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password | `Document`, `Decrypt`, `Save` | Shows how to open a password‑protected PDF using the owner password, remove its encryption, and s... |
| [encrypt-pdf-aes256-user-owner-password](./encrypt-pdf-aes256-user-owner-password.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, configurin... |
| [encrypt-pdf-allow-printing-only](./encrypt-pdf-allow-printing-only.cs) | Encrypt PDF and Allow Only Printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF with user/owner passwords using AES‑256 and set permissions so that on... |
| [encrypt-pdf-copy-permission-no-print](./encrypt-pdf-copy-permission-no-print.cs) | Encrypt PDF with Copy Permission and No Printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF with AES‑256, allow content extraction (copy) and block printing using... |
| [encrypt-pdf-disable-content-extraction](./encrypt-pdf-disable-content-extraction.cs) | Encrypt PDF and Disable Content Extraction | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF with AES‑256 using Aspose.Pdf, set permissions to allow only pr... |
| [encrypt-pdf-disable-printing](./encrypt-pdf-disable-printing.cs) | Encrypt PDF with User Password and Disable Printing | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF with a user and owner password, configure permissions that excl... |
| [encrypt-pdf-owner-password-edit-only](./encrypt-pdf-owner-password-edit-only.cs) | Encrypt PDF with Owner Password for Editing Only | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF so it can be opened for viewing without a password, while requiring an... |
| ... | | | *and 57 more files* |

## Category Statistics
- Total examples: 87

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

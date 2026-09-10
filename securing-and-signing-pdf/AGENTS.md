---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

> **Securing and signing PDF** in PDF using C# / .NET -- **78** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (78/78 files) ← category-specific
- `using Aspose.Pdf.Forms;` (52/78 files) ← category-specific
- `using Aspose.Pdf.Security;` (9/78 files)
- `using Aspose.Pdf.Annotations;` (4/78 files)
- `using Aspose.Pdf.Drawing;` (2/78 files)
- `using Aspose.Pdf.Signatures;` (2/78 files)
- `using Aspose.Pdf.Text;` (1/78 files)
- `using System;` (78/78 files)
- `using System.IO;` (78/78 files)
- `using System.Security.Cryptography.X509Certificates;` (16/78 files)
- `using System.Collections.Generic;` (5/78 files)
- `using System.Linq;` (4/78 files)
- `using System.Security.Cryptography;` (3/78 files)
- `using Azure.Storage.Blobs;` (1/78 files)
- `using System.Drawing.Imaging;` (1/78 files)
- `using System.IO.Compression;` (1/78 files)
- `using System.Net.Http;` (1/78 files)
- `using System.Text;` (1/78 files)
- `using System.Threading;` (1/78 files)
- `using System.Threading.Tasks;` (1/78 files)
- `using System.Xml;` (1/78 files)

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
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Add Invisible Digital Signature to PDF | `Document`, `Page`, `Rectangle` | Shows how to create an invisible digital signature field, sign it with a PKCS#7 certificate, reco... |
| [add-new-empty-signature-field](./add-new-empty-signature-field.cs) | Add a New Empty Signature Field to a PDF | `Document`, `SignatureField`, `Rectangle` | The example loads a PDF, lists any existing signature fields, creates a new empty signature field... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second Page of PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to place a visible digital signature in the bottom‑right corner of the second pa... |
| [apply-pkcs7-detached-digital-signature](./apply-pkcs7-detached-digital-signature.cs) | Apply PKCS#7 Detached Digital Signature to PDF | `Document`, `SignatureField`, `PKCS7Detached` | Demonstrates loading a PDF from a file stream, adding a signature field, and applying a PKCS#7 de... |
| [batch-decrypt-pdfs-lookup-table](./batch-decrypt-pdfs-lookup-table.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Document(string, string)`, `Decrypt` | Demonstrates iterating over a folder of encrypted PDFs, trying multiple possible passwords from a... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Digitally Sign PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with AES‑256 and then apply a PKCS#7 digital signa... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and then pa... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs Using File Name Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Iterates through all PDF files in a folder, derives a password from each file name, encrypts each... |
| [batch-encrypt-pdfs-date-password](./batch-encrypt-pdfs-date-password.cs) | Batch Encrypt PDFs with Date-Based Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to iterate over a folder of PDF files, add a digital signature field, sign each ... |
| [batch-sign-pdfs-title-based-certificate-selection](./batch-sign-pdfs-title-based-certificate-selection.cs) | Batch Sign PDFs with Title-Based Certificate Selection | `Document`, `DocumentInfo`, `PKCS7` | Loads PDFs from a folder, selects a certificate based on the document title, creates a PKCS7 sign... |
| [batch-sign-pdfs-with-certificate-per-document-type](./batch-sign-pdfs-with-certificate-per-document-type.cs) | Batch Sign PDFs with Certificate per Document Type | `Document`, `Info`, `SignatureField` | Loads PDF files from an input folder, determines a document type from the PDF title, retrieves th... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `Page`, `Rectangle` | Demonstrates how to iterate over PDF files in a folder and apply a PKCS#7 digital signature using... |
| [certified-pdf-signature-allow-add-pages](./certified-pdf-signature-allow-add-pages.cs) | Create a Certified PDF Signature Allowing Page Additions | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to add a certification (MDP) signature to a PDF using Aspose.Pdf so that new pag... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Custom Security Handler for Logging PDF Access Attempts | `Document`, `ICustomSecurityHandler`, `Permissions` | Demonstrates how to implement a custom ICustomSecurityHandler that logs password checks and encry... |
| [custom-security-handler-warning-overlay](./custom-security-handler-warning-overlay.cs) | Custom Security Handler with Warning Overlay | `Document`, `Permissions`, `Encrypt` | Shows how to attach a custom ICustomSecurityHandler to a PDF to disable copy‑paste permissions an... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt()`, `Save()` | Shows how to open an encrypted PDF with a user password, remove its encryption, and extract all e... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Page` | The example opens an encrypted PDF using a known password, decrypts it, extracts all embedded ima... |
| [decrypt-pdf-sign-fields-multiple-certs](./decrypt-pdf-sign-fields-multiple-certs.cs) | Decrypt PDF and Sign Each Signature Field with Different Cer... | `Document`, `Decrypt`, `SignatureField` | The example opens an encrypted PDF, removes its protection, locates all signature fields, and sig... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF and Update Metadata with Aspose.Pdf | `Document`, `Decrypt`, `Info` | Demonstrates how to open an encrypted PDF using the owner password, remove its encryption, modify... |
| [decrypt-pdf-with-certificate-token](./decrypt-pdf-with-certificate-token.cs) | Decrypt PDF Encrypted with Certificate Using Hardware Token | `Document`, `CertificateEncryptionOptions`, `Decrypt` | Shows how to open a PDF encrypted with a public certificate, retrieve the private key from a hard... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password using Aspose.Pdf | `Document`, `Decrypt`, `Save` | Demonstrates opening an encrypted PDF with the owner password, removing its protection, and savin... |
| [download-sign-pdf-azure-blob](./download-sign-pdf-azure-blob.cs) | Download PDF, Apply PKCS7 Signature, and Upload to Azure Blo... | `Document`, `Page`, `Rectangle` | The example downloads a PDF from a URL, creates a signature field, signs it with a PKCS7 certific... |
| [encrypt-pdf-aes256-user-owner-passwords](./encrypt-pdf-aes256-user-owner-passwords.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256, assign separate user and owner passwords, and co... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF and Allow Copying While Blocking Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF with Aspose.Pdf, set user and owner passwords, permit content extracti... |
| [encrypt-pdf-disable-content-extraction](./encrypt-pdf-disable-content-extraction.cs) | Encrypt PDF and Disable Content Extraction | `Document`, `Permissions`, `CryptoAlgorithm` | Loads a PDF, encrypts it with AES‑256 using user and owner passwords, sets permissions to none to... |
| [encrypt-pdf-disable-printing](./encrypt-pdf-disable-printing.cs) | Encrypt PDF with Password and Disable Printing | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using AES‑256 with user and owner passwords while restricting p... |
| [encrypt-pdf-owner-password-high-res-printing](./encrypt-pdf-owner-password-high-res-printing.cs) | Encrypt PDF with Owner Password and Allow High‑Resolution Pr... | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads a PDF, applies AES‑256 encryption with an owner password, enables high‑resoluti... |
| [encrypt-pdf-print-only-permission](./encrypt-pdf-print-only-permission.cs) | Encrypt PDF with Print‑Only Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf and restrict its permissions so that only printing is... |
| [encrypt-pdf-rc4-128-bit-restrict-editing](./encrypt-pdf-rc4-128-bit-restrict-editing.cs) | Encrypt PDF with RC4 128-bit and Restrict Editing | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads a PDF, applies 128‑bit RC4 encryption with user and owner passwords, restricts ... |
| ... | | | *and 48 more files* |

## Category Statistics
- Total examples: 78

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

---
name: securing-and-signing-pdf
description: C# examples for securing-and-signing-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - securing-and-signing-pdf

> **Securing and signing PDF** in PDF using C# / .NET -- **116** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **securing-and-signing-pdf** category.
This folder contains standalone C# examples for securing-and-signing-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **securing-and-signing-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (78/116 files) ← category-specific
- `using Aspose.Pdf.Forms;` (52/116 files)
- `using Aspose.Pdf.Security;` (9/116 files)
- `using Aspose.Pdf.Annotations;` (4/116 files)
- `using Aspose.Pdf.Drawing;` (2/116 files)
- `using Aspose.Pdf.Signatures;` (2/116 files)
- `using Aspose.Pdf.Text;` (1/116 files)
- `using System;` (78/116 files)
- `using System.IO;` (78/116 files)
- `using System.Security.Cryptography.X509Certificates;` (16/116 files)
- `using System.Collections.Generic;` (5/116 files)
- `using System.Linq;` (4/116 files)
- `using System.Security.Cryptography;` (3/116 files)
- `using Azure.Storage.Blobs;` (1/116 files)
- `using System.Drawing.Imaging;` (1/116 files)
- `using System.IO.Compression;` (1/116 files)
- `using System.Net.Http;` (1/116 files)
- `using System.Text;` (1/116 files)
- `using System.Threading;` (1/116 files)
- `using System.Threading.Tasks;` (1/116 files)
- `using System.Xml;` (1/116 files)

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
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add digital signature to pdf |  | Add digital signature to pdf |
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Add Invisible Digital Signature to PDF | `Document`, `Page`, `Rectangle` | Shows how to create an invisible digital signature field, sign it with a PKCS#7 certificate, reco... |
| [add-new-empty-signature-field](./add-new-empty-signature-field.cs) | Add a New Empty Signature Field to a PDF | `Document`, `SignatureField`, `Rectangle` | The example loads a PDF, lists any existing signature fields, creates a new empty signature field... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second Page of PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to place a visible digital signature in the bottom‑right corner of the second pa... |
| [add-warning-overlay-disable-copy-paste](./add-warning-overlay-disable-copy-paste.cs) | Add warning overlay disable copy paste |  | Add warning overlay disable copy paste |
| [apply-invisible-digital-signature](./apply-invisible-digital-signature.cs) | Apply invisible digital signature |  | Apply invisible digital signature |
| [apply-pkcs7-detached-digital-signature](./apply-pkcs7-detached-digital-signature.cs) | Apply PKCS#7 Detached Digital Signature to PDF | `Document`, `SignatureField`, `PKCS7Detached` | Demonstrates loading a PDF from a file stream, adding a signature field, and applying a PKCS#7 de... |
| [apply-pkcs7-detached-signature-to-pdf](./apply-pkcs7-detached-signature-to-pdf.cs) | Apply pkcs7 detached signature to pdf |  | Apply pkcs7 detached signature to pdf |
| [batch-decrypt-pdf-lookup](./batch-decrypt-pdf-lookup.cs) | Batch decrypt pdf lookup |  | Batch decrypt pdf lookup |
| [batch-decrypt-pdfs-lookup-table](./batch-decrypt-pdfs-lookup-table.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Document(string, string)`, `Decrypt` | Demonstrates iterating over a folder of encrypted PDFs, trying multiple possible passwords from a... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Digitally Sign PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with AES‑256 and then apply a PKCS#7 digital signa... |
| [batch-encrypt-pdfs-archive-zip](./batch-encrypt-pdfs-archive-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and then pa... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs Using File Name Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Iterates through all PDF files in a folder, derives a password from each file name, encrypts each... |
| [batch-encrypt-pdfs-date-password](./batch-encrypt-pdfs-date-password.cs) | Batch Encrypt PDFs with Date-Based Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-encrypt-pdfs-deterministic-passwords](./batch-encrypt-pdfs-deterministic-passwords.cs) | Batch encrypt pdfs deterministic passwords |  | Batch encrypt pdfs deterministic passwords |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to iterate over a folder of PDF files, add a digital signature field, sign each ... |
| [batch-sign-pdfs-metadata-certificate](./batch-sign-pdfs-metadata-certificate.cs) | Batch sign pdfs metadata certificate |  | Batch sign pdfs metadata certificate |
| [batch-sign-pdfs-title-based-certificate-selection](./batch-sign-pdfs-title-based-certificate-selection.cs) | Batch Sign PDFs with Title-Based Certificate Selection | `Document`, `DocumentInfo`, `PKCS7` | Loads PDFs from a folder, selects a certificate based on the document title, creates a PKCS7 sign... |
| [batch-sign-pdfs-with-certificate-per-document-type](./batch-sign-pdfs-with-certificate-per-document-type.cs) | Batch Sign PDFs with Certificate per Document Type | `Document`, `Info`, `SignatureField` | Loads PDF files from an input folder, determines a document type from the PDF title, retrieves th... |
| [batch-sign-pdfs-with-certificate](./batch-sign-pdfs-with-certificate.cs) | Batch Sign PDFs with Certificate and Timestamp | `Document`, `Page`, `Rectangle` | Demonstrates how to iterate over PDF files in a folder and apply a PKCS#7 digital signature using... |
| [batch-sign-pdfs-with-document-type-certificates](./batch-sign-pdfs-with-document-type-certificates.cs) | Batch sign pdfs with document type certificates |  | Batch sign pdfs with document type certificates |
| [certified-pdf-signature-allow-add-pages](./certified-pdf-signature-allow-add-pages.cs) | Create a Certified PDF Signature Allowing Page Additions | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to add a certification (MDP) signature to a PDF using Aspose.Pdf so that new pag... |
| [certify-pdf-allow-annotations](./certify-pdf-allow-annotations.cs) | Certify pdf allow annotations |  | Certify pdf allow annotations |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Custom Security Handler for Logging PDF Access Attempts | `Document`, `ICustomSecurityHandler`, `Permissions` | Demonstrates how to implement a custom ICustomSecurityHandler that logs password checks and encry... |
| [custom-security-handler-logging](./custom-security-handler-logging.cs) | Custom security handler logging |  | Custom security handler logging |
| [custom-security-handler-restrict-copy-paste](./custom-security-handler-restrict-copy-paste.cs) | Custom security handler restrict copy paste |  | Custom security handler restrict copy paste |
| [custom-security-handler-warning-overlay](./custom-security-handler-warning-overlay.cs) | Custom Security Handler with Warning Overlay | `Document`, `Permissions`, `Encrypt` | Shows how to attach a custom ICustomSecurityHandler to a PDF to disable copy‑paste permissions an... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt()`, `Save()` | Shows how to open an encrypted PDF with a user password, remove its encryption, and extract all e... |
| [decrypt-pdf-extract-images-reencrypt](./decrypt-pdf-extract-images-reencrypt.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Page` | The example opens an encrypted PDF using a known password, decrypts it, extracts all embedded ima... |
| [decrypt-pdf-resign-multiple-certificates](./decrypt-pdf-resign-multiple-certificates.cs) | Decrypt pdf resign multiple certificates |  | Decrypt pdf resign multiple certificates |
| ... | | | *and 86 more files* |

## Category Statistics
- Total examples: 116

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

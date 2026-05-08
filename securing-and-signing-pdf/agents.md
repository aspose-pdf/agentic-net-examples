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

- `using Aspose.Pdf;` (83/83 files) ← category-specific
- `using Aspose.Pdf.Forms;` (55/83 files) ← category-specific
- `using Aspose.Pdf.Security;` (10/83 files)
- `using Aspose.Pdf.Annotations;` (5/83 files)
- `using Aspose.Pdf.Drawing;` (3/83 files)
- `using Aspose.Pdf.Signatures;` (1/83 files)
- `using Aspose.Pdf.Text;` (1/83 files)
- `using Aspose.Pdf.XfaConverter;` (1/83 files)
- `using System;` (83/83 files)
- `using System.IO;` (83/83 files)
- `using System.Security.Cryptography.X509Certificates;` (16/83 files)
- `using System.Collections.Generic;` (7/83 files)
- `using System.Linq;` (4/83 files)
- `using System.Security.Cryptography;` (3/83 files)
- `using System.Drawing;` (2/83 files)
- `using System.Collections;` (1/83 files)
- `using System.Data;` (1/83 files)
- `using System.Drawing.Imaging;` (1/83 files)
- `using System.IO.Compression;` (1/83 files)
- `using System.Net.Http;` (1/83 files)
- `using System.Text;` (1/83 files)
- `using System.Threading.Tasks;` (1/83 files)
- `using System.Xml;` (1/83 files)

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
| [add-invisible-digital-signature-to-pdf](./add-invisible-digital-signature-to-pdf.cs) | Add Invisible Digital Signature with Timestamp to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to apply an invisible PKCS#7 digital signature to an existing PDF, recording the... |
| [add-visible-digital-signature-second-page](./add-visible-digital-signature-second-page.cs) | Add Visible Digital Signature to Second Page of PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to place a visible PKCS#7 digital signature in the bottom‑right corner of the se... |
| [batch-decrypt-pdfs-lookup](./batch-decrypt-pdfs-lookup.cs) | Batch Decrypt Password-Protected PDFs Using a Lookup Table | `Document`, `Decrypt`, `Save` | Demonstrates how to decrypt multiple password‑protected PDF files in a folder by reading password... |
| [batch-encrypt-and-sign-pdfs](./batch-encrypt-and-sign-pdfs.cs) | Batch Encrypt and Sign PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with AES‑256 and then apply a PKCS#7 digital signa... |
| [batch-encrypt-pdfs-by-filename](./batch-encrypt-pdfs-by-filename.cs) | Batch Encrypt PDFs with Passwords Derived from File Names | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt every PDF in a folder using Aspose.Pdf, creating user and owner passwords fr... |
| [batch-encrypt-pdfs-with-date-based-passwords](./batch-encrypt-pdfs-with-date-based-passwords.cs) | Batch Encrypt PDFs with Date-Based Passwords | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files in a folder using Aspose.Pdf, generating a unique ... |
| [batch-encrypt-pdfs-zip](./batch-encrypt-pdfs-zip.cs) | Batch Encrypt PDFs and Archive to ZIP | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt multiple PDF files with unique passwords using Aspose.Pdf and store t... |
| [batch-sign-and-compress-pdfs](./batch-sign-and-compress-pdfs.cs) | Batch Sign and Compress PDFs | `Document`, `Page`, `SignatureField` | Shows how to iterate through PDF files, add a PKCS#7 digital signature field to each document, op... |
| [batch-sign-pdfs-by-author-certificate](./batch-sign-pdfs-by-author-certificate.cs) | Batch Sign PDFs Using Author Metadata to Select Certificates | `Document`, `Info`, `Page` | Demonstrates how to process a folder of PDFs, read each document's Author metadata, choose a matc... |
| [batch-sign-pdfs-unique-visible-signature](./batch-sign-pdfs-unique-visible-signature.cs) | Batch Sign PDFs with Unique Visible Signature Images | `Document`, `Page`, `Rectangle` | Demonstrates how to process multiple PDF files, add a distinct visible signature image to each, a... |
| [batch-sign-pdfs-with-doc-type-certificates](./batch-sign-pdfs-with-doc-type-certificates.cs) | Batch Sign PDFs with Document-Type Specific Certificates | `Document`, `PKCS7`, `Sign` | Demonstrates how to sign multiple PDF files in a folder, selecting a certificate based on each do... |
| [certify-pdf-append-only-signature](./certify-pdf-append-only-signature.cs) | Certify PDF with Append-Only Signature to Allow Annotations | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to add a certification signature to a PDF that permits further annotations by en... |
| [certify-pdf-form-filling-permission](./certify-pdf-form-filling-permission.cs) | Certify PDF with Form Filling Permission | `PKCS7`, `DocMDPSignature`, `PdfFileSignature` | Shows how to apply a DocMDP certification signature to a PDF using Aspose.Pdf, allowing form fill... |
| [certify-pdf-prevent-modifications](./certify-pdf-prevent-modifications.cs) | Certify PDF to Prevent Further Modifications | `Document`, `Page`, `Rectangle` | Shows how to add a certification signature to a PDF with Aspose.Pdf and set SignaturesAppendOnly ... |
| [compute-verify-pdf-hash-before-after-signing](./compute-verify-pdf-hash-before-after-signing.cs) | Compute and Verify PDF Hash Before and After Signing | `Document`, `Save`, `SignaturesCompromiseDetector` | Shows how to calculate a SHA‑256 hash of a PDF before signing, sign the document, recompute the h... |
| [custom-security-handler-disable-copy-paste](./custom-security-handler-disable-copy-paste.cs) | Apply Custom Security Handler to Disable Copy‑Paste with War... | `Document`, `Page`, `TextAnnotation` | Demonstrates how to add a visible warning annotation to each PDF page and protect the document wi... |
| [custom-security-handler-logging-pdf-access](./custom-security-handler-logging-pdf-access.cs) | Custom Security Handler Logging PDF Access | `Document`, `ICustomSecurityHandler`, `EncryptionParameters` | Demonstrates how to implement a custom ICustomSecurityHandler that logs encryption/decryption ope... |
| [decrypt-extract-images-reencrypt-pdf](./decrypt-extract-images-reencrypt-pdf.cs) | Decrypt PDF, Extract Images, and Re‑encrypt with New Owner P... | `Document`, `Decrypt`, `Page` | The example loads an encrypted PDF using a user password, extracts all embedded images to a folde... |
| [decrypt-pdf-extract-embedded-images](./decrypt-pdf-extract-embedded-images.cs) | Decrypt PDF and Extract Embedded Images | `Document`, `Decrypt`, `Page` | Demonstrates opening an encrypted PDF with a user password, decrypting it, and extracting all emb... |
| [decrypt-pdf-re-sign-fields](./decrypt-pdf-re-sign-fields.cs) | Decrypt PDF and Re‑sign Each Signature Field with Different ... | `Document`, `Decrypt`, `SignatureField` | The example opens an encrypted PDF, removes its password protection, locates all signature fields... |
| [decrypt-pdf-update-metadata](./decrypt-pdf-update-metadata.cs) | Decrypt PDF with Owner Password and Update Metadata | `Document`, `Decrypt`, `Info` | Demonstrates opening an encrypted PDF using the owner password, decrypting it, modifying document... |
| [decrypt-pdf-using-owner-password](./decrypt-pdf-using-owner-password.cs) | Decrypt Password-Protected PDF Using Owner Password | `Document`, `Document(string, string)`, `Decrypt()` | Demonstrates opening an encrypted PDF with the owner password, decrypting it, and saving the unpr... |
| [decrypt-pdf-with-certificate-token](./decrypt-pdf-with-certificate-token.cs) | Decrypt PDF Using Certificate from Hardware Token | `Document`, `CertificateEncryptionOptions`, `StoreName` | Shows how to open a PDF encrypted with a certificate, retrieve the private key from a hardware to... |
| [digitally-sign-pdf-lock-readonly](./digitally-sign-pdf-lock-readonly.cs) | Digitally Sign PDF and Lock It for Read‑Only Access | `Document`, `SignatureField`, `PKCS7` | The example loads a PDF, adds a digital signature field, signs it with a PKCS#7 certificate, then... |
| [download-sign-upload-pdf-azure-blob](./download-sign-upload-pdf-azure-blob.cs) | Download, Sign, and Upload PDF to Azure Blob Storage | `Document`, `SignatureField`, `PKCS1` | The sample downloads a PDF (with a local fallback), adds a digital signature using a PFX certific... |
| [encrypt-pdf-aes256-user-owner-password](./encrypt-pdf-aes256-user-owner-password.cs) | Encrypt PDF with AES‑256 and Set User/Owner Passwords | `Document`, `Encrypt`, `Save` | Shows how to load a PDF with Aspose.Pdf, apply AES‑256 encryption, set both user and owner passwo... |
| [encrypt-pdf-allow-copy-block-print](./encrypt-pdf-allow-copy-block-print.cs) | Encrypt PDF and Allow Copying While Blocking Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using AES‑256, permit content extraction (copy) and disable pri... |
| [encrypt-pdf-custom-security-handler](./encrypt-pdf-custom-security-handler.cs) | Encrypt PDF with Custom Security Handler to Disable Copy‑Pas... | `Document`, `Encrypt`, `Permissions` | Demonstrates how to apply a custom security handler when encrypting a PDF with Aspose.Pdf, settin... |
| [encrypt-pdf-disable-printing](./encrypt-pdf-disable-printing.cs) | Encrypt PDF with User Password and Disable Printing | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using AES‑256 with user and owner passwords while restricting permissi... |
| [encrypt-pdf-owner-password-high-res-printing](./encrypt-pdf-owner-password-high-res-printing.cs) | Encrypt PDF with Owner Password and High‑Resolution Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using an owner password, enable high‑resolution printing, and block co... |
| ... | | | *and 53 more files* |

## Category Statistics
- Total examples: 83

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for securing-and-signing-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

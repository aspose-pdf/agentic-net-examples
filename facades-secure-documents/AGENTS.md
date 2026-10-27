---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **39** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (34/39 files) ← category-specific
- `using Aspose.Pdf.Facades;` (18/39 files)
- `using Aspose.Pdf.Security;` (4/39 files)
- `using Aspose.Pdf.Text;` (2/39 files)
- `using System;` (39/39 files)
- `using System.IO;` (39/39 files)
- `using Azure.Identity;` (1/39 files)
- `using Azure.Security.KeyVault.Secrets;` (1/39 files)
- `using Azure.Storage.Blobs;` (1/39 files)
- `using Azure.Storage.Blobs.Models;` (1/39 files)
- `using NUnit.Framework;` (1/39 files)
- `using System.Collections.Generic;` (1/39 files)
- `using System.Diagnostics;` (1/39 files)
- `using System.Security.Cryptography;` (1/39 files)
- `using System.Text;` (1/39 files)
- `using System.Threading;` (1/39 files)

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
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to PDF with AES‑256 Encryption | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to load an existing PDF, set user and owner passwords, define allowed permissions, encr... |
| [batch-decrypt-pdfs-owner-passwords](./batch-decrypt-pdfs-owner-passwords.cs) | Batch Decrypt PDFs Using Owner Passwords | `Document`, `Save` | Shows how to read a configuration file and decrypt multiple encrypted PDF files by providing the ... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDF Files with User and Owner Passwords | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt every PDF in a folder using Aspose.Pdf by applying the same user and ... |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Update PDF User Passwords in Bulk | `Document`, `Encrypt`, `Permissions` | Iterates through a folder of PDF files, re‑encrypts each document with a standardized user passwo... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change PDF User and Owner Passwords from CSV | `Document`, `Encrypt`, `Save` | Reads a CSV file containing PDF paths and new passwords, then updates each PDF's user and owner p... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF While Preserving Setti... | `Document`, `Encrypt`, `Save` | Demonstrates opening an encrypted PDF with the existing user password, re‑encrypting it with a ne... |
| [check-pdf-encryption-and-toggle-protection](./check-pdf-encryption-and-toggle-protection.cs) | Check PDF Encryption and Perform Encrypt/Decrypt | `PdfFileInfo`, `Document`, `Encrypt` | Demonstrates how to use Aspose.Pdf Facades to check if a PDF is encrypted, then encrypt an unprot... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Shows how to bind a PDF with PdfFileSignature and determine if it contains extended usage rights ... |
| [conditional-decrypt-modify-pdf](./conditional-decrypt-modify-pdf.cs) | Conditionally Decrypt and Modify PDF with Aspose.Pdf | `PdfFileInfo`, `Document`, `TextFragment` | The example checks if a PDF is encrypted, opens it with a password when needed, adds a text fragm... |
| [conditional-pdf-encryption-based-on-filename](./conditional-pdf-encryption-based-on-filename.cs) | Encrypt PDFs Using Filename-Based CryptoAlgorithm | `Document`, `Encrypt`, `Save` | Shows how to encrypt multiple PDF files with Aspose.Pdf, selecting the CryptoAlgorithm (AES‑256, ... |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF Using Owner Password | `Document`, `Save` | Loads an encrypted PDF with the owner password and saves an unencrypted copy of the document. |
| [decrypt-pdf-using-owner-password-azure-key-vault](./decrypt-pdf-using-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `Document`, `Decrypt`, `Save` | Demonstrates retrieving an owner password from Azure Key Vault and using Aspose.Pdf to open, decr... |
| [directory-watcher-pdf-encryption-service](./directory-watcher-pdf-encryption-service.cs) | Directory Watcher PDF Encryption Service | `Document`, `Encrypt`, `Save` | Shows how to monitor a folder for new PDF files, encrypt each PDF with user and owner passwords u... |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Disable Copying While Enabling Printing for PDF | `Document`, `Permissions`, `Encrypt` | Demonstrates how to protect a PDF with Aspose.Pdf by allowing printing but disabling content copy... |
| [encrypt-decrypt-pdf-roundtrip](./encrypt-decrypt-pdf-roundtrip.cs) | Encrypt and Decrypt PDF with AES‑256 and Verify Round‑Trip | `Document`, `Encrypt`, `Decrypt` | Demonstrates applying AES‑256 encryption to a PDF with Aspose.Pdf, decrypting it, and comparing S... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and Passwords | `Document`, `Encrypt`, `Save` | Shows how to load a PDF, apply 256‑bit AES encryption with user and owner passwords and set permi... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Permissions | `Document`, `Permissions`, `Encrypt` | Demonstrates how to load a PDF, apply AES‑256 encryption with user and owner passwords, set speci... |
| [encrypt-pdf-aes256-upload](./encrypt-pdf-aes256-upload.cs) | Encrypt PDF with AES‑256 and Upload to Azure Blob Storage | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt a PDF using AES‑256 with Aspose.Pdf, save the encrypted file, and upl... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array | `Document`, `Encrypt`, `Permissions` | Loads a PDF from a byte array, applies AES‑256 encryption with user and owner passwords, and retu... |
| [encrypt-pdf-rc4-128-print-edit-privileges](./encrypt-pdf-rc4-128-print-edit-privileges.cs) | Encrypt PDF with RC4‑128 and Allow Printing and Editing | `Document`, `Permissions`, `Encrypt` | The example loads a PDF, combines permission flags to allow printing and content modification, en... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `Document`, `Encrypt`, `Save` | Shows how to load a PDF, apply RC4‑40 encryption with user and owner passwords, set permissions, ... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `Document`, `Encrypt`, `Save` | Shows how to load a PDF from a stream, apply RC4‑40 encryption with user and owner passwords, and... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Load into MemoryStream for Network Transmiss... | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, saving it ... |
| [encrypt-pdf-with-password-and-capture-exceptions](./encrypt-pdf-with-password-and-capture-exceptions.cs) | Encrypt PDF with AES‑256 and Permissions using Aspose.Pdf | `Document`, `Encrypt`, `Permissions` | Demonstrates loading a PDF, applying AES‑256 encryption with user/owner passwords, setting specif... |
| [encrypt-pdf-with-user-password](./encrypt-pdf-with-user-password.cs) | Encrypt PDF with User Password Only | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt a PDF using only a user password while leaving the owner password und... |
| [encrypt-pdfs-with-performance-timing](./encrypt-pdfs-with-performance-timing.cs) | Encrypt PDFs and Log Encryption Time | `Document`, `Encrypt`, `Save` | Shows how to load multiple PDF files, encrypt each with user and owner passwords, and measure the... |
| [load-pdf-verify-page-count-facade](./load-pdf-verify-page-count-facade.cs) | Load PDF and Verify Page Count | `Document`, `Pages`, `PageCollection` | Demonstrates loading a PDF file with Aspose.Pdf and confirming successful loading by reading the ... |
| [pdf-decryption-failure-wrong-owner-password-test](./pdf-decryption-failure-wrong-owner-password-test.cs) | Test Decryption Fails with Incorrect Owner Password | `Document`, `Permissions`, `CryptoAlgorithm` | Creates an encrypted PDF, then verifies that opening it with a wrong owner password throws an Inv... |
| [pdf-encryption-summary](./pdf-encryption-summary.cs) | Generate PDF Encryption Summary CSV | `PdfFileInfo` | Scans PDFs in a folder, uses Aspose.Pdf.Facades.PdfFileInfo to detect encryption, and writes a CS... |
| [remove-extended-usage-rights-from-signed-pdf](./remove-extended-usage-rights-from-signed-pdf.cs) | Remove Extended Usage Rights from Signed PDF | `PdfFileSignature`, `BindPdf`, `RemoveUsageRights` | Demonstrates loading a signed PDF with PdfFileSignature, removing any extended usage rights, and ... |
| ... | | | *and 9 more files* |

## Category Statistics
- Total examples: 39

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-secure-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-21 | Run: `20261021_033353_7a47fb`
<!-- AUTOGENERATED:END -->

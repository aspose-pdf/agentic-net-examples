---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **38** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (35/38 files) ← category-specific
- `using Aspose.Pdf.Facades;` (20/38 files) ← category-specific
- `using Aspose.Pdf.Security;` (2/38 files)
- `using Aspose.Pdf.Text;` (1/38 files)
- `using System;` (38/38 files)
- `using System.IO;` (37/38 files)
- `using System.Collections.Generic;` (4/38 files)
- `using System.Collections;` (1/38 files)
- `using System.Diagnostics;` (1/38 files)
- `using System.Reflection;` (1/38 files)
- `using System.Security.Cryptography;` (1/38 files)
- `using System.Text;` (1/38 files)
- `using System.Text.Json;` (1/38 files)

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
| [apply-password-protection-to-pdf__v2](./apply-password-protection-to-pdf__v2.cs) | Apply Password Protection to Existing PDF | `Document`, `Encrypt`, `Permissions` | Shows how to load an existing PDF and add user and owner passwords with specific permissions usin... |
| [batch-decrypt-pdfs-owner-passwords](./batch-decrypt-pdfs-owner-passwords.cs) | Batch Decrypt PDFs Using Owner Passwords from Config | `PdfFileInfo`, `IsEncrypted`, `PdfFileSecurity` | Demonstrates how to read a JSON configuration file and batch‑process multiple encrypted PDFs, dec... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDFs with User and Owner Passwords | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt all PDF files in a folder using the same user and owner passwords, ap... |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Standardize PDF User Passwords in a Folder | `Document`, `Permissions`, `CryptoAlgorithm` | Iterates through all PDF files in a directory, removes any existing encryption and re‑encrypts ea... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change User and Owner Passwords for PDFs from CSV | `Document`, `Encrypt`, `Save` | Shows how to read a CSV file containing PDF paths and new passwords, then apply user and owner pa... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `Document`, `Encrypt`, `Permissions` | Shows how to open an encrypted PDF with the existing user password and re‑encrypt it using a new ... |
| [check-pdf-encryption-and-toggle-protection](./check-pdf-encryption-and-toggle-protection.cs) | Encrypt or Decrypt PDF Based on Encryption Status | `PdfFileInfo`, `IsEncrypted`, `PdfFileSecurity` | The example checks whether a PDF file is already encrypted and either decrypts it with the owner ... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `PdfFileSignature`, `ContainsUsageRights` | Shows how to detect whether a PDF file contains extended usage rights by using the PdfFileSignatu... |
| [conditional-decrypt-modify-pdf](./conditional-decrypt-modify-pdf.cs) | Decrypt and Modify PDF with Aspose.Pdf | `Document`, `InvalidPasswordException`, `Decrypt` | The example loads a PDF, detects if it is encrypted, decrypts it using a user password, adds a te... |
| [conditional-pdf-encryption-based-on-filename](./conditional-pdf-encryption-based-on-filename.cs) | Encrypt PDFs with Algorithm Based on File Name | `Document`, `CryptoAlgorithm`, `Permissions` | Shows how to load PDFs, choose a CryptoAlgorithm according to naming conventions, apply passwords... |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF with Owner Password | `PdfFileInfo`, `PdfFileSecurity`, `BindPdf` | Demonstrates how to verify if a PDF is encrypted and then decrypt it using the owner password wit... |
| [decrypt-pdf-using-owner-password-azure-key-vault](./decrypt-pdf-using-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password with Aspose.Pdf | `Document`, `Save`, `PdfException` | Shows how to open an encrypted PDF with its owner password and save it as an unencrypted file usi... |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Disable Copying While Enabling Printing in PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to protect a PDF with Aspose.Pdf by allowing printing and disabling content extr... |
| [encrypt-decrypt-pdf-roundtrip](./encrypt-decrypt-pdf-roundtrip.cs) | Encrypt and Decrypt PDF with Round‑Trip Verification | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF with AES‑256 using Aspose.Pdf, decrypt it, and confirm that the origin... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and Set Permissions | `Document`, `Encrypt`, `Save` | Shows how to load a PDF, apply 256‑bit AES encryption with user and owner passwords, define print... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Permissions | `Document`, `Encrypt`, `Permissions` | Demonstrates how to apply AES‑256 encryption to a PDF while specifying custom permissions such as... |
| [encrypt-pdf-aes256-upload](./encrypt-pdf-aes256-upload.cs) | Encrypt PDF with AES‑256 and Upload to Cloud Bucket | `Document`, `Encrypt`, `Save` | Shows how to encrypt a PDF using AES‑256 with user and owner passwords, then upload the encrypted... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF with AES‑256 from Byte Array | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF from a byte array, applying AES‑256 encryption with user and owner pas... |
| [encrypt-pdf-rc4-128-print-edit-privileges](./encrypt-pdf-rc4-128-print-edit-privileges.cs) | Encrypt PDF with Print and Edit Permissions (RC4‑128) | `Document`, `Permissions`, `Encrypt` | Demonstrates loading a PDF, combining print and edit privileges, encrypting it with the RC4‑128 a... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply RC4‑40 encryption with user and owner passwords, set permis... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF from a stream, apply RC4‑40 encryption with user and owner passwor... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Return as MemoryStream | `Document`, `Encrypt`, `Save` | Loads a PDF, applies AES‑256 encryption with user and owner passwords, and saves the encrypted do... |
| [encrypt-pdf-with-password-and-capture-exceptions](./encrypt-pdf-with-password-and-capture-exceptions.cs) | Encrypt PDF with AES‑256 and Permissions using Aspose.Pdf | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying AES‑256 encryption with specific permissions, and saving the... |
| [encrypt-pdf-with-user-password](./encrypt-pdf-with-user-password.cs) | Encrypt PDF with User Password Only | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt a PDF using Aspose.Pdf by providing only a user password (owner passw... |
| [encrypt-pdfs-with-performance-timing](./encrypt-pdfs-with-performance-timing.cs) | Encrypt PDFs and Log Processing Time | `Document`, `Permissions`, `CryptoAlgorithm` | Loads each PDF from a folder, applies password protection with AES‑256 encryption and specific pe... |
| [load-pdf-verify-page-count-facade](./load-pdf-verify-page-count-facade.cs) | Load PDF and Verify Page Count | `Document`, `PageCollection`, `Count` | Shows how to load a PDF file with Aspose.Pdf and confirm successful loading by reading the docume... |
| [pdf-decryption-failure-wrong-owner-password-test](./pdf-decryption-failure-wrong-owner-password-test.cs) | Decrypt PDF with Incorrect Owner Password Throws Exception | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF with user and owner passwords, then attempting to open it with a wr... |
| [pdf-encryption-summary](./pdf-encryption-summary.cs) | Generate PDF Encryption Summary CSV | `Document`, `PdfFileInfo`, `Permissions` | Scans a folder of PDF files, detects whether each file is encrypted, extracts the encryption algo... |
| [remove-extended-usage-rights-from-signed-pdf](./remove-extended-usage-rights-from-signed-pdf.cs) | Remove Usage Rights from Signed PDF | `PdfFileSignature`, `BindPdf`, `RemoveUsageRights` | Demonstrates how to load a signed PDF, remove any extended usage rights, and save the resulting d... |
| [remove-usage-rights-verify](./remove-usage-rights-verify.cs) | Remove Usage Rights from PDF and Verify Removal | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Demonstrates how to detect, remove, and confirm the removal of usage rights in a PDF using Aspose... |
| ... | | | *and 8 more files* |

## Category Statistics
- Total examples: 38

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-secure-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-11 | Run: `20261111_042212_53d92d`
<!-- AUTOGENERATED:END -->

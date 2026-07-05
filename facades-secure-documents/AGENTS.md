---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **39** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/39 files) ← category-specific
- `using Aspose.Pdf;` (23/39 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/39 files)
- `using System;` (39/39 files)
- `using System.IO;` (37/39 files)
- `using System.Collections.Generic;` (3/39 files)
- `using Azure.Identity;` (1/39 files)
- `using Azure.Security.KeyVault.Secrets;` (1/39 files)
- `using NUnit.Framework;` (1/39 files)
- `using System.Diagnostics;` (1/39 files)
- `using System.Reflection;` (1/39 files)
- `using System.Text.Json;` (1/39 files)

## Common Code Pattern

Most files in this category use `PdfFileSecurity` from `Aspose.Pdf.Facades`:

```csharp
PdfFileSecurity tool = new PdfFileSecurity();
tool.BindPdf("input.pdf");
// ... PdfFileSecurity operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to an Existing PDF | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF with user and owner passwords using the PdfFileSecurity faca... |
| [apply-pdf-privilege-allow-exceptions-false](./apply-pdf-privilege-allow-exceptions-false.cs) | Apply PDF Privilege with Exception Propagation | `PdfFileSecurity`, `AllowExceptions`, `SetPrivilege` | Demonstrates using Aspose.Pdf.Facades.PdfFileSecurity to set a document privilege while disabling... |
| [batch-decrypt-pdfs](./batch-decrypt-pdfs.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile`, `Dispose` | Demonstrates how to read a JSON configuration file and batch‑decrypt multiple PDF files using the... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDF Files with User and Owner Passwords | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt every PDF in a directory using Aspose.Pdf.Facades.PdfFileSecurity with the s... |
| [bulk-change-pdf-user-password](./bulk-change-pdf-user-password.cs) | Bulk Change PDF User Password | `PdfFileSecurity`, `TryChangePassword` | Iterates through a folder of PDF files and sets a common user password on each document using Asp... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change PDF User and Owner Passwords from CSV | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to read a CSV file containing PDF paths and passwords, then use Aspose.Pdf.Facades.PdfF... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an existing PDF in a single call ... |
| [change-user-password-encrypted-pdf](./change-user-password-encrypted-pdf.cs) | Change User Password of Encrypted PDF While Preserving Encry... | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to open an encrypted PDF using its owner password, change the user password, and save t... |
| [check-pdf-encryption-and-toggle](./check-pdf-encryption-and-toggle.cs) | Check PDF Encryption and Apply Decrypt/Encrypt | `PdfFileInfo`, `IsEncrypted`, `PdfFileSecurity` | Demonstrates how to inspect a PDF's IsEncrypted property and then either decrypt it with the owne... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Demonstrates how to bind a PDF file to the PdfFileSignature facade and determine whether it conta... |
| [decrypt-edit-save-pdf-facades](./decrypt-edit-save-pdf-facades.cs) | Decrypt, Edit, and Save PDF Using Aspose.Pdf Facades | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Demonstrates how to conditionally decrypt an encrypted PDF with PdfFileSecurity, modify its text ... |
| [decrypt-pdf-using-owner-password-from-azure-key-va...](./decrypt-pdf-using-owner-password-from-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `PdfFileSecurity`, `DecryptFile` | Demonstrates retrieving an owner password from Azure Key Vault and using Aspose.Pdf.Facades to de... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to decrypt an encrypted PDF file using the owner password via Aspose.Pdf.Facades... |
| [disable-copying-keep-printing-enabled](./disable-copying-keep-printing-enabled.cs) | Disable Copying While Keeping Printing Enabled for PDF | `DocumentPrivilege`, `PdfFileSecurity`, `BindPdf` | Shows how to use Aspose.Pdf.Facades to disable the copy permission on a PDF while leaving the pri... |
| [encrypt-decrypt-pdf-integrity-check](./encrypt-decrypt-pdf-integrity-check.cs) | Encrypt and Decrypt PDF with Integrity Check | `PdfFileSecurity`, `EncryptFile`, `DecryptFile` | Demonstrates how to encrypt a PDF with user/owner passwords, decrypt it using the owner password,... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt an existing PDF using 256‑bit AES, set user and owner passwords, appl... |
| [encrypt-pdf-aes256-custom-permissions](./encrypt-pdf-aes256-custom-permissions.cs) | Encrypt PDF with AES‑256 and Custom Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply AES‑256 encryption together with specific permissions, and ... |
| [encrypt-pdf-aes256-to-cloud](./encrypt-pdf-aes256-to-cloud.cs) | Encrypt PDF with AES‑256 and Save to Cloud Bucket | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to use Aspose.Pdf to encrypt a PDF with AES‑256 and write the encrypted file to a cloud... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array | `Document`, `PdfFileSecurity`, `SetPrivilege` | Shows how to load a PDF from a byte array, apply user and owner passwords using PdfFileSecurity, ... |
| [encrypt-pdf-rc4-128-allow-print-edit](./encrypt-pdf-rc4-128-allow-print-edit.cs) | Encrypt PDF with RC4‑128 and Allow Print/Edit | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates combining document privileges to permit printing and content modification, then encr... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF file with the RC4‑40 algorithm and a user password using the Aspose.Pd... |
| [encrypt-pdf-save-to-memorystream](./encrypt-pdf-save-to-memorystream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Page`, `TextFragment` | Shows how to create a PDF in memory, apply AES‑128 encryption with user and owner passwords and s... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `Document`, `PdfFileSecurity`, `EncryptFile` | Demonstrates how to load a PDF from a stream, apply RC4‑40 encryption with Aspose.Pdf, and return... |
| [encrypt-pdf-with-password-and-capture-exceptions](./encrypt-pdf-with-password-and-capture-exceptions.cs) | Encrypt PDF with Password and Capture Exceptions | `PdfFileSecurity`, `BindPdf`, `AllowExceptions` | Demonstrates how to encrypt a PDF using a user and owner password with 256‑bit AES, enable AllowE... |
| [encrypt-pdf-with-user-password-only](./encrypt-pdf-with-user-password-only.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF using Aspose.Pdf.Facades by supplying only a user password while leavi... |
| [encrypt-pdfs-and-log-encryption-time](./encrypt-pdfs-and-log-encryption-time.cs) | Encrypt PDFs and Log Encryption Time | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Shows how to encrypt PDF files with Aspose.Pdf.Facades and measure the time taken for each encryp... |
| [encrypt-pdfs-based-on-filename](./encrypt-pdfs-based-on-filename.cs) | Encrypt PDFs with Conditional CryptoAlgorithm Based on File ... | `PdfFileSecurity`, `EncryptFile`, `KeySize` | Demonstrates encrypting PDF files using Aspose.Pdf.Facades, selecting the key size and algorithm ... |
| [load-pdf-verify-page-count](./load-pdf-verify-page-count.cs) | Load PDF and Verify Page Count | `Document`, `Pages`, `Count` | Shows how to load a PDF file using Aspose.Pdf's Document class and confirm successful loading by ... |
| [pdf-encryption-report-generator](./pdf-encryption-report-generator.cs) | Generate PDF Encryption Algorithm Report | `PdfFileInfo`, `IsEncrypted`, `EncryptionAlgorithmName` | Scans a directory of PDF files, checks if each file is encrypted, retrieves the encryption algori... |
| [pdf-encryption-summary](./pdf-encryption-summary.cs) | Generate PDF Encryption Summary CSV | `PdfFileInfo`, `IsEncrypted`, `GetDocumentPrivilege` | Scans a folder of PDF files, uses Aspose.Pdf.Facades.PdfFileInfo to detect encryption status and ... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

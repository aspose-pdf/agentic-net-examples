---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (38/40 files) ← category-specific
- `using Aspose.Pdf;` (20/40 files) ← category-specific
- `using Aspose.Pdf.Security;` (1/40 files)
- `using Aspose.Pdf.Text;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (34/40 files)
- `using System.Collections.Generic;` (3/40 files)
- `using NUnit.Framework;` (1/40 files)
- `using System.Diagnostics;` (1/40 files)
- `using System.Drawing;` (1/40 files)
- `using System.Runtime.InteropServices;` (1/40 files)
- `using System.Text.Json;` (1/40 files)
- `using System.Threading;` (1/40 files)

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
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to PDF Using PdfFileSecurity | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF with user and owner passwords using the Aspose.Pdf.Facades.P... |
| [batch-change-pdf-passwords-from-csv](./batch-change-pdf-passwords-from-csv.cs) | Batch Change PDF User and Owner Passwords from CSV | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to read a CSV file and use Aspose.Pdf.Facades.PdfFileSecurity to change both user and o... |
| [batch-decrypt-pdfs-owner-passwords](./batch-decrypt-pdfs-owner-passwords.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile`, `Dispose` | Demonstrates reading a JSON configuration of PDF files and owner passwords, then decrypting each ... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDF Files with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt every PDF in a directory using Aspose.Pdf.Facades.PdfFileSecurity with the s... |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Batch Update PDF User Passwords | `PdfFileSecurity`, `ChangePassword` | Iterates through a folder of PDF files and applies a standardized user password to each document ... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword`, `Save` | Demonstrates how to update both the user and owner passwords of a PDF file in a single call using... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `ChangePassword` | Shows how to update the user password of an already encrypted PDF while keeping the existing encr... |
| [check-pdf-encryption-and-apply-security](./check-pdf-encryption-and-apply-security.cs) | Check PDF Encryption and Apply Encryption/Decryption | `PdfFileInfo`, `PdfFileSecurity`, `BindPdf` | Demonstrates how to inspect the IsEncrypted property of a PDF using PdfFileInfo and then encrypt ... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Shows how to bind a PDF with Aspose.Pdf.Facades.PdfFileSignature and determine if it contains ext... |
| [conditional-decrypt-add-text-pdf](./conditional-decrypt-add-text-pdf.cs) | Conditional Decrypt and Add Text to PDF | `PdfFileSecurity`, `BindPdf`, `TryDecryptFile` | Shows how to open a PDF, optionally decrypt it with an owner password, insert a text annotation u... |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF Using Owner Password with Aspose.Pdf | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to decrypt an encrypted PDF by supplying the owner password (retrieved from an e... |
| [decrypt-pdf-using-owner-password](./decrypt-pdf-using-owner-password.cs) | Decrypt PDF Using Owner Password | `PdfFileSecurity`, `DecryptFile` | Shows how to remove encryption from a PDF file by providing the owner password with Aspose.Pdf.Fa... |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Enable Printing and Disable Copying in PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to protect a PDF by allowing printing while preventing content extraction (copyi... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF with 256‑bit AES, set user and owner passwords, define privileg... |
| [encrypt-pdf-aes256-cloud](./encrypt-pdf-aes256-cloud.cs) | Encrypt PDF with AES‑256 and Save to Cloud Storage | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF using AES‑256 with Aspose.Pdf.Facades and write the encrypted f... |
| [encrypt-pdf-aes256-custom-permissions](./encrypt-pdf-aes256-custom-permissions.cs) | Encrypt PDF with AES‑256 and Custom Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to apply AES‑256 encryption to a PDF while specifying custom user privileges suc... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to load a PDF from a byte array, encrypt it with user and owner passwords using Aspose.... |
| [encrypt-pdf-rc4-128-print-edit-privileges](./encrypt-pdf-rc4-128-print-edit-privileges.cs) | Encrypt PDF with RC4‑128 and Set Print/Edit Privileges | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Shows how to combine printing and editing privileges and encrypt a PDF using RC4‑128 with Aspose.... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt an existing PDF file with the RC4‑40 algorithm and a user password us... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt a PDF provided as a stream using RC4‑40 encryption with Aspose.Pdf.Facades a... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Save to MemoryStream | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF with user and owner passwords using Aspose.Pdf.Facades.PdfFileS... |
| [encrypt-pdf-verify-roundtrip](./encrypt-pdf-verify-roundtrip.cs) | Encrypt PDF and Verify Round‑Trip Decryption | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF with user/owner passwords using Aspose.Pdf, then decrypt it and... |
| [encrypt-pdf-with-exception-handling](./encrypt-pdf-with-exception-handling.cs) | Encrypt PDF with Exception Handling using PdfFileSecurity | `PdfFileSecurity`, `AllowExceptions`, `BindPdf` | Demonstrates enabling AllowExceptions on PdfFileSecurity, encrypting a PDF with AES‑256 and Print... |
| [encrypt-pdf-with-user-password-only](./encrypt-pdf-with-user-password-only.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF using Aspose.Pdf by providing only a user password; the owner password... |
| [encrypt-pdfs-based-on-filename](./encrypt-pdfs-based-on-filename.cs) | Encrypt PDFs with Algorithm Selection Based on File Name | `PdfFileSecurity`, `EncryptFile`, `Algorithm` | Scans a directory for PDF files, determines the encryption algorithm and key size from each file'... |
| [encrypt-pdfs-with-performance-timing](./encrypt-pdfs-with-performance-timing.cs) | Encrypt PDFs with Performance Timing | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates encrypting multiple PDF files using Aspose.Pdf.Facades.PdfFileSecurity while measuri... |
| [folder-watcher-pdf-encryption-service](./folder-watcher-pdf-encryption-service.cs) | Folder Watcher PDF Encryption Service | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to monitor a directory for new PDF files, encrypt them with user and owner passw... |
| [generate-pdf-encryption-report](./generate-pdf-encryption-report.cs) | Generate PDF Encryption Report Using Aspose.Pdf.Facades | `PdfFileInfo`, `IsEncrypted` | Scans a directory of PDF files, uses PdfFileInfo to determine if each file is encrypted, and writ... |
| [get-pdf-encryption-algorithm](./get-pdf-encryption-algorithm.cs) | Get PDF Encryption Algorithm using Aspose.Pdf | `PdfFileInfo`, `IsEncrypted`, `Document` | Demonstrates how to check if a PDF is encrypted and retrieve its current encryption algorithm via... |
| [load-pdf-verify-page-count-facade](./load-pdf-verify-page-count-facade.cs) | Load PDF and Verify Page Count Using Facade API | `PdfFileInfo`, `BindPdf`, `Document` | Demonstrates how to load a PDF file with Aspose.Pdf.Facades.PdfFileInfo and verify successful loa... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-secure-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_123045_47f406`
<!-- AUTOGENERATED:END -->

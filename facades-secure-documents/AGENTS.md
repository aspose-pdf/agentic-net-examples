---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **39** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (38/39 files) ← category-specific
- `using Aspose.Pdf;` (24/39 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/39 files)
- `using System;` (39/39 files)
- `using System.IO;` (36/39 files)
- `using Azure.Identity;` (1/39 files)
- `using Azure.Security.KeyVault.Secrets;` (1/39 files)
- `using NUnit.Framework;` (1/39 files)
- `using System.Collections.Generic;` (1/39 files)
- `using System.Diagnostics;` (1/39 files)

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
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to PDF without Loading Document | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF file with user and owner passwords using the PdfFileSecurity... |
| [batch-decrypt-pdfs-owner-passwords](./batch-decrypt-pdfs-owner-passwords.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile`, `PdfFileSecurity (constructor)` | Shows how to read a simple configuration file containing PDF paths and owner passwords, then decr... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDF Files with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt every PDF in a directory using Aspose.Pdf.Facades.PdfFileSecurity with a com... |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Batch Update PDF User Passwords | `PdfFileSecurity`, `TryChangePassword` | Shows how to iterate through a folder of PDF files and change each file's user password to a stan... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change User and Owner Passwords for PDFs from CSV | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to read PDF file paths and passwords from a CSV file and use Aspose.Pdf.Facades.PdfFile... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an encrypted PDF in a single call... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `ChangePassword`, `ctor` | Demonstrates how to update the user password of an already encrypted PDF while keeping the existi... |
| [check-pdf-encryption-and-toggle-protection](./check-pdf-encryption-and-toggle-protection.cs) | Check PDF Encryption and Apply or Remove Protection | `PdfFileInfo`, `Document`, `Encrypt` | The example shows how to inspect a PDF's IsEncrypted flag using PdfFileInfo, then either encrypt ... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `Document`, `PdfFileSignature`, `BindPdf` | Shows how to use Aspose.Pdf's PdfFileSignature facade to determine if a PDF contains extended usa... |
| [conditional-decrypt-modify-pdf](./conditional-decrypt-modify-pdf.cs) | Conditional Decrypt and Modify PDF with Aspose.Pdf | `Document`, `PdfFileSecurity`, `DecryptFile` | The example loads a PDF, detects if it is encrypted, decrypts it using the owner password when ne... |
| [conditional-pdf-encryption-based-on-filename](./conditional-pdf-encryption-based-on-filename.cs) | Conditional PDF Encryption Based on File Name | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf, selecting the key size and encry... |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF Using Owner Password | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to remove encryption from a PDF file by providing the owner password using Aspos... |
| [decrypt-pdf-using-owner-password-azure-key-vault](./decrypt-pdf-using-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `PdfFileSecurity`, `DecryptFile` | Shows how to retrieve an owner password stored in Azure Key Vault and use Aspose.Pdf.Facades to d... |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Disable Copying While Enabling Printing for PDF | `PdfFileSecurity`, `SetPrivilege`, `DocumentPrivilege` | Demonstrates using Aspose.Pdf.Facades.PdfFileSecurity to set document privileges so that copying ... |
| [encrypt-decrypt-pdf-roundtrip](./encrypt-decrypt-pdf-roundtrip.cs) | Encrypt and Decrypt PDF with Round‑Trip Verification | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF using a user and owner password, decrypt it back, and verify in... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES using Aspose.Pdf | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF with 256‑bit AES, set user and owner passwords, and apply a print priv... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates encrypting a PDF using AES‑256 while applying custom document privileges such as all... |
| [encrypt-pdf-aes256-upload](./encrypt-pdf-aes256-upload.cs) | Encrypt PDF with AES‑256 and Upload to Cloud Storage | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt a PDF using Aspose.Pdf.Facades with AES‑256 and then upload the encrypted st... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array using Aspose.Pdf | `Document`, `PdfFileSecurity`, `DocumentPrivilege` | Shows how to load a PDF from a byte array, apply 256‑bit AES encryption with user and owner passw... |
| [encrypt-pdf-rc4-128-print-edit-privileges](./encrypt-pdf-rc4-128-print-edit-privileges.cs) | Encrypt PDF with RC4‑128 and Enable Print/Edit Privileges | `Document`, `DocumentPrivilege`, `PdfFileSecurity` | Demonstrates how to combine print and edit privileges, then encrypt a PDF using RC4‑128 with Aspo... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 and Password | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF using the RC4‑40 algorithm with user and owner passwords and... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt a PDF supplied as a stream with 40‑bit RC4 encryption using Aspose.Pdf's Pdf... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Encrypt`, `Permissions` | Demonstrates loading a PDF with Aspose.Pdf, applying AES‑256 encryption with user/owner passwords... |
| [encrypt-pdf-with-password-and-capture-exceptions](./encrypt-pdf-with-password-and-capture-exceptions.cs) | Encrypt PDF with Password and Capture Exceptions | `PdfFileSecurity`, `AllowExceptions`, `BindPdf` | Demonstrates encrypting a PDF using PdfFileSecurity, enabling AllowExceptions to suppress throws,... |
| [encrypt-pdf-with-user-password](./encrypt-pdf-with-user-password.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF using Aspose.Pdf.Facades by providing only a user password (owner pass... |
| [encrypt-pdfs-with-performance-timing](./encrypt-pdfs-with-performance-timing.cs) | Encrypt PDFs with Performance Timing | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt PDF files using Aspose.Pdf's PdfFileSecurity facade while measuring the time... |
| [load-pdf-verify-page-count-facade](./load-pdf-verify-page-count-facade.cs) | Load PDF and Verify Page Count Using PdfFileInfo Facade | `PdfFileInfo`, `Document`, `Pages` | Demonstrates loading a PDF file via the PdfFileInfo facade class and confirming successful loadin... |
| [pdf-decryption-failure-wrong-owner-password-test](./pdf-decryption-failure-wrong-owner-password-test.cs) | Unit Test for PDF Decryption Failure with Incorrect Owner Pa... | `Document`, `Page`, `TextFragment` | Demonstrates encrypting a PDF with Aspose.Pdf and writing an NUnit test that asserts DecryptFile ... |
| [pdf-encryption-summary](./pdf-encryption-summary.cs) | Generate PDF Encryption Summary with Privileges | `PdfFileInfo`, `IsEncrypted`, `GetDocumentPrivilege` | Demonstrates how to inspect PDF files for encryption status, algorithm (when detectable), and doc... |
| [remove-extended-usage-rights-from-signed-pdf](./remove-extended-usage-rights-from-signed-pdf.cs) | Remove Extended Usage Rights from Signed PDF | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Shows how to load a signed PDF, detect extended usage rights, remove them with PdfFileSignature, ... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **59** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (38/59 files) ← category-specific
- `using Aspose.Pdf;` (24/59 files)
- `using Aspose.Pdf.Text;` (3/59 files)
- `using System;` (39/59 files)
- `using System.IO;` (36/59 files)
- `using Azure.Identity;` (1/59 files)
- `using Azure.Security.KeyVault.Secrets;` (1/59 files)
- `using NUnit.Framework;` (1/59 files)
- `using System.Collections.Generic;` (1/59 files)
- `using System.Diagnostics;` (1/59 files)

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
| [add-text-annotation-to-encrypted-pdf](./add-text-annotation-to-encrypted-pdf.cs) | Add text annotation to encrypted pdf |  | Add text annotation to encrypted pdf |
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to PDF without Loading Document | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF file with user and owner passwords using the PdfFileSecurity... |
| [batch-decrypt-pdfs-from-config](./batch-decrypt-pdfs-from-config.cs) | Batch decrypt pdfs from config |  | Batch decrypt pdfs from config |
| [batch-decrypt-pdfs-owner-passwords](./batch-decrypt-pdfs-owner-passwords.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile`, `PdfFileSecurity (constructor)` | Shows how to read a simple configuration file containing PDF paths and owner passwords, then decr... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDF Files with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt every PDF in a directory using Aspose.Pdf.Facades.PdfFileSecurity with a com... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch encrypt pdfs |  | Batch encrypt pdfs |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Batch Update PDF User Passwords | `PdfFileSecurity`, `TryChangePassword` | Shows how to iterate through a folder of PDF files and change each file's user password to a stan... |
| [change-pdf-owner-password-preserve-user](./change-pdf-owner-password-preserve-user.cs) | Change pdf owner password preserve user |  | Change pdf owner password preserve user |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change User and Owner Passwords for PDFs from CSV | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to read PDF file paths and passwords from a CSV file and use Aspose.Pdf.Facades.PdfFile... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an encrypted PDF in a single call... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `ChangePassword`, `ctor` | Demonstrates how to update the user password of an already encrypted PDF while keeping the existi... |
| [check-pdf-encryption-and-encrypt-decrypt](./check-pdf-encryption-and-encrypt-decrypt.cs) | Check pdf encryption and encrypt decrypt |  | Check pdf encryption and encrypt decrypt |
| [check-pdf-encryption-and-toggle-protection](./check-pdf-encryption-and-toggle-protection.cs) | Check PDF Encryption and Apply or Remove Protection | `PdfFileInfo`, `Document`, `Encrypt` | The example shows how to inspect a PDF's IsEncrypted flag using PdfFileInfo, then either encrypt ... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `Document`, `PdfFileSignature`, `BindPdf` | Shows how to use Aspose.Pdf's PdfFileSignature facade to determine if a PDF contains extended usa... |
| [conditional-decrypt-modify-pdf](./conditional-decrypt-modify-pdf.cs) | Conditional Decrypt and Modify PDF with Aspose.Pdf | `Document`, `PdfFileSecurity`, `DecryptFile` | The example loads a PDF, detects if it is encrypted, decrypts it using the owner password when ne... |
| [conditional-pdf-encryption-based-on-filename](./conditional-pdf-encryption-based-on-filename.cs) | Conditional PDF Encryption Based on File Name | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf, selecting the key size and encry... |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF Using Owner Password | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to remove encryption from a PDF file by providing the owner password using Aspos... |
| [decrypt-pdf-using-owner-password-azure-key-vault](./decrypt-pdf-using-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `PdfFileSecurity`, `DecryptFile` | Shows how to retrieve an owner password stored in Azure Key Vault and use Aspose.Pdf.Facades to d... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt pdf with owner password |  | Decrypt pdf with owner password |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Disable Copying While Enabling Printing for PDF | `PdfFileSecurity`, `SetPrivilege`, `DocumentPrivilege` | Demonstrates using Aspose.Pdf.Facades.PdfFileSecurity to set document privileges so that copying ... |
| [encrypt-decrypt-pdf-roundtrip](./encrypt-decrypt-pdf-roundtrip.cs) | Encrypt and Decrypt PDF with Round‑Trip Verification | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF using a user and owner password, decrypt it back, and verify in... |
| [encrypt-decrypt-pdf-verify-integrity](./encrypt-decrypt-pdf-verify-integrity.cs) | Encrypt decrypt pdf verify integrity |  | Encrypt decrypt pdf verify integrity |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES using Aspose.Pdf | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF with 256‑bit AES, set user and owner passwords, and apply a print priv... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates encrypting a PDF using AES‑256 while applying custom document privileges such as all... |
| [encrypt-pdf-aes256-to-cloud](./encrypt-pdf-aes256-to-cloud.cs) | Encrypt pdf aes256 to cloud |  | Encrypt pdf aes256 to cloud |
| [encrypt-pdf-aes256-upload](./encrypt-pdf-aes256-upload.cs) | Encrypt PDF with AES‑256 and Upload to Cloud Storage | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt a PDF using Aspose.Pdf.Facades with AES‑256 and then upload the encrypted st... |
| [encrypt-pdf-byte-array](./encrypt-pdf-byte-array.cs) | Encrypt pdf byte array |  | Encrypt pdf byte array |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array using Aspose.Pdf | `Document`, `PdfFileSecurity`, `DocumentPrivilege` | Shows how to load a PDF from a byte array, apply 256‑bit AES encryption with user and owner passw... |
| [encrypt-pdf-rc4-128-allow-print-edit](./encrypt-pdf-rc4-128-allow-print-edit.cs) | Encrypt pdf rc4 128 allow print edit |  | Encrypt pdf rc4 128 allow print edit |
| [encrypt-pdf-rc4-128-print-edit-privileges](./encrypt-pdf-rc4-128-print-edit-privileges.cs) | Encrypt PDF with RC4‑128 and Enable Print/Edit Privileges | `Document`, `DocumentPrivilege`, `PdfFileSecurity` | Demonstrates how to combine print and edit privileges, then encrypt a PDF using RC4‑128 with Aspo... |
| ... | | | *and 29 more files* |

## Category Statistics
- Total examples: 59

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-secure-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

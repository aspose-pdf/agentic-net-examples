---
name: facades-secure-documents
description: C# examples for facades-secure-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-secure-documents

> **Facades secure documents** in PDF using C# / .NET -- **38** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-secure-documents** category.
This folder contains standalone C# examples for facades-secure-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-secure-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (34/38 files) ← category-specific
- `using Aspose.Pdf;` (22/38 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/38 files)
- `using System;` (38/38 files)
- `using System.IO;` (35/38 files)
- `using System.Collections.Generic;` (3/38 files)
- `using System.Diagnostics;` (1/38 files)
- `using System.Text.Json;` (1/38 files)

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
| [add-text-annotation-to-encrypted-pdf](./add-text-annotation-to-encrypted-pdf.cs) | Add Text Annotation to Encrypted PDF | `Document`, `InvalidPasswordException`, `Page` | Demonstrates opening a PDF with optional owner password handling, adding a text annotation to the... |
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to PDF Using PdfFileSecurity | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF with user and owner passwords using the PdfFileSecurity faca... |
| [batch-decrypt-pdfs-from-config](./batch-decrypt-pdfs-from-config.cs) | Batch Decrypt PDFs Using Owner Passwords from Config | `PdfFileSecurity`, `DecryptFile`, `Dispose` | Demonstrates how to read a JSON configuration file and batch‑decrypt multiple PDF files using the... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf by applying the same user ... |
| [batch-update-pdf-user-passwords](./batch-update-pdf-user-passwords.cs) | Batch Update PDF User Passwords | `PdfFileSecurity`, `ChangePassword`, `Close` | Iterates through all PDF files in a folder and applies a standardized user password to each docum... |
| [change-pdf-owner-password-preserve-user](./change-pdf-owner-password-preserve-user.cs) | Change PDF Owner Password While Preserving User Password | `PdfFileSecurity`, `ChangePassword`, `ctor` | Shows how to iterate over multiple PDF files and update their owner passwords using Aspose.Pdf.Fa... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change PDF User and Owner Passwords from CSV | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to read a CSV list of PDFs and use Aspose.Pdf.Facades to change both the user an... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an existing PDF in a single call ... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Demonstrates how to change the user password of an already encrypted PDF while keeping the origin... |
| [check-pdf-encryption-and-encrypt-decrypt](./check-pdf-encryption-and-encrypt-decrypt.cs) | Check PDF Encryption and Perform Encrypt/Decrypt | `PdfFileInfo`, `Document`, `Permissions` | Demonstrates how to inspect a PDF's IsEncrypted flag with PdfFileInfo and then encrypt or decrypt... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `Document`, `PdfFileSignature`, `BindPdf` | Demonstrates how to load a PDF, bind it to the PdfFileSignature facade, and determine whether the... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password using Aspose.Pdf | `PdfFileSecurity`, `DecryptFile` | Shows how to unlock and decrypt an encrypted PDF file by providing the owner password with Aspose... |
| [disable-copying-enable-printing-pdf](./disable-copying-enable-printing-pdf.cs) | Disable Copying While Enabling Printing for PDF | `PdfFileSecurity`, `SetPrivilege`, `Save` | Shows how to use Aspose.Pdf.Facades.PdfFileSecurity to allow printing but prevent content copying... |
| [encrypt-decrypt-pdf-verify-integrity](./encrypt-decrypt-pdf-verify-integrity.cs) | Encrypt and Decrypt PDF to Verify Integrity | `PdfFileSecurity`, `EncryptFile`, `DecryptFile` | Demonstrates encrypting a PDF with user and owner passwords using Aspose.Pdf.Facades.PdfFileSecur... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF using 256‑bit AES, set user and owner passwords, allow print... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt a PDF using AES‑256 while applying a custom set of document privilege... |
| [encrypt-pdf-aes256-to-cloud](./encrypt-pdf-aes256-to-cloud.cs) | Encrypt PDF with AES‑256 and Write to Cloud Stream | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to use Aspose.Pdf to encrypt a PDF with AES‑256 and directly save the encrypted file to... |
| [encrypt-pdf-byte-array](./encrypt-pdf-byte-array.cs) | Encrypt PDF Byte Array with Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates loading a PDF from a byte array, encrypting it with user and owner passwords using A... |
| [encrypt-pdf-rc4-128-allow-print-edit](./encrypt-pdf-rc4-128-allow-print-edit.cs) | Encrypt PDF with RC4‑128 and Allow Print/Edit Permissions | `Document`, `DocumentPrivilege`, `PdfFileSecurity` | Demonstrates how to create a PDF, combine privilege settings to allow printing and content modifi... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF file with the RC4‑40 algorithm and a user password using Aspose.Pdf's ... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to load a PDF from a stream, apply RC4‑40 encryption with user/owner passwords, ... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF with Aspose.Pdf, applying AES‑128 encryption with user/owner passwords... |
| [encrypt-pdf-with-exception-handling](./encrypt-pdf-with-exception-handling.cs) | Encrypt PDF with Detailed Exception Handling | `PdfFileSecurity`, `AllowExceptions`, `TryEncryptFile` | Demonstrates how to enable AllowExceptions on PdfFileSecurity, attempt PDF encryption with TryEnc... |
| [encrypt-pdf-with-user-password-only](./encrypt-pdf-with-user-password-only.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF using Aspose.Pdf.Facades by providing only a user password (owner pass... |
| [encrypt-pdfs-by-filename-algorithm](./encrypt-pdfs-by-filename-algorithm.cs) | Encrypt PDFs Using Naming Convention to Select Algorithm | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | The example scans a folder of PDF files, determines the encryption algorithm and key size from ea... |
| [encrypt-pdfs-with-performance-timing](./encrypt-pdfs-with-performance-timing.cs) | Encrypt PDFs with Performance Timing | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf's PdfFileSecurity facade while me... |
| [generate-encryption-usage-report](./generate-encryption-usage-report.cs) | Generate Encryption Usage Report for PDFs | `PdfFileInfo`, `IsEncrypted` | Iterates over a collection of PDF files, uses Aspose.Pdf.Facades.PdfFileInfo to determine if each... |
| [generate-pdf-encryption-summary](./generate-pdf-encryption-summary.cs) | Generate PDF Encryption Summary CSV | `PdfFileInfo`, `IsEncrypted`, `GetDocumentPrivilege` | Scans a folder of PDF files, detects each file’s encryption status, algorithm (if known), and doc... |
| [load-pdf-verify-page-count](./load-pdf-verify-page-count.cs) | Load PDF and Verify Page Count using PdfFileInfo | `PdfFileInfo`, `BindPdf`, `Document` | Demonstrates loading a PDF file with the PdfFileInfo facade, retrieving the underlying Document o... |
| [modify-pdf-privileges](./modify-pdf-privileges.cs) | Modify PDF Privileges Using PdfFileSecurity | `Document`, `PdfFileSecurity`, `BindPdf` | Demonstrates how to change PDF security privileges (e.g., allow printing) with Aspose.Pdf's PdfFi... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

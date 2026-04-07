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

- `using Aspose.Pdf.Facades;` (30/40 files) ← category-specific
- `using Aspose.Pdf;` (24/40 files) ← category-specific
- `using Aspose.Pdf.Text;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (37/40 files)
- `using System.Diagnostics;` (1/40 files)

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
| [apply-pdf-password-protection](./apply-pdf-password-protection.cs) | Apply Password Protection to PDF Using PdfFileSecurity | `PdfFileSecurity`, `BindPdf`, `SetPrivilege` | Demonstrates how to add user and owner passwords to an existing PDF file without creating a Docum... |
| [batch-decrypt-pdfs](./batch-decrypt-pdfs.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Decrypts multiple encrypted PDF files using owner passwords read from a configuration file. |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs in a Directory | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Encrypts every PDF file in a folder using the same user and owner passwords, producing encrypted ... |
| [change-pdf-passwords-csv](./change-pdf-passwords-csv.cs) | Change PDF Passwords from CSV List | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Reads a CSV file with PDF paths and passwords, then updates both user and owner passwords for eac... |
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF User and Owner Passwords in One Call | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an encrypted PDF using the PdfFil... |
| [change-pdf-user-password](./change-pdf-user-password.cs) | Change PDF User Password While Preserving Encryption | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Demonstrates how to change the user password of an encrypted PDF using Aspose.Pdf's PdfFileSecuri... |
| [change-pdf-user-password__v2](./change-pdf-user-password__v2.cs) | Change User Password for PDFs in a Folder | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Iterates through all PDF files in a folder and updates each file's user password to a standardize... |
| [check-pdf-encryption](./check-pdf-encryption.cs) | Check PDF Encryption Status and Apply Security | `Document`, `IsEncrypted`, `PdfFileSecurity` | Shows how to determine if a PDF is encrypted and then encrypt or decrypt it using Aspose.Pdf. |
| [decrypt-pdf-owner-password](./decrypt-pdf-owner-password.cs) | Decrypt PDF with Owner Password using PdfFileSecurity | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Demonstrates how to decrypt an encrypted PDF using the owner password via PdfFileSecurity. |
| [decrypt-pdf-owner-password__v2](./decrypt-pdf-owner-password__v2.cs) | Decrypt PDF Using Owner Password (Simulated Azure Key Vault) | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Shows how to decrypt an encrypted PDF using the owner password retrieved from a secure source (e.... |
| [detect-usage-rights](./detect-usage-rights.cs) | Detect Extended Usage Rights in PDF | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Shows how to check if a PDF file contains extended usage rights using Aspose.Pdf's PdfFileSignature. |
| [disable-copy-print](./disable-copy-print.cs) | Disable Copying While Enabling Printing for PDF | `PdfFileSecurity`, `DocumentPrivilege`, `BindPdf` | Demonstrates how to disable the copy permission and keep printing enabled on a PDF using Aspose.Pdf. |
| [encrypt-aes256-privileges](./encrypt-aes256-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `Document`, `Encrypt`, `Permissions` | Demonstrates applying AES‑256 encryption and specific permissions to a PDF in a single step. |
| [encrypt-decrypt-pdf](./encrypt-decrypt-pdf.cs) | Encrypt and Decrypt PDF with Verification | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates encrypting a PDF using PdfFileSecurity, decrypting it, and verifying that the origin... |
| [encrypt-pdf-aes256-bucket](./encrypt-pdf-aes256-bucket.cs) | Encrypt PDF with AES‑256 and Save to Cloud Bucket | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates encrypting a PDF using AES‑256 with Aspose.Pdf and saving the encrypted file to a si... |
| [encrypt-pdf-aes256](./encrypt-pdf-aes256.cs) | Encrypt PDF with 256‑bit AES | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF using 256‑bit AES with user and owner passwords. |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array and Return Encrypted Bytes | `Document`, `Encrypt`, `Permissions` | Loads a PDF from a byte array, encrypts it with a user and owner password, and returns the encryp... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 Algorithm | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates encrypting a PDF using the RC4‑40 algorithm and a user password. |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF provided as a stream with RC4‑40 encryption and return the encr... |
| [encrypt-pdf-to-memorystream](./encrypt-pdf-to-memorystream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Encrypt`, `Save(Stream)` | Demonstrates encrypting a PDF with a user and owner password and saving the encrypted document to... |
| [encrypt-pdf-user-password](./encrypt-pdf-user-password.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Encrypts a PDF using a user password while leaving the owner password undefined, causing Aspose.P... |
| [encrypt-pdf-with-exception-logging](./encrypt-pdf-with-exception-logging.cs) | Encrypt PDF with Detailed Exception Logging | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF using Aspose.Pdf, handling any errors, and logging detailed excepti... |
| [encrypt-pdfs-by-filename](./encrypt-pdfs-by-filename.cs) | Encrypt PDFs with Algorithm Based on File Name | `Document`, `Encrypt`, `Save` | Encrypts each PDF in a folder using a CryptoAlgorithm selected from the file name (high, medium, ... |
| [encrypt-pdfs-directory-archive](./encrypt-pdfs-directory-archive.cs) | Encrypt PDFs in a Directory and Archive Them | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates a one‑shot console app that encrypts each PDF found in a folder using Aspose.Pdf and... |
| [encrypt-pdfs-performance](./encrypt-pdfs-performance.cs) | Encrypt PDFs with Performance Timing | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Encrypts each PDF in a folder while logging the time taken for each encryption. |
| [encrypt-rc4-128-print-edit](./encrypt-rc4-128-print-edit.cs) | Encrypt PDF with RC4‑128 allowing printing and editing | `Document`, `Encrypt`, `Permissions` | Demonstrates how to combine printing and editing permissions and encrypt a PDF using RC4‑128. |
| [load-decrypt-modify-pdf](./load-decrypt-modify-pdf.cs) | Load, Decrypt If Encrypted, Modify, and Save PDF | `Document`, `Page`, `TextFragment` | Demonstrates loading a PDF, decrypting it when encrypted, adding a text fragment, and saving the ... |
| [load-pdf-verify](./load-pdf-verify.cs) | Load PDF Document and Verify | `Document`, `Save` | Demonstrates loading a PDF file using Aspose.Pdf and confirming it was loaded by checking the pag... |
| [pdf-encryption-summary](./pdf-encryption-summary.cs) | Generate Encryption Summary for PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Scans a folder of PDF files and creates a text summary listing each file’s encryption status, alg... |
| [remove-usage-rights](./remove-usage-rights.cs) | Remove Extended Usage Rights from Signed PDF | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Demonstrates how to remove extended usage rights from a signed PDF using Aspose.Pdf's PdfFileSign... |
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
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->

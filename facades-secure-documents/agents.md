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

- `using Aspose.Pdf.Facades;` (36/40 files) ← category-specific
- `using Aspose.Pdf;` (18/40 files)
- `using Aspose.Pdf.Text;` (2/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (35/40 files)
- `using Azure.Identity;` (1/40 files)
- `using Azure.Security.KeyVault.Secrets;` (1/40 files)
- `using NUnit.Framework;` (1/40 files)
- `using System.Collections.Generic;` (1/40 files)
- `using System.Diagnostics;` (1/40 files)
- `using System.Threading;` (1/40 files)
- `using System.Threading.Tasks;` (1/40 files)

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
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to an Existing PDF | `Document`, `Encrypt`, `Save` | Shows how to load an existing PDF, encrypt it with user and owner passwords using 256‑bit AES, an... |
| [apply-print-privilege-disable-exceptions](./apply-print-privilege-disable-exceptions.cs) | Apply Print Privilege to PDF and Disable Internal Exceptions | `PdfFileSecurity`, `SetPrivilege`, `DocumentPrivilege` | Shows how to use Aspose.Pdf.Facades.PdfFileSecurity to set a print privilege on a PDF while turni... |
| [batch-decrypt-pdfs-owner-password](./batch-decrypt-pdfs-owner-password.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to read a configuration file and decrypt multiple password‑protected PDF files i... |
| [batch-encrypt-pdfs](./batch-encrypt-pdfs.cs) | Batch Encrypt PDFs with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf.Facades, applying the same... |
| [change-pdf-passwords-from-csv](./change-pdf-passwords-from-csv.cs) | Change User and Owner Passwords for PDFs from CSV | `PdfFileSecurity`, `ChangePassword`, `Close` | Shows how to read a CSV file containing PDF paths and passwords, then use Aspose.Pdf.Facades.PdfF... |
| [change-pdf-user-owner-passwords](./change-pdf-user-owner-passwords.cs) | Change PDF User and Owner Passwords | `PdfFileSecurity`, `ChangePassword` | Demonstrates how to change both the user and owner passwords of an existing PDF file in a single ... |
| [change-pdf-user-password-bulk](./change-pdf-user-password-bulk.cs) | Change PDF User Password in Bulk | `PdfFileSecurity`, `ChangePassword` | Iterates through a folder of PDF files and applies a standardized user password to each document ... |
| [change-pdf-user-password](./change-pdf-user-password.cs) | Change PDF User Password While Preserving Encryption | `PdfFileSecurity`, `ChangePassword` | Shows how to change the user password of an encrypted PDF using Aspose.Pdf.Facades, keeping the e... |
| [check-pdf-extended-usage-rights](./check-pdf-extended-usage-rights.cs) | Check PDF for Extended Usage Rights | `Document`, `PdfFileSignature`, `BindPdf` | Shows how to use Aspose.Pdf's PdfFileSignature facade to determine if a PDF contains extended usa... |
| [decrypt-modify-save-pdf](./decrypt-modify-save-pdf.cs) | Decrypt, Modify, and Save PDF with Aspose.Pdf | `Document`, `Save`, `Pages` | Shows how to open a PDF, decrypt it if it is password‑protected using PdfFileSecurity, add a text... |
| [decrypt-pdf-owner-password-azure-key-vault](./decrypt-pdf-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `PdfFileSecurity`, `DecryptFile` | Demonstrates retrieving an owner password stored in Azure Key Vault and using Aspose.Pdf.Facades ... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password | `PdfFileSecurity`, `DecryptFile` | Shows how to remove encryption from a PDF by providing the owner password using Aspose.Pdf.Facade... |
| [disable-copy-enable-print-pdf](./disable-copy-enable-print-pdf.cs) | Disable Copying While Enabling High-Resolution Printing for ... | `PdfFileSecurity`, `SetPrivilege`, `DocumentPrivilege` | Shows how to forbid copying and allow high‑resolution printing on a PDF using Aspose.Pdf.Facades. |
| [encrypt-decrypt-pdf-verify](./encrypt-decrypt-pdf-verify.cs) | Encrypt and Decrypt PDF with Verification | `PdfFileSecurity`, `EncryptFile`, `DecryptFile` | Demonstrates encrypting a PDF using user and owner passwords with 256‑bit AES, then decrypting it... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF using 256‑bit AES, providing user and owner passwords and se... |
| [encrypt-pdf-aes256-azure-blob](./encrypt-pdf-aes256-azure-blob.cs) | Encrypt PDF with AES‑256 and Upload to Azure Blob | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates encrypting a PDF using Aspose.Pdf's PdfFileSecurity with AES‑256 and uploading the e... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt a PDF using AES‑256 while applying specific document privileges in a single ... |
| [encrypt-pdf-and-measure-performance](./encrypt-pdf-and-measure-performance.cs) | Encrypt PDF Files and Log Execution Time | `Document`, `Encrypt`, `Save` | The example loads each PDF, encrypts it with user and owner passwords using AES‑256, saves the en... |
| [encrypt-pdf-based-on-filename](./encrypt-pdf-based-on-filename.cs) | Encrypt PDFs Based on File Name | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Demonstrates how to encrypt PDF files using Aspose.Pdf.Facades by selecting the CryptoAlgorithm a... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates loading a PDF from a byte array, applying AES‑256 encryption with optional passwords... |
| [encrypt-pdf-rc4-128-privileges](./encrypt-pdf-rc4-128-privileges.cs) | Encrypt PDF with RC4‑128 and Enable Print/Edit Privileges | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to combine print and edit privileges for a PDF and encrypt the document using RC... |
| [encrypt-pdf-rc4-40](./encrypt-pdf-rc4-40.cs) | Encrypt PDF with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt an existing PDF with a user password using the RC4‑40 algorithm via A... |
| [encrypt-pdf-save-to-memorystream](./encrypt-pdf-save-to-memorystream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, and writin... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Demonstrates how to encrypt a PDF provided as a stream using RC4‑40 encryption with Aspose.Pdf's ... |
| [encrypt-pdf-with-password](./encrypt-pdf-with-password.cs) | Encrypt PDF with Password and Capture Exceptions | `PdfFileSecurity`, `TryEncryptFile` | Demonstrates how to encrypt a PDF using PdfFileSecurity, enable AllowExceptions to capture errors... |
| [encrypt-pdf-with-user-password-only](./encrypt-pdf-with-user-password-only.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt a PDF using Aspose.Pdf by setting only a user password, letting the l... |
| [generate-pdf-encryption-summary](./generate-pdf-encryption-summary.cs) | Generate Encryption Summary for PDF Files | `PdfFileInfo`, `GetDocumentPrivilege` | Iterates over a set of PDF documents, determines whether each file is encrypted, captures the (un... |
| [load-verify-pdf-using-pdffileinfo](./load-verify-pdf-using-pdffileinfo.cs) | Load and Verify PDF Using PdfFileInfo Facade | `Document`, `PdfFileInfo`, `BindPdf` | Shows how to load a PDF with the PdfFileInfo facade, retrieve the Document, and confirm it contai... |
| [monitor-folder-encrypt-pdfs-archive](./monitor-folder-encrypt-pdfs-archive.cs) | Monitor Folder and Encrypt PDFs to Secure Archive | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to watch a directory for new PDF files, encrypt them with user/owner passwords u... |
| [pdf-encryption-usage-report](./pdf-encryption-usage-report.cs) | Generate PDF Encryption Usage Report | `PdfFileInfo` | Scans a directory of PDF files, uses Aspose.Pdf.Facades.PdfFileInfo to determine if each PDF is e... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

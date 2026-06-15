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

- `using Aspose.Pdf.Facades;` (37/40 files) ← category-specific
- `using Aspose.Pdf;` (21/40 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (34/40 files)
- `using System.Collections.Generic;` (1/40 files)
- `using System.Diagnostics;` (1/40 files)
- `using System.Security.Cryptography;` (1/40 files)

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
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to an Existing PDF | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt an existing PDF with user and owner passwords using Aspose.Pdf.Facades, with... |
| [batch-decrypt-pdfs-owner-password](./batch-decrypt-pdfs-owner-password.cs) | Batch Decrypt PDFs Using Owner Passwords | `PdfFileSecurity`, `DecryptFile` | Shows how to read a list of PDF files and their owner passwords from a configuration file and dec... |
| [batch-encrypt-pdf-files](./batch-encrypt-pdf-files.cs) | Batch Encrypt PDFs with User and Owner Passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Shows how to encrypt every PDF in a directory using Aspose.Pdf.Facades, applying the same user an... |
| [change-user-and-owner-passwords](./change-user-and-owner-passwords.cs) | Change User and Owner Passwords in PDF | `PdfFileSecurity`, `ChangePassword`, `Close` | Demonstrates how to change both the user and owner passwords of an existing PDF file in a single ... |
| [change-user-password-of-encrypted-pdf](./change-user-password-of-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to change the user password of an encrypted PDF while keeping the existing encryption s... |
| [check-pdf-encryption-and-apply-security](./check-pdf-encryption-and-apply-security.cs) | Check PDF Encryption and Apply Security | `Document`, `IsEncrypted`, `Encrypt` | Loads a PDF, inspects its IsEncrypted property, encrypts it with user/owner passwords when not en... |
| [check-pdf-encryption-and-decrypt](./check-pdf-encryption-and-decrypt.cs) | Check PDF Encryption and Decrypt if Needed | `PdfFileInfo`, `IsEncrypted`, `PdfFileSecurity` | Shows how to determine if a PDF is encrypted with Aspose.Pdf.Facades.PdfFileInfo and, when encryp... |
| [decrypt-modify-pdf-asp](./decrypt-modify-pdf-asp.cs) | Decrypt and Modify PDF Using Aspose.Pdf | `Document`, `PdfFileSecurity`, `DecryptFile` | Shows how to detect an encrypted PDF, decrypt it with PdfFileSecurity, add a text fragment to the... |
| [decrypt-pdf-owner-password-azure-key-vault](./decrypt-pdf-owner-password-azure-key-vault.cs) | Decrypt PDF Using Owner Password from Azure Key Vault | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Shows how to retrieve an owner password stored in Azure Key Vault (or a stub) and use Aspose.Pdf.... |
| [decrypt-pdf-with-owner-password](./decrypt-pdf-with-owner-password.cs) | Decrypt PDF with Owner Password using Aspose.Pdf | `PdfFileSecurity`, `DecryptFile` | Demonstrates how to decrypt an encrypted PDF file by providing the owner password using Aspose.Pd... |
| [detect-extended-usage-rights-in-pdf](./detect-extended-usage-rights-in-pdf.cs) | Detect Extended Usage Rights in PDF | `PdfFileSignature`, `BindPdf`, `ContainsUsageRights` | Demonstrates how to bind a PDF with PdfFileSignature and check whether it contains extended usage... |
| [disable-copying-allow-printing-pdf](./disable-copying-allow-printing-pdf.cs) | Disable Copying While Allowing Printing in PDF | `DocumentPrivilege`, `PdfFileSecurity`, `BindPdf` | Shows how to set PDF document privileges to forbid copying but keep printing enabled using Aspose... |
| [encrypt-and-archive-pdf](./encrypt-and-archive-pdf.cs) | Encrypt New PDFs and Archive Them | `Document`, `PdfFileSecurity`, `DocumentPrivilege` | Monitors a folder for newly added PDF files, encrypts each with a password, and moves the encrypt... |
| [encrypt-pdf-256-aes](./encrypt-pdf-256-aes.cs) | Encrypt PDF with 256‑bit AES and passwords | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt an existing PDF using 256‑bit AES, setting user and owner passwords, ... |
| [encrypt-pdf-aes256-custom-privileges](./encrypt-pdf-aes256-custom-privileges.cs) | Encrypt PDF with AES‑256 and Custom Privileges | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF using AES‑256 while applying custom document privileges such as... |
| [encrypt-pdf-aes256-upload](./encrypt-pdf-aes256-upload.cs) | Encrypt PDF with AES‑256 and Upload to Cloud Storage | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to encrypt a PDF using AES‑256 with Aspose.Pdf and then upload the encrypted file to a ... |
| [encrypt-pdf-allow-exceptions](./encrypt-pdf-allow-exceptions.cs) | Encrypt PDF with AllowExceptions and Detailed Error Logging | `PdfFileSecurity`, `AllowExceptions`, `BindPdf` | Demonstrates how to enable AllowExceptions on PdfFileSecurity, encrypt a PDF with user/owner pass... |
| [encrypt-pdf-by-filename](./encrypt-pdf-by-filename.cs) | Encrypt PDF with Different CryptoAlgorithm Based on Filename | `Document`, `Permissions`, `CryptoAlgorithm` | Creates a sample PDF and encrypts copies using different CryptoAlgorithm values (AES or RC4) sele... |
| [encrypt-pdf-from-byte-array](./encrypt-pdf-from-byte-array.cs) | Encrypt PDF from Byte Array | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to load a PDF from a byte array, encrypt it with user and owner passwords using Aspose.... |
| [encrypt-pdf-rc4-128-allow-print-edit](./encrypt-pdf-rc4-128-allow-print-edit.cs) | Encrypt PDF with RC4‑128 and Enable Print/Edit | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Demonstrates how to combine privilege settings to allow printing and content modification, then e... |
| [encrypt-pdf-rc4-40-user-password](./encrypt-pdf-rc4-40-user-password.cs) | Encrypt PDF with RC4‑40 Using a User Password | `PdfFileSecurity`, `EncryptFile`, `Close` | Shows how to encrypt an existing PDF file with the RC4‑40 algorithm, specifying user and owner pa... |
| [encrypt-pdf-stream-rc4-40](./encrypt-pdf-stream-rc4-40.cs) | Encrypt PDF Stream with RC4‑40 using Aspose.Pdf | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Demonstrates how to encrypt a PDF provided as a stream using RC4‑40 encryption with Aspose.Pdf.Fa... |
| [encrypt-pdf-to-memory-stream](./encrypt-pdf-to-memory-stream.cs) | Encrypt PDF and Save to MemoryStream | `Document`, `Encrypt`, `Save` | Creates a sample PDF, encrypts it with passwords, and saves the encrypted document to a MemoryStr... |
| [encrypt-pdf-verify-decryption](./encrypt-pdf-verify-decryption.cs) | Encrypt PDF and Verify Decryption Integrity | `PdfFileSecurity`, `EncryptFile`, `DecryptFile` | Shows how to encrypt a PDF with user and owner passwords using 256‑bit AES, then decrypt it and c... |
| [encrypt-pdf-with-user-password-only](./encrypt-pdf-with-user-password-only.cs) | Encrypt PDF with User Password Only | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt a PDF using Aspose.Pdf by providing only a user password, allowing th... |
| [encrypt-pdfs-log-processing-time](./encrypt-pdfs-log-processing-time.cs) | Encrypt PDFs and Log Processing Time | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt multiple PDF files using Aspose.Pdf's PdfFileSecurity facade while me... |
| [generate-pdf-encryption-summary](./generate-pdf-encryption-summary.cs) | Generate PDF Encryption Summary CSV | `PdfFileInfo`, `IsEncrypted`, `GetDocumentPrivilege` | Scans a folder of PDF files, determines each file's encryption status, algorithm (if detectable),... |
| [get-pdf-encryption-algorithm](./get-pdf-encryption-algorithm.cs) | Get PDF Encryption Algorithm using Aspose.Pdf | `PdfFileInfo`, `IsEncrypted`, `Document` | Demonstrates how to check if a PDF is encrypted and retrieve its encryption algorithm via the Asp... |
| [load-pdf-verify-page-count](./load-pdf-verify-page-count.cs) | Load PDF and Verify Page Count with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `Document` | Demonstrates how to load a PDF file using the PdfFileInfo facade, access the underlying Document ... |
| [modify-pdf-privileges-exception-propagation](./modify-pdf-privileges-exception-propagation.cs) | Modify PDF Privileges with Exception Propagation | `Document`, `PdfFileSecurity`, `DocumentPrivilege` | Demonstrates how to change a PDF's security privileges using Aspose.Pdf while disabling internal ... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-secure-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->

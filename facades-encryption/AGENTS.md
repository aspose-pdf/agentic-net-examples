---
name: facades-encryption
description: C# examples for facades-encryption using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-encryption

> **Facades encryption** in PDF using C# / .NET -- **44** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-encryption** category.
This folder contains standalone C# examples for facades-encryption operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-encryption**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (39/44 files) ← category-specific
- `using Aspose.Pdf.Facades;` (21/44 files)
- `using Aspose.Pdf.Security;` (10/44 files)
- `using System;` (44/44 files)
- `using System.IO;` (44/44 files)

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
| [change-owner-password-of-encrypted-pdf](./change-owner-password-of-encrypted-pdf.cs) | Change Owner Password of Encrypted PDF | `Document`, `PdfFileSecurity`, `BindPdf` | Demonstrates loading an encrypted PDF with a user password, binding it to the PdfFileSecurity fac... |
| [change-owner-password-of-encrypted-pdf__v2](./change-owner-password-of-encrypted-pdf__v2.cs) | Change Owner Password of Encrypted PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to open an encrypted PDF with the existing user password, re‑encrypt it with a new owne... |
| [change-owner-password-of-encrypted-pdf__v3](./change-owner-password-of-encrypted-pdf__v3.cs) | Change Owner Password of Encrypted PDF | `PdfFileInfo`, `Document`, `Encrypt` | Loads an encrypted PDF, verifies its encryption status, then re‑encrypts it with the same user pa... |
| [change-pdf-encryption-passwords](./change-pdf-encryption-passwords.cs) | Change PDF Encryption Passwords | `PdfFileInfo`, `Document`, `Permissions` | The example checks if a PDF is encrypted, opens it with the current owner password, and re‑encryp... |
| [change-pdf-password-aes-256](./change-pdf-password-aes-256.cs) | Change PDF Password and Apply 256‑Bit AES Encryption | `Document`, `Encrypt`, `Save` | Loads an existing encrypted PDF, updates the user and owner passwords, enables printing permissio... |
| [change-pdf-password-disable-printing](./change-pdf-password-disable-printing.cs) | Change PDF Password and Disable Printing with 256‑bit Encryp... | `PdfFileSecurity`, `DocumentPrivilege`, `KeySize` | Loads an encrypted PDF, binds it to PdfFileSecurity, changes the user password, disables printing... |
| [change-pdf-passwords-aes256](./change-pdf-passwords-aes256.cs) | Change PDF Passwords with AES‑256 Encryption | `Document`, `Encrypt`, `CryptoAlgorithm` | Loads an existing encrypted PDF, re‑encrypts it with new user and owner passwords using AES‑256, ... |
| [change-pdf-passwords-enable-printing-aes256](./change-pdf-passwords-enable-printing-aes256.cs) | Change PDF Passwords and Enable Printing with AES‑256 Encryp... | `Document`, `Encrypt`, `Permissions` | Loads an encrypted PDF using the existing user password, updates both user and owner passwords, g... |
| [change-pdf-passwords__v4](./change-pdf-passwords__v4.cs) | Change PDF Passwords with Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Demonstrates opening an encrypted PDF using the owner password, changing both user and owner pass... |
| [change-pdf-passwords__v5](./change-pdf-passwords__v5.cs) | Change PDF Passwords Using PdfFileSecurity | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Shows how to load an encrypted PDF, bind it to PdfFileSecurity, replace the user and owner passwo... |
| [change-pdf-user-password-enable-print](./change-pdf-user-password-enable-print.cs) | Change PDF User Password and Enable Printing | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads an encrypted PDF with the current user password, re‑encrypts it using a new use... |
| [change-pdf-user-password-enable-printing](./change-pdf-user-password-enable-printing.cs) | Change PDF User Password and Enable Printing while Preservin... | `Document`, `Encrypt`, `Permissions` | Shows how to load an encrypted PDF, set a new user password, enable printing permission, and re‑s... |
| [change-pdf-user-password-enable-printing__v2](./change-pdf-user-password-enable-printing__v2.cs) | Change PDF User Password and Enable Printing | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading an encrypted PDF, updating the user password, granting print permission, and... |
| [change-user-password-encrypted-pdf](./change-user-password-encrypted-pdf.cs) | Change User Password of Encrypted PDF | `PdfFileInfo`, `Document`, `Encrypt` | Shows how to verify that a PDF is encrypted, open it with the existing user password, re‑encrypt ... |
| [change-user-password-encrypted-pdf__v2](./change-user-password-encrypted-pdf__v2.cs) | Change User Password of an Encrypted PDF | `PdfFileInfo`, `Document`, `Encrypt` | Demonstrates how to verify PDF encryption, open an encrypted document with the current password, ... |
| [change-user-password-of-encrypted-pdf__v2](./change-user-password-of-encrypted-pdf__v2.cs) | Change User Password of Encrypted PDF | `PdfFileSecurity`, `BindPdf`, `ChangePassword` | Demonstrates loading an encrypted PDF, binding it to PdfFileSecurity, updating only the user pass... |
| [change-user-password-preserve-permissions](./change-user-password-preserve-permissions.cs) | Change User Password of Encrypted PDF While Preserving Permi... | `Document`, `Encrypt`, `Save` | Demonstrates opening an encrypted PDF with the existing user password, retrieving its current per... |
| [check-pdf-encryption-status__v2](./check-pdf-encryption-status__v2.cs) | Check PDF Encryption Status and Write to File | `PdfFileInfo`, `BindPdf`, `IsEncrypted` | Demonstrates how to bind a PDF to PdfFileInfo, determine if it is encrypted, and write the result... |
| [check-pdf-encryption-status__v3](./check-pdf-encryption-status__v3.cs) | Check PDF Encryption Status with Aspose.Pdf | `PdfFileInfo`, `BindPdf`, `IsEncrypted` | Demonstrates how to load a PDF, bind it to a PdfFileInfo object, and determine whether the file i... |
| [encrypt-pdf-aes128-user-owner-password](./encrypt-pdf-aes128-user-owner-password.cs) | Encrypt PDF with User and Owner Passwords (AES‑128) | `PdfFileInfo`, `Document`, `Encrypt` | The example loads a PDF, checks whether it already has an edit password, then encrypts the docume... |
| [encrypt-pdf-aes256-forbid-all](./encrypt-pdf-aes256-forbid-all.cs) | Encrypt PDF with AES‑256 and Forbid All Permissions | `Document`, `Encrypt`, `Permissions` | Shows how to load a PDF, apply AES‑256 encryption with user and owner passwords, forbid all permi... |
| [encrypt-pdf-aes256-screen-reader](./encrypt-pdf-aes256-screen-reader.cs) | Encrypt PDF with AES‑256 and Enable Screen Reader Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying 256‑bit AES encryption with user and owner passwords, enabli... |
| [encrypt-pdf-and-generate-security-report](./encrypt-pdf-and-generate-security-report.cs) | Encrypt PDF and Generate Security Inspection Report | `Document`, `Encrypt`, `Save` | The example encrypts a PDF with AES‑256 using user and owner passwords, applies specific permissi... |
| [encrypt-pdf-forbid-all-permissions](./encrypt-pdf-forbid-all-permissions.cs) | Encrypt PDF with User and Owner Passwords (Forbid All Permis... | `Document`, `Encrypt`, `Save` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, and restri... |
| [encrypt-pdf-owner-password-no-permissions](./encrypt-pdf-owner-password-no-permissions.cs) | Encrypt PDF with Owner Password and No Permissions | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying encryption with only an owner password, disabling all permis... |
| [encrypt-pdf-print-only-permission__v2](./encrypt-pdf-print-only-permission__v2.cs) | Encrypt PDF with Print‑Only Permission | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, restrictin... |
| [encrypt-pdf-rc4-128-no-permissions](./encrypt-pdf-rc4-128-no-permissions.cs) | Encrypt PDF with 128‑bit RC4 and No Permissions | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to load a PDF, apply 128‑bit RC4 encryption, forbid all privileges, and save the... |
| [encrypt-pdf-rc4-check-open-password](./encrypt-pdf-rc4-check-open-password.cs) | Encrypt PDF with RC4 and Verify Open Password | `PdfFileInfo`, `HasOpenPassword`, `Document` | The example loads a PDF, uses PdfFileInfo to check if it has an open password, then encrypts the ... |
| [encrypt-pdf-rc4-screen-reader-permission](./encrypt-pdf-rc4-screen-reader-permission.cs) | Encrypt PDF with RC4 and Enable Screen Reader Permission | `Document`, `Encrypt`, `Save` | Loads a PDF, applies 128‑bit RC4 encryption with user and owner passwords, enables the ExtractCon... |
| [encrypt-pdf-with-passwords-and-copy-permission](./encrypt-pdf-with-passwords-and-copy-permission.cs) | Encrypt PDF with User/Owner Passwords and Copy Permission | `Document`, `Permissions`, `CryptoAlgorithm` | The example loads an existing PDF, applies AES‑256 encryption with a user and owner password, and... |
| ... | | | *and 14 more files* |

## Category Statistics
- Total examples: 44

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-encryption patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-27 | Run: `20261127_133719_3d0281`
<!-- AUTOGENERATED:END -->

---
name: encryption
description: C# examples for encryption using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - encryption

> **Encryption** in PDF using C# / .NET -- **32** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **encryption** category.
This folder contains standalone C# examples for encryption operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **encryption**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (32/32 files) ← category-specific
- `using Aspose.Pdf.Security;` (18/32 files) ← category-specific
- `using Aspose.Pdf.Text;` (1/32 files)
- `using System;` (32/32 files)
- `using System.IO;` (30/32 files)
- `using System.Text.Json;` (2/32 files)
- `using System.Collections.Generic;` (1/32 files)

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
| [change-pdf-passwords](./change-pdf-passwords.cs) | Change PDF Passwords Using Aspose.Pdf | `Document`, `Encrypt`, `Save` | Demonstrates encrypting a PDF with initial user/owner passwords, then opening it with the owner p... |
| [change-pdf-passwords__v2](./change-pdf-passwords__v2.cs) | Change PDF User and Owner Passwords with Aspose.Pdf | `Document`, `ChangePasswords`, `Save` | Demonstrates opening a password‑protected PDF using the owner password, updating both the user an... |
| [change-pdf-passwords__v3](./change-pdf-passwords__v3.cs) | Change PDF Owner and User Passwords | `Document`, `Encrypt`, `Save` | Opens an encrypted PDF with the owner password, re‑encrypts it using new user and owner passwords... |
| [check-pdf-encryption-and-generate-json-report](./check-pdf-encryption-and-generate-json-report.cs) | Check PDF Encryption and Generate JSON Report | `Document`, `IsEncrypted`, `InvalidPasswordException` | The example opens a PDF using Aspose.Pdf, determines whether it is encrypted, and writes a format... |
| [check-pdf-encryption-status](./check-pdf-encryption-status.cs) | Check PDF Encryption Status and Write to File | `Document`, `InvalidPasswordException` | Shows how to load a PDF using Aspose.Pdf, detect if it is encrypted, and write the encryption sta... |
| [check-pdf-requires-open-password](./check-pdf-requires-open-password.cs) | Check if PDF Requires an Open Password | `Document`, `InvalidPasswordException` | Loads a PDF using Aspose.Pdf, determines whether an open password is required, and writes the boo... |
| [create-pdf-no-permissions-rc4-128](./create-pdf-no-permissions-rc4-128.cs) | Create PDF with No Permissions and RC4 128-bit Encryption | `Document`, `Encrypt`, `Permissions` | Demonstrates creating an empty PDF, applying RC4 128‑bit encryption with user and owner passwords... |
| [encrypt-pdf-aes128-forbid-all](./encrypt-pdf-aes128-forbid-all.cs) | Encrypt PDF with AES‑128 and Forbid All Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Opens an unprotected PDF, applies AES‑128 encryption with no user or owner passwords, and disable... |
| [encrypt-pdf-aes128-forbid-all__v2](./encrypt-pdf-aes128-forbid-all__v2.cs) | Encrypt PDF with AES‑128 and forbid all permissions | `Document`, `Encrypt`, `Permissions` | Demonstrates how to load a PDF, apply AES‑128 encryption with a user and owner password, disable ... |
| [encrypt-pdf-aes128-no-permissions](./encrypt-pdf-aes128-no-permissions.cs) | Encrypt PDF with AES‑128 and No Permissions | `Document`, `Encrypt`, `Permissions` | Shows how to open a PDF protected with a user password, apply AES‑128 encryption with no permissi... |
| [encrypt-pdf-aes128-no-privileges](./encrypt-pdf-aes128-no-privileges.cs) | Encrypt PDF with AES‑128 without Changing Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying AES‑128 encryption with user and owner passwords while prese... |
| [encrypt-pdf-aes128-screen-reader-permission](./encrypt-pdf-aes128-screen-reader-permission.cs) | Encrypt PDF with AES‑128 and Screen Reader Permission | `Document`, `Encrypt`, `Save` | Loads a PDF, encrypts it using AES‑128 with user and owner passwords, enables the screen‑reader p... |
| [encrypt-pdf-aes128-with-permissions](./encrypt-pdf-aes128-with-permissions.cs) | Encrypt PDF with AES‑128 and Set Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Shows how to encrypt a PDF using Aspose.Pdf with a user password, an owner password, AES‑128 encr... |
| [encrypt-pdf-aes256-no-permissions](./encrypt-pdf-aes256-no-permissions.cs) | Encrypt PDF with AES‑256 and No Permissions | `Document`, `Encrypt`, `Permissions` | Demonstrates loading a PDF, applying AES‑256 encryption with user and owner passwords, forbidding... |
| [encrypt-pdf-allow-screen-readers](./encrypt-pdf-allow-screen-readers.cs) | Encrypt PDF Allowing Screen Readers (AES‑128) | `Document`, `Add`, `Permissions` | Demonstrates how to create a PDF, add a blank page, set permissions that enable screen‑reader acc... |
| [encrypt-pdf-forbid-all-permissions-aes128](./encrypt-pdf-forbid-all-permissions-aes128.cs) | Encrypt PDF, Forbid All Permissions, Enable Screen Reader (P... | `Document`, `Encrypt`, `Save` | Demonstrates opening a PDF with the owner password, disabling all user permissions, enabling PDF/... |
| [encrypt-pdf-rc4-128-forbid-all](./encrypt-pdf-rc4-128-forbid-all.cs) | Encrypt PDF with RC4 128‑bit and forbid all permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates loading a PDF, applying RC4‑128 encryption with user password "beta" and owner passw... |
| [encrypt-pdf-rc4-128-screen-reader-permission](./encrypt-pdf-rc4-128-screen-reader-permission.cs) | Encrypt PDF with RC4 128-bit and Screen Reader Permission | `Document`, `Encrypt`, `Save` | The example loads a PDF, applies RC4 128‑bit encryption with a user and owner password, enables t... |
| [encrypt-pdf-rc4-40-forbid-all](./encrypt-pdf-rc4-40-forbid-all.cs) | Encrypt PDF with RC4 40‑bit and forbid all permissions | `Document`, `Encrypt`, `Save` | The example loads a PDF, applies encryption using RC4 40‑bit with user and owner passwords, forbi... |
| [encrypt-pdf-rc4-allow-screen-readers](./encrypt-pdf-rc4-allow-screen-readers.cs) | Encrypt PDF with RC4 and Enable Screen Reader Permission | `Document`, `Permissions`, `CryptoAlgorithm` | The example opens an existing PDF without a password, applies RC4 128‑bit encryption, enables the... |
| [encrypt-pdf-rc4-no-permissions](./encrypt-pdf-rc4-no-permissions.cs) | Encrypt PDF with RC4 128‑bit and No Permissions | `Document`, `ctor(string, string)`, `Encrypt` | The example opens an existing PDF using a user password, applies RC4 128‑bit encryption with all ... |
| [encrypt-pdf-rc4-no-privileges](./encrypt-pdf-rc4-no-privileges.cs) | Encrypt PDF with RC4 128-bit and No Restrictions | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using RC4 128‑bit with user and owner passwords while granting ... |
| [encrypt-pdf-rc4x128-no-permissions](./encrypt-pdf-rc4x128-no-permissions.cs) | Encrypt PDF with RC4 128‑bit and No Permissions | `Document`, `Permissions`, `CryptoAlgorithm` | Loads a PDF, applies 128‑bit RC4 encryption with a user and owner password, revokes all permissio... |
| [encrypt-pdf-with-owner-password-no-permissions](./encrypt-pdf-with-owner-password-no-permissions.cs) | Encrypt PDF with Owner Password and No Permissions | `Document`, `ctor(string, string)`, `Permissions` | Shows how to open a PDF protected by an owner password, apply new encryption with user and owner ... |
| [encrypt-pdf-with-screen-reader-permission](./encrypt-pdf-with-screen-reader-permission.cs) | Encrypt PDF with User/Owner Passwords and Screen Reader Perm... | `Document`, `Encrypt`, `Permissions` | Shows how to encrypt a PDF using Aspose.Pdf by setting user and owner passwords, enabling the scr... |
| [encrypt-pdf-with-user-owner-passwords](./encrypt-pdf-with-user-owner-passwords.cs) | Encrypt PDF with User and Owner Passwords and Screen Reader ... | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt a PDF using AES‑128 with a user password, an owner password, and a pe... |
| [identify-pdf-password-type-report](./identify-pdf-password-type-report.cs) | Identify PDF Password Type and Create a Report | `Document`, `Permissions`, `InvalidPasswordException` | The example opens encrypted PDF files with supplied user and owner passwords, determines which pa... |
| [re-encrypt-pdf-aes128-preserve-passwords](./re-encrypt-pdf-aes128-preserve-passwords.cs) | Re‑encrypt PDF with AES‑128 while preserving passwords | `Document`, `ctor`, `Permissions` | Loads an encrypted PDF using the user password, applies new AES‑128 encryption with the same pass... |
| [re-encrypt-pdf-new-owner-aes128](./re-encrypt-pdf-new-owner-aes128.cs) | Re‑encrypt PDF with New Owner Password (AES‑128) | `Document`, `Encrypt`, `Save` | Opens an existing password‑protected PDF, changes the owner password while keeping the user passw... |
| [re-encrypt-pdf-rc4-128](./re-encrypt-pdf-rc4-128.cs) | Re‑encrypt PDF with New Passwords Using RC4‑128 | `Document`, `Encrypt`, `Save` | Opens a password‑protected PDF using the owner password, sets new user and owner passwords, appli... |
| ... | | | *and 2 more files* |

## Category Statistics
- Total examples: 32

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for encryption patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-25 | Run: `20261125_225818_69a5ec`
<!-- AUTOGENERATED:END -->

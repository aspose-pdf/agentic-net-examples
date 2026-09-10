---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **61** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (34/61 files) ← category-specific
- `using Aspose.Pdf;` (11/61 files)
- `using Aspose.Pdf.Forms;` (9/61 files)
- `using Aspose.Pdf.Text;` (1/61 files)
- `using System;` (34/61 files)
- `using System.IO;` (28/61 files)
- `using System.Drawing;` (10/61 files)
- `using System.Collections.Generic;` (5/61 files)
- `using System.Security.Cryptography.X509Certificates;` (4/61 files)
- `using System.Globalization;` (2/61 files)
- `using System.Security.Cryptography;` (2/61 files)
- `using System.Drawing.Imaging;` (1/61 files)
- `using System.Text;` (1/61 files)
- `using System.Text.Json;` (1/61 files)
- `using System.Threading;` (1/61 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-digital-signature-to-pdf-page](./add-digital-signature-to-pdf-page.cs) | Add Digital Signature to First PDF Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to bind a PDF, set a certificate, and apply a visible digital signature to page ... |
| [add-locale-specific-visible-signatures](./add-locale-specific-visible-signatures.cs) | Add locale specific visible signatures |  | Add locale specific visible signatures |
| [add-multiple-digital-signatures-to-pdf](./add-multiple-digital-signatures-to-pdf.cs) | Add multiple digital signatures to pdf |  | Add multiple digital signatures to pdf |
| [add-second-digital-signature-to-pdf-page-3](./add-second-digital-signature-to-pdf-page-3.cs) | Add Second Digital Signature to PDF Page 3 | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates adding a second visible digital signature on page three of an existing PDF using Asp... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Create Signature Field and Sign PDF Document | `Document`, `AddField`, `PdfFileSignature` | Demonstrates how to add an empty digital signature field to a specific page of a PDF using Aspose... |
| [apply-semi-transparent-signature-appearance](./apply-semi-transparent-signature-appearance.cs) | Apply semi transparent signature appearance |  | Apply semi transparent signature appearance |
| [audit-pdf-signatures-reason-location](./audit-pdf-signatures-reason-location.cs) | Audit pdf signatures reason location |  | Audit pdf signatures reason location |
| [configure-custom-signature-appearance](./configure-custom-signature-appearance.cs) | Configure Custom Signature Appearance with Background Color ... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to set a custom signature appearance by adding a foreground image and a backgrou... |
| [create-and-sign-pdf-signature-field](./create-and-sign-pdf-signature-field.cs) | Create and sign pdf signature field |  | Create and sign pdf signature field |
| [create-invisible-pdf-digital-signature](./create-invisible-pdf-digital-signature.cs) | Create invisible pdf digital signature |  | Create invisible pdf digital signature |
| [custom-pdf-signature-appearance](./custom-pdf-signature-appearance.cs) | Create Custom PDF Signature Appearance Without Caption | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF with Aspose.Pdf using a PKCS#1 signature and a custom appearance that ... |
| [custom-signature-appearance-hide-caption](./custom-signature-appearance-hide-caption.cs) | Custom signature appearance hide caption |  | Custom signature appearance hide caption |
| [digitally-sign-first-page-pdf](./digitally-sign-first-page-pdf.cs) | Digitally sign first page pdf |  | Digitally sign first page pdf |
| [extract-certificate-details-from-pdf-signature](./extract-certificate-details-from-pdf-signature.cs) | Extract certificate details from pdf signature |  | Extract certificate details from pdf signature |
| [extract-certificate-details-from-pdf-signatures](./extract-certificate-details-from-pdf-signatures.cs) | Extract Certificate Details from PDF Signatures | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to load a signed PDF, enumerate its digital signatures, and retrieve the X.509 c... |
| [extract-certificate-serial-number-from-pdf-signatu...](./extract-certificate-serial-number-from-pdf-signatures.cs) | Extract Certificate Serial Number from PDF Signatures | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Demonstrates how to open a signed PDF, enumerate its digital signatures, extract the associated X... |
| [extract-certificate-serial-number-from-pdf](./extract-certificate-serial-number-from-pdf.cs) | Extract certificate serial number from pdf |  | Extract certificate serial number from pdf |
| [extract-signature-image-png](./extract-signature-image-png.cs) | Extract signature image png |  | Extract signature image png |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images to HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to retrieve signature images from a signed PDF using Aspose.Pdf.Facades and embe... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to bind a PDF, extract the X.509 signing certificate from a specific signature field, a... |
| [extract-the-signing-certificate-s-subject-distingu...](./extract-the-signing-certificate-s-subject-distinguished-name-and-store-it-in-a-database.cs) | Extract the signing certificate s subject distinguished name... |  | Extract the signing certificate s subject distinguished name and store it in a database |
| [extract-witness-signature-image](./extract-witness-signature-image.cs) | Extract Witness Signature Image from PDF | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Demonstrates how to locate a signature field named 'WitnessSignature' in a PDF, extract its image... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Creates a PDF document that enumerates all digital signatures in a source PDF, displaying signer,... |
| [hide-pdf-signature-appearance](./hide-pdf-signature-appearance.cs) | Hide PDF Signature Appearance While Keeping It Valid | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates signing a PDF with Aspose.Pdf and making the signature invisible by setting the visi... |
| [list-pdf-signature-names](./list-pdf-signature-names.cs) | List pdf signature names |  | List pdf signature names |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF Document | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to use Aspose.Pdf.Facades to open a PDF file and retrieve all non‑empty signatur... |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List Signature Reasons and Locations in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF with Aspose.Pdf.Facades, enumerate its digital signatures, and retrieve e... |
| [locale-specific-pdf-digital-signature](./locale-specific-pdf-digital-signature.cs) | Locale‑Specific PDF Digital Signature with Custom Appearance | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF using Aspose.Pdf with appearance text localized for Germ... |
| [log-pdf-signature-verification-to-json](./log-pdf-signature-verification-to-json.cs) | Log PDF Signature Verification Results to JSON | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to verify each digital signature in a PDF using Aspose.Pdf.Facades and write the... |
| [remove-all-pdf-signatures](./remove-all-pdf-signatures.cs) | Remove all pdf signatures |  | Remove all pdf signatures |
| ... | | | *and 31 more files* |

## Category Statistics
- Total examples: 61

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Facades.Algorithm`
- `Aspose.Pdf.Facades.DocMDPAccessPermissions`
- `Aspose.Pdf.Facades.DocMDPSignature`
- `Aspose.Pdf.Facades.DocumentPrivilege`
- `Aspose.Pdf.Facades.KeySize`
- `Aspose.Pdf.Facades.PKCS7`
- `Aspose.Pdf.Facades.PdfFileInfo`
- `Aspose.Pdf.Facades.PdfFileSecurity`
- `Aspose.Pdf.Facades.PdfFileSecurity.BindPdf`
- `Aspose.Pdf.Facades.PdfFileSecurity.EncryptFile`
- `Aspose.Pdf.Facades.PdfFileSecurity.Save`
- `Aspose.Pdf.Facades.PdfFileSignature`
- `Aspose.Pdf.Facades.PdfFileSignature.BindPdf`
- `Aspose.Pdf.Facades.PdfFileSignature.Certify`

### Rules
- Create a PdfFileInfo for {input_pdf} and read its IsEncrypted property to obtain a {bool} indicating whether the PDF is encrypted (password‑protected).
- Use PdfFileInfo instead of loading a full Document when only document metadata such as encryption status is needed.
- Instantiate PdfFileSecurity, call BindPdf({input_pdf}) to load the encrypted PDF, then invoke DecryptFile({string_literal}) with the owner password to decrypt it.
- After decryption, call Save({output_pdf}) on the PdfFileSecurity instance to write the unprotected PDF to disk.
- Instantiate {class} (PdfFileSecurity), call BindPdf({input_pdf}) to load the document, then invoke ChangePassword({owner_password}, {new_user_password}, {new_owner_password}) to set new passwords, and finally Save({output_pdf}) to write the protected file.

### Warnings
- DecryptFile requires the owner password; decryption with only a user password is not covered by this example.
- The example assumes the PDF is protected with an owner password; if the PDF has no password, an empty string may be required for the current owner password.
- PdfFileSecurity belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in future releases; consider using the newer Aspose.Pdf.Security namespace if available.
- A valid PKCS#7 certificate file and correct password are required; otherwise signing will fail.
- Custom appearance may not be rendered identically across all PDF viewers.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-sign-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

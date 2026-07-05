---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/35 files) ← category-specific
- `using Aspose.Pdf.Forms;` (12/35 files)
- `using Aspose.Pdf;` (9/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (32/35 files)
- `using System.Collections.Generic;` (7/35 files)
- `using System.Drawing;` (6/35 files)
- `using System.Security.Cryptography.X509Certificates;` (3/35 files)
- `using System.Globalization;` (2/35 files)
- `using System.Data;` (1/35 files)
- `using System.Drawing.Imaging;` (1/35 files)
- `using System.Text;` (1/35 files)
- `using System.Text.Json;` (1/35 files)
- `using System.Threading;` (1/35 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-second-digital-signature-page-3](./add-second-digital-signature-page-3.cs) | Add a Second Digital Signature on Page 3 | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to apply an additional visible digital signature to page three of a PDF using As... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Add Signature Field and Sign PDF on Specific Page | `Document`, `AddField`, `Signature` | Demonstrates adding a signature field to a specific page of a PDF and then applying a PKCS#1 digi... |
| [custom-signature-appearance-image-background](./custom-signature-appearance-image-background.cs) | Configure Custom Signature Appearance with Image and Backgro... | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to set a foreground image and a background color for a PDF digital signature usi... |
| [custom-signature-appearance-no-caption](./custom-signature-appearance-no-caption.cs) | Create Custom PDF Signature Appearance Without Caption | `PdfFileSignature`, `BindPdf`, `PKCS1` | Demonstrates signing a PDF with Aspose.Pdf while hiding the default "Digitally signed by" label b... |
| [digitally-sign-pdf-visible-first-page](./digitally-sign-pdf-visible-first-page.cs) | Digitally Sign PDF with Visible Signature on First Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to bind a PDF, set a certificate, and add a visible digital signature to page on... |
| [digitally-sign-pdf-with-reason-location](./digitally-sign-pdf-with-reason-location.cs) | Digitally Sign PDF with Reason and Location | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to apply a visible digital signature to a PDF using Aspose.Pdf, specifying the s... |
| [extract-certificate-details-from-pdf-signature](./extract-certificate-details-from-pdf-signature.cs) | Extract Certificate Details from PDF Signature | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to bind a PDF, enumerate its signature fields, and retrieve the X.509 certificat... |
| [extract-certificate-from-pdf-signature](./extract-certificate-from-pdf-signature.cs) | Extract Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to retrieve the signing certificate from a digital signature field in a PDF document an... |
| [extract-signature-certificate-serial-number](./extract-signature-certificate-serial-number.cs) | Extract and Log Signature Certificate Serial Number | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF, enumerate its signatures, extract the X.509 certificate fo... |
| [extract-signature-image-png](./extract-signature-image-png.cs) | Extract Signature Image and Save as PNG | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Shows how to bind a PDF, extract the image of the 'WitnessSignature' signature field using Aspose... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images to HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Loads a signed PDF, extracts each signature's image using Aspose.Pdf.Facades.PdfFileSignature, an... |
| [extract-signing-certificate-subject-dn](./extract-signing-certificate-subject-dn.cs) | Extract Signing Certificate Subject DN from PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to bind a signed PDF, retrieve the first signature, extract its X.509 certificat... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to extract digital signature details from a PDF using PdfFileSignature and create a new... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF Document | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to retrieve the names of all active signatures in a PDF file using Aspose.Pdf.Facades. |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List Signature Reasons and Locations in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF with Aspose.Pdf, retrieve all signature names, and extract each signature... |
| [locale-specific-pdf-signature](./locale-specific-pdf-signature.cs) | Locale‑Specific PDF Signature Appearance | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to apply a digital signature to a PDF with locale‑specific labels and appearance... |
| [remove-all-pdf-signatures-recursively](./remove-all-pdf-signatures-recursively.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to walk through a directory tree, open each PDF with Aspose.Pdf.Facades.PdfFileSignatur... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to use Aspose.Pdf.Facades.PdfFileSignature to delete all digital signatures from a PDF ... |
| [remove-pdf-signature-missing-handling](./remove-pdf-signature-missing-handling.cs) | Remove PDF Signature with Missing Signature Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to remove a specific signature from a PDF using Aspose.Pdf.Facades while gracefu... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to delete a digital signature named 'ApprovalSignature' from an existing PDF usi... |
| [remove-signatures-re-sign-pdf](./remove-signatures-re-sign-pdf.cs) | Remove Existing Signatures and Re‑sign PDF with a New Certif... | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove all existing digital signatures from a PDF and then apply a new signat... |
| [retry-sign-pdf-when-file-locked](./retry-sign-pdf-when-file-locked.cs) | Retry Signing PDF When File Is Locked | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with Aspose.Pdf while handling file‑locking issues by implementing... |
| [sign-and-verify-pdf](./sign-and-verify-pdf.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF with a certificate using Aspose.Pdf.Facades.PdfFileSigna... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Sign PDF with French Digital Signature Caption | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades and configure the SignatureCust... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using a certificate and add a visible signature image po... |
| [sign-pdf-with-hidden-appearance](./sign-pdf-with-hidden-appearance.cs) | Sign PDF with Hidden Signature Appearance | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using Aspose.Pdf while completely hiding the signature a... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF file using a password‑protected PFX certificate with Asp... |
| [sign-pdf-with-semi-transparent-background](./sign-pdf-with-semi-transparent-background.cs) | Sign PDF with Semi‑Transparent Background Appearance | `PKCS1`, `SignatureCustomAppearance`, `FromRgb` | Demonstrates how to digitally sign a PDF using Aspose.Pdf and configure a custom signature appear... |
| [sign-pdf-with-visible-image](./sign-pdf-with-visible-image.cs) | Sign PDF with Visible Image Using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SignatureAppearance` | A console application that adds a visible signature appearance from an image to a PDF file using ... |
| [suppress-signature-reason-location](./suppress-signature-reason-location.cs) | Suppress Signature Reason and Location When Signing PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with Aspose.Pdf while suppressing the reason, location, and contac... |
| ... | | | *and 5 more files* |

## Category Statistics
- Total examples: 35

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

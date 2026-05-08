---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (34/34 files) ← category-specific
- `using Aspose.Pdf.Forms;` (12/34 files)
- `using Aspose.Pdf;` (10/34 files)
- `using Aspose.Pdf.Text;` (1/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (30/34 files)
- `using System.Drawing;` (11/34 files)
- `using System.Collections.Generic;` (3/34 files)
- `using System.Globalization;` (2/34 files)
- `using System.Security.Cryptography.X509Certificates;` (2/34 files)
- `using System.Drawing.Imaging;` (1/34 files)
- `using System.Linq;` (1/34 files)
- `using System.Text;` (1/34 files)
- `using System.Text.Json;` (1/34 files)
- `using System.Threading;` (1/34 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-image-signature-to-pdf](./add-image-signature-to-pdf.cs) | Add Image Signature to PDF | `PdfFileSignature`, `BindPdf`, `SignatureAppearance` | Demonstrates how to sign a PDF document with a visible image signature using Aspose.Pdf.Facades.P... |
| [add-second-digital-signature-to-pdf](./add-second-digital-signature-to-pdf.cs) | Add a Second Digital Signature to a PDF Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to apply a second digital signature on page three of an existing PDF using Aspose.Pdf.F... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Add Signature Field and Sign PDF | `Document`, `SignatureField`, `PKCS1` | Demonstrates how to create a signature field on a specific page of a PDF and then digitally sign ... |
| [check-absence-of-signatures-in-pdf](./check-absence-of-signatures-in-pdf.cs) | Check for Absence of Signatures in PDF | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Demonstrates using Aspose.Pdf.Facades.PdfFileSignature to confirm that a PDF contains no signatur... |
| [custom-pdf-signature-appearance-no-caption](./custom-pdf-signature-appearance-no-caption.cs) | Create Custom PDF Signature Appearance Without Default Capti... | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with a visible signature using Aspose.Pdf and hide the default "Di... |
| [digitally-sign-first-page-pdf](./digitally-sign-first-page-pdf.cs) | Digitally Sign First Page of PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to create a PdfFileSignature, bind a PDF, set a certificate, add a visible digital sign... |
| [extract-certificate-details-from-pdf-signature-fie...](./extract-certificate-details-from-pdf-signature-fields.cs) | Extract Certificate Details from PDF Signature Fields | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to load a signed PDF, enumerate its signature fields, and retrieve the associated X.509... |
| [extract-signature-certificate-serial-number](./extract-signature-certificate-serial-number.cs) | Extract and Log Signature Certificate Serial Number | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Shows how to open a PDF, verify it contains digital signatures, retrieve each signature's X.509 c... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images from PDF and Embed in HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to use Aspose.Pdf.Facades.PdfFileSignature to extract digital signature images from a P... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `Document`, `PdfFileSignature`, `BindPdf` | Shows how to load a PDF, bind it to PdfFileSignature, extract the signing certificate from the 'L... |
| [extract-witness-signature-to-png](./extract-witness-signature-to-png.cs) | Extract Signature Image and Save as PNG | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Demonstrates how to extract the image from a PDF signature field using Aspose.Pdf.Facades and con... |
| [list-pdf-signature-reason-location](./list-pdf-signature-reason-location.cs) | List PDF Signature Reason and Location | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to open a PDF with Aspose.Pdf.Facades, retrieve all signature names, and extract each s... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF document to the PdfFileSignature facade and retrieve the names of all act... |
| [locale-specific-pdf-signature](./locale-specific-pdf-signature.cs) | Locale‑Specific PDF Signature Appearance | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates signing a PDF with Aspose.Pdf using a PKCS7 signature and customizing the visual app... |
| [log-pdf-signature-verification-to-json](./log-pdf-signature-verification-to-json.cs) | Log PDF Signature Verification Results to JSON | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Demonstrates how to verify digital signatures in a PDF using Aspose.Pdf and write each signature'... |
| [pdf-signature-report](./pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to read digital signatures from a PDF, verify each signature, and create a new P... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to use Aspose.Pdf's PdfFileSignature facade to enumerate and delete every digita... |
| [remove-all-signatures-from-pdfs](./remove-all-signatures-from-pdfs.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to recursively find PDF files in a directory and use Aspose.Pdf.Facades to strip all di... |
| [remove-pdf-signature-missing-field](./remove-pdf-signature-missing-field.cs) | Remove PDF Signature with Missing-Field Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to remove a specific digital signature from a PDF using Aspose.Pdf, while gracef... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to load a PDF, remove a signature named 'ApprovalSignature' using Aspose.Pdf, an... |
| [remove-signatures-re-sign-pdf](./remove-signatures-re-sign-pdf.cs) | Remove Existing Signatures and Re‑sign PDF with a New Certif... | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to clear all existing signatures from a PDF and apply a new digital signature us... |
| [retry-pdf-signing-when-file-locked](./retry-pdf-signing-when-file-locked.cs) | Retry PDF Signing When File Is Locked | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF using Aspose.Pdf.Facades with a retry mechanism that handles file‑... |
| [sign-and-verify-pdf-using-pdffilesignature](./sign-and-verify-pdf-using-pdffilesignature.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF with a PKCS#1 certificate using PdfFileSignature and the... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Sign PDF with French Caption Using SignatureCustomAppearance | `PdfFileSignature`, `PKCS1`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF and set the signature appearance caption to French by co... |
| [sign-pdf-hidden-signature](./sign-pdf-hidden-signature.cs) | Sign PDF with Hidden Signature Appearance | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates digitally signing a PDF using Aspose.Pdf while completely hiding the signature appea... |
| [sign-pdf-semi-transparent-background](./sign-pdf-semi-transparent-background.cs) | Sign PDF with Semi-Transparent Background Appearance | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF using Aspose.Pdf and configure a custom signature appear... |
| [sign-pdf-suppress-signature-text](./sign-pdf-suppress-signature-text.cs) | Sign PDF and Suppress Signature Text | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades and hide the default location, ... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `Document`, `Page`, `PdfFileSignature` | Shows how to apply a visible digital signature to the bottom‑right corner of the last page of a P... |
| [sign-pdf-with-custom-appearance](./sign-pdf-with-custom-appearance.cs) | Sign PDF with Custom Appearance (Foreground Image & Backgrou... | `PdfFileSignature`, `PKCS1`, `SignatureCustomAppearance` | Demonstrates how to configure a SignatureCustomAppearance with a foreground image and background ... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `Document`, `PdfFileSignature`, `SetCertificate` | Demonstrates how to digitally sign a PDF document using a password‑protected PFX certificate with... |
| ... | | | *and 4 more files* |

## Category Statistics
- Total examples: 34

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-sign-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_144637_f72c91`
<!-- AUTOGENERATED:END -->

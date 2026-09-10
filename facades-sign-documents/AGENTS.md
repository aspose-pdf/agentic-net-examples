---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **34** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (11/34 files)
- `using Aspose.Pdf.Forms;` (9/34 files)
- `using Aspose.Pdf.Text;` (1/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (28/34 files)
- `using System.Drawing;` (10/34 files)
- `using System.Collections.Generic;` (5/34 files)
- `using System.Security.Cryptography.X509Certificates;` (4/34 files)
- `using System.Globalization;` (2/34 files)
- `using System.Security.Cryptography;` (2/34 files)
- `using System.Drawing.Imaging;` (1/34 files)
- `using System.Text;` (1/34 files)
- `using System.Text.Json;` (1/34 files)
- `using System.Threading;` (1/34 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-digital-signature-to-pdf-page](./add-digital-signature-to-pdf-page.cs) | Add Digital Signature to First PDF Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to bind a PDF, set a certificate, and apply a visible digital signature to page ... |
| [add-second-digital-signature-to-pdf-page-3](./add-second-digital-signature-to-pdf-page-3.cs) | Add Second Digital Signature to PDF Page 3 | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates adding a second visible digital signature on page three of an existing PDF using Asp... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Create Signature Field and Sign PDF Document | `Document`, `AddField`, `PdfFileSignature` | Demonstrates how to add an empty digital signature field to a specific page of a PDF using Aspose... |
| [configure-custom-signature-appearance](./configure-custom-signature-appearance.cs) | Configure Custom Signature Appearance with Background Color ... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to set a custom signature appearance by adding a foreground image and a backgrou... |
| [custom-pdf-signature-appearance](./custom-pdf-signature-appearance.cs) | Create Custom PDF Signature Appearance Without Caption | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF with Aspose.Pdf using a PKCS#1 signature and a custom appearance that ... |
| [extract-certificate-details-from-pdf-signatures](./extract-certificate-details-from-pdf-signatures.cs) | Extract Certificate Details from PDF Signatures | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to load a signed PDF, enumerate its digital signatures, and retrieve the X.509 c... |
| [extract-certificate-serial-number-from-pdf-signatu...](./extract-certificate-serial-number-from-pdf-signatures.cs) | Extract Certificate Serial Number from PDF Signatures | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Demonstrates how to open a signed PDF, enumerate its digital signatures, extract the associated X... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images to HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to retrieve signature images from a signed PDF using Aspose.Pdf.Facades and embe... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to bind a PDF, extract the X.509 signing certificate from a specific signature field, a... |
| [extract-witness-signature-image](./extract-witness-signature-image.cs) | Extract Witness Signature Image from PDF | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Demonstrates how to locate a signature field named 'WitnessSignature' in a PDF, extract its image... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Creates a PDF document that enumerates all digital signatures in a source PDF, displaying signer,... |
| [hide-pdf-signature-appearance](./hide-pdf-signature-appearance.cs) | Hide PDF Signature Appearance While Keeping It Valid | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates signing a PDF with Aspose.Pdf and making the signature invisible by setting the visi... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF Document | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to use Aspose.Pdf.Facades to open a PDF file and retrieve all non‑empty signatur... |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List Signature Reasons and Locations in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF with Aspose.Pdf.Facades, enumerate its digital signatures, and retrieve e... |
| [locale-specific-pdf-digital-signature](./locale-specific-pdf-digital-signature.cs) | Locale‑Specific PDF Digital Signature with Custom Appearance | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF using Aspose.Pdf with appearance text localized for Germ... |
| [log-pdf-signature-verification-to-json](./log-pdf-signature-verification-to-json.cs) | Log PDF Signature Verification Results to JSON | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to verify each digital signature in a PDF using Aspose.Pdf.Facades and write the... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to load a PDF, remove every digital signature using Aspose.Pdf.Facades, and save a clea... |
| [remove-all-signatures-from-pdfs](./remove-all-signatures-from-pdfs.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to traverse a directory tree, load each PDF with Aspose.Pdf.Facades, remove all ... |
| [remove-and-replace-pdf-signature](./remove-and-replace-pdf-signature.cs) | Remove Existing Signature and Apply New PDF Signature | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to delete all existing signatures from a PDF and then sign the document with a new cert... |
| [remove-pdf-signature-missing-handling](./remove-pdf-signature-missing-handling.cs) | Remove PDF Signature with Missing Signature Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to delete a specific digital signature from a PDF using Aspose.Pdf.Facades and g... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Existing Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Shows how to delete a digital signature named "ApprovalSignature" from a PDF file using Aspose.Pd... |
| [sign-and-verify-pdf-using-pdffilesignature](./sign-and-verify-pdf-using-pdffilesignature.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with a self‑signed certificate using Aspose.Pdf.Facades.PdfFileSig... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Sign PDF with French Caption Using SignatureCustomAppearance | `PdfFileSignature`, `PKCS1`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF with Aspose.Pdf and set the signature caption language t... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `Document`, `Page`, `PdfFileSignature` | Shows how to apply a digital signature that is visible in the bottom‑right corner of the last pag... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF file using a password‑protected PFX certificate with Asp... |
| [sign-pdf-with-reason-and-location](./sign-pdf-with-reason-and-location.cs) | Sign PDF with Reason and Location using Aspose.Pdf Facade | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF, setting the signature reason to "Approved for release" ... |
| [sign-pdf-with-retry-on-file-lock](./sign-pdf-with-retry-on-file-lock.cs) | Sign PDF with Retry on File Lock | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF using Aspose.Pdf.Facades with a retry mechanism that handles file‑... |
| [sign-pdf-with-semi-transparent-appearance](./sign-pdf-with-semi-transparent-appearance.cs) | Sign PDF with Semi‑Transparent Background Appearance | `PdfFileSignature`, `Sign`, `Save` | Demonstrates how to digitally sign a PDF using a PKCS7 certificate and apply a custom signature a... |
| [sign-pdf-with-visible-image-signature](./sign-pdf-with-visible-image-signature.cs) | Sign PDF with Visible Image Signature | `PdfFileSignature`, `BindPdf`, `Sign` | Shows how to add a visible image signature to a PDF file using Aspose.Pdf.Facades.PdfFileSignatur... |
| [suppress-reason-location-when-signing-pdf](./suppress-reason-location-when-signing-pdf.cs) | Suppress Reason and Location When Signing a PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF with Aspose.Pdf while clearing the Reason, Location, and... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-sign-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

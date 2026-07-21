---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (14/35 files)
- `using Aspose.Pdf.Forms;` (11/35 files)
- `using Aspose.Pdf.Drawing;` (1/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (30/35 files)
- `using System.Drawing;` (8/35 files)
- `using System.Collections.Generic;` (6/35 files)
- `using System.Security.Cryptography.X509Certificates;` (3/35 files)
- `using System.Globalization;` (2/35 files)
- `using System.Data;` (1/35 files)
- `using System.Drawing.Imaging;` (1/35 files)
- `using System.Text.Json;` (1/35 files)
- `using System.Threading;` (1/35 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-locale-specific-visible-signatures](./add-locale-specific-visible-signatures.cs) | Add Locale‑Specific Visible Signatures to PDF | `PdfFileSignature`, `BindPdf`, `Sign` | Signs a PDF on multiple pages with visible signatures, each using a different culture (German, Sp... |
| [add-multiple-digital-signatures-to-pdf](./add-multiple-digital-signatures-to-pdf.cs) | Add Multiple Digital Signatures to PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to apply two digital signatures on different pages and rectangle areas of a PDF ... |
| [apply-semi-transparent-signature-appearance](./apply-semi-transparent-signature-appearance.cs) | Apply Semi-Transparent Signature Appearance to PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF using Aspose.Pdf.Facades with a semi‑transparent PNG image as the ... |
| [audit-pdf-signatures-reason-location](./audit-pdf-signatures-reason-location.cs) | Audit PDF Signatures: List Reason and Location | `PdfFileSignature`, `SignatureName`, `BindPdf` | Shows how to use Aspose.Pdf.Facades to read digital signatures from a PDF and retrieve each signa... |
| [create-and-sign-pdf-signature-field](./create-and-sign-pdf-signature-field.cs) | Create and Sign a PDF Signature Field on a Specific Page | `Document`, `Rectangle`, `SignatureField` | Shows how to add a digital signature field to a chosen page of a PDF and then sign it using a cer... |
| [create-invisible-pdf-digital-signature](./create-invisible-pdf-digital-signature.cs) | Create an Invisible Digital Signature for a PDF | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF with a cryptographically valid digital signature while completely hidi... |
| [custom-signature-appearance-hide-caption](./custom-signature-appearance-hide-caption.cs) | Create Custom Signature Appearance Without 'Digitally signed... | `PdfFileSignature`, `PKCS1`, `BindPdf` | Demonstrates how to sign a PDF with Aspose.Pdf and hide the default "Digitally signed by" label b... |
| [digitally-sign-first-page-pdf](./digitally-sign-first-page-pdf.cs) | Digitally Sign First Page of PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to bind a PDF using PdfFileSignature, configure a certificate, add a visible digital si... |
| [extract-certificate-details-from-pdf-signature](./extract-certificate-details-from-pdf-signature.cs) | Extract Certificate Details from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to load a signed PDF, enumerate its signature fields, and extract the X.509 cert... |
| [extract-certificate-serial-number-from-pdf](./extract-certificate-serial-number-from-pdf.cs) | Extract and Log Certificate Serial Number from PDF Signature | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | The example opens a signed PDF, verifies the presence of digital signatures, extracts each signat... |
| [extract-signature-image-png](./extract-signature-image-png.cs) | Extract Signature Image and Save as PNG | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Shows how to extract the image of a PDF signature field named "WitnessSignature" using Aspose.Pdf... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images and Generate HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates extracting digital signature images from a PDF with Aspose.Pdf.Facades and embedding... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to retrieve the X.509 signing certificate from a specific signature field in a PDF and ... |
| [extract-the-signing-certificate-s-subject-distingu...](./extract-the-signing-certificate-s-subject-distinguished-name-and-store-it-in-a-database.cs) | Extract The Signing Certificate S Subject Distinguished Name... |  | Extract The Signing Certificate S Subject Distinguished Name And Store It In A Database |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `GetSignatureNames`, `VerifySignature` | Creates a PDF report that lists each digital signature in a signed document, showing signer, vali... |
| [list-pdf-signature-names](./list-pdf-signature-names.cs) | List Signature Names in a PDF Document | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to use Aspose.Pdf.Facades to retrieve and display all non‑empty signature names contain... |
| [remove-all-pdf-signatures](./remove-all-pdf-signatures.cs) | Remove All Signatures from a PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to use Aspose.Pdf.Facades to strip all digital signatures from a PDF document and save ... |
| [remove-all-signatures-from-pdfs-recursively](./remove-all-signatures-from-pdfs-recursively.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to traverse a directory tree, load each PDF with Aspose.Pdf.Facades, remove all ... |
| [remove-pdf-signature-with-missing-field-handling](./remove-pdf-signature-with-missing-field-handling.cs) | Remove PDF Signature with Missing-Field Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to remove a named digital signature from a PDF using Aspose.Pdf.Facades, with gr... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to delete an existing digital signature named 'ApprovalSignature' from a PDF fil... |
| [remove-signature-re-sign-pdf](./remove-signature-re-sign-pdf.cs) | Remove Existing Signature and Re‑sign PDF with New Certifica... | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove any existing digital signatures from a PDF and then apply a new signat... |
| [retry-sign-pdf-when-file-locked](./retry-sign-pdf-when-file-locked.cs) | Retry Signing PDF When File Is Locked | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with Aspose.Pdf while handling file‑lock conflicts by implementing... |
| [sign-and-verify-pdf-using-pdffilesignature](./sign-and-verify-pdf-using-pdffilesignature.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF with a PFX certificate using PdfFileSignature and then v... |
| [sign-pdf-french-appearance](./sign-pdf-french-appearance.cs) | Sign PDF with French Signature Appearance | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates digitally signing a PDF with a visible signature image and setting the signature's c... |
| [sign-pdf-suppress-reason-location](./sign-pdf-suppress-reason-location.cs) | Sign PDF and Suppress Signature Reason and Location | `PdfFileSignature`, `PKCS1`, `BindPdf` | Demonstrates digitally signing a PDF with Aspose.Pdf.Facades while hiding the reason and location... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using a certificate and add a visible signature image po... |
| [sign-pdf-with-custom-appearance](./sign-pdf-with-custom-appearance.cs) | Sign PDF with Custom Appearance (Foreground Image & Backgrou... | `PdfFileSignature`, `PKCS1`, `SignatureCustomAppearance` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades, configuring a custom signature... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates digitally signing a PDF with a password‑protected PFX certificate and optionally add... |
| [sign-pdf-with-reason-and-location](./sign-pdf-with-reason-and-location.cs) | Sign PDF with Reason and Location | `PdfFileSignature`, `BindPdf`, `PKCS1` | Demonstrates digitally signing a PDF using a PFX certificate and setting the signature reason and... |
| [sign-pdf-with-visible-image](./sign-pdf-with-visible-image.cs) | Sign PDF with Visible Image Using Aspose.Pdf Facade | `PdfFileSignature`, `BindPdf`, `SignatureAppearance` | Demonstrates how to apply a visible signature image to a PDF document using Aspose.Pdf.Facades.Pd... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

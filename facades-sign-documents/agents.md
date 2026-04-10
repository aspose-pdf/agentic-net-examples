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

- `using Aspose.Pdf.Facades;` (35/35 files) ← category-specific
- `using Aspose.Pdf;` (9/35 files)
- `using Aspose.Pdf.Forms;` (9/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (28/35 files)
- `using System.Drawing;` (9/35 files)
- `using System.Collections.Generic;` (8/35 files)
- `using System.Security.Cryptography.X509Certificates;` (4/35 files)
- `using System.Globalization;` (2/35 files)
- `using System.Data;` (1/35 files)
- `using System.Net;` (1/35 files)
- `using System.Security.Cryptography;` (1/35 files)
- `using System.Text;` (1/35 files)
- `using System.Text.Json;` (1/35 files)
- `using System.Threading;` (1/35 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-second-digital-signature-page-3](./add-second-digital-signature-page-3.cs) | Add Second Digital Signature on Page 3 of PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to apply two digital signatures to a PDF using Aspose.Pdf.Facades, with the seco... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Add Signature Field and Sign PDF with Aspose.Pdf | `Document`, `FormEditor`, `AddField` | Demonstrates creating a PDF, adding a signature field on a specific page using FormEditor, and di... |
| [create-invisible-pdf-digital-signature](./create-invisible-pdf-digital-signature.cs) | Create an Invisible Digital Signature in PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with Aspose.Pdf using a hidden appearance, preserving cryptographi... |
| [custom-pdf-signature-appearance](./custom-pdf-signature-appearance.cs) | Create Custom PDF Signature Appearance Without Default Capti... | `Document`, `PdfFileSignature`, `BindPdf` | Shows how to sign a PDF using Aspose.Pdf and hide the default "Digitally signed by" label by conf... |
| [custom-signature-appearance-with-image](./custom-signature-appearance-with-image.cs) | Configure Custom Signature Appearance with Image and Backgro... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF using a PKCS#7 certificate while customizing the visible signature app... |
| [digitally-sign-first-page-pdf](./digitally-sign-first-page-pdf.cs) | Digitally Sign First Page of PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to bind a PDF, configure a certificate, and add a visible digital signature to page one... |
| [digitally-sign-pdf-with-reason-location](./digitally-sign-pdf-with-reason-location.cs) | Digitally Sign PDF with Reason and Location | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to use Aspose.Pdf.Facades to digitally sign a PDF, specifying the signature reason as '... |
| [extract-certificate-details-from-pdf-signature](./extract-certificate-details-from-pdf-signature.cs) | Extract Certificate Details from PDF Signature | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF, enumerate its signature fields, and retrieve the X.509 cer... |
| [extract-certificate-serial-number](./extract-certificate-serial-number.cs) | Extract and Log Certificate Serial Number from PDF Signature... | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to open a signed PDF, enumerate its signatures, extract the associated X509 certificate... |
| [extract-signature-image-to-png](./extract-signature-image-to-png.cs) | Extract Signature Image from PDF and Save as PNG | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Demonstrates how to bind a PDF, extract the image of a signature field named 'WitnessSignature', ... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images to HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to extract visual signature images from a signed PDF using Aspose.Pdf.Facades.Pd... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to retrieve the signing certificate from a named signature field in a PDF document and ... |
| [extract-signing-certificate-subject-dn](./extract-signing-certificate-subject-dn.cs) | Extract Signing Certificate Subject DN from PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF with Aspose.Pdf, extract the signing certificate of the fir... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Report | `Document`, `PdfFileSignature`, `GetSignatureNames` | Loads a signed PDF, extracts each signature's details and verification status using PdfFileSignat... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a PDF with Aspose.Pdf.Facades, retrieve all active signature names, and ... |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List Signature Reasons and Locations in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to open a PDF with Aspose.Pdf.Facades, retrieve all signature names, and output each si... |
| [locale-specific-pdf-digital-signature](./locale-specific-pdf-digital-signature.cs) | Locale-Specific PDF Digital Signature with Custom Appearance | `PKCS7`, `SignatureCustomAppearance`, `PdfFileSignature` | Demonstrates signing a PDF with a PKCS#7 digital signature and a custom appearance that uses loca... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from a PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to use Aspose.Pdf.Facades to delete every digital signature from a PDF file and save th... |
| [remove-all-signatures-from-pdfs](./remove-all-signatures-from-pdfs.cs) | Remove All Signatures from PDFs | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to recursively scan a directory for PDF files, open each with Aspose.Pdf.Facades.PdfFil... |
| [remove-existing-signatures-and-resign-pdf](./remove-existing-signatures-and-resign-pdf.cs) | Remove Existing Signatures and Re‑sign PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to delete all signatures from a PDF and then apply a new digital signature using... |
| [remove-named-signature-from-pdf](./remove-named-signature-from-pdf.cs) | Remove a Named Signature from a PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Shows how to bind a PDF, remove a specific signature by its name using Aspose.Pdf.Facades, and sa... |
| [remove-specific-signature-from-pdf](./remove-specific-signature-from-pdf.cs) | Remove Specific Signature from PDF with Error Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates locating a named digital signature in a PDF, removing it while keeping the field, an... |
| [semi-transparent-signature-background](./semi-transparent-signature-background.cs) | Apply Semi-Transparent Background to PDF Signature | `Color`, `PdfFileSignature`, `PKCS1` | Demonstrates signing a PDF with Aspose.Pdf while configuring a semi‑transparent background color ... |
| [sign-pdf-and-verify-signature](./sign-pdf-and-verify-signature.cs) | Sign PDF and Verify Signature using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF with a PKCS#12 certificate using Aspose.Pdf.Facades.PdfF... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Sign PDF with French Digital Signature Caption | `PdfFileSignature`, `BindPdf`, `Sign` | Shows how to digitally sign a PDF and change the signature appearance labels to French by configu... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades, placing a visible signature im... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF file using a password‑protected PFX certificate with Asp... |
| [sign-pdf-with-retry](./sign-pdf-with-retry.cs) | Sign PDF with Retry on File Lock | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates signing a PDF using Aspose.Pdf.Facades with a retry mechanism that handles file‑lock... |
| [sign-pdf-with-visible-image](./sign-pdf-with-visible-image.cs) | Sign PDF with Visible Image Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to add a visible signature image to a PDF file using Aspose.Pdf's PdfFileSignatu... |
| [suppress-reason-location-when-signing-pdf](./suppress-reason-location-when-signing-pdf.cs) | Suppress Reason and Location When Signing PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with Aspose.Pdf while suppressing the Reason and Location fields b... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-sign-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

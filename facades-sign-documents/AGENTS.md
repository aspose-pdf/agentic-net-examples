---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (30/35 files) ← category-specific
- `using Aspose.Pdf.Forms;` (24/35 files) ← category-specific
- `using Aspose.Pdf;` (15/35 files)
- `using Aspose.Pdf.Drawing;` (1/35 files)
- `using Aspose.Pdf.Signatures;` (1/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (35/35 files)
- `using System.Drawing;` (10/35 files)
- `using System.Collections.Generic;` (6/35 files)
- `using System.Security.Cryptography.X509Certificates;` (4/35 files)
- `using System.Globalization;` (2/35 files)
- `using System.Data;` (1/35 files)
- `using System.Drawing.Imaging;` (1/35 files)
- `using System.Text;` (1/35 files)
- `using System.Text.Json;` (1/35 files)
- `using System.Threading;` (1/35 files)

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
| [add-digital-signature-to-pdf-page](./add-digital-signature-to-pdf-page.cs) | Sign PDF with Visible Signature on First Page | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to bind a PDF, create a visible digital signature using a PFX certificate, and a... |
| [add-second-digital-signature-to-pdf-page-3](./add-second-digital-signature-to-pdf-page-3.cs) | Add Second Digital Signature to PDF Page 3 | `Document`, `Rectangle`, `SignatureField` | Shows how to place a second digital signature on page three of an existing PDF by defining a rect... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Create and Sign a PDF Signature Field on a Specific Page | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to add a signature field to a chosen page of a PDF and then digitally sign it us... |
| [configure-custom-signature-appearance](./configure-custom-signature-appearance.cs) | Sign PDF with Custom Appearance (Foreground Image & Backgrou... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to configure SignatureCustomAppearance to set a background colour and draw a for... |
| [custom-pdf-signature-appearance](./custom-pdf-signature-appearance.cs) | Create Custom PDF Signature Appearance and Hide Default Capt... | `Document`, `PdfFileSignature`, `PKCS7` | Demonstrates signing a PDF with Aspose.Pdf, applying a custom signature image, and removing the d... |
| [extract-certificate-details-from-pdf-signatures](./extract-certificate-details-from-pdf-signatures.cs) | Extract Certificate Details from Signed PDF Fields | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to load a signed PDF, enumerate its signature fields, extract the embedded X.509 certif... |
| [extract-certificate-serial-number-from-pdf-signatu...](./extract-certificate-serial-number-from-pdf-signatures.cs) | Extract Signing Certificate Serial Number from PDF Signature... | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF, enumerate its digital signatures, extract each signing cer... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Images from PDF and Generate HTML Report | `PdfExtractor`, `BindPdf`, `ExtractImage` | Demonstrates how to use Aspose.Pdf.Facades.PdfExtractor to extract all images (e.g., signatures) ... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `Document`, `SignatureField`, `Field` | Loads a PDF, locates a signature field named 'LegalSignature', extracts the embedded signing cert... |
| [extract-signing-certificate-subject-dn](./extract-signing-certificate-subject-dn.cs) | Extract Signing Certificate Subject from PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to read digital signatures in a PDF, extract the signing X.509 certificate, and ... |
| [extract-witness-signature-image](./extract-witness-signature-image.cs) | Extract Signature Image from PDF Form Field | `Document`, `SignatureField`, `ExtractImage` | Loads a PDF, locates the 'WitnessSignature' signature field, extracts its appearance image, and s... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Creates a PDF document that lists all digital signatures in a source PDF, showing each signer, si... |
| [hide-pdf-signature-appearance](./hide-pdf-signature-appearance.cs) | Sign PDF with Hidden Signature Appearance | `Document`, `PdfFileSignature`, `BindPdf` | Demonstrates applying a cryptographic digital signature to a PDF while keeping the visual signatu... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF Document | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to retrieve all digital signature names from a PDF file using Aspose.Pdf.Facades. |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List PDF Signature Reason and Location | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a PDF with Aspose.Pdf, retrieve all active digital signatures, and outpu... |
| [locale-specific-pdf-digital-signature](./locale-specific-pdf-digital-signature.cs) | Locale‑Specific PDF Signature Appearance | `Document`, `PdfFileSignature`, `PKCS7` | Demonstrates how to apply digital signatures with visual appearance that adapts to German, Spanis... |
| [log-pdf-signature-verification-to-json](./log-pdf-signature-verification-to-json.cs) | Verify PDF Signatures and Log Results to JSON | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF, verify each digital signature, and write the verification ... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from a PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Shows how to bind a signed PDF with Aspose.Pdf.Facades.PdfFileSignature, remove every digital sig... |
| [remove-all-signatures-from-pdfs](./remove-all-signatures-from-pdfs.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Shows how to walk through a directory tree, find PDF files and strip every digital signature from... |
| [remove-and-replace-pdf-signature](./remove-and-replace-pdf-signature.cs) | Remove Existing Signature and Re‑sign PDF with Updated Certi... | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to delete the first digital signature from a PDF and then apply a new PKCS#7 signature ... |
| [remove-pdf-signature-missing-handling](./remove-pdf-signature-missing-handling.cs) | Remove PDF Signature with Missing Field Error Handling | `Document`, `PdfFileSignature`, `SignatureName` | Loads a signed PDF, checks whether a specified signature field exists, reports available signatur... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to bind a signed PDF, remove a specific digital signature by name, and save the ... |
| [sign-and-verify-pdf-using-pdffilesignature](./sign-and-verify-pdf-using-pdffilesignature.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `Sign` | Shows how to digitally sign a PDF with a PFX certificate using Aspose.Pdf.Facades.PdfFileSignatur... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Digitally Sign PDF with French Caption | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates applying a PKCS#7 digital signature to a PDF and configuring the signature appearanc... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `Document`, `PdfFileSignature`, `PKCS7` | Demonstrates adding a visible PKCS#7 digital signature to the bottom‑right corner of the last pag... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF document with a password‑protected PFX certificate, crea... |
| [sign-pdf-with-reason-and-location](./sign-pdf-with-reason-and-location.cs) | Sign PDF with Reason and Location | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades, setting the signature reason t... |
| [sign-pdf-with-retry-on-file-lock](./sign-pdf-with-retry-on-file-lock.cs) | Retry PDF Signing When File Is Locked | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to sign a PDF with a visible signature using Aspose.Pdf and implement a retry me... |
| [sign-pdf-with-semi-transparent-appearance](./sign-pdf-with-semi-transparent-appearance.cs) | Create Visible PDF Signature with Semi‑Transparent Backgroun... | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to add a visible digital signature field to a PDF and customize its appearance w... |
| [sign-pdf-with-visible-image-signature](./sign-pdf-with-visible-image-signature.cs) | Add Signature Image to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, place a signature image as an ImageStamp on the first page, and save the... |
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
Updated: 2026-11-11 | Run: `20261111_071730_a21483`
<!-- AUTOGENERATED:END -->

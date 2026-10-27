---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **34** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (26/34 files) ← category-specific
- `using Aspose.Pdf.Forms;` (24/34 files) ← category-specific
- `using Aspose.Pdf;` (23/34 files) ← category-specific
- `using Aspose.Pdf.Text;` (1/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (32/34 files)
- `using System.Collections.Generic;` (6/34 files)
- `using System.Drawing;` (6/34 files)
- `using System.Security.Cryptography.X509Certificates;` (6/34 files)
- `using System.Linq;` (5/34 files)
- `using System.Data;` (1/34 files)
- `using System.Globalization;` (1/34 files)
- `using System.Text;` (1/34 files)
- `using System.Text.Json;` (1/34 files)
- `using System.Threading;` (1/34 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
Document doc = new Document("input.pdf");
// ... operations ...
doc.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-digital-signature-to-pdf-page](./add-digital-signature-to-pdf-page.cs) | Digitally Sign First Page of PDF with Visible Signature | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to bind a PDF, create a visible signature rectangle, load a PKCS#7 certificate, ... |
| [add-second-digital-signature-to-pdf-page-3](./add-second-digital-signature-to-pdf-page-3.cs) | Add Multiple Digital Signatures to a PDF Page | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to place two digital signature fields on the same PDF page and sign them using a... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Add Signature Field and Sign PDF on Specific Page | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to create an empty signature field on a chosen page of a PDF and then apply a vi... |
| [custom-pdf-signature-appearance](./custom-pdf-signature-appearance.cs) | Hide "Digitally signed by" Caption in PDF Signature | `Document`, `SignatureCustomAppearance`, `SignatureField` | Demonstrates how to create a custom signature appearance that removes the default "Digitally sign... |
| [extract-certificate-details-from-pdf-signatures](./extract-certificate-details-from-pdf-signatures.cs) | Extract Certificate Details from PDF Signature Field | `Document`, `Form`, `Field` | Loads a signed PDF, iterates through its form fields, and extracts the X.509 certificate informat... |
| [extract-certificate-serial-number-from-pdf-signatu...](./extract-certificate-serial-number-from-pdf-signatures.cs) | Extract Signing Certificate Serial Number from PDF Signature | `Document`, `Form`, `Field` | Loads a PDF, locates signature fields, extracts the signing X509 certificate, and logs its serial... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images from PDF and Embed in HTML Report | `Document`, `Page`, `XImage` | The example loads a PDF, iterates through its pages, extracts all embedded images (e.g., signatur... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature Field | `Document`, `Field`, `SignatureField` | Shows how to locate a signature field in a PDF with Aspose.Pdf, extract the embedded signing cert... |
| [extract-signing-certificate-subject-dn](./extract-signing-certificate-subject-dn.cs) | Extract Signing Certificate Subject DN from PDF Signatures | `Document`, `Field`, `SignatureField` | Demonstrates how to load a signed PDF, iterate over signature fields, extract the signing X509 ce... |
| [extract-witness-signature-image](./extract-witness-signature-image.cs) | Extract Signature Field Appearance as PNG | `PdfFormEditor`, `ExtractFieldAppearance`, `Document` | Demonstrates how to extract the visual appearance of a PDF form field (e.g., a witness signature)... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `Document`, `SignatureField` | Loads a signed PDF, extracts each signature field, obtains signer details and signing time, verif... |
| [hide-pdf-signature-appearance](./hide-pdf-signature-appearance.cs) | Create Hidden Digital Signature in PDF | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF with a cryptographically valid digital signature that has no visible a... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Field Names in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to retrieve all digital signature field names from a PDF document using Aspose.P... |
| [list-signature-reasons-and-locations](./list-signature-reasons-and-locations.cs) | List Signature Reason and Location from PDF | `Document`, `Field`, `SignatureField` | Shows how to open a PDF, iterate over its form fields, identify signature fields, and extract the... |
| [locale-specific-pdf-digital-signature](./locale-specific-pdf-digital-signature.cs) | Locale‑Specific PDF Digital Signature (German, Spanish, Japa... | `PdfFileSignature`, `PKCS7`, `SignatureCustomAppearance` | Demonstrates signing a PDF with Aspose.Pdf using a PKCS7 certificate while providing language‑spe... |
| [log-pdf-signature-verification-to-json](./log-pdf-signature-verification-to-json.cs) | Verify PDF Signatures and Export Results to JSON | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to load a signed PDF, enumerate its digital signatures, verify each one, and wri... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from a PDF | `Document`, `PdfFileSignature`, `GetSignatureNames` | Shows how to load a signed PDF, enumerate its digital signatures with PdfFileSignature, delete ea... |
| [remove-all-signatures-from-pdfs](./remove-all-signatures-from-pdfs.cs) | Remove All Signatures from PDFs Recursively | `Document`, `BindPdf`, `RemoveAllSignatures` | The script walks through a directory tree, loads each PDF, deletes every signature field, and ove... |
| [remove-and-replace-pdf-signature](./remove-and-replace-pdf-signature.cs) | Remove Existing Signatures and Re‑sign PDF with a New Certif... | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to delete all existing digital signatures from a PDF and then apply a new PKCS#7... |
| [remove-pdf-signature-missing-handling](./remove-pdf-signature-missing-handling.cs) | Remove PDF Signature Field with Error Handling | `Document`, `BindPdf`, `RemoveSignature` | Demonstrates loading a PDF, attempting to remove a specific signature field, handling the case wh... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Shows how to delete a named digital signature from a PDF file using Aspose.Pdf.Facades.PdfFileSig... |
| [sign-and-verify-pdf-using-pdffilesignature](./sign-and-verify-pdf-using-pdffilesignature.cs) | Sign and Verify PDF Using PdfFileSignature | `PdfFileSignature`, `PKCS1`, `BindPdf` | Demonstrates how to digitally sign a PDF with a PFX certificate using Aspose.Pdf.Facades.PdfFileS... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Sign PDF with French Signature Caption | `PdfFileSignature`, `BindPdf`, `PKCS7` | Shows how to digitally sign a PDF using Aspose.Pdf and force the signature caption to French by s... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Add Visible Signature to Bottom‑Right of Last PDF Page | `Document`, `Page`, `SignatureField` | Shows how to create a visible signature field on the bottom‑right corner of the last page of a PD... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to digitally sign a PDF document using a password‑protected PFX certificate with... |
| [sign-pdf-with-reason-and-location](./sign-pdf-with-reason-and-location.cs) | Sign PDF with Reason and Location using Aspose.Pdf | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to add a visible digital signature to a PDF and set the signature reason and loc... |
| [sign-pdf-with-retry-on-file-lock](./sign-pdf-with-retry-on-file-lock.cs) | Retry PDF Signing When File Is Locked | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to sign a PDF with a certificate using Aspose.Pdf while handling file‑locking is... |
| [sign-pdf-with-semi-transparent-appearance](./sign-pdf-with-semi-transparent-appearance.cs) | Configure Semi-Transparent Background for PDF Signature | `Document`, `SignatureField`, `PKCS7` | Demonstrates adding a signature field to a PDF, signing it with a PKCS7 certificate, and setting ... |
| [sign-pdf-with-visible-image-signature](./sign-pdf-with-visible-image-signature.cs) | Apply Signature Image to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp from a signature image, positioning it at the ... |
| [suppress-reason-location-when-signing-pdf](./suppress-reason-location-when-signing-pdf.cs) | Sign PDF with PKCS7 and Suppress Signature Details | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates signing a PDF using a PKCS#7 certificate with Aspose.Pdf while hiding location, reas... |
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
Updated: 2026-10-23 | Run: `20261023_025405_3a5c42`
<!-- AUTOGENERATED:END -->

---
name: facades-sign-documents
description: C# examples for facades-sign-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-sign-documents

> **Facades sign documents** in PDF using C# / .NET -- **31** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-sign-documents** category.
This folder contains standalone C# examples for facades-sign-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-sign-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (31/31 files) ← category-specific
- `using Aspose.Pdf;` (8/31 files)
- `using Aspose.Pdf.Forms;` (8/31 files)
- `using Aspose.Pdf.Text;` (1/31 files)
- `using System;` (31/31 files)
- `using System.IO;` (28/31 files)
- `using System.Drawing;` (10/31 files)
- `using System.Collections.Generic;` (7/31 files)
- `using System.Security.Cryptography.X509Certificates;` (2/31 files)
- `using System.Text;` (2/31 files)
- `using System.Drawing.Imaging;` (1/31 files)
- `using System.Globalization;` (1/31 files)
- `using System.Linq;` (1/31 files)
- `using System.Text.Json;` (1/31 files)
- `using System.Threading;` (1/31 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-multiple-digital-signatures-to-pdf](./add-multiple-digital-signatures-to-pdf.cs) | Add Multiple Digital Signatures to PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to apply two digital signatures on different pages of a PDF using Aspose.Pdf.Facades, i... |
| [add-signature-field-and-sign-pdf](./add-signature-field-and-sign-pdf.cs) | Add Signature Field and Sign PDF on a Specific Page | `Document`, `AddField`, `PKCS7` | Demonstrates how to add a digital signature form field to a chosen page of a PDF using FormEditor... |
| [add-visible-image-signature-to-pdf](./add-visible-image-signature-to-pdf.cs) | Add Visible Image Signature to PDF | `PdfFileSignature`, `BindPdf`, `SignatureAppearance` | Shows how to apply a visible image signature to a PDF file using the Aspose.Pdf Facade API in a c... |
| [custom-pdf-signature-appearance-no-caption](./custom-pdf-signature-appearance-no-caption.cs) | Create Custom PDF Signature Appearance Without Default Capti... | `PdfFileSignature`, `BindPdf`, `PKCS1` | Demonstrates signing a PDF with Aspose.Pdf while hiding the default "Digitally signed by" label b... |
| [digitally-sign-first-page-pdf](./digitally-sign-first-page-pdf.cs) | Digitally Sign First Page of a PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Shows how to bind a PDF, configure a certificate, and add a visible digital signature to page one... |
| [extract-certificate-details-from-signed-pdf](./extract-certificate-details-from-signed-pdf.cs) | Extract Certificate Details from Signed PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to read a digital signature from a PDF, extract the associated X509 certificate,... |
| [extract-certificate-serial-number-from-pdf](./extract-certificate-serial-number-from-pdf.cs) | Extract and Log Certificate Serial Number from PDF Signature... | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to open a signed PDF, enumerate its signatures, extract the associated X509 cert... |
| [extract-signature-field-image-to-png](./extract-signature-field-image-to-png.cs) | Extract Signature Field Image to PNG | `PdfFileSignature`, `BindPdf`, `ExtractImage` | Shows how to bind a PDF, extract the image of a signature field (e.g., 'WitnessSignature') using ... |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images and Generate HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | The example loads a signed PDF, extracts each signature's visual image, converts it to a Base64 d... |
| [extract-signing-certificate-from-pdf](./extract-signing-certificate-from-pdf.cs) | Extract Signing Certificate from PDF Signature | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Shows how to bind a PDF, locate a specific signature field, extract its X.509 certificate, and sa... |
| [generate-pdf-signature-report](./generate-pdf-signature-report.cs) | Generate PDF Signature Verification Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to extract signature details from a PDF, verify each signature, and create a sum... |
| [list-pdf-signature-reason-location](./list-pdf-signature-reason-location.cs) | List PDF Signature Reason and Location | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF with Aspose.Pdf's PdfFileSignature facade, retrieve all signature names, ... |
| [list-signature-names-in-pdf](./list-signature-names-in-pdf.cs) | List Signature Names in a PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Shows how to bind a PDF file using Aspose.Pdf.Facades and retrieve all non‑empty signature names ... |
| [remove-all-signatures-from-pdf](./remove-all-signatures-from-pdf.cs) | Remove All Signatures from a PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Shows how to use Aspose.Pdf.Facades.PdfFileSignature to strip every digital signature from a PDF ... |
| [remove-all-signatures-from-pdfs-recursively](./remove-all-signatures-from-pdfs-recursively.cs) | Remove All Signatures from PDFs Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to traverse a directory tree, load each PDF with Aspose.Pdf.Facades, remove all ... |
| [remove-existing-signatures-re-sign-pdf](./remove-existing-signatures-re-sign-pdf.cs) | Remove Existing Signatures and Re‑sign PDF with New Certific... | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove all existing signatures from a PDF and apply a new digital signature u... |
| [remove-pdf-signature-missing-handling](./remove-pdf-signature-missing-handling.cs) | Remove PDF Signature with Missing Signature Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates checking for a specific signature in a PDF and removing it using Aspose.Pdf.Facades,... |
| [remove-signature-from-pdf](./remove-signature-from-pdf.cs) | Remove Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to delete an existing digital signature named 'ApprovalSignature' from a PDF fil... |
| [sign-pdf-french-caption](./sign-pdf-french-caption.cs) | Digitally Sign PDF with French Caption Using SignatureCustom... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to apply a digital PKCS#7 signature to a PDF and set the signature caption langu... |
| [sign-pdf-semi-transparent-background](./sign-pdf-semi-transparent-background.cs) | Sign PDF with Semi-Transparent Background Appearance | `PdfFileSignature`, `BindPdf`, `Sign` | Shows how to digitally sign a PDF using Aspose.Pdf and set a custom signature appearance with a s... |
| [sign-pdf-visible-signature-last-page](./sign-pdf-visible-signature-last-page.cs) | Sign PDF with Visible Signature on Last Page | `Document`, `PdfFileSignature`, `BindPdf` | Demonstrates how to digitally sign a PDF using a certificate and add a visible signature image po... |
| [sign-pdf-with-custom-appearance](./sign-pdf-with-custom-appearance.cs) | Sign PDF with Custom Appearance (Foreground Image & Backgrou... | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to configure a PDF signature's custom appearance by setting a background color a... |
| [sign-pdf-with-pfx-certificate](./sign-pdf-with-pfx-certificate.cs) | Sign PDF with PFX Certificate using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF file using a password‑protected PFX certificate with Asp... |
| [sign-pdf-with-reason-location](./sign-pdf-with-reason-location.cs) | Sign PDF with Reason and Location using Aspose.Pdf | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF document with a custom reason and location using Aspose.... |
| [sign-pdf-with-retry](./sign-pdf-with-retry.cs) | Sign PDF with Retry on File Lock | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using Aspose.Pdf.Facades with a retry mechanism when the... |
| [suppress-reason-location-in-pdf-signature](./suppress-reason-location-in-pdf-signature.cs) | Suppress Reason and Location in PDF Digital Signature | `PdfFileSignature`, `BindPdf`, `Sign` | Demonstrates how to digitally sign a PDF with Aspose.Pdf while clearing the Reason, Location, and... |
| [verify-manager-signature-in-pdf](./verify-manager-signature-in-pdf.cs) | Verify Manager Signature in PDF | `Document`, `PdfFileSignature`, `BindPdf` | Demonstrates loading a PDF document and verifying the cryptographic integrity of a specific signa... |
| [verify-multiple-pdf-signatures](./verify-multiple-pdf-signatures.cs) | Verify Multiple PDF Signatures Using PdfFileSignature | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | The example loads a signed PDF, retrieves all signature names, and verifies each signature using ... |
| [verify-pdf-no-signatures-false](./verify-pdf-no-signatures-false.cs) | Verify PDF Without Signatures Returns False | `PdfFileSignature`, `BindPdf`, `VerifySigned` | Demonstrates how to use Aspose.Pdf's PdfFileSignature facade to check that a PDF with no digital ... |
| [verify-pdf-signature-by-name](./verify-pdf-signature-by-name.cs) | Verify PDF Signature by Name | `PdfFileSignature`, `BindPdf`, `VerifySigned` | Demonstrates how to check whether a PDF contains a digital signature with a specific name (e.g., ... |
| ... | | | *and 1 more files* |

## Category Statistics
- Total examples: 31

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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

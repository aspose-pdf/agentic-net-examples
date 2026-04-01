---
name: Facades - Sign Documents
description: C# examples for Facades - Sign Documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - Sign Documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - Sign Documents** category.
This folder contains standalone C# examples for Facades - Sign Documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - Sign Documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (29/30 files) ← category-specific
- `using Aspose.Pdf;` (11/30 files)
- `using Aspose.Pdf.Forms;` (9/30 files)
- `using Aspose.Pdf.Annotations;` (1/30 files)
- `using Aspose.Pdf.Signatures;` (1/30 files)
- `using Aspose.Pdf.Text;` (1/30 files)
- `using System;` (30/30 files)
- `using System.IO;` (29/30 files)
- `using System.Collections.Generic;` (9/30 files)
- `using System.Drawing;` (8/30 files)
- `using System.Security.Cryptography.X509Certificates;` (2/30 files)
- `using System.Drawing.Imaging;` (1/30 files)
- `using System.Globalization;` (1/30 files)
- `using System.Text;` (1/30 files)
- `using System.Text.Json;` (1/30 files)
- `using System.Threading;` (1/30 files)

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
| [add-multiple-digital-signatures](./add-multiple-digital-signatures.cs) | Add Multiple Digital Signatures to PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates adding a second digital signature on page three of a PDF using a different rectangle... |
| [add-signature-field-and-sign](./add-signature-field-and-sign.cs) | Add Signature Field and Sign PDF | `Document`, `AddSignatureField`, `PdfFileSignature` | Demonstrates adding a signature field to a PDF page and then signing it using a PKCS#1 certificate. |
| [configure-signature-custom-appearance](./configure-signature-custom-appearance.cs) | Configure Custom Signature Appearance with Foreground Image ... | `PdfFileSignature`, `SignatureCustomAppearance`, `Color` | Demonstrates how to set a foreground image and background color for a digital signature using Sig... |
| [custom-signature-appearance](./custom-signature-appearance.cs) | Create Custom Signature Appearance without Default Caption | `Document`, `Page`, `SignatureField` | Demonstrates how to add a signature field with a custom appearance that hides the default "Digita... |
| [extract-certificate-details](./extract-certificate-details.cs) | Extract Certificate Issuer and Expiration from Signed PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to read a digital signature from a PDF, extract its X.509 certificate, and displ... |
| [extract-certificate-serial-number](./extract-certificate-serial-number.cs) | Extract Signing Certificate Serial Number from PDF | `PdfFileSignature`, `BindPdf`, `TryExtractCertificate` | Demonstrates how to extract the X.509 signing certificate from a signed PDF and log its serial nu... |
| [extract-signature-image](./extract-signature-image.cs) | Extract Signature Field Image to PNG | `Document`, `ExtractImage`, `Page` | Extracts the image from a signature form field named 'WitnessSignature' and saves it as a PNG file. |
| [extract-signature-images-html-report](./extract-signature-images-html-report.cs) | Extract Signature Images and Generate HTML Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Loads a PDF, extracts images of digital signatures, saves them as JPEG files, and creates an HTML... |
| [extract-signing-certificate](./extract-signing-certificate.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `BindPdf`, `ExtractCertificate` | Demonstrates how to extract the X.509 certificate from a signature field named 'LegalSignature' i... |
| [hidden-pdf-signature](./hidden-pdf-signature.cs) | Apply Hidden Digital Signature to PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF with a digital signature while hiding its visual appearance. |
| [list-pdf-signature-names](./list-pdf-signature-names.cs) | List Signature Names in PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to retrieve and display all digital signature names from a PDF using Aspose.Pdf. |
| [list-signature-reason-location](./list-signature-reason-location.cs) | List Signature Reason and Location from PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to enumerate digital signatures in a PDF and output each signature's reason and ... |
| [pdf-signature-report](./pdf-signature-report.cs) | Generate PDF Signature Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Creates a PDF report that lists all digital signatures in a document, their signers, and verifica... |
| [remove-and-resign-pdf](./remove-and-resign-pdf.cs) | Remove Existing Signature and Re‑sign PDF with Updated Certi... | `PdfFileSignature`, `Signature`, `Rectangle` | Demonstrates how to remove all existing digital signatures from a PDF and then apply a new signat... |
| [remove-pdf-signature](./remove-pdf-signature.cs) | Remove Specific Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to remove a digital signature named 'ApprovalSignature' from a PDF using Aspose.... |
| [remove-pdf-signatures](./remove-pdf-signatures.cs) | Remove All Signatures from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove all digital signatures from a PDF file using Aspose.Pdf's PdfFileSigna... |
| [remove-pdf-signatures__v2](./remove-pdf-signatures__v2.cs) | Remove All Signatures from PDFs in a Directory Recursively | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to recursively locate PDF files in a directory and remove all digital signatures... |
| [remove-signature-error-handling](./remove-signature-error-handling.cs) | Remove Specific Signature with Error Handling | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to safely remove a named digital signature from a PDF, handling the case where t... |
| [retry-pdf-signing](./retry-pdf-signing.cs) | Retry Signing PDF When File Is Locked | `PdfFileSignature`, `Signature`, `Document` | Demonstrates signing a PDF with a retry mechanism that handles file‑locking by another process. |
| [set-signature-caption-language-french](./set-signature-caption-language-french.cs) | Set Signature Caption Language to French | `PdfFileSignature`, `SignatureCustomAppearance`, `BindPdf` | Demonstrates how to change the language of a digital signature caption to French using SignatureC... |
| [sign-pdf-reason-location](./sign-pdf-reason-location.cs) | Sign PDF with Reason and Location | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to add a digital signature to a PDF and set the signature reason and location us... |
| [sign-pdf-visible-last-page](./sign-pdf-visible-last-page.cs) | Sign PDF with Visible Signature on Last Page | `Document`, `PdfFileSignature`, `BindPdf` | Demonstrates adding a visible digital signature to the bottom‑right corner of the last page of a ... |
| [sign-pdf-with-image](./sign-pdf-with-image.cs) | Sign PDF with Image Signature | `PdfFileSignature`, `Signature`, `BindPdf` | Demonstrates how to sign a PDF using an image as the signature appearance via PdfFileSignature. |
| [signature-appearance-semi-transparent](./signature-appearance-semi-transparent.cs) | Configure Signature Appearance with Semi-Transparent Backgro... | `Document`, `Page`, `SignatureField` | Demonstrates how to set a custom signature appearance with a semi‑transparent background color us... |
| [suppress-signature-text](./suppress-signature-text.cs) | Suppress Signature Reason, Contact, and Location in PDF | `PdfFileSignature`, `PKCS1`, `BindPdf` | Demonstrates how to create a digital signature with empty reason, contact, and location fields us... |
| [validate-manager-signature](./validate-manager-signature.cs) | Validate ManagerSignature Field in PDF | `PdfFileSignature`, `VerifySignature` | Demonstrates how to verify the cryptographic integrity of the ManagerSignature field in a PDF usi... |
| [verify-multiple-signatures](./verify-multiple-signatures.cs) | Verify Multiple Signatures in PDF | `PdfFileSignature`, `BindPdf`, `GetSignNames` | Loads a signed PDF, lists all signature names and verifies each using VerifySigned. |
| [verify-no-signature](./verify-no-signature.cs) | Verify PDF Without Signatures Returns False | `Document`, `BindPdf`, `ContainsSignature` | Demonstrates checking a PDF that has no digital signatures using PdfFileSignature.VerifySigned, w... |
| [verify-pdf-signature](./verify-pdf-signature.cs) | Verify PDF Signature 'ContractSigner' | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Shows how to check if a PDF contains a digital signature named 'ContractSigner' and verify its va... |
| [verify-pdf-signatures](./verify-pdf-signatures.cs) | Verify PDF Signatures and Log Results to JSON | `Document`, `PdfFileSignature`, `SignatureName` | Loads a signed PDF, verifies each digital signature, and writes the signature name and validation... |

## Category Statistics
- Total examples: 30

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
- Review code examples in this folder for Facades - Sign Documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-01 | Run: `20260401_083243_90e036`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf.Facades;` (30/32 files) ← category-specific
- `using Aspose.Pdf;` (15/32 files)
- `using Aspose.Pdf.Forms;` (13/32 files)
- `using Aspose.Pdf.Text;` (1/32 files)
- `using System;` (32/32 files)
- `using System.IO;` (29/32 files)
- `using System.Collections.Generic;` (8/32 files)
- `using System.Drawing;` (8/32 files)
- `using System.Globalization;` (2/32 files)
- `using System.Security.Cryptography.X509Certificates;` (2/32 files)
- `using System.Linq;` (1/32 files)
- `using System.Text;` (1/32 files)
- `using System.Text.Json;` (1/32 files)

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
| [add-digital-signature](./add-digital-signature.cs) | Add Digital Signature with Reason and Location | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to add a visible digital signature to a PDF and set the signature reason and loc... |
| [add-second-digital-signature-page-three](./add-second-digital-signature-page-three.cs) | Add a second digital signature on page three of a PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates adding two digital signatures to a PDF, with the second placed on page three using a... |
| [add-signature-field](./add-signature-field.cs) | Add Signature Field and Sign PDF | `Document`, `Form`, `SignatureField` | Demonstrates adding a signature field to a specific page and then signing the PDF using a certifi... |
| [custom-signature-appearance](./custom-signature-appearance.cs) | Create Custom Signature Appearance without Default Caption | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF signature field with a custom appearance that hides the default ... |
| [digitally-sign-pdf-page-one](./digitally-sign-pdf-page-one.cs) | Digitally Sign PDF on First Page | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to bind a PDF with PdfFileSignature, set a certificate, and add a digital signat... |
| [extract-certificate-details](./extract-certificate-details.cs) | Extract Certificate Issuer and Expiration from Signed PDF | `PdfFileSignature`, `GetSignatureNames`, `TryExtractCertificate` | Demonstrates how to read a signed PDF, retrieve each signature's X.509 certificate, and display i... |
| [extract-certificate-serial-number](./extract-certificate-serial-number.cs) | Extract Signing Certificate Serial Number from PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Loads a signed PDF, extracts the signing certificate from the first signature field, and logs its... |
| [extract-signature-image](./extract-signature-image.cs) | Extract Signature Field Image to PNG | `Document`, `Form`, `ExtractImage` | Demonstrates extracting the image from a signature form field named 'WitnessSignature' and saving... |
| [extract-signature-images-html](./extract-signature-images-html.cs) | Extract Signature Images and Generate HTML Report | `Document`, `PdfFileSignature`, `SignatureName` | Extracts signature images from a signed PDF and creates an HTML file that displays each signature... |
| [extract-signing-certificate](./extract-signing-certificate.cs) | Extract Signing Certificate from PDF Signature Field | `PdfFileSignature`, `ExtractCertificate`, `SignatureName` | Demonstrates how to extract the X.509 certificate from a PDF signature field named 'LegalSignatur... |
| [list-pdf-signature-names](./list-pdf-signature-names.cs) | List Signature Names in PDF | `PdfFileSignature`, `BindPdf`, `GetSignNames` | Demonstrates how to retrieve and display all digital signature names from a PDF using Aspose.Pdf. |
| [list-signature-reason-location](./list-signature-reason-location.cs) | List Signature Reasons and Locations in PDF | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Demonstrates how to enumerate digital signatures in a PDF and output each signature's reason and ... |
| [locale-specific-signature](./locale-specific-signature.cs) | Locale-Specific Signature Text in PDF | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Adds a visible digital signature to a PDF with signature text adapted to German, Spanish, or Japa... |
| [pdf-signature-report](./pdf-signature-report.cs) | Generate PDF Signature Report | `PdfFileSignature`, `BindPdf`, `GetSignatureNames` | Creates a PDF summarizing each digital signature's name, signer, and verification result from an ... |
| [remove-all-signatures](./remove-all-signatures.cs) | Remove All Signatures from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove every digital signature from a PDF file using Aspose.Pdf's PdfFileSign... |
| [remove-and-resign-pdf](./remove-and-resign-pdf.cs) | Remove Existing Signatures and Re‑sign PDF with New Certific... | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Demonstrates how to remove all signatures from a PDF and then apply a new digital signature using... |
| [remove-pdf-signature](./remove-pdf-signature.cs) | Remove Specific Signature from PDF | `PdfFileSignature`, `BindPdf`, `RemoveSignature` | Demonstrates how to remove a digital signature named 'ApprovalSignature' from a PDF using Aspose.... |
| [remove-pdf-signatures](./remove-pdf-signatures.cs) | Remove All Signatures from PDFs in a Directory | `PdfFileSignature`, `BindPdf`, `RemoveSignatures` | Recursively scans a directory for PDF files, removes all digital signatures from each PDF, and sa... |
| [remove-signature-with-error-handling](./remove-signature-with-error-handling.cs) | Remove Specific Signature with Error Handling | `PdfFileSignature`, `SignatureName`, `GetSignatureNames` | Shows how to safely remove a named digital signature from a PDF, checking for its existence and h... |
| [set-french-language-signature](./set-french-language-signature.cs) | Set French Language for Digital Signature Caption | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to change the language of a digital signature caption to French by setting the S... |
| [set-signature-background](./set-signature-background.cs) | Set Semi-Transparent Background for PDF Signature Appearance | `PdfFileSignature`, `SignatureCustomAppearance`, `Color` | Demonstrates how to configure a PDF signature's custom appearance with a semi‑transparent backgro... |
| [sign-pdf-empty-signature-fields](./sign-pdf-empty-signature-fields.cs) | Sign PDF with Empty Reason, Contact, and Location | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to sign a PDF using Aspose.Pdf while suppressing the reason, contact, and locati... |
| [sign-pdf-hidden-appearance](./sign-pdf-hidden-appearance.cs) | Sign PDF with Hidden Signature Appearance | `PdfFileSignature`, `PKCS7`, `BindPdf` | Demonstrates how to apply a cryptographically valid digital signature to a PDF while completely h... |
| [sign-pdf-pfx](./sign-pdf-pfx.cs) | Sign PDF with PFX Certificate | `PdfFileSignature`, `BindPdf`, `SetCertificate` | Demonstrates how to digitally sign a PDF using a password‑protected PFX certificate. |
| [sign-pdf-visible-last-page](./sign-pdf-visible-last-page.cs) | Sign PDF with Visible Signature on Last Page | `PdfFileSignature`, `Document`, `Page` | Demonstrates how to add a visible digital signature to the bottom‑right corner of the last page o... |
| [sign-pdf-with-image](./sign-pdf-with-image.cs) | Sign PDF with Image Signature | `PdfFileSignature`, `Document`, `BindPdf` | Demonstrates how to apply an image signature to a PDF using Aspose.Pdf's PdfFileSignature facade. |
| [signature-custom-appearance](./signature-custom-appearance.cs) | Configure SignatureCustomAppearance with Foreground Image an... | `PdfFileSignature`, `SignatureCustomAppearance`, `Document` | Demonstrates how to set a background color and draw the signature image as a foreground image usi... |
| [validate-pdf-signature](./validate-pdf-signature.cs) | Validate PDF Signature Integrity | `PdfFileSignature`, `VerifySignature` | Demonstrates how to verify the cryptographic integrity of a PDF signature field named 'ManagerSig... |
| [verify-multiple-signatures](./verify-multiple-signatures.cs) | Verify Multiple Signatures in PDF | `PdfFileSignature`, `BindPdf`, `GetSignNames` | Demonstrates how to list all signature names in a PDF and verify each signature using PdfFileSign... |
| [verify-pdf-no-signatures](./verify-pdf-no-signatures.cs) | Verify PDF Without Signatures Returns False | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | Demonstrates checking a PDF that has no digital signatures using PdfFileSignature.VerifySigned, w... |
| ... | | | *and 2 more files* |

## Category Statistics
- Total examples: 32

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
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->

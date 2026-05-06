---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (83/90 files) ← category-specific
- `using Aspose.Pdf;` (55/90 files) ← category-specific
- `using Aspose.Pdf.Forms;` (15/90 files)
- `using Aspose.Pdf.Annotations;` (10/90 files)
- `using Aspose.Pdf.Text;` (1/90 files)
- `using System;` (90/90 files)
- `using System.IO;` (81/90 files)
- `using System.Collections.Generic;` (6/90 files)
- `using System.Drawing;` (5/90 files)
- `using System.Text.Json;` (3/90 files)
- `using System.Linq;` (2/90 files)
- `using System.Security.Cryptography;` (1/90 files)

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
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF with Appearance | `Document`, `AddField`, `PKCS1` | Shows how to add a signature field named "DigitalSignature" to a PDF, assign a visual appearance ... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `Save` | Demonstrates how to attach a JavaScript action to the Email text field of a PDF to validate the e... |
| [add-email-validation-to-pdf-form-field](./add-email-validation-to-pdf-form-field.cs) | Add Email Validation to PDF Form Field | `Document`, `FormEditor`, `SetFieldScript` | Shows how to use Aspose.Pdf.Facades.FormEditor to attach a JavaScript validation script to a PDF ... |
| [add-gender-radio-button-group-to-pdf-form](./add-gender-radio-button-group-to-pdf-form.cs) | Add Gender Radio Button Group to PDF Form | `FormEditor`, `FieldType`, `AddField` | Demonstrates how to add a radio button group named "Gender" with options Male, Female, Other and ... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF using Aspose.Pdf Facade | `Form`, `BindPdf`, `Save` | Demonstrates how to generate a secure random token and embed it as a hidden text field named "Aut... |
| [add-hidden-numeric-version-field](./add-hidden-numeric-version-field.cs) | Add Hidden Numeric Version Field to PDF | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a hidden numeric form field named "Version" with a value of 2 into an existin... |
| [add-hidden-sessionid-field-with-guid-to-pdfs](./add-hidden-sessionid-field-with-guid-to-pdfs.cs) | Add Hidden SessionId Field with GUID to PDFs in Batch | `Document`, `AddField`, `SetFieldAppearance` | Shows how to batch‑process PDF files, add a hidden text field named "SessionId" on the first page... |
| [add-javascript-alert-to-pdf-push-button](./add-javascript-alert-to-pdf-push-button.cs) | Add JavaScript Alert to PDF Push Button | `Document`, `FormEditor`, `BindPdf` | Shows how to bind a PDF, locate a push button named "ShowInfo", attach a JavaScript alert that ru... |
| [add-list-box-field-priority](./add-list-box-field-priority.cs) | Add List Box Field 'Priority' to PDF | `Document`, `FormEditor`, `AddField` | Shows how to create or load a PDF and add a list box form field named "Priority" with items Low, ... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to PDF Dropdown Field | `FormEditor`, `AddListItem`, `Save` | Shows how to add a new option (list item) to an existing combo box (dropdown) form field in a PDF... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Add Radio Button Group "PaymentMethod" to PDF | `Document`, `FormEditor`, `BindPdf` | Shows how to create a three‑page PDF and add a radio button group named PaymentMethod with option... |
| [add-print-button-to-pdf](./add-print-button-to-pdf.cs) | Add Print Button to PDF Using Aspose.Pdf FormEditor | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a push button into a PDF that triggers the print dialog via JavaScript using ... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `FormEditor`, `AddField` | Shows how to insert a push‑button named "ResetForm" into a PDF and attach JavaScript that clears ... |
| [add-state-combo-box-to-pdf](./add-state-combo-box-to-pdf.cs) | Add State Combo Box to PDF | `Document`, `FormEditor`, `AddField` | Shows how to create a PDF, add a combo box form field named "State", and populate it with U.S. st... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Shows how to add a submit button on page 1 of a PDF that posts form data to a specified URL using... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Field to PDF Page Using FormEditor | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to add a text form field named "CustomerName" to page 1 of an existing PDF using... |
| [add-unchecked-checkbox-to-pdf-page-2](./add-unchecked-checkbox-to-pdf-page-2.cs) | Add Unchecked Checkbox to PDF Page 2 | `Document`, `FormEditor`, `AddField` | Shows how to insert an unchecked checkbox form field named "AgreeTerms" on the second page of an ... |
| [apply-consistent-decoration-to-checkbox-fields](./apply-consistent-decoration-to-checkbox-fields.cs) | Apply Consistent Decoration to Checkbox Fields in PDFs | `Document`, `FormEditor`, `FormFieldFacade` | Processes all PDF files in a directory, sets visual attributes for form fields using FormEditor a... |
| [apply-custom-font-to-pdf-text-fields](./apply-custom-font-to-pdf-text-fields.cs) | Apply Custom Font to All PDF Text Fields | `FormEditor`, `FormFieldFacade`, `Facade` | Shows how to set a custom font (Arial Bold) for every text form field in a PDF document using Asp... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Fields in a PDF | `FormEditor`, `FormFieldFacade`, `DecorateField` | Shows how to configure a FormFieldFacade with background, text, and border colors and alignment, ... |
| [attach-confirmation-js-to-pdf-submit-button](./attach-confirmation-js-to-pdf-submit-button.cs) | Attach Confirmation JavaScript to PDF Submit Button | `Document`, `FormEditor`, `AddFieldScript` | Shows how to load a PDF, add a JavaScript confirmation dialog to a submit button using FormEditor... |
| [attach-javascript-populate-date-field](./attach-javascript-populate-date-field.cs) | Attach JavaScript to Populate Date Field on PDF Open | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Demonstrates how to use Aspose.Pdf's PdfContentEditor to add a JavaScript action that fills a for... |
| [attach-javascript-validation-to-age-field](./attach-javascript-validation-to-age-field.cs) | Attach JavaScript Validation to Age Field in PDF | `Document`, `FormEditor`, `SetFieldScript` | Demonstrates loading a PDF, attaching a JavaScript snippet to the "Age" form field that shows a w... |
| [attach-js-clear-discountcode-field](./attach-js-clear-discountcode-field.cs) | Attach JavaScript to Clear DiscountCode Field on Focus | `Document`, `FormEditor`, `AddFieldScript` | Demonstrates using Aspose.Pdf's FormEditor to add a JavaScript action that clears the "DiscountCo... |
| [attach-js-reset-button-clear-hidden-fields](./attach-js-reset-button-clear-hidden-fields.cs) | Attach JavaScript to Reset Button to Clear Hidden Fields | `Document`, `FormEditor`, `AddFieldScript` | Shows how to use Aspose.Pdf's FormEditor facade to add JavaScript to a reset button that clears h... |
| [attach-js-to-quantity-field](./attach-js-to-quantity-field.cs) | Attach JavaScript to PDF Form Field for Auto‑Calculating Tot... | `Document`, `FormEditor`, `SetFieldScript` | Shows how to use Aspose.Pdf's FormEditor to add a JavaScript action to the 'Quantity' field so th... |
| [attach-validation-js-to-pdf-submit-button](./attach-validation-js-to-pdf-submit-button.cs) | Attach Validation JavaScript to PDF Submit Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to add JavaScript to a PDF submit button using Aspose.Pdf.Facades to validate re... |
| [batch-add-hidden-guid-field-to-pdfs](./batch-add-hidden-guid-field-to-pdfs.cs) | Batch Add Hidden GUID Field to PDFs | `Document`, `FormEditor`, `AddField` | Shows how to loop through PDF files in a folder and add a hidden text field containing a generate... |
| [batch-add-selectall-checkbox](./batch-add-selectall-checkbox.cs) | Batch Add 'SelectAll' Checkbox to First Page of PDFs | `Document`, `FormEditor`, `AddField` | Shows how to loop through PDFs in a folder and use the FormEditor facade to add a checkbox field ... |
| [batch-concatenate-pdfs-unique-form-fields](./batch-concatenate-pdfs-unique-form-fields.cs) | Batch Concatenate PDFs with Unique Form Field Names | `PdfFileEditor`, `KeepFieldsUnique`, `UniqueSuffix` | Demonstrates how to merge multiple PDF form files in a batch while automatically renaming duplica... |
| ... | | | *and 60 more files* |

## Category Statistics
- Total examples: 90

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.FieldType`
- `Aspose.Pdf.Facades.Form`
- `Aspose.Pdf.Facades.Form.BindPdf`
- `Aspose.Pdf.Facades.Form.BindPdf(string)`
- `Aspose.Pdf.Facades.Form.ExportFdf`
- `Aspose.Pdf.Facades.Form.FillField(string, string)`
- `Aspose.Pdf.Facades.Form.GetField(string)`
- `Aspose.Pdf.Facades.Form.ImportFdf`
- `Aspose.Pdf.Facades.Form.Save`
- `Aspose.Pdf.Facades.Form.Save(string)`
- `Aspose.Pdf.Facades.FormEditor`
- `Aspose.Pdf.Facades.FormEditor.BindPdf`
- `Aspose.Pdf.Facades.FormEditor.CopyOuterField`
- `Aspose.Pdf.Facades.FormEditor.Save`
- `Aspose.Pdf.Facades.FormFieldFacade`

### Rules
- Bind a PDF file to a Form facade with Form.BindPdf({input_pdf}).
- Flatten every form field in the bound document by calling Form.FlattenAllFields().
- Persist the flattened document using Form.Save({output_pdf}).
- Use Form.BindPdf({input_pdf}) to open a PDF document for form manipulation.
- Open an FDF file as a stream and call Form.ImportFdf({fdf_stream}) to populate the PDF form fields.

### Warnings
- The Form class belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in future releases; consider using the newer Document/FormField APIs.
- The example manually manages the FileStream; ensure the stream is closed or disposed to avoid resource leaks.
- The example assumes the target PDF already contains an AcroForm; otherwise AddField may have no effect.
- Coordinate values are in points; callers must convert from other units if needed.
- FormFieldFacade.Alignment expects one of the FormFieldFacade alignment constants (e.g., AlignCenter).

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-06 | Run: `20260506_114203_105326`
<!-- AUTOGENERATED:END -->

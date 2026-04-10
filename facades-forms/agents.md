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

- `using Aspose.Pdf.Facades;` (83/88 files) ← category-specific
- `using Aspose.Pdf;` (49/88 files) ← category-specific
- `using Aspose.Pdf.Forms;` (14/88 files)
- `using Aspose.Pdf.Annotations;` (5/88 files)
- `using System;` (88/88 files)
- `using System.IO;` (82/88 files)
- `using System.Collections.Generic;` (8/88 files)
- `using System.Drawing;` (4/88 files)
- `using System.Text.Json;` (3/88 files)
- `using System.Globalization;` (1/88 files)
- `using System.Linq;` (1/88 files)
- `using System.Security.Cryptography;` (1/88 files)

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
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `SetSubmitUrl` | Demonstrates how to attach JavaScript to a PDF form submit button using Aspose.Pdf to show a conf... |
| [add-customername-text-field-to-pdf](./add-customername-text-field-to-pdf.cs) | Add Text Field "CustomerName" to PDF Page | `Document`, `FormEditor`, `FieldType` | Demonstrates how to add a text form field named CustomerName to the first page of a PDF using Asp... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `AddField`, `PKCS7` | Demonstrates adding a signature field named "DigitalSignature" to a PDF, setting its visual appea... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to attach a JavaScript blur‑event script to the "Email" form field of a PDF usin... |
| [add-email-validation-script-to-pdf-form-field](./add-email-validation-script-to-pdf-form-field.cs) | Add Email Validation Script to PDF Form Field | `Document`, `FormEditor`, `AddFieldScript` | Demonstrates how to attach a JavaScript regular‑expression validation script to an "Email" form f... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `FormEditor`, `AddField`, `Save` | Demonstrates how to add a radio button group named "Gender" with options Male, Female, Other and ... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Document`, `FormEditor`, `FieldType` | Shows how to generate a secure token and insert it as a hidden form field in a PDF document using... |
| [add-hidden-numeric-version-field](./add-hidden-numeric-version-field.cs) | Add Hidden Numeric Version Field to PDF | `Document`, `FormEditor`, `FieldType` | Shows how to insert a hidden numeric field named "Version" into a PDF using Aspose.Pdf's FormEdit... |
| [add-hidden-sessionid-field-to-pdfs](./add-hidden-sessionid-field-to-pdfs.cs) | Add Hidden SessionId Field to PDFs | `Document`, `FormEditor`, `Form` | Demonstrates how to batch‑process PDF files, add a hidden text field named "SessionId" on the fir... |
| [add-javascript-alert-to-pdf-push-button](./add-javascript-alert-to-pdf-push-button.cs) | Add JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to attach a JavaScript alert to a push button named "ShowInfo" in a PDF using As... |
| [add-javascript-populate-date-field](./add-javascript-populate-date-field.cs) | Add JavaScript to Populate Date Field on PDF Open | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Demonstrates how to attach a JavaScript action to a PDF form that sets the "Date" field to the cu... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a ListBox form field named "Priority" with items Low, Medium, High and set Me... |
| [add-list-item-to-dropdown](./add-list-item-to-dropdown.cs) | Add List Item to Dropdown in PDF Form | `FormEditor`, `AddListItem`, `Save` | Shows how to add a new option to a combo box (dropdown) field in a PDF form using Aspose.Pdf.Faca... |
| [add-populate-state-combobox](./add-populate-state-combobox.cs) | Add and Populate a Combo Box with US State Abbreviations | `Document`, `FormEditor`, `FieldType` | Shows how to create a PDF in memory, add a ComboBox form field, and fill it with US state abbrevi... |
| [add-print-button-with-javascript](./add-print-button-with-javascript.cs) | Add Print Button with JavaScript to PDF | `Document`, `AddField`, `AddFieldScript` | Shows how to insert a push‑button form field into a PDF and attach JavaScript that opens the prin... |
| [add-radio-button-group-to-pdf](./add-radio-button-group-to-pdf.cs) | Add Radio Button Group to PDF Form | `Document`, `FormEditor` | Demonstrates how to create a three‑page PDF (if needed) and add a radio button group named "Payme... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a push button that clears all form fields in a PDF by attaching JavaScript us... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Demonstrates how to add a submit button on page 1 of a PDF that posts form data to a specified UR... |
| [add-unchecked-checkbox-field-to-pdf-page](./add-unchecked-checkbox-field-to-pdf-page.cs) | Add Unchecked Checkbox Field to PDF Page | `Document`, `FormEditor`, `AddField` | Demonstrates how to insert an unchecked checkbox form field named 'AgreeTerms' on the second page... |
| [apply-arial-bold-font-to-pdf-text-fields](./apply-arial-bold-font-to-pdf-text-fields.cs) | Apply Arial Bold Font to All PDF Text Form Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Shows how to use Aspose.Pdf.Facades.FormEditor to set a custom font for every text form field in ... |
| [apply-consistent-decoration-to-checkbox-fields](./apply-consistent-decoration-to-checkbox-fields.cs) | Apply Consistent Decoration to All Checkbox Fields in PDFs | `FormEditor`, `BindPdf`, `Save` | Shows how to batch‑process PDFs in a directory, set a visual facade for form fields, and apply th... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Shows how to configure a FormFieldFacade to set background, text, border colors and alignment for... |
| [attach-javascript-to-quantity-field](./attach-javascript-to-quantity-field.cs) | Attach JavaScript to PDF Form Field to Calculate Total Price | `FormEditor`, `BindPdf`, `SetFieldScript` | Shows how to bind a PDF, add a JavaScript action to the "Quantity" field that multiplies the valu... |
| [attach-javascript-validation-to-age-field](./attach-javascript-validation-to-age-field.cs) | Attach JavaScript Validation to Age Form Field | `Document`, `Page`, `Annotation` | Demonstrates locating the "Age" form field in a PDF and attaching a JavaScript action that alerts... |
| [attach-javascript-validation-to-pdf-submit-button](./attach-javascript-validation-to-pdf-submit-button.cs) | Attach JavaScript Validation to PDF Submit Button | `FormEditor`, `SetFieldScript`, `Save` | Demonstrates how to add JavaScript to a push‑button field in a PDF using Aspose.Pdf.Facades to va... |
| [attach-js-clear-discountcode-on-focus](./attach-js-clear-discountcode-on-focus.cs) | Attach JavaScript to Clear DiscountCode Field on Focus | `Document`, `FormEditor`, `SetFieldScript` | Shows how to use Aspose.Pdf's FormEditor to add a JavaScript action that clears a form field when... |
| [attach-js-reset-button-clear-hidden-fields](./attach-js-reset-button-clear-hidden-fields.cs) | Attach JavaScript to Reset Button to Clear Hidden Fields | `Document`, `FormEditor` | Shows how to use Aspose.Pdf to add JavaScript to a PDF form's reset button so that the form is re... |
| [batch-add-hidden-guid-field-to-pdfs](./batch-add-hidden-guid-field-to-pdfs.cs) | Batch Add Hidden GUID Field to PDFs | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to iterate over PDF files in a folder, add a hidden text field named "ProcessedD... |
| [batch-add-selectall-checkbox-to-first-page](./batch-add-selectall-checkbox-to-first-page.cs) | Batch Add SelectAll Checkbox to First Page of PDFs | `Document`, `FormEditor`, `AddField` | Shows how to loop through PDFs in a folder and use Aspose.Pdf's FormEditor facade to add a checkb... |
| [batch-rename-pdf-form-fields](./batch-rename-pdf-form-fields.cs) | Batch Rename PDF Form Fields | `Form`, `RenameField`, `Save` | Demonstrates how to iterate through all form fields in a PDF and rename those that start with a s... |
| ... | | | *and 58 more files* |

## Category Statistics
- Total examples: 88

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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

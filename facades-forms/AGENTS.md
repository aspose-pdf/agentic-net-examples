---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **87** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (66/87 files) ← category-specific
- `using Aspose.Pdf.Forms;` (53/87 files) ← category-specific
- `using Aspose.Pdf.Facades;` (46/87 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (16/87 files)
- `using Aspose.Pdf.Text;` (1/87 files)
- `using System;` (87/87 files)
- `using System.IO;` (86/87 files)
- `using System.Collections.Generic;` (9/87 files)
- `using System.Linq;` (7/87 files)
- `using System.Drawing;` (4/87 files)
- `using System.Text.Json;` (4/87 files)
- `using System.Reflection;` (1/87 files)
- `using System.Security.Cryptography;` (1/87 files)
- `using System.Text.RegularExpressions;` (1/87 files)

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
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Attach Confirmation JavaScript to SubmitForm Button | `PdfFormEditor`, `BindPdf`, `SetJavaScript` | Demonstrates how to bind a PDF, attach a JavaScript confirmation dialog to a SubmitForm button, a... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `SignatureField`, `PdfFileSignature` | Shows how to create a visible signature field, load a PKCS#1 certificate, and apply a digital sig... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates how to attach a JavaScript action to a PDF form TextBox field to validate an email a... |
| [add-email-validation-to-pdf-form-field](./add-email-validation-to-pdf-form-field.cs) | Apply Email Regex Validation to PDF Form Field | `PdfFormEditor`, `BindPdf`, `SetFieldRegEx` | Demonstrates how to bind a PDF, set a regular‑expression validation rule for the "Email" form fie... |
| [add-form-load-javascript-to-pdf](./add-form-load-javascript-to-pdf.cs) | Attach JavaScript to PDF Form Load Event to Populate Date Fi... | `PdfFormEditor`, `BindPdf`, `AddJavaScript` | Shows how to bind a PDF, add a document‑level JavaScript that sets the "Date" field to the curren... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `PdfFormEditor`, `BindPdf`, `AddRadioButtonGroup` | Shows how to insert a radio‑button group named "Gender" with options Male, Female, and Other, and... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Generates a cryptographically secure token and inserts it as a hidden text box form field named A... |
| [add-hidden-sessionid-field-to-pdfs-batch](./add-hidden-sessionid-field-to-pdfs-batch.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `TextBoxField`, `Rectangle` | Shows how to loop through PDF files, generate a GUID, create a hidden TextBoxField, add it to the... |
| [add-hidden-version-field-to-pdf](./add-hidden-version-field-to-pdf.cs) | Add Hidden Numeric Version Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to insert a hidden numeric textbox field named "Version" with a value of 2 into an exis... |
| [add-javascript-alert-to-push-button](./add-javascript-alert-to-push-button.cs) | Attach JavaScript Alert to PDF Push Button | `PdfFormEditor`, `BindPdf`, `SetJavaScript` | Shows how to bind an existing PDF, attach JavaScript that displays an alert to a push button name... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add Drop‑Down List Field to PDF | `Document`, `Rectangle`, `ComboBoxField` | Demonstrates how to add a combo box (drop‑down list) form field to an existing PDF, populate it w... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to PDF Dropdown Field | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to use Aspose.Pdf.Facades.FormEditor to add a new list item to an existing dropd... |
| [add-print-button-to-pdf](./add-print-button-to-pdf.cs) | Add Print Button to PDF Using Aspose.Pdf | `Document`, `Rectangle`, `ButtonField` | Shows how to insert a push button named PrintForm into a PDF that triggers the print dialog via J... |
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add PaymentMethod Radio Button Group to PDF Form | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a radio button field with "Credit" and "PayPal" options on page 3 of a... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `ButtonField`, `JavascriptAction` | Shows how to insert a push button into an existing PDF that clears all form fields when clicked, ... |
| [add-state-combobox-to-pdf](./add-state-combobox-to-pdf.cs) | Add State Combo Box to PDF | `Document`, `Rectangle`, `ComboBoxField` | Shows how to create a PDF, add a combo box form field named "State", and fill it with US state ab... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Form Button to PDF | `Document`, `Page`, `ButtonField` | Shows how to create a button field on a PDF page and attach a SubmitFormAction that posts the for... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Box Field to PDF Page | `Document`, `Rectangle`, `TextBoxField` | Shows how to insert a TextBoxField named CustomerName onto the first page of a PDF at specific re... |
| [add-unchecked-checkbox-field-to-pdf](./add-unchecked-checkbox-field-to-pdf.cs) | Add Unchecked Checkbox Field to PDF Page 2 | `Document`, `Page`, `Rectangle` | Demonstrates how to insert an unchecked checkbox form field named 'AgreeTerms' on the second page... |
| [apply-custom-font-to-pdf-form-text-fields](./apply-custom-font-to-pdf-form-text-fields.cs) | Apply Arial Bold Font to PDF Text Fields | `Document`, `TextBoxField`, `DefaultAppearance` | Loads a PDF, iterates through all form fields, and sets the default appearance of each text box t... |
| [apply-phone-number-input-mask](./apply-phone-number-input-mask.cs) | Set Input Mask on PDF TextBox Field via Reflection | `Document`, `Save`, `TextBoxField` | Demonstrates how to apply an input mask "(###) ###‑####" to a PDF form TextBoxField using Aspose.... |
| [attach-javascript-clear-discountcode-field](./attach-javascript-clear-discountcode-field.cs) | Attach Focus-Clear JavaScript to DiscountCode Field | `Document`, `TextBoxField`, `JavascriptAction` | Loads a PDF, finds the 'DiscountCode' text box form field, attaches a JavaScript action that clea... |
| [attach-javascript-to-pdf-form-field](./attach-javascript-to-pdf-form-field.cs) | Attach JavaScript Calculation to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to add a JavaScript calculate action to a PDF form field using Aspose.Pdf so that the T... |
| [attach-javascript-validation-to-pdf-form-field](./attach-javascript-validation-to-pdf-form-field.cs) | Attach JavaScript Validation to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to load a PDF, locate the "Age" text box form field, attach a JavaScript OnValidate act... |
| [attach-js-resetform-clear-hidden-fields](./attach-js-resetform-clear-hidden-fields.cs) | Attach Reset Form JavaScript to Clear Hidden Fields | `Document`, `ButtonField`, `JavascriptAction` | Loads a PDF, locates the 'ResetForm' button, and attaches JavaScript that resets the form and cle... |
| [attach-js-validation-to-pdf-submit-button](./attach-js-validation-to-pdf-submit-button.cs) | Attach Validation JavaScript to PDF Submit Button | `Document`, `FormEditor`, `BindPdf` | Demonstrates loading a PDF, creating JavaScript that validates required fields, and attaching it ... |
| [batch-add-hidden-guid-field-to-pdfs](./batch-add-hidden-guid-field-to-pdfs.cs) | Batch Add Hidden GUID Field to PDFs | `Document`, `TextBoxField`, `Rectangle` | Iterates through all PDF files in the "InputForms" folder, adds a hidden text field named "Proces... |
| [batch-add-selectall-checkbox-to-pdf](./batch-add-selectall-checkbox-to-pdf.cs) | Batch Add SelectAll Checkbox to PDFs | `Document`, `CheckboxField`, `Rectangle` | Shows how to iterate through a folder of PDF files, add a checkbox form field named "SelectAll" t... |
| [batch-rename-pdf-form-fields](./batch-rename-pdf-form-fields.cs) | Batch Rename PDF Form Fields from Old_ to New_ | `PdfFormEditor`, `BindPdf`, `GetFieldNames` | Demonstrates how to load a PDF, iterate over its form fields, and rename all fields that start wi... |
| [batch-update-submit-button-urls](./batch-update-submit-button-urls.cs) | Batch Update Submit Button URLs in PDFs | `Form`, `FormEditor`, `BindPdf` | Demonstrates how to iterate through a folder of PDF files and change the submit button action URL... |
| ... | | | *and 57 more files* |

## Category Statistics
- Total examples: 87

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-22 | Run: `20261022_032854_cd5d71`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf.Facades;` (62/83 files) ← category-specific
- `using Aspose.Pdf;` (53/83 files) ← category-specific
- `using Aspose.Pdf.Forms;` (28/83 files)
- `using Aspose.Pdf.Annotations;` (5/83 files)
- `using Aspose.Pdf.Text;` (1/83 files)
- `using System;` (83/83 files)
- `using System.IO;` (76/83 files)
- `using System.Collections.Generic;` (3/83 files)
- `using System.Drawing;` (2/83 files)
- `using System.Linq;` (1/83 files)
- `using System.Security.Cryptography;` (1/83 files)

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
| [add-combo-box-states](./add-combo-box-states.cs) | Add Combo Box with US State Abbreviations to PDF | `Document`, `Page`, `FormEditor` | Creates a PDF, adds a combo box field named 'State', and populates it with US state abbreviations... |
| [add-confirmation-js-submit](./add-confirmation-js-submit.cs) | Add Confirmation JavaScript to Submit Button | `FormEditor`, `SetFieldScript` | Demonstrates attaching a JavaScript confirmation dialog to a PDF submit button using Aspose.Pdf. |
| [add-country-list-items](./add-country-list-items.cs) | Add 195 Countries to List Box in PDF Form | `FormEditor`, `AddListItem` | Demonstrates how to populate a PDF form list box named CountryList with all country names using F... |
| [add-digital-signature-field](./add-digital-signature-field.cs) | Add Digital Signature Field and Sign PDF | `Document`, `SignatureField`, `PdfFileSignature` | Demonstrates adding a signature field named "DigitalSignature" to a PDF, setting its default appe... |
| [add-email-validation-js](./add-email-validation-js.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `AddFieldScript`, `Save` | Demonstrates adding JavaScript to the "Email" form field to validate the email format when the fi... |
| [add-hidden-authtoken-field](./add-hidden-authtoken-field.cs) | Add Hidden AuthToken Field to PDF | `Document`, `Form`, `FieldFlag` | Demonstrates how to generate a secure token and embed it as a hidden form field in a PDF using As... |
| [add-hidden-numeric-version-field](./add-hidden-numeric-version-field.cs) | Add Hidden Numeric Version Field to PDF Form | `FormEditor`, `AddField`, `Save` | Demonstrates how to add a hidden numeric field named "Version" with a value of 2 to an existing P... |
| [add-hidden-sessionid-field](./add-hidden-sessionid-field.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `Form`, `TextBoxField` | Adds a hidden form field named SessionId with a generated GUID to each PDF file in the current di... |
| [add-js-alert-to-push-button](./add-js-alert-to-push-button.cs) | Add JavaScript Alert to Push Button in PDF Form | `FormEditor`, `SetFieldScript` | Demonstrates how to attach a JavaScript alert to a push button named "ShowInfo" in an existing PD... |
| [add-js-clear-field-on-focus](./add-js-clear-field-on-focus.cs) | Add JavaScript to Clear a Form Field on Focus | `FormEditor`, `SetFieldScript`, `Save` | Demonstrates how to attach a JavaScript action to a PDF form field that clears its content when t... |
| [add-list-item-dropdown](./add-list-item-dropdown.cs) | Add List Item to Dropdown Field in PDF | `FormEditor`, `AddListItem` | Demonstrates adding a new option "Option A" to an existing dropdown field named "Choices" on page... |
| [add-phone-number-input-mask](./add-phone-number-input-mask.cs) | Add Phone Number Input Mask to PDF Form Field | `Document`, `Page`, `NumberField` | Creates a PDF form field named PhoneNumber and applies an input mask (###) ###‑#### using allowed... |
| [add-print-button](./add-print-button.cs) | Add Print Button with JavaScript to PDF | `Document`, `FormEditor`, `AddField` | Demonstrates adding a push button named "PrintForm" to a PDF that opens the print dialog via Java... |
| [add-priority-list-box](./add-priority-list-box.cs) | Add List Box Field with Priority Options to PDF | `Document`, `Page`, `ListBoxField` | Creates a PDF with a list box field named 'Priority' containing Low, Medium, High items and sets ... |
| [add-radio-button-group](./add-radio-button-group.cs) | Add Radio Button Group to PDF Page | `Document`, `RadioButtonField`, `AddOption` | Creates a PDF with three pages and adds a radio button group named "PaymentMethod" with "Credit" ... |
| [add-radio-button-group__v2](./add-radio-button-group__v2.cs) | Add Radio Button Group to PDF | `Document`, `FormEditor`, `FieldType` | Creates a PDF and adds a "Gender" radio button group with options Male, Female, Other, selecting ... |
| [add-reset-form-button](./add-reset-form-button.cs) | Add Reset Form Push Button to PDF | `Document`, `FormEditor`, `AddField` | Demonstrates adding a push button named "ResetForm" to a PDF that clears all form fields when cli... |
| [add-submit-button](./add-submit-button.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn` | Demonstrates adding a submit button named 'SubmitForm' to page 1 of a PDF that posts form data to... |
| [add-text-field](./add-text-field.cs) | Add Text Field to PDF Page | `FormEditor`, `AddField`, `Save` | Demonstrates adding a text form field named CustomerName to the first page of a PDF using Aspose.... |
| [apply-custom-font-to-text-fields](./apply-custom-font-to-text-fields.cs) | Apply Custom Font to All Text Fields in PDF | `Document`, `FormEditor`, `FormFieldFacade` | Demonstrates how to set the "Arial Bold" font for every text field in a PDF using FormEditor and ... |
| [apply-default-decoration-text-fields](./apply-default-decoration-text-fields.cs) | Apply Default Decoration to All Text Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Demonstrates how to set a default visual style for all text fields in a PDF using FormEditor and ... |
| [assign-custom-attribute-to-pdf-form-field](./assign-custom-attribute-to-pdf-form-field.cs) | Assign Custom Attribute to PDF Form Field | `Document`, `FormEditor`, `WidgetAnnotation` | Demonstrates how to locate a form field, set a standard attribute with FormEditor, and add a cust... |
| [attach-javascript-age-field](./attach-javascript-age-field.cs) | Attach JavaScript Warning to Age Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to add a JavaScript action to a PDF form field that shows a warning when the ent... |
| [attach-javascript-to-field](./attach-javascript-to-field.cs) | Attach JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to add a JavaScript action to a PDF form field that updates another field when t... |
| [attach-javascript-to-resetform-that-also-clears-hi...](./attach-javascript-to-resetform-that-also-clears-hidden-fields-during-reset.cs) | Attach Javascript To Resetform That Also Clears Hidden Field... | `FormEditor` | Attach Javascript To Resetform That Also Clears Hidden Fields During Reset |
| [attach-js-form-load-date](./attach-js-form-load-date.cs) | Attach JavaScript to PDF Form Load Event to Populate Date Fi... | `Document`, `JavaScriptAction`, `Add` | Demonstrates adding a document-level JavaScript that sets the "Date" form field to the current da... |
| [attach-js-validation-submit-button](./attach-js-validation-submit-button.cs) | Attach JavaScript Validation to PDF Submit Button | `FormEditor`, `SetFieldScript` | Demonstrates adding JavaScript to a PDF submit button that checks required fields before submitti... |
| [auto-wrap-address-field](./auto-wrap-address-field.cs) | Enable Auto‑Wrap and Dynamic Height for PDF Form Field | `Document`, `Single2Multiple`, `FitIntoRectangle` | Demonstrates converting a single‑line form field to multiline, enabling auto‑wrap and dynamic hei... |
| [batch-add-checkbox](./batch-add-checkbox.cs) | Batch Add Checkbox Field to PDFs | `Document`, `CheckboxField`, `Page` | Adds a 'SelectAll' checkbox to the first page of each PDF in a folder and saves the modified files. |
| [batch-add-processeddate-field](./batch-add-processeddate-field.cs) | Batch Add Hidden ProcessedDate Field to PDFs | `Document`, `Form`, `FillField` | Iterates over all PDF files in the InputForms folder, adds (or fills) a hidden field named Proces... |
| ... | | | *and 53 more files* |

## Category Statistics
- Total examples: 83

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

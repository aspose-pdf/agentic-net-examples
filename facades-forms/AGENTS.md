---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **86** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (61/86 files) ← category-specific
- `using Aspose.Pdf.Facades;` (59/86 files) ← category-specific
- `using Aspose.Pdf.Forms;` (32/86 files)
- `using Aspose.Pdf.Annotations;` (15/86 files)
- `using Aspose.Pdf.Text;` (2/86 files)
- `using System;` (86/86 files)
- `using System.IO;` (85/86 files)
- `using System.Collections.Generic;` (11/86 files)
- `using System.Linq;` (9/86 files)
- `using System.Drawing;` (5/86 files)
- `using System.Text.Json;` (4/86 files)
- `using System.Globalization;` (1/86 files)
- `using System.Security.Cryptography;` (1/86 files)
- `using System.Text;` (1/86 files)

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
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to Submit Button in PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Demonstrates locating a button field in a PDF form and attaching a JavaScript action that shows a... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `SignatureField`, `Rectangle` | Shows how to create a signature field named "DigitalSignature" in a PDF and then sign the documen... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to attach a JavaScript blur‑event script to an "Email" form field in a PDF using... |
| [add-email-validation-to-pdf-form-field](./add-email-validation-to-pdf-form-field.cs) | Add Email Regex Validation to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to apply a JavaScript regular‑expression validation script to an "Email" form fi... |
| [add-form-load-javascript-to-pdf](./add-form-load-javascript-to-pdf.cs) | Add JavaScript to Populate Date Field on PDF Open | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript action to a PDF document using Aspose.Pdf so that the "Da... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `Document`, `Page`, `RadioButtonField` | Shows how to create a 'Gender' radio button group with Male, Female, and Other options, set a def... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to generate a secure random token and embed it as invisible text on every page of a PDF... |
| [add-hidden-sessionid-field-to-pdfs-batch](./add-hidden-sessionid-field-to-pdfs-batch.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to iterate over a folder of PDF files, generate a GUID for each, and add a hidde... |
| [add-hidden-version-field-to-pdf](./add-hidden-version-field-to-pdf.cs) | Add Hidden Numeric Field to PDF Form | `Document`, `NumberField`, `Rectangle` | Shows how to create a hidden numeric form field named "Version" with a value of 2 in a PDF docume... |
| [add-javascript-alert-to-push-button](./add-javascript-alert-to-push-button.cs) | Attach JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to bind an existing PDF, add a JavaScript alert to a push button named "ShowInfo", and ... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field with Default Selection to PDF | `Document`, `FormEditor`, `AddField` | Demonstrates how to create a ListBox form field in a PDF, populate it with items, and set a defau... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to Dropdown Field in PDF | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to use Aspose.Pdf.Facades.FormEditor to add a new option to an existing dropdown... |
| [add-print-button-to-pdf](./add-print-button-to-pdf.cs) | Add Print Button to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a push‑button field into an existing PDF and attach JavaScript that opens the... |
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add Radio Button Group to PDF Form | `Document`, `Page`, `RadioButtonField` | Shows how to insert a radio button group named PaymentMethod with "Credit" and "PayPal" options o... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `ButtonField`, `DefaultAppearance` | Shows how to create a push button in a PDF that clears all form fields when clicked, using Aspose... |
| [add-state-combobox-to-pdf](./add-state-combobox-to-pdf.cs) | Add State Combo Box to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a combo box field named "State" into an existing PDF and fill it with US stat... |
| [add-submit-button-to-pdf-form__v2](./add-submit-button-to-pdf-form__v2.cs) | Add Submit Button to PDF Form | `Document`, `FormEditor`, `BindPdf` | Shows how to insert a push‑button field on the first page of a PDF and configure it to post form ... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Field to PDF Page Using FormEditor | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to add a text form field named 'CustomerName' to page 1 of an existing PDF using... |
| [add-unchecked-checkbox-field-to-pdf](./add-unchecked-checkbox-field-to-pdf.cs) | Add Unchecked Checkbox Field to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert an unchecked checkbox named "AgreeTerms" on page 2 of an existing PDF using t... |
| [apply-custom-font-to-pdf-form-text-fields](./apply-custom-font-to-pdf-form-text-fields.cs) | Apply Custom Font to PDF Form Text Fields | `Document`, `Field`, `TextBoxField` | Shows how to load a PDF, iterate over its form fields, and set the Arial Bold font for all text b... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Fields in a PDF | `Document`, `FormEditor`, `BindPdf` | The example loads a PDF, configures default font, colors, border, and alignment via FormEditor’s ... |
| [apply-phone-number-input-mask](./apply-phone-number-input-mask.cs) | Set Input Mask on PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to apply an input mask to a PDF form field by attaching a JavaScript snippet usi... |
| [attach-javascript-clear-discountcode-field](./attach-javascript-clear-discountcode-field.cs) | Attach JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to use Aspose.Pdf's FormEditor facade to add a JavaScript action that clears a form fie... |
| [attach-javascript-to-pdf-form-field](./attach-javascript-to-pdf-form-field.cs) | Attach JavaScript to Update Total Price from Quantity Field | `Document`, `FormEditor`, `BindPdf` | Shows how to bind a PDF with FormEditor, add a JavaScript action to the "Quantity" field that cal... |
| [attach-javascript-validation-to-pdf-form-field](./attach-javascript-validation-to-pdf-form-field.cs) | Attach JavaScript Validation to PDF Form Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to load a PDF, attach a JavaScript script to the "Age" form field that alerts when the ... |
| [attach-js-resetform-clear-hidden-fields](./attach-js-resetform-clear-hidden-fields.cs) | Attach Js Resetform Clear Hidden Fields | `FormEditor` | Attach Js Resetform Clear Hidden Fields |
| [attach-js-validation-to-pdf-submit-button](./attach-js-validation-to-pdf-submit-button.cs) | Add JavaScript Validation to PDF Submit Button | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to add a push‑button to a PDF and attach JavaScript that validates required fiel... |
| [batch-add-hidden-guid-field-to-pdfs](./batch-add-hidden-guid-field-to-pdfs.cs) | Batch Add Hidden ProcessedDate Field to PDFs | `Document`, `Page`, `TextBoxField` | Demonstrates how to iterate over PDF files in a folder, add a hidden TextBoxField named "Processe... |
| [batch-add-selectall-checkbox-to-pdf](./batch-add-selectall-checkbox-to-pdf.cs) | Add SelectAll Checkbox to PDF First Page | `Document`, `Page`, `Rectangle` | Demonstrates how to iterate through a folder of PDF files, add a checkbox form field named "Selec... |
| [batch-rename-pdf-form-fields](./batch-rename-pdf-form-fields.cs) | Batch Rename PDF Form Fields with Prefix | `Form`, `FormEditor`, `BindPdf` | Demonstrates how to retrieve all form field names from a PDF and rename those that start with "Ol... |
| ... | | | *and 56 more files* |

## Category Statistics
- Total examples: 86

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
Updated: 2026-11-10 | Run: `20261110_222211_3400ad`
<!-- AUTOGENERATED:END -->

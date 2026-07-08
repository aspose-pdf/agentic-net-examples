---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **88** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (84/88 files) ← category-specific
- `using Aspose.Pdf;` (41/88 files)
- `using Aspose.Pdf.Forms;` (16/88 files)
- `using Aspose.Pdf.Annotations;` (7/88 files)
- `using System;` (88/88 files)
- `using System.IO;` (83/88 files)
- `using System.Drawing;` (7/88 files)
- `using System.Collections.Generic;` (3/88 files)
- `using System.Text.Json;` (2/88 files)
- `using System.Security.Cryptography;` (1/88 files)
- `using System.Text.RegularExpressions;` (1/88 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `FormEditor`, `AddField` | Shows how to add a signature field named "DigitalSignature" to a PDF using FormEditor and then di... |
| [add-email-validation-javascript-to-pdf-form](./add-email-validation-javascript-to-pdf-form.cs) | Add Email Validation JavaScript to PDF Form Field | `Document`, `Page`, `TextBoxField` | Shows how to create a PDF with an Email text box and attach a JavaScript blur‑event script that v... |
| [add-email-validation-script-to-pdf-form-field](./add-email-validation-script-to-pdf-form-field.cs) | Add Email Validation Script to PDF Form Field | `FormEditor`, `AddFieldScript`, `Save` | Demonstrates how to attach a JavaScript regular‑expression validation script to an "Email" form f... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF | `FormEditor`, `BindPdf`, `AddField` | Shows how to create a radio button group named "Gender" with options Male, Female, Other and set ... |
| [add-header-image-and-center-form-field](./add-header-image-and-center-form-field.cs) | Add Header Image and Center Align Form Field | `Document`, `PdfFileStamp`, `AddHeader` | Creates a PDF with a textbox form field named "Header", adds a background header image to each pa... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Demonstrates generating a secure random token and inserting it as a hidden TextBoxField in a PDF ... |
| [add-hidden-sessionid-field-to-pdfs-batch](./add-hidden-sessionid-field-to-pdfs-batch.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to iterate over a folder of PDF files, generate a unique GUID for each, and add ... |
| [add-hidden-version-field-to-pdf-form](./add-hidden-version-field-to-pdf-form.cs) | Add Hidden Version Field to PDF Form | `Document`, `FormEditor`, `AddField` | Demonstrates how to add a hidden numeric field named "Version" to an existing PDF form and set it... |
| [add-javascript-alert-to-pdf-push-button](./add-javascript-alert-to-pdf-push-button.cs) | Add JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to bind an existing PDF, attach a JavaScript alert to a push button named "ShowInfo", a... |
| [add-javascript-populate-date-field](./add-javascript-populate-date-field.cs) | Add JavaScript to Populate Date Field on PDF Open | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Shows how to use Aspose.Pdf.Facades.PdfContentEditor to bind a PDF, attach a document‑open JavaSc... |
| [add-javascript-validation-to-pdf-submit-button](./add-javascript-validation-to-pdf-submit-button.cs) | Add JavaScript Validation to PDF Submit Button | `FormEditor`, `AddSubmitBtn`, `SetFieldScript` | Demonstrates how to add a submit button to a PDF and attach JavaScript that validates required fi... |
| [add-js-clear-discountcode-on-focus](./add-js-clear-discountcode-on-focus.cs) | Add JavaScript to Clear DiscountCode Field on Focus | `Document`, `Field`, `TextBoxField` | Demonstrates how to load a PDF with Aspose.Pdf, locate a text box form field named 'DiscountCode'... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field to PDF Form | `FormEditor`, `BindPdf`, `Items` | Demonstrates how to use Aspose.Pdf.Facades to add a ListBox form field named "Priority" with item... |
| [add-list-item-to-dropdown-field](./add-list-item-to-dropdown-field.cs) | Add List Item to Dropdown Field in PDF | `FormEditor`, `AddListItem`, `Save` | Shows how to insert a new option into a combo box (dropdown) form field of a PDF document using A... |
| [add-print-button-to-pdf](./add-print-button-to-pdf.cs) | Add Print Button to PDF with JavaScript | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a push‑button field into a PDF and attach JavaScript that opens the print dia... |
| [add-radio-button-group-to-pdf](./add-radio-button-group-to-pdf.cs) | Add Radio Button Group to PDF Form | `Document`, `FormEditor`, `AddField` | Shows how to insert a radio button group named "PaymentMethod" with "Credit" and "PayPal" options... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `FormEditor`, `AddField` | Shows how to insert a push button into a PDF that clears all form fields when clicked, using Aspo... |
| [add-state-combo-box-to-pdf-form](./add-state-combo-box-to-pdf-form.cs) | Add State Combo Box to PDF Form | `Document`, `FormEditor`, `AddField` | Shows how to insert a combo box field named "State" into a PDF form and fill it with U.S. state a... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Shows how to insert a submit button on the first page of a PDF using Aspose.Pdf's FormEditor faca... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Field to PDF Page | `Document`, `FormEditor`, `FieldType` | Shows how to insert a text field named "CustomerName" on page 1 of an existing PDF using Aspose.P... |
| [add-unchecked-checkbox-to-pdf-page](./add-unchecked-checkbox-to-pdf-page.cs) | Insert Unchecked Checkbox Field on PDF Page | `Document`, `FormEditor`, `AddField` | Demonstrates how to add a checkbox form field to a specific page of an existing PDF using Aspose.... |
| [apply-2-point-border-to-pdf-form-field](./apply-2-point-border-to-pdf-form-field.cs) | Apply 2-Point Border to PDF Form Field | `FormEditor`, `BindPdf`, `Facade` | Demonstrates how to set a custom border thickness of 2 points for the form field named "Signature... |
| [apply-custom-font-to-pdf-text-fields](./apply-custom-font-to-pdf-text-fields.cs) | Apply Custom Font to All PDF Text Form Fields | `FormEditor`, `FormFieldFacade`, `DecorateField` | Shows how to set a custom font (Arial Bold) for every text form field in a PDF using Aspose.Pdf F... |
| [apply-phone-number-input-mask-to-pdf-form-field](./apply-phone-number-input-mask-to-pdf-form-field.cs) | Apply Phone Number Input Mask to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldLimit` | Demonstrates how to bind a PDF, set a character limit, and attach JavaScript to enforce a (###) #... |
| [attach-confirmation-dialog-to-pdf-submit-button](./attach-confirmation-dialog-to-pdf-submit-button.cs) | Attach Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to use Aspose.Pdf Facades FormEditor to add JavaScript to a submit button that displays... |
| [attach-javascript-update-total-price](./attach-javascript-update-total-price.cs) | Attach JavaScript to Update Total Price in PDF Form | `Form`, `FormEditor`, `SetFieldScript` | Shows how to use Aspose.Pdf.Facades to add a JavaScript action to the "Quantity" field of a PDF f... |
| [attach-javascript-validation-to-pdf-form-field](./attach-javascript-validation-to-pdf-form-field.cs) | Attach JavaScript Validation to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Shows how to bind a PDF with Aspose.Pdf.Facades.FormEditor, attach a JavaScript validation script... |
| [attach-reset-form-js-clear-hidden-fields](./attach-reset-form-js-clear-hidden-fields.cs) | Attach Reset Form JavaScript that Clears Hidden Fields | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates using Aspose.Pdf Facades FormEditor to bind a PDF, attach JavaScript to a button tha... |
| [batch-add-hidden-processeddate-field](./batch-add-hidden-processeddate-field.cs) | Batch Add Hidden ProcessedDate Field to PDFs | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to iterate through PDF files in a folder and add a hidden text field named "Proc... |
| [batch-add-selectall-checkbox](./batch-add-selectall-checkbox.cs) | Batch Add SelectAll Checkbox to First Page of PDFs | `FormEditor`, `FieldType`, `BindPdf` | Shows how to loop through PDFs in a folder and use Aspose.Pdf.Facades.FormEditor to add a checkbo... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

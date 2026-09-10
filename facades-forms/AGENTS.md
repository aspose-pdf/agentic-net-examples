---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **86** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (81/86 files) ← category-specific
- `using Aspose.Pdf;` (52/86 files) ← category-specific
- `using Aspose.Pdf.Forms;` (27/86 files)
- `using Aspose.Pdf.Annotations;` (7/86 files)
- `using Aspose.Pdf.Text;` (1/86 files)
- `using System;` (86/86 files)
- `using System.IO;` (79/86 files)
- `using System.Collections.Generic;` (8/86 files)
- `using System.Drawing;` (4/86 files)
- `using System.Text.Json;` (3/86 files)
- `using System.Linq;` (2/86 files)
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
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to use Aspose.Pdf.Facades.FormEditor to attach JavaScript that displays a confirmation ... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `FormEditor`, `PdfFileSignature` | Shows how to add a visible signature field to a PDF page and apply a digital signature using Aspo... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `Save` | Demonstrates how to attach a JavaScript action to the "Email" form field that validates the email... |
| [add-email-validation-to-pdf-form-field](./add-email-validation-to-pdf-form-field.cs) | Add Email Validation to PDF Form Field | `Document`, `FormEditor`, `SetFieldScript` | Demonstrates how to attach JavaScript to a PDF form field using Aspose.Pdf to validate email addr... |
| [add-form-load-javascript-to-pdf](./add-form-load-javascript-to-pdf.cs) | Add Form Load JavaScript to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Demonstrates how to attach a document‑level JavaScript action that populates a form field named "... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a radio button group named "Gender" with options Male, Female, and Other, and... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Generates a secure random token, creates a zero‑size hidden TextBoxField named AuthToken, adds it... |
| [add-hidden-version-field-to-pdf](./add-hidden-version-field-to-pdf.cs) | Add Hidden Version Field to PDF | `Document`, `FormEditor`, `BindPdf` | Shows how to insert a hidden numeric field named "Version" with a value of 2 into a PDF using Asp... |
| [add-javascript-alert-to-push-button](./add-javascript-alert-to-push-button.cs) | Add JavaScript Alert to Push Button in PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF with a push‑button field and attach a JavaScript alert that displays wh... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to create a ListBox form field in a PDF, populate it with items, set a default v... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to PDF Dropdown Field | `FormEditor`, `AddListItem`, `Save` | Shows how to add a new option to a combo box (dropdown) form field in a PDF using Aspose.Pdf.Faca... |
| [add-print-button-to-pdf](./add-print-button-to-pdf.cs) | Add Print Button to PDF Using Aspose.Pdf | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a push‑button form field that triggers the PDF print dialog via JavaScript in... |
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add Radio Button Group to PDF Form | `FormEditor`, `AddField`, `Save` | Shows how to create a radio button group named "PaymentMethod" with options "Credit" and "PayPal"... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `FormEditor`, `AddField` | Demonstrates how to insert a push button into a PDF that clears all form fields when clicked, usi... |
| [add-state-combobox-to-pdf](./add-state-combobox-to-pdf.cs) | Add State Combo Box to PDF Form | `FormEditor`, `FieldType`, `AddField` | Shows how to insert a combo box field named "State" into an existing PDF and fill it with US stat... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Shows how to insert a submit button on page 1 of a PDF form that posts the form data to a specifi... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Field to PDF Page | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a text form field named "CustomerName" on page 1 of an existing PDF using Asp... |
| [add-unchecked-checkbox-field-to-pdf](./add-unchecked-checkbox-field-to-pdf.cs) | Add Unchecked Checkbox Field to PDF Page | `Document`, `FormEditor`, `AddField` | Demonstrates how to insert a checkbox form field on a specific PDF page and set its default state... |
| [apply-custom-font-to-pdf-form-text-fields](./apply-custom-font-to-pdf-form-text-fields.cs) | Apply Custom Font to All PDF Form Text Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Demonstrates how to set a custom font (e.g., Arial Bold) for every text field in a PDF form using... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Form Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Shows how to set default visual attributes via FormFieldFacade and apply them to every text field... |
| [apply-phone-number-input-mask](./apply-phone-number-input-mask.cs) | Apply Phone Number Input Mask to PDF Form Field | `FormEditor`, `SetFieldScript`, `Save` | Demonstrates how to attach JavaScript to a PDF form field using Aspose.Pdf to enforce a phone num... |
| [attach-javascript-clear-discountcode-field](./attach-javascript-clear-discountcode-field.cs) | Clear PDF Form Field on Focus with JavaScript | `Document`, `Field`, `JavascriptAction` | Demonstrates how to attach a JavaScript action to a PDF form field using Aspose.Pdf so that the f... |
| [attach-javascript-to-pdf-form-field](./attach-javascript-to-pdf-form-field.cs) | Attach JavaScript to PDF Form Field for Auto-Calculating Tot... | `Document`, `Form`, `FormField` | Shows how to load a PDF, retrieve the Quantity and TotalPrice form fields, attach a JavaScript On... |
| [attach-javascript-validation-to-pdf-form-field](./attach-javascript-validation-to-pdf-form-field.cs) | Attach JavaScript Validation to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Shows how to bind a PDF, add a JavaScript script to the "Age" form field that displays a warning ... |
| [attach-js-resetform-clear-hidden-fields](./attach-js-resetform-clear-hidden-fields.cs) | Attach JavaScript to ResetForm Button to Clear Hidden Fields | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to bind a PDF with Aspose.Pdf.Facades.FormEditor, add JavaScript to a ResetForm button ... |
| [attach-js-validation-to-pdf-submit-button](./attach-js-validation-to-pdf-submit-button.cs) | Attach JavaScript Validation to PDF Submit Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to use Aspose.Pdf.Facades.FormEditor to add a JavaScript script to a PDF submit button ... |
| [batch-add-selectall-checkbox-to-pdf](./batch-add-selectall-checkbox-to-pdf.cs) | Batch Add SelectAll Checkbox to First Page of PDFs | `FormEditor`, `BindPdf`, `AddField` | Shows how to process multiple PDF files in a directory and use Aspose.Pdf.Facades.FormEditor to a... |
| [batch-rename-pdf-form-fields](./batch-rename-pdf-form-fields.cs) | Batch Rename PDF Form Fields | `Form`, `FormEditor`, `RenameField` | Demonstrates how to rename all form fields in a PDF that start with a specific prefix using Aspos... |
| [batch-update-submit-button-urls](./batch-update-submit-button-urls.cs) | Batch Update Submit Button URLs in PDFs | `Form`, `FormEditor`, `FormSubmitButtonNames` | Demonstrates how to iterate through a folder of PDF files, detect submit buttons in each form, an... |
| [clear-and-populate-list-box-field](./clear-and-populate-list-box-field.cs) | Clear and Populate List Box Field in PDF | `Document`, `ListBoxField`, `Options` | Demonstrates how to remove all existing options from a PDF list box (combo) field and then add ne... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

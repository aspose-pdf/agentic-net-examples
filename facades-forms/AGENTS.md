---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **85** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (77/85 files) ← category-specific
- `using Aspose.Pdf;` (53/85 files) ← category-specific
- `using Aspose.Pdf.Forms;` (22/85 files)
- `using Aspose.Pdf.Annotations;` (8/85 files)
- `using Aspose.Pdf.Drawing;` (1/85 files)
- `using Aspose.Pdf.Text;` (1/85 files)
- `using System;` (85/85 files)
- `using System.IO;` (81/85 files)
- `using System.Collections.Generic;` (5/85 files)
- `using System.Drawing;` (3/85 files)
- `using System.Text.Json;` (3/85 files)
- `using System.Linq;` (1/85 files)
- `using System.Security.Cryptography;` (1/85 files)

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
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF with Appearance | `FormEditor`, `AddField`, `PdfFileSignature` | Demonstrates how to add a signature form field named "DigitalSignature" to a PDF using FormEditor... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `Document`, `Form`, `TextBoxField` | Demonstrates how to load a PDF, locate a text box field named "Email", and attach JavaScript that... |
| [add-email-validation-script-to-pdf-form-field](./add-email-validation-script-to-pdf-form-field.cs) | Add Email Validation Script to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to attach a JavaScript regular‑expression validation script to an "Email" form f... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Add Gender Radio Button Group to PDF | `FormEditor`, `AddField`, `Save` | Shows how to insert a radio button group named "Gender" with options Male, Female, and Other into... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to generate a cryptographically secure token and embed it as a hidden text box form fie... |
| [add-hidden-numeric-version-field](./add-hidden-numeric-version-field.cs) | Add Hidden Numeric Version Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to create a hidden numeric text field named "Version" in a PDF using Aspose.Pdf and sav... |
| [add-hidden-sessionid-field-to-pdfs](./add-hidden-sessionid-field-to-pdfs.cs) | Add Hidden SessionId Field to PDFs in Batch | `Form`, `BindPdf`, `Save` | Shows how to loop through PDF files in a folder, bind each with Aspose.Pdf.Facades.Form, create a... |
| [add-input-mask-to-pdf-form-field](./add-input-mask-to-pdf-form-field.cs) | Add Input Mask to PDF Form Field | `Document`, `FormEditor`, `BindPdf` | Shows how to bind a PDF to the FormEditor facade and attach JavaScript that enforces a phone numb... |
| [add-javascript-populate-date-field](./add-javascript-populate-date-field.cs) | Add JavaScript to Populate Date Field on PDF Open | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Demonstrates how to attach a JavaScript action to a PDF that sets the "Date" form field to the cu... |
| [add-javascript-to-quantity-field-total-price](./add-javascript-to-quantity-field-total-price.cs) | Add JavaScript to Quantity Field for Total Price Calculation | `Document`, `FormEditor`, `AddFieldScript` | Shows how to attach a JavaScript action to a PDF form field using Aspose.Pdf so that when the Qua... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field to PDF Form | `FormEditor`, `BindPdf`, `Items` | Shows how to insert a ListBox form field named "Priority" with items Low, Medium, High and a defa... |
| [add-list-item-to-dropdown-field](./add-list-item-to-dropdown-field.cs) | Add List Item to Dropdown Field in PDF | `FormEditor`, `AddListItem`, `Save` | Demonstrates how to use Aspose.Pdf.Facades.FormEditor to add a new option to an existing dropdown... |
| [add-print-dialog-button-to-pdf](./add-print-dialog-button-to-pdf.cs) | Add Print Dialog Button to PDF | `Document`, `FormEditor`, `AddField` | Shows how to insert a push button into a PDF that triggers the browser's print dialog via JavaScr... |
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add Radio Button Group to PDF Form | `FormEditor`, `BindPdf`, `AddField` | Shows how to create a radio button group named "PaymentMethod" with options "Credit" and "PayPal"... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `FormEditor`, `BindPdf` | Shows how to insert a push‑button field that resets all form fields in a PDF using Aspose.Pdf Fac... |
| [add-state-combo-box-to-pdf-form](./add-state-combo-box-to-pdf-form.cs) | Add State Combo Box to PDF Form | `FormEditor`, `AddField`, `AddListItem` | Shows how to insert a combo box field named "State" into an existing PDF form and fill it with US... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Shows how to insert a submit button into a PDF form on page 1 using Aspose.Pdf.Facades.FormEditor... |
| [add-text-field-to-pdf-page](./add-text-field-to-pdf-page.cs) | Add Text Field to PDF Page | `FormEditor`, `BindPdf`, `AddField` | Shows how to insert a text form field named 'CustomerName' on page 1 of a PDF using the Aspose.Pd... |
| [add-unchecked-checkbox-field-to-pdf-page](./add-unchecked-checkbox-field-to-pdf-page.cs) | Add Unchecked Checkbox Field to PDF Page | `Document`, `FormEditor`, `AddField` | Shows how to insert a checkbox form field on a specific PDF page and set its default state to unc... |
| [apply-consistent-decoration-to-checkbox-fields](./apply-consistent-decoration-to-checkbox-fields.cs) | Apply Consistent Decoration to All Checkbox Fields in PDFs | `FormEditor`, `DecorateField`, `FormFieldFacade` | Shows how to batch‑process PDFs in a directory, configure visual attributes with FormFieldFacade,... |
| [apply-custom-font-to-pdf-form-text-fields](./apply-custom-font-to-pdf-form-text-fields.cs) | Apply Custom Font to All PDF Form Text Fields | `Document`, `FormEditor`, `FormFieldFacade` | Demonstrates how to set a custom font (Arial Bold) for every text form field in a PDF using Aspos... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Form Fields | `FormEditor`, `FormFieldFacade`, `DecorateField` | Shows how to configure a FormFieldFacade with visual attributes and use FormEditor to decorate ev... |
| [attach-confirmation-js-to-pdf-submit-button](./attach-confirmation-js-to-pdf-submit-button.cs) | Attach Confirmation JavaScript to PDF Submit Button | `FormEditor`, `BindPdf`, `SetSubmitUrl` | Demonstrates using Aspose.Pdf.Facades.FormEditor to set a submit URL, configure the submit flag, ... |
| [attach-javascript-alert-to-pdf-push-button](./attach-javascript-alert-to-pdf-push-button.cs) | Attach JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `SetFieldScript` | Shows how to use Aspose.Pdf.Facades.FormEditor to set a JavaScript action on a push‑button field ... |
| [attach-javascript-to-resetform-that-also-clears-hi...](./attach-javascript-to-resetform-that-also-clears-hidden-fields-during-reset.cs) | Attach Javascript To Resetform That Also Clears Hidden Field... | `FormEditor` | Attach Javascript To Resetform That Also Clears Hidden Fields During Reset |
| [attach-javascript-validation-to-pdf-form-field](./attach-javascript-validation-to-pdf-form-field.cs) | Attach JavaScript Validation to PDF Form Field | `Document`, `FormEditor`, `SetFieldScript` | Shows how to load a PDF, use the FormEditor facade to attach a JavaScript script to the "Age" for... |
| [attach-js-validation-to-pdf-submit-button](./attach-js-validation-to-pdf-submit-button.cs) | Attach JavaScript Validation to PDF Submit Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to use Aspose.Pdf's FormEditor facade to bind an existing PDF, add a JavaScript script ... |
| [batch-add-hidden-processeddate-field](./batch-add-hidden-processeddate-field.cs) | Batch Add Hidden ProcessedDate Field to PDFs | `FormEditor`, `BindPdf`, `AddField` | Shows how to loop through all PDF files in a folder and add a hidden text field named "ProcessedD... |
| [batch-add-selectall-checkbox-to-pdf](./batch-add-selectall-checkbox-to-pdf.cs) | Batch Add SelectAll Checkbox to First Page of PDFs | `Document`, `FormEditor`, `BindPdf` | Shows how to iterate over PDF files in a folder and add a checkbox form field named "SelectAll" t... |
| [batch-rename-pdf-form-fields](./batch-rename-pdf-form-fields.cs) | Batch Rename PDF Form Fields with Aspose.Pdf | `Form`, `FieldNames`, `RenameField` | Shows how to iterate over PDF form fields and rename any field whose name starts with a given pre... |
| ... | | | *and 55 more files* |

## Category Statistics
- Total examples: 85

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **88** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (82/88 files) ← category-specific
- `using Aspose.Pdf;` (41/88 files)
- `using Aspose.Pdf.Forms;` (21/88 files)
- `using Aspose.Pdf.Annotations;` (8/88 files)
- `using System;` (88/88 files)
- `using System.IO;` (83/88 files)
- `using System.Collections.Generic;` (4/88 files)
- `using System.Drawing;` (4/88 files)
- `using System.Linq;` (2/88 files)
- `using System.Security.Cryptography;` (1/88 files)
- `using System.Text;` (1/88 files)
- `using System.Text.Json;` (1/88 files)
- `using System.Text.RegularExpressions;` (1/88 files)

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
| [add-customername-text-field](./add-customername-text-field.cs) | Add CustomerName Text Field to PDF Page | `FormEditor`, `AddField`, `Save` | Shows how to insert a single‑line text field named "CustomerName" on page 1 of an existing PDF us... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `FormEditor`, `FieldType` | Shows how to add a visible signature field named "DigitalSignature" to a PDF and then sign it usi... |
| [add-email-validation-javascript](./add-email-validation-javascript.cs) | Add Email Validation JavaScript to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Creates a PDF with an Email text field and attaches JavaScript that validates the email format wh... |
| [add-email-validation-script-to-pdf-form-field](./add-email-validation-script-to-pdf-form-field.cs) | Add Email Validation Script to PDF Form Field | `FormEditor`, `SetFieldScript`, `Save` | Demonstrates how to attach a JavaScript validation script to an existing PDF form field to enforc... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `Document`, `FormEditor`, `AddField` | Shows how to create a PDF (if missing) and add a radio button group named "Gender" with options M... |
| [add-header-image-center-align-form-field](./add-header-image-center-align-form-field.cs) | Add Header Image and Center Align a Form Field | `PdfFileStamp`, `FormEditor`, `FormFieldFacade` | Demonstrates how to add a background header image to a PDF and center‑align the text of a specifi... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken Field to PDF | `Form`, `BindPdf`, `Save` | Shows how to generate a cryptographically secure token and embed it as a hidden text field in a P... |
| [add-hidden-sessionid-field-batch](./add-hidden-sessionid-field-batch.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `TextBoxField`, `FieldFlags` | Creates sample PDFs and adds a hidden "SessionId" field with a GUID value to each PDF in a batch. |
| [add-hidden-version-field-to-pdf-form](./add-hidden-version-field-to-pdf-form.cs) | Add Hidden Version Field to PDF Form | `Document`, `Rectangle`, `TextBoxField` | Shows how to insert a hidden numeric text box named "Version" with a value of 2 into an existing ... |
| [add-javascript-to-pdf-form-load](./add-javascript-to-pdf-form-load.cs) | Add JavaScript to PDF Form Load Event | `PdfContentEditor`, `BindPdf`, `AddDocumentAdditionalAction` | Shows how to attach a JavaScript action to a PDF so that the 'Date' form field is automatically f... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to PDF Dropdown Field | `FormEditor`, `AddListItem`, `Save` | Shows how to open a PDF with Aspose.Pdf.Facades.FormEditor, add a new option to an existing dropd... |
| [add-print-button-with-javascript](./add-print-button-with-javascript.cs) | Add Print Button with JavaScript to PDF | `Document`, `FormEditor`, `AddField` | Shows how to insert a push‑button form field into a PDF and attach JavaScript that opens the prin... |
| [add-priority-list-box-to-pdf](./add-priority-list-box-to-pdf.cs) | Add List Box Field 'Priority' to PDF | `FormEditor`, `AddField`, `Save` | Demonstrates how to add a ListBox form field named "Priority" with items Low, Medium, High and se... |
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add Radio Button Group to PDF Form | `Document`, `FormEditor`, `AddField` | Shows how to create a radio button group named "PaymentMethod" with options "Credit" and "PayPal"... |
| [add-reset-form-button-to-pdf](./add-reset-form-button-to-pdf.cs) | Add Reset Form Button to PDF | `Document`, `FormEditor`, `AddField` | Shows how to insert a push button into a PDF that clears all form fields by attaching JavaScript ... |
| [add-selectall-checkbox-to-pdf-first-page](./add-selectall-checkbox-to-pdf-first-page.cs) | Add SelectAll Checkbox to PDF First Page | `Document`, `Page`, `Rectangle` | Iterates over a collection of PDF files, adds an unchecked "SelectAll" checkbox field to the firs... |
| [add-state-combo-box-to-pdf-form](./add-state-combo-box-to-pdf-form.cs) | Add State Combo Box to PDF Form | `FormEditor`, `FieldType`, `AddField` | Shows how to create a combo box field named "State" on a PDF form and fill it with US state abbre... |
| [add-submit-button-confirmation-dialog](./add-submit-button-confirmation-dialog.cs) | Add Submit Button with Confirmation Dialog to PDF | `FormEditor`, `BindPdf`, `AddSubmitBtn` | Demonstrates how to add a submit button to a PDF form and attach JavaScript that shows a confirma... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `FormEditor`, `AddSubmitBtn`, `Save` | Shows how to add a submit button named "SubmitForm" on page 1 of a PDF and configure it to post f... |
| [add-unchecked-checkbox-field-to-pdf-page](./add-unchecked-checkbox-field-to-pdf-page.cs) | Add Unchecked Checkbox Field to PDF Page | `Document`, `FormEditor`, `AddField` | Demonstrates how to insert an unchecked checkbox form field named 'AgreeTerms' on the second page... |
| [apply-custom-font-to-pdf-form-text-fields](./apply-custom-font-to-pdf-form-text-fields.cs) | Apply Custom Font to All PDF Form Text Fields | `FormEditor`, `FormFieldFacade`, `Facade` | Shows how to use Aspose.Pdf.Facades to set the custom font 'Arial Bold' for every text form field... |
| [apply-default-decoration-to-text-fields](./apply-default-decoration-to-text-fields.cs) | Apply Default Decoration to All Text Form Fields | `FormEditor`, `FormFieldFacade`, `FieldType` | Shows how to configure a FormFieldFacade with visual attributes and use FormEditor to apply the d... |
| [attach-javascript-alert-to-pdf-push-button](./attach-javascript-alert-to-pdf-push-button.cs) | Attach JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates using Aspose.Pdf.Facades.FormEditor to bind an existing PDF, add a JavaScript alert ... |
| [attach-javascript-to-pdf-form-field](./attach-javascript-to-pdf-form-field.cs) | Attach JavaScript to PDF Form Field | `Document`, `FormEditor`, `SetFieldScript` | Shows how to load a PDF, use FormEditor to attach a JavaScript action to the 'Quantity' field tha... |
| [attach-javascript-validation-to-age-field](./attach-javascript-validation-to-age-field.cs) | Attach JavaScript Validation to Age Field in PDF | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to attach a JavaScript snippet to a PDF form field using Aspose.Pdf.Facades to w... |
| [attach-javascript-validation-to-pdf-submit-button](./attach-javascript-validation-to-pdf-submit-button.cs) | Attach JavaScript Validation to PDF Submit Button | `FormEditor`, `AddFieldScript`, `SetSubmitUrl` | Shows how to use Aspose.Pdf.Facades.FormEditor to add JavaScript that validates required fields t... |
| [attach-js-clear-field](./attach-js-clear-field.cs) | Attach JavaScript to Clear Text Field on Focus | `Document`, `TextBoxField`, `FormEditor` | Creates a PDF with a text field named DiscountCode and adds JavaScript that clears the field when... |
| [attach-reset-form-js-clear-hidden-fields](./attach-reset-form-js-clear-hidden-fields.cs) | Attach Reset Form JavaScript to Clear Hidden Fields | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to use Aspose.Pdf's FormEditor facade to attach JavaScript to a push‑button that... |
| [batch-add-hidden-processeddate-field](./batch-add-hidden-processeddate-field.cs) | Batch Add Hidden ProcessedDate Field with GUID to PDFs | `Document`, `FormEditor`, `Form` | Iterates through all PDF files in a folder, adds a hidden text form field named "ProcessedDate" o... |
| [batch-rename-acroform-fields](./batch-rename-acroform-fields.cs) | Batch Rename AcroForm Fields from Old_ to New_ | `Form`, `FieldNames`, `RenameField` | Shows how to use the Form facade to iterate over PDF form fields, rename every field whose name s... |
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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

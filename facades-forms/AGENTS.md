---
name: facades-forms
description: C# examples for facades-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-forms

> **Facades forms** in PDF using C# / .NET -- **339** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-forms** category.
This folder contains standalone C# examples for facades-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (278/339 files) ← category-specific
- `using Aspose.Pdf;` (192/339 files) ← category-specific
- `using Aspose.Pdf.Forms;` (77/339 files)
- `using Aspose.Pdf.Annotations;` (46/339 files)
- `using Aspose.Pdf.Text;` (7/339 files)
- `using System;` (339/339 files)
- `using System.IO;` (337/339 files)
- `using System.Drawing;` (37/339 files)
- `using System.Linq;` (33/339 files)
- `using System.Collections.Generic;` (18/339 files)
- `using System.Data;` (11/339 files)
- `using System.Text.Json;` (6/339 files)
- `using System.Globalization;` (1/339 files)
- `using System.Security.Cryptography;` (1/339 files)
- `using System.Text;` (1/339 files)
- `using System.Xml.Linq;` (1/339 files)

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
| [add-border-to-pdf-signature-field](./add-border-to-pdf-signature-field.cs) | Add Border and Decoration to PDF Signature Field | `FormEditor`, `FormFieldFacade`, `BindPdf` | Shows how to bind a PDF with Aspose.Pdf.Facades.FormEditor, configure a FormFieldFacade to increa... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to Submit Button in PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Demonstrates locating a button field in a PDF form and attaching a JavaScript action that shows a... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF | `Document`, `SignatureField`, `Rectangle` | Shows how to create a signature field named "DigitalSignature" in a PDF and then sign the documen... |
| [add-disclaimer-field-to-pdf](./add-disclaimer-field-to-pdf.cs) | Add Disclaimer TextBox Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to load a PDF with Aspose.Pdf, create a TextBoxField at the top of the first page, set ... |
| [add-email-text-field-to-pdf-page](./add-email-text-field-to-pdf-page.cs) | Add Email Address Text Field to PDF Page with FormEditor | `FormEditor`, `BindPdf`, `AddField` | Shows how to bind an existing PDF, add a text form field named EmailAddress on page 2 at specific... |
| [add-email-validation-javascript-to-pdf-form-field](./add-email-validation-javascript-to-pdf-form-field.cs) | Add Email Validation JavaScript to PDF Form Field | `FormEditor`, `BindPdf`, `AddFieldScript` | Demonstrates how to attach a JavaScript blur‑event script to an "Email" form field in a PDF using... |
| [add-email-validation-to-pdf-form-field](./add-email-validation-to-pdf-form-field.cs) | Add Email Regex Validation to PDF Form Field | `FormEditor`, `BindPdf`, `SetFieldScript` | Demonstrates how to apply a JavaScript regular‑expression validation script to an "Email" form fi... |
| [add-footer-text-field-to-pdf-pages](./add-footer-text-field-to-pdf-pages.cs) | Add Footer Text Field to Each PDF Page | `Document`, `FormEditor`, `BindPdf` | Demonstrates how to bind an existing PDF with Aspose.Pdf.Facades.FormEditor, add a single‑line fo... |
| [add-form-load-javascript-to-pdf](./add-form-load-javascript-to-pdf.cs) | Add JavaScript to Populate Date Field on PDF Open | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript action to a PDF document using Aspose.Pdf so that the "Da... |
| [add-gender-radio-button-group](./add-gender-radio-button-group.cs) | Add Gender Radio Button Group to PDF Form | `Document`, `Page`, `RadioButtonField` | Shows how to create a 'Gender' radio button group with Male, Female, and Other options, set a def... |
| [add-hidden-authtoken-field-to-pdf](./add-hidden-authtoken-field-to-pdf.cs) | Add Hidden AuthToken to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to generate a secure random token and embed it as invisible text on every page of a PDF... |
| [add-hidden-sessionid-field-to-pdfs-batch](./add-hidden-sessionid-field-to-pdfs-batch.cs) | Add Hidden SessionId Field to PDFs in Batch | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to iterate over a folder of PDF files, generate a GUID for each, and add a hidde... |
| [add-hidden-version-field-to-pdf](./add-hidden-version-field-to-pdf.cs) | Add Hidden Numeric Field to PDF Form | `Document`, `NumberField`, `Rectangle` | Shows how to create a hidden numeric form field named "Version" with a value of 2 in a PDF docume... |
| [add-javascript-alert-to-push-button](./add-javascript-alert-to-push-button.cs) | Attach JavaScript Alert to PDF Push Button | `FormEditor`, `BindPdf`, `AddFieldScript` | Shows how to bind an existing PDF, add a JavaScript alert to a push button named "ShowInfo", and ... |
| [add-list-box-field-to-pdf-form](./add-list-box-field-to-pdf-form.cs) | Add List Box Field with Default Selection to PDF | `Document`, `FormEditor`, `AddField` | Demonstrates how to create a ListBox form field in a PDF, populate it with items, and set a defau... |
| [add-list-box-to-pdf-using-formeditor](./add-list-box-to-pdf-using-formeditor.cs) | Add List Box to PDF Using FormEditor | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to bind an existing PDF, set list items, add a ListBox field on a specific page,... |
| [add-list-item-to-pdf-dropdown](./add-list-item-to-pdf-dropdown.cs) | Add List Item to Dropdown Field in PDF | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to use Aspose.Pdf.Facades.FormEditor to add a new option to an existing dropdown... |
| [add-list-item-to-pdf-form-field](./add-list-item-to-pdf-form-field.cs) | Add List Item to PDF Form Field | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to bind a PDF document with Aspose.Pdf.Facades.FormEditor, add a new item to a l... |
| [add-list-item-to-pdf-form-field__v2](./add-list-item-to-pdf-form-field__v2.cs) | Add List Item to PDF Form Field | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to bind a PDF to the FormEditor, add a new item to a list-type form field, and s... |
| [add-list-item-to-pdf-form-field__v3](./add-list-item-to-pdf-form-field__v3.cs) | Add List Item to PDF Form Field and Apply Decoration | `FormEditor`, `FormFieldFacade`, `BindPdf` | Shows how to open a PDF with FormEditor, add a new list item "Extra" to an existing list/combobox... |
| [add-list-item-to-pdf-form-listbox](./add-list-item-to-pdf-form-listbox.cs) | Add List Item to PDF Form ListBox | `FormEditor`, `BindPdf`, `AddListItem` | Shows how to open an existing PDF, bind a FormEditor, add a new list item to a ListBox form field... |
| [add-list-item-to-pdf-form-listbox__v2](./add-list-item-to-pdf-form-listbox__v2.cs) | Add List Item to PDF Form ListBox | `Document`, `FormEditor`, `BindPdf` | Shows how to open a PDF, bind a FormEditor, add a new list item to a ListBox form field, and save... |
| [add-list-item-to-pdf-form-listbox__v3](./add-list-item-to-pdf-form-listbox__v3.cs) | Add List Item to PDF Form ListBox | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates loading a PDF, binding a FormEditor, adding a new item to a ListBox form field, and ... |
| [add-list-item-to-pdf-form](./add-list-item-to-pdf-form.cs) | Add List Item to PDF Form and Set Default Selection | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates binding a PDF with FormEditor, adding a new list item to a form field, then using Fo... |
| [add-list-item-to-pdf-list-box](./add-list-item-to-pdf-list-box.cs) | Add List Item to PDF List Box using FormEditor | `FormEditor`, `BindPdf`, `AddListItem` | Shows how to bind an existing PDF, add a new item (Germany) to a list‑box field named CountryList... |
| [add-list-item-to-pdf-listbox](./add-list-item-to-pdf-listbox.cs) | Add List Item to PDF ListBox using FormEditor | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates how to bind a PDF with FormEditor, add a new option to a ListBox form field, and sav... |
| [add-list-item-to-pdf-listbox__v2](./add-list-item-to-pdf-listbox__v2.cs) | Add List Item to PDF ListBox using FormEditor | `FormEditor`, `BindPdf`, `AddListItem` | Shows how to bind an existing PDF, add a new entry to a ListBox form field, and save the modified... |
| [add-list-item-to-pdf-listbox__v3](./add-list-item-to-pdf-listbox__v3.cs) | Add List Item to PDF ListBox with FormEditor | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates loading a PDF form, binding it with FormEditor, adding a new list item "Morning" to ... |
| [add-list-items-to-listbox-in-pdf-form](./add-list-items-to-listbox-in-pdf-form.cs) | Add List Items to a ListBox and Set Default Value in PDF For... | `FormEditor`, `BindPdf`, `AddListItem` | Demonstrates binding a PDF, adding items to a ListBox field with FormEditor, setting a default se... |
| [add-list-items-to-pdf-form-listbox](./add-list-items-to-pdf-form-listbox.cs) | Add List Items to a PDF Form ListBox | `FormEditor`, `BindPdf`, `AddListItem` | Shows how to bind an existing PDF, add "Morning" and "Afternoon" entries to a ListBox form field ... |
| ... | | | *and 309 more files* |

## Category Statistics
- Total examples: 339

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
Updated: 2026-11-27 | Run: `20261127_220449_128ac6`
<!-- AUTOGENERATED:END -->

---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (233/233 files) ← category-specific
- `using Aspose.Pdf.Forms;` (189/233 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (72/233 files)
- `using Aspose.Pdf.Text;` (19/233 files)
- `using Aspose.Pdf.Drawing;` (11/233 files)
- `using Aspose.Pdf.Facades;` (4/233 files)
- `using Aspose.Pdf.Comparison;` (1/233 files)
- `using Aspose.Pdf.Security;` (1/233 files)
- `using System;` (233/233 files)
- `using System.IO;` (205/233 files)
- `using System.Xml;` (19/233 files)
- `using System.Collections.Generic;` (13/233 files)
- `using System.Drawing;` (7/233 files)
- `using System.Text;` (6/233 files)
- `using System.Linq;` (4/233 files)
- `using System.Threading.Tasks;` (4/233 files)
- `using System.Net.Http;` (3/233 files)
- `using System.IO.Compression;` (2/233 files)
- `using System.Runtime.InteropServices;` (2/233 files)
- `using System.Xml.Linq;` (2/233 files)
- `using System.Xml.Xsl;` (2/233 files)
- `using System.Data;` (1/233 files)
- `using System.Net.Http.Headers;` (1/233 files)
- `using System.Security.Cryptography;` (1/233 files)
- `using System.Text.Json;` (1/233 files)
- `using System.Text.RegularExpressions;` (1/233 files)
- `using System.Threading;` (1/233 files)
- `using System.Xml.Schema;` (1/233 files)

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
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Pages`, `PageCollection` | Shows how to load an existing PDF form, append a blank page while preserving all fields, and save... |
| [add-calculated-total-field-to-pdf-form](./add-calculated-total-field-to-pdf-form.cs) | Add Calculated Total Field to PDF Form | `Document`, `Form`, `NumberField` | Demonstrates how to add Quantity and Unit Price fields to a PDF form and use a read‑only Total fi... |
| [add-calculated-total-field-using-javascript](./add-calculated-total-field-using-javascript.cs) | Add Calculated Total Field Using JavaScript in PDF Form | `Document`, `NumberField`, `TextBoxField` | Demonstrates how to add numeric fields to a PDF form and create a read‑only total field that auto... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `CheckboxField`, `Rectangle` | Shows how to load an existing PDF, create a CheckboxField, add it to the document's form, and sav... |
| [add-country-dropdown-to-pdf](./add-country-dropdown-to-pdf.cs) | Add Country ComboBox Dropdown to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF form with a ComboBox field named 'Country' and populate it with ... |
| [add-current-date-field-to-pdf](./add-current-date-field-to-pdf.cs) | Add Current Date Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF, add a DateField form element, set a custom date format, assign ... |
| [add-date-field-with-validation](./add-date-field-with-validation.cs) | Add Date Field with Validation to PDF | `Document`, `Page`, `Rectangle` | Creates a PDF containing a date picker form field and attaches JavaScript that prevents users fro... |
| [add-date-picker-field-default-current-date](./add-date-picker-field-default-current-date.cs) | Add Date Picker Field with Current Date Default | `Document`, `Page`, `Rectangle` | Demonstrates how to add a date picker form field to a PDF using Aspose.Pdf, set its default value... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF | `Document`, `DateField`, `Rectangle` | Shows how to create a DateField on a PDF page, attach JavaScript to open the calendar widget when... |
| [add-dynamic-date-time-field-to-pdf](./add-dynamic-date-time-field-to-pdf.cs) | Add Dynamic Date/Time Field to PDF | `Document`, `Rectangle`, `DateField` | Demonstrates how to insert a DateField into an existing PDF, set its format to show the current d... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Add Gender Radio Button Group to PDF Form | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a PDF document with an AcroForm and add a gender radio button group us... |
| [add-hidden-creation-timestamp-field](./add-hidden-creation-timestamp-field.cs) | Add Hidden Creation Timestamp Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a hidden DateField into a PDF using Aspose.Pdf to store the document c... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF with JavaScript | `Document`, `TextBoxField`, `AnnotationFlags` | Shows how to create a hidden text box form field in a PDF and populate it with a client IP addres... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session ID Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a hidden text box form field into an existing PDF to store a session i... |
| [add-javascript-email-validation-to-pdf-form-field](./add-javascript-email-validation-to-pdf-form-field.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `TextBoxField`, `Border` | Demonstrates how to create a text box form field in a PDF and attach a JavaScript OnValidate acti... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Page`, `Rectangle` | Shows how to attach a JavaScript action to a PDF form field with Aspose.Pdf so the script runs wh... |
| [add-javascript-onlostfocus-action-to-pdf-form-fiel...](./add-javascript-onlostfocus-action-to-pdf-form-field.cs) | Add JavaScript OnLostFocus Action to PDF Form Field | `Document`, `Form`, `NumberField` | Demonstrates creating or retrieving a NumberField in a PDF form and assigning a JavaScript action... |
| [add-locale-javascript-to-pdf](./add-locale-javascript-to-pdf.cs) | Add Locale-Based JavaScript to PDF Form | `Document`, `JavaScript`, `JavascriptAction` | Shows how to embed a JavaScript dictionary for translations in a PDF, update a form field label b... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Add Multiline Feedback Text Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to add a multiline TextBoxField named 'Feedback' with a 500‑character limit to a... |
| [add-new-page-with-signature-acroform-field](./add-new-page-with-signature-acroform-field.cs) | Add New Page with Signature AcroForm Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to append a blank page to an existing PDF and place a signature AcroForm field o... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Add PaymentMethod Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a radio button group named 'PaymentMethod' with 'Credit' and 'Debit' o... |
| [add-post-submit-button-to-pdf-form](./add-post-submit-button-to-pdf-form.cs) | Add POST Submit Button to PDF Form | `Document`, `ButtonField`, `SubmitFormAction` | Demonstrates how to create a push button in a PDF that submits form data using the HTTP POST meth... |
| [add-progress-bar-to-pdf-form](./add-progress-bar-to-pdf-form.cs) | Add Progress Bar to PDF Form Using JavaScript | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to insert a TextBox field that acts as a progress bar in an existing PDF form an... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `Form`, `ButtonField` | Shows how to create a push button on a PDF form that executes JavaScript to clear all user‑entere... |
| [add-reset-button-to-pdf-form__v2](./add-reset-button-to-pdf-form__v2.cs) | Add Reset Button to PDF Form | `Document`, `Form`, `ButtonField` | Shows how to insert a push button into a PDF that clears all form fields by executing JavaScript ... |
| [add-rich-text-box-field-with-html-formatting](./add-rich-text-box-field-with-html-formatting.cs) | Add Rich Text Box Field with HTML Formatting to PDF | `Document`, `RichTextBoxField`, `Rectangle` | Demonstrates how to create a RichTextBoxField in a PDF and set its FormattedValue using HTML mark... |
| [add-signature-field-to-pdf-form](./add-signature-field-to-pdf-form.cs) | Add Signature Field to PDF Form | `Document`, `Rectangle`, `SignatureField` | Shows how to load an existing PDF, create a signature form field, add it to the document's form, ... |
| [add-signature-field-with-image-stamp](./add-signature-field-with-image-stamp.cs) | Add Signature Field with Image Stamp to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a signature field named 'ClientSignature' into a PDF and set its visual appea... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to create a push button in an existing PDF that posts all form field data to a specifie... |
| [add-submit-button-with-url-action](./add-submit-button-with-url-action.cs) | Add Submit Button with URL Action to PDF | `Document`, `Rectangle`, `ButtonField` | Demonstrates how to create a push button in a PDF form, configure its SubmitForm action, and set ... |
| ... | | | *and 203 more files* |

## Category Statistics
- Total examples: 233

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Facades.Form`
- `Aspose.Pdf.Facades.Form.IsRequiredField`
- `Aspose.Pdf.Facades.FormEditor`
- `Aspose.Pdf.Form`
- `Aspose.Pdf.Form.Delete`
- `Aspose.Pdf.Forms.ComboBoxField`
- `Aspose.Pdf.Forms.ComboBoxField.AddOption`
- `Aspose.Pdf.Forms.Field`
- `Aspose.Pdf.Forms.Form`
- `Aspose.Pdf.Forms.Form.Add`
- `Aspose.Pdf.Forms.FormType`
- `Aspose.Pdf.Forms.TextBoxField`
- `Aspose.Pdf.Page`

### Rules
- Bind a PDF document to a FormEditor using BindPdf({input_pdf}) before performing any form modifications.
- Assign a submit URL to a button field with SetSubmitUrl({field_name}, {url}), where {field_name} is the name of the button and {url} is the target URL.
- Persist the changes by calling Save({output_pdf}) after all form updates are completed.
- Load a PDF document: Document {doc} = new Document({input_pdf});
- Set a tooltip for a form field: (({doc}.Form[{field_name}] as Field).AlternateName = {tooltip_text});

### Warnings
- SetSubmitUrl only works for button fields that are configured as submit buttons; applying it to other field types will have no effect.
- AlternateName is used as the tooltip; its visibility depends on the PDF viewer.
- The field must be cast to Aspose.Pdf.Forms.Field to access the AlternateName property.
- Arabic text may require a font that supports Arabic glyphs; the example does not explicitly set a font, which could affect rendering in some viewers.
- Page indexing in Aspose.Pdf is 1‑based; ensure the page exists before referencing it.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

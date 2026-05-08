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

- `using Aspose.Pdf;` (240/241 files) ← category-specific
- `using Aspose.Pdf.Forms;` (194/241 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (81/241 files)
- `using Aspose.Pdf.Text;` (19/241 files)
- `using Aspose.Pdf.Drawing;` (14/241 files)
- `using Aspose.Pdf.Facades;` (3/241 files)
- `using Aspose.Pdf.Comparison;` (1/241 files)
- `using System;` (241/241 files)
- `using System.IO;` (218/241 files)
- `using System.Xml;` (18/241 files)
- `using System.Collections.Generic;` (13/241 files)
- `using System.Text;` (8/241 files)
- `using System.Drawing;` (5/241 files)
- `using System.Xml.Linq;` (5/241 files)
- `using System.Linq;` (4/241 files)
- `using System.Net.Http;` (4/241 files)
- `using System.Text.Json;` (4/241 files)
- `using System.Threading.Tasks;` (4/241 files)
- `using System.Data;` (3/241 files)
- `using System.IO.Compression;` (2/241 files)
- `using System.Text.RegularExpressions;` (2/241 files)
- `using System.Threading;` (2/241 files)
- `using System.Xml.Xsl;` (2/241 files)
- `using NUnit.Framework;` (1/241 files)
- `using System.Globalization;` (1/241 files)
- `using System.Net;` (1/241 files)
- `using System.Net.Http.Headers;` (1/241 files)
- `using System.Security.Cryptography;` (1/241 files)
- `using System.Xml.Schema;` (1/241 files)

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
| [acroform-textfield-open-javascript](./acroform-textfield-open-javascript.cs) | Create AcroForm Text Field with Document Open JavaScript | `Document`, `Page`, `Rectangle` | Demonstrates how to generate a PDF, add an AcroForm text box field, and assign a document‑level J... |
| [add-blank-page-save-modified-pdf](./add-blank-page-save-modified-pdf.cs) | Add Blank Page and Save Modified PDF | `Document`, `Save`, `Pages` | Loads an existing PDF, adds a blank page, and saves the modified document to a new file, preservi... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Page`, `PageInfo` | Shows how to load an existing PDF form, insert a new blank page while keeping existing form field... |
| [add-calculated-total-field-javascript](./add-calculated-total-field-javascript.cs) | Add Calculated Total Field Using JavaScript | `Document`, `NumberField`, `Rectangle` | Demonstrates how to create numeric form fields in a PDF and use a JavaScript action to calculate ... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `Rectangle`, `CheckboxField` | Shows how to load an existing PDF, create a CheckboxField named 'AgreeTerms', add it to the docum... |
| [add-current-date-time-field-to-pdf](./add-current-date-time-field-to-pdf.cs) | Add Current Date and Time Field to PDF | `Document`, `DateField`, `Rectangle` | Demonstrates how to insert a DateField into a PDF form that automatically shows the current date ... |
| [add-date-picker-field-default-current-date](./add-date-picker-field-default-current-date.cs) | Add Date Picker Field with Default Current Date | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF form with a date picker field using Aspose.Pdf, set its default ... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF | `Document`, `Form`, `Page` | Shows how to create a DateField on a PDF page, set its display format, and attach a JavaScript ac... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Add Dynamic Barcode Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a barcode form field with a runtime‑generated value and insert it into a PDF ... |
| [add-formatted-currency-field-to-pdf](./add-formatted-currency-field-to-pdf.cs) | Add Formatted Currency Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF with an interactive number field formatted as currency, includin... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Add Gender Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Shows how to create a new PDF document with an AcroForm and add a radio button field for selectin... |
| [add-hidden-creation-timestamp-field](./add-hidden-creation-timestamp-field.cs) | Add Hidden Creation Timestamp Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a hidden, read‑only DateField into a PDF to store the document creatio... |
| [add-hidden-ip-address-field-to-pdf](./add-hidden-ip-address-field-to-pdf.cs) | Add Hidden IP Address Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to create a hidden text box field in a PDF and fill it with the user's IP address using... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session ID Field to PDF | `Document`, `TextBoxField`, `AnnotationFlags` | Shows how to insert a hidden text box form field into an existing PDF to store a session identifi... |
| [add-javascript-email-validation-to-pdf-form](./add-javascript-email-validation-to-pdf-form.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Demonstrates how to load a PDF, locate an existing "Email" form field, attach JavaScript that val... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field that runs when the field's value chan... |
| [add-list-box-to-pdf-acroform](./add-list-box-to-pdf-acroform.cs) | Add List Box to PDF AcroForm | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF document, add an AcroForm list box field, populate it with count... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Add Multiline Feedback Text Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Shows how to insert a multiline TextBoxField named 'Feedback' with a 500‑character limit into an ... |
| [add-new-page-with-acroform-fields](./add-new-page-with-acroform-fields.cs) | Add New Page with AcroForm Fields to PDF | `Document`, `Page`, `TextBoxField` | Shows how to insert a blank page into an existing PDF and place fresh AcroForm fields (text box, ... |
| [add-numeric-field-range-validation](./add-numeric-field-range-validation.cs) | Add Numeric Field with Range Validation to PDF | `Document`, `NumberField`, `JavascriptAction` | Creates a PDF form, adds a NumberField, and attaches JavaScript that validates the entered value ... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Add PaymentMethod Radio Button Group to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF form with a radio button group named 'PaymentMethod' containing 'Cred... |
| [add-progress-bar-field-to-pdf-form](./add-progress-bar-field-to-pdf-form.cs) | Add Progress Bar Field to PDF Form | `Document`, `Form`, `TextBoxField` | Shows how to insert a read‑only TextBoxField that serves as a visual progress bar in an existing ... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `Rectangle` | Shows how to insert a push‑button into an existing PDF form that clears all user‑entered data by ... |
| [add-reset-button-to-pdf-form__v2](./add-reset-button-to-pdf-form__v2.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to insert a button field into an existing PDF that clears all user‑entered data by exec... |
| [add-rich-text-box-field-with-html](./add-rich-text-box-field-with-html.cs) | Add Rich Text Box Field with HTML Formatting to PDF | `Document`, `Rectangle`, `RichTextBoxField` | Demonstrates how to insert a RichTextBoxField into a PDF and set its formatted value using HTML m... |
| [add-signature-field-to-pdf-form](./add-signature-field-to-pdf-form.cs) | Add Signature Field to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to insert a digital signature field into an existing PDF document using Aspose.Pdf and ... |
| [add-signature-field-with-image-stamp](./add-signature-field-with-image-stamp.cs) | Add Signature Field with Image Stamp to PDF | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to create a signature form field named 'ClientSignature' and set its visual appe... |
| [add-submit-button-http-post-urlencoded](./add-submit-button-http-post-urlencoded.cs) | Add Submit Button with HTTP POST and URL‑Encoded Form Data | `Document`, `Page`, `Rectangle` | Demonstrates how to place a push‑button on a PDF that submits the form via HTTP POST using applic... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `Document`, `ButtonField`, `SubmitFormAction` | Shows how to create a push‑button on an existing PDF form and configure it to post the form data ... |
| [add-submit-button-with-url-action](./add-submit-button-with-url-action.cs) | Add Submit Button with URL Action to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to programmatically add a push‑button to a PDF and configure its OnActivated act... |
| ... | | | *and 211 more files* |

## Category Statistics
- Total examples: 241

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

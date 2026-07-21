---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

> **Working with forms** in PDF using C# / .NET -- **230** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (227/230 files) ← category-specific
- `using Aspose.Pdf.Forms;` (181/230 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (74/230 files)
- `using Aspose.Pdf.Drawing;` (10/230 files)
- `using Aspose.Pdf.Text;` (9/230 files)
- `using Aspose.Pdf.Facades;` (2/230 files)
- `using Aspose.Pdf.Comparison;` (1/230 files)
- `using System;` (230/230 files)
- `using System.IO;` (203/230 files)
- `using System.Xml;` (17/230 files)
- `using System.Collections.Generic;` (15/230 files)
- `using System.Linq;` (6/230 files)
- `using System.Text;` (5/230 files)
- `using System.Threading.Tasks;` (5/230 files)
- `using System.Drawing;` (4/230 files)
- `using System.Xml.Linq;` (4/230 files)
- `using System.IO.Compression;` (2/230 files)
- `using System.Net.Http;` (2/230 files)
- `using Newtonsoft.Json;` (1/230 files)
- `using Newtonsoft.Json.Linq;` (1/230 files)
- `using System.Reflection;` (1/230 files)
- `using System.Security.Cryptography;` (1/230 files)
- `using System.Text.Json;` (1/230 files)
- `using System.Threading;` (1/230 files)
- `using System.Xml.Schema;` (1/230 files)
- `using System.Xml.Xsl;` (1/230 files)

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
| [add-acroform-textbox-absolute-coordinates](./add-acroform-textbox-absolute-coordinates.cs) | Add AcroForm TextBox at Absolute Coordinates | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF with an AcroForm text box field positioned using absolute coordinates... |
| [add-auto-updating-date-time-field-to-pdf](./add-auto-updating-date-time-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `DateField`, `JavascriptAction` | Shows how to insert a DateField into a PDF and attach JavaScript so the field automatically displ... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Pages`, `Add` | Shows how to load an existing PDF form, append a new blank page, and save the document while pres... |
| [add-calculated-total-field-to-pdf-form](./add-calculated-total-field-to-pdf-form.cs) | Add Calculated Total Field to PDF Form | `Document`, `Form`, `NumberField` | Demonstrates how to add numeric fields to a PDF form and use JavaScript to calculate a running to... |
| [add-calculation-button-to-pdf-form](./add-calculation-button-to-pdf-form.cs) | Add Calculation Button to PDF Form | `Document`, `TextBoxField`, `ButtonField` | Shows how to create quantity, unit price, and total fields in a PDF and attach JavaScript to a bu... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to load an existing PDF, create a CheckboxField named 'AgreeTerms', add it to the docum... |
| [add-date-picker-field-current-date](./add-date-picker-field-current-date.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a date picker form field into an existing PDF and set its default value to th... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF with JavaScript | `Document`, `Form`, `DateField` | Shows how to insert a DateField into a PDF, configure its display format, and attach a JavaScript... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Add Dynamic Barcode Field to PDF | `Document`, `Rectangle`, `BarcodeField` | Shows how to generate a unique value at runtime and insert it as a Code128 barcode field into a P... |
| [add-email-validation-to-pdf-form](./add-email-validation-to-pdf-form.cs) | Add JavaScript Email Validation to a PDF Form Field | `Document`, `Page`, `Rectangle` | Loads an existing PDF, creates a TextBox form field for an email address, attaches a JavaScript v... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Field to PDF with JavaScript | `Document`, `TextBoxField`, `Rectangle` | Shows how to create a hidden text box form field in a PDF and set its value to the user's IP addr... |
| [add-hidden-session-identifier-field-to-pdf](./add-hidden-session-identifier-field-to-pdf.cs) | Add Hidden Session Identifier Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a hidden, read‑only text box form field into a PDF to store a session ... |
| [add-ipv4-validation-to-pdf-textbox](./add-ipv4-validation-to-pdf-textbox.cs) | Add IPv4 Validation to PDF TextBox Field | `Document`, `Page`, `Rectangle` | Shows how to create a PDF with a text box form field and attach JavaScript that validates the ent... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field that fires when the field's value cha... |
| [add-locale-translation-to-pdf-form-label](./add-locale-translation-to-pdf-form-label.cs) | Add Locale‑Based Translation to PDF Form Labels with JavaScr... | `Document`, `Page`, `TextBoxField` | Demonstrates how to create a PDF with form fields and use a document‑level JavaScript dictionary ... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Add Multiline Feedback Text Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document with a multiline text box form field named 'Feedback' and enfo... |
| [add-new-page-with-acroform-fields](./add-new-page-with-acroform-fields.cs) | Add New Page with AcroForm Fields to PDF | `Document`, `Page`, `Rectangle` | Shows how to append a blank page to an existing PDF and place fresh AcroForm text box and checkbo... |
| [add-numeric-field-range-validation](./add-numeric-field-range-validation.cs) | Add Numeric Field with Range Validation to PDF | `Document`, `NumberField`, `Rectangle` | Demonstrates creating a NumberField in a PDF, restricting input characters, and attaching JavaScr... |
| [add-payment-method-radio-button-group](./add-payment-method-radio-button-group.cs) | Add Payment Method Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a PDF form with a radio button group named 'PaymentMethod' containing ... |
| [add-progress-bar-to-pdf-form](./add-progress-bar-to-pdf-form.cs) | Add Progress Bar to PDF Form | `Document`, `ButtonField`, `TextBoxField` | Shows how to create a progress bar field in a PDF form and update its value with JavaScript as th... |
| [add-readonly-date-field-to-pdf](./add-readonly-date-field-to-pdf.cs) | Add Read‑Only Date Field to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to create a DateField in a PDF, set its initial value, and mark it as read‑only using A... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `Form`, `ButtonField` | Shows how to insert a push‑button into an existing PDF that clears all user‑entered form data by ... |
| [add-reset-button-to-pdf-form__v2](./add-reset-button-to-pdf-form__v2.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to place a push button on an existing PDF form that clears all user‑entered data by exe... |
| [add-signature-field-to-pdf-form](./add-signature-field-to-pdf-form.cs) | Add Signature Field to PDF Form | `Document`, `Form`, `Rectangle` | Shows how to insert a signature field into an existing PDF document using Aspose.Pdf so users can... |
| [add-signature-field-with-image-stamp](./add-signature-field-with-image-stamp.cs) | Add Signature Field with Image Stamp to PDF | `Document`, `SignatureField`, `ImageStamp` | Shows how to create a signature form field named 'ClientSignature' in a PDF and set its visual ap... |
| [add-submit-button-http-post-pdf](./add-submit-button-http-post-pdf.cs) | Add Submit Button with HTTP POST to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a push button in a PDF that submits form data using the HTTP POST meth... |
| [add-submit-button-post-form-data-xfdf](./add-submit-button-post-form-data-xfdf.cs) | Add Submit Button to PDF to Post Form Data as XFDF | `Document`, `ButtonField`, `SubmitFormAction` | Demonstrates how to create a submit button in a PDF form that posts the form data to a remote XML... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `Document`, `Form`, `ButtonField` | Shows how to create a push button on a PDF form that posts all form field data to a specified URL... |
| [add-submit-button-to-pdf-form__v2](./add-submit-button-to-pdf-form__v2.cs) | Add Submit Button to PDF Form | `Document`, `Form`, `ButtonField` | Shows how to create a push button in a PDF form that posts the form data to a specified URL using... |
| [add-submit-button-to-pdf](./add-submit-button-to-pdf.cs) | Add Submit Button with URL Action to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to programmatically create a push button on a PDF page and assign a SubmitFormAc... |
| ... | | | *and 200 more files* |

## Category Statistics
- Total examples: 230

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

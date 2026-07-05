---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

> **Working with forms** in PDF using C# / .NET -- **239** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (237/239 files) ← category-specific
- `using Aspose.Pdf.Forms;` (184/239 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (76/239 files)
- `using Aspose.Pdf.Text;` (19/239 files)
- `using Aspose.Pdf.Drawing;` (18/239 files)
- `using Aspose.Pdf.Facades;` (4/239 files)
- `using System;` (239/239 files)
- `using System.IO;` (216/239 files)
- `using System.Xml;` (19/239 files)
- `using System.Collections.Generic;` (18/239 files)
- `using System.Drawing;` (7/239 files)
- `using System.Text;` (6/239 files)
- `using System.Xml.Linq;` (6/239 files)
- `using System.Threading.Tasks;` (4/239 files)
- `using System.Linq;` (3/239 files)
- `using System.Net.Http;` (3/239 files)
- `using System.Data;` (2/239 files)
- `using System.IO.Compression;` (2/239 files)
- `using System.Net;` (2/239 files)
- `using System.Text.Json;` (2/239 files)
- `using System.Threading;` (2/239 files)
- `using System.Xml.Xsl;` (2/239 files)
- `using NUnit.Framework;` (1/239 files)
- `using System.Data.SqlClient;` (1/239 files)
- `using System.Drawing.Imaging;` (1/239 files)
- `using System.Globalization;` (1/239 files)
- `using System.Net.Http.Headers;` (1/239 files)
- `using System.Reflection;` (1/239 files)
- `using System.Security.Cryptography;` (1/239 files)
- `using System.Text.Json.Nodes;` (1/239 files)
- `using System.Xml.Schema;` (1/239 files)

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
| [add-auto-updating-date-time-field-to-pdf](./add-auto-updating-date-time-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `Rectangle`, `DateField` | Shows how to insert a DateField into a PDF and attach JavaScript so the field displays the curren... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Page`, `PageInfo` | Shows how to load an existing PDF form, append a new blank page, and save the document while pres... |
| [add-calculated-sum-field-to-pdf-form](./add-calculated-sum-field-to-pdf-form.cs) | Add Calculated Sum Field to PDF Form Using JavaScript | `Document`, `Form`, `NumberField` | Demonstrates creating numeric form fields and a calculated field that sums them with JavaScript i... |
| [add-calculated-total-field-to-pdf-form](./add-calculated-total-field-to-pdf-form.cs) | Add Calculated Total Field to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to create a PDF with Quantity and Unit Price number fields and a read‑only Total field ... |
| [add-calculation-button-to-pdf-form](./add-calculation-button-to-pdf-form.cs) | Add Calculation Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to insert a push‑button into an existing PDF form and attach JavaScript that reads Quan... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `Rectangle`, `CheckboxField` | Shows how to load an existing PDF, create a CheckboxField named 'AgreeTerms', add it to the docum... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF with JavaScript | `Document`, `Rectangle`, `DateField` | Shows how to create a DateField on an existing PDF, set its display format, and attach JavaScript... |
| [add-date-picker-field-with-current-date](./add-date-picker-field-with-current-date.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Rectangle`, `DateField` | Shows how to insert a date picker form field into an existing PDF and set its default value to th... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Add Dynamic Barcode Field to PDF | `Document`, `BarcodeField`, `Rectangle` | Demonstrates loading a PDF, creating a barcode form field with a runtime‑generated GUID, and savi... |
| [add-formatted-currency-field-to-pdf](./add-formatted-currency-field-to-pdf.cs) | Add Formatted Currency Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF form with a NumberField that displays a currency value formatted... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF with JavaScript | `Document`, `TextBoxField`, `AnnotationFlags` | Demonstrates how to insert a hidden text box form field into a PDF and attach JavaScript that can... |
| [add-hidden-session-identifier-field-to-pdf](./add-hidden-session-identifier-field-to-pdf.cs) | Add Hidden Session Identifier Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to insert a hidden, read‑only text box field into an existing PDF to store a session id... |
| [add-http-post-submit-button-to-pdf](./add-http-post-submit-button-to-pdf.cs) | Add HTTP POST Submit Button to PDF Form | `Document`, `ButtonField`, `SubmitFormAction` | Demonstrates creating a push‑button field in a PDF and configuring a SubmitFormAction to send the... |
| [add-ipv4-validation-to-pdf-textbox](./add-ipv4-validation-to-pdf-textbox.cs) | Add IPv4 Validation to PDF Text Box Field | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF with a text box form field and attaching JavaScript that validates th... |
| [add-javascript-email-validation-to-pdf-form](./add-javascript-email-validation-to-pdf-form.cs) | Add JavaScript Email Validation to PDF Form | `Document`, `Form`, `TextBoxField` | Demonstrates how to create or locate an 'Email' text field in a PDF form and attach JavaScript th... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field using Aspose.Pdf so the script runs w... |
| [add-listbox-to-pdf-form](./add-listbox-to-pdf-form.cs) | Add List Box to PDF AcroForm | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF document, add a page, and insert a ListBoxField with country opt... |
| [add-localized-label-to-pdf](./add-localized-label-to-pdf.cs) | Add Localized Label to PDF Using JavaScript Dictionary | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a JavaScript dictionary of translations in a PDF and update a form fiel... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Add Multiline Feedback Text Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a multiline TextBoxField named 'Feedback' with a 500‑character limit i... |
| [add-new-page-with-acroform-fields](./add-new-page-with-acroform-fields.cs) | Add New Page with AcroForm Fields to PDF | `Document`, `Add`, `TextBoxField` | Shows how to append a blank page to an existing PDF and place fresh AcroForm fields (a text box a... |
| [add-numeric-field-with-range-validation](./add-numeric-field-with-range-validation.cs) | Add Numeric Field with Range Validation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a numeric form field in a PDF and attaching JavaScript to ensure the entere... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Add PaymentMethod Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a radio button group named 'PaymentMethod' with 'Credit' and 'Debit' o... |
| [add-progress-bar-field-to-pdf-form](./add-progress-bar-field-to-pdf-form.cs) | Add Progress Bar Field to PDF Form | `Document`, `Form`, `TextBoxField` | Shows how to insert a read‑only TextBoxField into a PDF form and update its value and color to ac... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to insert a push button into a PDF that clears all form fields when clicked by using a ... |
| [add-signature-field-to-pdf-form](./add-signature-field-to-pdf-form.cs) | Add Signature Field to PDF Form | `Document`, `Rectangle`, `SignatureField` | Shows how to insert a visible signature field into an existing PDF document using Aspose.Pdf, inc... |
| [add-signature-field-with-image-appearance](./add-signature-field-with-image-appearance.cs) | Add Signature Field with Image Appearance to PDF | `Document`, `SignatureField`, `ImageStamp` | Shows how to create a signature form field named 'ClientSignature' in a PDF and set its visual ap... |
| [add-submit-button-to-pdf-form](./add-submit-button-to-pdf-form.cs) | Add Submit Button to PDF Form | `Document`, `ButtonField`, `SubmitFormAction` | Shows how to create a push button on a PDF page and configure it to post all form fields to a spe... |
| [add-submit-button-to-pdf-form__v2](./add-submit-button-to-pdf-form__v2.cs) | Add Submit Button to PDF Form | `Document`, `Page`, `Rectangle` | Shows how to create a push button field in a PDF that posts form data to a specified URL using As... |
| [add-submit-button-with-validation-to-pdf](./add-submit-button-with-validation-to-pdf.cs) | Add Submit Button with Validation JavaScript to PDF | `Document`, `ButtonField`, `JavascriptAction` | Loads an existing PDF, creates a button field, attaches JavaScript that validates required form f... |
| [add-tooltip-to-pdf-form-field](./add-tooltip-to-pdf-form-field.cs) | Add Tooltip to PDF Form Field | `Document`, `Page`, `Rectangle` | Demonstrates creating (or loading) a PDF, adding a TextBox form field, setting its tooltip via th... |
| ... | | | *and 209 more files* |

## Category Statistics
- Total examples: 239

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

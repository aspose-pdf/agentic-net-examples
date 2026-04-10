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

- `using Aspose.Pdf;` (239/240 files) ← category-specific
- `using Aspose.Pdf.Forms;` (186/240 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (76/240 files)
- `using Aspose.Pdf.Text;` (19/240 files)
- `using Aspose.Pdf.Drawing;` (12/240 files)
- `using Aspose.Pdf.Facades;` (1/240 files)
- `using Aspose.Pdf.Security;` (1/240 files)
- `using Aspose.Pdf.Tagged;` (1/240 files)
- `using System;` (240/240 files)
- `using System.IO;` (217/240 files)
- `using System.Collections.Generic;` (18/240 files)
- `using System.Xml;` (18/240 files)
- `using System.Text;` (7/240 files)
- `using System.Xml.Linq;` (6/240 files)
- `using System.Drawing;` (5/240 files)
- `using System.Threading.Tasks;` (5/240 files)
- `using System.Data;` (3/240 files)
- `using System.Net.Http;` (3/240 files)
- `using System.IO.Compression;` (2/240 files)
- `using System.Linq;` (2/240 files)
- `using System.Net;` (2/240 files)
- `using System.Text.Json;` (2/240 files)
- `using System.Xml.Xsl;` (2/240 files)
- `using System.Drawing.Imaging;` (1/240 files)
- `using System.Security.Cryptography;` (1/240 files)
- `using System.Threading;` (1/240 files)
- `using System.Xml.Schema;` (1/240 files)

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
| [add-auto-backup-action-to-pdf-form-field](./add-auto-backup-action-to-pdf-form-field.cs) | Add Auto‑Backup Action to PDF Form Field | `Document`, `Save`, `Form` | Demonstrates how to attach a JavaScript action to a PDF form field that saves a backup copy each ... |
| [add-auto-updating-date-time-field-to-pdf](./add-auto-updating-date-time-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `Rectangle`, `DateField` | Demonstrates inserting a DateField into a PDF and attaching JavaScript so the field displays the ... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form While Preserving Fields | `Document`, `Add`, `Save` | Shows how to load an existing PDF form, insert a new blank page, and save the document, keeping a... |
| [add-calculated-total-field-javascript](./add-calculated-total-field-javascript.cs) | Add Calculated Total Field Using JavaScript | `Document`, `Save`, `Page` | Demonstrates how to create numeric form fields in a PDF and add a calculated field that sums them... |
| [add-calculated-total-price-field](./add-calculated-total-price-field.cs) | Add Calculated Total Price Field to PDF Form | `Document`, `Rectangle`, `NumberField` | Demonstrates how to add numeric fields to a PDF form and use JavaScript to automatically calculat... |
| [add-checkbox-field-to-pdf](./add-checkbox-field-to-pdf.cs) | Add Checkbox Field to Existing PDF | `Document`, `Rectangle`, `CheckboxField` | Demonstrates how to add a new checkbox form field named 'AgreeTerms' to an existing PDF using Asp... |
| [add-current-time-field-to-pdf](./add-current-time-field-to-pdf.cs) | Add Current Time Field to PDF on Load | `Document`, `Page`, `Rectangle` | Demonstrates how to create a DateField in a PDF, set its display format to HH:mm:ss, assign the c... |
| [add-custom-image-to-pdf-button](./add-custom-image-to-pdf-button.cs) | Add Custom Image Appearance to PDF Button Field | `Document`, `Save`, `ButtonField` | Demonstrates how to import an image file and set it as the appearance stream of a button field in... |
| [add-date-field-with-validation](./add-date-field-with-validation.cs) | Add Date Field with MM/DD/YYYY Validation | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, add a DateField, set its display format, and attach a JavaScript OnVal... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a date picker form field into a PDF and attach JavaScript to open the ... |
| [add-date-picker-field-to-pdf__v2](./add-date-picker-field-to-pdf__v2.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Rectangle`, `DateField` | Demonstrates how to insert a DateField into an existing PDF and set its default value to the curr... |
| [add-hidden-creation-timestamp-field-to-pdf](./add-hidden-creation-timestamp-field-to-pdf.cs) | Add Hidden Creation Timestamp Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a hidden, read‑only text box form field containing the document's crea... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF with JavaScript | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to add a hidden text box field to a PDF and populate it with the user's IP addre... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session ID Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to insert a hidden text box form field into an existing PDF to store a session i... |
| [add-ipv4-validation-to-pdf-textbox](./add-ipv4-validation-to-pdf-textbox.cs) | Add IPv4 Validation to PDF Text Box Field | `Document`, `Page`, `Rectangle` | Creates a PDF with a text box form field for an IPv4 address and attaches JavaScript that validat... |
| [add-javascript-calculation-button-to-pdf-form](./add-javascript-calculation-button-to-pdf-form.cs) | Add JavaScript Calculation Button to PDF Form | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a push‑button into a PDF form and attach JavaScript that calculates a ... |
| [add-javascript-email-validation-to-pdf-form-field](./add-javascript-email-validation-to-pdf-form-field.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Demonstrates how to attach a JavaScript action to a PDF form field using Aspose.Pdf to ensure the... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field that triggers when the field's value ... |
| [add-javascript-onlostfocus-action-to-pdf-form-fiel...](./add-javascript-onlostfocus-action-to-pdf-form-field.cs) | Add JavaScript OnLostFocus Action to PDF Form Field | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF form with a quantity field and a read‑only total field, and assi... |
| [add-javascript-validation-to-pdf-form-submit-butto...](./add-javascript-validation-to-pdf-form-submit-button.cs) | Add JavaScript Validation to PDF Form Submit Button | `Document`, `Form`, `TextBoxField` | Demonstrates how to add a required text field and a submit button with JavaScript that validates ... |
| [add-multiline-feedback-textbox](./add-multiline-feedback-textbox.cs) | Add Multiline Feedback TextBox to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a multiline TextBox form field named 'Feedback' with a 500‑character limit in... |
| [add-new-page-with-acroform-signature-field](./add-new-page-with-acroform-signature-field.cs) | Add New Page with AcroForm Signature Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to append a blank page to an existing PDF and place a fresh AcroForm signature f... |
| [add-numeric-validation-to-pdf-form-field](./add-numeric-validation-to-pdf-form-field.cs) | Add Numeric Validation to PDF Form Field | `Document`, `Rectangle`, `NumberField` | Demonstrates how to create a NumberField in a PDF and attach JavaScript validation to enforce a n... |
| [add-payment-method-radio-button-group](./add-payment-method-radio-button-group.cs) | Add PaymentMethod Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a PDF form with a radio button group named 'PaymentMethod' containing ... |
| [add-progress-bar-to-pdf-form](./add-progress-bar-to-pdf-form.cs) | Add Progress Bar to PDF Form Using Aspose.Pdf | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF form with a progress bar that updates automatically as the user check... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `Save`, `Form` | Demonstrates how to add a reset button to an existing PDF form using Aspose.Pdf, which clears all... |
| [add-reset-button-to-pdf-form__v2](./add-reset-button-to-pdf-form__v2.cs) | Add Reset Button to PDF Form | `Document`, `Form`, `Page` | Shows how to insert a reset button into an existing PDF form that clears all user‑entered data us... |
| [add-rich-text-field-with-html](./add-rich-text-field-with-html.cs) | Add Rich Text Field with HTML Formatting to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a RichTextBoxField, setting its FormattedValue with HTML markup, and adding... |
| [add-signature-field-to-pdf-form](./add-signature-field-to-pdf-form.cs) | Add Signature Field to PDF Form | `Document`, `Rectangle`, `SignatureField` | Shows how to load a PDF using Aspose.Pdf, create a signature form field, and save the modified do... |
| [add-signature-field-with-image-stamp](./add-signature-field-with-image-stamp.cs) | Add Signature Field with Image Stamp to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to create a signature form field named 'ClientSignature' in an existing PDF and ... |
| ... | | | *and 210 more files* |

## Category Statistics
- Total examples: 240

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
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

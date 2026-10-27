---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

> **Working with forms** in PDF using C# / .NET -- **228** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (218/228 files) ← category-specific
- `using Aspose.Pdf.Forms;` (197/228 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (63/228 files)
- `using Aspose.Pdf.Text;` (24/228 files)
- `using Aspose.Pdf.Drawing;` (5/228 files)
- `using Aspose.Pdf.Facades;` (2/228 files)
- `using Aspose.Pdf.LogicalStructure;` (2/228 files)
- `using Aspose.Pdf.Tagged;` (2/228 files)
- `using Aspose.Pdf.Devices;` (1/228 files)
- `using Aspose.Pdf.Security;` (1/228 files)
- `using Aspose.Pdf.Xfa;` (1/228 files)
- `using System;` (228/228 files)
- `using System.IO;` (209/228 files)
- `using System.Linq;` (35/228 files)
- `using System.Collections.Generic;` (32/228 files)
- `using System.Xml.Linq;` (25/228 files)
- `using System.Text.Json;` (18/228 files)
- `using System.Xml;` (11/228 files)
- `using System.Text;` (10/228 files)
- `using System.Drawing;` (8/228 files)
- `using System.Reflection;` (8/228 files)
- `using System.Text.RegularExpressions;` (3/228 files)
- `using System.Threading.Tasks;` (3/228 files)
- `using System.IO.Compression;` (2/228 files)
- `using System.Net.Http;` (2/228 files)
- `using System.Security.Cryptography.X509Certificates;` (2/228 files)
- `using System.Xml.Xsl;` (2/228 files)
- `using Microsoft.CSharp.RuntimeBinder;` (1/228 files)
- `using System.Data;` (1/228 files)
- `using System.Globalization;` (1/228 files)
- `using System.Net;` (1/228 files)
- `using System.Security.Cryptography;` (1/228 files)
- `using System.Xml.Schema;` (1/228 files)

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
| [acroform-with-margins](./acroform-with-margins.cs) | Create PDF with AcroForm Fields Using Relative Positioning | `Document`, `Page`, `MarginInfo` | Demonstrates how to generate a PDF with AcroForm text boxes and a checkbox, positioning the field... |
| [add-auto-updating-datetime-field-to-pdf](./add-auto-updating-datetime-field-to-pdf.cs) | Add Dynamic Date/Time Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a text box form field in a PDF and automatically fill it with the current dat... |
| [add-blank-page-save-as-new-pdf](./add-blank-page-save-as-new-pdf.cs) | Add Text Annotation and Save Modified PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF with Aspose.Pdf, adding a text annotation to the first page, and savin... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Page`, `PageInfo` | Shows how to load an existing PDF form, insert a new blank page while keeping existing fields int... |
| [add-calculated-total-field-with-javascript](./add-calculated-total-field-with-javascript.cs) | Add Calculated Total Field with JavaScript to PDF Form | `Document`, `TextBoxField`, `DefaultAppearance` | Shows how to insert a read‑only text box into a PDF form and attach JavaScript that sums all nume... |
| [add-calculated-total-price-field-to-pdf-form](./add-calculated-total-price-field-to-pdf-form.cs) | Add Calculated Total Price Field to PDF Form | `Document`, `Page`, `TextBoxField` | Shows how to insert Quantity and Unit Price text boxes into a PDF, add a read‑only Total Price fi... |
| [add-calculation-button-to-pdf-form](./add-calculation-button-to-pdf-form.cs) | Add JavaScript Calculation Button to PDF Form | `Document`, `Page`, `TextBoxField` | Shows how to create a PDF form with Quantity, Unit Price, and Total fields and a button that runs... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to Existing PDF | `Document`, `Rectangle`, `CheckboxField` | Shows how to insert a new checkbox form field named 'AgreeTerms' into an existing PDF document us... |
| [add-current-time-datefield-to-pdf](./add-current-time-datefield-to-pdf.cs) | Set Form Field Default Time on PDF Load | `Document`, `TextBoxField`, `Field` | Shows how to load a PDF with Aspose.Pdf, locate a TextBox form field, assign the current system t... |
| [add-date-field-validation](./add-date-field-validation.cs) | Validate Date Picker Minimum Date in PDF Form | `Document`, `Field`, `TextBoxField` | Demonstrates adding JavaScript validation to a PDF date picker field to reject dates earlier than... |
| [add-date-picker-field-current-date](./add-date-picker-field-current-date.cs) | Add Date Picker Field Defaulting to Current Date | `Document`, `Page`, `Rectangle` | Demonstrates how to add a text box field that acts as a date picker to an existing PDF and set it... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker to PDF Form Field using JavaScript | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to create a text box form field in a PDF and attach a JavaScript action that opens the ... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Populate Barcode Field with Dynamic Value in PDF | `Document`, `Form`, `BarcodeField` | Demonstrates loading a PDF template, locating a BarcodeField by name, assigning a unique runtime ... |
| [add-fallback-metadata-to-pdf](./add-fallback-metadata-to-pdf.cs) | Generate PDF from XML with Fallback Values | `Document`, `Page`, `TextFragment` | Shows how to read data from an XML file, supply default values when nodes are missing, and create... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Create PDF with Gender Radio Button AcroForm | `Document`, `Page`, `TextFragment` | Demonstrates how to generate a PDF, add a text label, and insert a radio button group for gender ... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF Form | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to create a hidden text box field in a PDF, inject JavaScript that captures the ... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session Identifier Field to PDF | `Document`, `Form`, `TextBoxField` | Demonstrates how to insert a hidden text box form field into a PDF to store a session identifier ... |
| [add-javascript-email-validation-to-pdf-form-field](./add-javascript-email-validation-to-pdf-form-field.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to load a PDF, locate the 'Email' TextBoxField, attach a JavaScript action that checks ... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener for PDF Form Field Change | `Document`, `JavascriptAction`, `TextBoxField` | Shows how to attach a JavaScript function to a PDF form field that triggers when the field's valu... |
| [add-javascript-localization-to-pdf-form](./add-javascript-localization-to-pdf-form.cs) | Add Locale‑Based Dynamic Label to PDF | `Document`, `Page`, `ComboBoxField` | Demonstrates how to create a PDF with a combo box for selecting a locale and a read‑only label th... |
| [add-listbox-to-pdf-form](./add-listbox-to-pdf-form.cs) | Create PDF with List Box AcroForm | `Document`, `Page`, `Rectangle` | Shows how to create a new PDF, add an AcroForm list box field, populate it with country options, ... |
| [add-multi-select-list-box-max-3](./add-multi-select-list-box-max-3.cs) | Set Maximum Selections for a Multi‑Select List Box | `Document`, `ListBoxField`, `JavascriptAction` | Demonstrates how to enable multi‑selection on a PDF list box, limit the number of selectable item... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Add Multiline Feedback TextBox to PDF Form | `Document`, `Rectangle`, `TextBoxField` | Shows how to insert a multiline text box field named 'Feedback' into an existing PDF document usi... |
| [add-new-page-with-acroform-fields](./add-new-page-with-acroform-fields.cs) | Add a New Page with AcroForm Fields to a PDF | `Document`, `Page`, `Rectangle` | Shows how to load an existing PDF, append a blank page, and place new AcroForm text box and check... |
| [add-numeric-field-with-validation](./add-numeric-field-with-validation.cs) | Numeric Range Validation Utility |  | Shows how to implement a simple validator that checks if a numeric value lies within an inclusive... |
| [add-password-textbox-field-to-pdf](./add-password-textbox-field-to-pdf.cs) | Secure PDF Form Field by Making It Read‑Only | `Document`, `Form`, `FormField` | Demonstrates how to protect a PDF form field by setting it to read‑only using Aspose.Pdf core API... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Create PaymentMethod Radio Button Group in PDF Form | `Document`, `Page`, `Rectangle` | Demonstrates how to add a radio button group named 'PaymentMethod' with 'Credit' and 'Debit' opti... |
| [add-read-only-date-field-to-pdf](./add-read-only-date-field-to-pdf.cs) | Set PDF Date Field to Read‑Only | `Document`, `Save`, `TextBoxField` | Loads a PDF, fills a text box form field with the current date‑time and marks the field as read‑o... |
| [add-reset-button-to-pdf-form](./add-reset-button-to-pdf-form.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to insert a reset button into an existing PDF form using Aspose.Pdf, which clears all u... |
| [add-reset-button-to-pdf-form__v2](./add-reset-button-to-pdf-form__v2.cs) | Add Reset Button to PDF Form | `Document`, `ButtonField`, `JavascriptAction` | Shows how to create a button field in a PDF that clears all form fields by attaching a JavaScript... |
| ... | | | *and 198 more files* |

## Category Statistics
- Total examples: 228

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
Updated: 2026-10-26 | Run: `20261026_221959_83adb1`
<!-- AUTOGENERATED:END -->

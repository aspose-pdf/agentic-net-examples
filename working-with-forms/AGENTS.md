---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

> **Working with forms** in PDF using C# / .NET -- **235** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (225/235 files) ← category-specific
- `using Aspose.Pdf.Forms;` (204/235 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (64/235 files)
- `using Aspose.Pdf.Text;` (24/235 files)
- `using Aspose.Pdf.Drawing;` (1/235 files)
- `using Aspose.Pdf.Facades;` (1/235 files)
- `using Aspose.Pdf.Operators;` (1/235 files)
- `using Aspose.Pdf.Security;` (1/235 files)
- `using System;` (235/235 files)
- `using System.IO;` (210/235 files)
- `using System.Collections.Generic;` (39/235 files)
- `using System.Linq;` (37/235 files)
- `using System.Xml.Linq;` (24/235 files)
- `using System.Drawing;` (17/235 files)
- `using System.Text.Json;` (14/235 files)
- `using System.Xml;` (9/235 files)
- `using System.Text;` (6/235 files)
- `using System.Threading.Tasks;` (4/235 files)
- `using System.Net.Http;` (3/235 files)
- `using System.Text.RegularExpressions;` (3/235 files)
- `using System.Globalization;` (2/235 files)
- `using System.IO.Compression;` (2/235 files)
- `using System.Xml.Xsl;` (2/235 files)
- `using System.Collections.Specialized;` (1/235 files)
- `using System.Net.Http.Headers;` (1/235 files)
- `using System.Reflection;` (1/235 files)
- `using System.Security.Cryptography;` (1/235 files)
- `using System.Security.Cryptography.X509Certificates;` (1/235 files)
- `using System.Xml.Schema;` (1/235 files)

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
| [acroform-with-margins](./acroform-with-margins.cs) | Create PDF with AcroForm Fields Using Relative Positioning | `Document`, `Page`, `MarginInfo` | Demonstrates how to add AcroForm fields (a text box and a checkbox) to a PDF using page margins a... |
| [add-auto-updating-datetime-field-to-pdf](./add-auto-updating-datetime-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to insert a text box form field into a PDF and attach document‑level JavaScript ... |
| [add-blank-page-save-as-new-pdf](./add-blank-page-save-as-new-pdf.cs) | Add Text Annotation to PDF and Save As New File | `Document`, `Page`, `Rectangle` | Shows how to open a PDF, place a text annotation on the first page, and save the modified documen... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Pages`, `Add` | Shows how to load an existing PDF form, append a new blank page, and save the document while pres... |
| [add-calculated-total-field-with-javascript](./add-calculated-total-field-with-javascript.cs) | Add Calculated Sum Field to PDF Form | `Document`, `TextBoxField`, `Rectangle` | Shows how to create numeric text box fields and a read‑only calculated field that sums them using... |
| [add-calculated-total-price-field-to-pdf-form](./add-calculated-total-price-field-to-pdf-form.cs) | Add Calculated Total Price Field to PDF Form | `Document`, `Page`, `Rectangle` | Demonstrates how to add Quantity, Unit Price, and a read‑only Total Price text box to a PDF form ... |
| [add-calculation-button-to-pdf-form](./add-calculation-button-to-pdf-form.cs) | Add JavaScript to PDF Button for Total Calculation | `Document`, `ButtonField`, `JavascriptAction` | Shows how to load a PDF, locate a button form field, attach a JavaScript action that multiplies Q... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `Rectangle`, `CheckboxField` | Shows how to open an existing PDF, create a checkbox form field named 'AgreeTerms', add it to the... |
| [add-current-time-datefield-to-pdf](./add-current-time-datefield-to-pdf.cs) | Set Form Field Default to Current Time on PDF Load | `Document`, `TextBoxField`, `Form` | Demonstrates how to set a PDF form text box field's default value to the current system time (HH:... |
| [add-date-field-validation](./add-date-field-validation.cs) | Validate Date Picker Minimum Date in PDF Form | `Document`, `Form`, `TextBoxField` | Demonstrates adding JavaScript validation to a PDF date picker field so that dates earlier than J... |
| [add-date-picker-field-current-date](./add-date-picker-field-current-date.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create or open a PDF, add a TextBox form field that acts as a date picker, se... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker to PDF Form Field | `Document`, `Page`, `Rectangle` | Shows how to insert a text box form field into a PDF and attach JavaScript that opens the built‑i... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Populate PDF Barcode Field with Dynamic Data | `Document`, `BarcodeField`, `Form` | Shows how to load a PDF template, generate a unique barcode value at runtime, assign it to a Barc... |
| [add-dynamic-code128-barcode-to-pdf](./add-dynamic-code128-barcode-to-pdf.cs) | Synchronize Code128 Barcode with Text Field in PDF | `Document`, `TextBoxField`, `BarcodeField` | Shows how to set a BarcodeField to Code128 and automatically update its value whenever a linked T... |
| [add-fallback-metadata-to-pdf](./add-fallback-metadata-to-pdf.cs) | Read XML with Default Values and Write to File |  | Shows how to load an XML document, retrieve element values with fallback defaults, and write the ... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Create PDF with Gender Radio Button Group | `Document`, `Page`, `RadioButtonField` | Shows how to generate a new PDF, add an AcroForm radio button field for gender selection, set a d... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF with JavaScript | `Document`, `Form`, `TextBoxField` | Demonstrates how to insert a hidden text box form field into a PDF and attach JavaScript that ret... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session Identifier Field to PDF | `Document`, `TextBoxField`, `Rectangle` | Shows how to insert a hidden text box form field containing a session ID into an existing PDF usi... |
| [add-javascript-email-validation-to-pdf-form-field](./add-javascript-email-validation-to-pdf-form-field.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `Field`, `TextBoxField` | Shows how to load a PDF, locate a text box form field named "Email", and attach a JavaScript acti... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field using Aspose.Pdf so the script runs w... |
| [add-javascript-localization-to-pdf-form](./add-javascript-localization-to-pdf-form.cs) | Create Localized PDF Label with JavaScript | `Document`, `Page`, `TextBoxField` | Shows how to add a hidden locale field and a read‑only label field to a PDF, embed a JavaScript d... |
| [add-listbox-to-pdf-form](./add-listbox-to-pdf-form.cs) | Create PDF with AcroForm List Box | `Document`, `Page`, `Rectangle` | Shows how to generate a new PDF, add an AcroForm list box containing country options, and save th... |
| [add-multi-select-list-box-max-3](./add-multi-select-list-box-max-3.cs) | Set Maximum Selections for PDF List Box | `Document`, `ListBoxField`, `JavascriptAction` | Shows how to enable multi‑select on a ListBoxField in a PDF form and enforce a limit of three sel... |
| [add-multiline-feedback-text-field](./add-multiline-feedback-text-field.cs) | Create Multiline Feedback Text Field in PDF | `Document`, `Page`, `Rectangle` | Shows how to add a multiline TextBoxField named 'Feedback' with a 500‑character limit to a PDF do... |
| [add-new-page-with-acroform-fields](./add-new-page-with-acroform-fields.cs) | Add New Page with AcroForm Fields to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to append a fresh page to an existing PDF and place various AcroForm fields (tex... |
| [add-numeric-field-with-validation](./add-numeric-field-with-validation.cs) | Numeric Range Validation Utility |  | Shows a simple method that checks whether an integer is within a specified inclusive range and th... |
| [add-password-textbox-field-to-pdf](./add-password-textbox-field-to-pdf.cs) | Protect PDF Form Field (Read‑Only) | `Document`, `Form`, `Field` | Shows how to load a PDF, locate a specific form field, set it to read‑only to protect it, and sav... |
| [add-paymentmethod-radio-button-group](./add-paymentmethod-radio-button-group.cs) | Add PaymentMethod Radio Button Group to PDF | `Document`, `Page`, `RadioButtonField` | Demonstrates how to create a radio button group named 'PaymentMethod' with 'Credit' and 'Debit' o... |
| [add-progress-bar-field-to-pdf-form](./add-progress-bar-field-to-pdf-form.cs) | Add a Progress Bar to a PDF Form with JavaScript | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to insert a TextBox field that acts as a progress bar in an existing PDF form an... |
| [add-read-only-date-field-to-pdf](./add-read-only-date-field-to-pdf.cs) | Set Date‑Time Form Field to Read‑Only in PDF | `Document`, `TextBoxField`, `Form` | Loads a PDF, populates a date‑time text box form field with the current timestamp, marks the fiel... |
| ... | | | *and 205 more files* |

## Category Statistics
- Total examples: 235

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
Updated: 2026-11-04 | Run: `20261104_052328_6aa9c8`
<!-- AUTOGENERATED:END -->

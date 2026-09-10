---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

> **Working with forms** in PDF using C# / .NET -- **358** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (232/358 files) ← category-specific
- `using Aspose.Pdf.Forms;` (187/358 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (71/358 files)
- `using Aspose.Pdf.Text;` (22/358 files)
- `using Aspose.Pdf.Drawing;` (13/358 files)
- `using Aspose.Pdf.Facades;` (3/358 files)
- `using Aspose.Pdf.Security;` (1/358 files)
- `using Aspose.Pdf.Tagged;` (1/358 files)
- `using System;` (232/358 files)
- `using System.IO;` (210/358 files)
- `using System.Xml;` (19/358 files)
- `using System.Collections.Generic;` (12/358 files)
- `using System.Drawing;` (7/358 files)
- `using System.Linq;` (6/358 files)
- `using System.Text;` (6/358 files)
- `using System.Xml.Linq;` (5/358 files)
- `using System.Text.Json;` (3/358 files)
- `using System.Threading.Tasks;` (3/358 files)
- `using System.IO.Compression;` (2/358 files)
- `using System.Net.Http;` (2/358 files)
- `using System.Data;` (1/358 files)
- `using System.Globalization;` (1/358 files)
- `using System.Security.Cryptography;` (1/358 files)
- `using System.Text.Json.Nodes;` (1/358 files)
- `using System.Text.RegularExpressions;` (1/358 files)
- `using System.Threading;` (1/358 files)
- `using System.Xml.Schema;` (1/358 files)
- `using System.Xml.Xsl;` (1/358 files)

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
| [acroform-with-margins](./acroform-with-margins.cs) | Create AcroForm TextBox Positioned by Page Margins | `Document`, `Page`, `MarginInfo` | Shows how to generate a PDF with an AcroForm text box whose position is calculated relative to th... |
| [add-acroform-textbox-absolute-coordinates](./add-acroform-textbox-absolute-coordinates.cs) | Add acroform textbox absolute coordinates |  | Add acroform textbox absolute coordinates |
| [add-auto-updating-date-time-field-to-pdf](./add-auto-updating-date-time-field-to-pdf.cs) | Add auto updating date time field to pdf |  | Add auto updating date time field to pdf |
| [add-auto-updating-datetime-field-to-pdf](./add-auto-updating-datetime-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `DateField`, `Rectangle` | Demonstrates inserting a DateField into a PDF and attaching a JavaScript OpenAction that refreshe... |
| [add-blank-page-save-as-new-pdf](./add-blank-page-save-as-new-pdf.cs) | Add Blank Page and Save PDF as New File | `Document`, `Save`, `Pages` | The example loads an existing PDF, adds a blank page, and saves the modified document to a new fi... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form | `Document`, `Pages`, `Add` | Shows how to load an existing PDF that contains form fields, append a new blank page, and save th... |
| [add-calculated-total-field-to-pdf-form](./add-calculated-total-field-to-pdf-form.cs) | Add calculated total field to pdf form |  | Add calculated total field to pdf form |
| [add-calculated-total-field-with-javascript](./add-calculated-total-field-with-javascript.cs) | Add Calculated Total Field with JavaScript in PDF Form | `Document`, `Page`, `NumberField` | Shows how to create numeric input fields and a read‑only total field that automatically sums them... |
| [add-calculated-total-price-field-to-pdf-form](./add-calculated-total-price-field-to-pdf-form.cs) | Add Calculated Total Price Field to PDF Form | `Document`, `Form`, `NumberField` | Demonstrates how to add quantity, unit price, and a read‑only calculated total price field to a P... |
| [add-calculation-button-to-pdf-form](./add-calculation-button-to-pdf-form.cs) | Add Calculation Button to PDF Form | `Document`, `Rectangle`, `ButtonField` | Shows how to create a push‑button in an existing PDF form and attach JavaScript that reads Quanti... |
| [add-checkbox-field-to-pdf-form](./add-checkbox-field-to-pdf-form.cs) | Add Checkbox Field to PDF Form | `Document`, `Rectangle`, `CheckboxField` | Shows how to load an existing PDF, create a CheckboxField, add it to the document's form, and sav... |
| [add-current-time-datefield-to-pdf](./add-current-time-datefield-to-pdf.cs) | Add Current Time DateField to PDF on Load | `Document`, `Page`, `Rectangle` | Demonstrates how to create a DateField in a PDF using Aspose.Pdf, set its display format to HH:mm... |
| [add-date-field-validation](./add-date-field-validation.cs) | Add Date Field with Validation to PDF | `Document`, `Rectangle`, `DateField` | Creates a PDF with a date form field and attaches JavaScript that blocks dates earlier than Janua... |
| [add-date-picker-field-current-date](./add-date-picker-field-current-date.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a date picker form field into a PDF and set its default value to the current ... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF | `Document`, `DateField`, `Border` | Shows how to create a DateField on an existing PDF, configure its appearance, initialize it, and ... |
| [add-dynamic-barcode-field-to-pdf](./add-dynamic-barcode-field-to-pdf.cs) | Add Dynamic Barcode Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF template, generate a unique value at runtime, create a Code128 barcode fi... |
| [add-dynamic-code128-barcode-to-pdf](./add-dynamic-code128-barcode-to-pdf.cs) | Add Dynamic Code128 Barcode Linked to a Text Field | `Document`, `TextBoxField`, `BarcodeField` | Shows how to insert a Code128 barcode field into a PDF and keep it updated automatically when a s... |
| [add-email-validation-to-pdf-form](./add-email-validation-to-pdf-form.cs) | Add email validation to pdf form |  | Add email validation to pdf form |
| [add-fallback-metadata-to-pdf](./add-fallback-metadata-to-pdf.cs) | Add Fallback Metadata to PDF from XML | `Document`, `Metadata`, `RegisterNamespaceUri` | Shows how to load XML metadata, create missing required fields with default values, and write the... |
| [add-gender-radio-button-group-to-pdf](./add-gender-radio-button-group-to-pdf.cs) | Add Gender Radio Button Group to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a new PDF document with an AcroForm and adding a gender selection radio but... |
| [add-hidden-ip-field-to-pdf](./add-hidden-ip-field-to-pdf.cs) | Add Hidden IP Address Field to PDF with JavaScript | `Document`, `TextBoxField`, `Rectangle` | Shows how to insert a hidden text box field into a PDF and attach JavaScript that captures the us... |
| [add-hidden-session-id-field-to-pdf](./add-hidden-session-id-field-to-pdf.cs) | Add Hidden Session ID Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Shows how to insert a hidden text box form field into an existing PDF document to store a session... |
| [add-hidden-session-identifier-field-to-pdf](./add-hidden-session-identifier-field-to-pdf.cs) | Add hidden session identifier field to pdf |  | Add hidden session identifier field to pdf |
| [add-ipv4-validation-to-pdf-textbox](./add-ipv4-validation-to-pdf-textbox.cs) | Add ipv4 validation to pdf textbox |  | Add ipv4 validation to pdf textbox |
| [add-javascript-email-validation-to-pdf-form-field](./add-javascript-email-validation-to-pdf-form-field.cs) | Add JavaScript Email Validation to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to load a PDF, locate the 'Email' form field, attach a JavaScript OnValidate action tha... |
| [add-javascript-listener-to-pdf-form-field](./add-javascript-listener-to-pdf-form-field.cs) | Add JavaScript Listener to PDF Form Field | `Document`, `Field`, `JavascriptAction` | Shows how to attach a JavaScript action to a PDF form field using Aspose.Pdf so the script runs w... |
| [add-javascript-localization-to-pdf-form](./add-javascript-localization-to-pdf-form.cs) | Add JavaScript Localization to PDF Form | `Document`, `TextBoxField`, `Rectangle` | Demonstrates embedding a JavaScript dictionary of translations into a PDF and updating a form fie... |
| [add-listbox-to-pdf-form](./add-listbox-to-pdf-form.cs) | Add List Box Field to PDF AcroForm | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF document, add an AcroForm list box with country options, and sav... |
| [add-locale-translation-to-pdf-form-label](./add-locale-translation-to-pdf-form-label.cs) | Add locale translation to pdf form label |  | Add locale translation to pdf form label |
| [add-multi-select-list-box-max-3](./add-multi-select-list-box-max-3.cs) | Add Multi-Select List Box with Max 3 Selections to PDF | `Document`, `ListBoxField`, `Rectangle` | Demonstrates creating a ListBoxField in a PDF, enabling multi‑selection, adding options, and limi... |
| ... | | | *and 328 more files* |

## Category Statistics
- Total examples: 358

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

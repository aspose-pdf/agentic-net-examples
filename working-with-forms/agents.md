---
name: Working with Forms
description: C# examples for Working with Forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Working with Forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Working with Forms** category.
This folder contains standalone C# examples for Working with Forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Working with Forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (11/11 files) ← category-specific
- `using Aspose.Pdf.Forms;` (11/11 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (8/11 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/11 files)
- `using Aspose.Pdf.Drawing;` (2/11 files)
- `using System;` (11/11 files)
- `using System.IO;` (11/11 files)
- `using System.Collections.Generic;` (1/11 files)
- `using System.Drawing;` (1/11 files)
- `using System.Text.Json;` (1/11 files)

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
| [add-progress-bar-field-to-pdf-form](./add-progress-bar-field-to-pdf-form.cs) | Add Progress Bar Field to PDF Form | `Document`, `Form`, `TextBoxField` | Shows how to insert a read‑only TextBoxField that serves as a visual progress bar in an existing ... |
| [add-submit-button-with-url-action](./add-submit-button-with-url-action.cs) | Add Submit Button with URL Action to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to programmatically add a push‑button to a PDF and configure its OnActivated act... |
| [apply-background-image-to-pdf-form-field](./apply-background-image-to-pdf-form-field.cs) | Set Background Image for PDF Form Field | `Document`, `Save`, `AddImage` | Demonstrates how to apply an image as the background of a PDF form field (handling both XFA and A... |
| [assign-image-to-pdf-button-field](./assign-image-to-pdf-button-field.cs) | Assign Image to PDF Button Field | `Document`, `ButtonField`, `AddImage` | Demonstrates loading a PDF, locating a button form field, and setting a custom appearance stream ... |
| [auto-update-qr-code-from-text-field](./auto-update-qr-code-from-text-field.cs) | Auto‑Update QR Code from Text Field in PDF | `Document`, `TextBoxField`, `BarcodeField` | Demonstrates how to create a text box and a QR code barcode field in a PDF and use JavaScript to ... |
| [create-pdf-form-field-with-custom-embedded-font](./create-pdf-form-field-with-custom-embedded-font.cs) | Create PDF Form Field with Custom Embedded Font | `OpenFont`, `Font`, `Document` | Demonstrates how to load an external TrueType font, embed it in a PDF, and apply it to a form Tex... |
| [create-pdf-form-template-from-json](./create-pdf-form-template-from-json.cs) | Create PDF Form Template and Populate from JSON | `Document`, `Page`, `TextBoxField` | Shows how to programmatically build a PDF form with placeholder fields using Aspose.Pdf, generate... |
| [create-pdf-form-with-qr-code](./create-pdf-form-with-qr-code.cs) | Create PDF Form with QR Code Field | `Document`, `Page`, `Rectangle` | Shows how to build a PDF form that contains a user‑editable text box and a QR code field that ref... |
| [export-form-field-appearance-streams](./export-form-field-appearance-streams.cs) | Export Form Field Appearance Streams to Separate PDFs | `Document`, `WidgetAnnotation`, `Clone` | Shows how to extract each form field's appearance stream from a PDF and save it as an individual ... |
| [language-specific-pdf-form-dynamic-label](./language-specific-pdf-form-dynamic-label.cs) | Language‑Specific PDF Form with Dynamic Label | `Document`, `Page`, `TextBoxField` | Shows how to create a PDF form that changes a label's text based on the selected locale using a c... |
| [set-default-appearance-for-pdf-form-fields](./set-default-appearance-for-pdf-form-fields.cs) | Set Default Appearance for PDF Form Fields | `Document`, `Save`, `Form` | Demonstrates how to define a default font, size, and color for all form fields in a PDF by assign... |

## Category Statistics
- Total examples: 11

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
- Review code examples in this folder for Working with Forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-06 | Run: `20260506_074010_1484c7`
<!-- AUTOGENERATED:END -->

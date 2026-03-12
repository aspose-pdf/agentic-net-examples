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

- `using Aspose.Pdf;` (73/74 files) ← category-specific
- `using Aspose.Pdf.Forms;` (46/74 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (16/74 files)
- `using Aspose.Pdf.Facades;` (13/74 files)
- `using Aspose.Pdf.Text;` (8/74 files)
- `using Aspose.Pdf.Drawing;` (2/74 files)
- `using Aspose.Pdf.Security;` (1/74 files)
- `using System;` (74/74 files)
- `using System.IO;` (71/74 files)
- `using System.Xml;` (4/74 files)
- `using System.Collections.Generic;` (2/74 files)
- `using System.Linq;` (2/74 files)
- `using System.Drawing;` (1/74 files)
- `using System.Net.Http;` (1/74 files)
- `using System.Net.Http.Headers;` (1/74 files)
- `using System.Reflection;` (1/74 files)
- `using System.Runtime.InteropServices;` (1/74 files)

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

| File | Key APIs | Description |
|------|----------|-------------|
| [Add-a-TextBoxField-a-CheckBoxField-and-a-ComboBoxField-as-in...](./Add-a-TextBoxField-a-CheckBoxField-and-a-ComboBoxField-as-interactive-form-fields-to-a-PDF-page.cs) | `Rectangle` | Add a TextBoxField a CheckBoxField and a ComboBoxField as interactive form fi... |
| [Add-various-form-fields-including-text-boxes-check-boxes-rad...](./Add-various-form-fields-including-text-boxes-check-boxes-radio-buttons-list-boxes-and-combo-boxe.cs) | `FormEditor` | Add various form fields including text boxes check boxes radio buttons list b... |
| [Analyze-the-XFA-form-documentation-to-comprehend-supported-c...](./Analyze-the-XFA-form-documentation-to-comprehend-supported-capabilities-limitations-and-integratio.cs) |  | Analyze the XFA form documentation to comprehend supported capabilities limit... |
| [Assign-a-boolean-value-to-a-CheckBox-field-within-a-PDF-docu...](./Assign-a-boolean-value-to-a-CheckBox-field-within-a-PDF-document-programmatically-using-the-API.cs) |  | Assign a boolean value to a CheckBox field within a PDF document programmatic... |
| [Assign-values-to-PDF-form-fields-and-flatten-the-document-to...](./Assign-values-to-PDF-form-fields-and-flatten-the-document-to-render-the-fields-non-editable.cs) |  | Assign values to PDF form fields and flatten the document to render the field... |
| [Assign-values-to-TextBoxField-CheckBoxField-and-RadioButtonF...](./Assign-values-to-TextBoxField-CheckBoxField-and-RadioButtonField-controls-within-a-PDF-form-progra.cs) |  | Assign values to TextBoxField CheckBoxField and RadioButtonField controls wit... |
| [Configure-PDF-form-field-attributes-such-as-name-value-visua...](./Configure-PDF-form-field-attributes-such-as-name-value-visual-appearance-and-export-values-prog.cs) | `Rectangle` | Configure PDF form field attributes such as name value visual appearance and ... |
| [Configure-a-TextBoxField-s-font-size-and-color-programmatica...](./Configure-a-TextBoxField-s-font-size-and-color-programmatically-using-the-appropriate-.NET-API-metho.cs) | `Rectangle` | Configure a TextBoxField s font size and color programmatically using the app... |
| [Configure-a-form-field-s-SubmitAction-programmatically-to-sp...](./Configure-a-form-field-s-SubmitAction-programmatically-to-specify-submission-behavior-for-the-PDF-fo.cs) | `FormEditor` | Configure a form field s SubmitAction programmatically to specify submission ... |
| [Convert-PDF-form-fields-into-static-content-flattening-the-d...](./Convert-PDF-form-fields-into-static-content-flattening-the-document-to-disable-any-further-field-ed.cs) |  | Convert PDF form fields into static content flattening the document to disabl... |
| [Create-a-new-Document-instance-that-loads-a-PDF-form-for-sub...](./Create-a-new-Document-instance-that-loads-a-PDF-form-for-subsequent-processing-within-your-applicati.cs) |  | Create a new Document instance that loads a PDF form for subsequent processin... |
| [Define-the-form-s-action-attribute-with-the-desired-endpoint...](./Define-the-form-s-action-attribute-with-the-desired-endpoint-URL-to-direct-submission-requests-appro.cs) | `FormEditor` | Define the form s action attribute with the desired endpoint URL to direct su... |
| [Delete-a-designated-form-field-from-a-PDF-document-while-mai...](./Delete-a-designated-form-field-from-a-PDF-document-while-maintaining-the-integrity-of-remaining-fiel.cs) |  | Delete a designated form field from a PDF document while maintaining the inte... |
| [Evaluate-the-procedure-for-updating-visual-appearance-of-PDF...](./Evaluate-the-procedure-for-updating-visual-appearance-of-PDF-elements-to-ensure-accurate-rendering.cs) |  | Evaluate the procedure for updating visual appearance of PDF elements to ensu... |
| [Evaluate-the-process-of-flattening-XFA-forms-to-ensure-prope...](./Evaluate-the-process-of-flattening-XFA-forms-to-ensure-proper-conversion-into-static-PDF-content.cs) |  | Evaluate the process of flattening XFA forms to ensure proper conversion into... |
| [Examine-how-to-create-populate-and-process-XFA-form-template...](./Examine-how-to-create-populate-and-process-XFA-form-templates-within-PDF-files-using-.NET-APIs.cs) |  | Examine how to create populate and process XFA form templates within PDF file... |
| [Examine-methods-for-modifying-form-field-properties-within-a...](./Examine-methods-for-modifying-form-field-properties-within-a-PDF-document-including-appearance-and.cs) | `FormEditor`, `Form` | Examine methods for modifying form field properties within a PDF document inc... |
| [Examine-methods-for-persisting-changes-to-a-document-after-e...](./Examine-methods-for-persisting-changes-to-a-document-after-editing-ensuring-updated-content-is-corr.cs) | `TextAnnotation`, `TextFragment` | Examine methods for persisting changes to a document after editing ensuring u... |
| [Examine-the-Create-Form-documentation-segment-to-ensure-API-...](./Examine-the-Create-Form-documentation-segment-to-ensure-API-usage-guidelines-are-accurate-and-comple.cs) | `Form` | Examine the Create Form documentation segment to ensure API usage guidelines ... |
| [Examine-the-Extract-Form-Data-documentation-section-thorough...](./Examine-the-Extract-Form-Data-documentation-section-thoroughly-to-verify-technical-accuracy-complet.cs) | `TextAbsorber` | Examine the Extract Form Data documentation section thoroughly to verify tech... |
| [Examine-the-Fill-Form-functionality-in-the-.NET-library-to-e...](./Examine-the-Fill-Form-functionality-in-the-.NET-library-to-ensure-correct-implementation-and-compreh.cs) | `Rectangle` | Examine the Fill Form functionality in the .NET library to ensure correct imp... |
| [Examine-the-Form-Fields-Overview-documentation-to-understand...](./Examine-the-Form-Fields-Overview-documentation-to-understand-handling-and-manipulation-of-PDF-form-f.cs) |  | Examine the Form Fields Overview documentation to understand handling and man... |
| [Examine-the-data-export-functionality-and-verify-that-output...](./Examine-the-data-export-functionality-and-verify-that-output-formats-conform-to-specified-standards.cs) | `XmlSaveOptions` | Examine the data export functionality and verify that output formats conform ... |
| [Examine-the-documentation-to-identify-and-verify-all-form-ty...](./Examine-the-documentation-to-identify-and-verify-all-form-types-supported-by-the-library.cs) |  | Examine the documentation to identify and verify all form types supported by ... |
| [Examine-the-implementation-of-form-submission-handling-and-v...](./Examine-the-implementation-of-form-submission-handling-and-validate-its-correctness-within-the-.NET.cs) | `FormEditor` | Examine the implementation of form submission handling and validate its corre... |
| [Examine-the-procedure-for-assigning-values-to-form-fields-wi...](./Examine-the-procedure-for-assigning-values-to-form-fields-within-PDF-documents-ensuring-correct-dat.cs) |  | Examine the procedure for assigning values to form fields within PDF document... |
| [Examine-the-procedure-for-inserting-interactive-form-fields-...](./Examine-the-procedure-for-inserting-interactive-form-fields-into-PDF-documents-ensuring-proper-fiel.cs) |  | Examine the procedure for inserting interactive form fields into PDF document... |
| [Examine-the-procedure-for-iterating-over-PDF-form-fields-and...](./Examine-the-procedure-for-iterating-over-PDF-form-fields-and-accessing-their-properties-programmatic.cs) |  | Examine the procedure for iterating over PDF form fields and accessing their ... |
| [Examine-the-procedure-for-persisting-a-completed-PDF-form-to...](./Examine-the-procedure-for-persisting-a-completed-PDF-form-to-a-file-ensuring-data-integrity.cs) |  | Examine the procedure for persisting a completed PDF form to a file ensuring ... |
| [Examine-the-procedures-for-handling-AcroForm-fields-within-P...](./Examine-the-procedures-for-handling-AcroForm-fields-within-PDF-documents-including-reading-updatin.cs) |  | Examine the procedures for handling AcroForm fields within PDF documents incl... |
| ... | | *and 44 more files* |

## Category Statistics
- Total examples: 74

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

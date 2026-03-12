---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-fill-forms** category.
This folder contains standalone C# examples for facades-fill-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-fill-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (12/12 files) ← category-specific
- `using Aspose.Pdf;` (2/12 files)
- `using System;` (12/12 files)
- `using System.IO;` (12/12 files)
- `using System.Data;` (6/12 files) ← category-specific
- `using System.Collections.Generic;` (5/12 files)
- `using System.Linq;` (1/12 files)

## Common Code Pattern

Most files in this category use `Form` from `Aspose.Pdf.Facades`:

```csharp
Form tool = new Form();
tool.BindPdf("input.pdf");
// ... Form operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [export-excel-workbook-data-into-matching-pdf-form-fields-to-...](./export-excel-workbook-data-into-matching-pdf-form-fields-to-automatically-fill-the-pdf-document.cs) | `Form` | Export excel workbook data into matching pdf form fields to automatically fil... |
| [populate-a-pdf-document-s-form-by-programmatically-setting-v...](./populate-a-pdf-document-s-form-by-programmatically-setting-values-for-all-supported-form-field-types.cs) | `Form` | Populate a pdf document s form by programmatically setting values for all sup... |
| [populate-a-pdf-form-programmatically-according-to-the-specif...](./populate-a-pdf-form-programmatically-according-to-the-specified-prerequisites-ensuring-all-required.cs) | `Form` | Populate a pdf form programmatically according to the specified prerequisites... |
| [populate-a-pdf-form-using-data-exported-from-an-excel-worksh...](./populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-according-to-the-defined-fill-proces.cs) | `Form` | Populate a pdf form using data exported from an excel worksheet according to ... |
| [populate-a-pdf-form-using-data-exported-from-an-excel-worksh...](./populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-mapping-worksheet-cells-to-correspo.cs) | `Form` | Populate a pdf form using data exported from an excel worksheet mapping works... |
| [populate-a-pdf-form-using-data-from-an-instantiated-excel-wo...](./populate-a-pdf-form-using-data-from-an-instantiated-excel-workbook-while-maintaining-.net-object-int.cs) | `Form` | Populate a pdf form using data from an instantiated excel workbook while main... |
| [populate-a-pdf-form-using-data-from-supported-excel-formats-...](./populate-a-pdf-form-using-data-from-supported-excel-formats-xls-xlsx-while-preserving-field-mappi.cs) | `Form` | Populate a pdf form using data from supported excel formats xls xlsx while pr... |
| [populate-a-pdf-form-with-data-extracted-from-an-exported-exc...](./populate-a-pdf-form-with-data-extracted-from-an-exported-excel-worksheet-ensuring-field-mapping-acc.cs) | `Form` | Populate a pdf form with data extracted from an exported excel worksheet ensu... |
| [populate-fields-of-a-loaded-pdf-form-programmatically-using-...](./populate-fields-of-a-loaded-pdf-form-programmatically-using-the-appropriate-api-ensuring-data-valid.cs) | `Form` | Populate fields of a loaded pdf form programmatically using the appropriate a... |
| [populate-pdf-form-fields-using-data-exported-from-an-excel-w...](./populate-pdf-form-fields-using-data-exported-from-an-excel-worksheet-mapping-cells-to-corresponding.cs) | `Form` | Populate pdf form fields using data exported from an excel worksheet mapping ... |
| [populate-pdf-form-fields-with-data-and-write-the-modified-do...](./populate-pdf-form-fields-with-data-and-write-the-modified-document-to-a-new-pdf-file.cs) | `Form` | Populate pdf form fields with data and write the modified document to a new p... |
| [populate-pdf-form-fields-with-data-exported-from-an-excel-wo...](./populate-pdf-form-fields-with-data-exported-from-an-excel-worksheet-mapping-each-cell-to-its-corres.cs) | `Form` | Populate pdf form fields with data exported from an excel worksheet mapping e... |

## Category Statistics
- Total examples: 12

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.FieldType`
- `Aspose.Pdf.Facades.Form`
- `Aspose.Pdf.Facades.Form.BindPdf`
- `Aspose.Pdf.Facades.Form.BindPdf(string)`
- `Aspose.Pdf.Facades.Form.ExportFdf`
- `Aspose.Pdf.Facades.Form.FillField(string, string)`
- `Aspose.Pdf.Facades.Form.GetField(string)`
- `Aspose.Pdf.Facades.Form.ImportFdf`
- `Aspose.Pdf.Facades.Form.Save`
- `Aspose.Pdf.Facades.Form.Save(string)`
- `Aspose.Pdf.Facades.FormEditor`
- `Aspose.Pdf.Facades.FormEditor.BindPdf`
- `Aspose.Pdf.Facades.FormEditor.CopyOuterField`
- `Aspose.Pdf.Facades.FormEditor.Save`
- `Aspose.Pdf.Facades.FormFieldFacade`

### Rules
- Bind a PDF file to a Form facade with Form.BindPdf({input_pdf}).
- Flatten every form field in the bound document by calling Form.FlattenAllFields().
- Persist the flattened document using Form.Save({output_pdf}).
- Use Form.BindPdf({input_pdf}) to open a PDF document for form manipulation.
- Open an FDF file as a stream and call Form.ImportFdf({fdf_stream}) to populate the PDF form fields.

### Warnings
- The Form class belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in future releases; consider using the newer Document/FormField APIs.
- The example manually manages the FileStream; ensure the stream is closed or disposed to avoid resource leaks.
- The example assumes the target PDF already contains an AcroForm; otherwise AddField may have no effect.
- Coordinate values are in points; callers must convert from other units if needed.
- FormFieldFacade.Alignment expects one of the FormFieldFacade alignment constants (e.g., AlignCenter).

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-fill-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

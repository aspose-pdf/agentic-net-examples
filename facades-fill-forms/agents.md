---
name: Facades - Fill Forms
description: C# examples for Facades - Fill Forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - Fill Forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - Fill Forms** category.
This folder contains standalone C# examples for Facades - Fill Forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - Fill Forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Files in this folder
- [export-excel-workbook-data-into-matching-pdf-form-fields-to-automatically-fill-the-pdf-document](./export-excel-workbook-data-into-matching-pdf-form-fields-to-automatically-fill-the-pdf-document.cs)
- [populate-a-pdf-document-s-form-by-programmatically-setting-values-for-all-supported-form-field-types](./populate-a-pdf-document-s-form-by-programmatically-setting-values-for-all-supported-form-field-types.cs)
- [populate-a-pdf-form-programmatically-according-to-the-specified-prerequisites-ensuring-all-required](./populate-a-pdf-form-programmatically-according-to-the-specified-prerequisites-ensuring-all-required.cs)
- [populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-according-to-the-defined-fill-proces](./populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-according-to-the-defined-fill-proces.cs)
- [populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-mapping-worksheet-cells-to-correspo](./populate-a-pdf-form-using-data-exported-from-an-excel-worksheet-mapping-worksheet-cells-to-correspo.cs)
- [populate-a-pdf-form-using-data-from-an-instantiated-excel-workbook-while-maintaining-.net-object-int](./populate-a-pdf-form-using-data-from-an-instantiated-excel-workbook-while-maintaining-.net-object-int.cs)
- [populate-a-pdf-form-using-data-from-supported-excel-formats-xls-xlsx-while-preserving-field-mappi](./populate-a-pdf-form-using-data-from-supported-excel-formats-xls-xlsx-while-preserving-field-mappi.cs)
- [populate-a-pdf-form-with-data-extracted-from-an-exported-excel-worksheet-ensuring-field-mapping-acc](./populate-a-pdf-form-with-data-extracted-from-an-exported-excel-worksheet-ensuring-field-mapping-acc.cs)
- [populate-fields-of-a-loaded-pdf-form-programmatically-using-the-appropriate-api-ensuring-data-valid](./populate-fields-of-a-loaded-pdf-form-programmatically-using-the-appropriate-api-ensuring-data-valid.cs)
- [populate-pdf-form-fields-using-data-exported-from-an-excel-worksheet-mapping-cells-to-corresponding](./populate-pdf-form-fields-using-data-exported-from-an-excel-worksheet-mapping-cells-to-corresponding.cs)
- [populate-pdf-form-fields-with-data-and-write-the-modified-document-to-a-new-pdf-file](./populate-pdf-form-fields-with-data-and-write-the-modified-document-to-a-new-pdf-file.cs)
- [populate-pdf-form-fields-with-data-exported-from-an-excel-worksheet-mapping-each-cell-to-its-corres](./populate-pdf-form-fields-with-data-exported-from-an-excel-worksheet-mapping-each-cell-to-its-corres.cs)

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
- Review code examples in this folder for Facades - Fill Forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_162911_2c5d86`
<!-- AUTOGENERATED:END -->

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
- [async-read-xlsx-write-pdf](./async-read-xlsx-write-pdf.cs)
- [autofiller-excel-streaming](./autofiller-excel-streaming.cs)
- [autofiller-fill-form](./autofiller-fill-form.cs)
- [autofiller-retry-import](./autofiller-retry-import.cs)
- [batch-fill-pdf-template](./batch-fill-pdf-template.cs)
- [bind-pdf-form-autofiller](./bind-pdf-form-autofiller.cs)
- [excel-to-pdf-mapper](./excel-to-pdf-mapper.cs)
- [excel-to-pdf-table](./excel-to-pdf-table.cs)
- [fill-pdf-form-data-table](./fill-pdf-form-data-table.cs)
- [fill-pdf-form-from-excel](./fill-pdf-form-from-excel.cs)
- [fill-pdf-form-with-cancellation](./fill-pdf-form-with-cancellation.cs)
- [fill-pdf-from-xlsx](./fill-pdf-from-xlsx.cs)
- [fill-pdf-validate-size](./fill-pdf-validate-size.cs)
- [fill-pdf-with-autofiller](./fill-pdf-with-autofiller.cs)
- [generate-pdfs-custom-filename](./generate-pdfs-custom-filename.cs)
- [import-datatable-autofiller](./import-datatable-autofiller.cs)
- [load-the-source-pdf-from-a-memory-stream-and-fill-it-using-autofiller-without-writing-intermediate-files](./load-the-source-pdf-from-a-memory-stream-and-fill-it-using-autofiller-without-writing-intermediate-files.cs)
- [log-datatable-rows-to-pdf](./log-datatable-rows-to-pdf.cs)
- [map-datatable-columns-to-pdf-fields](./map-datatable-columns-to-pdf-fields.cs)
- [merge-multiple-filled-pdf-documents-generated-from-separate-datatables-into-a-single-consolidated-pdf-file](./merge-multiple-filled-pdf-documents-generated-from-separate-datatables-into-a-single-consolidated-pdf-file.cs)
- [pdf-page-datatable-summary](./pdf-page-datatable-summary.cs)
- [pdf-to-byte-array](./pdf-to-byte-array.cs)
- [pdf-to-excel-temp-folder](./pdf-to-excel-temp-folder.cs)
- [populate-pdf-form-fields-numeric-format](./populate-pdf-form-fields-numeric-format.cs)
- [process-multiple-excel-worksheets-by-creating-separate-datatable-instances-and-invoking-autofiller-for-each](./process-multiple-excel-worksheets-by-creating-separate-datatable-instances-and-invoking-autofiller-for-each.cs)
- [protect-pdf-with-password](./protect-pdf-with-password.cs)
- [save-filled-pdf](./save-filled-pdf.cs)
- [split-pdf-single-pages](./split-pdf-single-pages.cs)
- [verify-pdf-pages-per-datarow](./verify-pdf-pages-per-datarow.cs)
- [xlsx-to-datatable-pdf](./xlsx-to-datatable-pdf.cs)

## Category Statistics
- Total examples: 30

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
Updated: 2026-03-30 | Run: `20260330_160100_22dc24`
<!-- AUTOGENERATED:END -->

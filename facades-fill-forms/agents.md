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

- `using Aspose.Pdf.Facades;` (27/28 files) ← category-specific
- `using Aspose.Pdf;` (13/28 files)
- `using Aspose.Pdf.Forms;` (3/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (28/28 files)
- `using System.Data;` (19/28 files) ← category-specific
- `using System.Collections.Generic;` (5/28 files)
- `using System.Threading;` (2/28 files)
- `using System.Threading.Tasks;` (2/28 files)
- `using System.Linq;` (1/28 files)
- `using System.Text.Json;` (1/28 files)

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
| [add-current-date-header-to-pdf](./add-current-date-header-to-pdf.cs) | Add Current Date Header to PDF Using AutoFiller | `BindPdf`, `Save`, `BindPdf` | Shows how to generate a PDF from a template with AutoFiller and then add a header containing the ... |
| [async-fill-pdf-form-from-xlsx](./async-fill-pdf-form-from-xlsx.cs) | Asynchronously Fill PDF Form from XLSX Data | `Document`, `Form`, `FillField` | The example reads an XLSX file asynchronously, extracts field values, fills a PDF form using Aspo... |
| [batch-fill-pdf-template-with-multiple-datatables](./batch-fill-pdf-template-with-multiple-datatables.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to repeatedly fill a single PDF form template using a list of DataTable objects, produc... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller and Save | `AutoFiller`, `BindPdf`, `Save` | Demonstrates how to bind an existing PDF form to an Aspose.Pdf.Facades.AutoFiller instance and sa... |
| [configure-datacolumn-properties-fill-pdf-form](./configure-datacolumn-properties-fill-pdf-form.cs) | Configure DataColumn Properties and Fill PDF Form with AutoF... | `Document`, `Page`, `Rectangle` | The example shows how to set DataColumn flags such as ReadOnly and Unique before importing a Data... |
| [convert-filled-pdf-to-byte-array](./convert-filled-pdf-to-byte-array.cs) | Convert Filled PDF to Byte Array Using PdfViewer | `PdfViewer`, `BindPdf`, `Save` | Shows how to load a filled PDF with Aspose.Pdf.Facades.PdfViewer and return its content as a byte... |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Extract PDF Pages with Custom File Names from DataTable | `PdfFileEditor`, `Extract` | Shows how to split a PDF into individual pages using Aspose.Pdf.Facades.PdfFileEditor and name ea... |
| [fill-pdf-form-async-timeout](./fill-pdf-form-async-timeout.cs) | Fill PDF Form Asynchronously with Timeout Cancellation | `Form`, `BindPdf`, `FillField` | Demonstrates how to fill a PDF form using Aspose.Pdf.Facades, then save the document asynchronous... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV Data | `Form`, `FillField`, `Save` | Shows how to read a CSV file into a DataTable, map column names to PDF form field names, and use ... |
| [fill-pdf-form-per-datarow-merge-pages](./fill-pdf-form-per-datarow-merge-pages.cs) | Fill PDF Form per DataTable Row and Merge Pages with Logging | `Document`, `AutoFiller`, `BindPdf` | Demonstrates how to fill a PDF form for each DataTable row using AutoFiller, merge the generated ... |
| [fill-pdf-form-using-autofiller-datatable](./fill-pdf-form-using-autofiller-datatable.cs) | Fill PDF Form Using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to bind a PDF template, import a DataTable whose column names match AcroForm fie... |
| [fill-pdf-form-validate-size](./fill-pdf-form-validate-size.cs) | Fill PDF Form and Validate Generated File Size | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf.Facades.AutoFiller to merge a DataTable into a PDF form, save the r... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form Using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to bind a PDF template, import data from a DataTable (simulating an XLSX source), and g... |
| [fill-pdf-form-with-autofiller__v2](./fill-pdf-form-with-autofiller__v2.cs) | Fill PDF Form Using AutoFiller and Dispose Resources | `AutoFiller`, `InputFileName`, `ImportDataTable` | Demonstrates how to populate a PDF form from a DataTable using Aspose.Pdf.Facades.AutoFiller and ... |
| [fill-pdf-form-with-csv-dynamic-mapping](./fill-pdf-form-with-csv-dynamic-mapping.cs) | Fill PDF Form Using CSV Data with Dynamic Column Mapping | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates loading CSV data into a DataTable, applying a JSON‑based column‑to‑PDF‑field mapping... |
| [fill-pdf-form-with-data-table-exception-handling](./fill-pdf-form-with-data-table-exception-handling.cs) | Fill PDF Form with DataTable and Handle Exceptions | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example shows how to use Aspose.Pdf.Facades.AutoFiller to bind a PDF template, import data fr... |
| [fill-pdf-form-with-datatable-custom-columns](./fill-pdf-form-with-datatable-custom-columns.cs) | Fill PDF Form Using DataTable with Custom Columns | `Document`, `Page`, `TextBoxField` | Shows how to create a PDF form, add matching DataTable columns (including extra custom columns) v... |
| [generate-pdf-page-summary-report](./generate-pdf-page-summary-report.cs) | Generate PDF Page‑to‑DataTable Summary Report | `Document`, `Save`, `Add` | Creates a PDF (or a blank one if missing), populates a DataTable with identifiers for each page, ... |
| [generate-pdf-per-datarow](./generate-pdf-per-datarow.cs) | Generate One‑Page PDFs from DataTable Rows | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF form template, filling its fields with values from each DataTable row,... |
| [generate-pdfs-from-multiple-csv-worksheets](./generate-pdfs-from-multiple-csv-worksheets.cs) | Generate PDFs from Multiple CSV Worksheets using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example reads each CSV file in a folder, converts it to a DataTable, and uses Aspose.Pdf.Faca... |
| [map-datatable-columns-to-pdf-form-fields](./map-datatable-columns-to-pdf-form-fields.cs) | Map DataTable Columns to PDF Form Fields with AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to rename DataTable columns to match PDF form field identifiers and use Aspose.Pdf.Faca... |
| [merge-filled-pdfs-from-datatables](./merge-filled-pdfs-from-datatables.cs) | Merge Multiple Filled PDFs Generated from DataTables | `BindPdf`, `ImportDataTable`, `Save` | The example fills a PDF form template with data from several DataTables, saves each filled docume... |
| [password-protect-filled-pdf](./password-protect-filled-pdf.cs) | Password‑Protect a Filled PDF with Aspose.Pdf | `Document`, `Save`, `PdfFileSecurity` | Demonstrates loading an already filled PDF and applying user and owner passwords with specific pr... |
| [process-pdf-with-temp-folder](./process-pdf-with-temp-folder.cs) | Process PDF with Temporary Folder and Disk Buffer | `Document`, `PdfSaveOptions`, `PdfFileEditor` | Demonstrates how to use a unique temporary folder and disk buffering while processing large PDFs ... |
| [save-filled-pdf-preserve-layout](./save-filled-pdf-preserve-layout.cs) | Save Filled PDF While Preserving Layout | `Form`, `BindPdf`, `Save` | Shows how to bind a filled PDF using the Form facade and save it to a new file, keeping the origi... |
| [split-filled-pdf-into-single-page-pdfs](./split-filled-pdf-into-single-page-pdfs.cs) | Split Filled PDF into Single-Page PDFs | `PdfFileEditor`, `SplitToPages` | Demonstrates loading a filled PDF and using Aspose.Pdf.Facades.PdfFileEditor to split it into ind... |
| [stream-large-csv-fill-pdf-form](./stream-large-csv-fill-pdf-form.cs) | Stream Large CSV Data into PDF Form Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to read a large CSV file row‑by‑row into a DataTable and use Aspose.Pdf.Facades.... |
| [validate-required-pdf-form-fields](./validate-required-pdf-form-fields.cs) | Validate Required PDF Form Fields Against DataTable Columns | `Form`, `FieldNames`, `IsRequiredField` | Shows how to check that every required AcroForm field in a PDF has a matching column in a DataTab... |

## Category Statistics
- Total examples: 28

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
Updated: 2026-05-08 | Run: `20260508_144436_050a95`
<!-- AUTOGENERATED:END -->

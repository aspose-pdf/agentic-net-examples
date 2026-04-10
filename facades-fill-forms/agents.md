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

- `using Aspose.Pdf.Facades;` (32/35 files) ← category-specific
- `using Aspose.Pdf;` (15/35 files)
- `using Aspose.Pdf.Text;` (4/35 files)
- `using Aspose.Pdf.Forms;` (3/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (33/35 files)
- `using System.Data;` (25/35 files) ← category-specific
- `using System.Collections.Generic;` (6/35 files)
- `using System.Threading;` (3/35 files)
- `using System.Linq;` (2/35 files)
- `using System.Threading.Tasks;` (2/35 files)
- `using Microsoft.VisualStudio.TestTools.UnitTesting;` (1/35 files)
- `using System.Runtime.InteropServices;` (1/35 files)
- `using System.Text.Json;` (1/35 files)

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
| [add-date-header-to-pdf-pages](./add-date-header-to-pdf-pages.cs) | Add Date Header to PDF Pages | `PageDate`, `FormattedText`, `PdfFileStamp` | Shows how to use Aspose.Pdf's PdfFileStamp to insert a header with the current date on every page... |
| [add-image-watermark-to-filled-pdf-pages](./add-image-watermark-to-filled-pdf-pages.cs) | Add Image Watermark to Filled PDF Pages | `AutoFiller`, `PdfFileStamp`, `Stamp` | Demonstrates how to fill a PDF form using Aspose.Pdf.Facades.AutoFiller and then apply a semi‑tra... |
| [async-fill-pdf-from-excel](./async-fill-pdf-from-excel.cs) | Asynchronously Fill PDF Form from Excel Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates loading Excel data asynchronously, binding it to a PDF template with AutoFiller, and... |
| [autofiller-retry-logic](./autofiller-retry-logic.cs) | AutoFiller PDF Merge with Retry Logic | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to use Aspose.Pdf's AutoFiller to bind a PDF template, import data from a DataTa... |
| [batch-fill-pdf-template-with-datatables](./batch-fill-pdf-template-with-datatables.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to fill a single PDF form template repeatedly using a list of DataTable objects,... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller Instance | `AutoFiller`, `BindPdf` | Shows how to create an AutoFiller object, bind a source PDF form from a file path, and prepare it... |
| [convert-filled-pdf-to-byte-array](./convert-filled-pdf-to-byte-array.cs) | Convert Filled PDF to Byte Array without Saving to Disk | `PdfViewer`, `BindPdf`, `Save` | Shows how to load a filled PDF using Aspose.Pdf.Facades.PdfViewer and save it directly to a Memor... |
| [convert-large-pdf-to-excel-with-temp-folder](./convert-large-pdf-to-excel-with-temp-folder.cs) | Convert Large PDF to Excel Using a Temporary Folder | `PdfFileEditor`, `Extract`, `Concatenate` | Demonstrates processing a large PDF by extracting each page to temporary files, concatenating the... |
| [encrypt-filled-pdf-with-password](./encrypt-filled-pdf-with-password.cs) | Encrypt Filled PDF with Password | `Document`, `Save`, `PdfFileSecurity` | Demonstrates how to protect a previously filled PDF by applying user and owner passwords using As... |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Extract PDF Pages with Custom File Names from DataTable | `Document`, `PdfFileEditor`, `Extract` | Shows how to extract specific pages from a multi‑page PDF and save each page as a separate PDF fi... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV using Aspose.Pdf | `Form`, `FillField`, `Save` | Demonstrates loading data from a CSV file into a DataTable and using Aspose.Pdf.Facades.Form to p... |
| [fill-pdf-form-from-datatable](./fill-pdf-form-from-datatable.cs) | Fill PDF Form from DataTable (Excel‑derived) | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to create a DataTable (simulating Excel column headers) and use Aspose.Pdf.Facades.Auto... |
| [fill-pdf-form-from-large-csv](./fill-pdf-form-from-large-csv.cs) | Fill PDF Form from Large CSV Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates loading a large CSV (converted from XLSX) row‑by‑row into a DataTable and using Aspo... |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream using AutoFiller | `Document`, `Save`, `Page` | Demonstrates loading a PDF template from a byte array, filling its form fields with data via Aspo... |
| [fill-pdf-form-from-multiple-csv-worksheets](./fill-pdf-form-from-multiple-csv-worksheets.cs) | Fill PDF Form from Multiple CSV Worksheets | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example reads each CSV file (representing an Excel worksheet) into a DataTable and uses Aspos... |
| [fill-pdf-form-from-xlsx-using-autofiller](./fill-pdf-form-from-xlsx-using-autofiller.cs) | Fill PDF Form from XLSX Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to create a DataTable (simulating XLSX data) and use Aspose.Pdf.Facades.AutoFiller to b... |
| [fill-pdf-form-using-csv-config-mapping](./fill-pdf-form-using-csv-config-mapping.cs) | Fill PDF Form Using CSV Data with Configurable Column Mappin... | `Form`, `FillField`, `Save` | Demonstrates loading a JSON mapping to translate CSV column names to PDF field names, reading the... |
| [fill-pdf-form-validate-size](./fill-pdf-form-validate-size.cs) | Fill PDF Form from DataTable and Validate File Size | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf AutoFiller to populate a PDF form from a DataTable, creating a temp... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form with AutoFiller and Proper Disposal | `Document`, `Page`, `Rectangle` | Shows how to bind a PDF template, import data from a DataTable, fill form fields using AutoFiller... |
| [fill-pdf-form-with-datatable-custom-columns](./fill-pdf-form-with-datatable-custom-columns.cs) | Fill PDF Form Using DataTable with Custom Columns | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates adding custom columns to a DataTable, binding it to a PDF template with Aspose.Pdf.F... |
| [fill-pdf-form-with-datatable-exception-handling](./fill-pdf-form-with-datatable-exception-handling.cs) | Fill PDF Form with DataTable and Handle Exceptions | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller to bind a template PDF, import a DataTable into form f... |
| [fill-pdf-form-with-timeout-cancellation](./fill-pdf-form-with-timeout-cancellation.cs) | Fill PDF Form with Timeout Cancellation | `Document`, `Form`, `SaveAsync` | Demonstrates how to fill fields in a PDF form using Aspose.Pdf and abort the operation with a can... |
| [format-numeric-values-fill-pdf-form](./format-numeric-values-fill-pdf-form.cs) | Format Numeric Values and Fill PDF Form Fields | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to format numeric DataTable values as strings and use Aspose.Pdf.Facades.AutoFiller to ... |
| [generate-pdf-per-datatable-row](./generate-pdf-per-datatable-row.cs) | Generate PDF with One Page per DataTable Row | `AutoFiller`, `Document` | Shows how to use Aspose.Pdf.Facades.AutoFiller to import a DataTable and create a merged PDF wher... |
| [generate-subset-pdf-from-filtered-datatable](./generate-subset-pdf-from-filtered-datatable.cs) | Generate Subset PDF from Filtered DataTable | `Document`, `Page`, `Table` | Creates a DataTable (simulating an exported Excel worksheet), filters rows by a column value, and... |
| [generate-summary-report-pdf-pages-datatable](./generate-summary-report-pdf-pages-datatable.cs) | Generate Summary Report Mapping PDF Pages to DataTable Rows | `Document`, `TableAbsorber`, `Visit` | Iterates through each PDF page, extracts tables with TableAbsorber, and writes a CSV that maps th... |
| [import-datatable-into-pdf-form](./import-datatable-into-pdf-form.cs) | Import DataTable with ReadOnly and Unique Columns into PDF F... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to configure DataColumn properties such as ReadOnly and Unique, then import the DataTab... |
| [import-datatable-into-pdf-form__v2](./import-datatable-into-pdf-form__v2.cs) | Import DataTable into PDF Form using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to bind a PDF form template, import a DataTable whose column names match AcroForm field... |
| [log-datatable-row-processing-auto-filler](./log-datatable-row-processing-auto-filler.cs) | Log DataTable Row Processing While Generating PDFs with Auto... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf.Facades.AutoFiller to fill a PDF template from a DataTable, create ... |
| [map-datatable-columns-to-pdf-fields](./map-datatable-columns-to-pdf-fields.cs) | Map DataTable Columns to PDF Field Names for AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to rename DataTable columns to match PDF form field identifiers and then use Asp... |
| ... | | | *and 5 more files* |

## Category Statistics
- Total examples: 35

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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

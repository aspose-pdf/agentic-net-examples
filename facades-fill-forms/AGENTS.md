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

- `using Aspose.Pdf.Facades;` (28/33 files) ← category-specific
- `using Aspose.Pdf;` (22/33 files) ← category-specific
- `using Aspose.Pdf.Forms;` (13/33 files)
- `using Aspose.Pdf.Text;` (2/33 files)
- `using System;` (33/33 files)
- `using System.Data;` (26/33 files) ← category-specific
- `using System.IO;` (22/33 files)
- `using System.Collections.Generic;` (3/33 files)
- `using System.Threading;` (3/33 files)
- `using System.Threading.Tasks;` (2/33 files)
- `using NUnit.Framework;` (1/33 files)
- `using System.IO.Compression;` (1/33 files)
- `using System.Linq;` (1/33 files)
- `using System.Text.Json;` (1/33 files)
- `using System.Xml.Linq;` (1/33 files)

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
| [async-fill-pdf-from-csv](./async-fill-pdf-from-csv.cs) | Asynchronously Fill PDF Form from CSV Data | `Document`, `ctor`, `SaveAsync` | Demonstrates how to read CSV data asynchronously, populate an Aspose.Pdf form using the Form faca... |
| [autofiller-pdf-merge-retry](./autofiller-pdf-merge-retry.cs) | AutoFiller PDF Merge with Retry on Transient I/O Errors | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller to bind a PDF template, import a DataTable, and save t... |
| [batch-fill-pdf-template](./batch-fill-pdf-template.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to use Aspose.Pdf's AutoFiller to fill a PDF form repeatedly for each DataTable in a li... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller and List Form Fields | `AutoFiller`, `BindPdf`, `Form` | Shows how to bind a PDF form to an Aspose.Pdf.Facades.AutoFiller instance and enumerate the form ... |
| [convert-filled-pdf-to-byte-array](./convert-filled-pdf-to-byte-array.cs) | Convert Filled PDF to Byte Array Using PdfViewer | `PdfViewer`, `BindPdf`, `Save` | Demonstrates loading a filled PDF with Aspose.Pdf.Facades.PdfViewer and returning its content as ... |
| [convert-pdf-to-excel-with-temp-folder](./convert-pdf-to-excel-with-temp-folder.cs) | Convert PDF to Excel Using Temporary Folder and Disk Buffer | `Document`, `PdfSaveOptions`, `PdfFileEditor` | Demonstrates converting a large PDF to an Excel file while using a dedicated temporary folder and... |
| [create-datatable-from-xlsx](./create-datatable-from-xlsx.cs) | Create DataTable from XLSX for PDF Form Filling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Reads the first worksheet of an XLSX file, uses the first row as column headers to build a DataTa... |
| [encrypt-pdf-with-user-owner-passwords](./encrypt-pdf-with-user-owner-passwords.cs) | Encrypt PDF with User and Owner Passwords | `PdfFileSecurity`, `BindPdf`, `EncryptFile` | Shows how to protect an existing filled PDF by applying user and owner passwords, setting privile... |
| [export-filter-import-pdf](./export-filter-import-pdf.cs) | Export XLSX Worksheet to DataTable, Filter Rows, and Import ... | `Document`, `ImportDataTable`, `Page` | Demonstrates creating a DataTable (simulating an exported Excel worksheet), filtering its rows, a... |
| [fill-pdf-acroform-from-csv](./fill-pdf-acroform-from-csv.cs) | Fill PDF AcroForm from CSV Data | `Form`, `FillField`, `Save` | Shows how to read a CSV file, map its columns to PDF AcroForm field names, fill the fields using ... |
| [fill-pdf-form-autofiller](./fill-pdf-form-autofiller.cs) | Fill PDF Form Using AutoFiller with DataColumnCollection | `Document`, `Page`, `TextBoxField` | Demonstrates creating a PDF with form fields, adding matching columns to a DataTable, and filling... |
| [fill-pdf-form-from-csv-mapping](./fill-pdf-form-from-csv-mapping.cs) | Fill PDF Form Fields from CSV Using a JSON Mapping | `Form`, `FillField`, `Save` | Shows how to read a CSV (Excel export), load a JSON column‑to‑PDF‑field mapping, and use Aspose.P... |
| [fill-pdf-form-from-datatable-with-logging](./fill-pdf-form-from-datatable-with-logging.cs) | Fill PDF Form from DataTable with Row Logging | `Document`, `PdfFileEditor`, `Form` | Demonstrates iterating over a DataTable, filling a PDF form template using Aspose.Pdf, saving eac... |
| [fill-pdf-form-memory](./fill-pdf-form-memory.cs) | Fill PDF Form from Memory Stream using AutoFiller | `Document`, `AutoFiller`, `DataTable` | Demonstrates loading a PDF from a memory stream, filling its form fields with data via AutoFiller... |
| [fill-pdf-form-with-datatable-exception-handling](./fill-pdf-form-with-datatable-exception-handling.cs) | Fill PDF Form with DataTable and Exception Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller to bind a PDF template, import data from a DataTable, ... |
| [fill-pdf-form-with-timeout-cancellation](./fill-pdf-form-with-timeout-cancellation.cs) | Fill PDF Form with Timeout Cancellation | `Document`, `Form`, `FillField` | Demonstrates how to fill a PDF form field using Aspose.Pdf and abort the operation if it exceeds ... |
| [fill-pdf-from-excel](./fill-pdf-from-excel.cs) | Fill PDF Form from Uploaded Excel using AutoFiller | `AutoFiller`, `Document`, `TextBoxField` | Demonstrates an ASP.NET Core controller that receives an XLSX file, creates a sample PDF template... |
| [fill-pdf-from-multiple-csv-worksheets](./fill-pdf-from-multiple-csv-worksheets.cs) | Fill PDF Form from Multiple CSV Worksheets | `AutoFiller`, `InputFileName`, `OutputFileName` | The example reads each CSV file in a folder as a separate worksheet, loads the data into a DataTa... |
| [fill-pdf-with-autofiller](./fill-pdf-with-autofiller.cs) | Fill PDF Form Using AutoFiller and Dispose Resources | `Document`, `AutoFiller`, `TextBoxField` | Creates a PDF with a form field, fills it using AutoFiller, saves the result, and disposes the Au... |
| [generate-pdf-page-summary-report](./generate-pdf-page-summary-report.cs) | Generate PDF Page Summary Report with DataTable Row IDs | `Document`, `Page`, `TableAbsorber` | Loads a PDF, extracts the first cell of the first table on each page as an identifier, optionally... |
| [generate-pdf-per-row](./generate-pdf-per-row.cs) | Generate Separate PDF Page per DataTable Row and Verify | `Document`, `Page`, `TextBoxField` | Creates a template PDF, fills it with data from a DataTable using AutoFiller to produce one PDF p... |
| [generate-pdfs-custom-filenames](./generate-pdfs-custom-filenames.cs) | Generate PDFs with Custom File Names from DataTable | `Document`, `Page`, `TextBoxField` | Creates a template PDF, fills a form field from a DataTable, and saves each document using a cust... |
| [import-datatable-autofiller](./import-datatable-autofiller.cs) | Import DataTable into AutoFiller to Fill PDF Form | `Document`, `TextBoxField`, `AutoFiller` | Demonstrates creating a PDF with a form field, populating a DataTable, and using AutoFiller to me... |
| [import-datatable-into-pdf-form](./import-datatable-into-pdf-form.cs) | Import DataTable into PDF Form with Column Settings | `Document`, `Page`, `TextBoxField` | Demonstrates setting DataColumn properties such as ReadOnly and Unique before importing the DataT... |
| [merge-filled-pdfs](./merge-filled-pdfs.cs) | Merge Multiple Filled PDFs into a Single Document | `Document`, `AutoFiller`, `Merge` | Creates a template PDF with a form field, fills it with data from separate DataTables to generate... |
| [populate-formatted-numeric-values](./populate-formatted-numeric-values.cs) | Populate PDF Form Fields with Formatted Numeric Values | `Document`, `TextBoxField`, `AutoFiller` | Creates a PDF with a text field, formats numeric values in a DataTable, and fills the field using... |
| [rename-datatable-columns](./rename-datatable-columns.cs) | Rename DataTable Columns to Match PDF Form Fields | `Document`, `TextBoxField`, `AutoFiller` | Demonstrates how to rename DataTable columns to match PDF form field names before using AutoFille... |
| [save-filled-pdf-preserve-layout](./save-filled-pdf-preserve-layout.cs) | Save Filled PDF While Preserving Original Layout | `Form`, `BindPdf`, `FillField` | Demonstrates how to bind an existing PDF, optionally fill its form fields, and save the document ... |
| [split-filled-pdf-into-single-page-pdfs](./split-filled-pdf-into-single-page-pdfs.cs) | Split Filled PDF into Single‑Page PDFs | `PdfFileEditor`, `SplitToPages` | Shows how to split a filled PDF into separate one‑page PDF files using Aspose.Pdf.Facades.PdfFile... |
| [stream-csv-to-pdf-auto-filler](./stream-csv-to-pdf-auto-filler.cs) | Stream CSV Data into PDF Form with AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates loading a large CSV file row‑by‑row into a DataTable and using Aspose.Pdf.Facades.Au... |
| ... | | | *and 3 more files* |

## Category Statistics
- Total examples: 33

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-fill-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-18 | Run: `20260618_032725_440ba6`
<!-- AUTOGENERATED:END -->

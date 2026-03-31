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

- `using Aspose.Pdf.Facades;` (26/32 files) ← category-specific
- `using Aspose.Pdf;` (23/32 files) ← category-specific
- `using Aspose.Pdf.Forms;` (8/32 files)
- `using Aspose.Pdf.Text;` (4/32 files)
- `using Aspose.Pdf.Annotations;` (1/32 files)
- `using Aspose.Pdf.Multithreading;` (1/32 files)
- `using System;` (32/32 files)
- `using System.IO;` (30/32 files)
- `using System.Data;` (24/32 files) ← category-specific
- `using System.Threading;` (2/32 files)
- `using System.Threading.Tasks;` (2/32 files)
- `using System.Linq;` (1/32 files)
- `using System.Runtime.InteropServices;` (1/32 files)

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
| [add-date-header-auto-filler](./add-date-header-auto-filler.cs) | Add Date Header to AutoFiller Generated PDF | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to use AutoFiller to generate a PDF from a template and then add a header with t... |
| [adjust-datatable-columns](./adjust-datatable-columns.cs) | Adjust DataTable Column Names for PDF Form Filling | `BindPdf`, `ImportDataTable`, `Save` | Demonstrates renaming DataTable columns to match PDF form field identifiers before using AutoFill... |
| [apply-pdf-password](./apply-pdf-password.cs) | Apply Password Protection to PDF Using PdfFileSecurity | `PdfFileSecurity`, `BindPdf`, `SetPrivilege` | Demonstrates how to protect an existing PDF with a user and owner password using Aspose.Pdf's Pdf... |
| [attach-xlsx-to-pdf](./attach-xlsx-to-pdf.cs) | Attach XLSX File to PDF Using Asynchronous I/O | `Document`, `Page`, `TextFragment` | Demonstrates reading an XLSX file asynchronously, embedding it as an attachment in a PDF, and wri... |
| [auto-filler-custom-filename](./auto-filler-custom-filename.cs) | Generate PDFs with Custom File Names Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to create separate PDF files from a template, naming each file based on a DataTa... |
| [autofiller-console-demo](./autofiller-console-demo.cs) | Fill PDF Template with DataTable using AutoFiller (Console D... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to use Aspose.Pdf.AutoFiller to import a DataTable and generate a filled PDF in ... |
| [autofiller-import-retry](./autofiller-import-retry.cs) | AutoFiller Import with Retry Mechanism | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to import a DataTable into a PDF form using AutoFiller with a retry mechanism fo... |
| [batch-fill-pdf-template](./batch-fill-pdf-template.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to use Aspose.Pdf.AutoFiller to fill a PDF template repeatedly with a list of DataTable... |
| [bind-pdf-form-autofiller](./bind-pdf-form-autofiller.cs) | Bind PDF Form to AutoFiller | `AutoFiller`, `BindPdf`, `Save` | Demonstrates how to bind a PDF form file to an AutoFiller instance and save the result. |
| [excel-to-datatable-pdf-form](./excel-to-datatable-pdf-form.cs) | Import Excel Data into PDF Form via DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Reads an XLSX worksheet, creates a DataTable using the first row as column headers, and fills a P... |
| [export-xlsx-filter-pdf](./export-xlsx-filter-pdf.cs) | Export XLSX to DataTable, Filter and Fill PDF | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates exporting data from an XLSX worksheet to a DataTable, filtering rows, and importing ... |
| [fill-pdf-form-autofiller](./fill-pdf-form-autofiller.cs) | Fill PDF Form Using AutoFiller with Custom DataTable Columns | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates adding custom columns to a DataTable and importing it into a PDF form using Aspose.P... |
| [fill-pdf-form-from-excel](./fill-pdf-form-from-excel.cs) | Fill PDF Form from Excel Data | `Form`, `BindPdf`, `FillField` | Loads a PDF form and an Excel file, fills the form fields with the first row of data, and saves t... |
| [fill-pdf-form-save](./fill-pdf-form-save.cs) | Fill PDF Form and Save Preserving Layout | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates filling a PDF form using a DataTable and saving the result while keeping the origina... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form with AutoFiller and Handle Missing Fields | `AutoFiller`, `BindPdf`, `FillField` | Demonstrates using AutoFiller to populate PDF form fields and handling errors when the input file... |
| [fill-pdf-form-with-mapping](./fill-pdf-form-with-mapping.cs) | Fill PDF Form Using Excel-to-PDF Field Mapping from Config | `AutoFiller`, `Document`, `DataTable` | Demonstrates reading a CSV (as Excel data), applying a mapping file to translate column names to ... |
| [fill-pdf-from-memory-stream](./fill-pdf-from-memory-stream.cs) | Fill PDF Form from Memory Stream using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Loads a PDF template from a memory stream, fills its form fields from a DataTable using AutoFille... |
| [fill-pdf-from-multiple-excel-worksheets](./fill-pdf-from-multiple-excel-worksheets.cs) | Fill PDF Template from Multiple Excel Worksheets using AutoF... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Reads each worksheet of an Excel file into a DataTable and uses Aspose.Pdf AutoFiller to generate... |
| [fill-pdf-with-autofiller](./fill-pdf-with-autofiller.cs) | Fill PDF Form Using AutoFiller and Dispose Resources | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates filling a PDF form from a DataTable using AutoFiller and properly disposing the Auto... |
| [fill-pdf-with-timeout](./fill-pdf-with-timeout.cs) | Fill PDF Form with Timeout Cancellation | `AutoFiller`, `InterruptMonitor`, `CancellationTokenSource` | Demonstrates filling a PDF form using AutoFiller and aborting the operation if it exceeds a speci... |
| [generate-separate-page-per-row](./generate-separate-page-per-row.cs) | Generate Separate PDF Page per DataTable Row and Verify | `AutoFiller`, `BindPdf`, `ImportDataTable` | Creates one PDF per DataTable row using AutoFiller, merges them, and checks that the merged docum... |
| [import-datatable-autofiller](./import-datatable-autofiller.cs) | Import DataTable into PDF Form using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to populate a DataTable and import it into an AcroForm PDF using Aspose.Pdf's Au... |
| [import-datatable-with-column-settings](./import-datatable-with-column-settings.cs) | Import DataTable with Column Settings into PDF Form | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates configuring DataColumn properties such as ReadOnly and Unique before importing a Dat... |
| [log-datatable-rows-to-pdf](./log-datatable-rows-to-pdf.cs) | Log DataTable Row Processing and Page Creation in PDF | `Document`, `Page`, `Table` | Shows how to iterate a DataTable, add each row to a PDF table, and log the processed row together... |
| [merge-filled-pdfs](./merge-filled-pdfs.cs) | Merge Multiple Filled PDFs into a Single Document | `AutoFiller`, `Merge`, `Save` | Fills a PDF template with several DataTables using AutoFiller, saves each filled PDF, and merges ... |
| [pdf-page-identifier-report](./pdf-page-identifier-report.cs) | Generate Summary Report of PDF Pages and DataTable Row Ident... | `Document`, `Page`, `TableAbsorber` | Extracts tables from each PDF page, reads the identifier from the first cell, matches it to a Dat... |
| [pdf-to-byte-array](./pdf-to-byte-array.cs) | Convert PDF to Byte Array for API Transmission | `Document`, `Save(Stream)` | Loads a filled PDF file and converts it to a byte array in memory without writing to disk, suitab... |
| [pdf-to-excel-temp-folder](./pdf-to-excel-temp-folder.cs) | Convert PDF to Excel Using a Temporary Folder for Intermedia... | `Document`, `ExcelSaveOptions`, `MemorySaveModePath` | Shows how to convert a PDF to an Excel workbook while configuring Aspose.Pdf to store intermediat... |
| [split-filled-pdf](./split-filled-pdf.cs) | Split Filled PDF into Single-Page PDFs | `SplitToPages` | Demonstrates how to split a filled PDF into separate one-page PDF files using Aspose.Pdf.Facades.... |
| [stream-xlsx-to-autofiller](./stream-xlsx-to-autofiller.cs) | Stream Large XLSX Data into AutoFiller for PDF Generation | `AutoFiller`, `OleDbConnection`, `DataTable` | Demonstrates reading a large XLSX file row by row and feeding batches into AutoFiller to generate... |
| ... | | | *and 2 more files* |

## Category Statistics
- Total examples: 32

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
Updated: 2026-03-31 | Run: `20260331_165923_676e8b`
<!-- AUTOGENERATED:END -->

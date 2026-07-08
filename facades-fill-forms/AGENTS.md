---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

> **Facades fill forms** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-fill-forms** category.
This folder contains standalone C# examples for facades-fill-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-fill-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (31/35 files) ← category-specific
- `using Aspose.Pdf;` (20/35 files) ← category-specific
- `using Aspose.Pdf.Forms;` (6/35 files)
- `using Aspose.Pdf.Text;` (5/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (34/35 files)
- `using System.Data;` (25/35 files) ← category-specific
- `using System.Collections.Generic;` (5/35 files)
- `using System.Threading;` (2/35 files)
- `using System.Threading.Tasks;` (2/35 files)
- `using NUnit.Framework;` (1/35 files)
- `using System.Globalization;` (1/35 files)
- `using System.Text;` (1/35 files)
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
| [add-date-header-to-pdf](./add-date-header-to-pdf.cs) | Add Date Header to PDF Using AutoFiller and PdfFileStamp | `AutoFiller`, `BindPdf`, `BindPdf` | Shows how to bind a PDF template, create a stamp, and add a header containing the current date to... |
| [add-text-watermark-to-pdf-pages](./add-text-watermark-to-pdf-pages.cs) | Add Text Watermark to PDF Pages | `FormattedText`, `BindLogo`, `IsBackground` | Demonstrates how to apply a semi‑transparent text watermark to every page of a PDF that has alrea... |
| [apply-password-protection-to-pdf](./apply-password-protection-to-pdf.cs) | Apply Password Protection to a PDF | `PdfFileSecurity`, `SetPrivilege`, `Print` | Shows how to protect a filled PDF with user and owner passwords using Aspose.Pdf.Facades after it... |
| [async-read-xlsx-write-pdf](./async-read-xlsx-write-pdf.cs) | Asynchronous XLSX Read and PDF Write | `Document`, `Page`, `TextFragment` | Demonstrates reading an XLSX file asynchronously into memory, creating a simple PDF with Aspose.P... |
| [batch-fill-pdf-forms-from-csv](./batch-fill-pdf-forms-from-csv.cs) | Batch Fill PDF Forms from Large CSV Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to stream a large CSV (converted from XLSX) in batches and use Aspose.Pdf.Facades.AutoF... |
| [batch-fill-pdf-template](./batch-fill-pdf-template.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to use Aspose.Pdf.Facades.AutoFiller to populate a PDF form repeatedly with different D... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller Instance | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to check for a PDF file, create an AutoFiller, bind the PDF to it, and prepare the inst... |
| [convert-filled-pdf-to-byte-array](./convert-filled-pdf-to-byte-array.cs) | Convert Filled PDF to Byte Array Using PdfViewer | `PdfViewer`, `BindPdf`, `Save` | Shows how to load a filled PDF from a file path or stream and obtain its contents as a byte array... |
| [convert-large-pdf-to-excel-with-temp-folder](./convert-large-pdf-to-excel-with-temp-folder.cs) | Convert Large PDF to Excel Using Temporary Folder | `Document`, `PdfSaveOptions`, `ExcelSaveOptions` | Demonstrates using a temporary directory for intermediate files while converting a large PDF to E... |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Extract PDF Pages with Custom Names from DataTable | `Document`, `Page`, `TextFragment` | Demonstrates how to split a source PDF into individual pages and save each page using a custom fi... |
| [fill-pdf-acroform-using-autofiller](./fill-pdf-acroform-using-autofiller.cs) | Fill PDF AcroForm Using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to bind a PDF template, import a populated DataTable, and automatically fill mat... |
| [fill-pdf-form-from-csv-mapping](./fill-pdf-form-from-csv-mapping.cs) | Fill PDF Form from CSV Using a Mapping Configuration | `Form`, `FillField`, `Save` | Demonstrates loading a CSV export of Excel, applying a JSON‑based column‑to‑PDF‑field map, and po... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV using Aspose.Pdf | `Form`, `FillField`, `Save` | Shows how to read field names and values from a CSV file and populate an existing PDF form with A... |
| [fill-pdf-form-from-excel-auto-filler](./fill-pdf-form-from-excel-auto-filler.cs) | Fill PDF Form from Excel Data using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to convert an uploaded XLSX file to a DataTable, bind it to a PDF template, and generat... |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream Using AutoFiller | `Document`, `AutoFiller`, `TextBoxField` | Demonstrates creating a PDF form template in memory, populating it with data from a DataTable, an... |
| [fill-pdf-form-using-autofiller](./fill-pdf-form-using-autofiller.cs) | Fill PDF Form Using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates binding a PDF template, importing data from a DataTable into form fields with AutoFi... |
| [fill-pdf-form-using-datatable](./fill-pdf-form-using-datatable.cs) | Fill PDF Form Using DataTable and AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to create a PDF form template, build a DataTable with matching columns, and use Aspose.... |
| [fill-pdf-form-with-autofiller-exception-handling](./fill-pdf-form-with-autofiller-exception-handling.cs) | Fill PDF Form with AutoFiller and Exception Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller to populate a PDF form from a DataTable and includes r... |
| [fill-pdf-form-with-timeout-cancellation](./fill-pdf-form-with-timeout-cancellation.cs) | Fill PDF Form with Timeout Cancellation | `Form`, `BindPdf`, `FillField` | Shows how to bind a PDF template, fill a form field, and save the document asynchronously while u... |
| [fill-pdf-template-with-multiple-csv-worksheets](./fill-pdf-template-with-multiple-csv-worksheets.cs) | Fill PDF Template with Multiple CSV Worksheets Using AutoFil... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Loads each CSV file into a DataTable, binds a PDF template, imports the table with AutoFiller, an... |
| [filter-datatable-fill-pdf-form](./filter-datatable-fill-pdf-form.cs) | Filter DataTable and Fill PDF Form with AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates loading a CSV into a DataTable, filtering rows based on a condition, and using Aspos... |
| [generate-pdf-page-summary](./generate-pdf-page-summary.cs) | Generate PDF Page Summary with DataTable Row IDs | `Document`, `TableAbsorber`, `Visit` | Demonstrates iterating through PDF pages with Aspose.Pdf, retrieving a matching DataTable row ide... |
| [import-datatable-into-pdf-form-with-constraints](./import-datatable-into-pdf-form-with-constraints.cs) | Import DataTable into PDF Form with Column Constraints | `Document`, `Page`, `TextBoxField` | Creates a PDF form template, configures a DataTable with ReadOnly and Unique column settings, and... |
| [load-csv-into-datatable](./load-csv-into-datatable.cs) | Load CSV into DataTable for Form Mapping |  | Shows how to read a CSV file, treat the first row as column headers, and populate a DataTable tha... |
| [log-datatable-rows-to-pdf](./log-datatable-rows-to-pdf.cs) | Log Processed DataTable Rows While Creating PDF Pages | `Document`, `Page`, `Table` | Shows how to iterate over a DataTable, add each row to its own PDF page with Aspose.Pdf, and log ... |
| [map-datatable-columns-to-pdf-fields](./map-datatable-columns-to-pdf-fields.cs) | Map DataTable Columns to PDF Form Fields and Fill the PDF | `Document`, `TextBoxField`, `Form` | Demonstrates how to rename DataTable columns to match PDF form field identifiers, verify the mapp... |
| [merge-filled-pdfs-from-datatables](./merge-filled-pdfs-from-datatables.cs) | Merge Multiple Filled PDFs from DataTables into a Single PDF | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to fill a PDF form template with data from several DataTables, save each filled ... |
| [pdf-one-page-per-datarow](./pdf-one-page-per-datarow.cs) | Create PDF with One Page per DataTable Row | `Document`, `Page`, `Table` | Demonstrates how to generate a PDF where each DataRow from a DataTable is placed on its own page ... |
| [populate-pdf-form-fields-with-formatted-numbers](./populate-pdf-form-fields-with-formatted-numbers.cs) | Populate PDF Form Fields with Formatted Numeric Values | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to format numeric and date values in a DataTable before importing them into PDF ... |
| [retry-autofiller-import-transient-file-access](./retry-autofiller-import-transient-file-access.cs) | Retry AutoFiller Import with Transient File Access Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller to bind a PDF template, import data from a DataTable, ... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-fill-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

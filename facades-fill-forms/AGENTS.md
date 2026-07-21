---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

> **Facades fill forms** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (20/35 files) ← category-specific
- `using Aspose.Pdf.Forms;` (10/35 files)
- `using Aspose.Pdf.Text;` (3/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (32/35 files)
- `using System.Data;` (26/35 files) ← category-specific
- `using System.Collections.Generic;` (3/35 files)
- `using System.Threading;` (3/35 files)
- `using System.Linq;` (2/35 files)
- `using System.Threading.Tasks;` (2/35 files)
- `using NUnit.Framework;` (1/35 files)
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
| [add-date-header-to-pdf-pages](./add-date-header-to-pdf-pages.cs) | Add Current Date Header to PDF Pages | `AutoFiller`, `BindPdf`, `Save` | Demonstrates how to fill a PDF template with AutoFiller and then add a header containing the curr... |
| [add-text-watermark-to-pdf-pages](./add-text-watermark-to-pdf-pages.cs) | Add Text Watermark to PDF Pages | `Document`, `PdfFileStamp`, `Stamp` | Shows how to apply a semi‑transparent text watermark to every page of a PDF after the form has be... |
| [adjust-datatable-columns-to-pdf-fields](./adjust-datatable-columns-to-pdf-fields.cs) | Adjust DataTable Columns to Match PDF Form Fields | `Form`, `AutoFiller`, `Document` | Demonstrates retrieving PDF form field names, renaming DataTable columns to match them (including... |
| [async-fill-pdf-from-xlsx](./async-fill-pdf-from-xlsx.cs) | Asynchronously Fill PDF Form from XLSX Data | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to read data from an XLSX file asynchronously, bind it to a PDF form using Aspos... |
| [batch-fill-pdf-form-data-tables](./batch-fill-pdf-form-data-tables.cs) | Batch Fill PDF Form Using DataTables | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF template, populating its form fields from multiple DataTable objects, ... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller | `AutoFiller`, `BindPdf` | Shows how to load a PDF form from a file path and bind it to an Aspose.Pdf.Facades.AutoFiller ins... |
| [cancel-pdf-form-filling-with-timeout](./cancel-pdf-form-filling-with-timeout.cs) | Cancel PDF Form Filling with Timeout | `Form`, `BindPdf`, `FillField` | Shows how to fill a PDF form using Aspose.Pdf and abort the operation if it exceeds a specified t... |
| [configure-datacolumn-properties-auto-fill-pdf-form](./configure-datacolumn-properties-auto-fill-pdf-form.cs) | Configure DataColumn Properties and AutoFill PDF Form | `Document`, `TextBoxField`, `Rectangle` | Demonstrates setting ReadOnly and Unique flags on DataTable columns before importing the table in... |
| [convert-large-pdf-to-excel-with-temp-disk-buffer](./convert-large-pdf-to-excel-with-temp-disk-buffer.cs) | Convert Large PDF to Excel Using Temporary Disk Buffer | `Document`, `PdfFileEditor`, `UseDiskBuffer` | Demonstrates extracting a large PDF to an intermediate file in a temporary folder with disk buffe... |
| [convert-pdf-to-byte-array](./convert-pdf-to-byte-array.cs) | Convert PDF to Byte Array In-Memory | `Document`, `Save`, `Pages` | Shows how to load a filled PDF with Aspose.Pdf and convert it to a byte array using a MemoryStrea... |
| [create-pdf-per-datarow](./create-pdf-per-datarow.cs) | Create PDF with One Page per DataTable Row | `Document`, `Page`, `TextFragment` | Generates a PDF where each row of a DataTable is placed on its own page and then verifies that th... |
| [fill-pdf-acroform-using-datatable](./fill-pdf-acroform-using-datatable.cs) | Fill PDF AcroForm Using DataTable with AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to bind a PDF template, import a DataTable whose column names match AcroForm fie... |
| [fill-pdf-form-from-csv-mapping](./fill-pdf-form-from-csv-mapping.cs) | Fill PDF Form Fields from CSV Using Mapping Config | `Form`, `FillField`, `Save` | Shows how to read a CSV (Excel) data file, load a JSON column‑to‑PDF‑field mapping, and dynamical... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form Fields from CSV Data | `Form`, `FillField`, `Save` | Shows how to read a CSV file into a DataTable and use Aspose.Pdf.Facades.Form to populate matchin... |
| [fill-pdf-form-from-excel-auto-filler](./fill-pdf-form-from-excel-auto-filler.cs) | Fill PDF Form from Excel Data using Aspose AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to bind a PDF template, import data from an Excel‑derived DataTable, and generat... |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream Using AutoFiller | `Document`, `Page`, `TextBoxField` | Shows how to create a PDF form entirely in memory, then populate its fields with data using Aspos... |
| [fill-pdf-form-using-datatable-autofiller](./fill-pdf-form-using-datatable-autofiller.cs) | Fill PDF Form Using DataTable and AutoFiller | `Document`, `Page`, `TextBoxField` | Demonstrates creating a PDF form template, defining matching columns in a DataTable, and using th... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form with AutoFiller and Proper Disposal | `Document`, `Page`, `TextBoxField` | Creates a PDF template with form fields, populates them from a DataTable using Aspose.Pdf.Facades... |
| [fill-pdf-form-with-datatable-exception-handling](./fill-pdf-form-with-datatable-exception-handling.cs) | Fill PDF Form with DataTable and Exception Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example shows how to use Aspose.Pdf's AutoFiller to bind a PDF template, import data from a D... |
| [fill-pdf-from-multiple-csv-worksheets](./fill-pdf-from-multiple-csv-worksheets.cs) | Fill PDF Template from Multiple CSV Worksheets | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example reads each CSV file in a folder, converts it to a DataTable, and uses Aspose.Pdf.Faca... |
| [filter-csv-and-fill-pdf-form](./filter-csv-and-fill-pdf-form.cs) | Filter CSV Data and Fill PDF Form with AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example loads a CSV file into a DataTable, filters rows based on a condition, and uses Aspose... |
| [generate-datatable-and-use-formdataconverter](./generate-datatable-and-use-formdataconverter.cs) | Create DataTable and Populate FormDataConverter | `FormDataConverter`, `DataType` | Shows how to build a DataTable using the first row as column headers (simulating an XLSX sheet) a... |
| [generate-pdf-pages-from-datatable-with-autofiller](./generate-pdf-pages-from-datatable-with-autofiller.cs) | Generate PDF Pages from DataTable with AutoFiller and Log Pa... | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf.Facades.AutoFiller to bind a PDF template, import a DataTable, crea... |
| [merge-filled-pdf-forms-from-datatables](./merge-filled-pdf-forms-from-datatables.cs) | Merge Multiple Filled PDF Forms from DataTables | `Document`, `Page`, `Rectangle` | Creates a PDF form template, fills it with data from several DataTables using AutoFiller, and con... |
| [pdf-page-table-summary-report](./pdf-page-table-summary-report.cs) | Create PDF Page‑to‑Row Identifier Summary Using TableAbsorbe... | `Document`, `TableAbsorber`, `Visit` | Demonstrates how to extract tables from each PDF page with Aspose.Pdf.Facades.TableAbsorber, read... |
| [populate-pdf-form-fields-from-datatable](./populate-pdf-form-fields-from-datatable.cs) | Populate PDF Form Fields from DataTable with Numeric Formatt... | `Document`, `Form`, `FillField` | Demonstrates loading a PDF template, iterating over a DataTable, formatting numeric and date valu... |
| [protect-pdf-with-password](./protect-pdf-with-password.cs) | Protect PDF with Password Using PdfFileSecurity | `PdfFileSecurity`, `EncryptFile`, `DocumentPrivilege` | Demonstrates how to encrypt an existing PDF with user and owner passwords, allowing printing, usi... |
| [retry-autofiller-import-transient-io](./retry-autofiller-import-transient-io.cs) | Retry AutoFiller Import on Transient I/O Errors | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to fill a PDF form with Aspose.Pdf.AutoFiller and implement a retry loop that handles t... |
| [save-filled-pdf-preserve-layout](./save-filled-pdf-preserve-layout.cs) | Save Filled PDF While Preserving Layout | `Form`, `FillField`, `Save` | Shows how to use Aspose.Pdf.Facades.Form to fill a form field and save the PDF to a new file, kee... |
| [split-filled-pdf-into-single-page-pdfs](./split-filled-pdf-into-single-page-pdfs.cs) | Split Filled PDF into Single‑Page PDFs | `PdfFileEditor`, `SplitToPages` | Shows how to split a filled PDF document into separate one‑page PDF files using Aspose.Pdf.Facades. |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

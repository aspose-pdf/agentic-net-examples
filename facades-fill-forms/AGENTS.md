---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

> **Facades fill forms** in PDF using C# / .NET -- **33** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-fill-forms** category.
This folder contains standalone C# examples for facades-fill-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-fill-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (31/33 files) ← category-specific
- `using Aspose.Pdf;` (22/33 files) ← category-specific
- `using Aspose.Pdf.Forms;` (12/33 files)
- `using Aspose.Pdf.Annotations;` (6/33 files)
- `using Aspose.Pdf.Text;` (5/33 files)
- `using Aspose.Pdf.Drawing;` (1/33 files)
- `using System;` (33/33 files)
- `using System.IO;` (30/33 files)
- `using System.Data;` (25/33 files) ← category-specific
- `using System.Collections.Generic;` (5/33 files)
- `using System.Threading;` (3/33 files)
- `using System.Text;` (2/33 files)
- `using System.Drawing;` (1/33 files)
- `using System.Linq;` (1/33 files)
- `using System.Text.Json;` (1/33 files)
- `using System.Threading.Tasks;` (1/33 files)

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
| [add-date-header-to-pdf-pages](./add-date-header-to-pdf-pages.cs) | Add Date Header to PDF Pages Using AutoFiller | `AutoFiller`, `PdfFileStamp`, `FormattedText` | Shows how to create a PDF template, fill it with data via AutoFiller, and then prepend a header c... |
| [apply-password-protection-to-filled-pdf](./apply-password-protection-to-filled-pdf.cs) | Apply Password Protection to a Filled PDF | `Document`, `PdfFileSecurity`, `EncryptFile` | Shows how to load a filled PDF, save a temporary copy, and encrypt it with user and owner passwor... |
| [async-fill-pdf-form-from-data-table](./async-fill-pdf-form-from-data-table.cs) | Asynchronously Fill PDF Form from DataTable using Aspose Aut... | `Document`, `Page`, `TextBoxField` | Demonstrates creating a simple PDF template, building a DataTable, and using Aspose.Pdf.Facades.A... |
| [batch-fill-pdf-forms-from-large-csv](./batch-fill-pdf-forms-from-large-csv.cs) | Batch Fill PDF Forms from Large CSV using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example streams rows from a large CSV (simulating an XLSX) in batches, builds a DataTable for... |
| [batch-fill-pdf-template-with-multiple-datatables](./batch-fill-pdf-template-with-multiple-datatables.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `GeneratingPath` | Shows how to use Aspose.Pdf's AutoFiller to fill a PDF form repeatedly for each DataTable in a li... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller | `AutoFiller`, `BindPdf` | Demonstrates how to create an AutoFiller instance, bind it to a PDF form file, and prepare it for... |
| [convert-in-memory-pdf-to-byte-array](./convert-in-memory-pdf-to-byte-array.cs) | Convert In-Memory PDF to Byte Array | `Document`, `Page`, `TextFragment` | Creates a PDF document entirely in memory with Aspose.Pdf, saves it to a MemoryStream, and return... |
| [convert-large-pdf-to-excel-with-temp-folder](./convert-large-pdf-to-excel-with-temp-folder.cs) | Convert Large PDF to Excel Using a Temporary Folder | `PdfFileEditor`, `Document`, `PdfSaveOptions` | Demonstrates how to convert a large PDF to Excel while using a temporary directory and disk buffe... |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Extract PDF Pages with Custom File Names from DataTable | `Document`, `PdfFileEditor`, `Extract` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to extract specific pages from a PDF and save... |
| [fill-pdf-form-field-preserve-layout](./fill-pdf-form-field-preserve-layout.cs) | Fill PDF Form Field and Preserve Layout | `Form`, `FillField`, `Save` | Demonstrates how to load a PDF with form fields, fill a specific field using the Aspose.Pdf.Facad... |
| [fill-pdf-form-from-csv-with-mapping](./fill-pdf-form-from-csv-with-mapping.cs) | Fill PDF Form Fields from CSV Using Mapping Configuration | `Form`, `BindPdf`, `FillField` | Demonstrates how to read a CSV file, map its column names to PDF form field names via a JSON conf... |
| [fill-pdf-form-from-csv-xlsx](./fill-pdf-form-from-csv-xlsx.cs) | Fill PDF Form from CSV/XLSX Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to read CSV (or CSV‑exported XLSX) data into a DataTable and use Aspose.Pdf.Facades.Aut... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV Data using Aspose.Pdf | `Form`, `ctor`, `FillField` | Demonstrates loading data from a CSV file into a DataTable and using Aspose.Pdf.Facades.Form to f... |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream using AutoFiller | `Document`, `AutoFiller`, `TextBoxField` | Creates a PDF form template in memory and fills it with data from a DataTable using Aspose.Pdf.Fa... |
| [fill-pdf-form-from-multiple-csv-worksheets](./fill-pdf-form-from-multiple-csv-worksheets.cs) | Fill PDF Form from Multiple CSV Worksheets using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example reads each CSV file as a worksheet, creates a DataTable, and uses Aspose.Pdf.Facades.... |
| [fill-pdf-form-using-autofiller-datatable](./fill-pdf-form-using-autofiller-datatable.cs) | Fill PDF Form Using AutoFiller and DataTable | `Document`, `Page`, `TextBoxField` | Demonstrates how to create a PDF template with AcroForm fields, populate a DataTable, and use Asp... |
| [fill-pdf-form-verify-size](./fill-pdf-form-verify-size.cs) | Fill PDF Form and Verify Output Size | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf.AutoFiller to populate a PDF form from a DataTable, creates a templ... |
| [fill-pdf-form-with-autofiller-exception-handling](./fill-pdf-form-with-autofiller-exception-handling.cs) | Fill PDF Form Using AutoFiller with Exception Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates binding a PDF template, importing data from a DataTable, and generating a filled PDF... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form Using AutoFiller with Custom DataTable Columns | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to create a DataTable, add required and custom columns, and use Aspose.Pdf.Facad... |
| [fill-pdf-form-with-autofiller__v2](./fill-pdf-form-with-autofiller__v2.cs) | Fill PDF Form Using AutoFiller and Dispose Resources | `AutoFiller`, `Document`, `Page` | Demonstrates creating a PDF form template, populating it from a DataTable with AutoFiller, and pr... |
| [fill-pdf-form-with-timeout](./fill-pdf-form-with-timeout.cs) | Fill PDF Form with Timeout Cancellation | `Form`, `BindPdf`, `FillField` | Shows how to fill a PDF form field using Aspose.Pdf's Form facade and abort the operation if it e... |
| [format-numeric-datatable-values-fill-pdf-form](./format-numeric-datatable-values-fill-pdf-form.cs) | Format Numeric DataTable Values and Fill PDF Form Fields | `Document`, `Page`, `Rectangle` | Shows how to format numeric values from a DataTable (e.g., currency) as strings and populate them... |
| [import-datatable-into-pdf-form-with-readonly-uniqu...](./import-datatable-into-pdf-form-with-readonly-unique.cs) | Import DataTable with ReadOnly and Unique Columns into PDF F... | `Document`, `Page`, `TextBoxField` | Demonstrates how to configure DataColumn properties such as ReadOnly and Unique, create matching ... |
| [load-csv-to-datatable-formdata-converter](./load-csv-to-datatable-formdata-converter.cs) | Load CSV into DataTable and Use FormDataConverter for PDF Fo... | `FormDataConverter` | Shows how to read a CSV (as a replacement for an XLSX) into a DataTable and assign it to Aspose.P... |
| [log-datatable-row-processing-pdf-page-creation](./log-datatable-row-processing-pdf-page-creation.cs) | Log DataTable Row Processing and PDF Page Creation | `Document`, `Page`, `TextBoxField` | Demonstrates creating a PDF template with form fields, filling it using AutoFiller from a DataTab... |
| [map-datatable-columns-to-pdf-form-fields](./map-datatable-columns-to-pdf-form-fields.cs) | Map DataTable Columns to PDF Form Fields with AutoFiller | `Document`, `Page`, `TextBoxField` | Demonstrates renaming DataTable columns to match PDF form field identifiers and filling the PDF u... |
| [merge-filled-pdfs-from-datatables](./merge-filled-pdfs-from-datatables.cs) | Merge Multiple Filled PDFs from DataTables into a Single PDF | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example creates a PDF form template from a DataTable, fills separate PDFs for each DataTable ... |
| [pdf-one-page-per-datarow](./pdf-one-page-per-datarow.cs) | Create PDF with One Page per DataTable Row | `Document`, `Page`, `Table` | Shows how to generate a PDF where each row of a DataTable is placed on a separate page using Aspo... |
| [pdf-page-identifier-summary-report](./pdf-page-identifier-summary-report.cs) | Generate PDF Page‑to‑Identifier Summary Report | `Document`, `Page`, `TableAbsorber` | The example opens a PDF, extracts a table identifier from each page using TableAbsorber, falls ba... |
| [retry-autofiller-import-transient-errors](./retry-autofiller-import-transient-errors.cs) | Retry AutoFiller Import with Transient File Access Handling | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf's AutoFiller within a retry loop to handle transient I/O and access... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

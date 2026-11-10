---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

> **Facades fill forms** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-fill-forms** category.
This folder contains standalone C# examples for facades-fill-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-fill-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (27/35 files) ← category-specific
- `using Aspose.Pdf.Facades;` (24/35 files) ← category-specific
- `using Aspose.Pdf.Forms;` (10/35 files)
- `using Aspose.Pdf.Text;` (7/35 files)
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (33/35 files)
- `using System.Data;` (21/35 files) ← category-specific
- `using System.Collections.Generic;` (10/35 files)
- `using System.Linq;` (7/35 files)
- `using System.Globalization;` (2/35 files)
- `using System.Threading;` (2/35 files)
- `using System.Threading.Tasks;` (2/35 files)
- `using System.Text.Json;` (1/35 files)
- `using System.Xml.Linq;` (1/35 files)

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
| [add-date-header-to-pdf-pages](./add-date-header-to-pdf-pages.cs) | Add Current Date Header to Filled PDF Form | `Document`, `Form`, `TextFragment` | Shows how to fill a PDF form with XML data using the Form facade and then add a header containing... |
| [add-watermark-to-pdf-after-form-filling](./add-watermark-to-pdf-after-form-filling.cs) | Add Watermark to PDF After Form Filling | `Document`, `TextStamp`, `FontRepository` | Shows how to load a PDF that has been populated by AutoFiller, create a semi‑transparent text sta... |
| [apply-password-protection-to-filled-pdf](./apply-password-protection-to-filled-pdf.cs) | Apply Password Protection to a Filled PDF | `Document`, `Encrypt`, `Field` | Fills a form field in an existing PDF and then encrypts the document with user and owner password... |
| [async-fill-pdf-form-from-data-table](./async-fill-pdf-form-from-data-table.cs) | Asynchronously Fill PDF Form from Excel Data | `Document`, `Form`, `Field` | Demonstrates loading field values from an XLSX file using asynchronous I/O, filling an Aspose.PDF... |
| [batch-fill-pdf-forms-from-large-csv](./batch-fill-pdf-forms-from-large-csv.cs) | Generate PDFs from Large CSV Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates streaming a CSV file row‑by‑row, creating a one‑row DataTable for each record, and u... |
| [batch-fill-pdf-template-with-multiple-datatables](./batch-fill-pdf-template-with-multiple-datatables.cs) | Batch Fill PDF Form Using Multiple DataTables | `Document`, `Form`, `Field` | Demonstrates loading a PDF form template and populating its fields with data from a list of DataT... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Fill PDF Form Fields Using Aspose.Pdf | `Document`, `Save`, `TextBoxField` | Shows how to load a PDF, locate specific form fields (text box and checkbox), set their values, a... |
| [convert-in-memory-pdf-to-byte-array](./convert-in-memory-pdf-to-byte-array.cs) | Convert Filled PDF to Byte Array | `Document`, `Save` | Loads a filled PDF using Aspose.Pdf and saves it directly to a MemoryStream, returning the PDF co... |
| [convert-large-pdf-to-excel-with-temp-folder](./convert-large-pdf-to-excel-with-temp-folder.cs) | Convert Large Excel to PDF with Temporary Folder | `Document`, `PdfFileEditor`, `Save` | Shows how to split a large Excel workbook into intermediate PDF files stored in a temporary folde... |
| [export-filtered-datatable-to-pdf-merge-template](./export-filtered-datatable-to-pdf-merge-template.cs) | Export Filtered DataTable to PDF and Merge with Template | `Document`, `Page`, `Table` | Creates a DataTable, filters rows, builds a PDF table from the filtered data, and optionally merg... |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Generate Separate PDFs with Custom Names from DataTable Rows | `Document`, `Page`, `TextFragment` | Demonstrates creating a combined PDF where each DataTable row adds a page with its value as text,... |
| [fill-pdf-form-field-preserve-layout](./fill-pdf-form-field-preserve-layout.cs) | Fill PDF Form and Preserve Layout | `Form`, `BindPdf`, `FillField` | Shows how to load an existing PDF, fill its form fields using the Form facade, and save the updat... |
| [fill-pdf-form-from-csv-with-mapping](./fill-pdf-form-from-csv-with-mapping.cs) | Fill PDF Form Fields from CSV Using a JSON Mapping | `Form`, `BindPdf`, `FillField` | Demonstrates how to read data from a CSV file, map column names to PDF form fields via a JSON con... |
| [fill-pdf-form-from-csv-xlsx](./fill-pdf-form-from-csv-xlsx.cs) | Fill PDF Form Using Aspose AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to use Aspose.Pdf.Facades.AutoFiller to bind a PDF template, import data from a ... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV using Aspose AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to read CSV data into a DataTable and use Aspose.Pdf.Facades.AutoFiller to bind a PDF t... |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream Using AutoFiller | `Document`, `AutoFiller`, `BindPdf` | Demonstrates loading a PDF into a MemoryStream, binding it to Aspose.Pdf.Facades.AutoFiller, impo... |
| [fill-pdf-form-from-multiple-csv-worksheets](./fill-pdf-form-from-multiple-csv-worksheets.cs) | Fill PDF Form from Multiple DataTables (One per Sheet) | `Document`, `Save`, `Form` | Shows how to load a PDF template, iterate over several in‑memory DataTable objects that represent... |
| [fill-pdf-form-using-autofiller-datatable](./fill-pdf-form-using-autofiller-datatable.cs) | Fill PDF AcroForm Using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates how to populate a DataTable and import it into Aspose.Pdf's AutoFiller to automatica... |
| [fill-pdf-form-verify-size](./fill-pdf-form-verify-size.cs) | Fill PDF Form from DataTable and Validate File Size | `Document`, `TextBoxField`, `PdfFileEditor` | Demonstrates filling a PDF form using a DataTable, optionally appending pages from another PDF, a... |
| [fill-pdf-form-with-autofiller-exception-handling](./fill-pdf-form-with-autofiller-exception-handling.cs) | Fill PDF Form Fields with Error Handling | `Document`, `TextBoxField`, `CheckboxField` | Demonstrates loading a PDF template, populating its form fields from an XML data source, and usin... |
| [fill-pdf-form-with-autofiller](./fill-pdf-form-with-autofiller.cs) | Fill PDF Form from CSV using AutoFiller and DataTable | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates mapping CSV data to PDF form fields by creating a DataTable with column names matchi... |
| [fill-pdf-form-with-autofiller__v2](./fill-pdf-form-with-autofiller__v2.cs) | Fill PDF Form Fields and Save with Aspose.Pdf | `Document`, `TextBoxField`, `Form` | Demonstrates loading a PDF, locating text box form fields, assigning values, and saving the fille... |
| [fill-pdf-form-with-timeout](./fill-pdf-form-with-timeout.cs) | Fill PDF Form with Cancellation Token and Timeout | `Document`, `WidgetAnnotation`, `Field` | Demonstrates how to populate form fields in a PDF using Aspose.Pdf and abort the operation if it ... |
| [format-numeric-datatable-values-fill-pdf-form](./format-numeric-datatable-values-fill-pdf-form.cs) | Populate PDF Form Fields from DataTable with Numeric Formatt... | `Document`, `Form`, `HasField` | Shows how to load a PDF template, format numeric and date values from a DataTable, and fill match... |
| [import-datatable-into-pdf-form-with-readonly-uniqu...](./import-datatable-into-pdf-form-with-readonly-unique.cs) | Import DataTable with Column Constraints into PDF Form | `Document`, `AutoFiller`, `BindPdf` | Shows how to configure DataColumn properties such as ReadOnly and Unique before importing a DataT... |
| [load-csv-to-datatable-formdata-converter](./load-csv-to-datatable-formdata-converter.cs) | Create Sample DataTable and Illustrate PdfConverter Usage | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to build a DataTable with sample data and includes a placeholder demonstrating the use ... |
| [log-datatable-row-processing-pdf-page-creation](./log-datatable-row-processing-pdf-page-creation.cs) | Log Row Processing and Create PDF Pages per DataTable Row | `Document`, `Page`, `TextFragment` | The example iterates through a DataTable, creates a separate PDF page for each row using Aspose.P... |
| [map-datatable-columns-to-pdf-form-fields](./map-datatable-columns-to-pdf-form-fields.cs) | Map DataTable Columns to PDF Form Fields and Fill PDF | `Form`, `AutoFiller`, `FieldNames` | Shows how to align DataTable column names with PDF form field identifiers and use Aspose.Pdf.Faca... |
| [merge-filled-pdfs-from-datatables](./merge-filled-pdfs-from-datatables.cs) | Merge Multiple PDFs into a Single Document | `Document`, `Pages`, `Add` | Demonstrates how to combine several PDF files generated from DataTables into one consolidated PDF... |
| [pdf-one-page-per-datarow](./pdf-one-page-per-datarow.cs) | Create PDF with One Page per DataTable Row | `Document`, `Page`, `TextFragment` | Demonstrates generating a PDF where each row of a DataTable is placed on its own page and verifie... |
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
Updated: 2026-11-10 | Run: `20261110_014525_797643`
<!-- AUTOGENERATED:END -->

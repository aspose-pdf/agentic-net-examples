---
name: facades-fill-forms
description: C# examples for facades-fill-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-fill-forms

> **Facades fill forms** in PDF using C# / .NET -- **61** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-fill-forms** category.
This folder contains standalone C# examples for facades-fill-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-fill-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (31/61 files) ← category-specific
- `using Aspose.Pdf;` (22/61 files)
- `using Aspose.Pdf.Forms;` (12/61 files)
- `using Aspose.Pdf.Annotations;` (6/61 files)
- `using Aspose.Pdf.Text;` (5/61 files)
- `using Aspose.Pdf.Drawing;` (1/61 files)
- `using System;` (33/61 files)
- `using System.IO;` (30/61 files)
- `using System.Data;` (25/61 files)
- `using System.Collections.Generic;` (5/61 files)
- `using System.Threading;` (3/61 files)
- `using System.Text;` (2/61 files)
- `using System.Drawing;` (1/61 files)
- `using System.Linq;` (1/61 files)
- `using System.Text.Json;` (1/61 files)
- `using System.Threading.Tasks;` (1/61 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-date-header-to-pdf-pages](./add-date-header-to-pdf-pages.cs) | Add Date Header to PDF Pages Using AutoFiller | `AutoFiller`, `PdfFileStamp`, `FormattedText` | Shows how to create a PDF template, fill it with data via AutoFiller, and then prepend a header c... |
| [add-text-watermark-to-pdf-pages](./add-text-watermark-to-pdf-pages.cs) | Add text watermark to pdf pages |  | Add text watermark to pdf pages |
| [adjust-datatable-columns-to-pdf-fields](./adjust-datatable-columns-to-pdf-fields.cs) | Adjust datatable columns to pdf fields |  | Adjust datatable columns to pdf fields |
| [apply-password-protection-to-filled-pdf](./apply-password-protection-to-filled-pdf.cs) | Apply Password Protection to a Filled PDF | `Document`, `PdfFileSecurity`, `EncryptFile` | Shows how to load a filled PDF, save a temporary copy, and encrypt it with user and owner passwor... |
| [async-fill-pdf-form-from-data-table](./async-fill-pdf-form-from-data-table.cs) | Asynchronously Fill PDF Form from DataTable using Aspose Aut... | `Document`, `Page`, `TextBoxField` | Demonstrates creating a simple PDF template, building a DataTable, and using Aspose.Pdf.Facades.A... |
| [async-fill-pdf-from-xlsx](./async-fill-pdf-from-xlsx.cs) | Async fill pdf from xlsx |  | Async fill pdf from xlsx |
| [batch-fill-pdf-form-data-tables](./batch-fill-pdf-form-data-tables.cs) | Batch fill pdf form data tables |  | Batch fill pdf form data tables |
| [batch-fill-pdf-forms-from-large-csv](./batch-fill-pdf-forms-from-large-csv.cs) | Batch Fill PDF Forms from Large CSV using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example streams rows from a large CSV (simulating an XLSX) in batches, builds a DataTable for... |
| [batch-fill-pdf-template-with-multiple-datatables](./batch-fill-pdf-template-with-multiple-datatables.cs) | Batch Fill PDF Template with Multiple DataTables | `AutoFiller`, `BindPdf`, `GeneratingPath` | Shows how to use Aspose.Pdf's AutoFiller to fill a PDF form repeatedly for each DataTable in a li... |
| [bind-pdf-form-to-autofiller](./bind-pdf-form-to-autofiller.cs) | Bind PDF Form to AutoFiller | `AutoFiller`, `BindPdf` | Demonstrates how to create an AutoFiller instance, bind it to a PDF form file, and prepare it for... |
| [cancel-pdf-form-filling-with-timeout](./cancel-pdf-form-filling-with-timeout.cs) | Cancel pdf form filling with timeout |  | Cancel pdf form filling with timeout |
| [configure-datacolumn-properties-auto-fill-pdf-form](./configure-datacolumn-properties-auto-fill-pdf-form.cs) | Configure datacolumn properties auto fill pdf form |  | Configure datacolumn properties auto fill pdf form |
| [convert-in-memory-pdf-to-byte-array](./convert-in-memory-pdf-to-byte-array.cs) | Convert In-Memory PDF to Byte Array | `Document`, `Page`, `TextFragment` | Creates a PDF document entirely in memory with Aspose.Pdf, saves it to a MemoryStream, and return... |
| [convert-large-pdf-to-excel-with-temp-disk-buffer](./convert-large-pdf-to-excel-with-temp-disk-buffer.cs) | Convert large pdf to excel with temp disk buffer |  | Convert large pdf to excel with temp disk buffer |
| [convert-large-pdf-to-excel-with-temp-folder](./convert-large-pdf-to-excel-with-temp-folder.cs) | Convert Large PDF to Excel Using a Temporary Folder | `PdfFileEditor`, `Document`, `PdfSaveOptions` | Demonstrates how to convert a large PDF to Excel while using a temporary directory and disk buffe... |
| [convert-pdf-to-byte-array](./convert-pdf-to-byte-array.cs) | Convert pdf to byte array |  | Convert pdf to byte array |
| [create-pdf-per-datarow](./create-pdf-per-datarow.cs) | Create pdf per datarow |  | Create pdf per datarow |
| [extract-pdf-pages-with-custom-names](./extract-pdf-pages-with-custom-names.cs) | Extract PDF Pages with Custom File Names from DataTable | `Document`, `PdfFileEditor`, `Extract` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to extract specific pages from a PDF and save... |
| [fill-pdf-acroform-using-datatable](./fill-pdf-acroform-using-datatable.cs) | Fill pdf acroform using datatable |  | Fill pdf acroform using datatable |
| [fill-pdf-form-field-preserve-layout](./fill-pdf-form-field-preserve-layout.cs) | Fill PDF Form Field and Preserve Layout | `Form`, `FillField`, `Save` | Demonstrates how to load a PDF with form fields, fill a specific field using the Aspose.Pdf.Facad... |
| [fill-pdf-form-from-csv-mapping](./fill-pdf-form-from-csv-mapping.cs) | Fill pdf form from csv mapping |  | Fill pdf form from csv mapping |
| [fill-pdf-form-from-csv-with-mapping](./fill-pdf-form-from-csv-with-mapping.cs) | Fill PDF Form Fields from CSV Using Mapping Configuration | `Form`, `BindPdf`, `FillField` | Demonstrates how to read a CSV file, map its column names to PDF form field names via a JSON conf... |
| [fill-pdf-form-from-csv-xlsx](./fill-pdf-form-from-csv-xlsx.cs) | Fill PDF Form from CSV/XLSX Using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | Shows how to read CSV (or CSV‑exported XLSX) data into a DataTable and use Aspose.Pdf.Facades.Aut... |
| [fill-pdf-form-from-csv](./fill-pdf-form-from-csv.cs) | Fill PDF Form from CSV Data using Aspose.Pdf | `Form`, `ctor`, `FillField` | Demonstrates loading data from a CSV file into a DataTable and using Aspose.Pdf.Facades.Form to f... |
| [fill-pdf-form-from-excel-auto-filler](./fill-pdf-form-from-excel-auto-filler.cs) | Fill pdf form from excel auto filler |  | Fill pdf form from excel auto filler |
| [fill-pdf-form-from-memory-stream](./fill-pdf-form-from-memory-stream.cs) | Fill PDF Form from Memory Stream using AutoFiller | `Document`, `AutoFiller`, `TextBoxField` | Creates a PDF form template in memory and fills it with data from a DataTable using Aspose.Pdf.Fa... |
| [fill-pdf-form-from-multiple-csv-worksheets](./fill-pdf-form-from-multiple-csv-worksheets.cs) | Fill PDF Form from Multiple CSV Worksheets using AutoFiller | `AutoFiller`, `BindPdf`, `ImportDataTable` | The example reads each CSV file as a worksheet, creates a DataTable, and uses Aspose.Pdf.Facades.... |
| [fill-pdf-form-using-autofiller-datatable](./fill-pdf-form-using-autofiller-datatable.cs) | Fill PDF Form Using AutoFiller and DataTable | `Document`, `Page`, `TextBoxField` | Demonstrates how to create a PDF template with AcroForm fields, populate a DataTable, and use Asp... |
| [fill-pdf-form-using-datatable-autofiller](./fill-pdf-form-using-datatable-autofiller.cs) | Fill pdf form using datatable autofiller |  | Fill pdf form using datatable autofiller |
| [fill-pdf-form-verify-size](./fill-pdf-form-verify-size.cs) | Fill PDF Form and Verify Output Size | `AutoFiller`, `BindPdf`, `ImportDataTable` | Demonstrates using Aspose.Pdf.AutoFiller to populate a PDF form from a DataTable, creates a templ... |
| ... | | | *and 31 more files* |

## Category Statistics
- Total examples: 61

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

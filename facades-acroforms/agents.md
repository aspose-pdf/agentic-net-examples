---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-acroforms** category.
This folder contains standalone C# examples for facades-acroforms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-acroforms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/42 files) ← category-specific
- `using Aspose.Pdf;` (13/42 files)
- `using Aspose.Pdf.Forms;` (4/42 files)
- `using Aspose.Pdf.Drawing;` (1/42 files)
- `using Aspose.Pdf.Text;` (1/42 files)
- `using System;` (42/42 files)
- `using System.IO;` (40/42 files)
- `using System.Collections.Generic;` (11/42 files)
- `using System.Text.Json;` (7/42 files)
- `using System.Xml;` (3/42 files)
- `using System.Linq;` (2/42 files)
- `using System.Text;` (2/42 files)
- `using System.Threading.Tasks;` (2/42 files)
- `using Newtonsoft.Json;` (1/42 files)
- `using Newtonsoft.Json.Linq;` (1/42 files)
- `using System.Collections;` (1/42 files)
- `using System.Threading;` (1/42 files)
- `using System.Xml.Schema;` (1/42 files)
- `using System.Xml.Xsl;` (1/42 files)

## Common Code Pattern

Most files in this category use `Form` from `Aspose.Pdf.Facades`:

```csharp
Form tool = new Form();
tool.BindPdf("input.pdf");
// ... Form operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-radio-button-group-with-default-selection](./add-radio-button-group-with-default-selection.cs) | Add Radio Button Group with Default Selection using FormEdit... | `FormEditor`, `AddField`, `Save` | Demonstrates how to add a radio button group with three options to a PDF and set a default select... |
| [add-text-field-to-pdf-using-formeditor](./add-text-field-to-pdf-using-formeditor.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `AddField`, `Save` | Demonstrates how to insert a new text field at specific coordinates on a PDF page using Aspose.Pd... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates how to export AcroForm fields from a PDF to an XML file using Aspose.Pdf's Form faca... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `Form`, `BindPdf`, `ExportXml` | Shows how to iterate over multiple PDF files, bind each with the Form facade, and export its Acro... |
| [batch-import-json-form-data](./batch-import-json-form-data.cs) | Batch Import Form Data from JSON Files into PDFs in Parallel | `Document`, `Form`, `Rectangle` | Creates sample PDFs with form fields, generates matching JSON files, and imports the JSON data in... |
| [combine-exported-json-from-multiple-pdfs](./combine-exported-json-from-multiple-pdfs.cs) | Combine Exported JSON from Multiple PDFs into a Single JSON ... | `Form`, `ExportJson` | Shows how to export AcroForm data from several PDF files to JSON, merge the individual JSON objec... |
| [export-acroform-data-to-xml-memorystream](./export-acroform-data-to-xml-memorystream.cs) | Export AcroForm Data to XML Using MemoryStream | `Form`, `ExportXml` | Demonstrates how to export PDF form field data to an XML string using Aspose.Pdf's Form facade an... |
| [export-customer-form-fields-to-xml](./export-customer-form-fields-to-xml.cs) | Export Customer Form Fields to XML | `Form`, `FieldNames`, `GetField` | Shows how to filter AcroForm fields whose names start with "Customer" and write their values to a... |
| [export-import-acroform-xfdf-roundtrip](./export-import-acroform-xfdf-roundtrip.cs) | Export and Import AcroForm Data via XFDF (Round‑Trip Verific... | `Form`, `FillField`, `ExportXfdf` | The example fills an AcroForm PDF with test values, exports the field data to an XFDF file, then ... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `ExportFdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all form field values from a PDF docume... |
| [export-pdf-form-data-to-html](./export-pdf-form-data-to-html.cs) | Export PDF Form Data to XML and Convert to HTML with XSLT | `Form`, `ExportXml` | Demonstrates exporting AcroForm field values from a PDF to an XML file using Aspose.Pdf.Facades.F... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON and Validate | `Form`, `ExportJson` | Demonstrates how to export all AcroForm field values from a PDF into an indented JSON file using ... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all form field values from a PDF docume... |
| [export-pdf-form-fields-to-json-chunks](./export-pdf-form-fields-to-json-chunks.cs) | Export PDF Form Fields to JSON in 100-Field Chunks | `Form`, `ExportJson` | Loads a PDF form with Aspose.Pdf.Facades.Form, exports all form fields to a JSON stream, and spli... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Deserialize | `Form`, `ExportJson` | Shows how to export AcroForm fields from a PDF to indented JSON using Aspose.Pdf.Facades and then... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Shows how to export all form field values from a PDF document to an XFDF file using the Aspose.Pd... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `ExportFieldsToJsonOptions`, `ExportFieldsOptions` | Demonstrates exporting only specific form fields from a PDF to a JSON file using Aspose.Pdf's Exp... |
| [export-text-form-field-names-to-json](./export-text-form-field-names-to-json.cs) | Export Text Form Field Names to JSON | `Form`, `FieldNames`, `GetFieldType` | The example opens a PDF, iterates through its AcroForm fields, selects only text fields, and writ... |
| [extract-pdf-acroform-field-names](./extract-pdf-acroform-field-names.cs) | Extract PDF AcroForm Field Names to JSON | `Form`, `FieldNames` | Shows how to open a PDF using Aspose.Pdf.Facades.Form, read all AcroForm field names, and output ... |
| [fill-acroform-fields-from-byte-array](./fill-acroform-fields-from-byte-array.cs) | Fill AcroForm Fields from Byte Array | `Form`, `BindPdf`, `FillField` | Loads a PDF from a byte array, fills specified AcroForm fields using the Form facade, and returns... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF AcroForm Fields with Aspose.Pdf.Facades | `Form`, `FillField`, `Save` | Shows how to open a PDF, populate its AcroForm fields from a dictionary, and save the updated fil... |
| [fill-pdf-form-fields-from-json](./fill-pdf-form-fields-from-json.cs) | Fill PDF Form Fields from JSON using Aspose.Pdf | `Form`, `FillField`, `Save` | Demonstrates how to load a JSON file, map its keys to AcroForm field names, and populate a PDF fo... |
| [fill-textbox-field-in-pdf](./fill-textbox-field-in-pdf.cs) | Fill TextBox Field in PDF Using Aspose.Pdf Form Facade | `Form`, `FillField`, `Save` | Demonstrates how to use the Aspose.Pdf.Facades.Form class to fill a specific AcroForm text box fi... |
| [generate-pdf-form-fields-report](./generate-pdf-form-fields-report.cs) | Generate PDF Form Fields Report | `Document`, `FormEditor`, `Field` | Creates a PDF document that lists all AcroForm fields from an input PDF, displaying each field's ... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Demonstrates how to read the currently selected option of a radio button group in an AcroForm usi... |
| [import-fdf-data-into-pdf](./import-fdf-data-into-pdf.cs) | Import FDF Data into PDF using Form Facade | `Form`, `ImportFdf`, `Save` | Shows how to import form data from an FDF file into an existing PDF and save the updated document... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Shows how to open a PDF with AcroForm fields, import field values from an XML file using the Form... |
| [import-json-data-into-pdf-acroform](./import-json-data-into-pdf-acroform.cs) | Import JSON Data into PDF AcroForm with Aspose.Pdf | `Form`, `ImportJson`, `Save` | Shows how to serialize a C# object to JSON, load it into a MemoryStream, and import the data into... |
| [import-json-data-into-pdf-form-with-missing-field-...](./import-json-data-into-pdf-form-with-missing-field-handling.cs) | Import JSON Data into PDF Form with Missing Field Handling | `Form`, `ImportJson`, `Save` | Demonstrates importing form field values from a JSON file into a PDF using Aspose.Pdf.Facades.For... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form While Ignoring Missing Fields | `Form`, `ImportJson`, `FieldNames` | Shows how to read a PDF form, filter a JSON payload to keep only fields present in the PDF, and i... |
| ... | | | *and 12 more files* |

## Category Statistics
- Total examples: 42

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_013009_d919e8`
<!-- AUTOGENERATED:END -->

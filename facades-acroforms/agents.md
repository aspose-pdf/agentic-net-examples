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

- `using Aspose.Pdf.Facades;` (36/41 files) ← category-specific
- `using Aspose.Pdf;` (13/41 files)
- `using Aspose.Pdf.Forms;` (4/41 files)
- `using Aspose.Pdf.Annotations;` (1/41 files)
- `using System;` (40/41 files)
- `using System.IO;` (39/41 files)
- `using System.Collections.Generic;` (9/41 files)
- `using System.Text.Json;` (5/41 files)
- `using System.Text;` (4/41 files)
- `using System.Linq;` (3/41 files)
- `using System.Threading.Tasks;` (2/41 files)
- `using System.Xml;` (2/41 files)
- `using Newtonsoft.Json;` (1/41 files)
- `using System.Collections;` (1/41 files)
- `using System.Threading;` (1/41 files)
- `using System.Xml.Linq;` (1/41 files)
- `using System.Xml.Schema;` (1/41 files)
- `using System.Xml.Xsl;` (1/41 files)

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
| [add-radio-button-group-default-selection](./add-radio-button-group-default-selection.cs) | Add Radio Button Group with Default Selection using FormEdit... | `FormEditor`, `AddField`, `Save` | Demonstrates how to create a radio button group with three options, arrange them, and set a defau... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `AddField`, `Save` | Shows how to insert a new text field at given coordinates on a specific page of a PDF using Aspos... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF document to an XML file using async/await, prevent... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `FormEditor`, `BindPdf`, `Form` | Shows how to iterate over several PDF files, load each with FormEditor, and export the AcroForm f... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDFs in Parallel | `Document`, `Form`, `ImportJson` | Demonstrates how to import form data from matching JSON files into PDF documents using Aspose.Pdf... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine AcroForm JSON Exports from Multiple PDFs | `Form`, `ExportJson` | Demonstrates how to export form data from several PDF files to JSON, merge the fragments into a s... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `ExportFdf` | Demonstrates how to export form field values from a PDF document to an FDF file using the Aspose.... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON and Deserialize with Json.NET | `Form`, `ExportJson` | Demonstrates how to export AcroForm fields from a PDF to a JSON string using Aspose.Pdf.Facades.F... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML | `Form`, `ExportXml` | Shows how to export all AcroForm field values from a PDF document to an XML file using the Form f... |
| [export-pdf-form-data-to-xml__v2](./export-pdf-form-data-to-xml__v2.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml` | Shows how to export AcroForm field data from a PDF directly to an XML stream with Aspose.Pdf, avo... |
| [export-pdf-form-fields-to-csv](./export-pdf-form-fields-to-csv.cs) | Export PDF Form Fields to CSV | `Document`, `Form`, `Field` | Loads a PDF document, enumerates all AcroForm fields using Aspose.Pdf's FormEditor, and writes ea... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Verify Structure | `Form`, `ExportJson` | Demonstrates how to export AcroForm field values from a PDF to a JSON file using Form.ExportJson ... |
| [export-pdf-form-fields-to-json__v2](./export-pdf-form-fields-to-json__v2.cs) | Export PDF Form Fields to JSON with File Splitting | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, extracting AcroForm field names and values, serializing them to JSON,... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Demonstrates how to export AcroForm field data from a PDF to an XFDF file using the Form facade. |
| [export-pdf-form-to-xml-and-html](./export-pdf-form-to-xml-and-html.cs) | Export PDF Form Data to XML and Convert to HTML via XSLT | `Form`, `ExportXml` | Shows how to export AcroForm field data from a PDF to an XML file using Aspose.Pdf.Facades and th... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `Form`, `WidgetAnnotation` | Demonstrates how to export only specific AcroForm fields from a PDF to a JSON file by using Widge... |
| [export-text-field-names-to-json](./export-text-field-names-to-json.cs) | Export Text Field Names from PDF AcroForm to JSON | `Form`, `GetFieldType` | Shows how to iterate over AcroForm fields, filter for text fields, and write the field names to a... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF AcroForm Fields and Save | `Form`, `FillField`, `Save` | Shows how to open a PDF containing AcroForm fields, populate them from a dictionary of values usi... |
| [fill-pdf-form-fields-from-byte-array](./fill-pdf-form-fields-from-byte-array.cs) | Fill PDF Form Fields from Byte Array | `Form`, `FillField`, `Save` | Loads a PDF from a byte array using a MemoryStream, fills the specified AcroForm fields, and retu... |
| [fill-pdf-form-from-json](./fill-pdf-form-from-json.cs) | Fill PDF Form Fields from JSON using Aspose.Pdf | `Form`, `ImportJson`, `Save` | Demonstrates how to load a PDF form, import field values from a JSON file, and save the populated... |
| [fill-textbox-field-pdf](./fill-textbox-field-pdf.cs) | Fill TextBox Field in PDF using Aspose.Pdf Form Facade | `Form`, `FillField`, `Save` | Demonstrates how to fill an existing AcroForm text box field with a string value using the Form f... |
| [filter-customer-acroform-fields-to-xfdf](./filter-customer-acroform-fields-to-xfdf.cs) | Filter Exported AcroForm Fields Starting with "Customer" | `Document`, `TextBoxField`, `Rectangle` | Shows how to export AcroForm fields from a PDF to XFDF, keep only fields whose names begin with "... |
| [generate-pdf-form-fields-report](./generate-pdf-form-fields-report.cs) | Generate PDF Form Fields Report | `Document`, `Page`, `Table` | Loads a PDF, iterates over its AcroForm fields, creates a new PDF containing a table with each fi... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Document`, `RadioButtonOptionField` | Shows how to load a PDF with Aspose.Pdf, locate a radio button group, and read the currently sele... |
| [import-fdf-data-into-pdf-form](./import-fdf-data-into-pdf-form.cs) | Import FDF Data into PDF Form | `Form`, `ImportFdf`, `Save` | Shows how to load an FDF file, import its field values into a PDF form using Aspose.Pdf.Facades.F... |
| [import-form-data-from-json](./import-form-data-from-json.cs) | Import Form Data from JSON into PDF | `Form`, `ImportJson`, `Save` | Shows how to load a JSON file containing form field values and import them into an existing PDF u... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `BindPdf`, `ImportXml` | Shows how to bind a PDF with the Form facade, import AcroForm field values from an XML stream usi... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form (Ignore Missing Fields) | `Form`, `BindPdf`, `ImportJson` | Demonstrates how to bind a PDF, import form field values from a JSON file, and save the updated d... |
| [import-json-data-into-pdf-form__v2](./import-json-data-into-pdf-form__v2.cs) | Import JSON Data into PDF Form using Aspose.Pdf | `Form`, `ImportJson`, `Save` | Shows how to serialize a C# object to JSON, load it into a MemoryStream, and import the values in... |
| [import-json-data-into-pdf-form__v3](./import-json-data-into-pdf-form__v3.cs) | Import JSON Data into PDF Form and Handle Missing Fields | `Form`, `ImportJson`, `Save` | Demonstrates loading a PDF form, importing field values from a JSON file, catching missing form f... |
| ... | | | *and 11 more files* |

## Category Statistics
- Total examples: 41

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

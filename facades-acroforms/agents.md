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

- `using Aspose.Pdf.Facades;` (41/41 files) ← category-specific
- `using Aspose.Pdf;` (13/41 files)
- `using Aspose.Pdf.Forms;` (5/41 files)
- `using Aspose.Pdf.Drawing;` (1/41 files)
- `using Aspose.Pdf.Text;` (1/41 files)
- `using System;` (41/41 files)
- `using System.IO;` (40/41 files)
- `using System.Collections.Generic;` (11/41 files)
- `using System.Text.Json;` (5/41 files)
- `using Newtonsoft.Json;` (2/41 files)
- `using System.Text;` (2/41 files)
- `using System.Threading.Tasks;` (2/41 files)
- `using System.Xml;` (2/41 files)
- `using System.Collections;` (1/41 files)
- `using System.Linq;` (1/41 files)
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
| [add-radio-button-group-to-pdf-form](./add-radio-button-group-to-pdf-form.cs) | Add Radio Button Group to PDF Form | `FormEditor`, `AddField`, `Save` | Demonstrates how to use Aspose.Pdf.Facades.FormEditor to add a radio button group with three opti... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF Using FormEditor | `Document`, `FormEditor`, `AddField` | Demonstrates how to load a PDF, add a new AcroForm text field at specific coordinates with FormEd... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF to an XML file using Aspose.Pdf's Form facade whil... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `FormEditor`, `BindPdf`, `Document` | Shows how to loop through a folder of PDF files, bind each with FormEditor, and export the AcroFo... |
| [batch-import-json-data-into-pdf-forms](./batch-import-json-data-into-pdf-forms.cs) | Batch Import JSON Data into PDF Forms in Parallel | `Form`, `ImportJson`, `Save` | Demonstrates how to import form data from matching JSON files into PDF documents using Aspose.Pdf... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine AcroForm JSON Exports from Multiple PDFs | `Form`, `ExportJson` | Shows how to export form fields from several PDFs to JSON, remove surrounding array brackets, and... |
| [export-acroform-data-to-xml-html](./export-acroform-data-to-xml-html.cs) | Export AcroForm Data to XML and Convert to HTML via XSLT | `Document`, `TextBoxField`, `Form` | Shows how to export PDF AcroForm field values to an XML file using Aspose.Pdf.Facades.Form and th... |
| [export-import-acroform-data-utf8](./export-import-acroform-data-utf8.cs) | Export and Import AcroForm Data with UTF-8 Encoding | `Document`, `TextBoxField`, `Form` | Demonstrates exporting AcroForm field values to JSON and XML using UTF-8 encoding, then re‑import... |
| [export-import-acroform-xfdf](./export-import-acroform-xfdf.cs) | Export and Import AcroForm Data via XFDF | `Form`, `ExportXfdf`, `ImportXfdf` | Demonstrates how to export AcroForm field values to an XFDF stream, import the XFDF back into a P... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF File | `Form`, `ExportFdf` | Shows how to use the Aspose.Pdf.Facades.Form class to export form field values from a PDF documen... |
| [export-pdf-form-data-to-json-chunks](./export-pdf-form-data-to-json-chunks.cs) | Export PDF Form Data to JSON and Split into Chunks | `Form`, `ExportJson`, `Document` | Loads a PDF containing an AcroForm, exports all form fields to a JSON array using Aspose.Pdf.Faca... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON and Deserialize | `Form`, `ExportJson`, `ImportJson` | Demonstrates exporting AcroForm fields from a PDF to a JSON string using Aspose.Pdf.Facades.Form,... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml` | Demonstrates how to export AcroForm field data from a PDF directly into an in‑memory XML stream u... |
| [export-pdf-form-fields-to-csv](./export-pdf-form-fields-to-csv.cs) | Export PDF Form Fields and Types to CSV | `Document`, `FormEditor`, `Form` | Demonstrates how to load a PDF, retrieve all AcroForm field names and their types using FormEdito... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Verify Structure | `Form`, `ExportJson` | Shows how to export AcroForm field values from a PDF into a JSON file using Aspose.Pdf and then r... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export AcroForm data from a PDF document into ... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Form`, `ExportXml` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all form field values from a PDF docume... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Form`, `BindPdf`, `ExportDataToJSON` | Shows how to export only specific form fields from a PDF to a JSON file using the Aspose.Pdf.Faca... |
| [export-text-field-names-to-json](./export-text-field-names-to-json.cs) | Export Text Field Names from PDF AcroForm to JSON | `Form`, `FieldNames`, `GetFieldType` | Demonstrates opening a PDF form with Aspose.Pdf.Facades, selecting only text fields, and writing ... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | Extract PDF Form Field Names to JSON | `Form`, `FieldNames`, `Dispose` | Shows how to open a PDF with Aspose.Pdf.Facades.Form, read all AcroForm field names, and print th... |
| [fill-pdf-acroform-fields-from-byte-array](./fill-pdf-acroform-fields-from-byte-array.cs) | Fill PDF AcroForm Fields from Byte Array | `Form`, `BindPdf`, `FillField` | Shows how to load a PDF from a byte array with MemoryStream, populate its AcroForm fields using A... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF AcroForm Fields Using Aspose.Pdf.Facades | `Form`, `FillField`, `Save` | Shows how to open a PDF containing AcroForm fields, populate them from a dictionary, and save the... |
| [fill-pdf-form-from-json](./fill-pdf-form-from-json.cs) | Fill PDF Form from JSON using Aspose.Pdf Facades | `Form`, `ImportJson`, `Save` | Demonstrates how to load a PDF with AcroForm fields, import values from a JSON file where keys ma... |
| [fill-textbox-field-pdf](./fill-textbox-field-pdf.cs) | Fill Text Box Field in PDF using Aspose.Pdf Form | `Form`, `FillField`, `Save` | Demonstrates how to locate a text box AcroForm field by name, set its value with Form.FillField, ... |
| [filter-pdf-form-fields-by-name](./filter-pdf-form-fields-by-name.cs) | Filter PDF Form Fields by Name and Export to XML | `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF to XML, keep only those whose names start with "Cu... |
| [generate-pdf-form-fields-report](./generate-pdf-form-fields-report.cs) | Generate PDF Report of AcroForm Fields | `Document`, `FormEditor`, `Field` | Demonstrates how to read AcroForm fields from an existing PDF using FormEditor and create a new P... |
| [get-selected-radio-button-value-from-pdf-form](./get-selected-radio-button-value-from-pdf-form.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Shows how to open a PDF form using Aspose.Pdf.Facades.Form and retrieve the currently selected op... |
| [import-fdf-data-into-pdf](./import-fdf-data-into-pdf.cs) | Import FDF Data into PDF using Form Facade | `Form`, `ImportFdf`, `Save` | Shows how to load an FDF file and import its form field values into a PDF document using the Aspo... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Shows how to bind to a PDF with the Form facade, import AcroForm field values from an XML stream,... |
| [import-json-data-into-pdf-acroform](./import-json-data-into-pdf-acroform.cs) | Import JSON Data into PDF AcroForm | `Form`, `BindPdf`, `ImportJson` | Shows how to serialize a C# object to JSON, load it into a memory stream, and import the data int... |
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
Updated: 2026-04-28 | Run: `20260428_161029_f17791`
<!-- AUTOGENERATED:END -->

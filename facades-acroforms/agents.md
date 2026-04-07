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

- `using Aspose.Pdf.Facades;` (32/40 files) ← category-specific
- `using Aspose.Pdf;` (16/40 files)
- `using Aspose.Pdf.Forms;` (5/40 files)
- `using Aspose.Pdf.Text;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (40/40 files)
- `using System.Collections.Generic;` (11/40 files)
- `using System.Text.Json;` (6/40 files)
- `using System.Linq;` (4/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Xml;` (2/40 files)
- `using Newtonsoft.Json;` (1/40 files)
- `using System.Collections;` (1/40 files)
- `using System.Text;` (1/40 files)
- `using System.Xml.Schema;` (1/40 files)
- `using System.Xml.Xsl;` (1/40 files)

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
| [add-radio-button-group](./add-radio-button-group.cs) | Add Radio Button Group with Default Selection using FormEdit... | `FormEditor`, `FieldType`, `AddField` | Demonstrates how to add a radio button group with three options to a PDF and set the default sele... |
| [add-text-field-formeditor](./add-text-field-formeditor.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `AddField`, `Save` | Demonstrates how to add a new text form field at specific coordinates to an existing PDF using As... |
| [batch-export-form-data-xml](./batch-export-form-data-xml.cs) | Batch Export PDF Form Data to XML | `Form`, `ExportXml` | Exports the fields of multiple PDF forms to separate XML files using Aspose.Pdf.Facades.Form. |
| [batch-import-form-json](./batch-import-form-json.cs) | Batch Import Form Data from JSON into PDFs in Parallel | `Form`, `ImportJson`, `Save` | Demonstrates how to import form field values from JSON files into matching PDF documents concurre... |
| [combine-pdf-json-data](./combine-pdf-json-data.cs) | Combine JSON Data from Multiple PDFs into a Single JSON Arra... | `File`, `JsonDocument`, `JsonSerializer` | Reads JSON files exported from PDFs, merges their contents into one JSON array, and writes the re... |
| [export-form-data-json-split](./export-form-data-json-split.cs) | Export PDF Form Data to JSON and Split into Multiple Files | `Document`, `ExportFormFieldsToJson`, `JsonSerializer` | Exports all form fields from a PDF to JSON, then splits the JSON into separate files each contain... |
| [export-form-data-json](./export-form-data-json.cs) | Export PDF Form Data to JSON and Deserialize with Json.NET | `Document`, `ExportFormFieldsToJson`, `JsonConvert` | Demonstrates exporting PDF form fields to a JSON file using Aspose.Pdf and then deserializing the... |
| [export-form-data-xml-memory](./export-form-data-xml-memory.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml`, `MemoryStream` | Demonstrates how to export the fields of a PDF form to an XML stream using Aspose.Pdf without cre... |
| [export-form-data-xml-to-html](./export-form-data-xml-to-html.cs) | Export PDF Form Data to XML and Transform to HTML using XSLT | `Form`, `ExportXml`, `XslCompiledTransform` | Demonstrates exporting form fields from a PDF to an XML file and then converting that XML to an H... |
| [export-form-fields-json](./export-form-fields-json.cs) | Export PDF Form Fields to JSON and Verify Structure | `Document`, `ExportToJson`, `JsonDocument` | Loads a PDF, exports all form field values to a JSON file using Aspose.Pdf, then reads the JSON t... |
| [export-pdf-form-data-xml-async](./export-pdf-form-data-xml-async.cs) | Export PDF Form Data to XML Asynchronously | `Form`, `ExportXml`, `FileStream` | Demonstrates how to export form field data from a PDF to an XML file using Aspose.Pdf.Facades.For... |
| [export-pdf-form-to-fdf](./export-pdf-form-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `ExportFdf`, `FileStream` | Demonstrates how to export the fields of an AcroForm PDF to an FDF file using Aspose.Pdf's Form f... |
| [export-pdf-form-to-xfdf](./export-pdf-form-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf`, `FileStream` | Demonstrates how to export the fields of a PDF form to an XFDF file using Aspose.Pdf.Facades.Form. |
| [export-pdf-form-to-xml](./export-pdf-form-to-xml.cs) | Export PDF Form Fields to XML | `Form`, `ExportXml` | Exports all form field values from a PDF document to an XML file using Aspose.Pdf.Facades.Form.Ex... |
| [export-selected-form-fields-json](./export-selected-form-fields-json.cs) | Export Selected Form Fields to JSON | `Document`, `ExportFieldsToJsonOptions`, `ExportToJson` | Exports only the specified PDF form fields to a JSON file using Aspose.Pdf. |
| [export-selected-form-fields](./export-selected-form-fields.cs) | Export Selected Form Fields to XML | `Form`, `GetField`, `FieldNames` | Exports only form fields whose names start with "Customer" from a PDF to an XML file. |
| [export-text-field-names](./export-text-field-names.cs) | Export Text Form Field Names to JSON | `Document`, `Form`, `Field` | Loads a PDF, filters its form fields to include only text fields, and writes the field names to a... |
| [fill-pdf-form-fields](./fill-pdf-form-fields.cs) | Fill PDF Form Fields and Save Document | `Form`, `FillField`, `Save` | Demonstrates how to open a PDF form, fill its fields from a dictionary, and save the updated docu... |
| [fill-pdf-form-from-byte-array](./fill-pdf-form-from-byte-array.cs) | Fill PDF Form Fields from Byte Array | `Form`, `FillField`, `Save` | Loads a PDF from a byte array, fills specified form fields using the Form facade, and returns the... |
| [fill-pdf-form-from-json](./fill-pdf-form-from-json.cs) | Fill PDF Form Fields from JSON Template | `Document`, `Form`, `ImportJson` | Loads a PDF form template, imports field values from a JSON file, and saves the populated PDF. |
| [fill-textbox-field](./fill-textbox-field.cs) | Fill Textbox Field in PDF using Form.FillField | `Form`, `FillField`, `Save` | Demonstrates how to fill a textbox form field in an existing PDF using Aspose.Pdf.Facades.Form an... |
| [form-fields-report](./form-fields-report.cs) | Generate PDF Report of Form Fields | `Document`, `Form`, `Table` | Creates a PDF report containing a table that lists the names, types, and current values of all fo... |
| [get-radio-button-value](./get-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Demonstrates how to retrieve the currently selected radio button option from a PDF form using Asp... |
| [import-export-form-utf8](./import-export-form-utf8.cs) | Import and Export PDF Form Fields with UTF-8 Encoding | `Form`, `ImportXml`, `ExportXml` | Demonstrates importing Unicode form field values from an XML file and exporting them back to XML ... |
| [import-form-data-fdf](./import-form-data-fdf.cs) | Import Form Data from FDF into PDF | `Form`, `ImportFdf`, `Save` | Shows how to import form field values from an FDF file into an existing PDF using Aspose.Pdf.Faca... |
| [import-form-data-json](./import-form-data-json.cs) | Import Form Data from JSON into PDF | `Form`, `ImportJson`, `Save` | Demonstrates how to import AcroForm field values from a JSON file into a PDF using the Aspose.Pdf... |
| [import-form-fields-missing-fields](./import-form-fields-missing-fields.cs) | Import Form Fields with Missing Field Handling | `Form`, `ImportXml`, `ImportResult` | Demonstrates importing form data from an XML file while handling missing fields by catching FormE... |
| [import-form-fields-xfdf](./import-form-fields-xfdf.cs) | Import Form Field Values from XFDF into PDF | `Form`, `ImportXfdf`, `Save` | Demonstrates how to import AcroForm field values from an XFDF file into a PDF using the Aspose.Pd... |
| [import-form-values-json](./import-form-values-json.cs) | Import PDF Form Values from JSON (ignore missing fields) | `Document`, `Form`, `ImportJson` | Shows how to import AcroForm field values from a JSON file into a PDF, automatically skipping any... |
| [import-form-xml](./import-form-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Demonstrates how to import AcroForm field values from an XML file into a PDF using Aspose.Pdf.Fac... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

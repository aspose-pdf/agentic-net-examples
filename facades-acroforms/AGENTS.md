---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

> **Facades AcroForms** in PDF using C# / .NET -- **39** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-acroforms** category.
This folder contains standalone C# examples for facades-acroforms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-acroforms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (29/39 files) ← category-specific
- `using Aspose.Pdf.Facades;` (26/39 files) ← category-specific
- `using Aspose.Pdf.Forms;` (14/39 files)
- `using Aspose.Pdf.Text;` (1/39 files)
- `using System;` (39/39 files)
- `using System.IO;` (39/39 files)
- `using System.Collections.Generic;` (10/39 files)
- `using System.Text.Json;` (8/39 files)
- `using System.Xml;` (3/39 files)
- `using System.Linq;` (2/39 files)
- `using System.Threading.Tasks;` (2/39 files)
- `using Newtonsoft.Json;` (1/39 files)
- `using System.Collections;` (1/39 files)
- `using System.Data;` (1/39 files)
- `using System.Text;` (1/39 files)
- `using System.Text.Json.Nodes;` (1/39 files)
- `using System.Xml.Schema;` (1/39 files)
- `using System.Xml.Xsl;` (1/39 files)

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
| [add-radio-button-group-to-pdf](./add-radio-button-group-to-pdf.cs) | Add Radio Button Group with Default Selection to PDF | `Document`, `Page`, `RadioButtonField` | Shows how to create a radio button group with three options in an existing PDF and set the defaul... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `BindPdf`, `AddField` | Demonstrates how to add a single‑line text field at specific coordinates to an existing PDF using... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Async Export PDF Form Data to XML (JSON) | `Document`, `Form`, `ExportToJson` | Demonstrates how to use async/await to export form field data from a PDF to a file (named *.xml) ... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDFs in Parallel | `Form` | Demonstrates how to import form data from multiple JSON files into matching PDF templates using A... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine PDF Exported JSON Files into a Single JSON Array | `PdfFileEditor` | Reads JSON files generated from AcroForm PDFs, validates them as JSON objects, and merges them in... |
| [create-pdf-form-fields-report](./create-pdf-form-fields-report.cs) | Generate PDF Report of AcroForm Fields | `Document`, `Page`, `Table` | Loads a PDF, extracts its AcroForm fields, and creates a new PDF containing a table that lists ea... |
| [export-filter-pdf-form-fields-xfdf](./export-filter-pdf-form-fields-xfdf.cs) | Export Customer AcroForm Fields to XML | `Document`, `Form`, `Field` | Loads a PDF, selects only form fields whose names start with "Customer", and writes their names a... |
| [export-import-pdf-form-fields-utf8](./export-import-pdf-form-fields-utf8.cs) | Export and Re‑Import Form Data with UTF‑8 Encoding (FDF) | `Document`, `Form`, `ExportFdf` | Demonstrates exporting AcroForm field values to an FDF stream and importing them back, preserving... |
| [export-pdf-form-data-to-fdf__v2](./export-pdf-form-data-to-fdf__v2.cs) | Export PDF Form Data to FDF | `Document`, `Form`, `BindPdf` | Shows how to load a PDF, bind it to the Form facade, and export its AcroForm field values to an F... |
| [export-pdf-form-data-to-json-chunks](./export-pdf-form-data-to-json-chunks.cs) | Export PDF Form Data to JSON and Split into Chunks | `Document`, `Form`, `ExportToJson` | Shows how to export AcroForm fields from a PDF to a JSON file using Aspose.Pdf and then split the... |
| [export-pdf-form-data-to-json__v7](./export-pdf-form-data-to-json__v7.cs) | Export PDF Form Data to JSON and Deserialize in C# | `Document`, `Form`, `ExportToJson` | Demonstrates exporting AcroForm fields from a PDF to a JSON file using Aspose.Pdf and deserializi... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export AcroForm Data to XML Using MemoryStream | `Document`, `Form`, `ExportXml` | Shows how to load a PDF, access its AcroForm via the Form facade, and export the form data to an ... |
| [export-pdf-form-data-to-xml__v5](./export-pdf-form-data-to-xml__v5.cs) | Export PDF Form Data to XML (FDF) | `Document`, `Form`, `ExportFdf` | Shows how to load a PDF document, use the Form facade, and export all AcroForm field values to an... |
| [export-pdf-form-fields-to-json__v6](./export-pdf-form-fields-to-json__v6.cs) | Export Pdf Form Fields To Json__V6 |  | Export Pdf Form Fields To Json__V6 |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Data to XFDF | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing AcroForm fields and export the field values to an XFDF ... |
| [export-pdf-form-to-xml-html](./export-pdf-form-to-xml-html.cs) | Export PDF Form Data to XML and Convert to HTML with XSLT | `Document`, `Form`, `AutoRecalculate` | Demonstrates exporting AcroForm fields from a PDF to Xfdf (XML) using the Form facade and then tr... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `Form`, `Field` | Demonstrates loading a PDF, extracting values of specific form fields by name, and writing those ... |
| [export-text-acroform-field-names-to-json](./export-text-acroform-field-names-to-json.cs) | Export Text Form Field Names to JSON | `Form`, `BindPdf`, `FieldNames` | Demonstrates how to use the Aspose.Pdf Facade to retrieve all AcroForm fields, filter only text f... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | Extract PDF Form Field Names to JSON | `Form`, `BindPdf`, `FieldNames` | Shows how to read AcroForm field names from a PDF using the Form facade and output them as indent... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF Form Fields Using Aspose.Pdf | `Document`, `Save`, `Field` | Demonstrates how to load a PDF, populate its AcroForm fields from a dictionary, and save the upda... |
| [fill-pdf-form-fields-from-json__v3](./fill-pdf-form-fields-from-json__v3.cs) | Fill PDF Form Fields from JSON Template | `Document`, `Form`, `Field` | Demonstrates loading a JSON file, mapping its key/value pairs to AcroForm field names, and saving... |
| [fill-textbox-field-in-pdf](./fill-textbox-field-in-pdf.cs) | Fill TextBox Form Field in PDF using Aspose.Pdf | `Form`, `BindPdf`, `FillField` | Demonstrates how to load a PDF, fill a textbox AcroForm field with a string value using the Form ... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `BindPdf`, `GetButtonOptionCurrentValue` | Shows how to bind a PDF document, retrieve the currently selected option of a radio button group ... |
| [import-fdf-data-into-pdf-form](./import-fdf-data-into-pdf-form.cs) | Import FDF Data into a PDF Using Form Facade | `Document`, `Form`, `ImportFdf` | Demonstrates how to load a PDF, import form field values from an FDF file via the Form facade, an... |
| [import-form-data-from-json__v2](./import-form-data-from-json__v2.cs) | Import JSON Form Data into PDF | `Document`, `Form`, `BindPdf` | Demonstrates loading a PDF, binding it to the Form facade, importing form field values from a JSO... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into a PDF | `Document`, `Form`, `ImportXml` | Shows how to load a PDF with AcroForm fields, read an XML file as a stream, import the field valu... |
| [import-json-data-into-pdf-acroform](./import-json-data-into-pdf-acroform.cs) | Import JSON Data into PDF Form Using Aspose.Pdf Facade | `Form`, `BindPdf`, `ImportJson` | Shows how to serialize a C# object to JSON, place it in a memory stream, and import the data into... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form | `Form`, `BindPdf`, `ImportJson` | Demonstrates loading a PDF form, importing field values from a JSON file using Form.ImportJson, a... |
| [import-json-data-into-pdf-form__v2](./import-json-data-into-pdf-form__v2.cs) | Import XML Data into PDF Form with Missing Field Error Handl... | `Form`, `ImportXml`, `Save` | Shows how to import form data from an XML file into a PDF using Aspose.Pdf.Facades.Form and catch... |
| [import-xfdf-data-into-pdf-form__v2](./import-xfdf-data-into-pdf-form__v2.cs) | Import XFDF Data into PDF Form | `Document`, `Form`, `ImportXfdf` | Shows how to load a PDF, import form field values from an XFDF file using the Form facade, and sa... |
| ... | | | *and 9 more files* |

## Category Statistics
- Total examples: 39

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-05 | Run: `20261105_205525_1dcfed`
<!-- AUTOGENERATED:END -->

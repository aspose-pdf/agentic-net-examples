---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

> **Facades AcroForms** in PDF using C# / .NET -- **41** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-acroforms** category.
This folder contains standalone C# examples for facades-acroforms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-acroforms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/41 files) ← category-specific
- `using Aspose.Pdf;` (13/41 files)
- `using Aspose.Pdf.Forms;` (5/41 files)
- `using System;` (41/41 files)
- `using System.IO;` (40/41 files)
- `using System.Collections.Generic;` (11/41 files)
- `using System.Text.Json;` (8/41 files)
- `using System.Text;` (3/41 files)
- `using System.Xml;` (3/41 files)
- `using System.Threading.Tasks;` (2/41 files)
- `using Newtonsoft.Json;` (1/41 files)
- `using System.Collections;` (1/41 files)
- `using System.Linq;` (1/41 files)
- `using System.Text.RegularExpressions;` (1/41 files)
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
| [add-radio-button-group-with-default-selection](./add-radio-button-group-with-default-selection.cs) | Add Radio Button Group with Default Selection using FormEdit... | `Document`, `FormEditor`, `AddField` | Demonstrates creating an in‑memory PDF, binding a FormEditor, defining a three‑option radio‑butto... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF with FormEditor | `FormEditor`, `FieldType`, `AddField` | Shows how to add a new text field at specific coordinates to a PDF using Aspose.Pdf.Facades.FormE... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Shows how to wrap the synchronous Aspose.Pdf.Facades.Form.ExportXml method in Task.Run to export ... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `FormEditor`, `BindPdf`, `Document` | Shows how to iterate over PDF files, bind each with FormEditor, and export its AcroForm fields to... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDFs in Parallel | `Document`, `Form`, `ImportJson` | Shows how to import form data from JSON files into matching PDF forms concurrently using Aspose.P... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine AcroForm JSON Exports from Multiple PDFs | `Form`, `ExportJson` | Exports form data from several PDF files to JSON, merges the individual JSON objects into a singl... |
| [export-customer-acroform-fields-to-xml](./export-customer-acroform-fields-to-xml.cs) | Export Customer AcroForm Fields to XML | `Document`, `Form`, `FieldNames` | Shows how to filter PDF form fields whose names start with "Customer" and write the selected fiel... |
| [export-import-acroform-xfdf-roundtrip](./export-import-acroform-xfdf-roundtrip.cs) | Export and Import AcroForm Data via XFDF (Round‑Trip) | `Form`, `ExportXfdf`, `ImportXfdf` | Demonstrates how to export PDF form fields to an XFDF stream, import the XFDF back into a new PDF... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `ExportFdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export AcroForm field values from a PDF docume... |
| [export-pdf-form-data-to-json-chunks](./export-pdf-form-data-to-json-chunks.cs) | Export PDF Form Data to JSON and Split into Chunks | `Form`, `ExportJson` | Demonstrates how to export AcroForm fields from a PDF to a JSON document using Aspose.Pdf and the... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON and Deserialize with Json.NET | `Form`, `ExportJson` | Shows how to export AcroForm fields from a PDF to JSON using Aspose.Pdf.Facades and then deserial... |
| [export-pdf-form-data-to-xml-html](./export-pdf-form-data-to-xml-html.cs) | Export PDF Form Data to XML and Convert to HTML with XSLT | `Document`, `Page`, `Rectangle` | Shows how to create a simple AcroForm PDF, export its field values to an XML file using the Form ... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF directly to an XML string via a MemoryStream, elim... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all form field values from a PDF docume... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON | `Form`, `ExportJson`, `Dispose` | Shows how to export AcroForm field values from a PDF into a JSON file using Aspose.Pdf.Facades.Fo... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all AcroForm field values from a PDF in... |
| [export-pdf-text-field-names-to-json](./export-pdf-text-field-names-to-json.cs) | Export PDF Text Field Names to JSON | `Form`, `FieldNames`, `GetFieldType` | Loads a PDF, extracts the names of text-type AcroForm fields, and writes them to a formatted JSON... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `Form`, `Fields` | Shows how to load a PDF, filter specific AcroForm fields by name, and write their values to a for... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | Extract PDF Form Field Names to JSON | `Form`, `FieldNames`, `SaveableFacade` | Shows how to open a PDF with Aspose.Pdf.Facades.Form, retrieve all AcroForm field names, and outp... |
| [fill-acroform-pdf-fields](./fill-acroform-pdf-fields.cs) | Fill AcroForm PDF Fields and Save | `Form`, `FillField`, `Save` | Shows how to open an AcroForm PDF with Aspose.Pdf, populate its form fields from a dictionary, an... |
| [fill-pdf-form-fields-from-byte-array](./fill-pdf-form-fields-from-byte-array.cs) | Fill PDF Form Fields from Byte Array | `Form`, `FillField`, `Save` | Loads a PDF from a byte array, fills specified AcroForm fields using the Form facade, and returns... |
| [fill-pdf-form-from-json](./fill-pdf-form-from-json.cs) | Fill PDF Form from JSON using Aspose.Pdf Form Facade | `Form`, `ImportJson`, `Save` | Demonstrates how to load an AcroForm PDF, import field values from a JSON file, and save the fill... |
| [fill-textbox-field-pdf](./fill-textbox-field-pdf.cs) | Fill TextBox Field in PDF Using Aspose.Pdf Form Facade | `Form`, `FillField`, `Save` | Demonstrates how to locate a textbox AcroForm field by name, set its value with Form.FillField, a... |
| [generate-pdf-form-field-report](./generate-pdf-form-field-report.cs) | Generate PDF Form Field Report Table | `Document`, `FormEditor`, `Table` | Creates a PDF report that lists all AcroForm fields with their names, types, and values in a tabl... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Demonstrates how to read the currently selected option of a radio button group in an AcroForm usi... |
| [import-form-data-from-fdf](./import-form-data-from-fdf.cs) | Import Form Data from FDF into PDF | `Form`, `ImportFdf`, `Save` | Shows how to load an FDF file, import its field values into an existing PDF form using the Form f... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Shows how to load a PDF containing AcroForm fields, import field values from an XML file using th... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form | `Form`, `ImportJson`, `ImportResult` | Shows how to load a PDF, import form field values from a JSON file using the Form facade, optiona... |
| [import-json-data-into-pdf-form__v2](./import-json-data-into-pdf-form__v2.cs) | Import JSON Data into PDF Form While Ignoring Missing Fields | `Form`, `FieldNames`, `ImportJson` | Shows how to load JSON, filter out entries that don't exist in a PDF AcroForm, and import the fil... |
| [import-json-data-into-pdf-form__v3](./import-json-data-into-pdf-form__v3.cs) | Import JSON Data into PDF Form | `Form`, `ImportJson`, `Save` | Demonstrates serializing a C# object to JSON, loading it into a memory stream, and importing the ... |
| ... | | | *and 11 more files* |

## Category Statistics
- Total examples: 41

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

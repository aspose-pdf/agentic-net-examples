---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

> **Facades AcroForms** in PDF using C# / .NET -- **41** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (11/41 files)
- `using Aspose.Pdf.Forms;` (3/41 files)
- `using Aspose.Pdf.Text;` (1/41 files)
- `using System;` (41/41 files)
- `using System.IO;` (39/41 files)
- `using System.Collections.Generic;` (11/41 files)
- `using System.Text.Json;` (8/41 files)
- `using System.Linq;` (3/41 files)
- `using System.Text;` (3/41 files)
- `using System.Threading.Tasks;` (2/41 files)
- `using System.Xml;` (2/41 files)
- `using Newtonsoft.Json;` (1/41 files)
- `using System.Collections;` (1/41 files)
- `using System.Text.Json.Nodes;` (1/41 files)
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
| [add-radio-button-group-to-pdf](./add-radio-button-group-to-pdf.cs) | Add Radio Button Group to PDF with Default Selection | `FormEditor`, `AddField`, `Save` | Demonstrates using Aspose.Pdf.Facades.FormEditor to insert a radio button group with three option... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `AddField`, `Save` | Demonstrates how to insert a new text field at specific coordinates on a PDF page using Aspose.Pd... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates exporting AcroForm fields from a PDF to an XML file using async/await to keep the UI... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `Document`, `FormEditor`, `Form` | Loops through PDF files in a folder, loads each document, and uses the FormEditor and Form facade... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDFs in Parallel | `Form`, `ImportJson`, `Save` | Demonstrates importing form field values from matching JSON files into PDF documents using Aspose... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine AcroForm JSON Exports from Multiple PDFs | `Form`, `ExportJson` | The example loads several PDF files, uses Aspose.Pdf.Facades.Form to export each document's AcroF... |
| [create-pdf-form-fields-report](./create-pdf-form-fields-report.cs) | Create PDF Report of AcroForm Fields | `Document`, `Page`, `Table` | Demonstrates how to enumerate AcroForm fields in a PDF and generate a new PDF containing a table ... |
| [export-filter-pdf-form-fields-xfdf](./export-filter-pdf-form-fields-xfdf.cs) | Export and Filter PDF Form Fields to XFDF | `Document`, `Form`, `ExportXfdf` | Shows how to export AcroForm fields from a PDF to XFDF, keep only those whose names start with "C... |
| [export-import-acroform-xfdf](./export-import-acroform-xfdf.cs) | Export and Import AcroForm Data via XFDF | `Document`, `Form`, `FillField` | Shows how to fill AcroForm fields, export the data to an XFDF file, import the XFDF back into a f... |
| [export-import-pdf-form-fields-utf8](./export-import-pdf-form-fields-utf8.cs) | Export and Import PDF Form Fields with UTF-8 Encoding | `Document`, `Form`, `ExportXfdf` | Demonstrates exporting AcroForm field values to an XFDF file and re‑importing them using UTF‑8 st... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF File | `Form`, `ExportFdf` | Demonstrates using the Aspose.Pdf.Facades.Form class to export all AcroForm field values from a P... |
| [export-pdf-form-data-to-json-chunks](./export-pdf-form-data-to-json-chunks.cs) | Export PDF Form Data to JSON and Split into Chunks | `Form`, `ExportJson` | Demonstrates how to export AcroForm fields from a PDF to a JSON document using Aspose.Pdf and the... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON and Deserialize | `Form`, `ExportJson`, `Dispose` | Shows how to export AcroForm fields from a PDF to a JSON string using Aspose.Pdf.Facades.Form and... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml`, `Close` | Shows how to export AcroForm field data from a PDF directly into a memory stream as XML, eliminat... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all AcroForm field values from a PDF do... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Verify Structure | `Document`, `Form`, `ExportJson` | Shows how to export AcroForm field values from a PDF to a JSON file using Aspose.Pdf's Form facad... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export AcroForm data from a PDF into an XFDF f... |
| [export-pdf-form-to-xml-html](./export-pdf-form-to-xml-html.cs) | Export PDF Form Data to XML and Convert to HTML with XSLT | `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF to an XML file using Aspose.Pdf.Facades.Form and t... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `Form`, `ExportJson` | Shows how to export only specific form fields from a PDF AcroForm to a JSON file by exporting the... |
| [export-text-acroform-field-names-to-json](./export-text-acroform-field-names-to-json.cs) | Export Text AcroForm Field Names to JSON | `Form`, `FieldNames`, `GetFieldType` | The example opens a PDF, filters AcroForm fields to include only text fields, and writes the fiel... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | Extract PDF Form Field Names to JSON | `Form`, `FieldNames`, `Dispose` | Shows how to open a PDF using Aspose.Pdf.Facades.Form, retrieve all AcroForm field names, and out... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF AcroForm Fields and Save | `Form`, `FillField`, `Save` | Shows how to open a PDF, populate AcroForm fields from a dictionary, and save the updated documen... |
| [fill-pdf-form-fields-from-json](./fill-pdf-form-fields-from-json.cs) | Fill PDF Form Fields from JSON using Aspose.Pdf | `Form`, `FillField`, `Save` | Shows how to read a JSON file into a dictionary, map its keys to AcroForm field names, fill those... |
| [fill-textbox-field-in-pdf](./fill-textbox-field-in-pdf.cs) | Fill TextBox Field in PDF using Aspose.Pdf | `Form`, `FillField`, `Save` | Shows how to open a PDF, fill a specific AcroForm textbox field with a string using Form.FillFiel... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Demonstrates how to open a PDF form with Aspose.Pdf.Facades and retrieve the currently selected o... |
| [import-fdf-data-into-pdf-form](./import-fdf-data-into-pdf-form.cs) | Import FDF Data into PDF Form | `Form`, `ImportFdf`, `Save` | Shows how to open a PDF form, import field values from an FDF file using a file stream, and save ... |
| [import-form-data-from-json](./import-form-data-from-json.cs) | Import Form Data from JSON into PDF (Ignore Missing Fields) | `Form`, `ImportJson`, `Save` | Demonstrates loading a PDF form, importing field values from a JSON file with Aspose.Pdf.Facades.... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Shows how to load a PDF containing AcroForm fields, import field values from an XML file using Fo... |
| [import-json-data-into-pdf-acroform](./import-json-data-into-pdf-acroform.cs) | Import JSON Data into PDF AcroForm | `Form`, `ImportJson`, `Save` | Shows how to serialize a C# object to JSON, place it in a memory stream, and import the data into... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form Fields | `Form`, `BindPdf`, `ImportJson` | Shows how to load a JSON file containing form field values and import them into an existing PDF u... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

> **Facades AcroForms** in PDF using C# / .NET -- **41** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (16/41 files)
- `using Aspose.Pdf.Forms;` (5/41 files)
- `using Aspose.Pdf.Drawing;` (1/41 files)
- `using Aspose.Pdf.Text;` (1/41 files)
- `using System;` (41/41 files)
- `using System.IO;` (37/41 files)
- `using System.Collections.Generic;` (12/41 files)
- `using System.Text.Json;` (7/41 files)
- `using System.Linq;` (4/41 files)
- `using Newtonsoft.Json;` (2/41 files)
- `using System.Threading.Tasks;` (2/41 files)
- `using System.Xml;` (2/41 files)
- `using Newtonsoft.Json.Linq;` (1/41 files)
- `using System.Collections;` (1/41 files)
- `using System.Text;` (1/41 files)
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
| [add-radio-button-group-default-selection](./add-radio-button-group-default-selection.cs) | Add Radio Button Group with Default Selection using FormEdit... | `Document`, `FormEditor`, `AddField` | Shows how to create a PDF, initialize FormEditor, configure a radio button group with three optio... |
| [add-text-field-to-pdf-using-formeditor](./add-text-field-to-pdf-using-formeditor.cs) | Add Text Field to PDF Using FormEditor | `FormEditor`, `AddField`, `Save` | Shows how to insert a new text field at specific coordinates into an existing PDF document using ... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `Form`, `ExportXml` | Shows how to use async/await with Aspose.Pdf to export AcroForm fields from a PDF document to an ... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML | `FormEditor`, `BindPdf`, `Form` | Shows how to loop through PDF files, bind each to a FormEditor, and export the AcroForm data to i... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDFs in Parallel | `Form`, `ImportJson`, `Save` | Shows how to import form data from matching JSON files into PDF documents using Aspose.Pdf.Facade... |
| [combine-exported-json-from-multiple-pdf-forms](./combine-exported-json-from-multiple-pdf-forms.cs) | Combine Exported JSON from Multiple PDF Forms | `Form`, `ExportJson` | Shows how to export AcroForm data from several PDFs as JSON, merge the fragments into a single JS... |
| [export-acroform-data-to-json-split-files](./export-acroform-data-to-json-split-files.cs) | Export AcroForm Data to JSON and Split into Multiple Files | `Form`, `ExportJson`, `Document` | Demonstrates exporting all form fields from a PDF AcroForm to a JSON array with Aspose.Pdf, then ... |
| [export-acroform-data-to-json](./export-acroform-data-to-json.cs) | Export AcroForm Data to JSON and Deserialize | `Document`, `Page`, `TextBoxField` | Creates a PDF with AcroForm fields, exports the form values to a JSON file using Aspose.Pdf.Facad... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `ExportFdf` | Demonstrates how to use Aspose.Pdf.Facades.Form to export the field values of a PDF form into an ... |
| [export-pdf-form-data-to-html](./export-pdf-form-data-to-html.cs) | Export PDF Form Data to HTML via XML and XSLT | `Document`, `Form`, `ExportXml` | Shows how to export AcroForm fields from a PDF to an XML file using Aspose.Pdf.Facades.Form and t... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to XML Using MemoryStream | `Form`, `ExportXml` | Demonstrates exporting AcroForm fields from a PDF to an XML string with Aspose.Pdf.Facades.Form a... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML | `Form`, `ExportXml` | Demonstrates how to use Aspose.Pdf.Facades.Form to export all AcroForm field values from a PDF do... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Validate Structure | `Form`, `ExportJson` | Shows how to export AcroForm field values from a PDF to a JSON file using Aspose.Pdf.Facades.Form... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Form`, `ExportXfdf` | Shows how to export all form field values from a PDF document to an XFDF file using the Aspose.Pd... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Form`, `ExportJson` | Shows how to export only specific form fields from a PDF by exporting all fields to JSON, filteri... |
| [export-text-form-field-names-to-json](./export-text-form-field-names-to-json.cs) | Export Text Form Field Names to JSON | `Form`, `FieldNames`, `GetFieldType` | Loads a PDF containing AcroForm fields, filters the fields to keep only text fields, and writes t... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | Extract PDF Form Field Names to JSON | `Form`, `FieldNames`, `Dispose` | Demonstrates how to load a PDF using Aspose.Pdf.Facades.Form, retrieve all AcroForm field names, ... |
| [fill-pdf-acroform-fields-from-json](./fill-pdf-acroform-fields-from-json.cs) | Fill PDF AcroForm Fields from JSON | `Form`, `FieldNames`, `FillField` | Shows how to read a JSON file into a dictionary and use Aspose.Pdf.Facades.Form to map JSON keys ... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF AcroForm Fields with Aspose.Pdf | `Form`, `FillField`, `Save` | Shows how to open a PDF, populate its AcroForm fields from a dictionary, and save the updated doc... |
| [fill-pdf-form-fields-from-byte-array](./fill-pdf-form-fields-from-byte-array.cs) | Fill PDF Form Fields from Byte Array | `Form`, `FillField`, `Save` | Shows how to load a PDF from a byte array using MemoryStream, fill AcroForm fields with Aspose.Pd... |
| [fill-textbox-field-in-pdf](./fill-textbox-field-in-pdf.cs) | Fill TextBox Field in PDF Using Aspose.Pdf Form Facade | `Form`, `FillField`, `Save` | Demonstrates how to load a PDF, fill a specific AcroForm text box field with a string value using... |
| [filter-pdf-form-fields-by-name](./filter-pdf-form-fields-by-name.cs) | Filter PDF Form Fields by Name and Export to XML | `Form`, `ExportXml` | Loads a PDF form, exports its fields to XFDF XML, keeps only fields whose names start with "Custo... |
| [generate-pdf-form-field-report](./generate-pdf-form-field-report.cs) | Generate PDF Form Field Report with Table | `Form`, `Document`, `Table` | Creates a PDF report that lists all AcroForm fields, showing each field's name, type, and value i... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `GetButtonOptionCurrentValue` | Demonstrates how to open a PDF form with the Aspose.Pdf.Facades.Form class and retrieve the curre... |
| [import-acroform-data-from-xml-with-missing-field-r...](./import-acroform-data-from-xml-with-missing-field-reporting.cs) | Import AcroForm Data from XML with Missing Field Reporting | `Document`, `Form`, `ImportXml` | Demonstrates how to import AcroForm field values from an XML file into a PDF using Aspose.Pdf.Fac... |
| [import-fdf-data-into-pdf-form](./import-fdf-data-into-pdf-form.cs) | Import FDF Data into PDF Form | `Form`, `ImportFdf`, `Save` | Shows how to open a PDF form, import field values from an FDF file via a file stream, and save th... |
| [import-form-data-from-json](./import-form-data-from-json.cs) | Import Form Data from JSON into PDF (ignore missing fields) | `Form`, `BindPdf`, `ImportJson` | Demonstrates how to bind a PDF, import AcroForm field values from a JSON file while automatically... |
| [import-form-data-from-xml-to-pdf](./import-form-data-from-xml-to-pdf.cs) | Import Form Field Values from XML into PDF | `Form`, `ImportXml`, `Save` | Shows how to read an XML file with AcroForm field values and import them into a PDF using Aspose.... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import JSON Data into PDF Form | `Form`, `ImportJson`, `Save` | Shows how to load a PDF form, import field values from a JSON file using Form.ImportJson, and sav... |
| [import-json-data-into-pdf-form__v2](./import-json-data-into-pdf-form__v2.cs) | Import JSON Data into PDF Form Using Aspose.Pdf | `Form`, `ImportJson`, `Save` | Shows how to serialize a C# object to JSON, load it into a memory stream, and import the data int... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

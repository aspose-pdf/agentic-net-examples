---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

> **Facades AcroForms** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-acroforms** category.
This folder contains standalone C# examples for facades-acroforms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-acroforms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (28/40 files) ← category-specific
- `using Aspose.Pdf.Facades;` (24/40 files) ← category-specific
- `using Aspose.Pdf.Forms;` (14/40 files)
- `using Aspose.Pdf.Text;` (2/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (39/40 files)
- `using System.Collections.Generic;` (9/40 files)
- `using System.Text.Json;` (8/40 files)
- `using System.Linq;` (4/40 files)
- `using System.Text;` (4/40 files)
- `using System.Xml;` (3/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using Newtonsoft.Json;` (1/40 files)
- `using System.Collections;` (1/40 files)
- `using System.Diagnostics.CodeAnalysis;` (1/40 files)
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
| [add-radio-button-group-to-pdf](./add-radio-button-group-to-pdf.cs) | Add Radio Button Group with Default Selection to PDF | `Document`, `Page`, `RadioButtonField` | Shows how to insert a radio button group with three options into an existing PDF and programmatic... |
| [add-text-field-to-pdf](./add-text-field-to-pdf.cs) | Add Text Field to PDF Using Aspose.Pdf | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to create and place a text box form field at specific coordinates in a PDF docum... |
| [async-export-pdf-form-data-to-xml](./async-export-pdf-form-data-to-xml.cs) | Asynchronously Export PDF Form Data to XML | `PdfFormEditor`, `BindPdf`, `ExportFormDataToXml` | Demonstrates using Aspose.Pdf.Facades.PdfFormEditor to bind a PDF and export its form fields to a... |
| [batch-export-pdf-form-data-to-xml](./batch-export-pdf-form-data-to-xml.cs) | Batch Export PDF Form Data to XML (XFDF) | `Document`, `Form`, `ExportXfdf` | Shows how to iterate over multiple PDF files, load each with Aspose.Pdf, and export its AcroForm ... |
| [batch-import-json-data-into-pdfs](./batch-import-json-data-into-pdfs.cs) | Batch Import JSON Data into PDF Forms in Parallel | `Document`, `Form`, `BindPdf` | Demonstrates how to import form field values from matching JSON files into multiple PDF documents... |
| [combine-acroform-json-exports](./combine-acroform-json-exports.cs) | Combine PDF Text Exports into a Single JSON Array | `Document`, `TextAbsorber`, `Accept` | Extracts text from multiple PDF files using Aspose.Pdf, creates a JSON object for each file, and ... |
| [create-pdf-form-fields-report](./create-pdf-form-fields-report.cs) | Generate PDF Report of AcroForm Fields | `Document`, `Page`, `Table` | Creates a PDF document that lists each form field's name, type, and value by iterating over Docum... |
| [export-filter-pdf-form-fields-xfdf](./export-filter-pdf-form-fields-xfdf.cs) | Export PDF Form Fields Starting with "Customer" to XML | `Document`, `Form`, `FieldCollection` | Loads a PDF document, filters AcroForm fields whose names begin with "Customer", and writes those... |
| [export-import-acroform-xfdf](./export-import-acroform-xfdf.cs) | Export and Import AcroForm Data via XFDF | `Document`, `Form`, `ExportXfdf` | Demonstrates exporting form field values to an XFDF file and importing them back into a fresh PDF... |
| [export-import-pdf-form-fields-utf8](./export-import-pdf-form-fields-utf8.cs) | Export and Import Unicode Form Field Values with UTF-8 | `PdfFormEditor`, `BindPdf`, `SetFormFieldValue` | Demonstrates how to set Unicode text in an AcroForm field, export the data to an UTF‑8 encoded XF... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF File | `Document`, `Form`, `BindPdf` | Shows how to load a PDF, bind it to the Form facade, and export its AcroForm data to an FDF file ... |
| [export-pdf-form-data-to-json-chunks](./export-pdf-form-data-to-json-chunks.cs) | Export PDF Form Data to JSON with Batch Splitting | `Document`, `Form`, `Field` | Shows how to read AcroForm fields from a PDF using Aspose.Pdf, serialize the field names and valu... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF AcroForm Data to JSON and Deserialize to Typed Ob... | `Document`, `Form`, `Field` | Demonstrates extracting AcroForm field values from a PDF, writing them to a JSON file, and deseri... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to JSON via Memory Stream | `Document`, `Form`, `ExportToJson` | Loads a PDF into a MemoryStream, creates an Aspose.Pdf Document, and exports the AcroForm fields ... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `AutoRecalculate` | Loads a PDF document, enables automatic form field recalculation, and exports all form field valu... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON and Verify Structure | `Document`, `Form`, `ExportToJson` | Demonstrates how to export AcroForm field names and values from a PDF into a JSON file using Aspo... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Data to XFDF | `Document`, `Form`, `BindPdf` | Demonstrates loading a PDF, binding it to the Form facade, and exporting its AcroForm fields to a... |
| [export-pdf-form-to-xml-html](./export-pdf-form-to-xml-html.cs) | Export PDF Form Data to XML and Transform to HTML | `PdfFormEditor`, `BindPdf`, `ExportFormDataToXml` | Shows how to extract AcroForm data from a PDF into an XML file using Aspose.Pdf.Facades.PdfFormEd... |
| [export-selected-acroform-fields-to-json](./export-selected-acroform-fields-to-json.cs) | Export Selected AcroForm Fields to JSON | `Document`, `Field`, `Form` | Shows how to load a PDF, pick specific form fields by name, and serialize their values into a JSO... |
| [export-text-acroform-field-names-to-json](./export-text-acroform-field-names-to-json.cs) | Export Text Form Field Names from PDF to JSON | `Document`, `Form`, `Field` | Loads a PDF document, extracts the names of all text box AcroForm fields, and writes the list to ... |
| [extract-pdf-form-field-names](./extract-pdf-form-field-names.cs) | List PDF Form Field Names as JSON | `PdfFormEditor`, `BindPdf`, `GetFields` | Demonstrates how to load a PDF using Aspose.Pdf.Facades, retrieve all AcroForm field names, and o... |
| [fill-pdf-acroform-fields](./fill-pdf-acroform-fields.cs) | Fill PDF Form Fields with Aspose.Pdf.Facades | `FormFiller`, `BindPdf`, `SetField` | Shows how to bind a PDF, set AcroForm field values using FormFiller, and save the updated document. |
| [fill-pdf-form-fields-from-json](./fill-pdf-form-fields-from-json.cs) | Fill PDF Form Fields from JSON | `Document`, `Form`, `Field` | Loads a PDF template containing AcroForm fields, reads a JSON file of key/value pairs, maps each ... |
| [fill-textbox-field-in-pdf](./fill-textbox-field-in-pdf.cs) | Fill TextBox Field in PDF using Aspose.Pdf Form | `Form`, `BindPdf`, `FillField` | Demonstrates how to load an existing PDF, fill a specific AcroForm textbox field with a string va... |
| [get-selected-radio-button-value](./get-selected-radio-button-value.cs) | Get Selected Radio Button Value from PDF Form | `Form`, `BindPdf`, `GetButtonOptionCurrentValue` | Demonstrates how to load a PDF document with Aspose.Pdf.Facades, bind it to a Form object, and re... |
| [import-fdf-data-into-pdf-form](./import-fdf-data-into-pdf-form.cs) | Import Form Data from FDF into PDF | `Document`, `Form`, `ImportFdf` | Shows how to load a PDF, open an FDF file as a stream, import the form data using the Form facade... |
| [import-form-data-from-json](./import-form-data-from-json.cs) | Import JSON Data into PDF Form While Ignoring Missing Fields | `Form`, `BindPdf`, `ImportJson` | Demonstrates how to load JSON data from a file and import it into an AcroForm PDF using Aspose.Pd... |
| [import-form-fields-from-xml](./import-form-fields-from-xml.cs) | Import XML Data into PDF Form Fields | `Form`, `BindPdf`, `ImportXml` | Demonstrates how to bind a PDF form and import field values from an XML file using the Form facad... |
| [import-json-data-into-pdf-acroform](./import-json-data-into-pdf-acroform.cs) | Import JSON Data into PDF Form using Aspose.Pdf Facade | `Document`, `Form`, `ImportJson` | Shows how to serialize a C# object to JSON, wrap it in a MemoryStream, import the JSON into an Ac... |
| [import-json-data-into-pdf-form](./import-json-data-into-pdf-form.cs) | Import FDF Data into PDF Form with Missing Field Handling | `Form`, `ImportFdf`, `Save` | Demonstrates how to import FDF data into an AcroForm PDF using Aspose.Pdf.Facades.Form and handle... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-21 | Run: `20261021_232641_b0d4cc`
<!-- AUTOGENERATED:END -->

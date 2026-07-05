---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **64** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (64/64 files) ← category-specific
- `using Aspose.Pdf.Forms;` (16/64 files)
- `using Aspose.Pdf.Text;` (13/64 files)
- `using Aspose.Pdf.Vector;` (13/64 files)
- `using Aspose.Pdf.Drawing;` (4/64 files)
- `using Aspose.Pdf.Devices;` (2/64 files)
- `using Aspose.Pdf.Annotations;` (1/64 files)
- `using System;` (64/64 files)
- `using System.IO;` (64/64 files)
- `using System.Collections.Generic;` (13/64 files)
- `using System.Text;` (4/64 files)
- `using System.Text.Json;` (4/64 files)
- `using System.Linq;` (3/64 files)
- `using System.Collections.Concurrent;` (1/64 files)
- `using System.Globalization;` (1/64 files)
- `using System.Text.RegularExpressions;` (1/64 files)
- `using System.Threading.Tasks;` (1/64 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-image-to-pdf-save-filestream](./add-image-to-pdf-save-filestream.cs) | Add Image to PDF and Save with FileStream | `Document`, `Image`, `Save` | Demonstrates loading a PDF, inserting an image onto the first page, and saving the result using a... |
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `HasVectorGraphics` | Shows how to iterate through PDF files in a folder, detect pages that contain vector graphics, an... |
| [combine-pdf-form-data-to-json](./combine-pdf-form-data-to-json.cs) | Combine PDF Form Data into a Single JSON Array | `Document`, `Form`, `ExportToJson` | Demonstrates loading multiple PDFs, exporting each form's fields to JSON, and merging them into a... |
| [combine-pdf-page-vector-graphics-into-svg](./combine-pdf-page-vector-graphics-into-svg.cs) | Combine PDF Page Vector Graphics into a Multi‑Page SVG | `Document`, `Page`, `SvgExtractor` | Loads a PDF, extracts vector graphics from each page using Aspose.Pdf.Vector.SvgExtractor, wraps ... |
| [combine-pdf-vector-graphics-to-svg](./combine-pdf-vector-graphics-to-svg.cs) | Combine Vector Graphics from Multiple PDFs into a Single SVG | `Document`, `Page`, `PageCollection` | The example loads several PDF files, extracts vector graphics from each page using Aspose.Pdf's S... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in a PDF | `Document`, `Form`, `Form` | Loads a PDF using Aspose.Pdf, accesses the Form collection, and outputs the total number of AcroF... |
| [enumerate-pdf-form-fields](./enumerate-pdf-form-fields.cs) | Enumerate PDF Form Fields and Log Details | `Document`, `Page`, `Field` | Loads a PDF, iterates through each page, and lists every form field with its name, type, and curr... |
| [export-acroform-fields-to-json](./export-acroform-fields-to-json.cs) | Export AcroForm Fields to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF with Aspose.Pdf, access its AcroForm, and export all form field names and... |
| [export-acroform-fields-to-xfdf](./export-acroform-fields-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf` | Demonstrates loading a PDF with Aspose.Pdf, verifying its existence, and exporting all AcroForm f... |
| [export-filtered-pdf-form-fields-to-json](./export-filtered-pdf-form-fields-to-json.cs) | Export Filtered PDF Form Fields to JSON | `Document`, `Field`, `TextBoxField` | Loads a PDF, selects form fields whose names start with a given prefix, extracts their values, an... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Document`, `Form`, `ExportFdf` | Loads a PDF document, accesses its form via the Aspose.Pdf.Facades.Form facade, and writes the fo... |
| [export-pdf-form-data-to-fdf__v2](./export-pdf-form-data-to-fdf__v2.cs) | Export PDF Form Data to FDF | `Document`, `Form`, `ExportFdf` | Loads a PDF document, accesses its form via the Form facade, and writes the form fields to an FDF... |
| [export-pdf-form-data-to-json-or-xfdf](./export-pdf-form-data-to-json-or-xfdf.cs) | Export PDF Form Data to JSON or XFDF | `Document`, `Form`, `ExportToJson` | Loads a PDF using Aspose.Pdf and exports its form fields to a JSON file or all annotations to an ... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF with Aspose.Pdf, export all form fields to a JSON string using Form.Expor... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Demonstrates loading a PDF document with form fields using Aspose.Pdf and exporting the form data... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON via FileStream | `Document`, `Form`, `ExportToJson` | Demonstrates how to export all form fields from a PDF document directly to a JSON file using a Fi... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML Using MemoryStream | `Document`, `Form`, `BindPdf` | Demonstrates how to export form fields from a PDF document to an XML representation directly into... |
| [export-pdf-form-fields-to-json-byte-array](./export-pdf-form-fields-to-json-byte-array.cs) | Export PDF Form Fields to JSON Byte Array | `Document`, `ctor(string)`, `Form` | Loads a PDF document, extracts its form fields as JSON, writes the JSON to a MemoryStream, and re... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON Files | `Document`, `Form`, `ExportToJson` | Shows how to process a folder of PDFs, extract each document's form data, and write the data to i... |
| [export-pdf-form-fields-to-xfdf](./export-pdf-form-fields-to-xfdf.cs) | Export PDF Form Fields to XFDF | `Document`, `Form`, `ExportXfdf` | Demonstrates how to use Aspose.Pdf to export form field data from a PDF document to an XFDF file ... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `XmlSaveOptions`, `Save` | Loads a PDF document and saves its form fields as an XML file using Aspose.Pdf's XmlSaveOptions. |
| [export-pdf-page-as-png](./export-pdf-page-as-png.cs) | Export PDF Page as PNG Image at Specified DPI | `Document`, `Page`, `Resolution` | Shows how to rasterize a single PDF page to a PNG file with a defined resolution (DPI) using Aspo... |
| [export-pdf-table-to-csv-with-border-markers](./export-pdf-table-to-csv-with-border-markers.cs) | Export PDF Table to CSV with Border Markers | `Document`, `TableAbsorber`, `Visit` | Shows how to extract tables from a PDF using Aspose.Pdf's TableAbsorber and write them to a CSV f... |
| [export-pdf-to-svg-with-custom-options](./export-pdf-to-svg-with-custom-options.cs) | Export PDF to SVG with Custom Options and Extract Vector Gra... | `Document`, `SvgSaveOptions`, `SvgExtractionOptions` | Demonstrates how to save an entire PDF as a single SVG using custom DPI and multithreading settin... |
| [export-subpaths-as-separate-svg-images](./export-subpaths-as-separate-svg-images.cs) | Export Subpaths as Separate SVG Images | `Document`, `SvgExtractionOptions`, `SvgExtractor` | Shows how to extract each subpath from a PDF page and save it as an individual SVG file with a tr... |
| [extract-checkbox-states-to-json](./extract-checkbox-states-to-json.cs) | Extract Checkbox States from PDF AcroForm to JSON | `Document`, `Form`, `Field` | Loads a PDF, iterates through its AcroForm fields, captures the checked state of each checkbox, a... |
| [extract-first-page-text-from-pdf](./extract-first-page-text-from-pdf.cs) | Extract First Page Text from PDF | `Document`, `TextDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf, extract the text of the first page using TextDevice, and... |
| [extract-fonts-from-pdf](./extract-fonts-from-pdf.cs) | Extract Fonts from PDF and Save as TTF Files | `Document`, `FontUtilities`, `GetAllFonts` | Shows how to load a PDF with Aspose.Pdf, retrieve all embedded fonts, and export each font to a .... |
| [extract-graphics-from-pdf-pages](./extract-graphics-from-pdf-pages.cs) | Extract Graphics from Specific PDF Pages to SVG | `Document`, `Page`, `GraphicsAbsorber` | Demonstrates how to use GraphicsAbsorber and SvgExtractor to collect graphic elements from select... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF and Save as PNG | `Document`, `Page`, `XImage` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through each page's image resources, and ... |
| ... | | | *and 34 more files* |

## Category Statistics
- Total examples: 64

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

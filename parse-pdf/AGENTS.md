---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **63** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (63/63 files) ← category-specific
- `using Aspose.Pdf.Forms;` (21/63 files)
- `using Aspose.Pdf.Text;` (16/63 files)
- `using Aspose.Pdf.Vector;` (11/63 files)
- `using Aspose.Pdf.Annotations;` (3/63 files)
- `using Aspose.Pdf.Devices;` (2/63 files)
- `using Aspose.Pdf.Drawing;` (1/63 files)
- `using System;` (63/63 files)
- `using System.IO;` (63/63 files)
- `using System.Collections.Generic;` (9/63 files)
- `using System.Text;` (9/63 files)
- `using System.Text.Json;` (3/63 files)
- `using System.Linq;` (2/63 files)
- `using System.Globalization;` (1/63 files)
- `using System.Text.RegularExpressions;` (1/63 files)
- `using System.Threading.Tasks;` (1/63 files)
- `using System.Xml;` (1/63 files)

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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `HasVectorGraphics` | Shows how to iterate through PDF files in a folder, detect pages containing vector graphics, and ... |
| [combine-pdf-form-data-to-json](./combine-pdf-form-data-to-json.cs) | Combine Form Data from Multiple PDFs into a JSON Array | `Document`, `Form`, `ExportToJson` | Demonstrates loading several PDF files, exporting each form's fields to JSON, and merging them in... |
| [combine-pdf-vector-graphics-to-svg](./combine-pdf-vector-graphics-to-svg.cs) | Combine PDF Vector Graphics into a Single SVG | `Document`, `Page`, `HasVectorGraphics` | Loads a PDF, extracts vector graphics from each page using Aspose.Pdf, and merges the graphics in... |
| [combine-pdf-vector-graphics-to-svg__v2](./combine-pdf-vector-graphics-to-svg__v2.cs) | Combine Vector Graphics from Multiple PDFs into a Single SVG | `Document`, `Page`, `HasVectorGraphics` | Shows how to extract vector graphics from each page of several PDF documents using Aspose.Pdf's S... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in PDF | `Document`, `Form`, `ctor(string)` | Shows how to load a PDF with Aspose.Pdf and retrieve the total number of AcroForm fields using th... |
| [enumerate-pdf-form-fields-on-page](./enumerate-pdf-form-fields-on-page.cs) | Enumerate PDF Form Fields on a Page | `Document`, `Page`, `FieldsInTabOrder` | Demonstrates opening a PDF with Aspose.Pdf, selecting a specific page, retrieving its form fields... |
| [export-acroform-fields-to-xfdf](./export-acroform-fields-to-xfdf.cs) | Export PDF AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf` | Loads a PDF document, creates a FileStream, and exports the AcroForm fields (as annotations) to a... |
| [export-filtered-pdf-form-fields-to-json](./export-filtered-pdf-form-fields-to-json.cs) | Export Filtered PDF Form Fields to JSON | `Document`, `Form`, `Field` | Loads a PDF, selects form fields whose names start with a given prefix, and writes their values t... |
| [export-pdf-acroform-fields-to-json](./export-pdf-acroform-fields-to-json.cs) | Export PDF AcroForm Fields to JSON | `Document`, `Form`, `ExportToJson` | Loads a PDF document, extracts all AcroForm field names and values, and writes them to a JSON fil... |
| [export-pdf-annotations-to-xfdf](./export-pdf-annotations-to-xfdf.cs) | Export PDF Annotations to XFDF File | `Document`, `ExportAnnotationsToXfdf` | Loads a PDF document and uses Aspose.Pdf's Document.ExportAnnotationsToXfdf method to write annot... |
| [export-pdf-annotations-to-xfdf__v2](./export-pdf-annotations-to-xfdf__v2.cs) | Export PDF Annotations to XFDF | `Document`, `ExportAnnotationsToXfdf` | Demonstrates loading a PDF with Aspose.Pdf, exporting its annotations and form field data to an X... |
| [export-pdf-form-data-to-json-bytes](./export-pdf-form-data-to-json-bytes.cs) | Export PDF Form Data to JSON Bytes | `Document`, `Form`, `ExportToJson` | Loads a PDF document, extracts all form fields, serializes them to JSON, and returns the JSON as ... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Loads a PDF document, extracts all form fields, and writes them as a UTF‑8 encoded JSON file on d... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to JSON Files | `Document`, `Form`, `ExportToJson` | Iterates through PDF files in a folder, loads each with Aspose.Pdf, and exports its form fields t... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to build a simple command‑line tool that loads a PDF with Aspose.Pdf and exports its in... |
| [export-pdf-form-data-to-json__v4](./export-pdf-form-data-to-json__v4.cs) | Export PDF Form Data to JSON Stream | `Document`, `Form`, `ExportToJson` | Demonstrates loading a PDF, accessing its form, and exporting all form field values directly to a... |
| [export-pdf-form-data-to-xfdf](./export-pdf-form-data-to-xfdf.cs) | Export PDF Form Data to XFDF using Aspose.Pdf | `Document`, `ExportAnnotationsToXfdf` | The example loads a PDF document with Aspose.Pdf and exports its form field data (as annotations)... |
| [export-pdf-form-data-to-xml-xfdf](./export-pdf-form-data-to-xml-xfdf.cs) | Export PDF Form Data to XML (XFDF) Using MemoryStream | `Document`, `ExportAnnotationsToXfdf` | Demonstrates how to export form field data from a PDF document to an XML (XFDF) representation di... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON | `Document`, `ExportToJson`, `ExportFieldsToJsonOptions` | Shows how to load a PDF document, export all its form fields to a JSON string using Aspose.Pdf's ... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `Field`, `TextBoxField` | Shows how to load a PDF with Aspose.Pdf, iterate its form fields, extract each field's value, and... |
| [export-pdf-page-as-png](./export-pdf-page-as-png.cs) | Export PDF Page as PNG Image | `Document`, `Page`, `Resolution` | Shows how to rasterize a PDF page, including all vector elements, into a PNG file at a specified ... |
| [export-pdf-pages-vector-graphics-to-svg](./export-pdf-pages-vector-graphics-to-svg.cs) | Export PDF Pages Vector Graphics to SVG Preserving Coordinat... | `Document`, `Page`, `TrySaveVectorGraphics` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and save each page's vector g... |
| [export-pdf-subpaths-to-separate-svgs](./export-pdf-subpaths-to-separate-svgs.cs) | Export PDF Subpaths as Separate SVG Images | `Document`, `Page`, `SvgExtractionOptions` | Shows how to extract each subpath of graphic elements from a PDF page and save them as individual... |
| [export-pdf-tables-to-csv-with-border-markers](./export-pdf-tables-to-csv-with-border-markers.cs) | Export PDF Tables to CSV with Visual Border Markers | `Document`, `TableAbsorber`, `Visit` | Loads a PDF, extracts tables using TableAbsorber, and writes them to a CSV file where each cell i... |
| [export-pdf-to-pdf-html-xml-with-stream-disposal](./export-pdf-to-pdf-html-xml-with-stream-disposal.cs) | Export PDF to PDF, HTML, and XML with Proper Stream Disposal | `Document`, `Save`, `HtmlSaveOptions` | Demonstrates how to export a PDF document to a copy PDF, HTML, and XML using Aspose.Pdf while cor... |
| [export-pdf-to-svg-with-custom-options](./export-pdf-to-svg-with-custom-options.cs) | Export PDF to SVG with Custom Rendering Options | `Document`, `SvgSaveOptions`, `SvgExtractionOptions` | Shows how to convert a PDF document to SVG using Aspose.Pdf with custom DPI scaling, multithreadi... |
| [extract-checkbox-states-to-json](./extract-checkbox-states-to-json.cs) | Extract Checkbox States from PDF AcroForm to JSON | `Document`, `Form`, `Field` | Loads a PDF, iterates through its AcroForm fields, collects the checked state of each checkbox, a... |
| [extract-filter-graphics-by-area](./extract-filter-graphics-by-area.cs) | Extract and Filter PDF Graphics by Area to SVG | `Document`, `Page`, `GraphicsAbsorber` | Demonstrates how to use Aspose.Pdf to collect graphic elements from each PDF page, filter them by... |
| [extract-first-page-text-to-utf8-file](./extract-first-page-text-to-utf8-file.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to load a PDF with Aspose.Pdf, extract the text from its first page using a Text... |
| [extract-fonts-from-pdf-ttf](./extract-fonts-from-pdf-ttf.cs) | Extract Fonts from PDF and Save as TTF Files | `Document`, `IDocumentFontUtilities`, `GetAllFonts` | Shows how to load a PDF with Aspose.Pdf, retrieve all embedded fonts, and export each font to a .... |
| ... | | | *and 33 more files* |

## Category Statistics
- Total examples: 63

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

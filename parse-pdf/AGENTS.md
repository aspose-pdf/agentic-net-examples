---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **64** verified, compile-tested examples for **Aspose.PDF for .NET** 26.5.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (62/64 files) ← category-specific
- `using Aspose.Pdf.Forms;` (20/64 files)
- `using Aspose.Pdf.Text;` (16/64 files)
- `using Aspose.Pdf.Vector;` (11/64 files)
- `using Aspose.Pdf.Annotations;` (6/64 files)
- `using Aspose.Pdf.Drawing;` (4/64 files)
- `using Aspose.Pdf.Devices;` (2/64 files)
- `using Aspose.Pdf.Facades;` (1/64 files)
- `using System;` (64/64 files)
- `using System.IO;` (64/64 files)
- `using System.Collections.Generic;` (13/64 files)
- `using System.Text;` (7/64 files)
- `using System.Text.Json;` (3/64 files)
- `using System.Linq;` (2/64 files)
- `using System.Collections;` (1/64 files)
- `using System.Globalization;` (1/64 files)
- `using System.Threading.Tasks;` (1/64 files)
- `using System.Xml;` (1/64 files)

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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `TrySaveVectorGraphics` | Demonstrates how to iterate through a folder of PDF files, extract each page's vector graphics us... |
| [check-missing-fonts-in-pdf](./check-missing-fonts-in-pdf.cs) | Check for Missing Fonts in a PDF Document | `Document`, `Page`, `Font` | The example loads a PDF, iterates through each page's font resources, and reports any fonts that ... |
| [combine-pdf-form-data-to-json](./combine-pdf-form-data-to-json.cs) | Combine Form Data from Multiple PDFs into a JSON Array | `Document`, `Form`, `ExportToJson` | The example loads several PDF files, exports each form's fields to JSON, and merges the individua... |
| [combine-pdf-vectors-into-multi-page-svg](./combine-pdf-vectors-into-multi-page-svg.cs) | Combine PDF Vector Graphics into a Multi‑Page SVG | `Document`, `Page`, `HasVectorGraphics` | Loads multiple PDF files, extracts vector graphics from each page using Aspose.Pdf's SvgExtractor... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in a PDF | `Document`, `Form`, `Form` | Loads a PDF using Aspose.Pdf, accesses the Form collection, retrieves the total number of AcroFor... |
| [enumerate-pdf-form-fields-on-page](./enumerate-pdf-form-fields-on-page.cs) | Enumerate PDF Form Fields on a Specific Page | `Document`, `Form`, `WidgetAnnotation` | Loads a PDF document, checks for form fields, and iterates over widget annotations on a given pag... |
| [export-acroform-fields-to-json](./export-acroform-fields-to-json.cs) | Export AcroForm Fields to JSON | `Document`, `Form`, `Form` | Shows how to load a PDF with Aspose.Pdf, access its AcroForm fields, and export them directly to ... |
| [export-acroform-fields-to-xfdf](./export-acroform-fields-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf` | Loads a PDF document, creates a FileStream, and uses Aspose.Pdf to export all AcroForm fields (an... |
| [export-filtered-pdf-form-fields-to-json](./export-filtered-pdf-form-fields-to-json.cs) | Export Filtered PDF Form Fields to JSON | `Document`, `Form`, `Field` | The example loads a PDF document, iterates over its form fields, selects only those whose names s... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Form`, `BindPdf`, `ExportFdf` | Demonstrates binding a PDF to the Aspose.Pdf.Facades.Form facade and exporting its form fields to... |
| [export-pdf-form-data-to-fdf__v2](./export-pdf-form-data-to-fdf__v2.cs) | Export PDF Form Data to FDF Using Aspose.Pdf | `Document`, `Form`, `ExportFdf` | Demonstrates loading a PDF document, accessing its form fields via the Form facade, and exporting... |
| [export-pdf-form-data-to-json-byte-array](./export-pdf-form-data-to-json-byte-array.cs) | Export PDF Form Fields to JSON Byte Array | `Document`, `Form`, `ExportToJson` | Loads a PDF, extracts all form fields as JSON using Aspose.Pdf, writes the JSON to a MemoryStream... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF with Aspose.Pdf, export all form fields to a JSON string using the Export... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to JSON Files | `Document`, `Form`, `ExportToJson` | Shows how to iterate over PDF files in a folder, load each with Aspose.Pdf, and export its form f... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to create a command‑line utility that loads a PDF with Aspose.Pdf and exports its inter... |
| [export-pdf-form-data-to-json__v4](./export-pdf-form-data-to-json__v4.cs) | Export PDF Form Data to JSON via FileStream | `Document`, `Form`, `ExportFieldsToJsonOptions` | Shows how to load a PDF, set JSON export options, and write all form fields directly to a JSON fi... |
| [export-pdf-form-data-to-xfdf](./export-pdf-form-data-to-xfdf.cs) | Export PDF Form Data to XFDF Using FileStream | `Document`, `Form`, `ExportXfdf` | Demonstrates loading a PDF, using the Form facade to export its form fields to an XFDF file via a... |
| [export-pdf-form-data-to-xml-memorystream](./export-pdf-form-data-to-xml-memorystream.cs) | Export PDF Form Data to XML via MemoryStream | `Form`, `ExportXml` | Shows how to export form fields from a PDF document to an XML representation using Aspose.Pdf.Fac... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `Form`, `Field` | Loads a PDF document, iterates through its form fields, and writes each field's name and value to... |
| [export-pdf-page-as-raster-image](./export-pdf-page-as-raster-image.cs) | Export PDF Page as Raster Image with Specified DPI | `Document`, `Page`, `Resolution` | Shows how to load a PDF, flatten transparency, and convert a page (including vector graphics) to ... |
| [export-pdf-tables-to-csv-with-border-markers](./export-pdf-tables-to-csv-with-border-markers.cs) | Export PDF Tables to CSV with Border Markers | `Document`, `Page`, `TableAbsorber` | Demonstrates how to extract tables from a PDF using Aspose.Pdf, preserve visual cell borders, and... |
| [export-pdf-to-pdf-html-svg-with-stream-cleanup](./export-pdf-to-pdf-html-svg-with-stream-cleanup.cs) | Export PDF to PDF, HTML, and SVG with Stream Cleanup | `Document`, `HtmlSaveOptions`, `SvgSaveOptions` | Demonstrates loading a PDF with Aspose.Pdf and exporting it to PDF, HTML, and SVG formats while p... |
| [export-pdf-to-svg-with-dpi-options](./export-pdf-to-svg-with-dpi-options.cs) | Export PDF to SVG with Custom DPI and Extraction Options | `Document`, `SvgSaveOptions`, `SvgExtractionOptions` | Demonstrates converting a PDF to SVG using Aspose.Pdf with pixel‑based scaling (DPI) and multithr... |
| [extract-annotate-superscripts-subscripts-pdf](./extract-annotate-superscripts-subscripts-pdf.cs) | Extract and Annotate Superscripts/Subscripts from PDF | `Document`, `TextFragmentAbsorber`, `TextExtractionOptions` | The example loads a PDF, extracts text fragments with positioning information, heuristically iden... |
| [extract-checkbox-states-from-pdf-acroform](./extract-checkbox-states-from-pdf-acroform.cs) | Extract Checkbox States from PDF AcroForm | `Document`, `Field`, `CheckboxField` | Loads a PDF, iterates over its AcroForm fields, collects the checked state of each checkbox, and ... |
| [extract-export-pdf-fonts-ttf](./extract-export-pdf-fonts-ttf.cs) | Extract and Export Fonts from a PDF to TTF Files | `Document`, `Page`, `Font` | Shows how to load a PDF with Aspose.Pdf, iterate through each page's font resources, and save eac... |
| [extract-first-page-text-to-utf8-file](./extract-first-page-text-to-utf8-file.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to load a PDF with Aspose.Pdf, extract the text of the first page using a TextAb... |
| [extract-graphics-svg-from-selected-pdf-pages](./extract-graphics-svg-from-selected-pdf-pages.cs) | Extract Graphics as SVG from Selected PDF Pages | `Document`, `Page`, `GraphicsAbsorber` | Shows how to use GraphicsAbsorber to collect graphic elements from specific PDF pages and convert... |
| [extract-images-from-pdf-to-png](./extract-images-from-pdf-to-png.cs) | Extract Images from PDF and Save as PNG | `Document`, `Page`, `XImage` | Loads a PDF with Aspose.Pdf, iterates through each page's image resources, and saves every extrac... |
| [extract-images-using-graphicsabsorber](./extract-images-using-graphicsabsorber.cs) | Extract Images from PDF Using GraphicsAbsorber | `Document`, `Page`, `GraphicsAbsorber` | Shows how to employ GraphicsAbsorber to collect only image elements on each PDF page, ignore othe... |
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
Updated: 2026-06-24 | Run: `20260624_150249_f15775`
<!-- AUTOGENERATED:END -->

---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **57** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (57/57 files) ← category-specific
- `using Aspose.Pdf.Forms;` (19/57 files)
- `using Aspose.Pdf.Text;` (12/57 files)
- `using Aspose.Pdf.Annotations;` (3/57 files)
- `using Aspose.Pdf.Devices;` (3/57 files)
- `using Aspose.Pdf.Drawing;` (3/57 files)
- `using Aspose.Pdf.Facades;` (1/57 files)
- `using Aspose.Pdf.Vector;` (1/57 files)
- `using System;` (57/57 files)
- `using System.IO;` (57/57 files)
- `using System.Collections.Generic;` (14/57 files)
- `using System.Linq;` (10/57 files)
- `using System.Text.Json;` (5/57 files)
- `using System.Collections;` (2/57 files)
- `using System.Collections.Concurrent;` (1/57 files)
- `using System.Drawing;` (1/57 files)
- `using System.Drawing.Imaging;` (1/57 files)
- `using System.Drawing.Text;` (1/57 files)
- `using System.Globalization;` (1/57 files)
- `using System.Reflection;` (1/57 files)
- `using System.Text;` (1/57 files)
- `using System.Text.RegularExpressions;` (1/57 files)
- `using System.Threading.Tasks;` (1/57 files)
- `using System.Xml;` (1/57 files)

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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Convert PDF Pages to SVG | `Document`, `SvgSaveOptions`, `Pages` | Shows how to iterate through PDF files in a folder, create a single‑page document for each page, ... |
| [combine-pdf-form-data-into-json-array](./combine-pdf-form-data-into-json-array.cs) | Combine PDF Form Data into JSON Batch | `Document`, `Field`, `Form` | Extracts form fields from multiple PDFs, aggregates the data into a list of objects, and writes t... |
| [combine-pdf-page-vector-graphics-into-svg](./combine-pdf-page-vector-graphics-into-svg.cs) | Extract Vector Graphics to Multi‑Page SVG | `Document`, `SvgSaveOptions`, `Save` | Loads a PDF document and saves its vector graphics as a single multi‑page SVG file using Aspose.Pdf. |
| [combine-pdf-vectors-to-svg](./combine-pdf-vectors-to-svg.cs) | Combine Multiple PDFs into a Multi‑Page SVG | `Document`, `Pages`, `SvgSaveOptions` | Demonstrates merging several PDF documents and exporting the combined result as a single multi‑pa... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in PDF | `Document`, `Form` | Loads a PDF using Aspose.Pdf, accesses the Form collection, and prints the total number of AcroFo... |
| [count-form-fields-on-each-pdf-page](./count-form-fields-on-each-pdf-page.cs) | Count Form Fields per PDF Page | `Document`, `Field`, `Form` | Shows how to load a PDF with Aspose.Pdf, iterate over its form fields, count the fields on each p... |
| [enumerate-pdf-form-fields](./enumerate-pdf-form-fields.cs) | Enumerate PDF Form Fields on a Specific Page | `Document`, `Form`, `Field` | Shows how to list all form fields that belong to a given page of a PDF, outputting each field's n... |
| [export-acroform-fields-to-json](./export-acroform-fields-to-json.cs) | Extract AcroForm Fields to JSON | `Document`, `Form`, `Field` | Shows how to read AcroForm field names and values from a PDF using Aspose.Pdf and serialize the c... |
| [export-acroform-to-xfdf](./export-acroform-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `Form`, `ExportAnnotationsToXfdf` | Loads a PDF, verifies the presence of AcroForm fields, and exports those fields to an XFDF file u... |
| [export-pdf-annotations-to-xfdf](./export-pdf-annotations-to-xfdf.cs) | Export PDF Form Data to XFDF | `Document`, `Form`, `ExportXfdf` | Demonstrates loading a PDF, accessing its AcroForm, and exporting the form data to an XFDF file u... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF Using FileStream | `Document`, `Form`, `Field` | Loads a PDF, verifies it contains form fields, and writes those fields to an FDF file using a Fil... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON Stream | `Document`, `Form`, `ExportToJson` | Demonstrates how to load a PDF document with a form and export its field data directly to a JSON ... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Fields to JSON via MemoryStream | `Document`, `Form`, `ExportToJson` | Loads a PDF document, accesses its AcroForm, and writes the form fields as JSON directly into a M... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `Form`, `FieldCollection` | Loads a PDF document, iterates over its form fields, and writes each field's name, type, and valu... |
| [export-pdf-form-fields-with-prefix-to-json](./export-pdf-form-fields-with-prefix-to-json.cs) | Export PDF Form Fields with Prefix Filter | `Document`, `Field`, `Form` | Loads a PDF, filters its form fields whose names start with a specified prefix, and writes the fi... |
| [export-pdf-form-to-fdf](./export-pdf-form-to-fdf.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `Count` | Loads a PDF document, checks for form fields, and exports the form data to a JSON file using Aspo... |
| [export-pdf-page-as-png](./export-pdf-page-as-png.cs) | Export PDF Page as PNG with Specified DPI | `Document`, `Resolution`, `PngDevice` | Demonstrates how to rasterize a PDF page to a PNG image at a custom resolution using Aspose.Pdf. |
| [export-pdf-pages-to-svg-preserving-coordinate-syst...](./export-pdf-pages-to-svg-preserving-coordinate-system.cs) | Convert PDF to SVG Preserving Original Coordinate System | `Document`, `SvgSaveOptions`, `Save` | Demonstrates how to convert a PDF file to SVG using Aspose.Pdf while keeping the original coordin... |
| [export-pdf-tables-to-csv-with-borders](./export-pdf-tables-to-csv-with-borders.cs) | Export PDF Tables to CSV with Visual Delimiter Markers | `Document`, `TextAbsorber`, `TextExtractionOptions` | Loads a PDF, extracts its text while preserving layout, splits the text into rows and columns bas... |
| [export-pdf-to-html-png-xfdf](./export-pdf-to-html-png-xfdf.cs) | Export PDF to Multiple Formats with Aspose.Pdf | `Document`, `Save`, `HtmlSaveOptions` | Loads a PDF document and exports it to HTML, SVG, DOCX, XLSX, PPTX, EPUB, and XML, using Aspose.P... |
| [export-pdf-to-svg-with-custom-options](./export-pdf-to-svg-with-custom-options.cs) | Export PDF to SVG with DPI and CSS Styling | `Document`, `SvgSaveOptions`, `Save` | Shows how to load a PDF, configure SVG export options such as raster image resolution (DPI) and C... |
| [extract-checkbox-states-from-pdf](./extract-checkbox-states-from-pdf.cs) | Extract Checkbox States from PDF AcroForm | `Document`, `Form`, `Field` | Loads a PDF, checks for an AcroForm, iterates over its fields, extracts the checked state of each... |
| [extract-first-page-text-to-utf8-file](./extract-first-page-text-to-utf8-file.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `TextAbsorber`, `TextExtractionOptions` | Demonstrates how to load a PDF with Aspose.Pdf, extract the text of the first page using a TextAb... |
| [extract-fonts-from-pdf-ttf](./extract-fonts-from-pdf-ttf.cs) | Extract Embedded Fonts from PDF as TTF/OTF Files | `Document`, `Page`, `Font` | Demonstrates how to read a PDF, locate embedded font resources, and save them as .ttf or .otf fil... |
| [extract-graphic-element-as-svg](./extract-graphic-element-as-svg.cs) | Extract Graphic Element from PDF and Save as SVG | `Document`, `Page`, `XImage` | Demonstrates how to locate an image on a PDF page by its index, copy it into a new PDF, and expor... |
| [extract-graphics-from-specific-pdf-pages](./extract-graphics-from-specific-pdf-pages.cs) | Extract Vector Graphics from Specific PDF Pages | `Document`, `Page`, `GraphicsAbsorber` | Shows how to use Aspose.Pdf's GraphicsAbsorber to visit selected pages and extract vector graphic... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF and Save as PNG | `Document`, `Page`, `Resources` | Shows how to iterate through PDF pages using Aspose.Pdf, extract each embedded XImage, and write ... |
| [extract-paragraphs-with-line-breaks](./extract-paragraphs-with-line-breaks.cs) | Extract Paragraph Text from PDF Preserving Line Breaks | `Document`, `TextAbsorber`, `TextExtractionOptions` | Demonstrates loading a PDF with Aspose.Pdf, extracting its text while keeping paragraph and line ... |
| [extract-pdf-font-table-form-field-statistics](./extract-pdf-font-table-form-field-statistics.cs) | Extract PDF Font, Table, and Form Field Counts | `Document`, `Page`, `Resources` | Demonstrates how to open PDF files with Aspose.Pdf and count the number of fonts, tables, and for... |
| [extract-pdf-form-field-values](./extract-pdf-form-field-values.cs) | Extract PDF Form Fields to Dictionary | `Document`, `Form`, `Fields` | Demonstrates loading a PDF with Aspose.Pdf, iterating over its form fields, and returning a case‑... |
| ... | | | *and 27 more files* |

## Category Statistics
- Total examples: 57

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

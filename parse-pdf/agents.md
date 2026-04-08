---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

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
- `using Aspose.Pdf.Forms;` (17/64 files)
- `using Aspose.Pdf.Text;` (14/64 files)
- `using Aspose.Pdf.Vector;` (12/64 files)
- `using Aspose.Pdf.Facades;` (7/64 files)
- `using Aspose.Pdf.Annotations;` (3/64 files)
- `using Aspose.Pdf.Devices;` (3/64 files)
- `using Aspose.Pdf.Drawing;` (3/64 files)
- `using System;` (64/64 files)
- `using System.IO;` (64/64 files)
- `using System.Collections.Generic;` (14/64 files)
- `using System.Text;` (8/64 files)
- `using System.Linq;` (4/64 files)
- `using System.Text.Json;` (4/64 files)
- `using System.Drawing.Imaging;` (2/64 files)
- `using System.Collections.Concurrent;` (1/64 files)
- `using System.Drawing;` (1/64 files)
- `using System.Globalization;` (1/64 files)
- `using System.Text.Json.Nodes;` (1/64 files)
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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `HasVectorGraphics`, `TrySaveVectorGraphics` | Demonstrates how to iterate through PDF files in a folder, detect pages with vector graphics, and... |
| [combine-pdf-form-data-into-json-array](./combine-pdf-form-data-into-json-array.cs) | Combine PDF Form Data into a Single JSON Array | `Document`, `ExportToJson` | Shows how to load several PDF files, export each form's fields to JSON using Aspose.Pdf, and merg... |
| [combine-pdf-vector-graphics-into-svg](./combine-pdf-vector-graphics-into-svg.cs) | Combine PDF Vector Graphics into a Single SVG | `Document`, `Page`, `SvgExtractor` | Loads a PDF, extracts vector graphics from each page using Aspose.Pdf's SvgExtractor, removes the... |
| [combine-pdfs-into-multi-page-svg](./combine-pdfs-into-multi-page-svg.cs) | Combine Multiple PDFs into a Single Multi‑Page SVG | `Document`, `Page`, `Extract` | Loads several PDF files, extracts each page as SVG fragments with Aspose.Pdf's SvgExtractor, and ... |
| [convert-pdf-to-markdown-preserve-indentation](./convert-pdf-to-markdown-preserve-indentation.cs) | Convert PDF to Markdown While Preserving Paragraph Indentati... | `Document`, `ProcessParagraphs`, `MarkdownSaveOptions` | Demonstrates loading a PDF with Aspose.Pdf, processing its paragraphs to retain indentation, and ... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in PDF | `Document`, `Form`, `Count` | Loads a PDF using Aspose.Pdf and retrieves the total number of AcroForm fields via Document.Form.... |
| [enumerate-pdf-form-fields-on-page](./enumerate-pdf-form-fields-on-page.cs) | Enumerate PDF Form Fields on a Page | `Document`, `Page`, `Field` | Loads a PDF, selects a specific page, and iterates through all form fields on that page, logging ... |
| [export-acroform-fields-to-xfdf](./export-acroform-fields-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf` | Loads a PDF containing AcroForm fields and exports those fields to an XFDF file using a FileStream. |
| [export-large-graphics-from-pdf](./export-large-graphics-from-pdf.cs) | Export Large Graphics from PDF as SVG | `Document`, `Page`, `GraphicsAbsorber` | Loads a PDF, filters graphic elements by bounding rectangle area, and exports each qualifying gra... |
| [export-pdf-acroform-fields-to-json](./export-pdf-acroform-fields-to-json.cs) | Export PDF AcroForm Fields to JSON | `Document`, `ExportToJson` | Loads a PDF document, extracts all AcroForm field names and values, and writes them to a JSON fil... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Document`, `BindPdf`, `ExportFdf` | Shows how to load a PDF, bind it to the Form facade, and export its form fields to an FDF file us... |
| [export-pdf-form-data-to-fdf__v2](./export-pdf-form-data-to-fdf__v2.cs) | Export PDF Form Data to FDF File | `Document`, `Form`, `BindPdf` | Shows how to load a PDF, bind it to the Form facade, and export its form fields to an FDF file us... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `ExportToJson` | Shows how to load a PDF document with Aspose.Pdf, export its form fields to a JSON string using F... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to UTF-8 JSON File | `Document`, `ExportToJson` | Shows how to open a PDF with Aspose.Pdf, extract all form fields, and write them to a UTF‑8 encod... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON | `Document`, `ExportToJson` | Demonstrates how to load a PDF document with Aspose.Pdf and export its form fields to a JSON file... |
| [export-pdf-form-data-to-json__v4](./export-pdf-form-data-to-json__v4.cs) | Export PDF Form Data to JSON via FileStream | `Document`, `ExportToJson` | Shows how to load a PDF with Aspose.Pdf, access its form, and export all form fields directly to ... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML Using a MemoryStream | `Form`, `BindPdf`, `ExportXml` | Shows how to bind a PDF containing form fields, export the form data as XML directly into a Memor... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Form`, `BindPdf`, `ExportXml` | Shows how to bind a PDF document with Aspose.Pdf.Facades.Form and export its form fields to an XM... |
| [export-pdf-form-fields-with-prefix-to-json](./export-pdf-form-fields-with-prefix-to-json.cs) | Export PDF Form Fields with Specific Prefix to JSON | `Document`, `Field` | Loads a PDF, filters form fields whose names start with a given prefix, and writes the field name... |
| [export-pdf-form-to-xfdf](./export-pdf-form-to-xfdf.cs) | Export PDF Form Data to XFDF Using Form Facade | `Form`, `BindPdf`, `ExportXfdf` | Shows how to bind a PDF document to the Aspose.Pdf.Facades.Form class and export its form annotat... |
| [export-pdf-page-as-raster-png](./export-pdf-page-as-raster-png.cs) | Export PDF Page as Raster PNG with Specified DPI | `Document`, `Resolution`, `PngDevice` | Demonstrates how to rasterize a PDF page, including all vector graphics, into a PNG image at a gi... |
| [export-pdf-subpaths-as-separate-svg](./export-pdf-subpaths-as-separate-svg.cs) | Export PDF Subpaths as Separate SVG Images | `Document`, `Page`, `SvgExtractionOptions` | Shows how to extract each graphic subpath from PDF pages and save them as individual SVG files wi... |
| [export-pdf-tables-to-csv-with-border-markers](./export-pdf-tables-to-csv-with-border-markers.cs) | Export PDF Tables to CSV with Border Markers | `Document`, `TableAbsorber`, `TextFragment` | Demonstrates extracting tables from a PDF using Aspose.Pdf, adding visual border markers to each ... |
| [export-pdf-to-html-png-xfdf](./export-pdf-to-html-png-xfdf.cs) | Export PDF to HTML, PNG, and XFDF with Stream Disposal | `Document`, `Save`, `ExportAnnotationsToXfdf` | Loads a PDF using Aspose.Pdf and exports the entire document to HTML, the first page to a PNG ima... |
| [export-pdf-to-svg-with-custom-options](./export-pdf-to-svg-with-custom-options.cs) | Export PDF to SVG with Custom Options and CSS Styling | `Document`, `SvgSaveOptions`, `Page` | Demonstrates exporting a PDF to SVG using SvgSaveOptions (DPI scaling and multithreading) and ext... |
| [extract-acroform-field-values](./extract-acroform-field-values.cs) | Extract AcroForm Field Values from a PDF | `Document`, `Form`, `GetField` | Loads a PDF file, accesses its AcroForm via the Form facade, and prints each field name with its ... |
| [extract-checkbox-states-from-pdf-acroform](./extract-checkbox-states-from-pdf-acroform.cs) | Extract Checkbox States from PDF AcroForm | `Document`, `Field`, `CheckboxField` | Loads a PDF, iterates over its AcroForm fields, collects the checked state of each checkbox, and ... |
| [extract-embedded-images-to-png](./extract-embedded-images-to-png.cs) | Extract Embedded Images from PDF to PNG | `Document`, `Page`, `XImage` | Shows how to load a PDF with Aspose.Pdf, iterate through each page's image resources, and save ea... |
| [extract-first-page-text-to-utf8](./extract-first-page-text-to-utf8.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `TextAbsorber`, `Accept` | Demonstrates loading a PDF with Aspose.Pdf, extracting the text from its first page using TextAbs... |
| [extract-graphic-element-to-svg](./extract-graphic-element-to-svg.cs) | Extract Specific Graphic Element from PDF and Save as SVG | `Document`, `Page`, `GraphicsAbsorber` | Demonstrates how to absorb graphic elements from a PDF page using Aspose.Pdf, select one by its i... |
| ... | | | *and 34 more files* |

## Category Statistics
- Total examples: 64

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-08 | Run: `20260408_122546_4772fc`
<!-- AUTOGENERATED:END -->

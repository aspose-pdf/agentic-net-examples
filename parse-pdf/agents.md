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

- `using Aspose.Pdf;` (64/65 files) ← category-specific
- `using Aspose.Pdf.Forms;` (21/65 files)
- `using Aspose.Pdf.Text;` (15/65 files)
- `using Aspose.Pdf.Vector;` (12/65 files)
- `using Aspose.Pdf.Annotations;` (2/65 files)
- `using Aspose.Pdf.Devices;` (2/65 files)
- `using Aspose.Pdf.Drawing;` (1/65 files)
- `using Aspose.Pdf.LogicalStructure;` (1/65 files)
- `using Aspose.Pdf.Tagged;` (1/65 files)
- `using System;` (65/65 files)
- `using System.IO;` (64/65 files)
- `using System.Collections.Generic;` (15/65 files)
- `using System.Text;` (7/65 files)
- `using System.Text.Json;` (4/65 files)
- `using System.Linq;` (3/65 files)
- `using System.Collections.Concurrent;` (1/65 files)
- `using System.Drawing;` (1/65 files)
- `using System.Drawing.Imaging;` (1/65 files)
- `using System.Globalization;` (1/65 files)
- `using System.Runtime.InteropServices;` (1/65 files)
- `using System.Text.RegularExpressions;` (1/65 files)
- `using System.Threading.Tasks;` (1/65 files)
- `using System.Xml;` (1/65 files)

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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `HasVectorGraphics` | Shows how to scan a folder of PDF files, detect pages that contain vector graphics, and save each... |
| [combine-pdf-form-data-to-json-array](./combine-pdf-form-data-to-json-array.cs) | Combine PDF Form Data into a Single JSON Array | `Document`, `Form`, `ExportToJson` | The example loads several PDF files, extracts their form fields as JSON using Aspose.Pdf, and mer... |
| [combine-pdf-vectors-to-svg](./combine-pdf-vectors-to-svg.cs) | Combine Vector Graphics from Multiple PDFs into a Single SVG | `Document`, `Page`, `SvgExtractor` | The example loads several PDF files, extracts vector graphics from each page using Aspose.Pdf's S... |
| [convert-pdf-to-markdown-preserve-indentation](./convert-pdf-to-markdown-preserve-indentation.cs) | Convert PDF to Markdown Preserving Paragraph Indentation | `Document`, `MarkdownSaveOptions`, `Save` | Demonstrates loading a PDF with Aspose.Pdf and saving it as a formatted Markdown file while keepi... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in PDF | `Document`, `Form`, `Count` | Loads a PDF document with Aspose.Pdf and retrieves the total number of AcroForm fields using the ... |
| [enumerate-pdf-form-fields-on-page](./enumerate-pdf-form-fields-on-page.cs) | Enumerate PDF Form Fields on a Specific Page | `Document`, `Page`, `Field` | Loads a PDF document, selects a given page, and logs each form field’s name, type, and value. |
| [export-acroform-fields-to-xfdf](./export-acroform-fields-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf`, `Form` | Demonstrates exporting AcroForm fields (annotations) from a PDF to an XFDF file using Aspose.Pdf'... |
| [export-filtered-pdf-form-fields-to-json](./export-filtered-pdf-form-fields-to-json.cs) | Export Filtered PDF Form Fields to JSON | `Document`, `Form`, `Form` | The example opens a PDF, selects form fields whose names start with a given prefix, extracts thei... |
| [export-pdf-acroform-fields-to-json](./export-pdf-acroform-fields-to-json.cs) | Export PDF AcroForm Fields to JSON | `Document`, `Form`, `Form` | Loads a PDF document, extracts all AcroForm field names and values, and writes them to a JSON fil... |
| [export-pdf-annotations-to-xfdf](./export-pdf-annotations-to-xfdf.cs) | Export PDF Annotations to XFDF Using a FileStream | `Document`, `ExportAnnotationsToXfdf` | Demonstrates loading a PDF with Aspose.Pdf, creating a FileStream, and exporting all annotations ... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF | `Document`, `Form`, `ExportFdf` | Demonstrates loading a PDF with Aspose.Pdf, binding it to the Facade Form class, and exporting th... |
| [export-pdf-form-data-to-fdf__v2](./export-pdf-form-data-to-fdf__v2.cs) | Export PDF Form Data to FDF Using Aspose.Pdf | `Document`, `Form`, `ExportFdf` | Demonstrates how to load a PDF document, bind it to the Aspose.Pdf.Facades.Form facade, and expor... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `ExportToJson` | Shows how to load a PDF document with Aspose.Pdf, export all form fields to a JSON string, and ou... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF with Aspose.Pdf and export its interactive form fields to a JSON file usi... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON Stream | `Document`, `Form`, `ExportToJson` | Demonstrates opening a PDF, creating a FileStream, and exporting all form fields directly to a JS... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML Using MemoryStream | `Document`, `Field`, `TextBoxField` | Loads a PDF with Aspose.Pdf, iterates its form fields, and writes each field's name, type, and va... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export PDF Form Fields to JSON Files | `Document`, `Form`, `Form` | Iterates through PDF files in a folder, loads each document with Aspose.Pdf, and exports its form... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `SaveXml` | Shows how to load a PDF document with Aspose.Pdf and export its form fields to an XML file using ... |
| [export-pdf-page-as-png](./export-pdf-page-as-png.cs) | Export PDF Page as PNG Image at Specified DPI | `Document`, `Resolution`, `PngDevice` | Shows how to rasterize a PDF page into a PNG file using Aspose.Pdf with a custom resolution (DPI). |
| [export-pdf-subpaths-as-png](./export-pdf-subpaths-as-png.cs) | Export PDF Subpaths as Individual PNG Images | `Document`, `Page`, `GraphicsAbsorber` | Shows how to capture each SubPath on PDF pages using GraphicsAbsorber and render each one to a se... |
| [export-pdf-subpaths-to-svg](./export-pdf-subpaths-to-svg.cs) | Export PDF Subpaths to Separate SVG Files | `Document`, `Page`, `SvgExtractionOptions` | Demonstrates how to load a PDF, configure SVG extraction to treat each subpath as an individual g... |
| [export-pdf-tables-to-csv-with-visual-borders](./export-pdf-tables-to-csv-with-visual-borders.cs) | Export PDF Tables to CSV with Visual Cell Borders | `Document`, `Page`, `TableAbsorber` | Demonstrates how to extract tables from a PDF using Aspose.Pdf's TableAbsorber and write them to ... |
| [export-pdf-to-pdf-html-streams](./export-pdf-to-pdf-html-streams.cs) | Export PDF to PDF, HTML, and Streams with Proper FileStream ... | `Document`, `HtmlSaveOptions`, `Save` | Demonstrates saving an Aspose.Pdf Document as a PDF file, as HTML, and into FileStream objects, i... |
| [export-pdf-to-svg-with-dpi-and-css](./export-pdf-to-svg-with-dpi-and-css.cs) | Export PDF to SVG with DPI Scaling and Custom CSS | `Document`, `SvgSaveOptions`, `SvgExtractionOptions` | Demonstrates converting a PDF to SVG with DPI‑like scaling, extracting vector graphics per page, ... |
| [extract-checkbox-states-to-json](./extract-checkbox-states-to-json.cs) | Extract Checkbox States from PDF AcroForm to JSON | `Document`, `Form`, `Field` | Loads a PDF, iterates over its AcroForm fields, collects the checked state of each checkbox, and ... |
| [extract-combine-vector-graphics-pdf-to-svg](./extract-combine-vector-graphics-pdf-to-svg.cs) | Extract and Combine Vector Graphics from PDF into Multi‑Page... | `Document`, `Page`, `SvgExtractor` | Demonstrates how to use Aspose.Pdf to extract vector graphics from each page of a PDF with SvgExt... |
| [extract-embedded-fonts-from-pdf](./extract-embedded-fonts-from-pdf.cs) | Extract Embedded Fonts from PDF and Save as TTF | `Document`, `FontUtilities`, `Font` | Demonstrates loading a PDF with Aspose.Pdf, retrieving all embedded fonts, and exporting each fon... |
| [extract-first-page-text-to-utf8-file](./extract-first-page-text-to-utf8-file.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `ctor(string)`, `Accept` | Demonstrates how to load a PDF with Aspose.Pdf, extract the text of the first page using a TextAb... |
| [extract-graphics-from-specific-pdf-pages](./extract-graphics-from-specific-pdf-pages.cs) | Extract Graphics from Specific PDF Pages to SVG | `Document`, `Page`, `GraphicsAbsorber` | Shows how to load a PDF, select given page numbers, collect graphic elements with GraphicsAbsorbe... |
| [extract-images-from-pdf-using-graphicsabsorber](./extract-images-from-pdf-using-graphicsabsorber.cs) | Extract Images from PDF Using GraphicsAbsorber and Save as J... | `Document`, `Page`, `GraphicsAbsorber` | The example opens a PDF, uses GraphicsAbsorber to locate image graphic elements, renders each pag... |
| ... | | | *and 35 more files* |

## Category Statistics
- Total examples: 65

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_123344_8df8ee`
<!-- AUTOGENERATED:END -->

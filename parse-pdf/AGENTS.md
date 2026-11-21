---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **62** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (61/62 files) ← category-specific
- `using Aspose.Pdf.Forms;` (26/62 files)
- `using Aspose.Pdf.Text;` (15/62 files)
- `using Aspose.Pdf.Annotations;` (3/62 files)
- `using Aspose.Pdf.Devices;` (2/62 files)
- `using Aspose.Pdf.Vector;` (2/62 files)
- `using Aspose.Pdf.Drawing;` (1/62 files)
- `using Aspose.Pdf.LogicalStructure;` (1/62 files)
- `using Aspose.Pdf.Operators;` (1/62 files)
- `using Aspose.Pdf.Tagged;` (1/62 files)
- `using System;` (62/62 files)
- `using System.IO;` (62/62 files)
- `using System.Collections.Generic;` (13/62 files)
- `using System.Linq;` (11/62 files)
- `using System.Text.Json;` (7/62 files)
- `using System.Text;` (6/62 files)
- `using System.Xml.Linq;` (3/62 files)
- `using System.Drawing.Imaging;` (2/62 files)
- `using System.Collections.Concurrent;` (1/62 files)
- `using System.Drawing.Text;` (1/62 files)
- `using System.Globalization;` (1/62 files)
- `using System.Threading.Tasks;` (1/62 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Extract Vector Graphics from PDFs to SVG Files | `Document`, `Save`, `Pages` | Demonstrates how to batch‑process PDF files, extract each page as a separate vector graphic, and ... |
| [combine-pdf-form-data-into-json-array](./combine-pdf-form-data-into-json-array.cs) | Combine Form Data from Multiple PDFs into JSON | `Document`, `Form`, `Field` | Loads several PDF files, extracts all AcroForm field names and values using Aspose.Pdf, and write... |
| [combine-pdf-page-vector-graphics-into-svg](./combine-pdf-page-vector-graphics-into-svg.cs) | Extract Vector Graphics to Multi‑Page SVG | `Document`, `SvgSaveOptions`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, configuring SVG save options, and saving all pages as... |
| [combine-pdf-vectors-to-svg](./combine-pdf-vectors-to-svg.cs) | Combine Multiple PDFs into a Single Multi‑Page SVG | `Document`, `Add`, `SvgSaveOptions` | Shows how to merge several PDF documents and export the combined result as one multi‑page SVG fil... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in a PDF | `Document`, `Form`, `Form` | Loads a PDF using Aspose.Pdf, accesses the Form collection, and prints the total number of AcroFo... |
| [count-form-fields-on-each-pdf-page](./count-form-fields-on-each-pdf-page.cs) | Log Number of Form Fields per PDF Page | `Document`, `Page`, `Annotation` | Demonstrates how to iterate through each page of a PDF using Aspose.Pdf and log the count of form... |
| [enumerate-pdf-form-fields](./enumerate-pdf-form-fields.cs) | Enumerate PDF Form Fields on a Specific Page | `Document`, `Form`, `Field` | Loads a PDF document, checks for form fields, and logs each field’s name, type, and value for the... |
| [export-acroform-fields-to-json](./export-acroform-fields-to-json.cs) | Extract AcroForm Fields to JSON | `Document`, `Form`, `Field` | Loads a PDF, reads its AcroForm fields, and writes the field names and values to a JSON file. |
| [export-acroform-to-xfdf](./export-acroform-to-xfdf.cs) | Export PDF AcroForm Fields to XFDF | `Document`, `Form`, `ExportAnnotationsToXfdf` | Loads a PDF document, checks for the presence of AcroForm fields, creates a FileStream, and expor... |
| [export-pdf-annotations-to-xfdf](./export-pdf-annotations-to-xfdf.cs) | Export PDF Form Data to XFDF | `Document`, `ExportAnnotationsToXfdf` | Demonstrates how to use Aspose.Pdf to export a PDF's form and annotation data to an XFDF file by ... |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to JSON via FileStream | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF, open a FileStream, export the form fields to JSON using Aspose.Pdf, and ... |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF document, retrieve its form fields, and export all form data to a JSON fi... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Save JSON Form Data to UTF-8 File |  | Demonstrates writing a JSON string containing form data to a UTF‑8 encoded file using File.WriteA... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Extract PDF Form Data to JSON Files | `Document`, `TextBoxField`, `CheckboxField` | Loads each PDF in a folder, reads all form fields, and writes the collected values to a formatted... |
| [export-pdf-form-data-to-json__v4](./export-pdf-form-data-to-json__v4.cs) | Export PDF Form Data to JSON or XML | `Document`, `Field`, `Form` | Demonstrates how to read form fields from a PDF using Aspose.Pdf and write the collected name/val... |
| [export-pdf-form-data-to-json__v5](./export-pdf-form-data-to-json__v5.cs) | Export PDF Form Data to JSON via FileStream | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF, verify it contains form fields, and export the form data directly to a J... |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML via MemoryStream | `Document`, `Load`, `Form` | Loads a PDF containing form fields, builds an XML representation of the form data, writes it to a... |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `Form`, `Field` | Loads a PDF document, extracts its form fields, creates an XML representation with field names an... |
| [export-pdf-form-fields-with-prefix-to-json](./export-pdf-form-fields-with-prefix-to-json.cs) | Export PDF Form Fields with Specific Prefix to CSV | `Document`, `Field`, `TextBoxField` | Loads a PDF, filters form fields whose names start with a given prefix, extracts their values, an... |
| [export-pdf-form-to-fdf](./export-pdf-form-to-fdf.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Loads a PDF document, verifies the presence of form fields, and exports the form data to a JSON f... |
| [export-pdf-page-as-png](./export-pdf-page-as-png.cs) | Export PDF Page as Raster PNG Image | `Document`, `Page`, `Resolution` | Shows how to render a specific PDF page to a PNG raster image at a given DPI using Aspose.Pdf. |
| [export-pdf-pages-to-svg-preserving-coordinate-syst...](./export-pdf-pages-to-svg-preserving-coordinate-system.cs) | Convert PDF to SVG Preserving Coordinate System | `Document`, `SvgSaveOptions`, `Save` | Demonstrates how to use Aspose.Pdf to convert a PDF file to SVG while keeping the original PDF co... |
| [export-pdf-tables-to-csv-with-borders](./export-pdf-tables-to-csv-with-borders.cs) | Export PDF Tables to CSV with Border Markers | `Document`, `TextAbsorber`, `TextExtractionOptions` | Demonstrates how to extract table text from a PDF using Aspose.Pdf, split it into rows and cells,... |
| [export-pdf-to-html-png-xfdf](./export-pdf-to-html-png-xfdf.cs) | Export PDF to Multiple Formats with Aspose.Pdf | `Document`, `HtmlSaveOptions`, `SvgSaveOptions` | Shows how to load a PDF using Aspose.Pdf and export it to HTML, SVG, DOCX, and EPUB, closing each... |
| [extract-checkbox-states-from-pdf](./extract-checkbox-states-from-pdf.cs) | Extract Checkbox States from PDF AcroForm | `Document`, `Form`, `Field` | The example loads a PDF, iterates through its AcroForm fields, gathers the checked state of each ... |
| [extract-first-page-text-to-utf8-file](./extract-first-page-text-to-utf8-file.cs) | Extract First Page Text from PDF to UTF-8 File | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to load a PDF with Aspose.Pdf, extract the text of the first page using TextAbso... |
| [extract-fonts-from-pdf-ttf](./extract-fonts-from-pdf-ttf.cs) | Extract Embedded Fonts from PDF as TTF Files | `Document`, `FontUtilities`, `Font` | Loads a PDF, enumerates all embedded fonts, and saves each font's data to a .ttf file in a specif... |
| [extract-graphic-element-as-svg](./extract-graphic-element-as-svg.cs) | Extract PDF Page as SVG | `Document`, `SvgSaveOptions`, `Add` | Loads a PDF document, extracts a specific page by its 1‑based index, and saves that single page a... |
| [extract-graphics-from-specific-pdf-pages](./extract-graphics-from-specific-pdf-pages.cs) | Extract Graphics from Specific PDF Pages | `Document`, `GraphicsAbsorber`, `Visit` | Demonstrates how to use Aspose.Pdf's GraphicsAbsorber to extract graphic objects from selected pa... |
| [extract-images-from-pdf](./extract-images-from-pdf.cs) | Extract Images from PDF and Save as PNG | `Document`, `Page`, `XImage` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through each page's image resources, and ... |
| ... | | | *and 32 more files* |

## Category Statistics
- Total examples: 62

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-13 | Run: `20261113_024521_318864`
<!-- AUTOGENERATED:END -->

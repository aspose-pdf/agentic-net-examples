---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

> **Parse PDF** in PDF using C# / .NET -- **97** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (64/97 files) ← category-specific
- `using Aspose.Pdf.Forms;` (22/97 files)
- `using Aspose.Pdf.Text;` (16/97 files)
- `using Aspose.Pdf.Vector;` (12/97 files)
- `using Aspose.Pdf.Drawing;` (4/97 files)
- `using Aspose.Pdf.Annotations;` (3/97 files)
- `using Aspose.Pdf.Devices;` (2/97 files)
- `using System;` (64/97 files)
- `using System.IO;` (64/97 files)
- `using System.Collections.Generic;` (9/97 files)
- `using System.Text;` (8/97 files)
- `using System.Linq;` (4/97 files)
- `using System.Text.Json;` (4/97 files)
- `using System.Reflection;` (2/97 files)
- `using System.Drawing;` (1/97 files)
- `using System.Drawing.Imaging;` (1/97 files)
- `using System.Globalization;` (1/97 files)
- `using System.Text.Json.Nodes;` (1/97 files)
- `using System.Threading;` (1/97 files)
- `using System.Threading.Tasks;` (1/97 files)
- `using System.Xml;` (1/97 files)

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
| [batch-extract-vector-graphics-to-svg](./batch-extract-vector-graphics-to-svg.cs) | Batch Extract Vector Graphics from PDFs to SVG | `Document`, `Page`, `HasVectorGraphics` | Shows how to iterate over PDF files in a folder, detect pages containing vector graphics, and sav... |
| [combine-pdf-form-data-into-json-array](./combine-pdf-form-data-into-json-array.cs) | Combine PDF Form Data into a Single JSON Array | `Document`, `Form`, `ExportToJson` | Demonstrates loading multiple PDF files, exporting each form's fields to JSON using Aspose.Pdf, a... |
| [combine-pdf-form-data-to-json](./combine-pdf-form-data-to-json.cs) | Combine pdf form data to json |  | Combine pdf form data to json |
| [combine-pdf-page-vector-graphics-into-svg](./combine-pdf-page-vector-graphics-into-svg.cs) | Combine PDF Page Vector Graphics into a Single SVG | `Document`, `Page`, `SvgExtractor` | Loads a PDF, extracts vector graphics from each page using Aspose.Pdf's SvgExtractor, and merges ... |
| [combine-pdf-vector-graphics-to-svg](./combine-pdf-vector-graphics-to-svg.cs) | Combine pdf vector graphics to svg |  | Combine pdf vector graphics to svg |
| [combine-pdf-vector-graphics-to-svg__v2](./combine-pdf-vector-graphics-to-svg__v2.cs) | Combine pdf vector graphics to svg__v2 |  | Combine pdf vector graphics to svg__v2 |
| [combine-pdf-vectors-to-svg](./combine-pdf-vectors-to-svg.cs) | Combine Vector Graphics from Multiple PDFs into a Single SVG | `Document`, `Page`, `HasVectorGraphics` | The example loads several PDF files, extracts vector graphics from each page using Aspose.Pdf's S... |
| [count-acroform-fields-in-pdf](./count-acroform-fields-in-pdf.cs) | Count AcroForm Fields in PDF | `Document`, `Form` | Loads a PDF with Aspose.Pdf and retrieves the total number of AcroForm fields using Document.Form... |
| [count-form-fields-on-each-pdf-page](./count-form-fields-on-each-pdf-page.cs) | Count Form Fields on Each PDF Page | `Document`, `Form`, `Page` | Demonstrates opening a PDF with Aspose.Pdf, verifying the presence of an AcroForm, iterating thro... |
| [enumerate-pdf-form-fields-on-page](./enumerate-pdf-form-fields-on-page.cs) | Enumerate pdf form fields on page |  | Enumerate pdf form fields on page |
| [enumerate-pdf-form-fields](./enumerate-pdf-form-fields.cs) | Enumerate PDF Form Fields on a Page | `Document`, `Page`, `Field` | Loads a PDF, accesses a specific page, and lists each form field on that page with its name, type... |
| [export-acroform-fields-to-json](./export-acroform-fields-to-json.cs) | Export AcroForm Fields to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF containing AcroForm fields and export the field names and values directly... |
| [export-acroform-to-xfdf](./export-acroform-to-xfdf.cs) | Export AcroForm Fields to XFDF | `Document`, `ExportAnnotationsToXfdf` | Shows how to load a PDF with Aspose.Pdf, create a FileStream, and export the document's AcroForm ... |
| [export-filtered-pdf-form-fields-to-json](./export-filtered-pdf-form-fields-to-json.cs) | Export filtered pdf form fields to json |  | Export filtered pdf form fields to json |
| [export-pdf-acroform-fields-to-json](./export-pdf-acroform-fields-to-json.cs) | Export pdf acroform fields to json |  | Export pdf acroform fields to json |
| [export-pdf-annotations-to-xfdf](./export-pdf-annotations-to-xfdf.cs) | Export PDF Annotations to XFDF using FileStream | `Document`, `ExportAnnotationsToXfdf` | Demonstrates how to load a PDF with Aspose.Pdf, export all its annotations to an XFDF file via a ... |
| [export-pdf-annotations-to-xfdf__v2](./export-pdf-annotations-to-xfdf__v2.cs) | Export pdf annotations to xfdf__v2 |  | Export pdf annotations to xfdf__v2 |
| [export-pdf-form-data-to-fdf](./export-pdf-form-data-to-fdf.cs) | Export PDF Form Data to FDF Using Aspose.Pdf | `Document`, `Form`, `BindPdf` | Loads a PDF document, binds it to the Aspose.Pdf.Facades.Form facade, and exports the form fields... |
| [export-pdf-form-data-to-json-bytes](./export-pdf-form-data-to-json-bytes.cs) | Export pdf form data to json bytes |  | Export pdf form data to json bytes |
| [export-pdf-form-data-to-json](./export-pdf-form-data-to-json.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF document with Aspose.Pdf, export all form fields to a JSON string using t... |
| [export-pdf-form-data-to-json__v2](./export-pdf-form-data-to-json__v2.cs) | Export PDF Form Data to JSON File | `Document`, `Form`, `ExportToJson` | Demonstrates loading a PDF with Aspose.Pdf, extracting all form fields, and saving them as a UTF‑... |
| [export-pdf-form-data-to-json__v3](./export-pdf-form-data-to-json__v3.cs) | Export PDF Form Data to JSON Files | `Document`, `Form`, `ExportToJson` | Iterates through PDF files in a folder, loads each with Aspose.Pdf, and exports the document's fo... |
| [export-pdf-form-data-to-json__v4](./export-pdf-form-data-to-json__v4.cs) | Export PDF Form Data to JSON | `Document`, `Form`, `Form` | Shows how to load a PDF with Aspose.Pdf and export its interactive form fields to a JSON file usi... |
| [export-pdf-form-data-to-json__v5](./export-pdf-form-data-to-json__v5.cs) | Export PDF Form Data to JSON via Stream | `Document`, `Form`, `ExportToJson` | Shows how to load a PDF document and export its form fields directly to a JSON file using a FileS... |
| [export-pdf-form-data-to-xfdf](./export-pdf-form-data-to-xfdf.cs) | Export pdf form data to xfdf |  | Export pdf form data to xfdf |
| [export-pdf-form-data-to-xml-xfdf](./export-pdf-form-data-to-xml-xfdf.cs) | Export pdf form data to xml xfdf |  | Export pdf form data to xml xfdf |
| [export-pdf-form-data-to-xml](./export-pdf-form-data-to-xml.cs) | Export PDF Form Data to XML Using MemoryStream | `Document`, `Form`, `Fields` | Loads a PDF, verifies the presence of form fields, and writes the form data as XML directly into ... |
| [export-pdf-form-fields-to-json](./export-pdf-form-fields-to-json.cs) | Export pdf form fields to json |  | Export pdf form fields to json |
| [export-pdf-form-fields-to-xml](./export-pdf-form-fields-to-xml.cs) | Export PDF Form Fields to XML | `Document`, `SaveXml` | Loads a PDF document and saves its complete model, including form field definitions and values, t... |
| [export-pdf-form-fields-with-prefix-to-json](./export-pdf-form-fields-with-prefix-to-json.cs) | Export PDF Form Fields with Specific Prefix to JSON | `Document`, `Field`, `Form` | Loads a PDF, filters its form fields to only those whose names start with a given prefix, and wri... |
| ... | | | *and 67 more files* |

## Category Statistics
- Total examples: 97

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

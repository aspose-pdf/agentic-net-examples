---
name: extraction
description: C# examples for extraction using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - extraction

> **Extraction** in PDF using C# / .NET -- **356** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **extraction** category.
This folder contains standalone C# examples for extraction operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **extraction**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (354/356 files) ← category-specific
- `using Aspose.Pdf.Text;` (173/356 files)
- `using Aspose.Pdf.Forms;` (65/356 files)
- `using Aspose.Pdf.Annotations;` (56/356 files)
- `using Aspose.Pdf.LogicalStructure;` (18/356 files)
- `using Aspose.Pdf.Tagged;` (18/356 files)
- `using Aspose.Pdf.Vector;` (13/356 files)
- `using Aspose.Pdf.Devices;` (9/356 files)
- `using Aspose.Pdf.Drawing;` (5/356 files)
- `using Aspose.Pdf.Operators;` (4/356 files)
- `using System;` (356/356 files)
- `using System.IO;` (356/356 files)
- `using System.Text.Json;` (72/356 files)
- `using System.Collections.Generic;` (71/356 files)
- `using System.Text;` (49/356 files)
- `using System.Linq;` (36/356 files)
- `using System.Drawing.Imaging;` (14/356 files)
- `using System.Xml.Linq;` (13/356 files)
- `using System.Reflection;` (5/356 files)
- `using System.Drawing;` (2/356 files)
- `using System.Drawing.Text;` (1/356 files)
- `using System.IO.Compression;` (1/356 files)
- `using System.Text.RegularExpressions;` (1/356 files)
- `using System.Xml;` (1/356 files)

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
| [adjust-font-size-page-text](./adjust-font-size-page-text.cs) | Adjust Font Size of Text Fragments on a PDF Page and Extract... | `Document`, `TextFragmentAbsorber`, `TextFragment` | Loads a PDF, uses TextFragmentAbsorber to capture text fragments on page 9, reduces each fragment... |
| [combine-pdf-tables-into-csv](./combine-pdf-tables-into-csv.cs) | Combine Extracted PDF Tables into a Single CSV | `Document`, `TableAbsorber`, `Visit` | Shows how to use Aspose.Pdf's TableAbsorber to extract all tables from a PDF in document order an... |
| [convert-pdf-pages-to-high-resolution-png__v2](./convert-pdf-pages-to-high-resolution-png__v2.cs) | Convert PDF Pages to High-Resolution PNG Images | `Document`, `PngDevice`, `Resolution` | Demonstrates rendering each page of a PDF as a high‑resolution PNG bitmap using Aspose.Pdf's PngD... |
| [count-detected-tables-in-pdf](./count-detected-tables-in-pdf.cs) | Count Detected Tables in PDF and Save to Text File | `Document`, `Page`, `TableAbsorber` | Demonstrates how to use Aspose.Pdf's TableAbsorber to detect tables in a PDF, count them, and wri... |
| [count-extracted-images-from-pdf](./count-extracted-images-from-pdf.cs) | Count Extracted Images from PDF and Write to Text File | `Document`, `Page`, `XImage` | Loads a PDF with Aspose.Pdf, iterates through each page to count embedded images, and writes the ... |
| [count-fonts-in-pdf](./count-fonts-in-pdf.cs) | Count Fonts in PDF and Save to Text File | `Document`, `FontUtilities`, `GetAllFonts` | Shows how to load a PDF with Aspose.Pdf, retrieve all fonts used in the document, count them, and... |
| [count-graphic-elements-on-pdf-page](./count-graphic-elements-on-pdf-page.cs) | Count Graphic Elements on a PDF Page | `Document`, `Page`, `GraphicsAbsorber` | Shows how to use Aspose.Pdf's GraphicsAbsorber to count graphic objects on page 11 of a PDF and w... |
| [count-pdf-form-fields-and-submit-actions](./count-pdf-form-fields-and-submit-actions.cs) | Count PDF Form Fields and Submit Actions | `Document`, `Form`, `Field` | Loads a PDF, accesses its AcroForm, counts total form fields and how many are button fields (used... |
| [count-pdf-operator-occurrences](./count-pdf-operator-occurrences.cs) | Count PDF Operator Occurrences and Export Summary | `Document`, `Pages`, `Page` | Opens a PDF with Aspose.Pdf, iterates through each page's low‑level operators, counts the occurre... |
| [count-pdf-operators-per-page](./count-pdf-operators-per-page.cs) | Count Low-Level PDF Operators per Page and Export to CSV | `Document`, `Page`, `PageCollection` | Loads a PDF using Aspose.Pdf, counts the low‑level content operators on each page, and writes the... |
| [count-pdf-page-graphic-elements-csv](./count-pdf-page-graphic-elements-csv.cs) | Count PDF Page Graphic Elements and Export to CSV | `Document`, `Page`, `GraphicsAbsorber` | Demonstrates how to use Aspose.Pdf's GraphicsAbsorber to count graphic elements on the first thre... |
| [count-table-rows-in-pdf](./count-table-rows-in-pdf.cs) | Count Table Rows in PDF and Save to Text File | `Document`, `Page`, `TableAbsorber` | The example loads a PDF, extracts tables using Aspose.Pdf's TableAbsorber, counts the rows in eac... |
| [count-tables-in-pdf__v2](./count-tables-in-pdf__v2.cs) | Count Tables in PDF and Save to Text File | `Document`, `ITaggedContent`, `TableElement` | Loads a PDF, extracts all tagged table elements, counts them, and writes the total number to a te... |
| [count-tables-in-tagged-pdf](./count-tables-in-tagged-pdf.cs) | Count Tables in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | The example opens a PDF, checks for tagged content, finds all TableElement objects in the structu... |
| [count-tables-per-pdf-page](./count-tables-per-pdf-page.cs) | Count Tables per PDF Page | `Document`, `TableAbsorber`, `Visit` | Shows how to load a PDF with Aspose.Pdf, use TableAbsorber to detect tables on each page, and out... |
| [count-tables-per-pdf-page__v2](./count-tables-per-pdf-page__v2.cs) | Count Tables per PDF Page | `Document`, `TableAbsorber`, `Visit` | Demonstrates loading a PDF with Aspose.Pdf, iterating through each page, using TableAbsorber to d... |
| [count-textfragment-on-page-two](./count-textfragment-on-page-two.cs) | Count TextFragment Objects on PDF Page Two | `Document`, `Pages`, `Page` | Shows how to load a PDF with Aspose.Pdf, access the second page, count all TextFragment objects o... |
| [count-total-cells-in-pdf-tables](./count-total-cells-in-pdf-tables.cs) | Count Total Cells in PDF Tables | `Document`, `TableAbsorber`, `Visit` | Demonstrates extracting tables from a PDF with Aspose.Pdf's TableAbsorber, counting all cells acr... |
| [delete-images-extract-text-pdf](./delete-images-extract-text-pdf.cs) | Delete Specific Images from a PDF Page and Extract Text | `Document`, `Page`, `XImageCollection` | The example loads a PDF, removes images at indexes 2 and 4 from page 7, extracts all document tex... |
| [detect-tables-in-pdf](./detect-tables-in-pdf.cs) | Detect Tables in PDF Using TableAbsorber | `Document`, `TableAbsorber`, `Page` | Shows how to use Aspose.Pdf's TableAbsorber to detect tables on each page of a PDF and iterate th... |
| [export-empty-pdf-form-fields-to-text](./export-empty-pdf-form-fields-to-text.cs) | Export Empty PDF Form Fields to Text File | `Document`, `ctor(string)`, `Form` | The example opens a PDF, checks its form fields, and writes the names of fields that have no valu... |
| [export-first-table-to-csv](./export-first-table-to-csv.cs) | Export First Table from PDF to CSV | `Document`, `Page`, `TableAbsorber` | Shows how to use Aspose.Pdf's TableAbsorber to locate the first table in a PDF document and write... |
| [export-graphic-state-matrices](./export-graphic-state-matrices.cs) | Export Graphic State Matrices from PDF Pages | `Document`, `Page`, `PageCollection` | The example opens a PDF, iterates through each page's low‑level content operators, extracts the p... |
| [export-installed-font-names-to-csv](./export-installed-font-names-to-csv.cs) | Export Installed Font Names to CSV | `InstalledFontCollection` | Retrieves all installed system font families, builds a properly quoted CSV line, and writes the l... |
| [export-last-pdf-table-to-csv](./export-last-pdf-table-to-csv.cs) | Export Last Extracted PDF Table to CSV | `Document`, `TableAbsorber`, `Visit` | Shows how to use Aspose.Pdf's TableAbsorber to extract tables from a PDF, select the last table, ... |
| [export-pdf-form-field-to-json](./export-pdf-form-field-to-json.cs) | Export PDF Form Field Value to JSON | `Document`, `ctor(Stream)`, `Form` | Loads a PDF via FileStream, reads a specific form field value, and writes it as a formatted JSON ... |
| [export-pdf-form-field-to-json__v2](./export-pdf-form-field-to-json__v2.cs) | Export PDF Form Field Value to JSON via MemoryStream | `Document`, `TextBoxField`, `Field` | Loads a PDF, reads a specific form field value, and serializes it to JSON using a MemoryStream. |
| [export-pdf-form-fields-to-compact-json](./export-pdf-form-fields-to-compact-json.cs) | Export PDF Form Fields to Compact JSON | `Document`, `ExportFieldsToJsonOptions`, `Form` | Demonstrates how to export all form fields from a PDF document to a JSON file using Aspose.Pdf wi... |
| [export-pdf-form-fields-to-csv__v3](./export-pdf-form-fields-to-csv__v3.cs) | Export PDF Form Fields to CSV | `Document`, `Form`, `Field` | Shows how to read all form fields from a PDF using Aspose.Pdf and write each field's name and val... |
| [export-pdf-form-fields-to-fdf__v2](./export-pdf-form-fields-to-fdf__v2.cs) | Export PDF Form Fields to FDF | `Document`, `Save`, `Field` | Loads a PDF document, iterates through all form fields to display their names, types, and values,... |
| ... | | | *and 326 more files* |

## Category Statistics
- Total examples: 356

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for extraction patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-26 | Run: `20261126_200200_a5ffc2`
<!-- AUTOGENERATED:END -->

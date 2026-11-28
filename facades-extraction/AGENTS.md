---
name: facades-extraction
description: C# examples for facades-extraction using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extraction

> **Facades extraction** in PDF using C# / .NET -- **123** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extraction** category.
This folder contains standalone C# examples for facades-extraction operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extraction**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (106/123 files) ← category-specific
- `using Aspose.Pdf;` (31/123 files)
- `using Aspose.Pdf.Annotations;` (11/123 files)
- `using System;` (123/123 files)
- `using System.IO;` (123/123 files)
- `using System.Drawing.Imaging;` (18/123 files)
- `using System.Text;` (15/123 files)
- `using System.Collections.Generic;` (3/123 files)
- `using System.Linq;` (2/123 files)
- `using System.Drawing;` (1/123 files)
- `using System.Text.Json;` (1/123 files)

## Common Code Pattern

Most files in this category use `PdfExtractor` from `Aspose.Pdf.Facades`:

```csharp
PdfExtractor tool = new PdfExtractor();
tool.BindPdf("input.pdf");
// ... PdfExtractor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [count-pdf-annotations](./count-pdf-annotations.cs) | Count PDF Annotations and Write to Text File | `Document`, `Page`, `Annotations` | Loads a PDF, iterates through all pages to count the total number of annotations, and writes the ... |
| [detect-images-in-pdf-to-json](./detect-images-in-pdf-to-json.cs) | Detect Images in PDF and Output Result to JSON | `PdfExtractor`, `BindPdf`, `ExtractImage` | Loads a PDF, uses PdfExtractor to determine whether the document contains any images, and writes ... |
| [detect-pdf-images-and-write-presence](./detect-pdf-images-and-write-presence.cs) | Detect Images in a PDF and Write Presence Result | `PdfExtractor`, `BindPdf`, `ExtractImage` | Loads a PDF, uses Aspose.Pdf's PdfExtractor to check if any images are present, and writes the de... |
| [detect-pdf-images-and-write-result](./detect-pdf-images-and-write-result.cs) | Detect Images in a PDF Using PdfExtractor | `PdfExtractor`, `BindPdf`, `HasNextImage` | Loads a PDF file, checks whether it contains any images using Aspose.Pdf.Facades.PdfExtractor, an... |
| [detect-text-and-images-in-pdf-to-csv](./detect-text-and-images-in-pdf-to-csv.cs) | Detect Text and Images in PDF and Save Results to CSV | `PdfExtractor`, `BindPdf`, `ExtractText` | The example loads a PDF, uses Aspose.Pdf.Facades.PdfExtractor to determine whether the document c... |
| [export-pdf-form-data-to-fdf__v3](./export-pdf-form-data-to-fdf__v3.cs) | Export PDF Form Data to FDF using Aspose.Pdf Form Facade | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind an interactive PDF form with Aspose.Pdf.Facades.Form and export its data... |
| [export-pdf-form-data-to-fdf__v4](./export-pdf-form-data-to-fdf__v4.cs) | Export PDF Form Data to FDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-fdf__v5](./export-pdf-form-data-to-fdf__v5.cs) | Export PDF Form Data to FDF using Aspose.Pdf.Facades | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-fdf__v6](./export-pdf-form-data-to-fdf__v6.cs) | Export PDF Form Data to FDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-fdf__v7](./export-pdf-form-data-to-fdf__v7.cs) | Export PDF Form Data to FDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-fdf__v8](./export-pdf-form-data-to-fdf__v8.cs) | Export PDF Form Data to FDF | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-xfdf](./export-pdf-form-data-to-xfdf.cs) | Export PDF Form Data to XFDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields and export its data to an XFDF file using t... |
| [export-pdf-form-data-to-xfdf__v2](./export-pdf-form-data-to-xfdf__v2.cs) | Export PDF Form Data to XFDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields and export the form data to an XFDF file us... |
| [export-pdf-form-data-to-xfdf__v3](./export-pdf-form-data-to-xfdf__v3.cs) | Export PDF Form Data to XFDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-xfdf__v4](./export-pdf-form-data-to-xfdf__v4.cs) | Export PDF Form Data to XFDF using Aspose.Pdf Facade | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields with the Aspose.Pdf.Facades.Form class and ... |
| [export-pdf-form-data-to-xfdf__v5](./export-pdf-form-data-to-xfdf__v5.cs) | Export PDF Form Data to XFDF | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields using Aspose.Pdf.Facades.Form and export th... |
| [export-pdf-form-data-to-xfdf__v6](./export-pdf-form-data-to-xfdf__v6.cs) | Export PDF Form Data to XFDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields and export the form data to an XFDF file us... |
| [export-pdf-form-data-to-xfdf__v7](./export-pdf-form-data-to-xfdf__v7.cs) | Export PDF Form Data to XFDF using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing form fields and export the form data to an XFDF file us... |
| [export-pdf-form-data-to-xml__v10](./export-pdf-form-data-to-xml__v10.cs) | Export PDF Form Data to XML using Aspose.Pdf | `Form`, `BindPdf`, `ExportXml` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-xml__v11](./export-pdf-form-data-to-xml__v11.cs) | Export PDF Form Data to XML using Aspose.Pdf | `Form`, `BindPdf`, `ExportXml` | Shows how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the form d... |
| [export-pdf-form-data-to-xml__v12](./export-pdf-form-data-to-xml__v12.cs) | Export PDF Form Data to XML using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXml` | Demonstrates how to bind a PDF containing an interactive form and export its fields and values to... |
| [export-pdf-form-data-to-xml__v13](./export-pdf-form-data-to-xml__v13.cs) | Export PDF Form Data to XML using Aspose.Pdf | `Form`, `BindPdf`, `ExportXml` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-xml__v8](./export-pdf-form-data-to-xml__v8.cs) | Export PDF Form Data to XML using Aspose.Pdf.Facades | `Form`, `BindPdf`, `ExportXml` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-data-to-xml__v9](./export-pdf-form-data-to-xml__v9.cs) | Export PDF Form Data to XML using Aspose.Pdf.Facades.Form | `Form`, `BindPdf`, `ExportXml` | Demonstrates how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the... |
| [export-pdf-form-fields-to-fdf__v7](./export-pdf-form-fields-to-fdf__v7.cs) | Export PDF Form Fields to FDF using Aspose.Pdf.Facades | `Form`, `BindPdf`, `ExportFdf` | Demonstrates how to bind a PDF containing an interactive form and export its fields to an FDF fil... |
| [export-pdf-form-fields-to-xfdf__v10](./export-pdf-form-fields-to-xfdf__v10.cs) | Export PDF Form Fields to XFDF | `Form`, `BindPdf`, `ExportXfdf` | Demonstrates how to bind a PDF containing an interactive form using Aspose.Pdf.Facades.Form and e... |
| [export-pdf-form-fields-to-xml__v11](./export-pdf-form-fields-to-xml__v11.cs) | Export PDF Form Fields to XML | `Form`, `BindPdf`, `ExportXml` | Shows how to bind a PDF that contains form fields using Aspose.Pdf.Facades.Form and export the fo... |
| [export-pdf-form-to-fdf__v2](./export-pdf-form-to-fdf__v2.cs) | Export PDF Form Data to FDF using Aspose.Pdf Facades | `Form`, `BindPdf`, `ExportFdf` | Shows how to bind a PDF containing form fields with Aspose.Pdf.Facades.Form and export the form d... |
| [extract-all-annotation-types](./extract-all-annotation-types.cs) | Extract All Annotation Types from PDF | `Document`, `Page`, `Annotation` | Loads a PDF document, iterates through each page, and writes the type of every annotation along w... |
| [extract-annotation-text-from-pdf-pages](./extract-annotation-text-from-pdf-pages.cs) | Extract Annotation Text from Specific PDF Pages | `Document`, `Page`, `Annotation` | Shows how to load a PDF with Aspose.Pdf, iterate over pages 10‑15, collect the Contents of each a... |
| ... | | | *and 93 more files* |

## Category Statistics
- Total examples: 123

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-extraction patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-27 | Run: `20261127_160714_81c788`
<!-- AUTOGENERATED:END -->

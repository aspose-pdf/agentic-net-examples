---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **593** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (592/593 files) ← category-specific
- `using Aspose.Pdf.Devices;` (103/593 files)
- `using Aspose.Pdf.Text;` (89/593 files)
- `using Aspose.Pdf.Drawing;` (8/593 files)
- `using Aspose.Pdf.Annotations;` (4/593 files)
- `using Aspose.Pdf.Facades;` (1/593 files)
- `using Aspose.Pdf.Forms;` (1/593 files)
- `using Aspose.Pdf.Tagged;` (1/593 files)
- `using System;` (593/593 files)
- `using System.IO;` (592/593 files)
- `using System.Text;` (13/593 files)
- `using System.Xml;` (12/593 files)
- `using System.Xml.Linq;` (12/593 files)
- `using System.Linq;` (8/593 files)
- `using System.IO.Compression;` (5/593 files)
- `using System.Collections.Generic;` (3/593 files)
- `using System.Drawing;` (3/593 files)
- `using System.Drawing.Imaging;` (3/593 files)
- `using System.Xml.Xsl;` (3/593 files)
- `using System.Diagnostics;` (2/593 files)
- `using System.Text.RegularExpressions;` (2/593 files)
- `using System.Collections;` (1/593 files)
- `using System.Security.Cryptography;` (1/593 files)
- `using System.Text.Json;` (1/593 files)

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
| [add-cell-to-table-from-xml](./add-cell-to-table-from-xml.cs) | Add a Cell to a Table in a PDF Loaded from XML | `Document`, `XmlLoadOptions`, `Page` | The example loads a PDF structure from an XML file, locates the first table, adds a new cell with... |
| [add-circle-to-graph-from-xml](./add-circle-to-graph-from-xml.cs) | Add Circle Shape to a Graph in a PDF Loaded from XML | `Document`, `XmlLoadOptions`, `Graph` | Demonstrates loading a PDF from its XML representation, locating the first Graph object, adding a... |
| [add-external-text-file-attachment-to-pdfa3b](./add-external-text-file-attachment-to-pdfa3b.cs) | Add External Text File Attachment to PDF/A‑3b Document | `Document`, `FileSpecification`, `Convert` | Demonstrates how to embed an external text file into a PDF, convert the document to PDF/A‑3b comp... |
| [add-external-xml-attachment-to-pdfa](./add-external-xml-attachment-to-pdfa.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `PDF_A_1B` | Shows how to convert a PDF to PDF/A‑1b format and embed an external XML file as an attachment usi... |
| [add-graph-element-embed-xml-into-pdf](./add-graph-element-embed-xml-into-pdf.cs) | Add Graph Element to XML and Embed into PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to programmatically insert a <Graph> element with <Circle> and <GraphInfo> into an XML ... |
| [add-html-cdata-fragment-to-xml-for-pdf-binding](./add-html-cdata-fragment-to-xml-for-pdf-binding.cs) | Add HTML CDATA Fragment to XML and Bind to PDF | `Document`, `BindXml`, `Save` | The example creates or loads an XML document, programmatically adds an HtmlFragment element conta... |
| [add-html-fragment-to-pdf](./add-html-fragment-to-pdf.cs) | Insert HTML Fragment into PDF via XML and Direct API | `Document`, `XmlSaveOptions`, `HtmlFragment` | Demonstrates creating an HtmlFragment element with CDATA‑wrapped HTML, inserting it into the PDF'... |
| [add-image-background-and-overlay-icon](./add-image-background-and-overlay-icon.cs) | Add Image Background and Overlay Icon to PDF | `Document`, `Page`, `ImageStamp` | Creates a new PDF, adds a page, places a TIFF image as a fixed-width background, then overlays a ... |
| [add-image-path-to-xml-for-pdf-binding](./add-image-path-to-xml-for-pdf-binding.cs) | Add Image Path to XML for PDF Data Binding | `Document`, `BindXml`, `Save` | Demonstrates how to insert an image file path into an XML document at runtime and bind the modifi... |
| [add-image-reference-via-xml-binding](./add-image-reference-via-xml-binding.cs) | Add Image Reference via XML Binding to PDF | `Document`, `XmlSaveOptions`, `Save` | Shows how to export a tagged PDF to XML, insert an <Image File="..."/> element, bind the modified... |
| [add-image-to-pdf-from-xml](./add-image-to-pdf-from-xml.cs) | Add Image to PDF Loaded from XML | `Document`, `XmlLoadOptions`, `Page` | Demonstrates loading a PDF structure from an XML file, inserting an image onto the first page, an... |
| [add-image-with-fixed-height-to-pdf](./add-image-with-fixed-height-to-pdf.cs) | Add Image with Fixed Height to PDF | `Document`, `Page`, `Image` | Demonstrates creating a PDF, adding a page, inserting an image from a file, fixing its height to ... |
| [add-logo-to-each-pdf-page](./add-logo-to-each-pdf-page.cs) | Add Logo Image to Each PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates how to create a new PDF, add multiple pages, and place the same image (logo.gif) on ... |
| [add-row-to-table-from-xml-pdf](./add-row-to-table-from-xml-pdf.cs) | Add Row with Cells to a Table Loaded from XML and Save as PD... | `Document`, `XmlLoadOptions`, `Page` | Demonstrates loading a PDF structure from an XML file, locating a table on the first page, adding... |
| [add-table-with-alignment-to-pdf](./add-table-with-alignment-to-pdf.cs) | Add Table with Alignment to PDF via XML Binding | `Document`, `BindXml`, `Save` | Shows how to programmatically insert a Table element with a custom Alignment attribute into an XM... |
| [adjust-pageinfo-margins-and-generate-pdf](./adjust-pageinfo-margins-and-generate-pdf.cs) | Adjust PageInfo Margins in XML and Generate PDF | `XmlLoadOptions`, `Document`, `Save` | The example loads an XML file, updates the PageInfo margin attributes, then uses Aspose.Pdf to lo... |
| [attach-file-convert-pdf-to-pdfa-3a](./attach-file-convert-pdf-to-pdfa-3a.cs) | Attach File and Convert PDF to PDF/A‑3a | `Document`, `FileSpecification`, `PdfFormatConversionOptions` | Shows how to embed an external file into a PDF, convert the document to PDF/A‑3a compliance, and ... |
| [attach-file-convert-pdf-to-pdfa-3a__v2](./attach-file-convert-pdf-to-pdfa-3a__v2.cs) | Attach a File and Convert PDF to PDF/A‑3a with Auto‑Tagging | `Document`, `FileSpecification`, `AutoTaggingSettings` | Demonstrates how to embed an external file into a PDF, enable global auto‑tagging, convert the do... |
| [attach-file-convert-pdf-to-pdfa-3a__v3](./attach-file-convert-pdf-to-pdfa-3a__v3.cs) | Attach File and Convert PDF to PDF/A‑3a with Font Substituti... | `Document`, `FileSpecification`, `FontRepository` | Demonstrates embedding an external file into a PDF, configuring simple font substitution, convert... |
| [batch-convert-pdf-to-pdfe1-with-validation](./batch-convert-pdf-to-pdfe1-with-validation.cs) | Batch Convert PDFs to PDF/E-1 with Validation Reports | `Document`, `Convert`, `Save` | Demonstrates how to convert all PDF files in a folder to PDF/E‑1, generate XML validation reports... |
| [batch-convert-pdfs-to-jpeg-custom-naming](./batch-convert-pdfs-to-jpeg-custom-naming.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Shows how to iterate through PDF files in a folder, render each page to a JPEG image using Aspose... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `Add`, `TiffDevice` | Demonstrates how to combine multiple PDF files into a single document and convert it to a multi‑p... |
| [batch-convert-pdfs-to-pdf-e1-with-validation](./batch-convert-pdfs-to-pdf-e1-with-validation.cs) | Batch Convert PDFs to PDF/E-1 with Validation Reports | `Document`, `Convert`, `PdfFormat` | Demonstrates how to process a folder of PDF files, convert each to PDF/E‑1 format, generate an XM... |
| [batch-convert-pdfs-to-pdfx-1a-2003](./batch-convert-pdfs-to-pdfx-1a-2003.cs) | Batch Convert PDFs to PDF/X‑1a:2003 without ICC Profile | `Document`, `Convert`, `Save` | Shows how to iterate over PDF files in a folder, convert each to PDF/X‑1a:2003 using Aspose.Pdf (... |
| [batch-convert-pdfs-to-pdfx-1a](./batch-convert-pdfs-to-pdfx-1a.cs) | Batch Convert PDFs to PDF/X-1a:2001 with ICC Profile | `Document`, `PdfFormatConversionOptions`, `SaveFormat` | Demonstrates iterating over a folder of PDF files, applying an ICC profile, and converting each d... |
| [batch-convert-pdfs-to-png](./batch-convert-pdfs-to-png.cs) | Batch Convert PDFs to PNG Images Preserving Folder Structure | `Document`, `PngDevice`, `Resolution` | Demonstrates how to recursively locate PDF files, keep their relative directory hierarchy, and co... |
| [batch-convert-pdfs-to-pptx-slides-as-images](./batch-convert-pdfs-to-pptx-slides-as-images.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Shows how to convert all PDF files in a directory to PPTX presentations using Aspose.Pdf, rasteri... |
| [bind-custom-pageinfo-xml-to-pdf](./bind-custom-pageinfo-xml-to-pdf.cs) | Bind Custom PageInfo XML to a PDF and Save | `Document`, `BindXml`, `Save` | Demonstrates how to create a custom PageInfo XML definition, bind it to an existing PDF using Asp... |
| [bind-image-to-xml-pdf-template](./bind-image-to-xml-pdf-template.cs) | Bind Image to XML PDF Template and Save | `Document`, `XmlLoadOptions`, `Form` | Loads an XML template as a PDF document, finds an image form field by its Id, sets the field's so... |
| [bind-xml-data-to-pdf-after-placeholder-replacement](./bind-xml-data-to-pdf-after-placeholder-replacement.cs) | Bind XML Data to PDF After Placeholder Replacement | `Document`, `BindXml`, `Save` | The example reads an XML template, replaces placeholder tokens with runtime values, binds the res... |
| ... | | | *and 563 more files* |

## Category Statistics
- Total examples: 593

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.CgmLoadOptions`
- `Aspose.Pdf.ConvertErrorAction`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.EpubLoadOptions`
- `Aspose.Pdf.ExcelSaveOptions`
- `Aspose.Pdf.ExcelSaveOptions.ExcelFormat`
- `Aspose.Pdf.FileSpecification`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.LoadOptions`
- `Aspose.Pdf.MarginInfo`
- `Aspose.Pdf.MdLoadOptions`
- `Aspose.Pdf.MhtLoadOptions`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.Page.Paragraphs`

### Rules
- Load a PDF file {input_pdf} into an Aspose.Pdf.Document instance and call Document.Save({output_excel}, ExcelSaveOptions) to export to Excel.
- Set ExcelSaveOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX to generate an .xlsx file instead of the default .xls.
- Set ExcelSaveOptions.InsertBlankColumnAtFirst = {bool} to control whether a blank column is added at the beginning of each worksheet.
- Set ExcelSaveOptions.MinimizeTheNumberOfWorksheets = {bool} to combine all PDF pages into a single worksheet when true.
- Create a {load_options} of type Aspose.Pdf.SvgLoadOptions and use it to instantiate a {doc} from an {input_svg} file path.

### Warnings
- The example assumes the presence of an input PDF file at the specified path.
- Output file paths are hard‑coded; in production code they should be configurable.
- The example assumes the CGM file exists at the specified path and that the Aspose.PDF license (if required) is already configured.
- The example assumes the presence of a valid Aspose.PDF license; without it, the output may contain a watermark.
- Placeholder {string_literal} is used for both input and output file paths; adjust as needed for your environment.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for conversion patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-25 | Run: `20261125_202056_733e94`
<!-- AUTOGENERATED:END -->

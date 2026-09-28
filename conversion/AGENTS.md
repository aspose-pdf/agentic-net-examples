---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **91** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (90/91 files) ← category-specific
- `using Aspose.Pdf.Devices;` (14/91 files)
- `using Aspose.Pdf.Text;` (14/91 files)
- `using Aspose.Pdf.Annotations;` (2/91 files)
- `using Aspose.Pdf.Facades;` (1/91 files)
- `using Aspose.Pdf.LogicalStructure;` (1/91 files)
- `using Aspose.Pdf.Tagged;` (1/91 files)
- `using System;` (91/91 files)
- `using System.IO;` (91/91 files)
- `using System.IO.Compression;` (4/91 files)
- `using System.Collections.Generic;` (2/91 files)
- `using System.Text;` (2/91 files)
- `using System.Xml.Linq;` (2/91 files)
- `using System.Diagnostics;` (1/91 files)
- `using System.Drawing;` (1/91 files)
- `using System.Drawing.Imaging;` (1/91 files)
- `using System.Linq;` (1/91 files)
- `using System.Reflection;` (1/91 files)
- `using System.Text.Json;` (1/91 files)
- `using System.Text.RegularExpressions;` (1/91 files)

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
| [add-external-text-file-attachment-to-pdfa3b](./add-external-text-file-attachment-to-pdfa3b.cs) | Embed Text File and Convert PDF to PDF/A‑3b | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed an external text file into a PDF, set the PDF/A‑3 relationship, convert... |
| [add-external-xml-attachment-to-pdfa](./add-external-xml-attachment-to-pdfa.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `PdfFormat`, `ConvertErrorAction` | Demonstrates converting a regular PDF to PDF/A‑1b format and embedding an external XML file as an... |
| [batch-convert-pdfs-to-jpeg-custom-naming](./batch-convert-pdfs-to-jpeg-custom-naming.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through a folder of PDF files, convert each page to a JPEG image usin... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `TiffDevice`, `Process` | Demonstrates how to convert all PDF files in a folder to multi‑page TIFF archives using Aspose.Pd... |
| [batch-convert-pdfs-to-png](./batch-convert-pdfs-to-png.cs) | Batch Convert PDFs to PNG Preserving Folder Structure | `Document`, `Page`, `PngDevice` | Demonstrates how to recursively locate PDF files, preserve their original directory hierarchy, an... |
| [batch-convert-pdfs-to-pptx-slides-as-images](./batch-convert-pdfs-to-pptx-slides-as-images.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Shows how to iterate over PDF files in a directory and convert each to a PPTX presentation, raste... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `Document`, `EpubLoadOptions`, `PageInfo` | Demonstrates loading an EPUB file using EpubLoadOptions and converting it to a PDF while applying... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX to PDF with Aspose.Pdf | `Document`, `TeXLoadOptions`, `Save` | Shows how to load a .tex file using TeXLoadOptions and directly save it as a PDF, preserving equa... |
| [convert-markdown-to-pdf-preserving-code-blocks](./convert-markdown-to-pdf-preserving-code-blocks.cs) | Convert Markdown to PDF Preserving Code Blocks | `MdLoadOptions`, `Document`, `ctor` | Demonstrates loading a Markdown file with Aspose.Pdf's MdLoadOptions and saving it as a PDF while... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD File to PDF | `Document`, `OfdLoadOptions`, `Save` | Demonstrates loading an OFD document with Aspose.Pdf using default load options and saving it as ... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL with HP‑GL/2 Vectors to PDF | `Document`, `PclLoadOptions`, `Save` | Loads a PCL file (including HP‑GL/2 vector data) using Aspose.Pdf and saves it as a PDF document. |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG with Default DPI | `Document`, `Page`, `JpegDevice` | Loads a PDF document, validates a specific page, and uses Aspose.Pdf's JpegDevice to render that ... |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `HtmlSaveOptions`, `RasterImagesSavingModes` | Shows how to load a PDF with Aspose.Pdf, configure HtmlSaveOptions to split the document into pag... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Pages` | Shows how to load a PDF document and convert each page to a BMP image using Aspose.Pdf's BmpDevic... |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC with Aspose.Pdf | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF file and save it as a DOC document using Aspose.Pdf's default text extrac... |
| [convert-pdf-to-doc-image-extraction](./convert-pdf-to-doc-image-extraction.cs) | Convert PDF to DOCX with Images Only | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf while configuring save opt... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to Plain-Text DOC using DocSaveOptions | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF with Aspose.Pdf and save it as a plain‑text .doc file by configuring DocS... |
| [convert-pdf-to-docx-add-table-of-figures](./convert-pdf-to-docx-add-table-of-figures.cs) | Convert PDF to DOCX and Add Table of Figures | `Document`, `Page`, `XImage` | Loads a PDF, extracts all images, creates a Table of Figures on a new first page, and converts th... |
| [convert-pdf-to-docx-and-pdfa](./convert-pdf-to-docx-and-pdfa.cs) | Convert PDF to DOCX and then to PDF/A | `Document`, `DocSaveOptions`, `PdfFormat` | Demonstrates loading a PDF, saving it as a DOCX file using DocSaveOptions, and then converting th... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Zip the Result | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF with Aspose.Pdf, save it as a DOCX file, and then compress the DOCX into ... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file and save it as a DOCX document using Aspose.Pdf with automati... |
| [convert-pdf-to-docx-enhanced-flow](./convert-pdf-to-docx-enhanced-flow.cs) | Convert PDF to DOCX with Enhanced Table and Graphic Recognit... | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to convert a PDF file to a DOCX document using Aspose.Pdf with enhanced recognit... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Embedded Images | `Document`, `DocSaveOptions`, `Save` | Loads a PDF, saves it as a DOCX file using Aspose.Pdf, then iterates through each page to extract... |
| [convert-pdf-to-docx-layout](./convert-pdf-to-docx-layout.cs) | Convert PDF to DOCX with Standard Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF and save it as a DOCX file using Aspose.Pdf with the standard content rec... |
| [convert-pdf-to-docx-with-embedded-fonts](./convert-pdf-to-docx-with-embedded-fonts.cs) | Convert PDF to DOCX with Embedded Custom Fonts | `Document`, `FontRepository`, `SimpleFontSubstitution` | Demonstrates converting a PDF to DOCX using Aspose.Pdf while registering a custom TrueType/OpenTy... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX Preserving Footnotes | `Document`, `DocSaveOptions`, `RecognitionMode` | Loads a PDF, sets DocSaveOptions to Flow recognition mode, and saves it as a DOCX while keeping f... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX using Aspose.Pdf | `Document`, `DocSaveOptions`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, configuring DocSaveOptions, and saving it as a DOCX f... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `Info`, `Save` | Shows how to convert a PDF file to DOCX using Aspose.Pdf while setting custom metadata properties... |
| [convert-pdf-to-docx-with-stats](./convert-pdf-to-docx-with-stats.cs) | Convert PDF to DOCX and Generate Conversion Report | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf and creating a JSON report with conve... |
| [convert-pdf-to-emf-images](./convert-pdf-to-emf-images.cs) | Convert PDF to EMF Images Preserving Vector Data | `Document`, `EmfDevice`, `Resolution` | Loads a PDF document and converts each page to an EMF file using Aspose.Pdf's EmfDevice, keeping ... |
| ... | | | *and 61 more files* |

## Category Statistics
- Total examples: 91

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
Updated: 2026-09-28 | Run: `20260928_001645_5c08e5`
<!-- AUTOGENERATED:END -->

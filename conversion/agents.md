---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (101/102 files) ← category-specific
- `using Aspose.Pdf.Devices;` (17/102 files)
- `using Aspose.Pdf.Text;` (13/102 files)
- `using Aspose.Pdf.Drawing;` (3/102 files)
- `using Aspose.Pdf.Annotations;` (2/102 files)
- `using Aspose.Pdf.Printing;` (1/102 files)
- `using System;` (101/102 files)
- `using System.IO;` (101/102 files)
- `using System.Text;` (6/102 files)
- `using System.IO.Compression;` (3/102 files)
- `using System.Collections.Generic;` (2/102 files)
- `using System.Drawing;` (2/102 files)
- `using System.Drawing.Imaging;` (2/102 files)
- `using System.Runtime.InteropServices;` (2/102 files)
- `using System.Diagnostics;` (1/102 files)
- `using System.Linq;` (1/102 files)
- `using System.Reflection;` (1/102 files)
- `using System.Text.Json;` (1/102 files)
- `using System.Text.RegularExpressions;` (1/102 files)
- `using System.Xml;` (1/102 files)

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
| [add-attachment-convert-to-pdfa-3b](./add-attachment-convert-to-pdfa-3b.cs) | Add Attachment and Convert PDF to PDF/A‑3b | `Document`, `FileSpecification`, `PdfFormat` | Demonstrates how to embed an external text file into a PDF, then convert the document to PDF/A‑3b... |
| [add-logo-to-pdf-and-convert-to-pptx](./add-logo-to-pdf-and-convert-to-pptx.cs) | Add Logo to PDF and Convert to PPTX | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF with Aspose.Pdf, stamping a logo image onto each page, and then saving... |
| [add-xml-attachment-to-pdfa](./add-xml-attachment-to-pdfa.cs) | Add External XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `Save` | Demonstrates converting a PDF to PDF/A-1b and embedding an external XML file as a file‑attachment... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF Archives | `Document`, `TiffDevice`, `Process` | Shows how to iterate through a directory of PDF files and convert each one into a multi‑page TIFF... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Demonstrates how to convert multiple PDF files in a folder to PPTX presentations using Aspose.Pdf... |
| [batch-pdf-to-jpeg-converter](./batch-pdf-to-jpeg-converter.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to convert each page of multiple PDF files in a folder to separate JPEG images u... |
| [batch-pdf-to-png-converter](./batch-pdf-to-png-converter.cs) | Batch Convert PDFs to PNG Images Preserving Folder Structure | `Document`, `Resolution`, `PngDevice` | Demonstrates how to recursively locate PDF files, convert each page to a PNG image using Aspose.P... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `EpubLoadOptions`, `Document`, `Save` | Shows how to load an EPUB file using EpubLoadOptions with a custom page size and convert it to a ... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX File to PDF | `TeXLoadOptions`, `Document`, `Save` | Shows how to load a LaTeX (.tex) file with TeXLoadOptions and save it as a PDF, keeping equations... |
| [convert-markdown-to-pdf-preserve-code-blocks](./convert-markdown-to-pdf-preserve-code-blocks.cs) | Convert Markdown to PDF Preserving Code Blocks | `MdLoadOptions`, `Document`, `Save` | Demonstrates loading a Markdown file with MdLoadOptions and converting it to a PDF while keeping ... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD File to PDF | `Document`, `OfdLoadOptions`, `Save` | Demonstrates loading an OFD document with OfdLoadOptions and saving it as a PDF using Aspose.Pdf'... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Vector Support | `PclLoadOptions`, `Document`, `Save` | Demonstrates loading a PCL file (including HP‑GL/2 vectors) using PclLoadOptions and saving it as... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `JpegDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf, select a specific page, and render it as a JPEG image us... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images Using BmpDevice | `Document`, `BmpDevice`, `Process` | Shows how to load a PDF document with Aspose.Pdf and convert each page to a BMP image using BmpDe... |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC with Default Settings | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file using Aspose.Pdf and save it as a DOC document using the defa... |
| [convert-pdf-to-doc-image-extraction](./convert-pdf-to-doc-image-extraction.cs) | Convert PDF to DOC with Image Extraction | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to convert a PDF file to a DOC document using Aspose.Pdf while extracting only i... |
| [convert-pdf-to-doc-textbox-mode](./convert-pdf-to-doc-textbox-mode.cs) | Convert PDF to DOC with Textbox Mode | `Document`, `DocSaveOptions`, `Save` | Shows how to convert a PDF file to a DOC file using Aspose.Pdf while setting DocSaveOptions.Mode ... |
| [convert-pdf-to-docx-add-table-of-figures](./convert-pdf-to-docx-add-table-of-figures.cs) | Convert PDF to DOCX and Add Table of Figures | `Document`, `Save`, `Page` | Demonstrates converting a PDF to DOCX using Aspose.Pdf and generating a Table of Figures by extra... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Compress to ZIP | `Document`, `DocSaveOptions`, `TextFragment` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf and then compressing the resulting DO... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `Save`, `DocSaveOptions` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf with DocSaveOptions.Mode set to Flow ... |
| [convert-pdf-to-docx-embed-custom-fonts](./convert-pdf-to-docx-embed-custom-fonts.cs) | Convert PDF to DOCX with Embedded Custom Fonts | `Document`, `DocSaveOptions`, `FontRepository` | Demonstrates converting a PDF to DOCX using Aspose.Pdf while registering and embedding a custom T... |
| [convert-pdf-to-docx-enhanced](./convert-pdf-to-docx-enhanced.cs) | Convert PDF to DOCX with Enhanced Recognition | `Document`, `Save`, `DocSaveOptions` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf with the default enhanced ... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Images | `Document`, `Save`, `DocSaveOptions` | The example loads a PDF, converts it to a DOCX file using Aspose.Pdf, and then extracts all embed... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions` | Shows how to convert a PDF file to a DOCX document using Aspose.Pdf's standard textbox recognitio... |
| [convert-pdf-to-docx-then-pdfa](./convert-pdf-to-docx-then-pdfa.cs) | Convert PDF to DOCX and then to PDF/A | `Document`, `DocSaveOptions`, `PdfFormat` | Demonstrates how to use Aspose.Pdf to convert a PDF file to DOCX format and subsequently create a... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX with Footnote Preservation | `Document`, `Save`, `DocSaveOptions` | Shows how to convert a PDF to DOCX using Aspose.Pdf with EnhancedFlow recognition mode to retain ... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX with Hyphenation Settings | `Document`, `DocSaveOptions`, `Save` | Demonstrates loading a PDF using Aspose.Pdf, configuring DocSaveOptions, and saving it as a DOCX ... |
| [convert-pdf-to-docx-with-json-report](./convert-pdf-to-docx-with-json-report.cs) | Convert PDF to DOCX with JSON Report | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to use Aspose.Pdf to convert a PDF file to DOCX format, collect conversion stati... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `Save`, `DocSaveOptions` | Demonstrates loading a PDF, setting author and title metadata, and saving it as a DOCX file using... |
| [convert-pdf-to-emf-images](./convert-pdf-to-emf-images.cs) | Convert PDF Pages to EMF Images | `Document`, `Resolution`, `EmfDevice` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through its pages, and save each page as ... |
| ... | | | *and 72 more files* |

## Category Statistics
- Total examples: 102

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for conversion patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

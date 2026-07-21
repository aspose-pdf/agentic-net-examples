---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **102** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (102/102 files) ← category-specific
- `using Aspose.Pdf.Devices;` (18/102 files)
- `using Aspose.Pdf.Text;` (8/102 files)
- `using Aspose.Pdf.Annotations;` (3/102 files)
- `using Aspose.Pdf.Drawing;` (2/102 files)
- `using Aspose.Pdf.Tagged;` (2/102 files)
- `using Aspose.Pdf.Facades;` (1/102 files)
- `using Aspose.Pdf.LogicalStructure;` (1/102 files)
- `using System;` (102/102 files)
- `using System.IO;` (102/102 files)
- `using System.IO.Compression;` (4/102 files)
- `using System.Collections.Generic;` (2/102 files)
- `using System.Drawing;` (2/102 files)
- `using System.Text;` (2/102 files)
- `using System.Text.RegularExpressions;` (2/102 files)
- `using System.Diagnostics;` (1/102 files)
- `using System.Drawing.Imaging;` (1/102 files)
- `using System.Text.Json;` (1/102 files)
- `using System.Xml.Linq;` (1/102 files)

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
| [add-attachment-to-pdfa3b-document](./add-attachment-to-pdfa3b-document.cs) | Add File Attachment and Convert to PDF/A‑3b | `Document`, `FileSpecification`, `Convert` | Demonstrates how to embed an external text file into a PDF/A‑3b document using Aspose.Pdf, then c... |
| [add-logo-to-pdf-and-convert-to-pptx](./add-logo-to-pdf-and-convert-to-pptx.cs) | Add Logo to PDF and Convert to PPTX | `Document`, `ImageStamp`, `AddStamp` | Loads a PDF, stamps a semi‑transparent company logo on each page, and then converts the modified ... |
| [add-xml-attachment-to-pdfa1b-document](./add-xml-attachment-to-pdfa1b-document.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `PdfFormat` | Shows how to convert a PDF to PDF/A‑1b and embed an external XML file as a file‑attachment annota... |
| [batch-convert-pdfs-to-jpeg-images](./batch-convert-pdfs-to-jpeg-images.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to enumerate PDF files in a folder, convert each page to a JPEG image using Aspo... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `TiffDevice`, `Process` | Shows how to iterate through a folder of PDF files and convert each document into a multi‑page TI... |
| [batch-convert-pdfs-to-png-preserve-hierarchy](./batch-convert-pdfs-to-png-preserve-hierarchy.cs) | Batch Convert PDFs to PNG Preserving Folder Hierarchy | `Document`, `PngDevice`, `Resolution` | Shows how to recursively locate PDF files, keep their relative directory structure, and convert e... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Demonstrates how to convert multiple PDF files in a folder to PPTX format, rendering each slide a... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `Document`, `EpubLoadOptions`, `Save` | Demonstrates loading an EPUB file with EpubLoadOptions to set a custom page size and saving it as... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX (.tex) to PDF with Aspose.Pdf | `TeXLoadOptions`, `RasterizeFormulas`, `ShowTerminalOutput` | Shows how to load a .tex file using TeXLoadOptions and convert it to a PDF document using Aspose.... |
| [convert-markdown-to-pdf](./convert-markdown-to-pdf.cs) | Convert Markdown to PDF with Code Block Preservation | `MdLoadOptions`, `Document`, `Save` | Demonstrates loading a Markdown file using MdLoadOptions and converting it to a PDF document whil... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF | `Document`, `OfdLoadOptions`, `Save` | Shows how to load an OFD document using OfdLoadOptions and save it as a PDF with default settings... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Vectors | `PclLoadOptions`, `Document`, `Save` | Demonstrates loading a PCL file (including HP‑GL/2 vector graphics) using Aspose.Pdf and saving i... |
| [convert-pdf-page-region-to-png](./convert-pdf-page-region-to-png.cs) | Convert PDF Page Region to PNG Image | `Document`, `Page`, `Rectangle` | Shows how to extract a specific rectangular area from a PDF page and save it as a PNG file using ... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `Page`, `JpegDevice` | Demonstrates how to convert a specific page of a PDF document to a JPEG image using Aspose.Pdf's ... |
| [convert-pdf-pages-to-gif](./convert-pdf-pages-to-gif.cs) | Convert PDF Pages to GIF Images | `Document`, `GifDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf and convert each page to a GIF image using the GifDevice ... |
| [convert-pdf-pages-to-html](./convert-pdf-pages-to-html.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `HtmlSaveOptions`, `SplitIntoPages` | Demonstrates how to convert each page of a PDF into an individual HTML file using Aspose.Pdf's Ht... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Page` | Demonstrates how to convert each page of a PDF document into separate BMP image files using Aspos... |
| [convert-pdf-to-doc-custom-recognition](./convert-pdf-to-doc-custom-recognition.cs) | Convert PDF to DOC with Images Using Custom Recognition Mode | `Document`, `DocSaveOptions`, `Save` | Demonstrates converting a PDF file to a DOC document with Aspose.Pdf while preserving images by u... |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC with Default Settings | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF and save it as a DOC file using Aspose.Pdf with default text extraction o... |
| [convert-pdf-to-doc-text-mode](./convert-pdf-to-doc-text-mode.cs) | Convert PDF to DOC Using Textbox Mode | `Document`, `DocSaveOptions`, `Format` | Loads a PDF file, configures DocSaveOptions to use the Textbox recognition mode for plain‑text ex... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Zip the Result | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf and then compressing the generated DO... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates loading a PDF using Aspose.Pdf and saving it as a DOCX with DocSaveOptions set to Re... |
| [convert-pdf-to-docx-enhanced-recognition](./convert-pdf-to-docx-enhanced-recognition.cs) | Convert PDF to DOCX with Enhanced Table and Graphic Recognit... | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates how to convert a PDF file to a DOCX document using Aspose.Pdf with the EnhancedFlow ... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Embedded Images | `Document`, `DocSaveOptions`, `Page` | Demonstrates how to convert a PDF file to DOCX format using Aspose.Pdf and then iterate through e... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates how to convert a PDF file to a DOCX document using Aspose.Pdf while preserving the o... |
| [convert-pdf-to-docx-with-embedded-fonts](./convert-pdf-to-docx-with-embedded-fonts.cs) | Convert PDF to DOCX with Embedded Fonts | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates loading a PDF with Aspose.Pdf, configuring DocSaveOptions to embed fonts, and saving... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX with Footnote Preservation | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf while enabling enhanced flow recognit... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX with Language‑Specific Hyphenation | `Document`, `Save`, `ITaggedContent` | Shows how to load a PDF, set its language to enable hyphenation, configure DOCX save options, and... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `DocumentInfo`, `DocSaveOptions` | Shows how to load a PDF using Aspose.Pdf, set author and title metadata, and convert it to a DOCX... |
| [convert-pdf-to-docx-with-table-of-figures](./convert-pdf-to-docx-with-table-of-figures.cs) | Convert PDF to DOCX with Table of Figures | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a PDF, creating a tagged Table of Figures from extracted images, and saving ... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for conversion patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

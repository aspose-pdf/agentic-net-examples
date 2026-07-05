---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **102** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Devices;` (17/102 files)
- `using Aspose.Pdf.Text;` (15/102 files)
- `using Aspose.Pdf.Annotations;` (3/102 files)
- `using Aspose.Pdf.Drawing;` (3/102 files)
- `using Aspose.Pdf.Facades;` (3/102 files)
- `using System;` (102/102 files)
- `using System.IO;` (102/102 files)
- `using System.IO.Compression;` (4/102 files)
- `using System.Text;` (4/102 files)
- `using System.Collections.Generic;` (2/102 files)
- `using System.Xml.Linq;` (2/102 files)
- `using System.Diagnostics;` (1/102 files)
- `using System.Drawing;` (1/102 files)
- `using System.Linq;` (1/102 files)
- `using System.Text.Json;` (1/102 files)
- `using System.Text.RegularExpressions;` (1/102 files)

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
| [add-file-attachment-pdfa3b](./add-file-attachment-pdfa3b.cs) | Add File Attachment and Convert PDF to PDF/A‑3b | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed an external file into a PDF, optionally add a visible attachment annota... |
| [add-logo-to-pdf-and-convert-to-pptx](./add-logo-to-pdf-and-convert-to-pptx.cs) | Add Logo to PDF and Convert to PPTX | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, stamp a company logo on each page, and then save the mod... |
| [add-xml-attachment-to-pdfa](./add-xml-attachment-to-pdfa.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `FileSpecification` | Demonstrates converting a PDF to PDF/A‑1b and attaching an external XML file as a file attachment... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through a folder of PDF files, convert each page to a high‑resolution... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `TiffDevice`, `Resolution` | Demonstrates how to load multiple PDF files from a folder and convert each one into a single mult... |
| [batch-convert-pdfs-to-png-preserve-hierarchy](./batch-convert-pdfs-to-png-preserve-hierarchy.cs) | Batch Convert PDFs to PNG Images Preserving Folder Hierarchy | `Document`, `Page`, `PngDevice` | Demonstrates how to recursively locate PDF files, convert each page to a PNG image using Aspose.P... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Shows how to convert all PDF files in a folder to PPTX format using Aspose.Pdf, enabling the Slid... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `EpubLoadOptions`, `Document`, `Save` | Demonstrates loading an EPUB file using EpubLoadOptions with a custom page size and converting it... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX to PDF with Aspose.Pdf | `TeXLoadOptions`, `Document`, `Save` | Shows how to load a .tex file using TeXLoadOptions and convert it to a PDF while keeping equation... |
| [convert-markdown-to-pdf-preserving-code-blocks](./convert-markdown-to-pdf-preserving-code-blocks.cs) | Convert Markdown to PDF Preserving Code Blocks | `MdLoadOptions`, `Document`, `Save` | Demonstrates how to load a Markdown file with MdLoadOptions and convert it to a PDF document usin... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF with Aspose.Pdf | `Document`, `OfdLoadOptions`, `Save` | Shows how to load an OFD document using OfdLoadOptions and save it as a PDF with default settings... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HPGL/2 Vector Support | `PclLoadOptions`, `EnableHPGL2`, `Document` | Demonstrates loading a PCL file using Aspose.Pdf, optionally enabling HPGL/2 vector rendering, an... |
| [convert-pdf-page-region-to-png](./convert-pdf-page-region-to-png.cs) | Convert PDF Page Region to PNG Image | `Document`, `Page`, `Rectangle` | Demonstrates how to define a rectangular region on a PDF page using the CropBox and render that s... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `JpegDevice`, `PageCollection` | Shows how to load a PDF, validate a page number, and convert that specific page to a JPEG image u... |
| [convert-pdf-pages-to-emf](./convert-pdf-pages-to-emf.cs) | Convert PDF Pages to EMF Images | `Document`, `EmfDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and render each page to an EM... |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `Save`, `Dispose` | Demonstrates how to convert each page of a PDF into individual HTML files using Aspose.Pdf's Html... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Process` | Shows how to load a PDF document and export each page as a BMP image using Aspose.Pdf's BmpDevice... |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC with Default Settings | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF file and save it as a DOC document using Aspose.Pdf with the default text... |
| [convert-pdf-to-doc-image-only](./convert-pdf-to-doc-image-only.cs) | Convert PDF to DOC Using Textbox Recognition Mode | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates how to convert a PDF file to a DOC file with Aspose.Pdf while using the Textbox reco... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to DOC with Plain Text Extraction | `Document`, `DocSaveOptions`, `RecognitionMode` | Shows how to load a PDF with Aspose.Pdf and save it as a DOC file while extracting only plain tex... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Compress into ZIP | `Document`, `Page`, `TextFragment` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf and then compressing the resulting DO... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `DocSaveOptions`, `Format` | Shows how to load a PDF, set DocSaveOptions to Flow recognition mode for automatic content detect... |
| [convert-pdf-to-docx-enhanced-recognition](./convert-pdf-to-docx-enhanced-recognition.cs) | Convert PDF to DOCX with Enhanced Table and Graphic Recognit... | `Document`, `DocSaveOptions`, `RecognitionMode` | Shows how to convert a PDF file to DOCX using Aspose.Pdf with the EnhancedFlow recognition mode t... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Embedded Images | `Document`, `DocSaveOptions`, `Page` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf and then extracting all em... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX While Preserving Layout | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates how to use Aspose.Pdf to convert a PDF file to DOCX format using the standard textbo... |
| [convert-pdf-to-docx-with-embedded-font](./convert-pdf-to-docx-with-embedded-font.cs) | Convert PDF to DOCX with Embedded Custom Font | `Document`, `FindFont`, `Font` | Shows how to convert a PDF to DOCX using Aspose.Pdf while registering a custom TrueType font so t... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX with Footnote Preservation | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF and save it as a DOCX file using Aspose.Pdf, configuring save options to ... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX with Hyphenation | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates loading a PDF using Aspose.Pdf, converting it to DOCX with DocSaveOptions, and apply... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `DocumentInfo`, `Save` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf while setting custom author and title... |
| [convert-pdf-to-docx-with-table-of-figures](./convert-pdf-to-docx-with-table-of-figures.cs) | Convert PDF to DOCX with Table of Figures | `Document`, `Page`, `XImage` | Shows how to extract images from a PDF, build a table of figures, insert it into the PDF, and the... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

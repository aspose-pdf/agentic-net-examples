---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **150** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (101/150 files) ← category-specific
- `using Aspose.Pdf.Devices;` (17/150 files)
- `using Aspose.Pdf.Text;` (8/150 files)
- `using Aspose.Pdf.Annotations;` (3/150 files)
- `using Aspose.Pdf.Drawing;` (1/150 files)
- `using Aspose.Pdf.Facades;` (1/150 files)
- `using Aspose.Pdf.LogicalStructure;` (1/150 files)
- `using Aspose.Pdf.Tagged;` (1/150 files)
- `using System;` (101/150 files)
- `using System.IO;` (101/150 files)
- `using System.IO.Compression;` (5/150 files)
- `using System.Text;` (4/150 files)
- `using System.Collections.Generic;` (2/150 files)
- `using System.Linq;` (2/150 files)
- `using System.Text.RegularExpressions;` (2/150 files)
- `using System.Xml.Linq;` (2/150 files)
- `using System.Diagnostics;` (1/150 files)
- `using System.Drawing;` (1/150 files)
- `using System.Text.Json;` (1/150 files)

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
| [add-attachment-to-pdfa3b-document](./add-attachment-to-pdfa3b-document.cs) | Add attachment to pdfa3b document |  | Add attachment to pdfa3b document |
| [add-external-text-file-attachment-to-pdfa3b](./add-external-text-file-attachment-to-pdfa3b.cs) | Add External Text File Attachment to PDF/A‑3b Document | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed an external text file as a file attachment annotation and convert the P... |
| [add-external-xml-attachment-to-pdfa](./add-external-xml-attachment-to-pdfa.cs) | Add External XML Attachment to PDF/A‑1b Document | `Document`, `Convert`, `FileSpecification` | Demonstrates converting a PDF to PDF/A‑1b and attaching an external XML file as a file‑attachment... |
| [add-logo-to-pdf-and-convert-to-pptx](./add-logo-to-pdf-and-convert-to-pptx.cs) | Add logo to pdf and convert to pptx |  | Add logo to pdf and convert to pptx |
| [add-xml-attachment-to-pdfa1b-document](./add-xml-attachment-to-pdfa1b-document.cs) | Add xml attachment to pdfa1b document |  | Add xml attachment to pdfa1b document |
| [batch-convert-pdfs-to-jpeg-custom-naming](./batch-convert-pdfs-to-jpeg-custom-naming.cs) | Batch Convert PDFs to JPEG with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to convert all PDF files in a folder to JPEG images page‑by‑page using Aspose.Pd... |
| [batch-convert-pdfs-to-jpeg-images](./batch-convert-pdfs-to-jpeg-images.cs) | Batch convert pdfs to jpeg images |  | Batch convert pdfs to jpeg images |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `Add`, `TiffDevice` | Demonstrates how to merge several PDF files into a single document and then convert the combined ... |
| [batch-convert-pdfs-to-png-preserve-hierarchy](./batch-convert-pdfs-to-png-preserve-hierarchy.cs) | Batch convert pdfs to png preserve hierarchy |  | Batch convert pdfs to png preserve hierarchy |
| [batch-convert-pdfs-to-png](./batch-convert-pdfs-to-png.cs) | Batch Convert PDFs to PNG Preserving Folder Structure | `Document`, `PngDevice`, `Resolution` | Demonstrates how to recursively locate PDF files, keep their relative folder hierarchy, and conve... |
| [batch-convert-pdfs-to-pptx-slides-as-images](./batch-convert-pdfs-to-pptx-slides-as-images.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `ctor`, `PptxSaveOptions` | Iterates through PDF files in a folder and converts each to a PPTX presentation using Aspose.Pdf,... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch convert pdfs to pptx slidesasimages |  | Batch convert pdfs to pptx slidesasimages |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `EpubLoadOptions`, `Document`, `Save` | Demonstrates loading an EPUB file with EpubLoadOptions to set a custom page size and converting i... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX (.tex) to PDF with Aspose.Pdf | `TeXLoadOptions`, `Document`, `Save` | Demonstrates loading a LaTeX (.tex) file using Aspose.Pdf's TeXLoadOptions and converting it to a... |
| [convert-markdown-to-pdf-preserving-code-blocks](./convert-markdown-to-pdf-preserving-code-blocks.cs) | Convert Markdown to PDF Preserving Code Blocks | `MdLoadOptions`, `Document`, `Save` | Demonstrates loading a Markdown file with MdLoadOptions and converting it to a PDF while keeping ... |
| [convert-markdown-to-pdf](./convert-markdown-to-pdf.cs) | Convert markdown to pdf |  | Convert markdown to pdf |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF with Aspose.Pdf | `Document`, `OfdLoadOptions`, `Save` | Demonstrates loading an OFD file using OfdLoadOptions and saving it as a PDF with default settings. |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Vectors | `PclLoadOptions`, `Document`, `ctor` | Demonstrates loading a PCL file (including HP‑GL/2 vector data) using Aspose.Pdf and saving it as... |
| [convert-pdf-page-region-to-png](./convert-pdf-page-region-to-png.cs) | Convert pdf page region to png |  | Convert pdf page region to png |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `JpegDevice`, `Page` | Demonstrates how to load a PDF, validate a page number, and convert that specific page to a JPEG ... |
| [convert-pdf-pages-to-html](./convert-pdf-pages-to-html.cs) | Convert pdf pages to html |  | Convert pdf pages to html |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `HtmlSaveOptions`, `Save` | Demonstrates how to use Aspose.Pdf to convert each page of a PDF into its own HTML file by enabli... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Pages` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and render each page to a BMP... |
| [convert-pdf-to-doc-custom-recognition](./convert-pdf-to-doc-custom-recognition.cs) | Convert pdf to doc custom recognition |  | Convert pdf to doc custom recognition |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC with Default Settings | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates converting a PDF file to the legacy DOC format using Aspose.Pdf with default text ex... |
| [convert-pdf-to-doc-image-extraction](./convert-pdf-to-doc-image-extraction.cs) | Convert PDF to DOC with Image Extraction (Textbox Mode) | `Document`, `DocSaveOptions`, `Format` | Demonstrates how to convert a PDF file to a DOC document using Aspose.Pdf with custom recognition... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to DOC with Plain Text Extraction | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to convert a PDF file to a DOC file using Aspose.Pdf, configuring DocSaveOptions to ext... |
| [convert-pdf-to-doc-text-mode](./convert-pdf-to-doc-text-mode.cs) | Convert pdf to doc text mode |  | Convert pdf to doc text mode |
| [convert-pdf-to-docx-add-table-of-figures](./convert-pdf-to-docx-add-table-of-figures.cs) | Convert PDF to DOCX and Add Table of Figures | `Document`, `Page`, `XImage` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf while extracting images, generating c... |
| [convert-pdf-to-docx-and-pdfa](./convert-pdf-to-docx-and-pdfa.cs) | Convert PDF to DOCX and then to PDF/A | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to a DOCX file, and then creating a PDF... |
| ... | | | *and 120 more files* |

## Category Statistics
- Total examples: 150

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

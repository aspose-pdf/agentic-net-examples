---
name: conversion
description: C# examples for conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - conversion

> **Conversion** in PDF using C# / .NET -- **99** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **conversion** category.
This folder contains standalone C# examples for conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (99/99 files) ← category-specific
- `using Aspose.Pdf.Devices;` (18/99 files)
- `using Aspose.Pdf.Text;` (18/99 files)
- `using Aspose.Pdf.Drawing;` (3/99 files)
- `using Aspose.Pdf.Annotations;` (1/99 files)
- `using Aspose.Pdf.Tagged;` (1/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (99/99 files)
- `using System.Text;` (4/99 files)
- `using System.IO.Compression;` (3/99 files)
- `using System.Collections.Generic;` (2/99 files)
- `using System.Diagnostics;` (2/99 files)
- `using System.Text.RegularExpressions;` (2/99 files)
- `using System.Xml;` (2/99 files)
- `using System.Drawing;` (1/99 files)
- `using System.Drawing.Imaging;` (1/99 files)
- `using System.Linq;` (1/99 files)
- `using System.Security.Cryptography;` (1/99 files)
- `using System.Text.Json;` (1/99 files)
- `using System.Xml.Xsl;` (1/99 files)

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
| [add-external-text-file-attachment-to-pdfa3b](./add-external-text-file-attachment-to-pdfa3b.cs) | Add External Text File Attachment to PDF/A‑3b Document | `Document`, `FileSpecification`, `Convert` | Demonstrates how to embed an external text file into a PDF, convert the document to PDF/A‑3b comp... |
| [add-external-xml-attachment-to-pdfa](./add-external-xml-attachment-to-pdfa.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `PDF_A_1B` | Shows how to convert a PDF to PDF/A‑1b format and embed an external XML file as an attachment usi... |
| [batch-convert-pdfs-to-jpeg-custom-naming](./batch-convert-pdfs-to-jpeg-custom-naming.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Shows how to iterate through PDF files in a folder, render each page to a JPEG image using Aspose... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `Add`, `TiffDevice` | Demonstrates how to combine multiple PDF files into a single document and convert it to a multi‑p... |
| [batch-convert-pdfs-to-png](./batch-convert-pdfs-to-png.cs) | Batch Convert PDFs to PNG Images Preserving Folder Structure | `Document`, `PngDevice`, `Resolution` | Demonstrates how to recursively locate PDF files, keep their relative directory hierarchy, and co... |
| [batch-convert-pdfs-to-pptx-slides-as-images](./batch-convert-pdfs-to-pptx-slides-as-images.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Shows how to convert all PDF files in a directory to PPTX presentations using Aspose.Pdf, rasteri... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `Document`, `EpubLoadOptions`, `Save` | Demonstrates loading an EPUB file using EpubLoadOptions, setting a custom page size, and saving i... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX to PDF using pdflatex and Aspose.Pdf | `Document`, `Save` | Shows how to compile a .tex file with pdflatex, load the generated PDF with Aspose.Pdf, and save ... |
| [convert-markdown-to-pdf-preserving-code-blocks](./convert-markdown-to-pdf-preserving-code-blocks.cs) | Convert Markdown to PDF Preserving Code Blocks | `MdLoadOptions`, `Document`, `Save` | Demonstrates loading a Markdown file with MdLoadOptions and converting it to a PDF while keeping ... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF with Aspose.Pdf | `Document`, `Save` | Shows how to load an OFD file and save it as a PDF using Aspose.Pdf's Document class with default... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Vectors | `PclLoadOptions`, `Document`, `Save` | Demonstrates loading a PCL file (including HP‑GL/2 vector graphics) using Aspose.Pdf and saving i... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `JpegDevice`, `Pages` | Shows how to render a specific page of a PDF document to a JPEG file using Aspose.Pdf's JpegDevic... |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `HtmlSaveOptions`, `Save` | Shows how to load a PDF with Aspose.Pdf, configure HtmlSaveOptions to split each page into its ow... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Page` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and save each page as a BMP f... |
| [convert-pdf-to-doc-default](./convert-pdf-to-doc-default.cs) | Convert PDF to DOC using Aspose.Pdf | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file with Aspose.Pdf and save it as a DOC document using default c... |
| [convert-pdf-to-doc-image-extraction](./convert-pdf-to-doc-image-extraction.cs) | Convert PDF to DOC with Recognition Mode | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF using Aspose.Pdf, set a recognition mode via DocSaveOptions, and save the... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to Plain Text DOC using TextAbsorber | `Document`, `TextAbsorber`, `Accept` | Loads a PDF file, extracts its plain text with Aspose.Pdf's TextAbsorber, and writes the text to ... |
| [convert-pdf-to-docx-add-table-of-figures](./convert-pdf-to-docx-add-table-of-figures.cs) | Convert PDF to DOCX and Extract Images | `Document`, `Page`, `XImage` | Shows how to extract images from a PDF and then convert the PDF to a DOCX file using Aspose.Pdf. |
| [convert-pdf-to-docx-and-pdfa](./convert-pdf-to-docx-and-pdfa.cs) | Convert PDF to DOCX and then to PDF/A | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to use Aspose.Pdf to convert a PDF file to DOCX format and then create a PDF/A‑1... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Zip the Result | `Document`, `DocSaveOptions`, `Save` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf and then compressing the generated DO... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `Save`, `DocSaveOptions` | Demonstrates how to load a PDF file and save it as a DOCX document using Aspose.Pdf with DocSaveO... |
| [convert-pdf-to-docx-enhanced-flow](./convert-pdf-to-docx-enhanced-flow.cs) | Convert PDF to DOCX with Enhanced Recognition | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF using Aspose.Pdf, configure DocSaveOptions for DOCX output (which enables... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Images | `Document`, `DocSaveOptions`, `Page` | Demonstrates how to convert a PDF file to DOCX format using Aspose.Pdf and then extract all embed... |
| [convert-pdf-to-docx-layout](./convert-pdf-to-docx-layout.cs) | Convert PDF to DOCX with Standard Layout Preservation | `Document`, `Save`, `DocSaveOptions` | Demonstrates how to convert a PDF file to a DOCX document using Aspose.Pdf's standard recognition... |
| [convert-pdf-to-docx-with-embedded-fonts](./convert-pdf-to-docx-with-embedded-fonts.cs) | Convert PDF to DOCX with Embedded Custom Fonts | `Document`, `DocSaveOptions`, `Substitutions` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf while registering and embedding custo... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX with Footnotes Preservation | `Document`, `DocSaveOptions`, `Format` | Demonstrates loading a PDF file and saving it as a DOCX document while preserving footnotes using... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX with Aspose.Pdf | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF file and save it as a DOCX document using Aspose.Pdf's Document and DocSa... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `Info`, `Save` | Shows how to convert a PDF to DOCX using Aspose.Pdf while setting custom metadata properties such... |
| [convert-pdf-to-docx-with-stats](./convert-pdf-to-docx-with-stats.cs) | Convert PDF to DOCX and Generate Conversion Report | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to DOCX format, measuring conversion ti... |
| [convert-pdf-to-emf-images](./convert-pdf-to-emf-images.cs) | Convert PDF to EMF Images Preserving Vector Data | `Document`, `Page`, `EmfDevice` | Demonstrates converting each page of a PDF into EMF vector images using Aspose.Pdf's EmfDevice wi... |
| ... | | | *and 69 more files* |

## Category Statistics
- Total examples: 99

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
Updated: 2026-11-05 | Run: `20261105_011725_543597`
<!-- AUTOGENERATED:END -->

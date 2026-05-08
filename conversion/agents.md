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

- `using Aspose.Pdf;` (102/102 files) ← category-specific
- `using Aspose.Pdf.Devices;` (16/102 files)
- `using Aspose.Pdf.Text;` (14/102 files)
- `using Aspose.Pdf.Annotations;` (2/102 files)
- `using Aspose.Pdf.Facades;` (2/102 files)
- `using System;` (102/102 files)
- `using System.IO;` (102/102 files)
- `using System.Collections.Generic;` (3/102 files)
- `using System.IO.Compression;` (3/102 files)
- `using System.Text;` (3/102 files)
- `using System.Linq;` (2/102 files)
- `using System.Drawing;` (1/102 files)
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
| [add-company-logo-and-convert-pdf-to-pptx](./add-company-logo-and-convert-pdf-to-pptx.cs) | Add Company Logo to PDF and Convert to PPTX | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, stamping each page with a logo image, and converting the modified doc... |
| [add-file-attachment-to-pdfa3b](./add-file-attachment-to-pdfa3b.cs) | Add File Attachment to PDF/A‑3b Document | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates embedding an external text file as an attachment, adding a file‑attachment annotatio... |
| [add-xml-attachment-to-pdfa1b](./add-xml-attachment-to-pdfa1b.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF, converting it to PDF/A‑1b compliance, embedding an external XML file ... |
| [batch-convert-pdfs-to-jpeg-images](./batch-convert-pdfs-to-jpeg-images.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through a folder of PDF files, convert each page to a JPEG image usin... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `Merge`, `TiffDevice` | Shows how to merge all PDF files in a folder and convert the combined document into a multi‑page ... |
| [batch-convert-pdfs-to-png-preserve-folder-structur...](./batch-convert-pdfs-to-png-preserve-folder-structure.cs) | Batch Convert PDFs to PNG While Preserving Folder Structure | `Document`, `PngDevice`, `Resolution` | Demonstrates how to recursively convert all PDF files in a source directory to PNG images using A... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `Save`, `Dispose` | Demonstrates how to convert multiple PDF files to PPTX format in a single run, using Aspose.Pdf w... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `EpubLoadOptions`, `Document`, `Save` | Shows how to load an EPUB file using EpubLoadOptions to set a custom page size and convert it to ... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX File to PDF | `TeXLoadOptions`, `RasterizeFormulas`, `ShowTerminalOutput` | Demonstrates loading a LaTeX (.tex) file with Aspose.Pdf, configuring TeX options, and saving the... |
| [convert-markdown-to-pdf](./convert-markdown-to-pdf.cs) | Convert Markdown to PDF Preserving Code Blocks | `Document`, `MdLoadOptions`, `LoadOptions` | Demonstrates how to load a Markdown file with MdLoadOptions and convert it directly to a PDF docu... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF with Aspose.Pdf | `Document`, `OfdLoadOptions`, `Save` | Shows how to load an OFD document using OfdLoadOptions and save it as a PDF with default settings... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Vector Support | `PclLoadOptions`, `ConversionEngine`, `ConversionEngines` | Demonstrates loading a PCL file, enabling HP‑GL/2 vector graphics via the NewEngine conversion en... |
| [convert-pdf-page-region-to-png](./convert-pdf-page-region-to-png.cs) | Convert PDF Page Region to PNG | `Document`, `Page`, `Rectangle` | Shows how to extract a defined rectangular region from a PDF page and render it as a PNG image us... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `Page`, `JpegDevice` | Shows how to load a PDF, validate a page number, and convert that specific page to a JPEG image u... |
| [convert-pdf-pages-to-jpeg-images](./convert-pdf-pages-to-jpeg-images.cs) | Convert PDF Pages to JPEG Images | `Document`, `PageCollection`, `Page` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, and save the pages as JPEG fi... |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `Save`, `HtmlSaveOptions` | Shows how to load a PDF with Aspose.Pdf, configure HtmlSaveOptions to split the document into pag... |
| [convert-pdf-to-bmp-images](./convert-pdf-to-bmp-images.cs) | Convert PDF to BMP Images | `Document`, `BmpDevice`, `Page` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages, and save each page as a BMP f... |
| [convert-pdf-to-doc-image-only](./convert-pdf-to-doc-image-only.cs) | Convert PDF to DOC with Image‑Only Extraction | `Document`, `DocSaveOptions`, `Format` | Shows how to convert a PDF file to a DOC document using Aspose.Pdf while preserving images only v... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to DOC with Plain Text Extraction | `Document`, `DocSaveOptions`, `RecognitionMode` | Shows how to convert a PDF file to a DOC file using Aspose.Pdf while configuring DocSaveOptions t... |
| [convert-pdf-to-doc](./convert-pdf-to-doc.cs) | Convert PDF to DOC using Aspose.Pdf | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF file and save it as a DOC document with default text recognition using As... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Zip the Result | `Document`, `DocSaveOptions`, `Format` | Shows how to use Aspose.Pdf to convert a PDF file to DOCX format and then compress the generated ... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Layout Detection | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to convert a PDF file to DOCX using Aspose.Pdf with DocSaveOptions.Mode set to Flow for... |
| [convert-pdf-to-docx-embed-custom-fonts](./convert-pdf-to-docx-embed-custom-fonts.cs) | Convert PDF to DOCX with Embedded Custom Fonts | `FontRepository`, `OpenFont`, `Add` | Demonstrates converting a PDF to DOCX using Aspose.Pdf while registering a custom TrueType font a... |
| [convert-pdf-to-docx-enhanced-recognition](./convert-pdf-to-docx-enhanced-recognition.cs) | Convert PDF to DOCX with Enhanced Table and Graphic Recognit... | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to convert a PDF file to a DOCX document using Aspose.Pdf with enhanced recognition set... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Images | `Document`, `DocSaveOptions`, `Save` | Shows how to convert a PDF file to DOCX using Aspose.Pdf and then extract all embedded images to ... |
| [convert-pdf-to-docx-layout-preservation](./convert-pdf-to-docx-layout-preservation.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates how to load a PDF file and save it as a DOCX document using Aspose.Pdf while preserv... |
| [convert-pdf-to-docx-with-flow-hyphenation](./convert-pdf-to-docx-with-flow-hyphenation.cs) | Convert PDF to DOCX with Flow Recognition for Hyphenation | `Document`, `DocSaveOptions`, `Save` | Loads a PDF, configures DocSaveOptions to use Flow recognition mode and hyphenation‑friendly sett... |
| [convert-pdf-to-docx-with-footnotes](./convert-pdf-to-docx-with-footnotes.cs) | Convert PDF to DOCX with Footnote Preservation | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf while preserving footnotes... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `Info`, `Save` | Shows how to convert a PDF file to DOCX using Aspose.Pdf while setting custom metadata properties... |
| [convert-pdf-to-docx-with-report](./convert-pdf-to-docx-with-report.cs) | Convert PDF to DOCX and Generate Conversion Report | `Document`, `DocSaveOptions`, `Format` | Demonstrates loading a PDF with Aspose.Pdf, converting it to DOCX using DocSaveOptions, and creat... |
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
Updated: 2026-04-28 | Run: `20260428_141822_cb9123`
<!-- AUTOGENERATED:END -->

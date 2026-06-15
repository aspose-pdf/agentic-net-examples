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

- `using Aspose.Pdf;` (101/101 files) ← category-specific
- `using Aspose.Pdf.Devices;` (17/101 files)
- `using Aspose.Pdf.Text;` (12/101 files)
- `using Aspose.Pdf.Annotations;` (3/101 files)
- `using Aspose.Pdf.Facades;` (2/101 files)
- `using Aspose.Pdf.LogicalStructure;` (2/101 files)
- `using Aspose.Pdf.Tagged;` (2/101 files)
- `using Aspose.Pdf.Drawing;` (1/101 files)
- `using System;` (101/101 files)
- `using System.IO;` (101/101 files)
- `using System.IO.Compression;` (4/101 files)
- `using System.Collections.Generic;` (3/101 files)
- `using System.Text;` (3/101 files)
- `using System.Text.RegularExpressions;` (2/101 files)
- `using System.Diagnostics;` (1/101 files)
- `using System.Drawing;` (1/101 files)
- `using System.Linq;` (1/101 files)
- `using System.Text.Json;` (1/101 files)
- `using System.Xml;` (1/101 files)
- `using System.Xml.Linq;` (1/101 files)

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
| [add-attachment-and-convert-to-pdfa3b](./add-attachment-and-convert-to-pdfa3b.cs) | Add External File Attachment and Convert to PDF/A‑3b | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed an external text file into a PDF, optionally add a visible file‑attachm... |
| [add-logo-to-pdf-and-convert-to-pptx](./add-logo-to-pdf-and-convert-to-pptx.cs) | Add Logo to PDF and Convert to PPTX | `Document`, `Page`, `ImageStamp` | Shows how to stamp a company logo onto each page of a PDF using ImageStamp and then save the modi... |
| [add-xml-attachment-to-pdfa1b](./add-xml-attachment-to-pdfa1b.cs) | Add XML Attachment to PDF/A-1b Document | `Document`, `Convert`, `FileSpecification` | Demonstrates converting a PDF to PDF/A‑1b and attaching an external XML file as a file attachment... |
| [batch-convert-pdfs-to-jpeg-with-custom-naming](./batch-convert-pdfs-to-jpeg-with-custom-naming.cs) | Batch Convert PDFs to JPEG Images with Custom Naming | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to convert each page of multiple PDF files in a folder to JPEG images using Aspo... |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to loop through a list of PDF files and convert each one into a multi‑page TIFF ... |
| [batch-convert-pdfs-to-png-preserving-folder-struct...](./batch-convert-pdfs-to-png-preserving-folder-structure.cs) | Batch Convert PDFs to PNG Preserving Folder Structure | `Document`, `PngDevice`, `Resolution` | Demonstrates how to recursively locate PDF files, convert each page to a PNG image using Aspose.P... |
| [batch-convert-pdfs-to-pptx-slidesasimages](./batch-convert-pdfs-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Demonstrates how to convert multiple PDF files to PPTX format in a batch, using the SlidesAsImage... |
| [convert-epub-to-pdf-custom-page-size](./convert-epub-to-pdf-custom-page-size.cs) | Convert EPUB to PDF with Custom Page Size | `EpubLoadOptions`, `Document`, `Save` | Demonstrates loading an EPUB file using EpubLoadOptions with a custom page size and saving it as ... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX to PDF with Aspose.Pdf | `TeXLoadOptions`, `RasterizeFormulas`, `ShowTerminalOutput` | Shows how to load a .tex file using TeXLoadOptions and convert it to a PDF while keeping equation... |
| [convert-markdown-to-pdf](./convert-markdown-to-pdf.cs) | Convert Markdown to PDF with Code Block Preservation | `MdLoadOptions`, `Document`, `Save` | Demonstrates loading a Markdown file using MdLoadOptions and converting it to a PDF document whil... |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF with Aspose.Pdf | `OfdLoadOptions`, `Document`, `Save` | Demonstrates loading an OFD document using OfdLoadOptions and saving it as a PDF with default set... |
| [convert-pcl-to-pdf-hpgl2](./convert-pcl-to-pdf-hpgl2.cs) | Convert PCL to PDF with HP‑GL/2 Support | `PclLoadOptions`, `Document`, `Save` | Demonstrates loading a PCL file (including HP‑GL/2 vectors) using Aspose.Pdf and saving it direct... |
| [convert-pdf-page-region-to-png](./convert-pdf-page-region-to-png.cs) | Convert PDF Page Region to PNG | `Document`, `Page`, `Rectangle` | Shows how to define a rectangular region on a PDF page, set the CropBox to that region, and rende... |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `Page`, `JpegDevice` | Shows how to load a PDF, select a specific page, and save it as a JPEG image using Aspose.Pdf's J... |
| [convert-pdf-pages-to-gif](./convert-pdf-pages-to-gif.cs) | Convert PDF Pages to GIF Images | `Document`, `GifDevice`, `Process` | Shows how to load a PDF with Aspose.Pdf and use GifDevice to render each page as a separate GIF f... |
| [convert-pdf-pages-to-separate-html-files](./convert-pdf-pages-to-separate-html-files.cs) | Convert PDF Pages to Separate HTML Files | `Document`, `HtmlSaveOptions`, `Save` | Demonstrates loading a PDF with Aspose.Pdf, configuring HtmlSaveOptions to split the document int... |
| [convert-pdf-to-bmp](./convert-pdf-to-bmp.cs) | Convert PDF Pages to BMP Images | `Document`, `BmpDevice`, `Process` | Shows how to load a PDF document and convert each page to a BMP image using Aspose.Pdf's BmpDevic... |
| [convert-pdf-to-doc-image-extraction](./convert-pdf-to-doc-image-extraction.cs) | Convert PDF to DOC with Image Extraction Using Custom Recogn... | `Document`, `DocSaveOptions`, `Save` | Shows how to convert a PDF file to a DOC document while extracting only images by using Aspose.Pd... |
| [convert-pdf-to-doc-plain-text](./convert-pdf-to-doc-plain-text.cs) | Convert PDF to DOC with Plain Text Extraction | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file and save it as a DOC document while extracting only plain tex... |
| [convert-pdf-to-doc](./convert-pdf-to-doc.cs) | Convert PDF to DOC with Aspose.Pdf | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF file and save it as a DOC document using Aspose.Pdf with default text rec... |
| [convert-pdf-to-docx-and-zip](./convert-pdf-to-docx-and-zip.cs) | Convert PDF to DOCX and Zip the Result | `Document`, `DocSaveOptions`, `Format` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf and then compressing the g... |
| [convert-pdf-to-docx-auto-detection](./convert-pdf-to-docx-auto-detection.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF using Aspose.Pdf and save it as a DOCX file with DocSaveOptions set to au... |
| [convert-pdf-to-docx-enhanced-recognition](./convert-pdf-to-docx-enhanced-recognition.cs) | Convert PDF to DOCX with Enhanced Table and Graphic Recognit... | `Document`, `DocSaveOptions`, `RecognitionMode` | Demonstrates how to convert a PDF file to a DOCX document using Aspose.Pdf with the enhanced flow... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Embedded Images | `Document`, `DocSaveOptions`, `Page` | Loads a PDF, converts it to a DOCX file using Aspose.Pdf, then iterates through each page to extr... |
| [convert-pdf-to-docx-preserve-footnotes](./convert-pdf-to-docx-preserve-footnotes.cs) | Convert PDF to DOCX while Preserving Footnotes | `Document`, `DocSaveOptions`, `DocFormat` | Shows how to load a PDF with Aspose.Pdf, configure DocSaveOptions for DOCX output, and save the d... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `DocFormat` | Demonstrates how to use Aspose.Pdf to convert a PDF file to DOCX while preserving the original la... |
| [convert-pdf-to-docx-with-embedded-custom-font](./convert-pdf-to-docx-with-embedded-custom-font.cs) | Convert PDF to DOCX with Embedded Custom Font | `Document`, `DocSaveOptions`, `FontRepository` | Demonstrates converting a PDF to DOCX using Aspose.Pdf while registering a custom TrueType font s... |
| [convert-pdf-to-docx-with-hyphenation](./convert-pdf-to-docx-with-hyphenation.cs) | Convert PDF to DOCX with Language‑Specific Hyphenation | `Document`, `ITaggedContent`, `TaggedContent` | Shows how to load a PDF, set its language to influence hyphenation, configure DOCX conversion opt... |
| [convert-pdf-to-docx-with-metadata](./convert-pdf-to-docx-with-metadata.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `DocumentInfo`, `DocSaveOptions` | Shows how to convert a PDF to DOCX using Aspose.Pdf while setting custom metadata properties such... |
| [convert-pdf-to-docx-with-report](./convert-pdf-to-docx-with-report.cs) | Convert PDF to DOCX and Generate Conversion Report | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to convert a PDF file to DOCX using Aspose.Pdf, measure conversion time, collect... |
| ... | | | *and 71 more files* |

## Category Statistics
- Total examples: 101

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
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for conversion patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_013009_d919e8`
<!-- AUTOGENERATED:END -->

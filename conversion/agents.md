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

- `using Aspose.Pdf;` (100/100 files) ← category-specific
- `using Aspose.Pdf.Devices;` (16/100 files)
- `using Aspose.Pdf.Text;` (8/100 files)
- `using Aspose.Pdf.Annotations;` (2/100 files)
- `using Aspose.Pdf.Facades;` (1/100 files)
- `using System;` (100/100 files)
- `using System.IO;` (100/100 files)
- `using System.Drawing;` (3/100 files)
- `using System.IO.Compression;` (3/100 files)
- `using System.Collections.Generic;` (2/100 files)
- `using System.Runtime.InteropServices;` (2/100 files)
- `using System.Text;` (2/100 files)
- `using System.Text.RegularExpressions;` (2/100 files)
- `using System.Diagnostics;` (1/100 files)
- `using System.Drawing.Imaging;` (1/100 files)
- `using System.Linq;` (1/100 files)
- `using System.Reflection;` (1/100 files)
- `using System.Text.Json;` (1/100 files)
- `using System.Xml.Linq;` (1/100 files)

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
| [add-text-file-attachment-pdfa3b](./add-text-file-attachment-pdfa3b.cs) | Add Text File Attachment to PDF/A‑3b Document | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed an external text file as a file‑attachment annotation and then convert ... |
| [add-xml-attachment-pdfa](./add-xml-attachment-pdfa.cs) | Add XML Attachment to PDF/A‑1b Document | `Document`, `Convert`, `PdfFormat` | Converts a PDF to PDF/A‑1b compliance and attaches an external XML file to the resulting document. |
| [batch-convert-pdf-to-jpeg](./batch-convert-pdf-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Pages` | Converts all PDF files in a folder to JPEG images, one image per page, using a custom naming patt... |
| [batch-convert-pdf-to-pptx-slidesasimages](./batch-convert-pdf-to-pptx-slidesasimages.cs) | Batch Convert PDFs to PPTX with SlidesAsImages | `Document`, `PptxSaveOptions`, `Save` | Converts all PDF files in a folder to PPTX presentations where each slide is rendered as an image. |
| [batch-convert-pdfs-to-multi-page-tiff](./batch-convert-pdfs-to-multi-page-tiff.cs) | Batch Convert PDFs to Multi‑Page TIFF | `Document`, `TiffDevice`, `Process` | Converts each PDF file in a folder into a single multi‑page TIFF image using Aspose.Pdf. |
| [batch-pdf-to-png](./batch-pdf-to-png.cs) | Batch Convert PDFs to PNG Images Preserving Folder Structure | `Document`, `Page`, `ConvertPageToPNGMemoryStream` | Recursively scans a source folder for PDF files, converts each page to PNG, and saves the images ... |
| [convert-latex-to-pdf](./convert-latex-to-pdf.cs) | Convert LaTeX File to PDF with Aspose.Pdf | `Document`, `TeXLoadOptions` | Loads a .tex file using TeXLoadOptions and saves it as a PDF, preserving equations and formatting. |
| [convert-ofd-to-pdf](./convert-ofd-to-pdf.cs) | Convert OFD to PDF | `Document`, `OfdLoadOptions`, `Save` | Loads an OFD file using OfdLoadOptions and saves it as a PDF with default settings. |
| [convert-pdf-page-to-jpeg](./convert-pdf-page-to-jpeg.cs) | Convert PDF Page to JPEG Image | `Document`, `JpegDevice`, `Process` | Demonstrates how to extract a specific page from a PDF and save it as a JPEG image using Aspose.P... |
| [convert-pdf-to-docx-add-table-of-figures](./convert-pdf-to-docx-add-table-of-figures.cs) | Convert PDF to DOCX and Add Table of Figures | `Document`, `Table`, `Row` | Loads a PDF, extracts its images, creates a table of figures listing each image and its page, the... |
| [convert-pdf-to-docx-auto](./convert-pdf-to-docx-auto.cs) | Convert PDF to DOCX with Automatic Content Detection | `Document`, `DocSaveOptions`, `Save(string, SaveOptions)` | Demonstrates converting a PDF file to DOCX format using DocSaveOptions with Mode set to Auto for ... |
| [convert-pdf-to-docx-extract-images](./convert-pdf-to-docx-extract-images.cs) | Convert PDF to DOCX and Extract Embedded Images | `Document`, `DocSaveOptions`, `XImage` | Demonstrates converting a PDF file to DOCX format and extracting all embedded images to a designa... |
| [convert-pdf-to-docx](./convert-pdf-to-docx.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Demonstrates converting a PDF file to a DOCX document using Aspose.Pdf with the Textbox recogniti... |
| [convert-pdf-to-docx__v2](./convert-pdf-to-docx__v2.cs) | Convert PDF to DOCX with Default Text Extraction | `Document`, `DocSaveOptions`, `Save` | Demonstrates converting a PDF file to DOCX format using Aspose.Pdf with default text extraction s... |
| [convert-pdf-to-docx__v3](./convert-pdf-to-docx__v3.cs) | Convert PDF to DOCX with Custom Metadata | `Document`, `DocSaveOptions`, `Save` | Loads a PDF, sets the author and title metadata, and saves it as a DOCX file. |
| [convert-pdf-to-epub](./convert-pdf-to-epub.cs) | Convert PDF to EPUB | `Document`, `EpubSaveOptions` | Demonstrates converting a PDF document to an EPUB file using Aspose.Pdf with default conversion s... |
| [convert-pdf-to-jpeg](./convert-pdf-to-jpeg.cs) | Convert PDF Pages to JPEG Images | `Document`, `JpegDevice`, `Process` | Demonstrates converting each page of a PDF document into separate JPEG images using Aspose.Pdf's ... |
| [convert-pdf-to-latex](./convert-pdf-to-latex.cs) | Convert PDF to LaTeX using TeXSaveOptions | `Document`, `Save` | Loads a PDF file and saves it as a LaTeX (.tex) document, directing the generated files to a spec... |
| [convert-pdf-to-mobixml](./convert-pdf-to-mobixml.cs) | Convert PDF to MobiXml using Aspose.Pdf | `Document`, `MobiXmlSaveOptions`, `Save` | Demonstrates loading a PDF file and saving it as MobiXml format with default MobiXmlSaveOptions. |
| [convert-pdf-to-mobixml__v2](./convert-pdf-to-mobixml__v2.cs) | Convert PDF to MobiXml with Custom Metadata | `Document`, `MobiXmlSaveOptions`, `DocumentInfo` | Loads a PDF, sets author and publisher metadata, and saves it as MobiXml using Aspose.Pdf. |
| [convert-pdf-to-pdfa-1b](./convert-pdf-to-pdfa-1b.cs) | Convert PDF to PDF/A-1b with Validation | `Document`, `Validate`, `Convert` | Loads a PDF, validates it for PDF/A‑1b compliance, converts it to PDF/A‑1b, validates the result,... |
| [convert-pdf-to-pdfa-auto-tagging](./convert-pdf-to-pdfa-auto-tagging.cs) | Convert PDF to PDF/A with Auto‑Tagging | `Document`, `AutoTaggingSettings`, `PdfFormatConversionOptions` | Shows how to enable auto‑tagging during PDF/A conversion by configuring AutoTaggingSettings on Pd... |
| [convert-pdf-to-pdfa-font-substitution](./convert-pdf-to-pdfa-font-substitution.cs) | Convert PDF to PDF/A with Font Substitution Fallback | `Document`, `PdfFormatConversionOptions`, `FontEmbeddingOptions` | Demonstrates how to convert a PDF to PDF/A while configuring FontEmbeddingOptions to substitute m... |
| [convert-pdf-to-pdfa1b-skip](./convert-pdf-to-pdfa1b-skip.cs) | Convert PDF to PDF/A-1b with Skip Error Action | `Document`, `Convert`, `PdfFormat` | Demonstrates converting a PDF to PDF/A‑1b while skipping (deleting) elements that cannot be conve... |
| [convert-pdf-to-pdfa3b-font-substitution](./convert-pdf-to-pdfa3b-font-substitution.cs) | Convert PDF to PDF/A‑3b with Font Substitution | `Document`, `FontSubstitution`, `PdfFormatConversionOptions` | Demonstrates converting a PDF to PDF/A‑3b while substituting missing fonts using the FontSubstitu... |
| [convert-pdf-to-pdfa4-auto-tagging](./convert-pdf-to-pdfa4-auto-tagging.cs) | Convert PDF to PDF/A‑4 with Auto‑Tagging | `Document`, `PdfFormatConversionOptions`, `AutoTaggingSettings` | Demonstrates converting a PDF to PDF/A‑4 while enabling automatic tagging using AutoTaggingSettin... |
| [convert-pdf-to-pdfa4](./convert-pdf-to-pdfa4.cs) | Convert PDF to PDF/A‑4 with ConvertErrorAction | `Document`, `Convert`, `PdfFormat` | Demonstrates converting a PDF to PDF/A‑4 while setting ConvertErrorAction to Convert to attempt c... |
| [convert-pdf-to-pptx-embed-fonts](./convert-pdf-to-pptx-embed-fonts.cs) | Convert PDF to PPTX with Embedded Fonts | `Document`, `PptxSaveOptions`, `Save` | Demonstrates converting a PDF file to a PPTX presentation while embedding font glyphs to preserve... |
| [convert-pdf-to-pptx-progress](./convert-pdf-to-pptx-progress.cs) | Convert PDF to PPTX with Progress Logging | `Document`, `Save`, `PptxSaveOptions` | Demonstrates converting a PDF file to PPTX format while logging conversion progress via a custom ... |
| [convert-pdf-to-pptx-with-progress](./convert-pdf-to-pptx-with-progress.cs) | Convert PDF to PPTX with Custom Progress Handler | `Document`, `PptxSaveOptions`, `IProgressHandler` | Demonstrates converting a PDF file to PPTX format while reporting conversion progress via a custo... |
| ... | | | *and 70 more files* |

## Category Statistics
- Total examples: 100

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

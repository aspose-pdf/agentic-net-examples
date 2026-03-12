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

- `using Aspose.Pdf;` (230/230 files) ← category-specific
- `using Aspose.Pdf.Facades;` (14/230 files)
- `using Aspose.Pdf.Text;` (4/230 files)
- `using Aspose.Pdf.Devices;` (1/230 files)
- `using Aspose.Pdf.LogicalStructure;` (1/230 files)
- `using Aspose.Pdf.Tagged;` (1/230 files)
- `using Aspose.Pdf.Vector;` (1/230 files)
- `using System;` (230/230 files)
- `using System.IO;` (228/230 files)
- `using System.Net;` (2/230 files)
- `using System.IO.Compression;` (1/230 files)

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

| File | Key APIs | Description |
|------|----------|-------------|
| [Compress-SVG-images-during-conversion-HTML-PDF](./Compress-SVG-images-during-conversion-HTML-PDF.cs) | `HtmlLoadOptions` | Compress SVG images during conversion HTML PDF |
| [Compress-SVG-images-during-conversion-PDF-HTML](./Compress-SVG-images-during-conversion-PDF-HTML.cs) | `HtmlSaveOptions` | Compress SVG images during conversion PDF HTML |
| [Convert-HTML-to-PDF-HTML-PDF](./Convert-HTML-to-PDF-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF HTML PDF |
| [Convert-HTML-to-PDF-and-compress-SVG-images-HTML-PDF](./Convert-HTML-to-PDF-and-compress-SVG-images-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF and compress SVG images HTML PDF |
| [Convert-HTML-to-PDF-and-generate-only-body-content-files-HTM...](./Convert-HTML-to-PDF-and-generate-only-body-content-files-HTML-HTML.cs) | `HtmlLoadOptions`, `HtmlSaveOptions` | Convert HTML to PDF and generate only body content files HTML HTML |
| [Convert-HTML-to-PDF-and-render-PDF-layers-separately-HTML-PD...](./Convert-HTML-to-PDF-and-render-PDF-layers-separately-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF and render PDF layers separately HTML PDF |
| [Convert-HTML-to-PDF-and-render-transparent-text-HTML-PDF](./Convert-HTML-to-PDF-and-render-transparent-text-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF and render transparent text HTML PDF |
| [Convert-HTML-to-PDF-and-save-images-as-PNG-background-HTML-P...](./Convert-HTML-to-PDF-and-save-images-as-PNG-background-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF and save images as PNG background HTML PDF |
| [Convert-HTML-to-PDF-and-specify-folder-for-SVG-files-HTML-PD...](./Convert-HTML-to-PDF-and-specify-folder-for-SVG-files-HTML-PDF.cs) | `HtmlLoadOptions`, `SvgExtractor` | Convert HTML to PDF and specify folder for SVG files HTML PDF |
| [Convert-HTML-to-PDF-and-split-output-into-multiple-HTML-page...](./Convert-HTML-to-PDF-and-split-output-into-multiple-HTML-pages-HTML-HTML.cs) | `HtmlLoadOptions` | Convert HTML to PDF and split output into multiple HTML pages HTML HTML |
| [Convert-HTML-to-PDF-preserving-logical-structure-HTML-PDF](./Convert-HTML-to-PDF-preserving-logical-structure-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF preserving logical structure HTML PDF |
| [Convert-HTML-to-PDF-using-default-options-HTML-PDF](./Convert-HTML-to-PDF-using-default-options-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF using default options HTML PDF |
| [Convert-HTML-to-PDF-with-HTML-markup-generation-mode-set-to-...](./Convert-HTML-to-PDF-with-HTML-markup-generation-mode-set-to-BodyOnly-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with HTML markup generation mode set to BodyOnly HTML PDF |
| [Convert-HTML-to-PDF-with-PNG-background-for-images-HTML-PDF](./Convert-HTML-to-PDF-with-PNG-background-for-images-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with PNG background for images HTML PDF |
| [Convert-HTML-to-PDF-with-SVG-image-compression-HTML-PDF](./Convert-HTML-to-PDF-with-SVG-image-compression-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with SVG image compression HTML PDF |
| [Convert-HTML-to-PDF-with-custom-HtmlSaveOptions-HTML-PDF](./Convert-HTML-to-PDF-with-custom-HtmlSaveOptions-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with custom HtmlSaveOptions HTML PDF |
| [Convert-HTML-to-PDF-with-custom-external-resource-authentica...](./Convert-HTML-to-PDF-with-custom-external-resource-authentication-HTML-PDF.cs) |  | Convert HTML to PDF with custom external resource authentication HTML PDF |
| [Convert-HTML-to-PDF-with-custom-page-size-HTML-PDF](./Convert-HTML-to-PDF-with-custom-page-size-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with custom page size HTML PDF |
| [Convert-HTML-to-PDF-with-custom-resource-loader-HTML-PDF](./Convert-HTML-to-PDF-with-custom-resource-loader-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with custom resource loader HTML PDF |
| [Convert-HTML-to-PDF-with-embedded-fonts-disabled-HTML-PDF](./Convert-HTML-to-PDF-with-embedded-fonts-disabled-HTML-PDF.cs) |  | Convert HTML to PDF with embedded fonts disabled HTML PDF |
| [Convert-HTML-to-PDF-with-external-resources-credentials-HTML...](./Convert-HTML-to-PDF-with-external-resources-credentials-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with external resources credentials HTML PDF |
| [Convert-HTML-to-PDF-with-media-type-set-to-Print-HTML-PDF](./Convert-HTML-to-PDF-with-media-type-set-to-Print-HTML-PDF.cs) | `HtmlLoadOptions` | Convert HTML to PDF with media type set to Print HTML PDF |
| [Convert-HTML-to-PDF-with-single-page-rendering-HTML-PDF](./Convert-HTML-to-PDF-with-single-page-rendering-HTML-PDF.cs) |  | Convert HTML to PDF with single page rendering HTML PDF |
| [Convert-PDF-A-document-to-PDF-from-CGM-to-PDF](./Convert-PDF-A-document-to-PDF-from-CGM-to-PDF.cs) | `CgmLoadOptions` | Convert PDF A document to PDF from CGM to PDF |
| [Convert-PDF-A-document-to-PDF-from-EPUB-to-PDF](./Convert-PDF-A-document-to-PDF-from-EPUB-to-PDF.cs) | `EpubLoadOptions` | Convert PDF A document to PDF from EPUB to PDF |
| [Convert-PDF-A-document-to-PDF-from-HTML-to-PDF](./Convert-PDF-A-document-to-PDF-from-HTML-to-PDF.cs) | `HtmlLoadOptions` | Convert PDF A document to PDF from HTML to PDF |
| [Convert-PDF-A-document-to-PDF-from-MD-to-PDF](./Convert-PDF-A-document-to-PDF-from-MD-to-PDF.cs) | `MdLoadOptions` | Convert PDF A document to PDF from MD to PDF |
| [Convert-PDF-A-document-to-PDF-from-MHT-to-PDF](./Convert-PDF-A-document-to-PDF-from-MHT-to-PDF.cs) | `MhtLoadOptions` | Convert PDF A document to PDF from MHT to PDF |
| [Convert-PDF-A-document-to-PDF-from-OFD-to-PDF](./Convert-PDF-A-document-to-PDF-from-OFD-to-PDF.cs) | `OfdLoadOptions` | Convert PDF A document to PDF from OFD to PDF |
| [Convert-PDF-A-document-to-PDF-from-PCL-to-PDF](./Convert-PDF-A-document-to-PDF-from-PCL-to-PDF.cs) | `PclLoadOptions` | Convert PDF A document to PDF from PCL to PDF |
| ... | | *and 200 more files* |

## Category Statistics
- Total examples: 230

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

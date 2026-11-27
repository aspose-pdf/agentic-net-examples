---
name: facades-conversion
description: C# examples for facades-conversion using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-conversion

> **Facades conversion** in PDF using C# / .NET -- **139** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-conversion** category.
This folder contains standalone C# examples for facades-conversion operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-conversion**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (118/139 files) ← category-specific
- `using Aspose.Pdf.Devices;` (97/139 files) ← category-specific
- `using Aspose.Pdf;` (65/139 files)
- `using Aspose.Pdf.Text;` (27/139 files)
- `using Aspose.Pdf.Drawing;` (1/139 files)
- `using System;` (139/139 files)
- `using System.IO;` (139/139 files)
- `using System.Drawing.Imaging;` (40/139 files)
- `using System.Drawing;` (6/139 files)
- `using System.Linq;` (1/139 files)

## Common Code Pattern

Most files in this category use `PdfConverter` from `Aspose.Pdf.Facades`:

```csharp
PdfConverter tool = new PdfConverter();
tool.BindPdf("input.pdf");
// ... PdfConverter operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [batch-convert-fdf-to-xml](./batch-convert-fdf-to-xml.cs) | Batch Convert FDF Files to XML | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to process all FDF files in a folder, converting each to an XML representation u... |
| [batch-convert-xml-to-fdf](./batch-convert-xml-to-fdf.cs) | Batch Convert XML to FDF Using FormDataConverter | `FormDataConverter`, `ConvertXmlToFdf` | Demonstrates how to iterate over XML files in a folder and convert each one to an FDF file with t... |
| [batch-convert-xml-to-fdf__v2](./batch-convert-xml-to-fdf__v2.cs) | Batch Convert XML Form Data to FDF | `FormDataConverter`, `ConvertXmlToFdf` | Shows how to iterate over XML files in a folder and convert each to an FDF file using Aspose.Pdf'... |
| [convert-cropped-pdf-page-to-gif](./convert-cropped-pdf-page-to-gif.cs) | Convert Cropped PDF Page to GIF | `Document`, `Rectangle`, `GifDevice` | Loads a PDF, applies a CropBox to page 5, and converts the cropped page into a GIF image using As... |
| [convert-dated-fdf-to-xml](./convert-dated-fdf-to-xml.cs) | Convert Dated FDF Files to XML with FormDataConverter | `FormDataConverter`, `ConvertFdfToXml` | The example scans an input folder for FDF files that contain a date stamp in their names, then us... |
| [convert-fdf-to-xml-and-archive](./convert-fdf-to-xml-and-archive.cs) | Convert FDF to XML and Archive the Result | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates converting an FDF file to XML using Aspose.Pdf's FormDataConverter and then moving t... |
| [convert-fdf-to-xml-preserve-hierarchy](./convert-fdf-to-xml-preserve-hierarchy.cs) | Convert FDF Files to XML While Preserving Folder Hierarchy | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates recursively locating FDF files, converting each to XML with FormDataConverter, and w... |
| [convert-fdf-to-xml-with-creation-date](./convert-fdf-to-xml-with-creation-date.cs) | Convert FDF to XML and Rename File with Creation Date | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates using Aspose.Pdf's FormDataConverter to transform an FDF file into XML and then rena... |
| [convert-fdf-to-xml-with-formdataconverter](./convert-fdf-to-xml-with-formdataconverter.cs) | Convert FDF to XML with FormDataConverter | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to read an FDF file and convert its contents to XML using Aspose.Pdf's FormDataC... |
| [convert-fdf-to-xml-with-modified-filename](./convert-fdf-to-xml-with-modified-filename.cs) | Convert FDF to XML with Modified Filename | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates using Aspose.Pdf's FormDataConverter to convert an FDF file to XML and save the outp... |
| [convert-fdf-to-xml](./convert-fdf-to-xml.cs) | Convert FDF File to XML | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to convert a single FDF file to XML using Aspose.Pdf's FormDataConverter and sav... |
| [convert-fdf-to-xml__v2](./convert-fdf-to-xml__v2.cs) | Convert FDF File to XML using FormDataConverter | `FormDataConverter`, `ConvertFdfToXml` | Shows how to convert an FDF form data file into an XML representation by using Aspose.Pdf.Facades... |
| [convert-fdf-to-xml__v3](./convert-fdf-to-xml__v3.cs) | Convert FDF to XML using FormDataConverter | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to use Aspose.Pdf.Facades.FormDataConverter to transform an FDF file into an XML... |
| [convert-fdf-to-xml__v4](./convert-fdf-to-xml__v4.cs) | Convert FDF Form Data to XML | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to convert an FDF file containing form data into an XML file using Aspose.Pdf's ... |
| [convert-fdf-with-spaces-to-xml](./convert-fdf-with-spaces-to-xml.cs) | Convert FDF File with Spaces to XML | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to use Aspose.Pdf's FormDataConverter to convert an FDF file whose name contains... |
| [convert-first-five-pdf-pages-to-jpeg](./convert-first-five-pdf-pages-to-jpeg.cs) | Convert First Five PDF Pages to JPEG Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to load a PDF with PdfConverter, limit conversion to pages 1‑5, set a custom resolution... |
| [convert-first-four-pdf-pages-to-tiff](./convert-first-four-pdf-pages-to-tiff.cs) | Convert First Four PDF Pages to TIFF Images | `PdfConverter`, `BindPdf`, `StartPage` | Shows how to load a PDF with PdfConverter, limit the conversion to pages 1‑4, and save each of th... |
| [convert-multiple-fdf-to-xml](./convert-multiple-fdf-to-xml.cs) | Convert Multiple FDF Files to XML Using FormDataConverter | `FormDataConverter`, `ConvertFdfToXml` | Demonstrates how to batch‑convert all FDF files in a directory to XML files with matching filenam... |
| [convert-pdf-page-to-bmp-cropbox-72dpi](./convert-pdf-page-to-bmp-cropbox-72dpi.cs) | Convert PDF Page to BMP Using CropBox and 72 DPI | `Document`, `BmpDevice`, `Resolution` | The example loads a PDF, sets a 72 DPI resolution, applies the CropBox coordinate type, and conve... |
| [convert-pdf-page-to-bmp-with-font-substitution](./convert-pdf-page-to-bmp-with-font-substitution.cs) | Convert PDF Page to BMP with Font Substitution | `Document`, `FontRepository`, `SimpleFontSubstitution` | Demonstrates loading a PDF, configuring fallback font substitutions, and rendering a specific pag... |
| [convert-pdf-page-to-bmp-with-font-substitution__v2](./convert-pdf-page-to-bmp-with-font-substitution__v2.cs) | Convert PDF Page to BMP with Font Substitution and 300 DPI | `Document`, `Substitutions`, `SimpleFontSubstitution` | Shows how to load a PDF, set up font substitution, configure a 300 DPI resolution via PdfConverte... |
| [convert-pdf-page-to-bmp-with-forms](./convert-pdf-page-to-bmp-with-forms.cs) | Convert PDF Page to BMP with Form Fields Rendered | `Document`, `PdfConverter`, `BindPdf` | Demonstrates loading a PDF, binding it to PdfConverter, setting Production form presentation mode... |
| [convert-pdf-page-to-gif-low-resolution](./convert-pdf-page-to-gif-low-resolution.cs) | Convert PDF Page to GIF with Low Resolution and Font Substit... | `Document`, `PdfConverter`, `GifDevice` | Loads a PDF, substitutes missing fonts, sets a low 72 DPI resolution using PdfConverter, and conv... |
| [convert-pdf-page-to-gif-with-font-substitution](./convert-pdf-page-to-gif-with-font-substitution.cs) | Convert PDF Page to GIF with Font Substitution | `Document`, `Add`, `BindPdf` | Demonstrates loading a PDF, applying a font substitution, binding it with PdfConverter, and rende... |
| [convert-pdf-page-to-gif](./convert-pdf-page-to-gif.cs) | Convert Specific PDF Page to GIF using PdfConverter | `PdfConverter`, `BindPdf`, `DoConvert` | Shows how to load a PDF with PdfConverter, limit conversion to page 2, and save that page as a GI... |
| [convert-pdf-page-to-high-res-gif](./convert-pdf-page-to-high-res-gif.cs) | Convert PDF Page to High-Resolution GIF with Custom Fonts | `Document`, `Substitutions`, `SimpleFontSubstitution` | Loads a PDF, registers custom TrueType/OpenType fonts, renders page 7 at 300 DPI using a PNG devi... |
| [convert-pdf-page-to-tiff](./convert-pdf-page-to-tiff.cs) | Convert Specific PDF Page to TIFF using PdfConverter | `Document`, `PdfConverter`, `BindPdf` | Demonstrates loading a PDF, binding it to PdfConverter, setting the page range to page 4, configu... |
| [convert-pdf-page3-to-bmp-cropbox](./convert-pdf-page3-to-bmp-cropbox.cs) | Convert PDF Page 3 to BMP Using CropBox Cropping | `PdfConverter`, `BindPdf`, `StartPage` | Demonstrates loading a PDF with PdfConverter, setting the start page to 3, using the CropBox coor... |
| [convert-pdf-page4-to-tiff](./convert-pdf-page4-to-tiff.cs) | Convert PDF Page 4 to TIFF Image | `Document`, `PdfConverter`, `BindPdf` | Loads a PDF document and uses Aspose.Pdf's PdfConverter to convert only page 4 into a high‑resolu... |
| [convert-pdf-page6-to-tiff-72dpi-cropbox](./convert-pdf-page6-to-tiff-72dpi-cropbox.cs) | Convert PDF Page 6 to TIFF with CropBox and 72 DPI | `Document`, `PdfConverter`, `BindPdf` | Demonstrates loading a PDF, setting a 72 DPI resolution, using the CropBox coordinate type, and c... |
| ... | | | *and 109 more files* |

## Category Statistics
- Total examples: 139

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
- Review code examples in this folder for facades-conversion patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-27 | Run: `20261127_100225_985f39`
<!-- AUTOGENERATED:END -->

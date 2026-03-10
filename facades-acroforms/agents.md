---
name: facades-acroforms
description: C# examples for facades-acroforms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-acroforms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-acroforms** category.
This folder contains standalone C# examples for facades-acroforms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-acroforms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/35 files) ← category-specific
- `using Aspose.Pdf;` (9/35 files)
- `using Aspose.Pdf.Forms;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (35/35 files)
- `using System.Collections.Generic;` (2/35 files)
- `using System.Drawing.Imaging;` (1/35 files)

## Common Code Pattern

Most files in this category use `Form` from `Aspose.Pdf.Facades`:

```csharp
Form tool = new Form();
tool.BindPdf("input.pdf");
// ... Form operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [Manipulate-AcroForm-fields-to-ExportFdf-fdfOutputStream-Save...](./Manipulate-AcroForm-fields-to-ExportFdf-fdfOutputStream-Save-PDF-document-using-PDF-input-and.cs) | `Form` | Manipulate AcroForm fields to ExportFdf fdfOutputStream Save PDF document usi... |
| [Manipulate-AcroForm-fields-to-ExportJson-jsonStream-Import-v...](./Manipulate-AcroForm-fields-to-ExportJson-jsonStream-Import-values-to-the-JSON-file-using-PD.cs) | `Rectangle` | Manipulate AcroForm fields to ExportJson jsonStream Import values to the JSON... |
| [Manipulate-AcroForm-fields-to-ExportXfdf-xfdfOutputStream-Sa...](./Manipulate-AcroForm-fields-to-ExportXfdf-xfdfOutputStream-Save-PDF-document-using-PDF-input-an.cs) | `Form` | Manipulate AcroForm fields to ExportXfdf xfdfOutputStream Save PDF document u... |
| [Manipulate-AcroForm-fields-to-ExportXml-xmlOutputStream-Impo...](./Manipulate-AcroForm-fields-to-ExportXml-xmlOutputStream-Import-data-from-FDF-into-a-PDF-fil.cs) | `Form` | Manipulate AcroForm fields to ExportXml xmlOutputStream Import data from FDF ... |
| [Manipulate-AcroForm-fields-to-Import-data-from-FDF-into-a-PD...](./Manipulate-AcroForm-fields-to-Import-data-from-FDF-into-a-PDF-file-export-data-to-FDF-from-a-PDF-fil.cs) | `Form` | Manipulate AcroForm fields to Import data from FDF into a PDF file export dat... |
| [Manipulate-AcroForm-fields-to-Import-data-from-XFDF-into-a-P...](./Manipulate-AcroForm-fields-to-Import-data-from-XFDF-into-a-PDF-file-export-data-to-XFDF-from-a-PDF-f.cs) | `Form` | Manipulate AcroForm fields to Import data from XFDF into a PDF file export da... |
| [Manipulate-AcroForm-fields-to-Import-from-JSON-file-using-Fi...](./Manipulate-AcroForm-fields-to-Import-from-JSON-file-using-FileStream-jsonStream-new-FileS-using.cs) | `Form` | Manipulate AcroForm fields to Import from JSON file using FileStream jsonStre... |
| [Manipulate-AcroForm-fields-to-ImportFdf-fdfInputStream-Save-...](./Manipulate-AcroForm-fields-to-ImportFdf-fdfInputStream-Save-PDF-document-using-PDF-input-and-P.cs) | `Form` | Manipulate AcroForm fields to ImportFdf fdfInputStream Save PDF document usin... |
| [Manipulate-AcroForm-fields-to-ImportXfdf-xfdfInputStream-Sav...](./Manipulate-AcroForm-fields-to-ImportXfdf-xfdfInputStream-Save-PDF-document-using-PDF-input-and.cs) | `Form` | Manipulate AcroForm fields to ImportXfdf xfdfInputStream Save PDF document us... |
| [Manipulate-AcroForm-fields-to-ImportXml-xmlInputStream-Save-...](./Manipulate-AcroForm-fields-to-ImportXml-xmlInputStream-Save-PDF-document-using-PDF-input-and-P.cs) | `Form` | Manipulate AcroForm fields to ImportXml xmlInputStream Save PDF document usin... |
| [Manipulate-AcroForm-fields-to-convert-PDF-document-using-PDF...](./Manipulate-AcroForm-fields-to-convert-PDF-document-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to convert PDF document using PDF input and PDF ou... |
| [Manipulate-AcroForm-fields-to-convert-data-from-one-format-t...](./Manipulate-AcroForm-fields-to-convert-data-from-one-format-to-another-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to convert data from one format to another using P... |
| [Manipulate-AcroForm-fields-to-convert-documents-using-PDF-in...](./Manipulate-AcroForm-fields-to-convert-documents-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to convert documents using PDF input and PDF output |
| [Manipulate-AcroForm-fields-to-explain-what-is-FDF-format-usi...](./Manipulate-AcroForm-fields-to-explain-what-is-FDF-format-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to explain what is FDF format using PDF input |
| [Manipulate-AcroForm-fields-to-explain-what-is-XFDF-format-us...](./Manipulate-AcroForm-fields-to-explain-what-is-XFDF-format-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to explain what is XFDF format using PDF input |
| [Manipulate-AcroForm-fields-to-explain-what-is-XML-format-usi...](./Manipulate-AcroForm-fields-to-explain-what-is-XML-format-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to explain what is XML format using PDF input |
| [Manipulate-AcroForm-fields-to-export-data-to-FDF-from-a-PDF-...](./Manipulate-AcroForm-fields-to-export-data-to-FDF-from-a-PDF-file-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to export data to FDF from a PDF file using PDF in... |
| [Manipulate-AcroForm-fields-to-export-data-to-XFDF-from-a-PDF...](./Manipulate-AcroForm-fields-to-export-data-to-XFDF-from-a-PDF-file-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to export data to XFDF from a PDF file using PDF i... |
| [Manipulate-AcroForm-fields-to-export-data-to-XML-from-a-PDF-...](./Manipulate-AcroForm-fields-to-export-data-to-XML-from-a-PDF-file-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to export data to XML from a PDF file using PDF in... |
| [Manipulate-AcroForm-fields-to-export-values-from-fields-to-t...](./Manipulate-AcroForm-fields-to-export-values-from-fields-to-the-JSON-file-using-PDF-input-and-PDF-out.cs) | `Form` | Manipulate AcroForm fields to export values from fields to the JSON file usin... |
| [Manipulate-AcroForm-fields-to-export-values-of-PDF-form-fiel...](./Manipulate-AcroForm-fields-to-export-values-of-PDF-form-fields-to-an-FDF-or-XFDF-file-using-PDF-inpu.cs) | `Form` | Manipulate AcroForm fields to export values of PDF form fields to an FDF or X... |
| [Manipulate-AcroForm-fields-to-extract-images-and-text-from-d...](./Manipulate-AcroForm-fields-to-extract-images-and-text-from-document-using-PDF-input.cs) | `PdfExtractor` | Manipulate AcroForm fields to extract images and text from document using PDF... |
| [Manipulate-AcroForm-fields-to-get-button-option-value-using-...](./Manipulate-AcroForm-fields-to-get-button-option-value-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to get button option value using PDF input |
| [Manipulate-AcroForm-fields-to-get-button-option-values-from-...](./Manipulate-AcroForm-fields-to-get-button-option-values-from-an-existing-PDF-file-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to get button option values from an existing PDF f... |
| [Manipulate-AcroForm-fields-to-get-current-button-option-valu...](./Manipulate-AcroForm-fields-to-get-current-button-option-value-from-an-existing-PDF-file-using-PDF-in.cs) | `Form` | Manipulate AcroForm fields to get current button option value from an existin... |
| [Manipulate-AcroForm-fields-to-identify-form-fields-names-usi...](./Manipulate-AcroForm-fields-to-identify-form-fields-names-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to identify form fields names using PDF input |
| [Manipulate-AcroForm-fields-to-implementation-details-using-P...](./Manipulate-AcroForm-fields-to-implementation-details-using-PDF-input.cs) | `FormEditor` | Manipulate AcroForm fields to implementation details using PDF input |
| [Manipulate-AcroForm-fields-to-import-and-export-data-using-P...](./Manipulate-AcroForm-fields-to-import-and-export-data-using-PDF-input-and-PDF-output.cs) | `Form` | Manipulate AcroForm fields to import and export data using PDF input and PDF ... |
| [Manipulate-AcroForm-fields-to-import-data-from-FDF-into-a-PD...](./Manipulate-AcroForm-fields-to-import-data-from-FDF-into-a-PDF-file-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to import data from FDF into a PDF file using PDF ... |
| [Manipulate-AcroForm-fields-to-import-data-from-XFDF-into-a-P...](./Manipulate-AcroForm-fields-to-import-data-from-XFDF-into-a-PDF-file-using-PDF-input.cs) | `Form` | Manipulate AcroForm fields to import data from XFDF into a PDF file using PDF... |
| ... | | *and 5 more files* |

## Category Statistics
- Total examples: 35

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-acroforms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_191450_ec5382`
<!-- AUTOGENERATED:END -->

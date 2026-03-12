---
name: facades-convert-documents
description: C# examples for facades-convert-documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-convert-documents

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-convert-documents** category.
This folder contains standalone C# examples for facades-convert-documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-convert-documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (27/28 files) ← category-specific
- `using Aspose.Pdf;` (16/28 files) ← category-specific
- `using Aspose.Pdf.Devices;` (5/28 files)
- `using Aspose.Pdf.Text;` (2/28 files)
- `using Aspose.Pdf.Drawing;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (28/28 files)
- `using System.Drawing.Imaging;` (4/28 files)
- `using System.Collections.Generic;` (2/28 files)
- `using System.Text;` (1/28 files)

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
| [combine-jpeg-png-bmp-and-gif-images-into-a-single-pdf-docume...](./combine-jpeg-png-bmp-and-gif-images-into-a-single-pdf-document-while-maintaining-image-fidelity.cs) | `PdfFileMend` | Combine jpeg png bmp and gif images into a single pdf document while maintain... |
| [combine-multiple-image-files-into-a-single-composite-image-w...](./combine-multiple-image-files-into-a-single-composite-image-while-maintaining-original-resolution-and.cs) |  | Combine multiple image files into a single composite image while maintaining ... |
| [combine-multiple-image-files-into-a-single-pdf-document-pres...](./combine-multiple-image-files-into-a-single-pdf-document-preserving-their-order-and-original-resolut.cs) | `PdfFileMend` | Combine multiple image files into a single pdf document preserving their orde... |
| [combine-several-image-files-into-a-single-pdf-document-prese...](./combine-several-image-files-into-a-single-pdf-document-preserving-image-order-and-quality.cs) | `PdfFileMend` | Combine several image files into a single pdf document preserving image order... |
| [configure-conversion-parameters-such-as-page-range-selection...](./configure-conversion-parameters-such-as-page-range-selection-and-output-image-quality-before-proce.cs) | `PdfConverter` | Configure conversion parameters such as page range selection and output image... |
| [convert-a-pdf-document-to-docx-pptx-html-or-image-format-whi...](./convert-a-pdf-document-to-docx-pptx-html-or-image-format-while-maintaining-content-fidelity.cs) | `DocSaveOptions`, `PptxSaveOptions`, `PdfConverter` | Convert a pdf document to docx pptx html or image format while maintaining co... |
| [convert-an-fdf-document-to-xml-format-using-the-.net-pdf-pro...](./convert-an-fdf-document-to-xml-format-using-the-.net-pdf-processing-library-api.cs) |  | Convert an fdf document to xml format using the .net pdf processing library api |
| [convert-documents-between-supported-formats-to-facilitate-se...](./convert-documents-between-supported-formats-to-facilitate-seamless-image-merging-within-the-resultin.cs) | `PdfFileEditor` | Convert documents between supported formats to facilitate seamless image merg... |
| [convert-pdf-files-to-docx-pptx-html-or-image-formats-while-m...](./convert-pdf-files-to-docx-pptx-html-or-image-formats-while-maintaining-content-fidelity.cs) | `PptxSaveOptions`, `PdfConverter` | Convert pdf files to docx pptx html or image formats while maintaining conten... |
| [convert-xml-formatted-data-into-an-fdf-file-for-pdf-form-fil...](./convert-xml-formatted-data-into-an-fdf-file-for-pdf-form-filling-preserving-field-mappings-and-data.cs) |  | Convert xml formatted data into an fdf file for pdf form filling preserving f... |
| [convert-xml-formatted-data-into-an-fdf-file-using-the-.net-p...](./convert-xml-formatted-data-into-an-fdf-file-using-the-.net-pdf-processing-library-preserving-field.cs) |  | Convert xml formatted data into an fdf file using the .net pdf processing lib... |
| [implement-a-facade-that-transforms-fdf-files-to-xml-format-a...](./implement-a-facade-that-transforms-fdf-files-to-xml-format-adhering-to-the-provided-conversion-exam.cs) |  | Implement a facade that transforms fdf files to xml format adhering to the pr... |
| [implement-a-facade-to-convert-documents-utilizing-the-provid...](./implement-a-facade-to-convert-documents-utilizing-the-provided-example-to-combine-images-into-a-pdf.cs) | `PdfFileMend` | Implement a facade to convert documents utilizing the provided example to com... |
| [implement-facade-methods-to-convert-source-documents-into-an...](./implement-facade-methods-to-convert-source-documents-into-any-supported-output-format-such-as-pdf-d.cs) | `HtmlSaveOptions`, `SvgSaveOptions`, `XmlSaveOptions` | Implement facade methods to convert source documents into any supported outpu... |
| [implement-facades-to-convert-document-data-from-fdf-to-xml-u...](./implement-facades-to-convert-document-data-from-fdf-to-xml-using-the-conversion-guide.cs) |  | Implement facades to convert document data from fdf to xml using the conversi... |
| [implement-facades-to-convert-supported-document-formats-into...](./implement-facades-to-convert-supported-document-formats-into-xml-enabling-subsequent-fdf-file-gener.cs) | `Form` | Implement facades to convert supported document formats into xml enabling sub... |
| [insert-each-image-onto-a-separate-page-within-the-pdf-docume...](./insert-each-image-onto-a-separate-page-within-the-pdf-document-creating-a-new-page-for-every-image.cs) | `PdfFileMend` | Insert each image onto a separate page within the pdf document creating a new... |
| [instantiate-an-fdfdocument-object-and-populate-it-with-data-...](./instantiate-an-fdfdocument-object-and-populate-it-with-data-extracted-from-an-xml-file.cs) |  | Instantiate an fdfdocument object and populate it with data extracted from an... |
| [persist-the-generated-fdf-document-to-storage-ensuring-prope...](./persist-the-generated-fdf-document-to-storage-ensuring-proper-file-formatting-and-data-integrity.cs) | `Form` | Persist the generated fdf document to storage ensuring proper file formatting... |
| [persist-the-generated-xml-output-to-a-file-on-disk-using-app...](./persist-the-generated-xml-output-to-a-file-on-disk-using-appropriate-i-o-handling-and-encoding-setti.cs) | `Form` | Persist the generated xml output to a file on disk using appropriate i o hand... |
| [transform-a-pdf-document-into-a-specified-output-format-whil...](./transform-a-pdf-document-into-a-specified-output-format-while-preserving-its-content-layout-and-me.cs) | `PptxSaveOptions`, `XmlSaveOptions`, `SvgSaveOptions` | Transform a pdf document into a specified output format while preserving its ... |
| [transform-an-fdf-file-into-an-xml-document-preserving-all-fo...](./transform-an-fdf-file-into-an-xml-document-preserving-all-form-field-data-and-structure.cs) |  | Transform an fdf file into an xml document preserving all form field data and... |
| [use-the-conversion-facade-to-transform-documents-to-target-f...](./use-the-conversion-facade-to-transform-documents-to-target-formats-according-to-the-provided-example.cs) | `HtmlSaveOptions`, `XmlSaveOptions`, `SvgSaveOptions` | Use the conversion facade to transform documents to target formats according ... |
| [use-the-facade-api-to-convert-documents-specifying-conversio...](./use-the-facade-api-to-convert-documents-specifying-conversion-options-such-as-target-format-qualit.cs) | `PdfConverter` | Use the facade api to convert documents specifying conversion options such as... |
| [use-the-facade-layer-to-convert-documents-replicating-the-xm...](./use-the-facade-layer-to-convert-documents-replicating-the-xml-to-fdf-conversion-example-workflow-an.cs) |  | Use the facade layer to convert documents replicating the xml to fdf conversi... |
| [use-the-facades-api-to-convert-documents-to-fdf-format-by-fo...](./use-the-facades-api-to-convert-documents-to-fdf-format-by-following-the-xml-to-fdf-guide.cs) |  | Use the facades api to convert documents to fdf format by following the xml t... |
| [utilize-facades-to-efficiently-transform-supported-document-...](./utilize-facades-to-efficiently-transform-supported-document-files-into-xml-by-processing-fdf-data.cs) | `Form` | Utilize facades to efficiently transform supported document files into xml by... |
| [write-the-resulting-pdf-document-to-a-file-stream-ensuring-i...](./write-the-resulting-pdf-document-to-a-file-stream-ensuring-it-is-saved-correctly-on-disk.cs) |  | Write the resulting pdf document to a file stream ensuring it is saved correc... |

## Category Statistics
- Total examples: 28

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
- Review code examples in this folder for facades-convert-documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

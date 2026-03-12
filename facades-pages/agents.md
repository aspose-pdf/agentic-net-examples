---
name: facades-pages
description: C# examples for facades-pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-pages

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-pages** category.
This folder contains standalone C# examples for facades-pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/43 files) ← category-specific
- `using Aspose.Pdf;` (39/43 files) ← category-specific
- `using Aspose.Pdf.Devices;` (4/43 files)
- `using Aspose.Pdf.Text;` (3/43 files)
- `using Aspose.Pdf.Vector;` (1/43 files)
- `using System;` (43/43 files)
- `using System.IO;` (42/43 files)
- `using System.Collections.Generic;` (3/43 files)
- `using System.Drawing;` (1/43 files)
- `using System.Text;` (1/43 files)

## Common Code Pattern

Most files in this category use `PdfPageEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfPageEditor tool = new PdfPageEditor();
tool.BindPdf("input.pdf");
// ... PdfPageEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [adjust-pdf-page-dimensions-before-converting-the-document-to...](./adjust-pdf-page-dimensions-before-converting-the-document-to-epub-to-ensure-proper-layout.cs) | `PdfPageEditor`, `EpubSaveOptions` | Adjust pdf page dimensions before converting the document to epub to ensure p... |
| [apply-a-page-transition-effect-to-an-mht-document-using-the-...](./apply-a-page-transition-effect-to-an-mht-document-using-the-facades-api-for-page-manipulation.cs) | `MhtLoadOptions`, `PdfPageEditor` | Apply a page transition effect to an mht document using the facades api for p... |
| [apply-page-rotation-using-the-rotation-property-in-the-facad...](./apply-page-rotation-using-the-rotation-property-in-the-facades-api-to-modify-pdf-page-orientation.cs) | `PdfPageEditor` | Apply page rotation using the rotation property in the facades api to modify ... |
| [convert-a-pdf-document-from-md-to-xlsm-by-manipulating-pages...](./convert-a-pdf-document-from-md-to-xlsm-by-manipulating-pages-using-facade-apis.cs) | `PdfFileEditor`, `ExcelSaveOptions` | Convert a pdf document from md to xlsm by manipulating pages using facade apis |
| [convert-a-pdf-document-stored-in-ofd-format-to-a-pptx-file-v...](./convert-a-pdf-document-stored-in-ofd-format-to-a-pptx-file-via-facade-page-manipulation.cs) | `OfdLoadOptions`, `PdfPageEditor`, `PptxSaveOptions` | Convert a pdf document stored in ofd format to a pptx file via facade page ma... |
| [convert-a-pdf-generated-from-xslfo-to-an-xlsx-workbook-by-ma...](./convert-a-pdf-generated-from-xslfo-to-an-xlsx-workbook-by-manipulating-its-pages-using-the-facades-a.cs) | `XslFoLoadOptions`, `PdfPageEditor`, `ExcelSaveOptions` | Convert a pdf generated from xslfo to an xlsx workbook by manipulating its pa... |
| [convert-cgm-pages-to-docx-format-by-manipulating-page-struct...](./convert-cgm-pages-to-docx-format-by-manipulating-page-structures-through-the-facade-api.cs) | `PdfPageEditor` | Convert cgm pages to docx format by manipulating page structures through the ... |
| [convert-each-page-of-a-pdf-generated-from-pcl-into-emf-forma...](./convert-each-page-of-a-pdf-generated-from-pcl-into-emf-format-using-page-manipulation-facades.cs) | `PclLoadOptions`, `EmfDevice` | Convert each page of a pdf generated from pcl into emf format using page mani... |
| [convert-svg-graphics-within-a-pdf-document-to-bmp-format-by-...](./convert-svg-graphics-within-a-pdf-document-to-bmp-format-by-manipulating-pages-via-the-provided-faca.cs) | `PdfConverter`, `SvgLoadOptions`, `BmpDevice` | Convert svg graphics within a pdf document to bmp format by manipulating page... |
| [implement-a-facade-to-programmatically-modify-epub-pages-ena...](./implement-a-facade-to-programmatically-modify-epub-pages-enabling-dynamic-insertion-of-explanatory.cs) | `EpubLoadOptions`, `PdfContentEditor` | Implement a facade to programmatically modify epub pages enabling dynamic ins... |
| [implement-fa-ade-methods-to-manipulate-document-pages-when-c...](./implement-fa-ade-methods-to-manipulate-document-pages-when-converting-to-mht-providing-fine-grained.cs) | `PdfPageEditor` | Implement fa ade methods to manipulate document pages when converting to mht ... |
| [implement-facade-classes-to-manipulate-document-pages-and-ex...](./implement-facade-classes-to-manipulate-document-pages-and-expose-tex-specific-implementation-details.cs) | `PdfPageEditor`, `TeXSaveOptions` | Implement facade classes to manipulate document pages and expose tex specific... |
| [implement-page-rotation-handling-within-cgm-files-using-the-...](./implement-page-rotation-handling-within-cgm-files-using-the-facade-api-to-manipulate-pages.cs) | `CgmLoadOptions`, `PdfPageEditor` | Implement page rotation handling within cgm files using the facade api to man... |
| [implement-page-rotation-manipulation-in-pdf-files-using-the-...](./implement-page-rotation-manipulation-in-pdf-files-using-the-fa-ade-api-to-adjust-page-orientation.cs) | `PdfPageEditor` | Implement page rotation manipulation in pdf files using the fa ade api to adj... |
| [leverage-page-manipulation-facades-to-programmatically-trans...](./leverage-page-manipulation-facades-to-programmatically-transform-tex-source-files-into-mobixml-outpu.cs) | `TeXLoadOptions`, `PdfFileEditor` | Leverage page manipulation facades to programmatically transform tex source f... |
| [manipulate-document-pages-to-convert-an-epub-file-into-djvu-...](./manipulate-document-pages-to-convert-an-epub-file-into-djvu-format-while-preserving-page-integrity.cs) | `EpubLoadOptions` | Manipulate document pages to convert an epub file into djvu format while pres... |
| [resize-pages-within-a-pdf-document-to-specified-dimensions-u...](./resize-pages-within-a-pdf-document-to-specified-dimensions-using-the-provided-page-manipulation-api.cs) |  | Resize pages within a pdf document to specified dimensions using the provided... |
| [retrieve-pdf-page-properties-from-an-existing-pdf-document-e...](./retrieve-pdf-page-properties-from-an-existing-pdf-document-embedded-in-an-epub-by-manipulating-its-p.cs) | `EpubLoadOptions`, `PdfFileInfo` | Retrieve pdf page properties from an existing pdf document embedded in an epu... |
| [retrieve-pdf-page-properties-from-an-existing-pdf-file-using...](./retrieve-pdf-page-properties-from-an-existing-pdf-file-using-html-based-page-manipulation-facades.cs) | `PdfFileInfo` | Retrieve pdf page properties from an existing pdf file using html based page ... |
| [rotate-a-specific-page-in-an-mht-document-using-the-facades-...](./rotate-a-specific-page-in-an-mht-document-using-the-facades-api-s-page-rotation-property.cs) | `MhtLoadOptions`, `PdfPageEditor` | Rotate a specific page in an mht document using the facades api s page rotati... |
| [rotate-a-specific-page-within-an-epub-document-by-applying-p...](./rotate-a-specific-page-within-an-epub-document-by-applying-pagerotations-through-the-facade-api.cs) | `EpubLoadOptions`, `PdfPageEditor` | Rotate a specific page within an epub document by applying pagerotations thro... |
| [rotate-a-specific-page-within-an-html-document-by-applying-p...](./rotate-a-specific-page-within-an-html-document-by-applying-pagerotations-through-the-page-manipulati.cs) | `HtmlLoadOptions`, `PdfPageEditor` | Rotate a specific page within an html document by applying pagerotations thro... |
| [transform-pdf-pages-by-extracting-embedded-svg-elements-and-...](./transform-pdf-pages-by-extracting-embedded-svg-elements-and-generating-equivalent-markdown-md-outp.cs) | `StringBuilder`, `SvgExtractor` | Transform pdf pages by extracting embedded svg elements and generating equiva... |
| [use-facade-interfaces-to-modify-document-pages-and-assign-tr...](./use-facade-interfaces-to-modify-document-pages-and-assign-transition-effects-to-tex-generated-conten.cs) | `TeXLoadOptions`, `PdfPageEditor` | Use facade interfaces to modify document pages and assign transition effects ... |
| [use-facades-to-adjust-pdf-page-dimensions-during-cgm-process...](./use-facades-to-adjust-pdf-page-dimensions-during-cgm-processing-ensuring-correct-page-size-transfor.cs) | `CgmLoadOptions`, `PdfPageEditor` | Use facades to adjust pdf page dimensions during cgm processing ensuring corr... |
| [use-facades-to-manipulate-pages-and-convert-data-from-epub-t...](./use-facades-to-manipulate-pages-and-convert-data-from-epub-to-epub-maintaining-original-pagination.cs) | `EpubLoadOptions`, `PdfPageEditor` | Use facades to manipulate pages and convert data from epub to epub maintainin... |
| [use-facades-to-manipulate-pdf-pages-and-convert-an-xml-gener...](./use-facades-to-manipulate-pdf-pages-and-convert-an-xml-generated-document-into-a-tiff-image.cs) | `PdfConverter` | Use facades to manipulate pdf pages and convert an xml generated document int... |
| [use-facades-to-manipulate-pdf-pages-and-convert-the-document...](./use-facades-to-manipulate-pdf-pages-and-convert-the-document-from-ps-format-to-png-images.cs) | `PsLoadOptions`, `PdfConverter` | Use facades to manipulate pdf pages and convert the document from ps format t... |
| [use-page-manipulation-facades-to-transform-a-pdf-generated-f...](./use-page-manipulation-facades-to-transform-a-pdf-generated-from-mht-into-high-resolution-jpeg-images.cs) | `HtmlLoadOptions`, `PdfConverter` | Use page manipulation facades to transform a pdf generated from mht into high... |
| [use-the-facade-api-to-manipulate-pages-and-convert-a-pdf-doc...](./use-the-facade-api-to-manipulate-pages-and-convert-a-pdf-document-from-ps-to-xslfo.cs) | `PsLoadOptions`, `PdfPageEditor` | Use the facade api to manipulate pages and convert a pdf document from ps to ... |
| ... | | *and 13 more files* |

## Category Statistics
- Total examples: 43

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.PdfFileEditor`
- `Aspose.Pdf.Facades.PdfFileEditor.Delete`
- `Aspose.Pdf.Facades.PdfFileEditor.Extract`
- `Aspose.Pdf.Facades.PdfFileEditor.SplitToEnd`

### Rules
- Instantiate Aspose.Pdf.Facades.PdfFileEditor and call Delete({input_pdf}, {int[]} pagesToDelete, {output_pdf}) to remove the specified pages.
- The page numbers in the array are 1‑based indices representing the pages to be removed.
- Use PdfFileEditor.Delete({input_pdf_stream}, {int[] pagesToDelete}, {output_pdf_stream}) to remove the specified pages (1‑based indices) from a PDF without loading it into a Document object.
- When working with streams, open the source PDF with FileMode.Open and create the destination PDF with FileMode.Create, then pass the streams to PdfFileEditor.Delete.
- Use PdfFileEditor.Extract({input_pdf}, new int[] {{int}, {int}, ...}, {output_pdf}) to create a new PDF containing only the listed pages.

### Warnings
- The example does not explicitly dispose the FileStream objects; callers should ensure streams are closed or wrapped in using statements.
- The output file will be created or overwritten; ensure the path is correct.
- The example assumes the input PDF exists at the specified location.
- Page numbers must be within the bounds of the source document; otherwise an exception will be thrown.
- Insert overwrites the output file if it already exists.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

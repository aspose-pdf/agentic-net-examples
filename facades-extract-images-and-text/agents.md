---
name: facades-extract-images-and-text
description: C# examples for facades-extract-images-and-text using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-extract-images-and-text

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-extract-images-and-text** category.
This folder contains standalone C# examples for facades-extract-images-and-text operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-extract-images-and-text**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (12/12 files) ← category-specific
- `using Aspose.Pdf;` (1/12 files)
- `using System;` (12/12 files)
- `using System.IO;` (12/12 files)
- `using System.Drawing.Imaging;` (2/12 files)
- `using System.Collections.Generic;` (1/12 files)
- `using System.Text;` (1/12 files)
- `using System.Threading.Tasks;` (1/12 files)

## Common Code Pattern

Most files in this category use `PdfExtractor` from `Aspose.Pdf.Facades`:

```csharp
PdfExtractor tool = new PdfExtractor();
tool.BindPdf("input.pdf");
// ... PdfExtractor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [determine-if-a-pdf-document-includes-any-images-or-textual-c...](./determine-if-a-pdf-document-includes-any-images-or-textual-content-using-the-facade-api.cs) | `PdfExtractor` | Determine if a pdf document includes any images or textual content using the ... |
| [extract-attachments-from-a-pdf-document-using-the-facade-api...](./extract-attachments-from-a-pdf-document-using-the-facade-api-for-image-and-text-extraction.cs) | `PdfExtractor` | Extract attachments from a pdf document using the facade api for image and te... |
| [extract-images-and-text-from-a-pdf-utilizing-the-facade-api-...](./extract-images-and-text-from-a-pdf-utilizing-the-facade-api-with-the-extractimagemode-setting.cs) | `PdfExtractor` | Extract images and text from a pdf utilizing the facade api with the extracti... |
| [extract-images-and-text-from-an-entire-pdf-into-streams-util...](./extract-images-and-text-from-an-entire-pdf-into-streams-utilizing-the-facade-abstraction.cs) | `PdfExtractor` | Extract images and text from an entire pdf into streams utilizing the facade ... |
| [extract-images-and-text-from-pdf-files-using-the-facade-api-...](./extract-images-and-text-from-pdf-files-using-the-facade-api-employing-the-text-extraction-mode-as-d.cs) | `PdfExtractor` | Extract images and text from pdf files using the facade api employing the tex... |
| [extract-images-from-a-defined-page-range-of-a-pdf-using-the-...](./extract-images-from-a-defined-page-range-of-a-pdf-using-the-facades-library.cs) | `PdfExtractor` | Extract images from a defined page range of a pdf using the facades library |
| [extract-text-content-from-a-pdf-document-using-the-facade-ap...](./extract-text-content-from-a-pdf-document-using-the-facade-api-while-preserving-layout-and-formatting.cs) | `PdfExtractor` | Extract text content from a pdf document using the facade api while preservin... |
| [use-the-facade-api-to-extract-both-images-and-text-from-a-pd...](./use-the-facade-api-to-extract-both-images-and-text-from-a-pdf-document-while-handling-background-pro.cs) | `PdfExtractor` | Use the facade api to extract both images and text from a pdf document while ... |
| [utilize-the-facades-api-in-image-extraction-mode-to-retrieve...](./utilize-the-facades-api-in-image-extraction-mode-to-retrieve-images-and-associated-text-from-pdf-fil.cs) | `PdfExtractor` | Utilize the facades api in image extraction mode to retrieve images and assoc... |
| [utilize-the-facades-api-to-extract-all-images-from-a-pdf-doc...](./utilize-the-facades-api-to-extract-all-images-from-a-pdf-document-and-save-each-to-separate-files.cs) | `PdfExtractor` | Utilize the facades api to extract all images from a pdf document and save ea... |
| [utilize-the-facades-api-to-extract-all-images-from-a-specifi...](./utilize-the-facades-api-to-extract-all-images-from-a-specified-page-of-a-pdf-document.cs) | `PdfExtractor` | Utilize the facades api to extract all images from a specified page of a pdf ... |
| [utilize-the-pdfextractor-fa-ade-to-retrieve-images-from-a-pd...](./utilize-the-pdfextractor-fa-ade-to-retrieve-images-from-a-pdf-document-while-maintaining-original-qu.cs) | `PdfExtractor` | Utilize the pdfextractor fa ade to retrieve images from a pdf document while ... |

## Category Statistics
- Total examples: 12

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.ExtractImageMode`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfConverter`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfExtractor.BindPdf`
- `Aspose.Pdf.Facades.PdfExtractor.ExtractText`
- `Aspose.Pdf.Facades.PdfExtractor.GetNextPageText`
- `Aspose.Pdf.Facades.PdfExtractor.HasNextPageText`
- `Aspose.Pdf.Facades.PdfFileEditor`
- `Aspose.Pdf.Facades.PdfFileEditor.Extract`

### Rules
- BindPdf({input_pdf}) must be called on a PdfContentEditor instance before any editing methods such as ReplaceText.
- ReplaceText({text_fragment}, {page}, {text_fragment}) replaces all occurrences of the first text fragment on the specified 1‑based page with the second text fragment.
- Save({output_pdf}) persists the edited PDF; it should be invoked after all edit operations are completed.
- Use PdfFileEditor.Extract({input_pdf}, new int[] {{int}, {int}, ...}, {output_pdf}) to create a new PDF containing only the listed pages.
- Page numbers supplied in the int array are 1‑based and must exist in {input_pdf}.

### Warnings
- Page numbers are 1‑based; passing 0 will cause an error.
- ReplaceText operates only on the specified page and replaces every matching occurrence on that page.
- The output file will be created or overwritten; ensure the path is correct.
- The example assumes the input PDF exists at the specified location.
- The example does not explicitly dispose the FileStream objects; callers should ensure streams are closed or wrapped in using statements.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-extract-images-and-text patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

---
name: pages
description: C# examples for pages using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - pages

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **pages** category.
This folder contains standalone C# examples for pages operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **pages**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (45/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/45 files)
- `using Aspose.Pdf.Facades;` (2/45 files)
- `using Aspose.Pdf.Annotations;` (1/45 files)
- `using Aspose.Pdf.Drawing;` (1/45 files)
- `using Aspose.Pdf.Tagged;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (45/45 files)

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
| [Add-a-blank-page-into-an-existing-PDF-file-maintaining-the-d...](./Add-a-blank-page-into-an-existing-PDF-file-maintaining-the-document-s-pagination-and-structure.cs) |  | Add a blank page into an existing PDF file maintaining the document s paginat... |
| [Add-a-customizable-watermark-to-a-PDF-file-by-embedding-it-d...](./Add-a-customizable-watermark-to-a-PDF-file-by-embedding-it-directly-within-the-PDF-format.cs) |  | Add a customizable watermark to a PDF file by embedding it directly within th... |
| [Append-a-blank-PDF-page-to-the-end-of-an-existing-PDF-docume...](./Append-a-blank-PDF-page-to-the-end-of-an-existing-PDF-document-while-maintaining-format-integrity.cs) |  | Append a blank PDF page to the end of an existing PDF document while maintain... |
| [Append-a-blank-page-to-the-end-of-a-PDF-document-maintaining...](./Append-a-blank-page-to-the-end-of-a-PDF-document-maintaining-PDF-format-integrity.cs) |  | Append a blank page to the end of a PDF document maintaining PDF format integ... |
| [Append-a-blank-page-to-the-end-of-a-PDF-document-while-prese...](./Append-a-blank-page-to-the-end-of-a-PDF-document-while-preserving-existing-content.cs) |  | Append a blank page to the end of a PDF document while preserving existing co... |
| [Apply-a-PDF-file-as-a-background-to-another-PDF-document-pre...](./Apply-a-PDF-file-as-a-background-to-another-PDF-document-preserving-the-original-content-layout.cs) | `PdfPageStamp` | Apply a PDF file as a background to another PDF document preserving the origi... |
| [Apply-a-text-or-image-watermark-to-PDF-documents-programmati...](./Apply-a-text-or-image-watermark-to-PDF-documents-programmatically-preserving-existing-content-and-l.cs) | `TextStamp` | Apply a text or image watermark to PDF documents programmatically preserving ... |
| [Apply-a-text-or-image-watermark-to-a-PDF-file-programmatical...](./Apply-a-text-or-image-watermark-to-a-PDF-file-programmatically-using-C-generating-a-watermarked-re.cs) | `ImageStamp` | Apply a text or image watermark to a PDF file programmatically using C genera... |
| [Divide-a-PDF-document-into-separate-PDF-files-preserving-eac...](./Divide-a-PDF-document-into-separate-PDF-files-preserving-each-page-s-content-and-associated-metadat.cs) |  | Divide a PDF document into separate PDF files preserving each page s content ... |
| [Explain-the-differences-between-Artbox-BleedBox-CropBox-Medi...](./Explain-the-differences-between-Artbox-BleedBox-CropBox-MediaBox-TrimBox-and-the-Rect-property.cs) | `Rectangle` | Explain the differences between Artbox BleedBox CropBox MediaBox TrimBox and ... |
| [Identify-and-retrieve-the-background-color-of-each-individua...](./Identify-and-retrieve-the-background-color-of-each-individual-page-within-a-PDF-document-using-the-A.cs) |  | Identify and retrieve the background color of each individual page within a P... |
| [Implement-functionality-to-apply-or-delete-Bates-numbering-o...](./Implement-functionality-to-apply-or-delete-Bates-numbering-on-PDF-files-while-maintaining-existing-c.cs) | `Rectangle` | Implement functionality to apply or delete Bates numbering on PDF files while... |
| [Insert-HTML-based-header-and-footer-fragments-into-a-PDF-doc...](./Insert-HTML-based-header-and-footer-fragments-into-a-PDF-document-preserving-layout-and-formatting.cs) | `HtmlFragment` | Insert HTML based header and footer fragments into a PDF document preserving ... |
| [Insert-a-blank-page-into-a-PDF-document-at-a-specified-posit...](./Insert-a-blank-page-into-a-PDF-document-at-a-specified-position-while-maintaining-file-integrity.cs) |  | Insert a blank page into a PDF document at a specified position while maintai... |
| [Insert-a-blank-page-into-an-existing-PDF-document-while-pres...](./Insert-a-blank-page-into-an-existing-PDF-document-while-preserving-document-structure-and-format-com.cs) |  | Insert a blank page into an existing PDF document while preserving document s... |
| [Insert-a-new-page-into-an-existing-PDF-document-at-a-specifi...](./Insert-a-new-page-into-an-existing-PDF-document-at-a-specified-position-while-preserving-existing-co.cs) |  | Insert a new page into an existing PDF document at a specified position while... |
| [Insert-a-new-page-into-an-existing-PDF-document-preserving-l...](./Insert-a-new-page-into-an-existing-PDF-document-preserving-layout-and-metadata-integrity-throughout.cs) | `TextFragment` | Insert a new page into an existing PDF document preserving layout and metadat... |
| [Insert-a-new-page-into-an-existing-PDF-document-specifying-s...](./Insert-a-new-page-into-an-existing-PDF-document-specifying-size-and-content-using-PDF-format.cs) | `TextFragment` | Insert a new page into an existing PDF document specifying size and content u... |
| [Insert-additional-pages-into-an-existing-PDF-document-while-...](./Insert-additional-pages-into-an-existing-PDF-document-while-maintaining-proper-PDF-format-compliance.cs) |  | Insert additional pages into an existing PDF document while maintaining prope... |
| [Insert-custom-header-and-footer-text-fragments-into-PDF-file...](./Insert-custom-header-and-footer-text-fragments-into-PDF-files-while-maintaining-existing-content-lay.cs) | `TextFragment`, `TextBuilder` | Insert custom header and footer text fragments into PDF files while maintaini... |
| [Insert-customizable-header-and-footer-elements-into-a-PDF-do...](./Insert-customizable-header-and-footer-elements-into-a-PDF-document-while-maintaining-PDF-compliance.cs) | `TextFragment` | Insert customizable header and footer elements into a PDF document while main... |
| [Insert-image-based-headers-and-footers-into-PDF-documents-en...](./Insert-image-based-headers-and-footers-into-PDF-documents-ensuring-proper-placement-and-rendering-a.cs) | `ImageStamp` | Insert image based headers and footers into PDF documents ensuring proper pla... |
| [Insert-sequential-page-numbers-into-a-PDF-document-formattin...](./Insert-sequential-page-numbers-into-a-PDF-document-formatting-each-page-according-to-the-specified.cs) | `PageNumberStamp` | Insert sequential page numbers into a PDF document formatting each page accor... |
| [Manipulate-PDF-pages-in-C-applications-including-adding-remo...](./Manipulate-PDF-pages-in-C-applications-including-adding-removing-and-reordering-pages-within-PDF.cs) |  | Manipulate PDF pages in C applications including adding removing and reorderi... |
| [Programmatically-change-the-orientation-of-pages-within-a-PD...](./Programmatically-change-the-orientation-of-pages-within-a-PDF-document-while-preserving-existing-con.cs) |  | Programmatically change the orientation of pages within a PDF document while ... |
| [Programmatically-crop-PDF-pages-in-C-while-preserving-the-or...](./Programmatically-crop-PDF-pages-in-C-while-preserving-the-original-PDF-file-format-and-page-layout.cs) | `Rectangle` | Programmatically crop PDF pages in C while preserving the original PDF file f... |
| [Programmatically-delete-pages-from-a-PDF-document-using-C-wh...](./Programmatically-delete-pages-from-a-PDF-document-using-C-while-maintaining-file-integrity-seamless.cs) |  | Programmatically delete pages from a PDF document using C while maintaining f... |
| [Programmatically-manipulate-artifacts-within-PDF-files-inclu...](./Programmatically-manipulate-artifacts-within-PDF-files-including-creation-extraction-and-modifica.cs) |  | Programmatically manipulate artifacts within PDF files including creation ext... |
| [Programmatically-reorder-or-relocate-pages-within-a-PDF-docu...](./Programmatically-reorder-or-relocate-pages-within-a-PDF-document-using-C-without-altering-the-file.cs) |  | Programmatically reorder or relocate pages within a PDF document using C with... |
| [Programmatically-rotate-PDF-pages-in-C-by-applying-specified...](./Programmatically-rotate-PDF-pages-in-C-by-applying-specified-angle-transformations-to-each-page.cs) |  | Programmatically rotate PDF pages in C by applying specified angle transforma... |
| ... | | *and 15 more files* |

## Category Statistics
- Total examples: 45

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BackgroundArtifact`
- `Aspose.Pdf.ColorType`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Pages`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.Page.GetPageRect(bool)`
- `Aspose.Pdf.PageCollection`
- `Aspose.Pdf.PageCollection.Add`
- `Aspose.Pdf.Rotation`
- `Aspose.Pdf.Text.TextFragment`

### Rules
- Load a PDF into a {doc} using new Document({input_pdf}).
- Delete a particular page by invoking {doc}.Pages.Delete({int}) where the integer is the 1‑based page number.
- Persist the changes by calling {doc}.Save({output_pdf}).
- Instantiate a {doc} by calling new Document({input_pdf}) to load a PDF file.
- Read the total number of pages via {doc}.Pages.Count after the document is successfully loaded.

### Warnings
- The Delete method expects a 1‑based page index and will throw if the index is out of range.
- The helper method RunExamples.GetDataDir_AsposePdf_Pages() is external to this snippet and must provide a valid directory path.
- The added page inherits the default page size of the document; specify size explicitly if a different layout is required.
- The Add method copies all pages; selective page ranges require additional filtering.
- If {output_pdf} already exists it will be overwritten without warning.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for pages patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

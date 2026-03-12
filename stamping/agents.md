---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **stamping** category.
This folder contains standalone C# examples for stamping operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **stamping**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (16/17 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/17 files)
- `using Aspose.Pdf.Annotations;` (2/17 files)
- `using Aspose.Pdf.Drawing;` (2/17 files)
- `using Aspose.Pdf.Facades;` (2/17 files)
- `using Aspose.Pdf.Optimization;` (1/17 files)
- `using System;` (17/17 files)
- `using System.IO;` (17/17 files)

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
| [Add-alternative-text-to-an-image-stamp-within-a-PDF-document...](./Add-alternative-text-to-an-image-stamp-within-a-PDF-document-loaded-from-PDF.cs) | `ImageStamp` | Add alternative text to an image stamp within a PDF document loaded from PDF |
| [Add-image-stamps-to-a-PDF-document-loaded-from-a-file-progra...](./Add-image-stamps-to-a-PDF-document-loaded-from-a-file-programmatically-using-C.cs) | `ImageStamp` | Add image stamps to a PDF document loaded from a file programmatically using C |
| [Adjust-the-compression-and-resolution-settings-of-stamp-imag...](./Adjust-the-compression-and-resolution-settings-of-stamp-images-when-applying-them-to-a-loaded-PDF-do.cs) | `ImageStamp` | Adjust the compression and resolution settings of stamp images when applying ... |
| [Apply-a-custom-page-stamp-to-each-page-of-a-loaded-PDF-docum...](./Apply-a-custom-page-stamp-to-each-page-of-a-loaded-PDF-document-using-C.cs) | `PdfPageStamp` | Apply a custom page stamp to each page of a loaded PDF document using C |
| [Apply-a-customizable-text-stamp-to-a-PDF-document-after-load...](./Apply-a-customizable-text-stamp-to-a-PDF-document-after-loading-it-from-an-existing-PDF-file.cs) | `TextStamp` | Apply a customizable text stamp to a PDF document after loading it from an ex... |
| [Apply-a-text-stamp-onto-a-loaded-PDF-document-and-save-the-m...](./Apply-a-text-stamp-onto-a-loaded-PDF-document-and-save-the-modified-PDF-file.cs) | `TextStamp` | Apply a text stamp onto a loaded PDF document and save the modified PDF file |
| [Apply-a-text-stamp-to-a-loaded-PDF-document-and-automaticall...](./Apply-a-text-stamp-to-a-loaded-PDF-document-and-automatically-scale-the-font-size-to-fit.cs) | `TextStamp` | Apply a text stamp to a loaded PDF document and automatically scale the font ... |
| [Apply-custom-page-stamps-to-a-loaded-PDF-document-in-C-using...](./Apply-custom-page-stamps-to-a-loaded-PDF-document-in-C-using-the-.NET-PDF-manipulation-API.cs) | `PdfPageStamp`, `ImageStamp`, `TextStamp` | Apply custom page stamps to a loaded PDF document in C using the .NET PDF man... |
| [Apply-stamping-to-a-PDF-document-loaded-from-a-PDF-file-prog...](./Apply-stamping-to-a-PDF-document-loaded-from-a-PDF-file-programmatically-using-C.cs) | `ImageStamp` | Apply stamping to a PDF document loaded from a PDF file programmatically using C |
| [Apply-text-stamps-to-a-PDF-document-after-loading-it-from-an...](./Apply-text-stamps-to-a-PDF-document-after-loading-it-from-an-existing-PDF-file.cs) | `TextStamp` | Apply text stamps to a PDF document after loading it from an existing PDF file |
| [Apply-text-stamps-to-a-loaded-PDF-document-using-C-APIs-to-a...](./Apply-text-stamps-to-a-loaded-PDF-document-using-C-APIs-to-annotate-the-file-programmatically.cs) | `TextStamp` | Apply text stamps to a loaded PDF document using C APIs to annotate the file ... |
| [Insert-an-image-stamp-into-a-PDF-document-that-has-been-load...](./Insert-an-image-stamp-into-a-PDF-document-that-has-been-loaded-from-an-existing-PDF-file.cs) | `ImageStamp` | Insert an image stamp into a PDF document that has been loaded from an existi... |
| [Overlay-image-stamps-onto-specific-pages-of-a-PDF-document-a...](./Overlay-image-stamps-onto-specific-pages-of-a-PDF-document-after-loading-the-file-into-memory.cs) | `PdfFileStamp`, `Stamp` | Overlay image stamps onto specific pages of a PDF document after loading the ... |
| [Render-fill-and-stroke-text-as-a-stamp-on-a-PDF-document-loa...](./Render-fill-and-stroke-text-as-a-stamp-on-a-PDF-document-loaded-from-an-existing-PDF-file.cs) | `TextStamp` | Render fill and stroke text as a stamp on a PDF document loaded from an exist... |
| [Render-stroke-text-onto-a-PDF-document-after-loading-it-from...](./Render-stroke-text-onto-a-PDF-document-after-loading-it-from-an-existing-PDF-file.cs) | `TextFragment`, `TextBuilder` | Render stroke text onto a PDF document after loading it from an existing PDF ... |
| [Set-an-image-stamp-as-the-background-of-a-floating-annotatio...](./Set-an-image-stamp-as-the-background-of-a-floating-annotation-box-on-a-loaded-PDF-document.cs) | `BorderInfo` | Set an image stamp as the background of a floating annotation box on a loaded... |
| [Set-the-alignment-of-a-TextStamp-applied-to-a-PDF-document-t...](./Set-the-alignment-of-a-TextStamp-applied-to-a-PDF-document-that-has-been-loaded.cs) | `TextStamp` | Set the alignment of a TextStamp applied to a PDF document that has been loaded |

## Category Statistics
- Total examples: 17

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

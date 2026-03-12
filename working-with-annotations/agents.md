---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-annotations** category.
This folder contains standalone C# examples for working-with-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (246/252 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (194/252 files) ← category-specific
- `using Aspose.Pdf.Text;` (36/252 files)
- `using Aspose.Pdf.Facades;` (30/252 files)
- `using Aspose.Pdf.Drawing;` (19/252 files)
- `using Aspose.Pdf.Tagged;` (5/252 files)
- `using Aspose.Pdf.LogicalStructure;` (3/252 files)
- `using Aspose.Pdf.Vector;` (2/252 files)
- `using System;` (252/252 files)
- `using System.IO;` (247/252 files)
- `using System.Drawing;` (12/252 files)
- `using System.Runtime.InteropServices;` (8/252 files)

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
| [Add-Delete-and-Get-Annotation-using-CGM-format](./Add-Delete-and-Get-Annotation-using-CGM-format.cs) | `CgmLoadOptions`, `TextAnnotation` | Add Delete and Get Annotation using CGM format |
| [Add-Delete-and-Get-Annotation-using-EPUB-format](./Add-Delete-and-Get-Annotation-using-EPUB-format.cs) | `Rectangle`, `TextAnnotation`, `PdfAnnotationEditor` | Add Delete and Get Annotation using EPUB format |
| [Add-Delete-and-Get-Annotation-using-MD-format](./Add-Delete-and-Get-Annotation-using-MD-format.cs) | `TextAnnotation` | Add Delete and Get Annotation using MD format |
| [Add-Delete-and-Get-Annotation-using-MHT-format](./Add-Delete-and-Get-Annotation-using-MHT-format.cs) | `TextAnnotation` | Add Delete and Get Annotation using MHT format |
| [Add-Delete-and-Get-Annotation-using-OFD-format](./Add-Delete-and-Get-Annotation-using-OFD-format.cs) | `OfdLoadOptions`, `TextAnnotation` | Add Delete and Get Annotation using OFD format |
| [Add-Delete-and-Get-Annotation-using-PCL-format](./Add-Delete-and-Get-Annotation-using-PCL-format.cs) | `PclLoadOptions`, `TextAnnotation` | Add Delete and Get Annotation using PCL format |
| [Add-Delete-and-Get-Annotation-using-PDF-format](./Add-Delete-and-Get-Annotation-using-PDF-format.cs) | `TextAnnotation` | Add Delete and Get Annotation using PDF format |
| [Add-Delete-and-Get-Annotation-using-PS-format](./Add-Delete-and-Get-Annotation-using-PS-format.cs) | `PsLoadOptions`, `TextAnnotation` | Add Delete and Get Annotation using PS format |
| [Add-Delete-and-Get-Annotation-using-SVG-format](./Add-Delete-and-Get-Annotation-using-SVG-format.cs) | `SquareAnnotation`, `SvgExtractor` | Add Delete and Get Annotation using SVG format |
| [Add-Delete-and-Get-Annotation-using-TeX-format](./Add-Delete-and-Get-Annotation-using-TeX-format.cs) | `TeXFragment`, `TextAnnotation` | Add Delete and Get Annotation using TeX format |
| [Add-Delete-and-Get-Annotation-using-XML-format](./Add-Delete-and-Get-Annotation-using-XML-format.cs) | `TextAnnotation` | Add Delete and Get Annotation using XML format |
| [Add-Delete-and-Get-Annotation-using-XPS-format](./Add-Delete-and-Get-Annotation-using-XPS-format.cs) | `XpsLoadOptions`, `TextAnnotation`, `PdfAnnotationEditor` | Add Delete and Get Annotation using XPS format |
| [Add-Figures-Annotations-using-C-using-CGM-format](./Add-Figures-Annotations-using-C-using-CGM-format.cs) | `CgmLoadOptions`, `CircleAnnotation`, `SquareAnnotation` | Add Figures Annotations using C using CGM format |
| [Add-Figures-Annotations-using-C-using-EPUB-format](./Add-Figures-Annotations-using-C-using-EPUB-format.cs) | `SquareAnnotation`, `CircleAnnotation` | Add Figures Annotations using C using EPUB format |
| [Add-Figures-Annotations-using-C-using-HTML-format](./Add-Figures-Annotations-using-C-using-HTML-format.cs) | `SquareAnnotation` | Add Figures Annotations using C using HTML format |
| [Add-Figures-Annotations-using-C-using-MD-format](./Add-Figures-Annotations-using-C-using-MD-format.cs) | `SquareAnnotation`, `CircleAnnotation` | Add Figures Annotations using C using MD format |
| [Add-Figures-Annotations-using-C-using-MHT-format](./Add-Figures-Annotations-using-C-using-MHT-format.cs) | `MhtLoadOptions`, `SquareAnnotation`, `CircleAnnotation` | Add Figures Annotations using C using MHT format |
| [Add-Figures-Annotations-using-C-using-OFD-format](./Add-Figures-Annotations-using-C-using-OFD-format.cs) | `OfdLoadOptions`, `SquareAnnotation`, `CircleAnnotation` | Add Figures Annotations using C using OFD format |
| [Add-Figures-Annotations-using-C-using-PDF-format](./Add-Figures-Annotations-using-C-using-PDF-format.cs) | `SquareAnnotation`, `CircleAnnotation`, `PolygonAnnotation` | Add Figures Annotations using C using PDF format |
| [Add-Figures-Annotations-using-C-using-PS-format](./Add-Figures-Annotations-using-C-using-PS-format.cs) | `SquareAnnotation`, `CircleAnnotation` | Add Figures Annotations using C using PS format |
| [Add-Figures-Annotations-using-C-using-SVG-format](./Add-Figures-Annotations-using-C-using-SVG-format.cs) | `SquareAnnotation`, `SvgSaveOptions` | Add Figures Annotations using C using SVG format |
| [Add-Figures-Annotations-using-C-using-TeX-format](./Add-Figures-Annotations-using-C-using-TeX-format.cs) | `SquareAnnotation`, `TeXFragment`, `TextAnnotation` | Add Figures Annotations using C using TeX format |
| [Add-Figures-Annotations-using-C-using-XML-format](./Add-Figures-Annotations-using-C-using-XML-format.cs) | `SquareAnnotation` | Add Figures Annotations using C using XML format |
| [Add-Figures-Annotations-using-C-using-XPS-format](./Add-Figures-Annotations-using-C-using-XPS-format.cs) | `SquareAnnotation`, `XpsSaveOptions` | Add Figures Annotations using C using XPS format |
| [Add-Reference-of-a-single-Image-multiple-times-in-a-PDF-Docu...](./Add-Reference-of-a-single-Image-multiple-times-in-a-PDF-Document-using-CGM-format.cs) | `CgmLoadOptions` | Add Reference of a single Image multiple times in a PDF Document using CGM fo... |
| [Add-Reference-of-a-single-Image-multiple-times-in-a-PDF-Docu...](./Add-Reference-of-a-single-Image-multiple-times-in-a-PDF-Document-using-PDF-format.cs) | `Rectangle` | Add Reference of a single Image multiple times in a PDF Document using PDF fo... |
| [Add-Screen-Annotation-using-CGM-format](./Add-Screen-Annotation-using-CGM-format.cs) | `ScreenAnnotation` | Add Screen Annotation using CGM format |
| [Add-Screen-Annotation-using-EPUB-format](./Add-Screen-Annotation-using-EPUB-format.cs) | `ScreenAnnotation` | Add Screen Annotation using EPUB format |
| [Add-Screen-Annotation-using-HTML-format](./Add-Screen-Annotation-using-HTML-format.cs) | `ScreenAnnotation` | Add Screen Annotation using HTML format |
| [Add-Screen-Annotation-using-MD-format](./Add-Screen-Annotation-using-MD-format.cs) | `ScreenAnnotation` | Add Screen Annotation using MD format |
| ... | | *and 222 more files* |

## Category Statistics
- Total examples: 252

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.AnnotationCollection`
- `Aspose.Pdf.AnnotationCollection.Delete`
- `Aspose.Pdf.Annotations.Annotation`
- `Aspose.Pdf.Annotations.AnnotationFlags`
- `Aspose.Pdf.Annotations.DefaultAppearance`
- `Aspose.Pdf.Annotations.FreeTextAnnotation`
- `Aspose.Pdf.Annotations.MarkupAnnotation`
- `Aspose.Pdf.Annotations.ScreenAnnotation`
- `Aspose.Pdf.Annotations.TextAnnotation`
- `Aspose.Pdf.Annotations.TextMarkupAnnotation`
- `Aspose.Pdf.Annotations.TextStyle`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Facades.PdfAnnotationEditor`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.BindPdf`

### Rules
- BindPdf({input_pdf}) must be called on a PdfContentEditor instance before any annotation operations.
- CreateFreeText({rect}, {string_literal}, {int}) adds a free‑text annotation containing the given text to the specified page number within the bound document.
- Save({output_pdf}) persists all changes made to the PDF after annotation creation.
- Load a PDF document with {doc} = new Document({input_pdf});
- Create a screen annotation using {annotation} = new ScreenAnnotation({page}, {rect}, {string_literal}) where {string_literal} points to a .swf file.

### Warnings
- The rectangle coordinates are expressed in points relative to the page's origin (bottom‑left).
- Page numbers are 1‑based; passing an invalid page index will throw an exception.
- SWF (Flash) content may not be supported by all PDF viewers; ensure target environment can render it.
- The rectangle coordinates must be within the bounds of {page} to be visible.
- The exact class name for the annotations collection is assumed to be Aspose.Pdf.AnnotationCollection; verify against the library version.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

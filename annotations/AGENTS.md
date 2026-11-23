---
name: annotations
description: C# examples for annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - annotations

> **Annotations** in PDF using C# / .NET -- **164** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **annotations** category.
This folder contains standalone C# examples for annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (164/164 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (157/164 files) ← category-specific
- `using Aspose.Pdf.Text;` (26/164 files)
- `using Aspose.Pdf.Drawing;` (3/164 files)
- `using Aspose.Pdf.LogicalStructure;` (1/164 files)
- `using Aspose.Pdf.Tagged;` (1/164 files)
- `using System;` (164/164 files)
- `using System.IO;` (164/164 files)
- `using System.Drawing;` (17/164 files)
- `using System.Collections.Generic;` (9/164 files)
- `using System.Text;` (9/164 files)
- `using System.Linq;` (3/164 files)
- `using System.Reflection;` (3/164 files)
- `using System.Globalization;` (1/164 files)
- `using System.Text.Json;` (1/164 files)

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
| [add-3d-annotation-to-pdf-page](./add-3d-annotation-to-pdf-page.cs) | Add 3D Annotation to PDF Page | `Document`, `PDF3DContent`, `PDF3DLightingScheme` | Demonstrates how to embed a U3D/PRC 3‑D model into a PDF as a PDF3DAnnotation on a specific page ... |
| [add-3d-annotation-with-custom-lighting](./add-3d-annotation-with-custom-lighting.cs) | Add 3D Annotation with Custom Lighting to PDF Page | `Document`, `Page`, `PDF3DContent` | Demonstrates loading a PDF, creating a PDF3DAnnotation with a lighting scheme and render mode, pl... |
| [add-3d-u3d-annotation-to-pdf](./add-3d-u3d-annotation-to-pdf.cs) | Add 3D U3D Annotation to PDF Page | `Document`, `Page`, `PDF3DContent` | Shows how to load a PDF, embed a U3D model as a PDF3DAnnotation on page 7, configure a view and b... |
| [add-blue-text-watermark-to-pdf-page-15](./add-blue-text-watermark-to-pdf-page-15.cs) | Add Blue Text Watermark to PDF Page 15 | `Document`, `Page`, `WatermarkAnnotation` | The example loads a PDF, creates a WatermarkAnnotation with blue 24‑point text on page 15, and sa... |
| [add-blue-underline-annotation](./add-blue-underline-annotation.cs) | Add Blue Underline Annotation to PDF | `Document`, `Page`, `Rectangle` | Loads a PDF, creates an underline annotation titled "Review" with a blue color on the first page,... |
| [add-caret-annotation-to-pdf](./add-caret-annotation-to-pdf.cs) | Add Red Caret Annotation to PDF Page | `Document`, `Rectangle`, `CaretAnnotation` | Demonstrates loading a PDF, creating a red caret annotation with a title on the first page, and s... |
| [add-caret-annotations-to-multiple-pdf-pages](./add-caret-annotations-to-multiple-pdf-pages.cs) | Add Caret Annotations to Multiple PDF Pages | `Document`, `Page`, `CaretAnnotation` | Shows how to load a PDF, create distinct CaretAnnotation objects on pages 1, 2 and 3 with unique ... |
| [add-centered-freetext-annotation](./add-centered-freetext-annotation.cs) | Add Centered FreeText Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a FreeTextAnnotation, set its text alignment to centered, and save the... |
| [add-centered-freetext-annotation__v2](./add-centered-freetext-annotation__v2.cs) | Add Centered FreeText Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a FreeTextAnnotation, set its text alignment to center, add it to a PD... |
| [add-circle-annotation-to-pdf](./add-circle-annotation-to-pdf.cs) | Create Circle Annotation in PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, adding a red circle annotation with a specific radius, color, and opa... |
| [add-circle-annotation-with-popup-note](./add-circle-annotation-with-popup-note.cs) | Add Circle Annotation with Popup Note to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a red semi‑transparent circle annotation, attach a popup annotati... |
| [add-circle-annotation-with-popup](./add-circle-annotation-with-popup.cs) | Add Circle Annotation with Popup to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a circle annotation with a light‑gray interior, semi‑transpa... |
| [add-comment-text-annotation-to-pdf](./add-comment-text-annotation-to-pdf.cs) | Add Comment Text Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a TextAnnotation with the Comment icon, adding it to the fir... |
| [add-custom-annotation-to-pdf-page-7](./add-custom-annotation-to-pdf-page-7.cs) | Add Custom Text Annotation to PDF Page 7 | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF with Aspose.Pdf, adding a custom text annotation to page 7, and saving... |
| [add-dashed-border-to-link-annotation](./add-dashed-border-to-link-annotation.cs) | Create Link Annotation with Dashed Border | `Document`, `Rectangle`, `Page` | The example loads a PDF, adds a link annotation that points to a URL, sets its border to a 1‑poin... |
| [add-delete-link-annotation](./add-delete-link-annotation.cs) | Add and Delete Link Annotation in PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, add a link annotation to a page, remove it by its index, and save the mo... |
| [add-diagonal-line-annotation](./add-diagonal-line-annotation.cs) | Add Diagonal Line Annotation with Arrow to PDF | `Document`, `Page`, `LineAnnotation` | Loads an existing PDF, creates a line annotation from (50,50) to (400,400) with an open arrow at ... |
| [add-embedded-media-summary-annotation](./add-embedded-media-summary-annotation.cs) | Add Summary of Embedded Media as FreeText Annotation | `Document`, `FileSpecification`, `Page` | The example loads a PDF, extracts the list of embedded files (media resources), builds a textual ... |
| [add-figure-and-rich-media-annotation](./add-figure-and-rich-media-annotation.cs) | Add Figure and Rich Media Annotation to a PDF | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to load a PDF, insert a tagged Figure element for accessibility, and place a Ric... |
| [add-figure-annotation-export-xfdf](./add-figure-annotation-export-xfdf.cs) | Add Figure Annotation and Export to XFDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, adding a square (figure) annotation, exporting all annotations to XFD... |
| [add-figure-annotation-to-pdf-page-3](./add-figure-annotation-to-pdf-page-3.cs) | Add Figure Annotation to PDF Page 3 | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a square (figure) annotation with a colored border on page 3... |
| [add-freetext-annotation-at-text-position](./add-freetext-annotation-at-text-position.cs) | Add FreeText Annotation at Located Text Position | `Document`, `TextFragmentAbsorber`, `TextFragment` | The example searches for a specific string in a PDF using TextFragmentAbsorber, then places a Fre... |
| [add-freetext-annotation-default-appearance](./add-freetext-annotation-default-appearance.cs) | Add FreeText Annotation with Default Appearance to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a FreeTextAnnotation, set its DefaultAppearance (font name, size, colo... |
| [add-freetext-callout-annotation-xfdf](./add-freetext-callout-annotation-xfdf.cs) | Add FreeText Callout Annotation via XFDF | `Document`, `ImportAnnotationsFromXfdf`, `Save` | Demonstrates creating an XFDF string that defines a FreeText annotation with a callout line, impo... |
| [add-goto-link-annotation-dashed-border](./add-goto-link-annotation-dashed-border.cs) | Add GoTo Link Annotation with Dashed Border to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a LinkAnnotation that navigates to another page using GoToAc... |
| [add-goto-link-annotation-to-pdf](./add-goto-link-annotation-to-pdf.cs) | Add GoTo Link Annotation to PDF | `Document`, `Page`, `XYZExplicitDestination` | Demonstrates loading a PDF, creating an XYZ explicit destination on page 2, adding a LinkAnnotati... |
| [add-goto-link-annotation-xyz-destination](./add-goto-link-annotation-xyz-destination.cs) | Create GoTo Link Annotation with XYZ Destination | `Document`, `Page`, `Rectangle` | The example loads an existing PDF, adds a link annotation on the first page that navigates to a s... |
| [add-hidden-caret-annotation-to-pdf-page-4](./add-hidden-caret-annotation-to-pdf-page-4.cs) | Add Hidden Caret Annotation to PDF Page 4 | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF with Aspose.Pdf, creating a CaretAnnotation with a subject and the Hid... |
| [add-highlight-annotation-export-xfdf](./add-highlight-annotation-export-xfdf.cs) | Add Highlight Annotation and Export to XFDF | `Document`, `HighlightAnnotation`, `Rectangle` | Demonstrates loading a PDF, adding a yellow highlight annotation to the first page, exporting all... |
| [add-highlight-annotation-export-xfdf__v2](./add-highlight-annotation-export-xfdf__v2.cs) | Add Highlight Annotation and Export to XFDF | `Document`, `Page`, `HighlightAnnotation` | Demonstrates loading a PDF, adding a highlight annotation, exporting all highlight annotations to... |
| ... | | | *and 134 more files* |

## Category Statistics
- Total examples: 164

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-22 | Run: `20261122_204815_38f195`
<!-- AUTOGENERATED:END -->

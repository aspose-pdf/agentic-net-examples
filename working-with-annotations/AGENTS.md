---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

> **Working with annotations** in PDF using C# / .NET -- **216** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-annotations** category.
This folder contains standalone C# examples for working-with-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (157/216 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (130/216 files) ← category-specific
- `using Aspose.Pdf.Text;` (15/216 files)
- `using Aspose.Pdf.Forms;` (10/216 files)
- `using Aspose.Pdf.Drawing;` (5/216 files)
- `using Aspose.Pdf.Facades;` (2/216 files)
- `using Aspose.Pdf.Devices;` (1/216 files)
- `using System;` (157/216 files)
- `using System.IO;` (155/216 files)
- `using System.Collections.Generic;` (14/216 files)
- `using System.Drawing;` (5/216 files)
- `using System.Linq;` (2/216 files)
- `using System.Threading.Tasks;` (2/216 files)
- `using Azure.Storage.Blobs;` (1/216 files)
- `using System.Net.Http;` (1/216 files)
- `using System.Net.Http.Headers;` (1/216 files)
- `using System.Text;` (1/216 files)
- `using System.Text.Json;` (1/216 files)
- `using System.Xml;` (1/216 files)

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
| [add-3d-annotation-custom-camera-view](./add-3d-annotation-custom-camera-view.cs) | Add 3d annotation custom camera view |  | Add 3d annotation custom camera view |
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Front View to PDF | `Document`, `PDF3DContent`, `PDF3DArtwork` | Shows how to embed a U3D/PRC 3‑D model into a PDF, create a 3‑D annotation, define a front‑facing... |
| [add-3d-annotation-with-custom-camera](./add-3d-annotation-with-custom-camera.cs) | Add 3D Annotation with Custom Camera to PDF | `Document`, `PDF3DContent`, `PDF3DArtwork` | Shows how to embed a 3D model into a PDF and define a custom camera view using Aspose.Pdf 3D anno... |
| [add-3d-u3d-annotation-initial-view-page-2](./add-3d-u3d-annotation-initial-view-page-2.cs) | Add 3d u3d annotation initial view page 2 |  | Add 3d u3d annotation initial view page 2 |
| [add-3d-u3d-annotation-initial-view-page-two](./add-3d-u3d-annotation-initial-view-page-two.cs) | Add 3D U3D Annotation with Initial View on Page Two | `Document`, `LoadAsU3D`, `PDF3DArtwork` | Shows how to embed a U3D model as a 3D annotation in a PDF, define an initial camera view, and pl... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF Screen Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to place a ScreenAnnotation that shows an animated GIF, looping continuously, on... |
| [add-auto-play-rich-media-annotation](./add-auto-play-rich-media-annotation.cs) | Add auto play rich media annotation |  | Add auto play rich media annotation |
| [add-automatic-background-music-to-pdf](./add-automatic-background-music-to-pdf.cs) | Add Automatic Background Music to PDF | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to embed an MP3 audio file as a RichMediaAnnotation that automatically plays whe... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Image Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to create a BackgroundArtifact from an image file and attach it to a page's Arti... |
| [add-background-image-to-pdf-page](./add-background-image-to-pdf-page.cs) | Add background image to pdf page |  | Add background image to pdf page |
| [add-background-music-to-pdf](./add-background-music-to-pdf.cs) | Add background music to pdf |  | Add background music to pdf |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering Artifact with Prefix to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to add a Bates numbering artifact with a custom "ABC" prefix and a six‑digit for... |
| [add-button-annotation-go-to-page-10](./add-button-annotation-go-to-page-10.cs) | Add Button Annotation to Navigate to Page 10 | `Document`, `Page`, `Rectangle` | Shows how to create a button form field that jumps to page 10 of the same PDF when activated, usi... |
| [add-button-annotation-highlight-text-fields](./add-button-annotation-highlight-text-fields.cs) | Create Button Annotation to Highlight Text Fields | `Document`, `Page`, `Rectangle` | Shows how to add a push‑button annotation to a PDF page with Aspose.Pdf, attach a JavaScript acti... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free-Text Annotation | `Document`, `Page`, `FreeTextAnnotation` | Demonstrates creating a FreeTextAnnotation with a callout leader line by configuring the Callout ... |
| [add-diagonal-watermark-annotation](./add-diagonal-watermark-annotation.cs) | Add Diagonal Watermark Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to place a WatermarkAnnotation diagonally across a PDF page using a rectangle ge... |
| [add-dynamic-date-watermark-to-pdf-pages](./add-dynamic-date-watermark-to-pdf-pages.cs) | Add Dynamic Date Watermark to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to add a WatermarkAnnotation with a runtime‑generated date string to each page o... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `WatermarkAnnotation` | Shows how to create a WatermarkAnnotation that displays the current page number on each page of a... |
| [add-export-annotations-button](./add-export-annotations-button.cs) | Add export annotations button |  | Add export annotations button |
| [add-external-link-annotation-to-pdf](./add-external-link-annotation-to-pdf.cs) | Add external link annotation to pdf |  | Add external link annotation to pdf |
| [add-external-url-link-annotation](./add-external-url-link-annotation.cs) | Add External URL Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that opens an external website when the user clicks the ann... |
| [add-figure-annotation-around-table](./add-figure-annotation-around-table.cs) | Add Figure Annotation Around a Detected Table | `Document`, `TableAbsorber`, `Rectangle` | Demonstrates how to use TableAbsorber to locate a table on a PDF page and draw a square (figure) ... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation Referencing External Image | `Document`, `Page`, `Rectangle` | Demonstrates how to create a FileAttachment (figure) annotation in a PDF that uses an external im... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add figure annotations custom line width color |  | Add figure annotations custom line width color |
| [add-flash-rich-media-annotation](./add-flash-rich-media-annotation.cs) | Add flash rich media annotation |  | Add flash rich media annotation |
| [add-footer-watermark-annotation](./add-footer-watermark-annotation.cs) | Add footer watermark annotation |  | Add footer watermark annotation |
| [add-footer-watermark-to-pdf-pages](./add-footer-watermark-to-pdf-pages.cs) | Add Footer Watermark to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF with Aspose.Pdf, iterating through each page, and adding a WatermarkAn... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a free‑text annotation on a PDF page using Arial, 12‑point font, and s... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load a PDF, create a free‑text annotation with a callout line, set its appear... |
| [add-goto-page-button-annotation](./add-goto-page-button-annotation.cs) | Add goto page button annotation |  | Add goto page button annotation |
| ... | | | *and 186 more files* |

## Category Statistics
- Total examples: 216

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
- Review code examples in this folder for working-with-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

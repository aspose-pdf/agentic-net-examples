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

- `using Aspose.Pdf;` (157/157 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (128/157 files) ← category-specific
- `using Aspose.Pdf.Text;` (18/157 files)
- `using Aspose.Pdf.Drawing;` (9/157 files)
- `using Aspose.Pdf.Forms;` (9/157 files)
- `using Aspose.Pdf.Facades;` (2/157 files)
- `using Aspose.Pdf.Devices;` (1/157 files)
- `using System;` (157/157 files)
- `using System.IO;` (147/157 files)
- `using System.Collections.Generic;` (13/157 files)
- `using System.Drawing;` (5/157 files)
- `using System.Threading.Tasks;` (3/157 files)
- `using System.Linq;` (2/157 files)
- `using System.Net.Http;` (2/157 files)
- `using System.Diagnostics.CodeAnalysis;` (1/157 files)
- `using System.Drawing.Imaging;` (1/157 files)
- `using System.Net.Http.Headers;` (1/157 files)
- `using System.Text;` (1/157 files)
- `using System.Text.Json;` (1/157 files)
- `using System.Xml;` (1/157 files)
- `using System.Xml.Linq;` (1/157 files)

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
| [add-3d-annotation-custom-camera-angle](./add-3d-annotation-custom-camera-angle.cs) | Add 3D Annotation with Custom Camera Angle | `Document`, `PDF3DContent`, `PDF3DArtwork` | Shows how to embed a U3D/PRC model into a PDF as a 3‑D annotation and create a custom view by def... |
| [add-3d-annotation-metal-lighting](./add-3d-annotation-metal-lighting.cs) | Add 3D Annotation with Metal‑like Lighting to PDF | `Document`, `Page`, `Rectangle` | Shows how to embed a 3D model into a PDF and configure lighting and render mode to simulate reali... |
| [add-3d-annotation-with-embedded-textures](./add-3d-annotation-with-embedded-textures.cs) | Add 3D Annotation with Embedded Textures and Front View | `Document`, `PDF3DContent`, `PDF3DArtwork` | Demonstrates embedding a 3D model (U3D/PRC) with textures into a PDF, creating a 3D annotation, d... |
| [add-a-3d-annotation-using-a-u3d-model-file-and-set...](./add-a-3d-annotation-using-a-u3d-model-file-and-set-its-initial-view-on-page-two.cs) | Add A 3D Annotation Using A U3D Model File And Set Its Initi... | `PDF3DAnnotation` | Add A 3D Annotation Using A U3D Model File And Set Its Initial View On Page Two |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF Screen Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Shows how to embed an animated GIF as a looping ScreenAnnotation on the third page of a PDF using... |
| [add-arrow-line-annotation-to-pdf](./add-arrow-line-annotation-to-pdf.cs) | Add Arrow Line Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a line annotation with arrow line endings on a PDF page using Aspose.Pdf. |
| [add-auto-play-background-music](./add-auto-play-background-music.cs) | Add Auto‑Play Background Music with RichMediaAnnotation | `Document`, `Page`, `Rectangle` | Shows how to embed an MP3 file as a RichMediaAnnotation that starts playing automatically when th... |
| [add-auto-play-rich-media-annotation](./add-auto-play-rich-media-annotation.cs) | Add Auto‑Play Rich Media Annotation to PDF | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to embed a video as a RichMediaAnnotation in a PDF and configure it to start aut... |
| [add-background-image-to-pdf-page](./add-background-image-to-pdf-page.cs) | Add Background Image to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to create a BackgroundArtifact from an image file, mark it as a background, and add it ... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Prefix to PDF | `Document`, `Pages`, `AddBatesNumbering` | Demonstrates how to add Bates numbering to each page of a PDF using Aspose.Pdf, with a custom "AB... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free‑Text Annotation | `Document`, `Page`, `FreeTextAnnotation` | Demonstrates how to create a free‑text annotation with a callout leader line in a PDF using Aspos... |
| [add-diagonal-watermark-annotation](./add-diagonal-watermark-annotation.cs) | Add Diagonal Watermark Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a WatermarkAnnotation that spans the page diagonally, settin... |
| [add-dynamic-date-watermark-to-pdf-pages](./add-dynamic-date-watermark-to-pdf-pages.cs) | Add Dynamic Date Watermark to Each PDF Page | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to add a semi‑transparent watermark annotation containing a runtime‑generated da... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `WatermarkAnnotation` | Shows how to create a WatermarkAnnotation that displays the current page number on each page, upd... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation Referencing External Image | `Document`, `Page`, `Rectangle` | Demonstrates how to create a file‑attachment (figure) annotation that points to an external image... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add Figure Annotations with Custom Line Width and Color | `Document`, `Page`, `SquareAnnotation` | Shows how to add square figure annotations with a specific stroke color and line width to selecte... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a free‑text annotation using Arial 12‑pt font into a PDF document with Aspose... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `FreeTextAnnotation` | Demonstrates how to add a free‑text annotation that includes a callout line to a PDF page using A... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a highlight annotation, set its opacity to 70%, and save the upda... |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a highlight annotation with a custom opacity to a PDF page using Aspose.Pdf. |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `LinkAnnotation` | Shows how to create a link annotation that navigates to a specific page within the same PDF docum... |
| [add-javascript-button-annotation-page-four](./add-javascript-button-annotation-page-four.cs) | Add JavaScript Button Annotation on Page Four | `Document`, `Page`, `Rectangle` | Demonstrates how to add a push button field to the fourth page of a PDF and attach a JavaScript a... |
| [add-javascript-link-annotation](./add-javascript-link-annotation.cs) | Add JavaScript Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that runs a JavaScript function to display a custom modal d... |
| [add-javascript-link-annotation__v2](./add-javascript-link-annotation__v2.cs) | Add JavaScript Link Annotation to PDF | `Document`, `Page`, `LinkAnnotation` | Creates a PDF, then adds a link annotation that runs JavaScript to open a URL in a new browser wi... |
| [add-looping-video-screen-annotation](./add-looping-video-screen-annotation.cs) | Add Looping Video Screen Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates embedding a video file in a PDF using a ScreenAnnotation, configuring it to play con... |
| [add-magenta-underline-annotation](./add-magenta-underline-annotation.cs) | Add Magenta Underline Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert an underline annotation on a PDF page, set its color to magenta, and define a... |
| [add-polyline-annotation-to-pdf](./add-polyline-annotation-to-pdf.cs) | Add Polyline Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a polyline annotation from a list of points and add it to a PDF page u... |
| [add-polyline-figure-annotation](./add-polyline-figure-annotation.cs) | Add Polyline Figure Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load a PDF, create a custom polyline figure annotation with visual properties... |
| [add-popup-annotation-linked-to-highlight](./add-popup-annotation-linked-to-highlight.cs) | Add Popup Annotation Linked to Highlight | `Document`, `Page`, `HighlightAnnotation` | Loads a PDF, adds a yellow highlight annotation on the first page, creates a popup note annotatio... |
| [add-popup-annotation-to-pdf](./add-popup-annotation-to-pdf.cs) | Add Popup Annotation Linked to a Sticky Note | `Document`, `Page`, `TextAnnotation` | Demonstrates how to create a TextAnnotation (sticky note) and associate a PopupAnnotation that sh... |
| ... | | | *and 127 more files* |

## Category Statistics
- Total examples: 157

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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf;` (163/163 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (130/163 files) ← category-specific
- `using Aspose.Pdf.Text;` (15/163 files)
- `using Aspose.Pdf.Facades;` (10/163 files)
- `using Aspose.Pdf.Forms;` (9/163 files)
- `using Aspose.Pdf.Drawing;` (7/163 files)
- `using Aspose.Pdf.Devices;` (1/163 files)
- `using Aspose.Pdf.LogicalStructure;` (1/163 files)
- `using System;` (163/163 files)
- `using System.IO;` (156/163 files)
- `using System.Collections.Generic;` (15/163 files)
- `using System.Drawing;` (5/163 files)
- `using System.Text;` (3/163 files)
- `using System.Net.Http;` (2/163 files)
- `using Azure.Storage.Blobs;` (1/163 files)
- `using System.Linq;` (1/163 files)
- `using System.Net.Http.Headers;` (1/163 files)
- `using System.Text.Json;` (1/163 files)
- `using System.Threading.Tasks;` (1/163 files)
- `using System.Xml.Linq;` (1/163 files)

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
| [add-3d-annotation-custom-camera](./add-3d-annotation-custom-camera.cs) | Add 3D Annotation with Custom Camera to PDF | `Document`, `PDF3DContent`, `PDF3DArtwork` | Shows how to embed a 3D model into a PDF as a 3D annotation and configure a custom camera view us... |
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Front View Rotation to PDF | `Document`, `PDF3DContent`, `PDF3DArtwork` | Demonstrates embedding a 3‑D model (U3D/PRC) into a PDF, creating a front‑view rotation, and addi... |
| [add-3d-annotation-u3d-page-two](./add-3d-annotation-u3d-page-two.cs) | Add 3D Annotation with U3D Model and Set Initial View on Pag... | `Document`, `PDF3DContent`, `PDF3DArtwork` | Demonstrates how to embed a U3D 3‑D model as a PDF3DAnnotation on the second page of a PDF and se... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF Screen Annotation to PDF Page | `Document`, `Rectangle`, `ScreenAnnotation` | Shows how to embed an animated GIF as a ScreenAnnotation on the third page of a PDF, making the a... |
| [add-arrow-line-annotation-to-pdf](./add-arrow-line-annotation-to-pdf.cs) | Add Arrow Line Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a line annotation with an arrow ending on a PDF page using Aspose.Pdf. |
| [add-auto-play-background-audio](./add-auto-play-background-audio.cs) | Add Auto‑Play Background Audio with RichMediaAnnotation | `Document`, `Page`, `Rectangle` | Demonstrates how to embed an audio file in a PDF and configure a RichMediaAnnotation to play auto... |
| [add-auto-play-rich-media-annotation](./add-auto-play-rich-media-annotation.cs) | Add Auto‑Play Rich Media Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates embedding a video as a RichMediaAnnotation in a PDF and configuring it to play autom... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Image Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to create a BackgroundArtifact from an image file and attach it to a specific PD... |
| [add-bates-numbering-with-prefix-six-digit](./add-bates-numbering-with-prefix-six-digit.cs) | Add Bates Numbering with Prefix and Six-Digit Format to PDF | `Document`, `AddBatesNumbering`, `BatesNumberingArtifact` | Demonstrates how to add Bates numbering to each page of a PDF using Aspose.Pdf, setting a custom ... |
| [add-callout-to-freetext-annotation](./add-callout-to-freetext-annotation.cs) | Add Callout Leader Line to Free Text Annotation | `Document`, `Rectangle`, `DefaultAppearance` | Demonstrates how to create a free‑text annotation with a callout (leader line) on a PDF page usin... |
| [add-diagonal-watermark-annotation](./add-diagonal-watermark-annotation.cs) | Add Diagonal Watermark Annotation to PDF | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a full‑page WatermarkAnnotation with semi‑transparent color, adds it to the ... |
| [add-dynamic-date-watermark-annotation](./add-dynamic-date-watermark-annotation.cs) | Add Dynamic Date Watermark Annotation to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates how to add a WatermarkAnnotation containing a runtime‑generated date string (and pag... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a WatermarkAnnotation that displays a page‑specific number on each page of a ... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation with External Image | `Document`, `Page`, `Rectangle` | Demonstrates how to create a figure (file attachment) annotation in a PDF that references an exte... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add Figure Annotations with Custom Line Width and Color | `Document`, `Page`, `Rectangle` | Demonstrates how to programmatically add square figure annotations with a specified border color ... |
| [add-footer-watermark-annotations-to-pdf-pages](./add-footer-watermark-annotations-to-pdf-pages.cs) | Add Footer Watermark Annotations to PDF Pages | `Document`, `Save`, `Page` | Loads a PDF, iterates through each page, and adds a unique WatermarkAnnotation in the footer area... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation with Arial, 12‑point font to a PDF page using Aspo... |
| [add-free-text-annotation-with-background](./add-free-text-annotation-with-background.cs) | Add Free Text Annotation with Light Gray Background | `Document`, `Page`, `Rectangle` | Loads a PDF document, creates a free‑text annotation with a light gray background and border, add... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a free‑text annotation with a callout line, customize its appearance, ... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity | `Document`, `Rectangle`, `HighlightAnnotation` | Demonstrates how to add a highlight annotation to a PDF page and set its opacity to 70% for subtl... |
| [add-highlight-annotation-with-custom-opacity](./add-highlight-annotation-with-custom-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a highlight annotation with a defined rectangle and 50% opac... |
| [add-internal-link-annotation](./add-internal-link-annotation.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that jumps to a specific page within the same PDF document ... |
| [add-javascript-button-annotation-to-pdf-page](./add-javascript-button-annotation-to-pdf-page.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to create a push button on page four of a PDF and attach a JavaScript action tha... |
| [add-javascript-link-annotation-modal-dialog](./add-javascript-link-annotation-modal-dialog.cs) | Add JavaScript Link Annotation to Show Modal Dialog | `Document`, `Page`, `Rectangle` | Demonstrates how to add a link annotation with a JavaScript action that displays a custom modal d... |
| [add-javascript-link-annotation-open-url](./add-javascript-link-annotation-open-url.cs) | Add JavaScript Link Annotation to Open URL in New Window | `Document`, `Save`, `Page` | Demonstrates how to add a link annotation with a JavaScript action that opens a specified URL in ... |
| [add-javascript-link-word-count](./add-javascript-link-word-count.cs) | Add JavaScript Link Annotation for Word Count | `Document`, `Page`, `Rectangle` | Demonstrates how to add a link annotation that runs JavaScript to calculate and display the PDF's... |
| [add-js-calculation-button-to-pdf-form](./add-js-calculation-button-to-pdf-form.cs) | Add JavaScript Calculation Button to PDF Form | `Document`, `FormEditor`, `FieldType` | Shows how to insert a push‑button field into a PDF form and attach JavaScript that adds two numer... |
| [add-link-annotation-external-url](./add-link-annotation-external-url.cs) | Add Link Annotation with External URL to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a link annotation on a PDF page that opens an external web URL when cl... |
| [add-magenta-underline-annotation](./add-magenta-underline-annotation.cs) | Add Magenta Underline Annotation with Custom Thickness | `Document`, `Save`, `Rectangle` | Shows how to insert an underline annotation on a PDF page, set its color to magenta, and define a... |
| [add-polyline-figure-annotation](./add-polyline-figure-annotation.cs) | Add Polyline Figure Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a custom polyline figure annotation, set its visual properties, and ad... |
| ... | | | *and 133 more files* |

## Category Statistics
- Total examples: 163

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

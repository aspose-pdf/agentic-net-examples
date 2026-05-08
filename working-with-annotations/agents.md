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

- `using Aspose.Pdf;` (160/160 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (131/160 files) ← category-specific
- `using Aspose.Pdf.Text;` (14/160 files)
- `using Aspose.Pdf.Forms;` (11/160 files)
- `using Aspose.Pdf.Drawing;` (3/160 files)
- `using Aspose.Pdf.Facades;` (2/160 files)
- `using System;` (160/160 files)
- `using System.IO;` (159/160 files)
- `using System.Collections.Generic;` (16/160 files)
- `using System.Text;` (3/160 files)
- `using System.Threading.Tasks;` (3/160 files)
- `using System.Drawing;` (2/160 files)
- `using System.Linq;` (2/160 files)
- `using System.Runtime.InteropServices;` (2/160 files)
- `using System.Xml.Linq;` (2/160 files)
- `using Azure.Storage.Blobs;` (1/160 files)
- `using System.Net.Http;` (1/160 files)
- `using System.Net.Http.Headers;` (1/160 files)
- `using System.Reflection;` (1/160 files)
- `using System.Text.Json;` (1/160 files)

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
| [add-3d-annotation-custom-camera](./add-3d-annotation-custom-camera.cs) | Add 3D Annotation with Custom Camera to PDF | `Document`, `Page`, `PDF3DContent` | Shows how to embed a U3D/PRC model in a PDF, define a custom camera matrix, create a 3D view, and... |
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Front View to PDF | `Document`, `Page`, `PDF3DContent` | Shows how to embed a 3‑D model (including its textures) into a PDF as a 3‑D annotation and set a ... |
| [add-3d-annotation-u3d-to-pdf-page](./add-3d-annotation-u3d-to-pdf-page.cs) | Add 3D Annotation with U3D Model to PDF Page | `Document`, `Page`, `PDF3DContent` | Shows how to embed a U3D model as a 3D annotation on the second page of a PDF and set a default 3... |
| [add-animated-gif-screen-annotation-page-3](./add-animated-gif-screen-annotation-page-3.cs) | Add Animated GIF Screen Annotation on Page 3 | `Document`, `Page`, `Rectangle` | Demonstrates how to place a ScreenAnnotation that shows an animated GIF, looping continuously, on... |
| [add-arrow-line-annotation-to-pdf](./add-arrow-line-annotation-to-pdf.cs) | Add Arrow Line Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a line annotation with an arrow ending on a PDF page using Aspose.Pdf. |
| [add-auto-play-background-music-to-pdf](./add-auto-play-background-music-to-pdf.cs) | Add Auto-Play Background Music to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to embed an MP3 file as a RichMediaAnnotation and configure it to play automatic... |
| [add-auto-play-rich-media-annotation](./add-auto-play-rich-media-annotation.cs) | Add Auto-Play Rich Media Annotation to PDF | `Document`, `Page`, `RichMediaAnnotation` | Shows how to embed a video or audio file in a PDF using a RichMediaAnnotation and configure it to... |
| [add-background-artifact-to-pdf-page](./add-background-artifact-to-pdf-page.cs) | Add Background Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a PDF, adding a BackgroundArtifact behind the page contents, setting a soli... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Image Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to create a BackgroundArtifact from an image file, configure its properties, and add it... |
| [add-bates-numbering-to-pdf](./add-bates-numbering-to-pdf.cs) | Add Bates Numbering to PDF with Prefix and Fixed Digits | `Document`, `Page`, `TextFragment` | Demonstrates how to add Bates numbers with a custom prefix and six‑digit format to each page of a... |
| [add-button-annotation-highlight-text-fields](./add-button-annotation-highlight-text-fields.cs) | Add Button Annotation with JavaScript to Highlight Text Fiel... | `Document`, `Page`, `Rectangle` | Demonstrates how to create a button annotation in a PDF using Aspose.Pdf, assign a JavaScript act... |
| [add-button-export-annotations-json](./add-button-export-annotations-json.cs) | Add Button to Export Annotations as JSON | `Document`, `Rectangle`, `ButtonField` | Shows how to insert a push‑button field into a PDF that runs JavaScript to retrieve all annotatio... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free‑Text Annotation | `Document`, `Page`, `FreeTextAnnotation` | Demonstrates how to create a FreeTextAnnotation, set its Callout property with three points to dr... |
| [add-centered-watermark-annotation-page-6](./add-centered-watermark-annotation-page-6.cs) | Insert Centered Watermark Annotation on Page 6 | `Document`, `Page`, `WatermarkAnnotation` | The example loads a PDF, creates a WatermarkAnnotation with 30 % opacity, positions it at the cen... |
| [add-diagonal-watermark-annotation](./add-diagonal-watermark-annotation.cs) | Add Diagonal Watermark Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a WatermarkAnnotation that covers an entire page, set its opacity and ... |
| [add-dynamic-date-watermark-annotation](./add-dynamic-date-watermark-annotation.cs) | Add Dynamic Date Watermark Annotation to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Loads a PDF, iterates through each page, and adds a WatermarkAnnotation that displays the current... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `WatermarkAnnotation` | Shows how to create a WatermarkAnnotation on each PDF page that displays the current page number,... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add Figure Annotations with Custom Line Width and Color | `Document`, `Page`, `SquareAnnotation` | Demonstrates how to programmatically add square and circle annotations with specific line width a... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation with External Image | `Document`, `Page`, `Rectangle` | Shows how to create a file attachment (figure) annotation in a PDF and use an external image file... |
| [add-flash-rich-media-annotation](./add-flash-rich-media-annotation.cs) | Add Flash Rich Media Annotation to PDF | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to embed a Flash video with a custom SWF player into a PDF using a RichMediaAnno... |
| [add-footer-watermark-to-pdf-pages](./add-footer-watermark-to-pdf-pages.cs) | Add Footer Watermark to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, iterating through each page, and adding a WatermarkAnnotation as a fo... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation to a PDF page using Arial 12‑point font and save t... |
| [add-free-text-annotation-light-gray](./add-free-text-annotation-light-gray.cs) | Add Free‑Text Annotation with Light Gray Background | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation to a PDF page, set its appearance, background colo... |
| [add-freetext-annotation-with-callout](./add-freetext-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load a PDF, create a free‑text annotation with a callout line, set its appear... |
| [add-goto-page-button-annotation](./add-goto-page-button-annotation.cs) | Add Go-To-Page Button Annotation | `Document`, `Page`, `ButtonField` | Demonstrates how to create a button annotation in a PDF that, when clicked, navigates to page 10 ... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `HighlightAnnotation` | Shows how to create a yellow highlight annotation with 70% opacity on the first page of a PDF and... |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a HighlightAnnotation with a specific color and opacity, add... |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that navigates to a specific page within the same PDF docum... |
| [add-javascript-button-annotation](./add-javascript-button-annotation.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `ButtonField`, `JavascriptAction` | Demonstrates how to place a button on the fourth page of a PDF and attach a JavaScript action tha... |
| [add-javascript-link-annotation-modal-dialog](./add-javascript-link-annotation-modal-dialog.cs) | Add JavaScript Link Annotation to Show Modal Dialog | `Document`, `Page`, `Rectangle` | Demonstrates how to add a link annotation with a JavaScript action that displays a custom modal d... |
| ... | | | *and 130 more files* |

## Category Statistics
- Total examples: 160

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
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

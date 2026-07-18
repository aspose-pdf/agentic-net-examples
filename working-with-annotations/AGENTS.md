---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

> **Working with annotations** in PDF using C# / .NET -- **156** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-annotations** category.
This folder contains standalone C# examples for working-with-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (156/156 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (128/156 files) ← category-specific
- `using Aspose.Pdf.Text;` (13/156 files)
- `using Aspose.Pdf.Forms;` (10/156 files)
- `using Aspose.Pdf.Drawing;` (6/156 files)
- `using Aspose.Pdf.Facades;` (1/156 files)
- `using System;` (156/156 files)
- `using System.IO;` (153/156 files)
- `using System.Collections.Generic;` (15/156 files)
- `using System.Drawing;` (7/156 files)
- `using System.Threading.Tasks;` (3/156 files)
- `using System.Linq;` (2/156 files)
- `using System.Net.Http;` (2/156 files)
- `using Azure.Storage.Blobs;` (1/156 files)
- `using System.Drawing.Drawing2D;` (1/156 files)
- `using System.Net.Http.Headers;` (1/156 files)
- `using System.Reflection;` (1/156 files)
- `using System.Text.Json;` (1/156 files)
- `using System.Xml.Linq;` (1/156 files)

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
| [add-3d-annotation-custom-camera-view](./add-3d-annotation-custom-camera-view.cs) | Add 3D Annotation with Custom Camera View | `Document`, `Page`, `Rectangle` | Shows how to embed a U3D model in a PDF and define a custom camera angle using a 3‑D annotation w... |
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Front View to PDF | `Document`, `Page`, `PDF3DContent` | Shows how to embed a 3‑D model (U3D/PRC) into a PDF, create a front‑view camera, and add a 3‑D an... |
| [add-3d-u3d-annotation-initial-view-page-2](./add-3d-u3d-annotation-initial-view-page-2.cs) | Add 3D U3D Annotation with Initial View on Page 2 | `Document`, `Page`, `PDF3DContent` | Demonstrates how to embed a U3D model as a 3D annotation on the second page of a PDF and set a cu... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF as Screen Annotation on PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to embed an animated GIF as a ScreenAnnotation on the third page of a PDF using ... |
| [add-auto-play-rich-media-annotation](./add-auto-play-rich-media-annotation.cs) | Add Auto‑Play Rich Media Annotation to PDF | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to embed a video or audio file into a PDF as a RichMediaAnnotation and configure... |
| [add-background-image-to-pdf-page](./add-background-image-to-pdf-page.cs) | Add Background Image to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to create a BackgroundArtifact from an image file and attach it to a specific PD... |
| [add-background-music-to-pdf](./add-background-music-to-pdf.cs) | Add Background Music to PDF Using RichMediaAnnotation | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates embedding an MP3 file as a RichMediaAnnotation that automatically plays when the pag... |
| [add-bates-numbering-with-prefix](./add-bates-numbering-with-prefix.cs) | Add Bates Numbering with Prefix to PDF | `Document`, `AddBatesNumbering`, `BatesNArtifact` | Demonstrates how to apply Bates numbering to every page of a PDF using Aspose.Pdf, with a custom ... |
| [add-button-annotation-highlight-text-fields](./add-button-annotation-highlight-text-fields.cs) | Create Button Annotation to Highlight Text Fields | `Document`, `Page`, `Rectangle` | Shows how to add a push‑button annotation to a PDF page with Aspose.Pdf and attach a JavaScript a... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free-Text Annotation | `Document`, `FreeTextAnnotation`, `DefaultAppearance` | Shows how to create a free‑text annotation with a callout leader line by setting the FreeTextCall... |
| [add-dynamic-date-watermark-to-pdf-pages](./add-dynamic-date-watermark-to-pdf-pages.cs) | Add Dynamic Date Watermark to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to add a WatermarkAnnotation with a runtime‑generated date string to each page o... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark with WatermarkAnnotation | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to place a WatermarkAnnotation on each PDF page that automatically shows the cur... |
| [add-export-annotations-button](./add-export-annotations-button.cs) | Add Button to Export Annotations as JSON | `Document`, `Rectangle`, `ButtonField` | Demonstrates how to create a push‑button field in a PDF that runs JavaScript to collect all annot... |
| [add-external-link-annotation-to-pdf](./add-external-link-annotation-to-pdf.cs) | Add External Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a link annotation that opens an external URL when clicked, using Aspos... |
| [add-figure-annotation-around-table](./add-figure-annotation-around-table.cs) | Add Figure Annotation Around a Table in PDF | `Document`, `Page`, `Table` | Shows how to create a table in a PDF document and surround it with a figure (square) annotation u... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation Referencing an External Image | `Document`, `Page`, `Rectangle` | Demonstrates how to attach an external image file to a PDF page as a figure (file attachment) ann... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add Figure Annotations with Custom Line Width and Color | `Document`, `Page`, `SquareAnnotation` | Shows how to add square figure annotations with a custom stroke color and line width to selected ... |
| [add-flash-rich-media-annotation](./add-flash-rich-media-annotation.cs) | Add Flash Rich Media Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a Flash (SWF) video in a PDF using a RichMediaAnnotation, configure a c... |
| [add-footer-watermark-annotation](./add-footer-watermark-annotation.cs) | Add Footer Watermark Annotation to PDF Pages | `Document`, `Page`, `Rectangle` | Loads a PDF, iterates through each page, and adds a WatermarkAnnotation in the footer area with a... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a free‑text annotation using Arial 12‑point font, and save the mo... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation that includes a callout line to a PDF page using A... |
| [add-goto-page-button-annotation](./add-goto-page-button-annotation.cs) | Add Go-To Page Button Annotation | `Document`, `ButtonField`, `GoToAction` | Demonstrates how to create a push‑button annotation that, when clicked, navigates to page 10 of t... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to place a highlight annotation on a PDF page and set its opacity to 70% using Aspose.Pdf. |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a highlight annotation with a custom opacity and add it to a PDF page using A... |
| [add-highlight-with-linked-popup-annotation](./add-highlight-with-linked-popup-annotation.cs) | Add Highlight with Linked Popup Annotation | `Document`, `Page`, `HighlightAnnotation` | Loads a PDF, adds a yellow highlight annotation on the first page, creates a popup annotation lin... |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation on a PDF page that jumps to a specific page within the same... |
| [add-javascript-button-annotation](./add-javascript-button-annotation.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to add a button field to the fourth page of a PDF and attach a JavaScript action... |
| [add-javascript-link-annotation-modal-dialog](./add-javascript-link-annotation-modal-dialog.cs) | Add JavaScript Link Annotation to Show Modal Dialog | `Document`, `Page`, `Rectangle` | Demonstrates how to add a link annotation with a JavaScript action that displays a custom modal d... |
| [add-javascript-link-annotation-open-url](./add-javascript-link-annotation-open-url.cs) | Add JavaScript Link Annotation to Open URL | `Document`, `Page`, `Rectangle` | Loads an existing PDF, creates a link annotation with a JavaScript action that opens a specified ... |
| [add-js-link-word-count](./add-js-link-word-count.cs) | Add JavaScript Link Annotation to Show Word Count | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation with a JavaScript action that displays the PDF's total word... |
| ... | | | *and 126 more files* |

## Category Statistics
- Total examples: 156

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

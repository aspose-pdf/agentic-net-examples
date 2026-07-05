---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

> **Working with annotations** in PDF using C# / .NET -- **162** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-annotations** category.
This folder contains standalone C# examples for working-with-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (162/162 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (129/162 files) ← category-specific
- `using Aspose.Pdf.Text;` (16/162 files)
- `using Aspose.Pdf.Forms;` (12/162 files)
- `using Aspose.Pdf.Drawing;` (7/162 files)
- `using Aspose.Pdf.Devices;` (1/162 files)
- `using Aspose.Pdf.Facades;` (1/162 files)
- `using Aspose.Pdf.Operators;` (1/162 files)
- `using System;` (162/162 files)
- `using System.IO;` (155/162 files)
- `using System.Collections.Generic;` (15/162 files)
- `using System.Drawing;` (5/162 files)
- `using System.Linq;` (5/162 files)
- `using System.Text;` (2/162 files)
- `using System.Threading.Tasks;` (2/162 files)
- `using System.Xml.Linq;` (2/162 files)
- `using Azure.Storage.Blobs;` (1/162 files)
- `using System.Net.Http;` (1/162 files)
- `using System.Net.Http.Headers;` (1/162 files)
- `using System.Reflection;` (1/162 files)
- `using System.Text.Json;` (1/162 files)

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
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Front View to PDF | `Document`, `PDF3DContent`, `PDF3DArtwork` | Shows how to embed a 3‑D model (U3D/PRC) into a PDF as a 3‑D annotation, create a front‑view came... |
| [add-3d-annotation-u3d-initial-view-page-two](./add-3d-annotation-u3d-initial-view-page-two.cs) | Add 3D Annotation with U3D Model and Initial View on Page Tw... | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a U3D 3‑D model into a PDF as a 3D annotation, set an initial view, and... |
| [add-3d-annotation-with-custom-camera-view](./add-3d-annotation-with-custom-camera-view.cs) | Add 3D Annotation with Custom Camera View to PDF | `Document`, `Page`, `PDF3DContent` | Demonstrates embedding a U3D/PRC 3D model into a PDF and defining a custom camera angle using Asp... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF as Screen Annotation on PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to place a ScreenAnnotation that displays an animated GIF on the third page of a... |
| [add-arrow-line-annotation-to-pdf](./add-arrow-line-annotation-to-pdf.cs) | Add Arrow Line Annotation to PDF | `Document`, `Page`, `LineAnnotation` | Shows how to create a line annotation with an arrow ending on an existing PDF page using Aspose.Pdf. |
| [add-background-audio-to-pdf-page](./add-background-audio-to-pdf-page.cs) | Add Background Audio to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to embed an MP3 file as a RichMedia annotation that automatically plays when the... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Image as Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a BackgroundArtifact from an image file, marking it as a background, adding... |
| [add-bates-numbering-artifact-to-pdf](./add-bates-numbering-artifact-to-pdf.cs) | Add Bates Numbering Artifact to PDF | `Document`, `AddBatesNumbering`, `BatesNumberingArtifact` | Shows how to add a Bates numbering artifact with a custom "ABC" prefix and a six‑digit format to ... |
| [add-button-annotation-navigate-page-10](./add-button-annotation-navigate-page-10.cs) | Add Button Annotation to Navigate to Page 10 | `Document`, `Page`, `ButtonField` | Shows how to create a button annotation in a PDF that, when activated, jumps to page 10 of the sa... |
| [add-button-export-annotations-json](./add-button-export-annotations-json.cs) | Add Button to Export Annotations as JSON | `Document`, `Page`, `Rectangle` | Shows how to insert a push‑button form field into a PDF that runs JavaScript to collect all annot... |
| [add-button-highlight-text-fields](./add-button-highlight-text-fields.cs) | Add Button to Highlight Text Fields via JavaScript | `Document`, `Rectangle`, `ButtonField` | Shows how to create a push‑button annotation in a PDF that executes JavaScript to highlight all t... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free‑Text Annotation | `Document`, `Page`, `Rectangle` | Demonstrates how to create a free‑text annotation with a callout leader line in a PDF using Aspos... |
| [add-centered-watermark-annotation](./add-centered-watermark-annotation.cs) | Add Centered Watermark Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a WatermarkAnnotation with 30 % opacity at the center of page six of a... |
| [add-dynamic-date-watermark-annotation](./add-dynamic-date-watermark-annotation.cs) | Add Dynamic Date Watermark Annotation to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to add a WatermarkAnnotation with a runtime‑generated date string to each page o... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `WatermarkAnnotation` | Shows how to place a WatermarkAnnotation with a page‑number placeholder that automatically update... |
| [add-external-link-annotation-to-pdf](./add-external-link-annotation-to-pdf.cs) | Add External Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a link annotation that opens an external URL when clicked, by adding i... |
| [add-figure-annotation-rounded-rectangle-around-tab...](./add-figure-annotation-rounded-rectangle-around-table.cs) | Add Rounded-Corner Rectangle Annotation Around a Table | `Document`, `TableAbsorber`, `ITableElement` | The example loads a PDF, detects the first table using TableAbsorber, expands its bounding rectan... |
| [add-figure-annotation-with-external-image](./add-figure-annotation-with-external-image.cs) | Add Figure Annotation with External Image | `Document`, `Page`, `FileSpecification` | Demonstrates how to embed an external image file into a PDF and reference it as a figure (file at... |
| [add-figure-annotations-custom-line-width-color](./add-figure-annotations-custom-line-width-color.cs) | Add Figure Annotations with Custom Line Width and Color | `Document`, `Page`, `SquareAnnotation` | Demonstrates how to programmatically add square figure annotations with a specific border color a... |
| [add-footer-watermark-annotation](./add-footer-watermark-annotation.cs) | Add Footer Watermark Annotation to Each PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, iterating through all pages, and placing a semi‑transparent Watermark... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a free‑text annotation on a PDF page using Aspose.Pdf, specifying Aria... |
| [add-free-text-annotation-light-gray](./add-free-text-annotation-light-gray.cs) | Add Free‑Text Annotation with Light Gray Background | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a FreeTextAnnotation with a light gray background and a simple bo... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a free‑text annotation with a callout line, configuring its appearance, and... |
| [add-gradient-background-artifact](./add-gradient-background-artifact.cs) | Add Gradient Background using BackgroundArtifact | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a PDF, adding a page, and applying a BackgroundArtifact as a page backgroun... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `HighlightAnnotation` | Demonstrates loading a PDF, creating a highlight annotation with 70% opacity, and saving the upda... |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `HighlightAnnotation` | Shows how to load a PDF, create a HighlightAnnotation with a specific color and opacity, attach i... |
| [add-highres-pdf-page-tiled-background](./add-highres-pdf-page-tiled-background.cs) | Add High‑Resolution PDF Page as Tiled Background | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to use a high‑resolution PDF page as a tiled background by creating a Background... |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that jumps to a specific page within the same PDF document ... |
| [add-javascript-button-annotation-to-pdf-page](./add-javascript-button-annotation-to-pdf-page.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `ButtonField`, `JavascriptAction` | Demonstrates how to create a push button on page 4 of a PDF and attach a JavaScript action that s... |
| [add-javascript-link-annotation-open-url](./add-javascript-link-annotation-open-url.cs) | Add JavaScript Link Annotation to Open URL | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation with a JavaScript action that opens a specified URL in a ne... |
| ... | | | *and 132 more files* |

## Category Statistics
- Total examples: 162

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

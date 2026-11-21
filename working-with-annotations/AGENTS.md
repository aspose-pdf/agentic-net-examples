---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

> **Working with annotations** in PDF using C# / .NET -- **161** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-annotations** category.
This folder contains standalone C# examples for working-with-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (161/161 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (129/161 files) ← category-specific
- `using Aspose.Pdf.Forms;` (15/161 files)
- `using Aspose.Pdf.Text;` (14/161 files)
- `using Aspose.Pdf.Drawing;` (6/161 files)
- `using Aspose.Pdf.LogicalStructure;` (1/161 files)
- `using Aspose.Pdf.Tagged;` (1/161 files)
- `using System;` (161/161 files)
- `using System.IO;` (160/161 files)
- `using System.Collections.Generic;` (17/161 files)
- `using System.Reflection;` (10/161 files)
- `using System.Drawing;` (9/161 files)
- `using System.Collections;` (5/161 files)
- `using System.Linq;` (4/161 files)
- `using System.Xml.Linq;` (3/161 files)
- `using System.Text;` (2/161 files)
- `using System.Text.RegularExpressions;` (2/161 files)
- `using System.Threading.Tasks;` (2/161 files)
- `using System.Net.Http;` (1/161 files)
- `using System.Net.Http.Headers;` (1/161 files)
- `using System.Text.Json;` (1/161 files)
- `using System.Xml;` (1/161 files)

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
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Embedded Textures and Front View | `Document`, `LoadAsU3D`, `PDF3DLightingScheme` | Demonstrates how to embed a U3D model that contains textures into a PDF as a 3D annotation and se... |
| [add-3d-annotation-with-custom-camera](./add-3d-annotation-with-custom-camera.cs) | Add 3D Annotation with Custom Camera to PDF | `Document`, `Page`, `PDF3DContent` | Demonstrates how to embed a 3‑D model into a PDF and define a custom camera view using Aspose.Pdf... |
| [add-3d-u3d-annotation-initial-view-page-two](./add-3d-u3d-annotation-initial-view-page-two.cs) | Add 3D U3D Annotation with Initial View on Page Two | `Document`, `Page`, `PDF3DContent` | Demonstrates how to embed a U3D model as a 3D annotation in a PDF, set an initial view, and place... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Animated GIF as Screen Annotation on PDF Page | `Document`, `Page`, `Rectangle` | Shows how to embed an animated GIF in a ScreenAnnotation on the third page of a PDF and save the ... |
| [add-automatic-background-music-to-pdf](./add-automatic-background-music-to-pdf.cs) | Add Sound Annotation with Background Music to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a SoundAnnotation that plays a WAV/AIFF file in a PDF using Aspose.Pdf,... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Image Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to create a BackgroundArtifact from an image file and attach it to a specific page's Ar... |
| [add-bates-numbering-with-prefix__v2](./add-bates-numbering-with-prefix__v2.cs) | Add Bates Numbering Artifact with Prefix to PDF | `Document`, `BatesNArtifact`, `Page` | Demonstrates how to add a Bates numbering artifact with a custom prefix and six‑digit format to e... |
| [add-button-annotation-go-to-page-10](./add-button-annotation-go-to-page-10.cs) | Add GoTo Button to Navigate to Page 10 | `Document`, `ButtonField`, `DefaultAppearance` | Demonstrates how to create a push‑button form field that, when clicked, jumps to page 10 of the s... |
| [add-button-annotation-highlight-text-fields](./add-button-annotation-highlight-text-fields.cs) | Create Button Annotation with JavaScript to Highlight Text F... | `Document`, `Page`, `Rectangle` | Demonstrates how to add a push‑button form field to a PDF and attach a JavaScript action that hig... |
| [add-button-calc-total-pdf](./add-button-calc-total-pdf.cs) | Add JavaScript Button to Calculate Total in PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add an interactive button to a PDF that runs JavaScript to sum two numeric fi... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free‑Text Annotation | `Document`, `Page`, `Rectangle` | Shows how to create a free‑text annotation with a callout leader line on a PDF page using Aspose.... |
| [add-diagonal-watermark-annotation](./add-diagonal-watermark-annotation.cs) | Add Diagonal Watermark to PDF Page | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to place a semi‑transparent, rotated text watermark across a PDF page using Aspo... |
| [add-dynamic-date-watermark-to-pdf-pages](./add-dynamic-date-watermark-to-pdf-pages.cs) | Add Dynamic Date Watermark Annotation to PDF Pages | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to add a WatermarkAnnotation with a runtime‑generated date string to each page o... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Add Dynamic Page Number Watermark to PDF | `Document`, `Page`, `WatermarkAnnotation` | Demonstrates how to create a WatermarkAnnotation with a page‑number placeholder that updates auto... |
| [add-external-url-link-annotation](./add-external-url-link-annotation.cs) | Create Link Annotation to Open URL | `Document`, `Page`, `Rectangle` | Shows how to add a link annotation to a PDF page that opens an external URL when the user clicks ... |
| [add-figure-annotation-around-table](./add-figure-annotation-around-table.cs) | Add Rounded Rectangle Annotation Around a Table | `Document`, `ITaggedContent`, `TableElement` | The example loads a PDF, locates the first table element via tagged content, and adds a square an... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation Using an External Image | `Document`, `Page`, `Rectangle` | Demonstrates how to create a StampAnnotation that displays an external image as its appearance an... |
| [add-footer-watermark-to-pdf-pages](./add-footer-watermark-to-pdf-pages.cs) | Add Unique Watermark Annotation to PDF Footer | `Document`, `Page`, `WatermarkAnnotation` | Loads a PDF, iterates through each page, and adds a WatermarkAnnotation containing a unique GUID ... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to place a free‑text annotation on a PDF page using Aspose.Pdf, specifying Arial... |
| [add-free-text-annotation-light-gray](./add-free-text-annotation-light-gray.cs) | Add Free‑Text Annotation with Gray Background and Rounded Co... | `Document`, `Rectangle`, `DefaultAppearance` | The example loads a PDF, creates a free‑text annotation with a light gray background and rounded ... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `FreeTextAnnotation` | Demonstrates how to create a free‑text annotation with a callout line on a PDF page using Aspose.... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `HighlightAnnotation` | Demonstrates how to add a highlight annotation to a PDF page and set its opacity to 70% for subtl... |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity | `Document`, `Page`, `Rectangle` | Demonstrates how to add a yellow highlight annotation with 50% opacity to a PDF page using Aspose... |
| [add-highres-pdf-page-as-tiled-background](./add-highres-pdf-page-as-tiled-background.cs) | Add Tiled High-Resolution PDF Background Using BackgroundArt... | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to use a high‑resolution PDF page as a tiled background for each page of a new d... |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a link annotation that navigates to a specific page within the same PD... |
| [add-javascript-button-annotation-to-pdf-page](./add-javascript-button-annotation-to-pdf-page.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to place a button form field on page 4 of a PDF and attach a JavaScript action t... |
| [add-javascript-link-annotation-modal-dialog](./add-javascript-link-annotation-modal-dialog.cs) | Add Link Annotation with JavaScript Modal Dialog | `Document`, `Page`, `Rectangle` | Demonstrates how to create a clickable link annotation in a PDF that runs a JavaScript action to ... |
| [add-javascript-link-annotation](./add-javascript-link-annotation.cs) | Add JavaScript Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation with a JavaScript action that opens a URL in a new browser ... |
| [add-magenta-underline-annotation](./add-magenta-underline-annotation.cs) | Add Magenta Underline Annotation to PDF | `Document`, `Page`, `UnderlineAnnotation` | Demonstrates how to add an underline annotation to a PDF page, set its color to magenta and line ... |
| [add-multiline-freetext-annotation-line-spacing](./add-multiline-freetext-annotation-line-spacing.cs) | Add Multiline Free‑Text Annotation with 1.5 Line Spacing | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation containing multiple lines and set its line spacing... |
| ... | | | *and 131 more files* |

## Category Statistics
- Total examples: 161

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
Updated: 2026-11-17 | Run: `20261117_132432_63884a`
<!-- AUTOGENERATED:END -->

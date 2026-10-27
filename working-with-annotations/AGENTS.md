---
name: working-with-annotations
description: C# examples for working-with-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-annotations

> **Working with annotations** in PDF using C# / .NET -- **160** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Annotations;` (126/160 files) ← category-specific
- `using Aspose.Pdf.Forms;` (15/160 files)
- `using Aspose.Pdf.Text;` (14/160 files)
- `using Aspose.Pdf.Drawing;` (11/160 files)
- `using Aspose.Pdf.LogicalStructure;` (2/160 files)
- `using Aspose.Pdf.Tagged;` (2/160 files)
- `using System;` (160/160 files)
- `using System.IO;` (160/160 files)
- `using System.Collections.Generic;` (19/160 files)
- `using System.Reflection;` (13/160 files)
- `using System.Linq;` (7/160 files)
- `using System.Drawing;` (6/160 files)
- `using System.Xml.Linq;` (6/160 files)
- `using System.Collections;` (2/160 files)
- `using System.Threading.Tasks;` (2/160 files)
- `using System.Net.Http;` (1/160 files)
- `using System.Net.Http.Headers;` (1/160 files)
- `using System.Text;` (1/160 files)
- `using System.Text.Json;` (1/160 files)
- `using System.Text.RegularExpressions;` (1/160 files)
- `using System.Xml;` (1/160 files)

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
| [add-3d-annotation-front-view](./add-3d-annotation-front-view.cs) | Add 3D Annotation with Embedded U3D and Front‑View Rotation | `Document`, `Rectangle`, `ThreeDAnnotation` | Demonstrates how to embed a U3D model into a PDF as a 3D annotation, set its initial rotation to ... |
| [add-3d-annotation-with-custom-camera](./add-3d-annotation-with-custom-camera.cs) | Add 3D Annotation with Custom Camera to PDF | `Document`, `Page`, `Rectangle` | Demonstrates embedding a 3‑D model (U3D/PRC) into a PDF as a ThreeDAnnotation and configuring a c... |
| [add-3d-u3d-annotation-initial-view-page-two](./add-3d-u3d-annotation-initial-view-page-two.cs) | Add 3D Annotation with U3D Model and Initial View | `Document`, `LoadAsU3D`, `PDF3DArtwork` | Demonstrates embedding a U3D model as a 3D annotation in a PDF, defining an initial view, and pla... |
| [add-animated-gif-screen-annotation](./add-animated-gif-screen-annotation.cs) | Add Looping Animated GIF as Screen Annotation on PDF Page | `Document`, `Page`, `RichMediaAnnotation` | Demonstrates how to add a RichMedia (Screen) annotation containing an animated GIF to page three ... |
| [add-automatic-background-music-to-pdf](./add-automatic-background-music-to-pdf.cs) | Add Auto-Playing Background Music with SoundAnnotation | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a SoundAnnotation in a PDF that automatically plays background music wh... |
| [add-background-image-artifact-to-pdf-page](./add-background-image-artifact-to-pdf-page.cs) | Add Background Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a BackgroundArtifact, setting its opacity, and adding it to a specific page... |
| [add-bates-numbering-with-prefix__v2](./add-bates-numbering-with-prefix__v2.cs) | Add Bates Numbering Artifact to PDF Pages | `Document`, `Page`, `BatesNArtifact` | Demonstrates how to apply automatic Bates numbering with a custom prefix and six‑digit format to ... |
| [add-button-annotation-go-to-page-10](./add-button-annotation-go-to-page-10.cs) | Add Button Annotation to Navigate to Page 10 | `Document`, `Page`, `Rectangle` | Shows how to insert a button form field into a PDF that, when pressed, jumps to page 10 of the sa... |
| [add-button-annotation-highlight-text-fields](./add-button-annotation-highlight-text-fields.cs) | Add Button to Highlight Text Fields via JavaScript | `Document`, `Rectangle`, `ButtonField` | Shows how to insert a button form field into a PDF that executes JavaScript to highlight all text... |
| [add-button-calc-total-pdf](./add-button-calc-total-pdf.cs) | Create PDF Button Annotation with JavaScript to Sum Form Fie... | `Document`, `Form`, `Page` | Demonstrates how to add a button field to a PDF, attach a JavaScript action that sums numeric for... |
| [add-callout-leader-line-to-freetext-annotation](./add-callout-leader-line-to-freetext-annotation.cs) | Add Callout Leader Line to Free‑Text Annotation | `Document`, `Save`, `Page` | Shows how to create a free‑text annotation with a callout (leader line) and add it to a PDF page ... |
| [add-dynamic-date-watermark-to-pdf-pages](./add-dynamic-date-watermark-to-pdf-pages.cs) | Add Dynamic Date Watermark Annotation to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates adding a WatermarkAnnotation with a runtime‑generated date string to each page of a ... |
| [add-dynamic-page-number-watermark](./add-dynamic-page-number-watermark.cs) | Create Dynamic Page Number Watermark Annotation | `Document`, `Page`, `WatermarkAnnotation` | Shows how to add a WatermarkAnnotation with page‑number placeholders that automatically update on... |
| [add-external-url-link-annotation](./add-external-url-link-annotation.cs) | Add External Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that opens an external URL when the user clicks the annotat... |
| [add-figure-annotation-around-table](./add-figure-annotation-around-table.cs) | Add Figure Annotation with Rounded Rectangle Around a Table | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to locate a table in a tagged PDF and add a square annotation (rectangle) around... |
| [add-figure-annotation-external-image](./add-figure-annotation-external-image.cs) | Add Figure Annotation with External Image to Tagged PDF | `Document`, `ITaggedContent`, `FigureElement` | Demonstrates creating a Figure element in a tagged PDF and linking it to an external image file a... |
| [add-footer-watermark-to-pdf-pages](./add-footer-watermark-to-pdf-pages.cs) | Add Footer Watermark Annotation to PDF Pages | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, iterating through each page, and adding a WatermarkAnnotation in the ... |
| [add-free-text-annotation-arial](./add-free-text-annotation-arial.cs) | Add Free‑Text Annotation with Arial Font to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation to a PDF page using Aspose.Pdf, specifying Arial f... |
| [add-free-text-annotation-light-gray](./add-free-text-annotation-light-gray.cs) | Add Free‑Text Annotation with Light Gray Background | `Document`, `Page`, `Rectangle` | Shows how to insert a free‑text annotation on a PDF page using Aspose.Pdf, set its background to ... |
| [add-free-text-annotation-with-callout](./add-free-text-annotation-with-callout.cs) | Add Free‑Text Annotation with Callout to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a free‑text annotation, set its appearance, define a callout, and add ... |
| [add-highlight-annotation-70-opacity](./add-highlight-annotation-70-opacity.cs) | Add Highlight Annotation with 70% Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a highlight annotation, set its color and 70% opacity, add it to ... |
| [add-highlight-annotation-with-opacity](./add-highlight-annotation-with-opacity.cs) | Add Highlight Annotation with Custom Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a highlight annotation with a specific rectangle, set its color a... |
| [add-internal-link-annotation-to-pdf](./add-internal-link-annotation-to-pdf.cs) | Add Internal Page Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation that navigates to a specific page within the same PDF using... |
| [add-javascript-button-annotation-to-pdf-page](./add-javascript-button-annotation-to-pdf-page.cs) | Add JavaScript Button Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to add a button field to the fourth page of a PDF and attach a JavaScript action... |
| [add-javascript-link-annotation-modal-dialog](./add-javascript-link-annotation-modal-dialog.cs) | Add JavaScript Link Annotation to Show Modal Dialog | `Document`, `Page`, `Rectangle` | Demonstrates how to create a link annotation with a JavaScript action that displays a custom moda... |
| [add-javascript-link-annotation](./add-javascript-link-annotation.cs) | Create JavaScript Link Annotation to Open a URL | `Document`, `Page`, `Rectangle` | The example loads an existing PDF, creates a link annotation with a JavaScript action that opens ... |
| [add-magenta-underline-annotation](./add-magenta-underline-annotation.cs) | Add Magenta Underline Annotation to PDF | `Document`, `Page`, `UnderlineAnnotation` | Demonstrates how to add an underline annotation with magenta color and a 2‑point border to a PDF ... |
| [add-multiline-freetext-annotation-line-spacing](./add-multiline-freetext-annotation-line-spacing.cs) | Add Multiline Free‑Text Annotation with Line Spacing | `Document`, `Page`, `Rectangle` | The example loads a PDF, creates a free‑text annotation containing multiple lines, sets its backg... |
| [add-polyline-figure-annotation](./add-polyline-figure-annotation.cs) | Add Figure Annotation with Custom Polyline to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a figure annotation using a custom polyline shape to a PDF page with Aspo... |
| [add-polyline-figure-annotation__v2](./add-polyline-figure-annotation__v2.cs) | Add Polyline Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a polyline annotation from a list of points, calculating its bounding recta... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-25 | Run: `20261025_232257_0da171`
<!-- AUTOGENERATED:END -->

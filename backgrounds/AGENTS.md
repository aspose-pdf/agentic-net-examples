---
name: backgrounds
description: C# examples for backgrounds using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - backgrounds

> **Backgrounds** in PDF using C# / .NET -- **30** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **backgrounds** category.
This folder contains standalone C# examples for backgrounds operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **backgrounds**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (30/30 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (5/30 files)
- `using Aspose.Pdf.Annotations;` (2/30 files)
- `using System;` (30/30 files)
- `using System.IO;` (29/30 files)
- `using System.Collections.Generic;` (3/30 files)

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
| [add-background-image-first-page-blank-page](./add-background-image-first-page-blank-page.cs) | Add Background Image to First Page and Append Blank Page | `Document`, `Page`, `ImageStamp` | Loads an existing PDF (or creates a new one), applies a background image to the first page using ... |
| [add-background-image-to-first-pdf-page](./add-background-image-to-first-pdf-page.cs) | Add Background Image to First PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to place a full‑page background image on the first page of a newly created PDF document... |
| [add-background-image-to-first-pdf-page__v2](./add-background-image-to-first-pdf-page__v2.cs) | Add Background Image to First PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates loading an image stream, creating a BackgroundArtifact, assigning it to the first pa... |
| [add-background-image-to-last-page](./add-background-image-to-last-page.cs) | Add Background Image to Last Page of PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to attach a background image to the final page of a six‑page PDF using Aspose.Pd... |
| [add-background-image-to-page-three](./add-background-image-to-page-three.cs) | Add Background Image to Specific PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to place a background image on the third page of an existing PDF while keeping all othe... |
| [add-background-image-to-pdf-page](./add-background-image-to-pdf-page.cs) | Add Background Image to PDF Page from Stream | `Document`, `Page`, `Image` | Demonstrates how to add a new page to a PDF, set its background image using a file stream, and sa... |
| [add-background-image-to-pdf-page__v2](./add-background-image-to-pdf-page__v2.cs) | Add Background Image to PDF Page | `Document`, `Page`, `Add` | Demonstrates how to load an image stream, create a BackgroundArtifact, assign the image as a back... |
| [add-background-image-to-pdf-page__v3](./add-background-image-to-pdf-page__v3.cs) | Add Background Image to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a BackgroundArtifact, assigning an image stream as its background, attachin... |
| [add-background-image-to-pdf-page__v4](./add-background-image-to-pdf-page__v4.cs) | Add Background Image to PDF Page Using a Stream | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to create a BackgroundArtifact, set its BackgroundImage from a file stream, atta... |
| [add-background-image-to-pdf-page__v5](./add-background-image-to-pdf-page__v5.cs) | Add Background Image to a PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to load an image stream, configure a BackgroundArtifact, attach it to the fourth page o... |
| [add-background-image-to-pdf-page__v6](./add-background-image-to-pdf-page__v6.cs) | Add Background Image to PDF Page Using Stream | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a BackgroundArtifact, assigning an image stream as its BackgroundImage, att... |
| [add-background-image-to-pdf-page__v7](./add-background-image-to-pdf-page__v7.cs) | Add Background Image to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to create a BackgroundArtifact, set its BackgroundImage from a memory stream, attach it... |
| [add-background-image-to-pdf-page__v8](./add-background-image-to-pdf-page__v8.cs) | Add Background Image to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to open an existing PDF, attach a background image to the third page using a Bac... |
| [add-background-image-watermark-to-pdf](./add-background-image-watermark-to-pdf.cs) | Add Background Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to apply a background image as a watermark to each page of a PDF using Aspose.Pd... |
| [add-decorative-background-image-to-pdf-page](./add-decorative-background-image-to-pdf-page.cs) | Add Decorative Background Image to PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates how to place an image as a background on the first page of a PDF using Aspose.Pdf an... |
| [add-different-background-images-to-pdf-pages](./add-different-background-images-to-pdf-pages.cs) | Add Different Background Images to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to load image streams, create BackgroundArtifact objects for each, assign them to separ... |
| [add-different-background-images-to-pdf-pages__v2](./add-different-background-images-to-pdf-pages__v2.cs) | Add Different Background Images to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to load image files as BackgroundArtifact objects, assign each to a separate PDF page, ... |
| [add-different-background-images-to-pdf-pages__v3](./add-different-background-images-to-pdf-pages__v3.cs) | Add Different Background Images to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to apply a background image to the first page and a different background image to the s... |
| [add-image-background-to-pdf-pages](./add-image-background-to-pdf-pages.cs) | Add Image Background to PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Shows how to load an image into memory, create BackgroundArtifact objects, assign them as page ba... |
| [add-same-background-image-to-multiple-pdf-pages](./add-same-background-image-to-multiple-pdf-pages.cs) | Add Same Background Image to Multiple PDF Pages | `Document`, `Page`, `BackgroundArtifact` | Demonstrates creating a three‑page PDF and attaching the same background image to each page using... |
| [apply-background-image-to-first-three-pdf-pages](./apply-background-image-to-first-three-pdf-pages.cs) | Apply Background Image to First Three PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to attach a background image to the first three pages of a PDF document and save... |
| [create-pdf-unique-background-per-page](./create-pdf-unique-background-per-page.cs) | Create PDF with Unique Background Image per Page | `Document`, `Page`, `ImageStamp` | Shows how to generate a PDF, add five pages, set a different background image on each page, and s... |
| [create-pdf-with-image-background](./create-pdf-with-image-background.cs) | Create PDF with Image Background Using BackgroundArtifact | `Document`, `Page`, `BackgroundArtifact` | Shows how to load an image into a memory stream, create BackgroundArtifact objects, apply the sam... |
| [set-different-background-colors-for-pdf-pages](./set-different-background-colors-for-pdf-pages.cs) | Set Different Background Colors for PDF Pages | `Document`, `Page`, `Add` | Shows how to create a PDF document, add two pages, and assign distinct background colors to each ... |
| [set-different-background-images-for-pdf-pages](./set-different-background-images-for-pdf-pages.cs) | Set Different Background Images for PDF Pages | `Document`, `Add`, `BackgroundImage` | Demonstrates how to add three pages to a PDF and assign a distinct background image to each page ... |
| [set-page-background-image-from-stream](./set-page-background-image-from-stream.cs) | Set PDF Page Background Image from Stream | `Document`, `Page`, `Image` | Demonstrates how to create a PDF, add a page, load an image from a file stream, assign it as the ... |
| [set-page-background-image-from-stream__v2](./set-page-background-image-from-stream__v2.cs) | Set Page Background Image from Stream | `Document`, `Page`, `Image` | Shows how to create a PDF document, add a page, load an image from a file stream and assign it as... |
| [set-page-background-image](./set-page-background-image.cs) | Set Page Background Image in PDF | `Document`, `Page`, `Image` | Demonstrates loading an image file and assigning it as the background image of a PDF page using A... |
| [set-page-background-image__v2](./set-page-background-image__v2.cs) | Set Page Background Image with BackgroundArtifact | `Document`, `Page`, `BackgroundArtifact` | Demonstrates how to load an image stream, create a BackgroundArtifact, assign the image as a page... |
| [set-pdf-page-background-image-from-stream](./set-pdf-page-background-image-from-stream.cs) | Set PDF Page Background Image from Stream | `Document`, `Page`, `Image` | Shows how to open an image file as a stream, assign it to a page's BackgroundImage property, and ... |

## Category Statistics
- Total examples: 30

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for backgrounds patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_031241_c7a5e6`
<!-- AUTOGENERATED:END -->

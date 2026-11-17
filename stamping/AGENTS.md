---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

> **Stamping** in PDF using C# / .NET -- **48** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **stamping** category.
This folder contains standalone C# examples for stamping operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **stamping**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (47/48 files) ← category-specific
- `using Aspose.Pdf.Text;` (13/48 files)
- `using Aspose.Pdf.Annotations;` (6/48 files)
- `using Aspose.Pdf.Drawing;` (5/48 files)
- `using Aspose.Pdf.Devices;` (1/48 files)
- `using Aspose.Pdf.Facades;` (1/48 files)
- `using System;` (48/48 files)
- `using System.IO;` (48/48 files)

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
| [add-background-image-stamp-to-pdf](./add-background-image-stamp-to-pdf.cs) | Add Background Image Stamp to PDF Page | `Document`, `Page`, `ImageStamp` | Shows how to place an image stamp behind the existing content of a PDF page by setting the Backgr... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outline Text Stamp to PDF | `Document`, `TextStamp`, `TextState` | Shows how to place a text stamp with a simulated bold outline on every page of a PDF using Aspose... |
| [add-bottom-left-text-stamp](./add-bottom-left-text-stamp.cs) | Align Text Stamp to Bottom-Left Corner | `Document`, `TextStamp`, `HorizontalAlignment` | Demonstrates adding a TextStamp to each page of a PDF and positioning it at the bottom‑left corne... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Semi-Transparent Confidential Text Stamp to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to load a PDF, create a TextStamp with 0.6 opacity, and apply it to every page b... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Image Stamp with Custom Size to Specific PDF Page | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp with custom width and height on a chosen page of a PDF u... |
| [add-custom-text-stamp-to-pdf-first-page](./add-custom-text-stamp-to-pdf-first-page.cs) | Add Blue Text Stamp to First PDF Page | `Document`, `TextStamp`, `FindFont` | Demonstrates how to create a text stamp with a custom font, size, and blue color, and apply it on... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `Rotation` | Demonstrates how to apply a rotated image stamp as a diagonal watermark on each page of a PDF usi... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates loading a PDF, creating a semi‑transparent TextStamp rotated 45°, configuring its ap... |
| [add-faint-overlay-stamp-to-pdf-pages](./add-faint-overlay-stamp-to-pdf-pages.cs) | Add Faint Image Overlay to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp with 40% opacity as a faint overlay on every page of a P... |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Text Watermark to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to apply a semi‑transparent, diagonal text stamp that covers the entire page as ... |
| [add-global-text-stamp-to-all-pdf-pages](./add-global-text-stamp-to-all-pdf-pages.cs) | Add Image Stamp to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF with Aspose.Pdf, create an ImageStamp, and apply it to every page ... |
| [add-image-stamp-alt-text-pdfa](./add-image-stamp-alt-text-pdfa.cs) | Add Image Stamp with Alternative Text for PDF/A‑1b | `Document`, `ImageStamp`, `AddStamp` | Demonstrates stamping an image on each page, setting alternative text for accessibility, and conv... |
| [add-image-stamp-annotation-to-pdf](./add-image-stamp-annotation-to-pdf.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp with transparency, applying it to every page, ... |
| [add-image-stamp-flatten-annotations](./add-image-stamp-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to stamp an image onto every page of a PDF and then flatten all annotations so t... |
| [add-image-stamp-from-memory-stream-to-pdf](./add-image-stamp-from-memory-stream-to-pdf.cs) | Add Image Stamp from Memory Stream to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load an image into a MemoryStream and apply it as an ImageStamp to every page... |
| [add-image-stamp-percentage-position](./add-image-stamp-percentage-position.cs) | Add Image Stamp with Percentage Offsets to PDF Pages | `Document`, `ImageStamp`, `Page` | Shows how to place an image stamp on each page of a PDF using X and Y offsets expressed as percen... |
| [add-image-stamp-preserve-acroform-fields](./add-image-stamp-preserve-acroform-fields.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Shows how to apply an image stamp to every page of a PDF using Aspose.Pdf and keep the existing A... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks | `Document`, `Page`, `ImageStamp` | Shows how to overlay an image stamp on every page of a PDF using Aspose.Pdf and keep the existing... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF that contains embedded files, applying an image stamp to each page, an... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript Actions | `Document`, `ImageStamp`, `AddStamp` | The example loads an existing PDF, creates an ImageStamp from a PNG file, places it at the bottom... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp to PDF While Preserving Page Labels | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay an image stamp on every page of a PDF using Aspose.Pdf, keeping exist... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp While Preserving XMP Metadata | `Document`, `GetXmpMetadata`, `SetXmpMetadata` | Demonstrates loading a PDF, extracting its XMP metadata, applying an image stamp to every page, a... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates opening an encrypted PDF with a user password, decrypting it, applying an image stam... |
| [add-image-stamp-to-pdf-form](./add-image-stamp-to-pdf-form.cs) | Add Image Stamp to PDF Form While Preserving Fields | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF containing form fields, place an image stamp on each page, and save the d... |
| [add-image-stamp-to-pdf-page](./add-image-stamp-to-pdf-page.cs) | Add Image Stamp to PDF Page with Quality and Opacity | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp on the second page of a PDF using Aspose.Pdf, setting th... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF Without Invalidating Signature | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp on an existing digitally signed PDF using Aspose.Pdf whi... |
| [add-image-stamp-unicode-alt-text](./add-image-stamp-unicode-alt-text.cs) | Add Image Stamp with Multilingual Alternative Text | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp on each page of a PDF and set Unicode alternative text f... |
| [add-image-stamp-with-alt-text-to-pdf-page](./add-image-stamp-with-alt-text-to-pdf-page.cs) | Add Image Stamp with Alt Text to PDF Page 3 | `Document`, `Page`, `ImageStamp` | Demonstrates how to place an image stamp on the third page of a PDF and assign alternative text t... |
| [add-low-quality-image-stamp-to-pdf](./add-low-quality-image-stamp-to-pdf.cs) | Add Low‑Quality Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to apply a low‑quality (10 % JPEG) image stamp to every page of a PDF using Aspose.Pdf,... |
| [add-multi-line-text-stamp](./add-multi-line-text-stamp.cs) | Add Multi-Line Disclaimer Text Stamp with Custom Line Spacin... | `Document`, `TextStamp`, `TextState` | Shows how to place a multi-line disclaimer as a text stamp on every page of a PDF and adjust its ... |
| ... | | | *and 18 more files* |

## Category Statistics
- Total examples: 48

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-17 | Run: `20261117_050229_ebe501`
<!-- AUTOGENERATED:END -->

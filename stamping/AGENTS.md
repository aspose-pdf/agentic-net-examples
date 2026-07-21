---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

> **Stamping** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **stamping** category.
This folder contains standalone C# examples for stamping operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **stamping**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (50/50 files) ← category-specific
- `using Aspose.Pdf.Text;` (12/50 files)
- `using Aspose.Pdf.Annotations;` (5/50 files)
- `using Aspose.Pdf.Facades;` (5/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)

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
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates how to place a text stamp with fill‑stroke rendering on each page of a PDF, creating... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add a semi‑transparent 'CONFIDENTIAL' text stamp to each page of a PDF using ... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Custom-Sized Page Stamp to Specific PDF Page | `Document`, `Page`, `PdfPageStamp` | Shows how to create a page stamp from an existing PDF page, set custom width, height and position... |
| [add-custom-text-stamp-to-pdf](./add-custom-text-stamp-to-pdf.cs) | Add Custom Text Stamp to PDF | `Document`, `TextState`, `FontRepository` | Demonstrates how to place a text stamp with a custom font, size, and blue color on the first page... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `Rotation` | Demonstrates how to apply an image stamp rotated 90 degrees as a diagonal watermark on every page... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to add a semi‑transparent diagonal "CONFIDENTIAL" text stamp to each page of a P... |
| [add-faint-text-overlay-stamp-to-pdf-pages](./add-faint-text-overlay-stamp-to-pdf-pages.cs) | Add Faint Text Overlay Stamp to PDF Pages | `Document`, `TextStamp`, `FindFont` | Shows how to apply a semi‑transparent text stamp as a faint overlay on every page of a PDF using ... |
| [add-fixed-size-image-stamp-to-pdf-pages](./add-fixed-size-image-stamp-to-pdf-pages.cs) | Add Fixed-Size Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp with a fixed width and height on every page of a PDF, independe... |
| [add-full-page-image-watermark](./add-full-page-image-watermark.cs) | Add Full-Page Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay an image stamp as a semi‑transparent background watermark that covers... |
| [add-fully-opaque-image-watermark](./add-fully-opaque-image-watermark.cs) | Add Fully Opaque Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with full opacity, center it, and apply it to every... |
| [add-image-background-floatingbox-page-4](./add-image-background-floatingbox-page-4.cs) | Add Image Background to FloatingBox on Page 4 | `Document`, `Page`, `FloatingBox` | Loads a PDF, creates an Image from a PNG file, sets it as the background of a FloatingBox, and in... |
| [add-image-stamp-alt-text-pdfa1b](./add-image-stamp-alt-text-pdfa1b.cs) | Add Image Stamp with Alternative Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp with alternative (accessibility) text on each page of a PDF and... |
| [add-image-stamp-as-background-to-pdf-pages](./add-image-stamp-as-background-to-pdf-pages.cs) | Add Image Stamp as Background to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp, set its Background property to true, and apply it ... |
| [add-image-stamp-flatten-annotations](./add-image-stamp-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp on a PDF page and then flatten all annotations, resulting in a ... |
| [add-image-stamp-from-memory-stream](./add-image-stamp-from-memory-stream.cs) | Add Image Stamp from Memory Stream to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load an image into a MemoryStream and use Aspose.Pdf to stamp that image onto... |
| [add-image-stamp-preserve-acroform-fields](./add-image-stamp-preserve-acroform-fields.cs) | Add Image Stamp to PDF While Preserving AcroForm Fields | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, iterating its pages, applying an image stamp with alignment and opaci... |
| [add-image-stamp-preserve-annotations](./add-image-stamp-preserve-annotations.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `ImageStamp`, `Page` | Demonstrates loading a PDF, creating an ImageStamp, positioning it on the first page, and saving ... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks and Outline | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp to every page of a PDF using Aspose.Pdf while keeping exis... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF, add a semi‑transparent image stamp to each page, and save the doc... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript Actions | `Document`, `ImageStamp`, `AddStamp` | Demonstrates adding an image stamp to every page of a PDF using Aspose.Pdf while keeping any exis... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp to PDF While Preserving Page Labels | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, apply the same image stamp to every page, and save the document w... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp While Preserving XMP Metadata | `Document`, `Page`, `ImageStamp` | Shows how to place an image stamp on each page of a PDF and keep the original XMP metadata intact... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `ImageStamp`, `HorizontalAlignment` | Demonstrates opening a password‑protected PDF, applying an image stamp with custom appearance, an... |
| [add-image-stamp-to-page-two](./add-image-stamp-to-page-two.cs) | Add Image Stamp to Page Two | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with 100 % quality and 0.8 opacity, and apply it to... |
| [add-image-stamp-to-pdf-with-form-fields](./add-image-stamp-to-pdf-with-form-fields.cs) | Add Image Stamp to PDF with Form Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates adding an image stamp to each page of a PDF that contains interactive form fields wh... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF without Invalidating Signature | `Document`, `ImageStamp`, `PdfSaveOptions` | Shows how to place an image stamp on an already digitally signed PDF while preserving the existin... |
| [add-image-stamp-top-right-corner](./add-image-stamp-top-right-corner.cs) | Add Image Stamp to Top‑Right Corner of PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp (e.g., a logo) at the top‑right corner of each page in a... |
| [add-image-stamp-with-alt-text-to-pdf-page](./add-image-stamp-with-alt-text-to-pdf-page.cs) | Add Image Stamp with Alt Text to PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp on the third page of a PDF and set alternative text for ... |
| [add-image-stamp-with-unicode-alt-text](./add-image-stamp-with-unicode-alt-text.cs) | Add Image Stamp with Unicode Alternative Text to PDF | `Document`, `ImageStamp`, `AlternativeText` | Demonstrates how to add an image stamp to a PDF using Aspose.Pdf, set a Unicode alternative text ... |
| [add-low-quality-image-stamp-to-pdf](./add-low-quality-image-stamp-to-pdf.cs) | Add Low-Quality Image Stamp to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with reduced quality (10 %) and optional visual set... |
| ... | | | *and 20 more files* |

## Category Statistics
- Total examples: 50

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

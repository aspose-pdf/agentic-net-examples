---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

> **Stamping** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (11/50 files)
- `using Aspose.Pdf.Annotations;` (9/50 files)
- `using Aspose.Pdf.Drawing;` (5/50 files)
- `using Aspose.Pdf.Facades;` (2/50 files)
- `using Aspose.Pdf.Tagged;` (1/50 files)
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
| [add-auto-adjusting-text-stamp-to-pdf](./add-auto-adjusting-text-stamp-to-pdf.cs) | Add Auto-Adjusting Text Stamp to PDF Pages | `Document`, `TextStamp`, `AutoAdjustFontSizeToFitStampRectangle` | Demonstrates how to add a text stamp to each page of a PDF and automatically adjust its font size... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `TextState` | Demonstrates how to place a centered text stamp with a bold font to simulate an outlined (fill‑st... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp with Opacity to PDF | `Document`, `Page`, `TextStamp` | Demonstrates loading a PDF, creating a TextStamp with 0.6 opacity, and applying it to every page ... |
| [add-custom-page-stamp-to-pdf](./add-custom-page-stamp-to-pdf.cs) | Add Custom Page Stamp with Specific Size and Position | `Document`, `Page`, `PdfPageStamp` | Demonstrates how to create a PdfPageStamp, set custom width, height, and placement coordinates, a... |
| [add-custom-text-stamp-to-first-page](./add-custom-text-stamp-to-first-page.cs) | Add Custom Text Stamp to First PDF Page | `Document`, `TextState`, `FontRepository` | Shows how to place a text stamp with a custom font, size, and blue color on the first page of a P... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to place a rotated image stamp as a diagonal, semi‑transparent watermark on each page o... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates how to apply a semi‑transparent, 45‑degree rotated text stamp as a diagonal watermar... |
| [add-fixed-size-image-stamp-to-pdf](./add-fixed-size-image-stamp-to-pdf.cs) | Add Fixed-Size Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a fixed-dimension image stamp on every page of a PDF, independent of pa... |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to apply an image stamp as a full‑page background watermark on every page of a P... |
| [add-fully-opaque-image-stamp-watermark](./add-fully-opaque-image-stamp-watermark.cs) | Add Fully Opaque Image Stamp Watermark to PDF | `Document`, `ImageStamp`, `Opacity` | Shows how to load a PDF, create an ImageStamp from a PNG, set its opacity to 1.0, center it, and ... |
| [add-image-background-floatingbox-page-4](./add-image-background-floatingbox-page-4.cs) | Add Image Background to Page Using FloatingBox | `Document`, `Page`, `FloatingBox` | Demonstrates how to place an image as a background inside a FloatingBox that covers the entire fo... |
| [add-image-stamp-alt-text-page-3](./add-image-stamp-alt-text-page-3.cs) | Add Image Stamp with Alt Text to PDF Page 3 | `Document`, `ImageStamp`, `Page` | Demonstrates loading a PDF, creating an ImageStamp with alternative text for accessibility, posit... |
| [add-image-stamp-and-flatten-annotations](./add-image-stamp-and-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `ImageStamp`, `AddStamp` | Loads a PDF, adds an image stamp to the first page, flattens all annotations so they become part ... |
| [add-image-stamp-as-background-to-pdf](./add-image-stamp-as-background-to-pdf.cs) | Add Image Stamp as Background to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp behind the content of each page in a PDF by setting the ImageSt... |
| [add-image-stamp-from-memory-stream](./add-image-stamp-from-memory-stream.cs) | Add Image Stamp from Memory Stream to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to load an image into a MemoryStream and apply it as an ImageStamp to every page... |
| [add-image-stamp-percentage-position](./add-image-stamp-percentage-position.cs) | Add Image Stamp with Percentage Positioning | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp on each PDF page using offsets expressed as percentages ... |
| [add-image-stamp-preserve-acroform](./add-image-stamp-preserve-acroform.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, apply an image stamp to every page, and save the document while keeping ... |
| [add-image-stamp-preserve-annotations](./add-image-stamp-preserve-annotations.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `ImageStamp`, `HorizontalAlignment` | Demonstrates loading an existing PDF, creating an image stamp with alignment and opacity, adding ... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks | `Document`, `ImageStamp`, `AddStamp` | Shows how to add an image stamp to every page of an existing PDF using Aspose.Pdf while keeping t... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp to every page of a PDF and keep any embedded files intac... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript Actions | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a semi‑transparent image stamp on every page of a PDF using Aspose.Pdf ... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp to PDF While Preserving Page Labels | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF, create an ImageStamp, place it on every page, and save the docume... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp While Preserving XMP Metadata | `Document`, `GetXmpMetadata`, `SetXmpMetadata` | Shows how to place an image stamp on every page of a PDF and keep the original XMP metadata intac... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to open an encrypted PDF with a password, apply an image stamp to every page, and save ... |
| [add-image-stamp-to-pdf-page](./add-image-stamp-to-pdf-page.cs) | Add Image Stamp to PDF Page with Quality and Opacity | `Document`, `ImageStamp`, `Page` | Shows how to place an image stamp on the second page of a PDF, setting the stamp to 100 % quality... |
| [add-image-stamp-to-pdf-preserve-form-fields](./add-image-stamp-to-pdf-preserve-form-fields.cs) | Add Image Stamp to PDF While Preserving Form Fields | `Document`, `ImageStamp`, `AddStamp` | Loads a PDF containing form fields, creates a semi‑transparent ImageStamp from a PNG, positions i... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF without Invalidating Signature | `Document`, `ImageStamp`, `Page` | Demonstrates loading a digitally signed PDF, adding a semi‑transparent image stamp to the first p... |
| [add-image-stamp-top-right-corner](./add-image-stamp-top-right-corner.cs) | Add Image Stamp to Top‑Right Corner of PDF | `Document`, `ImageStamp`, `HorizontalAlignment` | Shows how to place an image stamp (e.g., a logo) in the top‑right corner of every page of a PDF w... |
| [add-image-stamp-with-alt-text-pdfa1b](./add-image-stamp-with-alt-text-pdfa1b.cs) | Add Image Stamp with Alternative Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `AddStamp` | Demonstrates adding an image stamp with alternative (alt) text to each page of a PDF and converti... |
| [add-image-stamp-with-opacity-to-pdf-pages](./add-image-stamp-with-opacity-to-pdf-pages.cs) | Add Image Stamp with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay an image stamp with 40% opacity on every page of a PDF using Aspose.Pdf. |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

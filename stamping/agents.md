---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

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
- `using Aspose.Pdf.Text;` (10/50 files)
- `using Aspose.Pdf.Annotations;` (6/50 files)
- `using Aspose.Pdf.Drawing;` (1/50 files)
- `using Aspose.Pdf.Facades;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)
- `using System.Drawing;` (1/50 files)
- `using System.Runtime.InteropServices;` (1/50 files)

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
| [add-auto-adjusting-text-stamp](./add-auto-adjusting-text-stamp.cs) | Add Auto‑Adjusting Text Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Demonstrates how to place a text stamp on each page of a PDF and automatically adjust its font si... |
| [add-background-image-stamp-to-pdf-pages](./add-background-image-stamp-to-pdf-pages.cs) | Add Background Image Stamp to PDF Pages | `Document`, `ImageStamp`, `Background` | Demonstrates how to place an image stamp behind the content of each page in a PDF by setting the ... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Shows how to place a TextStamp with fill‑stroke rendering to create bold outlined text on a PDF p... |
| [add-bottom-left-text-stamp](./add-bottom-left-text-stamp.cs) | Add Bottom‑Left Text Stamp with Margin to PDF | `Document`, `TextStamp`, `Page` | Demonstrates loading a PDF with Aspose.Pdf, creating a TextStamp, aligning it to the bottom‑left ... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating a semi‑transparent "CONFIDENTIAL" TextStamp,... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Custom-Sized Page Stamp to PDF | `Document`, `PdfPageStamp`, `AddStamp` | Demonstrates how to create a page stamp from another PDF, set custom width, height, and position,... |
| [add-custom-text-stamp-to-first-pdf-page](./add-custom-text-stamp-to-first-pdf-page.cs) | Add Custom Text Stamp to First PDF Page | `Document`, `TextState`, `FindFont` | Demonstrates how to create a text stamp with a specific font, size, and blue color, and apply it ... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp rotated 90° as a diagonal watermark on every page of a P... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to apply a semi‑transparent, 45° rotated text stamp as a diagonal watermark on e... |
| [add-fixed-size-image-stamp-to-pdf](./add-fixed-size-image-stamp-to-pdf.cs) | Add Fixed-Size Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a PNG image stamp with constant width and height on every page of a PDF... |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to apply an image stamp as a semi‑transparent background watermark that covers t... |
| [add-image-background-floatingbox-page-4](./add-image-background-floatingbox-page-4.cs) | Add Image Background to Page 4 Using FloatingBox | `Document`, `Page`, `FloatingBox` | Shows how to place an image as the background of a FloatingBox that covers the entire fourth page... |
| [add-image-stamp-alt-text-page-3](./add-image-stamp-alt-text-page-3.cs) | Add Image Stamp with Alt Text to PDF Page 3 | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp with alternative text for accessibility on the third page of a ... |
| [add-image-stamp-alt-text-pdfa1b](./add-image-stamp-alt-text-pdfa1b.cs) | Add Image Stamp with Alt Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `AddStamp` | Shows how to add an image stamp with alternative text only when creating a PDF/A‑1b document and ... |
| [add-image-stamp-flatten-annotations](./add-image-stamp-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `AddStamp`, `ImageStamp` | Demonstrates how to add an image stamp to every page of a PDF and then flatten all annotations so... |
| [add-image-stamp-from-memory-stream](./add-image-stamp-from-memory-stream.cs) | Add Image Stamp from Memory Stream to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to create an ImageStamp from a MemoryStream and apply it to each page of a PDF d... |
| [add-image-stamp-preserve-acroform-fields](./add-image-stamp-preserve-acroform-fields.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place a semi‑transparent image stamp on every page of a PDF using Aspose.Pdf ... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp to PDF While Preserving Bookmarks | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading an existing PDF, creating an ImageStamp, applying it to every page, and savi... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp and Preserve Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Loads a PDF containing embedded files, adds an image stamp to each page, and saves the document w... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp While Preserving JavaScript Actions | `Document`, `Page`, `ImageStamp` | Shows how to place an image stamp on every page of a PDF using Aspose.Pdf without removing existi... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp to PDF While Preserving Page Labels | `Document`, `ImageStamp`, `Page` | Demonstrates how to add a semi‑transparent image stamp to every page of a PDF using Aspose.Pdf an... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp and Preserve XMP Metadata in PDF | `Document`, `GetXmpMetadata`, `SetXmpMetadata` | Shows how to place an image stamp on every page of a PDF while retaining the original XMP metadat... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to open an encrypted PDF with a password, decrypt it, apply an image stamp to every pag... |
| [add-image-stamp-to-pdf-form](./add-image-stamp-to-pdf-form.cs) | Add Image Stamp to PDF Form While Preserving Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with form fields, creating an ImageStamp, positioning it, and applying... |
| [add-image-stamp-to-pdf-page](./add-image-stamp-to-pdf-page.cs) | Add Image Stamp with Quality and Opacity to PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp, setting its quality to 100 % and opacity to 0... |
| [add-image-stamp-to-pdf-preserving-annotations](./add-image-stamp-to-pdf-preserving-annotations.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF, creating an ImageStamp, positioning it, and adding it to a page witho... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF without Invalidating Signature | `Document`, `ImageStamp`, `HorizontalAlignment` | Shows how to load a digitally signed PDF, place an image stamp on a page, and save the document u... |
| [add-image-stamp-with-opacity-to-pdf-pages](./add-image-stamp-with-opacity-to-pdf-pages.cs) | Add Image Stamp with Opacity to PDF Pages | `Document`, `ImageStamp`, `Opacity` | Demonstrates how to overlay a semi‑transparent image stamp on every page of a PDF document using ... |
| [add-image-stamp-with-percentage-offsets](./add-image-stamp-with-percentage-offsets.cs) | Add Image Stamp with Percentage Positioning | `Document`, `Page`, `ImageStamp` | Demonstrates how to place an image stamp on each page of a PDF using percentage‑based offsets rel... |
| [add-image-stamp-with-unicode-alt-text](./add-image-stamp-with-unicode-alt-text.cs) | Add Image Stamp with Unicode Alternative Text | `Document`, `ImageStamp`, `AlternativeText` | Demonstrates how to add an image stamp to a PDF and set multilingual Unicode alternative text for... |
| ... | | | *and 20 more files* |

## Category Statistics
- Total examples: 50

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_123822_43348a`
<!-- AUTOGENERATED:END -->

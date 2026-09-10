---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

> **Stamping** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Annotations;` (7/50 files)
- `using Aspose.Pdf.Drawing;` (2/50 files)
- `using Aspose.Pdf.Facades;` (2/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)
- `using System.Drawing;` (2/50 files)
- `using System.Drawing.Imaging;` (1/50 files)

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
| [add-background-image-stamp-to-pdf](./add-background-image-stamp-to-pdf.cs) | Add Background Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp behind the existing content of each PDF page by setting the Bac... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `TextState` | Shows how to create a text stamp with fill‑stroke rendering to produce bold outlined text on a PD... |
| [add-bottom-left-text-stamp](./add-bottom-left-text-stamp.cs) | Add Bottom‑Left Text Stamp to PDF | `Document`, `TextStamp`, `HorizontalAlignment` | Shows how to place a text stamp at the bottom‑left corner of each PDF page with a 10‑point margin... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating a semi‑transparent TextStamp, centering it o... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Custom-Sized Page Stamp to PDF | `Document`, `Page`, `PdfPageStamp` | Demonstrates creating a PdfPageStamp from an existing page, setting custom width, height, and pos... |
| [add-custom-text-stamp-to-pdf-first-page](./add-custom-text-stamp-to-pdf-first-page.cs) | Add Custom Text Stamp to PDF First Page | `Document`, `TextState`, `FindFont` | Demonstrates how to place a text stamp with a custom font, size, and blue color on the first page... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp rotated 90° as a diagonal watermark on every page of a P... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to add a semi‑transparent, 45‑degree rotated text stamp as a diagonal watermark ... |
| [add-faint-overlay-stamp-to-pdf-pages](./add-faint-overlay-stamp-to-pdf-pages.cs) | Add Faint Overlay Stamp to PDF Pages | `Document`, `TextStamp`, `AddStamp` | Shows how to apply a semi‑transparent text stamp as a faint overlay on every page of a PDF using ... |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to apply an image stamp as a full‑page background watermark to every page of a P... |
| [add-global-text-stamp-to-all-pdf-pages](./add-global-text-stamp-to-all-pdf-pages.cs) | Add Global Text Stamp to All PDF Pages | `Document`, `TextStamp`, `Page` | Shows how to create a TextStamp and apply it to every page of a PDF document using Aspose.Pdf's A... |
| [add-image-stamp-alt-text-pdfa](./add-image-stamp-alt-text-pdfa.cs) | Add Image Stamp with Alt Text for PDF/A‑1b | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp with alternative text on every page of a PDF and then co... |
| [add-image-stamp-annotation-to-pdf](./add-image-stamp-annotation-to-pdf.cs) | Add Image Stamp Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to place an image as a StampAnnotation on a PDF page while preserving existing annotati... |
| [add-image-stamp-flatten-annotations](./add-image-stamp-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `Page`, `ImageStamp` | Demonstrates how to add an image stamp to every page of a PDF and then flatten all annotations so... |
| [add-image-stamp-floatingbox-page-4](./add-image-stamp-floatingbox-page-4.cs) | Add Image Stamp as Background Using FloatingBox on Page 4 | `Document`, `Page`, `FloatingBox` | Demonstrates how to place an image stamp as the background of a FloatingBox on the fourth page of... |
| [add-image-stamp-from-memory-stream-to-pdf](./add-image-stamp-from-memory-stream-to-pdf.cs) | Add Image Stamp from Memory Stream to PDF | `ImageStamp`, `Document`, `Page` | Demonstrates how to load an image into a MemoryStream and apply it as an ImageStamp to every page... |
| [add-image-stamp-percentage-position](./add-image-stamp-percentage-position.cs) | Add Image Stamp with Percentage‑Based Positioning | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp on each page of a PDF using offsets expressed as percent... |
| [add-image-stamp-preserve-acroform-fields](./add-image-stamp-preserve-acroform-fields.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an ImageStamp to every page of a PDF using Aspose.Pdf while keeping exi... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks and Outline | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load an existing PDF, apply a semi‑transparent image stamp to every page, and... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp to PDF While Preserving Embedded Files | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, iterate through its pages, add an image stamp to each page, and s... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript Actions | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp, applying it to every page, and saving the doc... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp to PDF While Preserving Page Labels | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to load a PDF, create an ImageStamp, apply it to every page, and save the docume... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp While Preserving XMP Metadata | `Document`, `Page`, `ImageStamp` | Demonstrates how to load a PDF, retrieve its XMP metadata, apply an image stamp to every page, re... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `Decrypt`, `ImageStamp` | Shows how to open an encrypted PDF with a password, decrypt it, place an image stamp on a page, a... |
| [add-image-stamp-to-pdf-form](./add-image-stamp-to-pdf-form.cs) | Add Image Stamp to PDF Form While Preserving Fields | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF that contains form fields, adding an image stamp to each page, and sav... |
| [add-image-stamp-to-pdf-page](./add-image-stamp-to-pdf-page.cs) | Add Image Stamp to PDF Page with Quality and Opacity | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp on the second page of a PDF, configuring 100% quality and 0.8 o... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF without Invalidating Signature | `Document`, `ImageStamp`, `Page` | Shows how to place a semi‑transparent image stamp on a digitally signed PDF using Aspose.Pdf whil... |
| [add-image-stamp-unicode-alt-text](./add-image-stamp-unicode-alt-text.cs) | Add Image Stamp with Unicode Alternative Text to PDF | `Document`, `ImageStamp`, `AlternativeText` | Demonstrates how to place an image stamp on a PDF page and set multilingual Unicode alternative t... |
| [add-image-stamp-with-alt-text-to-pdf-page](./add-image-stamp-with-alt-text-to-pdf-page.cs) | Add Image Stamp with Alt Text to PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp with alternative text for accessibility to the third page ... |
| [add-low-quality-image-stamp-to-pdf](./add-low-quality-image-stamp-to-pdf.cs) | Add Low-Quality Image Stamp to PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp with reduced quality (10 %) and apply it to every p... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

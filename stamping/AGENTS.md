---
name: stamping
description: C# examples for stamping using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - stamping

> **Stamping** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Annotations;` (5/50 files)
- `using Aspose.Pdf.Drawing;` (3/50 files)
- `using Aspose.Pdf.Devices;` (2/50 files)
- `using Aspose.Pdf.Facades;` (2/50 files)
- `using Aspose.Pdf.Forms;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)
- `using System.Collections.Generic;` (1/50 files)
- `using System.Drawing;` (1/50 files)
- `using System.Drawing.Imaging;` (1/50 files)
- `using System.Linq;` (1/50 files)

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
| [add-background-image-stamp-to-pdf](./add-background-image-stamp-to-pdf.cs) | Add Background Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp behind the existing content of each page in a PDF using ... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Shows how to create a centered bold text stamp and apply it to every page of a PDF using Aspose.Pdf. |
| [add-bottom-left-text-stamp](./add-bottom-left-text-stamp.cs) | Align Text Stamp to Bottom‑Left Corner with Margin | `Document`, `TextStamp`, `AddStamp` | Demonstrates how to place a text stamp at the bottom‑left of each PDF page using Aspose.Pdf, with... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp to PDF | `Document`, `TextStamp`, `AddStamp` | Shows how to load a PDF with Aspose.Pdf, create a semi‑transparent 'Confidential' text stamp, app... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Image Stamp with Custom Size to Specific PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp with custom width, height, and position to a specific page... |
| [add-custom-text-stamp-to-pdf-first-page](./add-custom-text-stamp-to-pdf-first-page.cs) | Add Custom Text Stamp to First PDF Page | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to load a PDF, create a TextStamp with a specific font, size, and blue color, an... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp, rotating it 90 degrees, setting opacity, and ... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `TextState` | Shows how to place a 45‑degree semi‑transparent text stamp on every page of a PDF as a diagonal w... |
| [add-faint-overlay-stamp-to-pdf-pages](./add-faint-overlay-stamp-to-pdf-pages.cs) | Add Image Stamp with Opacity to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to overlay an image stamp with 40% opacity on every page of a PDF using Aspose.Pdf. |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Text Watermark to PDF | `Document`, `Page`, `TextStamp` | Demonstrates how to create a TextStamp as a background watermark that covers the entire page and ... |
| [add-global-text-stamp-to-all-pdf-pages](./add-global-text-stamp-to-all-pdf-pages.cs) | Add Image Stamp to All PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, create an ImageStamp, apply it to every page with Page.AddStamp, and sav... |
| [add-image-stamp-alt-text-pdfa](./add-image-stamp-alt-text-pdfa.cs) | Add Image Stamp with Alternative Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `AddStamp` | Demonstrates adding an image stamp to each page of a PDF, setting alternative text for the stamp ... |
| [add-image-stamp-annotation-to-pdf](./add-image-stamp-annotation-to-pdf.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, creating an ImageStamp, and applying it to each page while keeping ex... |
| [add-image-stamp-flatten-annotations](./add-image-stamp-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp to every page of a PDF, flatten all existing annotations... |
| [add-image-stamp-floatingbox-page-4](./add-image-stamp-floatingbox-page-4.cs) | Add Image Background to PDF Page Using FloatingBox | `Document`, `Page`, `FloatingBox` | Demonstrates how to place an image stamp as a full‑page background inside a FloatingBox on the fo... |
| [add-image-stamp-from-memory-stream-to-pdf](./add-image-stamp-from-memory-stream-to-pdf.cs) | Add Image Stamp from Memory Stream to PDF | `ImageStamp`, `Document`, `AddStamp` | Demonstrates how to load an image into a MemoryStream and apply it as an ImageStamp to each page ... |
| [add-image-stamp-percentage-position](./add-image-stamp-percentage-position.cs) | Apply Image Stamp with Percentage Offsets to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates how to place an image stamp on each PDF page using offsets expressed as percentages ... |
| [add-image-stamp-preserve-acroform-fields](./add-image-stamp-preserve-acroform-fields.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF, optionally reading AcroForm field values, applying an ImageStamp to e... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to overlay an image stamp on every page of a PDF using Aspose.Pdf while keeping ... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Shows how to load a PDF, apply an image stamp to every page, and save the document while keeping ... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript Actions | `Document`, `ImageStamp`, `AddStamp` | This example demonstrates how to apply an image stamp to each page of a PDF using Aspose.Pdf whil... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp While Preserving Page Labels | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp to every page of a PDF using Aspose.Pdf while keeping th... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp and Preserve Custom XMP Metadata in PDF | `Document`, `ImageStamp`, `AddStamp` | The example loads a PDF, adds a custom XMP metadata property, creates a semi‑transparent image st... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `Decrypt`, `ImageStamp` | Shows how to open an encrypted PDF with a password, decrypt it, and apply an image stamp to every... |
| [add-image-stamp-to-pdf-form](./add-image-stamp-to-pdf-form.cs) | Add Image Stamp to PDF Form Without Flattening | `Document`, `ImageStamp`, `AddStamp` | Demonstrates loading a PDF with form fields, creating an ImageStamp, applying it to each page, an... |
| [add-image-stamp-to-pdf-page](./add-image-stamp-to-pdf-page.cs) | Add Image Stamp to Page Two | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp with 100% quality and 80% opacity on the second page of ... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF Without Invalidating Signature | `Document`, `ImageStamp`, `Page` | Shows how to overlay an image stamp onto a digitally signed PDF using Aspose.Pdf while preserving... |
| [add-image-stamp-unicode-alt-text](./add-image-stamp-unicode-alt-text.cs) | Add Image Stamp with Unicode Alt Text to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp on every page of a PDF and embed multilingual Unicode al... |
| [add-image-stamp-with-alt-text-to-pdf-page](./add-image-stamp-with-alt-text-to-pdf-page.cs) | Add Image Stamp with Alt Text to PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to place an image stamp on the third page of a PDF and assign alternative text f... |
| [add-low-quality-image-stamp-to-pdf](./add-low-quality-image-stamp-to-pdf.cs) | Add Low-Quality Image Stamp to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates compressing a stamp image to a low‑quality JPEG (10 % quality) in memory and applyin... |
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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

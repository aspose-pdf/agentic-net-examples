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

- `using Aspose.Pdf;` (49/49 files) ← category-specific
- `using Aspose.Pdf.Text;` (11/49 files)
- `using Aspose.Pdf.Facades;` (9/49 files)
- `using Aspose.Pdf.Annotations;` (7/49 files)
- `using Aspose.Pdf.Tagged;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (49/49 files)

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
| [add-auto-adjusting-text-stamp-to-pdf](./add-auto-adjusting-text-stamp-to-pdf.cs) | Add Auto-Adjusting Text Stamp to PDF | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to place a text stamp on each PDF page that automatically scales its font size t... |
| [add-background-image-stamp-to-pdf-pages](./add-background-image-stamp-to-pdf-pages.cs) | Add Background Image Stamp to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to place an image stamp behind the existing content of every page in a PDF using Aspose... |
| [add-bold-outlined-text-stamp](./add-bold-outlined-text-stamp.cs) | Add Bold Outlined Text Stamp to PDF | `Document`, `TextStamp`, `FindFont` | Demonstrates creating a text stamp with fill‑stroke rendering to produce bold outlined text and a... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp with Opacity to PDF | `Document`, `TextStamp`, `AddStamp` | Shows how to place a semi‑transparent 'CONFIDENTIAL' text stamp on every page of a PDF using Aspo... |
| [add-custom-blue-text-stamp-to-first-page](./add-custom-blue-text-stamp-to-first-page.cs) | Add Custom Blue Text Stamp to First PDF Page | `Document`, `Save`, `TextState` | Demonstrates how to add a text stamp with a custom font, size, and blue color to the first page o... |
| [add-custom-sized-page-stamp](./add-custom-sized-page-stamp.cs) | Add Custom-Sized Page Stamp to Specific PDF Page | `Document`, `PdfPageStamp`, `Page` | Demonstrates creating a PdfPageStamp with custom width, height, and position, then applying it to... |
| [add-diagonal-image-watermark-to-pdf](./add-diagonal-image-watermark-to-pdf.cs) | Add Diagonal Image Watermark to PDF | `Document`, `ImageStamp`, `Page` | Shows how to place a rotated image stamp as a diagonal watermark on every page of a PDF using Asp... |
| [add-diagonal-text-stamp-watermark](./add-diagonal-text-stamp-watermark.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `HorizontalAlignment` | Demonstrates how to apply a semi‑transparent, 45° rotated text stamp as a diagonal watermark on e... |
| [add-faint-text-overlay-to-pdf-pages](./add-faint-text-overlay-to-pdf-pages.cs) | Add Faint Text Overlay to PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates how to place a semi‑transparent 'CONFIDENTIAL' text stamp as a faint overlay on ever... |
| [add-fixed-size-image-stamp-to-pdf-pages](./add-fixed-size-image-stamp-to-pdf-pages.cs) | Add Fixed-Size Image Stamp to PDF Pages | `Document`, `ImageStamp`, `Page` | Demonstrates how to place a fixed-size image stamp at the center of every page in a PDF, independ... |
| [add-full-page-image-watermark-to-pdf](./add-full-page-image-watermark-to-pdf.cs) | Add Full-Page Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to overlay a semi‑transparent image across every page of a PDF as a background w... |
| [add-global-text-stamp-to-all-pdf-pages](./add-global-text-stamp-to-all-pdf-pages.cs) | Add Global Text Stamp to All PDF Pages | `Document`, `Save`, `Page` | Shows how to create a TextStamp and apply it to every page of a PDF document using Aspose.Pdf's D... |
| [add-image-stamp-alt-text-page-3](./add-image-stamp-alt-text-page-3.cs) | Add Image Stamp with Alt Text to PDF Page 3 | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp with alternative text for accessibility to the third page ... |
| [add-image-stamp-alt-text-pdfa1b](./add-image-stamp-alt-text-pdfa1b.cs) | Add Image Stamp with Alt Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `Page` | Demonstrates adding an image stamp with alternative text to each page of a PDF and converting the... |
| [add-image-stamp-and-flatten-annotations](./add-image-stamp-and-flatten-annotations.cs) | Add Image Stamp and Flatten Annotations to Create Read‑Only ... | `Document`, `ImageStamp`, `Page` | Demonstrates how to place an image stamp on every page of a PDF and then flatten all annotations ... |
| [add-image-stamp-preserve-acroform](./add-image-stamp-preserve-acroform.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp to every page of a PDF using Aspose.Pdf while keeping ex... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks | `Document`, `ImageStamp`, `AddStamp` | Shows how to overlay an image stamp on every page of a PDF using Aspose.Pdf, keeping existing boo... |
| [add-image-stamp-preserve-embedded-files](./add-image-stamp-preserve-embedded-files.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add a semi‑transparent image stamp to every page of a PDF using Aspose.Pdf wh... |
| [add-image-stamp-preserve-javascript](./add-image-stamp-preserve-javascript.cs) | Add Image Stamp to PDF While Preserving JavaScript | `Document`, `ImageStamp`, `AddStamp` | Shows how to place a semi‑transparent image stamp on each page of a PDF using Aspose.Pdf, while k... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp While Preserving Page Labels | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to apply an image stamp to every page of a PDF using Aspose.Pdf without affectin... |
| [add-image-stamp-preserve-xmp-metadata](./add-image-stamp-preserve-xmp-metadata.cs) | Add Image Stamp and Preserve XMP Metadata in PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to add a semi‑transparent image stamp to each page of a PDF while preserving the... |
| [add-image-stamp-quality-opacity](./add-image-stamp-quality-opacity.cs) | Add Image Stamp with Quality and Opacity to PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates how to add an image stamp to the second page of a PDF using Aspose.Pdf, setting the ... |
| [add-image-stamp-to-encrypted-pdf](./add-image-stamp-to-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `ImageStamp`, `AddStamp` | Shows how to open an encrypted PDF with a password, decrypt it, apply an image stamp to each page... |
| [add-image-stamp-to-pdf-form](./add-image-stamp-to-pdf-form.cs) | Add Image Stamp to PDF Form While Preserving Fields | `Document`, `ImageStamp`, `HorizontalAlignment` | Demonstrates how to overlay an image stamp on each page of a PDF that contains form fields using ... |
| [add-image-stamp-to-pdf](./add-image-stamp-to-pdf.cs) | Add Image Stamp to PDF While Preserving Annotations | `Document`, `ImageStamp`, `Page` | Demonstrates loading a PDF, creating an ImageStamp with opacity and alignment, adding it to a pag... |
| [add-image-stamp-to-signed-pdf](./add-image-stamp-to-signed-pdf.cs) | Add Image Stamp to Signed PDF Without Invalidating Signature | `Document`, `ImageStamp`, `Page` | Demonstrates loading an already digitally signed PDF, adding a semi‑transparent image stamp, and ... |
| [add-image-stamp-unicode-alt-text](./add-image-stamp-unicode-alt-text.cs) | Add Image Stamp with Unicode Alt Text to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp to each page of a PDF and set Unicode alternative text for... |
| [add-low-quality-image-stamp-to-large-pdf](./add-low-quality-image-stamp-to-large-pdf.cs) | Add Low‑Quality Image Stamp to Large PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp with reduced quality (10 %) to each page of a PDF using As... |
| [add-multi-line-text-stamp-custom-line-spacing](./add-multi-line-text-stamp-custom-line-spacing.cs) | Add Multi‑Line Text Stamp with Custom Line Spacing | `Document`, `AddStamp`, `Save` | Demonstrates how to add a multi‑line text stamp with custom line spacing to each page of a PDF us... |
| [add-opaque-image-stamp-watermark](./add-opaque-image-stamp-watermark.cs) | Add Fully Opaque Image Stamp Watermark to PDF | `Document`, `ImageStamp`, `Page` | Demonstrates how to place a fully opaque image stamp at the center of each page in a PDF using As... |
| ... | | | *and 19 more files* |

## Category Statistics
- Total examples: 49

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

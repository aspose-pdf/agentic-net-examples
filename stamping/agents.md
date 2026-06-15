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
- `using Aspose.Pdf.Text;` (29/50 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (5/50 files)
- `using Aspose.Pdf.Forms;` (2/50 files)
- `using Aspose.Pdf.Drawing;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (31/50 files)
- `using System.Drawing;` (7/50 files)
- `using System.Drawing.Imaging;` (4/50 files)
- `using System.Collections.Generic;` (1/50 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document())
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-bottom-left-text-stamp](./add-bottom-left-text-stamp.cs) | Add Bottom-Left Text Stamp with Margin | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to add a text stamp aligned to the bottom‑left corner of each page with a 10‑poi... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Subtle Confidential Text Stamp to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to create a PDF, add a semi‑transparent "Confidential" text stamp with 0.6 opaci... |
| [add-custom-page-stamp](./add-custom-page-stamp.cs) | Add Custom Sized Page Stamp to PDF | `Document`, `Page`, `TextFragment` | Shows how to create a PDF page stamp with specific width, height, and position, and apply it to a... |
| [add-diagonal-image-watermark](./add-diagonal-image-watermark.cs) | Add Diagonal Image Watermark to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to add an image stamp rotated 90 degrees as a diagonal watermark to each page of... |
| [add-diagonal-watermark](./add-diagonal-watermark.cs) | Add Diagonal Watermark Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Creates a sample PDF and adds a diagonal text watermark rotated 45 degrees on each page. |
| [add-faint-page-stamp](./add-faint-page-stamp.cs) | Add Faint Text Stamp to All PDF Pages | `Document`, `TextStamp`, `AddStamp` | Creates a sample PDF and adds a semi‑transparent text stamp to each page. |
| [add-full-page-watermark](./add-full-page-watermark.cs) | Add Full-Page Watermark Using PdfPageStamp | `Document`, `Page`, `TextFragment` | Demonstrates how to create a watermark that covers the entire page by using a PdfPageStamp as a b... |
| [add-image-stamp-alt-text](./add-image-stamp-alt-text.cs) | Add Image Stamp with Alternative Text on Page 3 | `Document`, `ImageStamp`, `AddStamp` | Creates a PDF with three pages, adds an image stamp on the third page and sets alternative text f... |
| [add-image-stamp-alternative-text](./add-image-stamp-alternative-text.cs) | Add Image Stamp with Unicode Alternative Text | `Document`, `AddStamp`, `ImageStamp` | Creates a PDF, adds an image stamp and sets multilingual alternative text for accessibility. |
| [add-image-stamp-annotation](./add-image-stamp-annotation.cs) | Add Image Stamp Annotation to PDF while Preserving Existing ... | `Document`, `Page`, `TextAnnotation` | Creates a PDF, adds a text annotation, then adds an image stamp annotation, demonstrating how to ... |
| [add-image-stamp-custom-position](./add-image-stamp-custom-position.cs) | Add Image Stamp with Custom Position | `Document`, `ImageStamp`, `AddStamp` | Creates a PDF, then adds an image stamp at specified X and Y coordinates on the first page. |
| [add-image-stamp-encrypted-pdf](./add-image-stamp-encrypted-pdf.cs) | Add Image Stamp to Encrypted PDF | `Document`, `Encrypt`, `Document(string, string)` | Creates a PDF, encrypts it with a password, then opens it using the password and adds an image st... |
| [add-image-stamp-flatten](./add-image-stamp-flatten.cs) | Add Image Stamp and Flatten Annotations to PDF | `Document`, `Page`, `StampAnnotation` | Creates a PDF, adds an image stamp as an annotation, then flattens the annotation to make the doc... |
| [add-image-stamp-form](./add-image-stamp-form.cs) | Add Image Stamp to PDF with Form Fields | `Document`, `Page`, `TextBoxField` | Creates a PDF containing a form field and then adds an image stamp while preserving the field's f... |
| [add-image-stamp-from-memory-stream](./add-image-stamp-from-memory-stream.cs) | Add Image Stamp from Memory Stream to PDF | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to create an image stamp from a memory stream and apply it to a PDF page. |
| [add-image-stamp-keep-javascript](./add-image-stamp-keep-javascript.cs) | Add Image Stamp While Preserving JavaScript Actions | `Document`, `Page`, `TextFragment` | Creates a PDF with a JavaScript action, adds an image stamp to each page, and saves the result, d... |
| [add-image-stamp-pdfa1b](./add-image-stamp-pdfa1b.cs) | Add Image Stamp with Alternative Text for PDF/A‑1b Output | `Document`, `ImageStamp`, `PdfSaveOptions` | Creates a PDF, adds an image stamp with alternative text, and saves the document as PDF/A‑1b comp... |
| [add-image-stamp-preserve-acroform](./add-image-stamp-preserve-acroform.cs) | Add Image Stamp While Preserving AcroForm Fields | `Document`, `AddStamp`, `ImageStamp` | Creates a PDF with a form field, adds an image stamp to the page, and saves the result while keep... |
| [add-image-stamp-preserve-bookmarks](./add-image-stamp-preserve-bookmarks.cs) | Add Image Stamp While Preserving Bookmarks | `Document`, `AddStamp`, `ImageStamp` | Creates a PDF with bookmarks, then adds an image stamp to each page without affecting the existin... |
| [add-image-stamp-preserve-embedded](./add-image-stamp-preserve-embedded.cs) | Add Image Stamp While Preserving Embedded Files | `Document`, `FileSpecification`, `ImageStamp` | Creates a PDF with an embedded file, then adds an image stamp to each page, ensuring the embedded... |
| [add-image-stamp-preserve-page-labels](./add-image-stamp-preserve-page-labels.cs) | Add Image Stamp While Preserving Page Labels | `Document`, `Page`, `ImageStamp` | Creates a PDF, adds a page label, then applies an image stamp to the page without altering the ex... |
| [add-image-stamp-signed-pdf](./add-image-stamp-signed-pdf.cs) | Add Image Stamp to Signed PDF without Invalidating Signature | `Document`, `Page`, `TextFragment` | Creates a PDF, digitally signs it, then adds an image stamp using incremental update so the exist... |
| [add-image-stamp-top-right](./add-image-stamp-top-right.cs) | Add Image Stamp Aligned Top-Right with Margins | `Document`, `ImageStamp`, `Page` | Creates a PDF and adds an image stamp positioned at the top‑right corner with specified margin of... |
| [add-image-stamp-xmp](./add-image-stamp-xmp.cs) | Add Image Stamp and Preserve XMP Metadata | `Document`, `ImageStamp`, `AddStamp` | Creates a PDF, adds custom XMP metadata, stamps an image on each page, and saves the result while... |
| [add-image-stamp](./add-image-stamp.cs) | Add Image Stamp with Quality and Opacity to PDF Page | `Document`, `ImageStamp`, `AddStamp` | Demonstrates how to add an image stamp with 100% quality and 0.8 opacity to the second page of a ... |
| [add-low-quality-image-stamp](./add-low-quality-image-stamp.cs) | Add Low‑Quality Image Stamp to PDF | `Document`, `Page`, `TextFragment` | Demonstrates creating a PDF, then adding an image stamp with 10% quality to improve performance o... |
| [add-multi-line-text-stamp](./add-multi-line-text-stamp.cs) | Add Multi-Line Text Stamp with Custom Line Spacing | `Document`, `AddStamp`, `TextStamp` | Shows how to add a text stamp containing several lines with extra spacing to a PDF using Aspose.Pdf. |
| [add-opaque-image-stamp](./add-opaque-image-stamp.cs) | Add Fully Opaque Image Stamp as Watermark | `Document`, `ImageStamp`, `AddStamp` | Creates a sample PDF and adds a fully opaque image stamp to each page as a visual watermark. |
| [add-outlined-text-stamp](./add-outlined-text-stamp.cs) | Add Outlined Text Stamp to PDF | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to add a text stamp with stroke rendering mode (outlined text) to a PDF page usi... |
| [add-page-stamp-background](./add-page-stamp-background.cs) | Add Page Stamp as Background to PDF | `Document`, `AddStamp`, `PdfPageStamp` | Demonstrates how to add a page stamp behind the content of a PDF page using the Background property. |
| ... | | | *and 20 more files* |

## Category Statistics
- Total examples: 50

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for stamping patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->

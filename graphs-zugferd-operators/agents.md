---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (83/84 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (22/84 files)
- `using Aspose.Pdf.Text;` (12/84 files)
- `using Aspose.Pdf.Forms;` (8/84 files)
- `using Aspose.Pdf.Drawing;` (7/84 files)
- `using Aspose.Pdf.Devices;` (6/84 files)
- `using Aspose.Pdf.Operators;` (5/84 files)
- `using Aspose.Pdf.Optimization;` (1/84 files)
- `using Aspose.Pdf.Printing;` (1/84 files)
- `using Aspose.Pdf.Security;` (1/84 files)
- `using Aspose.Pdf.Signatures;` (1/84 files)
- `using Aspose.Pdf.Tagged;` (1/84 files)
- `using System;` (83/84 files)
- `using System.IO;` (76/84 files)
- `using System.Collections.Generic;` (5/84 files)
- `using System.Text;` (2/84 files)
- `using System.Diagnostics;` (1/84 files)
- `using System.Drawing;` (1/84 files)
- `using System.Drawing.Imaging;` (1/84 files)
- `using System.Linq;` (1/84 files)
- `using System.Reflection;` (1/84 files)
- `using System.Runtime.InteropServices;` (1/84 files)
- `using System.Text.Json;` (1/84 files)
- `using System.Xml;` (1/84 files)
- `using System.Xml.Linq;` (1/84 files)
- `using System.Xml.Schema;` (1/84 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto‑Close JavaScript to PDF | `Document`, `JavascriptAction` | Shows how to embed a JavaScript timeout in a PDF that automatically closes the document after a s... |
| [add-auto-print-javascript-action-to-pdf](./add-auto-print-javascript-action-to-pdf.cs) | Add Auto‑Print JavaScript Action to PDF | `Document`, `JavascriptAction`, `Save` | Shows how to embed a JavaScript action that automatically opens the print dialog when the PDF is ... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Auto‑Save JavaScript to PDF | `Document`, `Save`, `JavascriptAction` | Shows how to embed a document‑level JavaScript action that automatically saves the PDF at defined... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `Save` | Demonstrates how to load an existing ZUGFeRD PDF with Aspose.Pdf, add custom metadata entries suc... |
| [add-document-expiry-javascript-action](./add-document-expiry-javascript-action.cs) | Add Document-Level JavaScript Expiry Action to PDF | `Document`, `JavascriptAction`, `Save` | Demonstrates how to embed a document-level JavaScript action that checks an expiry date and close... |
| [add-encrypted-attachment-to-pdf](./add-encrypted-attachment-to-pdf.cs) | Add Encrypted Attachment to PDF | `Document`, `FileSpecification`, `Permissions` | Shows how to embed a file as an attachment, encrypt the PDF (including the attachment) with user ... |
| [add-file-attachment-annotation-to-pdf](./add-file-attachment-annotation-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to attach an external PDF file to a PDF document as a file attachment annotation... |
| [add-javascript-calculation-to-pdf-form-total](./add-javascript-calculation-to-pdf-form-total.cs) | Add JavaScript Calculation to PDF Form Total Field | `Document`, `Form`, `Field` | Demonstrates how to embed JavaScript in a PDF form using Aspose.Pdf to sum line‑item fields and d... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction` | Demonstrates how to set a JavaScript open action in a PDF using Aspose.Pdf so the document automa... |
| [add-javascript-password-prompt-to-pdf](./add-javascript-password-prompt-to-pdf.cs) | Add JavaScript Password Prompt to PDF | `Document`, `JavascriptAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf to prompt the user for a password on docum... |
| [add-javascript-toggle-optional-sections](./add-javascript-toggle-optional-sections.cs) | Add JavaScript to Toggle Optional Sections in a PDF Form | `Document`, `Form`, `CheckboxField` | Demonstrates how to embed JavaScript in a PDF form using Aspose.Pdf to show or hide optional fiel... |
| [add-js-open-action-navigate-page-10](./add-js-open-action-navigate-page-10.cs) | Add JavaScript Open Action to Navigate to Page 10 | `Document`, `JavascriptAction`, `Save` | Demonstrates how to embed a JavaScript open action in a PDF using Aspose.Pdf so the viewer automa... |
| [add-page-level-javascript-alert-to-pdf](./add-page-level-javascript-alert-to-pdf.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Save`, `Page` | Demonstrates how to embed a page-level JavaScript action that shows an alert when a specific PDF ... |
| [add-semi-transparent-background-watermark](./add-semi-transparent-background-watermark.cs) | Add Semi-Transparent Background Watermark to PDF Pages | `Document`, `Save`, `FindFont` | Demonstrates how to use Aspose.Pdf to place a semi-transparent watermark text behind existing pag... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add XMP Metadata for PDF/A Compliance | `Document`, `SetXmpMetadata`, `Convert` | Shows how to embed required XMP metadata (DocumentID and CreatorTool) into a PDF and convert the ... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files to a PDF with Descriptions | `Document`, `Page`, `Rectangle` | Demonstrates how to add several file attachment annotations to a PDF, each with its own MIME type... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `PdfFormat` | Shows how to embed a ZUGFeRD XML invoice into a PDF as an attached file with the correct AFRelati... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to convert every page of each PDF file in a folder to separate JPEG images using... |
| [batch-encrypt-pdfs-with-passwords](./batch-encrypt-pdfs-with-passwords.cs) | Batch Encrypt PDFs with Passwords | `Document`, `Encrypt`, `Save` | Demonstrates how to iterate through a folder of PDF files and apply identical user and owner pass... |
| [batch-generate-zugferd-invoice-pdfs](./batch-generate-zugferd-invoice-pdfs.cs) | Batch Generate ZUGFeRD-Compliant Invoice PDFs from CSV | `Document`, `Page`, `TextFragment` | Shows how to read invoice data from a CSV file, create a PDF with Aspose.Pdf, and embed the corre... |
| [batch-pdf-text-extraction-search-index](./batch-pdf-text-extraction-search-index.cs) | Batch PDF Text Extraction and In-Memory Search Index | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to load multiple PDF files, extract their full text using Aspose.Pdf, and store ... |
| [calculate-total-order-amount-pdf](./calculate-total-order-amount-pdf.cs) | Calculate Total Order Amount from PDF Form Fields | `Save`, `AutoRecalculate`, `Recalculate` | Shows how to iterate over PDF form fields, sum line‑item values, and write the calculated total b... |
| [combine-multiple-graphs-2x2-pdf](./combine-multiple-graphs-2x2-pdf.cs) | Combine Multiple Graphs on a Single PDF Page | `Document`, `Page`, `Height` | Demonstrates how to place four Aspose.Pdf.Drawing.Graph objects in a 2×2 grid on one PDF page, ad... |
| [compress-images-optimize-pdf](./compress-images-optimize-pdf.cs) | Compress Images and Optimize PDF Size | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to reduce a PDF's file size by compressing images, limiting resolution, and removing un... |
| [convert-pdf-page-to-high-res-png](./convert-pdf-page-to-high-res-png.cs) | Convert PDF Page to High-Resolution PNG | `Document`, `Resolution`, `PngDevice` | Demonstrates loading a PDF with Aspose.Pdf, selecting the first page, and rendering it to a 300 D... |
| [convert-pdf-to-docx-with-layout](./convert-pdf-to-docx-with-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Demonstrates converting a PDF file to DOCX using Aspose.Pdf while preserving the original layout,... |
| [convert-pdf-to-html-embedded-images](./convert-pdf-to-html-embedded-images.cs) | Convert PDF to HTML with Embedded Base64 Images | `Document`, `HtmlSaveOptions` | Demonstrates converting a PDF file to a single HTML document using Aspose.Pdf, embedding raster i... |
| [convert-pdf-to-html-enhanced-antialiasing](./convert-pdf-to-html-enhanced-antialiasing.cs) | Convert PDF to HTML with Enhanced Anti-Aliasing | `Document`, `HtmlSaveOptions` | Demonstrates how to convert a PDF document to HTML using Aspose.Pdf while enabling advanced anti-... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to load a PDF with Aspose.Pdf and convert each page into a multi‑page TIFF file ... |
| [convert-pdf-to-pdfa2b](./convert-pdf-to-pdfa2b.cs) | Convert PDF to PDF/A-2b Compliance | `Document`, `Convert`, `Save` | Shows how to load a PDF, convert it to PDF/A‑2b compliance with a conversion log, and save the re... |
| ... | | | *and 54 more files* |

## Category Statistics
- Total examples: 84

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BorderInfo`
- `Aspose.Pdf.BorderSide`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Drawing.GradientAxialShading`
- `Aspose.Pdf.Drawing.Graph`
- `Aspose.Pdf.Drawing.GraphInfo`
- `Aspose.Pdf.Drawing.Line`
- `Aspose.Pdf.Drawing.Line.GraphInfo`
- `Aspose.Pdf.Drawing.Paragraphs`
- `Aspose.Pdf.Drawing.Point`
- `Aspose.Pdf.Drawing.Rectangle`
- `Aspose.Pdf.Drawing.Shapes`
- `Aspose.Pdf.GraphInfo`
- `Aspose.Pdf.Matrix`

### Rules
- Create a {doc} (Aspose.Pdf.Document), add a {page} (Aspose.Pdf.Page) via doc.Pages.Add(), instantiate a Graph (Aspose.Pdf.Drawing.Graph) with width and height, and add it to page.Paragraphs.
- Instantiate a Line (Aspose.Pdf.Drawing.Line) with a float[] of coordinates, optionally set line.GraphInfo.DashArray = int[] and line.GraphInfo.DashPhase = int to define dash style, then add the line to graph.Shapes.
- Save the {doc} to a file path ({output_pdf}) using doc.Save().
- Create a {graph} (Aspose.Pdf.Drawing.Graph) with dimensions {float} width and {float} height, set IsChangePosition={bool}, position it using Left={float} and Top={float}, add a Rectangle shape (Aspose.Pdf.Drawing.Rectangle) at (0,0) with the same dimensions, set its fill and border color to {color}, assign Graph.ZIndex={int}, then add the Graph to {page}.Paragraphs.
- Set {page}.PageInfo.Margin.Left={float} and .Top={float} to zero (or desired offset) before placing Graph objects to ensure absolute positioning aligns with page coordinates.

### Warnings
- GraphInfo is accessed through the Line instance (line.GraphInfo); ensure the line object supports this property.
- DashArray expects an int[] where the pattern values represent dash and gap lengths; incorrect values may produce unexpected rendering.
- GraphInfo is accessed via the Rectangle.GraphInfo property; the exact type name may differ in newer library versions.
- Rectangle constructor uses integer parameters for coordinates and size; ensure correct units.
- GraphInfo may be null until the shape is added to a Graph; setting FillColor before adding is safe in this pattern.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for graphs-zugferd-operators patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

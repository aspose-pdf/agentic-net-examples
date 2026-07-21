---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

> **Graphs ZUGFeRD operators** in PDF using C# / .NET -- **82** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (82/82 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (22/82 files)
- `using Aspose.Pdf.Text;` (11/82 files)
- `using Aspose.Pdf.Forms;` (10/82 files)
- `using Aspose.Pdf.Devices;` (6/82 files)
- `using Aspose.Pdf.Operators;` (6/82 files)
- `using Aspose.Pdf.Drawing;` (5/82 files)
- `using Aspose.Pdf.Optimization;` (1/82 files)
- `using Aspose.Pdf.Signatures;` (1/82 files)
- `using Aspose.Pdf.Tagged;` (1/82 files)
- `using Aspose.Pdf.Vector;` (1/82 files)
- `using System;` (82/82 files)
- `using System.IO;` (76/82 files)
- `using System.Collections.Generic;` (4/82 files)
- `using System.Drawing;` (1/82 files)
- `using System.Drawing.Printing;` (1/82 files)
- `using System.Linq;` (1/82 files)
- `using System.Text.Json;` (1/82 files)
- `using System.Xml;` (1/82 files)
- `using System.Xml.Linq;` (1/82 files)
- `using System.Xml.Schema;` (1/82 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto‑Close JavaScript to PDF | `Document`, `Load`, `Save` | Demonstrates how to embed a JavaScript action in a PDF that automatically closes the document aft... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Auto‑Save JavaScript to PDF Document | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a document‑level JavaScript action that automatically saves the PDF at defined... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `Metadata`, `Add` | Shows how to load a ZUGFeRD PDF with Aspose.Pdf, add custom metadata entries such as ProjectCode ... |
| [add-expiry-javascript-action-to-pdf](./add-expiry-javascript-action-to-pdf.cs) | Add Expiry JavaScript Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a document‑level JavaScript open action that checks an expiry date and ... |
| [add-file-attachment-annotation-to-pdf](./add-file-attachment-annotation-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Shows how to embed an external PDF as a file attachment annotation, set its description and icon,... |
| [add-javascript-calculate-total-pdf](./add-javascript-calculate-total-pdf.cs) | Add JavaScript to Calculate Total from Line Items in PDF | `Document`, `Field`, `JavascriptAction` | Shows how to attach JavaScript to a PDF form field with Aspose.Pdf to sum line‑item fields and di... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to assign a JavaScript open action to a PDF using Aspose.Pdf so that the documen... |
| [add-javascript-password-prompt-to-pdf](./add-javascript-password-prompt-to-pdf.cs) | Add JavaScript Password Prompt to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf so that a password is requested when the d... |
| [add-javascript-toggle-pdf-form-sections](./add-javascript-toggle-pdf-form-sections.cs) | Add JavaScript to Toggle PDF Form Sections | `Document`, `Field`, `JavascriptAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf to show or hide optional sections based on... |
| [add-js-open-action-navigate-page-10](./add-js-open-action-navigate-page-10.cs) | Add JavaScript Open Action to Navigate to Page 10 | `Document`, `Document(string)`, `JavascriptAction` | Demonstrates loading a PDF with Aspose.Pdf, setting a JavaScript open action that jumps to page 1... |
| [add-page-level-javascript-alert](./add-page-level-javascript-alert.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Page`, `JavascriptAction` | Demonstrates how to embed a JavaScript action that shows an alert when a specific PDF page is ope... |
| [add-semi-transparent-background-watermark](./add-semi-transparent-background-watermark.cs) | Add Semi-Transparent Background Watermark to PDF Pages | `Document`, `Page`, `TextState` | Demonstrates how to place a semi‑transparent text watermark behind existing content on every page... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add XMP Metadata for PDF/A Compliance | `Document`, `Info`, `Metadata` | Demonstrates how to embed required XMP metadata entries, including document ID and creator tool, ... |
| [apply-form-filling-only-security](./apply-form-filling-only-security.cs) | Apply Form-Filling Only Security to PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF with Aspose.Pdf so that only form filling is permitted while editin... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files to a PDF with Custom Descriptions | `Document`, `Page`, `FileSpecification` | Demonstrates how to add several file attachment annotations to a PDF, each with its own MIME type... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A-3B | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a ZUGFeRD XML invoice into a PDF, set the AFRelationship to Data, and convert ... |
| [auto-print-pdf-on-open](./auto-print-pdf-on-open.cs) | Auto‑Print PDF on Open with JavaScript Action | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a JavaScript action in a PDF using Aspose.Pdf so that the print dialog ... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through all PDF files in a folder and convert each page to a high‑res... |
| [batch-encrypt-pdfs-with-password](./batch-encrypt-pdfs-with-password.cs) | Batch Encrypt PDFs with Password Protection | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf by applying the same user ... |
| [batch-generate-zugferd-pdfs-from-csv](./batch-generate-zugferd-pdfs-from-csv.cs) | Batch Generate ZUGFeRD‑Compliant PDFs from CSV | `Document`, `BindXml`, `TextFragment` | Demonstrates how to read invoice data from a CSV file, create a PDF for each invoice with Aspose.... |
| [calculate-total-order-amount-pdf-form](./calculate-total-order-amount-pdf-form.cs) | Calculate Total Order Amount with JavaScript in PDF Form | `Document`, `Form`, `JavascriptAction` | Demonstrates embedding and executing JavaScript in an Aspose.Pdf form to sum line‑item quantities... |
| [combine-multiple-graphs-2x2-grid](./combine-multiple-graphs-2x2-grid.cs) | Combine Multiple Graphs on a Single PDF Page in a 2x2 Grid | `Document`, `Page`, `Graph` | Shows how to create several Aspose.Pdf Graph objects and arrange them on one PDF page using a two... |
| [compress-pdf-and-optimize-images](./compress-pdf-and-optimize-images.cs) | Compress PDF and Optimize Images | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates how to reduce a PDF file size by applying optimization options, including object com... |
| [convert-pdf-pages-to-high-resolution-png](./convert-pdf-pages-to-high-resolution-png.cs) | Convert PDF Pages to High-Resolution PNG Images | `Document`, `Page`, `TextFragment` | Shows how to load a PDF with Aspose.Pdf, set a 300 DPI resolution, and convert each page to a PNG... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `Save`, `DocSaveOptions` | Shows how to load a PDF and save it as a DOCX file using Aspose.Pdf while preserving the original... |
| [convert-pdf-to-html-embedded-images](./convert-pdf-to-html-embedded-images.cs) | Convert PDF to HTML with Embedded Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Demonstrates converting a PDF file to a single HTML document while embedding all resources, inclu... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi-Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to use Aspose.Pdf to convert a PDF document into a multi‑page TIFF file, preserv... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A‑2b Compliance | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, converting it to PDF/A‑2b archival format, and saving... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD to PDF/A‑3U preserving XML | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF that contains a ZUGFeRD XML attachment and convert it to PDF/A‑3U while k... |
| [create-layered-graphics-with-transparency](./create-layered-graphics-with-transparency.cs) | Create Layered Graphics with Transparency in PDF | `Document`, `Page`, `Layer` | Demonstrates how to add optional‑content groups (layers) and draw semi‑transparent shapes such as... |
| ... | | | *and 52 more files* |

## Category Statistics
- Total examples: 82

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for graphs-zugferd-operators patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

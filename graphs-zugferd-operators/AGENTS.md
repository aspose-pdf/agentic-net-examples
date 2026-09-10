---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

> **Graphs ZUGFeRD operators** in PDF using C# / .NET -- **85** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (84/85 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (24/85 files)
- `using Aspose.Pdf.Text;` (12/85 files)
- `using Aspose.Pdf.Forms;` (9/85 files)
- `using Aspose.Pdf.Drawing;` (7/85 files)
- `using Aspose.Pdf.Devices;` (6/85 files)
- `using Aspose.Pdf.Operators;` (5/85 files)
- `using Aspose.Pdf.Facades;` (3/85 files)
- `using Aspose.Pdf.Tagged;` (2/85 files)
- `using Aspose.Pdf.Optimization;` (1/85 files)
- `using Aspose.Pdf.Signatures;` (1/85 files)
- `using System;` (85/85 files)
- `using System.IO;` (77/85 files)
- `using System.Collections.Generic;` (7/85 files)
- `using System.Drawing;` (1/85 files)
- `using System.Drawing.Printing;` (1/85 files)
- `using System.Reflection;` (1/85 files)
- `using System.Text.Json;` (1/85 files)
- `using System.Xml;` (1/85 files)
- `using System.Xml.Schema;` (1/85 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto‑Close JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a document‑level JavaScript action that automatically closes a PDF after a spe... |
| [add-auto-print-javascript-action-to-pdf](./add-auto-print-javascript-action-to-pdf.cs) | Add Auto‑Print JavaScript Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript action that automatically opens the print dialog when the... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Document-Level Auto‑Save JavaScript to PDF | `Document`, `JavascriptAction`, `ctor(string)` | Demonstrates how to embed a document‑level JavaScript action that automatically saves the PDF at ... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `DocumentInfo`, `Save` | Demonstrates loading an existing PDF with Aspose.Pdf, adding custom metadata entries such as proj... |
| [add-encrypted-file-attachment-to-pdf](./add-encrypted-file-attachment-to-pdf.cs) | Add Encrypted File Attachment to PDF | `Document`, `Page`, `TextFragment` | Shows how to embed a file attachment using a stream and then encrypt the PDF so only authorized u... |
| [add-expiry-javascript-action-to-pdf](./add-expiry-javascript-action-to-pdf.cs) | Add Expiry JavaScript Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a document‑level JavaScript action that checks a predefined expiry date and cl... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF with Description | `Document`, `Page`, `Rectangle` | Shows how to attach an external PDF file to a PDF document using Aspose.Pdf, including a descript... |
| [add-javascript-calculation-to-pdf-total](./add-javascript-calculation-to-pdf-total.cs) | Add JavaScript Calculation to PDF Form Total Field | `Document`, `Form`, `Field` | Shows how to embed JavaScript in a PDF using Aspose.Pdf to sum line‑item fields and display the r... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript open action to a PDF using Aspose.Pdf so that the documen... |
| [add-javascript-toggle-optional-sections](./add-javascript-toggle-optional-sections.cs) | Add JavaScript to Toggle Optional Sections in PDF Form | `Document`, `CheckboxField`, `JavascriptAction` | Shows how to attach a JavaScript action to a checkbox field in an Aspose.Pdf form so that optiona... |
| [add-page-level-javascript-alert-to-pdf](./add-page-level-javascript-alert-to-pdf.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Page`, `JavascriptAction` | Demonstrates how to embed a JavaScript action that shows an alert when a specific PDF page is ope... |
| [add-password-prompt-javascript-to-pdf](./add-password-prompt-javascript-to-pdf.cs) | Add Password Prompt JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates embedding JavaScript in a PDF with Aspose.Pdf to prompt for a password on document o... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark Behind PDF Content | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to place a semi‑transparent text watermark behind the existing content on every ... |
| [add-signature-field-with-js-validation](./add-signature-field-with-js-validation.cs) | Add Digital Signature Field with JavaScript Validation to PD... | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, insert a digital signature field, and attach a JavaScript action that ... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add XMP Metadata for PDF/A Compliance | `Document`, `DocumentInfo`, `Metadata` | Shows how to load a PDF, add required XMP metadata entries (creator tool, creation date, document... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files with Descriptions to a PDF | `Document`, `Page`, `FileSpecification` | Demonstrates how to embed several external files into a PDF, each with its own MIME type, descrip... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, embed a ZUGFeRD XML file as an attached file (AFRelationship.Data), conv... |
| [batch-apply-pdf-password-protection](./batch-apply-pdf-password-protection.cs) | Batch Apply Password Protection to PDFs | `Document`, `Encrypt`, `Save` | Demonstrates how to iterate through a folder of PDF files and apply identical user and owner pass... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `Resolution`, `JpegDevice` | Demonstrates how to iterate through all PDF files in a folder, convert each page to a JPEG image ... |
| [batch-generate-zugferd-pdfs-from-csv](./batch-generate-zugferd-pdfs-from-csv.cs) | Batch Generate ZUGFeRD‑Compliant PDFs from CSV | `Document`, `Page`, `TextFragment` | Reads invoice data from a CSV file, creates a PDF for each record, adds basic invoice text, and a... |
| [calculate-total-order-amount](./calculate-total-order-amount.cs) | Calculate Total Order Amount from PDF Form Fields | `Document`, `Form`, `Field` | Shows how to read quantity and price fields from an Aspose.Pdf form, compute line totals, and wri... |
| [change-pdf-line-width](./change-pdf-line-width.cs) | Change PDF Line Width from 1 to 3 Points | `Document`, `Page`, `Operator` | Shows how to iterate through each page's content operators and modify SetLineWidth operators to c... |
| [combine-multiple-graphs-on-single-pdf-page](./combine-multiple-graphs-on-single-pdf-page.cs) | Combine Multiple Graphs on a Single PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to place four separate graphs in a 2×2 grid on one PDF page using Aspose.Pdf, in... |
| [compress-pdf-images-optimization](./compress-pdf-images-optimization.cs) | Compress PDF Images and Optimize Resources | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates how to reduce a PDF file size by enabling image compression, resizing high‑resolutio... |
| [convert-pdf-pages-to-high-resolution-png](./convert-pdf-pages-to-high-resolution-png.cs) | Convert PDF Pages to High-Resolution PNG Images | `Document`, `Resolution`, `PngDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 300 DPI resolution, and converting each pag... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Shows how to convert a PDF file to DOCX using Aspose.Pdf while keeping the original layout, fonts... |
| [convert-pdf-to-html-with-embedded-images](./convert-pdf-to-html-with-embedded-images.cs) | Convert PDF to HTML with Embedded Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Demonstrates converting a PDF to HTML using Aspose.Pdf while embedding all resources and raster i... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to load a PDF with Aspose.Pdf, configure TIFF settings, and convert the entire d... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A‑2b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF, converts it to PDF/A‑2b archival format using Aspose.Pdf, logs any conversion errors... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD to PDF/A‑3U preserving XML | `Document`, `Convert`, `Save` | Shows how to load a PDF containing a ZUGFeRD XML attachment and convert it to PDF/A‑3U while keep... |
| ... | | | *and 55 more files* |

## Category Statistics
- Total examples: 85

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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

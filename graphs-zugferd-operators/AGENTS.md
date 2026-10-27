---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

> **Graphs ZUGFeRD operators** in PDF using C# / .NET -- **85** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (85/85 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (22/85 files)
- `using Aspose.Pdf.Text;` (15/85 files)
- `using Aspose.Pdf.Forms;` (11/85 files)
- `using Aspose.Pdf.Devices;` (6/85 files)
- `using Aspose.Pdf.Drawing;` (5/85 files)
- `using Aspose.Pdf.Operators;` (4/85 files)
- `using Aspose.Pdf.Security;` (2/85 files)
- `using Aspose.Pdf.Tagged;` (2/85 files)
- `using Aspose.Pdf.LogicalStructure;` (1/85 files)
- `using Aspose.Pdf.Optimization;` (1/85 files)
- `using System;` (85/85 files)
- `using System.IO;` (78/85 files)
- `using System.Collections.Generic;` (8/85 files)
- `using System.Drawing;` (3/85 files)
- `using System.Text.Json;` (3/85 files)
- `using System.Drawing.Printing;` (2/85 files)
- `using Microsoft.CSharp.RuntimeBinder;` (1/85 files)
- `using System.Linq;` (1/85 files)
- `using System.Reflection;` (1/85 files)
- `using System.Text;` (1/85 files)
- `using System.Xml;` (1/85 files)
- `using System.Xml.Linq;` (1/85 files)
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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto-Close JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed JavaScript in a PDF using Aspose.Pdf so the document automatically clos... |
| [add-auto-print-javascript-action-to-pdf](./add-auto-print-javascript-action-to-pdf.cs) | Add Auto‑Print JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript action that automatically opens the print dialog when the... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Document-Level Auto‑Save JavaScript to PDF | `Document`, `Save`, `OpenAction` | Shows how to embed a document‑level JavaScript action that automatically saves the PDF at defined... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `DocumentInfo`, `Save` | Shows how to load an existing ZUGFeRD PDF with Aspose.Pdf, add custom metadata entries such as Pr... |
| [add-encrypted-file-attachment-to-pdf](./add-encrypted-file-attachment-to-pdf.cs) | Encrypt PDF with Embedded Attachment | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed an external file into a PDF and encrypt the whole document (including the atta... |
| [add-expiry-javascript-action-to-pdf](./add-expiry-javascript-action-to-pdf.cs) | Add Document-Level Expiry JavaScript to PDF | `Document`, `ctor`, `JavascriptAction` | Demonstrates how to embed a document-level JavaScript action that checks an expiry date and close... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF with Description and MIME Type | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed an external PDF file into an existing PDF document using Aspose.Pdf, se... |
| [add-javascript-calculation-to-pdf-total](./add-javascript-calculation-to-pdf-total.cs) | Add JavaScript Total Price Calculation to PDF Form | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf to sum line‑item price fields and write th... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates loading a PDF with Aspose.Pdf, setting a JavaScript open action that navigates to pa... |
| [add-javascript-toggle-optional-sections](./add-javascript-toggle-optional-sections.cs) | Add JavaScript to Toggle PDF Form Sections | `Document`, `Form`, `Field` | Demonstrates how to attach JavaScript actions to PDF form fields using Aspose.Pdf so that checkin... |
| [add-page-level-javascript-alert-to-pdf](./add-page-level-javascript-alert-to-pdf.cs) | Add Page-Level JavaScript Alert to PDF Pages | `Document`, `Page`, `JavascriptAction` | Shows how to attach a JavaScript action to each PDF page using Aspose.Pdf so that an alert is dis... |
| [add-password-prompt-javascript-to-pdf](./add-password-prompt-javascript-to-pdf.cs) | Add Password Prompt JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf so that a password prompt appears when the... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Watermark Behind PDF Content | `Document`, `TextStamp`, `TextState` | Demonstrates loading a PDF, creating a semi‑transparent TextStamp set as a background, and applyi... |
| [add-signature-field-with-js-validation](./add-signature-field-with-js-validation.cs) | Create PDF Signature Field with JavaScript Validation | `Document`, `Page`, `Rectangle` | Demonstrates how to add a digital signature field to a PDF and attach a JavaScript action that ru... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add XMP Metadata for PDF/A‑1b Compliance | `Document`, `Metadata`, `Info` | Demonstrates creating a PDF/A‑1b document by registering XMP namespaces, adding required metadata... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files with Custom MIME Types to PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates how to embed several files into a PDF, each with its own MIME type and description, ... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `Convert` | Demonstrates embedding a ZUGFeRD invoice XML file into a PDF as an embedded file, setting the AFR... |
| [batch-apply-pdf-password-protection](./batch-apply-pdf-password-protection.cs) | Batch Encrypt PDFs with Password Protection | `Document`, `Encrypt`, `Save` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf, applying the same user an... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through all PDF files in a folder, load each document with Aspose.Pdf... |
| [batch-generate-zugferd-pdfs-from-csv](./batch-generate-zugferd-pdfs-from-csv.cs) | Batch Generate ZUGFeRD‑Compliant Invoice PDFs from CSV | `Document`, `Page`, `TextFragment` | Shows how to read invoice data from a CSV, create PDF/A‑3U invoices with Aspose.Pdf, embed the co... |
| [calculate-total-order-amount](./calculate-total-order-amount.cs) | Add JavaScript Calculation to PDF Form | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates loading a PDF form, attaching a JavaScript action that computes each line item's tot... |
| [change-pdf-line-width](./change-pdf-line-width.cs) | Update PDF Line Width Operators to 3 Points | `Document`, `Page`, `SetLineWidth` | Loads a PDF, iterates through each page's content streams, replaces any SetLineWidth operator wit... |
| [combine-multiple-graphs-on-single-pdf-page](./combine-multiple-graphs-on-single-pdf-page.cs) | Combine Multiple Graph Images into a 2x2 Grid PDF | `Document`, `Page`, `ImageStamp` | Shows how to place four image files on a single PDF page arranged in a two‑by‑two grid using Aspo... |
| [compress-pdf-images-optimization](./compress-pdf-images-optimization.cs) | Compress PDF Images Using OptimizationOptions | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Shows how to reduce a PDF's file size by compressing raster images and removing unused objects wi... |
| [convert-pdf-pages-to-high-resolution-png](./convert-pdf-pages-to-high-resolution-png.cs) | Convert PDF Page to PNG at 300 DPI | `Document`, `Resolution`, `PngDevice` | Loads a PDF, validates a page index, and renders the specified page to a high‑resolution 300 DPI ... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file with Aspose.Pdf and save it as a DOCX document while preservi... |
| [convert-pdf-to-html-with-embedded-images](./convert-pdf-to-html-with-embedded-images.cs) | Convert PDF to HTML with Embedded Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Demonstrates loading a PDF with Aspose.Pdf, configuring HtmlSaveOptions to embed all resources (i... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `TiffSettings`, `CompressionType` | Shows how to convert a PDF document into a multi‑page TIFF file, with each PDF page rendered as a... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A‑2b Compliance | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑2b format for long‑term archival using Aspose.Pdf, and sav... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD attachment to PDF/A‑3u | `Document`, `PdfFormatConversionOptions`, `Convert` | Loads a PDF containing a ZUGFeRD XML attachment, verifies the embedded files, and converts the do... |
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
Updated: 2026-10-23 | Run: `20261023_025405_3a5c42`
<!-- AUTOGENERATED:END -->

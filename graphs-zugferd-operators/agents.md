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

- `using Aspose.Pdf;` (82/83 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (19/83 files)
- `using Aspose.Pdf.Text;` (11/83 files)
- `using Aspose.Pdf.Forms;` (8/83 files)
- `using Aspose.Pdf.Drawing;` (7/83 files)
- `using Aspose.Pdf.Operators;` (7/83 files)
- `using Aspose.Pdf.Devices;` (5/83 files)
- `using Aspose.Pdf.Tagged;` (2/83 files)
- `using Aspose.Pdf.Facades;` (1/83 files)
- `using Aspose.Pdf.LogicalStructure;` (1/83 files)
- `using Aspose.Pdf.Optimization;` (1/83 files)
- `using Aspose.Pdf.Signatures;` (1/83 files)
- `using System;` (83/83 files)
- `using System.IO;` (76/83 files)
- `using System.Collections.Generic;` (5/83 files)
- `using System.Runtime.InteropServices;` (4/83 files)
- `using System.Linq;` (1/83 files)
- `using System.Xml;` (1/83 files)
- `using System.Xml.Schema;` (1/83 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto‑Close JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates embedding JavaScript in a PDF with Aspose.Pdf to automatically close the document af... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Auto‑Save JavaScript to PDF | `Document`, `JavascriptAction`, `Actions` | Demonstrates embedding a document‑level JavaScript action that automatically saves the PDF at def... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `Metadata`, `Save` | Shows how to load a ZUGFeRD PDF with Aspose.Pdf, add custom metadata entries such as project code... |
| [add-expiry-date-javascript-action-to-pdf](./add-expiry-date-javascript-action-to-pdf.cs) | Add Expiry Date JavaScript Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a document‑level JavaScript open action that checks the current date ag... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to attach an external PDF file to a PDF document as a file‑attachment annotation... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to set a JavaScript open action in a PDF using Aspose.Pdf so the document automa... |
| [add-javascript-password-prompt-and-encryption](./add-javascript-password-prompt-and-encryption.cs) | Add JavaScript Password Prompt and Encryption to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed JavaScript that asks for a password when the PDF is opened and then encrypt th... |
| [add-javascript-total-calculation-to-pdf-form](./add-javascript-total-calculation-to-pdf-form.cs) | Add JavaScript Total Calculation to PDF Form | `Document`, `JavascriptAction`, `Field` | Demonstrates how to attach a JavaScript action to a PDF form field using Aspose.Pdf to sum line‑i... |
| [add-page-level-javascript-alert](./add-page-level-javascript-alert.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Page`, `JavascriptAction` | Shows how to embed a JavaScript action on a specific PDF page that displays an alert when the pag... |
| [add-qr-code-barcode-with-low-level-operators](./add-qr-code-barcode-with-low-level-operators.cs) | Add QR Code Barcode Using Low-Level Operators | `Document`, `Rectangle`, `BarcodeField` | Loads an existing PDF, creates a QR code barcode field at defined coordinates, inserts it into th... |
| [add-semi-transparent-watermark-behind-pdf-content](./add-semi-transparent-watermark-behind-pdf-content.cs) | Add Semi-Transparent Watermark Behind PDF Content | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to place a low‑level WatermarkArtifact behind existing page content on every pag... |
| [add-xmp-metadata-pdfa](./add-xmp-metadata-pdfa.cs) | Add XMP Metadata for PDF/A Compliance | `Document`, `SetXmpMetadata`, `Convert` | Shows how to embed required XMP metadata (creation date, creator tool, producer, document ID) int... |
| [add-zugferd-attachment-validate-pdfa3](./add-zugferd-attachment-validate-pdfa3.cs) | Add ZUGFeRD attachment and validate PDF/A‑3A compliance | `Document`, `FileSpecification`, `Add` | Loads an existing PDF/A‑3 document, embeds a ZUGFeRD XML invoice as an attachment, saves the upda... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files with Descriptions to a PDF | `Document`, `Save`, `Page` | Shows how to add several file attachment annotations to a PDF, each with its own MIME type, descr... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `Convert` | Shows how to load a PDF, embed a ZUGFeRD XML file as an attached file, optionally convert the doc... |
| [auto-print-pdf-on-open](./auto-print-pdf-on-open.cs) | Auto‑Print PDF on Open with JavaScript Action | `Document`, `Add`, `JavascriptAction` | Demonstrates how to embed a JavaScript action in a PDF that automatically opens the print dialog ... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `Resolution`, `JpegDevice` | Demonstrates how to iterate through all PDF files in a folder, convert each page to a high‑resolu... |
| [batch-generate-zugferd-pdfs-from-csv](./batch-generate-zugferd-pdfs-from-csv.cs) | Batch Generate ZUGFeRD‑Compliant PDFs from CSV | `Document`, `Page`, `TextFragment` | Shows how to read invoice data from a CSV file, embed the matching ZUGFeRD XML as an attachment, ... |
| [batch-password-protection](./batch-password-protection.cs) | Batch Apply Password Protection to PDFs | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt all PDF files in a folder with the same user and owner passwords, app... |
| [batch-pdf-text-extraction-search-index](./batch-pdf-text-extraction-search-index.cs) | Batch PDF Text Extraction and Searchable Index | `Document`, `TextAbsorber`, `Pages` | Extracts text from all PDF files in a folder using Aspose.Pdf and stores the content in an in‑mem... |
| [calculate-order-total-pdf-form](./calculate-order-total-pdf-form.cs) | Calculate Order Total in PDF Form | `Document`, `Form`, `TextBoxField` | Demonstrates how to read quantity and price fields from a PDF form, compute the total amount, and... |
| [change-pdf-line-width](./change-pdf-line-width.cs) | Change PDF Line Width from 1 to 3 Points | `Document`, `Page`, `SetLineWidth` | Shows how to iterate PDF page operators and modify SetLineWidth operators to change line width fr... |
| [combine-multiple-graphs-2x2-grid](./combine-multiple-graphs-2x2-grid.cs) | Combine Multiple Graphs on a Single PDF Page (2x2 Grid) | `Document`, `Page`, `Graph` | Creates a PDF with four Aspose.Pdf Graph objects arranged in a two‑by‑two grid on one page, showi... |
| [compress-pdf-images-optimization](./compress-pdf-images-optimization.cs) | Compress PDF Images and Optimize Resources | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates how to use Aspose.Pdf's OptimizationOptions and ImageCompressionOptions to compress ... |
| [convert-pdf-page-to-high-resolution-png](./convert-pdf-page-to-high-resolution-png.cs) | Convert PDF Page to High-Resolution PNG | `Document`, `Resolution`, `PngDevice` | Demonstrates loading a PDF with Aspose.Pdf, setting a 300 DPI resolution, and converting the firs... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Shows how to load a PDF using Aspose.Pdf, configure DocSaveOptions to keep the original layout, f... |
| [convert-pdf-to-html-embedded-base64-images](./convert-pdf-to-html-embedded-base64-images.cs) | Convert PDF to HTML with Embedded Base64 Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Demonstrates converting a PDF document to a single HTML file using Aspose.Pdf while embedding all... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `Resolution`, `TiffSettings` | Demonstrates how to use Aspose.Pdf to load a PDF document and convert each page into a multi‑page... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A-2b Compliance | `Document`, `Convert`, `PdfFormat` | Loads a PDF file, converts it to PDF/A‑2b archival format using Aspose.Pdf, and saves the complia... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD attachment to PDF/A‑3U | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | Demonstrates loading a PDF that contains a ZUGFeRD XML attachment, converting it to PDF/A‑3U form... |
| ... | | | *and 53 more files* |

## Category Statistics
- Total examples: 83

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
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

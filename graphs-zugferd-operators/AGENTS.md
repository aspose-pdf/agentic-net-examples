---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

> **Graphs ZUGFeRD operators** in PDF using C# / .NET -- **89** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (89/89 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (23/89 files)
- `using Aspose.Pdf.Text;` (15/89 files)
- `using Aspose.Pdf.Forms;` (9/89 files)
- `using Aspose.Pdf.Drawing;` (7/89 files)
- `using Aspose.Pdf.Operators;` (6/89 files)
- `using Aspose.Pdf.Devices;` (5/89 files)
- `using Aspose.Pdf.Facades;` (2/89 files)
- `using Aspose.Pdf.Tagged;` (2/89 files)
- `using Aspose.Pdf.LogicalStructure;` (1/89 files)
- `using Aspose.Pdf.Optimization;` (1/89 files)
- `using Aspose.Pdf.Printing;` (1/89 files)
- `using Aspose.Pdf.Security;` (1/89 files)
- `using Aspose.Pdf.Signatures;` (1/89 files)
- `using Aspose.Pdf.Vector;` (1/89 files)
- `using System;` (89/89 files)
- `using System.IO;` (78/89 files)
- `using System.Collections.Generic;` (5/89 files)
- `using System.Linq;` (2/89 files)
- `using System.Drawing.Printing;` (1/89 files)
- `using System.Text.Json;` (1/89 files)
- `using System.Xml;` (1/89 files)
- `using System.Xml.Linq;` (1/89 files)
- `using System.Xml.Schema;` (1/89 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto‑Close JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to load a PDF with Aspose.Pdf, attach a JavaScript OpenAction that closes the document ... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `Metadata`, `Save` | Shows how to load an existing ZUGFeRD PDF with Aspose.Pdf, add custom metadata entries such as Pr... |
| [add-document-level-auto-save-javascript](./add-document-level-auto-save-javascript.cs) | Add Document-Level Auto‑Save JavaScript to PDF | `Document`, `OpenAction`, `Save` | Demonstrates embedding a document‑level JavaScript action that automatically saves the PDF at reg... |
| [add-expiry-javascript-to-pdf](./add-expiry-javascript-to-pdf.cs) | Add Expiry JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a document‑level JavaScript action that checks an expiry date and autom... |
| [add-file-attachment-annotation-to-pdf](./add-file-attachment-annotation-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to attach an external PDF file to a PDF document as a file attachment annotation... |
| [add-javascript-calculation-to-pdf-form-total](./add-javascript-calculation-to-pdf-form-total.cs) | Add JavaScript Calculation to PDF Form Total Field | `Document`, `FormField`, `JavascriptAction` | Shows how to embed JavaScript in a PDF using Aspose.Pdf to sum line‑item fields and write the res... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to set a JavaScript open action in a PDF using Aspose.Pdf so that the document a... |
| [add-javascript-password-prompt-to-pdf](./add-javascript-password-prompt-to-pdf.cs) | Add JavaScript Password Prompt to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a JavaScript OpenAction in a PDF that prompts the user for a password and clos... |
| [add-javascript-toggle-pdf-form-visibility](./add-javascript-toggle-pdf-form-visibility.cs) | Add JavaScript to Toggle PDF Form Section Visibility | `Document`, `ChoiceField`, `TextBoxField` | Demonstrates how to embed JavaScript in a PDF using Aspose.Pdf to hide a form field on open and s... |
| [add-js-open-action-jump-to-page-10](./add-js-open-action-jump-to-page-10.cs) | Add JavaScript Open Action to Jump to Page 10 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to assign a JavaScript open action to a PDF using Aspose.Pdf so that the documen... |
| [add-page-level-javascript-alert-to-pdf](./add-page-level-javascript-alert-to-pdf.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Page`, `JavascriptAction` | Shows how to attach a page‑level JavaScript action that displays an alert when a specific PDF pag... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark Behind PDF Content | `Document`, `Page`, `WatermarkArtifact` | Demonstrates how to place a semi‑transparent text watermark behind existing page content on every... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add XMP Metadata for PDF/A Compliance | `Document`, `DocumentInfo`, `SetXmpMetadata` | Shows how to embed required XMP metadata (DocumentID, CreatorTool) and enable PDF/A settings when... |
| [apply-pdf-security-allow-only-form-filling](./apply-pdf-security-allow-only-form-filling.cs) | Apply PDF Security to Allow Only Form Filling | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates encrypting a PDF with Aspose.Pdf so that only form filling is permitted while editin... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files to PDF with Custom Descriptions | `Document`, `Page`, `FileSpecification` | Demonstrates adding several file attachment annotations to a PDF, each with its own MIME type inf... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, embed a ZUGFeRD XML invoice as an attached file, convert the document to... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `Resolution`, `JpegDevice` | Demonstrates how to iterate through all PDF files in a folder, convert each page to a JPEG image ... |
| [batch-password-protection-pdfs](./batch-password-protection-pdfs.cs) | Batch Apply Password Protection to PDFs | `Document`, `Encrypt`, `CryptoAlgorithm` | Demonstrates how to encrypt all PDF files in a folder with the same user and owner passwords, app... |
| [batch-pdf-text-extraction-indexing](./batch-pdf-text-extraction-indexing.cs) | Batch PDF Text Extraction and Indexing | `Document`, `TextAbsorber`, `Accept` | Extracts text from all PDF files in a folder using Aspose.Pdf, builds a dictionary of file paths ... |
| [combine-multiple-graphs-on-single-pdf-page](./combine-multiple-graphs-on-single-pdf-page.cs) | Combine Multiple Graphs on a Single PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to place four Aspose.Pdf Graph objects on one PDF page in a 2×2 grid layout, cal... |
| [compress-pdf-images-optimization](./compress-pdf-images-optimization.cs) | Compress PDF Images Using Aspose.Pdf Optimization | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates how to reduce PDF file size by compressing and resizing images with Aspose.Pdf's Opt... |
| [convert-pdf-page-to-high-resolution-png](./convert-pdf-page-to-high-resolution-png.cs) | Convert PDF Page to High-Resolution PNG | `Document`, `Page`, `Resolution` | Demonstrates how to load a PDF document with Aspose.Pdf, select the first page, and export it as ... |
| [convert-pdf-to-docx-layout](./convert-pdf-to-docx-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `Save` | Demonstrates how to load a PDF file and save it as a DOCX document while preserving the original ... |
| [convert-pdf-to-html-embedded-images](./convert-pdf-to-html-embedded-images.cs) | Convert PDF to HTML with Embedded Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Shows how to convert a PDF into a single HTML file with all resources, including images, embedded... |
| [convert-pdf-to-multi-page-tiff](./convert-pdf-to-multi-page-tiff.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `TiffDevice`, `Resolution` | Shows how to load a PDF with Aspose.Pdf and convert each page into a separate layer of a multi‑pa... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A-2b Compliance | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF, converting it to PDF/A‑2b archival format with Aspose.Pdf, and saving... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD attachment to PDF/A‑3U | `Document`, `Convert`, `PdfFormat` | Demonstrates loading a PDF that contains a ZUGFeRD XML attachment, converting it to PDF/A‑3U whil... |
| [create-bar-chart-pdf-custom-colors](./create-bar-chart-pdf-custom-colors.cs) | Create Bar Chart PDF with Custom Colors and Axis Labels | `Document`, `Page`, `Graph` | This example demonstrates how to generate a PDF containing a bar chart using Aspose.Pdf's Graph A... |
| [create-line-graph-multiple-stroke-styles](./create-line-graph-multiple-stroke-styles.cs) | Create Line Graph with Multiple Stroke Styles in PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a line graph with three data series, each using a different stroke style... |
| [create-multi-column-pdf-layout](./create-multi-column-pdf-layout.cs) | Create Multi-Column PDF Layout with Aspose.Pdf | `Document`, `Page`, `TextFragment` | Shows how to programmatically arrange text in two columns by calculating X/Y positions and adding... |
| ... | | | *and 59 more files* |

## Category Statistics
- Total examples: 89

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

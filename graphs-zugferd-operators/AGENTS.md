---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

> **Graphs ZUGFeRD operators** in PDF using C# / .NET -- **86** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (86/86 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (23/86 files)
- `using Aspose.Pdf.Text;` (14/86 files)
- `using Aspose.Pdf.Forms;` (12/86 files)
- `using Aspose.Pdf.Drawing;` (7/86 files)
- `using Aspose.Pdf.Devices;` (6/86 files)
- `using Aspose.Pdf.Operators;` (4/86 files)
- `using Aspose.Pdf.Optimization;` (1/86 files)
- `using Aspose.Pdf.Security;` (1/86 files)
- `using Aspose.Pdf.Tagged;` (1/86 files)
- `using System;` (86/86 files)
- `using System.IO;` (81/86 files)
- `using System.Collections.Generic;` (10/86 files)
- `using System.Text;` (3/86 files)
- `using System.Text.Json;` (3/86 files)
- `using System.Drawing;` (2/86 files)
- `using System.Collections;` (1/86 files)
- `using System.Diagnostics;` (1/86 files)
- `using System.Drawing.Printing;` (1/86 files)
- `using System.Linq;` (1/86 files)
- `using System.Reflection;` (1/86 files)
- `using System.Security.Cryptography.X509Certificates;` (1/86 files)
- `using System.Text.RegularExpressions;` (1/86 files)
- `using System.Xml;` (1/86 files)
- `using System.Xml.Linq;` (1/86 files)
- `using System.Xml.Schema;` (1/86 files)

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
| [add-auto-close-javascript-to-pdf](./add-auto-close-javascript-to-pdf.cs) | Add Auto-Close JavaScript to PDF | `Document`, `OpenAction`, `Save` | Demonstrates embedding JavaScript in a PDF with Aspose.Pdf to automatically close the document af... |
| [add-auto-print-javascript-action-to-pdf](./add-auto-print-javascript-action-to-pdf.cs) | Add Auto‑Print JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a JavaScript action in a PDF that automatically opens the print dialog ... |
| [add-auto-save-javascript-to-pdf](./add-auto-save-javascript-to-pdf.cs) | Add Auto‑Save JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a document‑level JavaScript action that automatically saves the PDF at defined... |
| [add-custom-metadata-to-zugferd-pdf](./add-custom-metadata-to-zugferd-pdf.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `DocumentInfo`, `Save` | Shows how to load an existing ZUGFeRD PDF, add custom metadata entries such as ProjectCode and De... |
| [add-encrypted-file-attachment-to-pdf](./add-encrypted-file-attachment-to-pdf.cs) | Encrypt PDF and Add Encrypted Attachment | `Document`, `FileSpecification`, `Add` | Shows how to embed a file into a PDF and encrypt the whole document (including the attachment) wi... |
| [add-expiry-javascript-action-to-pdf](./add-expiry-javascript-action-to-pdf.cs) | Add Expiry JavaScript Action to PDF | `Document`, `Document(string)`, `OpenAction` | Shows how to embed a document‑level JavaScript OpenAction that checks an expiry date and closes t... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to embed a PDF file as a file‑attachment annotation in another PDF, including setting a... |
| [add-javascript-calculation-to-pdf-total](./add-javascript-calculation-to-pdf-total.cs) | Add JavaScript Calculation to PDF Form Total Field | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates how to attach a JavaScript calculate action to a PDF form using Aspose.Pdf, summing ... |
| [add-javascript-open-action-jump-to-page-5](./add-javascript-open-action-jump-to-page-5.cs) | Add JavaScript Open Action to Jump to Page 5 | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript open action to a PDF that automatically navigates to page... |
| [add-javascript-toggle-optional-sections](./add-javascript-toggle-optional-sections.cs) | Add JavaScript to Toggle PDF Form Section Visibility | `Document`, `ComboBoxField`, `TextBoxField` | Demonstrates creating a combo box and optional text fields in a PDF, setting their initial hidden... |
| [add-page-level-javascript-alert-to-pdf](./add-page-level-javascript-alert-to-pdf.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `JavascriptAction`, `Page` | Shows how to embed a page‑level JavaScript action that displays an alert when the user navigates ... |
| [add-password-prompt-javascript-to-pdf](./add-password-prompt-javascript-to-pdf.cs) | Add JavaScript Password Prompt to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed JavaScript in a PDF using Aspose.Pdf to request a password when the doc... |
| [add-semi-transparent-text-watermark__v3](./add-semi-transparent-text-watermark__v3.cs) | Add Semi-Transparent Watermark Behind PDF Content | `Document`, `Page`, `TextStamp` | Demonstrates how to place a semi‑transparent text watermark behind the existing content on every ... |
| [add-signature-field-with-js-validation](./add-signature-field-with-js-validation.cs) | Add a Digital Signature Field to a PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a page, and place a digital signature field using Aspose.... |
| [add-xmp-metadata-pdfa-compliance](./add-xmp-metadata-pdfa-compliance.cs) | Add PDF/A XMP Metadata to a PDF | `Document`, `Metadata`, `Metadata` | Shows how to embed required PDF/A XMP metadata such as document ID, creator tool, and timestamps ... |
| [attach-multiple-files-to-pdf](./attach-multiple-files-to-pdf.cs) | Attach Multiple Files with Descriptions to a PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed several files into a PDF, assign custom descriptions, and optionally ad... |
| [attach-zugferd-xml-to-pdf](./attach-zugferd-xml-to-pdf.cs) | Attach ZUGFeRD XML to PDF and Convert to PDF/A‑3B | `Document`, `FileSpecification`, `Convert` | Demonstrates embedding a ZUGFeRD invoice XML file into a PDF as an embedded file, setting the AFR... |
| [batch-apply-pdf-password-protection](./batch-apply-pdf-password-protection.cs) | Batch Encrypt PDFs with Password Protection | `Document`, `Permissions`, `CryptoAlgorithm` | Demonstrates how to encrypt all PDF files in a folder using Aspose.Pdf by applying the same user ... |
| [batch-convert-pdfs-to-jpeg](./batch-convert-pdfs-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to iterate through all PDF files in a folder, convert each page to a high‑resolu... |
| [batch-generate-zugferd-pdfs-from-csv](./batch-generate-zugferd-pdfs-from-csv.cs) | Batch Generate ZUGFeRD PDF Invoices from CSV | `Document`, `Page`, `TextFragment` | Shows how to read invoice data from a CSV file, create PDF/A‑3B invoices with Aspose.Pdf, embed t... |
| [calculate-total-order-amount](./calculate-total-order-amount.cs) | Add JavaScript Calculation to PDF Form Total Field | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates loading a PDF form, retrieving a TextBoxField, attaching a JavaScript Calculate acti... |
| [change-pdf-line-width](./change-pdf-line-width.cs) | Change PDF Line Width from 1pt to 3pt | `Document`, `Page`, `SetLineWidth` | Shows how to load a PDF, iterate through each page's content operators, locate SetLineWidth opera... |
| [combine-multiple-graphs-on-single-pdf-page](./combine-multiple-graphs-on-single-pdf-page.cs) | Combine Four Graph Images into a 2x2 Grid PDF | `Document`, `Page`, `ImageStamp` | Shows how to place four image files onto a single PDF page arranged in a 2‑by‑2 grid using Aspose... |
| [compress-pdf-images-optimization](./compress-pdf-images-optimization.cs) | Compress PDF Using Image Optimization Options | `Document`, `OptimizationOptions`, `ImageCompressionOptions` | Demonstrates how to reduce a PDF file size by applying image compression, down‑sampling, and clea... |
| [convert-pdf-pages-to-high-resolution-png](./convert-pdf-pages-to-high-resolution-png.cs) | Convert PDF Page to High-Resolution PNG | `Document`, `PngDevice`, `Resolution` | Demonstrates loading a PDF with Aspose.Pdf, selecting a specific page, and rendering it to a PNG ... |
| [convert-pdf-to-docx-preserve-layout](./convert-pdf-to-docx-preserve-layout.cs) | Convert PDF to DOCX with Layout Preservation | `Document`, `DocSaveOptions`, `RecognitionMode` | Shows how to load a PDF using Aspose.Pdf and save it as a DOCX file while preserving the original... |
| [convert-pdf-to-html-with-embedded-images](./convert-pdf-to-html-with-embedded-images.cs) | Convert PDF to HTML with Embedded Base64 Images | `Document`, `HtmlSaveOptions`, `PartsEmbeddingModes` | Demonstrates converting a PDF file to a single HTML document using Aspose.Pdf, embedding all imag... |
| [convert-pdf-to-multi-page-tiff__v3](./convert-pdf-to-multi-page-tiff__v3.cs) | Convert PDF to Multi‑Page TIFF | `Document`, `TiffSettings`, `Resolution` | Demonstrates how to load a PDF with Aspose.Pdf, configure TIFF settings, and export all pages as ... |
| [convert-pdf-to-pdfa-2b](./convert-pdf-to-pdfa-2b.cs) | Convert PDF to PDF/A‑2b for Archival Compliance | `Document`, `Convert`, `PdfFormat` | Loads an existing PDF, converts it to PDF/A‑2b compliance using Aspose.Pdf, and saves the resulti... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD attachment to PDF/A-3u | `Document`, `Convert`, `Save` | Shows how to load a PDF containing a ZUGFeRD XML attachment, convert it to PDF/A‑3u while preserv... |
| ... | | | *and 56 more files* |

## Category Statistics
- Total examples: 86

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
Updated: 2026-11-12 | Run: `20261112_223639_6830fe`
<!-- AUTOGENERATED:END -->

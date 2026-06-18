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

- `using Aspose.Pdf;` (82/82 files) ← category-specific
- `using Aspose.Pdf.Text;` (26/82 files)
- `using Aspose.Pdf.Annotations;` (25/82 files)
- `using Aspose.Pdf.Forms;` (10/82 files)
- `using Aspose.Pdf.Drawing;` (7/82 files)
- `using Aspose.Pdf.Devices;` (5/82 files)
- `using Aspose.Pdf.Operators;` (4/82 files)
- `using Aspose.Pdf.Optimization;` (1/82 files)
- `using Aspose.Pdf.Security;` (1/82 files)
- `using Aspose.Pdf.Tagged;` (1/82 files)
- `using System;` (82/82 files)
- `using System.IO;` (34/82 files)
- `using System.Text;` (10/82 files)
- `using System.Collections.Generic;` (4/82 files)
- `using System.Drawing;` (1/82 files)
- `using System.Text.Json;` (1/82 files)
- `using System.Xml;` (1/82 files)
- `using System.Xml.Schema;` (1/82 files)

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
| [add-auto-close-javascript](./add-auto-close-javascript.cs) | Add Auto-Close JavaScript to PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to embed JavaScript that automatically closes the PDF window after a timeout. |
| [add-custom-metadata-zugferd](./add-custom-metadata-zugferd.cs) | Add Custom Metadata to ZUGFeRD PDF | `Document`, `Add`, `Save` | Creates a sample PDF, then adds custom metadata entries such as project code and department, and ... |
| [add-document-expiry-javascript](./add-document-expiry-javascript.cs) | Add Document-Level JavaScript Expiry Action | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to add a document‑level JavaScript action that closes the PDF after a specified ... |
| [add-document-level-javascript-autosave](./add-document-level-javascript-autosave.cs) | Add Document-Level JavaScript for Auto‑Save | `Document`, `Save`, `JavaScript` | Creates a PDF, then adds a document‑level JavaScript that triggers automatic saving at regular in... |
| [add-encrypted-attachment](./add-encrypted-attachment.cs) | Add Encrypted Attachment to PDF | `Document`, `Page`, `TextFragment` | Creates a PDF, adds a text file attachment, encrypts the PDF so only authorized users can open it. |
| [add-file-attachment](./add-file-attachment.cs) | Add File Attachment with Description to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed a PDF file as an attachment annotation with a description and MIME type. |
| [add-javascript-calculation](./add-javascript-calculation.cs) | Add JavaScript Calculation to PDF Form Fields | `Document`, `Page`, `Rectangle` | Creates a PDF, adds price fields and a total field, and attaches JavaScript to automatically calc... |
| [add-javascript-open-action](./add-javascript-open-action.cs) | Add JavaScript Open Action to Jump to Page Five | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to add a JavaScript open action that navigates to page five when the PDF is opened. |
| [add-javascript-password-prompt](./add-javascript-password-prompt.cs) | Add JavaScript Prompt for Password to PDF | `Document`, `TextFragment`, `JavaScriptAction` | Creates a PDF, then adds a JavaScript action that asks the user for a password before showing the... |
| [add-javascript-print-dialog](./add-javascript-print-dialog.cs) | Add JavaScript Print Dialog on PDF Open | `Document`, `JavascriptAction`, `OpenAction` | Creates a PDF and adds a JavaScript action that automatically opens the print dialog when the doc... |
| [add-page-level-javascript](./add-page-level-javascript.cs) | Add Page-Level JavaScript Alert to PDF | `Document`, `Page`, `TextAnnotation` | Demonstrates how to attach a JavaScript action to a PDF page that shows an alert when the page be... |
| [add-pdfa-xmp-metadata](./add-pdfa-xmp-metadata.cs) | Add PDF/A XMP Metadata (Document ID and Creator Tool) | `Document`, `DocumentInfo`, `Add` | Creates a sample PDF, then adds required XMP metadata entries for PDF/A compliance such as Docume... |
| [add-semi-transparent-text-watermark](./add-semi-transparent-text-watermark.cs) | Add Semi-Transparent Text Watermark Behind PDF Content | `Document`, `Page`, `TextFragment` | Creates a sample PDF and adds a semi‑transparent text watermark behind the existing page content ... |
| [aes256-encryption](./aes256-encryption.cs) | Apply AES‑256 Encryption with User and Owner Passwords | `Document`, `TextFragment`, `Permissions` | Demonstrates how to encrypt a PDF using AES‑256, setting both user and owner passwords for access... |
| [apply-form-filling-security](./apply-form-filling-security.cs) | Apply Form Filling Only Security to PDF | `Document`, `Permissions`, `CryptoAlgorithm` | Creates a PDF, then encrypts it so only form filling is allowed while editing, printing, and cont... |
| [attach-multiple-files](./attach-multiple-files.cs) | Attach Multiple Files with Custom MIME Types and Description... | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed several files into a PDF, each with its own MIME type and description. |
| [attach-zugferd-xml](./attach-zugferd-xml.cs) | Attach ZUGFeRD XML to PDF using File Attachment Annotation | `Document`, `Page`, `FileAttachmentAnnotation` | Demonstrates how to embed a ZUGFeRD XML invoice into a PDF as a file attachment annotation using ... |
| [batch-convert-pdf-to-jpeg](./batch-convert-pdf-to-jpeg.cs) | Batch Convert PDFs to JPEG Images | `Document`, `JpegDevice`, `Resolution` | Demonstrates how to create sample PDFs, enumerate PDF files in a directory, and convert each page... |
| [batch-generate-zugferd-pdfs](./batch-generate-zugferd-pdfs.cs) | Batch Generate ZUGFeRD‑Compliant PDFs from CSV and Embed XML | `Document`, `Page`, `TextFragment` | Creates invoice PDFs from a CSV list, embeds the corresponding ZUGFeRD XML file into each PDF, an... |
| [batch-invoice-graph](./batch-invoice-graph.cs) | Batch Generate Invoice PDFs with Sales Graphs | `Document`, `Page`, `TextFragment` | Creates a template PDF and generates multiple invoice PDFs, each containing a sales bar graph for... |
| [batch-password-protect-pdfs](./batch-password-protect-pdfs.cs) | Batch Apply Password Protection to PDFs | `Document`, `Permissions`, `CryptoAlgorithm` | Creates sample PDFs and then encrypts all PDFs in the current folder with identical user and owne... |
| [batch-text-extraction-index](./batch-text-extraction-index.cs) | Batch Extract Text from PDFs and Build Simple Search Index | `Document`, `Page`, `TextFragment` | Creates sample PDFs, extracts their text using TextAbsorber, and builds an in‑memory keyword inde... |
| [calculate-total-order-amount](./calculate-total-order-amount.cs) | Calculate Total Order Amount Using JavaScript in PDF Form | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates adding form fields to a PDF and using JavaScript actions to compute a line‑item amou... |
| [change-line-width](./change-line-width.cs) | Change Line Width Operators in PDF | `Document`, `Page`, `SetLineWidth` | Demonstrates how to modify SetLineWidth operators in an existing PDF to change line width from 1 ... |
| [combine-graphs-2x2-grid](./combine-graphs-2x2-grid.cs) | Combine Multiple Graphs on a Single PDF Page (2x2 Grid) | `Document`, `Page`, `Graph` | Creates a PDF with a single page containing four graphs arranged in a two‑by‑two grid. |
| [compress-pdf-images](./compress-pdf-images.cs) | Compress Images in PDF to Reduce File Size | `Document`, `Page`, `Image` | Demonstrates how to compress images in a PDF using Aspose.Pdf OptimizationOptions to reduce file ... |
| [convert-pdf-to-docx](./convert-pdf-to-docx.cs) | Convert PDF to DOCX preserving layout | `Document`, `Table`, `DocxSaveOptions` | Creates a sample PDF with text and a table, then converts it to a DOCX file while keeping the ori... |
| [convert-pdf-zugferd-to-pdfa3u](./convert-pdf-zugferd-to-pdfa3u.cs) | Convert PDF with ZUGFeRD attachment to PDF/A-3u preserving X... | `Document`, `FileSpecification`, `EmbeddedFile` | Creates a PDF containing an embedded ZUGFeRD XML file and converts it to PDF/A‑3u while keeping t... |
| [create-pdf-invoice-embed-zugferd](./create-pdf-invoice-embed-zugferd.cs) | Create PDF Invoice and Embed ZUGFeRD XML | `Document`, `Page`, `TextFragment` | Generates a simple PDF invoice, creates ZUGFeRD XML from an order object, and embeds the XML into... |
| [create-toc-with-outlines](./create-toc-with-outlines.cs) | Create Table of Contents Page with Clickable Outline Entries | `Document`, `Page`, `TextFragment` | Demonstrates how to generate a PDF with a Table of Contents page and clickable outline entries li... |
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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

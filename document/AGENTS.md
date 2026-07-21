---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

> **Document** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **document** category.
This folder contains standalone C# examples for document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (117/117 files) ← category-specific
- `using Aspose.Pdf.Text;` (30/117 files)
- `using Aspose.Pdf.Annotations;` (25/117 files)
- `using Aspose.Pdf.Forms;` (13/117 files)
- `using Aspose.Pdf.Devices;` (6/117 files)
- `using Aspose.Pdf.Drawing;` (5/117 files)
- `using Aspose.Pdf.LogicalStructure;` (5/117 files)
- `using Aspose.Pdf.Optimization;` (5/117 files)
- `using Aspose.Pdf.Tagged;` (5/117 files)
- `using Aspose.Pdf.Facades;` (2/117 files)
- `using Aspose.Pdf.Comparison;` (1/117 files)
- `using Aspose.Pdf.Multithreading;` (1/117 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (1/117 files)
- `using System;` (117/117 files)
- `using System.IO;` (105/117 files)
- `using System.Collections.Generic;` (3/117 files)
- `using System.Drawing;` (3/117 files)
- `using System.Data;` (2/117 files)
- `using System.Diagnostics;` (2/117 files)
- `using System.Text;` (2/117 files)
- `using NUnit.Framework;` (1/117 files)
- `using System.Drawing.Imaging;` (1/117 files)
- `using System.Linq;` (1/117 files)
- `using System.Threading;` (1/117 files)
- `using System.Threading.Tasks;` (1/117 files)
- `using System.Xml;` (1/117 files)

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
| [add-background-color-to-pdf-page](./add-background-color-to-pdf-page.cs) | Add Background Color to PDF Page Using Rectangle with Opacit... | `Document`, `Page`, `Graph` | Demonstrates how to apply a semi‑transparent background color to a PDF page by drawing a full‑pag... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate through PDF pages, locate each image, and insert a styled text caption benea... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF document, adding a checkbox form field, setting its default state to ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an existing PDF with Aspose.Pdf, iterate over all pages, and place an ImageStam... |
| [add-custom-signature-appearance-last-page](./add-custom-signature-appearance-last-page.cs) | Add Custom Signature Appearance to Last PDF Page | `Document`, `Page`, `Rectangle` | Shows how to create a signature field on the last page of a PDF and apply a custom signature imag... |
| [add-custom-xml-xmp-metadata-to-pdf](./add-custom-xml-xmp-metadata-to-pdf.cs) | Add Custom XML XMP Metadata to PDF | `Document`, `DocumentInfo`, `Add` | The example loads an existing PDF, adds custom key‑value pairs to the document information dictio... |
| [add-diagonal-text-stamp-to-pdf-pages](./add-diagonal-text-stamp-to-pdf-pages.cs) | Add Diagonal Text Stamp to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Demonstrates loading a PDF with Aspose.Pdf, creating a TextStamp with a custom message, configuri... |
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add Digital Signature Field and Sign PDF with Self‑Signed Ce... | `Document`, `Page`, `SignatureField` | Demonstrates how to create a signature field on a PDF page, configure a PKCS#7 self‑signed certif... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF Document | `Document`, `Page`, `Heading` | Demonstrates loading an existing PDF, creating a heading with dynamic content (date and user name... |
| [add-heading-to-pdf-outline](./add-heading-to-pdf-outline.cs) | Add Heading to PDF Outline (Bookmark) | `Document`, `OutlineItemCollection`, `FitExplicitDestination` | Demonstrates how to insert a new outline (bookmark) into an existing PDF and set its destination ... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a clickable link annotation that opens an external website when clicked, usin... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indented Paragraph with Custom Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | Shows how to load an existing PDF, create a TextParagraph with first‑line and subsequent‑line ind... |
| [add-javascript-calculation-to-pdf-form-fields](./add-javascript-calculation-to-pdf-form-fields.cs) | Add JavaScript Calculation to PDF Form Fields | `Document`, `Page`, `NumberField` | Demonstrates creating a PDF with two numeric fields and a read‑only sum field, then attaching a J... |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed a JavaScript action that shows an alert dialog when the PDF document is... |
| [add-javascript-total-calculation-to-pdf-form](./add-javascript-total-calculation-to-pdf-form.cs) | Add JavaScript Total Calculation to PDF Form Fields | `Document`, `Form`, `TextBoxField` | Demonstrates how to programmatically attach a JavaScript calculate action to PDF form fields usin... |
| [add-line-annotation-custom-color-thickness](./add-line-annotation-custom-color-thickness.cs) | Add Line Annotation with Custom Color and Thickness to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a line annotation with a custom blue color and thickness, ad... |
| [add-line-separator-annotation-to-pdf-page](./add-line-separator-annotation-to-pdf-page.cs) | Add Line Separator Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a horizontal line annotation as a visual separator on a PDF page using... |
| [add-link-annotation-open-pdf-attachment](./add-link-annotation-open-pdf-attachment.cs) | Create Link Annotation to Open PDF Attachment | `Document`, `Page`, `Rectangle` | Demonstrates how to add a link annotation to a PDF page that opens an external PDF attachment whe... |
| [add-page-number-footer-to-pdf](./add-page-number-footer-to-pdf.cs) | Add Page Number Footer to PDF | `Document`, `Page`, `TextStamp` | Shows how to add a custom footer that displays "Page X of Y" on every page of an existing PDF usi... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Shows how to insert dynamic page numbers in the footer of each PDF page using Aspose.Pdf's PageNu... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a TextAnnotation (sticky‑note) with an associated PopupAnnotation that displa... |
| [add-reusable-background-image-to-all-pdf-pages](./add-reusable-background-image-to-all-pdf-pages.cs) | Add Reusable Background Image to All PDF Pages | `Document`, `PdfPageStamp`, `AddStamp` | Shows how to use a single‑page PDF as a reusable background stamp and apply it to every page of a... |
| [add-signature-field-to-pdf](./add-signature-field-to-pdf.cs) | Add Signature Field to PDF with Default Appearance | `Document`, `Form`, `DefaultAppearance` | Demonstrates how to load a PDF, set a default appearance for form fields, create a signature fiel... |
| [add-structured-headings-to-pdf](./add-structured-headings-to-pdf.cs) | Add Structured Headings to PDF | `Document`, `Page`, `Heading` | Demonstrates how to create a PDF, add Heading objects with different levels and automatic numberi... |
| [add-styled-table-with-borders-to-pdf](./add-styled-table-with-borders-to-pdf.cs) | Add Styled Table with Borders and Alternating Row Colors to ... | `Document`, `Page`, `Table` | Demonstrates creating a table with a full border, header styling, and alternating row background ... |
| [add-visible-signature-field-and-lock-pdf](./add-visible-signature-field-and-lock-pdf.cs) | Add Visible Signature Field and Lock PDF after Signing | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to create a visible signature field in a PDF, sign it with a PKCS#7 certificate,... |
| [apply-semi-transparent-image-watermark-to-pdf](./apply-semi-transparent-image-watermark-to-pdf.cs) | Apply Semi-Transparent Image Watermark to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Demonstrates adding a semi‑transparent image overlay as a watermark to each page of a PDF using A... |
| [attach-pdf-portfolio-with-metadata](./attach-pdf-portfolio-with-metadata.cs) | Attach PDF Portfolio and Set Custom Metadata | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates embedding a PDF as a portfolio (embedded file) into an existing PDF, adding an optio... |
| [auto-tagging-and-headings-pdf](./auto-tagging-and-headings-pdf.cs) | Create Clean Navigable PDF with Auto‑Tagging and Headings | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates how to enable Aspose.Pdf auto‑tagging for PDF sanitization, set document metadata, a... |
| [batch-sanitize-pdfs](./batch-sanitize-pdfs.cs) | Batch Sanitize PDFs and Save Cleaned Copies | `Document`, `RemoveMetadata`, `RemovePdfaCompliance` | Demonstrates how to iterate over a folder of PDF files, remove metadata and compliance flags, opt... |
| ... | | | *and 87 more files* |

## Category Statistics
- Total examples: 117

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.ArtifactCollection`
- `Aspose.Pdf.ArtifactCollection.Add`
- `Aspose.Pdf.ArtifactCollection.CopyTo`
- `Aspose.Pdf.ArtifactCollection.Count`
- `Aspose.Pdf.ArtifactCollection.Delete`
- `Aspose.Pdf.ArtifactCollection.FindByValue`
- `Aspose.Pdf.ArtifactCollection.GetEnumerator`
- `Aspose.Pdf.ArtifactCollection.IsReadOnly`
- `Aspose.Pdf.ArtifactCollection.IsSynchronized`
- `Aspose.Pdf.ArtifactCollection.Item`
- `Aspose.Pdf.ArtifactCollection.SyncRoot`
- `Aspose.Pdf.ArtifactCollection.Update`
- `Aspose.Pdf.AutoTaggingSettings`
- `Aspose.Pdf.AutoTaggingSettings.Default`
- `Aspose.Pdf.AutoTaggingSettings.EnableAutoTagging`

### Rules
- Create MarkdownSaveOptions with parameterless constructor: new MarkdownSaveOptions().
- To convert PDF to Markdown: create MarkdownSaveOptions, configure options, then call document.Save(outputPath, options).
- Configure MarkdownSaveOptions by setting properties: ExtractVectorGraphics, AreaToExtract, SubscriptAndSuperscriptConversion, ResourcesDirectoryName, UseImageHtmlTag.
- Create Matrix3D with parameterless constructor: new Matrix3D().
- Create Matrix3D with: new Matrix3D(double[] matrix3DArray).

### Warnings
- Ensure the output file extension matches the format when using MarkdownSaveOptions.
- Ensure the output file extension matches the format when using SaveOptions.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

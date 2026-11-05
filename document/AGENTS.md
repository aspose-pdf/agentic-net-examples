---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

> **Document** in PDF using C# / .NET -- **117** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Text;` (41/117 files)
- `using Aspose.Pdf.Annotations;` (25/117 files)
- `using Aspose.Pdf.Forms;` (12/117 files)
- `using Aspose.Pdf.LogicalStructure;` (6/117 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (6/117 files)
- `using Aspose.Pdf.Tagged;` (6/117 files)
- `using Aspose.Pdf.Devices;` (5/117 files)
- `using Aspose.Pdf.Drawing;` (3/117 files)
- `using Aspose.Pdf.Facades;` (1/117 files)
- `using System;` (117/117 files)
- `using System.IO;` (111/117 files)
- `using System.Collections.Generic;` (6/117 files)
- `using System.Drawing;` (5/117 files)
- `using System.Linq;` (3/117 files)
- `using System.Text;` (3/117 files)
- `using System.Reflection;` (2/117 files)
- `using System.Security.Cryptography.X509Certificates;` (2/117 files)
- `using System.Diagnostics;` (1/117 files)
- `using System.Drawing.Imaging;` (1/117 files)
- `using System.Security.Cryptography;` (1/117 files)
- `using System.Text.RegularExpressions;` (1/117 files)
- `using System.Xml.Linq;` (1/117 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Table) | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to load a PDF, enable tagged content, and add accessibility tags such as heading... |
| [add-background-color-to-pdf-page](./add-background-color-to-pdf-page.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Shows how to add a semi‑transparent light‑gray rectangle as a background to the first page of a P... |
| [add-background-template-to-pdf-pages](./add-background-template-to-pdf-pages.cs) | Add Background Image to All PDF Pages | `Document`, `ImageStamp`, `PageInfo` | Shows how to apply a background image to every page of a PDF using Aspose.Pdf by creating an Imag... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Each Image in a PDF | `Document`, `Page`, `XImage` | Demonstrates how to iterate through PDF pages, locate images, and add a styled caption paragraph ... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a checkbox form field into an existing PDF document and set its default state... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an existing PDF with Aspose.Pdf, create an ImageStamp for a logo, position it a... |
| [add-custom-document-info-and-xmp-metadata](./add-custom-document-info-and-xmp-metadata.cs) | Add Custom XML Metadata to PDF | `Document`, `DocumentInfo`, `Save` | Demonstrates how to embed custom XML metadata and simple key/value pairs into a PDF using Aspose.... |
| [add-diagonal-text-stamp-to-pdf](./add-diagonal-text-stamp-to-pdf.cs) | Add Diagonal Text Stamp to PDF Pages | `Document`, `Page`, `TextStamp` | Shows how to load a PDF, iterate through its pages, and add a rotated text stamp (e.g., CONFIDENT... |
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add Digital Signature to PDF with Self‑Signed Certificate | `Document`, `SignatureField`, `PKCS7` | Shows how to generate a self‑signed certificate, create a signature field on a PDF page, and appl... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF Using Tagged Content | `Document`, `ITaggedContent`, `AutoTaggingSettings` | Shows how to load a PDF, create a level‑1 heading with dynamic text (user name and date) via the ... |
| [add-hierarchical-numbered-headings](./add-hierarchical-numbered-headings.cs) | Apply Decimal Numbering to PDF Headings | `Document`, `Page`, `Heading` | Shows how to add hierarchical headings with Arabic decimal numbering to a PDF document using Aspo... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `LinkAnnotation` | Demonstrates how to insert a clickable hyperlink annotation that opens an external website in a P... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indentation and Line Spacing to PDF Text | `Document`, `Page`, `TextFragment` | Loads an existing PDF, creates a TextFragment, applies a left margin for paragraph indentation an... |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to attach a JavaScript action that runs when a PDF is opened, displaying an aler... |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Add JavaScript Sum Calculation to PDF Form | `Document`, `Page`, `TextBoxField` | Demonstrates creating a PDF with two input fields, a read‑only result field, and a button that ru... |
| [add-javascript-sum-numeric-pdf-form-fields](./add-javascript-sum-numeric-pdf-form-fields.cs) | Add JavaScript to Sum Numeric PDF Form Fields | `Document`, `JavascriptAction`, `Form` | Shows how to embed a custom JavaScript function in a PDF with Aspose.Pdf to calculate and display... |
| [add-level-1-heading-to-pdf](./add-level-1-heading-to-pdf.cs) | Add Level 1 Heading to PDF | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to create a new PDF document, set its language and title, and add a Level 1 head... |
| [add-line-annotation-with-custom-color-thickness](./add-line-annotation-with-custom-color-thickness.cs) | Add Line Annotation with Custom Color and Thickness to PDF | `Document`, `TextFragmentAbsorber`, `TextFragment` | Shows how to locate specific text in a PDF using TextFragmentAbsorber and overlay it with a line ... |
| [add-line-separator-annotation-to-pdf-page](./add-line-separator-annotation-to-pdf-page.cs) | Add Line Separator Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a horizontal line annotation as a section separator on a PDF page using Aspos... |
| [add-link-annotation-open-pdf-attachment](./add-link-annotation-open-pdf-attachment.cs) | Add Clickable Attachment Annotation to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates embedding a PDF file into another PDF and creating a file‑attachment annotation that... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Table of Contents to PDF with Aspose.Pdf | `Document`, `Page`, `TextFragment` | Demonstrates how to detect headings in an existing PDF, insert a new page, and create a clickable... |
| [add-new-bookmark-to-pdf-outline](./add-new-bookmark-to-pdf-outline.cs) | Insert New Bookmark into PDF Outline | `Document`, `OutlineItemCollection`, `GoToAction` | Shows how to add a top‑level outline (bookmark) to an existing PDF, place it at a specific index ... |
| [add-page-count-footer-to-pdf](./add-page-count-footer-to-pdf.cs) | Add Custom Footer with Page Numbers to PDF | `Document`, `Page`, `TextStamp` | Shows how to insert a centered footer on each PDF page that displays "Page X of Y" using Aspose.Pdf. |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `TextStamp` | Shows how to insert sequential page numbers at the bottom center of each page in a PDF using Aspo... |
| [add-page-with-text-to-pdf](./add-page-with-text-to-pdf.cs) | Convert PDF to HTML with Progress Indicator | `Document`, `HtmlSaveOptions`, `Save` | Shows how to load a PDF, convert it to HTML using Aspose.Pdf, and manually update a console progr... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load a PDF, create a TextAnnotation (pop‑up note) with a title and contents, ... |
| [add-signature-field-to-pdf](./add-signature-field-to-pdf.cs) | Add Signature Field with Styled Appearance to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load a PDF, create a signature form field, customize its background, border a... |
| [add-signature-image-stamp-to-last-page](./add-signature-image-stamp-to-last-page.cs) | Add Signature Appearance with Image on Last PDF Page | `Document`, `SignatureField`, `ImageStamp` | Demonstrates how to create a signature field on the last page of a PDF and apply a custom image a... |
| [add-styled-table-alternating-row-colors](./add-styled-table-alternating-row-colors.cs) | Add Styled Table with Borders and Alternating Row Colors to ... | `Document`, `Page`, `Table` | Shows how to create a three‑column table, apply outer and cell borders, style the header, add alt... |
| [add-watermark-layer-merge-layers](./add-watermark-layer-merge-layers.cs) | Add Watermark Artifact Layer to PDF Pages | `Document`, `Save`, `TextState` | Demonstrates how to create a watermark artifact as an optional‑content layer, apply it to each pa... |
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
Updated: 2026-11-05 | Run: `20261105_075327_f480ef`
<!-- AUTOGENERATED:END -->

---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

> **Document** in PDF using C# / .NET -- **175** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **document** category.
This folder contains standalone C# examples for document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (120/175 files) ← category-specific
- `using Aspose.Pdf.Text;` (33/175 files)
- `using Aspose.Pdf.Annotations;` (23/175 files)
- `using Aspose.Pdf.Forms;` (12/175 files)
- `using Aspose.Pdf.Optimization;` (7/175 files)
- `using Aspose.Pdf.Tagged;` (7/175 files)
- `using Aspose.Pdf.LogicalStructure;` (6/175 files)
- `using Aspose.Pdf.Devices;` (5/175 files)
- `using Aspose.Pdf.Drawing;` (3/175 files)
- `using Aspose.Pdf.Facades;` (2/175 files)
- `using Aspose.Pdf.Multithreading;` (1/175 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (1/175 files)
- `using System;` (120/175 files)
- `using System.IO;` (101/175 files)
- `using System.Collections.Generic;` (3/175 files)
- `using System.Data;` (2/175 files)
- `using System.Diagnostics;` (2/175 files)
- `using System.Drawing;` (2/175 files)
- `using System.Text;` (2/175 files)
- `using NUnit.Framework;` (1/175 files)
- `using System.Linq;` (1/175 files)
- `using System.Reflection;` (1/175 files)
- `using System.Threading;` (1/175 files)
- `using System.Threading.Tasks;` (1/175 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Tables) | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, enables auto‑tagging, sets language and title metadata, and creates tagged... |
| [add-background-color-to-pdf-page](./add-background-color-to-pdf-page.cs) | Add Background Color to PDF Page with Opacity | `Document`, `Page`, `Graph` | Demonstrates how to fill an entire PDF page with a semi‑transparent color by adding a rectangle s... |
| [add-background-template-to-pdf-pages](./add-background-template-to-pdf-pages.cs) | Add Background Template to All PDF Pages | `Document`, `Page`, `PdfPageStamp` | Shows how to load a PDF and a single‑page background template, then stamp each page of the target... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate over image resources in a PDF and insert a styled caption paragraph beneath ... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a checkbox form field, set its default state to checked, ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an existing PDF, create an ImageStamp for a logo, position it as a top‑center h... |
| [add-custom-document-info-and-xmp-metadata](./add-custom-document-info-and-xmp-metadata.cs) | Add Custom Document Info and XMP Metadata to PDF | `Document`, `DocumentInfo`, `Add` | Demonstrates how to add custom key/value pairs to a PDF's DocumentInfo dictionary and embed XMP m... |
| [add-custom-signature-appearance-last-page](./add-custom-signature-appearance-last-page.cs) | Add custom signature appearance last page |  | Add custom signature appearance last page |
| [add-custom-xml-xmp-metadata-to-pdf](./add-custom-xml-xmp-metadata-to-pdf.cs) | Add custom xml xmp metadata to pdf |  | Add custom xml xmp metadata to pdf |
| [add-diagonal-text-stamp-to-pdf](./add-diagonal-text-stamp-to-pdf.cs) | Add Diagonal Text Stamp Watermark to PDF | `Document`, `TextStamp`, `FontRepository` | Shows how to load a PDF with Aspose.Pdf, create a TextStamp with a custom message, configure its ... |
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add Digital Signature Field and Sign PDF with Self‑Signed Ce... | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to create a signature field in a PDF, configure a PKCS#1 signature using a self‑... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF | `Document`, `Page`, `Heading` | Shows how to load a PDF template, create a heading with dynamic date and user information, positi... |
| [add-heading-to-pdf-outline](./add-heading-to-pdf-outline.cs) | Add heading to pdf outline |  | Add heading to pdf outline |
| [add-hierarchical-numbered-headings](./add-hierarchical-numbered-headings.cs) | Add Hierarchical Numbered Headings to PDF | `Document`, `Page`, `Heading` | Shows how to apply decimal numbering to headings and create hierarchical numbering (chapters and ... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a clickable hyperlink annotation that opens an external website in a PDF docu... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indented Paragraph with Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | Shows how to load a PDF, create a TextParagraph with first‑line and subsequent line indentation, ... |
| [add-javascript-calculation-to-pdf-form-fields](./add-javascript-calculation-to-pdf-form-fields.cs) | Add javascript calculation to pdf form fields |  | Add javascript calculation to pdf form fields |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `Save`, `JavascriptAction` | Demonstrates embedding JavaScript in a PDF with Aspose.Pdf so an alert dialog appears when the do... |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Calculate Sum of Two PDF Form Fields with JavaScript | `Document`, `Page`, `NumberField` | Demonstrates creating a PDF with two numeric input fields and a read‑only sum field, then attachi... |
| [add-javascript-sum-numeric-pdf-form-fields](./add-javascript-sum-numeric-pdf-form-fields.cs) | Add JavaScript to Sum Numeric PDF Form Fields | `Document`, `Form`, `TextBoxField` | Demonstrates embedding Acrobat JavaScript in a PDF with Aspose.Pdf to calculate the total of two ... |
| [add-javascript-total-calculation-to-pdf-form](./add-javascript-total-calculation-to-pdf-form.cs) | Add javascript total calculation to pdf form |  | Add javascript total calculation to pdf form |
| [add-level-1-heading-to-pdf](./add-level-1-heading-to-pdf.cs) | Add Level 1 Heading to PDF using Tagged Content | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to create a PDF document, use the tagged content API to insert a Level 1 heading... |
| [add-line-annotation-with-custom-color-thickness](./add-line-annotation-with-custom-color-thickness.cs) | Add Line Annotation with Custom Color and Thickness to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a line annotation on a PDF page, set its color, and adjust its thickne... |
| [add-line-separator-annotation-to-pdf-page](./add-line-separator-annotation-to-pdf-page.cs) | Add Line Separator Annotation to PDF Page | `Document`, `Page`, `Point` | Demonstrates how to insert a horizontal line annotation as a visual separator on a specific PDF p... |
| [add-link-annotation-open-pdf-attachment](./add-link-annotation-open-pdf-attachment.cs) | Add Link Annotation to Open PDF Attachment | `Document`, `Page`, `Rectangle` | Shows how to create a link annotation on a PDF page that opens an external PDF attachment using A... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi‑Level Table of Contents to PDF | `Document`, `AutoTaggingSettings`, `ProcessParagraphs` | Demonstrates how to enable auto‑tagging, detect headings, create a TOC element, insert a dedicate... |
| [add-new-bookmark-to-pdf-outline](./add-new-bookmark-to-pdf-outline.cs) | Add New Bookmark to PDF Outline | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Demonstrates how to create an outline (bookmark) in an existing PDF, set its title, style, and de... |
| [add-page-count-footer-to-pdf](./add-page-count-footer-to-pdf.cs) | Add Page Count Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates how to add a custom footer that shows "Page X of Y" on each page of a PDF using Aspo... |
| [add-page-number-footer-to-pdf](./add-page-number-footer-to-pdf.cs) | Add page number footer to pdf |  | Add page number footer to pdf |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert sequential page numbers into the footer of each page in a PDF using As... |
| ... | | | *and 145 more files* |

## Category Statistics
- Total examples: 175

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

> **Document** in PDF using C# / .NET -- **116** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **document** category.
This folder contains standalone C# examples for document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (116/116 files) ← category-specific
- `using Aspose.Pdf.Text;` (36/116 files)
- `using Aspose.Pdf.Annotations;` (26/116 files)
- `using Aspose.Pdf.Forms;` (11/116 files)
- `using Aspose.Pdf.Tagged;` (8/116 files)
- `using Aspose.Pdf.LogicalStructure;` (6/116 files)
- `using Aspose.Pdf.Devices;` (5/116 files)
- `using Aspose.Pdf.Drawing;` (4/116 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (4/116 files)
- `using Aspose.Pdf.Security;` (1/116 files)
- `using System;` (116/116 files)
- `using System.IO;` (109/116 files)
- `using System.Collections.Generic;` (5/116 files)
- `using System.Text;` (4/116 files)
- `using System.Drawing;` (3/116 files)
- `using System.Reflection;` (3/116 files)
- `using System.Linq;` (2/116 files)
- `using System.Drawing.Printing;` (1/116 files)
- `using System.Security.Cryptography.X509Certificates;` (1/116 files)
- `using System.Threading;` (1/116 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Table) | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to create a tagged PDF with language, title, heading, paragraph, and table eleme... |
| [add-background-color-to-pdf-page](./add-background-color-to-pdf-page.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to add a semi‑transparent colored rectangle as a background to the first page of... |
| [add-background-template-to-pdf-pages](./add-background-template-to-pdf-pages.cs) | Add Background Image to All PDF Pages | `Document`, `ImageStamp`, `Page` | Shows how to apply a reusable background image to every page of a PDF using Aspose.Pdf's ImageSta... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate over image resources in a PDF and insert a styled caption text fragment bene... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a checkbox form field, set its default state to checked, ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an existing PDF with Aspose.Pdf, create an ImageStamp for a logo, place it as a... |
| [add-custom-document-info-and-xmp-metadata](./add-custom-document-info-and-xmp-metadata.cs) | Add Custom XML Metadata to PDF Document | `Document`, `DocumentInfo`, `Title` | Shows how to update a PDF's document information and store custom XML metadata using Aspose.Pdf's... |
| [add-diagonal-text-stamp-to-pdf](./add-diagonal-text-stamp-to-pdf.cs) | Add Line‑like Text Stamp to PDF Pages | `Document`, `TextStamp`, `AddStamp` | Demonstrates how to add a centered, semi‑transparent text stamp that acts like a line annotation ... |
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add Digital Signature to PDF Using a Self‑Signed Certificate | `Document`, `SignatureField`, `Rectangle` | Demonstrates how to insert a visible signature field into a PDF, load a self‑signed PFX certifica... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF Using Tagged Content | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to create a dynamic heading (e.g., user name and date) in a PDF by using Aspose.... |
| [add-hierarchical-numbered-headings](./add-hierarchical-numbered-headings.cs) | Add Decimal Numbering to PDF Headings | `Document`, `ITaggedContent`, `Heading` | Demonstrates enabling PDF tagging, creating hierarchical Heading elements with automatic decimal ... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a clickable hyperlink annotation that opens an external website in a P... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Paragraph Indent and Line Spacing to PDF | `Document`, `TextFragment`, `TextState` | The example loads an existing PDF, creates a TextFragment, sets line spacing and simulates a firs... |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed JavaScript in a PDF using Aspose.Pdf so that an alert dialog appears wh... |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Add JavaScript Sum Calculation to PDF Form | `Document`, `Page`, `TextBoxField` | Demonstrates creating a PDF with two input fields and a read‑only result field, then attaching a ... |
| [add-javascript-sum-numeric-pdf-form-fields](./add-javascript-sum-numeric-pdf-form-fields.cs) | Add JavaScript to Sum PDF Form Fields | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates embedding JavaScript in a PDF with Aspose.Pdf to sum numeric form fields and display... |
| [add-level-1-heading-to-pdf](./add-level-1-heading-to-pdf.cs) | Create PDF with Level 1 Heading using Tagged Content | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to create a new PDF document, add a level 1 heading via the tagged content API, ... |
| [add-line-annotation-with-custom-color-thickness](./add-line-annotation-with-custom-color-thickness.cs) | Add Colored Line Annotation to PDF | `Document`, `Page`, `Graph` | Shows how to load a PDF, create a line shape with a custom color and thickness, add it to a page,... |
| [add-line-separator-annotation-to-pdf-page](./add-line-separator-annotation-to-pdf-page.cs) | Add Horizontal Line Separator to PDF Pages | `Document`, `Page`, `Graph` | Shows how to open a PDF with Aspose.Pdf, loop through each page, and draw a horizontal line as a ... |
| [add-link-annotation-open-pdf-attachment](./add-link-annotation-open-pdf-attachment.cs) | Create Link Annotation that Opens an Embedded PDF | `Document`, `FileSpecification`, `LinkAnnotation` | Loads a PDF, embeds another PDF as an attachment, and adds a link annotation that launches the at... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi-Level Table of Contents to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a Table of Contents page into an existing PDF by detecting heading styles bas... |
| [add-new-bookmark-to-pdf-outline](./add-new-bookmark-to-pdf-outline.cs) | Add New Bookmark to PDF Outline | `Document`, `Page`, `GoToAction` | Demonstrates how to insert a heading as a bookmark into an existing PDF's outline and link it to ... |
| [add-page-count-footer-to-pdf](./add-page-count-footer-to-pdf.cs) | Add Page Number Footer to PDF | `Document`, `Page`, `TextFragment` | Shows how to insert a custom footer that displays "Page X of Y" on every page of a PDF using Aspo... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `TextStamp` | Shows how to insert dynamic page numbers into the footer of each page in a PDF using Aspose.Pdf's... |
| [add-page-with-text-to-pdf](./add-page-with-text-to-pdf.cs) | Add a New Page with Text to a PDF (No Progress Event) | `Document`, `Page`, `TextFragment` | Demonstrates loading an existing PDF, adding a new page containing a text fragment, and saving th... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a TextAnnotation (pop‑up note) on a PDF page with Aspose.Pdf and save the upd... |
| [add-signature-field-to-pdf](./add-signature-field-to-pdf.cs) | Add User Signature Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a signature form field into a PDF document and set its visual appearance to m... |
| [add-signature-image-stamp-to-last-page](./add-signature-image-stamp-to-last-page.cs) | Add Signature Image to Last Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF with Aspose.Pdf, create an ImageStamp from a signature PNG, position it o... |
| [add-styled-table-alternating-row-colors](./add-styled-table-alternating-row-colors.cs) | Add Styled Table with Alternating Row Colors to PDF | `Document`, `Page`, `Table` | Shows how to load an existing PDF using Aspose.Pdf, create a table with borders and a header, app... |
| [add-watermark-layer-merge-layers](./add-watermark-layer-merge-layers.cs) | Add Text Watermark to PDF Pages | `Document`, `Page`, `TextStamp` | Loads an existing PDF, creates a semi‑transparent diagonal TextStamp watermark, applies it to eve... |
| ... | | | *and 86 more files* |

## Category Statistics
- Total examples: 116

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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

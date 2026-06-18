---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **document** category.
This folder contains standalone C# examples for document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (121/121 files) ← category-specific
- `using Aspose.Pdf.Text;` (30/121 files)
- `using Aspose.Pdf.Annotations;` (20/121 files)
- `using Aspose.Pdf.Forms;` (13/121 files)
- `using Aspose.Pdf.LogicalStructure;` (7/121 files)
- `using Aspose.Pdf.Tagged;` (7/121 files)
- `using Aspose.Pdf.Devices;` (6/121 files)
- `using Aspose.Pdf.Drawing;` (6/121 files)
- `using Aspose.Pdf.Optimization;` (4/121 files)
- `using Aspose.Pdf.Facades;` (3/121 files)
- `using Aspose.Pdf.Sanitization;` (3/121 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (2/121 files)
- `using Aspose.Pdf.Multithreading;` (1/121 files)
- `using Aspose.Pdf.Signatures;` (1/121 files)
- `using System;` (121/121 files)
- `using System.IO;` (103/121 files)
- `using System.Runtime.InteropServices;` (8/121 files)
- `using System.Collections.Generic;` (4/121 files)
- `using System.Text;` (2/121 files)
- `using System.Threading;` (2/121 files)
- `using NUnit.Framework;` (1/121 files)
- `using System.Data;` (1/121 files)
- `using System.Drawing.Imaging;` (1/121 files)
- `using System.Linq;` (1/121 files)
- `using System.Threading.Tasks;` (1/121 files)
- `using System.Xml;` (1/121 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Table) | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates enabling auto‑tagging and programmatically inserting heading, paragraph, and table e... |
| [add-automatic-page-numbers-to-pdf](./add-automatic-page-numbers-to-pdf.cs) | Add Automatic Page Numbers to PDF | `Document`, `PageNumberStamp`, `FindFont` | Demonstrates how to add a page‑number stamp to each page of a PDF, insert a new page, and update ... |
| [add-background-image-to-all-pdf-pages](./add-background-image-to-all-pdf-pages.cs) | Add Background Image to All PDF Pages Using a Template | `Document`, `Page`, `PdfPageStamp` | Shows how to apply a background page from a template PDF to every page of another PDF using Aspos... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate through PDF pages and images and insert a styled text paragraph as a caption... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a checkbox form field, set its default state to checked, ... |
| [add-custom-documentinfo-xmp-metadata](./add-custom-documentinfo-xmp-metadata.cs) | Add Custom DocumentInfo and XMP Metadata to PDF | `Document`, `DocumentInfo`, `SetXmpMetadata` | Demonstrates how to add custom key/value pairs to a PDF's DocumentInfo dictionary and embed custo... |
| [add-custom-image-signature-last-page](./add-custom-image-signature-last-page.cs) | Add Custom Image Signature to Last Page of PDF | `Document`, `Page`, `SignatureField` | Shows how to create a signature field on the last page of a PDF and overlay it with a custom sign... |
| [add-diagonal-text-stamp-to-pdf-pages](./add-diagonal-text-stamp-to-pdf-pages.cs) | Add Diagonal Text Stamp to PDF Pages | `Document`, `AddStamp`, `TextStamp` | Demonstrates how to load a PDF, create a semi‑transparent red text stamp, rotate it diagonally, a... |
| [add-digital-signature-to-pdf](./add-digital-signature-to-pdf.cs) | Add Digital Signature Field and Sign PDF with a Self‑Signed ... | `Document`, `Rectangle`, `SignatureField` | Shows how to create a signature field on a PDF page and sign the document using a self‑signed PKC... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF with Aspose.Pdf | `Document`, `SetTitle`, `Heading` | Shows how to create a heading with dynamic content (user name and date), position and style it, s... |
| [add-header-logo-to-pdf-pages](./add-header-logo-to-pdf-pages.cs) | Add Header Logo to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to insert a company logo as a header on every page of an existing PDF using Aspose.Pdf'... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a clickable link annotation that opens an external website within a PDF using... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indented Paragraph with Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | Loads an existing PDF, creates a TextParagraph with first‑line and subsequent‑line indentation, s... |
| [add-javascript-calculation-to-pdf-form-fields](./add-javascript-calculation-to-pdf-form-fields.cs) | Add JavaScript Calculation to PDF Form Fields | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF with two numeric input fields and a read‑only sum field, then attachi... |
| [add-javascript-total-calculation-to-pdf-form](./add-javascript-total-calculation-to-pdf-form.cs) | Add JavaScript Total Calculation to PDF Form | `Document`, `NumberField`, `Border` | Shows how to create a read‑only numeric field that automatically sums other numeric fields by att... |
| [add-level-1-heading-to-pdf](./add-level-1-heading-to-pdf.cs) | Add Level 1 Heading to PDF | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates creating a new PDF document, using the tagged‑content API to insert a level‑1 headin... |
| [add-line-annotation-to-pdf](./add-line-annotation-to-pdf.cs) | Add Line Annotation with Color and Thickness to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a line annotation, set its color and border width, and save the m... |
| [add-line-separator-annotation-to-pdf-page](./add-line-separator-annotation-to-pdf-page.cs) | Add Line Separator Annotation to PDF Page | `Document`, `Page`, `Point` | Demonstrates how to insert a horizontal line annotation as a visual separator on a PDF page using... |
| [add-link-annotation-launch-pdf-attachment](./add-link-annotation-launch-pdf-attachment.cs) | Create Link Annotation that Launches a PDF Attachment | `Document`, `Page`, `FileSpecification` | Loads an existing PDF, embeds another PDF as a file attachment annotation, and adds a link annota... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi-Level Table of Contents to PDF | `Document`, `AutoTaggingSettings`, `ProcessParagraphs` | Shows how to generate a hierarchical Table of Contents in an existing PDF by enabling auto‑taggin... |
| [add-open-action-javascript-alert-to-pdf](./add-open-action-javascript-alert-to-pdf.cs) | Add Open-Action JavaScript Alert to PDF | `Document`, `JavascriptAction`, `OpenAction` | Demonstrates how to embed JavaScript in a PDF using Aspose.Pdf so that an alert dialog appears wh... |
| [add-outline-heading-to-pdf](./add-outline-heading-to-pdf.cs) | Add Outline Heading to PDF Document | `Document`, `OutlineCollection`, `OutlineItemCollection` | Demonstrates how to insert a new heading into an existing PDF's outline (bookmarks) and link it t... |
| [add-page-count-footer-to-pdf](./add-page-count-footer-to-pdf.cs) | Add Page Count Footer to PDF Pages | `Document`, `Page`, `TextFragment` | Shows how to insert a centered footer on each PDF page that displays the current page number and ... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Shows how to insert dynamic page numbers into the footer of each page in a PDF using Aspose.Pdf's... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a TextAnnotation (sticky note) with a linked PopupAnnotation that show... |
| [add-semi-transparent-background-to-pdf-pages](./add-semi-transparent-background-to-pdf-pages.cs) | Add Semi-Transparent Background Color to PDF Pages | `Document`, `Page`, `Graph` | Shows how to overlay a semi‑transparent colored rectangle on each page of a PDF to create a backg... |
| [add-signature-field-with-styled-appearance](./add-signature-field-with-styled-appearance.cs) | Add Signature Field with Styled Appearance to PDF | `Document`, `SignatureField`, `DefaultAppearance` | Shows how to insert a signature form field into a PDF using Aspose.Pdf and configure its default ... |
| [add-styled-table-with-alternating-row-colors](./add-styled-table-with-alternating-row-colors.cs) | Add Styled Table with Alternating Row Colors to PDF | `Document`, `Page`, `Table` | Demonstrates how to insert a table with borders and alternating row background colors into an exi... |
| [add-visible-signature-and-lock-pdf](./add-visible-signature-and-lock-pdf.cs) | Add Visible Signature Field and Lock PDF | `Document`, `Rectangle`, `SignatureField` | Shows how to create a visible signature field in a PDF, sign it with a PKCS#7 certificate, and lo... |
| [apply-decimal-numbering-to-headings](./apply-decimal-numbering-to-headings.cs) | Apply Decimal Numbering to Hierarchical Headings | `Document`, `Page`, `Heading` | Demonstrates how to use Aspose.Pdf to add Heading objects with decimal (Arabic) numbering and hie... |
| ... | | | *and 91 more files* |

## Category Statistics
- Total examples: 121

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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

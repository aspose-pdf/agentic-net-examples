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

- `using Aspose.Pdf;` (118/118 files) ← category-specific
- `using Aspose.Pdf.Text;` (31/118 files)
- `using Aspose.Pdf.Annotations;` (19/118 files)
- `using Aspose.Pdf.Forms;` (12/118 files)
- `using Aspose.Pdf.Devices;` (5/118 files)
- `using Aspose.Pdf.LogicalStructure;` (4/118 files)
- `using Aspose.Pdf.Optimization;` (4/118 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (4/118 files)
- `using Aspose.Pdf.Tagged;` (4/118 files)
- `using Aspose.Pdf.Sanitization;` (3/118 files)
- `using Aspose.Pdf.Facades;` (2/118 files)
- `using Aspose.Pdf.Drawing;` (1/118 files)
- `using Aspose.Pdf.Multithreading;` (1/118 files)
- `using Aspose.Pdf.Operators;` (1/118 files)
- `using Aspose.Pdf.Security;` (1/118 files)
- `using System;` (118/118 files)
- `using System.IO;` (108/118 files)
- `using System.Runtime.InteropServices;` (13/118 files)
- `using System.Collections.Generic;` (3/118 files)
- `using System.Text;` (3/118 files)
- `using NUnit.Framework;` (1/118 files)
- `using System.Data;` (1/118 files)
- `using System.Diagnostics;` (1/118 files)
- `using System.Linq;` (1/118 files)
- `using System.Threading;` (1/118 files)
- `using System.Threading.Tasks;` (1/118 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Tables) | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to use Aspose.Pdf's tagged‑content API to add semantic accessibility tags such a... |
| [add-automatic-page-numbers-to-pdf](./add-automatic-page-numbers-to-pdf.cs) | Add Automatic Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to insert a page number stamp that automatically updates when pages are added or remove... |
| [add-background-image-to-all-pdf-pages](./add-background-image-to-all-pdf-pages.cs) | Add Background Image to All PDF Pages | `Document`, `Page`, `PdfPageStamp` | Shows how to use a template PDF page as a background stamp and apply it to every page of another ... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate over each image on every PDF page and insert a styled text caption beneath t... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a checkbox form field, set its default state to checked, ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Demonstrates loading an existing PDF, creating an ImageStamp from a logo image, positioning it as... |
| [add-custom-xml-metadata-to-pdf](./add-custom-xml-metadata-to-pdf.cs) | Add Custom XML Metadata to PDF Document | `Document`, `DocumentInfo`, `SetXmpMetadata` | Demonstrates how to add a custom XML entry to a PDF's DocumentInfo dictionary and embed optional ... |
| [add-digital-signature-field-to-pdf](./add-digital-signature-field-to-pdf.cs) | Add Digital Signature Field and Sign PDF with Self‑Signed Ce... | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to create a signature field on a PDF page, configure a PKCS#7 signature using a ... |
| [add-digital-signature-with-tsa-timestamp](./add-digital-signature-with-tsa-timestamp.cs) | Add Digital Signature with TSA Timestamp to PDF | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to create a PKCS#7 digital signature on a PDF using Aspose.Pdf and include a tru... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF | `Document`, `Heading`, `FontRepository` | Shows how to load an existing PDF, create a level‑1 heading with dynamic content (user name and d... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to insert a clickable hyperlink annotation that opens an external website in a PDF docu... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indented Paragraph with Custom Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | The example loads an existing PDF, creates a TextParagraph with first‑line and subsequent‑line in... |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed a JavaScript action in a PDF using Aspose.Pdf so that an alert dialog appears ... |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Add JavaScript Sum Calculation to PDF Form | `Document`, `Page`, `Rectangle` | Creates a PDF with two input text fields and a read‑only result field, then attaches a JavaScript... |
| [add-js-calc-total-field](./add-js-calc-total-field.cs) | Add JavaScript Calculation for Total PDF Form Field | `Document`, `TextBoxField`, `JavascriptAction` | Shows how to embed JavaScript in a PDF with Aspose.Pdf to automatically sum numeric form fields a... |
| [add-line-annotation-custom-color-thickness](./add-line-annotation-custom-color-thickness.cs) | Add Line Annotation with Custom Color and Thickness to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a line annotation, set its color and border width, and save the d... |
| [add-line-annotation-separator-to-pdf-page](./add-line-annotation-separator-to-pdf-page.cs) | Add Line Annotation Separator to PDF Page | `Document`, `Page`, `LineAnnotation` | Demonstrates how to insert a horizontal line annotation as a visual separator on a PDF page using... |
| [add-link-annotation-open-embedded-pdf](./add-link-annotation-open-embedded-pdf.cs) | Add Link Annotation to Open Embedded PDF Attachment | `Document`, `FileSpecification`, `Page` | Shows how to embed a PDF file into another PDF and create a link annotation that opens the embedd... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi-Level Table of Contents to PDF | `Document`, `Page`, `TocInfo` | Shows how to insert a TOC page, configure TocInfo, and add hierarchical Heading objects with page... |
| [add-new-bookmark-to-pdf-outline](./add-new-bookmark-to-pdf-outline.cs) | Add New Bookmark to PDF Outline | `Document`, `OutlineItemCollection`, `GoToAction` | Demonstrates how to create a new outline (bookmark) in an existing PDF, set its title and page de... |
| [add-page-count-footer-to-pdf](./add-page-count-footer-to-pdf.cs) | Add Page Count Footer to PDF Pages | `Document`, `HeaderFooter`, `TextFragment` | Demonstrates how to add a custom footer that displays the current page number and total page coun... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Shows how to insert dynamic page numbers into the footer of each page in a PDF using Aspose.Pdf's... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `TextAnnotation` | Shows how to create a sticky‑note (TextAnnotation) with a linked PopupAnnotation that displays de... |
| [add-readonly-text-field-with-default-value](./add-readonly-text-field-with-default-value.cs) | Add Read‑Only Text Field with Default Value to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, add a TextBoxField with a default value, and set the field to read‑onl... |
| [add-semi-transparent-background-to-pdf-page](./add-semi-transparent-background-to-pdf-page.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Shows how to fill an entire PDF page with a semi‑transparent colored rectangle by using Aspose.Pd... |
| [add-signature-image-to-last-page](./add-signature-image-to-last-page.cs) | Add Signature Image to Last Page of PDF | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF, create an ImageStamp from a signature PNG, position it on the document's... |
| [add-styled-table-to-pdf](./add-styled-table-to-pdf.cs) | Add Styled Table with Borders and Alternating Row Colors to ... | `Document`, `Table`, `BorderInfo` | Shows how to create a table with defined column widths, apply borders and alternating row backgro... |
| [add-text-stamp-to-pdf-pages](./add-text-stamp-to-pdf-pages.cs) | Add Text Stamp to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Shows how to load a PDF with Aspose.Pdf, create a TextStamp with a custom message, configure its ... |
| [add-user-signature-field-with-style](./add-user-signature-field-with-style.cs) | Add User Signature Field with Styled Appearance to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to add a signature form field to a PDF using Aspose.Pdf, set its default appeara... |
| [add-visible-signature-field-and-lock-pdf](./add-visible-signature-field-and-lock-pdf.cs) | Add Visible Signature Field and Lock PDF after Signing | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to create a visible signature field in a PDF, apply a PKCS#7 digital signature u... |
| ... | | | *and 88 more files* |

## Category Statistics
- Total examples: 118

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-05 | Run: `20260505_104420_c3b386`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf;` (120/122 files) ← category-specific
- `using Aspose.Pdf.Text;` (33/122 files)
- `using Aspose.Pdf.Annotations;` (22/122 files)
- `using Aspose.Pdf.Forms;` (13/122 files)
- `using Aspose.Pdf.Drawing;` (9/122 files)
- `using Aspose.Pdf.LogicalStructure;` (7/122 files)
- `using Aspose.Pdf.Tagged;` (7/122 files)
- `using Aspose.Pdf.Devices;` (6/122 files)
- `using Aspose.Pdf.Facades;` (3/122 files)
- `using Aspose.Pdf.Optimization;` (3/122 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (3/122 files)
- `using Aspose.Pdf.Multithreading;` (1/122 files)
- `using Aspose.Pdf.Sanitization;` (1/122 files)
- `using Aspose.Pdf.Security;` (1/122 files)
- `using System;` (122/122 files)
- `using System.IO;` (106/122 files)
- `using System.Collections.Generic;` (3/122 files)
- `using System.Drawing;` (2/122 files)
- `using System.Runtime.InteropServices;` (2/122 files)
- `using System.Text;` (2/122 files)
- `using NUnit.Framework;` (1/122 files)
- `using System.Data;` (1/122 files)
- `using System.Drawing.Imaging;` (1/122 files)
- `using System.IO.Compression;` (1/122 files)
- `using System.Linq;` (1/122 files)
- `using System.Threading;` (1/122 files)
- `using System.Threading.Tasks;` (1/122 files)

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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags (Heading, Paragraph, Table) to PDF | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates how to enable auto‑tagging, set document language and title, and add accessible head... |
| [add-automatic-page-numbers-to-pdf](./add-automatic-page-numbers-to-pdf.cs) | Add Automatic Page Numbers to PDF | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to insert a page number stamp that updates automatically across all pages, inclu... |
| [add-background-image-to-pdf-pages](./add-background-image-to-pdf-pages.cs) | Add Background Image to All PDF Pages Using a Template | `Document`, `Page`, `PdfPageStamp` | Demonstrates how to apply a single‑page PDF template as a background image to every page of anoth... |
| [add-captions-below-images-pdf](./add-captions-below-images-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | The example loads a PDF, iterates through each page and its images, creates a styled text paragra... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, add a checkbox form field, set its default state to checked, and save ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `ImageStamp`, `AddStamp` | Shows how to load an existing PDF, create an ImageStamp for a logo, place it as a top‑center head... |
| [add-confidential-text-stamp-to-pdf-pages](./add-confidential-text-stamp-to-pdf-pages.cs) | Add Confidential Text Stamp to All PDF Pages | `Document`, `Page`, `TextStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating a semi‑transparent text stamp, and applying ... |
| [add-custom-colored-line-annotation](./add-custom-colored-line-annotation.cs) | Add Custom Colored Line Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a line annotation with a custom blue color and thickness to a PDF page us... |
| [add-custom-xml-metadata-to-pdf](./add-custom-xml-metadata-to-pdf.cs) | Add Custom XML Metadata to PDF Document | `Document`, `DocumentInfo`, `SetXmpMetadata` | Demonstrates how to add custom key‑value pairs to a PDF's document info dictionary and embed XMP ... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF with Self‑Signed Ce... | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to create a signature field on a PDF page and apply a PKCS#7 digital signature u... |
| [add-digital-signature-with-timestamp](./add-digital-signature-with-timestamp.cs) | Add Digital Signature with Timestamp to PDF | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a signature field, and applies a PKCS#7 digital signature using a PFX certif... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF with Aspose.Pdf | `Document`, `Page`, `Heading` | Loads an existing PDF, creates a heading containing runtime data such as the current date and use... |
| [add-horizontal-separator-line-to-pdf-page](./add-horizontal-separator-line-to-pdf-page.cs) | Add Horizontal Separator Line to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a thin horizontal line annotation as a visual separator on a PDF page ... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Save`, `Page` | Demonstrates how to insert a clickable hyperlink annotation that opens an external website into a... |
| [add-indented-paragraph-with-line-spacing](./add-indented-paragraph-with-line-spacing.cs) | Add Indented Paragraph with Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | Loads an existing PDF, creates a TextParagraph with first‑line and subsequent‑line indentation, a... |
| [add-javascript-calculation-to-pdf-form-fields](./add-javascript-calculation-to-pdf-form-fields.cs) | Add JavaScript Calculation to PDF Form Fields | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF with numeric form fields and a button that runs JavaScript to sum the... |
| [add-javascript-open-action-to-pdf](./add-javascript-open-action-to-pdf.cs) | Add JavaScript Open Action to PDF | `Document`, `Save`, `JavascriptAction` | Shows how to embed a JavaScript alert that runs when the PDF is opened using Aspose.Pdf. |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Add JavaScript Sum Calculation to PDF Form | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to create or locate two text box fields in a PDF, add a read‑only result field, ... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi‑Level Table of Contents to PDF | `Document`, `AutoTaggingSettings`, `ProcessParagraphs` | Shows how to enable auto‑tagging on an existing PDF, detect heading hierarchy, insert a dedicated... |
| [add-new-outline-bookmark-to-pdf](./add-new-outline-bookmark-to-pdf.cs) | Add New Outline Bookmark to PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Shows how to create a top‑level outline (bookmark) in an existing PDF, link it to a specific page... |
| [add-page-number-footer-to-pdf](./add-page-number-footer-to-pdf.cs) | Add Page Number Footer to PDF | `Document`, `Page`, `HeaderFooter` | Shows how to add a centered footer displaying "Page X of Y" on each page of an existing PDF using... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Demonstrates how to add dynamic page number stamps to the footer of each page in a PDF using Aspo... |
| [add-pdf-attachment-link-annotation](./add-pdf-attachment-link-annotation.cs) | Add Link Annotation to Open PDF Attachment | `Document`, `Page`, `Rectangle` | Shows how to create a clickable link annotation in a PDF that opens an attached PDF file when the... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Popup Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a sticky‑note (TextAnnotation) with an associated PopupAnnotation to a PD... |
| [add-readonly-text-field-with-default-value](./add-readonly-text-field-with-default-value.cs) | Add Read‑Only Text Field with Default Value to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a text box field to an existing PDF, set a default value, and make it rea... |
| [add-semi-transparent-background-to-pdf-page](./add-semi-transparent-background-to-pdf-page.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to fill an entire PDF page with a semi-transparent colored rectangle using Aspos... |
| [add-signature-field-to-pdf](./add-signature-field-to-pdf.cs) | Add a Signature Field to a PDF Document | `Document`, `Rectangle`, `Color` | Demonstrates inserting a signature form field into an existing PDF, configuring its appearance an... |
| [add-signature-field-with-image-appearance](./add-signature-field-with-image-appearance.cs) | Add Signature Field with Image Appearance to Last PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to add a signature field to the last page of a PDF and display a custom signatur... |
| [add-styled-table-with-borders-and-alternating-row-...](./add-styled-table-with-borders-and-alternating-row-colors.cs) | Add Styled Table with Borders and Alternating Row Colors to ... | `Document`, `Page`, `Table` | Demonstrates how to create a PDF table with a full border, cell borders, padding, and alternating... |
| [add-visible-signature-field-and-lock-pdf](./add-visible-signature-field-and-lock-pdf.cs) | Add Visible Signature Field and Lock PDF After Signing | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to create a visible signature field, apply a PKCS#7 digital signature, and make ... |
| ... | | | *and 92 more files* |

## Category Statistics
- Total examples: 122

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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

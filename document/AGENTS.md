---
name: document
description: C# examples for document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - document

> **Document** in PDF using C# / .NET -- **122** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **document** category.
This folder contains standalone C# examples for document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (122/122 files) ← category-specific
- `using Aspose.Pdf.Text;` (32/122 files)
- `using Aspose.Pdf.Annotations;` (20/122 files)
- `using Aspose.Pdf.Forms;` (12/122 files)
- `using Aspose.Pdf.Drawing;` (8/122 files)
- `using Aspose.Pdf.Optimization;` (7/122 files)
- `using Aspose.Pdf.Devices;` (6/122 files)
- `using Aspose.Pdf.LogicalStructure;` (6/122 files)
- `using Aspose.Pdf.Tagged;` (6/122 files)
- `using Aspose.Pdf.Facades;` (3/122 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (2/122 files)
- `using Aspose.Pdf.Multithreading;` (1/122 files)
- `using Aspose.Pdf.Printing;` (1/122 files)
- `using Aspose.Pdf.Sanitization;` (1/122 files)
- `using Aspose.Pdf.Security;` (1/122 files)
- `using System;` (122/122 files)
- `using System.IO;` (111/122 files)
- `using System.Collections.Generic;` (4/122 files)
- `using System.Data;` (2/122 files)
- `using System.Drawing;` (2/122 files)
- `using System.Text;` (2/122 files)
- `using System.Drawing.Imaging;` (1/122 files)
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
| [add-accessibility-tags-to-pdf](./add-accessibility-tags-to-pdf.cs) | Add Accessibility Tags to PDF (Headings, Paragraphs, Tables) | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates enabling auto‑tagging, setting document language and title, and inserting heading, p... |
| [add-automatic-page-numbers-to-pdf](./add-automatic-page-numbers-to-pdf.cs) | Add Automatic Page Numbers to PDF | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to stamp page numbers on all pages of a PDF, insert a new page, and refresh pagi... |
| [add-background-color-to-pdf-page](./add-background-color-to-pdf-page.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to fill an entire PDF page with a semi‑transparent color by adding a rectangle s... |
| [add-background-template-to-all-pdf-pages](./add-background-template-to-all-pdf-pages.cs) | Add Background Template to All PDF Pages | `Document`, `PdfPageStamp`, `AddStamp` | Shows how to load a PDF and a background template, create a PdfPageStamp, and apply it as a backg... |
| [add-captions-below-images-in-pdf](./add-captions-below-images-in-pdf.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Shows how to iterate through images on each PDF page and insert a styled caption paragraph beneat... |
| [add-checked-checkbox-form-field](./add-checked-checkbox-form-field.cs) | Add Checked Checkbox Form Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF document, add a checkbox form field, set its default state to checked, ... |
| [add-company-logo-header-to-pdf-pages](./add-company-logo-header-to-pdf-pages.cs) | Add Company Logo Header to PDF Pages | `Document`, `Page`, `ImageStamp` | Shows how to load an existing PDF, create an ImageStamp with a company logo, place it as a header... |
| [add-custom-xml-metadata-to-pdf](./add-custom-xml-metadata-to-pdf.cs) | Add Custom XML Metadata to PDF Document | `Document`, `Info`, `DocumentInfo` | Demonstrates how to add custom key‑value pairs, such as XML fragments, to a PDF's document inform... |
| [add-digital-signature-field-and-sign-pdf](./add-digital-signature-field-and-sign-pdf.cs) | Add Digital Signature Field and Sign PDF with Self‑Signed Ce... | `Document`, `SignatureField`, `PKCS1` | Demonstrates how to create a signature field in a PDF, configure a PKCS#1 signature using a self‑... |
| [add-digital-signature-with-timestamp](./add-digital-signature-with-timestamp.cs) | Add Digital Signature with Trusted Timestamp to PDF | `Document`, `SignatureField`, `PKCS7` | Demonstrates how to create a signature field, configure a PKCS#7 signature with a trusted Time‑St... |
| [add-dynamic-heading-to-pdf](./add-dynamic-heading-to-pdf.cs) | Add Dynamic Heading to PDF | `Document`, `Heading`, `Position` | Demonstrates loading a PDF template, creating a level‑1 heading with dynamic user name and date, ... |
| [add-header-current-date-javascript](./add-header-current-date-javascript.cs) | Add Header with Current Date via JavaScript | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF with a header that displays the current date by using a DateFiel... |
| [add-hyperlink-annotation-to-pdf](./add-hyperlink-annotation-to-pdf.cs) | Add Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a clickable link annotation that opens an external website in a PDF us... |
| [add-javascript-calculation-to-pdf-form](./add-javascript-calculation-to-pdf-form.cs) | Calculate Total of Numeric Fields in PDF Form | `Document`, `Page`, `Rectangle` | Demonstrates creating numeric form fields and a read‑only total field in a PDF with Aspose.Pdf, t... |
| [add-javascript-sum-calculation-to-pdf-form](./add-javascript-sum-calculation-to-pdf-form.cs) | Add JavaScript Sum Calculation to PDF Form | `Document`, `Page`, `NumberField` | Demonstrates creating a PDF with two numeric fields and a read‑only result field, then attaching ... |
| [add-line-annotation-with-color-and-thickness](./add-line-annotation-with-color-and-thickness.cs) | Add Line Annotation with Color and Thickness to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a line annotation on a PDF page, set its color, and adjust its thickne... |
| [add-link-annotation-open-embedded-pdf](./add-link-annotation-open-embedded-pdf.cs) | Add Link Annotation to Open Embedded PDF | `Document`, `FileSpecification`, `LinkAnnotation` | Shows how to embed a PDF file into another PDF and create a clickable link annotation that opens ... |
| [add-multi-level-toc-to-pdf](./add-multi-level-toc-to-pdf.cs) | Add Multi-Level Table of Contents to PDF | `Document`, `AutoTaggingSettings`, `ProcessParagraphs` | Demonstrates how to enable auto‑tagging, generate a logical structure, create a TOC element, inse... |
| [add-open-action-javascript-to-pdf](./add-open-action-javascript-to-pdf.cs) | Add Open-Action JavaScript to PDF | `Document`, `JavascriptAction`, `OpenAction` | Shows how to embed JavaScript in a PDF using Aspose.Pdf so that an alert dialog appears when the ... |
| [add-outline-item-to-pdf](./add-outline-item-to-pdf.cs) | Add Outline Item (Bookmark) to PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Demonstrates how to create a new outline (bookmark) in an existing PDF, set its destination to a ... |
| [add-page-count-footer-to-pdf-pages](./add-page-count-footer-to-pdf-pages.cs) | Add Page Count Footer to PDF Pages | `Document`, `PageNumberStamp`, `AddStamp` | Shows how to add a custom footer that displays the current page number and total page count on ea... |
| [add-page-numbers-to-pdf-footer](./add-page-numbers-to-pdf-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `Page`, `PageNumberStamp` | Shows how to insert dynamic page numbers into the footer of each page in a PDF using Aspose.Pdf's... |
| [add-paragraph-indentation-line-spacing](./add-paragraph-indentation-line-spacing.cs) | Add Paragraph with Indentation and Line Spacing to PDF | `Document`, `Page`, `TextParagraph` | Shows how to load or create a PDF, define a text rectangle, set first‑line and subsequent line in... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a TextAnnotation (sticky‑note) with an associated PopupAnnotation to a PD... |
| [add-read-only-text-field-to-pdf](./add-read-only-text-field-to-pdf.cs) | Add Read‑Only Text Field to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a TextBoxField in a PDF, assign a default value, set it to read‑only, and sav... |
| [add-separator-line-annotation](./add-separator-line-annotation.cs) | Add Separator Line Annotation to PDF Page | `Document`, `Page`, `Point` | Shows how to load a PDF with Aspose.Pdf, create a line annotation as a visual separator, add it t... |
| [add-signature-field-with-styled-appearance](./add-signature-field-with-styled-appearance.cs) | Add Signature Field with Styled Appearance to PDF | `Document`, `Rectangle`, `SignatureField` | Demonstrates how to insert a signature form field into a PDF and set its default appearance (font... |
| [add-signature-image-to-last-page](./add-signature-image-to-last-page.cs) | Add Signature Image to Last Page of PDF | `Document`, `Page`, `ImageStamp` | Demonstrates loading a PDF with Aspose.Pdf, creating an ImageStamp from a custom signature image,... |
| [add-structured-headings-and-toc](./add-structured-headings-and-toc.cs) | Add Structured Headings and Table of Contents to PDF | `Document`, `Heading`, `ITaggedContent` | Shows how to insert Heading elements with different numbering styles, enable auto‑tagging, create... |
| [add-styled-table-with-borders-and-alternating-row-...](./add-styled-table-with-borders-and-alternating-row-colors.cs) | Add Styled Table with Borders and Alternating Row Colors to ... | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a table with a full border, set column widths, style th... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

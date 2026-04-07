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

- `using Aspose.Pdf;` (111/111 files) ← category-specific
- `using Aspose.Pdf.Text;` (29/111 files)
- `using Aspose.Pdf.Annotations;` (21/111 files)
- `using Aspose.Pdf.Forms;` (12/111 files)
- `using Aspose.Pdf.Facades;` (7/111 files)
- `using Aspose.Pdf.Devices;` (6/111 files)
- `using Aspose.Pdf.Drawing;` (4/111 files)
- `using Aspose.Pdf.LogicalStructure;` (4/111 files)
- `using Aspose.Pdf.Tagged;` (4/111 files)
- `using Aspose.Pdf.Optimization;` (3/111 files)
- `using Aspose.Pdf.Security.HiddenDataSanitization;` (3/111 files)
- `using Aspose.Pdf.Multithreading;` (1/111 files)
- `using Aspose.Pdf.Security;` (1/111 files)
- `using System;` (111/111 files)
- `using System.IO;` (97/111 files)
- `using System.Runtime.InteropServices;` (18/111 files)
- `using System.Collections.Generic;` (3/111 files)
- `using System.Data;` (2/111 files)
- `using NUnit.Framework;` (1/111 files)
- `using System.Drawing;` (1/111 files)
- `using System.Text;` (1/111 files)
- `using System.Threading;` (1/111 files)
- `using System.Threading.Tasks;` (1/111 files)
- `using System.Xml;` (1/111 files)

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
| [add-accessibility-tags](./add-accessibility-tags.cs) | Add Accessibility Tags to Headings, Paragraphs, and Tables | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates how to tag headings, paragraphs, and a table in an existing PDF to improve screen‑re... |
| [add-background-image-template](./add-background-image-template.cs) | Add Background Image to Every PDF Page Using a Reusable Temp... | `Document`, `Page`, `Image` | Demonstrates how to create a single‑page template containing a background image and stamp it onto... |
| [add-captions-below-images](./add-captions-below-images.cs) | Add Captions Below Images in PDF | `Document`, `Page`, `XImage` | Demonstrates how to iterate over images on each page of a PDF and add a styled text caption below... |
| [add-checkbox-checked](./add-checkbox-checked.cs) | Add Checkbox Form Field with Default Checked State | `Document`, `Page`, `CheckboxField` | Creates a PDF with a checkbox form field and sets it to be checked by default. |
| [add-company-logo-header](./add-company-logo-header.cs) | Add Company Logo Header to PDF Pages | `Document`, `AddStamp`, `ImageStamp` | Shows how to place a company logo as a header on every page of an existing PDF using Aspose.Pdf f... |
| [add-custom-xml-metadata](./add-custom-xml-metadata.cs) | Add Custom XML Metadata to PDF Document | `Document`, `Add`, `SetXmpMetadata` | Demonstrates how to add custom XML metadata and a custom dictionary entry to a PDF's document inf... |
| [add-digital-signature-timestamp](./add-digital-signature-timestamp.cs) | Add Digital Signature with Timestamp to PDF | `Document`, `SignatureField`, `Signature` | Demonstrates how to add a digital signature to a PDF and attach a trusted timestamp from a TSA us... |
| [add-digital-signature](./add-digital-signature.cs) | Add Digital Signature Field and Sign PDF | `Document`, `SignatureField`, `Signature` | Demonstrates how to add a signature form field to a PDF and sign it using a self‑signed certificate. |
| [add-external-hyperlink-annotation](./add-external-hyperlink-annotation.cs) | Add External Hyperlink Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a link annotation that opens an external website when clicked. |
| [add-footer-total-page-count](./add-footer-total-page-count.cs) | Add Footer with Total Page Count to PDF | `Document`, `Page`, `HeaderFooter` | Demonstrates adding a custom footer that displays the current page number and total page count on... |
| [add-javascript-calculation-form](./add-javascript-calculation-form.cs) | Add JavaScript Calculation to PDF Form Fields | `Document`, `TextBoxField`, `JavascriptAction` | Demonstrates adding two text box fields and a calculated field that shows their sum using a JavaS... |
| [add-line-annotation](./add-line-annotation.cs) | Add Line Annotation with Custom Color and Thickness | `Document`, `Page`, `LineAnnotation` | Demonstrates how to add a line annotation to a PDF page, customize its color and thickness, and s... |
| [add-line-annotation__v2](./add-line-annotation__v2.cs) | Add Line Annotation as Section Separator | `Document`, `LineAnnotation`, `Annotations` | Demonstrates adding a horizontal line annotation to a PDF page to act as a visual separator betwe... |
| [add-line-stamp-annotation](./add-line-stamp-annotation.cs) | Add Line Stamp Annotation to PDF Page | `Document`, `Page`, `StampAnnotation` | Demonstrates how to add a rubber stamp annotation with custom text across a PDF page. |
| [add-link-annotation-open-attachment](./add-link-annotation-open-attachment.cs) | Add Link Annotation to Open PDF Attachment | `Document`, `FileSpecification`, `LinkAnnotation` | Demonstrates how to embed a PDF file as an attachment and create a link annotation that opens the... |
| [add-multi-level-toc](./add-multi-level-toc.cs) | Add Multi‑Level Table of Contents to PDF | `Document`, `Page`, `LinkAnnotation` | Creates a Table of Contents page at the beginning of an existing PDF, adding hierarchical entries... |
| [add-page-numbers-footer](./add-page-numbers-footer.cs) | Add Page Numbers to PDF Footer | `Document`, `PageNumberStamp`, `AddStamp` | Demonstrates how to add dynamic page numbers to the footer of each page in a PDF using Aspose.Pdf... |
| [add-popup-note-annotation](./add-popup-note-annotation.cs) | Add Pop‑up Note Annotation to PDF | `Document`, `Page`, `TextAnnotation` | Demonstrates how to add a sticky‑note annotation with an associated pop‑up window that displays a... |
| [add-readonly-text-field](./add-readonly-text-field.cs) | Add Read‑Only Text Field with Default Value to PDF | `Document`, `Page`, `Rectangle` | Creates a PDF, adds a text box form field with a default value and sets it to read‑only, then sav... |
| [add-semi-transparent-background](./add-semi-transparent-background.cs) | Add Semi-Transparent Background Color to PDF Page | `Document`, `Page`, `Graph` | Demonstrates how to add a semi‑transparent colored rectangle as a background to a PDF page using ... |
| [add-signature-appearance](./add-signature-appearance.cs) | Add Signature Appearance with Custom Image on Last Page | `Document`, `SignatureField`, `ImageStamp` | Demonstrates adding a signature field and placing a custom image as its appearance on the last pa... |
| [add-signature-field](./add-signature-field.cs) | Add Signature Form Field with Custom Appearance | `Document`, `SignatureField`, `AddFieldAppearance` | Demonstrates adding a signature form field to a PDF and customizing its appearance to match the d... |
| [add-styled-table](./add-styled-table.cs) | Add Styled Table with Alternating Row Colors to PDF | `Document`, `Page`, `Table` | Creates a PDF, adds a table with borders and alternating row background colors, and saves the doc... |
| [add-visible-signature-lock-pdf](./add-visible-signature-lock-pdf.cs) | Add Visible Signature Field and Lock PDF | `Document`, `SignatureField`, `Signature` | Demonstrates how to add a visible signature field to a PDF, sign it with a certificate, and lock ... |
| [apply-decimal-numbering-headings](./apply-decimal-numbering-headings.cs) | Apply Decimal Numbering to Hierarchical Headings in PDF | `Document`, `Page`, `Heading` | Demonstrates how to create headings with automatic decimal numbering and hierarchical levels usin... |
| [apply-password-protection](./apply-password-protection.cs) | Apply Password Protection to PDF | `Document`, `Encrypt`, `Permissions` | Demonstrates how to encrypt a PDF with user and owner passwords and set specific permissions usin... |
| [attach-pdf-portfolio](./attach-pdf-portfolio.cs) | Attach PDF Portfolio File with Description | `Document`, `Portfolio`, `AddFile` | Demonstrates how to attach a PDF file as a portfolio entry to another PDF and set a custom descri... |
| [batch-convert-pdf-to-images](./batch-convert-pdf-to-images.cs) | Batch Convert PDF Pages to Images Using HiddenDataSanitizer | `Document`, `SanitizeAllToImages` | Demonstrates how to use Aspose.Pdf's HiddenDataSanitizer to convert each page of a PDF into separ... |
| [batch-sanitize-pdfs](./batch-sanitize-pdfs.cs) | Batch sanitize PDFs and save cleaned copies | `Document`, `RemoveMetadata`, `Flatten` | Iterates over a folder of PDF files, removes metadata, flattens forms, strips PDF/A and PDF/UA co... |
| [check-hidden-annotations](./check-hidden-annotations.cs) | Check for Hidden Annotations in PDF | `Document`, `Page`, `AnnotationCollection` | Demonstrates how to detect hidden annotations in a PDF file using Aspose.Pdf for .NET. |
| ... | | | *and 81 more files* |

## Category Statistics
- Total examples: 111

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

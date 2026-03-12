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

- `using Aspose.Pdf;` (70/72 files) ← category-specific
- `using Aspose.Pdf.Text;` (14/72 files)
- `using Aspose.Pdf.Optimization;` (5/72 files)
- `using Aspose.Pdf.Facades;` (4/72 files)
- `using Aspose.Pdf.LogicalStructure;` (4/72 files)
- `using Aspose.Pdf.Tagged;` (4/72 files)
- `using Aspose.Pdf.Annotations;` (2/72 files)
- `using Aspose.Pdf.Drawing;` (2/72 files)
- `using Aspose.Pdf.Comparison;` (1/72 files)
- `using Aspose.Pdf.Operators;` (1/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (70/72 files)
- `using System.Drawing;` (2/72 files)
- `using System.Collections.Generic;` (1/72 files)

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

| File | Key APIs | Description |
|------|----------|-------------|
| [add-a-custom-text-watermark-to-an-existing-PDF-document-inpu...](./add-a-custom-text-watermark-to-an-existing-PDF-document-input-format-PDF.cs) | `TextStamp` | Add a custom text watermark to an existing PDF document input format PDF |
| [add-a-heading-with-specific-level-to-a-PDF-page-and-generate...](./add-a-heading-with-specific-level-to-a-PDF-page-and-generate-a-table-of-contents-output-format-PDF.cs) | `TextFragment`, `TocInfo` | Add a heading with specific level to a PDF page and generate a table of conte... |
| [add-content-to-a-PDF-layer-input-format-PDF](./add-content-to-a-PDF-layer-input-format-PDF.cs) | `TextFragment`, `TextBuilder` | Add content to a PDF layer input format PDF |
| [add-headings-to-a-PDF-input-format-PDF](./add-headings-to-a-PDF-input-format-PDF.cs) |  | Add headings to a PDF input format PDF |
| [add-images-to-a-PDF-document-input-format-PDF](./add-images-to-a-PDF-document-input-format-PDF.cs) |  | Add images to a PDF document input format PDF |
| [add-pages-from-one-PDF-to-another-input-format-PDF](./add-pages-from-one-PDF-to-another-input-format-PDF.cs) |  | Add pages from one PDF to another input format PDF |
| [add-pages-to-a-PDF-document-input-format-PDF](./add-pages-to-a-PDF-document-input-format-PDF.cs) |  | Add pages to a PDF document input format PDF |
| [add-text-to-a-PDF-document-input-format-PDF](./add-text-to-a-PDF-document-input-format-PDF.cs) | `TextFragment`, `TextBuilder` | Add text to a PDF document input format PDF |
| [add-text-to-a-PDF-layer-input-format-PDF](./add-text-to-a-PDF-layer-input-format-PDF.cs) | `TextFragment`, `TextBuilder` | Add text to a PDF layer input format PDF |
| [add-watermarks-headers-footers-and-customize-page-margins-in...](./add-watermarks-headers-footers-and-customize-page-margins-input-format-PDF.cs) | `TextFragment` | Add watermarks headers footers and customize page margins input format PDF |
| [add-watermarks-to-a-PDF-input-format-PDF](./add-watermarks-to-a-PDF-input-format-PDF.cs) | `Rectangle` | Add watermarks to a PDF input format PDF |
| [apply-encryption-using-doc.Encrypt-userPassword-ownerPasswor...](./apply-encryption-using-doc.Encrypt-userPassword-ownerPassword-Permissions.All-EncryptionAlgor.cs) | `PdfFileSecurity` | Apply encryption using doc.Encrypt userPassword ownerPassword Permissions.All... |
| [apply-page-layouts-to-a-PDF-input-format-PDF](./apply-page-layouts-to-a-PDF-input-format-PDF.cs) |  | Apply page layouts to a PDF input format PDF |
| [apply-text-styles-in-a-PDF-input-format-PDF](./apply-text-styles-in-a-PDF-input-format-PDF.cs) | `TextFragmentAbsorber` | Apply text styles in a PDF input format PDF |
| [capture-exceptions-during-PDF-processing-input-format-PDF](./capture-exceptions-during-PDF-processing-input-format-PDF.cs) |  | Capture exceptions during PDF processing input format PDF |
| [clean-the-PDF-document-using-PdfCleaner-input-format-PDF](./clean-the-PDF-document-using-PdfCleaner-input-format-PDF.cs) |  | Clean the PDF document using PdfCleaner input format PDF |
| [clear-hidden-data-from-a-PDF-input-format-PDF](./clear-hidden-data-from-a-PDF-input-format-PDF.cs) |  | Clear hidden data from a PDF input format PDF |
| [close-a-PDF-document-input-format-PDF](./close-a-PDF-document-input-format-PDF.cs) |  | Close a PDF document input format PDF |
| [combine-PDF-documents-input-format-PDF](./combine-PDF-documents-input-format-PDF.cs) |  | Combine PDF documents input format PDF |
| [compress-a-PDF-document-input-format-PDF](./compress-a-PDF-document-input-format-PDF.cs) |  | Compress a PDF document input format PDF |
| [compress-images-and-remove-unused-objects-to-reduce-PDF-file...](./compress-images-and-remove-unused-objects-to-reduce-PDF-file-size-input-format-PDF.cs) |  | Compress images and remove unused objects to reduce PDF file size input forma... |
| [compress-images-remove-unused-objects-and-apply-other-optimi...](./compress-images-remove-unused-objects-and-apply-other-optimizations-input-format-PDF.cs) |  | Compress images remove unused objects and apply other optimizations input for... |
| [configure-document-security-input-format-PDF](./configure-document-security-input-format-PDF.cs) |  | Configure document security input format PDF |
| [control-layer-visibility-input-format-PDF](./control-layer-visibility-input-format-PDF.cs) |  | Control layer visibility input format PDF |
| [create-a-blank-PDF-document-output-format-PDF](./create-a-blank-PDF-document-output-format-PDF.cs) |  | Create a blank PDF document output format PDF |
| [create-a-new-PDF-layer-input-format-PDF](./create-a-new-PDF-layer-input-format-PDF.cs) |  | Create a new PDF layer input format PDF |
| [create-diagnostic-PDF-content-input-format-PDF](./create-diagnostic-PDF-content-input-format-PDF.cs) |  | Create diagnostic PDF content input format PDF |
| [delete-PDF-metadata-input-format-PDF](./delete-PDF-metadata-input-format-PDF.cs) |  | Delete PDF metadata input format PDF |
| [delete-page-2-from-a-PDF-document-input-format-PDF](./delete-page-2-from-a-PDF-document-input-format-PDF.cs) |  | Delete page 2 from a PDF document input format PDF |
| [delete-pages-from-a-PDF-input-format-PDF](./delete-pages-from-a-PDF-input-format-PDF.cs) |  | Delete pages from a PDF input format PDF |
| ... | | *and 42 more files* |

## Category Statistics
- Total examples: 72

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

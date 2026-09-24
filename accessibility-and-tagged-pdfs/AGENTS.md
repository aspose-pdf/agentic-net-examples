---
name: accessibility-and-tagged-pdfs
description: C# examples for accessibility-and-tagged-pdfs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility-and-tagged-pdfs

> **Accessibility and tagged PDFs** in PDF using C# / .NET -- **70** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **accessibility-and-tagged-pdfs** category.
This folder contains standalone C# examples for accessibility-and-tagged-pdfs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **accessibility-and-tagged-pdfs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (43/70 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (33/70 files)
- `using Aspose.Pdf.LogicalStructure;` (31/70 files)
- `using Aspose.Pdf.Text;` (8/70 files)
- `using Aspose.Pdf.Annotations;` (5/70 files)
- `using Aspose.Pdf.Forms;` (2/70 files)
- `using System;` (45/70 files)
- `using System.IO;` (45/70 files)
- `using System.Collections.Generic;` (8/70 files)
- `using System.Xml.Linq;` (3/70 files)
- `using System.Linq;` (2/70 files)
- `using System.Text.Json;` (2/70 files)
- `using System.Xml;` (2/70 files)
- `using System.Text;` (1/70 files)
- `using System.Xml.Xsl;` (1/70 files)

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
| [add-actualtext-to-images](./add-actualtext-to-images.cs) | Add ActualText Attribute to Image Figure in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a PDF, using the TaggedContent API to create a Figure element for an image, ... |
| [add-caption-note-to-figure](./add-caption-note-to-figure.cs) | Add Caption to Figure Using Note Element in Tagged PDF | `Document`, `ITaggedContent`, `FigureElement` | Demonstrates how to create a figure element in a tagged PDF and attach a note element as a captio... |
| [add-custom-cell-tags-and-validate-pdf](./add-custom-cell-tags-and-validate-pdf.cs) | Add custom cell tags and validate pdf |  | Add custom cell tags and validate pdf |
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `ParagraphElement` | Demonstrates opening a PDF, accessing its tagged content, creating a paragraph element, assigning... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to Tagged PDF | `Document`, `ITaggedContent`, `Page` | Shows how to insert a clickable link annotation that points to an external URL and set its title/... |
| [add-internal-link-element-to-tagged-pdf](./add-internal-link-element-to-tagged-pdf.cs) | Add internal link element to tagged pdf |  | Add internal link element to tagged pdf |
| [add-internal-page-link-to-tagged-pdf](./add-internal-page-link-to-tagged-pdf.cs) | Create Internal /Link Element in a Tagged PDF | `Document`, `ITaggedContent`, `CreateLinkElement` | Demonstrates how to add an internal page link to a PDF while using the tagged‑content API to set ... |
| [add-missing-language-attributes-to-pdf-structure](./add-missing-language-attributes-to-pdf-structure.cs) | Add missing language attributes to pdf structure |  | Add missing language attributes to pdf structure |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add Note Element to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a note element as a child of a paragraph in a tagged PDF, set document langua... |
| [add-page-break-to-tagged-pdf](./add-page-break-to-tagged-pdf.cs) | Add Page Break Element to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to insert a PageBreak element into the logical structure tree of a PDF to improv... |
| [add-placeholder-textbox-form-field-tagged](./add-placeholder-textbox-form-field-tagged.cs) | Add Text Box Form Field with Placeholder to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a TextBox form field with placeholder text into a PDF and associate it... |
| [add-tagged-form-field-with-placeholder](./add-tagged-form-field-with-placeholder.cs) | Add tagged form field with placeholder |  | Add tagged form field with placeholder |
| [add-tagged-table-to-pdf](./add-tagged-table-to-pdf.cs) | Add tagged table to pdf |  | Add tagged table to pdf |
| [add-textbox-form-field-tagged-pdf](./add-textbox-form-field-tagged-pdf.cs) | Create a Form Field and Associate It with a Tagged /Form Ele... | `Document`, `TextBoxField`, `Form` | Demonstrates how to add a TextBox field to the AcroForm of a PDF, create a /Form structure elemen... |
| [append-paragraph-actualtext-to-toci](./append-paragraph-actualtext-to-toci.cs) | Add Paragraph under TOCI with ActualText in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a TOCI element in a tagged PDF, add a paragraph beneath it, and set th... |
| [apply-custom-row-borders-to-pdf-table](./apply-custom-row-borders-to-pdf-table.cs) | Apply custom row borders to pdf table |  | Apply custom row borders to pdf table |
| [batch-convert-pdfs-to-tagged-pdfa](./batch-convert-pdfs-to-tagged-pdfa.cs) | Batch Auto‑Tag PDFs and Save Tagged Versions | `Document`, `AutoTaggingSettings`, `Save` | Demonstrates how to process a folder of untagged PDFs, enable Aspose.Pdf auto‑tagging, optionally... |
| [batch-convert-untagged-pdfs-to-tagged](./batch-convert-untagged-pdfs-to-tagged.cs) | Batch convert untagged pdfs to tagged |  | Batch convert untagged pdfs to tagged |
| [batch-pdf-validation-dashboard](./batch-pdf-validation-dashboard.cs) | Batch pdf validation dashboard |  | Batch pdf validation dashboard |
| [batch-pdfa1b-validation-xml-logs-dashboard](./batch-pdfa1b-validation-xml-logs-dashboard.cs) | Batch Validate PDFs for PDF/UA Compliance and Show Dashboard | `Document`, `PdfFormat`, `Validate` | The example processes all PDF files in a folder, validates each one for PDF/UA compliance using A... |
| [batch-tag-pdfs-in-folder](./batch-tag-pdfs-in-folder.cs) | Batch tag pdfs in folder |  | Batch tag pdfs in folder |
| [batch-tag-pdfs-with-suffix](./batch-tag-pdfs-with-suffix.cs) | Batch Tag PDFs for Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Iterates through all PDF files in a folder, adds basic accessibility tags (language, title, a par... |
| [batch-validate-pdfs-summary-csv](./batch-validate-pdfs-summary-csv.cs) | Batch Validate PDFs and Generate XML Logs with Summary CSV | `Document` | The example iterates through a folder of PDF files, uses Aspose.Pdf to check basic structural val... |
| [batch-validate-pdfs-xml-logs-summary-csv](./batch-validate-pdfs-xml-logs-summary-csv.cs) | Batch validate pdfs xml logs summary csv |  | Batch validate pdfs xml logs summary csv |
| [check-pdf-ua-compliance-and-log-result](./check-pdf-ua-compliance-and-log-result.cs) | Check pdf ua compliance and log result |  | Check pdf ua compliance and log result |
| [check-pdf-ua-compliance-and-log](./check-pdf-ua-compliance-and-log.cs) | Check PDF/UA Compliance and Log Result | `Document`, `Validate`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, validating it against PDF/UA‑1 standards, and outputt... |
| [convert-pdf-to-pdf-ua-with-auto-tagging](./convert-pdf-to-pdf-ua-with-auto-tagging.cs) | Convert pdf to pdf ua with auto tagging |  | Convert pdf to pdf ua with auto tagging |
| [create-merged-table-header-tagged-pdf](./create-merged-table-header-tagged-pdf.cs) | Create merged table header tagged pdf |  | Create merged table header tagged pdf |
| [create-nested-bullet-numbered-lists-tagged-pdf](./create-nested-bullet-numbered-lists-tagged-pdf.cs) | Create Tagged PDF with Bullet and Numbered Lists | `Document`, `Page`, `SetLanguage` | Demonstrates how to generate a PDF with tagged content and add simple bullet and numbered list it... |
| [create-nested-table-in-paragraph](./create-nested-table-in-paragraph.cs) | Create nested table in paragraph |  | Create nested table in paragraph |
| ... | | | *and 40 more files* |

## Category Statistics
- Total examples: 70

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.AttributeKey`
- `Aspose.Pdf.AttributeOwnerStandard`
- `Aspose.Pdf.BorderInfo`
- `Aspose.Pdf.BorderSide`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Document.Validate`
- `Aspose.Pdf.FontRepository`
- `Aspose.Pdf.FontStyles`
- `Aspose.Pdf.HorizontalAlignment`
- `Aspose.Pdf.ITaggedContent`
- `Aspose.Pdf.ITaggedContent.CreateArtElement`
- `Aspose.Pdf.ITaggedContent.CreateDivElement`
- `Aspose.Pdf.ITaggedContent.CreateSectElement`

### Rules
- Obtain the tagged content interface via {doc}.TaggedContent and set metadata using {doc}.TaggedContent.SetTitle({string_literal}) and {doc}.TaggedContent.SetLanguage({string_literal}) before saving.
- Persist the PDF after configuring tagged metadata with {doc}.Save({output_pdf}).
- When creating a tagged PDF, retrieve the ITaggedContent from {doc}.TaggedContent, then call SetTitle({string_literal}) and SetLanguage({string_literal}) to define document metadata before adding any structure elements.
- To insert textual content, use ITaggedContent.CreateParagraphElement() to obtain a ParagraphElement, set its text with SetText({string_literal}), and attach it to the document hierarchy via ITaggedContent.RootElement.AppendChild({paragraph_element}).
- Persist the tagged PDF by invoking {doc}.Save({output_pdf}).

### Warnings
- The example creates an empty Document; real scenarios may need to add pages/content before saving.
- SetLanguage expects a BCP‑47 language tag (e.g., "en-US").
- Assumed fully qualified names for element classes (SectElement, DivElement, ArtElement) are in Aspose.Pdf.Tagged namespace; verify against the library version.
- StructureTextState properties are applicable only to elements that support text styling; ensure the element type (e.g., ParagraphElement) supports them.
- The example relies on default page creation; no explicit page handling is shown.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for accessibility-and-tagged-pdfs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-24 | Run: `20260924_014443_7960f0`
<!-- AUTOGENERATED:END -->

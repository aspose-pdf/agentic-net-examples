---
name: accessibility-and-tagged-pdfs
description: C# examples for accessibility-and-tagged-pdfs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility-and-tagged-pdfs

> **Accessibility and tagged PDFs** in PDF using C# / .NET -- **45** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **accessibility-and-tagged-pdfs** category.
This folder contains standalone C# examples for accessibility-and-tagged-pdfs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **accessibility-and-tagged-pdfs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (43/45 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (33/45 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (31/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/45 files)
- `using Aspose.Pdf.Annotations;` (5/45 files)
- `using Aspose.Pdf.Forms;` (2/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (45/45 files)
- `using System.Collections.Generic;` (8/45 files)
- `using System.Xml.Linq;` (3/45 files)
- `using System.Linq;` (2/45 files)
- `using System.Text.Json;` (2/45 files)
- `using System.Xml;` (2/45 files)
- `using System.Text;` (1/45 files)
- `using System.Xml.Xsl;` (1/45 files)

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
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `ParagraphElement` | Demonstrates opening a PDF, accessing its tagged content, creating a paragraph element, assigning... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to Tagged PDF | `Document`, `ITaggedContent`, `Page` | Shows how to insert a clickable link annotation that points to an external URL and set its title/... |
| [add-internal-page-link-to-tagged-pdf](./add-internal-page-link-to-tagged-pdf.cs) | Create Internal /Link Element in a Tagged PDF | `Document`, `ITaggedContent`, `CreateLinkElement` | Demonstrates how to add an internal page link to a PDF while using the tagged‑content API to set ... |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add Note Element to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a note element as a child of a paragraph in a tagged PDF, set document langua... |
| [add-page-break-to-tagged-pdf](./add-page-break-to-tagged-pdf.cs) | Add Page Break Element to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to insert a PageBreak element into the logical structure tree of a PDF to improv... |
| [add-placeholder-textbox-form-field-tagged](./add-placeholder-textbox-form-field-tagged.cs) | Add Text Box Form Field with Placeholder to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a TextBox form field with placeholder text into a PDF and associate it... |
| [add-textbox-form-field-tagged-pdf](./add-textbox-form-field-tagged-pdf.cs) | Create a Form Field and Associate It with a Tagged /Form Ele... | `Document`, `TextBoxField`, `Form` | Demonstrates how to add a TextBox field to the AcroForm of a PDF, create a /Form structure elemen... |
| [append-paragraph-actualtext-to-toci](./append-paragraph-actualtext-to-toci.cs) | Add Paragraph under TOCI with ActualText in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a TOCI element in a tagged PDF, add a paragraph beneath it, and set th... |
| [batch-convert-pdfs-to-tagged-pdfa](./batch-convert-pdfs-to-tagged-pdfa.cs) | Batch Auto‑Tag PDFs and Save Tagged Versions | `Document`, `AutoTaggingSettings`, `Save` | Demonstrates how to process a folder of untagged PDFs, enable Aspose.Pdf auto‑tagging, optionally... |
| [batch-pdfa1b-validation-xml-logs-dashboard](./batch-pdfa1b-validation-xml-logs-dashboard.cs) | Batch Validate PDFs for PDF/UA Compliance and Show Dashboard | `Document`, `PdfFormat`, `Validate` | The example processes all PDF files in a folder, validates each one for PDF/UA compliance using A... |
| [batch-tag-pdfs-with-suffix](./batch-tag-pdfs-with-suffix.cs) | Batch Tag PDFs for Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Iterates through all PDF files in a folder, adds basic accessibility tags (language, title, a par... |
| [batch-validate-pdfs-summary-csv](./batch-validate-pdfs-summary-csv.cs) | Batch Validate PDFs and Generate XML Logs with Summary CSV | `Document` | The example iterates through a folder of PDF files, uses Aspose.Pdf to check basic structural val... |
| [check-pdf-ua-compliance-and-log](./check-pdf-ua-compliance-and-log.cs) | Check PDF/UA Compliance and Log Result | `Document`, `Validate`, `PdfFormat` | Demonstrates loading a PDF with Aspose.Pdf, validating it against PDF/UA‑1 standards, and outputt... |
| [create-nested-bullet-numbered-lists-tagged-pdf](./create-nested-bullet-numbered-lists-tagged-pdf.cs) | Create Tagged PDF with Bullet and Numbered Lists | `Document`, `Page`, `SetLanguage` | Demonstrates how to generate a PDF with tagged content and add simple bullet and numbered list it... |
| [create-pdf-with-heading-and-language](./create-pdf-with-heading-and-language.cs) | Create Tagged PDF with Heading and Language | `Document`, `TextFragment`, `ITaggedContent` | Demonstrates how to create a new PDF, add a level‑1 heading as a logical structure element, set d... |
| [create-tagged-3x4-table-in-pdf](./create-tagged-3x4-table-in-pdf.cs) | Add Tagged Table to PDF for Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a three‑row, four‑column table in a PDF's logical structure tree using... |
| [create-tagged-pdf-table-with-merged-header](./create-tagged-pdf-table-with-merged-header.cs) | Create Tagged Table with Header Cells in PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to add a tagged table to an existing PDF, merge cells for a header row, assign t... |
| [create-tagged-pdf-with-toc](./create-tagged-pdf-with-toc.cs) | Create Tagged PDF with Table of Contents | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to insert a Table of Contents page into a PDF and tag it for accessibility using... |
| [create-tagged-table-with-custom-tags](./create-tagged-table-with-custom-tags.cs) | Tag Table Cells with Data Types and Validate PDF/UA Complian... | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a tagged PDF table, assign custom data‑type tags to each cell using Al... |
| [custom-table-cell-borders-by-row-index](./custom-table-cell-borders-by-row-index.cs) | Apply Custom Borders to Table Cells by Row Index | `Document`, `Page`, `Table` | Demonstrates how to create a PDF table with Aspose.Pdf and apply different border colors and thic... |
| [enable-auto-tagging-save-tagged-pdf](./enable-auto-tagging-save-tagged-pdf.cs) | Enable Automatic Tagging for PDF/UA Conversion | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates how to turn on Aspose.PDF automatic tagging, set basic document metadata, and save t... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a tagged PDF, traverses its logical structure tree, and serializes the hierarchy (element t... |
| [export-structure-tree-to-xml-xsl-report](./export-structure-tree-to-xml-xsl-report.cs) | Export PDF Structure Tree to XML and Generate HTML Report vi... | `Document`, `XmlSaveOptions`, `Save` | Demonstrates how to export a PDF's logical structure tree to an XML file using Aspose.Pdf and the... |
| [export-tagged-pdf-content-to-json](./export-tagged-pdf-content-to-json.cs) | Export Tagged PDF Structure to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, verifies that it contains a tagged structure, traverses the logical structure tree, ... |
| [extract-notes-from-tagged-pdf](./extract-notes-from-tagged-pdf.cs) | Extract PDF Note Annotations to Text File | `Document`, `Page`, `Annotation` | The example opens a PDF, iterates through all pages and their annotations, extracts the contents ... |
| [extract-pdf-links-to-csv](./extract-pdf-links-to-csv.cs) | Extract PDF Links to CSV | `Document`, `Page`, `Annotation` | Shows how to open a PDF with Aspose.Pdf, iterate through its pages to collect link annotations, r... |
| [extract-text-from-tagged-pdf-structure](./extract-text-from-tagged-pdf-structure.cs) | Extract Text from a Tagged PDF via Structure Tree Traversal | `Document`, `ITaggedContent`, `StructureElement` | The example loads a PDF, checks if it is tagged, and if so walks the logical structure tree to ou... |
| [filter-xml-missing-alt-text](./filter-xml-missing-alt-text.cs) | Find Images Missing Alt Text in Validation XML |  | Loads an XML validation report, selects Image elements that have an Id attribute but no non‑empty... |
| ... | | | *and 15 more files* |

## Category Statistics
- Total examples: 45

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

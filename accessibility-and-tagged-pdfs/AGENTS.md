---
name: accessibility-and-tagged-pdfs
description: C# examples for accessibility-and-tagged-pdfs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility-and-tagged-pdfs

> **Accessibility and tagged PDFs** in PDF using C# / .NET -- **43** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **accessibility-and-tagged-pdfs** category.
This folder contains standalone C# examples for accessibility-and-tagged-pdfs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **accessibility-and-tagged-pdfs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (41/43 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (31/43 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (28/43 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (6/43 files)
- `using Aspose.Pdf.Text;` (4/43 files)
- `using Aspose.Pdf.Drawing;` (1/43 files)
- `using Aspose.Pdf.Forms;` (1/43 files)
- `using System;` (43/43 files)
- `using System.IO;` (43/43 files)
- `using System.Collections.Generic;` (6/43 files)
- `using System.Text;` (3/43 files)
- `using System.Xml.Linq;` (3/43 files)
- `using System.Linq;` (2/43 files)
- `using System.Text.Json;` (2/43 files)
- `using System.Xml.Xsl;` (1/43 files)

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
| [add-actualtext-to-images](./add-actualtext-to-images.cs) | Add ActualText Alt Text to Images in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create Figure elements for images in a PDF, set their AlternativeText (mapped... |
| [add-caption-note-to-figure](./add-caption-note-to-figure.cs) | Add Caption Note to Figure in Tagged PDF | `Document`, `SetLanguage`, `SetTitle` | The example loads a PDF, enables tagged PDF features, creates a figure element with alternative t... |
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a paragraph element in a tagged PDF, assign a custom tag name, set its text, ... |
| [add-internal-page-link-to-tagged-pdf](./add-internal-page-link-to-tagged-pdf.cs) | Add Internal Page Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to create a link annotation that navigates to another page within the same PDF u... |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add Note Element to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a note element and attach it as a child of a paragraph in a tagged PDF to pro... |
| [add-page-break-to-tagged-pdf](./add-page-break-to-tagged-pdf.cs) | Add Page Break to Tagged PDF Logical Structure | `Document`, `ITaggedContent`, `DivElement` | Shows how to insert a page‑break element into the logical structure tree of a tagged PDF using As... |
| [add-textbox-form-field-tagged-pdf](./add-textbox-form-field-tagged-pdf.cs) | Add Tagged Form Field and Structure Element to PDF | `Document`, `ITaggedContent`, `TextBoxField` | Demonstrates how to create a text box form field, register it in the AcroForm, and link it to a /... |
| [append-paragraph-actualtext-to-toci](./append-paragraph-actualtext-to-toci.cs) | Append Paragraph with ActualText to TOCI in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates creating a TOCI placeholder in a PDF's logical structure, adding a paragraph element... |
| [batch-convert-pdfs-to-tagged-pdfa](./batch-convert-pdfs-to-tagged-pdfa.cs) | Batch Convert PDFs to Tagged PDFs with Auto‑Tagging | `Document`, `ITaggedContent`, `EnableAutoTagging` | Shows how to iterate over a folder of untagged PDF files, enable Aspose.Pdf auto‑tagging, set lan... |
| [batch-pdfa1b-validation-xml-logs-dashboard](./batch-pdfa1b-validation-xml-logs-dashboard.cs) | Batch PDF Validation with XML Logs and HTML Dashboard | `Document`, `XmlSaveOptions`, `Save` | Processes all PDFs in a folder, checks basic compliance, saves an XML log for each file, and gene... |
| [batch-tag-pdfs-with-suffix](./batch-tag-pdfs-with-suffix.cs) | Batch Auto‑Tag PDFs and Save with Suffix | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Processes all PDF files in a folder, enables Aspose.Pdf auto‑tagging, adds basic structure elemen... |
| [batch-validate-pdfs-summary-csv](./batch-validate-pdfs-summary-csv.cs) | Batch PDF Validation with XML Logs and CSV Summary | `Document`, `Pages`, `Validate` | Iterates through a folder of PDF files, validates each using Aspose.Pdf, creates an XML log for e... |
| [check-pdf-ua-compliance-and-log](./check-pdf-ua-compliance-and-log.cs) | Check PDF/UA Compliance and Log Result | `Document`, `Validate`, `PdfFormat` | Demonstrates how to programmatically validate a PDF against PDF/UA‑1 using Aspose.Pdf and output ... |
| [create-nested-bullet-numbered-lists-tagged-pdf](./create-nested-bullet-numbered-lists-tagged-pdf.cs) | Create Nested Bullet and Numbered List in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to build a PDF with an outer bullet list that contains an indented numbered sub‑... |
| [create-pdf-with-heading-and-language](./create-pdf-with-heading-and-language.cs) | Create Tagged PDF with Heading and Language Attribute | `Document`, `ITaggedContent`, `SetLanguage` | Shows how to create a new PDF, enable tagged PDF, set the document language, add an H1 heading el... |
| [create-tagged-3x4-table-in-pdf](./create-tagged-3x4-table-in-pdf.cs) | Add a 3×4 Table to a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a three‑row by four‑column table in a tagged PDF, set alternative text for ac... |
| [create-tagged-pdf-table-with-merged-header](./create-tagged-pdf-table-with-merged-header.cs) | Create Tagged Table Header with TH Role and ActualText | `Document`, `ITaggedContent`, `StructureElement` | The example loads a PDF, enables tagged PDF support, creates a table structure with a header row,... |
| [create-tagged-pdf-with-toc](./create-tagged-pdf-with-toc.cs) | Create Tagged Table of Contents in PDF | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates how to add a visual Table of Contents page and corresponding logical heading structu... |
| [create-tagged-table-with-custom-tags](./create-tagged-table-with-custom-tags.cs) | Tag Table Cells with Custom Data Types in a PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates creating a tagged PDF, adding a table structure, and assigning custom data‑type tags... |
| [custom-table-cell-borders-by-row-index](./custom-table-cell-borders-by-row-index.cs) | Apply Custom Border Styles to PDF Table Cells | `Document`, `Table`, `Row` | Demonstrates how to create a table in a PDF and set different border widths and colors for cells ... |
| [enable-auto-tagging-save-tagged-pdf](./enable-auto-tagging-save-tagged-pdf.cs) | Automatic PDF/UA Tagging and Save | `Document`, `AutoTaggingSettings`, `ITaggedContent` | Demonstrates how to enable Aspose.Pdf automatic tagging (PDF/UA conversion), set the document lan... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructureElement` | Shows how to load a tagged PDF, traverse its logical structure tree, and serialize the hierarchy ... |
| [export-structure-tree-to-xml-xsl-report](./export-structure-tree-to-xml-xsl-report.cs) | Export PDF Structure Tree to XML and Generate Report via XSL... | `Document`, `XmlSaveOptions`, `Save` | Shows how to export a PDF's logical structure tree to an XML file using Aspose.Pdf and then trans... |
| [export-tagged-pdf-content-to-json](./export-tagged-pdf-content-to-json.cs) | Export Tagged PDF Content to JSON | `Document`, `ITaggedContent`, `StructureElement` | Shows how to read a PDF's logical (tagged) structure with Aspose.Pdf, build a hierarchical DTO, a... |
| [extract-notes-from-tagged-pdf](./extract-notes-from-tagged-pdf.cs) | Extract PDF Note Annotations to Text File | `Document`, `Page`, `Annotation` | Demonstrates how to iterate through a PDF, collect all TextAnnotation (note) contents, concatenat... |
| [extract-pdf-links-to-csv](./extract-pdf-links-to-csv.cs) | Extract PDF Links to CSV | `Document`, `Page`, `Annotation` | Shows how to open a PDF with Aspose.Pdf, iterate through pages and link annotations, collect the ... |
| [extract-text-from-tagged-pdf-structure](./extract-text-from-tagged-pdf-structure.cs) | Extract Text from Tagged PDF Structure Tree | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to load a PDF with Aspose.Pdf, verify that it is tagged, and recursively travers... |
| [filter-xml-missing-alt-text](./filter-xml-missing-alt-text.cs) | Extract Image IDs with Missing Alt Text from Validation XML |  | Loads an XML validation report, filters errors that indicate missing alternative text, and output... |
| [iterate-tagged-pdf-structure-elements](./iterate-tagged-pdf-structure-elements.cs) | Iterate Tagged PDF Structure Elements and Log Details | `Document`, `ITaggedContent`, `StructureElement` | The example loads a PDF, accesses its tagged content, and recursively walks the structure tree to... |
| [nested-table-in-paragraph-tagged-pdf](./nested-table-in-paragraph-tagged-pdf.cs) | Create Nested Table Inside Paragraph and Validate Tagging Hi... | `Document`, `ITaggedContent`, `StructureElement` | Shows how to add a paragraph containing an outer table with a nested table using Aspose.Pdf's tag... |
| ... | | | *and 13 more files* |

## Category Statistics
- Total examples: 43

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
Updated: 2026-10-27 | Run: `20261027_020845_de658f`
<!-- AUTOGENERATED:END -->

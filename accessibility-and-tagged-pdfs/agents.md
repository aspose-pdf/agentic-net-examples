---
name: accessibility-and-tagged-pdfs
description: C# examples for accessibility-and-tagged-pdfs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility-and-tagged-pdfs

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
- `using Aspose.Pdf.Tagged;` (35/45 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (33/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/45 files)
- `using Aspose.Pdf.Annotations;` (2/45 files)
- `using Aspose.Pdf.Forms;` (2/45 files)
- `using Aspose.Pdf.Drawing;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (45/45 files)
- `using System.Collections.Generic;` (5/45 files)
- `using System.Runtime.InteropServices;` (5/45 files)
- `using System.Linq;` (3/45 files)
- `using System.Text;` (2/45 files)
- `using System.Text.Json;` (2/45 files)
- `using System.Xml;` (2/45 files)
- `using System.Xml.Linq;` (2/45 files)
- `using System.Text.Json.Serialization;` (1/45 files)
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
| [add-actualtext-to-image-structure-element](./add-actualtext-to-image-structure-element.cs) | Add ActualText to Image Structure Elements for PDF Accessibi... | `Document`, `ITaggedContent`, `SetLanguage` | The example loads a PDF, accesses its tagged‑content API, and adds alternative text to each image... |
| [add-actualtext-to-span-pronunciation](./add-actualtext-to-span-pronunciation.cs) | Add ActualText to a Span for Pronunciation in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, accesses its tagged content, creates a Span element, sets visible text and hidden pr... |
| [add-caption-to-figure-in-tagged-pdf](./add-caption-to-figure-in-tagged-pdf.cs) | Add Caption to Figure in Tagged PDF | `Document`, `ITaggedContent`, `FigureElement` | Shows how to create a Figure element in a tagged PDF and attach a Note element as a caption using... |
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a tagged PDF, add a paragraph element, assign a custom tag name, and s... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates inserting a link element into a PDF's tagged structure, assigning an external URL, a... |
| [add-internal-link-to-tagged-pdf](./add-internal-link-to-tagged-pdf.cs) | Add Internal Link to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates creating a LinkElement with role /Link in a tagged PDF, associating it with a LocalH... |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add a Note Element to a Paragraph in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a paragraph and attach a note (footnote/endnote) as its child in a tag... |
| [add-page-break-to-tagged-pdf](./add-page-break-to-tagged-pdf.cs) | Add Page Break to Tagged PDF Using Logical Structure | `Document`, `ITaggedContent`, `StructureElement` | Shows how to insert a page‑break element into the logical structure tree of a tagged PDF for impr... |
| [add-paragraph-actualtext-to-toci](./add-paragraph-actualtext-to-toci.cs) | Add Paragraph with ActualText under TOCI in a Tagged PDF | `Document`, `ITaggedContent`, `TOCIElement` | Demonstrates how to create a TOCI (Table of Contents Item) element in a tagged PDF, add a paragra... |
| [add-placeholder-textbox-form-field](./add-placeholder-textbox-form-field.cs) | Add Placeholder TextBox Form Field and Tag as /Form Element | `Document`, `TextBoxField`, `Add` | Loads an existing PDF, creates a TextBox form field with placeholder text, adds it to the documen... |
| [add-table-to-tagged-pdf](./add-table-to-tagged-pdf.cs) | Add Table to Tagged PDF Structure | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to create a 3x4 table within the logical structure tree of a PDF using Aspose.Pd... |
| [batch-add-missing-tags-to-pdfs](./batch-add-missing-tags-to-pdfs.cs) | Batch Add Missing Tags to PDFs | `Document`, `ITaggedContent`, `AutoTaggingSettings` | Processes all PDF files in a folder, enables Aspose.Pdf auto‑tagging to add missing accessibility... |
| [batch-convert-pdfs-to-tagged-pdf-ua](./batch-convert-pdfs-to-tagged-pdf-ua.cs) | Batch Convert PDFs to Tagged PDF/UA with Auto‑Tagging | `Document`, `AutoTaggingSettings`, `PdfFormatConversionOptions` | Processes all PDF files in a folder, enables Aspose.Pdf auto‑tagging, converts each document to P... |
| [batch-pdf-validation-xml-logs-dashboard](./batch-pdf-validation-xml-logs-dashboard.cs) | Batch PDF Validation with XML Logs and Compliance Dashboard | `Document`, `Validate`, `PdfFormat` | Demonstrates how to validate multiple PDF files against PDF/UA, generate XML validation logs, com... |
| [batch-pdfa1b-validation-xml-csv](./batch-pdfa1b-validation-xml-csv.cs) | Batch PDF/A-1B Validation with XML Logs and CSV Summary | `Document`, `Validate`, `PdfFormat` | Demonstrates how to validate multiple PDF files for PDF/A‑1B compliance using Aspose.Pdf, generat... |
| [check-pdf-ua-compliance-and-log-result](./check-pdf-ua-compliance-and-log-result.cs) | Check PDF/UA Compliance and Log Result | `Document`, `IsPdfUaCompliant` | Loads a PDF using Aspose.Pdf, checks its PDF/UA (accessibility) compliance, outputs the boolean t... |
| [create-nested-list-tagged-pdf](./create-nested-list-tagged-pdf.cs) | Create Nested Bullet and Numbered List in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to build a top‑level bullet list with a nested numbered sub‑list in a PDF using ... |
| [create-tagged-form-field](./create-tagged-form-field.cs) | Create Tagged Form Field in PDF | `Document`, `Page`, `TextBoxField` | Shows how to add a text box form field, register it in the AcroForm, and link it to a /Form struc... |
| [create-tagged-pdf-heading-language](./create-tagged-pdf-heading-language.cs) | Create Tagged PDF with Heading and Language | `Document`, `ITaggedContent`, `HeaderElement` | Shows how to create a new PDF, add a level‑1 heading structure element, assign a language attribu... |
| [create-tagged-pdf-with-toc](./create-tagged-pdf-with-toc.cs) | Create Tagged PDF with Table of Contents | `Document`, `ITaggedContent`, `TocInfo` | Demonstrates how to generate an accessible tagged PDF, set language and title metadata, add a Tab... |
| [create-tagged-table-header-with-merged-cells](./create-tagged-table-header-with-merged-cells.cs) | Create Tagged Table Header with Merged Cells in PDF | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to add a tagged table header with merged cells, assign the TH role, and set Actu... |
| [custom-table-cell-borders-pdf](./custom-table-cell-borders-pdf.cs) | Custom Table Cell Borders in PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, adding a table, and applying different border styles to row... |
| [enable-auto-tagging-pdf-ua-conversion](./enable-auto-tagging-pdf-ua-conversion.cs) | Enable Automatic Tagging for PDF/UA Conversion | `Document`, `PdfFormatConversionOptions`, `AutoTaggingSettings` | Demonstrates how to convert a PDF to a PDF/UA‑compliant tagged PDF using Aspose.Pdf's automatic t... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructTreeRootElement` | Loads a tagged PDF, extracts its logical structure tree, converts it into a serializable object, ... |
| [export-structure-tree-report](./export-structure-tree-report.cs) | Export PDF Structure Tree to XML and Generate Report via XSL... | `Document`, `SaveXml` | Shows how to export a PDF's accessibility structure tree to an XML file using Aspose.Pdf and then... |
| [export-tagged-pdf-structure-to-json](./export-tagged-pdf-structure-to-json.cs) | Export Tagged PDF Structure to JSON | `Document`, `ITaggedContent`, `StructureElement` | Shows how to read a PDF's tagged structure with Aspose.Pdf, build a hierarchical POCO model, and ... |
| [extract-notes-from-tagged-pdf](./extract-notes-from-tagged-pdf.cs) | Extract Notes from Tagged PDF to Text File | `Document`, `ITaggedContent`, `NoteElement` | Loads a tagged PDF, finds all NoteElement objects in the logical structure, concatenates their vi... |
| [extract-pdf-link-urls-to-csv](./extract-pdf-link-urls-to-csv.cs) | Extract PDF Link URLs to CSV | `Document`, `Page`, `Annotation` | Shows how to load a PDF with Aspose.Pdf, iterate through link annotations, retrieve their URI act... |
| [extract-text-from-tagged-pdf-structure-tree](./extract-text-from-tagged-pdf-structure-tree.cs) | Extract Text from Tagged PDF Structure Tree | `Document`, `ITaggedContent`, `StructureElement` | Shows how to load a PDF with Aspose.Pdf, access its tagged content, and recursively walk the stru... |
| [iterate-tagged-pdf-structure-elements](./iterate-tagged-pdf-structure-elements.cs) | Iterate Tagged PDF Structure Elements and Log Metadata | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, accesses its tagged content, and recursively walks the logical structure tree, print... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for accessibility-and-tagged-pdfs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_112942_856ae1`
<!-- AUTOGENERATED:END -->

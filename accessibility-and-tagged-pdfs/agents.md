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

- `using Aspose.Pdf;` (44/45 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (35/45 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (33/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/45 files)
- `using Aspose.Pdf.Annotations;` (2/45 files)
- `using Aspose.Pdf.Forms;` (2/45 files)
- `using Aspose.Pdf.Drawing;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (44/45 files)
- `using System.Collections.Generic;` (7/45 files)
- `using System.Runtime.InteropServices;` (3/45 files)
- `using System.Text.Json;` (2/45 files)
- `using System.Xml.Linq;` (2/45 files)
- `using System.Data;` (1/45 files)
- `using System.Linq;` (1/45 files)
- `using System.Text;` (1/45 files)
- `using System.Xml;` (1/45 files)
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
| [add-actualtext-to-images-in-tagged-pdf](./add-actualtext-to-images-in-tagged-pdf.cs) | Add ActualText to Images in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to assign ActualText (and AlternativeText) to image elements in a PDF using Aspo... |
| [add-caption-note-to-figure-in-tagged-pdf](./add-caption-note-to-figure-in-tagged-pdf.cs) | Add Caption Note to Figure in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a Figure element, attach a Note element as its caption, and save the document... |
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in Tagged PDF | `Document`, `SetLanguage`, `SetTitle` | Demonstrates creating a tagged PDF, adding a paragraph element, assigning a custom tag name, and ... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to Tagged PDF | `Document`, `SetLanguage`, `SetTitle` | Loads an existing PDF, accesses its tagged content, creates a link element that points to an exte... |
| [add-note-to-paragraph](./add-note-to-paragraph.cs) | Add a Note (Footnote) to a Paragraph in a PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add a paragraph, attach a supplemental note as a footnote to th... |
| [add-page-break-element-to-tagged-pdf](./add-page-break-element-to-tagged-pdf.cs) | Add Page Break Element to Tagged PDF | `Document`, `Save`, `ITaggedContent` | Shows how to insert a page‑break DIV element into the logical structure tree of a tagged PDF usin... |
| [add-paragraph-actualtext-under-toci](./add-paragraph-actualtext-under-toci.cs) | Add Paragraph with ActualText under TOCI Element | `Document`, `Save`, `ITaggedContent` | Demonstrates creating a TOCI element in a tagged PDF, appending a paragraph beneath it, setting t... |
| [batch-auto-tag-pdfs](./batch-auto-tag-pdfs.cs) | Batch Auto‑Tag PDFs in a Folder | `Document`, `Save`, `ITaggedContent` | Shows how to iterate through a directory of PDF files, enable Aspose.Pdf auto‑tagging for each do... |
| [batch-convert-pdfs-to-tagged-pdfs](./batch-convert-pdfs-to-tagged-pdfs.cs) | Batch Convert PDFs to Tagged PDFs with Auto‑Tagging | `Document`, `Convert`, `Save` | Demonstrates how to process a folder of untagged PDF files, enable global auto‑tagging, convert e... |
| [batch-pdfa1b-validation-with-xml-dashboard](./batch-pdfa1b-validation-with-xml-dashboard.cs) | Batch PDF/A-1b Validation with XML Logs and Dashboard | `Document`, `Validate`, `PdfFormat` | Loads PDF files from a folder, validates each against the PDF/A‑1b standard using Aspose.Pdf, wri... |
| [batch-validate-pdfs-summary-csv](./batch-validate-pdfs-summary-csv.cs) | Batch Validate PDFs and Generate Summary CSV | `Document`, `Validate`, `PdfFormat` | Demonstrates how to validate multiple PDF files for PDF/A‑1b compliance using Aspose.Pdf, write X... |
| [check-pdf-ua-compliance](./check-pdf-ua-compliance.cs) | Check PDF/UA Compliance and Log Result | `Document`, `IsPdfUaCompliant` | Shows how to load a PDF with Aspose.Pdf, evaluate its PDF/UA accessibility compliance, and record... |
| [create-form-field-with-structure-element](./create-form-field-with-structure-element.cs) | Create Form Field and Associate with /Form Structure Element | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to add a TextBox field to a PDF's AcroForm, create a corresponding /Form structu... |
| [create-internal-page-link-tagged-pdf](./create-internal-page-link-tagged-pdf.cs) | Create Internal Page Link with /Link Role in Tagged PDF | `Document`, `Save`, `ITaggedContent` | Demonstrates how to add a logical /Link element that points to an internal page number in a tagge... |
| [create-nested-list-tagged-pdf](./create-nested-list-tagged-pdf.cs) | Create Nested Bullet and Numbered List in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to build a hierarchical bullet and numbered list in a PDF using Aspose.Pdf's tag... |
| [create-nested-table-in-paragraph](./create-nested-table-in-paragraph.cs) | Create Nested Table Inside Paragraph and Validate Tagging Hi... | `Document`, `ITaggedContent`, `ParagraphElement` | Demonstrates adding a paragraph that contains a nested table to a PDF using Aspose.Pdf's tagged‑c... |
| [create-pdf-heading-language-attribute](./create-pdf-heading-language-attribute.cs) | Create PDF with Heading and Language Attribute | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to create a new PDF, add a level‑1 heading as a tagged structure element, and as... |
| [create-table-header-merged-cells-actualtext](./create-table-header-merged-cells-actualtext.cs) | Create Table Header with Merged Cells and ActualText | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to add a tagged table header to a PDF, merge header cells across columns, assign... |
| [create-tagged-3x4-table-pdf](./create-tagged-3x4-table-pdf.cs) | Create a Tagged 3x4 Table in PDF | `Document`, `Save`, `Add` | Shows how to build a 3‑row by 4‑column accessible table and add it to the structure tree of a PDF... |
| [create-tagged-pdf-table-with-custom-tags](./create-tagged-pdf-table-with-custom-tags.cs) | Create Tagged PDF Table with Custom Data Type Tags and Valid... | `Document`, `Page`, `ITaggedContent` | The example builds a PDF containing a visual table and a corresponding logical structure (tagged ... |
| [create-tagged-pdf-with-logical-toc](./create-tagged-pdf-with-logical-toc.cs) | Create Tagged PDF with Logical Table of Contents | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to use Aspose.Pdf's TaggedContent API to build a logical structure tree, add a T... |
| [custom-row-borders-pdf-table](./custom-row-borders-pdf-table.cs) | Apply Custom Row Borders to PDF Table | `Document`, `Save`, `Page` | Shows how to set alternating background colors and custom border styles for each row of a PDF tab... |
| [enable-auto-tagging-pdf-ua](./enable-auto-tagging-pdf-ua.cs) | Enable Automatic Tagging for PDF/UA Conversion | `Document`, `ITaggedContent`, `PdfFormatConversionOptions` | Shows how to configure Aspose.Pdf to automatically tag a PDF for PDF/UA compliance and save the r... |
| [export-pdf-structure-to-xml-xsl-report](./export-pdf-structure-to-xml-xsl-report.cs) | Export PDF Structure Tree to XML and Generate Report via XSL... | `Document`, `Save`, `XmlSaveOptions` | Demonstrates exporting a PDF's logical structure tree to an XML file using Aspose.Pdf and then ap... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructTreeRootElement` | Shows how to load a tagged PDF, traverse its logical structure tree, and serialize the hierarchy ... |
| [export-tagged-pdf-content-to-json](./export-tagged-pdf-content-to-json.cs) | Export Tagged PDF Content to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, reads its tagged logical structure, builds a hierarchical model of StructureElements... |
| [extract-pdf-links-to-csv](./extract-pdf-links-to-csv.cs) | Extract URLs from PDF Link Annotations to CSV | `Document`, `Page`, `Annotation` | Loads a PDF, iterates through all pages and link annotations, collects each link's URL, and write... |
| [extract-pdf-notes-to-text](./extract-pdf-notes-to-text.cs) | Extract PDF Note Elements and Save to Text File | `Document`, `ITaggedContent`, `NoteElement` | Shows how to load a tagged PDF, find all NoteElement objects, concatenate their ActualText values... |
| [extract-text-from-tagged-pdf-structure-traversal](./extract-text-from-tagged-pdf-structure-traversal.cs) | Extract Text from Tagged PDF via Structure Tree Traversal | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a PDF with Aspose.Pdf, accessing its tagged content, recursively walking the... |
| [find-images-missing-alt-text](./find-images-missing-alt-text.cs) | Find Images Missing Alt Text in PDF | `Document`, `Page`, `XImage` | Opens a PDF with Aspose.Pdf, iterates through each page's images, checks for alternative text, an... |
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
Updated: 2026-04-10 | Run: `20260410_111705_7ee31f`
<!-- AUTOGENERATED:END -->

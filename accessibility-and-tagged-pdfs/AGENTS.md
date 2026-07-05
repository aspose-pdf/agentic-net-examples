---
name: accessibility-and-tagged-pdfs
description: C# examples for accessibility-and-tagged-pdfs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility-and-tagged-pdfs

> **Accessibility and tagged PDFs** in PDF using C# / .NET -- **45** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.LogicalStructure;` (34/45 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/45 files)
- `using Aspose.Pdf.Forms;` (2/45 files)
- `using Aspose.Pdf.Text;` (2/45 files)
- `using Aspose.Pdf.Drawing;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (42/45 files)
- `using System.Collections.Generic;` (6/45 files)
- `using System.Linq;` (2/45 files)
- `using System.Text;` (2/45 files)
- `using System.Text.Json;` (2/45 files)
- `using System.Xml.Linq;` (2/45 files)
- `using System.Reflection;` (1/45 files)
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
| [add-actualtext-to-images](./add-actualtext-to-images.cs) | Add ActualText to Images for PDF Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to add an ActualText attribute to image elements in a PDF using Aspose.Pdf's tag... |
| [add-caption-note-to-figure-tagged-pdf](./add-caption-note-to-figure-tagged-pdf.cs) | Add Caption Note to a Figure in a Tagged PDF | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to create a Figure element, bind it to an image, and append a Note element as a ... |
| [add-custom-tags-to-pdf-table-cells](./add-custom-tags-to-pdf-table-cells.cs) | Add Custom Data-Type Tags to PDF Table Cells and Validate PD... | `Document`, `ITaggedContent`, `StructureElement` | The example loads an existing PDF, creates a tagged logical structure with a table, assigns custo... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to insert a LinkElement with a title attribute into a tagged PDF and associate it with ... |
| [add-figure-title-and-alt-text-to-tagged-pdf](./add-figure-title-and-alt-text-to-tagged-pdf.cs) | Add Figure Title and Alt Text to Tagged PDF | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to create a figure element in a tagged PDF, set its Title and AlternativeText fo... |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add a Note Element to a Paragraph in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a paragraph in a tagged PDF, add a supplemental note element as its ch... |
| [add-page-break-to-tagged-pdf](./add-page-break-to-tagged-pdf.cs) | Add Page Break Element to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to insert a PageBreak element into the logical structure tree of a tagged PDF us... |
| [add-paragraph-to-toci-tagged-pdf](./add-paragraph-to-toci-tagged-pdf.cs) | Add Paragraph with ActualText under TOCI in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a TOCI (Table of Contents Item) element, add a paragraph with ActualTe... |
| [add-tagged-table-to-pdf](./add-tagged-table-to-pdf.cs) | Add Tagged Table to PDF | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to use Aspose.Pdf's Tagged Content API to create a structure‑tree table with 3 r... |
| [add-textbox-form-field-with-placeholder](./add-textbox-form-field-with-placeholder.cs) | Add Text Box Form Field with Placeholder and Tag as Form Ele... | `Document`, `TextBoxField`, `Rectangle` | Demonstrates how to insert a TextBox form field with placeholder text into an existing PDF and ta... |
| [apply-custom-row-borders-to-pdf-table](./apply-custom-row-borders-to-pdf-table.cs) | Apply Custom Row Borders to PDF Table | `Document`, `Page`, `Table` | Demonstrates how to set different border styles for table rows in a PDF using Aspose.Pdf, alterna... |
| [batch-auto-tag-pdfs](./batch-auto-tag-pdfs.cs) | Batch Auto‑Tag PDFs and Save with Suffix | `Document`, `ITaggedContent`, `AutoTaggingSettings` | Demonstrates how to process a folder of PDF files, enable Aspose.Pdf auto‑tagging to add missing ... |
| [batch-convert-pdfs-to-tagged-pdfa1b](./batch-convert-pdfs-to-tagged-pdfa1b.cs) | Batch Convert PDFs to Tagged PDF/A-1B with Auto‑Tagging | `Document`, `AutoTaggingSettings`, `PdfFormatConversionOptions` | Loads each PDF from an input folder, enables Aspose.Pdf's auto‑tagging engine, converts the docum... |
| [batch-pdfa1b-validation-xml-dashboard](./batch-pdfa1b-validation-xml-dashboard.cs) | Batch PDF/A-1B Validation with XML Logs and CSV Dashboard | `Document`, `Validate`, `PdfFormat` | The example processes all PDF files in a folder, validates each against PDF/A‑1B using Aspose.Pdf... |
| [batch-validate-pdfs-xml-logs-summary-csv](./batch-validate-pdfs-xml-logs-summary-csv.cs) | Batch Validate PDFs and Generate XML Logs with Summary CSV | `Document`, `Validate`, `PdfFormat` | The example iterates through all PDF files in a folder, validates each against PDF/A‑1b using Asp... |
| [check-pdf-ua-compliance-and-log-result](./check-pdf-ua-compliance-and-log-result.cs) | Check PDF/UA Compliance and Log Result | `Document`, `IsPdfUaCompliant` | Demonstrates loading a PDF with Aspose.Pdf, checking its PDF/UA accessibility compliance via the ... |
| [create-bullet-numbered-lists-tagged-pdf](./create-bullet-numbered-lists-tagged-pdf.cs) | Create Bullet and Numbered Lists in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to generate a PDF containing both unordered (bullet) and ordered (numbered) list... |
| [create-form-field-and-associate-tagged-form-elemen...](./create-form-field-and-associate-tagged-form-element.cs) | Create a Form Field and Associate a Tagged /Form Element | `Document`, `TextBoxField`, `Add` | Demonstrates how to add a TextBox form field to a PDF's AcroForm, create a corresponding /Form st... |
| [create-internal-page-link-tagged-pdf](./create-internal-page-link-tagged-pdf.cs) | Create Internal Page Link with Tagged PDF Role /Link | `Document`, `LinkAnnotation`, `GoToAction` | Demonstrates how to add a link annotation that jumps to another page and associate it with a Tagg... |
| [create-merged-table-header-th-actualtext](./create-merged-table-header-th-actualtext.cs) | Create Merged Table Header with TH Role and ActualText | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates building a tagged PDF table, merging a header cell across columns, assigning the TH ... |
| [create-pdf-tagged-heading-language](./create-pdf-tagged-heading-language.cs) | Create PDF with Tagged Heading and Language Attribute | `Document`, `ITaggedContent`, `SetLanguage` | Demonstrates how to create a PDF, enable tagged content, add a level‑1 heading element, and assig... |
| [create-tagged-pdf-with-toc](./create-tagged-pdf-with-toc.cs) | Create Tagged PDF with Table of Contents | `Document`, `ITaggedContent`, `TOCElement` | Demonstrates how to generate a PDF with logical structure, set accessibility properties, and add ... |
| [custom-tagged-paragraph-pdf](./custom-tagged-paragraph-pdf.cs) | Create PDF with a Custom Tagged Paragraph | `Document`, `ITaggedContent`, `SetLanguage` | Shows how to generate a tagged PDF and assign a custom tag name to a paragraph element using Aspo... |
| [enable-auto-tagging-pdf-ua](./enable-auto-tagging-pdf-ua.cs) | Enable Automatic Tagging and Save PDF/UA | `Document`, `ITaggedContent`, `PdfFormatConversionOptions` | Demonstrates how to enable Aspose.Pdf's automatic tagging for PDF/UA compliance, set document lan... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a tagged PDF, traverses its logical structure tree, and serializes the hierarchy (type, act... |
| [export-structure-tree-to-xml-report](./export-structure-tree-to-xml-report.cs) | Export PDF Structure Tree to XML and Generate HTML Report vi... | `Document`, `XmlSaveOptions`, `Save` | Demonstrates how to extract a PDF's tagged structure tree to an XML file using Aspose.Pdf and the... |
| [extract-notes-from-tagged-pdf](./extract-notes-from-tagged-pdf.cs) | Extract Notes from Tagged PDF to Text File | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to traverse the structure tree of a tagged PDF, locate all NoteElement objects, ... |
| [extract-pdf-links-to-csv](./extract-pdf-links-to-csv.cs) | Extract PDF Link URLs to CSV | `Document`, `Page`, `Annotation` | Demonstrates how to iterate through a PDF, find link annotations, extract their target URLs, and ... |
| [extract-tagged-pdf-structure-to-json](./extract-tagged-pdf-structure-to-json.cs) | Extract Tagged PDF Structure to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, reads its tagged (accessibility) logical structure, builds a hierarchical POCO repre... |
| [extract-text-from-tagged-pdf](./extract-text-from-tagged-pdf.cs) | Extract Text from Tagged PDF via Structure Tree | `Document`, `ITaggedContent`, `StructureElement` | The example opens a PDF, accesses its tagged content, recursively walks the logical structure tre... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

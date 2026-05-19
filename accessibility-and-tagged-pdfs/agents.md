---
name: Accessibility and Tagged PDFs
description: C# examples for Accessibility and Tagged PDFs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Accessibility and Tagged PDFs

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Accessibility and Tagged PDFs** category.
This folder contains standalone C# examples for Accessibility and Tagged PDFs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Accessibility and Tagged PDFs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (44/44 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (34/44 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (33/44 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/44 files)
- `using Aspose.Pdf.Drawing;` (2/44 files)
- `using Aspose.Pdf.Forms;` (2/44 files)
- `using Aspose.Pdf.Text;` (2/44 files)
- `using System;` (44/44 files)
- `using System.IO;` (43/44 files)
- `using System.Runtime.InteropServices;` (5/44 files)
- `using System.Collections.Generic;` (4/44 files)
- `using System.Text;` (2/44 files)
- `using System.Text.Json;` (2/44 files)
- `using System.Linq;` (1/44 files)
- `using System.Xml;` (1/44 files)
- `using System.Xml.Linq;` (1/44 files)
- `using System.Xml.Xsl;` (1/44 files)

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
| [add-3x4-table-to-tagged-pdf](./add-3x4-table-to-tagged-pdf.cs) | Add a 3x4 Table to a Tagged PDF | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to create a 3‑row by 4‑column table and insert it into the structure tree of a P... |
| [add-actualtext-to-images](./add-actualtext-to-images.cs) | Add ActualText to Images for PDF Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Shows how to attach an ActualText attribute to image elements in a tagged PDF using Aspose.Pdf, p... |
| [add-caption-note-to-figure-in-tagged-pdf](./add-caption-note-to-figure-in-tagged-pdf.cs) | Add Caption Note to Figure in Tagged PDF | `Document`, `ITaggedContent`, `CreateFigureElement` | Demonstrates how to create a Figure element in a tagged PDF and attach a Note element as a captio... |
| [add-custom-tag-to-paragraph](./add-custom-tag-to-paragraph.cs) | Add Custom Tag to Paragraph in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a tagged PDF, add a paragraph element, assign a custom tag name, and s... |
| [add-external-link-with-title-to-tagged-pdf](./add-external-link-with-title-to-tagged-pdf.cs) | Add External Link with Title to a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a LinkElement in a tagged PDF, assign a title attribute for accessibil... |
| [add-note-element-to-paragraph](./add-note-element-to-paragraph.cs) | Add Note Element to Paragraph in Tagged PDF | `Document`, `ITaggedContent`, `CreateParagraphElement` | Shows how to create a note (footnote/endnote) and attach it as a child of a paragraph in a tagged... |
| [add-page-break-element-to-tagged-pdf](./add-page-break-element-to-tagged-pdf.cs) | Add Page Break Element to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to insert a page‑break element into the logical structure of a PDF using Aspose.Pdf's t... |
| [add-paragraph-actualtext-under-toci](./add-paragraph-actualtext-under-toci.cs) | Add Paragraph with ActualText under TOCI in Tagged PDF | `Document`, `ITaggedContent`, `TOCIElement` | Demonstrates how to create a TOCI (Table of Contents Item) element in a tagged PDF, add a paragra... |
| [add-placeholder-textbox-form-field-tagged-pdf](./add-placeholder-textbox-form-field-tagged-pdf.cs) | Add Placeholder TextBox Form Field and Tag in PDF | `Document`, `Page`, `TextBoxField` | Loads an existing PDF, creates a TextBox form field with placeholder text, adds it to the documen... |
| [add-tagged-form-field-to-pdf](./add-tagged-form-field-to-pdf.cs) | Add Tagged Form Field to PDF | `Document`, `TextBoxField`, `Add` | Demonstrates how to create a text box form field, register it in the AcroForm, and associate it w... |
| [apply-custom-row-borders-to-pdf-table](./apply-custom-row-borders-to-pdf-table.cs) | Apply Custom Row Borders to PDF Table | `Document`, `Page`, `Table` | Shows how to create a PDF document with a table and set different border styles for rows based on... |
| [batch-convert-pdfs-to-tagged-pdfs](./batch-convert-pdfs-to-tagged-pdfs.cs) | Batch Convert PDFs to Tagged PDFs with Auto‑Tagging | `AutoTaggingSettings`, `Document`, `PdfFormatConversionOptions` | Demonstrates enabling Aspose.Pdf auto‑tagging, converting each PDF in a folder to a tagged PDF (P... |
| [batch-pdf-ua-validation-dashboard](./batch-pdf-ua-validation-dashboard.cs) | Batch PDF/UA Validation with XML Logs and Compliance Dashboa... | `Document`, `Validate`, `PdfFormat` | Loads all PDF files from a folder, validates each against the PDF/UA accessibility standard using... |
| [batch-pdfua-validation-csv-summary](./batch-pdfua-validation-csv-summary.cs) | Batch PDF/UA Validation with CSV Summary | `Document`, `Validate`, `PdfFormat` | The example validates all PDF files in a folder against PDF/UA 1.0, writes XML logs for each file... |
| [batch-tag-pdfs-with-suffix](./batch-tag-pdfs-with-suffix.cs) | Batch Tag PDFs and Save with Suffix | `Document`, `ITaggedContent`, `EnableAutoTagging` | Shows how to iterate over a folder of PDF files, enable Aspose.Pdf auto‑tagging, add missing acce... |
| [check-pdf-ua-compliance](./check-pdf-ua-compliance.cs) | Check PDF/UA Compliance with Aspose.Pdf | `Document`, `IsPdfUaCompliant` | Loads a PDF document, evaluates its PDF/UA accessibility compliance using Aspose.Pdf, and writes ... |
| [create-internal-page-link-in-tagged-pdf](./create-internal-page-link-in-tagged-pdf.cs) | Create Internal Page Link in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to add a Link structure element with a LocalHyperlink that jumps to a specific page in ... |
| [create-merged-table-header-tagged-pdf](./create-merged-table-header-tagged-pdf.cs) | Create Merged Table Header in Tagged PDF | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to add a table with a merged header row to a PDF, assign the TH role and set Act... |
| [create-tagged-bullet-and-numbered-lists](./create-tagged-bullet-and-numbered-lists.cs) | Create Tagged Bullet and Numbered Lists in PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, adds a bulleted list and a numbered list using the Aspose.Pdf.Tagged API, ... |
| [create-tagged-pdf-heading-language](./create-tagged-pdf-heading-language.cs) | Create Tagged PDF with Heading and Language Attribute | `Document`, `ITaggedContent`, `HeaderElement` | Shows how to create a new PDF, enable tagged content, set the document language and title, add a ... |
| [create-tagged-pdf-table-custom-cell-tags](./create-tagged-pdf-table-custom-cell-tags.cs) | Create Tagged PDF Table with Custom Cell Types and Validate ... | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to add a logical table to a PDF, assign custom data‑type tags to each cell, rend... |
| [enable-auto-tagging-pdf-ua-conversion](./enable-auto-tagging-pdf-ua-conversion.cs) | Enable Auto‑Tagging and Convert PDF to PDF/UA | `Document`, `AutoTaggingSettings`, `PdfFormatConversionOptions` | Demonstrates how to turn on automatic tagging, convert a PDF to the PDF/UA accessibility standard... |
| [export-pdf-structure-tree-to-json](./export-pdf-structure-tree-to-json.cs) | Export PDF Structure Tree to JSON | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to read the tagged content of a PDF, traverse its logical structure elements, an... |
| [export-structure-tree-to-xml-xsl-report](./export-structure-tree-to-xml-xsl-report.cs) | Export PDF Structure Tree to XML and Generate Report via XSL... | `Document`, `XmlSaveOptions`, `Save` | Demonstrates how to export a PDF's tagged structure tree to an XML file using Aspose.Pdf and then... |
| [export-tagged-pdf-structure-to-json](./export-tagged-pdf-structure-to-json.cs) | Export Tagged PDF Structure to JSON | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, accesses its tagged logical structure, recursively builds a plain‑object hierarchy a... |
| [extract-link-urls-to-csv](./extract-link-urls-to-csv.cs) | Extract Link URLs from PDF and Export to CSV | `Document`, `Page`, `Annotation` | Opens a PDF, iterates through its pages and link annotations, retrieves external URLs via GoToURI... |
| [extract-notes-from-tagged-pdf](./extract-notes-from-tagged-pdf.cs) | Extract Notes from Tagged PDF to Text File | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, accesses its tagged structure, finds all Note elements, concatenates their actual te... |
| [extract-text-from-tagged-pdf-structure-tree](./extract-text-from-tagged-pdf-structure-tree.cs) | Extract Text from Tagged PDF Structure Tree | `Document`, `ITaggedContent`, `StructureElement` | Shows how to load a PDF with Aspose.Pdf, access its tagged content, recursively walk the logical ... |
| [find-images-missing-alt-text](./find-images-missing-alt-text.cs) | Find Images Missing Alternative Text in PDF | `Document`, `Page`, `XImage` | Shows how to load a PDF with Aspose.Pdf, iterate through its pages and images, and collect the ID... |
| [iterate-tagged-pdf-structure-elements](./iterate-tagged-pdf-structure-elements.cs) | Iterate Tagged PDF Structure Elements and Log Metadata | `Document`, `ITaggedContent`, `StructureElement` | Loads a PDF, accesses its tagged content, walks the logical structure tree recursively, and print... |
| ... | | | *and 14 more files* |

## Category Statistics
- Total examples: 44

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
- Review code examples in this folder for Accessibility and Tagged PDFs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-19 | Run: `20260519_173406_f33d5d`
<!-- AUTOGENERATED:END -->

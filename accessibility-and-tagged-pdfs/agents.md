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

- `using Aspose.Pdf;` (45/45 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (35/45 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (34/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/45 files)
- `using Aspose.Pdf.Drawing;` (2/45 files)
- `using Aspose.Pdf.Forms;` (2/45 files)
- `using Aspose.Pdf.Annotations;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (43/45 files)
- `using System.Runtime.InteropServices;` (7/45 files)
- `using System.Collections.Generic;` (5/45 files)
- `using System.Linq;` (2/45 files)
- `using System.Text.Json;` (2/45 files)
- `using System.Reflection;` (1/45 files)
- `using System.Text;` (1/45 files)
- `using System.Xml;` (1/45 files)
- `using System.Xml.Linq;` (1/45 files)
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
| [add-actualtext-to-image-structure](./add-actualtext-to-image-structure.cs) | Add Actualtext To Image Structure |  | Add Actualtext To Image Structure |
| [add-custom-tag-paragraph](./add-custom-tag-paragraph.cs) | Add Custom Tag Paragraph |  | Add Custom Tag Paragraph |
| [add-form-field-placeholder](./add-form-field-placeholder.cs) | Add Form Field Placeholder | `Rectangle` | Add Form Field Placeholder |
| [add-missing-language-attributes](./add-missing-language-attributes.cs) | Add Missing Language Attributes |  | Add Missing Language Attributes |
| [add-note-caption-under-figure](./add-note-caption-under-figure.cs) | Add Note Caption Under Figure |  | Add Note Caption Under Figure |
| [add-note-element](./add-note-element.cs) | Add Note Element |  | Add Note Element |
| [add-section-page-break](./add-section-page-break.cs) | Add Section Page Break |  | Add Section Page Break |
| [append-paragraph-under-toci](./append-paragraph-under-toci.cs) | Append Paragraph Under Toci |  | Append Paragraph Under Toci |
| [assign-custom-tags-table-validate](./assign-custom-tags-table-validate.cs) | Assign Custom Tags Table Validate |  | Assign Custom Tags Table Validate |
| [batch-add-missing-tags](./batch-add-missing-tags.cs) | Batch Add Missing Tags |  | Batch Add Missing Tags |
| [batch-convert-untagged-to-tagged](./batch-convert-untagged-to-tagged.cs) | Batch Convert Untagged To Tagged |  | Batch Convert Untagged To Tagged |
| [batch-pdf-validation](./batch-pdf-validation.cs) | Batch Pdf Validation |  | Batch Pdf Validation |
| [batch-validate-pdfs-dashboard](./batch-validate-pdfs-dashboard.cs) | Batch Validate Pdfs Dashboard |  | Batch Validate Pdfs Dashboard |
| [check-pdf-ua-compliance](./check-pdf-ua-compliance.cs) | Check Pdf Ua Compliance |  | Check Pdf Ua Compliance |
| [create-form-field-tagged](./create-form-field-tagged.cs) | Create Form Field Tagged | `Rectangle` | Create Form Field Tagged |
| [create-internal-link-element](./create-internal-link-element.cs) | Create Internal Link Element |  | Create Internal Link Element |
| [create-logical-toc](./create-logical-toc.cs) | Create Logical Toc | `TextFragment` | Create Logical Toc |
| [create-nested-list](./create-nested-list.cs) | Create Nested List |  | Create Nested List |
| [create-pdf-heading-language](./create-pdf-heading-language.cs) | Create Pdf Heading Language |  | Create Pdf Heading Language |
| [create-tagged-table](./create-tagged-table.cs) | Create Tagged Table |  | Create Tagged Table |
| [custom-table-borders](./custom-table-borders.cs) | Custom Table Borders | `BorderInfo`, `TextFragment` | Custom Table Borders |
| [enable-auto-tagging-pdf](./enable-auto-tagging-pdf.cs) | Enable Auto Tagging Pdf |  | Enable Auto Tagging Pdf |
| [export-structure-tree-json](./export-structure-tree-json.cs) | Export Structure Tree Json |  | Export Structure Tree Json |
| [export-structure-xml-xslt](./export-structure-xml-xslt.cs) | Export Structure Xml Xslt | `XmlSaveOptions` | Export Structure Xml Xslt |
| [extract-note-elements](./extract-note-elements.cs) | Extract Note Elements |  | Extract Note Elements |
| [extract-pdf-links-to-csv](./extract-pdf-links-to-csv.cs) | Extract Pdf Links To Csv |  | Extract Pdf Links To Csv |
| [extract-tagged-pdf-text](./extract-tagged-pdf-text.cs) | Extract Tagged Pdf Text |  | Extract Tagged Pdf Text |
| [extract-tagged-structure](./extract-tagged-structure.cs) | Extract Tagged Structure |  | Extract Tagged Structure |
| [insert-external-link-element](./insert-external-link-element.cs) | Insert External Link Element |  | Insert External Link Element |
| [iterate-structure-elements](./iterate-structure-elements.cs) | Iterate Structure Elements |  | Iterate Structure Elements |
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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

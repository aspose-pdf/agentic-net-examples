---
name: accessibility
description: C# examples for accessibility using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - accessibility

> **Accessibility** in PDF using C# / .NET -- **110** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **accessibility** category.
This folder contains standalone C# examples for accessibility operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **accessibility**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (110/110 files) ← category-specific
- `using Aspose.Pdf.Tagged;` (100/110 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (91/110 files) ← category-specific
- `using Aspose.Pdf.Text;` (20/110 files)
- `using Aspose.Pdf.Annotations;` (6/110 files)
- `using Aspose.Pdf.Forms;` (2/110 files)
- `using System;` (110/110 files)
- `using System.IO;` (110/110 files)
- `using System.Collections.Generic;` (5/110 files)
- `using System.Linq;` (3/110 files)
- `using System.Text;` (3/110 files)
- `using System.Text.Json;` (3/110 files)
- `using System.Drawing;` (2/110 files)

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
| [add-accessibility-note-to-tagged-pdf](./add-accessibility-note-to-tagged-pdf.cs) | Add Accessibility Note to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a tagged PDF, creating a NoteElement, setting its text and alternative text,... |
| [add-accessible-paragraph-to-tagged-pdf](./add-accessible-paragraph-to-tagged-pdf.cs) | Add Accessible Paragraph to Existing Tagged PDF | `Document`, `ITaggedContent`, `ParagraphElement` | Demonstrates loading a PDF while preserving its existing tag structure, creating a new paragraph ... |
| [add-accessible-table-of-contents](./add-accessible-table-of-contents.cs) | Add Accessible Table of Contents to PDF | `Document`, `ITaggedContent`, `DivElement` | Shows how to create a visible Table of Contents page, build its logical structure using tagged PD... |
| [add-accessible-textbox-form-field](./add-accessible-textbox-form-field.cs) | Add Accessible Text Box Form Field to a Tagged PDF | `Document`, `ITaggedContent`, `FormElement` | Demonstrates how to create a text box form field, register it in the AcroForm, link it to a /Form... |
| [add-accessible-toc-to-pdf](./add-accessible-toc-to-pdf.cs) | Add Accessible Table of Contents to PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create an accessible Table of Contents by building logical structure elements... |
| [add-accessible-toc-to-pdf__v2](./add-accessible-toc-to-pdf__v2.cs) | Add Accessible Table of Contents to PDF | `Document`, `ITaggedContent`, `DivElement` | Demonstrates how to create a tagged PDF, add a new page with a visible TOC title, build a hierarc... |
| [add-alt-text-to-pdf-images](./add-alt-text-to-pdf-images.cs) | Add Alt Text to PDF Images | `Document`, `Page`, `XImage` | Shows how to load a PDF with Aspose.Pdf, iterate over each page's image resources, set alternativ... |
| [add-alt-text-to-pdf-images__v2](./add-alt-text-to-pdf-images__v2.cs) | Add Alt Text to PDF Images | `Document`, `Page`, `XImage` | Loads a PDF, iterates through each image on every page, sets descriptive alternative text that in... |
| [add-caption-to-image-in-tagged-pdf](./add-caption-to-image-in-tagged-pdf.cs) | Add Caption to Image in Tagged PDF | `Document`, `ITaggedContent`, `FigureElement` | The example loads a PDF, verifies it has a logical structure, finds the first Figure (image) elem... |
| [add-custom-header-to-tagged-pdf-section](./add-custom-header-to-tagged-pdf-section.cs) | Add Custom Header to a Tagged PDF Section | `Document`, `ITaggedContent`, `AutoTaggingSettings` | Demonstrates loading a PDF, enabling tagged content, creating a custom section with a level‑1 hea... |
| [add-custom-tag-to-structure-element](./add-custom-tag-to-structure-element.cs) | Add Custom Tag to Structure Element in Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to load a tagged PDF, create a paragraph structure element, assign a custom tag using S... |
| [add-custom-tagged-section-paragraph](./add-custom-tagged-section-paragraph.cs) | Add Custom Tagged Section and Paragraph to PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a tagged PDF, creating a new Div (section) and a Paragraph with a custom tag... |
| [add-custom-tagged-section-with-language](./add-custom-tagged-section-with-language.cs) | Create Custom Tagged Section with Language in PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads a tagged PDF, creates a custom Div element with a user-defined tag, sets a specific languag... |
| [add-footer-structure-element-to-tagged-pdf](./add-footer-structure-element-to-tagged-pdf.cs) | Add Footer Structure Element to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, creates a paragraph element as a footer, attaches it to the document's tag... |
| [add-header-element-to-tagged-pdf](./add-header-element-to-tagged-pdf.cs) | Add Header Element to a Tagged PDF | `Document`, `ITaggedContent`, `HeaderElement` | Shows how to load a PDF, create a level‑1 header structure element via ITaggedContent, attach it ... |
| [add-header-element-to-tagged-pdf__v2](./add-header-element-to-tagged-pdf__v2.cs) | Add Header Element to Tagged PDF | `Document`, `ITaggedContent`, `HeaderElement` | Shows how to load a tagged PDF, create a HeaderElement, attach it to the structure tree, and save... |
| [add-header-to-tagged-pdf](./add-header-to-tagged-pdf.cs) | Add Header Element to Tagged PDF with Styling | `Document`, `ITaggedContent`, `HeaderElement` | Demonstrates loading a PDF, creating a level‑1 header structure element, applying font size and b... |
| [add-hyperlink-to-tagged-pdf](./add-hyperlink-to-tagged-pdf.cs) | Add Hyperlink to Tagged PDF Structure | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a tagged PDF, creating a LinkElement via ITaggedContent, attaching it to the... |
| [add-image-with-alt-text-to-tagged-pdf](./add-image-with-alt-text-to-tagged-pdf.cs) | Add Image with Alternative Text to Tagged PDF | `Document`, `Page`, `ImageStamp` | Shows how to load a PDF while preserving its existing tags, insert a new image, set alternative t... |
| [add-italic-paragraph-to-tagged-pdf](./add-italic-paragraph-to-tagged-pdf.cs) | Add Italic Paragraph to Tagged PDF Structure | `Document`, `ITaggedContent`, `TaggedContent` | Demonstrates loading a PDF, creating a paragraph structure element, setting its text to italic vi... |
| [add-link-element-to-tagged-pdf-table](./add-link-element-to-tagged-pdf-table.cs) | Add Link Element to Tagged PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Shows how to load a tagged PDF, create a table structure, insert a link structure element inside ... |
| [add-paragraph-element-to-tagged-pdf](./add-paragraph-element-to-tagged-pdf.cs) | Add Paragraph Element to Tagged PDF | `Document`, `ITaggedContent`, `ParagraphElement` | Shows how to load a PDF, create a paragraph structure element via ITaggedContent, attach it to th... |
| [add-section-header-note-to-tagged-pdf](./add-section-header-note-to-tagged-pdf.cs) | Add Section with Header and Note to Tagged PDF | `Document`, `ITaggedContent`, `SetLanguage` | The example loads an existing tagged PDF, creates a new Section element containing a level‑1 Head... |
| [add-section-paragraph-span-to-tagged-pdf](./add-section-paragraph-span-to-tagged-pdf.cs) | Add Section with Paragraph and Span to Tagged PDF | `Document`, `ITaggedContent`, `DivElement` | Loads an existing tagged PDF, creates a new Div (section) containing a paragraph with a span of t... |
| [add-section-with-hyperlink-to-tagged-pdf](./add-section-with-hyperlink-to-tagged-pdf.cs) | Add Section with Hyperlink to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, accesses its tagged content, creates a new section containing a heading an... |
| [add-styled-paragraph-to-tagged-pdf](./add-styled-paragraph-to-tagged-pdf.cs) | Add Styled Paragraph to a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a PDF, creating a paragraph element in the tagged structure, setting its fon... |
| [add-table-border-to-tagged-pdf](./add-table-border-to-tagged-pdf.cs) | Add Border to Table in Tagged PDF | `Document`, `TableAbsorber`, `AbsorbedTable` | Demonstrates loading a tagged PDF, locating the first visual table using TableAbsorber, applying ... |
| [add-table-footer-to-tagged-pdf](./add-table-footer-to-tagged-pdf.cs) | Add Table Footer to a Tagged PDF | `Document`, `ITaggedContent`, `TableElement` | Shows how to load a tagged PDF, locate or create a TableElement, add a table footer (TFoot) with ... |
| [add-table-footer-to-tagged-pdf__v2](./add-table-footer-to-tagged-pdf__v2.cs) | Add Table Footer to Tagged PDF | `Document`, `ITaggedContent`, `SetLanguage` | Loads a tagged PDF, sets language and title for accessibility, creates a table with a footer elem... |
| [add-table-header-to-tagged-pdf](./add-table-header-to-tagged-pdf.cs) | Add Table Header to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a tagged PDF, creating a table element with a header row, attaching it to th... |
| ... | | | *and 80 more files* |

## Category Statistics
- Total examples: 110

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for accessibility patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_013643_ec685e`
<!-- AUTOGENERATED:END -->

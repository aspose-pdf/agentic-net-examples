---
name: documents
description: C# examples for documents using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - documents

> **Documents** in PDF using C# / .NET -- **30** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **documents** category.
This folder contains standalone C# examples for documents operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **documents**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (30/30 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (10/30 files)
- `using Aspose.Pdf.Text;` (6/30 files)
- `using Aspose.Pdf.LogicalStructure;` (5/30 files)
- `using Aspose.Pdf.Tagged;` (5/30 files)
- `using System;` (30/30 files)
- `using System.IO;` (22/30 files)

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
| [add-tagged-span-to-pdf](./add-tagged-span-to-pdf.cs) | Add a Tagged Span to a PDF for Accessibility | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates loading a PDF, enabling tagged content, creating a Span element with accessibility t... |
| [configure-duplex-printing-and-zoom-open-action](./configure-duplex-printing-and-zoom-open-action.cs) | Configure Duplex Printing and Zoom Open Action for a PDF | `Document`, `PrintDuplex`, `GoToAction` | Demonstrates how to load a PDF, set the duplex printing mode to flip on the long edge, define an ... |
| [create-multi-page-pdf-with-text-fragments](./create-multi-page-pdf-with-text-fragments.cs) | Create Multi‑Page PDF with Text Fragments | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add multiple pages, place a TextFragment on each page, and save... |
| [create-pdf-add-textfragment](./create-pdf-add-textfragment.cs) | Create PDF and Add TextFragment | `Document`, `Page`, `TextFragment` | Demonstrates how to create a new PDF document, add a blank page, insert a TextFragment with custo... |
| [create-pdf-centered-window-ltr](./create-pdf-centered-window-ltr.cs) | Create PDF with Centered Viewer Window and LTR Direction | `Document`, `Page`, `CenterWindow` | Demonstrates how to create a PDF, add a blank page, set the viewer window to open centered, speci... |
| [create-pdf-centered-window-title](./create-pdf-centered-window-title.cs) | Create PDF with Centered Window and Document Title | `Document`, `Add`, `CenterWindow` | Demonstrates how to create a new PDF, add a blank page, set the window to open centered and displ... |
| [create-pdf-duplex-long-edge](./create-pdf-duplex-long-edge.cs) | Create PDF with Duplex Long Edge Printing | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add simple text, set the duplex printing mode to flip on the lo... |
| [create-pdf-enable-displaydoctitle](./create-pdf-enable-displaydoctitle.cs) | Create PDF and Enable DisplayDocTitle | `Document`, `Add`, `DisplayDocTitle` | Demonstrates how to create a new PDF, add a blank page, enable the DisplayDocTitle flag so viewer... |
| [create-pdf-set-open-zoom-180](./create-pdf-set-open-zoom-180.cs) | Create PDF and Set Open Action Zoom to 180% | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add a page with text, set the document's OpenAction to a 1.8 (1... |
| [create-rtl-tagged-pdf-with-span](./create-rtl-tagged-pdf-with-span.cs) | Create RTL Tagged PDF with Span Element | `Document`, `Page`, `TextFragment` | Demonstrates how to generate a PDF with right‑to‑left reading direction, add Arabic text, and use... |
| [create-tagged-pdf-with-span-elements](./create-tagged-pdf-with-span-elements.cs) | Create Tagged PDF with Span Elements for Paragraphs | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, add paragraphs, and tag each paragraph with a Span element usin... |
| [create-tagged-pdf-with-zoomed-openaction](./create-tagged-pdf-with-zoomed-openaction.cs) | Create Tagged PDF with Zoomed OpenAction | `Document`, `Page`, `ITaggedContent` | Demonstrates how to generate a PDF, add a tagged Span element, set the document language, and def... |
| [disable-pdf-window-title-and-centering](./disable-pdf-window-title-and-centering.cs) | Disable PDF Window Title and Centering | `Document`, `CenterWindow`, `DisplayDocTitle` | Shows how to load a PDF with Aspose.Pdf, turn off the CenterWindow and DisplayDocTitle options, a... |
| [enable-display-doc-title](./enable-display-doc-title.cs) | Enable DisplayDocTitle and Save PDF | `Document`, `DisplayDocTitle`, `Save` | Loads a PDF with Aspose.Pdf, sets the DisplayDocTitle flag so PDF viewers show the document title... |
| [extract-zoom-factor-from-openaction](./extract-zoom-factor-from-openaction.cs) | Extract Zoom Factor from PDF OpenAction | `Document`, `GoToAction`, `XYZExplicitDestination` | Loads a PDF, checks its OpenAction for a GoToAction, reads the zoom factor from an XYZ explicit d... |
| [set-default-zoom-pdf-open-action](./set-default-zoom-pdf-open-action.cs) | Set Default Zoom for PDF on Open | `Document`, `Add`, `GoToAction` | Demonstrates how to create a PDF, add a page, and set an OpenAction that opens the document at a ... |
| [set-duplex-printing-and-center-window](./set-duplex-printing-and-center-window.cs) | Configure Duplex Printing and Center Window in PDF | `Document`, `PrintDuplex`, `Duplex` | Demonstrates how to load a PDF with Aspose.Pdf, set the duplex printing mode to flip on the long ... |
| [set-pdf-center-window-and-duplex](./set-pdf-center-window-and-duplex.cs) | Set PDF Viewer Center Window and Duplex Printing Mode | `Document`, `PrintDuplex`, `CenterWindow` | Demonstrates how to load a PDF with Aspose.Pdf, enable the CenterWindow option so the viewer open... |
| [set-pdf-centered-window](./set-pdf-centered-window.cs) | Set PDF to Open with Centered Window | `Document`, `CenterWindow`, `Save` | Loads an existing PDF, enables the CenterWindow flag so the document opens with the viewer window... |
| [set-pdf-duplex-long-edge](./set-pdf-duplex-long-edge.cs) | Set PDF Duplex Printing Mode to Long Edge | `Document`, `PrintDuplex`, `Duplex` | The example loads a PDF file, configures its duplex printing mode to flip on the long edge, and s... |
| [set-pdf-fullscreen-mode](./set-pdf-fullscreen-mode.cs) | Set PDF to Open in Full‑Screen Mode | `Document`, `PageMode`, `PageMode` | Demonstrates loading a PDF with Aspose.Pdf, setting the document's PageMode to FullScreen, and sa... |
| [set-pdf-open-zoom-150](./set-pdf-open-zoom-150.cs) | Set PDF Open Zoom to 150% Using XYZExplicitDestination | `Document`, `OpenAction`, `Save` | Shows how to create a PDF, define an XYZ explicit destination with a 1.5 zoom factor, assign it a... |
| [set-pdf-openaction-zoom-2-5](./set-pdf-openaction-zoom-2-5.cs) | Set PDF OpenAction Zoom Level Using GoToAction | `Document`, `XYZExplicitDestination`, `GoToAction` | Demonstrates how to load a PDF, assign an OpenAction with a GoToAction that uses an XYZ explicit ... |
| [set-pdf-openaction-zoom-2x](./set-pdf-openaction-zoom-2x.cs) | Set PDF OpenAction Zoom to 200% Using GoToAction | `Document`, `Pages`, `GoToAction` | Loads a PDF, creates a GoToAction with an XYZExplicitDestination that sets a 2.0 (200%) zoom leve... |
| [set-pdf-openaction-zoom-75](./set-pdf-openaction-zoom-75.cs) | Set PDF OpenAction Zoom to 75% | `Document`, `GoToAction`, `XYZExplicitDestination` | Shows how to load a PDF with Aspose.Pdf, assign a GoToAction as the document's OpenAction with a ... |
| [set-pdf-reading-direction-ltr](./set-pdf-reading-direction-ltr.cs) | Set PDF Reading Direction to Left-to-Right | `Document`, `Save`, `Direction` | Loads a PDF file, changes its reading direction to left‑to‑right, and saves the modified document... |
| [set-pdf-window-centering-rtl](./set-pdf-window-centering-rtl.cs) | Set PDF Window Centering and RTL Reading Direction | `Document`, `CenterWindow`, `Direction` | Demonstrates how to load a PDF, enable CenterWindow, set the document direction to right‑to‑left,... |
| [set-rtl-direction-and-display-title](./set-rtl-direction-and-display-title.cs) | Set PDF Reading Direction to RTL and Display Document Title | `Document`, `Direction`, `Direction` | The example loads an existing PDF, changes its reading direction to right‑to‑left, enables the do... |
| [set-zoom-and-hide-doc-title](./set-zoom-and-hide-doc-title.cs) | Set PDF OpenAction Zoom and Hide Document Title | `Document`, `XYZExplicitDestination`, `GoToAction` | Demonstrates how to load a PDF, set an OpenAction with a 100% zoom using a GoToAction, disable th... |
| [tag-paragraph-with-span](./tag-paragraph-with-span.cs) | Tag a Paragraph with a Span in a PDF | `Document`, `ITaggedContent`, `SetLanguage` | Shows how to load a PDF, use the TaggedContent API to create a paragraph and a span, attach them ... |

## Category Statistics
- Total examples: 30

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for documents patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-25 | Run: `20261125_221056_c7ce71`
<!-- AUTOGENERATED:END -->

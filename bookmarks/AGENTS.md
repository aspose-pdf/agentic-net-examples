---
name: bookmarks
description: C# examples for bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - bookmarks

> **Bookmarks** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **bookmarks** category.
This folder contains standalone C# examples for bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (35/35 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (22/35 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (1/35 files)
- `using Aspose.Pdf.Tagged;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (35/35 files)
- `using System.Collections.Generic;` (4/35 files)
- `using System.Collections;` (1/35 files)
- `using System.Linq;` (1/35 files)
- `using System.Text.Json;` (1/35 files)

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
| [add-chapter-one-bookmark-to-pdf](./add-chapter-one-bookmark-to-pdf.cs) | Add Chapter One Bookmark to PDF | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Demonstrates loading a PDF, creating a top‑level bookmark that points to page 5, and saving the u... |
| [add-child-bookmark-to-pdf-outline](./add-child-bookmark-to-pdf-outline.cs) | Add Child Bookmark to PDF Outline | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Shows how to locate an existing bookmark in a PDF, create a child bookmark that points to a speci... |
| [add-child-bookmark-to-pdf](./add-child-bookmark-to-pdf.cs) | Add Child Bookmark to Existing PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Loads a PDF, finds the "Chapter One" outline item, creates a child bookmark "Section 1.1" that ju... |
| [add-delete-top-level-bookmark](./add-delete-top-level-bookmark.cs) | Add and Delete a Top-Level PDF Bookmark | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Demonstrates loading a PDF, creating a top‑level bookmark on a specific page with the Open flag s... |
| [add-expanded-child-bookmark-under-introduction](./add-expanded-child-bookmark-under-introduction.cs) | Add Expanded Child Bookmark under Introduction | `Document`, `OutlineItemCollection`, `Outlines` | Loads a PDF, finds the "Introduction" bookmark, adds a child bookmark named "Note" with the Open ... |
| [add-figure-bookmark-under-appendix-a](./add-figure-bookmark-under-appendix-a.cs) | Add Figure Bookmark under Appendix A | `Document`, `OutlineCollection`, `OutlineItemCollection` | Loads a PDF, ensures a parent bookmark named "Appendix A" exists, adds a child bookmark "Figure 1... |
| [add-glossary-bookmark-to-pdf](./add-glossary-bookmark-to-pdf.cs) | Add Glossary Bookmark to PDF | `Document`, `Page`, `OutlineItemCollection` | Demonstrates loading a PDF, creating a top‑level bookmark that points to page 45, setting it to o... |
| [add-parent-child-bookmarks-to-pdf](./add-parent-child-bookmarks-to-pdf.cs) | Add Parent-Child Bookmarks to PDF | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Loads a PDF, verifies it has at least 51 pages, creates a parent bookmark "Index" on page 50 and ... |
| [add-top-level-bookmark-export-outline](./add-top-level-bookmark-export-outline.cs) | Add Top-Level Bookmark and Export Outline Hierarchy | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Demonstrates how to add a top‑level bookmark pointing to page 2, set it to open on document load,... |
| [add-top-level-bookmark-to-pdf](./add-top-level-bookmark-to-pdf.cs) | Add Top-Level Bookmark to PDF | `Document`, `OutlineItemCollection`, `FitExplicitDestination` | Loads a PDF, creates a top‑level bookmark named "Introduction" that points to the first page, and... |
| [add-top-level-bookmark-to-pdf__v2](./add-top-level-bookmark-to-pdf__v2.cs) | Add Top-Level Bookmark to PDF | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Loads a PDF, creates a top‑level bookmark named "Chapter Three" that points to page 60, and saves... |
| [add-top-level-bookmark-to-pdf__v3](./add-top-level-bookmark-to-pdf__v3.cs) | Add Top-Level Bookmark to PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Shows how to load a PDF, create a top‑level bookmark that navigates to page 1, and save the modif... |
| [add-top-level-bookmarks-to-pdf](./add-top-level-bookmarks-to-pdf.cs) | Add Top-Level Bookmarks to PDF | `Document`, `OutlineItemCollection`, `GoToAction` | The example loads an existing PDF, creates three top‑level outline items (bookmarks) that navigat... |
| [add-update-top-level-bookmark](./add-update-top-level-bookmark.cs) | Add and Update a Top-Level Bookmark in a PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Demonstrates how to create a top‑level bookmark (outline) on the first page of a PDF, modify its ... |
| [count-pdf-bookmarks](./count-pdf-bookmarks.cs) | Count PDF Bookmarks and Save Total | `Document`, `Outlines`, `OutlineItemCollection` | Loads a PDF, recursively counts all outline bookmarks, and writes the total number to a text file... |
| [count-top-level-bookmarks](./count-top-level-bookmarks.cs) | Count Top-Level Bookmarks in PDF | `Document`, `Outlines`, `OutlineCollection` | Loads a PDF file, retrieves the number of top‑level bookmarks (outlines) and writes that count to... |
| [delete-add-pdf-bookmark](./delete-add-pdf-bookmark.cs) | Delete and Add Top-Level Bookmark in PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Demonstrates how to load a PDF, remove an existing bookmark, create a new top‑level bookmark that... |
| [delete-pdf-bookmark-by-title](./delete-pdf-bookmark-by-title.cs) | Delete a PDF Bookmark by Title | `Document`, `OutlineCollection`, `Delete` | Shows how to load a PDF, remove a bookmark with a specific title using OutlineCollection.Delete, ... |
| [expand-all-bookmarks](./expand-all-bookmarks.cs) | Expand All Bookmarks in a PDF | `Document`, `Save`, `Outlines` | Loads a PDF, recursively sets the Open property of every bookmark (outline) to true, and saves th... |
| [expand-bookmarks-delete-appendix-a](./expand-bookmarks-delete-appendix-a.cs) | Expand All Bookmarks and Delete a Specific Bookmark | `Document`, `OutlineCollection`, `OutlineItemCollection` | The example loads a PDF, expands every bookmark in the outline tree, removes the bookmark titled ... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `Document`, `Outlines`, `OutlineItemCollection` | Loads a PDF, traverses its outline (bookmarks) to collect each title and destination, and writes ... |
| [extract-child-bookmarks](./extract-child-bookmarks.cs) | Extract Child Bookmarks of a Specific Parent | `Document`, `Outlines`, `OutlineCollection` | Loads a PDF, locates the bookmark named "Introduction" and writes the titles of its direct child ... |
| [extract-pdf-bookmark-titles](./extract-pdf-bookmark-titles.cs) | Extract PDF Bookmark Titles to Text File | `Document`, `Outlines`, `OutlineItemCollection` | Loads a PDF, recursively traverses its outline (bookmarks) and writes each bookmark title to a te... |
| [extract-top-level-bookmark-page-numbers](./extract-top-level-bookmark-page-numbers.cs) | Extract Top-Level Bookmark Page Numbers from PDF | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Loads a PDF, reads the page number of each top‑level bookmark (outline) and writes those numbers ... |
| [open-child-bookmarks-under-chapter-one](./open-child-bookmarks-under-chapter-one.cs) | Open Child Bookmarks Under a Specific Bookmark | `Document`, `OutlineItemCollection`, `Outlines` | Loads a PDF, finds the bookmark titled "Chapter One", sets the Open property of all its direct ch... |
| [remove-all-bookmarks-from-pdf__v2](./remove-all-bookmarks-from-pdf__v2.cs) | Remove All Bookmarks from a PDF | `Document`, `Outlines`, `Clear` | Shows how to load a PDF with Aspose.Pdf, clear its outline collection to delete all bookmarks, an... |
| [rename-child-bookmark-pdf](./rename-child-bookmark-pdf.cs) | Rename a Child Bookmark in a PDF | `Document`, `OutlineItemCollection`, `Outlines` | Demonstrates how to load a PDF, locate a top‑level bookmark and its child, rename the child bookm... |
| [replace-bookmark-add-child](./replace-bookmark-add-child.cs) | Replace a Bookmark and Add a Child Bookmark in PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Loads a PDF, removes an existing bookmark, then creates a new child bookmark under a parent outli... |
| [reset-pdf-bookmarks-add-new-bookmark](./reset-pdf-bookmarks-add-new-bookmark.cs) | Reset PDF Bookmarks and Add a New Top-Level Bookmark | `Document`, `OutlineItemCollection`, `FitExplicitDestination` | The example loads a PDF, removes all existing bookmarks (outlines), creates a new top-level bookm... |
| [retrieve-bookmark-destination-page-number](./retrieve-bookmark-destination-page-number.cs) | Retrieve Bookmark Destination Page Number and Save to Text F... | `Document`, `OutlineCollection`, `OutlineItemCollection` | Loads a PDF, searches for the bookmark named "Chapter One", extracts its destination page number ... |
| ... | | | *and 5 more files* |

## Category Statistics
- Total examples: 35

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Annotations.GoToAction`
- `Aspose.Pdf.Annotations.XYZExplicitDestination`
- `Aspose.Pdf.Bookmark`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Outlines`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Facades.PdfBookmarkEditor`
- `Aspose.Pdf.OutlineCollection`
- `Aspose.Pdf.OutlineCollection.Delete`
- `Aspose.Pdf.OutlineItemCollection`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.PageMode`
- `Document.Outlines`
- `Document.Save`
- `OutlineItemCollection.Add`

### Rules
- Load a PDF by creating a new {doc} = new Document({input_pdf});
- Iterate over the bookmark collection via foreach (OutlineItemCollection {outline_item} in {doc}.Outlines) to access each bookmark.
- Read bookmark attributes like {outline_item}.Title (string), {outline_item}.Italic (bool), {outline_item}.Bold (bool) and {outline_item}.Color (Color) without modifying the document.
- Create an OutlineItemCollection with the parent collection {doc}.Outlines, set its Title, Italic, and Bold properties, assign its Action to a new GoToAction({page}), and add it to {doc}.Outlines.
- Obtain the destination page for a bookmark via {doc}.Pages[{int}] (1‑based index) and pass it to the GoToAction constructor.

### Warnings
- The example only traverses top‑level bookmarks; nested bookmarks require recursive traversal of OutlineItemCollection.SubItems.
- The exact type name for the outline collection may be Aspose.Pdf.OutlineCollection, but only OutlineItemCollection is explicitly referenced.
- Aspose.Pdf.Pages collection uses 1‑based indexing; ensure the page number is valid for the document.
- PdfBookmarkEditor belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in future releases; consider using the Document class API for bookmark manipulation.
- CreateBookmarks overwrites existing bookmarks if a bookmark with the same title already exists; ensure uniqueness if required.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for bookmarks patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_035055_b06971`
<!-- AUTOGENERATED:END -->

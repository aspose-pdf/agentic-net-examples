---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-bookmarks** category.
This folder contains standalone C# examples for facades-bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (26/35 files) ← category-specific
- `using Aspose.Pdf.Facades;` (26/35 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (7/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (35/35 files)
- `using System.Collections.Generic;` (8/35 files)
- `using System.Drawing;` (3/35 files)
- `using System.Text.Json;` (3/35 files)
- `using System.Data;` (1/35 files)
- `using System.Text;` (1/35 files)
- `using System.Text.RegularExpressions;` (1/35 files)

## Common Code Pattern

Most files in this category use `PdfBookmarkEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfBookmarkEditor tool = new PdfBookmarkEditor();
tool.BindPdf("input.pdf");
// ... PdfBookmarkEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Color PDF Bookmarks Based on Title | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to load a PDF, extract its bookmarks using the PdfBookmarkEditor facade, and set... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `Document`, `OutlineItemCollection`, `GoToURIAction` | Shows how to create PDF bookmarks that open external web URLs using Aspose.Pdf. |
| [add-image-bookmarks-to-pdf](./add-image-bookmarks-to-pdf.cs) | Add Image Bookmarks to PDF | `Document`, `Page`, `XImage` | The example loads a PDF, iterates through each page's image resources, and creates a top‑level bo... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Create JavaScript Bookmarks in PDF | `Document`, `OutlineItemCollection`, `JavascriptAction` | Shows how to add outline bookmarks with JavaScript actions to an existing PDF document using Aspo... |
| [add-subbookmarks-to-pdf-chapter](./add-subbookmarks-to-pdf-chapter.cs) | Add Child Bookmarks Under an Existing Parent Bookmark | `Document`, `OutlineItemCollection`, `XYZExplicitDestination` | Demonstrates how to locate an existing outline entry in a PDF and add child bookmarks that point ... |
| [add-toc-bookmark-to-pdfs-batch](./add-toc-bookmark-to-pdfs-batch.cs) | Batch Add Table of Contents Bookmark to PDFs | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to loop through all PDF files in a folder and add a standard "Table of Contents" bookma... |
| [add-top-level-blue-bookmark-to-pdf](./add-top-level-blue-bookmark-to-pdf.cs) | Add Top-Level Blue Bookmark to First Page | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Shows how to create a top-level bookmark that points to page 1 and sets its title color to blue u... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Insert Blank Pages at Document Start and Update Bookmarks | `Document`, `Page`, `PdfBookmarkEditor` | Demonstrates how to prepend blank pages to an existing PDF and then shift all bookmark destinatio... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDF Files | `Document`, `PdfBookmarkEditor`, `Bookmark` | Demonstrates how to iterate through a folder of PDFs, determine each document's last page, and ap... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to open password‑protected PDF files, bind them to a PdfBookmarkEditor, delete all exis... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks on Open | `Document`, `OutlineItemCollection`, `Outlines` | Demonstrates loading a PDF with Aspose.Pdf, locating bookmarks by title, setting their Open prope... |
| [convert-bookmark-titles-to-title-case](./convert-bookmark-titles-to-title-case.cs) | Update PDF Bookmark Titles to Title Case | `Document`, `OutlineItemCollection`, `Outlines` | Loads a PDF, walks through its bookmark (outline) hierarchy, and converts each title to title cas... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination | `Document`, `OutlineItemCollection`, `Outlines` | Shows how to add a top‑level bookmark that links to an existing named destination in a PDF using ... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical Bookmarks in a PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to bind a PDF with PdfBookmarkEditor, build a root bookmark with child chapter entries,... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove every bookmark from a PDF document using Aspose.Pdf.Facades.PdfBookmarkEditor... |
| [delete-bookmark-from-pdf](./delete-bookmark-from-pdf.cs) | Delete a Single PDF Bookmark and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to delete a specific bookmark from a PDF using the PdfBookmarkEditor facade, sav... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to remove PDF bookmarks whose titles match a regular expression using the Aspose... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and writ... |
| [export-pdf-bookmarks-to-excel](./export-pdf-bookmarks-to-excel.cs) | Export PDF Bookmarks to CSV for Excel | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract PDF bookmarks using Aspose.Pdf.Facades.PdfBookmarkEditor and write th... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmark Hierarchy to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the bookmark tree from a PDF with Aspose.Pdf.Facades, capture each node's ti... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting PDF bookmarks with PdfBookmarkEditor and writing them to a plain‑text fil... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to bind a PDF document with PdfBookmarkEditor and export its complete bookmark h... |
| [extract-pdf-bookmarks-to-json](./extract-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to Nested JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the hierarchical bookmark structure from a PDF using Aspose.Pdf.Facades and ... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Shows how to read a CSV file, parse each line into title, level, and destination, and add those b... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into PDF | `Document`, `PdfBookmarkEditor`, `Bookmark` | Shows how to read bookmark records (simulated with a DataTable) and add them to an existing PDF u... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into a PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read a JSON file, map its entries to Aspose.Pdf.Facades Bookmark objects, and add th... |
| [import-ofd-bookmarks-to-pdf](./import-ofd-bookmarks-to-pdf.cs) | Import OFD Bookmarks into PDF | `Document`, `OutlineItemCollection`, `GoToAction` | Shows how to load an OFD file, convert its outline hierarchy to PDF outline items, and add them t... |
| [merge-pdf-bookmarks](./merge-pdf-bookmarks.cs) | Merge PDF Bookmarks from an External Document | `Document`, `OutlineCollection`, `OutlineItemCollection` | Shows how to import bookmark outlines from a source PDF and merge them into a target PDF while pr... |
| [remove-duplicate-bookmarks](./remove-duplicate-bookmarks.cs) | Remove Duplicate Bookmarks from PDF Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract the outline (bookmarks) of a PDF, identify and remove entries that ha... |
| [rename-pdf-bookmarks-using-translation-dictionary](./rename-pdf-bookmarks-using-translation-dictionary.cs) | Rename PDF Bookmarks Using a Translation Dictionary | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Demonstrates how to rename PDF bookmarks based on a case‑insensitive translation dictionary using... |
| ... | | | *and 5 more files* |

## Category Statistics
- Total examples: 35

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.Bookmark`
- `Aspose.Pdf.Facades.Bookmark.Action`
- `Aspose.Pdf.Facades.Bookmark.PageNumber`
- `Aspose.Pdf.Facades.Bookmark.Title`
- `Aspose.Pdf.Facades.Bookmarks`
- `Aspose.Pdf.Facades.PdfBookmarkEditor`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.BindPdf`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.DeleteBookmarks(string)`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.ExtractBookmarks`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.ImportBookmarksWithXML`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.Save`
- `Aspose.Pdf.Facades.PdfBookmarkEditor.Save(string)`

### Rules
- Instantiate a PdfBookmarkEditor, then bind the source PDF with BindPdf({input_pdf}) before performing any bookmark operations.
- Export the document's bookmarks to an XML file using ExportBookmarksToXML({string_literal}) after the PDF is bound.
- Call Save({output_pdf}) on the PdfBookmarkEditor to write out the PDF (required if any modifications are made or to finalize the operation).
- Load a PDF with PdfBookmarkEditor.BindPdf({input_pdf}) before performing any bookmark operations.
- Create a bookmark that points to a page using PdfBookmarkEditor.CreateBookmarkOfPage({string_literal}, {int}) where the page number is 1‑based.

### Warnings
- Save() creates a new PDF file even if no bookmark changes were made; it may be unnecessary if only exporting bookmarks.
- PdfBookmarkEditor belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in future releases; consider using the Document class for newer APIs.
- CreateBookmarkOfPage expects the bookmark and page arrays to be of equal length; each entry maps a single page (range support may be limited).
- PdfBookmarkEditor belongs to the Aspose.Pdf.Facades namespace, which may be deprecated in newer versions of Aspose.PDF.
- DeleteBookmarks removes every bookmark; there is no overload for selective deletion.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-bookmarks patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-06 | Run: `20261106_022525_96dfe4`
<!-- AUTOGENERATED:END -->

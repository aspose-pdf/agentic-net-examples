---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **34** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-bookmarks** category.
This folder contains standalone C# examples for facades-bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (29/34 files) ← category-specific
- `using Aspose.Pdf.Facades;` (18/34 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (11/34 files)
- `using Aspose.Pdf.Text;` (1/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (34/34 files)
- `using System.Collections.Generic;` (14/34 files)
- `using System.Text.Json;` (3/34 files)
- `using System.Drawing;` (2/34 files)
- `using System.Collections;` (1/34 files)
- `using System.Data;` (1/34 files)
- `using System.Linq;` (1/34 files)
- `using System.Text.RegularExpressions;` (1/34 files)
- `using System.Xml.Linq;` (1/34 files)

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
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Color PDF Bookmarks by Section Type | `PdfBookmarkEditor`, `BindPdf`, `GetBookmarks` | Shows how to load a PDF, walk through its bookmark hierarchy, and set bookmark colors to red for ... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `Document`, `OutlineItemCollection`, `GoToURIAction` | Shows how to create top‑level PDF bookmarks that open external web URLs using Aspose.Pdf. |
| [add-image-bookmarks-to-pdf](./add-image-bookmarks-to-pdf.cs) | Create Bookmarks for Each Image in a PDF | `Document`, `Page`, `XImage` | Shows how to iterate through a PDF's pages, detect images, and add a bookmark for each image that... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Actions to PDF Bookmarks | `Document`, `OutlineItemCollection`, `JavascriptAction` | Demonstrates how to create top‑level and child bookmarks in a PDF that execute JavaScript code wh... |
| [add-subbookmarks-to-pdf-chapter](./add-subbookmarks-to-pdf-chapter.cs) | Add Child Bookmarks Under a Parent Bookmark | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to locate an existing parent bookmark in a PDF and add child bookmarks beneath it using... |
| [add-toc-bookmark-to-pdfs-batch](./add-toc-bookmark-to-pdfs-batch.cs) | Batch Add Table of Contents Bookmark to PDFs | `Document`, `OutlineItemCollection`, `GoToAction` | Demonstrates iterating over a folder of PDF files, adding a top‑level "Table of Contents" bookmar... |
| [add-top-level-blue-bookmark-to-pdf](./add-top-level-blue-bookmark-to-pdf.cs) | Add Top-Level Blue Bookmark to First Page | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Shows how to create a top-level bookmark that points to the first page of a PDF and set its title... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDFs | `Document`, `Page`, `OutlineItemCollection` | Shows how to iterate through a folder of PDF files, add a top‑level "Reviewed" bookmark that jump... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `Outlines`, `Clear` | Demonstrates how to open password‑protected PDFs, remove all bookmark outlines, and save the clea... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks | `Document`, `OutlineItemCollection`, `Outlines` | Shows how to load a PDF with Aspose.Pdf, locate outline items by title and set their Open propert... |
| [convert-bookmark-titles-to-title-case](./convert-bookmark-titles-to-title-case.cs) | Update PDF Bookmark Titles to Title Case | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to read, modify, and save PDF bookmarks using Aspose.Pdf.Facades, converting eac... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination | `Document`, `GoToAction`, `OutlineItemCollection` | Demonstrates how to add a PDF bookmark that links to an existing named destination using Aspose.P... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Chapter Bookmarks in a PDF | `Document`, `TextAbsorber`, `TextExtractionOptions` | The example loads a PDF, extracts text to locate chapter headings, and adds top‑level bookmarks t... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove every bookmark from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and save... |
| [delete-bookmark-from-pdf](./delete-bookmark-from-pdf.cs) | Delete a Bookmark from PDF using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove a specific bookmark from a PDF with Aspose.Pdf.Facades.PdfBookmarkEditor and ... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to bind a PDF, extract its bookmarks, and remove those whose titles match a regu... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Extract PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to read all bookmarks from a PDF using Aspose.Pdf.Facades and write their title, destin... |
| [export-pdf-bookmarks-to-excel](./export-pdf-bookmarks-to-excel.cs) | Export PDF Bookmarks to CSV for Excel | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract PDF bookmarks using Aspose.Pdf and write them to a CSV file that can ... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmark Hierarchy to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract a PDF's bookmark tree using Aspose.Pdf.Facades and write each bookmark's tit... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `Document`, `OutlineCollection`, `OutlineItemCollection` | Shows how to read a PDF's bookmark hierarchy with Aspose.Pdf and write it to a plain‑text file, u... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to bind a PDF document with PdfBookmarkEditor and export its complete bookmark h... |
| [extract-pdf-bookmarks-to-json](./extract-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to JSON | `Document`, `Outlines`, `OutlineCollection` | Loads a PDF using Aspose.Pdf, extracts its bookmark (outline) hierarchy, converts it into a plain... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read a CSV file and add each line as a PDF bookmark using Aspose.Pdf's PdfBookmarkEd... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into PDF | `Document`, `GoToAction`, `OutlineItemCollection` | Demonstrates loading a PDF, reading bookmark data from a DataTable (simulating a database query),... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON and Add to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read bookmark definitions from a JSON file, map them to Aspose.Pdf Bookmark objects,... |
| [import-ofd-bookmarks-to-pdf](./import-ofd-bookmarks-to-pdf.cs) | Import OFD Bookmarks into a PDF Document | `Document`, `OfdLoadOptions`, `OutlineItemCollection` | Demonstrates loading an OFD file, extracting (simulated) bookmark data, converting each entry to ... |
| [merge-pdf-bookmarks](./merge-pdf-bookmarks.cs) | Merge Bookmarks from One PDF into Another | `Document`, `OutlineItemCollection`, `Outlines` | Demonstrates how to import bookmarks from an external PDF and merge them into an existing PDF whi... |
| [remove-duplicate-bookmarks](./remove-duplicate-bookmarks.cs) | Remove Duplicate Bookmarks from PDF | `Document`, `OutlineItemCollection`, `Outlines` | Demonstrates how to iterate through a PDF's outline collection, identify bookmarks with identical... |
| [rename-pdf-bookmarks-using-translation-dictionary](./rename-pdf-bookmarks-using-translation-dictionary.cs) | Rename PDF Bookmarks Using a Translation Dictionary | `Document`, `OutlineItemCollection`, `Outlines` | Shows how to load a PDF with Aspose.Pdf, recursively walk through its outline (bookmark) items, r... |
| [set-bookmark-zoom-level-150-percent](./set-bookmark-zoom-level-150-percent.cs) | Update Bookmark Zoom Level to 150% Magnification | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to modify existing PDF bookmarks so that each opens its page at 150% zoom using Aspose.... |
| ... | | | *and 4 more files* |

## Category Statistics
- Total examples: 34

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
Updated: 2026-10-26 | Run: `20261026_042003_ed00d8`
<!-- AUTOGENERATED:END -->

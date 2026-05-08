---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-bookmarks** category.
This folder contains standalone C# examples for facades-bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/35 files) ← category-specific
- `using Aspose.Pdf;` (13/35 files)
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (34/35 files)
- `using System.Collections.Generic;` (10/35 files)
- `using System.Drawing;` (4/35 files)
- `using System.Text.Json;` (3/35 files)
- `using System.Linq;` (1/35 files)
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
| [add-bookmarks-verify-count](./add-bookmarks-verify-count.cs) | Add Bookmarks and Verify Count in PDF | `Document`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates adding page bookmarks to a PDF with PdfBookmarkEditor, saving the file, then extract... |
| [add-child-bookmarks-to-pdf](./add-child-bookmarks-to-pdf.cs) | Add Child Bookmarks Under a Parent Bookmark | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to create a parent bookmark (e.g., a chapter) and attach child bookmarks represe... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Demonstrates how to create bookmarks that open external web URLs in an existing PDF using Aspose.... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that executes a JavaScript alert using Aspose.Pdf.Facades. |
| [add-top-level-blue-bookmark](./add-top-level-blue-bookmark.cs) | Add Top-Level Blue Bookmark to First Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to create a new top-level bookmark that points to page 1 of a PDF and set its ti... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `PdfBookmarkEditor`, `ExtractBookmarks` | Demonstrates how to insert blank pages at the beginning of a PDF and shift existing bookmark dest... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate through PDF files in a directory, add a "Reviewed" bookmark on the last page... |
| [batch-add-toc-bookmark-to-pdfs](./batch-add-toc-bookmark-to-pdfs.cs) | Batch Add Table of Contents Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate over all PDF files in a folder and add a top‑level "Table of Contents" bookm... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Demonstrates how to open encrypted PDF files with a password, remove all bookmarks using the PdfB... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to programmatically collapse selected bookmarks in a PDF by setting their Open p... |
| [color-pdf-bookmarks-by-content](./color-pdf-bookmarks-by-content.cs) | Color PDF Bookmarks Based on Content | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to read existing PDF bookmarks, set their title color to red for warnings and green for... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to add a bookmark that references a named destination defined elsewhere in a PDF using ... |
| [create-bookmarks-for-each-image](./create-bookmarks-for-each-image.cs) | Create Bookmarks for Each Image in a PDF | `Document`, `Page`, `XImage` | Shows how to iterate through PDF pages, locate image resources, and add a bookmark for each image... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical PDF Bookmarks with PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to bind a PDF file to PdfBookmarkEditor, build a chapter/section bookmark hierarchy, an... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from a PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove every bookmark from a PDF document using the PdfBookmarkEditor facade ... |
| [delete-bookmark-verify-removal](./delete-bookmark-verify-removal.cs) | Delete a Bookmark and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove a specific bookmark from a PDF using PdfBookmarkEditor and then confirm the d... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to remove PDF bookmarks whose titles match a regular expression using Aspose.Pdf's PdfB... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF using PdfBookmarkEditor and write each bookmark's t... |
| [export-pdf-bookmarks-to-csv__v2](./export-pdf-bookmarks-to-csv__v2.cs) | Export PDF Bookmarks to CSV for Excel | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Loads a PDF, extracts its bookmark hierarchy with PdfBookmarkEditor, and writes each bookmark's t... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting bookmarks from a PDF with Aspose.Pdf.Facades.PdfBookmarkEditor, convertin... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the hierarchical bookmark structure from a PDF using PdfBookmarkEditor and s... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract all bookmarks from a PDF using Aspose.Pdf and write them to a plain‑t... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates loading a PDF with PdfBookmarkEditor and exporting its complete bookmark hierarchy t... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates reading a CSV file, building a hierarchical bookmark structure, and applying it to a... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to retrieve bookmark titles and page numbers (simulated as database records) and add th... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read a JSON file with bookmark definitions, map them to Aspose.Pdf.Facades.Bookmark ... |
| [import-ofd-bookmarks-into-pdf](./import-ofd-bookmarks-into-pdf.cs) | Import OFD Bookmarks into a PDF | `Document`, `OfdLoadOptions`, `PdfBookmarkEditor` | Demonstrates how to load an OFD file, extract its bookmarks as XML, and import them into an exist... |
| [merge-pdf-bookmarks-using-aspose](./merge-pdf-bookmarks-using-aspose.cs) | Merge Bookmarks from One PDF into Another | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to export bookmarks from a source PDF as XML and import them into a target PDF, ... |
| [remove-duplicate-bookmarks-from-pdf](./remove-duplicate-bookmarks-from-pdf.cs) | Remove Duplicate Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract all bookmarks from a PDF, identify and remove duplicates based on tit... |
| [rename-pdf-bookmarks-using-translation-dictionary](./rename-pdf-bookmarks-using-translation-dictionary.cs) | Rename PDF Bookmarks Using Translation Dictionary | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Loads a PDF, iterates over a translation dictionary, renames matching bookmarks with PdfBookmarkE... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-bookmarks patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-05 | Run: `20260505_154553_afd6cb`
<!-- AUTOGENERATED:END -->

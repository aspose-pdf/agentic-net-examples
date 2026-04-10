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
- `using Aspose.Pdf;` (20/35 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (34/35 files)
- `using System.Collections.Generic;` (11/35 files)
- `using System.Drawing;` (3/35 files)
- `using System.Text.Json;` (3/35 files)
- `using System.Linq;` (2/35 files)
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
| [add-bookmarks-for-each-image-in-pdf](./add-bookmarks-for-each-image-in-pdf.cs) | Add Bookmarks for Each Image in a PDF | `Document`, `PdfBookmarkEditor`, `BindPdf` | Demonstrates how to iterate through PDF pages, locate images, and create a bookmark for each imag... |
| [add-child-bookmarks-to-pdf-chapter](./add-child-bookmarks-to-pdf-chapter.cs) | Add Child Bookmarks to a PDF Chapter | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to create a parent bookmark and attach child bookmarks representing subsections within ... |
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Add Colored Bookmarks to PDF Sections | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Demonstrates using Aspose.Pdf.Facades.PdfContentEditor to create PDF bookmarks with red or green ... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to add bookmarks that link to external web URLs in an existing PDF using Aspose.... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that runs JavaScript code when clicked using Aspose.Pdf.Facades. |
| [add-toc-bookmark-to-pdfs-batch](./add-toc-bookmark-to-pdfs-batch.cs) | Add Table of Contents Bookmark to PDFs in Batch | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate through a folder of PDF files and add a top‑level "Table of Contents" bookma... |
| [add-top-level-blue-bookmark-to-first-page](./add-top-level-blue-bookmark-to-first-page.cs) | Add Top-Level Blue Bookmark to First Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to create a top-level bookmark that links to page 1 and set its title color to b... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `Save`, `Insert` | Demonstrates inserting blank pages at the beginning of a PDF and updating existing bookmarks to r... |
| [batch-add-reviewed-bookmark](./batch-add-reviewed-bookmark.cs) | Batch Add "Reviewed" Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates how to iterate over PDF files in a folder and add a "Reviewed" bookmark pointing to ... |
| [batch-remove-bookmarks-from-encrypted-pdfs](./batch-remove-bookmarks-from-encrypted-pdfs.cs) | Batch Remove Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Shows how to open password‑protected PDF files, delete all bookmarks using PdfBookmarkEditor, and... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks | `PdfBookmarkEditor`, `Bookmarks`, `Bookmark` | Demonstrates how to collapse selected bookmarks in a PDF by setting their Open property to false ... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to define a named destination in a PDF and add a bookmark that references it usi... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical PDF Bookmarks with PdfBookmarkEditor | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to bind a PDF to PdfBookmarkEditor and add a root bookmark with child chapter bookmarks... |
| [create-validate-pdf-bookmarks](./create-validate-pdf-bookmarks.cs) | Create and Validate PDF Bookmarks | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to generate a bookmark for each page using PdfBookmarkEditor and then verify the total ... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from a PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove every bookmark from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and save... |
| [delete-bookmark-verify-removal](./delete-bookmark-verify-removal.cs) | Delete a Bookmark and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Shows how to remove a specific bookmark from a PDF using PdfBookmarkEditor and then confirm the d... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `BindPdf`, `ExtractBookmarks`, `DeleteBookmarks` | Shows how to remove PDF bookmarks whose titles match a regular expression using Aspose.Pdf's PdfB... |
| [export-bookmarks-to-excel](./export-bookmarks-to-excel.cs) | Export Bookmarks to Excel Workbook | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting PDF bookmarks with PdfBookmarkEditor, arranging title, level, and destina... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and writ... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to Nested JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting PDF bookmarks with Aspose.Pdf.Facades, converting them into a hierarchica... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting a PDF's bookmark hierarchy with PdfBookmarkEditor, flattening it while pr... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `Document`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract the hierarchical bookmarks from a PDF using Aspose.Pdf and write them... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Shows how to load a PDF with PdfBookmarkEditor and export its full bookmark hierarchy to an XML f... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read a CSV file, build a hierarchical Bookmark structure, and add the bookmarks to a... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into a PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to read a JSON file containing bookmark definitions, convert them to Aspose.Pdf.... |
| [import-bookmarks-merge-pdf](./import-bookmarks-merge-pdf.cs) | Import Bookmarks from One PDF into Another | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates exporting bookmarks from a source PDF to XML and importing them into a target PDF, p... |
| [import-database-records-as-pdf-bookmarks](./import-database-records-as-pdf-bookmarks.cs) | Import Database Records as PDF Bookmarks | `Document`, `Bookmark`, `PdfBookmarkEditor` | Shows how to generate an in‑memory PDF and convert each database record into a PdfBookmark using ... |
| [import-ofd-bookmarks-into-pdf](./import-ofd-bookmarks-into-pdf.cs) | Import OFD Bookmarks into PDF | `Document`, `OfdLoadOptions`, `PdfBookmarkEditor` | Shows how to load an OFD document, extract its bookmarks, and add them to an existing or newly cr... |
| [remove-duplicate-bookmarks](./remove-duplicate-bookmarks.cs) | Remove Duplicate Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract PDF bookmarks, deduplicate them by title and page number, delete the origina... |
| [rename-pdf-bookmarks-using-translation-dictionary](./rename-pdf-bookmarks-using-translation-dictionary.cs) | Rename PDF Bookmarks Using a Translation Dictionary | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Demonstrates how to rename PDF bookmarks by applying a translation map with Aspose.Pdf's PdfBookm... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

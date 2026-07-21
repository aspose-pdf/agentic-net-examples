---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (21/35 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (30/35 files)
- `using System.Collections.Generic;` (10/35 files)
- `using System.Drawing;` (3/35 files)
- `using System.Text.Json;` (3/35 files)
- `using System.Data;` (1/35 files)
- `using System.Globalization;` (1/35 files)
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
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Add Colored Bookmarks to PDF Sections | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create PDF bookmarks with different colors (red for warnings, green for informationa... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `Document`, `PdfBookmarkEditor`, `Bookmark` | Shows how to create PDF bookmarks that open external web URLs using Aspose.Pdf's bookmark editor. |
| [add-subbookmarks-to-pdf-chapter](./add-subbookmarks-to-pdf-chapter.cs) | Add Sub-Bookmarks to a PDF Chapter | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to create a parent bookmark with child bookmarks (sub‑sections) in a PDF using Aspose.P... |
| [add-top-level-blue-bookmark](./add-top-level-blue-bookmark.cs) | Add Top-Level Blue Bookmark to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to create a top-level bookmark that points to the first page of a PDF and set it... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `Insert`, `BindPdf` | Shows how to insert pages at the start of a PDF and shift existing bookmark destinations so they ... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to loop through PDF files in a directory and append a "Reviewed" bookmark on the last p... |
| [batch-add-toc-bookmark-to-pdfs](./batch-add-toc-bookmark-to-pdfs.cs) | Batch Add Table of Contents Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to loop through a directory of PDF files and add a standard "Table of Contents" bookmar... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Shows how to open password‑protected PDF files, remove all bookmarks using PdfBookmarkEditor, and... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks Using Aspose.Pdf | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to bind a PDF, extract its bookmarks, recursively set the Open property to false for bo... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `Document`, `Page`, `FitExplicitDestination` | Shows how to define a named destination in a PDF and add a bookmark that references it using Aspo... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical PDF Bookmarks with PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to bind a PDF to PdfBookmarkEditor, create top‑level chapter bookmarks with child secti... |
| [create-image-bookmarks-in-pdf](./create-image-bookmarks-in-pdf.cs) | Create Image Bookmarks in PDF | `PdfBookmarkEditor`, `Document`, `Page` | Shows how to add a bookmark for each image found in a PDF, linking the bookmark to the page that ... |
| [create-javascript-bookmark](./create-javascript-bookmark.cs) | Create JavaScript Bookmark in PDF | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to add a bookmark that executes JavaScript code when clicked using Aspose.Pdf's PdfCont... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove every bookmark from a PDF document using the PdfBookmarkEditor facade ... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to remove PDF bookmarks whose titles match a regular expression using Aspose.Pdf's PdfB... |
| [delete-pdf-bookmark-verify](./delete-pdf-bookmark-verify.cs) | Delete a PDF Bookmark and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to delete a specific bookmark from a PDF using PdfBookmarkEditor and then confir... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and writ... |
| [export-pdf-bookmarks-to-excel](./export-pdf-bookmarks-to-excel.cs) | Export PDF Bookmarks to Excel | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Extracts all bookmarks from a PDF using PdfBookmarkEditor, traverses the bookmark hierarchy, and ... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | The example opens a PDF file, extracts its hierarchical bookmarks using Aspose.Pdf.Facades.PdfBoo... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `BindPdf`, `ExtractBookmarks`, `Close` | Demonstrates extracting the bookmark hierarchy from a PDF with Aspose.Pdf, flattening it while pr... |
| [export-pdf-bookmarks-to-text-outline](./export-pdf-bookmarks-to-text-outline.cs) | Export PDF Bookmarks to a Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract hierarchical bookmarks from a PDF using Aspose.Pdf and write them to ... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to bind a PDF document with PdfBookmarkEditor and export its complete bookmark h... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `Bookmarks`, `Bookmark`, `PdfBookmarkEditor` | Shows how to read a CSV file, build a hierarchical bookmark tree, and add the bookmarks to an exi... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into a PDF | `Document`, `Save`, `PdfBookmarkEditor` | Demonstrates how to read bookmark data from a database‑like result set and add each entry as a PD... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into PDF | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to read a JSON file with bookmark titles and page numbers, deserialize it, and add thos... |
| [import-ofd-bookmarks-into-pdf](./import-ofd-bookmarks-into-pdf.cs) | Import OFD Bookmarks into PDF | `Document`, `OfdLoadOptions`, `OutlineItemCollection` | Demonstrates loading an OFD file, converting its outline items to Aspose.Pdf.Bookmark objects, an... |
| [merge-bookmarks-into-pdf](./merge-bookmarks-into-pdf.cs) | Merge Bookmarks from One PDF into Another | `PdfBookmarkEditor`, `Document`, `BindPdf` | Demonstrates how to extract the bookmark hierarchy from a source PDF and append it to a target PD... |
| [modify-pdf-bookmarks-using-memorystream](./modify-pdf-bookmarks-using-memorystream.cs) | Modify PDF Bookmarks Using MemoryStream | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Shows how to bind a PDF from a byte array via MemoryStream, rename a specific bookmark, and retur... |
| [remove-duplicate-bookmarks](./remove-duplicate-bookmarks.cs) | Remove Duplicate Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract all bookmarks from a PDF, identify and remove duplicates based on tit... |
| [rename-pdf-bookmarks-using-translation-dictionary](./rename-pdf-bookmarks-using-translation-dictionary.cs) | Rename PDF Bookmarks Using a Translation Dictionary | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Shows how to rename PDF bookmarks by applying a case‑insensitive translation map with Aspose.Pdf.... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

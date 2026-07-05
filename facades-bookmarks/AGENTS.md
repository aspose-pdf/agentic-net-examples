---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **35** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (16/35 files)
- `using Aspose.Pdf.Text;` (4/35 files)
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (32/35 files)
- `using System.Collections.Generic;` (10/35 files)
- `using System.Drawing;` (4/35 files)
- `using System.Text.Json;` (3/35 files)
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
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Add Colored Bookmarks to PDF Sections | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Shows how to create PDF bookmarks with custom colors (red for warnings, green for informational s... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that executes JavaScript code when clicked using Aspose.Pdf Fa... |
| [add-subbookmarks-to-pdf-chapter](./add-subbookmarks-to-pdf-chapter.cs) | Add Sub‑Bookmarks to PDF Chapter | `PdfBookmarkEditor`, `BindPdf`, `Bookmark` | Demonstrates creating a parent bookmark for a chapter, attaching child bookmarks for sections, an... |
| [add-top-level-blue-bookmark-to-first-page](./add-top-level-blue-bookmark-to-first-page.cs) | Add Top-Level Blue Bookmark to First Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to create a top-level bookmark that links to the first page of a PDF and sets it... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `PdfBookmarkEditor`, `BindPdf` | Demonstrates how to insert pages at the beginning of a PDF and shift existing bookmark destinatio... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add 'Reviewed' Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to loop through PDF files in a folder and add a "Reviewed" bookmark on the last page of... |
| [batch-add-toc-bookmark-to-pdfs](./batch-add-toc-bookmark-to-pdfs.cs) | Batch Add Table of Contents Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate over all PDF files in a folder and add a top‑level "Table of Contents" bookm... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Demonstrates how to open password‑protected PDFs, remove all bookmarks using PdfBookmarkEditor, a... |
| [bookmark-toc-consistency-check](./bookmark-toc-consistency-check.cs) | Validate Bookmark Page Numbers Against Table of Contents | `Document`, `PdfBookmarkEditor`, `Bookmarks` | Extracts PDF bookmarks, generates a simple table of contents by reading the first line of each pa... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks to Improve Outline Visibilit... | `BindPdf`, `ExtractBookmarks`, `Title` | Demonstrates how to programmatically set selected PDF bookmarks to a collapsed state using Aspose... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `Document`, `Add`, `PdfContentEditor` | Demonstrates how to add a named destination to a PDF and then create a bookmark that links to tha... |
| [create-hierarchical-bookmarks-pdfbookmarkeditor](./create-hierarchical-bookmarks-pdfbookmarkeditor.cs) | Create Hierarchical Bookmarks with PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to bind a PDF to PdfBookmarkEditor, build a hierarchy of Chapter and Section boo... |
| [create-image-bookmarks-in-pdf](./create-image-bookmarks-in-pdf.cs) | Create Bookmarks for Images in a PDF | `Document`, `Page`, `XImage` | Shows how to locate images on each page of a PDF and add bookmarks that navigate directly to thos... |
| [create-pdf-bookmarks-external-urls](./create-pdf-bookmarks-external-urls.cs) | Create PDF Bookmarks with External URLs | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to add bookmarks that open external web pages in a PDF using Aspose.Pdf.Facades. |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove every bookmark from a PDF document using Aspose.Pdf's PdfBookmarkEdito... |
| [delete-bookmark-and-verify](./delete-bookmark-and-verify.cs) | Delete a Bookmark from PDF and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to remove a specific bookmark from a PDF using Aspose.Pdf.Facades, save the upda... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to load a PDF, extract its bookmarks, and automatically remove those whose title... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the bookmark hierarchy from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor... |
| [export-pdf-bookmarks-to-excel](./export-pdf-bookmarks-to-excel.cs) | Export PDF Bookmarks to Excel Workbook | `Document`, `PdfBookmarkEditor`, `ExtractBookmarks` | Demonstrates how to extract all bookmarks from a PDF, place their title, level, and destination p... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to Hierarchical JSON | `PdfBookmarkEditor`, `ExtractBookmarks`, `Bookmark` | Demonstrates how to extract bookmarks from a PDF using Aspose.Pdf.Facades, convert them into a ne... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `Document`, `PdfBookmarkEditor`, `BindPdf` | Demonstrates how to read the bookmark hierarchy from a PDF using Aspose.Pdf.Facades, convert it t... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract bookmarks from a PDF using Aspose.Pdf.Facades.PdfBookmarkEditor and write th... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to bind a PDF document with PdfBookmarkEditor and export its bookmark hierarchy ... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to retrieve bookmark records (simulated) and add them to an existing PDF using Aspose.P... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into PDF | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Shows how to read hierarchical bookmark data from a JSON file, convert it to Aspose.Pdf.Facades.B... |
| [import-csv-bookmarks-into-pdf](./import-csv-bookmarks-into-pdf.cs) | Import CSV Bookmarks into PDF | `Bookmark`, `PdfBookmarkEditor`, `BindPdf` | The example reads a CSV file containing bookmark title, level, and page number, builds a hierarch... |
| [import-merge-bookmarks-from-external-pdf](./import-merge-bookmarks-from-external-pdf.cs) | Import and Merge Bookmarks from External PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract bookmarks from a source PDF and add them to another PDF while preserving the... |
| [import-ofd-bookmarks-to-pdf](./import-ofd-bookmarks-to-pdf.cs) | Import Bookmarks from OFD into PDF | `Document`, `OfdLoadOptions`, `PdfBookmarkEditor` | Demonstrates how to load an OFD file, extract its bookmarks using PdfBookmarkEditor, and add thos... |
| [modify-pdf-bookmark-title](./modify-pdf-bookmark-title.cs) | Modify PDF Bookmark Title Using MemoryStream | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Shows how to bind a PDF from a byte array with a MemoryStream, change an existing bookmark title,... |
| [remove-duplicate-bookmarks-from-pdf](./remove-duplicate-bookmarks-from-pdf.cs) | Remove Duplicate Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF, filter out duplicates based on title and page numb... |
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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

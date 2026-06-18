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
- `using Aspose.Pdf;` (16/35 files)
- `using Aspose.Pdf.Annotations;` (1/35 files)
- `using Aspose.Pdf.Text;` (1/35 files)
- `using System;` (35/35 files)
- `using System.IO;` (33/35 files)
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
| [add-child-bookmarks-to-pdf-chapter](./add-child-bookmarks-to-pdf-chapter.cs) | Add Child Bookmarks to PDF Chapter | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to create a parent bookmark and attach multiple child bookmarks representing sec... |
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Add Colored Bookmarks to PDF Sections | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to create hierarchical PDF bookmarks with custom colors (red for warnings, green for in... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to use Aspose.Pdf.Facades.PdfContentEditor to create bookmarks that open external web U... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that executes JavaScript code when clicked using Aspose.Pdf.Fa... |
| [add-top-level-bookmark-to-pdf](./add-top-level-bookmark-to-pdf.cs) | Add Top-Level Bookmark to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to use Aspose.Pdf.Facades.PdfBookmarkEditor to create a top‑level bookmark that links t... |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `PdfBookmarkEditor`, `PdfFileEditor`, `Document` | Demonstrates how to extract existing bookmarks, insert new pages at the beginning of a PDF, and r... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate over PDFs in a folder and use Aspose.Pdf.Facades.PdfBookmarkEditor to add a ... |
| [batch-add-toc-bookmark-to-pdfs](./batch-add-toc-bookmark-to-pdfs.cs) | Batch Add Table of Contents Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to iterate over a folder of PDF files and add a top‑level "Table of Contents" bookmark ... |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Shows how to open password‑protected PDF files, remove all bookmarks using PdfBookmarkEditor, and... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks Using Aspose.Pdf | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to set the open state of bookmarks with a given title to collapsed in a PDF docu... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `Document`, `GoToAction`, `NamedDestinations` | Shows how to add a named destination to a PDF and create a bookmark that references it using Aspo... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical Bookmarks in a PDF with PdfBookmarkEdito... | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Demonstrates how to bind a PDF to PdfBookmarkEditor, build a nested bookmark structure using Book... |
| [create-image-bookmarks-in-pdf](./create-image-bookmarks-in-pdf.cs) | Create Bookmarks for Images in a PDF | `Document`, `Page`, `XImage` | Shows how to scan each page of a PDF, locate images, and add a bookmark pointing to the page of e... |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove every bookmark from a PDF document using the Aspose.Pdf.Facades PdfBoo... |
| [delete-bookmark-and-verify-removal](./delete-bookmark-and-verify-removal.cs) | Delete a Bookmark from PDF and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to remove a specific bookmark by its title using PdfBookmarkEditor and confirm the dele... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to remove PDF bookmarks whose titles match a regular expression using Aspose.Pdf's PdfB... |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF, including their hierarchical levels, and write the... |
| [export-pdf-bookmarks-to-csv__v2](./export-pdf-bookmarks-to-csv__v2.cs) | Export PDF Bookmarks to CSV for Excel | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract PDF bookmarks with Aspose.Pdf.Facades and write them to a CSV file that Exce... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmarks to Nested JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract hierarchical bookmarks from a PDF using Aspose.Pdf.Facades and serial... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates extracting the bookmark hierarchy from a PDF using PdfBookmarkEditor and writing eac... |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract all bookmarks from a PDF with Aspose.Pdf and write them to a plain‑text file... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to use Aspose.Pdf.Facades.PdfBookmarkEditor to export the full bookmark hierarch... |
| [import-bookmarks-from-csv](./import-bookmarks-from-csv.cs) | Import Bookmarks from CSV into PDF | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Shows how to read a CSV file, construct a hierarchical bookmark structure, and add it to a PDF do... |
| [import-bookmarks-from-database](./import-bookmarks-from-database.cs) | Import Bookmarks from Database into PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to read bookmark titles and page numbers from a database query result and add them to a... |
| [import-bookmarks-from-json-to-pdf](./import-bookmarks-from-json-to-pdf.cs) | Import Bookmarks from JSON into a PDF | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Demonstrates reading a JSON file, converting its structure to Aspose.Pdf.Facades.Bookmark objects... |
| [import-ofd-bookmarks-into-pdf](./import-ofd-bookmarks-into-pdf.cs) | Import OFD Bookmarks into PDF | `Document`, `OfdLoadOptions`, `PdfBookmarkEditor` | Shows how to load an OFD file, create a bookmark for each page, and add those bookmarks to an exi... |
| [merge-bookmarks-into-pdf](./merge-bookmarks-into-pdf.cs) | Merge Bookmarks from One PDF into Another | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to extract the bookmark hierarchy from a source PDF and append it to a target PDF while... |
| [modify-pdf-bookmark-using-memorystream](./modify-pdf-bookmark-using-memorystream.cs) | Modify PDF Bookmark Using MemoryStream | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Shows how to bind a PDF from a byte array with MemoryStream, change an existing bookmark title, a... |
| [remove-duplicate-bookmarks-from-pdf](./remove-duplicate-bookmarks-from-pdf.cs) | Remove Duplicate Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to extract all bookmarks from a PDF, identify and remove duplicates based on tit... |
| [translate-pdf-bookmarks](./translate-pdf-bookmarks.cs) | Translate PDF Bookmarks Using Aspose.Pdf | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Demonstrates how to rename PDF bookmarks based on a translation dictionary by using Aspose.Pdf's ... |
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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

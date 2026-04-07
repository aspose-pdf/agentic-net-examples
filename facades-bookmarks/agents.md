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

- `using Aspose.Pdf.Facades;` (33/34 files) ← category-specific
- `using Aspose.Pdf;` (25/34 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/34 files)
- `using System;` (34/34 files)
- `using System.IO;` (34/34 files)
- `using System.Collections.Generic;` (9/34 files)
- `using System.Text.Json;` (3/34 files)
- `using System.Drawing;` (1/34 files)
- `using System.Text.RegularExpressions;` (1/34 files)

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
| [add-child-bookmarks](./add-child-bookmarks.cs) | Add Child Bookmarks Under a Parent Bookmark | `Document`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to add sub‑bookmarks under an existing parent bookmark in a PDF using PdfBookmar... |
| [add-external-url-bookmarks](./add-external-url-bookmarks.cs) | Add External URL Bookmarks to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates adding bookmarks that link to external web URLs using Aspose.Pdf.Facades.PdfBookmark... |
| [add-reviewed-bookmark](./add-reviewed-bookmark.cs) | Add "Reviewed" Bookmark to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates how to add a "Reviewed" bookmark pointing to the last page of a PDF using PdfBookmar... |
| [add-toc-bookmark-batch](./add-toc-bookmark-batch.cs) | Add Table of Contents Bookmark to PDFs in a Folder | `PdfBookmarkEditor`, `Bookmark`, `Document` | Processes all PDF files in a folder, adding a "Table of Contents" bookmark to each document and s... |
| [add-top-level-bookmark](./add-top-level-bookmark.cs) | Add Top-Level Bookmark to PDF | `Document`, `PdfBookmarkEditor`, `Bookmark` | Creates a new top‑level bookmark that points to the first page of a PDF and sets its title color ... |
| [adjust-bookmarks-after-inserting-pages](./adjust-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `PdfBookmarkEditor`, `Insert` | Inserts pages at the beginning of a PDF and updates bookmark destinations so they continue to poi... |
| [bookmark-javascript-action](./bookmark-javascript-action.cs) | Create Bookmark with JavaScript Action | `PdfContentEditor`, `CreateBookmarksAction`, `Color` | Adds a bookmark that runs a JavaScript alert when clicked using PdfContentEditor. |
| [bookmark-named-destination](./bookmark-named-destination.cs) | Create Bookmark to Named Destination in PDF | `Document`, `NamedDestination`, `GoToAction` | Demonstrates defining a named destination in a PDF and adding a bookmark that points to it for cr... |
| [collapse-pdf-bookmarks](./collapse-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Demonstrates how to set the open state of selected bookmarks to collapsed using PdfBookmarkEditor. |
| [create-hierarchical-bookmarks](./create-hierarchical-bookmarks.cs) | Create Hierarchical Bookmarks in PDF | `PdfBookmarkEditor`, `BindPdf`, `Bookmark` | Demonstrates binding a PDF to PdfBookmarkEditor and adding a parent bookmark with child chapter b... |
| [create-image-bookmarks](./create-image-bookmarks.cs) | Create Bookmarks for Each Image in PDF | `Document`, `PdfBookmarkEditor`, `Page` | Adds a bookmark for every image in a PDF, linking to the page where the image appears. |
| [delete-all-bookmarks](./delete-all-bookmarks.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove all bookmarks from a PDF using PdfBookmarkEditor and save the modified... |
| [delete-bookmark-verify](./delete-bookmark-verify.cs) | Delete a Specific Bookmark and Verify Removal | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to delete a bookmark by title using PdfBookmarkEditor and verify its removal by ... |
| [delete-bookmarks-by-pattern](./delete-bookmarks-by-pattern.cs) | Delete Bookmarks Matching a Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to remove PDF bookmarks whose titles match a regular expression using PdfBookmar... |
| [delete-bookmarks-encrypted-pdf](./delete-bookmarks-encrypted-pdf.cs) | Delete All Bookmarks from Encrypted PDF | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Demonstrates how to open an encrypted PDF with a password, remove all its bookmarks using PdfBook... |
| [delete-duplicate-bookmarks](./delete-duplicate-bookmarks.cs) | Remove Duplicate Bookmarks from PDF Outline | `PdfFileEditor`, `MergeDuplicateOutlines`, `BindPdf` | Demonstrates how to merge and remove duplicate bookmarks (outline entries) in a PDF using Aspose.... |
| [export-bookmarks-to-a-plain-text-outline-file-pres...](./export-bookmarks-to-a-plain-text-outline-file-preserving-indentation-to-reflect-hierarchy-levels.cs) | Export Bookmarks To A Plain Text Outline File Preserving Ind... | `PdfBookmarkEditor` | Export Bookmarks To A Plain Text Outline File Preserving Indentation To Reflect Hierarchy Levels |
| [export-bookmarks-to-excel](./export-bookmarks-to-excel.cs) | Export PDF Bookmarks to Excel Workbook | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Extracts PDF bookmarks and writes their title, hierarchical level, and destination page to an Exc... |
| [export-pdf-bookmarks-xml](./export-pdf-bookmarks-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Exports all bookmarks from a PDF file to an XML file using Aspose.Pdf.Facades.PdfBookmarkEditor. |
| [export-pdf-bookmarks](./export-pdf-bookmarks.cs) | Export PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Extracts the bookmark hierarchy from a PDF and writes it to a JSON file with title, level, and pa... |
| [extract-bookmarks-verify-toc](./extract-bookmarks-verify-toc.cs) | Extract Bookmarks and Verify Table of Contents Consistency | `PdfBookmarkEditor`, `ExtractBookmarks`, `Bookmark` | Extracts PDF bookmarks, compares their page numbers with an expected table of contents, and repor... |
| [extract-pdf-bookmarks-csv](./extract-pdf-bookmarks-csv.cs) | Extract PDF Bookmarks to CSV | `PdfBookmarkEditor`, `Bookmark`, `Bookmarks` | Reads all bookmarks from a PDF, captures their title, destination (page or explicit destination),... |
| [extract-pdf-bookmarks-json](./extract-pdf-bookmarks-json.cs) | Extract PDF Bookmarks to Nested JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Extracts all bookmarks from a PDF using PdfBookmarkEditor and builds a nested JSON structure that... |
| [import-bookmarks-csv](./import-bookmarks-csv.cs) | Import Bookmarks from CSV into PDF | `Document`, `PdfBookmarkEditor`, `Bookmarks` | Reads a CSV file containing bookmark title, level, and page number, builds a hierarchical bookmar... |
| [import-bookmarks-from-json](./import-bookmarks-from-json.cs) | Import Bookmarks from JSON into PDF | `Document`, `PdfBookmarkEditor`, `CreateBookmarks` | Demonstrates reading bookmark definitions from a JSON file, converting them to Aspose.Pdf.Facades... |
| [import-bookmarks-merge](./import-bookmarks-merge.cs) | Import Bookmarks from One PDF into Another Preserving Order | `Document`, `ExportBookmarksToXML`, `ImportBookmarksWithXML` | Demonstrates how to export bookmarks from a source PDF to XML and import them into a target PDF, ... |
| [import-ofd-bookmarks](./import-ofd-bookmarks.cs) | Import Bookmarks from OFD to PDF | `Document`, `OfdLoadOptions`, `PdfBookmarkEditor` | Loads an OFD file, converts its bookmarks into PDF bookmarks using PdfBookmarkEditor, and saves t... |
| [modify-pdf-bookmarks](./modify-pdf-bookmarks.cs) | Modify PDF Bookmarks Using MemoryStream | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Demonstrates loading a PDF from a MemoryStream, changing bookmark titles with PdfBookmarkEditor, ... |
| [rename-pdf-bookmarks](./rename-pdf-bookmarks.cs) | Rename PDF Bookmarks Using a Translation Dictionary | `PdfBookmarkEditor`, `BindPdf`, `ModifyBookmarks` | Demonstrates how to rename existing PDF bookmarks based on a translation dictionary using PdfBook... |
| [set-bookmark-colors](./set-bookmark-colors.cs) | Set Bookmark Colors Based on Document Sections | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Demonstrates how to add bookmarks with different title colors (red for warnings, green for inform... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-bookmarks patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->

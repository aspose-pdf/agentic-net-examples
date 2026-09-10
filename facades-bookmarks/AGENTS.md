---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **48** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-bookmarks** category.
This folder contains standalone C# examples for facades-bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (35/48 files) ← category-specific
- `using Aspose.Pdf;` (16/48 files)
- `using System;` (35/48 files)
- `using System.IO;` (34/48 files)
- `using System.Collections.Generic;` (12/48 files)
- `using System.Drawing;` (4/48 files)
- `using System.Text.Json;` (3/48 files)
- `using System.Text.RegularExpressions;` (1/48 files)

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
| [add-colored-bookmarks-to-pdf](./add-colored-bookmarks-to-pdf.cs) | Add Colored Bookmarks to PDF Sections | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to create PDF bookmarks and set their title color (red for warnings, green for informat... |
| [add-external-url-bookmarks-to-pdf](./add-external-url-bookmarks-to-pdf.cs) | Add External URL Bookmarks to PDF | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to create PDF bookmarks that open external web addresses using Aspose.Pdf Facades. |
| [add-image-bookmarks-to-pdf](./add-image-bookmarks-to-pdf.cs) | Add Image Bookmarks to PDF | `Document`, `Page`, `XImage` | Scans a PDF for images, records each image's page number, and creates a bookmark that links direc... |
| [add-javascript-bookmark-to-pdf](./add-javascript-bookmark-to-pdf.cs) | Add JavaScript Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that runs JavaScript code when clicked using the Aspose.Pdf Fa... |
| [add-subbookmarks-to-pdf-chapter](./add-subbookmarks-to-pdf-chapter.cs) | Add Subbookmarks to a PDF Chapter | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to create a parent bookmark and attach child bookmarks to represent sections within a c... |
| [add-toc-bookmark-to-pdfs-batch](./add-toc-bookmark-to-pdfs-batch.cs) | Add Table of Contents Bookmark to PDFs in Batch | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to process all PDF files in a directory and add a top‑level "Table of Contents" bookmar... |
| [add-top-level-blue-bookmark-to-pdf](./add-top-level-blue-bookmark-to-pdf.cs) | Add Top-Level Blue Bookmark to PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to create a top‑level bookmark that points to the first page of a PDF and set it... |
| [add-top-level-blue-bookmark](./add-top-level-blue-bookmark.cs) | Add top level blue bookmark |  | Add top level blue bookmark |
| [adjust-pdf-bookmarks-after-inserting-pages](./adjust-pdf-bookmarks-after-inserting-pages.cs) | Adjust PDF Bookmarks After Inserting Pages | `Document`, `PdfBookmarkEditor`, `ExtractBookmarks` | Shows how to extract existing bookmarks, insert blank pages at the beginning of a PDF, and recrea... |
| [batch-add-reviewed-bookmark-to-pdfs](./batch-add-reviewed-bookmark-to-pdfs.cs) | Batch Add "Reviewed" Bookmark to PDFs | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to loop through PDF files in a directory, use PdfBookmarkEditor to create a "Reviewed" ... |
| [batch-add-toc-bookmark-to-pdfs](./batch-add-toc-bookmark-to-pdfs.cs) | Batch add toc bookmark to pdfs |  | Batch add toc bookmark to pdfs |
| [batch-delete-bookmarks-from-encrypted-pdfs](./batch-delete-bookmarks-from-encrypted-pdfs.cs) | Batch Delete Bookmarks from Encrypted PDFs | `Document`, `PdfBookmarkEditor`, `DeleteBookmarks` | Shows how to open password‑protected PDF files, remove all bookmarks using PdfBookmarkEditor, and... |
| [collapse-specific-pdf-bookmarks](./collapse-specific-pdf-bookmarks.cs) | Collapse Specific PDF Bookmarks Using Aspose.Pdf | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to set selected PDF bookmarks to a collapsed (closed) state by extracting, modif... |
| [convert-bookmark-titles-to-title-case](./convert-bookmark-titles-to-title-case.cs) | Convert PDF Bookmark Titles to Title Case with Proper Noun P... | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to read PDF bookmarks, convert each title to title case while keeping predefined... |
| [create-bookmark-to-named-destination](./create-bookmark-to-named-destination.cs) | Create Bookmark to Named Destination in PDF | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to add a bookmark that points to an existing named destination within a PDF using Aspos... |
| [create-hierarchical-pdf-bookmarks](./create-hierarchical-pdf-bookmarks.cs) | Create Hierarchical Bookmarks in a PDF | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to bind an existing PDF with PdfBookmarkEditor and add a multi‑level bookmark hi... |
| [create-image-bookmarks-in-pdf](./create-image-bookmarks-in-pdf.cs) | Create image bookmarks in pdf |  | Create image bookmarks in pdf |
| [create-javascript-bookmark](./create-javascript-bookmark.cs) | Create javascript bookmark |  | Create javascript bookmark |
| [delete-all-bookmarks-from-pdf](./delete-all-bookmarks-from-pdf.cs) | Delete All Bookmarks from PDF | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates how to remove every bookmark from a PDF document using Aspose.Pdf's PdfBookmarkEdito... |
| [delete-bookmark-from-pdf](./delete-bookmark-from-pdf.cs) | Delete Bookmark from PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates binding a PDF to PdfBookmarkEditor, listing existing bookmarks, deleting a specific ... |
| [delete-bookmarks-matching-regex](./delete-bookmarks-matching-regex.cs) | Delete Bookmarks Matching a Regex Pattern | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Demonstrates how to remove PDF bookmarks whose titles match a specified regular expression using ... |
| [delete-pdf-bookmark-verify](./delete-pdf-bookmark-verify.cs) | Delete pdf bookmark verify |  | Delete pdf bookmark verify |
| [export-pdf-bookmarks-to-csv](./export-pdf-bookmarks-to-csv.cs) | Export PDF Bookmarks to CSV | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Opens a PDF, extracts all bookmarks recursively using PdfBookmarkEditor, and writes each bookmark... |
| [export-pdf-bookmarks-to-excel](./export-pdf-bookmarks-to-excel.cs) | Export PDF Bookmarks to Excel | `PdfBookmarkEditor`, `ExcelSaveOptions`, `Document` | Shows how to extract all bookmarks from a PDF using PdfBookmarkEditor, place them in a table, and... |
| [export-pdf-bookmarks-to-json](./export-pdf-bookmarks-to-json.cs) | Export PDF Bookmark Hierarchy to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the bookmark tree from a PDF using PdfBookmarkEditor and write each bookmark... |
| [export-pdf-bookmarks-to-json__v2](./export-pdf-bookmarks-to-json__v2.cs) | Export pdf bookmarks to json__v2 |  | Export pdf bookmarks to json__v2 |
| [export-pdf-bookmarks-to-text-outline](./export-pdf-bookmarks-to-text-outline.cs) | Export pdf bookmarks to text outline |  | Export pdf bookmarks to text outline |
| [export-pdf-bookmarks-to-text](./export-pdf-bookmarks-to-text.cs) | Export PDF Bookmarks to Text Outline | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to extract the hierarchical bookmarks from a PDF using Aspose.Pdf.Facades.PdfBookmarkEd... |
| [export-pdf-bookmarks-to-xml](./export-pdf-bookmarks-to-xml.cs) | Export PDF Bookmarks to XML | `PdfBookmarkEditor`, `BindPdf`, `ExportBookmarksToXML` | Demonstrates how to bind a PDF with Aspose.Pdf.Facades.PdfBookmarkEditor and export its complete ... |
| [extract-pdf-bookmarks-to-json](./extract-pdf-bookmarks-to-json.cs) | Extract PDF Bookmarks to JSON | `PdfBookmarkEditor`, `BindPdf`, `ExtractBookmarks` | Shows how to use Aspose.Pdf.Facades.PdfBookmarkEditor to extract PDF bookmarks, build a hierarchi... |
| ... | | | *and 18 more files* |

## Category Statistics
- Total examples: 48

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

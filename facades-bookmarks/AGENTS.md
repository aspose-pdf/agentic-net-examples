---
name: facades-bookmarks
description: C# examples for facades-bookmarks using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-bookmarks

> **Facades bookmarks** in PDF using C# / .NET -- **177** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-bookmarks** category.
This folder contains standalone C# examples for facades-bookmarks operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-bookmarks**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (163/177 files) ← category-specific
- `using Aspose.Pdf;` (98/177 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (13/177 files)
- `using Aspose.Pdf.Text;` (1/177 files)
- `using System;` (177/177 files)
- `using System.IO;` (176/177 files)
- `using System.Drawing;` (32/177 files)
- `using System.Collections.Generic;` (9/177 files)
- `using System.Xml;` (4/177 files)
- `using System.Xml.Linq;` (4/177 files)
- `using System.Text.Json;` (3/177 files)
- `using System.Linq;` (2/177 files)
- `using System.Data;` (1/177 files)
- `using System.Text;` (1/177 files)
- `using System.Text.RegularExpressions;` (1/177 files)

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
| [add-black-page-bookmarks-to-pdf](./add-black-page-bookmarks-to-pdf.cs) | Add Black Page Bookmarks to PDF with PdfBookmarkEditor | `PdfBookmarkEditor`, `PdfFileInfo`, `Bookmark` | Demonstrates how to load a PDF, create a black‑colored top‑level bookmark for each page, and save... |
| [add-blue-bold-bookmarks-to-all-pdf-pages](./add-blue-bold-bookmarks-to-all-pdf-pages.cs) | Add Blue Bold Bookmarks to All PDF Pages | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Loads a PDF, iterates through each page, creates a blue, bold bookmark for every page using PdfBo... |
| [add-blue-bold-bookmarks-to-pdf-pages](./add-blue-bold-bookmarks-to-pdf-pages.cs) | Add Blue Bold Bookmarks to Each PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to load a PDF with PdfBookmarkEditor, create a blue, bold top-level bookmark for... |
| [add-bold-bookmarks-to-all-pdf-pages](./add-bold-bookmarks-to-all-pdf-pages.cs) | Add Bold Bookmarks to All PDF Pages | `PdfBookmarkEditor`, `BindPdf`, `DeleteBookmarks` | Demonstrates loading a PDF with PdfBookmarkEditor, clearing existing bookmarks, creating a bold‑s... |
| [add-bold-bookmarks-to-all-pdf-pages__v2](./add-bold-bookmarks-to-all-pdf-pages__v2.cs) | Add Bold Bookmarks to All PDF Pages | `Document`, `PdfBookmarkEditor`, `Bookmark` | Demonstrates loading a PDF, binding it to PdfBookmarkEditor, creating a bold bookmark for each pa... |
| [add-bold-italic-bookmarks-to-all-pdf-pages](./add-bold-italic-bookmarks-to-all-pdf-pages.cs) | Add Bold and Italic Bookmarks to All PDF Pages | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates how to bind a PDF with PdfBookmarkEditor, generate a bookmark for each page using bo... |
| [add-bold-italic-bookmarks-to-all-pdf-pages__v2](./add-bold-italic-bookmarks-to-all-pdf-pages__v2.cs) | Add Bold and Italic Bookmarks to All PDF Pages | `Document`, `Bookmark`, `PdfBookmarkEditor` | Demonstrates using PdfBookmarkEditor to bind a PDF, create a bold and italic bookmark for each pa... |
| [add-bookmark-to-last-page](./add-bookmark-to-last-page.cs) | Add Bookmark to Last Page Using PdfBookmarkEditor | `Document`, `PdfBookmarkEditor`, `BindPdf` | Demonstrates loading a PDF, obtaining its page count, and adding a bookmark titled "End" on the f... |
| [add-bookmark-to-last-page__v2](./add-bookmark-to-last-page__v2.cs) | Add Bookmark to Last Page of PDF | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to load a PDF, determine its last page, create a "References" bookmark on that page usi... |
| [add-bookmark-to-last-page__v3](./add-bookmark-to-last-page__v3.cs) | Add Bookmark to Last Page of PDF | `Document`, `PdfBookmarkEditor`, `BindPdf` | Shows how to load a PDF, find its last page, create a bookmark named "Index" that points to that ... |
| [add-bookmark-to-pdf-page__v2](./add-bookmark-to-pdf-page__v2.cs) | Add Bookmark to PDF Page Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a bookmark titled "Details" on page 9... |
| [add-bookmark-to-pdf-page__v3](./add-bookmark-to-pdf-page__v3.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading an existing PDF, creating a top‑level bookmark titled "Editor Note" on page ... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor](./add-bookmark-to-pdf-using-pdfbookmarkeditor.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF, adding a bookmark titled "Chapter 1" on page 5, and saving the update... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v10](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v10.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Shows how to load a PDF with PdfBookmarkEditor, create a bookmark titled "Acknowledgements" on pa... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v11](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v11.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a bookmark titled "Overview" on page ... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v12](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v12.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, adding a bookmark titled "Analysis" on page 14... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v13](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v13.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to load a PDF, bind it to PdfBookmarkEditor, create a bookmark titled "Editor Note" on ... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v14](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v14.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to bind an existing PDF, create a bookmark titled "Editor Reference" on page 13, and sa... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v15](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v15.cs) | Add a Bookmark to a PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, adding a "Foreword" bookmark on page 2, and sa... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v16](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v16.cs) | Add a Bookmark to a PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a top‑level bookmark titled "Postscri... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v17](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v17.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a bookmark titled "Dedication" on the... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v18](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v18.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a bookmark titled "Introduction" on p... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v19](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v19.cs) | Add a Bookmark to a PDF using PdfBookmarkEditor | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a "Conclusion" bookmark on page 15, a... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v2](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v2.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading a PDF with PdfBookmarkEditor, adding a bookmark titled "Appendix" on page 10... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v20](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v20.cs) | Add a Bookmark to a PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates loading a PDF with PdfBookmarkEditor, preserving existing bookmarks, adding a new bo... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v3](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v3.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to load an existing PDF, create a "Cover" bookmark on the first page with PdfBookmarkEd... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v4](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v4.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarks` | Demonstrates loading a PDF with PdfBookmarkEditor, creating a "Summary" bookmark on page 8, and s... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v5](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v5.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading an existing PDF with PdfBookmarkEditor, creating a bookmark titled "Details"... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v6](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v6.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Demonstrates loading an existing PDF, creating a bookmark titled "Editor Bookmark" on page 7, and... |
| [add-bookmark-to-pdf-using-pdfbookmarkeditor__v7](./add-bookmark-to-pdf-using-pdfbookmarkeditor__v7.cs) | Add Bookmark to PDF Using PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to load a PDF, bind it with PdfBookmarkEditor, create a bookmark titled "Editor Section... |
| ... | | | *and 147 more files* |

## Category Statistics
- Total examples: 177

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
Updated: 2026-11-27 | Run: `20261127_044947_4ffd01`
<!-- AUTOGENERATED:END -->

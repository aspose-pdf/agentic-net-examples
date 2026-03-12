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

- `using Aspose.Pdf.Facades;` (22/23 files) ← category-specific
- `using Aspose.Pdf;` (10/23 files)
- `using Aspose.Pdf.Devices;` (2/23 files)
- `using Aspose.Pdf.Printing;` (1/23 files)
- `using System;` (23/23 files)
- `using System.IO;` (22/23 files)
- `using System.Collections.Generic;` (1/23 files)

## Common Code Pattern

Most files in this category use `PdfBookmarkEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfBookmarkEditor tool = new PdfBookmarkEditor();
tool.BindPdf("input.pdf");
// ... PdfBookmarkEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [add-a-bookmark-pointing-to-a-specified-page-in-a-pdf-documen...](./add-a-bookmark-pointing-to-a-specified-page-in-a-pdf-document-enabling-quick-navigation.cs) | `PdfBookmarkEditor` | Add a bookmark pointing to a specified page in a pdf document enabling quick ... |
| [convert-data-between-various-formats-by-processing-a-pdf-fil...](./convert-data-between-various-formats-by-processing-a-pdf-file-as-both-the-source-and-the-destination.cs) | `PngDevice` | Convert data between various formats by processing a pdf file as both the sou... |
| [create-modify-retrieve-and-delete-pdf-bookmarks-programmatic...](./create-modify-retrieve-and-delete-pdf-bookmarks-programmatically-to-navigate-and-organize-documen.cs) | `PdfBookmarkEditor` | Create modify retrieve and delete pdf bookmarks programmatically to navigate ... |
| [export-bookmarks-from-a-pdf-to-an-xml-file-while-retaining-t...](./export-bookmarks-from-a-pdf-to-an-xml-file-while-retaining-the-source-pdf-unchanged-as-output.cs) | `PdfBookmarkEditor` | Export bookmarks from a pdf to an xml file while retaining the source pdf unc... |
| [extract-the-bookmark-hierarchy-from-an-existing-pdf-document...](./extract-the-bookmark-hierarchy-from-an-existing-pdf-document-and-programmatically-access-it-for-furt.cs) | `PdfBookmarkEditor` | Extract the bookmark hierarchy from an existing pdf document and programmatic... |
| [generate-a-bookmark-for-every-page-in-a-pdf-document-to-faci...](./generate-a-bookmark-for-every-page-in-a-pdf-document-to-facilitate-quick-navigation.cs) | `PdfBookmarkEditor` | Generate a bookmark for every page in a pdf document to facilitate quick navi... |
| [generate-bookmarks-for-a-specified-page-range-within-a-pdf-d...](./generate-bookmarks-for-a-specified-page-range-within-a-pdf-document-preserving-the-navigation-hiera.cs) | `PdfBookmarkEditor` | Generate bookmarks for a specified page range within a pdf document preservin... |
| [generate-bookmarks-for-each-pdf-page-assigning-custom-proper...](./generate-bookmarks-for-each-pdf-page-assigning-custom-properties-such-as-title-color-and-navigati.cs) | `PdfBookmarkEditor` | Generate bookmarks for each pdf page assigning custom properties such as titl... |
| [generate-hierarchical-bookmarks-within-a-pdf-document-to-ena...](./generate-hierarchical-bookmarks-within-a-pdf-document-to-enable-quick-navigation-of-its-sections.cs) | `PdfBookmarkEditor` | Generate hierarchical bookmarks within a pdf document to enable quick navigat... |
| [implement-functions-to-retrieve-modify-and-delete-bookmarks-...](./implement-functions-to-retrieve-modify-and-delete-bookmarks-within-a-pdf-document-programmatically.cs) | `PdfBookmarkEditor` | Implement functions to retrieve modify and delete bookmarks within a pdf docu... |
| [import-bookmarks-from-a-pdf-document-and-export-them-to-anot...](./import-bookmarks-from-a-pdf-document-and-export-them-to-another-pdf-while-preserving-hierarchy.cs) | `PdfBookmarkEditor` | Import bookmarks from a pdf document and export them to another pdf while pre... |
| [import-bookmarks-from-xml-into-an-existing-pdf-file-programm...](./import-bookmarks-from-xml-into-an-existing-pdf-file-programmatically-using-the-pdf-api.cs) | `PdfBookmarkEditor` | Import bookmarks from xml into an existing pdf file programmatically using th... |
| [insert-a-child-bookmark-into-an-existing-pdf-document-while-...](./insert-a-child-bookmark-into-an-existing-pdf-document-while-maintaining-the-hierarchical-bookmark-st.cs) | `PdfBookmarkEditor` | Insert a child bookmark into an existing pdf document while maintaining the h... |
| [insert-a-new-bookmark-into-an-existing-pdf-document-to-enabl...](./insert-a-new-bookmark-into-an-existing-pdf-document-to-enable-navigation-to-a-specific-page.cs) | `PdfBookmarkEditor` | Insert a new bookmark into an existing pdf document to enable navigation to a... |
| [modify-the-properties-of-an-existing-bookmark-within-a-pdf-d...](./modify-the-properties-of-an-existing-bookmark-within-a-pdf-document-using-the-api.cs) | `PdfBookmarkEditor` | Modify the properties of an existing bookmark within a pdf document using the... |
| [remove-a-specified-bookmark-from-a-pdf-document-while-mainta...](./remove-a-specified-bookmark-from-a-pdf-document-while-maintaining-the-integrity-of-the-remaining-con.cs) | `PdfBookmarkEditor` | Remove a specified bookmark from a pdf document while maintaining the integri... |
| [remove-all-existing-bookmarks-from-a-pdf-document-while-pres...](./remove-all-existing-bookmarks-from-a-pdf-document-while-preserving-the-remaining-content-and-file-st.cs) | `PdfBookmarkEditor` | Remove all existing bookmarks from a pdf document while preserving the remain... |
| [retrieve-all-bookmarks-from-a-pdf-document-and-return-them-i...](./retrieve-all-bookmarks-from-a-pdf-document-and-return-them-in-a-structured-collection.cs) | `PdfBookmarkEditor` | Retrieve all bookmarks from a pdf document and return them in a structured co... |
| [retrieve-all-bookmarks-from-a-pdf-document-and-return-them-i...](./retrieve-all-bookmarks-from-a-pdf-document-and-return-them-in-a-structured-format.cs) | `PdfBookmarkEditor` | Retrieve all bookmarks from a pdf document and return them in a structured fo... |
| [retrieve-the-bookmark-hierarchy-from-a-pdf-document-s-facade...](./retrieve-the-bookmark-hierarchy-from-a-pdf-document-s-facades-for-programmatic-navigation-and-proces.cs) | `PdfBookmarkEditor` | Retrieve the bookmark hierarchy from a pdf document s facades for programmati... |
| [send-pdf-documents-to-a-printer-programmatically-preserving-...](./send-pdf-documents-to-a-printer-programmatically-preserving-layout-formatting-and-page-size-fidel.cs) |  | Send pdf documents to a printer programmatically preserving layout formatting... |
| [serialize-pdf-document-bookmarks-to-xml-taking-a-pdf-file-as...](./serialize-pdf-document-bookmarks-to-xml-taking-a-pdf-file-as-input-and-returning-a-pdf-file-as-outp.cs) | `PdfBookmarkEditor` | Serialize pdf document bookmarks to xml taking a pdf file as input and return... |
| [transform-an-input-pdf-file-into-a-new-pdf-output-using-spec...](./transform-an-input-pdf-file-into-a-new-pdf-output-using-specified-conversion-options.cs) | `PdfConverter` | Transform an input pdf file into a new pdf output using specified conversion ... |

## Category Statistics
- Total examples: 23

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

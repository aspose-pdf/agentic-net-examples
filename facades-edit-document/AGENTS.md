---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (193/215 files) ← category-specific
- `using Aspose.Pdf;` (126/215 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (28/215 files)
- `using Aspose.Pdf.Text;` (14/215 files)
- `using Aspose.Pdf.Devices;` (9/215 files)
- `using Aspose.Pdf.Printing;` (4/215 files)
- `using Aspose.Pdf.Drawing;` (2/215 files)
- `using System;` (215/215 files)
- `using System.IO;` (206/215 files)
- `using System.Drawing;` (29/215 files)
- `using System.Collections.Generic;` (18/215 files)
- `using System.Text.Json;` (9/215 files)
- `using System.Drawing.Imaging;` (5/215 files)
- `using System.Net.Http;` (3/215 files)
- `using System.Threading.Tasks;` (3/215 files)
- `using System.Security.Cryptography;` (2/215 files)
- `using System.Text;` (2/215 files)
- `using System.Threading;` (2/215 files)
- `using System.Xml.Linq;` (2/215 files)
- `using System.Drawing.Printing;` (1/215 files)
- `using System.Linq;` (1/215 files)
- `using System.Reflection;` (1/215 files)
- `using System.Security.Cryptography.X509Certificates;` (1/215 files)
- `using System.Xml;` (1/215 files)
- `using System.Xml.XPath;` (1/215 files)

## Common Code Pattern

Most files in this category use `PdfContentEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfContentEditor tool = new PdfContentEditor();
tool.BindPdf("input.pdf");
// ... PdfContentEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-and-verify-document-attachment](./add-and-verify-document-attachment.cs) | Add and Verify Document Attachment in PDF | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates attaching a file to a PDF with PdfContentEditor, extracting it using PdfExtractor, a... |
| [add-attachment-and-verify-name](./add-attachment-and-verify-name.cs) | Add Attachment to PDF and Verify Its Name | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates how to attach a file to a PDF using PdfContentEditor and then retrieve the attachmen... |
| [add-attachment-md5-checksum-metadata](./add-attachment-md5-checksum-metadata.cs) | Add Attachment with MD5 Checksum Stored in PDF Metadata | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to compute an MD5 checksum for a file, attach the file to a PDF, and store the checksum... |
| [add-attachment-set-viewer-preference](./add-attachment-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF and configure the viewer to open the attachment panel automat... |
| [add-attachment-set-viewer-preferences-and-encrypt-...](./add-attachment-set-viewer-preferences-and-encrypt-pdf.cs) | Add Attachment, Set Viewer Preferences, and Encrypt PDF | `PdfContentEditor`, `AddDocumentAttachment`, `ChangeViewerPreference` | The example adds a file attachment and custom viewer preferences to an existing PDF, then encrypt... |
| [add-attachment-to-pdf-preserve-existing](./add-attachment-to-pdf-preserve-existing.cs) | Add Attachment to PDF Without Removing Existing Attachments | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a new document attachment to an existing PDF using Aspose.Pdf.Facades.Pdf... |
| [add-attachment-update-moddate](./add-attachment-update-moddate.cs) | Add Attachment and Update Modification Date in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to an existing PDF and set the document's modification date to the cur... |
| [add-bookmark-external-url-to-pdf](./add-bookmark-external-url-to-pdf.cs) | Add Bookmark with External URL to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a PDF bookmark that opens an external website (https://example.org) using Asp... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page with Aspose.Pdf | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to create a bookmark titled "Project Overview" that navigates to page five of a PDF usi... |
| [add-chapter-section-bookmarks-pdf](./add-chapter-section-bookmarks-pdf.cs) | Add Chapter and Section Bookmarks to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Demonstrates using Aspose.Pdf.Facades.PdfContentEditor to create a parent bookmark for a chapter ... |
| [add-clickable-web-link-to-pdf](./add-clickable-web-link-to-pdf.cs) | Add Clickable Web Link to PDF | `PdfContentEditor`, `BindPdf`, `CreateWebLink` | Shows how to insert a link annotation that opens an external website using the PdfContentEditor f... |
| [add-confidential-text-stamp-to-pdf](./add-confidential-text-stamp-to-pdf.cs) | Add Confidential Text Stamp with Transparency to PDF | `PdfFileStamp`, `BindPdf`, `FormattedText` | Demonstrates how to create a red "Confidential" text stamp with a semi‑transparent background and... |
| [add-configurable-text-watermark-to-pdfs](./add-configurable-text-watermark-to-pdfs.cs) | Add Configurable Text Watermark to PDFs in a Folder | `PdfFileStamp`, `BindPdf`, `AddStamp` | Shows how to load watermark settings from a JSON file and apply a text watermark to every PDF in ... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `SetSubmitUrl` | Demonstrates how to attach JavaScript to a push‑button field in a PDF using Aspose.Pdf.Facades so... |
| [add-custom-project-metadata-to-pdf](./add-custom-project-metadata-to-pdf.cs) | Add Custom Project Metadata to PDF using XMP | `PdfXmpMetadata`, `BindPdf`, `RegisterNamespaceURI` | Demonstrates how to bind a PDF, register a custom XMP namespace, add project identifier and versi... |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata (ProjectID) to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Shows how to bind an existing PDF, add a custom XMP metadata field named ProjectID with the value... |
| [add-dashed-line-annotation-to-pdf](./add-dashed-line-annotation-to-pdf.cs) | Add Dashed Line Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateLine` | Shows how to create a line annotation with a custom dash pattern on a PDF page using Aspose.Pdf.F... |
| [add-dashed-rectangle-annotation-opacity](./add-dashed-rectangle-annotation-opacity.cs) | Add Dashed Rectangle Annotation with 75% Opacity | `Document`, `Page`, `Rectangle` | Loads a PDF, creates a red rectangle annotation on the first page with a custom dash pattern and ... |
| [add-dashed-rectangle-annotation-to-pdf-page](./add-dashed-rectangle-annotation-to-pdf-page.cs) | Add Dashed Rectangle Annotation to PDF Page | `Document`, `Page`, `SquareAnnotation` | Demonstrates how to place a square (rectangle) annotation with a dashed border and 50% opacity on... |
| [add-document-attachment-and-list-attachments](./add-document-attachment-and-list-attachments.cs) | Add Document Attachment and List Attachments in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a file as a document attachment to a PDF using PdfContentEditor and then ... |
| [add-document-attachment-data-relationship](./add-document-attachment-data-relationship.cs) | Add Document Attachment with Data Relationship to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF using PdfContentEditor and set its AFRelationship to Data for... |
| [add-document-attachment-to-pdf](./add-document-attachment-to-pdf.cs) | Add Document Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file (Terms.pdf) to an existing PDF document using Aspose.... |
| [add-document-attachment-to-pdf__v2](./add-document-attachment-to-pdf__v2.cs) | Add Document Attachment to PDF (No Custom MIME Type) | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates attaching a file to an existing PDF using PdfContentEditor and notes that a custom M... |
| [add-document-attachment-with-creation-date](./add-document-attachment-with-creation-date.cs) | Add Document Attachment with Creation Date to PDF | `Document`, `PdfContentEditor`, `AddDocumentAttachment` | Demonstrates how to attach a PDF file to an existing PDF, assign a description, and set the attac... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external PDF file to an existing PDF and set a descriptive label us... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to a PDF using Aspose.Pdf.Facades.PdfContentEditor wh... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file as a document attachment in an existing PDF using Aspose.Pdf.Facades. |
| [add-encrypted-attachment-to-pdf](./add-encrypted-attachment-to-pdf.cs) | Add Encrypted Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-file-attachment-annotation-with-opacity](./add-file-attachment-annotation-with-opacity.cs) | Add File Attachment Annotation with Opacity to PDF Page | `Document`, `Page`, `FileSpecification` | Demonstrates how to attach a file to a specific PDF page using Aspose.Pdf and set the annotation'... |
| [add-file-attachment-link-annotation](./add-file-attachment-link-annotation.cs) | Add File Attachment Link Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Shows how to insert a file‑attachment annotation that opens an attached PDF when the user clicks ... |
| ... | | | *and 185 more files* |

## Category Statistics
- Total examples: 215

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.DataEditor.CosPdfBoolean`
- `Aspose.Pdf.DataEditor.CosPdfBoolean.Equals`
- `Aspose.Pdf.DataEditor.CosPdfBoolean.GetHashCode`
- `Aspose.Pdf.DataEditor.CosPdfBoolean.ToCosPdfBoolean`
- `Aspose.Pdf.DataEditor.CosPdfBoolean.ToString`
- `Aspose.Pdf.DataEditor.CosPdfBoolean.Value`
- `Aspose.Pdf.DataEditor.CosPdfDictionary`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.Add`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.AllKeys`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.Clear`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.Contains`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.ContainsKey`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.CopyTo`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.Count`
- `Aspose.Pdf.DataEditor.CosPdfDictionary.CreateEmptyDictionary`

### Rules
- Create DictionaryEditor with: new DictionaryEditor(Page page).
- Create DictionaryEditor with: new DictionaryEditor(Document document).
- Create DictionaryEditor with: new DictionaryEditor(Resources resources).
- Use DictionaryEditor.Add() to insert items into the collection.
- Create CosPdfDictionary with: new CosPdfDictionary(Resources resources).

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-edit-document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-18 | Run: `20260618_032725_440ba6`
<!-- AUTOGENERATED:END -->

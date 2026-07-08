---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **213** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (197/213 files) ← category-specific
- `using Aspose.Pdf;` (137/213 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (26/213 files)
- `using Aspose.Pdf.Text;` (14/213 files)
- `using Aspose.Pdf.Devices;` (6/213 files)
- `using Aspose.Pdf.Printing;` (4/213 files)
- `using Aspose.Pdf.Drawing;` (2/213 files)
- `using Aspose.Pdf.Forms;` (2/213 files)
- `using System;` (213/213 files)
- `using System.IO;` (201/213 files)
- `using System.Drawing;` (34/213 files)
- `using System.Collections.Generic;` (13/213 files)
- `using System.Text.Json;` (6/213 files)
- `using System.Drawing.Imaging;` (5/213 files)
- `using System.Threading.Tasks;` (4/213 files)
- `using System.Net.Http;` (3/213 files)
- `using NUnit.Framework;` (2/213 files)
- `using System.Collections;` (2/213 files)
- `using System.Drawing.Printing;` (2/213 files)
- `using System.Xml;` (2/213 files)
- `using System.Security.Cryptography;` (1/213 files)
- `using System.Security.Cryptography.X509Certificates;` (1/213 files)
- `using System.Threading;` (1/213 files)
- `using System.Xml.Linq;` (1/213 files)

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
| [add-and-verify-document-attachment](./add-and-verify-document-attachment.cs) | Add and Verify Document Attachment in PDF | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates how to attach a file to a PDF using PdfContentEditor, extract the attachment with Pd... |
| [add-annotation-and-merge-pdfs](./add-annotation-and-merge-pdfs.cs) | Add Annotation and Merge PDFs | `Document`, `TextAnnotation`, `Rectangle` | Shows how to add a text annotation to the first PDF and then concatenate it with a second PDF usi... |
| [add-attachment-and-list-pdf-attachments](./add-attachment-and-list-pdf-attachments.cs) | Add Attachment to PDF and List Attachments | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Shows how to embed a file into a PDF using PdfContentEditor and then retrieve all attachment name... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment to PDF and Retrieve Its Name | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file as an attachment in a PDF using Aspose.Pdf, save the document, a... |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add Attachment and Set Modification Date in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF and update the document's modification date to the cur... |
| [add-attachment-set-viewer-preference](./add-attachment-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF and configure the viewer to display the attachment pan... |
| [add-attachment-store-checksum-metadata](./add-attachment-store-checksum-metadata.cs) | Add Attachment and Store Checksum in PDF Metadata | `BindPdf`, `AddDocumentAttachment`, `Save` | Shows how to attach a file to a PDF, retrieve its checksum, and save that checksum as custom meta... |
| [add-attachment-to-pdf-and-verify](./add-attachment-to-pdf-and-verify.cs) | Add Attachment to PDF and Verify with PdfExtractor | `Document`, `PdfContentEditor`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF using the PdfContentEditor facade API and then verify ... |
| [add-attachment-to-pdf-async](./add-attachment-to-pdf-async.cs) | Add Attachment to PDF Asynchronously | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF and save it using Aspose.Pdf's PdfContentEditor on a backgrou... |
| [add-attachment-viewer-preferences-encrypt-pdf](./add-attachment-viewer-preferences-encrypt-pdf.cs) | Add Attachment and Viewer Preferences, Then Encrypt PDF with... | `Document`, `PdfContentEditor`, `ViewerPreference` | Demonstrates adding a file attachment and setting viewer preferences using PdfContentEditor, foll... |
| [add-attachment-with-custom-mime-type-to-pdf](./add-attachment-with-custom-mime-type-to-pdf.cs) | Add Attachment with Custom MIME Type to PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates how to embed a file into a PDF document and assign a custom MIME type using Aspose.Pdf. |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to add a "Project Overview" bookmark that links to page 5 of an existing PDF using Aspo... |
| [add-centered-red-bold-text-stamp](./add-centered-red-bold-text-stamp.cs) | Add Centered Red Bold Text Stamp to PDF Page | `Document`, `TextStamp`, `TextState` | Demonstrates how to place a bold, red, centered text stamp on the fifth page of a PDF using Aspos... |
| [add-clickable-web-link-to-pdf](./add-clickable-web-link-to-pdf.cs) | Add Clickable Web Link to PDF | `PdfContentEditor`, `BindPdf`, `CreateWebLink` | Shows how to insert a link annotation that opens an external website when clicked, using Aspose.P... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp with Red Font and Transparency | `PdfFileStamp`, `Stamp`, `BindLogo` | Demonstrates how to create a red "Confidential" text stamp with semi‑transparent background and a... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `Document`, `FormEditor`, `BindPdf` | Demonstrates how to create a PDF in memory, add a submit button, and attach JavaScript that shows... |
| [add-custom-appearance-stamp](./add-custom-appearance-stamp.cs) | Add Custom Appearance Stamp to PDF | `PdfContentEditor`, `BindPdf`, `CreateRubberStamp` | Shows how to create a rubber‑stamp annotation with a custom appearance stream taken from an exter... |
| [add-custom-dashed-line-annotation](./add-custom-dashed-line-annotation.cs) | Add Custom Dashed Line Annotation to PDF Diagram | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates how to use Aspose.Pdf.Facades to add a line annotation with a custom dash pattern to... |
| [add-custom-xmp-projectid-metadata](./add-custom-xmp-projectid-metadata.cs) | Add Custom XMP ProjectID Metadata to PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to bind XMP metadata to a PDF, add a custom field ProjectID with value 12345, an... |
| [add-dashed-rectangle-annotation-opacity](./add-dashed-rectangle-annotation-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to place a rectangle (square) annotation on page six of a PDF, set a dashed bord... |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF | `Document`, `Page`, `Rectangle` | Shows how to place a rectangle (square) annotation on the first page of a PDF, applying a custom ... |
| [add-department-xmp-metadata-to-pdf](./add-department-xmp-metadata-to-pdf.cs) | Add Department XMP Metadata to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Shows how to bind an existing PDF, add a custom XMP metadata field "Department" with the value "F... |
| [add-document-attachment-to-multiple-pdfs](./add-document-attachment-to-multiple-pdfs.cs) | Add Document Attachment to Multiple PDFs | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to batch‑process a collection of PDF files and add the same document attachment ... |
| [add-document-attachment-to-pdf](./add-document-attachment-to-pdf.cs) | Add Document Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external PDF file to an existing PDF document using Aspose.Pdf.Faca... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach an external PDF file to an existing PDF and assign a descriptive label using ... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF using Aspose.Pdf's PdfContentEditor facade while check... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to embed a file as a document attachment in a PDF using Aspose.Pdf's PdfContentE... |
| [add-encrypted-attachment-with-custom-description](./add-encrypted-attachment-with-custom-description.cs) | Add Encrypted Attachment with Custom Description to PDF | `PdfContentEditor`, `PdfFileSecurity`, `AddDocumentAttachment` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to insert a bookmark that opens an external website (https://example.org) into an exist... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Demonstrates how to embed a file as a clickable attachment annotation on a PDF page using Aspose.... |
| ... | | | *and 183 more files* |

## Category Statistics
- Total examples: 213

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

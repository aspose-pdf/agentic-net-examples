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

- `using Aspose.Pdf.Facades;` (192/214 files) ← category-specific
- `using Aspose.Pdf;` (138/214 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (29/214 files)
- `using Aspose.Pdf.Text;` (19/214 files)
- `using Aspose.Pdf.Devices;` (8/214 files)
- `using Aspose.Pdf.Printing;` (4/214 files)
- `using Aspose.Pdf.Drawing;` (2/214 files)
- `using Aspose.Pdf.Forms;` (2/214 files)
- `using Aspose.Pdf.Security;` (1/214 files)
- `using System;` (214/214 files)
- `using System.IO;` (203/214 files)
- `using System.Drawing;` (31/214 files)
- `using System.Collections.Generic;` (16/214 files)
- `using System.Text.Json;` (6/214 files)
- `using System.Threading.Tasks;` (4/214 files)
- `using System.Linq;` (3/214 files)
- `using System.Net.Http;` (3/214 files)
- `using NUnit.Framework;` (2/214 files)
- `using System.Drawing.Imaging;` (2/214 files)
- `using System.Drawing.Printing;` (2/214 files)
- `using System.Security.Cryptography;` (2/214 files)
- `using System.Text;` (2/214 files)
- `using System.Xml;` (2/214 files)
- `using System.Xml.Linq;` (2/214 files)
- `using System.Collections;` (1/214 files)
- `using System.Reflection;` (1/214 files)
- `using System.Security.Cryptography.X509Certificates;` (1/214 files)
- `using System.Threading;` (1/214 files)

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
| [add-aes-encrypted-attachment-to-pdf](./add-aes-encrypted-attachment-to-pdf.cs) | Add AES Encrypted Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates encrypting a file with AES‑256 and attaching the encrypted data to a PDF using Aspos... |
| [add-and-verify-attachment-in-pdf](./add-and-verify-attachment-in-pdf.cs) | Add and Verify Attachment in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF with PdfContentEditor, extract the attachment using PdfExtrac... |
| [add-attachment-and-list-names](./add-attachment-and-list-names.cs) | Add Attachment to PDF and List Attachment Names | `Document`, `Page`, `TextFragment` | Demonstrates how to attach a file to a PDF using PdfContentEditor, save the document, and then re... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment to PDF and Retrieve Its Name | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF using the Facades API and then extract the attachment'... |
| [add-attachment-data-relationship](./add-attachment-data-relationship.cs) | Add Attachment with Data Relationship to PDF | `Document`, `FileSpecification`, `AFRelationship` | Shows how to embed a file into a PDF document and set its AFRelationship to Data using Aspose.Pdf. |
| [add-attachment-set-viewer-preference](./add-attachment-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF document and configure the viewer to open the attachment pane... |
| [add-attachment-store-checksum-metadata](./add-attachment-store-checksum-metadata.cs) | Add Attachment and Store Checksum in PDF Metadata | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF, retrieve its MD5 checksum, and save the checksum as c... |
| [add-attachment-to-pdf-async](./add-attachment-to-pdf-async.cs) | Asynchronously Add Document Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to an existing PDF and save the result using Aspose.Pdf.Facades while ... |
| [add-attachment-update-moddate](./add-attachment-update-moddate.cs) | Add Attachment and Update Modification Date in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF using PdfContentEditor and set the document's modifica... |
| [add-author-title-metadata-to-pdf](./add-author-title-metadata-to-pdf.cs) | Add Author and Title Metadata to PDF | `Document`, `DocumentInfo`, `Save` | Demonstrates how to load a PDF with Aspose.Pdf, set the Author and Title metadata fields, and sav... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp with Red Font and Semi‑Transpare... | `Document`, `TextStamp`, `FontRepository` | Shows how to create a red "Confidential" text stamp with a semi‑transparent background and place ... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `Document`, `FormEditor`, `AddSubmitBtn` | Demonstrates how to add a submit button to a PDF form and attach JavaScript that shows a confirma... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add Corporate Branding XMP Metadata to PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to embed custom XMP metadata—including a logo URL and brand color—into a PDF document u... |
| [add-custom-mime-type-attachment-to-pdf](./add-custom-mime-type-attachment-to-pdf.cs) | Add Custom MIME Type Attachment to PDF | `Document`, `FileSpecification`, `Save` | Shows how to embed a file into an existing PDF and assign a custom MIME type using Aspose.Pdf. |
| [add-custom-xmp-projectid-metadata](./add-custom-xmp-projectid-metadata.cs) | Add Custom XMP ProjectID Metadata to PDF | `Document`, `BindPdf`, `RegisterNamespaceURI` | Shows how to bind a PDF to the XMP metadata facade, register a custom namespace, add a ProjectID ... |
| [add-dashed-line-annotation](./add-dashed-line-annotation.cs) | Add Dashed Line Annotation with Custom Dash Pattern | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates using PdfContentEditor to insert a red dashed line annotation with a custom dash pat... |
| [add-dashed-rectangle-annotation-opacity](./add-dashed-rectangle-annotation-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `PdfAnnotationEditor`, `BindPdf`, `Save` | Shows how to add a rectangle (square) annotation with a dashed border and 50% opacity to page six... |
| [add-department-xmp-metadata-to-pdf](./add-department-xmp-metadata-to-pdf.cs) | Add Department XMP Metadata to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to bind an existing PDF, add a custom XMP metadata field called Department with ... |
| [add-document-attachment-to-multiple-pdfs](./add-document-attachment-to-multiple-pdfs.cs) | Add Document Attachment to Multiple PDFs | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to batch‑process a list of PDF files, adding the same file attachment to each do... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external PDF file to an existing PDF and set a descriptive label us... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to a PDF using Aspose.Pdf.Facades.PdfContentEditor wh... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to embed a file as an attachment in an existing PDF using Aspose.Pdf.Facades Pdf... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a bookmark in an existing PDF that opens an external website when selected, u... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Shows how to create a clickable file‑attachment annotation on a PDF page using Aspose.Pdf's PdfCo... |
| [add-file-attachment-set-creation-date](./add-file-attachment-set-creation-date.cs) | Add File Attachment and Set Creation Date | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Demonstrates how to embed a file attachment into a PDF using Aspose.Pdf.Facades and then set the ... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed an external file (Terms.pdf) into a PDF document using Aspose.Pdf.Facades with... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to Existing PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to an existing PDF document using Aspose.Pdf's PdfContentEditor... |
| [add-file-attachment-to-secured-pdf](./add-file-attachment-to-secured-pdf.cs) | Add File Attachment to a Secured PDF | `Document`, `PdfContentEditor`, `AddDocumentAttachment` | Demonstrates opening an encrypted PDF with a password, adding a file attachment, and saving the u... |
| [add-free-text-annotation-helvetica](./add-free-text-annotation-helvetica.cs) | Add Free‑Text Annotation with Helvetica Font to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, add a blank page, and place a free‑text annotation using Helvetica 12‑... |
| [add-free-text-annotation-with-html](./add-free-text-annotation-with-html.cs) | Add Free‑Text Annotation with HTML to PDF | `PdfContentEditor`, `BindPdf`, `CreateFreeText` | Demonstrates how to add a free‑text annotation containing rich HTML content to a PDF using Aspose... |
| ... | | | *and 184 more files* |

## Category Statistics
- Total examples: 214

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-edit-document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

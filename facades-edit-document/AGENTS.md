---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **213** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (191/213 files) ← category-specific
- `using Aspose.Pdf;` (141/213 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (30/213 files)
- `using Aspose.Pdf.Text;` (17/213 files)
- `using Aspose.Pdf.Devices;` (9/213 files)
- `using Aspose.Pdf.Printing;` (4/213 files)
- `using Aspose.Pdf.Forms;` (3/213 files)
- `using Aspose.Pdf.Drawing;` (1/213 files)
- `using System;` (213/213 files)
- `using System.IO;` (197/213 files)
- `using System.Drawing;` (36/213 files)
- `using System.Collections.Generic;` (16/213 files)
- `using System.Drawing.Imaging;` (5/213 files)
- `using System.Text.Json;` (5/213 files)
- `using System.Threading.Tasks;` (4/213 files)
- `using System.Net.Http;` (3/213 files)
- `using System.Xml.Linq;` (3/213 files)
- `using NUnit.Framework;` (2/213 files)
- `using System.Reflection;` (2/213 files)
- `using System.Threading;` (2/213 files)
- `using System.Collections;` (1/213 files)
- `using System.Linq;` (1/213 files)
- `using System.Security.Cryptography;` (1/213 files)
- `using System.Security.Cryptography.X509Certificates;` (1/213 files)
- `using System.Text;` (1/213 files)
- `using System.Text.RegularExpressions;` (1/213 files)
- `using System.Xml;` (1/213 files)

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
| [add-and-verify-attachment-in-pdf](./add-and-verify-attachment-in-pdf.cs) | Add and Verify Attachment in PDF | `PdfContentEditor`, `PdfExtractor`, `BindPdf` | Shows how to attach a file to a PDF with PdfContentEditor, extract it using PdfExtractor, and com... |
| [add-and-verify-document-attachment](./add-and-verify-document-attachment.cs) | Add and Verify Document Attachment in PDF | `AddDocumentAttachment`, `Save`, `BindPdf` | Demonstrates how to attach a file to a PDF using PdfContentEditor and then retrieve the attachmen... |
| [add-attachment-and-list-attachments](./add-attachment-and-list-attachments.cs) | Add Attachment to PDF and List Attachments | `Document`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to embed a file as a document attachment using PdfContentEditor and then retriev... |
| [add-attachment-set-moddate-pdf](./add-attachment-set-moddate-pdf.cs) | Add Attachment and Set Modification Date in PDF | `Document`, `PdfContentEditor`, `BindPdf` | Shows how to attach a file to a PDF document and update its modification date to the current UTC ... |
| [add-attachment-set-viewer-preference](./add-attachment-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates adding a file attachment to a PDF without a visible annotation and configuring the v... |
| [add-attachment-store-checksum-metadata](./add-attachment-store-checksum-metadata.cs) | Add Attachment and Store Checksum in PDF Metadata | `PdfContentEditor`, `AddDocumentAttachment`, `Document` | Shows how to embed a file into a PDF, retrieve its MD5 checksum, and write that checksum as custo... |
| [add-attachment-with-custom-mime-type-to-pdf](./add-attachment-with-custom-mime-type-to-pdf.cs) | Add Attachment with Custom MIME Type to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file into a PDF and specify a custom MIME type using Aspose.Pdf. |
| [add-author-and-title-metadata-to-pdf](./add-author-and-title-metadata-to-pdf.cs) | Add Author and Title Metadata to PDF | `Document`, `DocumentInfo`, `SaveFormat` | Demonstrates how to set the author and title metadata of a PDF using Aspose.Pdf by updating the d... |
| [add-base64-image-stamp-to-pdf-page](./add-base64-image-stamp-to-pdf-page.cs) | Add Base64 Image Stamp to Specific PDF Page | `Stamp`, `PdfFileStamp`, `BindImage` | Shows how to decode a Base64‑encoded image, create a Stamp, position it at 50 mm, and apply it to... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to create a bookmark titled "Project Overview" that navigates to page 5 of a PDF using ... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Red Text Stamp with Transparency | `Document`, `TextStamp`, `FindFont` | Demonstrates how to place a red "Confidential" text stamp with 50 % opacity on every page of a PD... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `AddSubmitBtn` | Demonstrates how to add a submit button to a PDF form and attach JavaScript that shows a confirma... |
| [add-custom-project-schema-to-pdf-xmp-metadata](./add-custom-project-schema-to-pdf-xmp-metadata.cs) | Add Custom Project Schema to PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `RegisterNamespaceURI` | Demonstrates how to register a custom namespace and add project identifier and version fields to ... |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata (ProjectID) to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata field named ProjectID with the value 12345 to a PDF... |
| [add-dashed-line-annotation](./add-dashed-line-annotation.cs) | Add Dashed Line Annotation with Custom Pattern to PDF | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates using Aspose.Pdf.Facades.PdfContentEditor to create a line annotation with a custom ... |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates how to add a square (rectangle) annotation with a dashed border and 50% opacity to p... |
| [add-document-attachment-preserve-existing](./add-document-attachment-preserve-existing.cs) | Add Document Attachment Without Deleting Existing Attachment... | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a new file attachment to an existing PDF using Aspose.Pdf.Facades.PdfCont... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external PDF file to an existing PDF and set a descriptive label us... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to a PDF using Aspose.Pdf.Facades.PdfContentEditor wh... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file into an existing PDF using Aspose.Pdf.Facades.PdfContentEditor without ... |
| [add-encrypted-attachment-with-custom-description](./add-encrypted-attachment-with-custom-description.cs) | Add Encrypted Attachment with Custom Description to PDF | `PdfContentEditor`, `AddDocumentAttachment`, `PdfFileSecurity` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to create a bookmark in a PDF that opens an external website (URI action) using Aspose.... |
| [add-file-attachment-annotation-opacity](./add-file-attachment-annotation-opacity.cs) | Add File Attachment Annotation with Opacity to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Demonstrates how to use PdfContentEditor to attach a file as an annotation on page 3 of a PDF and... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Shows how to create a file‑attachment annotation on a PDF page that opens the attached file when ... |
| [add-file-attachment-data-relationship](./add-file-attachment-data-relationship.cs) | Add File Attachment with Data Relationship to PDF | `PdfContentEditor`, `Document`, `FileSpecification` | Demonstrates attaching a file to a PDF page using PdfContentEditor and setting the AFRelationship... |
| [add-file-attachment-to-encrypted-pdf](./add-file-attachment-to-encrypted-pdf.cs) | Add File Attachment to Encrypted PDF | `Document`, `PdfContentEditor`, `AddDocumentAttachment` | Demonstrates opening a password‑protected PDF, adding a file attachment without a visual annotati... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF Document | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file (Terms.pdf) to an existing PDF using Aspose.Pdf's Pdf... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to PDF using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to an existing PDF document using the Aspose.Pdf.Faca... |
| [add-file-attachment-with-creation-date](./add-file-attachment-with-creation-date.cs) | Add File Attachment with Creation Date to PDF | `PdfContentEditor`, `CreateFileAttachment`, `Document` | Demonstrates attaching a PDF file to another PDF using PdfContentEditor and setting the attachmen... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry on Network Timeout | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Shows how to download a file from a URL and attach it to a PDF using Aspose.Pdf's PdfContentEdito... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **207** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (185/207 files) ← category-specific
- `using Aspose.Pdf.Facades;` (67/207 files)
- `using Aspose.Pdf.Annotations;` (63/207 files)
- `using Aspose.Pdf.Text;` (25/207 files)
- `using Aspose.Pdf.Drawing;` (8/207 files)
- `using Aspose.Pdf.Devices;` (7/207 files)
- `using Aspose.Pdf.Forms;` (4/207 files)
- `using Aspose.Pdf.Security;` (4/207 files)
- `using Aspose.Pdf.Printing;` (3/207 files)
- `using System;` (207/207 files)
- `using System.IO;` (206/207 files)
- `using Microsoft.CSharp.RuntimeBinder;` (28/207 files)
- `using System.Collections.Generic;` (12/207 files)
- `using System.Drawing;` (8/207 files)
- `using System.Reflection;` (6/207 files)
- `using System.Text.Json;` (6/207 files)
- `using System.Linq;` (4/207 files)
- `using System.Drawing.Imaging;` (3/207 files)
- `using System.Net.Http;` (3/207 files)
- `using System.Security.Cryptography;` (3/207 files)
- `using System.Text;` (3/207 files)
- `using System.Threading.Tasks;` (3/207 files)
- `using System.Xml.Serialization;` (2/207 files)
- `using System.Diagnostics;` (1/207 files)
- `using System.IO.Compression;` (1/207 files)
- `using System.Runtime.CompilerServices;` (1/207 files)
- `using System.Text.RegularExpressions;` (1/207 files)
- `using System.Threading;` (1/207 files)
- `using System.Xml;` (1/207 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-annotation-and-merge-pdfs](./add-annotation-and-merge-pdfs.cs) | Add Annotation and Merge PDFs | `Document`, `Page`, `TextAnnotation` | Shows how to add a text annotation to the first PDF page and then concatenate that annotated PDF ... |
| [add-attachment-and-encrypt-pdf](./add-attachment-and-encrypt-pdf.cs) | Encrypt PDF with Attachment and Viewer Preferences | `Document`, `FileSpecification`, `EmbeddedFiles` | Loads a PDF, adds an embedded file attachment, sets viewer preferences, encrypts the document wit... |
| [add-attachment-and-list-names](./add-attachment-and-list-names.cs) | Add Attachment to PDF and List Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file into a PDF using Aspose.Pdf, save the document, and then enumerate the ... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment to PDF and Verify File Name | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates embedding a file into a PDF using Aspose.Pdf, saving the document, and retrieving th... |
| [add-attachment-and-set-viewer-preference](./add-attachment-and-set-viewer-preference.cs) | Add Attachment and Set Viewer Preferences to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file into a PDF and configure viewer preferences such as hiding the toolbar ... |
| [add-attachment-and-store-checksum-metadata](./add-attachment-and-store-checksum-metadata.cs) | Compute and Store Attachment SHA-256 Checksums in PDF Metada... | `Document`, `FileSpecification`, `DocumentInfo` | The example loads a PDF, iterates over its embedded file attachments, calculates a SHA‑256 checks... |
| [add-attachment-preserve-existing](./add-attachment-preserve-existing.cs) | Add New Attachment to PDF Without Removing Existing Attachme... | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed an additional file into an existing PDF while keeping any previously embedded ... |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add Attachment with Audit Date to PDF | `Document`, `FileSpecification`, `Params` | Shows how to embed a file into a PDF document and set its modification (audit) date to the curren... |
| [add-attachment-to-multiple-pdfs](./add-attachment-to-multiple-pdfs.cs) | Add Attachment to Multiple PDFs | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to batch‑process PDF files and embed the same attachment into each document using Aspos... |
| [add-attachment-verify-extraction](./add-attachment-verify-extraction.cs) | Add and Verify Embedded File Attachment in PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file into a PDF, extract it back, and verify that the extracted conte... |
| [add-batch-text-annotations-to-pdf](./add-batch-text-annotations-to-pdf.cs) | Batch Add Text Annotations to PDF | `Document`, `Page`, `Rectangle` | Shows how to add multiple text annotations to a PDF in a single batch by iterating over a list of... |
| [add-bold-red-centered-text-stamp-page-5](./add-bold-red-centered-text-stamp-page-5.cs) | Add Centered Red Bold Text Stamp to PDF Page | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to create a bold red text stamp, center it on the page, and apply it to the fift... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add a PDF Bookmark with PdfBookmarkEditor | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to create a top‑level bookmark named "Project Overview" that navigates to page 5 of a P... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp with Transparent Background | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to load a PDF, create a red "Confidential" text stamp with a semi‑transparent ba... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add XMP Branding Metadata to PDF | `Document`, `Metadata`, `Save` | Shows how to embed custom XMP metadata with a corporate logo URL and brand color into a PDF docum... |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata (ProjectID) to PDF | `Document`, `DocumentInfo`, `Save` | Demonstrates how to add or update a custom XMP metadata entry named ProjectID in a PDF using Aspo... |
| [add-dashed-line-annotation-to-pdf](./add-dashed-line-annotation-to-pdf.cs) | Add Line Annotation with Arrow to PDF | `Document`, `Page`, `Point` | Shows how to load a PDF, create a line annotation with an arrow and custom color, add it to a pag... |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `Document`, `Page`, `Rectangle` | Shows how to place a rectangle (square) annotation with a dashed border and 50% opacity on page 6... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Attachment with Descriptive Label to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file into a PDF and assign a descriptive label using Aspose.Pdf. |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file attachment into a PDF using Aspose.Pdf while validating input pa... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file as an attachment inside a PDF document using Aspose.Pdf by creating a F... |
| [add-encrypted-attachment-aes256](./add-encrypted-attachment-aes256.cs) | Add Encrypted Attachment to PDF with Custom Description | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to encrypt a file using AES‑256, create a FileSpecification with a custom description, ... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `Document`, `OutlineItemCollection`, `GoToURIAction` | Shows how to create a top‑level bookmark in a PDF that opens an external web page when selected, ... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation with Embedded File | `Document`, `Page`, `Rectangle` | Demonstrates how to embed a file into a PDF and create a file‑attachment annotation that opens th... |
| [add-file-attachment-data-relationship](./add-file-attachment-data-relationship.cs) | Add Data Attachment to PDF | `Document`, `FileSpecification`, `AFRelationship` | Shows how to embed a file as a Data attachment in an existing PDF document using Aspose.Pdf. |
| [add-file-attachment-to-encrypted-pdf](./add-file-attachment-to-encrypted-pdf.cs) | Add Attachment to Encrypted PDF | `Document`, `ctor(string, string)`, `FileSpecification` | Shows how to open a password‑protected PDF with Aspose.Pdf, embed a file attachment, and save the... |
| [add-file-attachment-to-pdf__v3](./add-file-attachment-to-pdf__v3.cs) | Add File Attachment Annotation to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to embed a file (Terms.pdf) as an attachment annotation with a description on a ... |
| [add-file-attachment-to-pdf__v4](./add-file-attachment-to-pdf__v4.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a local file as an attachment in an existing PDF document using Aspose.Pdf. |
| [add-file-attachment-with-creation-date](./add-file-attachment-with-creation-date.cs) | Add Attachment with Description and Creation Date to PDF | `Document`, `FileSpecification`, `Params` | Demonstrates how to embed a file into a PDF, set its description, and assign the current system t... |
| [add-file-attachment-with-custom-mime](./add-file-attachment-with-custom-mime.cs) | Add Custom MIME Type Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file into a PDF document and set a custom MIME type for the attachment using... |
| ... | | | *and 177 more files* |

## Category Statistics
- Total examples: 207

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
Updated: 2026-11-08 | Run: `20261108_210627_9f4484`
<!-- AUTOGENERATED:END -->

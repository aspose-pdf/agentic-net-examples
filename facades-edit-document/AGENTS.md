---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **214** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (137/214 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (26/214 files)
- `using Aspose.Pdf.Text;` (20/214 files)
- `using Aspose.Pdf.Devices;` (7/214 files)
- `using Aspose.Pdf.Printing;` (4/214 files)
- `using Aspose.Pdf.Drawing;` (2/214 files)
- `using Aspose.Pdf.Forms;` (2/214 files)
- `using System;` (214/214 files)
- `using System.IO;` (201/214 files)
- `using System.Drawing;` (39/214 files)
- `using System.Collections.Generic;` (10/214 files)
- `using System.Text.Json;` (6/214 files)
- `using System.Drawing.Imaging;` (4/214 files)
- `using System.Threading.Tasks;` (4/214 files)
- `using System.Net.Http;` (3/214 files)
- `using NUnit.Framework;` (2/214 files)
- `using System.Security.Cryptography;` (2/214 files)
- `using System.Xml.Linq;` (2/214 files)
- `using System.Collections;` (1/214 files)
- `using System.Drawing.Printing;` (1/214 files)
- `using System.Linq;` (1/214 files)
- `using System.Threading;` (1/214 files)
- `using System.Xml;` (1/214 files)

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
| [add-an-attachment-with-custom-mime-type-applicatio...](./add-an-attachment-with-custom-mime-type-application-pdf-and-description-invoice-document.cs) | Add An Attachment With Custom Mime Type Application Pdf And ... | `PdfContentEditor` | Add An Attachment With Custom Mime Type Application Pdf And Description Invoice Document |
| [add-annotation-and-merge-pdfs](./add-annotation-and-merge-pdfs.cs) | Add Annotation to PDF and Merge with Another PDF | `Document`, `Page`, `TextAnnotation` | The example loads a PDF, adds a text annotation to its first page, saves it, and then concatenate... |
| [add-attachment-and-encrypt-pdf](./add-attachment-and-encrypt-pdf.cs) | Add Attachment and Encrypt PDF with Password | `PdfContentEditor`, `AddDocumentAttachment`, `ChangeViewerPreference` | Demonstrates adding a document attachment, setting viewer preferences, and then encrypting the PD... |
| [add-attachment-and-list-names](./add-attachment-and-list-names.cs) | Add Attachment to PDF and List Attachment Names | `Document`, `PdfContentEditor`, `PdfExtractor` | Demonstrates how to embed a PDF file as an attachment using PdfContentEditor and then extract and... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment and Retrieve Its Name from PDF | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates how to embed a file as a PDF attachment using PdfContentEditor and then extract the ... |
| [add-attachment-and-set-viewer-preference](./add-attachment-and-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file as a document attachment and configure the PDF viewer to open the attac... |
| [add-attachment-and-store-checksum-metadata](./add-attachment-and-store-checksum-metadata.cs) | Add PDF Attachment and Store Checksum in Metadata | `Document`, `TextFragment`, `PdfContentEditor` | Demonstrates attaching a PDF file to another PDF using Aspose.Pdf.Facades, computing its MD5 chec... |
| [add-attachment-preserve-existing](./add-attachment-preserve-existing.cs) | Add Attachment to PDF Without Deleting Existing Attachments | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a new document attachment to an existing PDF while keeping any previously... |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add Attachment and Set Modification Date in PDF | `PdfContentEditor`, `AddDocumentAttachment`, `BindPdf` | Demonstrates how to attach a file to a PDF and update the document's modification date to the cur... |
| [add-attachment-to-multiple-pdfs](./add-attachment-to-multiple-pdfs.cs) | Add Attachment to Multiple PDFs | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to batch‑process a list of PDF files and add the same document attachment to each using... |
| [add-attachment-verify-extraction](./add-attachment-verify-extraction.cs) | Add Attachment to PDF and Verify Extraction | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates how to attach a file to a PDF using PdfContentEditor, then extract the attachment wi... |
| [add-batch-text-annotations-to-pdf](./add-batch-text-annotations-to-pdf.cs) | Add Batch Text Annotations to PDF | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Shows how to add multiple text annotations to a PDF in a single batch by looping over annotation ... |
| [add-bold-red-centered-text-stamp-page-5](./add-bold-red-centered-text-stamp-page-5.cs) | Add Bold Red Centered Text Stamp to Page 5 | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to place a bold, red, centered text stamp on the fifth page of a PDF using Aspos... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to create a bookmark titled "Project Overview" that links to page five of a PDF using A... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp to PDF | `FormattedText`, `BindLogo`, `IsBackground` | Demonstrates creating a red "Confidential" text stamp with semi‑transparent background and applyi... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add Corporate Branding XMP Metadata to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to bind a PDF, add custom XMP metadata such as a logo URL and brand color, and s... |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata and Document Property to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata field (ProjectID) to a PDF and synchronize it with ... |
| [add-dashed-line-annotation-to-pdf](./add-dashed-line-annotation-to-pdf.cs) | Add Dashed Line Annotation to PDF Diagram | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates how to add a custom dashed line annotation to a PDF page using Aspose.Pdf.Facades.Pd... |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateSquareCircle` | Demonstrates how to add a rectangle (square) annotation on page 6 of a PDF, apply a dashed border... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach an external PDF file to an existing PDF and assign a descriptive label using ... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to a PDF using Aspose.Pdf.Facades.PdfContentEditor wh... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed an external file as a hidden attachment in an existing PDF using Aspose.Pdf's ... |
| [add-encrypted-attachment-aes256](./add-encrypted-attachment-aes256.cs) | Add Encrypted Attachment to PDF with AES‑256 | `Document`, `PdfContentEditor`, `PdfFileSecurity` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to use Aspose.Pdf.Facades.PdfContentEditor to create a bookmark that opens an external ... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Demonstrates how to attach a file to a PDF page as a clickable annotation using Aspose.Pdf.Facade... |
| [add-file-attachment-data-relationship](./add-file-attachment-data-relationship.cs) | Add File Attachment with Data Relationship to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to an existing PDF using PdfContentEditor and set the attachment's AFR... |
| [add-file-attachment-to-encrypted-pdf](./add-file-attachment-to-encrypted-pdf.cs) | Add File Attachment to Encrypted PDF | `Document`, `PdfContentEditor`, `Document(string, string)` | Shows how to open a password‑protected PDF, attach a file to it, and save the modified document. |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file (Terms.pdf) to an existing PDF document using Aspose.... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to PDF using PdfContentEditor | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a local file to an existing PDF document and save the updated PDF using Aspos... |
| [add-file-attachment-with-creation-date](./add-file-attachment-with-creation-date.cs) | Add File Attachment with Creation Date to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Shows how to attach a file to a PDF page, give it a description, and set the attachment's creatio... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-edit-document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

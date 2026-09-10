---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **328** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (192/328 files) ← category-specific
- `using Aspose.Pdf;` (137/328 files)
- `using Aspose.Pdf.Annotations;` (26/328 files)
- `using Aspose.Pdf.Text;` (20/328 files)
- `using Aspose.Pdf.Devices;` (7/328 files)
- `using Aspose.Pdf.Printing;` (4/328 files)
- `using Aspose.Pdf.Drawing;` (2/328 files)
- `using Aspose.Pdf.Forms;` (2/328 files)
- `using System;` (214/328 files)
- `using System.IO;` (201/328 files)
- `using System.Drawing;` (39/328 files)
- `using System.Collections.Generic;` (10/328 files)
- `using System.Text.Json;` (6/328 files)
- `using System.Drawing.Imaging;` (4/328 files)
- `using System.Threading.Tasks;` (4/328 files)
- `using System.Net.Http;` (3/328 files)
- `using NUnit.Framework;` (2/328 files)
- `using System.Security.Cryptography;` (2/328 files)
- `using System.Xml.Linq;` (2/328 files)
- `using System.Collections;` (1/328 files)
- `using System.Drawing.Printing;` (1/328 files)
- `using System.Linq;` (1/328 files)
- `using System.Threading;` (1/328 files)
- `using System.Xml;` (1/328 files)

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
| [add-and-verify-attachment-in-pdf](./add-and-verify-attachment-in-pdf.cs) | Add and verify attachment in pdf |  | Add and verify attachment in pdf |
| [add-and-verify-document-attachment](./add-and-verify-document-attachment.cs) | Add and verify document attachment |  | Add and verify document attachment |
| [add-annotation-and-merge-pdfs](./add-annotation-and-merge-pdfs.cs) | Add Annotation to PDF and Merge with Another PDF | `Document`, `Page`, `TextAnnotation` | The example loads a PDF, adds a text annotation to its first page, saves it, and then concatenate... |
| [add-attachment-and-encrypt-pdf](./add-attachment-and-encrypt-pdf.cs) | Add Attachment and Encrypt PDF with Password | `PdfContentEditor`, `AddDocumentAttachment`, `ChangeViewerPreference` | Demonstrates adding a document attachment, setting viewer preferences, and then encrypting the PD... |
| [add-attachment-and-list-attachments](./add-attachment-and-list-attachments.cs) | Add attachment and list attachments |  | Add attachment and list attachments |
| [add-attachment-and-list-names](./add-attachment-and-list-names.cs) | Add Attachment to PDF and List Attachment Names | `Document`, `PdfContentEditor`, `PdfExtractor` | Demonstrates how to embed a PDF file as an attachment using PdfContentEditor and then extract and... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment and Retrieve Its Name from PDF | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates how to embed a file as a PDF attachment using PdfContentEditor and then extract the ... |
| [add-attachment-and-set-viewer-preference](./add-attachment-and-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file as a document attachment and configure the PDF viewer to open the attac... |
| [add-attachment-and-store-checksum-metadata](./add-attachment-and-store-checksum-metadata.cs) | Add PDF Attachment and Store Checksum in Metadata | `Document`, `TextFragment`, `PdfContentEditor` | Demonstrates attaching a PDF file to another PDF using Aspose.Pdf.Facades, computing its MD5 chec... |
| [add-attachment-preserve-existing](./add-attachment-preserve-existing.cs) | Add Attachment to PDF Without Deleting Existing Attachments | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a new document attachment to an existing PDF while keeping any previously... |
| [add-attachment-set-moddate-pdf](./add-attachment-set-moddate-pdf.cs) | Add attachment set moddate pdf |  | Add attachment set moddate pdf |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add Attachment and Set Modification Date in PDF | `PdfContentEditor`, `AddDocumentAttachment`, `BindPdf` | Demonstrates how to attach a file to a PDF and update the document's modification date to the cur... |
| [add-attachment-store-checksum-metadata](./add-attachment-store-checksum-metadata.cs) | Add attachment store checksum metadata |  | Add attachment store checksum metadata |
| [add-attachment-to-multiple-pdfs](./add-attachment-to-multiple-pdfs.cs) | Add Attachment to Multiple PDFs | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to batch‑process a list of PDF files and add the same document attachment to each using... |
| [add-attachment-verify-extraction](./add-attachment-verify-extraction.cs) | Add Attachment to PDF and Verify Extraction | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates how to attach a file to a PDF using PdfContentEditor, then extract the attachment wi... |
| [add-attachment-with-custom-mime-type-to-pdf](./add-attachment-with-custom-mime-type-to-pdf.cs) | Add attachment with custom mime type to pdf |  | Add attachment with custom mime type to pdf |
| [add-author-and-title-metadata-to-pdf](./add-author-and-title-metadata-to-pdf.cs) | Add author and title metadata to pdf |  | Add author and title metadata to pdf |
| [add-base64-image-stamp-to-pdf-page](./add-base64-image-stamp-to-pdf-page.cs) | Add base64 image stamp to pdf page |  | Add base64 image stamp to pdf page |
| [add-batch-text-annotations-to-pdf](./add-batch-text-annotations-to-pdf.cs) | Add Batch Text Annotations to PDF | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Shows how to add multiple text annotations to a PDF in a single batch by looping over annotation ... |
| [add-bold-red-centered-text-stamp-page-5](./add-bold-red-centered-text-stamp-page-5.cs) | Add Bold Red Centered Text Stamp to Page 5 | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to place a bold, red, centered text stamp on the fifth page of a PDF using Aspos... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to create a bookmark titled "Project Overview" that links to page five of a PDF using A... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Text Stamp to PDF | `FormattedText`, `BindLogo`, `IsBackground` | Demonstrates creating a red "Confidential" text stamp with semi‑transparent background and applyi... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add confirmation dialog to pdf submit button |  | Add confirmation dialog to pdf submit button |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add Corporate Branding XMP Metadata to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to bind a PDF, add custom XMP metadata such as a logo URL and brand color, and s... |
| [add-custom-project-schema-to-pdf-xmp-metadata](./add-custom-project-schema-to-pdf-xmp-metadata.cs) | Add custom project schema to pdf xmp metadata |  | Add custom project schema to pdf xmp metadata |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata and Document Property to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata field (ProjectID) to a PDF and synchronize it with ... |
| [add-dashed-line-annotation-to-pdf](./add-dashed-line-annotation-to-pdf.cs) | Add Dashed Line Annotation to PDF Diagram | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates how to add a custom dashed line annotation to a PDF page using Aspose.Pdf.Facades.Pd... |
| [add-dashed-line-annotation](./add-dashed-line-annotation.cs) | Add dashed line annotation |  | Add dashed line annotation |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateSquareCircle` | Demonstrates how to add a rectangle (square) annotation on page 6 of a PDF, apply a dashed border... |
| ... | | | *and 298 more files* |

## Category Statistics
- Total examples: 328

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

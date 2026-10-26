---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

> **Facades edit document** in PDF using C# / .NET -- **206** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (190/206 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (67/206 files)
- `using Aspose.Pdf.Facades;` (57/206 files)
- `using Aspose.Pdf.Text;` (22/206 files)
- `using Aspose.Pdf.Devices;` (8/206 files)
- `using Aspose.Pdf.Drawing;` (6/206 files)
- `using Aspose.Pdf.Forms;` (6/206 files)
- `using Aspose.Pdf.Printing;` (2/206 files)
- `using Aspose.Pdf.Security;` (2/206 files)
- `using Aspose.Pdf.LogicalStructure;` (1/206 files)
- `using Aspose.Pdf.Tagged;` (1/206 files)
- `using System;` (206/206 files)
- `using System.IO;` (206/206 files)
- `using Microsoft.CSharp.RuntimeBinder;` (21/206 files)
- `using System.Collections.Generic;` (16/206 files)
- `using System.Reflection;` (8/206 files)
- `using System.Text.Json;` (7/206 files)
- `using System.Linq;` (5/206 files)
- `using System.Security.Cryptography;` (4/206 files)
- `using System.Net.Http;` (3/206 files)
- `using System.Text;` (3/206 files)
- `using System.Threading.Tasks;` (3/206 files)
- `using System.Drawing;` (2/206 files)
- `using System.Xml.Linq;` (2/206 files)
- `using NUnit.Framework;` (1/206 files)
- `using System.Diagnostics;` (1/206 files)
- `using System.Drawing.Imaging;` (1/206 files)
- `using System.Drawing.Printing;` (1/206 files)
- `using System.Runtime.Loader;` (1/206 files)
- `using System.Security.Cryptography.X509Certificates;` (1/206 files)
- `using System.Threading;` (1/206 files)
- `using System.Xml;` (1/206 files)
- `using System.Xml.Serialization;` (1/206 files)

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
| [add-annotation-and-merge-pdfs](./add-annotation-and-merge-pdfs.cs) | Add Annotation and Merge PDFs | `Document`, `TextAnnotation`, `Rectangle` | Shows how to add a text annotation to a PDF using Aspose.Pdf and then concatenate the annotated f... |
| [add-attachment-and-encrypt-pdf](./add-attachment-and-encrypt-pdf.cs) | Encrypt PDF with Attachment and Viewer Preferences | `Document`, `FileSpecification`, `Add` | Loads an existing PDF, adds an external file as an attachment, optionally configures viewer prefe... |
| [add-attachment-and-list-names](./add-attachment-and-list-names.cs) | Add and List Embedded File Attachments in PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file into a PDF using Aspose.Pdf, save the document, and then enumera... |
| [add-attachment-and-retrieve-name](./add-attachment-and-retrieve-name.cs) | Add Attachment to PDF and Verify File Name | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file as an attachment in a PDF using Aspose.Pdf, then retrieve and confirm t... |
| [add-attachment-and-set-viewer-preference](./add-attachment-and-set-viewer-preference.cs) | Add Attachment and Set Viewer Preferences in PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to embed a file as an attachment in a PDF and configure viewer preferences such as hidi... |
| [add-attachment-and-store-checksum-metadata](./add-attachment-and-store-checksum-metadata.cs) | Add Attachment Checksums to PDF Custom Metadata | `Document`, `FileSpecification`, `DocumentInfo` | The example iterates over embedded files in a PDF, computes a SHA‑256 checksum for each, and stor... |
| [add-attachment-preserve-existing](./add-attachment-preserve-existing.cs) | Add Attachment to PDF While Preserving Existing Attachments | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates how to embed a new file into an existing PDF using Aspose.Pdf without deleting any p... |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add File Attachment with UTC Modification Date to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates attaching a file to a PDF page and setting the attachment's modification date to the... |
| [add-attachment-to-multiple-pdfs](./add-attachment-to-multiple-pdfs.cs) | Add Attachment to Multiple PDFs in Batch | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load several PDF files, embed the same external file as an attachment, and sa... |
| [add-attachment-verify-extraction](./add-attachment-verify-extraction.cs) | Add and Verify Embedded File Attachment in PDF | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file into a PDF, extract it back, and compare the extracted content w... |
| [add-batch-text-annotations-to-pdf](./add-batch-text-annotations-to-pdf.cs) | Add Multiple Text Annotations to a PDF | `Document`, `PdfAnnotationEditor`, `Page` | Demonstrates how to batch‑add text annotations to a PDF document using Aspose.Pdf's Document and ... |
| [add-bold-red-centered-text-stamp-page-5](./add-bold-red-centered-text-stamp-page-5.cs) | Add Bold Red Centered Text Stamp to PDF Page | `Document`, `TextStamp`, `FindFont` | Demonstrates how to create a TextStamp with bold, red, centered text and apply it to a specific p... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Navigating to Page 5 | `Document`, `OutlineItemCollection`, `GoToAction` | Shows how to load a PDF, verify it has at least five pages, create an outline bookmark that jumps... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Create Red Confidential Text Stamp with Semi‑Transparent Bac... | `TextStamp`, `Document`, `AddStamp` | Demonstrates how to create a red "Confidential" text stamp with a semi‑transparent background and... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add XMP Corporate Branding Metadata to PDF | `Document`, `Metadata`, `Save` | Shows how to embed custom XMP metadata—including a logo URL and brand color—into a PDF file using... |
| [add-custom-xmp-metadata-projectid](./add-custom-xmp-metadata-projectid.cs) | Add Custom XMP Metadata to PDF | `Document`, `Metadata`, `Info` | Shows how to add or update a custom XMP metadata field (ProjectID) and also store it in the PDF's... |
| [add-dashed-rectangle-annotation-with-opacity](./add-dashed-rectangle-annotation-with-opacity.cs) | Add Dashed Rectangle Annotation to PDF Page | `Document`, `Page`, `SquareAnnotation` | Shows how to place a rectangle (square) annotation with a dashed border and 50% opacity on a spec... |
| [add-document-attachment-with-description](./add-document-attachment-with-description.cs) | Add File Attachment with Description to PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file as an attachment in a PDF and set a descriptive label using Aspo... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add File Attachment to PDF with Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed an external file (e.g., a DOCX) into an existing PDF using Aspose.Pdf, ... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to embed a file as an attachment in a PDF using Aspose.Pdf by creating a FileSpecificat... |
| [add-encrypted-attachment-aes256](./add-encrypted-attachment-aes256.cs) | Add Encrypted AES Attachment to PDF | `Document`, `FileSpecification`, `Save` | Demonstrates encrypting a file with AES‑256 and embedding it as an attachment in a PDF using Aspo... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add Bookmark with External URL to PDF | `Document`, `OutlineItemCollection`, `GoToURIAction` | Shows how to create an outline bookmark in a PDF that opens an external web page when selected, u... |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Link Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to attach a file to a PDF and create a clickable file‑attachment annotation that... |
| [add-file-attachment-data-relationship](./add-file-attachment-data-relationship.cs) | Add Attachment with Data Relationship to PDF | `Document`, `FileSpecification`, `AFRelationship` | Demonstrates how to embed a file into a PDF using Aspose.Pdf and set its AFRelationship to Data f... |
| [add-file-attachment-to-encrypted-pdf](./add-file-attachment-to-encrypted-pdf.cs) | Add File Attachment to Encrypted PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates opening a password‑protected PDF, embedding a file as an attachment, and saving the ... |
| [add-file-attachment-to-pdf__v3](./add-file-attachment-to-pdf__v3.cs) | Add File Attachment to PDF with Description | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed an external file (Terms.pdf) into an existing PDF and assign a human‑readable ... |
| [add-file-attachment-to-pdf__v4](./add-file-attachment-to-pdf__v4.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a local file as an attachment in an existing PDF document using Aspose.Pdf. |
| [add-file-attachment-with-creation-date](./add-file-attachment-with-creation-date.cs) | Add Attachment with Timestamp to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file as an attachment in a PDF and include the current creation date in the ... |
| [add-file-attachment-with-custom-mime](./add-file-attachment-with-custom-mime.cs) | Add Custom MIME Type Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to attach a file to a PDF and specify a custom MIME type using Aspose.Pdf. |
| [add-free-text-annotation-helvetica](./add-free-text-annotation-helvetica.cs) | Add Free‑Text Annotation with Custom Appearance to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to add a free‑text annotation to a PDF page using a Helvetica font, size 12, and... |
| ... | | | *and 176 more files* |

## Category Statistics
- Total examples: 206

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
Updated: 2026-10-26 | Run: `20261026_001223_9dc44d`
<!-- AUTOGENERATED:END -->

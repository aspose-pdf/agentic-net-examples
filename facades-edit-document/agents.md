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

- `using Aspose.Pdf;` (207/275 files) ← category-specific
- `using Aspose.Pdf.Facades;` (148/275 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (69/275 files)
- `using Aspose.Pdf.Text;` (23/275 files)
- `using Aspose.Pdf.Devices;` (11/275 files)
- `using Aspose.Pdf.Printing;` (7/275 files)
- `using Aspose.Pdf.Forms;` (3/275 files)
- `using Aspose.Pdf.Security;` (2/275 files)
- `using Aspose.Pdf.Drawing;` (1/275 files)
- `using System;` (275/275 files)
- `using System.IO;` (273/275 files)
- `using System.Collections.Generic;` (14/275 files)
- `using System.Drawing;` (13/275 files)
- `using System.Drawing.Imaging;` (8/275 files)
- `using System.Text.Json;` (5/275 files)
- `using System.Xml;` (4/275 files)
- `using NUnit.Framework;` (3/275 files)
- `using System.Collections;` (2/275 files)
- `using System.Drawing.Printing;` (2/275 files)
- `using System.Net.Http;` (2/275 files)
- `using System.Threading.Tasks;` (2/275 files)
- `using System.Net;` (1/275 files)
- `using System.Reflection;` (1/275 files)
- `using System.Runtime.InteropServices;` (1/275 files)
- `using System.Security.Cryptography;` (1/275 files)
- `using System.Threading;` (1/275 files)

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
| [add-annotation-custom-border](./add-annotation-custom-border.cs) | Add Annotation with Custom Border Thickness | `Document`, `Page`, `SquareAnnotation` | Demonstrates adding a square annotation to a PDF page and setting its border width to three point... |
| [add-attachment-batch](./add-attachment-batch.cs) | Add Same Attachment to Multiple PDFs in Batch | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to each PDF in a collection using PdfContentEditor. |
| [add-attachment-checksum](./add-attachment-checksum.cs) | Add Attachment and Store MD5 Checksum in PDF Metadata | `Document`, `PdfContentEditor`, `EmbeddedFile` | Demonstrates adding a file attachment to a PDF, retrieving its MD5 checksum via FileParams.CheckS... |
| [add-attachment-custom-mime-type](./add-attachment-custom-mime-type.cs) | Add Attachment with Custom MIME Type to PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file attachment with a custom MIME type into a PDF using Aspose.Pdf f... |
| [add-attachment-modification-date](./add-attachment-modification-date.cs) | Add File Attachment with Modification Date to PDF | `Document`, `Attachment`, `Attachments` | Demonstrates how to attach a file to a PDF and set its modification date to the current UTC time ... |
| [add-attachment-viewer-preference](./add-attachment-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF and change the viewer preference to show the attachmen... |
| [add-bookmark-to-pdf](./add-bookmark-to-pdf.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Creates a bookmark titled "Project Overview" that links to page five of a PDF document. |
| [add-bookmark-to-pdf__v2](./add-bookmark-to-pdf__v2.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Adds a bookmark titled "Project Overview" that navigates to page five of a PDF document. |
| [add-center-bold-red-text-stamp](./add-center-bold-red-text-stamp.cs) | Add Centered Bold Red Text Stamp to Page Five | `Document`, `TextStamp`, `AddStamp` | Demonstrates how to place a centered bold red text stamp on the fifth page of a PDF. |
| [add-centered-underlined-text-stamp](./add-centered-underlined-text-stamp.cs) | Add Centered Underlined Text Stamp with Yellow Background to... | `Document`, `AddStamp`, `TextStamp` | Demonstrates adding a centered, underlined text stamp with a yellow background to page 10 of a PD... |
| [add-circle-annotation](./add-circle-annotation.cs) | Add Circle Annotation with Thick Green Outline | `Document`, `Page`, `CircleAnnotation` | Demonstrates adding a circle annotation with a thick green outline around a diagram on page six o... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Red Confidential Text Stamp with Semi-Transparent Backgr... | `Document`, `TextState`, `FontRepository` | Demonstrates how to add a red "Confidential" text stamp with a semi‑transparent background to eac... |
| [add-confidential-text-stamp__v2](./add-confidential-text-stamp__v2.cs) | Add Red Confidential Text Stamp with Transparency | `Document`, `TextStamp`, `AddStamp` | Demonstrates adding a red "Confidential" text stamp with a semi‑transparent background to all pag... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add Corporate Branding XMP Metadata to PDF | `Document`, `BindPdf`, `RegisterNamespaceURI` | Demonstrates how to embed custom XMP metadata such as a logo URL and brand color into a PDF using... |
| [add-custom-schema-xmp](./add-custom-schema-xmp.cs) | Add Custom Schema to PDF XMP Metadata | `PdfXmpMetadata`, `RegisterNamespaceURI`, `Add` | Demonstrates how to register a custom namespace and add project identifier and version fields to ... |
| [add-custom-schema-xmp__v2](./add-custom-schema-xmp__v2.cs) | Add Custom Schema to PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `RegisterNamespaceURI` | Demonstrates how to register a custom namespace and add project identifier and version fields to ... |
| [add-custom-xmp-metadata](./add-custom-xmp-metadata.cs) | Add Custom XMP Metadata Field to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata field named ProjectID with value 12345 to a PDF and... |
| [add-custom-xmp-metadata__v2](./add-custom-xmp-metadata__v2.cs) | Add Custom XMP Metadata Field to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata field (ProjectID) to a PDF and save the updated doc... |
| [add-dashed-line-annotation](./add-dashed-line-annotation.cs) | Add Dashed Line Annotation to PDF | `Document`, `Page`, `LineAnnotation` | Demonstrates adding a line annotation with a custom dash pattern to a PDF page using Aspose.Pdf. |
| [add-dashed-rectangle-annotation](./add-dashed-rectangle-annotation.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates adding a rectangle annotation with a dashed border and 50% opacity on page six of a ... |
| [add-dashed-rectangle-annotation__v2](./add-dashed-rectangle-annotation__v2.cs) | Add Dashed Rectangle Annotation with Opacity to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates adding a rectangle (square) annotation with a dashed border and 50% opacity on page ... |
| [add-document-attachment-error-handling](./add-document-attachment-error-handling.cs) | Add Document Attachment with Error Handling | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates how to attach a file to a PDF using PdfContentEditor while checking that the attachm... |
| [add-document-attachment-retrieve-name](./add-document-attachment-retrieve-name.cs) | Add Document Attachment and Retrieve Its Name | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates adding a file attachment to a PDF using PdfContentEditor and then reading back the a... |
| [add-document-attachment](./add-document-attachment.cs) | Add Document Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to embed a file attachment into a PDF and provide a descriptive label using PdfC... |
| [add-document-attachment__v2](./add-document-attachment__v2.cs) | Add Document Attachment While Preserving Existing Attachment... | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates how to add a new file attachment to a PDF without deleting existing attachments usin... |
| [add-embedded-file-attachment](./add-embedded-file-attachment.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file into a PDF document using PdfContentEditor without creating a visible a... |
| [add-encrypted-attachment](./add-encrypted-attachment.cs) | Add Encrypted Attachment to PDF with AES | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-encrypted-attachment__v2](./add-encrypted-attachment__v2.cs) | Add Encrypted Attachment to PDF with AES | `BindPdf`, `AddDocumentAttachment`, `Save` | Demonstrates adding a file attachment with a custom description to a PDF and then encrypting the ... |
| [add-external-url-bookmark](./add-external-url-bookmark.cs) | Add External URL Bookmark to PDF | `PdfBookmarkEditor`, `Bookmark`, `BindPdf` | Demonstrates how to add a bookmark that opens an external URL when selected using Aspose.Pdf. |
| [add-file-attachment-annotation](./add-file-attachment-annotation.cs) | Add File Attachment Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateFileAttachment` | Demonstrates how to add a file attachment annotation that opens an attached file when the user cl... |
| ... | | | *and 245 more files* |

## Category Statistics
- Total examples: 275

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-edit-document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-31 | Run: `20260331_170055_a44d3c`
<!-- AUTOGENERATED:END -->

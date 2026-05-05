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

- `using Aspose.Pdf.Facades;` (183/209 files) ← category-specific
- `using Aspose.Pdf;` (120/209 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (26/209 files)
- `using Aspose.Pdf.Text;` (18/209 files)
- `using Aspose.Pdf.Devices;` (9/209 files)
- `using Aspose.Pdf.Printing;` (4/209 files)
- `using Aspose.Pdf.Security;` (2/209 files)
- `using Aspose.Pdf.Drawing;` (1/209 files)
- `using System;` (209/209 files)
- `using System.IO;` (197/209 files)
- `using System.Drawing;` (30/209 files)
- `using System.Collections.Generic;` (15/209 files)
- `using System.Text.Json;` (6/209 files)
- `using System.Drawing.Imaging;` (5/209 files)
- `using System.Threading.Tasks;` (4/209 files)
- `using System.Drawing.Printing;` (3/209 files)
- `using System.Net.Http;` (3/209 files)
- `using System.Runtime.InteropServices;` (3/209 files)
- `using System.Xml;` (2/209 files)
- `using System.Xml.Linq;` (2/209 files)
- `using Microsoft.VisualStudio.TestTools.UnitTesting;` (1/209 files)
- `using System.Collections;` (1/209 files)
- `using System.Runtime.Versioning;` (1/209 files)
- `using System.Security.Cryptography;` (1/209 files)
- `using System.Threading;` (1/209 files)

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
| [add-and-verify-document-attachment](./add-and-verify-document-attachment.cs) | Add and Verify Document Attachment in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF using PdfContentEditor and then retrieve the attachmen... |
| [add-attachment-set-modification-date](./add-attachment-set-modification-date.cs) | Add Attachment and Set Modification Date in PDF | `Document`, `PdfContentEditor`, `AddDocumentAttachment` | Shows how to attach a file to a PDF using PdfContentEditor and update the document's modification... |
| [add-attachment-set-viewer-preference](./add-attachment-set-viewer-preference.cs) | Add Attachment and Set Viewer Preference in PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to attach a file to a PDF and configure the viewer to open the attachments pane using A... |
| [add-attachment-set-viewer-preferences-encrypt-pdf](./add-attachment-set-viewer-preferences-encrypt-pdf.cs) | Add Attachment, Set Viewer Preferences, and Encrypt PDF | `PdfContentEditor`, `AddDocumentAttachment`, `ChangeViewerPreference` | Demonstrates adding a file attachment and changing viewer preferences with PdfContentEditor, then... |
| [add-attachment-store-checksum-metadata](./add-attachment-store-checksum-metadata.cs) | Add Attachment and Store Checksum in PDF Metadata | `Document`, `PdfContentEditor`, `BindPdf` | Demonstrates how to embed a file into a PDF, retrieve its MD5 checksum, and store that checksum i... |
| [add-attachment-verify-extraction](./add-attachment-verify-extraction.cs) | Add Attachment to PDF and Verify Extraction | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to embed a file as a document attachment using PdfContentEditor, then extract it... |
| [add-attachment-with-custom-mime-type](./add-attachment-with-custom-mime-type.cs) | Add Attachment with Custom MIME Type to PDF | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file into a PDF and specify a custom MIME type using Aspose.Pdf. |
| [add-attachment-with-description-to-pdf](./add-attachment-with-description-to-pdf.cs) | Add Attachment with Description to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed an external file into a PDF and assign a descriptive label using Aspose.Pdf.Fa... |
| [add-base64-image-stamp-to-pdf-page](./add-base64-image-stamp-to-pdf-page.cs) | Add Base64 Image Stamp to PDF Page | `Document`, `Page`, `ImageStamp` | Demonstrates how to decode a Base64‑encoded image, create an ImageStamp, and place it on the thir... |
| [add-bold-red-centered-text-stamp](./add-bold-red-centered-text-stamp.cs) | Add Bold Red Centered Text Stamp to PDF Page | `Document`, `TextState`, `TextStamp` | Demonstrates how to create a bold, red, center‑aligned text stamp and apply it only to page five ... |
| [add-bookmark-to-pdf-page](./add-bookmark-to-pdf-page.cs) | Add Bookmark to PDF Page | `PdfBookmarkEditor`, `BindPdf`, `CreateBookmarkOfPage` | Shows how to add a bookmark titled "Project Overview" that navigates to page 5 of a PDF using Asp... |
| [add-circle-annotation-green-outline](./add-circle-annotation-green-outline.cs) | Add Circle Annotation with Green Outline to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateSquareCircle` | Shows how to use Aspose.Pdf.Facades.PdfContentEditor to place a circular annotation with a thick ... |
| [add-confidential-text-stamp](./add-confidential-text-stamp.cs) | Add Confidential Red Text Stamp with Transparency | `Document`, `TextStamp`, `FindFont` | Demonstrates how to load a PDF, create a red "Confidential" text stamp with 50% opacity, and appl... |
| [add-confirmation-dialog-to-pdf-submit-button](./add-confirmation-dialog-to-pdf-submit-button.cs) | Add Confirmation Dialog to PDF Submit Button | `FormEditor`, `BindPdf`, `AddSubmitBtn` | Shows how to insert a submit button into a PDF form and attach JavaScript that displays a confirm... |
| [add-corporate-branding-xmp-metadata](./add-corporate-branding-xmp-metadata.cs) | Add Corporate Branding XMP Metadata to PDF | `Document`, `XmpMetadata`, `RegisterNamespaceUri` | Demonstrates how to register a custom XMP namespace and add corporate branding information such a... |
| [add-custom-appearance-stream-to-rubber-stamp](./add-custom-appearance-stream-to-rubber-stamp.cs) | Add Custom Appearance Stream to Rubber Stamp Annotation | `PdfContentEditor`, `BindPdf`, `CreateRubberStamp` | Shows how to use PdfContentEditor to add a rubber‑stamp annotation with a custom appearance PDF s... |
| [add-custom-project-metadata-to-pdf-xmp](./add-custom-project-metadata-to-pdf-xmp.cs) | Add Custom Project Metadata to PDF XMP | `PdfXmpMetadata`, `BindPdf`, `RegisterNamespaceURI` | Shows how to bind XMP metadata to an existing PDF, register a custom namespace, and add project I... |
| [add-custom-xmp-metadata-field-to-pdf](./add-custom-xmp-metadata-field-to-pdf.cs) | Add Custom XMP Metadata Field to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add a custom XMP metadata entry (ProjectID) to a PDF and verify it appears in... |
| [add-dashed-line-annotation](./add-dashed-line-annotation.cs) | Add Dashed Line Annotation to PDF | `PdfContentEditor`, `BindPdf`, `CreateLine` | Demonstrates using Aspose.Pdf.Facades.PdfContentEditor to create a line annotation with a custom ... |
| [add-dashed-rectangle-annotation-opacity](./add-dashed-rectangle-annotation-opacity.cs) | Add Dashed Rectangle Annotation with 50% Opacity to PDF Page | `PdfContentEditor`, `BindPdf`, `CreateSquareCircle` | Demonstrates how to add a rectangle (square) annotation on page six of a PDF, set its border to a... |
| [add-department-xmp-metadata-to-pdf](./add-department-xmp-metadata-to-pdf.cs) | Add Department XMP Metadata to PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Shows how to bind an existing PDF, add a custom XMP property named Department with the value Fina... |
| [add-document-attachment-and-list-attachments](./add-document-attachment-and-list-attachments.cs) | Add Document Attachment and List Attachments in PDF | `PdfContentEditor`, `AddDocumentAttachment`, `Save` | Demonstrates adding a file as a document attachment to a PDF using PdfContentEditor and then retr... |
| [add-document-attachment-to-pdf](./add-document-attachment-to-pdf.cs) | Add Document Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file (Terms.pdf) to an existing PDF using Aspose.Pdf.Facad... |
| [add-document-attachment-to-pdf__v2](./add-document-attachment-to-pdf__v2.cs) | Add Document Attachment to PDF without Removing Existing Att... | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to add a new file attachment to an existing PDF using Aspose.Pdf.Facades.PdfCont... |
| [add-document-attachment-with-error-handling](./add-document-attachment-with-error-handling.cs) | Add Document Attachment to PDF with Error Handling | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach an external file to a PDF using Aspose.Pdf.Facades.PdfContentEditor wh... |
| [add-document-attachment-with-mime-type](./add-document-attachment-with-mime-type.cs) | Add Document Attachment to PDF with Custom MIME Type | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | This example shows how to attach an external PDF file to an existing PDF document using Aspose.Pd... |
| [add-embedded-file-attachment-to-pdf](./add-embedded-file-attachment-to-pdf.cs) | Add Embedded File Attachment to PDF | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Shows how to embed a file as an attachment in a PDF document using the PdfContentEditor facade. |
| [add-encrypted-attachment-aes](./add-encrypted-attachment-aes.cs) | Add Encrypted Attachment and Secure PDF with AES | `PdfContentEditor`, `BindPdf`, `AddDocumentAttachment` | Demonstrates how to attach a file to a PDF with a custom description using PdfContentEditor, then... |
| [add-external-url-bookmark-to-pdf](./add-external-url-bookmark-to-pdf.cs) | Add External URL Bookmark to PDF | `PdfContentEditor`, `BindPdf`, `CreateBookmarksAction` | Shows how to bind an existing PDF, create a bookmark that opens an external URL, and save the upd... |
| [add-file-attachment-data-relationship](./add-file-attachment-data-relationship.cs) | Add File Attachment with Data Relationship to PDF | `PdfContentEditor`, `CreateFileAttachment`, `Page` | Shows how to add a file attachment annotation to a PDF page using PdfContentEditor and set its AF... |
| ... | | | *and 179 more files* |

## Category Statistics
- Total examples: 209

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
Updated: 2026-05-05 | Run: `20260505_163812_2ca013`
<!-- AUTOGENERATED:END -->

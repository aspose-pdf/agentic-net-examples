---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

> **Working with attachments** in PDF using C# / .NET -- **49** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-attachments** category.
This folder contains standalone C# examples for working-with-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (48/49 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (14/49 files)
- `using Aspose.Pdf.Drawing;` (2/49 files)
- `using Aspose.Pdf.AI;` (1/49 files)
- `using Aspose.Pdf.Devices;` (1/49 files)
- `using Aspose.Pdf.Optimization;` (1/49 files)
- `using Aspose.Pdf.Text;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (48/49 files)
- `using System.Collections.Generic;` (7/49 files)
- `using System.Threading.Tasks;` (2/49 files)
- `using NUnit.Framework;` (1/49 files)
- `using System.Diagnostics;` (1/49 files)
- `using System.IO.Compression;` (1/49 files)
- `using System.Linq;` (1/49 files)
- `using System.Net.Http;` (1/49 files)
- `using System.Reflection;` (1/49 files)
- `using System.Security.Cryptography;` (1/49 files)
- `using System.Text.Json;` (1/49 files)
- `using System.Threading;` (1/49 files)
- `using System.Xml.Linq;` (1/49 files)

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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates opening an encrypted PDF with a user password, embedding a file as an attachment, an... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `Save`, `FileSpecification` | Demonstrates verifying the existence of a PDF and an attachment file, then adding the attachment ... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load an existing PDF, create a FileSpecification for a file, embed it as a Fi... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to PDF and Save to Output Folder | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading a PDF, embedding an external file as an attachment using FileSpecification, ... |
| [add-file-attachment-with-mime-validation](./add-file-attachment-with-mime-validation.cs) | Add File Attachment with MIME Type Validation to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | The example verifies that a file's extension maps to an allowed MIME type before embedding it as ... |
| [add-file-attachments-with-custom-description](./add-file-attachments-with-custom-description.cs) | Add File Attachments with Custom Description and MIME Type | `Document`, `Save`, `Page` | Demonstrates how to attach files to a PDF, set a custom description (tooltip) and let Aspose.Pdf ... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF using Aspose.Pdf | `Document`, `Add`, `FileSpecification` | Demonstrates how to embed a file into a PDF directly from a MemoryStream, avoiding any temporary ... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to load a PDF with Aspose.Pdf, iterate a collection of file paths, embed each file as a... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to PDF Portfolio | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Creates a PDF portfolio and embeds every file from a specified folder into the document using a s... |
| [add-nested-pdf-to-portfolio](./add-nested-pdf-to-portfolio.cs) | Add Nested PDF to an Existing PDF Portfolio | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed another PDF file as a nested item inside an existing PDF Portfolio usin... |
| [add-pdf-attachment-with-retry](./add-pdf-attachment-with-retry.cs) | Add File Attachment to PDF with Retry on Network Share | `Document`, `FileSpecification`, `Rectangle` | Demonstrates how to attach a file to the first page of a PDF stored on a network share and implem... |
| [add-thumbnails-to-pdf-pages](./add-thumbnails-to-pdf-pages.cs) | Add Thumbnails to PDF Pages | `Document`, `Page`, `ThumbnailDevice` | Demonstrates generating a thumbnail image for each page of a PDF using Aspose.Pdf's ThumbnailDevi... |
| [add-unicode-filename-attachment-to-pdf](./add-unicode-filename-attachment-to-pdf.cs) | Add Unicode Filename Attachment to PDF | `Document`, `Page`, `Rectangle` | Shows how to attach a file to a PDF with a Unicode filename using Aspose.Pdf, creating a file att... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a Word (.docx) file into a PDF portfolio using Aspose.Pdf's EmbeddedFiles coll... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Page`, `Graph` | Shows how to add a full‑page graphical background and a header text to each page of an existing P... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF as File Attachment Annotation | `Document`, `Page`, `Rectangle` | Downloads a file from a remote URL into memory and embeds it in a PDF as a file attachment annota... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to loop through a directory of PDF files and embed the same external file as an attachm... |
| [batch-extract-pdf-attachments-zip](./batch-extract-pdf-attachments-zip.cs) | Batch Extract PDF Attachments and Create a ZIP Archive | `Document`, `EmbeddedFiles`, `EmbeddedFileCollection` | Shows how to iterate over a folder of PDFs, extract embedded files with Aspose.Pdf, and package a... |
| [compress-pdf-portfolio-optimization](./compress-pdf-portfolio-optimization.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `OptimizeResources` | Loads an existing PDF portfolio, enables object compression via OptimizationOptions, and saves th... |
| [create-pdf-portfolio-with-embedded-files](./create-pdf-portfolio-with-embedded-files.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Demonstrates how to convert an existing PDF into a portfolio by adding multiple embedded files us... |
| [create-pdf-portfolio](./create-pdf-portfolio.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Shows how to create a PDF portfolio and embed existing files such as PDFs, DOCX, and images as at... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFiles`, `Delete` | Demonstrates how to remove an embedded file from a PDF by specifying its filename using Aspose.Pdf. |
| [delete-pdf-outline-items-by-title](./delete-pdf-outline-items-by-title.cs) | Delete PDF Outline Items by Title | `Document`, `OutlineCollection`, `Delete` | Demonstrates how to remove outline (bookmark) entries from a PDF by matching their title text ins... |
| [embed-attachment-and-metadata](./embed-attachment-and-metadata.cs) | Embed Attachment and Custom Metadata into PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file as an attachment in a PDF and add custom metadata about the atta... |
| [embed-file-from-byte-array-into-pdf](./embed-file-from-byte-array-into-pdf.cs) | Embed a File from Byte Array into a PDF | `Document`, `Page`, `FileSpecification` | Demonstrates how to create a FileSpecification from a byte array using a MemoryStream and attach ... |
| [embed-hidden-xml-metadata-attachment](./embed-hidden-xml-metadata-attachment.cs) | Embed Hidden XML Metadata as PDF Attachment | `Document`, `Page`, `FileSpecification` | Shows how to serialize XML metadata to a memory stream, wrap it in a FileSpecification, and embed... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Display Name | `Document`, `Collection`, `FileSpecification` | Demonstrates how to create a PDF portfolio, embed an image file as an attachment, and set a custo... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `Decrypt`, `EmbeddedFiles` | Shows how to open an encrypted PDF with a password, decrypt it, and extract all embedded file att... |
| [extract-embedded-attachment-from-pdf](./extract-embedded-attachment-from-pdf.cs) | Extract Embedded Attachment from PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to locate an embedded file in a PDF by its name and save it to a target directory using... |
| [extract-embedded-files-from-pdf-portfolio](./extract-embedded-files-from-pdf-portfolio.cs) | Extract Embedded Files from PDF Portfolio | `Document`, `EmbeddedFileCollection`, `EmbeddedFile` | Demonstrates how to load a PDF portfolio with Aspose.Pdf, enumerate its embedded files, preserve ... |
| ... | | | *and 19 more files* |

## Category Statistics
- Total examples: 49

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.EmbeddedFileCollection`
- `Aspose.Pdf.EmbeddedFilesCollection`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.FileEncoding`
- `Aspose.Pdf.FileSpecification`
- `Aspose.Pdf.FileSpecification.Params`
- `Aspose.Pdf.FileSpecificationParams`
- `PdfContentEditor.AddDocumentAttachment`
- `PdfContentEditor.BindPdf`
- `PdfContentEditor.Save`

### Rules
- Create a {attachment_file} FileSpecification with a {string_literal} description and add it to {doc}.EmbeddedFiles via the Add method to embed the file in the PDF.
- After modifying the attachment collection, persist changes by calling {doc}.Save({output_pdf}).
- Bind a PDF document with PdfContentEditor.BindPdf({input_pdf}) before performing any edit operations.
- Add a file attachment using PdfContentEditor.AddDocumentAttachment({attachment_file}, {string_literal}) where the second argument is the attachment description.
- Persist the changes by calling PdfContentEditor.Save({output_pdf}).

### Warnings
- The EmbeddedFiles collection is lazily instantiated; ensure {doc}.EmbeddedFiles is not null before adding.
- FileSpecification constructor expects the source file to exist on disk.
- AddDocumentAttachment only supports attaching external files; other attachment types are not covered in this example.
- The example assumes the PDF contains an EmbeddedFiles collection; calling Delete() on an empty collection is safe but may be unnecessary.
- The source file referenced in the FileSpecification must exist on disk; otherwise an exception will be thrown.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-attachments patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

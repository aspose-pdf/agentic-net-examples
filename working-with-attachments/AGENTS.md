---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

> **Working with attachments** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-attachments** category.
This folder contains standalone C# examples for working-with-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (49/50 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (13/50 files)
- `using Aspose.Pdf.Drawing;` (2/50 files)
- `using Aspose.Pdf.Text;` (2/50 files)
- `using Aspose.Pdf.Devices;` (1/50 files)
- `using Aspose.Pdf.Facades;` (1/50 files)
- `using Aspose.Pdf.Optimization;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (49/50 files)
- `using System.Collections.Generic;` (8/50 files)
- `using NUnit.Framework;` (1/50 files)
- `using System.Collections.Concurrent;` (1/50 files)
- `using System.Diagnostics;` (1/50 files)
- `using System.IO.Compression;` (1/50 files)
- `using System.Net.Http;` (1/50 files)
- `using System.Security.Cryptography;` (1/50 files)
- `using System.Text.Json;` (1/50 files)
- `using System.Threading;` (1/50 files)
- `using System.Threading.Tasks;` (1/50 files)
- `using System.Xml.Linq;` (1/50 files)

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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `Add` | Shows how to open an encrypted PDF with a user password, embed a file as an attachment, and save ... |
| [add-attachment-to-pdf-and-save](./add-attachment-to-pdf-and-save.cs) | Add Attachment to PDF and Save to Output Folder | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to load a PDF, embed a file as an attachment, and save the updated document to a specif... |
| [add-custom-descriptions-to-pdf-attachments](./add-custom-descriptions-to-pdf-attachments.cs) | Add Custom Descriptions to PDF Attachments | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed files into a PDF and assign a custom description (and MIME type) to each attac... |
| [add-extract-remove-pdf-attachments](./add-extract-remove-pdf-attachments.cs) | Add, Extract, and Remove PDF Attachments with Timing | `Document`, `Page`, `FileSpecification` | Demonstrates how to add a file attachment annotation to a PDF, extract the attached file, and rem... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `Page`, `Rectangle` | Demonstrates how to attach a file to a PDF using Aspose.Pdf while checking for missing source fil... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment Annotation to PDF | `Document`, `Page`, `Rectangle` | Demonstrates loading an existing PDF, creating a FileSpecification and a FileAttachmentAnnotation... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry Logic | `Document`, `Page`, `FileSpecification` | Demonstrates attaching a file to a PDF using Aspose.Pdf and implementing retry handling for I/O e... |
| [add-image-watermark-to-pdf-pages](./add-image-watermark-to-pdf-pages.cs) | Add Image Watermark to All PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Shows how to load a PDF with Aspose.Pdf, iterate through each page, create a WatermarkArtifact fr... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF Using Aspose.Pdf | `Document`, `Add`, `FileSpecification` | Demonstrates how to embed a file into a PDF using a MemoryStream, avoiding any intermediate disk ... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed several files into a PDF by iterating a collection of file paths and sa... |
| [add-nested-pdf-to-portfolio](./add-nested-pdf-to-portfolio.cs) | Add Nested PDF to Existing PDF Portfolio | `Document`, `Collection`, `FileSpecification` | Shows how to embed a PDF file as a nested item inside an existing PDF Portfolio (collection) usin... |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode File Attachment to PDF | `Document`, `Page`, `FileSpecification` | Demonstrates creating a PDF (if missing), attaching a file with a Unicode display name using a Fi... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add a Word Document to a PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a Word (.docx) file into a PDF document using Aspose.Pdf, turning the PDF into... |
| [apply-visual-template-to-pdf-portfolio](./apply-visual-template-to-pdf-portfolio.cs) | Apply Visual Template to PDF Portfolio Pages | `Document`, `Page`, `Graph` | Demonstrates how to load an existing PDF Portfolio, add a semi‑transparent header graphic and tit... |
| [attach-binary-file-to-pdf](./attach-binary-file-to-pdf.cs) | Attach Binary File to PDF from Byte Array | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates creating a FileSpecification from a byte array, embedding it in a PDF document, and ... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF as File Attachment Annotation | `Document`, `Page`, `Rectangle` | Downloads a file from a remote URL into memory and embeds it in a PDF as a file‑attachment annota... |
| [attachment-removal-integration-tests](./attachment-removal-integration-tests.cs) | Attachment Removal Integration Tests |  | Shows how to write NUnit integration tests that verify removing, clearing, and removing by index ... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to iterate through a folder of PDF files and embed the same attachment into each... |
| [batch-extract-pdf-attachments-to-zip](./batch-extract-pdf-attachments-to-zip.cs) | Batch Extract PDF Attachments to ZIP Archive | `Document`, `EmbeddedFiles`, `EmbeddedFile` | Demonstrates how to iterate through multiple PDF files, extract all embedded file attachments, an... |
| [compress-pdf-portfolio-with-optimization](./compress-pdf-portfolio-with-optimization.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `OptimizeResources` | Shows how to open an existing PDF Portfolio, enable object compression via OptimizationOptions, a... |
| [convert-pdf-to-portfolio-with-embedded-files](./convert-pdf-to-portfolio-with-embedded-files.cs) | Convert PDF to Portfolio with Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load a regular PDF, embed multiple files, and automatically convert it into a... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Attachments | `Document`, `Page`, `TextFragment` | Demonstrates how to build a PDF Portfolio by creating a document, adding a page with text, embedd... |
| [create-pdf-portfolio-with-multiple-embedded-files](./create-pdf-portfolio-with-multiple-embedded-files.cs) | Create PDF Portfolio with Multiple Embedded Files | `Document`, `FileSpecification`, `Add` | Demonstrates how to add several files of different types to a PDF portfolio in a single loop usin... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFileCollection`, `Delete` | Demonstrates how to remove an embedded file from a PDF by specifying its filename using Aspose.Pdf. |
| [delete-pdf-outline-items-matching-text](./delete-pdf-outline-items-matching-text.cs) | Delete PDF Outline Items Matching Text | `Document`, `OutlineItemCollection`, `OutlineCollection` | Loads a PDF, searches its outline (bookmark) entries for a specific description text, and removes... |
| [embed-attachment-metadata-in-pdf](./embed-attachment-metadata-in-pdf.cs) | Embed Attachment and Add Metadata to PDF Document Info | `Document`, `FileSpecification`, `DocumentInfo` | Demonstrates how to embed a file into a PDF using Aspose.Pdf, store custom attachment metadata in... |
| [embed-hidden-xml-metadata-attachment](./embed-hidden-xml-metadata-attachment.cs) | Embed Hidden XML Metadata as PDF Attachment | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Creates or loads a PDF, builds an XML document with metadata, serializes it to a stream, and embe... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Display Name | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed an image file into a PDF portfolio and assign a custom display name usi... |
| [extract-attachment-by-name-from-pdf](./extract-attachment-by-name-from-pdf.cs) | Extract Attachment by Name from PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | The example creates a PDF with an embedded file, then loads the PDF, locates the attachment by it... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `Decrypt`, `EmbeddedFiles` | Shows how to open an encrypted PDF with a password, decrypt it, and extract all embedded file att... |
| ... | | | *and 20 more files* |

## Category Statistics
- Total examples: 50

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

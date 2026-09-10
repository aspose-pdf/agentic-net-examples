---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

> **Working with attachments** in PDF using C# / .NET -- **69** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-attachments** category.
This folder contains standalone C# examples for working-with-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (48/69 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (16/69 files)
- `using Aspose.Pdf.Drawing;` (2/69 files)
- `using Aspose.Pdf.AI;` (1/69 files)
- `using Aspose.Pdf.Devices;` (1/69 files)
- `using Aspose.Pdf.Facades;` (1/69 files)
- `using Aspose.Pdf.Optimization;` (1/69 files)
- `using Aspose.Pdf.Text;` (1/69 files)
- `using System;` (49/69 files)
- `using System.IO;` (48/69 files)
- `using System.Collections.Generic;` (6/69 files)
- `using NUnit.Framework;` (2/69 files)
- `using System.Threading.Tasks;` (2/69 files)
- `using System.Diagnostics;` (1/69 files)
- `using System.IO.Compression;` (1/69 files)
- `using System.Net.Http;` (1/69 files)
- `using System.Security.Cryptography;` (1/69 files)
- `using System.Text;` (1/69 files)
- `using System.Text.Json;` (1/69 files)
- `using System.Threading;` (1/69 files)
- `using System.Xml.Serialization;` (1/69 files)

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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open an encrypted PDF with a user password, embed a file as an attachment, and save ... |
| [add-attachment-to-pdf-and-save](./add-attachment-to-pdf-and-save.cs) | Add attachment to pdf and save |  | Add attachment to pdf and save |
| [add-custom-descriptions-to-pdf-attachments](./add-custom-descriptions-to-pdf-attachments.cs) | Add custom descriptions to pdf attachments |  | Add custom descriptions to pdf attachments |
| [add-extract-remove-pdf-attachments](./add-extract-remove-pdf-attachments.cs) | Add extract remove pdf attachments |  | Add extract remove pdf attachments |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to attach a file to a PDF page using Aspose.Pdf and includes robust error handli... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to load an existing PDF, create a FileSpecification for a file, attach it to a p... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry Logic | `Document`, `Page`, `FileSpecification` | Shows how to attach a file to the first page of a PDF located on a network share using Aspose.Pdf... |
| [add-image-watermark-to-pdf-attachments](./add-image-watermark-to-pdf-attachments.cs) | Add Image Watermark to PDF Attachments | `Document`, `FileSpecification`, `ImageStamp` | Shows how to iterate through embedded PDF attachments, apply an image watermark to every page of ... |
| [add-image-watermark-to-pdf-pages](./add-image-watermark-to-pdf-pages.cs) | Add image watermark to pdf pages |  | Add image watermark to pdf pages |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file stored in a MemoryStream as an attachment in a PDF using Aspose.... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `Add` | Shows how to embed several files into an existing PDF by iterating a list of file paths and savin... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to a PDF Portfolio | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Demonstrates how to embed several files of different formats into a single PDF portfolio using As... |
| [add-nested-pdf-to-pdf-portfolio](./add-nested-pdf-to-pdf-portfolio.cs) | Add Nested PDF to Existing PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates how to embed a PDF file as a nested item inside an existing PDF portfolio using Aspo... |
| [add-nested-pdf-to-portfolio](./add-nested-pdf-to-portfolio.cs) | Add nested pdf to portfolio |  | Add nested pdf to portfolio |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode File Attachment to PDF | `Document`, `Page`, `Rectangle` | Shows how to embed a file with a Unicode (e.g., Chinese) filename into a PDF as a file‑attachment... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `Collection`, `FileSpecification` | Demonstrates how to create a PDF portfolio and embed a Word (.docx) file using Aspose.Pdf's Colle... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Page`, `Graph` | Shows how to load a PDF Portfolio and apply a visual template to each page by adding a semi‑trans... |
| [apply-visual-template-to-pdf-portfolio](./apply-visual-template-to-pdf-portfolio.cs) | Apply visual template to pdf portfolio |  | Apply visual template to pdf portfolio |
| [attach-binary-file-to-pdf](./attach-binary-file-to-pdf.cs) | Attach binary file to pdf |  | Attach binary file to pdf |
| [attach-file-from-byte-array-to-pdf](./attach-file-from-byte-array-to-pdf.cs) | Attach a File from a Byte Array to a PDF | `Document`, `Page`, `FileSpecification` | Demonstrates how to create a FileSpecification from a byte array using a MemoryStream and add it ... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF | `Document`, `Page`, `Rectangle` | Downloads a file from a remote URL into memory and adds it as a file attachment annotation to an ... |
| [attachment-removal-integration-tests](./attachment-removal-integration-tests.cs) | Attachment removal integration tests |  | Attachment removal integration tests |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to iterate through a folder of PDF files, attach the same external file to each documen... |
| [batch-extract-pdf-attachments-to-zip](./batch-extract-pdf-attachments-to-zip.cs) | Batch Extract PDF Attachments to a ZIP Archive | `Document`, `EmbeddedFilesCollection`, `EmbeddedFile` | Shows how to iterate over multiple PDF files, retrieve their embedded attachments with Aspose.Pdf... |
| [compress-pdf-portfolio-with-optimization](./compress-pdf-portfolio-with-optimization.cs) | Compress PDF Portfolio and Save with Optimization | `Document`, `OptimizationOptions`, `OptimizeResources` | Shows how to load an existing PDF Portfolio, enable object compression using OptimizationOptions,... |
| [convert-pdf-to-portfolio-with-embedded-files](./convert-pdf-to-portfolio-with-embedded-files.cs) | Convert pdf to portfolio with embedded files |  | Convert pdf to portfolio with embedded files |
| [create-pdf-portfolio-embed-files](./create-pdf-portfolio-embed-files.cs) | Create PDF Portfolio by Embedding Files | `Document`, `FileSpecification`, `Pages` | Demonstrates how to convert an existing PDF into a PDF Portfolio by adding multiple embedded file... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Attachments | `Document`, `Collection`, `FileSpecification` | Demonstrates how to create a PDF portfolio document and embed files as attachments using Aspose.P... |
| [create-pdf-portfolio-with-multiple-embedded-files](./create-pdf-portfolio-with-multiple-embedded-files.cs) | Create pdf portfolio with multiple embedded files |  | Create pdf portfolio with multiple embedded files |
| [delete-outline-items-by-text](./delete-outline-items-by-text.cs) | Delete PDF Outline Items by Matching Text | `Document`, `OutlineCollection`, `OutlineItemCollection` | Demonstrates loading a PDF, searching its outline (bookmark) entries for a specific text, and rem... |
| ... | | | *and 39 more files* |

## Category Statistics
- Total examples: 69

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

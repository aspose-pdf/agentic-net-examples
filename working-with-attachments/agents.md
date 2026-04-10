---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

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
- `using Aspose.Pdf.Annotations;` (12/50 files)
- `using Aspose.Pdf.Drawing;` (2/50 files)
- `using Aspose.Pdf.AI;` (1/50 files)
- `using Aspose.Pdf.Devices;` (1/50 files)
- `using Aspose.Pdf.Optimization;` (1/50 files)
- `using Aspose.Pdf.Text;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (49/50 files)
- `using System.Collections.Generic;` (6/50 files)
- `using NUnit.Framework;` (2/50 files)
- `using System.Threading.Tasks;` (2/50 files)
- `using System.Diagnostics;` (1/50 files)
- `using System.IO.Compression;` (1/50 files)
- `using System.Net.Http;` (1/50 files)
- `using System.Security.Cryptography;` (1/50 files)
- `using System.Text.Json;` (1/50 files)
- `using System.Threading;` (1/50 files)
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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `Save` | Demonstrates opening a password‑protected PDF with Aspose.Pdf, embedding a file as an attachment,... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `Page`, `Rectangle` | The example loads a PDF, checks that both the PDF and attachment files exist, creates a file atta... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF Using FileSpecification | `Document`, `Save`, `Page` | Shows how to load an existing PDF and embed a single file attachment by creating a FileAttachment... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry | `Document`, `Save`, `FileSpecification` | Demonstrates how to attach a file to a PDF stored on a network share and implement a retry mechan... |
| [add-file-attachment-with-size-validation](./add-file-attachment-with-size-validation.cs) | Add File Attachment to PDF with Size Validation | `Document`, `Save`, `Page` | The example checks that a file does not exceed a 5 MB limit before attaching it to a PDF as a fil... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF Using MemoryStream | `Document`, `Save`, `FileSpecification` | Demonstrates how to load a PDF from a MemoryStream, create an attachment entirely in memory, embe... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to PDF | `Document`, `FileSpecification`, `Save` | Demonstrates loading a PDF, validating file paths, and embedding several external files as attach... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to a PDF Portfolio | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed several files of different types into a single PDF portfolio using Aspo... |
| [add-nested-pdf-to-portfolio](./add-nested-pdf-to-portfolio.cs) | Add Nested PDF to an Existing PDF Portfolio | `Document`, `FileSpecification`, `Save` | Shows how to embed a PDF file as a nested item inside an existing PDF Portfolio using Aspose.Pdf. |
| [add-remote-file-attachment-to-pdf](./add-remote-file-attachment-to-pdf.cs) | Add Remote File Attachment to PDF | `Document`, `Page`, `Rectangle` | Downloads a file from a remote URL into memory and embeds it into an existing PDF as a file attac... |
| [add-text-watermark-to-pdf-pages](./add-text-watermark-to-pdf-pages.cs) | Add Text Watermark to PDF Pages | `Document`, `Save`, `Page` | Loads a PDF document, adds a semi‑transparent text watermark to each page using a WatermarkArtifa... |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode File Attachment to PDF | `Document`, `Save`, `Add` | Shows how to embed a file with a Unicode filename into a PDF and display it using a file attachme... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a Word (.docx) file into a PDF portfolio using Aspose.Pdf's Document.Co... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Save`, `Page` | The example loads a PDF Portfolio, adds a background rectangle, a header banner, and a text label... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification` | Shows how to loop through a directory of PDF files and embed the same external file as an attachm... |
| [batch-extract-pdf-attachments-to-zip](./batch-extract-pdf-attachments-to-zip.cs) | Batch Extract PDF Attachments to a ZIP Archive | `Document`, `FileSpecification` | The example loads each PDF in a folder, iterates over its embedded files, and writes those attach... |
| [compress-pdf-portfolio-using-optimization](./compress-pdf-portfolio-using-optimization.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `OptimizeResources` | Loads an existing PDF portfolio, enables object compression via OptimizationOptions, and saves th... |
| [create-pdf-portfolio-with-embedded-files](./create-pdf-portfolio-with-embedded-files.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Shows how to convert an existing PDF into a PDF Portfolio by adding multiple embedded files using... |
| [create-pdf-portfolio](./create-pdf-portfolio.cs) | Create PDF Portfolio and Add Files | `Document`, `Collection`, `FileSpecification` | Demonstrates how to create a PDF Portfolio with Aspose.Pdf, add multiple files as attachments, se... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `Delete`, `Save` | Demonstrates how to remove an embedded file from a PDF by specifying its filename using Aspose.Pdf. |
| [delete-pdf-outline-items-by-description](./delete-pdf-outline-items-by-description.cs) | Delete PDF Outline Items by Description | `Document`, `OutlineItemCollection`, `Title` | Demonstrates loading a PDF with Aspose.Pdf, finding outline (bookmark) entries whose titles match... |
| [embed-attachment-metadata-into-pdf](./embed-attachment-metadata-into-pdf.cs) | Embed Attachment and Metadata into PDF | `Document`, `FileSpecification`, `PdfSaveOptions` | Shows how to add a file attachment to a PDF and store attachment metadata in the document informa... |
| [embed-file-attachment-in-pdf](./embed-file-attachment-in-pdf.cs) | Embed a File as an Attachment in a PDF | `Document`, `Save`, `Page` | Shows how to create a FileSpecification from a byte array and add it as a file attachment annotat... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Display Name | `Document`, `FileSpecification` | Shows how to embed an image file into a PDF portfolio and assign a custom display name for the at... |
| [embed-xml-metadata-hidden-attachment](./embed-xml-metadata-hidden-attachment.cs) | Embed XML Metadata as Hidden Attachment in PDF | `Document`, `Page`, `FileAttachmentAnnotation` | Demonstrates how to serialize XML metadata, embed it as a file attachment, and hide the annotatio... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `Decrypt()`, `EmbeddedFiles` | Opens an encrypted PDF with a password, decrypts it, and extracts all embedded file attachments t... |
| [extract-embedded-file-by-index](./extract-embedded-file-by-index.cs) | Extract Embedded File from PDF by Index | `Document`, `FileSpecification` | Demonstrates how to load a PDF, locate an embedded file by its 1‑based index, and save the extrac... |
| [extract-embedded-files-from-pdf-portfolio](./extract-embedded-files-from-pdf-portfolio.cs) | Extract Embedded Files from PDF Portfolio | `Document`, `FileSpecification` | Loads a PDF portfolio, iterates through its embedded files, and writes each file to a structured ... |
| [extract-embedded-files-from-pdfs-in-parallel](./extract-embedded-files-from-pdfs-in-parallel.cs) | Extract Embedded Files from PDFs in Parallel | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load multiple PDF documents concurrently, enumerate their embedded files, and... |
| [extract-pdf-attachment-metadata](./extract-pdf-attachment-metadata.cs) | Extract PDF Attachment Metadata to JSON | `Document`, `Page`, `Annotation` | Demonstrates how to iterate through a PDF document, locate file attachment annotations, retrieve ... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-attachments patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_121416_bd35e2`
<!-- AUTOGENERATED:END -->

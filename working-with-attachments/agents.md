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
- `using Aspose.Pdf.Annotations;` (15/50 files)
- `using Aspose.Pdf.Text;` (2/50 files)
- `using Aspose.Pdf.Devices;` (1/50 files)
- `using Aspose.Pdf.Drawing;` (1/50 files)
- `using Aspose.Pdf.Facades;` (1/50 files)
- `using Aspose.Pdf.Optimization;` (1/50 files)
- `using Aspose.Pdf.Tagged;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (49/50 files)
- `using System.Collections.Generic;` (6/50 files)
- `using System.Threading.Tasks;` (2/50 files)
- `using NUnit.Framework;` (1/50 files)
- `using System.Diagnostics;` (1/50 files)
- `using System.Drawing;` (1/50 files)
- `using System.IO.Compression;` (1/50 files)
- `using System.Linq;` (1/50 files)
- `using System.Net.Http;` (1/50 files)
- `using System.Reflection;` (1/50 files)
- `using System.Security.Cryptography;` (1/50 files)
- `using System.Text.Json;` (1/50 files)
- `using System.Threading;` (1/50 files)
- `using System.Xml.Linq;` (1/50 files)
- `using Xunit;` (1/50 files)

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
| [add-attachment-to-protected-pdf](./add-attachment-to-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open an encrypted PDF with a user password, embed a file attachment, and save the do... |
| [add-attachments-to-pdf-save-output](./add-attachments-to-pdf-save-output.cs) | Add Attachments to PDF and Save to Output Folder | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed files as attachments in a PDF document using Aspose.Pdf and save the mo... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to load an existing PDF, embed a file using FileSpecification, and create a file... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to PDF with Error Handling | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, checking for missing files, creating a file attachment annotation, an... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry | `Document`, `Page`, `FileSpecification` | Demonstrates how to attach a file to the first page of a PDF stored on a network share and retry ... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF | `Document`, `FileSpecification`, `Add` | Demonstrates embedding a file into an existing PDF using a MemoryStream, avoiding intermediate di... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to attach several files to an existing PDF by iterating a list of file paths and saving... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to a PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates embedding various file types into a single PDF portfolio using a loop with Aspose.Pdf. |
| [add-nested-pdf-to-portfolio](./add-nested-pdf-to-portfolio.cs) | Add Nested PDF to Existing PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a PDF file as a nested item inside an existing PDF Portfolio using Aspo... |
| [add-unicode-attachment-to-pdf](./add-unicode-attachment-to-pdf.cs) | Add Unicode File Attachment to PDF | `Document`, `Page`, `Rectangle` | Demonstrates embedding a file with a Unicode filename as a file attachment annotation in a PDF us... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `Collection`, `FileSpecification` | Shows how to embed a Word (.docx) file into a PDF portfolio by creating a FileSpecification and a... |
| [apply-custom-template-to-pdf-portfolio](./apply-custom-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Page`, `Graph` | Loads an existing PDF portfolio, adds a semi‑transparent background rectangle and a header text t... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF as a File Annotation | `Document`, `Page`, `Rectangle` | Downloads a file from a remote URL into memory and embeds it in a PDF as a file‑attachment annota... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Shows how to iterate over a folder of PDF files and embed the same attachment into each document ... |
| [batch-add-watermark-to-pdf-pages](./batch-add-watermark-to-pdf-pages.cs) | Batch Add Watermark to PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Loads a PDF, iterates through all pages, creates a centered red text watermark using WatermarkArt... |
| [batch-extract-pdf-attachments-zip](./batch-extract-pdf-attachments-zip.cs) | Batch Extract PDF Attachments to ZIP Archive | `Document`, `Page`, `FileAttachmentAnnotation` | Demonstrates how to iterate through multiple PDF files, collect file‑attachment annotations, and ... |
| [compress-pdf-portfolio-using-optimization-options](./compress-pdf-portfolio-using-optimization-options.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `OptimizeResources` | Loads an existing PDF portfolio, enables object compression via OptimizationOptions, and saves th... |
| [create-pdf-portfolio-embed-files](./create-pdf-portfolio-embed-files.cs) | Create PDF Portfolio by Embedding Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to convert a regular PDF into a PDF Portfolio by adding multiple embedded files using A... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Attachments | `Document`, `Collection`, `FileSpecification` | Shows how to create an empty PDF document, initialize its collection, add files as attachments us... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFiles`, `FindByName` | Demonstrates how to locate and remove an embedded file from a PDF using its filename with Aspose.... |
| [delete-pdf-outline-item-by-description](./delete-pdf-outline-item-by-description.cs) | Delete PDF Outline Item by Description | `Document`, `OutlineCollection`, `Delete` | Shows how to remove a PDF outline (bookmark) whose title matches a given string using Aspose.Pdf. |
| [embed-attachment-with-metadata-into-pdf](./embed-attachment-with-metadata-into-pdf.cs) | Embed Attachment with Metadata into PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file into an existing PDF using Aspose.Pdf and add custom attachment ... |
| [embed-file-attachment-from-byte-array](./embed-file-attachment-from-byte-array.cs) | Embed a File Attachment in PDF from a Byte Array | `Document`, `Page`, `Rectangle` | Shows how to create a FileSpecification from a byte array using a MemoryStream and add it to a PD... |
| [embed-hidden-xml-metadata-pdf-attachment](./embed-hidden-xml-metadata-pdf-attachment.cs) | Embed Hidden XML Metadata as PDF Attachment | `Document`, `Page`, `FileSpecification` | Shows how to serialize XML metadata to a memory stream and embed it as a hidden file attachment a... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Display Name | `Document`, `FileSpecification`, `Add` | Shows how to create a PDF portfolio, embed an image file, and assign a custom display name to the... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `EmbeddedFiles`, `EmbeddedFileCollection` | Demonstrates how to open a password‑protected PDF with Aspose.Pdf, enumerate its embedded file at... |
| [extract-embedded-attachment-from-pdf](./extract-embedded-attachment-from-pdf.cs) | Extract Embedded Attachment from PDF | `Document`, `EmbeddedFiles`, `FileSpecification` | Demonstrates how to locate an embedded file in a PDF by name and save it to a specified directory... |
| [extract-embedded-file-by-index](./extract-embedded-file-by-index.cs) | Extract Embedded File from PDF by Index | `Document`, `FileSpecification`, `EmbeddedFiles` | Loads a PDF document, retrieves an embedded file at a given 1‑based index, and saves it to disk p... |
| [extract-files-from-pdf-portfolio](./extract-files-from-pdf-portfolio.cs) | Extract Files from PDF Portfolio | `Document`, `EmbeddedFiles`, `Save(string)` | Demonstrates how to enumerate embedded files in a PDF portfolio using Aspose.Pdf, preserve their ... |
| [extract-pdf-attachment-metadata](./extract-pdf-attachment-metadata.cs) | Extract PDF Attachment Metadata to JSON | `Document`, `Page`, `Annotation` | Shows how to read file attachment annotations from a PDF, retrieve each attachment's file name an... |
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
Updated: 2026-05-08 | Run: `20260508_124239_26063e`
<!-- AUTOGENERATED:END -->

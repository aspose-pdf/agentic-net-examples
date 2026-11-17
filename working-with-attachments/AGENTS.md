---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

> **Working with attachments** in PDF using C# / .NET -- **49** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-attachments** category.
This folder contains standalone C# examples for working-with-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (49/49 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (3/49 files)
- `using Aspose.Pdf.Text;` (3/49 files)
- `using Aspose.Pdf.Drawing;` (1/49 files)
- `using Aspose.Pdf.Optimization;` (1/49 files)
- `using System;` (49/49 files)
- `using System.IO;` (49/49 files)
- `using System.Collections.Generic;` (4/49 files)
- `using System.Linq;` (2/49 files)
- `using System.Threading.Tasks;` (2/49 files)
- `using System.Diagnostics;` (1/49 files)
- `using System.IO.Compression;` (1/49 files)
- `using System.Net.Http;` (1/49 files)
- `using System.Security.Cryptography;` (1/49 files)
- `using System.Text.Json;` (1/49 files)
- `using System.Threading;` (1/49 files)
- `using System.Xml.Serialization;` (1/49 files)

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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open a password‑protected PDF with Aspose.Pdf, embed a file attachment, and save the... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add Attachments to PDF with Missing File Handling | `Document`, `FileSpecification`, `Contents` | Demonstrates how to embed multiple files into a PDF using Aspose.Pdf while gracefully handling mi... |
| [add-file-attachment-to-pdf__v2](./add-file-attachment-to-pdf__v2.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading an existing PDF, creating a FileSpecification for a file, adding it to the d... |
| [add-file-attachment-to-pdf__v5](./add-file-attachment-to-pdf__v5.cs) | Add File Attachment to PDF and Save to Output Folder | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file as an attachment in a PDF using Aspose.Pdf and save the modified... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add Attachment to PDF with Retry on Network Share | `Document`, `FileSpecification`, `Add` | Demonstrates adding a file attachment to a PDF stored on a network share using Aspose.Pdf, with a... |
| [add-image-watermark-to-pdf-attachments](./add-image-watermark-to-pdf-attachments.cs) | Batch Watermark PDF Attachments | `Document`, `FileSpecification`, `TextStamp` | Shows how to iterate over embedded PDF attachments, apply a text watermark to each page, and repl... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `Contents` | Demonstrates how to embed a file into a PDF directly from a MemoryStream without creating tempora... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `Save`, `FileSpecification` | Shows how to attach several files to an existing PDF by iterating a collection of file paths, cre... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to a PDF Portfolio in a Single Loop | `Document`, `Collection`, `FileSpecification` | Demonstrates embedding various file types into a PDF portfolio by creating a FileSpecification fo... |
| [add-nested-pdf-to-pdf-portfolio](./add-nested-pdf-to-pdf-portfolio.cs) | Add Embedded PDF to Existing PDF Portfolio | `Document`, `Save`, `Collection` | Demonstrates how to embed a PDF file as a nested item inside an existing PDF Portfolio using Aspo... |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode Filename Attachment to PDF | `Document`, `Page`, `TextFragment` | Demonstrates embedding a file with a Unicode (e.g., Chinese) filename into a PDF using Aspose.Pdf... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `FileSpecification`, `Collection` | Demonstrates how to embed a Word (.docx) file into an existing PDF portfolio using Aspose.Pdf's C... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Add Custom Cover Page to PDF Portfolio | `Document`, `Page`, `Graph` | Demonstrates how to insert a cover page with a background rectangle and styled text into a PDF an... |
| [attach-file-from-byte-array-to-pdf](./attach-file-from-byte-array-to-pdf.cs) | Attach Byte Array as Embedded File to PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to create a FileSpecification from a byte array using a MemoryStream and embed i... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF from Memory | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates downloading a file from a URL into memory and attaching it to a PDF as a file‑attach... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to Multiple PDFs | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to iterate over a directory of PDF files and embed the same external file as an attachm... |
| [batch-extract-pdf-attachments-to-zip](./batch-extract-pdf-attachments-to-zip.cs) | Batch Extract PDF Attachments to a ZIP Archive | `Document`, `EmbeddedFiles`, `FileSpecification` | Shows how to iterate over multiple PDF files, extract their embedded attachments, and consolidate... |
| [compress-pdf-portfolio-with-optimization](./compress-pdf-portfolio-with-optimization.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `All` | Shows how to load an existing PDF Portfolio, configure optimization settings to compress objects,... |
| [create-pdf-portfolio-embed-files](./create-pdf-portfolio-embed-files.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Shows how to convert a regular PDF into a PDF portfolio by adding multiple files as embedded atta... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Shows how to build a PDF Portfolio document and embed multiple files using Aspose.Pdf's Document,... |
| [delete-outline-items-by-text](./delete-outline-items-by-text.cs) | Delete PDF Portfolio Items by Description | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Shows how to remove embedded (portfolio) files from a PDF by matching their description text, usi... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFilesCollection`, `Count` | Demonstrates how to remove an embedded file from a PDF by matching its filename using Aspose.PDF. |
| [embed-attachment-metadata-into-pdf](./embed-attachment-metadata-into-pdf.cs) | Embed Attachment Metadata into PDF Document Information | `Document`, `FileSpecification`, `PdfSaveOptions` | Demonstrates adding a file attachment to a PDF and storing its filename and size in the document ... |
| [embed-hidden-xml-metadata-pdf-attachment](./embed-hidden-xml-metadata-pdf-attachment.cs) | Embed XML Metadata as Hidden PDF Annotation | `Document`, `Rectangle`, `TextAnnotation` | Demonstrates how to serialize a custom metadata object to XML and embed it in a PDF as a hidden t... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Custom Display Name | `Document`, `Collection`, `FileSpecification` | Demonstrates how to turn an existing PDF into a portfolio, embed an image file as an attachment, ... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from an Encrypted PDF | `Document`, `Decrypt()`, `EmbeddedFiles` | Demonstrates how to open a password‑protected PDF, decrypt it, and extract all embedded file atta... |
| [extract-embedded-file-from-pdf-portfolio](./extract-embedded-file-from-pdf-portfolio.cs) | Extract Embedded File from PDF Portfolio by Index | `Document`, `EmbeddedFiles`, `EmbeddedFilesCollection` | Shows how to open a PDF portfolio, retrieve an embedded file using its 1‑based index, and save it... |
| [extract-embedded-files-from-pdf-portfolio__v2](./extract-embedded-files-from-pdf-portfolio__v2.cs) | Extract Embedded Files from PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load a PDF Portfolio with Aspose.Pdf, iterate over its embedded files, and wr... |
| [extract-pdf-attachment-metadata](./extract-pdf-attachment-metadata.cs) | Extract PDF Attachment Metadata to JSON | `Document`, `EmbeddedFiles`, `FileSpecification` | Shows how to read embedded file information from a PDF using Aspose.Pdf and write the attachment ... |
| [extract-pdf-attachments-in-parallel](./extract-pdf-attachments-in-parallel.cs) | Extract PDF Attachments in Parallel | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load multiple PDF files concurrently and extract their embedded attachments to separ... |
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
Updated: 2026-11-17 | Run: `20261117_144849_8ad21b`
<!-- AUTOGENERATED:END -->

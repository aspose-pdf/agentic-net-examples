---
name: working-with-attachments
description: C# examples for working-with-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-attachments

> **Working with attachments** in PDF using C# / .NET -- **50** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-attachments** category.
This folder contains standalone C# examples for working-with-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (50/50 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/50 files)
- `using Aspose.Pdf.Drawing;` (2/50 files)
- `using Aspose.Pdf.Text;` (2/50 files)
- `using Aspose.Pdf.Optimization;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (50/50 files)
- `using System.Collections.Generic;` (5/50 files)
- `using NUnit.Framework;` (1/50 files)
- `using System.Collections.Concurrent;` (1/50 files)
- `using System.Diagnostics;` (1/50 files)
- `using System.IO.Compression;` (1/50 files)
- `using System.Linq;` (1/50 files)
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
| [add-attachment-to-password-protected-pdf](./add-attachment-to-password-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open an encrypted PDF with a user password, embed a file attachment, and re‑encrypt ... |
| [add-attachments-to-pdf-and-save](./add-attachments-to-pdf-and-save.cs) | Add Attachments to PDF and Save to Output Folder | `Document`, `FileSpecification`, `Add` | Demonstrates loading a PDF, embedding files as attachments, and saving the modified document to a... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add Attachments to PDF with Missing File Handling | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed files as attachments in a PDF using Aspose.Pdf while handling missing s... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading an existing PDF and embedding a single file attachment using Aspose.Pdf's Fi... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add Attachment to PDF with Retry on Network Share | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates how to embed a file into a PDF stored on a UNC network share and implement a retry m... |
| [add-image-watermark-to-pdf-attachments](./add-image-watermark-to-pdf-attachments.cs) | Batch Watermark PDF Attachments | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Loads a PDF, iterates through its embedded files, adds a text watermark to each PDF attachment, a... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file into an existing PDF directly from a MemoryStream without creati... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed several files into an existing PDF by iterating a list of file paths and savin... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to PDF Portfolio | `Document`, `Collection`, `FileSpecification` | Demonstrates embedding various file types into a single PDF portfolio using Aspose.Pdf with a sin... |
| [add-nested-pdf-to-pdf-portfolio](./add-nested-pdf-to-pdf-portfolio.cs) | Add Nested PDF to an Existing PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a PDF file as a nested item inside a PDF Portfolio collection using Aspose.Pdf. |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode Attachment to PDF and Verify | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file with a Unicode display name into a PDF using Aspose.Pdf and then... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add Word Document to PDF Portfolio | `Document`, `FileSpecification`, `Collection` | Shows how to load or create a PDF portfolio and embed a Word (.docx) file as an attachment using ... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Page`, `Graph` | Loads an existing PDF portfolio, adds a light‑gray background rectangle, a centered title, an opt... |
| [attach-file-from-byte-array-to-pdf](./attach-file-from-byte-array-to-pdf.cs) | Embed a Byte Array as a File Attachment in a PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to create a FileSpecification from a byte array and add it as an embedded file a... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF as Annotation | `Document`, `Page`, `Rectangle` | Demonstrates downloading a file from a URL into memory, creating a file specification, and embedd... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to Multiple PDFs | `Document`, `FileSpecification`, `Save` | Shows how to iterate over a folder of PDF files and embed the same attachment into each document ... |
| [batch-extract-pdf-attachments-to-zip](./batch-extract-pdf-attachments-to-zip.cs) | Batch Extract PDF Attachments to ZIP Archive | `Document`, `EmbeddedFiles`, `EmbeddedFileCollection` | Iterates over all PDF files in a folder, extracts any embedded attachments, and stores them in a ... |
| [compress-pdf-portfolio-with-optimization](./compress-pdf-portfolio-with-optimization.cs) | Compress PDF Portfolio Using Optimization Options | `Document`, `OptimizationOptions`, `All` | Shows how to load an existing PDF portfolio, apply optimization settings to compress embedded ima... |
| [create-pdf-portfolio-embed-files](./create-pdf-portfolio-embed-files.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Demonstrates converting a regular PDF into a PDF Portfolio by embedding multiple files using Aspo... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Embedded File | `Document`, `Collection`, `FileSpecification` | Demonstrates how to create a PDF Portfolio using Aspose.Pdf, embed a file as a FileSpecification,... |
| [delete-outline-items-by-text](./delete-outline-items-by-text.cs) | Delete PDF Portfolio Items by Description | `Document`, `FileSpecification`, `Collection` | Shows how to locate and remove embedded files (portfolio items) from a PDF by matching their desc... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFiles`, `FileSpecification` | Shows how to load a PDF, locate an embedded file by its name, delete that attachment using Aspose... |
| [embed-attachment-metadata-into-pdf](./embed-attachment-metadata-into-pdf.cs) | Embed Attachment Metadata into PDF Document Information | `Document`, `FileSpecification`, `DocumentInfo` | Demonstrates how to add a file attachment to a PDF and store its details as custom entries in the... |
| [embed-hidden-xml-metadata-pdf-attachment](./embed-hidden-xml-metadata-pdf-attachment.cs) | Embed Hidden XML Metadata as PDF Attachment | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to create an XML metadata document, convert it to a stream, and embed it in a PD... |
| [embed-image-into-pdf-portfolio](./embed-image-into-pdf-portfolio.cs) | Embed Image into PDF Portfolio with Display Name | `Document`, `Collection`, `FileSpecification` | Shows how to create a PDF portfolio, embed an image file as an attachment with a custom display n... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `Decrypt()`, `EmbeddedFiles` | Demonstrates how to open an encrypted PDF with a password, decrypt it, and extract any embedded f... |
| [extract-embedded-file-from-pdf-portfolio](./extract-embedded-file-from-pdf-portfolio.cs) | Extract Embedded Portfolio File by Index | `Document`, `EmbeddedFiles`, `FileSpecification` | Demonstrates how to load a PDF, locate an embedded (portfolio) file by its zero‑based index, and ... |
| [extract-embedded-files-from-pdf-portfolio](./extract-embedded-files-from-pdf-portfolio.cs) | Extract Embedded Files from PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Loads a PDF portfolio and extracts all embedded files, recreating any folder hierarchy encoded in... |
| [extract-pdf-attachment-metadata](./extract-pdf-attachment-metadata.cs) | Extract PDF Attachment Metadata to JSON | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to read embedded file attachments from a PDF with Aspose.Pdf, gather properties like na... |
| [extract-pdf-attachments-in-parallel](./extract-pdf-attachments-in-parallel.cs) | Extract PDF Attachments Concurrently | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load PDF files with Aspose.Pdf, enumerate their embedded files, and write each attac... |
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
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->

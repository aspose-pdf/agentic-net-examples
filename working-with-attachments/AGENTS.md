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
- `using Aspose.Pdf.Annotations;` (14/50 files)
- `using Aspose.Pdf.AI;` (2/50 files)
- `using Aspose.Pdf.Drawing;` (2/50 files)
- `using Aspose.Pdf.Devices;` (1/50 files)
- `using Aspose.Pdf.Optimization;` (1/50 files)
- `using System;` (50/50 files)
- `using System.IO;` (49/50 files)
- `using System.Collections.Generic;` (7/50 files)
- `using System.Threading.Tasks;` (2/50 files)
- `using NUnit.Framework;` (1/50 files)
- `using System.Diagnostics;` (1/50 files)
- `using System.IO.Compression;` (1/50 files)
- `using System.Linq;` (1/50 files)
- `using System.Net.Http;` (1/50 files)
- `using System.Security.Cryptography;` (1/50 files)
- `using System.Text;` (1/50 files)
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
| [add-attachment-to-pdf-and-save](./add-attachment-to-pdf-and-save.cs) | Add Attachment to PDF and Save to Output Folder | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file as an attachment in an existing PDF using Aspose.Pdf and save th... |
| [add-attachment-to-protected-pdf](./add-attachment-to-protected-pdf.cs) | Add Attachment to Password-Protected PDF | `Document`, `ctor(string, string)`, `FileSpecification` | Shows how to open an encrypted PDF with a user password, embed a file as an attachment, and save ... |
| [add-attachments-with-size-validation](./add-attachments-with-size-validation.cs) | Add File Attachments with Size Validation to PDF | `Document`, `Page`, `FileSpecification` | Demonstrates loading a PDF, checking each file's existence and size against a limit, and adding v... |
| [add-file-attachment-to-pdf-with-error-handling](./add-file-attachment-to-pdf-with-error-handling.cs) | Add File Attachment to PDF with Error Handling | `Document`, `Save`, `Page` | Demonstrates how to attach a file to the first page of a PDF using Aspose.Pdf while handling miss... |
| [add-file-attachment-to-pdf](./add-file-attachment-to-pdf.cs) | Add File Attachment to PDF | `Document`, `FileSpecification`, `Rectangle` | Shows how to load an existing PDF and attach a file using a FileAttachmentAnnotation with Aspose.... |
| [add-file-attachment-with-retry](./add-file-attachment-with-retry.cs) | Add File Attachment to PDF with Retry on Network Share | `Document`, `Page`, `FileSpecification` | Demonstrates how to attach a file to a PDF stored on a network share and implement a retry mechan... |
| [add-in-memory-attachment-to-pdf](./add-in-memory-attachment-to-pdf.cs) | Add In-Memory Attachment to PDF Using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a file into a PDF directly from a MemoryStream, avoiding any intermediate disk... |
| [add-multiple-attachments-to-pdf](./add-multiple-attachments-to-pdf.cs) | Add Multiple Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed several files into an existing PDF by iterating a collection of file paths and... |
| [add-multiple-files-to-pdf-portfolio](./add-multiple-files-to-pdf-portfolio.cs) | Add Multiple Files to PDF Portfolio | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Demonstrates creating an empty PDF and embedding various file types as a portfolio in a single lo... |
| [add-pdf-to-existing-portfolio](./add-pdf-to-existing-portfolio.cs) | Add PDF to Existing PDF Portfolio | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to embed a PDF file as a nested item inside an existing PDF Portfolio collection using ... |
| [add-unicode-file-attachment-to-pdf](./add-unicode-file-attachment-to-pdf.cs) | Add Unicode File Attachment to PDF | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates how to attach a file with a Unicode filename to a PDF using Aspose.Pdf by creating a... |
| [add-word-document-to-pdf-portfolio](./add-word-document-to-pdf-portfolio.cs) | Add a Word Document to a PDF Portfolio | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a Word (.docx) file into a PDF portfolio using Aspose.Pdf's Document an... |
| [apply-custom-visual-template-to-pdf-portfolio](./apply-custom-visual-template-to-pdf-portfolio.cs) | Apply Custom Visual Template to PDF Portfolio | `Document`, `Page`, `Graph` | The example opens an existing PDF portfolio, iterates through each page, and uses Aspose.Pdf.Draw... |
| [attach-remote-file-to-pdf](./attach-remote-file-to-pdf.cs) | Attach Remote File to PDF as File Attachment | `Document`, `Page`, `Rectangle` | Demonstrates downloading a file from a URL into memory and embedding it in a PDF as a file‑attach... |
| [attachment-removal-integration-tests](./attachment-removal-integration-tests.cs) | Integration Tests for Attachment Removal | `Attachment`, `ThreadMessageResponse` | Shows how to test that removing attachments from a ThreadMessageResponse correctly updates the At... |
| [batch-add-attachment-to-pdfs](./batch-add-attachment-to-pdfs.cs) | Batch Add Attachment to PDFs | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to iterate through a folder of PDF files, attach a specified file to each docume... |
| [batch-extract-pdf-attachments-zip](./batch-extract-pdf-attachments-zip.cs) | Batch Extract PDF Attachments into a ZIP Archive | `Document`, `EmbeddedFiles`, `FileSpecification` | Demonstrates how to iterate over multiple PDF files, extract their embedded attachments, and cons... |
| [batch-watermark-pdf-attachments](./batch-watermark-pdf-attachments.cs) | Batch Watermark PDF Attachments | `Document`, `FileSpecification`, `ImageStamp` | Shows how to iterate through embedded PDF attachments, apply an image watermark to every page of ... |
| [compress-pdf-portfolio](./compress-pdf-portfolio.cs) | Compress and Save PDF Portfolio | `Document`, `OptimizationOptions`, `OptimizeResources` | Loads an existing PDF Portfolio, enables object compression using OptimizationOptions, and saves ... |
| [create-pdf-portfolio-with-attachments](./create-pdf-portfolio-with-attachments.cs) | Create PDF Portfolio with Attachments | `Document`, `Collection`, `FileSpecification` | Demonstrates how to build a PDF Portfolio using Aspose.Pdf by adding a blank page, creating a col... |
| [create-pdf-portfolio-with-embedded-files](./create-pdf-portfolio-with-embedded-files.cs) | Create PDF Portfolio with Embedded Files | `Document`, `Collection`, `FileSpecification` | Shows how to convert a regular PDF into a portfolio by embedding multiple files using Aspose.Pdf'... |
| [delete-pdf-attachment-by-filename](./delete-pdf-attachment-by-filename.cs) | Delete PDF Attachment by Filename | `Document`, `EmbeddedFiles`, `FindByName` | Shows how to remove an embedded file from a PDF by matching its filename using Aspose.Pdf. The ex... |
| [delete-pdf-outline-items-by-description](./delete-pdf-outline-items-by-description.cs) | Delete PDF Outline Items by Description | `Document`, `OutlineItemCollection`, `OutlineCollection` | The example loads a PDF, searches its outline (bookmark) entries for a specific description text,... |
| [embed-attachment-metadata-hidden-annotation](./embed-attachment-metadata-hidden-annotation.cs) | Embed Attachment Metadata as Hidden XML Annotation | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | The example extracts embedded file information from a PDF, serializes it to an XML document, and ... |
| [embed-attachment-metadata-to-pdf](./embed-attachment-metadata-to-pdf.cs) | Embed File Attachment and Add Metadata to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file attachment in a PDF and store custom attachment metadata in the ... |
| [embed-file-from-byte-array-pdf-attachment](./embed-file-from-byte-array-pdf-attachment.cs) | Embed File from Byte Array as PDF Attachment | `Document`, `FileSpecification`, `FileAttachmentAnnotation` | Demonstrates creating a FileSpecification from a byte array using a MemoryStream and adding it as... |
| [embed-image-in-pdf-portfolio](./embed-image-in-pdf-portfolio.cs) | Embed Image as Attachment in PDF Portfolio | `Document`, `FileSpecification`, `Name` | Shows how to embed an image file into a PDF portfolio and assign a custom display name using Aspo... |
| [extract-attachments-from-encrypted-pdf](./extract-attachments-from-encrypted-pdf.cs) | Extract Attachments from Encrypted PDF | `Document`, `Decrypt()`, `EmbeddedFiles` | Demonstrates opening an encrypted PDF with a password, decrypting it, and extracting all embedded... |
| [extract-embedded-attachment-from-pdf](./extract-embedded-attachment-from-pdf.cs) | Extract Embedded Attachment from PDF | `Document`, `FileSpecification`, `FindByName` | Shows how to locate an embedded file in a PDF by its name and save it to a target directory using... |
| [extract-embedded-file-from-pdf-portfolio](./extract-embedded-file-from-pdf-portfolio.cs) | Extract Embedded File from PDF Portfolio by Index | `Document`, `EmbeddedFileCollection`, `FileSpecification` | Shows how to load a PDF, access its embedded files collection, retrieve a file using a 1‑based in... |
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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

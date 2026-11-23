---
name: attachments
description: C# examples for attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - attachments

> **Attachments** in PDF using C# / .NET -- **48** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **attachments** category.
This folder contains standalone C# examples for attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (48/48 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (5/48 files)
- `using Aspose.Pdf.Text;` (1/48 files)
- `using System;` (48/48 files)
- `using System.IO;` (48/48 files)
- `using System.Collections.Generic;` (4/48 files)
- `using System.Text;` (4/48 files)
- `using System.Text.Json;` (2/48 files)

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
| [add-and-retrieve-file-attachment](./add-and-retrieve-file-attachment.cs) | Add and Retrieve File Attachment in PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, attach a file using FileSpecification, save the document, and then retri... |
| [add-attachment-from-memory-stream-to-pdf](./add-attachment-from-memory-stream-to-pdf.cs) | Add Attachment from Memory Stream to PDF | `Document`, `Save`, `FileSpecification` | Shows how to load an existing PDF, create an attachment from a memory stream with a custom name, ... |
| [add-attachment-to-pdf-from-stream](./add-attachment-to-pdf-from-stream.cs) | Add Attachment to PDF from Stream | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading a PDF, creating a file attachment from a memory stream, adding it to the doc... |
| [add-attachment-to-pdf-from-stream__v2](./add-attachment-to-pdf-from-stream__v2.cs) | Add Attachment to PDF from Stream and List Embedded Files | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file into a PDF using a memory stream, set a custom description and M... |
| [add-attachments-and-export-descriptions-to-csv](./add-attachments-and-export-descriptions-to-csv.cs) | Add Attachments to PDF and Export Descriptions to CSV | `Document`, `FileSpecification`, `Save` | The example adds file attachments to a PDF using Aspose.Pdf, saves the document, then reopens it ... |
| [add-file-attachment-and-export-metadata](./add-file-attachment-and-export-metadata.cs) | Add File Attachment to PDF and Export Metadata to CSV | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file into a PDF using Aspose.Pdf, retrieve its embedded file metadata... |
| [add-file-attachment-and-retrieve-description](./add-file-attachment-and-retrieve-description.cs) | Add File Attachment to PDF and Retrieve Its Description | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file into a PDF using Aspose.Pdf, set a description for the attachmen... |
| [add-file-attachment-and-retrieve-name](./add-file-attachment-and-retrieve-name.cs) | Add File Attachment to PDF and Retrieve Its Name | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load a PDF, embed a file attachment with a description, retrieve the attachme... |
| [add-file-attachment-annotation__v2](./add-file-attachment-annotation__v2.cs) | Add File Attachment Annotation with Long Description to PDF | `Document`, `Page`, `Rectangle` | Shows how to open a PDF, create a FileAttachmentAnnotation with a detailed description, embed a f... |
| [add-file-attachment-to-pdf-and-verify-embedded-fil...](./add-file-attachment-to-pdf-and-verify-embedded-files.cs) | Add File Attachment to PDF and Verify Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Loads an existing PDF, embeds a text file as an attachment using FileSpecification, saves the doc... |
| [add-file-attachment-to-pdf__v6](./add-file-attachment-to-pdf__v6.cs) | Add File Attachment to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `Contents` | Shows how to load a PDF, create a FileSpecification with a description, embed it in the document'... |
| [add-file-attachment-to-pdf__v7](./add-file-attachment-to-pdf__v7.cs) | Add File Attachment to PDF with Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open an existing PDF, embed a file as an attachment with a description, and save the... |
| [add-file-attachment-with-unicode-name-to-pdf](./add-file-attachment-with-unicode-name-to-pdf.cs) | Add File Attachment with Unicode Name to PDF | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file into a PDF using Aspose.Pdf, set its UnicodeName property, retri... |
| [add-file-attachments-to-pdf](./add-file-attachments-to-pdf.cs) | Add File Attachments to a PDF Document | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to load an existing PDF, embed multiple files with custom descriptions as attach... |
| [add-multiple-attachments-unique-extraction](./add-multiple-attachments-unique-extraction.cs) | Add Multiple Attachments with Same Name and Extract with Uni... | `Document`, `FileSpecification`, `Add` | Creates a PDF, embeds several files that share the same filename, then extracts each attachment a... |
| [add-multiple-file-attachments-to-pdf](./add-multiple-file-attachments-to-pdf.cs) | Add Multiple File Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading a PDF, embedding two files as attachments using FileSpecification, and savin... |
| [add-pdf-attachments-and-list](./add-pdf-attachments-and-list.cs) | Add Attachments to a PDF and Export Their Names | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading a PDF, embedding multiple files as attachments using Aspose.Pdf, saving the ... |
| [add-pdf-attachments-and-save-descriptions](./add-pdf-attachments-and-save-descriptions.cs) | Add Attachments to PDF and Save Descriptions | `Document`, `FileSpecification`, `EmbeddedFiles` | Loads an existing PDF, embeds two files as attachments, saves the updated PDF, then extracts each... |
| [add-stream-attachment-with-description](./add-stream-attachment-with-description.cs) | Add Stream Attachment with Description to PDF and Export Des... | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file into a PDF from a memory stream, set a custom description for th... |
| [add-unicode-attachment-to-pdf](./add-unicode-attachment-to-pdf.cs) | Add Unicode Attachment to PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates embedding a file with a Unicode name into a PDF using Aspose.Pdf's FileSpecification... |
| [add-unicode-attachment-to-pdf__v2](./add-unicode-attachment-to-pdf__v2.cs) | Add Unicode Attachment to PDF and Verify Name Preservation | `Document`, `FileSpecification`, `Save` | Demonstrates how to embed a file with a Unicode name into a PDF using Aspose.Pdf, then reopen the... |
| [add-unicode-attachment-to-pdf__v3](./add-unicode-attachment-to-pdf__v3.cs) | Add Unicode-Named Attachment to PDF and Verify | `Document`, `FileSpecification`, `Add` | Demonstrates how to embed a file with a Unicode name into a PDF using Aspose.Pdf, save the docume... |
| [calculate-total-attachment-size](./calculate-total-attachment-size.cs) | Calculate Total Size of PDF Attachments | `Document`, `FileSpecification`, `Params` | Loads a PDF, iterates through all embedded file attachments, sums their byte sizes, and writes th... |
| [count-embedded-file-attachments](./count-embedded-file-attachments.cs) | Count Embedded File Attachments in PDF | `Document`, `Page`, `Annotation` | Loads a PDF, iterates through each page's annotations, identifies FileAttachmentAnnotation object... |
| [count-embedded-file-attachments__v2](./count-embedded-file-attachments__v2.cs) | Count Embedded File Attachments in a PDF | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates loading a PDF with Aspose.Pdf, enumerating its embedded file specifications, countin... |
| [count-pdf-attachments](./count-pdf-attachments.cs) | Count PDF Attachments and Save to Text File | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Loads a PDF, enumerates its embedded file attachments, counts them, and writes the attachment cou... |
| [create-pdf-with-multiple-attachments](./create-pdf-with-multiple-attachments.cs) | Create PDF with Multiple Attachments and Descriptions | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF, embed three different files each with a custom description, and... |
| [delete-and-add-pdf-attachments](./delete-and-add-pdf-attachments.cs) | Delete All PDF Attachments and Add New File Attachments | `Document`, `EmbeddedFiles`, `Delete` | The example loads a PDF, removes any existing embedded file attachments, then adds three new atta... |
| [delete-attachments-add-file-attachment](./delete-attachments-add-file-attachment.cs) | Delete All Attachments and Add a New File Attachment to PDF | `Document`, `EmbeddedFiles`, `Delete` | Demonstrates loading a PDF, removing all embedded file attachments, adding a new file attachment,... |
| [embed-file-with-special-char-description](./embed-file-with-special-char-description.cs) | Embed File with Special-Character Description and Verify Int... | `Document`, `FileSpecification`, `Save` | Shows how to create a FileSpecification whose description contains special characters, embed the ... |
| ... | | | *and 18 more files* |

## Category Statistics
- Total examples: 48

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
- Review code examples in this folder for attachments patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_023724_090db2`
<!-- AUTOGENERATED:END -->

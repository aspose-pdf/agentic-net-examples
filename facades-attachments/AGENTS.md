---
name: facades-attachments
description: C# examples for facades-attachments using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-attachments

> **Facades attachments** in PDF using C# / .NET -- **24** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-attachments** category.
This folder contains standalone C# examples for facades-attachments operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-attachments**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (23/24 files) ← category-specific
- `using Aspose.Pdf.Facades;` (5/24 files)
- `using System;` (24/24 files)
- `using System.IO;` (24/24 files)
- `using System.Text;` (2/24 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
Document doc = new Document("input.pdf");
// ... operations ...
doc.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-and-delete-pdf-file-attachments](./add-and-delete-pdf-file-attachments.cs) | Add and Delete File Attachments in PDF | `Document`, `FileSpecification`, `Add` | The example loads an existing PDF, adds three file attachments via the Document.EmbeddedFiles col... |
| [add-attachment-to-pdf-and-list-embedded-files](./add-attachment-to-pdf-and-list-embedded-files.cs) | Add Attachment to PDF and List Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to open an existing PDF, embed a file as an attachment using FileSpecification, enumera... |
| [add-attachment-to-pdf](./add-attachment-to-pdf.cs) | Add Attachment to PDF with Aspose.Pdf | `Document`, `FileSpecification`, `Add` | Demonstrates loading a PDF, creating a file specification from a stream, embedding the file as an... |
| [add-file-attachment-to-pdf-and-list-embedded-files](./add-file-attachment-to-pdf-and-list-embedded-files.cs) | Add File Attachment to PDF and List Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | The example loads an existing PDF, attaches a text file as an embedded file with a description, s... |
| [add-file-attachment-to-pdf__v10](./add-file-attachment-to-pdf__v10.cs) | Add File Attachment to PDF and Verify Embedded Files | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed a file attachment into a PDF using Aspose.Pdf, save the document, and t... |
| [add-file-attachment-to-pdf__v11](./add-file-attachment-to-pdf__v11.cs) | Add File Attachment to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates loading a PDF, creating an embedded file from a memory stream with a description, ad... |
| [add-file-attachment-to-pdf__v8](./add-file-attachment-to-pdf__v8.cs) | Add File Attachment to PDF with Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, create a FileSpecification for an external file, embed it as an attachme... |
| [add-file-attachment-to-pdf__v9](./add-file-attachment-to-pdf__v9.cs) | Add File Attachment to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, create a FileSpecification for an external file, embed it as an attachme... |
| [add-file-attachments-to-pdf__v2](./add-file-attachments-to-pdf__v2.cs) | Add File Attachments to PDF Using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, create file specifications from streams with distinct descriptions, embe... |
| [add-multiple-attachments-to-pdf__v2](./add-multiple-attachments-to-pdf__v2.cs) | Add Multiple Attachments to a PDF using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load an existing PDF, embed several files as attachments, and save the resulting doc... |
| [add-multiple-attachments-to-pdf__v3](./add-multiple-attachments-to-pdf__v3.cs) | Add Multiple Attachments to PDF using Aspose.Pdf | `Document`, `FileSpecification`, `EmbeddedFiles` | Shows how to load a PDF, embed three files from streams as attachments, and save the modified doc... |
| [add-multiple-file-attachments-to-pdf__v2](./add-multiple-file-attachments-to-pdf__v2.cs) | Add Multiple File Attachments to a PDF | `Document`, `FileSpecification`, `EmbeddedFilesCollection` | Demonstrates loading a PDF with Aspose.Pdf, embedding several files as attachments, and saving th... |
| [add-multiple-stream-attachments-to-pdf](./add-multiple-stream-attachments-to-pdf.cs) | Add Multiple Stream Attachments to PDF and List Embedded Fil... | `Document`, `FileSpecification`, `EmbeddedFiles` | Demonstrates how to embed several in‑memory text files into a PDF using Aspose.Pdf, save the docu... |
| [delete-attachments-add-new-files](./delete-attachments-add-new-files.cs) | Delete All PDF Attachments and Add New Files | `PdfContentEditor`, `BindPdf`, `DeleteAttachments` | Demonstrates how to remove every attachment from a PDF using PdfContentEditor, then add new file ... |
| [delete-attachments-add-new-pdf-attachments](./delete-attachments-add-new-pdf-attachments.cs) | Delete Existing Attachments and Add New File Attachments to ... | `PdfContentEditor`, `BindPdf`, `DeleteAttachments` | Demonstrates how to remove all existing attachments from a PDF using PdfContentEditor.DeleteAttac... |
| [manage-pdf-attachments-with-pdfcontenteditor](./manage-pdf-attachments-with-pdfcontenteditor.cs) | Manage PDF Attachments with PdfContentEditor | `PdfContentEditor`, `BindPdf`, `Save` | The example demonstrates binding a PDF with PdfContentEditor, removing all existing embedded file... |
| [manage-pdf-attachments-with-pdfcontenteditor__v2](./manage-pdf-attachments-with-pdfcontenteditor__v2.cs) | Manage PDF Attachments with PdfContentEditor | `PdfContentEditor`, `BindPdf`, `Document` | The example shows how to bind a PDF using PdfContentEditor, add file attachments via the Document... |
| [remove-all-attachments-from-pdf__v3](./remove-all-attachments-from-pdf__v3.cs) | Remove All Attachments from PDF | `Document`, `Save`, `EmbeddedFileCollection` | Shows how to load a PDF with Aspose.Pdf, iterate over its embedded file attachments, delete each ... |
| [remove-all-attachments-from-pdf__v4](./remove-all-attachments-from-pdf__v4.cs) | Remove All Attachments from a PDF | `Document`, `EmbeddedFileCollection`, `Delete` | Demonstrates how to load a PDF with Aspose.Pdf, delete every embedded file attachment, and save t... |
| [remove-all-attachments-from-pdf__v5](./remove-all-attachments-from-pdf__v5.cs) | Remove All Attachments from PDF | `PdfContentEditor`, `BindPdf`, `DeleteAttachments` | Demonstrates how to open a PDF with Aspose.Pdf.Facades, delete every embedded attachment, and sav... |
| [remove-and-add-pdf-attachments](./remove-and-add-pdf-attachments.cs) | Remove Existing Attachments and Add New Stream Attachments t... | `Document`, `FileSpecification`, `EmbeddedFileCollection` | Demonstrates how to load a PDF, delete all embedded file attachments, and then add new attachment... |
| [remove-pdf-attachment](./remove-pdf-attachment.cs) | Remove PDF Attachment Using Aspose.Pdf | `Document`, `EmbeddedFiles`, `EmbeddedFile` | Shows how to load a PDF, detect embedded file attachments, delete the first attachment, and save ... |
| [replace-pdf-attachments-with-new-file](./replace-pdf-attachments-with-new-file.cs) | Replace PDF Attachments with a New File | `Document`, `FileSpecification`, `EmbeddedFileCollection` | Shows how to load a PDF, remove all embedded file attachments, add a new file attachment, and sav... |
| [replace-pdf-attachments-with-new-files](./replace-pdf-attachments-with-new-files.cs) | Replace PDF Attachments with New Files | `Document`, `FileSpecification`, `EmbeddedFiles` | The example loads a PDF, removes any existing embedded file attachments, adds three new file atta... |

## Category Statistics
- Total examples: 24

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfContentEditor.BindPdf`
- `Aspose.Pdf.Facades.PdfContentEditor.DeleteAttachments`
- `Aspose.Pdf.Facades.PdfContentEditor.Save`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfExtractor.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfExtractor.ExtractAttachment()`
- `Aspose.Pdf.Facades.PdfExtractor.GetAttachment(string)`

### Rules
- Bind the target PDF with PdfContentEditor.BindPdf({input_pdf}) before performing any modifications.
- Add a document attachment using PdfContentEditor.AddDocumentAttachment({attachment_stream}, {attachment_name}, {attachment_description}) where the stream can be a FileStream or MemoryStream.
- Save the modified PDF with PdfContentEditor.Save({output_pdf}) after adding the attachment.
- Load a PDF document by calling PdfContentEditor.BindPdf({input_pdf}).
- Remove every attachment in the loaded document with PdfContentEditor.DeleteAttachments().

### Warnings
- The example assumes the input PDF and attachment files exist at the specified paths and that the stream remains open until Save is called.
- GetAttachNames returns a non‑generic IList; callers should cast elements to string.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-attachments patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-27 | Run: `20261127_021938_f0e1c6`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf;` (74/95 files) ← category-specific
- `using Aspose.Pdf.Facades;` (38/95 files)
- `using Aspose.Pdf.Annotations;` (11/95 files)
- `using Aspose.Pdf.Text;` (5/95 files)
- `using Aspose.Pdf.LogicalStructure;` (3/95 files)
- `using Aspose.Pdf.Tagged;` (3/95 files)
- `using Aspose.Pdf.Drawing;` (1/95 files)
- `using System;` (95/95 files)
- `using System.IO;` (95/95 files)
- `using System.Collections.Generic;` (9/95 files)
- `using System.Drawing;` (1/95 files)

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

| File | Key APIs | Description |
|------|----------|-------------|
| [Add-an-attachment-to-a-PDF-document-and-extract-and-save-an-...](./Add-an-attachment-to-a-PDF-document-and-extract-and-save-an-attachment-using-PDF-input-and-save-as-P.cs) | `PdfContentEditor`, `PdfExtractor` | Add an attachment to a PDF document and extract and save an attachment using ... |
| [Add-an-attachment-to-a-PDF-document-using-PDF-input-and-save...](./Add-an-attachment-to-a-PDF-document-using-PDF-input-and-save-as-PDF.cs) | `FileAttachmentAnnotation` | Add an attachment to a PDF document using PDF input and save as PDF |
| [Add-an-attachment-to-a-tagged-PDF-then-extract-and-save-that...](./Add-an-attachment-to-a-tagged-PDF-then-extract-and-save-that-attachment-as-a-separate-PDF-file.cs) | `PdfContentEditor`, `PdfExtractor` | Add an attachment to a tagged PDF then extract and save that attachment as a ... |
| [Aspose.PDF-allows-you-to-access-a-PDF-file-s-XMP-metadata-to...](./Aspose.PDF-allows-you-to-access-a-PDF-file-s-XMP-metadata-to-get-a-PDF-file-s-metadata-use-PDF-inpu.cs) |  | Aspose.PDF allows you to access a PDF file s XMP metadata to get a PDF file s... |
| [Aspose.PDF-allows-you-to-set-metadata-in-a-PDF-file-to-set-t...](./Aspose.PDF-allows-you-to-set-metadata-in-a-PDF-file-to-set-this-use-PDF-input-and-save-as-PDF.cs) |  | Aspose.PDF allows you to set metadata in a PDF file to set this use PDF input... |
| [Aspose.PDF-can-remove-attachments-from-PDF-files-a-PDF-docum...](./Aspose.PDF-can-remove-attachments-from-PDF-files-a-PDF-document-s-attachments-are-held-in-the-Docum.cs) |  | Aspose.PDF can remove attachments from PDF files a PDF document s attachments... |
| [Aspose.PDF-for-.NET-allows-you-to-set-file-specific-informat...](./Aspose.PDF-for-.NET-allows-you-to-set-file-specific-information-such-as-author-creation-date-subje.cs) |  | Aspose.PDF for .NET allows you to set file specific information such as autho... |
| [Assign-a-custom-XMP-metadata-heading-to-a-PDF-document-using...](./Assign-a-custom-XMP-metadata-heading-to-a-PDF-document-using-the-source-PDF-as-input.cs) |  | Assign a custom XMP metadata heading to a PDF document using the source PDF a... |
| [Assign-custom-metadata-to-a-PDF-document-using-its-Metadata-...](./Assign-custom-metadata-to-a-PDF-document-using-its-Metadata-property-and-output-the-modified-file-as.cs) | `PdfFileInfo` | Assign custom metadata to a PDF document using its Metadata property and outp... |
| [Assign-property-values-from-an-input-PDF-and-write-the-updat...](./Assign-property-values-from-an-input-PDF-and-write-the-updated-document-back-to-PDF-format.cs) |  | Assign property values from an input PDF and write the updated document back ... |
| [Attach-a-file-to-a-PDF-then-retrieve-and-save-the-embedded-a...](./Attach-a-file-to-a-PDF-then-retrieve-and-save-the-embedded-attachment-as-a-separate-PDF-document.cs) | `PdfContentEditor`, `PdfExtractor` | Attach a file to a PDF then retrieve and save the embedded attachment as a se... |
| [Attach-an-external-PDF-file-to-an-existing-PDF-document-and-...](./Attach-an-external-PDF-file-to-an-existing-PDF-document-and-save-the-result-as-a-new-PDF.cs) | `PdfContentEditor` | Attach an external PDF file to an existing PDF document and save the result a... |
| [Call-the-EmbeddedFiles-collection-s-Delete-method-using-PDF-...](./Call-the-EmbeddedFiles-collection-s-Delete-method-using-PDF-input-and-save-as-PDF.cs) |  | Call the EmbeddedFiles collection s Delete method using PDF input and save as... |
| [Create-a-Document-object-and-open-the-input-PDF-file-using-P...](./Create-a-Document-object-and-open-the-input-PDF-file-using-PDF-input-and-save-as-PDF.cs) |  | Create a Document object and open the input PDF file using PDF input and save... |
| [Create-a-Document-object-using-PDF-input-and-save-as-PDF](./Create-a-Document-object-using-PDF-input-and-save-as-PDF.cs) |  | Create a Document object using PDF input and save as PDF |
| [Create-a-DocumentInfo-object-using-PDF-input-and-save-as-PDF](./Create-a-DocumentInfo-object-using-PDF-input-and-save-as-PDF.cs) |  | Create a DocumentInfo object using PDF input and save as PDF |
| [Create-a-custom-metadata-namespace-with-a-prefix-add-prefixe...](./Create-a-custom-metadata-namespace-with-a-prefix-add-prefixed-metadata-to-a-PDF-then-save-the-outp.cs) |  | Create a custom metadata namespace with a prefix add prefixed metadata to a P... |
| [Delete-a-specific-attachment-from-a-PDF-file-and-save-the-mo...](./Delete-a-specific-attachment-from-a-PDF-file-and-save-the-modified-document-as-a-new-PDF.cs) |  | Delete a specific attachment from a PDF file and save the modified document a... |
| [Delete-all-attachments-associated-with-a-PDF-file-using-PDF-...](./Delete-all-attachments-associated-with-a-PDF-file-using-PDF-input-and-save-as-PDF.cs) |  | Delete all attachments associated with a PDF file using PDF input and save as... |
| [Execute-advanced-PDF-attachment-manipulations-delete-a-speci...](./Execute-advanced-PDF-attachment-manipulations-delete-a-specified-attachment-and-save-the-updated-d.cs) |  | Execute advanced PDF attachment manipulations delete a specified attachment a... |
| [Execute-advanced-attachment-manipulations-delete-a-specified...](./Execute-advanced-attachment-manipulations-delete-a-specified-attachment-from-a-PDF-and-save-the-mo.cs) |  | Execute advanced attachment manipulations delete a specified attachment from ... |
| [Execute-advanced-attachment-manipulations-extracting-a-PDF-a...](./Execute-advanced-attachment-manipulations-extracting-a-PDF-attachment-from-a-source-PDF-and-saving.cs) | `PdfExtractor` | Execute advanced attachment manipulations extracting a PDF attachment from a ... |
| [Extract-all-embedded-PDF-attachments-from-an-input-PDF-and-w...](./Extract-all-embedded-PDF-attachments-from-an-input-PDF-and-write-each-attachment-to-separate-PDF-fil.cs) | `PdfExtractor` | Extract all embedded PDF attachments from an input PDF and write each attachm... |
| [Extract-an-attachment-from-a-PDF-portfolio-save-it-as-PDF-an...](./Extract-an-attachment-from-a-PDF-portfolio-save-it-as-PDF-and-remove-it-from-the-original-PDF.cs) |  | Extract an attachment from a PDF portfolio save it as PDF and remove it from ... |
| [Extract-an-embedded-PDF-attachment-from-a-source-PDF-documen...](./Extract-an-embedded-PDF-attachment-from-a-source-PDF-document-and-save-it-as-a-separate-PDF-file.cs) | `PdfExtractor` | Extract an embedded PDF attachment from a source PDF document and save it as ... |
| [Extract-and-save-an-attachment-and-remove-an-attachment-from...](./Extract-and-save-an-attachment-and-remove-an-attachment-from-an-existing-PDF-portfolio-file-using-PD.cs) | `PdfContentEditor` | Extract and save an attachment and remove an attachment from an existing PDF ... |
| [Extract-and-save-an-attachment-heading-using-PDF-input](./Extract-and-save-an-attachment-heading-using-PDF-input.cs) |  | Extract and save an attachment heading using PDF input |
| [Extract-and-save-an-attachment-using-PDF-input-and-save-as-P...](./Extract-and-save-an-attachment-using-PDF-input-and-save-as-PDF.cs) | `PdfExtractor` | Extract and save an attachment using PDF input and save as PDF |
| [Extract-every-attachment-from-the-supplied-PDF-document-incl...](./Extract-every-attachment-from-the-supplied-PDF-document-including-those-associated-with-heading-ele.cs) |  | Extract every attachment from the supplied PDF document including those assoc... |
| [Extract-the-embedded-attachment-from-a-PDF-file-and-save-it-...](./Extract-the-embedded-attachment-from-a-PDF-file-and-save-it-to-a-specified-location.cs) | `PdfExtractor` | Extract the embedded attachment from a PDF file and save it to a specified lo... |
| ... | | *and 65 more files* |

## Category Statistics
- Total examples: 95

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
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

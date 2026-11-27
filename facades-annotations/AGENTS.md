---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **206** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (148/206 files) ← category-specific
- `using Aspose.Pdf.Facades;` (141/206 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (106/206 files) ← category-specific
- `using Aspose.Pdf.Text;` (6/206 files)
- `using Aspose.Pdf.Forms;` (4/206 files)
- `using System;` (206/206 files)
- `using System.IO;` (206/206 files)
- `using System.Collections.Generic;` (18/206 files)
- `using System.Linq;` (4/206 files)
- `using System.Text.Json;` (4/206 files)
- `using System.Threading;` (4/206 files)
- `using System.Diagnostics;` (3/206 files)
- `using System.Drawing;` (3/206 files)
- `using System.Text;` (3/206 files)
- `using System.Threading.Tasks;` (3/206 files)
- `using System.IO.Compression;` (2/206 files)
- `using System.Xml;` (2/206 files)
- `using System.Xml.Linq;` (2/206 files)
- `using System.Text.RegularExpressions;` (1/206 files)
- `using System.Xml.Schema;` (1/206 files)

## Common Code Pattern

Most files in this category use `PdfAnnotationEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfAnnotationEditor tool = new PdfAnnotationEditor();
tool.BindPdf("input.pdf");
// ... PdfAnnotationEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-attachment-and-comment-annotation](./add-attachment-and-comment-annotation.cs) | Add Attachment and Comment Annotation to PDF | `Document`, `FileSpecification`, `Rectangle` | Demonstrates how to embed a file attachment and add a sticky‑note comment annotation to a PDF usi... |
| [add-comment-and-highlight-annotations](./add-comment-and-highlight-annotations.cs) | Add Comment and Highlight Annotations to PDF | `Document`, `Rectangle`, `TextAnnotation` | Demonstrates loading a PDF, adding a text (sticky‑note) comment and a yellow highlight annotation... |
| [add-comment-annotation-to-pdf](./add-comment-annotation-to-pdf.cs) | Add Comment Annotation to PDF | `Document`, `Rectangle`, `TextAnnotation` | Demonstrates how to load a PDF, create a text (comment) annotation on a page, and save the modifi... |
| [add-free-text-annotation-export-xfdf](./add-free-text-annotation-export-xfdf.cs) | Add Free Text Annotation and Export to XFDF | `Document`, `FreeTextAnnotation`, `DefaultAppearance` | Shows how to add a free‑text annotation to a PDF document and then export all annotations to an X... |
| [add-free-text-annotation-to-pdf-page](./add-free-text-annotation-to-pdf-page.cs) | Add Free Text Annotation to PDF Page | `Document`, `Rectangle`, `DefaultAppearance` | Shows how to load a PDF, create a free‑text annotation with custom appearance and border, add it ... |
| [add-free-text-annotation-with-pdfcontenteditor](./add-free-text-annotation-with-pdfcontenteditor.cs) | Add Free Text Annotation to PDF using PdfContentEditor | `BindPdf`, `Save`, `Document` | The example loads a PDF with PdfContentEditor, creates a free‑text annotation on page 2 with a cu... |
| [add-highlight-annotation-to-pdf__v3](./add-highlight-annotation-to-pdf__v3.cs) | Add Highlight Annotation to PDF | `Document`, `HighlightAnnotation`, `Rectangle` | Loads a PDF document, creates a yellow highlight annotation on a defined rectangle of the first p... |
| [add-line-annotation-export-xfdf](./add-line-annotation-export-xfdf.cs) | Add Line Annotation and Export Annotations to XFDF | `Document`, `LineAnnotation`, `Point` | Shows how to insert a line annotation into a PDF document and then export all annotations to an X... |
| [add-line-annotation-to-pdf](./add-line-annotation-to-pdf.cs) | Add Line Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a default line annotation on page 1, and save the modified docume... |
| [add-orange-text-annotation](./add-orange-text-annotation.cs) | Add Orange Text Annotation to PDF | `Document`, `Page`, `Rectangle` | Shows how to create a TextAnnotation on a PDF page, set its color to orange, and save the modifie... |
| [add-red-dashed-line-annotation](./add-red-dashed-line-annotation.cs) | Add Red Dashed Line Annotation to PDF Page | `Document`, `Page`, `LineAnnotation` | Shows how to load a PDF, create a red dashed line annotation on page 2, and save the modified doc... |
| [add-red-text-annotation-to-pdf-page](./add-red-text-annotation-to-pdf-page.cs) | Add Red Text Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, creating a red text annotation on page 2, and saving the modified doc... |
| [add-text-annotation-to-pdf-page__v5](./add-text-annotation-to-pdf-page__v5.cs) | Add Text Annotation to PDF Page | `Document`, `Rectangle`, `TextAnnotation` | Shows how to load a PDF, create a TextAnnotation titled "Comment" on page 2, and save the modifie... |
| [add-text-annotation-to-pdf-page__v6](./add-text-annotation-to-pdf-page__v6.cs) | Add Text Annotation to PDF Page | `Document`, `Page`, `Rectangle` | Shows how to load a PDF, create a TextAnnotation on the first page, set its Subject, and save the... |
| [add-text-annotation-to-pdf-page__v7](./add-text-annotation-to-pdf-page__v7.cs) | Add Text Annotation to PDF Page with Open Flag | `Document`, `BindPdf`, `ModifyAnnotations` | Shows how to load a PDF, bind it to PdfAnnotationEditor, create a TextAnnotation on page 3 with t... |
| [add-text-annotation-to-pdf-page__v8](./add-text-annotation-to-pdf-page__v8.cs) | Add Text Annotation to PDF Page | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Demonstrates loading a PDF, binding it to a PdfAnnotationEditor, creating a TextAnnotation on pag... |
| [add-text-annotation-to-pdf-page__v9](./add-text-annotation-to-pdf-page__v9.cs) | Add Text Annotation to PDF Page | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Demonstrates loading a PDF, binding it to a PdfAnnotationEditor, creating a blue text annotation ... |
| [add-text-annotation-update-moddate](./add-text-annotation-update-moddate.cs) | Add Text Annotation to PDF Page and Update Modified Date | `Document`, `BindPdf`, `ModifyAnnotations` | Demonstrates loading a PDF, binding it with PdfAnnotationEditor, adding a TextAnnotation on page ... |
| [add-text-annotation-with-custom-flags](./add-text-annotation-with-custom-flags.cs) | Add Custom Annotation Flags to PDF | `Document`, `Page`, `Annotation` | Demonstrates how to load a PDF, retrieve an annotation, combine existing flags with standard and ... |
| [add-text-annotation-with-opacity](./add-text-annotation-with-opacity.cs) | Add Text Annotation with 0.75 Opacity | `Document`, `Page`, `Rectangle` | Demonstrates how to create a TextAnnotation, set its opacity to 0.75, add it to a PDF page, and s... |
| [add-text-annotation-with-pdfcontenteditor](./add-text-annotation-with-pdfcontenteditor.cs) | Add Text Annotation to PDF with PdfContentEditor | `BindPdf`, `Document`, `Page` | Demonstrates loading a PDF using PdfContentEditor, creating a TextAnnotation titled "Note" on the... |
| [annotation-removal-report](./annotation-removal-report.cs) | Generate Annotation Removal Report for PDF Files | `Document`, `Page`, `PdfAnnotationEditor` | Counts and removes all annotations from each PDF in a folder, saves the cleaned PDFs, and creates... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Add Text Annotation to PDF Asynchronously | `Document`, `Page`, `TextAnnotation` | Demonstrates adding a text annotation to a PDF document with Aspose.Pdf and executing the operati... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to create a backup of a PDF file before flattening its annotations using Aspose.Pdf's P... |
| [batch-delete-pdf-annotations-retain-types](./batch-delete-pdf-annotations-retain-types.cs) | Retain Specific Annotation Types During PDF Batch Deletion | `Document`, `Page`, `AnnotationCollection` | Demonstrates loading a JSON configuration that lists annotation types to keep, then iterates thro... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Console Progress Bar | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to delete all annotations from multiple PDF files using Aspose.Pdf.Facades.PdfAn... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Processes each PDF in a specified folder, removes all stamp annotations using PdfAnnotationEditor... |
| [batch-export-delete-annotations-xfdf](./batch-export-delete-annotations-xfdf.cs) | Export PDF Annotations to XFDF, Delete Them, and Archive | `Document`, `PdfAnnotationEditor`, `ExportAnnotationsXfdf` | Processes each PDF in a folder, exports its annotations to an XFDF file, removes the annotations ... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates flattening annotations in multiple PDF files using Aspose.Pdf's PdfAnnotationEditor ... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Iterates through all PDF files in a folder, flattens their annotations using Aspose.Pdf, and save... |
| ... | | | *and 176 more files* |

## Category Statistics
- Total examples: 206

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Annotations.AnnotationType`
- `Aspose.Pdf.Facades.PdfAnnotationEditor`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.BindPdf`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.DeleteAnnotations()`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.DeleteAnnotations(string)`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.ExportAnnotationsXfdf`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.Save`
- `Aspose.Pdf.Facades.PdfAnnotationEditor.Save(string)`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfContentEditor.BindPdf`
- `Aspose.Pdf.Facades.PdfContentEditor.CreateFileAttachment`
- `Aspose.Pdf.Facades.PdfContentEditor.Save`

### Rules
- Instantiate Aspose.Pdf.Facades.PdfContentEditor, bind the source PDF via BindPdf({input_pdf}), then call CreateFileAttachment({rect}, {string_literal}, {string_literal}, {int}, {string_literal}, {float}) where the parameters are the annotation rectangle, description, attached file path, page number, icon name, and icon transparency.
- After adding the annotation, persist the changes by invoking Save({output_pdf}) on the same PdfContentEditor instance.
- To delete all annotations: instantiate {class:PdfAnnotationEditor}, call BindPdf({input_pdf}), invoke DeleteAnnotations(), then Save({output_pdf}).
- PdfAnnotationEditor must be bound to a PDF via BindPdf before any annotation‑related methods (e.g., DeleteAnnotations) can be used.
- Bind a PDF file ({input_pdf}) to a PdfAnnotationEditor instance using BindPdf before any annotation operations.

### Warnings
- The example uses System.Drawing.Rectangle for the annotation bounds, which requires a reference to System.Drawing.Common on non‑Windows platforms.
- Transparency support may depend on the chosen icon and PDF viewer.
- The example does not use a using statement for FileStream; callers should ensure proper disposal.
- Only FreeText and Line annotation types are shown; other types can be included by adding their string names to the array.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-27 | Run: `20261127_013318_3d25e2`
<!-- AUTOGENERATED:END -->

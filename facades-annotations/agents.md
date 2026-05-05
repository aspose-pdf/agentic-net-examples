---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (103/106 files) ← category-specific
- `using Aspose.Pdf;` (62/106 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (43/106 files)
- `using Aspose.Pdf.Drawing;` (2/106 files)
- `using Aspose.Pdf.Text;` (1/106 files)
- `using System;` (106/106 files)
- `using System.IO;` (99/106 files)
- `using System.Collections.Generic;` (17/106 files)
- `using System.Threading;` (4/106 files)
- `using System.Threading.Tasks;` (4/106 files)
- `using System.Diagnostics;` (3/106 files)
- `using System.Text.Json;` (3/106 files)
- `using System.Xml.Linq;` (3/106 files)
- `using NUnit.Framework;` (2/106 files)
- `using System.Linq;` (2/106 files)
- `using Azure;` (1/106 files)
- `using Azure.Storage.Blobs;` (1/106 files)
- `using Azure.Storage.Blobs.Models;` (1/106 files)
- `using System.Drawing;` (1/106 files)
- `using System.IO.Compression;` (1/106 files)
- `using System.Text;` (1/106 files)
- `using System.Xml;` (1/106 files)

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
| [add-custom-annotation-with-flags](./add-custom-annotation-with-flags.cs) | Add Custom Annotation with Flags to PDF | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotations` | Demonstrates creating a TextAnnotation, setting custom annotation flags, and applying it to a PDF... |
| [add-custom-metadata-to-pdf-annotation](./add-custom-metadata-to-pdf-annotation.cs) | Add Custom Metadata to PDF Annotation | `Document`, `Page`, `TextAnnotation` | Demonstrates how to add a text annotation to a PDF page and embed custom metadata by storing a JS... |
| [annotation-summary-report](./annotation-summary-report.cs) | Generate Annotation Summary Report for PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Shows how to load PDF files, extract all annotations with PdfAnnotationEditor, count them by type... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Asynchronous PDF Annotation Operations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to perform common PDF annotation tasks (flatten, delete, import, export) asynchr... |
| [backup-pdf-and-delete-annotations](./backup-pdf-and-delete-annotations.cs) | Backup Original PDF and Delete Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Creates a backup of the source PDF, then removes all annotations using PdfAnnotationEditor and sa... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Form Fields | `Form`, `FlattenAllFields`, `Save` | Demonstrates how to create a backup of a PDF before flattening its form fields using Aspose.Pdf.F... |
| [batch-delete-annotations-azure-blob](./batch-delete-annotations-azure-blob.cs) | Batch Delete Annotations from PDFs in Azure Blob Storage | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to stream PDF blobs from Azure Blob Storage, use Aspose.Pdf.Facades.PdfAnnotatio... |
| [batch-delete-pdf-annotations-by-type](./batch-delete-pdf-annotations-by-type.cs) | Batch Delete PDF Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `BindPdf`, `Save` | Shows how to load a JSON configuration that lists annotation types to keep, iterate through each ... |
| [batch-delete-pdf-annotations-progress-bar](./batch-delete-pdf-annotations-progress-bar.cs) | Batch Delete PDF Annotations with Progress Bar | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates iterating over PDFs in a folder, removing all annotations using PdfAnnotationEditor,... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to iterate over PDF files in a directory and remove all stamp annotations using Aspose.... |
| [batch-export-delete-pdf-annotations-xfdf](./batch-export-delete-pdf-annotations-xfdf.cs) | Batch Export and Delete PDF Annotations with XFDF Archiving | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Demonstrates iterating over PDF files, exporting all annotations to XFDF using PdfAnnotationEdito... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `Document`, `PdfAnnotationEditor`, `Save` | Demonstrates how to process a batch of PDF files, flatten their annotations using Aspose.Pdf, and... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations in a Folder | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to iterate over all PDF files in a directory and use Aspose.Pdf.Facades.PdfAnnotationEd... |
| [batch-import-xfdf-annotations-into-pdfs](./batch-import-xfdf-annotations-into-pdfs.cs) | Batch Import XFDF Annotations into PDFs | `PdfAnnotationEditor`, `BindPdf`, `ImportAnnotationsFromXfdf` | Shows how to scan a folder for PDF files, locate matching XFDF files by name, import the annotati... |
| [batch-remove-pdf-annotations-summary](./batch-remove-pdf-annotations-summary.cs) | Batch Remove Annotations from PDFs and Generate Summary Repo... | `Document`, `PdfAnnotationEditor`, `BindPdf` | The example iterates through PDF files in a folder, counts and deletes all annotations using Aspo... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author in Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Shows how to loop through PDF files in a directory and use PdfAnnotationEditor.ModifyAnnotationsA... |
| [benchmark-deleteannotations-vs-deleteannotation](./benchmark-deleteannotations-vs-deleteannotation.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to measure and compare the performance of deleting all annotations at once with DeleteA... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check Duplicate Annotation Names in PDF | `PdfAnnotationEditor`, `BindPdf`, `Document` | Shows how to traverse all pages and annotations in a PDF, collect annotation names, and log any d... |
| [clone-modify-text-annotation](./clone-modify-text-annotation.cs) | Clone and Modify a Text Annotation in PDF | `Document`, `BindPdf`, `Page` | Demonstrates how to clone the first TextAnnotation on a PDF page, change its properties such as t... |
| [clone-recolor-annotation-to-another-page](./clone-recolor-annotation-to-another-page.cs) | Clone and Recolor PDF Annotation to Another Page | `PdfAnnotationEditor`, `Document`, `Page` | Shows how to clone an existing annotation, change its color, and add the cloned annotation to a d... |
| [compile-pdf-annotations-summary](./compile-pdf-annotations-summary.cs) | Compile PDF Text Annotations into a Summary Document | `PdfAnnotationEditor`, `BindPdf`, `ExtractAnnotations` | Demonstrates how to extract text (comment) annotations from an existing PDF using PdfAnnotationEd... |
| [concurrent-delete-import-annotations](./concurrent-delete-import-annotations.cs) | Concurrent Delete Pages and Import Annotations in PDF | `PdfFileEditor`, `TryDelete`, `PdfAnnotationEditor` | Demonstrates running page deletion and annotation import on the same PDF simultaneously using Asp... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Shows how to export annotations from a template PDF to an XFDF stream and import them into severa... |
| [count-pdf-annotation-types](./count-pdf-annotation-types.cs) | Count PDF Annotation Types | `Document`, `PdfAnnotationEditor`, `BindPdf` | Shows how to load a PDF, extract all annotations with PdfAnnotationEditor, and return a dictionar... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations from a PDF using PdfAnnotationEditor | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to bind a PDF to the PdfAnnotationEditor facade, remove every annotation, and sa... |
| [delete-all-annotations-from-pdfs](./delete-all-annotations-from-pdfs.cs) | Delete All Annotations from PDFs in a Folder | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to bind each PDF file in a directory, remove all annotations with PdfAnnotationE... |
| [delete-all-annotations-verify-count](./delete-all-annotations-verify-count.cs) | Delete All Annotations and Verify Count | `Document`, `PdfAnnotationEditor`, `DeleteAnnotations` | The example loads a PDF, counts existing annotations, removes all annotations using PdfAnnotation... |
| [delete-and-flatten-pdf-annotations](./delete-and-flatten-pdf-annotations.cs) | Delete and Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates using Aspose.Pdf.Facades.PdfAnnotationEditor to remove all annotations from a PDF, f... |
| [delete-annotation-with-error-handling](./delete-annotation-with-error-handling.cs) | Delete Annotation with Error Handling in PDF | `Document`, `PdfAnnotationEditor`, `DeleteAnnotation` | Demonstrates how to delete a specific annotation from a PDF using Aspose.Pdf.Facades and graceful... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Delete Annotations by Author Using PdfAnnotationEditor | `PdfAnnotationEditor`, `Document`, `Page` | Shows how to filter and remove PDF annotations whose author (Title) matches a specific name by us... |
| ... | | | *and 76 more files* |

## Category Statistics
- Total examples: 106

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-05 | Run: `20260505_111249_df0b7c`
<!-- AUTOGENERATED:END -->

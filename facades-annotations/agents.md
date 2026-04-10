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

- `using Aspose.Pdf.Facades;` (103/107 files) ← category-specific
- `using Aspose.Pdf;` (69/107 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (41/107 files)
- `using Aspose.Pdf.Text;` (2/107 files)
- `using Aspose.Pdf.Drawing;` (1/107 files)
- `using System;` (107/107 files)
- `using System.IO;` (103/107 files)
- `using System.Collections.Generic;` (18/107 files)
- `using System.Threading.Tasks;` (5/107 files)
- `using System.Diagnostics;` (4/107 files)
- `using System.Threading;` (4/107 files)
- `using System.Linq;` (3/107 files)
- `using System.Text.Json;` (3/107 files)
- `using System.Xml.Linq;` (3/107 files)
- `using NUnit.Framework;` (2/107 files)
- `using System.Drawing;` (2/107 files)
- `using System.Text;` (2/107 files)
- `using Azure.Storage.Blobs;` (1/107 files)
- `using Azure.Storage.Blobs.Models;` (1/107 files)
- `using System.IO.Compression;` (1/107 files)
- `using System.Xml;` (1/107 files)
- `using System.Xml.Schema;` (1/107 files)

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
| [add-custom-annotation-flags](./add-custom-annotation-flags.cs) | Add Custom Annotation Flags to PDF | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates creating a TextAnnotation, setting custom annotation flags (Print and Locked), and a... |
| [add-custom-metadata-to-pdf-annotation](./add-custom-metadata-to-pdf-annotation.cs) | Add Custom Metadata to PDF Annotation via Facades | `Document`, `Page`, `Rectangle` | Demonstrates how to embed custom key/value pairs into a PDF annotation by modifying its dictionar... |
| [annotation-workflow-verbose-logging](./annotation-workflow-verbose-logging.cs) | Annotation Workflow with Verbose Logging | `Document`, `BindPdf`, `CreateText` | Demonstrates loading a PDF, creating a text annotation, exporting and importing annotations via X... |
| [apply-read-only-flag-to-pdf-annotation](./apply-read-only-flag-to-pdf-annotation.cs) | Apply Read‑Only Flag to a PDF Annotation | `Document`, `Page`, `Rectangle` | Demonstrates how to set the ReadOnly flag on a PDF annotation using Aspose.Pdf's Facades API and ... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Asynchronous PDF Annotation Operations with Aspose.Pdf | `Document`, `PdfAnnotationEditor`, `FlatteningAnnotations` | Shows how to flatten, delete, import, and export PDF annotations asynchronously by wrapping Aspos... |
| [backup-and-delete-all-annotations](./backup-and-delete-all-annotations.cs) | Backup PDF and Delete All Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to create a backup copy of a PDF file and then remove all annotations using Aspose.Pdf.... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Form Fields | `Document`, `Save`, `Form` | Shows how to create a backup of a PDF file before flattening all form fields using Aspose.Pdf's F... |
| [batch-delete-pdf-annotations](./batch-delete-pdf-annotations.cs) | Batch Delete PDF Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to read a JSON configuration that lists annotation types to keep, then removes all othe... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to iterate through a directory of PDF files and remove all stamp annotations using Aspo... |
| [batch-export-delete-archive-pdf-annotations](./batch-export-delete-archive-pdf-annotations.cs) | Batch Export, Delete, and Archive PDF Annotations to XFDF | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Demonstrates how to iterate over PDF files, export all annotations to XFDF, delete the annotation... |
| [batch-flatten-pdf-annotations-skip-readonly](./batch-flatten-pdf-annotations-skip-readonly.cs) | Batch Flatten PDF Annotations with Optional Read‑Only Skip | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to batch‑process PDF files to flatten their annotations using Aspose.Pdf, with a... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations in a Folder | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to iterate over PDF files in a directory and use Aspose.Pdf.Facades.PdfAnnotationEditor... |
| [batch-flatten-pdf-annotations__v2](./batch-flatten-pdf-annotations__v2.cs) | Batch Flatten PDF Annotations with Cancellation | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to flatten annotations in multiple PDF files using Aspose.Pdf's PdfAnnotationEdi... |
| [batch-import-xfdf-annotations](./batch-import-xfdf-annotations.cs) | Batch Import XFDF Annotations into PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to import annotation data from matching XFDF files into multiple PDF documents i... |
| [batch-process-pdfs-azure-blob](./batch-process-pdfs-azure-blob.cs) | Batch Process PDFs from Azure Blob Storage with PdfAnnotatio... | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to stream PDF files from an Azure Blob container, flatten all annotations using Aspose.... |
| [batch-remove-annotations-with-progress](./batch-remove-annotations-with-progress.cs) | Batch Remove Annotations from PDFs with Progress Indicator | `Document`, `Page`, `Save` | Shows how to process a folder of PDF files, clear all annotations from each page using Aspose.Pdf... |
| [batch-remove-old-annotations](./batch-remove-old-annotations.cs) | Batch Remove Old Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to iterate through all PDF files in a folder and delete annotations whose modifi... |
| [batch-remove-pdf-annotations-report](./batch-remove-pdf-annotations-report.cs) | Batch Remove PDF Annotations and Generate Report | `Document`, `Page`, `PdfAnnotationEditor` | Loads each PDF in a folder, counts existing annotations, deletes all annotations using Aspose.Pdf... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Shows how to iterate over PDF files, bind each to PdfAnnotationEditor, and replace the author of ... |
| [benchmark-deleteannotations-vs-deleteannotation](./benchmark-deleteannotations-vs-deleteannotation.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation | `Document`, `Page`, `TextAnnotation` | Demonstrates how to measure the performance of removing all annotations with DeleteAnnotations ve... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check Duplicate Annotation Names in PDF | `PdfAnnotationEditor`, `BindPdf`, `Document` | Demonstrates how to use Aspose.Pdf to scan a PDF for annotations that share the same Name and log... |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone PDF Annotation, Change Color, and Move to Another Page | `Document`, `Page`, `Annotation` | Shows how to clone a text annotation from one page, change its color, and add the cloned annotati... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `PdfAnnotationEditor`, `Page` | Shows how to clone the first annotation on a PDF page, change its properties (color, contents, et... |
| [concurrent-pdf-delete-insert-test](./concurrent-pdf-delete-insert-test.cs) | Concurrent PDF Delete and Insert Test | `PdfFileEditor`, `TryDelete`, `Insert` | Demonstrates running delete and page‑import operations on the same PDF concurrently using separat... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from a Template PDF to Multiple PDFs | `Document`, `Rectangle`, `Color` | Demonstrates how to export annotations from a template PDF and import them into several target PD... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations from a PDF and Verify Count | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to count annotations on each page, remove all annotations using PdfAnnotationEditor, an... |
| [delete-all-annotations-from-pdf__v2](./delete-all-annotations-from-pdf__v2.cs) | Delete All Annotations from PDF Using PdfAnnotationEditor | `Document`, `Page`, `Rectangle` | Demonstrates creating a PDF with text annotations, then using PdfAnnotationEditor.DeleteAnnotatio... |
| [delete-annotation-with-error-handling](./delete-annotation-with-error-handling.cs) | Delete Annotation with Error Handling | `PdfAnnotationEditor`, `PdfException` | Shows how to delete a PDF annotation using PdfAnnotationEditor and catch exceptions when the spec... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Delete Annotations by Author Using PdfAnnotationEditor | `Document`, `Page`, `Annotation` | The example loads a PDF, finds markup annotations whose author (Title) matches a specified name, ... |
| [delete-annotations-export-xfdf](./delete-annotations-export-xfdf.cs) | Delete Specific Annotations and Export Remaining to XFDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to remove all annotations of a given type from a PDF using PdfAnnotationEditor and then... |
| ... | | | *and 77 more files* |

## Category Statistics
- Total examples: 107

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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->

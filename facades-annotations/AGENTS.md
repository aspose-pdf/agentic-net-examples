---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **107** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (104/107 files) ← category-specific
- `using Aspose.Pdf;` (63/107 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (42/107 files)
- `using Aspose.Pdf.Drawing;` (2/107 files)
- `using Aspose.Pdf.Text;` (2/107 files)
- `using System;` (107/107 files)
- `using System.IO;` (101/107 files)
- `using System.Collections.Generic;` (17/107 files)
- `using System.Linq;` (6/107 files)
- `using System.Threading;` (5/107 files)
- `using System.Threading.Tasks;` (4/107 files)
- `using NUnit.Framework;` (3/107 files)
- `using System.Diagnostics;` (3/107 files)
- `using System.Text.Json;` (3/107 files)
- `using System.Xml.Linq;` (3/107 files)
- `using System.Text;` (2/107 files)
- `using System.Xml;` (2/107 files)
- `using Azure.Storage.Blobs;` (1/107 files)
- `using Azure.Storage.Blobs.Models;` (1/107 files)
- `using System.Drawing;` (1/107 files)
- `using System.IO.Compression;` (1/107 files)
- `using System.Windows.Forms;` (1/107 files)

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
| [add-custom-annotation-flags-to-pdf](./add-custom-annotation-flags-to-pdf.cs) | Add Custom Annotation Flags to PDF Pages | `Document`, `Page`, `TextAnnotation` | Demonstrates how to create a TextAnnotation, set custom annotation flags, and apply it to all pag... |
| [add-custom-metadata-to-pdf-annotation](./add-custom-metadata-to-pdf-annotation.cs) | Add Custom Metadata to PDF Annotation | `Document`, `Page`, `Rectangle` | Shows how to create a text annotation, embed custom metadata (as JSON) in its dictionary via the ... |
| [annotation-diagnostic-workflow](./annotation-diagnostic-workflow.cs) | Diagnostic Annotation Workflow with Verbose Logging | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Demonstrates loading a PDF, adding a text annotation, exporting annotations to XFDF, and saving t... |
| [apply-read-only-flag-to-pdf-annotations](./apply-read-only-flag-to-pdf-annotations.cs) | Apply Read‑Only Flag to PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotations` | Demonstrates how to set the ReadOnly flag on a TextAnnotation and apply it to all pages of a PDF ... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Asynchronous PDF Annotation Operations with Aspose.Pdf | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to perform common PDF annotation tasks—flattening, deleting, importing, and expo... |
| [backup-pdf-before-flattening](./backup-pdf-before-flattening.cs) | Backup PDF and Flatten Form Fields | `Form`, `FlattenAllFields`, `Save` | Demonstrates creating a backup copy of a PDF file and then flattening all form fields using Aspos... |
| [backup-pdf-delete-annotations](./backup-pdf-delete-annotations.cs) | Backup PDF and Delete All Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to create a backup copy of a PDF file and then remove all annotations using Aspo... |
| [batch-add-text-annotation-to-pdfs](./batch-add-text-annotation-to-pdfs.cs) | Batch Add Text Annotation to PDFs from Azure Blob Storage | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to enumerate PDF blobs in Azure Blob Storage, stream each into Aspose.Pdf's PdfA... |
| [batch-annotation-deletion-retain-types](./batch-annotation-deletion-retain-types.cs) | Batch Delete PDF Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `AnnotationType`, `TextAnnotation` | Shows how to delete all annotations from a PDF except those listed in a JSON configuration file, ... |
| [batch-delete-pdf-annotations-progress](./batch-delete-pdf-annotations-progress.cs) | Batch Delete PDF Annotations with Progress Indicator | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to remove all annotations from multiple PDF files using Aspose.Pdf.Facades and s... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to iterate through PDF files in a folder and remove all stamp annotations using Aspose.... |
| [batch-export-delete-archive-pdf-annotations](./batch-export-delete-archive-pdf-annotations.cs) | Batch Export, Delete, and Archive PDF Annotations to XFDF | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Shows how to process a folder of PDFs, export all annotations to XFDF files, delete the annotatio... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to flatten annotations in multiple PDF files using Aspose.Pdf.Facades with async... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations in a Folder | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to loop through all PDF files in a directory, flatten their annotations with Aspose.Pdf... |
| [batch-import-xfdf-annotations-into-pdfs](./batch-import-xfdf-annotations-into-pdfs.cs) | Batch Import XFDF Annotations into Matching PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to iterate through a folder of PDFs, locate matching XFDF files by name, and imp... |
| [batch-remove-annotations-report](./batch-remove-annotations-report.cs) | Batch Remove Annotations from PDFs and Generate Report | `Document`, `PdfAnnotationEditor`, `Pages` | The example iterates through PDF files, counts existing annotations on each page, removes all ann... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Shows how to loop through PDF files in a directory and use PdfAnnotationEditor.ModifyAnnotationsA... |
| [benchmark-deleteannotations-vs-deleteannotation](./benchmark-deleteannotations-vs-deleteannotation.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation | `Document`, `PdfAnnotationEditor`, `DeleteAnnotations` | Demonstrates how to measure and compare the performance of removing all annotations with DeleteAn... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in PDF | `PdfAnnotationEditor`, `BindPdf`, `Document` | Opens a PDF, iterates through all pages and annotations, counts annotation names, and logs any du... |
| [clone-and-recolor-pdf-annotation](./clone-and-recolor-pdf-annotation.cs) | Clone and Recolor PDF Annotation | `Document`, `Page`, `Annotation` | Shows how to clone an existing annotation from one page, change its color, and add the cloned ann... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify a PDF Annotation with Aspose.Pdf | `Document`, `PdfAnnotationEditor`, `Page` | Demonstrates how to clone the first TextAnnotation on a specified page, change its properties, an... |
| [concurrent-import-delete-pdf](./concurrent-import-delete-pdf.cs) | Concurrent Import and Delete Operations on PDF | `Document`, `PdfFileEditor`, `Pages` | Demonstrates how to run page import and page delete operations on the same PDF concurrently using... |
| [conditional-pdf-flattening-based-on-digital-signat...](./conditional-pdf-flattening-based-on-digital-signatures.cs) | Conditional PDF Flattening Based on Digital Signatures | `Document`, `PdfFileSignature`, `Flatten()` | The example checks if a PDF contains digital signatures and only flattens form fields when no sig... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Shows how to export annotations from a template PDF to an in‑memory XFDF stream and import them i... |
| [count-annotations-before-after-deletion](./count-annotations-before-after-deletion.cs) | Count Annotations Before and After Deletion | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to bind a PDF, count its annotations, delete all annotations using PdfAnnotation... |
| [count-pdf-annotation-types](./count-pdf-annotation-types.cs) | Count PDF Annotation Types | `PdfAnnotationEditor`, `BindPdf`, `Document` | Shows how to iterate through a PDF and count the number of occurrences for each annotation type u... |
| [delete-all-annotations-from-pdfs](./delete-all-annotations-from-pdfs.cs) | Delete All Annotations from PDFs in a Folder | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to loop through PDF files in a directory, bind each with PdfAnnotationEditor, remove ev... |
| [delete-annotation-by-name](./delete-annotation-by-name.cs) | Delete PDF Annotation by Name | `Document`, `Page`, `TextAnnotation` | Demonstrates how to remove a PDF annotation whose Name (ID) is stored in a variable using Aspose.... |
| [delete-annotations-by-color](./delete-annotations-by-color.cs) | Delete Annotations by Color in PDF | `PdfAnnotationEditor`, `FromArgb`, `Document` | Shows how to remove PDF annotations whose Color property matches a specific RGB value using the P... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

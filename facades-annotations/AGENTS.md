---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **105** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (103/105 files) ← category-specific
- `using Aspose.Pdf;` (66/105 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (45/105 files)
- `using Aspose.Pdf.Drawing;` (2/105 files)
- `using Aspose.Pdf.Text;` (2/105 files)
- `using System;` (105/105 files)
- `using System.IO;` (95/105 files)
- `using System.Collections.Generic;` (17/105 files)
- `using System.Diagnostics;` (4/105 files)
- `using System.Linq;` (4/105 files)
- `using System.Threading.Tasks;` (4/105 files)
- `using System.Threading;` (3/105 files)
- `using System.Text;` (2/105 files)
- `using System.Xml;` (2/105 files)
- `using System.Xml.Linq;` (2/105 files)
- `using Azure.Storage.Blobs;` (1/105 files)
- `using Azure.Storage.Blobs.Models;` (1/105 files)
- `using Microsoft.VisualStudio.TestTools.UnitTesting;` (1/105 files)
- `using System.IO.Compression;` (1/105 files)
- `using System.Runtime.CompilerServices;` (1/105 files)
- `using System.Text.Json;` (1/105 files)
- `using System.Xml.Schema;` (1/105 files)

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
| [add-custom-metadata-to-pdf-annotation](./add-custom-metadata-to-pdf-annotation.cs) | Add Custom Metadata to a PDF Annotation | `Document`, `Page`, `TextAnnotation` | Demonstrates how to create a TextAnnotation, store custom key/value metadata using PdfFileInfo, o... |
| [add-text-annotation-with-custom-flags](./add-text-annotation-with-custom-flags.cs) | Add Text Annotation with Custom Flags to PDF | `PdfAnnotationEditor`, `TextAnnotation`, `AnnotationFlags` | Demonstrates creating a TextAnnotation, setting custom annotation flags (Invisible, NoZoom), and ... |
| [annotation-removal-report](./annotation-removal-report.cs) | Annotation Removal Report for PDFs | `Document`, `Page`, `PdfAnnotationEditor` | Demonstrates how to count and delete all annotations from multiple PDF files using Aspose.Pdf and... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Asynchronous PDF Annotation Operations with Aspose.Pdf | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to perform common PDF annotation tasks—flattening, deleting, importing, and expo... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Form Fields | `Document`, `Form`, `FlattenAllFields` | Demonstrates how to create a backup of a PDF before flattening all interactive form fields using ... |
| [batch-delete-pdf-annotations-azure-blob](./batch-delete-pdf-annotations-azure-blob.cs) | Batch Delete PDF Annotations from Azure Blob Storage | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to enumerate PDF blobs in an Azure Blob container, stream each PDF into Aspose.P... |
| [batch-delete-pdf-annotations-retain-types](./batch-delete-pdf-annotations-retain-types.cs) | Batch Delete PDF Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `AnnotationType`, `BindPdf` | Demonstrates how to load a PDF, read a JSON configuration that lists annotation types to keep, an... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Progress | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to remove all annotations from multiple PDF files using Aspose.Pdf.Facades.PdfAn... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to iterate through PDF files in a folder and remove all stamp annotations using ... |
| [batch-export-delete-annotations-xfdf](./batch-export-delete-annotations-xfdf.cs) | Batch Export Annotations to XFDF and Remove Them | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Shows how to process a folder of PDFs, export all annotations to XFDF files for archiving, delete... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates flattening all annotations in multiple PDF files using Aspose.Pdf's PdfAnnotationEdi... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Processes every PDF in a specified folder, flattens all annotations using Aspose.Pdf's PdfAnnotat... |
| [batch-import-xfdf-annotations-into-pdfs](./batch-import-xfdf-annotations-into-pdfs.cs) | Batch Import XFDF Annotations into Matching PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates iterating over PDF files, locating corresponding XFDF files by name, importing their... |
| [batch-remove-old-annotations](./batch-remove-old-annotations.cs) | Batch Remove Old Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to iterate through PDFs in a folder and delete annotations whose modification da... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Shows how to iterate through PDF files in a directory and use PdfAnnotationEditor to replace the ... |
| [benchmark-delete-annotations](./benchmark-delete-annotations.cs) | Benchmark Deleting PDF Annotations with Aspose.Pdf | `Document`, `Page`, `TextAnnotation` | Creates a PDF with multiple text annotations and measures the performance of deleting all annotat... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in a PDF | `PdfAnnotationEditor`, `Document`, `Page` | Shows how to iterate through all annotations in a PDF with Aspose.Pdf, detect duplicate annotatio... |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone PDF Annotation, Change Its Color, and Add to Another P... | `Document`, `Page`, `Annotation` | Demonstrates how to load a PDF, clone an existing annotation, modify its color, and place the clo... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `PdfAnnotationEditor`, `BindPdf`, `Save` | Shows how to clone an existing annotation on a PDF page, change its properties, and add the clone... |
| [concurrent-insert-delete-pdf](./concurrent-insert-delete-pdf.cs) | Concurrent Insert and Delete Operations on a PDF | `PdfFileEditor`, `TryInsert`, `TryDelete` | Demonstrates how to safely perform simultaneous page insertions and deletions on the same PDF usi... |
| [conditional-pdf-flattening](./conditional-pdf-flattening.cs) | Conditional PDF Flattening Based on Digital Signatures | `Document`, `PdfFileSignature`, `BindPdf` | The example checks if a PDF contains digital signatures using PdfFileSignature and only flattens ... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `PdfAnnotationEditor`, `Document`, `Page` | Demonstrates exporting annotations from a template PDF to an XFDF stream and importing them into ... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations from PDF and Verify Count | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to bind a PDF with PdfAnnotationEditor, count existing annotations, remove them using D... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Delete Annotations by Author Using Aspose.Pdf | `Document`, `Page`, `Annotation` | Shows how to filter PDF annotations by the author's Title and remove them using PdfAnnotationEditor. |
| [delete-annotations-export-xfdf](./delete-annotations-export-xfdf.cs) | Delete Specific Annotations and Export Remaining to XFDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to remove all annotations of a given type (e.g., Text) from a PDF using PdfAnnot... |
| [delete-annotations-from-pdf-using-config](./delete-annotations-from-pdf-using-config.cs) | Delete Specific Annotations from PDF Using Configuration Fil... | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to read annotation types from a text configuration file and delete those (or all) annot... |
| [delete-annotations-from-pdf](./delete-annotations-from-pdf.cs) | Delete All Annotations from PDF Using PdfAnnotationEditor | `Document`, `Page`, `TextAnnotation` | Shows how to create a PDF with text annotations, remove them with PdfAnnotationEditor.DeleteAnnot... |
| [delete-annotations-with-backup](./delete-annotations-with-backup.cs) | Delete All Annotations with Optional Backup | `Document`, `PdfAnnotationEditor`, `DeleteAnnotations` | Demonstrates how to create a backup copy of a PDF before removing all annotations using Aspose.Pd... |
| [delete-flatten-pdf-annotations](./delete-flatten-pdf-annotations.cs) | Delete and Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates using Aspose.Pdf.Facades.PdfAnnotationEditor to remove all annotations from a PDF, f... |
| [delete-pdf-annotation-by-name](./delete-pdf-annotation-by-name.cs) | Delete a PDF Annotation by Name | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Shows how to remove a specific annotation identified by its name from a PDF using Aspose.Pdf.Faca... |
| ... | | | *and 75 more files* |

## Category Statistics
- Total examples: 105

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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

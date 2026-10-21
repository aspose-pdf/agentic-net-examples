---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **98** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (78/98 files) ← category-specific
- `using Aspose.Pdf.Facades;` (54/98 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (44/98 files)
- `using Aspose.Pdf.Text;` (2/98 files)
- `using Aspose.Pdf.Devices;` (1/98 files)
- `using Aspose.Pdf.Forms;` (1/98 files)
- `using System;` (98/98 files)
- `using System.IO;` (94/98 files)
- `using System.Collections.Generic;` (21/98 files)
- `using System.Linq;` (7/98 files)
- `using System.Threading.Tasks;` (5/98 files)
- `using System.Diagnostics;` (3/98 files)
- `using System.Reflection;` (3/98 files)
- `using System.Text.Json;` (3/98 files)
- `using System.Threading;` (3/98 files)
- `using System.IO.Compression;` (2/98 files)
- `using System.Xml.Linq;` (2/98 files)
- `using Azure.Storage.Blobs;` (1/98 files)
- `using Azure.Storage.Blobs.Models;` (1/98 files)
- `using NUnit.Framework;` (1/98 files)
- `using System.Xml;` (1/98 files)
- `using System.Xml.Schema;` (1/98 files)

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
| [add-text-annotation-with-custom-flags](./add-text-annotation-with-custom-flags.cs) | Add Custom Annotation Flags to PDF | `Document`, `Page`, `Annotation` | Demonstrates how to iterate over annotations on a PDF page and set custom flags such as NoView us... |
| [annotation-removal-report](./annotation-removal-report.cs) | Generate PDF Annotation Removal Report | `Document`, `Page`, `PdfAnnotationEditor` | The sample counts all annotations in each PDF, removes them using Aspose.Pdf's annotation editor,... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Add Text Annotation Asynchronously to PDF | `Document`, `Rectangle`, `TextAnnotation` | Shows how to add a text annotation to a PDF document using Aspose.Pdf's PdfAnnotationEditor wrapp... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to create a backup of a PDF file before flattening its annotations using Aspose.Pdf.Fac... |
| [batch-delete-pdf-annotations-azure-blob](./batch-delete-pdf-annotations-azure-blob.cs) | Batch Add Text Annotation to PDFs in Azure Blob Storage | `PdfAnnotationEditor`, `BindPdf`, `TextAnnotation` | Streams PDF blobs from an Azure Blob container, binds each to Aspose.Pdf's PdfAnnotationEditor, a... |
| [batch-delete-pdf-annotations-retain-types](./batch-delete-pdf-annotations-retain-types.cs) | Batch Delete PDF Annotations Except Specified Types | `Document`, `Page`, `Annotation` | Loads a JSON configuration that lists annotation types to keep, iterates through each page of a P... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Progress Indicator | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates iterating over PDFs in a folder, removing all annotations with PdfAnnotationEditor, ... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Remove Stamp Annotations from PDFs | `Document`, `Page`, `StampAnnotation` | Iterates through all PDF files in a folder, loads each document, finds and deletes every stamp an... |
| [batch-export-delete-annotations-xfdf](./batch-export-delete-annotations-xfdf.cs) | Export, Delete and Archive PDF Annotations to XFDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates a batch process that copies PDFs, exports their annotations to XFDF files, removes a... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to flatten annotations in multiple PDF files using Aspose.Pdf.Facades while supp... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to iterate over all PDF files in a folder and use Aspose.Pdf.Facades.PdfAnnotationEdito... |
| [batch-import-xfdf-annotations-into-pdfs](./batch-import-xfdf-annotations-into-pdfs.cs) | Batch Import XFDF Data into Matching PDFs | `Document`, `Form`, `BindPdf` | Demonstrates iterating over a folder, matching each PDF with an XFDF file of the same name, impor... |
| [batch-remove-old-annotations](./batch-remove-old-annotations.cs) | Batch Remove Old PDF Annotations | `Document`, `Page`, `Annotation` | Shows how to scan PDFs in a folder, find annotations older than a specified number of days, and d... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across PDFs | `Document`, `PdfAnnotationEditor`, `Pages` | Demonstrates how to loop through multiple PDF files and use Aspose.Pdf's PdfAnnotationEditor to c... |
| [benchmark-delete-annotations](./benchmark-delete-annotations.cs) | Benchmark DeleteAnnotation vs DeleteAnnotations Performance | `Document`, `Page`, `TextAnnotation` | Creates a PDF with many text annotations and measures the time required to delete a single annota... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check Duplicate Annotation Names in PDF | `Document`, `Page`, `Annotation` | Loads a PDF, iterates through all pages and their annotations, and reports any annotation names t... |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone Annotation, Change Color, and Add to Another PDF Page | `Document`, `Page`, `Annotation` | Demonstrates how to clone an existing annotation from a source PDF, modify its color, and insert ... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `Page`, `Annotation` | Shows how to clone the first text annotation on a PDF page, change its title, contents and other ... |
| [concurrent-insert-delete-pdf](./concurrent-insert-delete-pdf.cs) | Concurrent Import and Delete Operations on a PDF | `PdfFileEditor`, `Insert`, `Delete` | Demonstrates how to run multiple import and delete operations on the same PDF file concurrently u... |
| [conditional-pdf-flattening](./conditional-pdf-flattening.cs) | Conditional PDF Flattening Preserving Digital Signatures | `Document`, `SignatureField`, `Flatten` | Loads a PDF, checks for digital signature fields and skips flattening to keep signatures valid; o... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `Document`, `Page`, `Annotation` | Demonstrates how to extract annotations from a template PDF, cache them, and import them into sev... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations from a PDF and Verify Removal | `Document`, `Page`, `PageCollection` | Loads a PDF, counts its annotations, removes all of them, saves the result, and then reloads the ... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Remove Annotations by Author from PDF | `Document`, `Page`, `Annotation` | Demonstrates loading a PDF with Aspose.Pdf, iterating through each page's annotations, filtering ... |
| [delete-annotations-export-xfdf](./delete-annotations-export-xfdf.cs) | Delete Text Annotations and Export Remaining to XFDF | `Document`, `Page`, `Annotation` | Demonstrates how to remove all Free‑Text (Text) annotations from a PDF document and then export t... |
| [delete-annotations-from-pdf-using-config](./delete-annotations-from-pdf-using-config.cs) | Delete Specified Annotations from PDF Using Configuration | `Document`, `Page`, `Annotation` | Shows how to read a JSON file that lists annotation types and remove those annotations from a PDF... |
| [delete-annotations-from-pdf](./delete-annotations-from-pdf.cs) | Delete All Annotations from a PDF Page | `Document`, `Page`, `TextAnnotation` | Shows how to create a PDF with text annotations and then remove them using PdfAnnotationEditor wi... |
| [delete-annotations-with-backup](./delete-annotations-with-backup.cs) | Delete All Annotations with Backup Option | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to create a backup of the original PDF and then remove all annotations from the ... |
| [delete-flatten-pdf-annotations](./delete-flatten-pdf-annotations.cs) | Delete and Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates loading a PDF, removing all annotations, flattening any remaining ones (e.g., form f... |
| [delete-pdf-annotation-by-name](./delete-pdf-annotation-by-name.cs) | Delete PDF Annotation by Name using Aspose.Pdf | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to remove a specific annotation from a PDF by passing its name to the DeleteAnno... |
| [delete-pdf-annotation-with-error-handling](./delete-pdf-annotation-with-error-handling.cs) | Delete Annotation with Error Handling | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to delete a PDF annotation using PdfAnnotationEditor and gracefully handle the e... |
| ... | | | *and 68 more files* |

## Category Statistics
- Total examples: 98

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
Updated: 2026-10-21 | Run: `20261021_010254_451208`
<!-- AUTOGENERATED:END -->

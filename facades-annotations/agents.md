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

- `using Aspose.Pdf;` (78/105 files) ← category-specific
- `using Aspose.Pdf.Facades;` (64/105 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (47/105 files)
- `using Aspose.Pdf.Drawing;` (1/105 files)
- `using Aspose.Pdf.Multithreading;` (1/105 files)
- `using Aspose.Pdf.Text;` (1/105 files)
- `using System;` (105/105 files)
- `using System.IO;` (103/105 files)
- `using System.Collections.Generic;` (14/105 files)
- `using System.Diagnostics;` (5/105 files)
- `using System.Threading.Tasks;` (4/105 files)
- `using NUnit.Framework;` (2/105 files)
- `using System.IO.Compression;` (2/105 files)
- `using System.Text;` (2/105 files)
- `using System.Text.Json;` (2/105 files)
- `using System.Threading;` (2/105 files)
- `using System.Xml;` (2/105 files)
- `using Azure.Storage.Blobs;` (1/105 files)
- `using Azure.Storage.Blobs.Models;` (1/105 files)
- `using System.Collections.Concurrent;` (1/105 files)
- `using System.Linq;` (1/105 files)
- `using System.Xml.Linq;` (1/105 files)
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
| [add-annotation-verbose-logging](./add-annotation-verbose-logging.cs) | Add Annotation with Verbose Logging | `Document`, `Page`, `Rectangle` | Demonstrates how to enable verbose logging for annotation operations via a command‑line flag and ... |
| [annotation-count-per-page](./annotation-count-per-page.cs) | Get Annotation Count Per PDF Page | `Document`, `Page`, `Count` | Loads a PDF document and reports the total number of annotations present on each page. |
| [annotation-performance-logger](./annotation-performance-logger.cs) | Log Annotation Operation Durations in PDF | `Document`, `Page`, `TextAnnotation` | Demonstrates how to measure and log the time taken for adding and deleting annotations on each pa... |
| [annotation-summary-report](./annotation-summary-report.cs) | Generate Annotation Type Summary Report for PDFs | `Document`, `Page`, `Annotation` | Scans one or more PDF files, counts each annotation type present, and prints a summary to the con... |
| [async-delete-annotations](./async-delete-annotations.cs) | Asynchronously Delete All Annotations from PDF | `Document`, `PdfAnnotationEditor`, `DeleteAnnotations` | Demonstrates how to perform non‑blocking annotation operations by wrapping PdfAnnotationEditor ca... |
| [backup-before-flattening](./backup-before-flattening.cs) | Backup PDF before Flattening to Prevent Data Loss | `Document`, `FlattenSettings`, `Flatten` | Shows how to automatically back up a PDF file before flattening its form fields and annotations, ... |
| [batch-annotation-flattening-cancel](./batch-annotation-flattening-cancel.cs) | Batch Annotation Flattening with Cancellation Support | `PdfAnnotationEditor`, `InterruptMonitor`, `BindPdf` | Demonstrates how to flatten all annotations in a PDF using PdfAnnotationEditor while allowing the... |
| [batch-delete-annotations-progress](./batch-delete-annotations-progress.cs) | Batch Delete PDF Annotations with Progress Indicator | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Deletes all annotations from multiple PDF files while displaying a console progress percentage. |
| [batch-delete-annotations-report](./batch-delete-annotations-report.cs) | Batch Delete Annotations and Report Count per PDF | `Document`, `PdfAnnotationEditor`, `PageCollection` | Deletes all annotations from each PDF in a folder, saves cleaned PDFs, and prints the number of a... |
| [batch-delete-annotations-retain](./batch-delete-annotations-retain.cs) | Batch Delete Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to read a JSON configuration that lists annotation types to keep, then delete al... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Iterates over all PDF files in a folder and removes stamp annotations using PdfAnnotationEditor. |
| [batch-import-xfdf-annotations](./batch-import-xfdf-annotations.cs) | Batch Import XFDF Annotations into Matching PDFs | `Document`, `ImportAnnotationsFromXfdf`, `Save` | Imports annotations from XFDF files into PDFs where the XFDF file name matches the PDF name, savi... |
| [batch-process-azure-pdf](./batch-process-azure-pdf.cs) | Batch Process PDFs from Azure Blob Storage using PdfAnnotati... | `PdfAnnotationEditor`, `BindPdf`, `Save` | Streams each PDF blob from Azure Blob Storage into Aspose.Pdf.Facades.PdfAnnotationEditor, proces... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author in PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Loops through PDF files in a folder and changes the author of all annotations from a specified so... |
| [benchmark-delete-annotations](./benchmark-delete-annotations.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation in Aspose.Pd... | `Document`, `PdfAnnotationEditor`, `DeleteAnnotations` | Measures and compares the performance of deleting all annotations at once versus deleting a singl... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in PDF | `Document`, `Page`, `AnnotationCollection` | Opens a PDF, scans all annotations, and reports any annotation names that appear more than once. |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone Annotation, Change Color, and Add to Another Page | `Document`, `Clone`, `Color` | Loads a PDF, clones an existing annotation, changes its color, and adds the cloned annotation to ... |
| [clone-modify-annotation](./clone-modify-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `Page`, `Annotation` | Demonstrates cloning an existing annotation, changing its properties, and adding the clone back t... |
| [compare-annotation-counts](./compare-annotation-counts.cs) | Compare Annotation Counts Before and After Deletion | `Document`, `Page`, `PdfAnnotationEditor` | Loads a PDF, reports the number of annotations, deletes all annotations using PdfAnnotationEditor... |
| [concurrent-import-delete](./concurrent-import-delete.cs) | Concurrent Import and Delete Operations on PDF | `Document`, `TryDelete`, `Add` | Demonstrates running page deletion and page import (merge) on the same PDF concurrently using tas... |
| [copy-annotations-template-to-multiple](./copy-annotations-template-to-multiple.cs) | Copy Annotations from Template PDF to Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ExportAnnotationsToXfdf` | Exports annotations from a template PDF to an in‑memory XFDF stream and imports them into several... |
| [count-pdf-annotation-types](./count-pdf-annotation-types.cs) | Count PDF Annotation Types | `Document`, `PdfAnnotationEditor`, `ExtractAnnotations` | Loads a PDF, extracts all annotations, and returns a dictionary with the occurrence count for eac... |
| [delete-all-annotations](./delete-all-annotations.cs) | Delete All Annotations from PDF using PdfAnnotationEditor | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to bind a PDF to PdfAnnotationEditor, remove all annotations, and save the clean... |
| [delete-annotation-by-name](./delete-annotation-by-name.cs) | Delete Annotation by Name from PDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to delete a single PDF annotation identified by its name using PdfAnnotationEditor. |
| [delete-annotation-by-name__v2](./delete-annotation-by-name__v2.cs) | Delete Annotation by Name using PdfAnnotationEditor | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to delete a specific annotation from a PDF by its name using the PdfAnnotationEd... |
| [delete-annotation-error-handling](./delete-annotation-error-handling.cs) | Delete Annotation with Error Handling | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to delete an annotation by name and gracefully handle the case when the annotati... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Delete PDF Annotations by Author | `Document`, `Page`, `Delete` | Demonstrates how to remove annotations from a PDF whose author matches a specified name. |
| [delete-annotations-by-color](./delete-annotations-by-color.cs) | Delete Annotations by Color in PDF | `Document`, `Page`, `Delete` | Demonstrates how to remove only those annotations whose color matches a specific RGB value. |
| [delete-annotations-by-name](./delete-annotations-by-name.cs) | Delete PDF Annotations by Name | `PdfAnnotationEditor`, `DeleteAnnotation`, `BindPdf` | Shows how to delete a PDF annotation by its name using a string literal and a variable. |
| [delete-annotations-export-xfdf](./delete-annotations-export-xfdf.cs) | Delete Specific Annotations and Export Remaining to XFDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to delete annotations of a given type from a PDF using PdfAnnotationEditor and t... |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_213136_a66d65`
<!-- AUTOGENERATED:END -->

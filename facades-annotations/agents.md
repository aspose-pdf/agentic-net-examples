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

- `using Aspose.Pdf.Facades;` (100/106 files) ← category-specific
- `using Aspose.Pdf;` (68/106 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (42/106 files)
- `using Aspose.Pdf.Text;` (3/106 files)
- `using Aspose.Pdf.Drawing;` (1/106 files)
- `using System;` (106/106 files)
- `using System.IO;` (96/106 files)
- `using System.Collections.Generic;` (19/106 files)
- `using System.Linq;` (5/106 files)
- `using System.Threading.Tasks;` (5/106 files)
- `using System.Diagnostics;` (3/106 files)
- `using System.Text.Json;` (3/106 files)
- `using System.Threading;` (3/106 files)
- `using System.Text;` (2/106 files)
- `using System.Xml;` (2/106 files)
- `using Azure.Storage.Blobs;` (1/106 files)
- `using Azure.Storage.Blobs.Models;` (1/106 files)
- `using NUnit.Framework;` (1/106 files)
- `using System.IO.Compression;` (1/106 files)
- `using System.Xml.Linq;` (1/106 files)
- `using System.Xml.Schema;` (1/106 files)

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
| [add-custom-annotation-flags](./add-custom-annotation-flags.cs) | Add Custom Annotation Flags to PDF Annotations | `Document`, `PdfAnnotationEditor`, `TextAnnotation` | Shows how to set custom annotation flags such as Print and Locked on a TextAnnotation and apply t... |
| [annotation-operations-logging](./annotation-operations-logging.cs) | Log Annotation Operations with Aspose PDF | `PdfAnnotationEditor`, `TextAnnotation`, `Rectangle` | Demonstrates how to wrap Aspose.Pdf annotation actions (add, modify, delete, import, save) with a... |
| [annotation-summary-report](./annotation-summary-report.cs) | Generate PDF Annotation Summary Report | `PdfAnnotationEditor`, `BindPdf`, `ExtractAnnotations` | The example loads one or more PDF files, extracts all annotations using the PdfAnnotationEditor f... |
| [apply-read-only-flag-to-pdf-annotations](./apply-read-only-flag-to-pdf-annotations.cs) | Apply Read‑Only Flag to PDF Annotations | `Document`, `Page`, `TextAnnotation` | Demonstrates setting the ReadOnly flag on a TextAnnotation template and applying it to existing a... |
| [async-process-pdf-annotations](./async-process-pdf-annotations.cs) | Asynchronously Process PDF Annotations with PdfAnnotationEdi... | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to perform annotation operations on a PDF (bind, delete, import, flatten, save) ... |
| [audit-pdf-annotations-to-json](./audit-pdf-annotations-to-json.cs) | Audit PDF Annotations and Log to JSON | `PdfAnnotationEditor`, `Document`, `Page` | Extracts annotation properties from a PDF using PdfAnnotationEditor, serializes the data to inden... |
| [backup-pdf-delete-all-annotations](./backup-pdf-delete-all-annotations.cs) | Backup PDF and Delete All Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates creating a backup copy of a PDF file and then removing all annotations using Aspose.... |
| [backup-pdf-flatten-form-fields](./backup-pdf-flatten-form-fields.cs) | Backup PDF and Flatten Form Fields | `Document`, `Form`, `Save` | Shows how to create a backup copy of a PDF before flattening all form fields using Aspose.Pdf's F... |
| [batch-delete-pdf-annotations-retain-types](./batch-delete-pdf-annotations-retain-types.cs) | Batch Delete PDF Annotations While Retaining Specified Types | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to delete all annotations from a PDF except those listed in a JSON configuration... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Progress | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to delete all annotations from multiple PDF files using Aspose.Pdf.Facades and s... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to iterate through PDF files in a directory and remove all stamp annotations using Aspo... |
| [batch-flatten-annotations-skip-readonly](./batch-flatten-annotations-skip-readonly.cs) | Batch Flatten Annotations Skipping Read‑Only | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates iterating through all pages of a PDF, flattening each annotation except those marked... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to flatten all annotations in multiple PDF files using Aspose.Pdf while supporti... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations in a Folder | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to iterate over all PDF files in a directory, flatten their annotations using Aspose.Pd... |
| [batch-import-xfdf-annotations-into-matching-pdfs](./batch-import-xfdf-annotations-into-matching-pdfs.cs) | Batch Import XFDF Annotations into Matching PDFs | `PdfAnnotationEditor`, `BindPdf`, `ImportAnnotationsFromXfdf` | Demonstrates how to iterate through a folder of PDFs, locate matching XFDF files by name, and imp... |
| [batch-process-pdfs-azure-blob-pdfannotationeditor](./batch-process-pdfs-azure-blob-pdfannotationeditor.cs) | Batch Process PDFs from Azure Blob Storage with PdfAnnotatio... | `PdfAnnotationEditor`, `BindPdf`, `DeleteAllAnnotations` | The example enumerates PDF blobs in an Azure Blob container, streams each PDF into Aspose.Pdf.Fac... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Shows how to iterate over PDF files, use PdfAnnotationEditor to replace the author of every annot... |
| [benchmark-deleteannotations-vs-deleteannotation](./benchmark-deleteannotations-vs-deleteannotation.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation for PDF Anno... | `PdfAnnotationEditor`, `DeleteAnnotations`, `DeleteAnnotation` | Demonstrates how to measure the performance of removing all annotations with DeleteAnnotations ve... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in a PDF | `PdfAnnotationEditor`, `BindPdf`, `Document` | Shows how to iterate through all annotations in a PDF, identify annotation names that appear more... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `Page`, `Annotation` | Shows how to clone an existing annotation, adjust its properties (color, contents, title, etc.), ... |
| [clone-text-annotation-change-color](./clone-text-annotation-change-color.cs) | Clone Text Annotation and Change Its Color | `PdfAnnotationEditor`, `ExtractAnnotations`, `Save` | Demonstrates how to clone a text annotation from one page, modify its color, and add it to anothe... |
| [concurrent-import-delete-pdf](./concurrent-import-delete-pdf.cs) | Concurrent Import and Delete Operations on a PDF | `PdfFileEditor`, `TryAppend`, `TryDelete` | Demonstrates how to safely perform simultaneous page import and delete operations on the same PDF... |
| [conditional-pdf-flattening-based-on-digital-signat...](./conditional-pdf-flattening-based-on-digital-signatures.cs) | Conditional PDF Flattening Based on Digital Signatures | `Document`, `Flatten`, `PdfFileSignature` | The example checks a PDF for existing digital signatures using the PdfFileSignature facade and on... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Shows how to export annotations from a source PDF to an XFDF byte array and import them into seve... |
| [count-pdf-annotation-types](./count-pdf-annotation-types.cs) | Count PDF Annotation Types | `PdfAnnotationEditor`, `BindPdf`, `ExtractAnnotations` | Demonstrates how to use PdfAnnotationEditor to extract all annotations from a PDF and return a di... |
| [delete-all-annotations-and-verify-count](./delete-all-annotations-and-verify-count.cs) | Delete All Annotations and Verify Count | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | The example binds a PDF to a PdfAnnotationEditor, counts the total annotations, removes all annot... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations from PDF using PdfAnnotationEditor | `Document`, `TextAnnotation`, `Rectangle` | Demonstrates creating a PDF with a text annotation, removing all annotations with PdfAnnotationEd... |
| [delete-all-annotations-from-pdfs](./delete-all-annotations-from-pdfs.cs) | Delete All Annotations from PDFs in a Folder | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates iterating over PDF files in a directory, binding each with PdfAnnotationEditor, remo... |
| [delete-annotation-by-name](./delete-annotation-by-name.cs) | Delete PDF Annotations by Name | `Document`, `TextAnnotation`, `PdfAnnotationEditor` | Shows how to delete a PDF annotation by its name using a string literal and a variable. |
| [delete-annotation-with-error-handling](./delete-annotation-with-error-handling.cs) | Delete Annotation with Error Handling | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates loading a PDF, attempting to delete a non‑existent annotation using PdfAnnotationEdi... |
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
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_013009_d919e8`
<!-- AUTOGENERATED:END -->

---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **106** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (105/106 files) ← category-specific
- `using Aspose.Pdf;` (68/106 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (44/106 files)
- `using Aspose.Pdf.Drawing;` (2/106 files)
- `using Aspose.Pdf.Text;` (2/106 files)
- `using System;` (106/106 files)
- `using System.IO;` (97/106 files)
- `using System.Collections.Generic;` (18/106 files)
- `using System.Threading.Tasks;` (7/106 files)
- `using System.Threading;` (4/106 files)
- `using System.Diagnostics;` (3/106 files)
- `using System.Linq;` (3/106 files)
- `using System.Text.Json;` (3/106 files)
- `using System.Xml.Linq;` (3/106 files)
- `using NUnit.Framework;` (2/106 files)
- `using System.Drawing;` (2/106 files)
- `using System.Text;` (2/106 files)
- `using Azure;` (1/106 files)
- `using Azure.Storage.Blobs;` (1/106 files)
- `using Azure.Storage.Blobs.Models;` (1/106 files)
- `using System.IO.Compression;` (1/106 files)
- `using System.Xml;` (1/106 files)
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
| [add-custom-metadata-to-pdf-annotation](./add-custom-metadata-to-pdf-annotation.cs) | Add Custom Metadata to PDF Annotation | `Document`, `TextAnnotation`, `ModifyAnnotations` | Demonstrates how to create a TextAnnotation, extend its dictionary with custom metadata, modify i... |
| [add-text-annotation-with-custom-flags](./add-text-annotation-with-custom-flags.cs) | Add Text Annotation with Custom Flags | `Document`, `Page`, `TextAnnotation` | Shows how to create a TextAnnotation, set custom annotation flags, and apply it to specific pages... |
| [annotation-performance-logger](./annotation-performance-logger.cs) | Log Annotation Operation Durations in PDF | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates using Aspose.Pdf.Facades.PdfAnnotationEditor to add, flatten, and save PDF annotatio... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Asynchronous PDF Annotation Operations with PdfAnnotationEdi... | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to perform annotation tasks such as flattening, deleting, importing, and exporting on a... |
| [backup-and-flatten-pdf-form-fields](./backup-and-flatten-pdf-form-fields.cs) | Backup and Flatten PDF Form Fields | `Document`, `Form`, `FlattenAllFields` | Shows how to create a backup of a PDF file and then flatten all form fields using Aspose.Pdf.Faca... |
| [backup-pdf-delete-all-annotations](./backup-pdf-delete-all-annotations.cs) | Backup PDF and Delete All Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Creates a backup copy of the original PDF and then removes every annotation using the PdfAnnotati... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Progress Display | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to remove all annotations from multiple PDF files using Aspose.Pdf.Facades.PdfAnnotatio... |
| [batch-delete-pdf-annotations](./batch-delete-pdf-annotations.cs) | Batch Delete PDF Annotations Using Retention Config | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to load a JSON configuration that lists annotation types to keep, then removes all othe... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Shows how to loop through PDF files in a directory and remove all stamp annotations using Aspose.... |
| [batch-export-delete-pdf-annotations](./batch-export-delete-pdf-annotations.cs) | Batch Export and Delete PDF Annotations to XFDF | `Document`, `PdfAnnotationEditor`, `ExportAnnotationsToXfdf` | Demonstrates how to iterate over PDF files, export all annotations to XFDF, remove the annotation... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Demonstrates how to flatten annotations in multiple PDF files using Aspose.Pdf's PdfAnnotationEdi... |
| [batch-flatten-pdf-annotations-skip-readonly](./batch-flatten-pdf-annotations-skip-readonly.cs) | Batch Flatten PDF Annotations with Read‑Only Skip | `Document`, `Page`, `TextAnnotation` | Demonstrates how to iterate through all pages and annotations in a PDF, flatten each annotation, ... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Iterates through all PDF files in a directory and uses Aspose.Pdf.Facades.PdfAnnotationEditor to ... |
| [batch-import-xfdf-annotations-into-pdfs](./batch-import-xfdf-annotations-into-pdfs.cs) | Batch Import XFDF Annotations into Matching PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to iterate through a folder of PDFs, locate corresponding XFDF files by name, im... |
| [batch-process-pdfs-azure-blob-pdfannotationeditor](./batch-process-pdfs-azure-blob-pdfannotationeditor.cs) | Batch Process PDFs from Azure Blob Storage with PdfAnnotatio... | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Streams PDF files from an Azure Blob Storage container, uses Aspose.Pdf.Facades.PdfAnnotationEdit... |
| [batch-remove-annotations-report](./batch-remove-annotations-report.cs) | Batch Remove Annotations from PDFs and Generate Report | `Document`, `PdfAnnotationEditor`, `BindPdf` | The example counts all annotations in each PDF, removes them using PdfAnnotationEditor, saves a c... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author in Multiple PDFs | `PdfAnnotationEditor`, `BindPdf`, `ModifyAnnotationsAuthor` | Demonstrates how to loop through a folder of PDF files and use Aspose.Pdf.Facades.PdfAnnotationEd... |
| [benchmark-deleteannotations-vs-deleteannotation](./benchmark-deleteannotations-vs-deleteannotation.cs) | Benchmark DeleteAnnotations vs DeleteAnnotation | `Document`, `Page`, `TextAnnotation` | Shows how to measure the performance of deleting all PDF annotations at once versus deleting a si... |
| [change-annotation-subject-color](./change-annotation-subject-color.cs) | Change Annotation Subject and Color | `Document`, `Page`, `TextAnnotation` | Demonstrates how to modify the Subject and Color of an existing PDF annotation by creating a new ... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in a PDF | `BindPdf`, `Document`, `Pages` | Shows how to traverse all annotations in a PDF with Aspose.Pdf, detect duplicate annotation names... |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone PDF Annotation, Change Its Color, and Add to Another P... | `PdfAnnotationEditor`, `BindPdf`, `ExtractAnnotations` | Demonstrates how to extract a specific annotation from one page, clone it with a new color, and p... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `Page`, `TextAnnotation` | Demonstrates how to clone an existing annotation on a PDF page, modify its properties, add the cl... |
| [concurrent-insert-delete-pdf](./concurrent-insert-delete-pdf.cs) | Concurrent Insert and Delete Operations on PDF | `PdfFileEditor`, `Insert`, `Delete` | Demonstrates how to run page insertion and page deletion on the same PDF simultaneously using Asp... |
| [conditional-pdf-flattening-based-on-digital-signat...](./conditional-pdf-flattening-based-on-digital-signatures.cs) | Conditional PDF Flattening Based on Digital Signatures | `PdfFileSignature`, `BindPdf`, `ContainsSignature` | The example checks whether a PDF contains digital signatures and skips flattening to preserve sig... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `Document`, `TextAnnotation`, `PdfAnnotationEditor` | Shows how to export annotations from a template PDF to an XFDF stream and import them into severa... |
| [count-pdf-annotations-by-type](./count-pdf-annotations-by-type.cs) | Count PDF Annotations by Type | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates how to load a PDF, extract all annotations using the PdfAnnotationEditor facade, and... |
| [delete-all-annotations-from-pdfs](./delete-all-annotations-from-pdfs.cs) | Delete All Annotations from PDFs in a Folder | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to loop through a directory of PDF files, bind each with Aspose.Pdf.Facades.PdfA... |
| [delete-annotations-and-log-to-json](./delete-annotations-and-log-to-json.cs) | Delete All Annotations and Log Deletions to JSON | `Document`, `PdfAnnotationEditor`, `Page` | The example loads a PDF, iterates through every page, deletes each annotation, and writes a JSON ... |
| [delete-annotations-and-verify-count](./delete-annotations-and-verify-count.cs) | Delete All Annotations and Verify Count | `Document`, `PdfAnnotationEditor`, `BindPdf` | The example counts annotations in a PDF, removes all of them using PdfAnnotationEditor, saves the... |
| [delete-annotations-by-rgb-color](./delete-annotations-by-rgb-color.cs) | Delete Annotations by Specific RGB Color | `PdfAnnotationEditor`, `BindPdf`, `Save` | Demonstrates how to remove PDF annotations whose Color property matches a given RGB value using A... |
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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

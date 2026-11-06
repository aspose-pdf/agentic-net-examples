---
name: facades-annotations
description: C# examples for facades-annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-annotations

> **Facades annotations** in PDF using C# / .NET -- **104** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-annotations** category.
This folder contains standalone C# examples for facades-annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (80/104 files) ← category-specific
- `using Aspose.Pdf.Facades;` (70/104 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (47/104 files)
- `using Aspose.Pdf.Text;` (5/104 files)
- `using Aspose.Pdf.Forms;` (1/104 files)
- `using System;` (104/104 files)
- `using System.IO;` (104/104 files)
- `using System.Collections.Generic;` (16/104 files)
- `using System.Threading;` (4/104 files)
- `using System.Diagnostics;` (3/104 files)
- `using System.Linq;` (3/104 files)
- `using System.Text.Json;` (3/104 files)
- `using System.Threading.Tasks;` (3/104 files)
- `using System.IO.Compression;` (2/104 files)
- `using System.Text;` (2/104 files)
- `using System.Xml;` (2/104 files)
- `using System.Xml.Linq;` (2/104 files)
- `using System.Xml.Schema;` (1/104 files)

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
| [add-text-annotation-with-custom-flags](./add-text-annotation-with-custom-flags.cs) | Add Custom Annotation Flags to PDF | `Document`, `Page`, `Annotation` | Demonstrates how to load a PDF, retrieve an annotation, combine existing flags with standard and ... |
| [annotation-removal-report](./annotation-removal-report.cs) | Generate Annotation Removal Report for PDF Files | `Document`, `Page`, `PdfAnnotationEditor` | Counts and removes all annotations from each PDF in a folder, saves the cleaned PDFs, and creates... |
| [async-pdf-annotation-operations](./async-pdf-annotation-operations.cs) | Add Text Annotation to PDF Asynchronously | `Document`, `Page`, `TextAnnotation` | Demonstrates adding a text annotation to a PDF document with Aspose.Pdf and executing the operati... |
| [backup-pdf-and-flatten-form-fields](./backup-pdf-and-flatten-form-fields.cs) | Backup PDF and Flatten Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Shows how to create a backup of a PDF file before flattening its annotations using Aspose.Pdf's P... |
| [batch-delete-pdf-annotations-retain-types](./batch-delete-pdf-annotations-retain-types.cs) | Retain Specific Annotation Types During PDF Batch Deletion | `Document`, `Page`, `AnnotationCollection` | Demonstrates loading a JSON configuration that lists annotation types to keep, then iterates thro... |
| [batch-delete-pdf-annotations-with-progress](./batch-delete-pdf-annotations-with-progress.cs) | Batch Delete PDF Annotations with Console Progress Bar | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to delete all annotations from multiple PDF files using Aspose.Pdf.Facades.PdfAn... |
| [batch-delete-stamp-annotations](./batch-delete-stamp-annotations.cs) | Batch Delete Stamp Annotations from PDFs | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Processes each PDF in a specified folder, removes all stamp annotations using PdfAnnotationEditor... |
| [batch-export-delete-annotations-xfdf](./batch-export-delete-annotations-xfdf.cs) | Export PDF Annotations to XFDF, Delete Them, and Archive | `Document`, `PdfAnnotationEditor`, `ExportAnnotationsXfdf` | Processes each PDF in a folder, exports its annotations to an XFDF file, removes the annotations ... |
| [batch-flatten-pdf-annotations-cancellation](./batch-flatten-pdf-annotations-cancellation.cs) | Batch Flatten PDF Annotations with Cancellation Support | `Document`, `PdfAnnotationEditor`, `BindPdf` | Demonstrates flattening annotations in multiple PDF files using Aspose.Pdf's PdfAnnotationEditor ... |
| [batch-flatten-pdf-annotations](./batch-flatten-pdf-annotations.cs) | Batch Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `FlatteningAnnotations` | Iterates through all PDF files in a folder, flattens their annotations using Aspose.Pdf, and save... |
| [batch-import-xfdf-annotations-into-pdfs__v2](./batch-import-xfdf-annotations-into-pdfs__v2.cs) | Batch Import XFDF Annotations into Matching PDFs | `Document`, `Form`, `ImportXfdf` | Demonstrates how to iterate over a folder of PDFs, locate corresponding XFDF files by name, and i... |
| [batch-remove-old-annotations](./batch-remove-old-annotations.cs) | Remove Old Annotations from PDFs in Batch | `Document`, `Page`, `AnnotationCollection` | Demonstrates how to iterate through PDFs in a folder, examine each annotation's modification date... |
| [batch-update-annotation-author](./batch-update-annotation-author.cs) | Batch Update Annotation Author Across Multiple PDFs | `Document`, `PdfAnnotationEditor`, `BindPdf` | Shows how to iterate over PDF files in a directory and use PdfAnnotationEditor.ModifyAnnotationsA... |
| [benchmark-delete-annotations](./benchmark-delete-annotations.cs) | Benchmark DeleteAnnotation vs DeleteAnnotations | `Document`, `TextAnnotation`, `Rectangle` | Shows how to measure the performance difference between deleting a single annotation with DeleteA... |
| [check-duplicate-annotation-names](./check-duplicate-annotation-names.cs) | Check for Duplicate Annotation Names in a PDF | `Document`, `Page`, `Annotation` | Loads a PDF, iterates through all pages to collect annotation names, groups them, and logs any na... |
| [clone-annotation-change-color](./clone-annotation-change-color.cs) | Clone PDF Annotation, Change Its Color, and Add to Another P... | `Document`, `Page`, `Annotation` | Demonstrates how to clone an existing annotation from one page, modify its color, and place the c... |
| [clone-modify-pdf-annotation](./clone-modify-pdf-annotation.cs) | Clone and Modify PDF Annotation | `Document`, `Page`, `Annotation` | Shows how to clone an existing annotation on a PDF page, adjust its properties, and add the clone... |
| [concurrent-insert-delete-pdf](./concurrent-insert-delete-pdf.cs) | Concurrent Import and Delete PDF Pages | `Document`, `Page`, `TextFragment` | Demonstrates thread‑safe concurrent importing of pages from one PDF and deleting pages from anoth... |
| [conditional-pdf-flattening](./conditional-pdf-flattening.cs) | Conditional PDF Flattening with Signature Preservation | `Document`, `Form`, `Flatten()` | The example loads a PDF, checks for any digital signature fields, and only flattens the form when... |
| [copy-annotations-to-multiple-pdfs](./copy-annotations-to-multiple-pdfs.cs) | Copy Annotations from Template PDF to Multiple PDFs | `Document`, `ExportAnnotationsToXfdf`, `ImportAnnotationsFromXfdf` | Demonstrates exporting annotations from a template PDF to an XFDF file and importing them into a ... |
| [delete-all-annotations-from-pdf](./delete-all-annotations-from-pdf.cs) | Delete All Annotations and Verify Removal | `Document`, `Page`, `PdfAnnotationEditor` | The example counts PDF annotations before and after using Aspose.Pdf, deletes all annotations wit... |
| [delete-annotations-by-author](./delete-annotations-by-author.cs) | Remove Annotations by Author from PDF | `Document`, `Page`, `Annotation` | Demonstrates how to filter and delete PDF annotations whose Author matches a specified name, iter... |
| [delete-annotations-export-xfdf](./delete-annotations-export-xfdf.cs) | Delete Specific Annotations and Export Remaining to XFDF | `Document`, `Page`, `Annotation` | The example removes all TextAnnotation objects from a PDF, saves the cleaned document, and then e... |
| [delete-annotations-from-pdf-using-config](./delete-annotations-from-pdf-using-config.cs) | Delete PDF Annotations Using JSON Configuration | `Document`, `Page`, `AnnotationType` | Shows how to create or read a JSON file that lists annotation types, map them to Aspose.Pdf.Annot... |
| [delete-annotations-from-pdf](./delete-annotations-from-pdf.cs) | Delete All Annotations from a PDF | `Document`, `Page`, `TextAnnotation` | Creates a PDF with two text annotations, verifies their count, clears all annotations from the fi... |
| [delete-annotations-with-backup](./delete-annotations-with-backup.cs) | Backup PDF and Remove All Annotations | `Document`, `Page`, `Annotations` | Creates a backup of the source PDF, then loads the document and clears all annotations from each ... |
| [delete-flatten-pdf-annotations](./delete-flatten-pdf-annotations.cs) | Delete and Flatten PDF Annotations | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotations` | Demonstrates how to remove all annotations from a PDF, flatten any remaining ones, and save the p... |
| [delete-pdf-annotation-by-name](./delete-pdf-annotation-by-name.cs) | Delete a Specific Annotation from a PDF | `Document`, `Page`, `Annotations` | Shows how to locate an annotation by its Name property and remove it from a PDF document using As... |
| [delete-pdf-annotation-by-name__v2](./delete-pdf-annotation-by-name__v2.cs) | Delete PDF Annotation by Name | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Shows how to remove a specific annotation from a PDF using Aspose.Pdf.Facades by passing the anno... |
| [delete-pdf-annotation-with-error-handling](./delete-pdf-annotation-with-error-handling.cs) | Delete PDF Annotation with Error Handling | `PdfAnnotationEditor`, `BindPdf`, `DeleteAnnotation` | Demonstrates how to bind a PDF, attempt to delete a named annotation, and handle errors when the ... |
| ... | | | *and 74 more files* |

## Category Statistics
- Total examples: 104

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
Updated: 2026-11-06 | Run: `20261106_012731_84a3e2`
<!-- AUTOGENERATED:END -->

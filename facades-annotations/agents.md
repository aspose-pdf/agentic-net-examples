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

- `using Aspose.Pdf.Facades;` (7/9 files) ← category-specific
- `using Aspose.Pdf;` (3/9 files)
- `using Aspose.Pdf.Annotations;` (3/9 files)
- `using Aspose.Pdf.Drawing;` (1/9 files)
- `using System;` (9/9 files)
- `using System.IO;` (9/9 files)

## Common Code Pattern

Most files in this category use `PdfAnnotationEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfAnnotationEditor tool = new PdfAnnotationEditor();
tool.BindPdf("input.pdf");
// ... PdfAnnotationEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [delete-an-annotation-with-a-specified-name-from-a-pdf-docume...](./delete-an-annotation-with-a-specified-name-from-a-pdf-document-via-the-facade-interface.cs) | `PdfAnnotationEditor` | Delete an annotation with a specified name from a pdf document via the facade... |
| [import-and-export-annotations-using-xfdf-format-load-a-pdf-a...](./import-and-export-annotations-using-xfdf-format-load-a-pdf-and-save-it-back-as-pdf.cs) | `PdfAnnotationEditor` | Import and export annotations using xfdf format load a pdf and save it back a... |
| [import-xfdf-annotations-into-a-loaded-pdf-then-export-the-up...](./import-xfdf-annotations-into-a-loaded-pdf-then-export-the-updated-annotations-back-to-xfdf-and-save.cs) | `PdfAnnotationEditor` | Import xfdf annotations into a loaded pdf then export the updated annotations... |
| [remove-all-annotations-from-a-pdf-document-using-the-facades...](./remove-all-annotations-from-a-pdf-document-using-the-facades-api-preserving-the-original-content-la.cs) | `PdfAnnotationEditor` | Remove all annotations from a pdf document using the facades api preserving t... |
| [remove-every-annotation-from-an-existing-pdf-document-ensuri...](./remove-every-annotation-from-an-existing-pdf-document-ensuring-the-resulting-file-retains-its-origi.cs) | `PdfAnnotationEditor` | Remove every annotation from an existing pdf document ensuring the resulting ... |
| [remove-every-annotation-of-a-given-type-from-a-pdf-document-...](./remove-every-annotation-of-a-given-type-from-a-pdf-document-while-preserving-the-remaining-content-s.cs) | `PdfAnnotationEditor` | Remove every annotation of a given type from a pdf document while preserving ... |
| [retrieve-all-annotations-from-a-pdf-document-preserving-thei...](./retrieve-all-annotations-from-a-pdf-document-preserving-their-types-positions-and-associated-meta.cs) |  | Retrieve all annotations from a pdf document preserving their types positions... |
| [update-existing-annotations-in-a-pdf-file-altering-their-pro...](./update-existing-annotations-in-a-pdf-file-altering-their-properties-while-preserving-the-document-s.cs) |  | Update existing annotations in a pdf file altering their properties while pre... |
| [update-properties-of-an-existing-annotation-within-a-pdf-fil...](./update-properties-of-an-existing-annotation-within-a-pdf-file-preserving-its-positioning-and-appear.cs) | `PdfAnnotationEditor`, `TextAnnotation` | Update properties of an existing annotation within a pdf file preserving its ... |

## Category Statistics
- Total examples: 9

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Annotations.AnnotationType`
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`

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
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

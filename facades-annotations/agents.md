---
name: Facades - Annotations
description: C# examples for Facades - Annotations using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - Annotations

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - Annotations** category.
This folder contains standalone C# examples for Facades - Annotations operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - Annotations**.
- Files are standalone `.cs` examples stored directly in this folder.

## Files in this folder
- [delete-an-annotation-with-a-specified-name-from-a-pdf-document-via-the-facade-interface](./delete-an-annotation-with-a-specified-name-from-a-pdf-document-via-the-facade-interface.cs)
- [import-and-export-annotations-using-xfdf-format-load-a-pdf-and-save-it-back-as-pdf](./import-and-export-annotations-using-xfdf-format-load-a-pdf-and-save-it-back-as-pdf.cs)
- [import-xfdf-annotations-into-a-loaded-pdf-then-export-the-updated-annotations-back-to-xfdf-and-save](./import-xfdf-annotations-into-a-loaded-pdf-then-export-the-updated-annotations-back-to-xfdf-and-save.cs)
- [remove-all-annotations-from-a-pdf-document-using-the-facades-api-preserving-the-original-content-la](./remove-all-annotations-from-a-pdf-document-using-the-facades-api-preserving-the-original-content-la.cs)
- [remove-every-annotation-from-an-existing-pdf-document-ensuring-the-resulting-file-retains-its-origi](./remove-every-annotation-from-an-existing-pdf-document-ensuring-the-resulting-file-retains-its-origi.cs)
- [remove-every-annotation-of-a-given-type-from-a-pdf-document-while-preserving-the-remaining-content-s](./remove-every-annotation-of-a-given-type-from-a-pdf-document-while-preserving-the-remaining-content-s.cs)
- [retrieve-all-annotations-from-a-pdf-document-preserving-their-types-positions-and-associated-meta](./retrieve-all-annotations-from-a-pdf-document-preserving-their-types-positions-and-associated-meta.cs)
- [update-existing-annotations-in-a-pdf-file-altering-their-properties-while-preserving-the-document-s](./update-existing-annotations-in-a-pdf-file-altering-their-properties-while-preserving-the-document-s.cs)
- [update-properties-of-an-existing-annotation-within-a-pdf-file-preserving-its-positioning-and-appear](./update-properties-of-an-existing-annotation-within-a-pdf-file-preserving-its-positioning-and-appear.cs)

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
- Review code examples in this folder for Facades - Annotations patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_191202_db0088`
<!-- AUTOGENERATED:END -->

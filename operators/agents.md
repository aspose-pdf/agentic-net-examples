---
name: operators
description: C# examples for operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - operators

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **operators** category.
This folder contains standalone C# examples for operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (7/7 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/7 files) ← category-specific
- `using Aspose.Pdf.Operators;` (2/7 files)
- `using Aspose.Pdf.Annotations;` (1/7 files)
- `using System;` (7/7 files)
- `using System.IO;` (7/7 files)
- `using System.Text.RegularExpressions;` (2/7 files)
- `using System.Collections.Generic;` (1/7 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [Implement-PDF-manipulation-by-loading-a-PDF-and-evaluating-v...](./Implement-PDF-manipulation-by-loading-a-PDF-and-evaluating-values-using-comparison-operators.cs) | `TextAbsorber` | Implement PDF manipulation by loading a PDF and evaluating values using compa... |
| [Load-a-PDF-and-apply-boolean-logic-using-and-operators-to-ma...](./Load-a-PDF-and-apply-boolean-logic-using-and-operators-to-manipulate-its-content.cs) | `TextAnnotation`, `TextFragment` | Load a PDF and apply boolean logic using and operators to manipulate its content |
| [Load-a-PDF-and-perform-arithmetic-operations-addition-subtra...](./Load-a-PDF-and-perform-arithmetic-operations-addition-subtraction-multiplication-division-on-nume.cs) | `TextAbsorber`, `TextFragment` | Load a PDF and perform arithmetic operations addition subtraction multiplicat... |
| [Load-a-PDF-document-and-concatenate-string-values-using-the-...](./Load-a-PDF-document-and-concatenate-string-values-using-the-operator-for-dynamic-content-generatio.cs) | `TextFragment`, `TextBuilder` | Load a PDF document and concatenate string values using the operator for dyna... |
| [Utilize-PDF-operators-to-evaluate-logical-conditions-program...](./Utilize-PDF-operators-to-evaluate-logical-conditions-programmatically-and-load-the-resulting-PDF-doc.cs) |  | Utilize PDF operators to evaluate logical conditions programmatically and loa... |
| [Utilize-PDF-operators-to-examine-the-operator-overview-and-l...](./Utilize-PDF-operators-to-examine-the-operator-overview-and-load-a-PDF-document-programmatically.cs) |  | Utilize PDF operators to examine the operator overview and load a PDF documen... |
| [Utilize-PDF-operators-with-comparison-logic-to-validate-data...](./Utilize-PDF-operators-with-comparison-logic-to-validate-data-integrity-before-loading-the-document-i.cs) |  | Utilize PDF operators with comparison logic to validate data integrity before... |

## Category Statistics
- Total examples: 7

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Matrix`
- `Aspose.Pdf.OperatorCollection`
- `Aspose.Pdf.Operators.BDC`
- `Aspose.Pdf.Operators.BDC.Accept`
- `Aspose.Pdf.Operators.BDC.Properties`
- `Aspose.Pdf.Operators.BDC.Tag`
- `Aspose.Pdf.Operators.BDC.ToString`
- `Aspose.Pdf.Operators.ClosePathStroke`
- `Aspose.Pdf.Operators.ConcatenateMatrix`
- `Aspose.Pdf.Operators.Do`
- `Aspose.Pdf.Operators.Fill`
- `Aspose.Pdf.Operators.GRestore`
- `Aspose.Pdf.Operators.GSave`
- `Aspose.Pdf.Operators.MoveTextPosition`

### Rules
- To place an image on {page}, first add the image stream ({image_stream}) to {page}.Resources.Images, then append the operators in order: GSave, ConcatenateMatrix (with a matrix derived from the desired {rect}), Do (using the image name retrieved from the resources), and finally GRestore.
- Create a placement matrix from a {rect} using: new Matrix(new double[] { rect.URX - rect.LLX, 0, 0, rect.URY - rect.LLY, rect.LLX, rect.LLY }). This matrix maps the image to the rectangle's coordinates.
- After modifying the page contents, persist the changes by calling {doc}.Save({output_pdf}).
- Create an XForm on {page} of {doc} using XForm.CreateNewForm({page}, {doc}), add it to {page}.Resources.Forms, then within the XForm's Contents add a GSave, a ConcatenateMatrix({float_width},0,0,{float_height},0,0) to set size, load an image stream from {string_literal} and add it to form.Resources.Images, retrieve the XImage via form.Resources.Images[form.Resources.Images.Count], draw the image with a Do operator using the XImage.Name, and finish with a GRestore.
- To draw an existing XForm {form} at a specific location on a page, insert a GSave, apply a translation matrix with ConcatenateMatrix(1,0,0,1,{float_x},{float_y}), invoke a Do operator with {form}.Name, and close with a GRestore.

### Warnings
- The example accesses the newly added image via page.Resources.Images[page.Resources.Images.Count]; this relies on 1‑based indexing of the Images collection and may be error‑prone if the collection indexing changes.
- The FileStream used for the image is not explicitly disposed; in production code it should be wrapped in a using statement.
- The exact type name for the resources collection (e.g., Aspose.Pdf.Resources) is inferred; verify against the library version.
- OperatorCollection is used via doc.Pages[1].Contents; the fully qualified name may differ.
- The actual Delete call is commented out in the sample, so the example does not produce an output PDF with removed graphics.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for operators patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

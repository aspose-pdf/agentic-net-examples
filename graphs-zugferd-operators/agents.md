---
name: graphs-zugferd-operators
description: C# examples for graphs-zugferd-operators using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - graphs-zugferd-operators

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **graphs-zugferd-operators** category.
This folder contains standalone C# examples for graphs-zugferd-operators operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **graphs-zugferd-operators**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (9/9 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/9 files)
- `using Aspose.Pdf.Text;` (1/9 files)
- `using System;` (9/9 files)
- `using System.IO;` (9/9 files)
- `using Microsoft.CSharp.RuntimeBinder;` (1/9 files)

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
| [access-embedded-zugferd-xml-in-a-pdf-via-the-document.zugfer...](./access-embedded-zugferd-xml-in-a-pdf-via-the-document.zugferdinfo-property-and-load-the-pdf-document__v2.cs) |  | Access embedded zugferd xml in a pdf via the document.zugferdinfo property an... |
| [examine-the-zugferd-overview-and-load-a-pdf-to-access-its-re...](./examine-the-zugferd-overview-and-load-a-pdf-to-access-its-relevant-zugferd-data__v2.cs) |  | Examine the zugferd overview and load a pdf to access its relevant zugferd da... |
| [generate-zugferd-compliant-pdfs-and-load-existing-pdf-files-...](./generate-zugferd-compliant-pdfs-and-load-existing-pdf-files-to-extract-or-manipulate-embedded-zugfer__v2.cs) | `TextFragment` | Generate zugferd compliant pdfs and load existing pdf files to extract or man... |
| [load-a-pdf-document-and-extract-its-embedded-zugferd-data-fo...](./load-a-pdf-document-and-extract-its-embedded-zugferd-data-for-downstream-processing-and-validation__v2.cs) |  | Load a pdf document and extract its embedded zugferd data for downstream proc... |
| [load-a-zugferd-pdf-retrieve-its-embedded-invoice-xml-and-pro...](./load-a-zugferd-pdf-retrieve-its-embedded-invoice-xml-and-process-the-document-programmatically__v2.cs) |  | Load a zugferd pdf retrieve its embedded invoice xml and process the document... |
| [load-pdf-files-containing-zugferd-data-and-extract-or-manipu...](./load-pdf-files-containing-zugferd-data-and-extract-or-manipulate-the-embedded-zugferd-documents__v2.cs) |  | Load pdf files containing zugferd data and extract or manipulate the embedded... |
| [provide-a-comprehensive-guide-for-processing-zugferd-data-wi...](./provide-a-comprehensive-guide-for-processing-zugferd-data-within-pdf-files-and-loading-the-pdf__v2.cs) |  | Provide a comprehensive guide for processing zugferd data within pdf files an... |
| [save-zugferd-data-extracted-from-a-pdf-and-load-the-pdf-to-e...](./save-zugferd-data-extracted-from-a-pdf-and-load-the-pdf-to-enable-zugferd-file-persistence__v2.cs) |  | Save zugferd data extracted from a pdf and load the pdf to enable zugferd fil... |
| [update-zugferd-invoice-fields-buyer-seller-line-items-in-a-p...](./update-zugferd-invoice-fields-buyer-seller-line-items-in-a-pdf-via-the-xml-api-and-reload-the-do__v2.cs) |  | Update zugferd invoice fields buyer seller line items in a pdf via the xml ap... |

## Category Statistics
- Total examples: 9

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BorderInfo`
- `Aspose.Pdf.BorderSide`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Drawing.GradientAxialShading`
- `Aspose.Pdf.Drawing.Graph`
- `Aspose.Pdf.Drawing.GraphInfo`
- `Aspose.Pdf.Drawing.Line`
- `Aspose.Pdf.Drawing.Line.GraphInfo`
- `Aspose.Pdf.Drawing.Paragraphs`
- `Aspose.Pdf.Drawing.Point`
- `Aspose.Pdf.Drawing.Rectangle`
- `Aspose.Pdf.Drawing.Shapes`
- `Aspose.Pdf.GraphInfo`
- `Aspose.Pdf.Matrix`

### Rules
- Create a {doc} (Aspose.Pdf.Document), add a {page} (Aspose.Pdf.Page) via doc.Pages.Add(), instantiate a Graph (Aspose.Pdf.Drawing.Graph) with width and height, and add it to page.Paragraphs.
- Instantiate a Line (Aspose.Pdf.Drawing.Line) with a float[] of coordinates, optionally set line.GraphInfo.DashArray = int[] and line.GraphInfo.DashPhase = int to define dash style, then add the line to graph.Shapes.
- Save the {doc} to a file path ({output_pdf}) using doc.Save().
- Create a {graph} (Aspose.Pdf.Drawing.Graph) with dimensions {float} width and {float} height, set IsChangePosition={bool}, position it using Left={float} and Top={float}, add a Rectangle shape (Aspose.Pdf.Drawing.Rectangle) at (0,0) with the same dimensions, set its fill and border color to {color}, assign Graph.ZIndex={int}, then add the Graph to {page}.Paragraphs.
- Set {page}.PageInfo.Margin.Left={float} and .Top={float} to zero (or desired offset) before placing Graph objects to ensure absolute positioning aligns with page coordinates.

### Warnings
- GraphInfo is accessed through the Line instance (line.GraphInfo); ensure the line object supports this property.
- DashArray expects an int[] where the pattern values represent dash and gap lengths; incorrect values may produce unexpected rendering.
- GraphInfo is accessed via the Rectangle.GraphInfo property; the exact type name may differ in newer library versions.
- Rectangle constructor uses integer parameters for coordinates and size; ensure correct units.
- GraphInfo may be null until the shape is added to a Graph; setting FillColor before adding is safe in this pattern.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for graphs-zugferd-operators patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (33/33 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (17/33 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (5/33 files)
- `using Aspose.Pdf.Text;` (4/33 files)
- `using Aspose.Pdf.Facades;` (3/33 files)
- `using Aspose.Pdf.Vector;` (1/33 files)
- `using System;` (33/33 files)
- `using System.IO;` (25/33 files)
- `using System.Runtime.InteropServices;` (7/33 files)
- `using System.Drawing.Imaging;` (1/33 files)

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
| [Add-a-rectangle-featuring-an-alpha-channel-to-a-PDF-document...](./Add-a-rectangle-featuring-an-alpha-channel-to-a-PDF-document-using-the-drawing-API.cs) | `Rectangle` | Add a rectangle featuring an alpha channel to a PDF document using the drawin... |
| [Add-a-solid-filled-rectangle-shape-to-a-PDF-page-using-the-P...](./Add-a-solid-filled-rectangle-shape-to-a-PDF-page-using-the-PDF-graphics-API.cs) | `Rectangle` | Add a solid filled rectangle shape to a PDF page using the PDF graphics API |
| [Generate-a-filled-Curve-shape-within-a-PDF-document-specifyi...](./Generate-a-filled-Curve-shape-within-a-PDF-document-specifying-stroke-and-fill-properties-as-requir.cs) |  | Generate a filled Curve shape within a PDF document specifying stroke and fil... |
| [Generate-a-filled-arc-shape-within-a-PDF-document-using-the-...](./Generate-a-filled-arc-shape-within-a-PDF-document-using-the-PDF-drawing-API-to-define-geometry-and-f.cs) |  | Generate a filled arc shape within a PDF document using the PDF drawing API t... |
| [Generate-a-filled-ellipse-shape-within-a-PDF-file-using-the-...](./Generate-a-filled-ellipse-shape-within-a-PDF-file-using-the-appropriate-drawing-API.cs) |  | Generate a filled ellipse shape within a PDF file using the appropriate drawi... |
| [Generate-a-solid-circular-shape-within-a-PDF-file-using-the-...](./Generate-a-solid-circular-shape-within-a-PDF-file-using-the-appropriate-drawing-API.cs) |  | Generate a solid circular shape within a PDF file using the appropriate drawi... |
| [Instantiate-a-drawing-object-with-given-width-and-height-for...](./Instantiate-a-drawing-object-with-given-width-and-height-for-rendering-a-PDF-graph.cs) |  | Instantiate a drawing object with given width and height for rendering a PDF ... |
| [Instantiate-a-graphical-object-representing-a-PDF-document-t...](./Instantiate-a-graphical-object-representing-a-PDF-document-to-enable-rendering-and-manipulation-oper.cs) |  | Instantiate a graphical object representing a PDF document to enable renderin... |
| [Instantiate-a-new-PDF-document-object-to-begin-constructing-...](./Instantiate-a-new-PDF-document-object-to-begin-constructing-or-manipulating-PDF-content-programmatic.cs) |  | Instantiate a new PDF document object to begin constructing or manipulating P... |
| [Instantiate-a-rectangle-object-to-define-a-precise-graphical...](./Instantiate-a-rectangle-object-to-define-a-precise-graphical-region-within-a-PDF-document.cs) | `SquareAnnotation` | Instantiate a rectangle object to define a precise graphical region within a ... |
| [Load-a-PDF-document-and-insert-a-Rectangle-shape-at-specifie...](./Load-a-PDF-document-and-insert-a-Rectangle-shape-at-specified-coordinates-within-the-page-content.cs) | `Rectangle`, `Graph` | Load a PDF document and insert a Rectangle shape at specified coordinates wit... |
| [Load-a-PDF-document-and-insert-a-vector-Circle-shape-onto-a-...](./Load-a-PDF-document-and-insert-a-vector-Circle-shape-onto-a-specific-page.cs) | `CircleAnnotation` | Load a PDF document and insert a vector Circle shape onto a specific page |
| [Load-a-PDF-document-and-manipulate-its-graphical-elements-pr...](./Load-a-PDF-document-and-manipulate-its-graphical-elements-programmatically-for-analysis-or-modificat.cs) | `Rectangle`, `Ellipse`, `Line` | Load a PDF document and manipulate its graphical elements programmatically fo... |
| [Load-a-PDF-document-and-programmatically-insert-a-Curve-elem...](./Load-a-PDF-document-and-programmatically-insert-a-Curve-element-into-its-content-stream.cs) | `PdfContentEditor` | Load a PDF document and programmatically insert a Curve element into its cont... |
| [Load-a-PDF-document-and-programmatically-insert-a-Line-objec...](./Load-a-PDF-document-and-programmatically-insert-a-Line-object-at-specified-coordinates-within-the-pa.cs) | `LineAnnotation` | Load a PDF document and programmatically insert a Line object at specified co... |
| [Load-a-PDF-document-create-a-Line-object-and-embed-it-into-t...](./Load-a-PDF-document-create-a-Line-object-and-embed-it-into-the-desired-page-of-the-file.cs) | `LineAnnotation` | Load a PDF document create a Line object and embed it into the desired page o... |
| [Load-a-PDF-document-insert-an-ellipse-shape-onto-a-page-and-...](./Load-a-PDF-document-insert-an-ellipse-shape-onto-a-page-and-save-the-modified-file.cs) |  | Load a PDF document insert an ellipse shape onto a page and save the modified... |
| [Load-a-PDF-document-into-memory-then-draw-a-vector-line-span...](./Load-a-PDF-document-into-memory-then-draw-a-vector-line-spanning-the-page-width-at-a-specified-posi.cs) |  | Load a PDF document into memory then draw a vector line spanning the page wid... |
| [Load-a-PDF-document-programmatically-and-detect-all-graphica...](./Load-a-PDF-document-programmatically-and-detect-all-graphical-elements-such-as-charts-and-diagrams.cs) | `PdfExtractor`, `SvgExtractor` | Load a PDF document programmatically and detect all graphical elements such a... |
| [Load-a-PDF-document-then-insert-an-Arc-graphical-object-into...](./Load-a-PDF-document-then-insert-an-Arc-graphical-object-into-the-desired-page.cs) |  | Load a PDF document then insert an Arc graphical object into the desired page |
| [Load-a-PDF-file-and-append-a-text-fragment-to-the-page-s-par...](./Load-a-PDF-file-and-append-a-text-fragment-to-the-page-s-paragraph-collection.cs) | `TextFragment`, `TextBuilder` | Load a PDF file and append a text fragment to the page s paragraph collection |
| [Load-a-PDF-file-and-ensure-all-necessary-conditions-are-met-...](./Load-a-PDF-file-and-ensure-all-necessary-conditions-are-met-before-evaluating-shape-boundaries.cs) |  | Load a PDF file and ensure all necessary conditions are met before evaluating... |
| [Load-a-PDF-file-and-evaluate-the-bounding-rectangles-of-shap...](./Load-a-PDF-file-and-evaluate-the-bounding-rectangles-of-shapes-within-its-graphical-objects.cs) |  | Load a PDF file and evaluate the bounding rectangles of shapes within its gra... |
| [Load-a-PDF-file-and-insert-a-graph-object-into-its-paragraph...](./Load-a-PDF-file-and-insert-a-graph-object-into-its-paragraphs-collection-programmatically.cs) | `TextFragment` | Load a PDF file and insert a graph object into its paragraphs collection prog... |
| [Load-a-PDF-file-and-insert-a-graph-object-into-the-specified...](./Load-a-PDF-file-and-insert-a-graph-object-into-the-specified-page-s-paragraphs-collection.cs) |  | Load a PDF file and insert a graph object into the specified page s paragraph... |
| [Load-a-PDF-file-and-insert-an-ellipse-shape-into-the-documen...](./Load-a-PDF-file-and-insert-an-ellipse-shape-into-the-document-s-page-content.cs) |  | Load a PDF file and insert an ellipse shape into the document s page content |
| [Load-a-PDF-file-and-insert-text-inside-an-ellipse-while-main...](./Load-a-PDF-file-and-insert-text-inside-an-ellipse-while-maintaining-existing-content.cs) | `TextFragment`, `TextBuilder` | Load a PDF file and insert text inside an ellipse while maintaining existing ... |
| [Load-a-PDF-file-and-overlay-a-dotted-dashed-line-onto-the-de...](./Load-a-PDF-file-and-overlay-a-dotted-dashed-line-onto-the-desired-page.cs) | `LineAnnotation` | Load a PDF file and overlay a dotted dashed line onto the desired page |
| [Load-a-PDF-file-and-programmatically-append-a-blank-page-to-...](./Load-a-PDF-file-and-programmatically-append-a-blank-page-to-its-pages-collection.cs) |  | Load a PDF file and programmatically append a blank page to its pages collection |
| [Load-a-PDF-file-and-retrieve-the-bounding-rectangles-of-each...](./Load-a-PDF-file-and-retrieve-the-bounding-rectangles-of-each-shape-within-its-shape-collection.cs) |  | Load a PDF file and retrieve the bounding rectangles of each shape within its... |
| ... | | *and 3 more files* |

## Category Statistics
- Total examples: 33

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BorderInfo`
- `Aspose.Pdf.BorderSide`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Drawing.Ellipse`
- `Aspose.Pdf.Drawing.Ellipse.Bottom`
- `Aspose.Pdf.Drawing.Ellipse.CheckBounds`
- `Aspose.Pdf.Drawing.Ellipse.Height`
- `Aspose.Pdf.Drawing.Ellipse.Left`
- `Aspose.Pdf.Drawing.Ellipse.Width`
- `Aspose.Pdf.Drawing.GradientAxialShading`
- `Aspose.Pdf.Drawing.GradientRadialShading`
- `Aspose.Pdf.Drawing.GradientRadialShading.End`
- `Aspose.Pdf.Drawing.GradientRadialShading.EndColor`
- `Aspose.Pdf.Drawing.GradientRadialShading.EndingRadius`

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
- Review code examples in this folder for working-with-graphs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

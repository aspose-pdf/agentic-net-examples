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

- `using Aspose.Pdf;` (78/78 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (74/78 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/78 files)
- `using Aspose.Pdf.Annotations;` (2/78 files)
- `using Aspose.Pdf.Operators;` (2/78 files)
- `using System;` (78/78 files)
- `using System.Runtime.InteropServices;` (41/78 files) ← category-specific
- `using System.IO;` (28/78 files)
- `using System.Collections.Generic;` (6/78 files)
- `using System.Diagnostics.CodeAnalysis;` (1/78 files)
- `using System.Drawing;` (1/78 files)
- `using System.Text.Json;` (1/78 files)
- `using System.Threading.Tasks;` (1/78 files)

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

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-background-image-and-draw-shapes](./add-background-image-and-draw-shapes.cs) | Add Background Image to PDF Page and Draw Shapes Over It | `Document`, `Page`, `Image` | Demonstrates setting a page background image in a PDF and overlaying a Graph with rectangle, line... |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph, align it to the horizontal center of a PDF page, add it to the page'... |
| [add-centered-text-graph](./add-centered-text-graph.cs) | Add Centered Text Inside a Rectangle on a Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a graph, draw a rectangle shape, and place a centered "Hello World" te... |
| [add-colored-shapes-to-pdf-graph](./add-colored-shapes-to-pdf-graph.cs) | Add Colored Shapes to PDF Using a Graph and Dictionary | `Document`, `Page`, `Graph` | Shows how to map shape identifiers to fill colors with a dictionary and apply those colors when b... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add Dashed Rectangle to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph, draw a rectangle with a 2‑point dashed border, and save the PDF usin... |
| [add-ellipse-with-border-and-centered-text](./add-ellipse-with-border-and-centered-text.cs) | Add Ellipse with Border and Centered Text to PDF | `Document`, `Page`, `Ellipse` | Creates a PDF, adds an ellipse with a thick border and semi‑transparent fill, then places a cente... |
| [add-filled-arc-graph-to-pdf](./add-filled-arc-graph-to-pdf.cs) | Add Filled Arc Graph to PDF | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document with Aspose.Pdf, add a Graph containing a filled Arc sh... |
| [add-filled-arc-radial-gradient](./add-filled-arc-radial-gradient.cs) | Add Filled Arc with Radial Gradient to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw an arc shape with a radial gradient fill inside a graph on a PDF page. |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add Filled Circle to PDF with Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a Graph, draw a filled circle with custom colors a... |
| [add-filled-curve-to-pdf](./add-filled-curve-to-pdf.cs) | Add Filled Curve to PDF with Opacity and Border | `Document`, `Page`, `Graph` | Shows how to create a Bezier curve inside a Graph, set its fill opacity and border thickness, and... |
| [add-filled-dashed-rectangle-to-pdf](./add-filled-dashed-rectangle-to-pdf.cs) | Add Filled Dashed Rectangle to PDF using Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled rectangle with a dashed border on a PDF page using Aspose.Pdf's... |
| [add-filled-ellipse-gradient](./add-filled-ellipse-gradient.cs) | Add Filled Ellipse with Gradient to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF, adds a graph containing an ellipse filled with an axial gradient, and saves the re... |
| [add-gradient-ellipses-to-pdfs-parallel](./add-gradient-ellipses-to-pdfs-parallel.cs) | Add Gradient-Filled Ellipses to PDFs in Parallel | `Document`, `Page`, `Graph` | Demonstrates loading multiple PDF files concurrently, adding a graph of gradient‑filled ellipses ... |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Shapes to Existing PDF | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, appending a new page, and drawing a graph containing rectan... |
| [add-graph-with-shapes-to-pdf__v2](./add-graph-with-shapes-to-pdf__v2.cs) | Add Graph with Shapes to PDF | `Document`, `Page`, `Graph` | Shows how to load a PDF, create a Graph covering the page, add rectangle and ellipse shapes, and ... |
| [add-hexagon-graph-to-pdf](./add-hexagon-graph-to-pdf.cs) | Add a Hexagon Graph with Border Styling to PDF | `Document`, `Page`, `Graph` | Demonstrates how to create a regular hexagon as a Graph, set its border color and thickness, and ... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Add Multi-Colored Line Segments to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a Graph in Aspose.Pdf, add several Line shapes with different colors t... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Shows how to place multiple rectangles of varying sizes onto a PDF graph while preventing overlap... |
| [add-rectangle-ellipse-graph-to-pdf](./add-rectangle-ellipse-graph-to-pdf.cs) | Add Rectangle and Ellipse Shapes to a PDF Graph | `Document`, `Page`, `Graph` | The example shows how to create a Graph, add a rectangle and an ellipse with distinct fill and st... |
| [add-rectangle-gradient](./add-rectangle-gradient.cs) | Add Rectangle with Linear Gradient (Transparent to Opaque) | `Document`, `Page`, `Artifact` | Demonstrates adding a rectangle filled with a linear gradient that transitions from transparent t... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with a drop‑shadow by using a Graph container and a semi‑transparen... |
| [add-red-filled-rectangle-to-pdf](./add-red-filled-rectangle-to-pdf.cs) | Add Red Filled Rectangle to PDF Using Graph | `Document`, `Page`, `Graph` | Demonstrates how to add a rectangle with absolute coordinates and a solid red fill to a PDF using... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle with Solid Fill to PDF | `Document`, `Page`, `Graph` | Shows how to insert a rectangle with rounded corners and a solid fill color into a PDF page using... |
| [add-shadow-to-rectangle](./add-shadow-to-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in a Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a filled rectangle inside a graph and apply a shadow effect by setting... |
| [add-text-inside-graph-pdf](./add-text-inside-graph-pdf.cs) | Add Text Inside a Graph in PDF | `Document`, `Page`, `Graph` | Shows how to create a Graph on a PDF page, position it, and insert a TextFragment inside the grap... |
| [add-unfilled-arc-line-width-dash-style](./add-unfilled-arc-line-width-dash-style.cs) | Add Unfilled Arc with Line Width and Dash Style | `Document`, `Page`, `Graph` | Shows how to draw an unfilled arc in a PDF using Aspose.Pdf, configure its line width and dash pa... |
| [apply-clipping-region-graph](./apply-clipping-region-graph.cs) | Apply Clipping Region to a Graph | `Document`, `Graph`, `Rectangle` | Demonstrates how to set a clipping rectangle on a Graph so that its shapes are rendered only insi... |
| [apply-linear-gradient-ellipse](./apply-linear-gradient-ellipse.cs) | Apply Linear Gradient Fill to an Ellipse in a PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF document, add a graph with an ellipse, and fill the ellipse with a blue... |
| [batch-insert-logo-graph-into-pdf-pages](./batch-insert-logo-graph-into-pdf-pages.cs) | Batch Insert Logo Graph into PDF Pages | `Document`, `Page`, `Graph` | Loads each PDF from a source folder, creates a Graph with a rectangle representing a company logo... |
| [catch-out-of-bounds-shapes-pdf-graph](./catch-out-of-bounds-shapes-pdf-graph.cs) | Catch Out-of-Bounds Shapes When Adding to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to enable bounds checking for a Graph, add shapes, catch BoundsOutOfRangeExcepti... |
| ... | | | *and 48 more files* |

## Category Statistics
- Total examples: 78

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-graphs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-18 | Run: `20260618_030657_3e36a8`
<!-- AUTOGENERATED:END -->

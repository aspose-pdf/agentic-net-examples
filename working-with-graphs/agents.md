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

- `using Aspose.Pdf;` (69/70 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (66/70 files) ← category-specific
- `using Aspose.Pdf.Text;` (11/70 files)
- `using Aspose.Pdf.Operators;` (3/70 files)
- `using Aspose.Pdf.Annotations;` (1/70 files)
- `using System;` (70/70 files)
- `using System.Runtime.InteropServices;` (50/70 files) ← category-specific
- `using System.IO;` (30/70 files)
- `using System.Collections.Generic;` (6/70 files)
- `using System.Text.Json;` (1/70 files)
- `using System.Threading.Tasks;` (1/70 files)

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
| [add-background-image-and-draw-shapes](./add-background-image-and-draw-shapes.cs) | Add Background Image to PDF Page and Draw Shapes on Top | `Document`, `Page`, `Image` | Demonstrates how to set a background image for a PDF page using Aspose.Pdf and overlay a graph wi... |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph, set its horizontal and vertical alignment to center, configure its a... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add Dashed Rectangle to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, define a rectangle with a 2‑point dashed border... |
| [add-ellipse-with-border-and-centered-text](./add-ellipse-with-border-and-centered-text.cs) | Add Ellipse with Border and Centered Text to PDF | `Document`, `Ellipse`, `GraphInfo` | Shows how to draw an ellipse with a thick border and semi‑transparent fill and place a centered T... |
| [add-filled-bezier-curve-to-pdf-graph](./add-filled-bezier-curve-to-pdf-graph.cs) | Add Filled Bezier Curve with Opacity to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph, draw a Bezier curve, set a semi‑transparent fill color an... |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add Filled Circle to PDF with Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates creating a PDF document, adding a Graph that spans the page, and drawing a filled ci... |
| [add-filled-rectangle-dashed-border-to-pdf-graph](./add-filled-rectangle-dashed-border-to-pdf-graph.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph on a PDF page and draw a rectangle with a light‑gray fill, black dash... |
| [add-gradient-ellipse-graph-to-pdf-pages](./add-gradient-ellipse-graph-to-pdf-pages.cs) | Add Gradient Ellipse Graph to PDF Pages Concurrently | `Document`, `Page`, `Graph` | The example loads multiple PDF files in parallel, creates a full‑page graph on each page, and fil... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Document | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, creating a Graph container with rectangle and line shapes, ... |
| [add-graph-watermark-to-pdf-pages](./add-graph-watermark-to-pdf-pages.cs) | Add Graph Watermark to PDF Pages | `Document`, `Page`, `Graph` | The example iterates through PDF files in a folder, adds a Graph containing a rectangle shape as ... |
| [add-multiple-colored-line-segments-to-pdf-graph](./add-multiple-colored-line-segments-to-pdf-graph.cs) | Add Multiple Colored Line Segments to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a Graph in Aspose.Pdf, adding several Line shapes with different colors, an... |
| [add-non-overlapping-random-rectangles-to-pdf-graph](./add-non-overlapping-random-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Random Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates placing multiple random-sized rectangles on a PDF page using Aspose.Pdf while ensuri... |
| [add-page-draw-shapes-graph](./add-page-draw-shapes-graph.cs) | Add a New Page and Draw Shapes Using Graph | `Document`, `Page`, `Graph` | The example loads an existing PDF, adds a blank page, creates a Graph container, draws a rectangl... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with a drop‑shadow in a PDF by using a Graph container and a semi‑t... |
| [add-red-filled-rectangle-to-pdf](./add-red-filled-rectangle-to-pdf.cs) | Add a Red Filled Rectangle to a PDF with Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF, add a Graph canvas, draw a rectangle using absolute coordinates... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Regular Hexagon to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph, calculate the vertices of a regular hexagon, draw its sides with cus... |
| [add-rounded-rectangle-with-fill-to-pdf](./add-rounded-rectangle-with-fill-to-pdf.cs) | Add Rounded Rectangle with Fill to PDF | `Document`, `Page`, `Graph` | The example creates a PDF document, adds a Graph container, defines a rectangle with rounded corn... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle with Fill to PDF | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph, draw a rectangle with rounded corners, apply a solid fill... |
| [add-semi-transparent-rectangle-graph-layer](./add-semi-transparent-rectangle-graph-layer.cs) | Add Semi-Transparent Rectangle Using Graph Layer | `Document`, `Page`, `Graph` | Demonstrates how to begin a transparency layer with Aspose.Pdf's Graph container, draw a semi‑tra... |
| [add-shapes-to-pdf-using-dictionary](./add-shapes-to-pdf-using-dictionary.cs) | Add Shapes to PDF Using Dictionary for Fill Colors | `Document`, `Page`, `Graph` | Shows how to map shape identifiers to fill colors with a dictionary and apply those colors when d... |
| [add-shapes-with-bounds-checking-to-pdf](./add-shapes-with-bounds-checking-to-pdf.cs) | Add Shapes with Bounds Checking and Logging to PDF | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding rectangle shapes inside a Graph container, performing pre‑val... |
| [add-text-inside-graph-pdf](./add-text-inside-graph-pdf.cs) | Add Text Inside a Graph with Font Settings in PDF | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF, add a Graph container with a background rectangle, and place a ... |
| [add-unfilled-arc-with-line-width-and-dash-style](./add-unfilled-arc-with-line-width-and-dash-style.cs) | Add Unfilled Arc with Custom Line Width and Dash Style to PD... | `Document`, `Page`, `Graph` | Demonstrates creating an unfilled arc in a PDF using Aspose.Pdf, setting its line width and dash ... |
| [adjust-ellipse-bounds-in-pdf](./adjust-ellipse-bounds-in-pdf.cs) | Adjust Ellipse Position Within PDF Page Bounds | `Document`, `Page`, `Graph` | Demonstrates loading a PDF, creating an ellipse shape, checking if it fits within the page, and r... |
| [align-graph-left-margin-pdf](./align-graph-left-margin-pdf.cs) | Align Graph to Left Margin in PDF | `Document`, `Page`, `Graph` | Demonstrates loading a PDF, creating a Graph object, positioning it with a left offset, adding a ... |
| [apply-clipping-region-to-pdf-graph](./apply-clipping-region-to-pdf-graph.cs) | Apply Clipping Region to PDF Graph | `Document`, `Page`, `MoveTo` | Demonstrates defining a rectangular clipping path on a PDF page and rendering a graph so that onl... |
| [apply-clipping-region-to-pdf-shapes](./apply-clipping-region-to-pdf-shapes.cs) | Apply Clipping Region to PDF Shapes | `Document`, `Page`, `MoveTo` | Shows how to use the Clip and EOClip operators to restrict drawing of shapes within a defined rec... |
| [batch-insert-logo-rectangle-into-pdfs](./batch-insert-logo-rectangle-into-pdfs.cs) | Batch Insert Logo Rectangle into PDFs | `Document`, `Page`, `Graph` | Loads each PDF from a source folder, adds a Graph containing a rectangle that represents a compan... |
| [create-bezier-curve-pdf](./create-bezier-curve-pdf.cs) | Create Bezier Curve in PDF with Aspose.Pdf | `Document`, `Page`, `Graph` | Shows how to draw a cubic Bezier curve using four control points on a PDF page and set its stroke... |
| [create-custom-star-shape-pdf](./create-custom-star-shape-pdf.cs) | Create Custom Star Shape in PDF | `Document`, `Page`, `Graph` | Shows how to compute star vertices, build a closed Path with Line segments, apply fill and stroke... |
| ... | | | *and 40 more files* |

## Category Statistics
- Total examples: 70

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
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

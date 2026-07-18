---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **77** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (77/77 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (72/77 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/77 files)
- `using Aspose.Pdf.Annotations;` (2/77 files)
- `using Aspose.Pdf.Operators;` (2/77 files)
- `using Aspose.Pdf.Vector;` (1/77 files)
- `using System;` (77/77 files)
- `using System.IO;` (33/77 files)
- `using System.Collections.Generic;` (5/77 files)
- `using System.Text.Json;` (1/77 files)
- `using System.Threading.Tasks;` (1/77 files)

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
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph, center it on a PDF page, add a rectangle shape, and insert the graph... |
| [add-colored-line-segments-to-pdf-graph](./add-colored-line-segments-to-pdf-graph.cs) | Add Colored Line Segments to a PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, and draw consecutive line segments with varying... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add Dashed Rectangle to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a full‑page graph, and draws a rectangle with a 2‑point dashed borde... |
| [add-ellipse-with-border-and-centered-text](./add-ellipse-with-border-and-centered-text.cs) | Add Ellipse with Border and Centered Text to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a semi‑transparent ellipse with a thick border and place a centered text... |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add Filled Circle to PDF with Aspose.Pdf | `Document`, `Page`, `Graph` | Demonstrates creating a PDF document, adding a Graph that covers the page, drawing a filled circl... |
| [add-filled-curve-to-pdf-graph](./add-filled-curve-to-pdf-graph.cs) | Add Filled Curve with Opacity and Border to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a Graph in a PDF, drawing a Bezier curve, applying a semi‑transparent fill ... |
| [add-filled-rectangle-dashed-border-to-pdf-graph](./add-filled-rectangle-dashed-border-to-pdf-graph.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Graph`, `Rectangle` | Shows how to create a Graph, draw a rectangle with fill color, border color, line width, and dash... |
| [add-graph-matching-page-size](./add-graph-matching-page-size.cs) | Create a Graph Matching PDF Page Size | `Document`, `Page`, `PageInfo` | Shows how to instantiate an Aspose.Pdf.Drawing.Graph using the width and height of a PDF page and... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Document | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, creating an Aspose.Pdf.Drawing.Graph with a rectangle shape... |
| [add-graph-watermark-to-pdf-pages](./add-graph-watermark-to-pdf-pages.cs) | Add Graph Watermark Rectangle to PDF Pages | `Document`, `Page`, `Graph` | Demonstrates how to batch‑process PDF files, adding the same graph containing a rectangle waterma... |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Shapes to a PDF Page | `Document`, `Page`, `Graph` | Loads an existing PDF, adds a new blank page, creates a Graph object, draws a rectangle, ellipse ... |
| [add-line-to-pdf-graph](./add-line-to-pdf-graph.cs) | Add a Dimension‑Specific Line to a PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph, define a line with exact coordinates, set its color and thickness vi... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF, adds a Graph canvas, draws multiple rectangles of varying sizes while checking for... |
| [add-polygon-annotation-with-dashed-outline](./add-polygon-annotation-with-dashed-outline.cs) | Add Polygon Annotation with Dashed Outline to PDF | `Document`, `Page`, `PolygonAnnotation` | Demonstrates creating a polygon annotation in a PDF, applying a solid interior color, and configu... |
| [add-rectangle-ellipse-graph-to-pdf](./add-rectangle-ellipse-graph-to-pdf.cs) | Add Rectangle and Ellipse Shapes to PDF using Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a graph container, draws a rectangle and an ellipse with distinct fi... |
| [add-rectangle-radial-gradient-pdf](./add-rectangle-radial-gradient-pdf.cs) | Add Rectangle with Radial Gradient to PDF | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a Graph container, drawing a rectangle and applying a radial ... |
| [add-rectangle-with-bounds-checking](./add-rectangle-with-bounds-checking.cs) | Add Rectangle with Bounds Checking and Error Logging to PDF | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a rectangle shape inside a Graph container, manually checking... |
| [add-rectangle-with-drop-shadow-to-pdf](./add-rectangle-with-drop-shadow-to-pdf.cs) | Add Rectangle with Drop Shadow to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle with a semi‑transparent offset shadow using Aspose.Pdf's Gra... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Regular Hexagon to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a regular six‑sided polygon (hexagon) inside a Graph container, applying bo... |
| [add-rotated-ellipse-to-pdf](./add-rotated-ellipse-to-pdf.cs) | Add Rotated Ellipse to PDF | `Document`, `Page`, `Graph` | Demonstrates creating an ellipse shape, applying a 45° rotation using GraphInfo, and adding it to... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle with Fill to PDF | `Document`, `Page`, `Graph` | Shows how to insert a rectangle with rounded corners and a solid fill into an existing PDF docume... |
| [add-shadow-effect-to-filled-rectangle](./add-shadow-effect-to-filled-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in PDF | `Document`, `Page`, `Graph` | Shows how to simulate a shadow for a filled rectangle by drawing an offset semi‑transparent recta... |
| [add-solid-red-rectangle-to-pdf-graph](./add-solid-red-rectangle-to-pdf-graph.cs) | Add Solid Red Rectangle to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a graph, and draw a solid red rectangle using abso... |
| [add-text-inside-graph-pdf](./add-text-inside-graph-pdf.cs) | Add Text Inside a Graph with Font Styling | `Document`, `Page`, `Graph` | Demonstrates how to place a text fragment inside a Graph container in a PDF, set its font family,... |
| [add-unfilled-arc-with-line-width-and-dash-style](./add-unfilled-arc-with-line-width-and-dash-style.cs) | Add Unfilled Arc with Custom Line Width and Dash Style to PD... | `Document`, `Page`, `Graph` | This example creates a PDF document, adds a graph container, and draws an unfilled arc with a spe... |
| [adjust-rectangle-bounds-in-pdf](./adjust-rectangle-bounds-in-pdf.cs) | Adjust Rectangle Position Within PDF Page Bounds | `Document`, `Page`, `Graph` | The example loads a PDF, draws a rectangle using a Graph, checks whether the shape fits inside th... |
| [apply-background-image-to-pdf-graph](./apply-background-image-to-pdf-graph.cs) | Apply Background Image to PDF Graph and Draw Shapes on Top | `Document`, `Page`, `Image` | Demonstrates how to set a page background image and overlay a graph with rectangle and line shape... |
| [apply-clipping-region-to-graph](./apply-clipping-region-to-graph.cs) | Apply Clipping Region to a Graph in PDF | `Document`, `Page`, `MoveTo` | Demonstrates how to define a clipping rectangle using low‑level PDF operators and then draw a Gra... |
| [batch-insert-logo-graph-into-pdfs](./batch-insert-logo-graph-into-pdfs.cs) | Batch Insert Logo Graph into PDF Files | `Document`, `Graph`, `Rectangle` | Loads each PDF from an input folder, creates a Graph with a rectangle representing a company logo... |
| [center-text-in-rectangle-pdf](./center-text-in-rectangle-pdf.cs) | Center Text Inside a Rectangle on a PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle using a Graph container and place a centered TextFragment in... |
| ... | | | *and 47 more files* |

## Category Statistics
- Total examples: 77

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

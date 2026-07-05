---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **79** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (79/79 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (76/79 files) ← category-specific
- `using Aspose.Pdf.Text;` (8/79 files)
- `using Aspose.Pdf.Annotations;` (3/79 files)
- `using Aspose.Pdf.Facades;` (1/79 files)
- `using Aspose.Pdf.Operators;` (1/79 files)
- `using System;` (79/79 files)
- `using System.IO;` (36/79 files)
- `using System.Collections.Generic;` (3/79 files)
- `using NUnit.Framework;` (1/79 files)
- `using System.Reflection;` (1/79 files)
- `using System.Threading.Tasks;` (1/79 files)

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
| [add-a-rectangle-with-rounded-corners-by-specifying...](./add-a-rectangle-with-rounded-corners-by-specifying-corner-radius-and-apply-a-solid-fill.cs) | Add A Rectangle With Rounded Corners By Specifying Corner Ra... | `Graph`, `Rectangle` | Add A Rectangle With Rounded Corners By Specifying Corner Radius And Apply A Solid Fill |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph, center it on a PDF page, add a simple rectangle shape, and save the ... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add Dashed Rectangle to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, and draw a rectangle with a 2‑point border thic... |
| [add-ellipse-with-centered-text](./add-ellipse-with-centered-text.cs) | Add Ellipse with Transparent Fill and Centered Text | `Document`, `Page`, `Graph` | Demonstrates how to draw an ellipse with a semi‑transparent fill and a thick border, then place a... |
| [add-filled-arc-to-pdf-graph](./add-filled-arc-to-pdf-graph.cs) | Add Filled Arc to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a Graph container, and draw a filled arc with a cu... |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add Filled Circle to PDF Using Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a graph container, draw a filled circle with a bor... |
| [add-filled-curve-with-opacity-to-pdf-graph](./add-filled-curve-with-opacity-to-pdf-graph.cs) | Add Filled Curve with Opacity to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a Bezier curve inside a Graph, apply a semi‑transparent fill color and b... |
| [add-filled-dashed-rectangle-to-pdf](./add-filled-dashed-rectangle-to-pdf.cs) | Add Filled Dashed Rectangle to PDF Using Graph | `Document`, `Page`, `Graph` | Demonstrates how to load an existing PDF, create a Graph, and add a rectangle with fill color, cu... |
| [add-filled-ellipse-to-pdf-graph](./add-filled-ellipse-to-pdf-graph.cs) | Add Filled Ellipse to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF, add a Graph container, draw an ellipse inside it, and apply a s... |
| [add-gradient-ellipse-graphs-to-pdfs](./add-gradient-ellipse-graphs-to-pdfs.cs) | Add Gradient Ellipse Graphs to PDFs in Parallel | `Document`, `Page`, `Graph` | Shows how to load PDF files, add a graph containing gradient‑filled ellipses to every page, and s... |
| [add-graph-align-left-margin](./add-graph-align-left-margin.cs) | Add Graph to PDF Aligned to Left Margin | `Document`, `Page`, `Graph` | Demonstrates creating a Graph, setting its left offset, title, and border, and inserting it into ... |
| [add-graph-shapes-to-existing-pdf](./add-graph-shapes-to-existing-pdf.cs) | Add a Graph with Shapes to an Existing PDF | `Document`, `Page`, `Graph` | Shows how to load an existing PDF, add a new blank page, create a Graph, draw rectangle, ellipse ... |
| [add-graph-to-pdf-footer-using-margins](./add-graph-to-pdf-footer-using-margins.cs) | Add Graph to PDF Footer Using Margins | `Document`, `Page`, `HeaderFooter` | Demonstrates creating a Graph, setting its margin and horizontal alignment, placing it in the pag... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Document | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, creating a Graph with rectangle and line shapes, inserting ... |
| [add-graph-watermark-to-pdf-pages](./add-graph-watermark-to-pdf-pages.cs) | Add Graph Watermark Rectangle to PDF Pages | `Document`, `Page`, `Graph` | The example iterates over all PDF files in a folder, adds a Graph containing a rectangle watermar... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Add Multi-Colored Line Segments to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a PDF document with a Graph container and adding multiple line segments of ... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates placing multiple rectangles of varying sizes on a PDF graph while checking for overl... |
| [add-polygon-annotation-with-fill-and-dashed-outlin...](./add-polygon-annotation-with-fill-and-dashed-outline.cs) | Add Polygon Annotation with Fill and Dashed Outline to PDF | `Document`, `Page`, `PolygonAnnotation` | Shows how to create a polygon annotation in a PDF, apply a solid fill color (as a placeholder for... |
| [add-rectangle-ellipse-graph-to-pdf](./add-rectangle-ellipse-graph-to-pdf.cs) | Add Rectangle and Ellipse Shapes to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a Graph container, adding rectangle and ellipse shapes with distinct fill a... |
| [add-rectangle-solid-red-fill-to-pdf-graph](./add-rectangle-solid-red-fill-to-pdf-graph.cs) | Add Rectangle with Solid Red Fill to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph, define a rectangle using absolute coordinates, apply a solid red fil... |
| [add-rectangle-to-graph-pdf](./add-rectangle-to-graph-pdf.cs) | Add Rectangle to a Graph in a PDF | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF, add a Graph container, and draw a rectangle with specific dimen... |
| [add-rectangle-with-alpha-gradient](./add-rectangle-with-alpha-gradient.cs) | Add Rectangle with Alpha Gradient Fill to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle in a PDF and apply a semi‑transparent fill using the alpha c... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with a drop‑shadow effect in a PDF by placing a semi‑transparent of... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Regular Hexagon to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a regular six‑sided polygon, set its border color and thickness, and a... |
| [add-rotated-ellipse-to-pdf](./add-rotated-ellipse-to-pdf.cs) | Add Rotated Ellipse to PDF Using Aspose.Pdf | `Document`, `Page`, `Graph` | Shows how to create an ellipse, rotate it 45 degrees, and insert it into a PDF page using Aspose.... |
| [add-rounded-rectangle-with-fill-to-pdf-graph](./add-rounded-rectangle-with-fill-to-pdf-graph.cs) | Add Rounded Rectangle with Fill to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph container, and inserts a rectangle with rounded corners and ... |
| [add-shadow-effect-to-rectangle](./add-shadow-effect-to-rectangle.cs) | Add Shadow Effect to Rectangle in PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a rectangle with a simulated shadow inside a Graph container using Asp... |
| [add-text-inside-graph](./add-text-inside-graph.cs) | Add Text Inside a Graph with Font Settings | `Document`, `Page`, `Graph` | Demonstrates how to create a graph, draw a rectangle shape inside it, and place styled text at sp... |
| [add-unfilled-arc-with-custom-line-width-and-dash-s...](./add-unfilled-arc-with-custom-line-width-and-dash-style.cs) | Add Unfilled Arc with Custom Line Width and Dash Style | `Document`, `Page`, `Graph` | Demonstrates how to draw an unfilled arc on a PDF page using Aspose.Pdf, configuring its line wid... |
| [apply-background-image-to-pdf-graph](./apply-background-image-to-pdf-graph.cs) | Apply Background Image to PDF Graph with Overlaid Shapes | `Document`, `Page`, `Image` | Demonstrates setting a page background image in a PDF using Aspose.Pdf, then creating a Graph obj... |
| ... | | | *and 49 more files* |

## Category Statistics
- Total examples: 79

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **84** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (84/84 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (75/84 files) ← category-specific
- `using Aspose.Pdf.Text;` (17/84 files)
- `using Aspose.Pdf.Annotations;` (5/84 files)
- `using System;` (84/84 files)
- `using System.IO;` (44/84 files)
- `using System.Collections.Generic;` (5/84 files)
- `using NUnit.Framework;` (1/84 files)
- `using System.Drawing;` (1/84 files)
- `using System.Text.Json;` (1/84 files)
- `using System.Threading.Tasks;` (1/84 files)

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
| [add-background-image-and-graph-to-pdf](./add-background-image-and-graph-to-pdf.cs) | Apply Background Image and Overlay Shapes in PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to add an image as a background stamp on a PDF page and then draw vector shapes ... |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph object, set its horizontal and vertical alignment to center, add it t... |
| [add-circle-to-pdf-graph-gradient](./add-circle-to-pdf-graph-gradient.cs) | Add Circle to PDF Graph with Gradient Fill | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a full‑page Graph, drawing a circle shape, applying a solid f... |
| [add-dashed-rectangle-to-graph](./add-dashed-rectangle-to-graph.cs) | Add Dashed Rectangle to PDF Using Aspose.Pdf | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle with a 2‑point black border and a dashed line style on a PDF... |
| [add-ellipse-gradient-simulation-center-verificatio...](./add-ellipse-gradient-simulation-center-verification.cs) | Add Ellipse with Gradient Simulation and Center Verification | `Document`, `Page`, `Ellipse` | The example creates an ellipse on a PDF page, applies a solid fill to simulate a conical gradient... |
| [add-ellipse-to-pdf-page](./add-ellipse-to-pdf-page.cs) | Create an Ellipse in a PDF with Specified Radii and Stroke C... | `Document`, `Page`, `Ellipse` | Demonstrates how to add an ellipse shape to a PDF page using Aspose.Pdf, specifying its center, h... |
| [add-filled-arc-to-pdf-graph](./add-filled-arc-to-pdf-graph.cs) | Create PDF with Filled Arc Graph | `Document`, `Page`, `Graph` | Shows how to generate a PDF, add a Graph canvas, draw an Arc shape, fill it with a custom color, ... |
| [add-filled-arc-with-radial-gradient](./add-filled-arc-with-radial-gradient.cs) | Add Filled Arc with Radial Gradient to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF graph, draw an arc with specific start and sweep angles, and fill it us... |
| [add-filled-circle-to-pdf-graph](./add-filled-circle-to-pdf-graph.cs) | Add Filled Circle to PDF Using Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled circle on a PDF page using Aspose.Pdf's Graph and Ellipse classes. |
| [add-filled-ellipse-with-gradient-to-pdf-graph](./add-filled-ellipse-with-gradient-to-pdf-graph.cs) | Add Filled Ellipse with Gradient Direction to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF document, add a Graph, draw an ellipse that fills the graph area, and s... |
| [add-filled-rectangle-with-dashed-border](./add-filled-rectangle-with-dashed-border.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Page`, `Graph` | The example creates a PDF document, adds a Graph container, and draws a filled rectangle with a c... |
| [add-full-size-graph-to-pdf-page](./add-full-size-graph-to-pdf-page.cs) | Create a Full-Page Graph and Draw a Rectangle in PDF | `Document`, `Page`, `Graph` | Demonstrates how to instantiate a Graph object that matches a PDF page's width and height and add... |
| [add-gradient-ellipse-graphs-to-pdfs](./add-gradient-ellipse-graphs-to-pdfs.cs) | Add Ellipse Graph to PDFs Concurrently | `Document`, `Page`, `Graph` | Loads each PDF, adds a Graph containing an ellipse shape, and saves the modified file while proce... |
| [add-graph-rectangle-watermark-to-pdf](./add-graph-rectangle-watermark-to-pdf.cs) | Insert Graph Image and Watermark into Multiple PDFs | `Document`, `Page`, `ImageStamp` | The example iterates through all PDF files in a folder, adds a graph image stamp and a semi‑trans... |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Axes and Data Points to PDF | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, adding a new page, and drawing a simple graph with axes, da... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Add Colored Line Segments to a Graph in PDF | `Document`, `Page`, `TextFragment` | Shows how to create a PDF, add a titled graph, and draw consecutive line segments with varying co... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to PDF with Bounds Checking | `Document`, `Page`, `Rectangle` | Demonstrates adding multiple rectangle annotations to a PDF while ensuring they do not overlap by... |
| [add-polygon-gradient-fill-dashed-outline](./add-polygon-gradient-fill-dashed-outline.cs) | Add Polygon with Gradient Fill and Dashed Outline to PDF Gra... | `Document`, `Page`, `Graph` | Shows how to create a Graph, draw a closed polygon, fill it with an axial gradient pattern, and a... |
| [add-rectangle-solid-red-fill-to-pdf-graph](./add-rectangle-solid-red-fill-to-pdf-graph.cs) | Add Solid Red Rectangle to PDF Using Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a full‑page Graph, and draw a rectangle with absolute coordinates ... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle and a semi‑transparent offset shadow on a PDF page using Aspose.Pdf... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Hexagon Shape to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a Graph, draw a regular six‑sided polygon (hexagon) using a Line shape... |
| [add-rounded-rectangle-with-fill-to-pdf-graph](./add-rounded-rectangle-with-fill-to-pdf-graph.cs) | Add Rounded Rectangle with Fill Color to PDF | `Document`, `Page`, `Graph` | Demonstrates how to insert a rectangle shape into a PDF page using a Graph container, set rounded... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle with Fill to PDF | `Document`, `Page`, `Graph` | Shows how to create a rectangle with rounded corners, apply a solid fill color, and place it on a... |
| [add-semi-transparent-rectangle-using-transparency-...](./add-semi-transparent-rectangle-using-transparency-layer.cs) | Draw Semi-Transparent Rectangle Using Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a Graph that covers a PDF page, add a rectangle shape with a semi‑tran... |
| [add-semi-transparent-rectangle](./add-semi-transparent-rectangle.cs) | Add Gradient Rectangle with Alpha Transparency to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to draw a rectangle filled with a vertical axial gradient that transitions from ... |
| [add-shadow-effect-to-filled-rectangle](./add-shadow-effect-to-filled-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to emulate a shadow for a rectangle inside an Aspose.Pdf Graph by drawing an off... |
| [add-shapes-with-bounds-checking](./add-shapes-with-bounds-checking.cs) | Catch Out-of-Bounds PDF Shape Annotations | `Document`, `Page`, `Rectangle` | Demonstrates adding rectangle (square) annotations to PDF pages with Aspose.Pdf while catching bo... |
| [add-square-annotations-with-bounds-validation](./add-square-annotations-with-bounds-validation.cs) | Add Square Annotations with Bounds Validation and Error Logg... | `Document`, `Page`, `Rectangle` | Demonstrates loading a PDF, iterating its pages, adding square annotations defined by custom rect... |
| [add-text-inside-graph](./add-text-inside-graph.cs) | Add Text at Specific Coordinates to PDF Graph | `Document`, `Page`, `TextFragment` | Shows how to insert a text fragment into an existing PDF, set its font family, size, and color, a... |
| [add-unfilled-arc-with-custom-stroke](./add-unfilled-arc-with-custom-stroke.cs) | Add Unfilled Dashed Arc to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph object, configures line width and dash pattern, and draws an... |
| ... | | | *and 54 more files* |

## Category Statistics
- Total examples: 84

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
Updated: 2026-10-26 | Run: `20261026_093327_b1e1a0`
<!-- AUTOGENERATED:END -->

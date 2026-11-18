---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **87** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (87/87 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (78/87 files) ← category-specific
- `using Aspose.Pdf.Text;` (37/87 files)
- `using Aspose.Pdf.Annotations;` (2/87 files)
- `using Aspose.Pdf.Operators;` (1/87 files)
- `using System;` (87/87 files)
- `using System.IO;` (56/87 files)
- `using System.Collections.Generic;` (6/87 files)
- `using System.Drawing;` (2/87 files)
- `using System.Text.Json;` (1/87 files)
- `using System.Threading.Tasks;` (1/87 files)

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
| [add-background-image-and-graph-to-pdf](./add-background-image-and-graph-to-pdf.cs) | Add Background Image and Overlay Shapes to PDF | `Document`, `Page`, `ImageStamp` | Demonstrates how to place a background image on each PDF page using an ImageStamp and then draw s... |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Bar Chart Graph to PDF Page | `Document`, `Page`, `Graph` | Loads an existing PDF, creates a Graph containing a simple bar chart, centers it on the page usin... |
| [add-circle-to-pdf-graph-gradient](./add-circle-to-pdf-graph-gradient.cs) | Insert Gradient Filled Circle into PDF Graph | `Document`, `Page`, `Graph` | Shows how to add a Graph to a PDF page and draw a circle with a radial gradient fill using two co... |
| [add-dashed-rectangle-to-graph](./add-dashed-rectangle-to-graph.cs) | Add Dashed Rectangle Annotation to PDF Graph | `Document`, `Page`, `Rectangle` | Shows how to place a rectangle with a 2‑point dashed border over an existing graph in a PDF using... |
| [add-ellipse-gradient-simulation-center-verificatio...](./add-ellipse-gradient-simulation-center-verification.cs) | Add Ellipse with Gradient Fill and Verify Center | `Document`, `Page`, `Graph` | Creates a PDF, adds an ellipse shape filled with a radial gradient (as a stand‑in for a conical g... |
| [add-ellipse-to-pdf-page](./add-ellipse-to-pdf-page.cs) | Create Ellipse in PDF Graph with Custom Radii and Stroke Col... | `Document`, `Page`, `Graph` | Shows how to add a Graph to a PDF page and draw an ellipse with specified horizontal and vertical... |
| [add-filled-arc-to-pdf-graph](./add-filled-arc-to-pdf-graph.cs) | Add Filled Arc to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a PDF document, adding a Graph container, drawing an Arc shape, filling it ... |
| [add-filled-arc-with-radial-gradient](./add-filled-arc-with-radial-gradient.cs) | Add Filled Arc with Radial Gradient to Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF graph, draw an arc shape, and apply a radial gradient fill using Aspose... |
| [add-filled-circle-to-pdf-graph](./add-filled-circle-to-pdf-graph.cs) | Add Filled Circle to PDF Using Aspose.Pdf | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document and draw a filled circle by defining its bounding recta... |
| [add-filled-curve-with-opacity-and-border](./add-filled-curve-with-opacity-and-border.cs) | Add Filled Curve with Opacity and Border to PDF | `Document`, `Page`, `Graph` | Shows how to draw a Bezier curve on a PDF page, apply a semi‑transparent fill color and set the b... |
| [add-filled-ellipse-with-gradient-to-pdf-graph](./add-filled-ellipse-with-gradient-to-pdf-graph.cs) | Add Filled Ellipse with Gradient to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw an ellipse inside a Graph container and fill it with a vertical axial gr... |
| [add-filled-rectangle-with-dashed-border](./add-filled-rectangle-with-dashed-border.cs) | Add Filled Rectangle with Dashed Border to a PDF Graph | `Document`, `Page`, `Graph` | The example creates a PDF document, adds a Graph container, and draws a rectangle with a light gr... |
| [add-full-size-graph-to-pdf-page](./add-full-size-graph-to-pdf-page.cs) | Create a Graph Matching Page Size in PDF | `Document`, `Page`, `Graph` | Demonstrates how to instantiate a Graph object with the same width and height as the PDF page, ad... |
| [add-gradient-ellipse-graphs-to-pdfs](./add-gradient-ellipse-graphs-to-pdfs.cs) | Add Gradient Ellipses to PDFs in Parallel | `Document`, `Page`, `Graph` | Demonstrates how to process multiple PDF files concurrently with Parallel.ForEach and add a graph... |
| [add-graph-rectangle-watermark-to-pdf](./add-graph-rectangle-watermark-to-pdf.cs) | Insert Identical Graph and Watermark into Multiple PDFs | `Document`, `Page`, `Graph` | The example iterates over PDF files in a folder, adds the same chart (Graph) and a semi‑transpare... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Bar Graph to PDF Using Aspose.Pdf | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a simple bar‑graph with a Table and Graph shapes, and s... |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Shapes to a New PDF Page | `Document`, `Page`, `Graph` | Demonstrates loading an existing PDF, adding a blank page, creating a Graph object, drawing basic... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Add Colored Continuous Line Segments to a PDF Graph | `Document`, `Page`, `Rectangle` | Demonstrates how to create a PDF document, add a Graph, and draw multiple line segments with diff... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to PDF | `Document`, `Page`, `Graph` | Creates a PDF, adds a Graph container, and draws multiple rectangles of varying sizes while check... |
| [add-polygon-gradient-fill-dashed-outline](./add-polygon-gradient-fill-dashed-outline.cs) | Add Polygon to PDF Graph with Dashed Outline | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph object, draws a closed polygon using a Line shape, fills it ... |
| [add-rectangle-solid-red-fill-to-pdf-graph](./add-rectangle-solid-red-fill-to-pdf-graph.cs) | Add Solid Red Rectangle to PDF Using Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a Graph container, define a rectangle with absolute coordinates, apply... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with a semi‑transparent offset shadow in a PDF using Aspose.Pdf's d... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Hexagonal Polygon to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a Graph in a PDF, drawing a regular six‑sided polygon (hexagon) with a Line... |
| [add-rounded-rectangle-with-fill-to-pdf-graph](./add-rounded-rectangle-with-fill-to-pdf-graph.cs) | Insert Rounded Rectangle into PDF Graph with Fill Color | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, draw a rectangle with rounded corners, apply a ... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle Annotation with Fill to PDF | `Document`, `Rectangle`, `SquareAnnotation` | Shows how to create a square annotation with rounded corners, apply a solid fill color, and add i... |
| [add-semi-transparent-rectangle-using-transparency-...](./add-semi-transparent-rectangle-using-transparency-layer.cs) | Add Semi-Transparent Rectangle Using Graph | `Document`, `Page`, `Graph` | Shows how to create a Graph, draw a semi‑transparent rectangle with 50% opacity, and save the res... |
| [add-semi-transparent-rectangle](./add-semi-transparent-rectangle.cs) | Add Gradient Rectangle with Transparency to PDF | `Document`, `Page`, `Graph` | Creates a PDF with a rectangle filled by a linear gradient that fades from fully transparent to f... |
| [add-shadow-effect-to-filled-rectangle](./add-shadow-effect-to-filled-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in a PDF Graph | `Document`, `Page`, `Graph` | Shows how to simulate a shadow for a filled rectangle by drawing an offset semi‑transparent recta... |
| [add-shapes-with-bounds-checking](./add-shapes-with-bounds-checking.cs) | Handle Out-of-Bounds Shapes When Adding to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to catch exceptions caused by shapes that exceed page limits, log their coordina... |
| [add-square-annotations-with-bounds-validation](./add-square-annotations-with-bounds-validation.cs) | Add Rectangle Shape with Bounds Error Logging to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle on a PDF page using Aspose.Pdf and logs detailed coordinate ... |
| ... | | | *and 57 more files* |

## Category Statistics
- Total examples: 87

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
Updated: 2026-11-18 | Run: `20261118_010903_d0b86c`
<!-- AUTOGENERATED:END -->

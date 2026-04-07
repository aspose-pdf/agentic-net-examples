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

- `using Aspose.Pdf;` (84/85 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (81/85 files) ← category-specific
- `using Aspose.Pdf.Text;` (10/85 files)
- `using Aspose.Pdf.Facades;` (4/85 files)
- `using Aspose.Pdf.Operators;` (3/85 files)
- `using Aspose.Pdf.Annotations;` (2/85 files)
- `using Aspose.Pdf.Generator;` (1/85 files)
- `using System;` (85/85 files)
- `using System.IO;` (35/85 files)
- `using System.Collections.Generic;` (5/85 files)
- `using NUnit.Framework;` (1/85 files)
- `using System.Reflection;` (1/85 files)
- `using System.Runtime.InteropServices;` (1/85 files)
- `using System.Text.Json;` (1/85 files)
- `using System.Threading.Tasks;` (1/85 files)

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
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Graph`, `HorizontalAlignment` | Loads an existing PDF, creates a Graph object, aligns it to the page center horizontally, adds it... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add Dashed Rectangle to PDF Graph | `Document`, `Save`, `Page` | Demonstrates drawing a rectangle with a 2‑point border and dashed line style inside a Graph conta... |
| [add-ellipse-graph-to-pdf-pages-parallel](./add-ellipse-graph-to-pdf-pages-parallel.cs) | Add Ellipse Graph to PDF Pages Using Parallel.ForEach | `Document`, `Save`, `Page` | Demonstrates loading PDF files, iterating through each page, creating a Graph with an ellipse sha... |
| [add-filled-arc-with-radial-gradient](./add-filled-arc-with-radial-gradient.cs) | Add Filled Arc with Radial Gradient to PDF Graph | `Document`, `Save`, `Page` | Shows how to create an Arc shape inside a Graph and fill it with a radial gradient shading in a P... |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add a Filled Circle to a PDF Using Aspose.Pdf | `Document`, `Page`, `Graph` | Shows how to create a PDF document, add a Graph container, and draw a filled circle with a given ... |
| [add-filled-curve-to-pdf-graph](./add-filled-curve-to-pdf-graph.cs) | Add Filled Curve to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF document, add a Graph container, draw a Bezier curve with a fill color ... |
| [add-filled-rectangle-with-dashed-border](./add-filled-rectangle-with-dashed-border.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a Graph container, drawing a filled rectangle with a custom d... |
| [add-gradient-filled-ellipse-to-pdf-graph](./add-gradient-filled-ellipse-to-pdf-graph.cs) | Add Gradient-Filled Ellipse to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph container, draws an ellipse, and fills it with an axial grad... |
| [add-gradient-rectangle-alpha-transparency](./add-gradient-rectangle-alpha-transparency.cs) | Add Gradient Rectangle with Alpha Transparency to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a rectangle filled with a linear axial gradient that transitions from fu... |
| [add-graph-shapes-to-pdf-page](./add-graph-shapes-to-pdf-page.cs) | Add Graph with Shapes to a New PDF Page | `Document`, `Save`, `Page` | The example loads an existing PDF, adds a blank page, creates a Graph object, draws rectangle, el... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Using Aspose.Pdf | `Document`, `Save`, `Page` | The example loads an existing PDF, creates a Graph containing a rectangle, ellipse, and line, add... |
| [add-graph-with-labels-to-pdf](./add-graph-with-labels-to-pdf.cs) | Add Graph with Labels to PDF | `Document`, `Page`, `Graph` | Demonstrates loading a PDF from a byte array, creating a Graph with rectangle, line, and ellipse ... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Add Multi-Colored Line Segments to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF graph and add multiple line segments, each with its own color, u... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Multiple Rectangles to a PDF Graph with Overlap Checking | `Document`, `Save`, `Page` | Creates a PDF document, adds a graph area, and places several rectangles of varying sizes while v... |
| [add-polygon-annotation-with-dashed-outline](./add-polygon-annotation-with-dashed-outline.cs) | Add Polygon Annotation with Dashed Outline and Fill | `Document`, `Save`, `PdfContentEditor` | Demonstrates how to create a polygon annotation on a PDF page, fill it with a color (placeholder ... |
| [add-radial-gradient-rectangle-to-pdf](./add-radial-gradient-rectangle-to-pdf.cs) | Add Radial Gradient Rectangle to PDF | `Document`, `Save`, `Page` | Demonstrates how to create a rectangle inside a graph and fill it with a radial gradient that tra... |
| [add-rectangle-shape-with-bounds-checking](./add-rectangle-shape-with-bounds-checking.cs) | Add Rectangle Shape with Bounds Checking to PDF | `Document`, `Rectangle`, `GraphInfo` | Loads a PDF, creates a rectangle shape, verifies that it fits within the page bounds, logs detail... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Save`, `Page` | Shows how to create a PDF, add a Graph container, draw a semi‑transparent offset rectangle as a s... |
| [add-red-filled-rectangle-to-pdf](./add-red-filled-rectangle-to-pdf.cs) | Add a Red Filled Rectangle to a PDF Using Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF, add a Graph that covers the page, and draw a solid red rectangl... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Regular Hexagon to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a regular hexagon shape inside a PDF graph, set its border color and t... |
| [add-shadow-effect-to-filled-rectangle](./add-shadow-effect-to-filled-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in a PDF Graph | `Document`, `Page`, `Graph` | Shows how to simulate a shadow for a rectangle inside an Aspose.Pdf Graph by drawing an offset se... |
| [add-shapes-with-bounds-checking-to-pdf](./add-shapes-with-bounds-checking-to-pdf.cs) | Add Shapes with Bounds Checking to PDF | `Document`, `Page`, `PageInfo` | Demonstrates creating a PDF, adding rectangles, ellipses and lines inside a Graph container, perf... |
| [add-text-inside-graph](./add-text-inside-graph.cs) | Add Text Inside a Graph with Custom Font and Position | `Document`, `Save`, `Page` | Demonstrates creating a graph in a PDF, applying styling, and inserting a text fragment with a sp... |
| [add-unfilled-arc-with-custom-stroke](./add-unfilled-arc-with-custom-stroke.cs) | Add Unfilled Arc with Custom Stroke to PDF | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a Graph container, drawing an unfilled arc, and configuring i... |
| [adjust-shape-position-to-fit-page-bounds](./adjust-shape-position-to-fit-page-bounds.cs) | Adjust Shape Position to Fit Within PDF Page Bounds | `Document`, `Page`, `Graph` | Shows how to verify a rectangle's coordinates against a PDF page size and reposition it so the sh... |
| [apply-background-image-and-overlay-graph](./apply-background-image-and-overlay-graph.cs) | Apply Background Image and Overlay Graph on PDF | `Document`, `PdfFileStamp`, `Stamp` | Demonstrates how to stamp a background image onto each PDF page and then draw vector shapes using... |
| [apply-clipping-region-to-pdf-graph](./apply-clipping-region-to-pdf-graph.cs) | Apply Clipping Region to PDF Graph | `Document`, `Page`, `MoveTo` | Demonstrates how to create a clipping rectangle with low‑level PDF operators and render a graph s... |
| [apply-dictionary-fill-colors-to-pdf-shapes](./apply-dictionary-fill-colors-to-pdf-shapes.cs) | Apply Dictionary-Based Fill Colors to PDF Shapes | `Document`, `Save`, `Page` | Demonstrates using a Dictionary to map shape identifiers to Aspose.Pdf.Drawing.Color values and a... |
| [apply-json-defined-fill-colors-to-pdf-shapes](./apply-json-defined-fill-colors-to-pdf-shapes.cs) | Apply JSON-Defined Fill Colors to PDF Shapes | `Document`, `Page`, `Graph` | The example loads a JSON configuration that maps shape types to color names, resolves the colors,... |
| [apply-radial-gradient-ellipse](./apply-radial-gradient-ellipse.cs) | Apply Radial Gradient Fill to an Ellipse with Dark Gray Bord... | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph canvas, draw an ellipse, set a dark gray border, and fill ... |
| ... | | | *and 55 more files* |

## Category Statistics
- Total examples: 85

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
Updated: 2026-04-07 | Run: `20260407_212538_113d5e`
<!-- AUTOGENERATED:END -->

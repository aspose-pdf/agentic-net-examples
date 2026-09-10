---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **129** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (72/129 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (69/129 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/129 files)
- `using Aspose.Pdf.Annotations;` (1/129 files)
- `using Aspose.Pdf.Operators;` (1/129 files)
- `using System;` (72/129 files)
- `using System.IO;` (30/129 files)
- `using System.Collections.Generic;` (6/129 files)
- `using NUnit.Framework;` (1/129 files)
- `using System.Text.Json;` (1/129 files)
- `using System.Threading.Tasks;` (1/129 files)

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
| [add-background-image-and-graph-to-pdf](./add-background-image-and-graph-to-pdf.cs) | Add Background Image to PDF and Draw Graph Shapes on Top | `Document`, `Page`, `Artifact` | Demonstrates how to place a background image on a PDF page using an Artifact and then draw a Grap... |
| [add-centered-graph-to-pdf-page](./add-centered-graph-to-pdf-page.cs) | Add Centered Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph, center it horizontally and vertically on a PDF page, add it to the p... |
| [add-colored-line-segments-to-pdf-graph](./add-colored-line-segments-to-pdf-graph.cs) | Add colored line segments to pdf graph |  | Add colored line segments to pdf graph |
| [add-dashed-rectangle-to-graph](./add-dashed-rectangle-to-graph.cs) | Add Dashed Rectangle to Graph in PDF | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph container, draws a rectangle with a 2‑point black dashed bor... |
| [add-dashed-rectangle-to-pdf-graph](./add-dashed-rectangle-to-pdf-graph.cs) | Add dashed rectangle to pdf graph |  | Add dashed rectangle to pdf graph |
| [add-ellipse-to-pdf-page](./add-ellipse-to-pdf-page.cs) | Add Ellipse to PDF Page with Specified Radii and Stroke Colo... | `Document`, `Page`, `Graph` | Demonstrates how to open an existing PDF, create a Graph container, draw an ellipse with given ho... |
| [add-ellipse-with-border-and-centered-text](./add-ellipse-with-border-and-centered-text.cs) | Add ellipse with border and centered text |  | Add ellipse with border and centered text |
| [add-filled-arc-to-pdf-graph](./add-filled-arc-to-pdf-graph.cs) | Add Filled Arc to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a graph, and draw a filled arc with a custom color... |
| [add-filled-arc-with-radial-gradient](./add-filled-arc-with-radial-gradient.cs) | Add Filled Arc with Radial Gradient to PDF | `Document`, `Page`, `Graph` | Creates a PDF document, adds a graph containing an arc shape, and demonstrates how to apply a rad... |
| [add-filled-circle-to-pdf-graph](./add-filled-circle-to-pdf-graph.cs) | Add Filled Circle to PDF Using Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a Graph container, draw a filled circle with a bor... |
| [add-filled-circle-to-pdf](./add-filled-circle-to-pdf.cs) | Add filled circle to pdf |  | Add filled circle to pdf |
| [add-filled-curve-to-pdf-graph](./add-filled-curve-to-pdf-graph.cs) | Add filled curve to pdf graph |  | Add filled curve to pdf graph |
| [add-filled-curve-with-opacity-and-border](./add-filled-curve-with-opacity-and-border.cs) | Add Filled Curve with Opacity and Border to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled Bézier curve in a PDF using Aspose.Pdf, setting fill opacity an... |
| [add-filled-rectangle-dashed-border-to-pdf-graph](./add-filled-rectangle-dashed-border-to-pdf-graph.cs) | Add filled rectangle dashed border to pdf graph |  | Add filled rectangle dashed border to pdf graph |
| [add-filled-rectangle-with-dashed-border](./add-filled-rectangle-with-dashed-border.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled rectangle with a custom dashed border inside a Graph container ... |
| [add-full-size-graph-to-pdf-page](./add-full-size-graph-to-pdf-page.cs) | Add Full-Size Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph object that matches the page dimensions of a PDF, add a rectangle sha... |
| [add-gradient-ellipse-graphs-to-pdfs](./add-gradient-ellipse-graphs-to-pdfs.cs) | Add Gradient Ellipse Graphs to PDFs in Parallel | `Document`, `Page`, `Graph` | Demonstrates processing multiple PDF files concurrently and adding a graph with gradient‑filled e... |
| [add-graph-matching-page-size](./add-graph-matching-page-size.cs) | Add graph matching page size |  | Add graph matching page size |
| [add-graph-rectangle-watermark-to-pdf](./add-graph-rectangle-watermark-to-pdf.cs) | Add Graph Rectangle Watermark to PDF Pages | `Document`, `Page`, `Graph` | The example loads each PDF from an input folder, creates a Graph that matches the page size, adds... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Document | `Document`, `Page`, `Graph` | Shows how to load a PDF, create a Graph with shapes such as a rectangle and line, place it on a p... |
| [add-graph-watermark-to-pdf-pages](./add-graph-watermark-to-pdf-pages.cs) | Add graph watermark to pdf pages |  | Add graph watermark to pdf pages |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Shapes to a PDF Page | `Document`, `Page`, `Graph` | Loads an existing PDF, adds a new page, creates a Graph container, draws a rectangle, ellipse, an... |
| [add-line-to-pdf-graph](./add-line-to-pdf-graph.cs) | Add line to pdf graph |  | Add line to pdf graph |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Create a Multi‑Color Line Graph in PDF | `Document`, `Page`, `Graph` | Demonstrates how to build a PDF document, add a Graph container, and draw multiple line segments ... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates placing multiple rectangles of varying sizes on a PDF graph while preventing overlap... |
| [add-polygon-annotation-with-dashed-outline](./add-polygon-annotation-with-dashed-outline.cs) | Add polygon annotation with dashed outline |  | Add polygon annotation with dashed outline |
| [add-rectangle-ellipse-graph-to-pdf](./add-rectangle-ellipse-graph-to-pdf.cs) | Add rectangle ellipse graph to pdf |  | Add rectangle ellipse graph to pdf |
| [add-rectangle-radial-gradient-pdf](./add-rectangle-radial-gradient-pdf.cs) | Add rectangle radial gradient pdf |  | Add rectangle radial gradient pdf |
| [add-rectangle-solid-red-fill-to-pdf-graph](./add-rectangle-solid-red-fill-to-pdf-graph.cs) | Add Rectangle with Solid Red Fill to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, define a rectangle using absolute coordinates, ... |
| [add-rectangle-with-bounds-checking](./add-rectangle-with-bounds-checking.cs) | Add rectangle with bounds checking |  | Add rectangle with bounds checking |
| ... | | | *and 99 more files* |

## Category Statistics
- Total examples: 129

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

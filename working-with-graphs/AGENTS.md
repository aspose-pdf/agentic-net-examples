---
name: working-with-graphs
description: C# examples for working-with-graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-graphs

> **Working with graphs** in PDF using C# / .NET -- **72** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-graphs** category.
This folder contains standalone C# examples for working-with-graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (72/72 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (69/72 files) ← category-specific
- `using Aspose.Pdf.Text;` (7/72 files)
- `using Aspose.Pdf.Annotations;` (1/72 files)
- `using Aspose.Pdf.Operators;` (1/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (30/72 files)
- `using System.Collections.Generic;` (6/72 files)
- `using NUnit.Framework;` (1/72 files)
- `using System.Text.Json;` (1/72 files)
- `using System.Threading.Tasks;` (1/72 files)

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
| [add-dashed-rectangle-to-graph](./add-dashed-rectangle-to-graph.cs) | Add Dashed Rectangle to Graph in PDF | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph container, draws a rectangle with a 2‑point black dashed bor... |
| [add-ellipse-to-pdf-page](./add-ellipse-to-pdf-page.cs) | Add Ellipse to PDF Page with Specified Radii and Stroke Colo... | `Document`, `Page`, `Graph` | Demonstrates how to open an existing PDF, create a Graph container, draw an ellipse with given ho... |
| [add-filled-arc-to-pdf-graph](./add-filled-arc-to-pdf-graph.cs) | Add Filled Arc to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a graph, and draw a filled arc with a custom color... |
| [add-filled-arc-with-radial-gradient](./add-filled-arc-with-radial-gradient.cs) | Add Filled Arc with Radial Gradient to PDF | `Document`, `Page`, `Graph` | Creates a PDF document, adds a graph containing an arc shape, and demonstrates how to apply a rad... |
| [add-filled-circle-to-pdf-graph](./add-filled-circle-to-pdf-graph.cs) | Add Filled Circle to PDF Using Aspose.Pdf Graph | `Document`, `Page`, `Graph` | Demonstrates how to create a PDF document, add a Graph container, draw a filled circle with a bor... |
| [add-filled-curve-with-opacity-and-border](./add-filled-curve-with-opacity-and-border.cs) | Add Filled Curve with Opacity and Border to PDF | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled Bézier curve in a PDF using Aspose.Pdf, setting fill opacity an... |
| [add-filled-rectangle-with-dashed-border](./add-filled-rectangle-with-dashed-border.cs) | Add Filled Rectangle with Dashed Border to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a filled rectangle with a custom dashed border inside a Graph container ... |
| [add-full-size-graph-to-pdf-page](./add-full-size-graph-to-pdf-page.cs) | Add Full-Size Graph to PDF Page | `Document`, `Page`, `Graph` | Shows how to create a Graph object that matches the page dimensions of a PDF, add a rectangle sha... |
| [add-gradient-ellipse-graphs-to-pdfs](./add-gradient-ellipse-graphs-to-pdfs.cs) | Add Gradient Ellipse Graphs to PDFs in Parallel | `Document`, `Page`, `Graph` | Demonstrates processing multiple PDF files concurrently and adding a graph with gradient‑filled e... |
| [add-graph-rectangle-watermark-to-pdf](./add-graph-rectangle-watermark-to-pdf.cs) | Add Graph Rectangle Watermark to PDF Pages | `Document`, `Page`, `Graph` | The example loads each PDF from an input folder, creates a Graph that matches the page size, adds... |
| [add-graph-to-pdf](./add-graph-to-pdf.cs) | Add Graph to PDF Document | `Document`, `Page`, `Graph` | Shows how to load a PDF, create a Graph with shapes such as a rectangle and line, place it on a p... |
| [add-graph-with-shapes-to-pdf](./add-graph-with-shapes-to-pdf.cs) | Add Graph with Shapes to a PDF Page | `Document`, `Page`, `Graph` | Loads an existing PDF, adds a new page, creates a Graph container, draws a rectangle, ellipse, an... |
| [add-multi-colored-line-segments-to-pdf-graph](./add-multi-colored-line-segments-to-pdf-graph.cs) | Create a Multi‑Color Line Graph in PDF | `Document`, `Page`, `Graph` | Demonstrates how to build a PDF document, add a Graph container, and draw multiple line segments ... |
| [add-non-overlapping-rectangles-to-pdf-graph](./add-non-overlapping-rectangles-to-pdf-graph.cs) | Add Non-Overlapping Rectangles to a PDF Graph | `Document`, `Page`, `Graph` | Demonstrates placing multiple rectangles of varying sizes on a PDF graph while preventing overlap... |
| [add-rectangle-solid-red-fill-to-pdf-graph](./add-rectangle-solid-red-fill-to-pdf-graph.cs) | Add Rectangle with Solid Red Fill to PDF Graph | `Document`, `Page`, `Graph` | Shows how to create a PDF, add a Graph container, define a rectangle using absolute coordinates, ... |
| [add-rectangle-with-shadow-to-pdf](./add-rectangle-with-shadow-to-pdf.cs) | Add Rectangle with Shadow to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with a semi‑transparent offset shadow in a PDF using Aspose.Pdf's G... |
| [add-regular-hexagon-to-pdf-graph](./add-regular-hexagon-to-pdf-graph.cs) | Add Regular Hexagon to PDF Graph | `Document`, `Page`, `Graph` | Demonstrates how to draw a regular six‑sided polygon (hexagon) on a PDF page using Aspose.Pdf's G... |
| [add-rounded-rectangle-with-fill-to-pdf-graph](./add-rounded-rectangle-with-fill-to-pdf-graph.cs) | Add Rounded Rectangle with Fill to PDF Graph | `Document`, `Page`, `Graph` | Creates a PDF document, adds a Graph container, and draws a rounded rectangle with a solid fill a... |
| [add-rounded-rectangle-with-fill](./add-rounded-rectangle-with-fill.cs) | Add Rounded Rectangle with Fill to PDF | `Document`, `Page`, `Graph` | Shows how to draw a rectangle with rounded corners, set a corner radius, and apply a solid fill u... |
| [add-semi-transparent-rectangle-using-transparency-...](./add-semi-transparent-rectangle-using-transparency-layer.cs) | Add Semi-Transparent Rectangle via Transparency Layer | `Document`, `Page`, `Graph` | The example opens an existing PDF, creates a Graph as a transparency layer, draws a semi‑transpar... |
| [add-semi-transparent-rectangle](./add-semi-transparent-rectangle.cs) | Add Semi-Transparent Rectangle Using Alpha Channel | `Document`, `Page`, `Graph` | Creates a PDF containing a rectangle filled with a semi‑transparent color to demonstrate alpha‑ch... |
| [add-shadow-effect-to-filled-rectangle](./add-shadow-effect-to-filled-rectangle.cs) | Add Shadow Effect to a Filled Rectangle in PDF | `Document`, `Page`, `Graph` | Shows how to draw a filled rectangle with a simulated shadow by adding an offset semi‑transparent... |
| [add-shapes-with-bounds-checking](./add-shapes-with-bounds-checking.cs) | Add Shapes to PDF with Bounds Checking | `Document`, `Page`, `Graph` | Demonstrates loading a PDF, creating a Graph that spans the page, enabling bounds‑checking, addin... |
| [add-text-inside-graph](./add-text-inside-graph.cs) | Add Text Inside a Graph with Custom Font and Position | `Document`, `Page`, `Graph` | Demonstrates how to insert a Graph into a PDF page and place a formatted TextFragment inside it a... |
| [add-unfilled-arc-with-custom-stroke](./add-unfilled-arc-with-custom-stroke.cs) | Add Unfilled Arc with Custom Stroke to PDF | `Document`, `Page`, `Graph` | Demonstrates creating a PDF, adding a Graph container, drawing an unfilled arc, and customizing i... |
| [align-graph-to-left-margin-pdf](./align-graph-to-left-margin-pdf.cs) | Align Graph to Left Margin in PDF | `Document`, `Page`, `Graph` | Demonstrates creating a Graph, positioning it with a left offset and top margin, adding a shape, ... |
| [batch-insert-company-logo-graph-into-pdfs](./batch-insert-company-logo-graph-into-pdfs.cs) | Batch Insert Company Logo Graph into PDFs | `Document`, `Page`, `Graph` | Shows how to process all PDF files in a folder, load each with Aspose.Pdf, add a predefined Graph... |
| [build-reusable-graph-for-pdf-pages](./build-reusable-graph-for-pdf-pages.cs) | Create Reusable Graph with Shapes for PDF Pages | `Document`, `Page`, `Graph` | Demonstrates how to build a predefined Graph containing a rectangle, ellipse, and line, and add i... |
| ... | | | *and 42 more files* |

## Category Statistics
- Total examples: 72

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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

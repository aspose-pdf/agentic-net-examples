---
name: Working with Graphs
description: C# examples for Working with Graphs using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Working with Graphs

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Working with Graphs** category.
This folder contains standalone C# examples for Working with Graphs operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Working with Graphs**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (78/78 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (74/78 files) ← category-specific
- `using Aspose.Pdf.Text;` (9/78 files)
- `using Aspose.Pdf.Operators;` (4/78 files)
- `using Aspose.Pdf.Facades;` (2/78 files)
- `using Aspose.Pdf.Annotations;` (1/78 files)
- `using System;` (78/78 files)
- `using System.Runtime.InteropServices;` (56/78 files) ← category-specific
- `using System.IO;` (31/78 files)
- `using System.Collections.Generic;` (3/78 files)
- `using System.Drawing;` (3/78 files)
- `using System.Linq;` (1/78 files)
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
| [add-a-filled-arc-with-a-radial-gradient-fill-to-a-...](./add-a-filled-arc-with-a-radial-gradient-fill-to-a-graph-defining-start-and-sweep-angles.cs) | Add A Filled Arc With A Radial Gradient Fill To A Graph Defi... |  | Add A Filled Arc With A Radial Gradient Fill To A Graph Defining Start And Sweep Angles |
| [add-a-filled-circle-to-a-graph-specifying-radius-a...](./add-a-filled-circle-to-a-graph-specifying-radius-and-center-coordinates.cs) | Add A Filled Circle To A Graph Specifying Radius And Center ... |  | Add A Filled Circle To A Graph Specifying Radius And Center Coordinates |
| [add-a-filled-circle-to-a-graph-specifying-radius-a...](./add-a-filled-circle-to-a-graph-specifying-radius-and-center-coordinates__v2.cs) | Add A Filled Circle To A Graph Specifying Radius And Center ... |  | Add A Filled Circle To A Graph Specifying Radius And Center Coordinates__V2 |
| [add-a-filled-rectangle-to-a-graph-defining-border-...](./add-a-filled-rectangle-to-a-graph-defining-border-dash-pattern-and-width.cs) | Add A Filled Rectangle To A Graph Defining Border Dash Patte... | `Rectangle` | Add A Filled Rectangle To A Graph Defining Border Dash Pattern And Width |
| [add-a-filled-rectangle-to-a-graph-defining-border-...](./add-a-filled-rectangle-to-a-graph-defining-border-dash-pattern-and-width__v2.cs) | Add A Filled Rectangle To A Graph Defining Border Dash Patte... | `Rectangle` | Add A Filled Rectangle To A Graph Defining Border Dash Pattern And Width__V2 |
| [add-a-graph-to-a-page-s-paragraphs-collection-and-...](./add-a-graph-to-a-page-s-paragraphs-collection-and-align-it-to-the-page-center.cs) | Add A Graph To A Page S Paragraphs Collection And Align It T... | `Rectangle` | Add A Graph To A Page S Paragraphs Collection And Align It To The Page Center |
| [add-a-graph-to-a-page-s-paragraphs-collection-and-...](./add-a-graph-to-a-page-s-paragraphs-collection-and-align-it-to-the-page-center__v2.cs) | Add A Graph To A Page S Paragraphs Collection And Align It T... | `Rectangle` | Add A Graph To A Page S Paragraphs Collection And Align It To The Page Center__V2 |
| [add-a-rectangle-to-the-graph-using-absolute-coordi...](./add-a-rectangle-to-the-graph-using-absolute-coordinates-and-a-solid-red-fill.cs) | Add A Rectangle To The Graph Using Absolute Coordinates And ... | `Rectangle` | Add A Rectangle To The Graph Using Absolute Coordinates And A Solid Red Fill |
| [add-a-rectangle-with-a-2-point-border-thickness-an...](./add-a-rectangle-with-a-2-point-border-thickness-and-a-dashed-line-style-to-the-graph.cs) | Add A Rectangle With A 2 Point Border Thickness And A Dashed... | `Rectangle` | Add A Rectangle With A 2 Point Border Thickness And A Dashed Line Style To The Graph |
| [add-a-rectangle-with-a-radial-gradient-transitioni...](./add-a-rectangle-with-a-radial-gradient-transitioning-from-red-at-the-center-to-transparent-edges.cs) | Add A Rectangle With A Radial Gradient Transitioning From Re... | `Rectangle`, `Point`, `Color` | Add A Rectangle With A Radial Gradient Transitioning From Red At The Center To Transparent Edges |
| [add-a-rectangle-with-rounded-corners-by-specifying...](./add-a-rectangle-with-rounded-corners-by-specifying-corner-radius-and-apply-a-solid-fill.cs) | Add A Rectangle With Rounded Corners By Specifying Corner Ra... | `Rectangle` | Add A Rectangle With Rounded Corners By Specifying Corner Radius And Apply A Solid Fill |
| [add-a-regular-polygon-with-six-sides-to-a-graph-an...](./add-a-regular-polygon-with-six-sides-to-a-graph-and-set-its-border-color-and-thickness.cs) | Add A Regular Polygon With Six Sides To A Graph And Set Its ... | `Point`, `Path` | Add A Regular Polygon With Six Sides To A Graph And Set Its Border Color And Thickness |
| [add-an-ellipse-with-a-thick-border-and-semi-transp...](./add-an-ellipse-with-a-thick-border-and-semi-transparent-fill-then-place-a-centered-textfragment.cs) | Add An Ellipse With A Thick Border And Semi Transparent Fill... | `TextFragment`, `TextBuilder` | Add An Ellipse With A Thick Border And Semi Transparent Fill Then Place A Centered Textfragment |
| [add-an-unfilled-arc-to-a-graph-setting-its-line-wi...](./add-an-unfilled-arc-to-a-graph-setting-its-line-width-and-dash-style.cs) | Add An Unfilled Arc To A Graph Setting Its Line Width And Da... |  | Add An Unfilled Arc To A Graph Setting Its Line Width And Dash Style |
| [add-an-unfilled-arc-to-a-graph-setting-its-line-wi...](./add-an-unfilled-arc-to-a-graph-setting-its-line-width-and-dash-style__v2.cs) | Add An Unfilled Arc To A Graph Setting Its Line Width And Da... |  | Add An Unfilled Arc To A Graph Setting Its Line Width And Dash Style__V2 |
| [add-multiple-line-segments-to-a-graph-to-represent...](./add-multiple-line-segments-to-a-graph-to-represent-continuous-data-series-with-varying-colors.cs) | Add Multiple Line Segments To A Graph To Represent Continuou... |  | Add Multiple Line Segments To A Graph To Represent Continuous Data Series With Varying Colors |
| [add-multiple-line-segments-to-a-graph-to-represent...](./add-multiple-line-segments-to-a-graph-to-represent-continuous-data-series-with-varying-colors__v2.cs) | Add Multiple Line Segments To A Graph To Represent Continuou... |  | Add Multiple Line Segments To A Graph To Represent Continuous Data Series With Varying Colors__V2 |
| [add-multiple-rectangles-of-varying-sizes-to-a-grap...](./add-multiple-rectangles-of-varying-sizes-to-a-graph-and-verify-no-overlap-using-bounds-checking.cs) | Add Multiple Rectangles Of Varying Sizes To A Graph And Veri... | `Rectangle` | Add Multiple Rectangles Of Varying Sizes To A Graph And Verify No Overlap Using Bounds Checking |
| [apply-a-background-image-to-a-graph-and-ensure-sha...](./apply-a-background-image-to-a-graph-and-ensure-shapes-are-drawn-on-top-of-the-image.cs) | Apply A Background Image To A Graph And Ensure Shapes Are Dr... | `Image`, `Rectangle`, `Line` | Apply A Background Image To A Graph And Ensure Shapes Are Drawn On Top Of The Image |
| [apply-a-clipping-region-to-a-graph-so-that-shapes-...](./apply-a-clipping-region-to-a-graph-so-that-shapes-render-only-within-a-defined-rectangle.cs) | Apply A Clipping Region To A Graph So That Shapes Render Onl... | `Rectangle` | Apply A Clipping Region To A Graph So That Shapes Render Only Within A Defined Rectangle |
| [apply-a-rotation-transformation-to-a-graph-element...](./apply-a-rotation-transformation-to-a-graph-element-specifying-the-rotation-angle-in-degrees.cs) | Apply A Rotation Transformation To A Graph Element Specifyin... | `Rectangle` | Apply A Rotation Transformation To A Graph Element Specifying The Rotation Angle In Degrees |
| [apply-a-texture-brush-as-fill-for-an-ellipse-by-lo...](./apply-a-texture-brush-as-fill-for-an-ellipse-by-loading-an-image-resource-as-a-pattern.cs) | Apply A Texture Brush As Fill For An Ellipse By Loading An I... | `Rectangle`, `Ellipse` | Apply A Texture Brush As Fill For An Ellipse By Loading An Image Resource As A Pattern |
| [batch-process-a-folder-of-pdfs-inserting-a-predefi...](./batch-process-a-folder-of-pdfs-inserting-a-predefined-graph-with-a-company-logo-rectangle-into-each.cs) | Batch Process A Folder Of Pdfs Inserting A Predefined Graph ... | `Rectangle` | Batch Process A Folder Of Pdfs Inserting A Predefined Graph With A Company Logo Rectangle Into Each |
| [combine-multiple-graphs-on-a-single-page-by-positi...](./combine-multiple-graphs-on-a-single-page-by-positioning-each-at-different-coordinates-to-form-a-collage.cs) | Combine Multiple Graphs On A Single Page By Positioning Each... | `Rectangle` | Combine Multiple Graphs On A Single Page By Positioning Each At Different Coordinates To Form A C... |
| [configure-the-graph-s-line-width-and-dash-style-be...](./configure-the-graph-s-line-width-and-dash-style-before-adding-rectangles-for-custom-borders.cs) | Configure The Graph S Line Width And Dash Style Before Addin... | `Rectangle` | Configure The Graph S Line Width And Dash Style Before Adding Rectangles For Custom Borders |
| [create-a-bezier-curve-in-a-graph-using-four-contro...](./create-a-bezier-curve-in-a-graph-using-four-control-points-and-set-its-stroke-color.cs) | Create A Bezier Curve In A Graph Using Four Control Points A... |  | Create A Bezier Curve In A Graph Using Four Control Points And Set Its Stroke Color |
| [create-a-bezier-curve-in-a-graph-using-four-contro...](./create-a-bezier-curve-in-a-graph-using-four-control-points-and-set-its-stroke-color__v2.cs) | Create A Bezier Curve In A Graph Using Four Control Points A... | `TextFragment` | Create A Bezier Curve In A Graph Using Four Control Points And Set Its Stroke Color__V2 |
| [create-a-custom-star-shape-in-a-graph-with-a-speci...](./create-a-custom-star-shape-in-a-graph-with-a-specified-number-of-points-and-fill-color.cs) | Create A Custom Star Shape In A Graph With A Specified Numbe... |  | Create A Custom Star Shape In A Graph With A Specified Number Of Points And Fill Color |
| [create-a-graph-containing-rectangles-and-ellipses-...](./create-a-graph-containing-rectangles-and-ellipses-with-distinct-fill-types-then-add-it-to-a-page.cs) | Create A Graph Containing Rectangles And Ellipses With Disti... | `Rectangle` | Create A Graph Containing Rectangles And Ellipses With Distinct Fill Types Then Add It To A Page |
| [create-a-multi-page-pdf-where-each-page-contains-a...](./create-a-multi-page-pdf-where-each-page-contains-a-graph-sized-to-that-page-s-dimensions.cs) | Create A Multi Page Pdf Where Each Page Contains A Graph Siz... | `TextFragment` | Create A Multi Page Pdf Where Each Page Contains A Graph Sized To That Page S Dimensions |
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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for Working with Graphs patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-03 | Run: `20260403_183447_12b012`
<!-- AUTOGENERATED:END -->

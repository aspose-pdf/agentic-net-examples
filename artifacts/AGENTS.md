---
name: artifacts
description: C# examples for artifacts using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - artifacts

> **Artifacts** in PDF using C# / .NET -- **19** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **artifacts** category.
This folder contains standalone C# examples for artifacts operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **artifacts**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (19/19 files) ← category-specific
- `using Aspose.Pdf.Text;` (5/19 files)
- `using Aspose.Pdf.Annotations;` (1/19 files)
- `using Aspose.Pdf.Facades;` (1/19 files)
- `using System;` (19/19 files)
- `using System.IO;` (19/19 files)
- `using System.Linq;` (2/19 files)
- `using System.Collections.Generic;` (1/19 files)
- `using System.Globalization;` (1/19 files)
- `using System.Text.Json;` (1/19 files)

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
| [add-background-image-artifact](./add-background-image-artifact.cs) | Add Background Image Artifact to PDF Page | `Document`, `Page`, `BackgroundArtifact` | Shows how to load a PDF, create a BackgroundArtifact from an image stream, add it to the first pa... |
| [add-bates-numbering-to-pdf-pages__v3](./add-bates-numbering-to-pdf-pages__v3.cs) | Add Bates Numbering to PDF Pages | `Document`, `TextStamp`, `FontRepository` | Shows how to load a PDF, generate sequential Bates numbers, apply them as TextStamp artifacts on ... |
| [add-bates-numbering-to-pdf](./add-bates-numbering-to-pdf.cs) | Add Bates Numbering to PDF Pages | `Document`, `Save`, `TextFragment` | Demonstrates loading a PDF, applying a batch of custom Bates numbering artifacts to each page usi... |
| [add-footer-pagination-artifact](./add-footer-pagination-artifact.cs) | Add Footer Pagination Artifact to PDF Page | `Document`, `Page`, `FooterArtifact` | Loads a PDF, creates a pagination footer artifact containing the page number, adds it to page six... |
| [add-multiple-watermark-artifacts-to-pdf-page](./add-multiple-watermark-artifacts-to-pdf-page.cs) | Add Multiple Watermark Artifacts to a PDF Page | `Document`, `Page`, `ArtifactCollection` | The example loads a PDF, accesses page five, adds two WatermarkArtifact objects to the page's Art... |
| [add-rotated-watermark-artifact-to-pdf-page](./add-rotated-watermark-artifact-to-pdf-page.cs) | Add Rotated Watermark Artifact to PDF Page | `Document`, `Page`, `WatermarkArtifact` | Demonstrates loading a PDF, creating a WatermarkArtifact with a 45° rotation, adding it to the th... |
| [add-semi-transparent-text-watermark__v4](./add-semi-transparent-text-watermark__v4.cs) | Add Semi-Transparent Text Watermark to All PDF Pages | `Document`, `Page`, `WatermarkArtifact` | Shows how to load a PDF, create a WatermarkArtifact with styled, semi‑transparent text, attach it... |
| [count-background-artifacts-per-pdf-page](./count-background-artifacts-per-pdf-page.cs) | Count Background Artifacts per PDF Page | `Document`, `Page`, `Artifacts` | Demonstrates loading a PDF with Aspose.Pdf, iterating through its pages, counting BackgroundArtif... |
| [count-pdf-pagination-artifacts](./count-pdf-pagination-artifacts.cs) | Count PDF Pagination Artifacts and Save to JSON | `Document`, `Pages`, `Count` | Loads a PDF using Aspose.Pdf, counts the total number of pages (pagination artifacts), and writes... |
| [count-watermark-artifacts-per-page](./count-watermark-artifacts-per-page.cs) | Count WatermarkArtifacts per PDF Page and Generate Report | `Document`, `Page`, `WatermarkArtifact` | Loads a PDF, iterates through each page, counts the WatermarkArtifact objects using LINQ, and wri... |
| [export-page2-artifact-rectangles-to-csv](./export-page2-artifact-rectangles-to-csv.cs) | Export Page 2 Artifact Rectangles to CSV | `Document`, `Page`, `Artifact` | Loads a PDF, extracts the rectangle bounds of all artifacts on the second page, and writes them t... |
| [export-page7-background-artifact-images-to-csv](./export-page7-background-artifact-images-to-csv.cs) | Export Background Artifact Image Names from Page 7 to CSV | `Document`, `Page`, `BackgroundArtifact` | Loads a PDF, extracts BackgroundArtifact objects from the seventh page, and writes their internal... |
| [extract-artifact-subtypes-page9-csv](./extract-artifact-subtypes-page9-csv.cs) | Extract Artifact Subtypes from PDF Page 9 to CSV | `Document`, `Pages`, `Page` | Loads a PDF, accesses the ninth page, iterates through its artifacts, and writes each artifact's ... |
| [extract-background-images-from-pdf-page](./extract-background-images-from-pdf-page.cs) | Extract Background Images from PDF Page | `Document`, `Page`, `Artifact` | Loads a PDF, accesses page three, iterates its artifacts, extracts any BackgroundArtifact images,... |
| [extract-watermark-texts-to-csv](./extract-watermark-texts-to-csv.cs) | Extract Watermark Texts from PDF Page to CSV | `Document`, `Page`, `Artifact` | Loads a PDF, reads all WatermarkArtifact texts from the first page, and writes them into a CSV file. |
| [list-watermark-artifact-rectangles-page-2](./list-watermark-artifact-rectangles-page-2.cs) | List Watermark Artifact Rectangles on PDF Page 2 | `Document`, `Page`, `Artifacts` | The example loads a PDF, verifies that a second page exists, enumerates WatermarkArtifact objects... |
| [remove-bates-numbering-artifacts](./remove-bates-numbering-artifacts.cs) | Remove Bates Numbering Artifacts from PDF | `Document`, `Page`, `ArtifactCollection` | Demonstrates how to load a PDF with Aspose.Pdf, iterate through each page's artifact collection, ... |
| [replace-page-background-image](./replace-page-background-image.cs) | Replace Background Image of a PDF Page | `Document`, `Page`, `Artifact` | Shows how to load a PDF with Aspose.Pdf, locate or create a BackgroundArtifact on page two, repla... |
| [set-watermark-opacity-first-page](./set-watermark-opacity-first-page.cs) | Set Watermark Opacity on First PDF Page | `Document`, `Page`, `Artifact` | Loads a PDF, iterates over WatermarkArtifact objects on the first page, sets their opacity to 0.5... |

## Category Statistics
- Total examples: 19

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for artifacts patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_015728_da9c7c`
<!-- AUTOGENERATED:END -->

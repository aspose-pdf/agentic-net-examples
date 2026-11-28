---
name: facades-layouts
description: C# examples for facades-layouts using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-layouts

> **Facades layouts** in PDF using C# / .NET -- **45** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-layouts** category.
This folder contains standalone C# examples for facades-layouts operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-layouts**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (45/45 files) ← category-specific
- `using Aspose.Pdf;` (15/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (45/45 files)

## Common Code Pattern

Most files in this category use `PdfFileEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfFileEditor tool = new PdfFileEditor();
tool.BindPdf("input.pdf");
// ... PdfFileEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [create-1x5-nup-a5](./create-1x5-nup-a5.cs) | Create 1×5 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `Document` | Shows how to generate a 1×5 N‑up layout from a PDF and resize each resulting page to A5 using Asp... |
| [create-1x5-nup-pdf](./create-1x5-nup-pdf.cs) | Create 1×5 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Shows how to generate a 1‑by‑5 N‑up layout PDF from a source file using Aspose.Pdf.Facades.PdfFil... |
| [create-1x7-nup-a5-pdf](./create-1x7-nup-a5-pdf.cs) | Create 1×7 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp` | Shows how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 1×7 N‑up PDF in A5 size from an e... |
| [create-1x7-nup-pdf](./create-1x7-nup-pdf.cs) | Create 1×7 N-up PDF using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Demonstrates loading a PDF, applying a 1‑by‑7 N‑up layout with PdfFileEditor, and saving the resu... |
| [create-2x2-nup-a5-pdf](./create-2x2-nup-a5-pdf.cs) | Create 2×2 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `Document` | Shows how to generate a 2‑by‑2 N‑up layout from a source PDF and then set each resulting page to ... |
| [create-2x2-nup-pdf](./create-2x2-nup-pdf.cs) | Create 2×2 N‑up PDF Layout | `PdfFileEditor`, `MakeNUp` | Shows how to generate a PDF with a 2‑by‑2 N‑up layout (2 rows, 2 columns) using Aspose.Pdf.Facade... |
| [create-2x2-nup-pdf__v2](./create-2x2-nup-pdf__v2.cs) | Create 2×2 N‑up PDF using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Loads a PDF, applies a 2×2 N‑up layout with Aspose.Pdf.Facades.PdfFileEditor, and saves the combi... |
| [create-2x3-nup-a5-pdf](./create-2x3-nup-a5-pdf.cs) | Create 2×3 N‑up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to generate an N‑up PDF (2 columns × 3 r... |
| [create-2x3-nup-pdf-layout](./create-2x3-nup-pdf-layout.cs) | Create 2x3 N-up PDF Layout | `PdfFileEditor`, `MakeNUp` | Demonstrates how to generate a PDF where each page contains two rows and three columns of the ori... |
| [create-2x3-nup-pdf](./create-2x3-nup-pdf.cs) | Create 2×3 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Shows how to load a PDF, generate a 2×3 N‑up layout using Aspose.Pdf.Facades.PdfFileEditor, and s... |
| [create-2x4-nup-a5-pdf](./create-2x4-nup-a5-pdf.cs) | Create 2×4 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `Document` | Demonstrates how to generate a 2×4 N‑up layout from an existing PDF and resize the resulting page... |
| [create-2x4-nup-pdf](./create-2x4-nup-pdf.cs) | Create 2x4 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Shows how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 2‑by‑4 N‑up layout from an existi... |
| [create-2x6-nup-a5-pdf](./create-2x6-nup-a5-pdf.cs) | Create 2×6 N‑up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp` | Demonstrates how to generate a 2 × 6 N‑up layout PDF on A5 pages using the Aspose.Pdf.Facades Pdf... |
| [create-2x6-nup-pdf](./create-2x6-nup-pdf.cs) | Create 2×6 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Shows how to load a PDF and use Aspose.Pdf.Facades.PdfFileEditor.MakeNUp to produce a 2×6 N‑up la... |
| [create-3x2-nup-a5-pdf](./create-3x2-nup-a5-pdf.cs) | Create 3×2 N‑up PDF on A5 Page | `PdfFileEditor`, `MakeNUp` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to generate a 3×2 N‑up layout on an A5‑sized ... |
| [create-3x2-nup-pdf](./create-3x2-nup-pdf.cs) | Create 3x2 N‑up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp`, `PageSize` | Shows how to load a PDF, apply a 3 rows × 2 columns N‑up layout using A4 page size with Aspose.Pd... |
| [create-3x3-nup-a5-pdf](./create-3x3-nup-a5-pdf.cs) | Create 3×3 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `NUpOptions` | Demonstrates how to generate a 3×3 N‑up layout PDF on an A5 page using Aspose.Pdf's PdfFileEditor... |
| [create-3x3-nup-pdf-layout](./create-3x3-nup-pdf-layout.cs) | Create 3x3 N-up PDF Layout | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to combine nine pages into a single page... |
| [create-3x3-nup-pdf](./create-3x3-nup-pdf.cs) | Create 3×3 N-up PDF using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Shows how to load a PDF and use Aspose.Pdf.Facades.PdfFileEditor to generate a 3×3 N‑up layout sa... |
| [create-4x2-nup-a5-pdf](./create-4x2-nup-a5-pdf.cs) | Create 4×2 N-up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `Document` | Shows how to generate a 4×2 N‑up layout from a source PDF and then set each resulting page to A5 ... |
| [create-4x2-nup-pdf](./create-4x2-nup-pdf.cs) | Create 4×2 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Loads a source PDF, generates a 4‑row by 2‑column N‑up layout using Aspose.Pdf.Facades.PdfFileEdi... |
| [create-4x4-nup-a5-pdf](./create-4x4-nup-a5-pdf.cs) | Create 4×4 N‑up PDF with A5 Page Size | `PdfFileEditor`, `MakeNUp`, `Document` | Demonstrates how to generate a 4×4 N‑up layout from a source PDF and resize the resulting pages t... |
| [create-4x4-nup-pdf-layout](./create-4x4-nup-pdf-layout.cs) | Create 4x4 N-up PDF Layout | `PdfFileEditor`, `MakeNUp` | Demonstrates using Aspose.Pdf.Facades.PdfFileEditor to generate a PDF with a 4‑by‑4 N‑up layout (... |
| [create-4x4-nup-pdf](./create-4x4-nup-pdf.cs) | Create 4x4 N‑up PDF using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Loads a PDF, arranges its pages in a 4×4 N‑up layout with PdfFileEditor, and saves the result as ... |
| [create-5x1-a5-nup-pdf](./create-5x1-a5-nup-pdf.cs) | Create 5×1 A5 N-up PDF with PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Loads a PDF, applies a 5×1 N-up layout using A5 page size via PdfFileEditor, and saves the result... |
| [create-5x1-nup-pdf](./create-5x1-nup-pdf.cs) | Create 5×1 N-up PDF using PdfFileEditor | `PdfFileEditor`, `MakeNUp` | Loads a PDF file, applies a 5‑by‑1 N‑up layout with Aspose.Pdf.Facades.PdfFileEditor, and saves t... |
| [create-5x5-nup-a5-pdf](./create-5x5-nup-a5-pdf.cs) | Create 5×5 N-up PDF on A5 Pages | `PdfFileEditor`, `MakeNUp` | Shows how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 5‑by‑5 N‑up layout on A5‑sized pa... |
| [create-5x5-nup-pdf-layout](./create-5x5-nup-pdf-layout.cs) | Create 5x5 N-up PDF Layout | `PdfFileEditor`, `MakeNUp` | Demonstrates how to generate a 5‑by‑5 N‑up PDF using Aspose.Pdf.Facades.PdfFileEditor to place 25... |
| [create-5x5-nup-pdf-layout__v2](./create-5x5-nup-pdf-layout__v2.cs) | Create 5x5 N-up PDF Layout | `PdfFileEditor`, `MakeNUp` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 5×5 N‑up layout from a sin... |
| [create-6x2-nup-pdf-a5](./create-6x2-nup-pdf-a5.cs) | Create 6x2 N-up PDF on A5 Pages | `PdfFileEditor`, `MakeNUp`, `PageSize` | Shows how to use Aspose.Pdf.Facades.PdfFileEditor to generate a 6‑by‑2 N‑up layout on A5 sized pa... |
| ... | | | *and 15 more files* |

## Category Statistics
- Total examples: 45

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-layouts patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-28 | Run: `20261128_033253_0c1eab`
<!-- AUTOGENERATED:END -->

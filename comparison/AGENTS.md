---
name: comparison
description: C# examples for comparison using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - comparison

> **Comparison** in PDF using C# / .NET -- **32** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **comparison** category.
This folder contains standalone C# examples for comparison operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **comparison**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (32/32 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (31/32 files) ← category-specific
- `using Aspose.Pdf.Devices;` (7/32 files)
- `using Aspose.Pdf.Text;` (2/32 files)
- `using System;` (32/32 files)
- `using System.IO;` (32/32 files)
- `using System.Drawing;` (6/32 files)
- `using System.Collections.Generic;` (5/32 files)
- `using System.Drawing.Imaging;` (5/32 files)
- `using System.Linq;` (2/32 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [compare-pdf-pages-save-difference-png](./compare-pdf-pages-save-difference-png.cs) | Compare PDF Pages and Save Difference as PNG | `Document`, `Page`, `GraphicalPdfComparer` | Loads two PDF files, compares selected pages using Aspose.Pdf's graphical comparer, and saves the... |
| [compare-pdf-pages-to-png-diff](./compare-pdf-pages-to-png-diff.cs) | Compare PDF Pages and Generate PNG Diff with Custom Threshol... | `Document`, `GraphicalPdfComparer`, `ComparePagesToImage` | Loads two PDF files, sets a 5% pixel change threshold and a custom red highlight color, then comp... |
| [compare-pdf-text-to-markdown-excluding-tables](./compare-pdf-text-to-markdown-excluding-tables.cs) | Compare PDF Text and Export Markdown Diff (Exclude Tables) | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files, performs a flat text comparison while ignoring tables, and writes the differ... |
| [compare-pdfs-excluding-tables-json-diff](./compare-pdfs-excluding-tables-json-diff.cs) | Compare PDFs Excluding Tables and Save Diff as JSON | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to load two PDF documents, set ComparisonOptions to exclude tables, run a page‑by‑page ... |
| [compare-pdfs-export-diff-statistics-json](./compare-pdfs-export-diff-statistics-json.cs) | Compare Two PDFs and Export Difference Statistics as JSON | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to load two PDF files, perform side‑by‑side visual comparison and text‑based com... |
| [compare-pdfs-ignore-spaces](./compare-pdfs-ignore-spaces.cs) | Compare PDFs Ignoring Spaces with Side‑by‑Side Comparison | `Document`, `Page`, `TextFragment` | Demonstrates creating two PDFs, setting ComparisonMode to IgnoreSpaces, performing a side‑by‑side... |
| [compare-pdfs-insert-first-html-report](./compare-pdfs-insert-first-html-report.cs) | Compare PDFs with Insert-First Priority and Generate HTML Re... | `Document`, `ComparisonOptions`, `EditOperationsOrder` | Loads two PDF files, sets the comparison to prioritize insert operations, performs a page‑by‑page... |
| [compare-pdfs-markdown-diff](./compare-pdfs-markdown-diff.cs) | Compare Two PDFs and Generate a Markdown Diff | `Document`, `TextPdfComparer`, `ComparisonOptions` | Loads two PDF files, performs a flat‑text comparison using Aspose.Pdf's TextPdfComparer, and writ... |
| [compare-pdfs-page-by-page-exclude-areas-json-diff](./compare-pdfs-page-by-page-exclude-areas-json-diff.cs) | Compare PDFs Page‑by‑Page with Excluded Areas and Save JSON ... | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Loads two PDF files, defines exclusion rectangles for the first document, compares them page by p... |
| [compare-pdfs-page-by-page-html-diff](./compare-pdfs-page-by-page-html-diff.cs) | Compare PDFs Page‑by‑Page and Generate HTML Diff | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Loads two PDF documents, compares their text content page by page within a specified extraction a... |
| [compare-pdfs-page-by-page-json-diff](./compare-pdfs-page-by-page-json-diff.cs) | Compare Two PDFs Page‑by‑Page and Generate JSON Diff | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Loads two PDF files, performs a text‑based page‑by‑page comparison using Aspose.Pdf, and writes t... |
| [compare-pdfs-side-by-side-ignore-spaces](./compare-pdfs-side-by-side-ignore-spaces.cs) | Compare Two PDFs Side-by-Side Ignoring Spaces | `Document`, `SideBySideComparisonOptions`, `ComparisonMode` | Demonstrates loading two PDF documents, configuring side‑by‑side comparison options to show chang... |
| [compare-pdfs-text-extraction-area-markdown-diff](./compare-pdfs-text-extraction-area-markdown-diff.cs) | Compare PDFs Text Within a Specified Area and Generate Markd... | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | The example loads two PDF files, defines a rectangular extraction area for text comparison, compa... |
| [compare-two-pdfs-side-by-side](./compare-two-pdfs-side-by-side.cs) | Compare Two PDFs Side‑by‑Side and Save Result | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Demonstrates loading two PDF files, performing a side‑by‑side comparison using Aspose.Pdf's defau... |
| [generate-pdf-comparison-report](./generate-pdf-comparison-report.cs) | Generate PDF Comparison Report with Aspose.Pdf | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Loads two PDF files, compares them using GraphicalPdfComparer, and saves a visual comparison repo... |
| [generate-png-diff-between-two-pdfs](./generate-png-diff-between-two-pdfs.cs) | Generate PNG Diff Image Between Two PDFs | `Document`, `GraphicalPdfComparer`, `Color` | Loads two PDF files, configures a GraphicalPdfComparer to highlight differences in green, and cre... |
| [generate-png-diff-between-two-pdfs__v2](./generate-png-diff-between-two-pdfs__v2.cs) | Generate PNG Diff Image Between Two PDFs at 150 DPI | `Document`, `GraphicalPdfComparer`, `Resolution` | Loads two PDF files, configures GraphicalPdfComparer to 150 DPI, creates a pixel-difference image... |
| [generate-red-highlight-png-diff-first-pages](./generate-red-highlight-png-diff-first-pages.cs) | Generate Red-Highlight PNG Diff of First Pages of Two PDFs | `Document`, `GraphicalPdfComparer`, `Resolution` | Loads two PDF files, compares their first pages using Aspose.Pdf's GraphicalPdfComparer, and save... |
| [generate-visual-difference-image-between-two-pdfs](./generate-visual-difference-image-between-two-pdfs.cs) | Generate Visual Difference Image Between Two PDFs | `Document`, `GraphicalPdfComparer`, `ImagesDifference` | Shows how to compare the first pages of two PDF documents with Aspose.Pdf's GraphicalPdfComparer,... |
| [pdf-diff-300dpi-png](./pdf-diff-300dpi-png.cs) | Generate PNG Diff Images for PDFs at 300 DPI | `Document`, `GraphicalPdfComparer`, `GetDifference` | Loads two PDF files, configures a GraphicalPdfComparer with a 300 DPI resolution, and creates per... |
| [pdf-flat-text-comparison-delete-first-markdown](./pdf-flat-text-comparison-delete-first-markdown.cs) | Flat‑Text PDF Comparison with Delete‑First Priority and Mark... | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | The example loads two PDF files, configures the comparison to prioritize delete operations, perfo... |
| [pdf-visual-diff-blue-png](./pdf-visual-diff-blue-png.cs) | Create a Blue‑Highlighted PDF Visual Diff and Save as PNG | `Document`, `GraphicalPdfComparer`, `ImagesDifference` | Loads two PDF files, uses Aspose.Pdf's GraphicalPdfComparer to highlight differences in blue, ren... |
| [render-pdf-page-to-png](./render-pdf-page-to-png.cs) | Render PDF Page to PNG Image | `Document`, `PngDevice`, `Resolution` | Loads a PDF document, renders a specified page to a PNG image using Aspose.Pdf.Devices, and saves... |
| [side-by-side-pdf-comparison-additional-change-mark...](./side-by-side-pdf-comparison-additional-change-marks.cs) | Side-by-Side PDF Comparison with Additional Change Marks | `Document`, `SideBySidePdfComparer`, `Compare` | Shows how to load two PDFs, enable additional change marks in side‑by‑side comparison options, an... |
| [side-by-side-pdf-comparison-exclude-areas](./side-by-side-pdf-comparison-exclude-areas.cs) | Side-by-side PDF Comparison with Excluded Areas | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Shows how to load two PDFs, define exclusion rectangles for the second document, run a side‑by‑si... |
| [side-by-side-pdf-comparison-excluded-areas](./side-by-side-pdf-comparison-excluded-areas.cs) | Side-by-side PDF Comparison with Excluded Areas | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates loading two PDFs, defining exclusion rectangles, and generating a side‑by‑side compa... |
| [side-by-side-pdf-comparison-no-additional-marks](./side-by-side-pdf-comparison-no-additional-marks.cs) | Side‑by‑Side PDF Comparison without Additional Change Marks | `Document`, `SideBySideComparisonOptions`, `Compare` | Loads two PDF files, disables the visual markers for additional changes, compares the documents s... |
| [side-by-side-pdf-comparison-no-additional-marks__v...](./side-by-side-pdf-comparison-no-additional-marks__v2.cs) | Side‑by‑Side PDF Comparison without Additional Change Marks | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | The example loads two PDF files, disables additional change marks, performs a side‑by‑side compar... |
| [side-by-side-pdf-comparison](./side-by-side-pdf-comparison.cs) | Side‑by‑Side PDF Comparison with Change Marks | `Document`, `SideBySideComparisonOptions`, `Compare` | Loads two PDF files, enables additional change marks, performs a side‑by‑side visual comparison, ... |
| [side-by-side-pdf-comparison__v2](./side-by-side-pdf-comparison__v2.cs) | Side-by-Side PDF Comparison | `Document`, `SideBySideComparisonOptions`, `Compare` | Loads two PDF files, compares them page‑by‑page using Aspose.Pdf's SideBySidePdfComparer, and sav... |
| ... | | | *and 2 more files* |

## Category Statistics
- Total examples: 32

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for comparison patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-24 | Run: `20261124_214509_2134bd`
<!-- AUTOGENERATED:END -->

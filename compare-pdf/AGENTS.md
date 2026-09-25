---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

> **Compare PDF** in PDF using C# / .NET -- **27** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **compare-pdf** category.
This folder contains standalone C# examples for compare-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **compare-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (27/27 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (17/27 files) ← category-specific
- `using Aspose.Pdf.Text;` (9/27 files)
- `using Aspose.Pdf.Annotations;` (2/27 files)
- `using Aspose.Pdf.Devices;` (1/27 files)
- `using System;` (27/27 files)
- `using System.IO;` (27/27 files)
- `using System.Collections.Generic;` (11/27 files)
- `using System.Linq;` (3/27 files)
- `using System.Threading.Tasks;` (2/27 files)
- `using System.Collections;` (1/27 files)
- `using System.Collections.Concurrent;` (1/27 files)
- `using System.IO.Compression;` (1/27 files)
- `using System.Text.Json;` (1/27 files)

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
| [batch-pdf-comparison-parallel](./batch-pdf-comparison-parallel.cs) | Batch PDF Comparison with Parallel Processing | `Document`, `TextAbsorber`, `Accept` | Shows how to compare a reference PDF against multiple PDFs in a folder by checking page count and... |
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs with Text and Visual Differences | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Demonstrates opening password‑protected PDF files and comparing them both textually and side‑by‑s... |
| [compare-pdf-documents-page-by-page-diff](./compare-pdf-documents-page-by-page-diff.cs) | Compare PDFs Page by Page and Generate a Diff PDF | `Document`, `Page`, `TextAbsorber` | Shows how to compare two PDF files page by page by extracting their text and creating a new PDF t... |
| [compare-pdf-text-ignoring-compression](./compare-pdf-text-ignoring-compression.cs) | Compare PDF Text Content with Different Compression Settings | `Document`, `TextAbsorber`, `TextExtractionOptions` | Demonstrates extracting pure text from two PDFs using Aspose.Pdf and comparing the strings to ver... |
| [compare-pdfs-detect-font-differences](./compare-pdfs-detect-font-differences.cs) | Compare Embedded Fonts Between PDFs | `Document`, `Page`, `Font` | Loads two PDF files, iterates through each page, extracts font information, and reports differenc... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes Side‑by‑Side | `Document`, `SideBySideComparisonOptions`, `Compare` | Demonstrates how to perform a side‑by‑side visual comparison of two PDFs that have different page... |
| [compare-pdfs-generate-json-diff-report](./compare-pdfs-generate-json-diff-report.cs) | Generate JSON Diff Report for PDF Comparison | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF files with Aspose.Pdf's TextPdfComparer, collect DiffOperation objec... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare Two PDFs and Log Differences by Page | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Demonstrates how to compare two PDF documents page‑by‑page using Aspose.Pdf and write each differ... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Unicode PDF Text Comparison | `Document`, `TextAbsorber`, `TextExtractionOptions` | Extracts Unicode text from two PDF files using Aspose.Pdf and compares the strings to detect the ... |
| [compare-pdfs-with-form-fields](./compare-pdfs-with-form-fields.cs) | Compare PDFs with Form Field Values Using Side‑by‑Side Compa... | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Loads two PDF documents and performs a visual side‑by‑side comparison that includes form field va... |
| [compare-selected-pdf-pages-side-by-side](./compare-selected-pdf-pages-side-by-side.cs) | Compare Selected PDF Pages with Aspose.Pdf | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates how to compare specific pages of two PDF documents by passing a page list to the Asp... |
| [compare-specific-page-range-pdf](./compare-specific-page-range-pdf.cs) | Compare Selected Page Range of Two PDFs | `Document`, `ComparePages`, `ComparisonOptions` | Demonstrates how to load two PDF documents, define a start and end page, and compare only that pa... |
| [compare-two-pdfs-aspdf-comparison](./compare-two-pdfs-aspdf-comparison.cs) | Case‑Insensitive PDF Text Comparison | `Document`, `TextAbsorber`, `Accept` | Demonstrates extracting text from two PDF documents page‑by‑page with Aspose.Pdf and comparing th... |
| [compare-two-pdfs-visual-diff](./compare-two-pdfs-visual-diff.cs) | Side-by-Side PDF Comparison with Aspose.Pdf | `Document`, `SideBySidePdfComparer`, `Compare` | Demonstrates how to compare two PDF files visually using Aspose.Pdf's side‑by‑side comparison fea... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Shows how to gather annotation bounding rectangles from two PDFs and set them as excluded areas i... |
| [exclude-areas-from-pdf-comparison](./exclude-areas-from-pdf-comparison.cs) | Exclude Areas in Side‑by‑Side PDF Comparison | `Document`, `SideBySideComparisonOptions`, `Compare` | Demonstrates how to exclude specific rectangular regions from both PDFs using the ExcludedAreasFi... |
| [exclude-footer-areas-in-pdf-comparison](./exclude-footer-areas-in-pdf-comparison.cs) | Compare PDFs While Excluding Footer Regions | `Document`, `Page`, `Rectangle` | Demonstrates how to define a rectangular footer area and add it to the ExcludeAreas collections o... |
| [generate-diff-pdf-by-comparing-two-pdfs](./generate-diff-pdf-by-comparing-two-pdfs.cs) | Create Diff PDF with Text Stamp Using CompareDocumentsToPdf | `Document`, `TextStamp`, `FontRepository` | Demonstrates how to generate a diff PDF by copying the modified document, adding a text stamp ann... |
| [generate-diff-pdf-highlighted-text-changes](./generate-diff-pdf-highlighted-text-changes.cs) | Create Side‑by‑Side PDF Diff and Verify Highlight Colors | `Document`, `Compare`, `SideBySideComparisonOptions` | The example compares two PDF files side‑by‑side, generates a diff PDF with default highlight colo... |
| [generate-pdf-diff-images-zip](./generate-pdf-diff-images-zip.cs) | Create PDF Comparison Diff Images and Package into ZIP | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDFs side‑by‑side using Aspose.Pdf, convert each comparison page ... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Create Visual PDF Diff with GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to generate a visual difference PDF by loading two documents and using Aspose.Pd... |
| [get-image-differences-between-pdf-pages](./get-image-differences-between-pdf-pages.cs) | Compare Images Between Two PDF Documents | `Document`, `Page`, `Images` | Loads two PDF files, iterates through their pages, extracts embedded images, and reports differen... |
| [in-memory-pdf-comparison-diff](./in-memory-pdf-comparison-diff.cs) | In Memory Pdf Comparison Diff |  | In Memory Pdf Comparison Diff |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi‑threaded PDF Comparison with Aspose.Pdf | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to compare two PDF files page‑by‑page using Aspose.Pdf, extracting text with Tex... |
| [preserve-original-metadata-in-diff-pdf](./preserve-original-metadata-in-diff-pdf.cs) | Preserve PDF Metadata After Side‑by‑Side Comparison | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDFs using Aspose.Pdf's side‑by‑side comparer, generate a diff PD... |
| [replace-text-using-diffoperation](./replace-text-using-diffoperation.cs) | Replace Modified PDF Text with Original Content | `Document`, `Page`, `TextFragmentAbsorber` | Compares two PDFs page‑by‑page, extracts their text, and replaces any differing page content in t... |
| [set-custom-image-similarity-tolerance-pdf-comparis...](./set-custom-image-similarity-tolerance-pdf-comparison.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to configure a custom image similarity tolerance using GraphicalPdfComparer when... |

## Category Statistics
- Total examples: 27

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Comparison.ComparisonOptions`
- `Aspose.Pdf.Comparison.ComparisonOptions.EditOperationsOrder`
- `Aspose.Pdf.Comparison.ComparisonOptions.ExcludeAreas1`
- `Aspose.Pdf.Comparison.ComparisonOptions.ExcludeAreas2`
- `Aspose.Pdf.Comparison.ComparisonOptions.ExcludeTables`
- `Aspose.Pdf.Comparison.ComparisonOptions.ExtractionArea`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.Color`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.CompareDocumentsToImages`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.CompareDocumentsToPdf`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.ComparePagesToImage`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.ComparePagesToPdf`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.GetDifference`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.Resolution`
- `Aspose.Pdf.Comparison.GraphicalPdfComparer.Threshold`

### Rules
- Create HtmlDiffOutputGenerator with parameterless constructor: new HtmlDiffOutputGenerator().
- Create HtmlDiffOutputGenerator with: new HtmlDiffOutputGenerator(OutputTextStyle textStyle).
- Configure HtmlDiffOutputGenerator by setting properties: EqualStyle, InsertStyle, DeleteStyle, StrikethroughDeleted.
- Create ComparisonOptions with parameterless constructor: new ComparisonOptions().
- Configure ComparisonOptions by setting properties: ExtractionArea, ExcludeTables, ExcludeAreas1, ExcludeAreas2, EditOperationsOrder.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for compare-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-24 | Run: `20260924_235708_c79043`
<!-- AUTOGENERATED:END -->

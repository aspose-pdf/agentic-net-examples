---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

> **Compare PDF** in PDF using C# / .NET -- **29** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **compare-pdf** category.
This folder contains standalone C# examples for compare-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **compare-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (29/29 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (17/29 files) ← category-specific
- `using Aspose.Pdf.Text;` (12/29 files)
- `using Aspose.Pdf.Annotations;` (2/29 files)
- `using Aspose.Pdf.Devices;` (1/29 files)
- `using Aspose.Pdf.Forms;` (1/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (29/29 files)
- `using System.Collections.Generic;` (8/29 files)
- `using System.Linq;` (4/29 files)
- `using System.Threading.Tasks;` (3/29 files)
- `using System.Collections.Concurrent;` (2/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.IO.Compression;` (1/29 files)
- `using System.Reflection;` (1/29 files)
- `using System.Text.Json;` (1/29 files)

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
| [batch-pdf-comparison-parallel](./batch-pdf-comparison-parallel.cs) | Batch PDF Comparison with Parallel Processing | `Document`, `TextAbsorber`, `Accept` | Demonstrates how to compare a large set of PDF files against a reference PDF using parallel proce... |
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs with Passwords | `Document`, `SideBySidePdfComparer`, `Compare` | Demonstrates loading password‑protected PDF files and performing a visual side‑by‑side comparison... |
| [compare-pdf-documents-page-by-page-diff](./compare-pdf-documents-page-by-page-diff.cs) | Compare PDF Page Dimensions and Generate Summary Report | `Document`, `Page`, `TextFragment` | Loads two PDF files, compares their page counts and page dimensions, and creates a diff PDF conta... |
| [compare-pdf-text-ignoring-compression](./compare-pdf-text-ignoring-compression.cs) | Compare PDF Text While Ignoring Compression Differences | `Document`, `TextAbsorber`, `TextExtractionOptions` | Loads two PDFs with different compression settings, extracts their pure text using Aspose.Pdf, an... |
| [compare-pdfs-detect-font-differences](./compare-pdfs-detect-font-differences.cs) | Compare PDFs and Detect Font Differences | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to use Aspose.Pdf to compare two PDF files, count font‑related diff operations, and opt... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes and Text Content | `Document`, `Page`, `TextAbsorber` | Loads two PDF files, checks for page count and size differences, extracts pure text from each pag... |
| [compare-pdfs-generate-json-diff-report](./compare-pdfs-generate-json-diff-report.cs) | Generate JSON Diff Report from PDF Comparison | `Document`, `Compare`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDFs with Aspose.Pdf, create a visual side‑by‑side result, extrac... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Digital Signature Fields | `Document`, `Page`, `TextAbsorber` | Demonstrates how to compare two PDF documents using Aspose.Pdf while treating digital signature f... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare Two PDFs and Log Text Differences by Page | `Document`, `Pages`, `Accept` | Shows how to compare two PDF files page‑by‑page using Aspose.Pdf, extract each page's text with T... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Compare PDFs with Unicode Text Extraction | `Document`, `TextAbsorber`, `TextExtractionOptions` | Loads two PDF files, extracts their Unicode text using Aspose.Pdf's TextAbsorber, and reports lin... |
| [compare-pdfs-with-form-fields](./compare-pdfs-with-form-fields.cs) | Compare PDFs Including Form Field Values | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Shows how to perform a side‑by‑side visual comparison of two PDF documents with Aspose.Pdf, ensur... |
| [compare-selected-pdf-pages-side-by-side](./compare-selected-pdf-pages-side-by-side.cs) | Compare Specific PDF Pages Using Side‑by‑Side Comparison | `Document`, `Page`, `SideBySideComparisonOptions` | Demonstrates how to compare selected pages of two PDFs by passing a page list to Aspose.Pdf's sid... |
| [compare-specific-page-range-pdf](./compare-specific-page-range-pdf.cs) | Compare Selected Page Range of Two PDFs | `Document`, `TextAbsorber`, `TextExtractionOptions` | Loads two PDF documents, extracts text from a specified page range using Aspose.Pdf, and compares... |
| [compare-two-pdfs-aspdf-comparison](./compare-two-pdfs-aspdf-comparison.cs) | Side‑by‑Side PDF Comparison with Aspose.Pdf | `Document`, `ComparisonOptions`, `SideBySideComparisonOptions` | Loads two PDF documents, configures comparison options, and generates a side‑by‑side visual compa... |
| [compare-two-pdfs-visual-diff](./compare-two-pdfs-visual-diff.cs) | Side-by-side PDF Comparison with Aspose.Pdf | `Document`, `SideBySidePdfComparer`, `Compare` | Loads two PDF files, configures side‑by‑side comparison options, and uses Aspose.Pdf's SideBySide... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Demonstrates how to collect annotation bounding rectangles from two PDFs and exclude those areas ... |
| [exclude-areas-from-pdf-comparison](./exclude-areas-from-pdf-comparison.cs) | Compare PDFs with Excluded Areas Using Side‑by‑Side Comparis... | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Shows how to compare two PDF documents side‑by‑side while excluding specific rectangular regions ... |
| [exclude-footer-areas-in-pdf-comparison](./exclude-footer-areas-in-pdf-comparison.cs) | Exclude Footer Areas from PDF Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to perform a side‑by‑side PDF comparison while excluding the footer region by ad... |
| [generate-diff-pdf-by-comparing-two-pdfs](./generate-diff-pdf-by-comparing-two-pdfs.cs) | Generate Diff PDF with Side‑by‑Side Comparison | `Document`, `SideBySidePdfComparer`, `Compare` | Loads two PDF files, compares them side‑by‑side using Aspose.Pdf.Comparison, and saves a diff PDF... |
| [generate-diff-pdf-highlighted-text-changes](./generate-diff-pdf-highlighted-text-changes.cs) | Generate Diff PDF and Verify Highlight Colors | `Document`, `Compare`, `HighlightAnnotation` | Creates a diff PDF that highlights text changes using Aspose.Pdf's comparison feature, then check... |
| [generate-pdf-diff-images-zip](./generate-pdf-diff-images-zip.cs) | Export PDF Comparison Diff Images to ZIP Archive | `Document`, `Compare`, `SideBySideComparisonOptions` | Shows how to compare two PDFs with Aspose.Pdf, generate a side‑by‑side comparison PDF, render eac... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Create Visual PDF Diff with GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to generate a visual difference PDF by loading two documents and using Aspose.Pd... |
| [get-image-differences-between-pdf-pages](./get-image-differences-between-pdf-pages.cs) | Detect Visual Differences Between Two PDFs | `Document`, `GraphicalPdfComparer`, `ImagesDifference` | Shows how to compare two PDF files page‑by‑page using Aspose.Pdf's GraphicalPdfComparer to find p... |
| [in-memory-pdf-comparison-diff](./in-memory-pdf-comparison-diff.cs) | Merge Two PDFs into a Comparison Document | `Document`, `Page`, `TextFragment` | Shows how to load two PDFs from in‑memory byte arrays, add a title and separator page, merge thei... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Parallel PDF Comparison Using Aspose.Pdf | `Document`, `TextAbsorber`, `Pages` | Demonstrates how to compare multiple PDF files concurrently by checking page counts and full text... |
| [preserve-original-metadata-in-diff-pdf](./preserve-original-metadata-in-diff-pdf.cs) | Compare PDFs and Preserve Metadata in Diff Document | `Document`, `Info`, `TextAbsorber` | Loads two PDF files, copies the original PDF's metadata to a new diff PDF, compares page counts a... |
| [replace-text-using-diffoperation](./replace-text-using-diffoperation.cs) | Replace PDF Text with Content from Another PDF | `Document`, `TextAbsorber`, `TextExtractionOptions` | Loads two PDF files, extracts the text from each page of the first PDF, clears the corresponding ... |
| [set-custom-image-similarity-tolerance-pdf-comparis...](./set-custom-image-similarity-tolerance-pdf-comparison.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `Threshold` | Shows how to configure a GraphicalPdfComparer with a custom threshold to compare scanned PDFs and... |
| [write-pdf-comparison-diff-to-http-response](./write-pdf-comparison-diff-to-http-response.cs) | Write PDF Comparison Diff Directly to HTTP Response | `Document`, `SideBySidePdfComparer`, `Compare` | Demonstrates how to compare two PDFs and stream the side‑by‑side diff PDF straight to an HTTP res... |

## Category Statistics
- Total examples: 29

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
Updated: 2026-11-04 | Run: `20261104_231744_27f796`
<!-- AUTOGENERATED:END -->

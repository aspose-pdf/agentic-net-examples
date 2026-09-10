---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

> **Compare PDF** in PDF using C# / .NET -- **28** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **compare-pdf** category.
This folder contains standalone C# examples for compare-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **compare-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (28/28 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (27/28 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/28 files)
- `using Aspose.Pdf.Annotations;` (3/28 files)
- `using Aspose.Pdf.Devices;` (1/28 files)
- `using Aspose.Pdf.Forms;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (27/28 files)
- `using System.Collections.Generic;` (12/28 files)
- `using System.Drawing.Imaging;` (2/28 files)
- `using System.Threading.Tasks;` (2/28 files)
- `using System.Drawing;` (1/28 files)
- `using System.IO.Compression;` (1/28 files)
- `using System.Linq;` (1/28 files)
- `using System.Text.Json;` (1/28 files)
- `using System.Threading;` (1/28 files)

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
| [batch-pdf-comparison-parallel](./batch-pdf-comparison-parallel.cs) | Batch PDF Comparison with Parallel Processing | `Document`, `SideBySidePdfComparer`, `Compare` | Demonstrates how to compare a large set of PDFs against a reference file using Aspose.Pdf's compa... |
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs Side‑by‑Side with Passwords | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Demonstrates loading two password‑protected PDF files and performing a side‑by‑side visual compar... |
| [compare-pdf-documents-page-by-page-diff](./compare-pdf-documents-page-by-page-diff.cs) | Compare PDF Documents Page by Page and Generate Diff PDF | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates how to compare two PDF files page by page using Aspose.Pdf's default ComparisonOptio... |
| [compare-pdf-text-ignoring-compression](./compare-pdf-text-ignoring-compression.cs) | Compare PDFs Textually Ignoring Compression Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF files that have identical content but different compression s... |
| [compare-pdfs-detect-font-differences](./compare-pdfs-detect-font-differences.cs) | Compare PDFs and Detect Font Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF files using Aspose.Pdf and report font changes as separate di... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes | `Document`, `Page`, `PageInfo` | Shows how to align the page dimensions of two PDFs and use GraphicalPdfComparer to compare them, ... |
| [compare-pdfs-generate-json-diff-report](./compare-pdfs-generate-json-diff-report.cs) | Compare PDFs and Generate JSON Diff Report | `Document`, `TextAbsorber`, `Visit` | The example loads two PDF files, extracts their text page by page using Aspose.Pdf, identifies ad... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Signature Fields | `Document`, `Page`, `Annotation` | Loads two signed PDFs, extracts the rectangles of all signature fields, excludes those areas from... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare PDFs and Log Differences by Page | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents page‑by‑page with Aspose.Pdf and write an audit log that r... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Compare PDFs with Unicode Text and Detect Differences | `Document`, `Page`, `TextFragment` | Creates English and Russian PDFs, then uses Aspose.Pdf's comparison API to detect Unicode text di... |
| [compare-pdfs-with-form-fields](./compare-pdfs-with-form-fields.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents, including form field values, and generate both a J... |
| [compare-selected-pdf-pages-side-by-side](./compare-selected-pdf-pages-side-by-side.cs) | Compare Selected PDF Pages Side‑by‑Side | `Document`, `ComparisonOptions`, `SideBySideComparisonOptions` | Demonstrates how to compare specific pages of two PDFs using Aspose.Pdf's side‑by‑side visual com... |
| [compare-specific-page-range-pdf](./compare-specific-page-range-pdf.cs) | Compare Specific Page Range of Two PDFs | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Loads two PDF documents, defines a page range, compares the selected pages using Aspose.Pdf's com... |
| [compare-two-pdfs-aspdf-comparison](./compare-two-pdfs-aspdf-comparison.cs) | Compare Two PDFs Using Aspose.Pdf.Comparison | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates how to compare two PDF documents with Aspose.Pdf, generating a result PDF that highl... |
| [compare-two-pdfs-visual-diff](./compare-two-pdfs-visual-diff.cs) | Compare Two PDFs and Generate a Visual Diff | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF documents using Aspose.Pdf, generate a list of differences, a... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Demonstrates collecting annotation bounding rectangles from two PDFs and configuring SideBySideCo... |
| [exclude-areas-from-pdf-comparison](./exclude-areas-from-pdf-comparison.cs) | Exclude Areas from PDF Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to define rectangular regions to exclude from each PDF and perform a side‑by‑sid... |
| [exclude-footer-areas-in-pdf-comparison](./exclude-footer-areas-in-pdf-comparison.cs) | Exclude Footer Areas in PDF Comparison | `Document`, `Page`, `Rectangle` | Demonstrates how to define rectangular exclude areas to omit footers when performing a side‑by‑si... |
| [generate-diff-pdf-by-comparing-two-pdfs](./generate-diff-pdf-by-comparing-two-pdfs.cs) | Generate a Diff PDF by Comparing Two PDFs | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to use Aspose.Pdf to compare two PDF documents and create a highlighted diff PDF... |
| [generate-diff-pdf-highlighted-text-changes](./generate-diff-pdf-highlighted-text-changes.cs) | Generate Diff PDF with Highlighted Text Changes and Verify D... | `Document`, `ComparisonOptions`, `ComparePages` | Demonstrates how to compare two PDF pages, generate a diff PDF with highlighted insertions/deleti... |
| [generate-pdf-diff-images-zip](./generate-pdf-diff-images-zip.cs) | Generate PDF Diff Images and Zip Them | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Shows how to compare two PDFs with Aspose.Pdf's GraphicalPdfComparer, export the visual differenc... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF documents and create a visual diff PDF with Aspose.Pdf's Grap... |
| [get-image-differences-between-pdf-pages](./get-image-differences-between-pdf-pages.cs) | Get Image Differences Between PDF Pages | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates how to compare two PDF documents page‑by‑page using Aspose.Pdf's GraphicalPdfCompare... |
| [in-memory-pdf-comparison-diff](./in-memory-pdf-comparison-diff.cs) | In-Memory PDF Comparison with Diff Output | `Document`, `GraphicalPdfComparer`, `ComparePagesToPdf` | Demonstrates loading two PDFs from memory streams, comparing them page‑by‑page using Aspose's Gra... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi-Threaded PDF Comparison with Aspose.Pdf | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Demonstrates how to compare multiple pairs of PDF files concurrently using Aspose.Pdf's side‑by‑s... |
| [preserve-original-metadata-in-diff-pdf](./preserve-original-metadata-in-diff-pdf.cs) | Copy Original PDF Metadata to Diff PDF | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | The example compares two PDF files using Aspose.Pdf.Comparison and then copies all document metad... |
| [replace-text-using-diffoperation](./replace-text-using-diffoperation.cs) | Replace Text in PDF Using DiffOperation Comparison | `Document`, `ComparisonOptions`, `ComparePages` | Demonstrates how to compare two PDF pages, reconstruct the original text from the first PDF using... |
| [set-custom-image-similarity-tolerance-pdf-comparis...](./set-custom-image-similarity-tolerance-pdf-comparison.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `Threshold` | Demonstrates how to configure a custom tolerance percentage for image differences when comparing ... |

## Category Statistics
- Total examples: 28

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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->

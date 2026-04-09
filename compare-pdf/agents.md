---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

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
- `using Aspose.Pdf.Comparison;` (29/29 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/29 files)
- `using Aspose.Pdf.Facades;` (1/29 files)
- `using Aspose.Pdf.Forms;` (1/29 files)
- `using Aspose.Pdf.Text;` (1/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (27/29 files)
- `using System.Collections.Generic;` (12/29 files)
- `using System.Threading.Tasks;` (2/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.Drawing.Imaging;` (1/29 files)
- `using System.IO.Compression;` (1/29 files)
- `using System.Reflection;` (1/29 files)
- `using System.Threading;` (1/29 files)

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
| [compare-encrypted-pdfs-with-passwords](./compare-encrypted-pdfs-with-passwords.cs) | Compare Encrypted PDFs with Passwords | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to open password‑protected PDF files and compare them using flat text and side‑by‑side ... |
| [compare-pdf-form-fields-diff](./compare-pdf-form-fields-diff.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Loads two PDF documents, sets ComparisonOptions to include form field values, and generates a dif... |
| [compare-pdf-text-compression](./compare-pdf-text-compression.cs) | Compare PDF Text with Different Compression Settings | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to compare two PDFs that have identical logical content but different compression, usin... |
| [compare-pdf-with-custom-image-similarity-tolerance](./compare-pdf-with-custom-image-similarity-tolerance.cs) | Compare PDFs with Custom Image Similarity Threshold | `Document`, `GraphicalPdfComparer`, `ComparisonOptions` | Shows how to compare two PDFs containing scanned images using Aspose.Pdf's GraphicalPdfComparer a... |
| [compare-pdfs-case-insensitive](./compare-pdfs-case-insensitive.cs) | Compare PDFs with Case‑Insensitive Text Comparison | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF documents using Aspose.Pdf while attempting to ignore case di... |
| [compare-pdfs-detect-font-differences](./compare-pdfs-detect-font-differences.cs) | Compare PDFs and Detect Font Differences | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF files with Aspose.Pdf, generate a visual diff PDF, and count ... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes Side‑by‑Side | `Document`, `SideBySideComparisonOptions`, `Compare` | Demonstrates how to compare two PDF documents that have different page dimensions using Aspose.Pd... |
| [compare-pdfs-excluding-footer-areas](./compare-pdfs-excluding-footer-areas.cs) | Compare PDFs While Excluding Footer Regions | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to perform a side‑by‑side PDF comparison with Aspose.Pdf while excluding footers... |
| [compare-pdfs-from-streams-to-diff-stream](./compare-pdfs-from-streams-to-diff-stream.cs) | Compare PDFs from Streams and Write Diff to Stream | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates loading two PDF files from in‑memory streams, comparing them page‑by‑page with Aspos... |
| [compare-pdfs-generate-difference-pdf](./compare-pdfs-generate-difference-pdf.cs) | Flat PDF Comparison with Difference Report | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to compare two PDF files using Aspose.Pdf's TextPdfComparer, detect changes, and save a... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Signature Fields | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents with Aspose.Pdf and exclude the rectangular areas of digit... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare Two PDFs and Log Differences | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents page‑by‑page using Aspose.Pdf and write each diff o... |
| [compare-pdfs-page-by-page-diff](./compare-pdfs-page-by-page-diff.cs) | Compare PDFs Page by Page and Generate Diff PDF | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents page by page using Aspose.Pdf's default ComparisonOptions ... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Detect Unicode Text Differences Between PDFs | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files with different language encodings, performs a flat text comparison using Aspo... |
| [compare-specific-page-range-of-two-pdfs](./compare-specific-page-range-of-two-pdfs.cs) | Compare Specific Page Range of Two PDFs | `Document`, `ComparePages`, `ComparisonOptions` | Loads two PDF files, sets a start and end page, and uses Aspose.Pdf's comparison API to detect te... |
| [compare-specific-pages-of-two-pdfs](./compare-specific-pages-of-two-pdfs.cs) | Compare Specific Pages of Two PDFs | `Document`, `Page`, `ComparisonOptions` | Demonstrates how to compare selected pages of two PDF documents using Aspose.Pdf's comparison API... |
| [copy-pdf-metadata-to-diff-pdf](./copy-pdf-metadata-to-diff-pdf.cs) | Copy PDF Metadata to Comparison Diff PDF | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDFs using Aspose.Pdf, generate a diff PDF, and then copy all pre... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Shows how to gather annotation bounding rectangles from two PDFs and exclude those areas during a... |
| [exclude-areas-pdf-comparison](./exclude-areas-pdf-comparison.cs) | Exclude Specific Areas in PDF Side‑by‑Side Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Shows how to define rectangular regions to exclude from each PDF and perform a side‑by‑side compa... |
| [extract-image-differences-between-pdfs](./extract-image-differences-between-pdfs.cs) | Extract Image Differences Between Two PDFs | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates how to compare pages of two PDF files using Aspose.Pdf's graphical comparer and save... |
| [generate-diff-pdf-compare-documents](./generate-diff-pdf-compare-documents.cs) | Generate Diff PDF by Comparing Two Documents | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF files with Aspose.Pdf and create a diff PDF using GraphicalPd... |
| [generate-diff-pdf-with-highlighted-changes](./generate-diff-pdf-with-highlighted-changes.cs) | Generate Diff PDF with Highlighted Text Changes | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates comparing two PDFs page‑by‑page, creating a diff PDF with default red/green highligh... |
| [generate-json-diff-report-pdf-comparison](./generate-json-diff-report-pdf-comparison.cs) | Generate JSON Diff Report for PDF Comparison | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | This example compares two PDF documents page by page, enumerates text differences, and generates ... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Shows how to compare two PDF documents visually and produce a diff PDF with Aspose.Pdf's Graphica... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi-Threaded PDF Comparison Using Aspose.Pdf | `Document`, `Compare`, `SideBySideComparisonOptions` | Shows how to compare multiple PDF pairs concurrently by running each side‑by‑side comparison in i... |
| [parallel-batch-pdf-comparison](./parallel-batch-pdf-comparison.cs) | Parallel Batch PDF Comparison with Limited Concurrency | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to compare a large batch of PDFs against a reference PDF in parallel while limiting the... |
| [pdf-comparison-diff-images-zip](./pdf-comparison-diff-images-zip.cs) | Generate PDF Comparison Diff Images and Zip Archive | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Demonstrates how to compare two PDFs using Aspose.Pdf, export the visual differences as PNG image... |
| [replace-changed-text-using-diffoperation](./replace-changed-text-using-diffoperation.cs) | Replace Changed Text in PDF Using DiffOperation | `Document`, `ComparePages`, `ComparisonOptions` | Demonstrates how to compare two PDFs, obtain DiffOperation objects, and replace the changed text ... |
| [write-pdf-comparison-diff-to-stream](./write-pdf-comparison-diff-to-stream.cs) | Write PDF Comparison Diff Directly to a Stream | `Document`, `ComparisonOptions`, `Compare` | Shows how to compare two PDF files with Aspose.Pdf and write the resulting diff PDF straight to a... |

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for compare-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-09 | Run: `20260409_101849_33a133`
<!-- AUTOGENERATED:END -->

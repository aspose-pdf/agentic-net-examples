---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

> **Compare PDF** in PDF using C# / .NET -- **28** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Comparison;` (28/28 files) ← category-specific
- `using Aspose.Pdf.Text;` (4/28 files)
- `using Aspose.Pdf.Annotations;` (2/28 files)
- `using Aspose.Pdf.Drawing;` (1/28 files)
- `using Aspose.Pdf.Forms;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (27/28 files)
- `using System.Collections.Generic;` (13/28 files)
- `using System.Drawing.Imaging;` (3/28 files)
- `using System.Threading.Tasks;` (2/28 files)
- `using System.Drawing;` (1/28 files)
- `using System.IO.Compression;` (1/28 files)

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
| [compare-a-large-batch-of-pdfs-using-parallel-proce...](./compare-a-large-batch-of-pdfs-using-parallel-processing-limiting-concurrency-to-avoid-excessive-memory-consumption.cs) | Compare A Large Batch Of Pdfs Using Parallel Processing Limi... |  | Compare A Large Batch Of Pdfs Using Parallel Processing Limiting Concurrency To Avoid Excessive M... |
| [compare-encrypted-pdfs-with-passwords](./compare-encrypted-pdfs-with-passwords.cs) | Compare Encrypted PDFs with Passwords Using Aspose.Pdf | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates opening password‑protected PDF files by providing user passwords to the Document con... |
| [compare-pdf-documents-page-by-page-diff](./compare-pdf-documents-page-by-page-diff.cs) | Compare PDF Documents Page by Page and Generate Diff PDF | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF files page‑by‑page using Aspose.Pdf's default ComparisonOptio... |
| [compare-pdf-font-differences](./compare-pdf-font-differences.cs) | Compare PDFs for Font Differences Using Aspose.Pdf | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files, compares them with Aspose.Pdf.Comparison to detect font changes as separate ... |
| [compare-pdf-text-ignore-compression](./compare-pdf-text-ignore-compression.cs) | Compare PDF Text While Ignoring Compression Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDFs that have identical textual content but different compressio... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes | `Document`, `Page`, `PageInfo` | Loads two PDFs with differing page dimensions, resizes their first pages to a common size, and pe... |
| [compare-pdfs-from-memory-streams](./compare-pdfs-from-memory-streams.cs) | Compare PDFs from Memory Streams and Generate Diff PDF | `Document`, `GraphicalPdfComparer`, `ComparePagesToPdf` | Loads two PDF documents directly from MemoryStream objects, compares them page by page using Aspo... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Signature Fields | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents with Aspose.Pdf and treat digital signature fields as unch... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare PDFs and Log Differences by Page | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents page‑by‑page with Aspose.Pdf, extract diff operations, and... |
| [compare-pdfs-text-differences](./compare-pdfs-text-differences.cs) | Compare Two PDFs and Generate Text Difference Report | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files and uses Aspose.Pdf.Comparison's TextPdfComparer to perform a flat text compa... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Compare PDFs with Unicode Text Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates using Aspose.Pdf's Comparison API to detect Unicode text differences between two PDF... |
| [compare-pdfs-with-form-fields](./compare-pdfs-with-form-fields.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `ExcludeTables` | Demonstrates how to compare two PDF documents that contain form fields using Aspose.Pdf, ensuring... |
| [compare-specific-page-range-pdfs](./compare-specific-page-range-pdfs.cs) | Compare Specific Page Range of Two PDFs | `Document`, `ComparisonOptions`, `ComparePages` | Loads two PDF files, defines a start and end page, and compares each page within that range using... |
| [compare-specific-pages-pdf](./compare-specific-pages-pdf.cs) | Compare Specific Pages of Two PDFs | `Document`, `Page`, `ComparisonOptions` | Demonstrates how to compare selected pages of two PDF documents using Aspose.Pdf's comparison API... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Demonstrates collecting annotation bounding rectangles from two PDFs and configuring ComparisonOp... |
| [exclude-areas-side-by-side-pdf-comparison](./exclude-areas-side-by-side-pdf-comparison.cs) | Exclude Areas in Side‑by‑Side PDF Comparison | `Document`, `SideBySideComparisonOptions`, `Compare` | Shows how to compare two PDF files side‑by‑side with Aspose.Pdf while excluding specified rectang... |
| [exclude-footer-areas-from-pdf-comparison](./exclude-footer-areas-from-pdf-comparison.cs) | Exclude Footer Areas from PDF Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to define rectangular footer regions and exclude them from a side‑by‑side PDF co... |
| [extract-image-differences-between-pdf-pages](./extract-image-differences-between-pdf-pages.cs) | Extract Image Differences Between Two PDF Pages | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates how to compare the first pages of two PDF documents using Aspose.Pdf's GraphicalPdfC... |
| [generate-diff-pdf-compare-two-pdfs](./generate-diff-pdf-compare-two-pdfs.cs) | Generate Diff PDF by Comparing Two PDFs | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to use Aspose.Pdf's GraphicalPdfComparer to compare two PDF documents and create... |
| [generate-diff-pdf-with-highlight-verification](./generate-diff-pdf-with-highlight-verification.cs) | Generate Diff PDF with Highlight Verification | `Document`, `GraphicalPdfComparer`, `Color` | Shows how to compare two PDFs using GraphicalPdfComparer, generate a diff PDF with highlighted ch... |
| [generate-json-diff-report-pdf-comparison](./generate-json-diff-report-pdf-comparison.cs) | Generate JSON Diff Report for PDF Comparison | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | This example demonstrates how to compare two PDF documents using Aspose.Pdf's text comparison and... |
| [generate-pdf-comparison-images-zip](./generate-pdf-comparison-images-zip.cs) | Generate PDF Comparison Images and Zip Them | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Demonstrates how to compare two PDFs using Aspose.Pdf's GraphicalPdfComparer, export the visual d... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF files and create a visual diff PDF with Aspose.Pdf's Graphica... |
| [parallel-pdf-comparison](./parallel-pdf-comparison.cs) | Parallel PDF Comparison Using Aspose.Pdf | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Demonstrates how to compare multiple PDF pairs concurrently with Aspose.Pdf's side‑by‑side compar... |
| [preserve-metadata-when-comparing-pdfs](./preserve-metadata-when-comparing-pdfs.cs) | Preserve Metadata When Comparing PDFs with Aspose | `Document`, `Page`, `TextFragment` | Demonstrates how to compare two PDF files using Aspose.Pdf, generate a diff PDF, and copy all ori... |
| [replace-changed-text-in-pdf](./replace-changed-text-in-pdf.cs) | Replace Changed Text in PDF with Original Text | `Document`, `ComparePages`, `DiffOperation` | Demonstrates how to compare two PDFs, extract the original text using DiffOperation objects, and ... |
| [set-custom-image-similarity-tolerance-pdf-comparis...](./set-custom-image-similarity-tolerance-pdf-comparison.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `Threshold` | Demonstrates how to compare two PDFs containing scanned images using Aspose.Pdf's GraphicalPdfCom... |
| [universal-pdf-comparison](./universal-pdf-comparison.cs) | Universal PDF Comparison with Visual Diff | `Document`, `TextPdfComparer`, `CompareFlatDocuments` | Demonstrates how to compare two PDF files using Aspose.Pdf's TextPdfComparer to detect difference... |

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
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

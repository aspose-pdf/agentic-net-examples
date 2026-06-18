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
- `using Aspose.Pdf.Text;` (4/29 files)
- `using Aspose.Pdf.Annotations;` (3/29 files)
- `using Aspose.Pdf.Devices;` (1/29 files)
- `using Aspose.Pdf.Forms;` (1/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (28/29 files)
- `using System.Collections.Generic;` (13/29 files)
- `using System.Drawing.Imaging;` (2/29 files)
- `using System.Threading.Tasks;` (2/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.IO.Compression;` (1/29 files)
- `using System.Linq;` (1/29 files)
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
| [compare-a-large-batch-of-pdfs-using-parallel-proce...](./compare-a-large-batch-of-pdfs-using-parallel-processing-limiting-concurrency-to-avoid-excessive-memory-consumption.cs) | Compare A Large Batch Of Pdfs Using Parallel Processing Limi... |  | Compare A Large Batch Of Pdfs Using Parallel Processing Limiting Concurrency To Avoid Excessive M... |
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs Side‑by‑Side | `Document`, `SideBySidePdfComparer`, `Compare` | Demonstrates how to open two password‑protected PDF files and perform a side‑by‑side visual compa... |
| [compare-pdf-documents-page-by-page-diff](./compare-pdf-documents-page-by-page-diff.cs) | Compare PDF Documents Page by Page and Generate Diff PDF | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF files page by page using Aspose.Pdf's default ComparisonOptio... |
| [compare-pdfs-detect-font-differences](./compare-pdfs-detect-font-differences.cs) | Compare PDFs and Detect Font Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to use Aspose.Pdf to compare two PDF documents, count font‑related diff operatio... |
| [compare-pdfs-diff-stream](./compare-pdfs-diff-stream.cs) | Compare Two PDFs and Write Diff PDF to HTTP Response Stream | `Document`, `Page`, `TextFragment` | Creates two sample PDFs, compares them, and writes the difference PDF directly to an output strea... |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Compare PDFs with Different Page Sizes | `Document`, `Page`, `PageInfo` | Shows how to compare two PDF pages using Aspose.Pdf, automatically resizing pages when their dime... |
| [compare-pdfs-generate-difference-report](./compare-pdfs-generate-difference-report.cs) | Compare Two PDFs and Generate a Difference Report | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to use Aspose.Pdf's comparison API to perform a flat text comparison between two... |
| [compare-pdfs-generate-side-by-side-diff](./compare-pdfs-generate-side-by-side-diff.cs) | Compare PDFs and Generate Side‑by‑Side Difference PDF | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF documents using Aspose.Pdf's comparison API, retrieve text di... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Signature Fields | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two signed PDF documents with Aspose.Pdf and treat digital signature fields ... |
| [compare-pdfs-ignoring-compression](./compare-pdfs-ignoring-compression.cs) | Compare PDFs Ignoring Compression Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDFs that have the same content but different compression setting... |
| [compare-pdfs-include-form-fields](./compare-pdfs-include-form-fields.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to compare two PDF documents with Aspose.Pdf while enabling the IncludeFormFields optio... |
| [compare-pdfs-memory-stream](./compare-pdfs-memory-stream.cs) | Compare PDFs from Memory Streams and Output Diff to Memory S... | `Document`, `Page`, `TextFragment` | Creates two sample PDFs, loads them from memory streams, compares them graphically, and writes th... |
| [compare-selected-page-range-pdf](./compare-selected-page-range-pdf.cs) | Compare Selected Page Range of Two PDFs | `Document`, `ComparisonOptions`, `TextPdfComparer` | Loads two PDF files, sets the start and end pages (using reflection if the properties are unavail... |
| [compare-specific-pdf-pages](./compare-specific-pdf-pages.cs) | Compare Specific PDF Pages Using Aspose.Pdf | `Document`, `Page`, `ComparePages` | Demonstrates how to compare selected pages of two PDFs by passing an array of page numbers, colle... |
| [compare-unicode-text-pdfs](./compare-unicode-text-pdfs.cs) | Compare Unicode Text in PDFs with Different Encodings | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to load two PDF files with different language encodings and use Aspose.Pdf's comparison... |
| [exclude-annotations-comparison](./exclude-annotations-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `TextAnnotation` | Demonstrates how to exclude annotation areas from side‑by‑side PDF comparison by adding their bou... |
| [exclude-areas-from-pdf-side-by-side-comparison](./exclude-areas-from-pdf-side-by-side-comparison.cs) | Exclude Areas from PDF Side‑by‑Side Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to define rectangular regions to exclude from each PDF and run a side‑by‑side co... |
| [exclude-footer-areas-in-pdf-comparison](./exclude-footer-areas-in-pdf-comparison.cs) | Exclude Footer Areas in PDF Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to define rectangular footer regions and set them in SideBySideComparisonOptions... |
| [generate-diff-pdf-compare-two-pdfs](./generate-diff-pdf-compare-two-pdfs.cs) | Generate Diff PDF by Comparing Two PDFs | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Shows how to compare two PDF documents with Aspose.Pdf's GraphicalPdfComparer and create a highli... |
| [generate-diff-pdf-highlight-color-verification](./generate-diff-pdf-highlight-color-verification.cs) | Generate Diff PDF with Highlighted Text Changes and Verify D... | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | This example compares two PDF documents, creates a diff PDF that highlights text differences, and... |
| [generate-json-diff-report-for-pdf-comparison](./generate-json-diff-report-for-pdf-comparison.cs) | Generate JSON Diff Report for PDF Comparison | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Demonstrates how to compare two PDF files with Aspose.Pdf and generate a JSON report of the diffe... |
| [generate-pdf-comparison-images-zip](./generate-pdf-comparison-images-zip.cs) | Generate PDF Comparison Images and Zip Them | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Shows how to compare two PDF files with Aspose.Pdf, create visual diff images for each page, and ... |
| [generate-pdf-image-differences](./generate-pdf-image-differences.cs) | Generate Image Differences Between Two PDFs | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates how to compare the first pages of two PDF files with Aspose.Pdf's GraphicalPdfCompar... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF documents and create a visual diff PDF with Aspose.Pdf's Grap... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi‑Threaded PDF Comparison with Aspose.Pdf | `Document`, `SideBySidePdfComparer`, `SideBySideComparisonOptions` | Demonstrates how to compare multiple pairs of PDF files concurrently using Aspose.Pdf's side‑by‑s... |
| [pdf-comparison-audit-log](./pdf-comparison-audit-log.cs) | PDF Comparison Audit Log | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents with Aspose.Pdf, capture each page's diff operation... |
| [preserve-pdf-metadata-after-comparison](./preserve-pdf-metadata-after-comparison.cs) | Preserve PDF Metadata After Comparison | `Document`, `SideBySidePdfComparer`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDFs side‑by‑side using Aspose.Pdf and then copy all standard and... |
| [replace-changed-text-in-pdf](./replace-changed-text-in-pdf.cs) | Replace Changed Text in PDF Using DiffOperation | `Document`, `Page`, `ComparisonOptions` | Demonstrates how to compare two PDFs page‑by‑page, extract the original text from differences, an... |
| [set-custom-tolerance-pdf-image-comparison](./set-custom-tolerance-pdf-image-comparison.cs) | Set Custom Tolerance for PDF Image Comparison | `Document`, `GraphicalPdfComparer`, `Threshold` | Demonstrates how to configure a custom similarity tolerance when comparing PDFs that contain scan... |

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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->

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

- `using Aspose.Pdf;` (28/28 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (28/28 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/28 files)
- `using Aspose.Pdf.Text;` (2/28 files)
- `using Aspose.Pdf.Forms;` (1/28 files)
- `using System;` (28/28 files)
- `using System.IO;` (27/28 files)
- `using System.Collections.Generic;` (11/28 files)
- `using System.Threading.Tasks;` (2/28 files)
- `using System.Collections.Concurrent;` (1/28 files)
- `using System.Drawing.Imaging;` (1/28 files)
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
| [case-insensitive-pdf-text-comparison](./case-insensitive-pdf-text-comparison.cs) | Case‑Insensitive PDF Text Comparison | `Document`, `TextFragmentAbsorber`, `TextFragment` | Demonstrates how to perform a case‑insensitive comparison of two PDF files by normalizing all tex... |
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs Using Side‑by‑Side Comparison | `Document`, `Compare`, `SideBySideComparisonOptions` | The example opens two password‑protected PDF files by providing their passwords to the Document c... |
| [compare-pdf-page-range](./compare-pdf-page-range.cs) | Compare Specific Page Range of Two PDFs | `Document`, `ComparisonOptions`, `ComparePages` | Loads two PDF files, validates a user‑defined page range, and uses Aspose.Pdf's comparison engine... |
| [compare-pdfs-detect-font-changes](./compare-pdfs-detect-font-changes.cs) | Compare PDFs and Detect Font Changes | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Shows how to use Aspose.Pdf's TextPdfComparer to compare two PDF documents page‑by‑page and repor... |
| [compare-pdfs-diff-memory](./compare-pdfs-diff-memory.cs) | Compare PDFs and Generate Diff PDF in Memory | `Document`, `GraphicalPdfComparer`, `Pages` | Loads two PDF documents from streams, uses Aspose's graphical comparer to create a visual diff fo... |
| [compare-pdfs-excluding-digital-signature-fields](./compare-pdfs-excluding-digital-signature-fields.cs) | Compare PDFs Excluding Digital Signature Fields | `Document`, `Page`, `Annotation` | Demonstrates how to compare two PDF documents while treating digital signature fields as unchange... |
| [compare-pdfs-excluding-footer-areas](./compare-pdfs-excluding-footer-areas.cs) | Compare PDFs While Excluding Footer Areas | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDF documents side‑by‑side while excluding footer regions by defi... |
| [compare-pdfs-including-form-field-values](./compare-pdfs-including-form-field-values.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `TextPdfComparer` | Loads two PDF files, compares them page‑by‑page using Aspose.Pdf.Comparison, and saves a visual d... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare PDFs and Log Differences by Page | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents page‑by‑page using Aspose.Pdf, extract the diff ope... |
| [compare-pdfs-page-by-page-diff](./compare-pdfs-page-by-page-diff.cs) | Compare PDFs Page by Page and Generate Diff PDF | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents page by page using Aspose.Pdf's default ComparisonO... |
| [compare-pdfs-text-diff-ignore-compression](./compare-pdfs-text-diff-ignore-compression.cs) | Compare PDFs Textually While Ignoring Compression | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDFs with identical content but different compression settings and performs a flat text... |
| [compare-pdfs-unicode-text](./compare-pdfs-unicode-text.cs) | Compare PDFs with Unicode Text Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF documents that use different language encodings, detect Unico... |
| [compare-pdfs-with-excluded-areas](./compare-pdfs-with-excluded-areas.cs) | Compare PDFs with Excluded Areas | `Document`, `Rectangle`, `ComparisonOptions` | Demonstrates how to compare two PDF documents while excluding specific rectangular regions from e... |
| [compare-selected-pdf-pages](./compare-selected-pdf-pages.cs) | Compare Selected PDF Pages | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Demonstrates extracting specific pages from two PDFs and comparing them with Aspose.Pdf's TextPdf... |
| [compare-two-pdfs-visual-difference](./compare-two-pdfs-visual-difference.cs) | Compare Two PDFs and Generate Visual Difference Report | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates how to use Aspose.Pdf's comparison API to detect differences between two PDF files a... |
| [copy-pdf-metadata-to-diff-document](./copy-pdf-metadata-to-diff-document.cs) | Copy PDF Metadata to Diff Document After Comparison | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDFs with Aspose.Pdf, generate a diff PDF, and preserve the origi... |
| [create-pdf-diff-using-comparedocuments-to-pdf](./create-pdf-diff-using-comparedocuments-to-pdf.cs) | Create Diff PDF Using CompareDocumentsToPdf | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | The example loads two PDF files, compares them with Aspose.Pdf.Comparison's GraphicalPdfComparer,... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Shows how to gather annotation bounding rectangles from two PDFs and configure side‑by‑side compa... |
| [generate-diff-pdf-with-highlighted-changes](./generate-diff-pdf-with-highlighted-changes.cs) | Generate Diff PDF with Highlighted Changes | `Document`, `GraphicalPdfComparer`, `Color` | Shows how to compare two PDFs using Aspose.Pdf's GraphicalPdfComparer to create a diff PDF with h... |
| [generate-json-diff-report-pdf-comparison](./generate-json-diff-report-pdf-comparison.cs) | Generate JSON Diff Report for PDF Comparison | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF files using Aspose.Pdf's TextPdfComparer, iterate over DiffOp... |
| [generate-pdf-diff-images-zip](./generate-pdf-diff-images-zip.cs) | Generate PDF Diff Images and Zip Them | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Shows how to compare two PDF files with Aspose.Pdf, create visual diff images for each page, and ... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF files graphically and create a visual diff PDF with Aspose.Pd... |
| [get-image-differences-between-pdf-pages](./get-image-differences-between-pdf-pages.cs) | Get Image Differences Between PDF Pages | `Document`, `Page`, `GraphicalPdfComparer` | Demonstrates how to compare corresponding pages of two PDF documents and retrieve the pixel‑level... |
| [parallel-batch-pdf-comparison](./parallel-batch-pdf-comparison.cs) | Parallel Batch PDF Comparison with Concurrency Limit | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates comparing multiple PDFs against a reference document in parallel while restricting t... |
| [parallel-pdf-comparison](./parallel-pdf-comparison.cs) | Parallel PDF Comparison with Aspose.Pdf | `Document`, `SideBySidePdfComparer`, `SideBySideComparisonOptions` | Demonstrates how to compare multiple pairs of PDF files concurrently using Aspose.Pdf's side‑by‑s... |
| [replace-inserted-text-using-diffoperation](./replace-inserted-text-using-diffoperation.cs) | Replace Inserted Text in PDF Using DiffOperation | `Document`, `Page`, `ComparisonOptions` | The example loads two PDFs, compares each page with TextPdfComparer to find insert operations, an... |
| [set-custom-image-similarity-tolerance](./set-custom-image-similarity-tolerance.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two scanned‑image PDFs using Aspose.Pdf's GraphicalPdfComparer while ... |
| [side-by-side-pdf-comparison](./side-by-side-pdf-comparison.cs) | Side‑by‑Side PDF Comparison with Page Size Alignment | `Document`, `SideBySideComparisonOptions`, `AdditionalChangeMarks` | Demonstrates comparing two PDFs that have different page sizes using Aspose.Pdf's side‑by‑side co... |

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for compare-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_141454_e72a77`
<!-- AUTOGENERATED:END -->

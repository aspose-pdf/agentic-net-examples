---
name: compare-pdf
description: C# examples for compare-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - compare-pdf

> **Compare PDF** in PDF using C# / .NET -- **29** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf.Comparison;` (28/29 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/29 files)
- `using Aspose.Pdf.Forms;` (1/29 files)
- `using Aspose.Pdf.Text;` (1/29 files)
- `using System;` (29/29 files)
- `using System.IO;` (29/29 files)
- `using System.Collections.Generic;` (13/29 files)
- `using System.Drawing.Imaging;` (2/29 files)
- `using System.Threading.Tasks;` (2/29 files)
- `using System.Drawing;` (1/29 files)
- `using System.IO.Compression;` (1/29 files)
- `using System.Linq;` (1/29 files)
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
| [compare-encrypted-pdfs-side-by-side](./compare-encrypted-pdfs-side-by-side.cs) | Compare Encrypted PDFs with Side‑by‑Side Visual Result | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates loading password‑protected PDF files, comparing them using Aspose.Pdf's side‑by‑side... |
| [compare-pdf-documents-page-by-page](./compare-pdf-documents-page-by-page.cs) | Compare PDF Documents Page by Page and Generate Diff PDF |  | Compare PDF Documents Page by Page and Generate Diff PDF |
| [compare-pdfs-different-page-sizes](./compare-pdfs-different-page-sizes.cs) | Align and Compare PDF Pages with Different Sizes | `Document`, `Page`, `PageInfo` | The example loads two PDF documents, adjusts the second page to match the first page's dimensions... |
| [compare-pdfs-excluding-footer-areas](./compare-pdfs-excluding-footer-areas.cs) | Compare PDFs While Excluding Footer Areas | `Document`, `Page`, `Rectangle` | Demonstrates how to exclude rectangular footer regions from a side‑by‑side PDF comparison using A... |
| [compare-pdfs-font-differences](./compare-pdfs-font-differences.cs) | Compare PDFs for Font Differences and Generate JSON Report | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to compare two PDF files, filter out font‑related differences, and export the diff oper... |
| [compare-pdfs-from-streams](./compare-pdfs-from-streams.cs) | Compare PDFs from Streams and Generate Diff PDF | `Document`, `GraphicalPdfComparer`, `ComparePagesToPdf` | Shows how to load two PDF documents from in‑memory streams, perform a visual page‑by‑page compari... |
| [compare-pdfs-ignore-compression](./compare-pdfs-ignore-compression.cs) | Compare PDFs Ignoring Compression Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Shows how to compare two PDFs that use different compression settings, focusing on textual differ... |
| [compare-pdfs-ignore-signature-fields](./compare-pdfs-ignore-signature-fields.cs) | Compare PDFs While Ignoring Signature Fields | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents, verify their digital signatures, and exclude signa... |
| [compare-pdfs-including-form-field-values](./compare-pdfs-including-form-field-values.cs) | Compare PDFs Including Form Field Values | `Document`, `ComparisonOptions`, `SideBySideComparisonOptions` | Shows how to compare two PDF files with Aspose.Pdf while taking AcroForm field values into accoun... |
| [compare-pdfs-log-diff-operations](./compare-pdfs-log-diff-operations.cs) | Compare PDFs and Log Diff Operations by Page | `Document`, `ComparisonOptions`, `TextPdfComparer` | Demonstrates how to compare two PDF documents page‑by‑page using Aspose.Pdf and write an audit lo... |
| [compare-pdfs-unicode-text-differences](./compare-pdfs-unicode-text-differences.cs) | Detect Unicode Text Differences Between PDFs | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF files with different language encodings (e.g., English and Ru... |
| [compare-selected-page-range-pdfs](./compare-selected-page-range-pdfs.cs) | Compare Selected Page Range of Two PDFs | `Document`, `Page`, `ComparisonOptions` | Loads two PDF documents, adjusts a user‑defined page range, and performs a text‑based comparison ... |
| [compare-selected-pdf-pages](./compare-selected-pdf-pages.cs) | Compare Selected PDF Pages Using Aspose.Pdf | `Document`, `SideBySidePdfComparer`, `SideBySideComparisonOptions` | Demonstrates how to compare specific pages from two PDF documents by extracting those pages into ... |
| [compare-two-pdfs-and-generate-visual-diff](./compare-two-pdfs-and-generate-visual-diff.cs) | Compare Two PDFs and Generate a Visual Diff | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files, compares them with Aspose.Pdf.Comparison, saves a diff PDF, and prints basic... |
| [compare-two-pdfs-visual-diff](./compare-two-pdfs-visual-diff.cs) | Compare Two PDFs and Generate Visual Diff | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Loads two PDF files, compares them with Aspose.Pdf's flat document comparison, saves a PDF that h... |
| [create-diff-pdf-using-graphicalpdfcomparer](./create-diff-pdf-using-graphicalpdfcomparer.cs) | Create Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Loads two PDF files, compares them with Aspose.Pdf's GraphicalPdfComparer, and saves a diff PDF t... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Page`, `Annotation` | Shows how to collect annotation bounding rectangles from two PDFs and configure side‑by‑side comp... |
| [exclude-areas-from-pdf-comparison](./exclude-areas-from-pdf-comparison.cs) | Exclude Areas from PDF Side‑by‑Side Comparison | `Document`, `Rectangle`, `SideBySideComparisonOptions` | Shows how to define rectangular regions to exclude from both PDFs and perform a side‑by‑side comp... |
| [generate-diff-pdf-highlighted-text-changes](./generate-diff-pdf-highlighted-text-changes.cs) | Generate Diff PDF with Highlighted Text Changes | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates comparing two PDFs, creating a diff PDF with highlighted text changes using Aspose.P... |
| [generate-pdf-difference-image](./generate-pdf-difference-image.cs) | Generate Visual Difference Image Between Two PDFs | `Document`, `GraphicalPdfComparer`, `ImagesDifference` | Demonstrates how to compare the first pages of two PDF files using Aspose.Pdf's GraphicalPdfCompa... |
| [generate-visual-diff-pdf](./generate-visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToPdf` | Demonstrates how to compare two PDF documents visually and create a diff PDF using Aspose.Pdf's G... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi‑Threaded PDF Comparison with Aspose.Pdf | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Demonstrates how to compare multiple PDF pairs concurrently using Aspose.Pdf's side‑by‑side compa... |
| [parallel-batch-pdf-comparison](./parallel-batch-pdf-comparison.cs) | Parallel Batch PDF Comparison with Concurrency Limit | `Document`, `SideBySideComparisonOptions`, `SideBySidePdfComparer` | Compares multiple PDF files against a baseline PDF in parallel while limiting the degree of paral... |
| [pdf-diff-images-to-zip](./pdf-diff-images-to-zip.cs) | Generate PDF Diff Images and Zip Them | `Document`, `GraphicalPdfComparer`, `CompareDocumentsToImages` | Demonstrates how to compare two PDFs with Aspose.Pdf, create visual diff images, and package the ... |
| [pdf-diff-report-json](./pdf-diff-report-json.cs) | Generate PDF Difference Report in JSON | `Document`, `Pages`, `PageInfo` | The example loads two PDF files, compares page count and page dimensions, records any differences... |
| [preserve-pdf-metadata-after-comparison](./preserve-pdf-metadata-after-comparison.cs) | Preserve PDF Metadata After Comparison | `Document`, `SideBySidePdfComparer`, `SideBySideComparisonOptions` | Shows how to compare two PDFs side‑by‑side using Aspose.Pdf and then copy the original document's... |
| [replace-changed-text-in-pdf](./replace-changed-text-in-pdf.cs) | Replace Changed Text in PDF Using DiffOperation | `Document`, `ComparePages`, `ComparisonOptions` | Compares two PDF pages, extracts the original text from the first PDF via DiffOperation objects, ... |
| [set-custom-image-similarity-tolerance-pdf-comparis...](./set-custom-image-similarity-tolerance-pdf-comparison.cs) | Set Custom Image Similarity Tolerance for PDF Comparison | `Document`, `GraphicalPdfComparer`, `Threshold` | Demonstrates how to compare two PDFs containing scanned images using Aspose.Pdf's GraphicalPdfCom... |
| [write-pdf-diff-to-http-response](./write-pdf-diff-to-http-response.cs) | Write PDF Diff Directly to HTTP Response Stream | `Document`, `SideBySideComparisonOptions`, `Compare` | Demonstrates comparing two PDFs side‑by‑side and writing the resulting diff PDF directly to a pro... |

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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->

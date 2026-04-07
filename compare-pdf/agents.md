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

- `using Aspose.Pdf;` (27/27 files) ← category-specific
- `using Aspose.Pdf.Comparison;` (27/27 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (2/27 files)
- `using Aspose.Pdf.Forms;` (1/27 files)
- `using Aspose.Pdf.Text;` (1/27 files)
- `using System;` (27/27 files)
- `using System.IO;` (27/27 files)
- `using System.Collections.Generic;` (14/27 files)
- `using System.Threading.Tasks;` (2/27 files)
- `using System.Drawing.Imaging;` (1/27 files)
- `using System.IO.Compression;` (1/27 files)
- `using System.Linq;` (1/27 files)

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
| [compare-encrypted-pdfs](./compare-encrypted-pdfs.cs) | Compare Encrypted PDFs Using Passwords | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates opening two password‑protected PDF files by supplying passwords to the Document cons... |
| [compare-pdf-font-differences](./compare-pdf-font-differences.cs) | Compare PDFs and Detect Font Differences | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Loads two PDF files, compares them using Aspose.Pdf's TextPdfComparer, and reports font differenc... |
| [compare-pdf-page-range__v2](./compare-pdf-page-range__v2.cs) | Compare Selected Page Range of Two PDFs | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Loads two PDF files, sets a page range in ComparisonOptions, and compares the selected pages, sav... |
| [compare-pdfs-excluded-areas__v2](./compare-pdfs-excluded-areas__v2.cs) | Compare PDFs with Excluded Areas | `Document`, `Rectangle`, `ComparisonOptions` | Shows how to exclude specific rectangular regions from two PDF documents during comparison using ... |
| [compare-pdfs-excluding-footers](./compare-pdfs-excluding-footers.cs) | Compare PDFs Excluding Footer Areas | `Document`, `SideBySideComparisonOptions`, `Compare` | Demonstrates how to compare two PDF documents while ignoring footer regions by setting ExcludeAre... |
| [compare-pdfs-excluding-signatures](./compare-pdfs-excluding-signatures.cs) | Compare PDFs Excluding Signature Fields | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Compares two PDF documents while treating digital signature fields as unchanged regions by exclud... |
| [compare-pdfs-ignore-case](./compare-pdfs-ignore-case.cs) | Compare PDFs Ignoring Case Differences | `Document`, `ComparisonOptions`, `CompareFlatDocuments` | Demonstrates how to compare two PDF documents while ignoring case differences by setting Comparis... |
| [compare-pdfs-ignore-compression](./compare-pdfs-ignore-compression.cs) | Compare PDFs Textually Ignoring Compression Differences | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Demonstrates how to compare two PDFs that differ only in compression settings using TextPdfCompar... |
| [compare-pdfs-image-tolerance](./compare-pdfs-image-tolerance.cs) | Compare PDFs with Custom Image Similarity Tolerance | `Document`, `GraphicalPdfComparer`, `Threshold` | Demonstrates how to compare two PDF files containing scanned images using a custom tolerance for ... |
| [compare-pdfs-log-differences](./compare-pdfs-log-differences.cs) | Compare Two PDFs and Log Differences | `Document`, `CompareDocumentsPageByPage`, `ComparisonOptions` | Compares two PDF documents page by page and writes each difference operation type with its page n... |
| [compare-pdfs-page-by-page__v2](./compare-pdfs-page-by-page__v2.cs) | Compare Two PDFs Page by Page and Save Diff PDF | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents page by page using Aspose.Pdf's TextPdfComparer wit... |
| [compare-pdfs-side-by-side](./compare-pdfs-side-by-side.cs) | Compare PDFs with Different Page Sizes Using Side‑by‑Side Co... | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare two PDF files that have different page dimensions by using the SideBy... |
| [compare-pdfs-unicode](./compare-pdfs-unicode.cs) | Compare PDFs with Different Language Encodings | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Demonstrates how to compare two PDF files that use different language encodings and detect Unicod... |
| [compare-pdfs-with-form-fields](./compare-pdfs-with-form-fields.cs) | Compare PDFs with Form Fields Using Aspose.Pdf | `Document`, `ComparisonOptions`, `CompareDocumentsPageByPage` | Demonstrates how to compare two PDF documents that contain form fields and include the field valu... |
| [compare-specific-pages](./compare-specific-pages.cs) | Compare Specific Pages of Two PDFs | `Document`, `ComparisonOptions`, `DiffOperation` | Demonstrates how to compare selected pages of two PDF documents using Aspose.Pdf's Document.Compa... |
| [copy-metadata-to-diff-pdf](./copy-metadata-to-diff-pdf.cs) | Copy Original PDF Metadata to Comparison Result PDF | `Document`, `Compare`, `SideBySideComparisonOptions` | Compares two PDFs side‑by‑side, generates a diff PDF, and copies the original document's metadata... |
| [exclude-annotations-from-pdf-comparison](./exclude-annotations-from-pdf-comparison.cs) | Exclude Annotations from PDF Comparison | `Document`, `Annotation`, `Rectangle` | Demonstrates how to exclude annotation regions from side‑by‑side PDF comparison by adding their b... |
| [generate-diff-pdf](./generate-diff-pdf.cs) | Generate Diff PDF Using GraphicalPdfComparer | `Document`, `CompareDocumentsToPdf` | Creates a diff PDF that highlights changes between two PDFs using GraphicalPdfComparer.CompareDoc... |
| [generate-diff-pdf__v2](./generate-diff-pdf__v2.cs) | Generate Diff PDF with Highlighted Text Changes | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Compares two PDFs, creates a diff PDF with highlighted text changes using default styles, and ver... |
| [get-image-differences](./get-image-differences.cs) | Get Image Differences Between Two PDF Pages | `Document`, `Page`, `GraphicalPdfComparer` | Loads two PDF files, compares their first pages graphically, and outputs basic information about ... |
| [json-diff-report](./json-diff-report.cs) | Generate JSON Diff Report from PDF Comparison | `Document`, `SideBySidePdfComparer`, `JsonDiffOutputGenerator` | Compares two PDF files, obtains the list of DiffOperation objects, and writes a JSON report using... |
| [multi-threaded-pdf-comparison](./multi-threaded-pdf-comparison.cs) | Multi-Threaded PDF Comparison Using Aspose.Pdf | `Document`, `Compare`, `SideBySideComparisonOptions` | Demonstrates how to compare multiple PDF pairs concurrently using Aspose.Pdf's SideBySidePdfCompa... |
| [parallel-pdf-comparison](./parallel-pdf-comparison.cs) | Parallel PDF Comparison with Concurrency Limit | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Compares a batch of PDF files against a reference PDF using Aspose.Pdf's comparison API with para... |
| [pdf-comparison-zip](./pdf-comparison-zip.cs) | Generate PDF Comparison Images and Package into ZIP | `Document`, `GraphicalPdfComparer`, `ZipArchive` | Compares two PDFs graphically, saves the difference images, and packages them into a ZIP archive ... |
| [replace-changed-text-pdf](./replace-changed-text-pdf.cs) | Replace Changed Text in PDF Using DiffOperation | `Document`, `ComparisonOptions`, `TextPdfComparer` | Compares two PDFs, extracts the original text using DiffOperation objects, and replaces the text ... |
| [universal-pdf-comparison](./universal-pdf-comparison.cs) | Universal PDF Comparison with Embedded Multimedia Support | `Document`, `CompareFlatDocuments`, `ComparisonOptions` | Demonstrates using Aspose.Pdf's generic TextPdfComparer to compare two PDFs (including those with... |
| [visual-diff-pdf](./visual-diff-pdf.cs) | Generate Visual Diff PDF Using GraphicalPdfComparer | `Document`, `CompareDocumentsToPdf` | Compares two PDF files graphically and creates a PDF that highlights the differences. |

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
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for compare-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->

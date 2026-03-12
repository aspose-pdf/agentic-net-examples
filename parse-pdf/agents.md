---
name: parse-pdf
description: C# examples for parse-pdf using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - parse-pdf

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **parse-pdf** category.
This folder contains standalone C# examples for parse-pdf operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **parse-pdf**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (76/76 files) ← category-specific
- `using Aspose.Pdf.Text;` (46/76 files) ← category-specific
- `using Aspose.Pdf.Forms;` (8/76 files)
- `using Aspose.Pdf.Annotations;` (6/76 files)
- `using Aspose.Pdf.Vector;` (6/76 files)
- `using Aspose.Pdf.Devices;` (3/76 files)
- `using Aspose.Pdf.Facades;` (3/76 files)
- `using Aspose.Pdf.AI;` (1/76 files)
- `using Aspose.Pdf.Tagged;` (1/76 files)
- `using System;` (76/76 files)
- `using System.IO;` (75/76 files)
- `using System.Collections.Generic;` (11/76 files)
- `using System.Text;` (3/76 files)
- `using System.Data;` (1/76 files)
- `using System.Drawing.Imaging;` (1/76 files)
- `using System.Threading.Tasks;` (1/76 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [Demonstrate-parsing-a-PDF-to-retrieve-images-with-configurab...](./Demonstrate-parsing-a-PDF-to-retrieve-images-with-configurable-extraction-options-providing-a-code.cs) | `ImagePlacementAbsorber` | Demonstrate parsing a PDF to retrieve images with configurable extraction opt... |
| [Extract-a-specific-table-identified-by-its-index-from-a-PDF-...](./Extract-a-specific-table-identified-by-its-index-from-a-PDF-document-and-return-its-data.cs) | `TableAbsorber`, `StringBuilder` | Extract a specific table identified by its index from a PDF document and retu... |
| [Extract-text-from-every-stamp-annotation-within-a-PDF-file-b...](./Extract-text-from-every-stamp-annotation-within-a-PDF-file-by-parsing-the-document.cs) |  | Extract text from every stamp annotation within a PDF file by parsing the doc... |
| [Implement-PDF-parsing-functionality-that-extracts-and-displa...](./Implement-PDF-parsing-functionality-that-extracts-and-displays-all-formats-supported-by-the-library.cs) |  | Implement PDF parsing functionality that extracts and displays all formats su... |
| [Instantiate-a-Document-object-and-load-the-target-PDF-file-t...](./Instantiate-a-Document-object-and-load-the-target-PDF-file-to-parse-its-content-programmatically.cs) | `TextAbsorber` | Instantiate a Document object and load the target PDF file to parse its conte... |
| [Iterate-sequentially-through-each-page-of-a-PDF-document-to-...](./Iterate-sequentially-through-each-page-of-a-PDF-document-to-programmatically-parse-its-contents.cs) | `TextAbsorber`, `ImagePlacementAbsorber` | Iterate sequentially through each page of a PDF document to programmatically ... |
| [Iterate-through-PDF-pages-to-extract-and-aggregate-paragraph...](./Iterate-through-PDF-pages-to-extract-and-aggregate-paragraph-content-into-a-structured-collection.cs) | `ParagraphAbsorber` | Iterate through PDF pages to extract and aggregate paragraph content into a s... |
| [Parse-PDF-documents-gather-detected-text-fragments-and-store...](./Parse-PDF-documents-gather-detected-text-fragments-and-store-them-securely-for-subsequent-processi.cs) | `TextFragmentAbsorber`, `TextFragment` | Parse PDF documents gather detected text fragments and store them securely fo... |
| [Parse-PDF-documents-to-detect-and-extract-all-tables-preserv...](./Parse-PDF-documents-to-detect-and-extract-all-tables-preserving-the-original-PDF-format.cs) | `TableAbsorber` | Parse PDF documents to detect and extract all tables preserving the original ... |
| [Parse-PDF-documents-to-locate-and-extract-designated-font-re...](./Parse-PDF-documents-to-locate-and-extract-designated-font-resources-preserving-their-format-specifi.cs) | `FontAbsorber` | Parse PDF documents to locate and extract designated font resources preservin... |
| [Parse-PDF-documents-with-the-provided-example-to-extract-the...](./Parse-PDF-documents-with-the-provided-example-to-extract-their-textual-content-efficiently-programma.cs) | `TextAbsorber` | Parse PDF documents with the provided example to extract their textual conten... |
| [Parse-PDF-files-to-identify-and-retrieve-superscript-and-sub...](./Parse-PDF-files-to-identify-and-retrieve-superscript-and-subscript-text-elements-preserving-their-f.cs) | `TextFragmentAbsorber` | Parse PDF files to identify and retrieve superscript and subscript text eleme... |
| [Parse-PDF-files-to-programmatically-retrieve-all-vector-grap...](./Parse-PDF-files-to-programmatically-retrieve-all-vector-graphics-outputting-them-in-PDF-format.cs) |  | Parse PDF files to programmatically retrieve all vector graphics outputting t... |
| [Parse-a-PDF-document-and-append-its-extracted-text-to-a-Stri...](./Parse-a-PDF-document-and-append-its-extracted-text-to-a-StringBuilder-or-write-it-directly-to-a-file.cs) | `TextAbsorber`, `StringBuilder` | Parse a PDF document and append its extracted text to a StringBuilder or writ... |
| [Parse-a-PDF-document-and-configure-the-TextAbsorber-s-Extrac...](./Parse-a-PDF-document-and-configure-the-TextAbsorber-s-ExtractionOptions-to-extract-text-organized-by.cs) | `TextAbsorber` | Parse a PDF document and configure the TextAbsorber s ExtractionOptions to ex... |
| [Parse-a-PDF-document-and-export-its-images-in-a-specified-fo...](./Parse-a-PDF-document-and-export-its-images-in-a-specified-format-such-as-PNG-or-JPEG.cs) | `JpegDevice`, `PngDevice` | Parse a PDF document and export its images in a specified format such as PNG ... |
| [Parse-a-PDF-document-and-extract-its-images-utilizing-config...](./Parse-a-PDF-document-and-extract-its-images-utilizing-configurable-image-extraction-options-illustr.cs) | `ImagePlacementAbsorber` | Parse a PDF document and extract its images utilizing configurable image extr... |
| [Parse-a-PDF-document-and-extract-its-images-utilizing-config...](./Parse-a-PDF-document-and-extract-its-images-utilizing-configurable-image-extraction-options-while-pr.cs) | `PdfExtractor` | Parse a PDF document and extract its images utilizing configurable image extr... |
| [Parse-a-PDF-document-and-extract-tables-employing-configurab...](./Parse-a-PDF-document-and-extract-tables-employing-configurable-table-extraction-options-preserving.cs) | `TableAbsorber` | Parse a PDF document and extract tables employing configurable table extracti... |
| [Parse-a-PDF-document-and-extract-vector-graphics-from-a-desi...](./Parse-a-PDF-document-and-extract-vector-graphics-from-a-designated-page-while-retaining-their-vector.cs) | `SvgExtractor` | Parse a PDF document and extract vector graphics from a designated page while... |
| [Parse-a-PDF-document-and-filter-extracted-images-based-on-th...](./Parse-a-PDF-document-and-filter-extracted-images-based-on-their-MIME-type-specifications.cs) | `ImagePlacementAbsorber` | Parse a PDF document and filter extracted images based on their MIME type spe... |
| [Parse-a-PDF-document-and-generate-a-concise-summary-of-its-c...](./Parse-a-PDF-document-and-generate-a-concise-summary-of-its-concluding-section-for-further-analysis.cs) | `TextAbsorber` | Parse a PDF document and generate a concise summary of its concluding section... |
| [Parse-a-PDF-document-and-programmatically-extract-all-supers...](./Parse-a-PDF-document-and-programmatically-extract-all-superscript-and-subscript-characters-as-define.cs) | `TextFragmentAbsorber` | Parse a PDF document and programmatically extract all superscript and subscri... |
| [Parse-a-PDF-document-and-retrieve-all-embedded-fonts-outputt...](./Parse-a-PDF-document-and-retrieve-all-embedded-fonts-outputting-them-in-PDF-format.cs) | `TextFragmentAbsorber`, `TextFragment` | Parse a PDF document and retrieve all embedded fonts outputting them in PDF f... |
| [Parse-a-PDF-document-and-retrieve-its-textual-content-progra...](./Parse-a-PDF-document-and-retrieve-its-textual-content-programmatically-for-downstream-data-processin.cs) | `TextAbsorber` | Parse a PDF document and retrieve its textual content programmatically for do... |
| [Parse-a-PDF-document-and-retrieve-paragraphs-grouped-under-e...](./Parse-a-PDF-document-and-retrieve-paragraphs-grouped-under-each-heading-while-preserving-hierarchica.cs) | `ParagraphAbsorber` | Parse a PDF document and retrieve paragraphs grouped under each heading while... |
| [Parse-a-PDF-document-and-retrieve-text-from-every-page-accor...](./Parse-a-PDF-document-and-retrieve-text-from-every-page-according-to-the-specified-heading.cs) | `TextAbsorber` | Parse a PDF document and retrieve text from every page according to the speci... |
| [Parse-a-PDF-document-and-retrieve-the-text-content-from-each...](./Parse-a-PDF-document-and-retrieve-the-text-content-from-each-page-in-sequential-order.cs) | `TextAbsorber` | Parse a PDF document and retrieve the text content from each page in sequenti... |
| [Parse-a-PDF-document-and-retrieve-the-text-content-of-a-desi...](./Parse-a-PDF-document-and-retrieve-the-text-content-of-a-designated-stamp-annotation.cs) |  | Parse a PDF document and retrieve the text content of a designated stamp anno... |
| [Parse-a-PDF-document-and-store-the-extracted-text-into-a-str...](./Parse-a-PDF-document-and-store-the-extracted-text-into-a-string-variable-or-write-it-to-a-file.cs) | `TextAbsorber` | Parse a PDF document and store the extracted text into a string variable or w... |
| ... | | *and 46 more files* |

## Category Statistics
- Total examples: 76

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for parse-pdf patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

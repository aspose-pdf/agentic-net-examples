---
name: working-with-tables
description: C# examples for working-with-tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-tables

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-tables** category.
This folder contains standalone C# examples for working-with-tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (118/118 files) ← category-specific
- `using Aspose.Pdf.Text;` (97/118 files) ← category-specific
- `using Aspose.Pdf.Facades;` (7/118 files)
- `using Aspose.Pdf.Drawing;` (6/118 files)
- `using Aspose.Pdf.LogicalStructure;` (3/118 files)
- `using Aspose.Pdf.Tagged;` (3/118 files)
- `using Aspose.Pdf.Annotations;` (1/118 files)
- `using Aspose.Pdf.Vector;` (1/118 files)
- `using System;` (118/118 files)
- `using System.IO;` (107/118 files)
- `using System.Data;` (18/118 files)
- `using System.Linq;` (15/118 files)
- `using System.Collections.Generic;` (8/118 files)
- `using System.Text.Json;` (1/118 files)

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
| [Add-rows-and-cells-to-a-PDF-document-programmatically-ensuri...](./Add-rows-and-cells-to-a-PDF-document-programmatically-ensuring-proper-table-structure-and-formattin.cs) | `BorderInfo`, `MarginInfo` | Add rows and cells to a PDF document programmatically ensuring proper table s... |
| [Apply-cell-based-formatting-properties-to-customize-the-appe...](./Apply-cell-based-formatting-properties-to-customize-the-appearance-and-layout-of-PDF-documents-progr.cs) | `BorderInfo` | Apply cell based formatting properties to customize the appearance and layout... |
| [Apply-cell-formatting-by-setting-its-property-values-program...](./Apply-cell-formatting-by-setting-its-property-values-programmatically-through-the-API-within-a-PDF-d.cs) | `BorderInfo`, `TableFragment` | Apply cell formatting by setting its property values programmatically through... |
| [Apply-custom-styling-to-the-table-and-output-the-resulting-d...](./Apply-custom-styling-to-the-table-and-output-the-resulting-document-in-PDF-format.cs) | `BorderInfo`, `MarginInfo` | Apply custom styling to the table and output the resulting document in PDF fo... |
| [Apply-formatting-to-a-PDF-to-exactly-replicate-the-source-do...](./Apply-formatting-to-a-PDF-to-exactly-replicate-the-source-document-s-layout-and-style.cs) |  | Apply formatting to a PDF to exactly replicate the source document s layout a... |
| [Apply-formatting-to-ensure-the-PDF-output-mirrors-the-source...](./Apply-formatting-to-ensure-the-PDF-output-mirrors-the-source-document-s-original-layout-and-styling.cs) |  | Apply formatting to ensure the PDF output mirrors the source document s origi... |
| [Assign-custom-text-to-a-PDF-table-cell-by-utilizing-the-Text...](./Assign-custom-text-to-a-PDF-table-cell-by-utilizing-the-TextFragment-class-for-precise-content-place.cs) | `TableAbsorber`, `TextFragment` | Assign custom text to a PDF table cell by utilizing the TextFragment class fo... |
| [Assign-new-text-to-a-PDF-table-cell-using-a-TextFragment-obj...](./Assign-new-text-to-a-PDF-table-cell-using-a-TextFragment-object-for-precise-formatting.cs) | `TableAbsorber` | Assign new text to a PDF table cell using a TextFragment object for precise f... |
| [Assign-values-to-PDF-table-cells-via-the-Cell-class-updating...](./Assign-values-to-PDF-table-cells-via-the-Cell-class-updating-each-cell-s-content-programmatically.cs) | `TextFragment` | Assign values to PDF table cells via the Cell class updating each cell s cont... |
| [Combine-adjacent-cells-within-a-PDF-table-to-create-unified-...](./Combine-adjacent-cells-within-a-PDF-table-to-create-unified-spanning-cells-while-maintaining-layout.cs) | `TableAbsorber`, `TextFragment` | Combine adjacent cells within a PDF table to create unified spanning cells wh... |
| [Combine-adjacent-table-cells-within-a-PDF-document-to-create...](./Combine-adjacent-table-cells-within-a-PDF-document-to-create-merged-cells-while-maintaining-layout-i.cs) | `TableAbsorber`, `TextFragment` | Combine adjacent table cells within a PDF document to create merged cells whi... |
| [Combine-multiple-table-cells-within-a-PDF-using-the-Cell.Mer...](./Combine-multiple-table-cells-within-a-PDF-using-the-Cell.Merge-function-to-create-a-unified-cell.cs) |  | Combine multiple table cells within a PDF using the Cell.Merge function to cr... |
| [Configure-and-modify-table-properties-within-a-PDF-document-...](./Configure-and-modify-table-properties-within-a-PDF-document-including-borders-padding-alignment.cs) | `BorderInfo`, `MarginInfo`, `TextFragment` | Configure and modify table properties within a PDF document including borders... |
| [Configure-table-cells-in-a-PDF-with-borders-background-color...](./Configure-table-cells-in-a-PDF-with-borders-background-colors-and-text-styling-using-the-API.cs) | `BorderInfo`, `MarginInfo`, `TextFragment` | Configure table cells in a PDF with borders background colors and text stylin... |
| [Configure-table-styling-in-a-PDF-by-applying-custom-border-s...](./Configure-table-styling-in-a-PDF-by-applying-custom-border-styles-background-colors-and-text-forma.cs) | `MarginInfo`, `BorderInfo`, `TextFragment` | Configure table styling in a PDF by applying custom border styles background ... |
| [Create-a-PDF-table-by-specifying-column-widths-and-row-heigh...](./Create-a-PDF-table-by-specifying-column-widths-and-row-heights-for-the-desired-topic.cs) | `BorderInfo` | Create a PDF table by specifying column widths and row heights for the desire... |
| [Create-a-Table-instance-programmatically-within-a-PDF-docume...](./Create-a-Table-instance-programmatically-within-a-PDF-document-to-define-structured-tabular-content.cs) | `BorderInfo`, `TextFragment` | Create a Table instance programmatically within a PDF document to define stru... |
| [Create-modify-and-retrieve-tables-within-PDF-documents-using...](./Create-modify-and-retrieve-tables-within-PDF-documents-using-the-.NET-PDF-processing-APIs.cs) | `BorderInfo` | Create modify and retrieve tables within PDF documents using the .NET PDF pro... |
| [Create-rows-and-cells-in-a-PDF-based-on-imported-data-dynami...](./Create-rows-and-cells-in-a-PDF-based-on-imported-data-dynamically-populating-table-structures.cs) | `BorderInfo`, `MarginInfo` | Create rows and cells in a PDF based on imported data dynamically populating ... |
| [Delete-table-elements-from-a-PDF-document-while-preserving-s...](./Delete-table-elements-from-a-PDF-document-while-preserving-surrounding-headings-and-overall-layout.cs) | `TableAbsorber` | Delete table elements from a PDF document while preserving surrounding headin... |
| [Delete-the-detected-tables-from-a-PDF-document-ensuring-the-...](./Delete-the-detected-tables-from-a-PDF-document-ensuring-the-remaining-content-stays-properly-intact.cs) | `TableAbsorber` | Delete the detected tables from a PDF document ensuring the remaining content... |
| [Demonstrate-how-to-programmatically-edit-existing-tables-wit...](./Demonstrate-how-to-programmatically-edit-existing-tables-within-a-PDF-document-using-.NET-APIs.cs) | `TableAbsorber`, `BorderInfo` | Demonstrate how to programmatically edit existing tables within a PDF documen... |
| [Describe-the-process-for-extracting-tables-from-a-PDF-docume...](./Describe-the-process-for-extracting-tables-from-a-PDF-document-programmatically-using-the-available.cs) | `TableAbsorber` | Describe the process for extracting tables from a PDF document programmatical... |
| [Detect-and-locate-tables-in-a-PDF-document-by-identifying-th...](./Detect-and-locate-tables-in-a-PDF-document-by-identifying-their-heading-markers-and-extracting-their.cs) | `TableAbsorber`, `TextFragmentAbsorber` | Detect and locate tables in a PDF document by identifying their heading marke... |
| [Eliminate-all-tables-from-a-PDF-document-by-invoking-the-Tab...](./Eliminate-all-tables-from-a-PDF-document-by-invoking-the-TableAbsorber-class-s-Remove-method-program.cs) | `TableAbsorber` | Eliminate all tables from a PDF document by invoking the TableAbsorber class ... |
| [Enumerate-each-entry-in-the-TableAbsorber.TableList-collecti...](./Enumerate-each-entry-in-the-TableAbsorber.TableList-collection-of-a-PDF-document-to-process-tables.cs) | `TableAbsorber` | Enumerate each entry in the TableAbsorber.TableList collection of a PDF docum... |
| [Export-extracted-table-headings-to-a-PDF-file-ensuring-prope...](./Export-extracted-table-headings-to-a-PDF-file-ensuring-proper-formatting-and-data-integrity.cs) | `TableAbsorber`, `BorderInfo`, `MarginInfo` | Export extracted table headings to a PDF file ensuring proper formatting and ... |
| [Extract-a-specific-table-from-an-existing-PDF-document-while...](./Extract-a-specific-table-from-an-existing-PDF-document-while-maintaining-its-layout-and-data-integri.cs) | `TableAbsorber`, `BorderInfo`, `MarginInfo` | Extract a specific table from an existing PDF document while maintaining its ... |
| [Extract-a-specific-table-identified-by-its-heading-from-an-e...](./Extract-a-specific-table-identified-by-its-heading-from-an-existing-PDF-document-using-the-API.cs) | `TableAbsorber` | Extract a specific table identified by its heading from an existing PDF docum... |
| [Extract-a-table-from-an-existing-PDF-document-while-retainin...](./Extract-a-table-from-an-existing-PDF-document-while-retaining-its-layout-and-data-integrity.cs) | `TableAbsorber` | Extract a table from an existing PDF document while retaining its layout and ... |
| ... | | *and 88 more files* |

## Category Statistics
- Total examples: 118

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.BorderCornerStyle`
- `Aspose.Pdf.BorderInfo`
- `Aspose.Pdf.BorderSide`
- `Aspose.Pdf.Cell`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.ColumnAdjustment`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.GraphInfo`
- `Aspose.Pdf.HorizontalAlignment`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.MarginInfo`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.Row`
- `Aspose.Pdf.Table`
- `Aspose.Pdf.Table.GetWidth`

### Rules
- Create an {image} object, assign its File property to a {string_literal} path, and embed it in a table cell by invoking cell.Paragraphs.Add({image}).
- Add a {table} to a {page} via page.Paragraphs.Add({table}), configure its DefaultCellBorder with new BorderInfo(BorderSide.All, {float}) and set ColumnWidths using a space‑separated {string_literal}; then populate rows with table.Rows.Add() and cells with row.Cells.Add(...), optionally adjusting cell properties such as VerticalAlignment.
- Instantiate a PDF document and add a page: {doc} = new Document(); {page} = {doc}.Pages.Add();
- Create a Table, set column widths via a space‑separated string and enable auto‑fit to window: {table} = new Table(); {table}.ColumnWidths = "{string_literal}"; {table}.ColumnAdjustment = ColumnAdjustment.AutoFitToWindow;
- Define default cell border and overall table border using BorderInfo with BorderSide.All and a thickness: {table}.DefaultCellBorder = new BorderInfo(BorderSide.All, {float}); {table}.Border = new BorderInfo(BorderSide.All, {float});

### Warnings
- ColumnWidths expects a space‑separated string of numeric values; ensure the format matches the table layout requirements.
- ColumnAdjustment.AutoFitToWindow only takes effect when ColumnWidths are explicitly set; otherwise the table may not resize as expected.
- GetWidth may return a meaningful value only after the table has been laid out (e.g., added to a page or after layout processing). In this isolated example the table is not added to the page, which could lead to default or zero width in some scenarios.
- TableAbsorber and AbsorbedTable reside in the Aspose.Pdf.Text namespace; ensure the appropriate using directive is present.
- TableAbsorber.TableList may be empty; accessing index 0 without checking can cause an exception.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-tables patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->

---
name: tables
description: C# examples for tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - tables

> **Tables** in PDF using C# / .NET -- **121** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **tables** category.
This folder contains standalone C# examples for tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (121/121 files) ← category-specific
- `using Aspose.Pdf.Text;` (80/121 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (16/121 files)
- `using Aspose.Pdf.LogicalStructure;` (7/121 files)
- `using Aspose.Pdf.Tagged;` (7/121 files)
- `using System;` (121/121 files)
- `using System.IO;` (121/121 files)
- `using System.Linq;` (7/121 files)
- `using System.Data;` (6/121 files)
- `using System.Collections.Generic;` (1/121 files)

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

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-auto-fit-table-to-pdf](./add-auto-fit-table-to-pdf.cs) | Add Auto-Fit Table to PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table with ColumnAdjustment.AutoFitToWindow to automatically fit... |
| [add-auto-fit-table-with-padding-to-pdf](./add-auto-fit-table-with-padding-to-pdf.cs) | Add Auto-Fit Table with Padding to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table whose columns automatically fit the page w... |
| [add-auto-fitting-table-with-borders](./add-auto-fitting-table-with-borders.cs) | Add Auto-Fitting Table with Borders to PDF | `Document`, `Table`, `ColumnAdjustment` | Demonstrates loading an existing PDF, creating a table whose columns auto‑fit to content, applyin... |
| [add-autofit-table-from-dataview-to-pdf](./add-autofit-table-from-dataview-to-pdf.cs) | Add Auto-Fit Table from DataView to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table from a DataView, applying AutoFitToContent... |
| [add-autofit-table-to-pdf](./add-autofit-table-to-pdf.cs) | Add Auto-Fit Table to PDF Document | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table with ColumnAdjustment set to AutoFitToCont... |
| [add-bordered-table-to-pdf-page](./add-bordered-table-to-pdf-page.cs) | Add Bordered Table to PDF Page | `Document`, `Table`, `BorderInfo` | Opens an existing PDF, creates a table with a default cell border, adds header and data rows, ens... |
| [add-cell-to-pdf-table-page-27](./add-cell-to-pdf-table-page-27.cs) | Insert a New Cell into an Existing PDF Table | `Document`, `Page`, `TableAbsorber` | Demonstrates how to open a PDF, extract a table from page 27 using TableAbsorber, add a new cell ... |
| [add-filtered-datatable-to-pdf-with-repeating-colum...](./add-filtered-datatable-to-pdf-with-repeating-columns.cs) | Add Filtered DataTable as Table with Repeating Columns to PD... | `Document`, `Table`, `ImportDataTable` | Loads an existing PDF, creates a DataTable, filters it to the first five rows, and imports the da... |
| [add-new-cell-to-pdf-table-page-18](./add-new-cell-to-pdf-table-page-18.cs) | Add New Cell to a PDF Table on Page 18 | `Document`, `TableAbsorber`, `AbsorbedTable` | The example opens a PDF, uses TableAbsorber to locate a table on page 18, creates a new table pos... |
| [add-new-table-row-to-pdf-page](./add-new-table-row-to-pdf-page.cs) | Add a New Table Row to a Specific PDF Page | `Document`, `Page`, `TableAbsorber` | Shows how to open a PDF, use TableAbsorber to target page 13, create a table with a row and cells... |
| [add-row-to-pdf-table-using-tableabsorber](./add-row-to-pdf-table-using-tableabsorber.cs) | Add Row to PDF Table on Page 33 Using TableAbsorber | `Document`, `Page`, `TableAbsorber` | Demonstrates how to open a PDF, use TableAbsorber to extract an existing table on page 33, add a ... |
| [add-simple-table-to-pdf](./add-simple-table-to-pdf.cs) | Add Simple Table to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table with one row and three cells containing th... |
| [add-table-autofit-merged-cells-to-pdf](./add-table-autofit-merged-cells-to-pdf.cs) | Add Table with AutoFit and Merged Cells to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, inserting a table whose columns auto‑fit the content, mergi... |
| [add-table-autofit-repeating-columns-pdf](./add-table-autofit-repeating-columns-pdf.cs) | Add Table with AutoFit and Repeating Columns to PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table whose columns automatically fit the content and repeats th... |
| [add-table-autofit-repeating-header-to-pdf](./add-table-autofit-repeating-header-to-pdf.cs) | Add Table with AutoFit and Repeating Header to PDF | `Document`, `Table`, `Row` | Loads an existing PDF, creates a table whose columns automatically adjust to the content, sets th... |
| [add-table-get-width-pdf](./add-table-get-width-pdf.cs) | Add Table to PDF and Retrieve Its Width | `Document`, `Table`, `Row` | Demonstrates loading an existing PDF, inserting a simple two‑column table, obtaining the table's ... |
| [add-table-new-page-autofit-columns](./add-table-new-page-autofit-columns.cs) | Add Table to New Page with AutoFit Columns in PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table that starts on a new page with column widths automatically... |
| [add-table-on-new-page-to-pdf](./add-table-on-new-page-to-pdf.cs) | Add Table on New Page to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table that forces a new page using IsInNewPage, ... |
| [add-table-repeating-columns-broken-layout](./add-table-repeating-columns-broken-layout.cs) | Add Table with Repeating Columns and Broken Layout to PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a five‑column table with the first three columns set to repeat on ... |
| [add-table-row-on-new-page](./add-table-row-on-new-page.cs) | Add Table Row on New Page in PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table, forces the last row to start on a new page using the IsIn... |
| [add-table-to-new-page-in-pdf](./add-table-to-new-page-in-pdf.cs) | Add Table to New Page in PDF | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a table that starts on a fresh page using IsInNewPage, ... |
| [add-table-vertically-merged-cells-to-pdf](./add-table-vertically-merged-cells-to-pdf.cs) | Add Table with Vertically Merged Cells to PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a two‑column table, merges the second column across two rows using... |
| [add-table-with-3pt-border-to-pdf](./add-table-with-3pt-border-to-pdf.cs) | Add Table with 3pt Border to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, inserting a table with a uniform 3‑point border, and saving... |
| [add-table-with-bold-html-cell-to-pdf](./add-table-with-bold-html-cell-to-pdf.cs) | Add Table with Bold HTML Cell to PDF | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, creates a tagged table, embeds an HTML paragraph with bold text in the thi... |
| [add-table-with-borders-to-pdf](./add-table-with-borders-to-pdf.cs) | Add Table with Borders to PDF | `Document`, `Table`, `BorderInfo` | Shows how to load an existing PDF, create a table, set a default cell border using BorderInfo, in... |
| [add-table-with-cell-borders-to-pdf](./add-table-with-cell-borders-to-pdf.cs) | Add Table with Cell Borders to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table, setting a uniform 1‑point border on all c... |
| [add-table-with-cell-padding-to-pdf](./add-table-with-cell-padding-to-pdf.cs) | Add Table with Cell Padding to PDF | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table, setting a uniform cell padding of 5 point... |
| [add-table-with-column-widths-and-padding](./add-table-with-column-widths-and-padding.cs) | Add Table with Column Widths and Padding to PDF | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a table with specific column widths and cell padding, a... |
| [add-table-with-column-widths-to-pdf](./add-table-with-column-widths-to-pdf.cs) | Add Table with Fixed Column Widths to PDF | `Document`, `Table`, `Row` | Demonstrates loading an existing PDF, creating a table with specified column widths and AutoFitTo... |
| [add-table-with-custom-cell-padding-to-pdf](./add-table-with-custom-cell-padding-to-pdf.cs) | Add Table with Custom Cell Padding to PDF | `Document`, `Table`, `MarginInfo` | Shows how to load an existing PDF, create a table, set its default cell padding to 6 points, add ... |
| ... | | | *and 91 more files* |

## Category Statistics
- Total examples: 121

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
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for tables patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-21 | Run: `20261121_001840_65b72d`
<!-- AUTOGENERATED:END -->

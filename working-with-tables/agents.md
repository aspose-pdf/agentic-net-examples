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

- `using Aspose.Pdf;` (91/91 files) ← category-specific
- `using Aspose.Pdf.Text;` (75/91 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (9/91 files)
- `using Aspose.Pdf.LogicalStructure;` (6/91 files)
- `using Aspose.Pdf.Tagged;` (5/91 files)
- `using Aspose.Pdf.Annotations;` (3/91 files)
- `using Aspose.Pdf.Forms;` (2/91 files)
- `using Aspose.Pdf.Operators;` (1/91 files)
- `using System;` (91/91 files)
- `using System.IO;` (79/91 files)
- `using System.Runtime.InteropServices;` (43/91 files)
- `using System.Data;` (9/91 files)
- `using System.Linq;` (8/91 files)
- `using System.Collections.Generic;` (5/91 files)
- `using System.Globalization;` (1/91 files)
- `using System.Text.Json;` (1/91 files)

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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to PDF Table | `Document`, `Page`, `Table` | Shows how to create a PDF document with a table using Aspose.Pdf, add rows, and automatically fil... |
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Add Centered Paragraph to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates opening an existing PDF, creating a table, adding a row and cell, and inserting a ce... |
| [add-footnote-references-in-table-cells](./add-footnote-references-in-table-cells.cs) | Add Footnote References in Table Cells | `Document`, `ITaggedContent`, `Page` | Shows how to insert a superscript footnote number inside a table cell and associate it with a foo... |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add Hyperlink to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates creating a LinkAnnotation with a GoToURIAction and inserting it into a table cell's ... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to a Table Cell | `Document`, `Page`, `Table` | Shows how to create a PDF table cell containing multiline text by adding several TextFragment obj... |
| [add-radio-button-group-to-table-cell](./add-radio-button-group-to-table-cell.cs) | Add Radio Button Group Inside Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a PDF table and placing a grouped set of radio buttons inside a cell using ... |
| [add-repeating-footer-row-to-pdf-table](./add-repeating-footer-row-to-pdf-table.cs) | Add Repeating Footer Row to PDF Table | `Document`, `Table`, `Row` | Shows how to add a visual footer row to a PDF table, make it repeat on every page, and define the... |
| [add-rotated-table-to-pdf-page](./add-rotated-table-to-pdf-page.cs) | Add Rotated Table to PDF Page | `Document`, `Page`, `Rotation` | Shows how to rotate a PDF page and then insert a table that automatically aligns with the rotated... |
| [add-styled-text-to-table-cell](./add-styled-text-to-table-cell.cs) | Add Styled Text to a Table Cell in PDF | `Document`, `Page`, `Table` | Shows how to place a TextFragment with a specific font and size into a table cell, using the cell... |
| [add-table-inside-paragraph-to-pdf](./add-table-inside-paragraph-to-pdf.cs) | Insert Table Within a Paragraph in a PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to add a text fragment and then insert a table as a paragraph into an existing P... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a simple 2×2 table, and insert it onto a specified page... |
| [add-table-to-textbox-form-field](./add-table-to-textbox-form-field.cs) | Add a Table Inside a TextBox Form Field | `Document`, `TextBoxField`, `Border` | Demonstrates creating a TextBox form field, building a tagged table structure, attaching the tabl... |
| [add-table-with-proportional-column-widths](./add-table-with-proportional-column-widths.cs) | Add Table with Proportional Column Widths to PDF | `Document`, `Table`, `Row` | Loads an existing PDF, calculates column width percentages, creates a table with those proportion... |
| [add-transparent-background-to-pdf-table](./add-transparent-background-to-pdf-table.cs) | Add Transparent Background Color to PDF Table | `Document`, `Page`, `Table` | Demonstrates setting a semi‑transparent background color for an Aspose.Pdf Table and saving the P... |
| [apply-different-autofit-behaviors-to-multiple-tabl...](./apply-different-autofit-behaviors-to-multiple-tables.cs) | Apply Different AutoFit Behaviors to Multiple Tables in a PD... | `Document`, `Page`, `Table` | Demonstrates how to add two tables to a PDF and set distinct ColumnAdjustment values (AutoFitToCo... |
| [apply-solid-border-to-pdf-table](./apply-solid-border-to-pdf-table.cs) | Apply Solid Border to PDF Table | `Document`, `Page`, `Table` | Demonstrates how to set a solid black border on an entire Aspose.Pdf Table by configuring a Borde... |
| [auto-fit-row-height-in-pdf-table](./auto-fit-row-height-in-pdf-table.cs) | Auto-fit Row Height in PDF Table | `Document`, `Page`, `Table` | Shows how to let a table row automatically adjust its height to fit the cell content by setting F... |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Demonstrates creating a PDF with a table whose column widths automatically adjust to the cell con... |
| [batch-add-table-with-logo-to-pdfs](./batch-add-table-with-logo-to-pdfs.cs) | Batch Add Table with Logo to PDFs | `Document`, `Page`, `Table` | Demonstrates iterating over a folder of PDF files, creating a table that contains a company logo ... |
| [batch-replace-tables-in-pdfs](./batch-replace-tables-in-pdfs.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `Page`, `TableAbsorber` | Shows how to process all PDF files in a folder, locate tables on each page with TableAbsorber, an... |
| [calculate-remaining-page-space-add-table](./calculate-remaining-page-space-add-table.cs) | Calculate Remaining Page Space and Add Table to PDF | `Document`, `Page`, `Rectangle` | Loads a PDF, computes the usable vertical space by subtracting existing content height from the p... |
| [center-table-in-pdf](./center-table-in-pdf.cs) | Center Table in PDF using Aspose.Pdf | `Document`, `Table`, `Row` | Demonstrates how to create a table, set its HorizontalAlignment to Center, and add it to a PDF pa... |
| [check-table-isbroken-property](./check-table-isbroken-property.cs) | Check and Set Table Break Using IsBroken Property | `Document`, `Page`, `Table` | The example creates a PDF, adds a table, reads the Table.IsBroken property to see if the table wi... |
| [conditional-formatting-table-cells](./conditional-formatting-table-cells.cs) | Conditional Formatting of Table Cells in PDF | `Document`, `Page`, `Table` | Demonstrates how to apply background colors to PDF table cells based on numeric thresholds using ... |
| [count-tables-in-pdf](./count-tables-in-pdf.cs) | Count Tables in PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Shows how to load a PDF with Aspose.Pdf, use TableAbsorber to extract tables, and obtain the numb... |
| [create-fixed-width-table](./create-fixed-width-table.cs) | Create Fixed-Width Table in PDF | `Document`, `Page`, `Table` | Shows how to create a table with a total width of 500 points, add a row and cell, position it on ... |
| [create-pdf-table-from-datatable](./create-pdf-table-from-datatable.cs) | Create PDF Table from a DataTable | `Document`, `Page`, `Table` | Demonstrates how to fill a DataTable, import it into an Aspose.Pdf Table, add the table to a PDF ... |
| [create-pdf-table-from-datatable__v2](./create-pdf-table-from-datatable__v2.cs) | Create PDF Table with Dynamic Row Count from DataTable | `Document`, `Page`, `Table` | Demonstrates how to count source records, import a DataTable into an Aspose.Pdf Table, and save t... |
| [create-repeating-table-header](./create-repeating-table-header.cs) | Create Repeating Table Header in PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to add a table with a header row that repeats on each new page using Aspose.Pdf's tagge... |
| [create-table-stretched-to-page-width](./create-table-stretched-to-page-width.cs) | Create Table Stretched to Page Width in PDF | `Document`, `Page`, `Table` | Shows how to generate a PDF with a table whose columns are sized proportionally to fill the entir... |
| ... | | | *and 61 more files* |

## Category Statistics
- Total examples: 91

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
Updated: 2026-05-08 | Run: `20260508_145008_6ada82`
<!-- AUTOGENERATED:END -->

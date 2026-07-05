---
name: working-with-tables
description: C# examples for working-with-tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-tables

> **Working with tables** in PDF using C# / .NET -- **98** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-tables** category.
This folder contains standalone C# examples for working-with-tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (98/98 files) ← category-specific
- `using Aspose.Pdf.Text;` (69/98 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (11/98 files)
- `using Aspose.Pdf.Tagged;` (5/98 files)
- `using Aspose.Pdf.LogicalStructure;` (4/98 files)
- `using Aspose.Pdf.Annotations;` (3/98 files)
- `using Aspose.Pdf.Forms;` (3/98 files)
- `using System;` (98/98 files)
- `using System.IO;` (69/98 files)
- `using System.Data;` (13/98 files)
- `using System.Linq;` (8/98 files)
- `using System.Collections.Generic;` (7/98 files)
- `using System.Drawing;` (1/98 files)
- `using System.Reflection;` (1/98 files)
- `using System.Text;` (1/98 files)
- `using System.Text.Json;` (1/98 files)

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
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Add Centered Paragraph to Table Cell in PDF | `Document`, `Page`, `Table` | Shows how to create a PDF document, insert a table, and place a centered paragraph inside a table... |
| [add-checkbox-to-table-cell](./add-checkbox-to-table-cell.cs) | Add Checkbox Form Field Inside a Table Cell | `Document`, `Page`, `Table` | Shows how to create a PDF table, calculate the cell coordinates, and insert a CheckboxField form ... |
| [add-footnote-references-in-table-cells](./add-footnote-references-in-table-cells.cs) | Add Footnote References in Table Cells | `Document`, `Page`, `Table` | Demonstrates inserting superscript footnote numbers inside table cells and adding matching footno... |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add Hyperlink to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to create a table in a PDF, define a rectangle over a cell, and attach a LinkAnn... |
| [add-list-inside-table-cell](./add-list-inside-table-cell.cs) | Add List Inside a Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to place a bulleted list inside a table cell by creating TextFragment paragraphs... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert multiple TextFragment objects with line‑break fragments into a table c... |
| [add-radio-button-group-to-table-cell](./add-radio-button-group-to-table-cell.cs) | Add Radio Button Group to a PDF Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a RadioButtonField, grouping its options with a common name, and placing th... |
| [add-styled-text-to-table-cell](./add-styled-text-to-table-cell.cs) | Add Styled Text to a Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert a TextFragment with a specific font, size, and color into a table cell... |
| [add-table-background-color-gradient-workaround](./add-table-background-color-gradient-workaround.cs) | Add Table Background Color with Gradient Workaround | `Document`, `Page`, `Table` | Demonstrates setting a solid background color for an Aspose.Pdf Table and explains that gradient ... |
| [add-table-footer-to-pdf](./add-table-footer-to-pdf.cs) | Add Table Footer Row to PDF Using Tagged Content | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a table with header, body, and a footer row that repeats at the bottom... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to load an existing PDF, create a table with header and data rows, and insert it into a... |
| [add-table-to-textboxfield](./add-table-to-textboxfield.cs) | Add Table Appearance to a TextBox Form Field | `Document`, `TextBoxField`, `Border` | Demonstrates creating a TextBox form field, building a tagged PDF table structure, and linking th... |
| [add-table-with-remaining-page-space](./add-table-with-remaining-page-space.cs) | Add Table Within Remaining Page Space | `Document`, `Page`, `CalculateContentBBox` | Demonstrates how to calculate the free vertical space on a PDF page by subtracting margins and ex... |
| [add-table-with-semi-transparent-background](./add-table-with-semi-transparent-background.cs) | Add Table with Semi-Transparent Background to PDF | `Document`, `Page`, `Table` | Demonstrates creating a table, applying a semi‑transparent background color, and inserting it int... |
| [adjust-table-column-widths-proportionally](./adjust-table-column-widths-proportionally.cs) | Adjust Table Column Widths Proportionally in PDF | `Document`, `Table`, `Row` | Shows how to calculate proportional column widths and apply them to a PDF table using Aspose.Pdf. |
| [alternating-row-colors-pdf-table](./alternating-row-colors-pdf-table.cs) | Apply Alternating Row Colors in PDF Table | `Document`, `Page`, `Table` | Demonstrates how to create a PDF table with Aspose.Pdf, add data rows, and set alternating backgr... |
| [apply-different-autofit-behaviors-to-multiple-tabl...](./apply-different-autofit-behaviors-to-multiple-tables.cs) | Apply Different AutoFit Behaviors to Multiple Tables | `Document`, `Page`, `Table` | Creates a PDF containing two tables and shows how to assign distinct AutoFit behaviors—AutoFitToC... |
| [apply-shadow-effect-to-pdf-table](./apply-shadow-effect-to-pdf-table.cs) | Apply Shadow Effect to a PDF Table | `Document`, `Page`, `Table` | Shows how to create a table in a PDF document and add a shadow effect by configuring the Table.Sh... |
| [apply-solid-border-to-pdf-table](./apply-solid-border-to-pdf-table.cs) | Apply Solid Border to PDF Table | `Document`, `Page`, `Table` | Creates a PDF document, adds a three‑column table, and applies a uniform solid black border aroun... |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Creates a PDF with a table whose column widths automatically adjust to fit the cell contents usin... |
| [auto-fit-table-row-height](./auto-fit-table-row-height.cs) | Auto‑Fit Table Row Height in PDF | `Document`, `Page`, `Table` | Shows how to let a table row automatically adjust its height to fit wrapped text by setting Fixed... |
| [auto-fit-table-to-page-width](./auto-fit-table-to-page-width.cs) | Stretch Table Across Page Width in PDF | `Document`, `Page`, `Table` | Demonstrates how to create a PDF with a table that automatically stretches to fill the page width... |
| [auto-numbered-table](./auto-numbered-table.cs) | Create Auto‑Numbered Table in PDF | `Document`, `Page`, `Table` | Shows how to add a table to a PDF document and automatically insert sequential numbers into the f... |
| [batch-add-table-with-logo-to-pdfs](./batch-add-table-with-logo-to-pdfs.cs) | Batch Add Table with Company Logo to PDFs | `Document`, `Table`, `Row` | Demonstrates how to process a folder of PDF files, inserting a two‑column table containing a comp... |
| [batch-replace-tables-in-multiple-pdfs](./batch-replace-tables-in-multiple-pdfs.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `TableAbsorber`, `Visit` | Demonstrates how to locate tables in each PDF using TableAbsorber and replace them with new table... |
| [center-align-table-in-pdf](./center-align-table-in-pdf.cs) | Center Align Table in PDF | `Document`, `Table`, `Row` | Shows how to create a table, set its HorizontalAlignment to Center, and insert it into a PDF docu... |
| [conditional-formatting-table-cells](./conditional-formatting-table-cells.cs) | Conditional Formatting of Table Cells in PDF | `Document`, `Page`, `ImportDataTable` | Demonstrates importing a DataTable into an Aspose.Pdf Table and applying background colors to cel... |
| [count-tables-in-pdf](./count-tables-in-pdf.cs) | Count Tables in a PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Shows how to use Aspose.Pdf's TableAbsorber to detect tables in a PDF document and retrieve the t... |
| [create-and-populate-table-pdf](./create-and-populate-table-pdf.cs) | Create and Populate a Table in a PDF | `Document`, `Page`, `Table` | Demonstrates how to programmatically create a Table, add rows and cells, apply basic styling, and... |
| [create-fixed-width-table](./create-fixed-width-table.cs) | Create Fixed-Width Table in PDF | `Document`, `Page`, `Table` | Shows how to generate a PDF with a table whose width is fixed to 500 points using Aspose.Pdf for C#. |
| ... | | | *and 68 more files* |

## Category Statistics
- Total examples: 98

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
- Review code examples in this folder for working-with-tables patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->

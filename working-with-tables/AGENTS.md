---
name: working-with-tables
description: C# examples for working-with-tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-tables

> **Working with tables** in PDF using C# / .NET -- **153** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-tables** category.
This folder contains standalone C# examples for working-with-tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (96/153 files) ← category-specific
- `using Aspose.Pdf.Text;` (70/153 files)
- `using Aspose.Pdf.Drawing;` (14/153 files)
- `using Aspose.Pdf.LogicalStructure;` (4/153 files)
- `using Aspose.Pdf.Tagged;` (4/153 files)
- `using Aspose.Pdf.Forms;` (2/153 files)
- `using Aspose.Pdf.Annotations;` (1/153 files)
- `using Aspose.Pdf.Devices;` (1/153 files)
- `using System;` (96/153 files)
- `using System.IO;` (72/153 files)
- `using System.Data;` (13/153 files)
- `using System.Collections.Generic;` (8/153 files)
- `using System.Linq;` (8/153 files)
- `using System.Xml.Linq;` (2/153 files)
- `using System.Globalization;` (1/153 files)
- `using System.Text;` (1/153 files)
- `using System.Text.Json;` (1/153 files)

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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to PDF Table | `Document`, `Page`, `Table` | Shows how to create a PDF table with Aspose.Pdf and fill the first column with sequential numbers... |
| [add-background-color-to-table-cell](./add-background-color-to-table-cell.cs) | Add background color to table cell |  | Add background color to table cell |
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Add Centered Paragraph to Table Cell | `Document`, `Page`, `Table` | Demonstrates how to create a PDF table with Aspose.Pdf and insert a paragraph into a cell with ho... |
| [add-checkbox-form-field-in-table-cell](./add-checkbox-form-field-in-table-cell.cs) | Add Checkbox Form Field Inside Table Cell | `Document`, `Page`, `Table` | Shows how to create a PDF with a table and place a checkbox form field inside one of the table's ... |
| [add-checkbox-in-table-cell](./add-checkbox-in-table-cell.cs) | Add checkbox in table cell |  | Add checkbox in table cell |
| [add-footer-row-to-pdf-table](./add-footer-row-to-pdf-table.cs) | Add footer row to pdf table |  | Add footer row to pdf table |
| [add-footnote-references-in-pdf-table](./add-footnote-references-in-pdf-table.cs) | Add footnote references in pdf table |  | Add footnote references in pdf table |
| [add-footnote-references-to-table-cells](./add-footnote-references-to-table-cells.cs) | Add Footnote References to Table Cells in PDF | `Document`, `Page`, `Table` | Shows how to insert superscript footnote markers inside table cells, attach Aspose.Pdf footnote n... |
| [add-gradient-background-behind-table](./add-gradient-background-behind-table.cs) | Add gradient background behind table |  | Add gradient background behind table |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add hyperlink to table cell |  | Add hyperlink to table cell |
| [add-list-items-in-table-cell](./add-list-items-in-table-cell.cs) | Add List Items Inside a Table Cell | `Document`, `Page`, `Table` | Shows how to create a bullet list within a PDF table cell by adding TextFragment paragraphs with ... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert multiline text into a PDF table cell by adding separate TextFragment o... |
| [add-numbered-list-in-table-cell](./add-numbered-list-in-table-cell.cs) | Add numbered list in table cell |  | Add numbered list in table cell |
| [add-radio-button-group-in-table-cell](./add-radio-button-group-in-table-cell.cs) | Add Radio Button Group Inside a Table Cell | `Document`, `Page`, `Table` | Demonstrates how to place a group of radio buttons inside a table cell by creating a RadioButtonF... |
| [add-solid-border-to-pdf-table](./add-solid-border-to-pdf-table.cs) | Add Solid Border to PDF Table | `Document`, `Table`, `BorderInfo` | Demonstrates how to load an existing PDF, create a table, apply a solid black border to the entir... |
| [add-styled-text-to-table-cell](./add-styled-text-to-table-cell.cs) | Add styled text to table cell |  | Add styled text to table cell |
| [add-table-footer-to-pdf](./add-table-footer-to-pdf.cs) | Add Table Footer Row Repeated on Each PDF Page | `Document`, `Table`, `ITaggedContent` | Demonstrates how to add a visual table with a footer row that repeats at the bottom of each page ... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to insert a formatted table onto a chosen page of a PDF document using Aspose.Pdf, incl... |
| [add-table-with-background-color](./add-table-with-background-color.cs) | Add Table with Background Color to PDF | `Document`, `Page`, `Table` | Demonstrates creating a PDF, inserting a table, and applying a solid background color while notin... |
| [add-table-with-semi-transparent-background](./add-table-with-semi-transparent-background.cs) | Add Table with Semi-Transparent Background to PDF | `Document`, `Page`, `Table` | Demonstrates how to create a PDF, add a table, and set the table's background color with opacity ... |
| [add-text-fragment-to-table-cell](./add-text-fragment-to-table-cell.cs) | Add TextFragment with Font and Size to a PDF Table Cell | `Document`, `Table`, `Row` | Demonstrates how to insert text with a specific font and size into a table cell by creating a Tex... |
| [adjust-table-column-widths-proportionally](./adjust-table-column-widths-proportionally.cs) | Adjust Table Column Widths Proportionally in PDF | `Document`, `Page`, `Table` | Shows how to compute the total width of a table's columns, convert each width to a percentage of ... |
| [adjust-table-column-widths](./adjust-table-column-widths.cs) | Adjust table column widths |  | Adjust table column widths |
| [alternating-row-colors-pdf-table](./alternating-row-colors-pdf-table.cs) | Alternating row colors pdf table |  | Alternating row colors pdf table |
| [apply-different-autofit-behavior-to-tables](./apply-different-autofit-behavior-to-tables.cs) | Apply different autofit behavior to tables |  | Apply different autofit behavior to tables |
| [apply-different-autofit-behaviors-to-tables](./apply-different-autofit-behaviors-to-tables.cs) | Apply Different AutoFit Behaviors to Tables in a PDF | `Document`, `Page`, `Table` | Demonstrates how to add multiple tables to a PDF and set distinct ColumnAdjustment (AutoFitToCont... |
| [apply-double-border-to-pdf-table](./apply-double-border-to-pdf-table.cs) | Apply Double Border to PDF Table | `Document`, `Page`, `Table` | Creates a PDF with a 3x3 table and sets a double‑border effect by configuring BorderInfo with wid... |
| [apply-solid-border-to-pdf-table](./apply-solid-border-to-pdf-table.cs) | Apply solid border to pdf table |  | Apply solid border to pdf table |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | Auto‑Fit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table, sets ColumnAdjustment to AutoFitToContent so columns resi... |
| [auto-fit-table-row-height](./auto-fit-table-row-height.cs) | Auto‑Fit Table Row Height in PDF | `Document`, `Page`, `Table` | Demonstrates how to let a table row automatically adjust its height to fit wrapped cell content b... |
| ... | | | *and 123 more files* |

## Category Statistics
- Total examples: 153

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
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->

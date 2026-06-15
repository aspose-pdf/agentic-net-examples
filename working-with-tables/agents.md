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

- `using Aspose.Pdf;` (97/97 files) ← category-specific
- `using Aspose.Pdf.Text;` (78/97 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (12/97 files)
- `using Aspose.Pdf.LogicalStructure;` (7/97 files)
- `using Aspose.Pdf.Tagged;` (7/97 files)
- `using Aspose.Pdf.Forms;` (3/97 files)
- `using Aspose.Pdf.Annotations;` (1/97 files)
- `using System;` (96/97 files)
- `using System.IO;` (62/97 files)
- `using System.Runtime.InteropServices;` (36/97 files)
- `using System.Data;` (10/97 files)
- `using System.Linq;` (7/97 files)
- `using System.Collections.Generic;` (5/97 files)
- `using System.Drawing;` (1/97 files)
- `using System.Globalization;` (1/97 files)
- `using System.Reflection;` (1/97 files)
- `using System.Text.Json;` (1/97 files)
- `using System.Xml.Linq;` (1/97 files)

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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to PDF Table | `Document`, `Table`, `Row` | Shows how to create a table in a PDF, insert sequential numbers into the first cell of each row, ... |
| [add-centered-paragraph-cell](./add-centered-paragraph-cell.cs) | Add Centered Paragraph Inside Table Cell | `Document`, `Table`, `Row` | Demonstrates creating a PDF, adding a table cell, and inserting a paragraph with centered alignme... |
| [add-checkbox-in-table-cell](./add-checkbox-in-table-cell.cs) | Add Checkbox Form Field Inside Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a PDF with a table and inserting a checkbox form field inside a table cell. |
| [add-double-border-table](./add-double-border-table.cs) | Add Double Border to Table in PDF | `Document`, `Table`, `BorderInfo` | Creates a PDF, adds a table and applies a double border style with a specified width. |
| [add-footnote-references-in-table-cells](./add-footnote-references-in-table-cells.cs) | Add Footnote References in Table Cells | `Document`, `Page`, `Table` | Demonstrates how to insert superscript footnote markers in table cells using TextFragment.FootNot... |
| [add-gradient-background-to-table](./add-gradient-background-to-table.cs) | Add Gradient Background to Table | `Document`, `Page`, `Table` | Demonstrates how to apply a linear gradient brush as the background of a table in a PDF. |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add Hyperlink to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert a web hyperlink inside a table cell by creating a LinkAnnotation and a... |
| [add-list-inside-table-cell](./add-list-inside-table-cell.cs) | Add List Inside Table Cell in PDF | `Document`, `Table`, `Row` | Shows how to create a PDF, add a table, and insert a bullet list inside a cell using Paragraphs. |
| [add-multiline-text-cell](./add-multiline-text-cell.cs) | Add Multiline Text to Table Cell using TextFragments | `Document`, `Table`, `Row` | Demonstrates how to create a table cell with multiline text by adding multiple TextFragment objec... |
| [add-numbered-list-in-cell](./add-numbered-list-in-cell.cs) | Add Numbered List Inside a Table Cell | `Document`, `Page`, `Table` | Demonstrates how to create a numbered list inside a PDF table cell using Aspose.PDF tagged PDF st... |
| [add-radio-button-group](./add-radio-button-group.cs) | Add Radio Button Group Inside a PDF Cell | `Document`, `Table`, `Row` | Demonstrates creating a PDF, adding a table cell and placing a radio button group with multiple o... |
| [add-table-as-paragraph-to-pdf](./add-table-as-paragraph-to-pdf.cs) | Insert Table as Paragraph in PDF | `Document`, `Page`, `TextFragment` | Demonstrates how to load an existing PDF, add a text fragment, create a Table object, populate it... |
| [add-table-background-color-opacity](./add-table-background-color-opacity.cs) | Add Table Background Color with Opacity | `Document`, `Page`, `Table` | Demonstrates creating a PDF table and setting its BackgroundColor using Color.FromArgb to apply a... |
| [add-table-to-pdf-shadow-effect](./add-table-to-pdf-shadow-effect.cs) | Add Table to PDF and Explain Shadow Effect Limitation | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table with header and data rows, adds it to the page, and notes ... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to insert a table into a chosen page of a PDF document using Aspose.Pdf. |
| [add-table-to-textbox-form-field](./add-table-to-textbox-form-field.cs) | Add Tagged Table Inside a TextBox Form Field | `Document`, `Form`, `TextBoxField` | Demonstrates creating a TextBox form field, building a tagged table with the logical structure AP... |
| [add-table-with-remaining-page-space](./add-table-with-remaining-page-space.cs) | Add Table Within Remaining Page Space | `Document`, `Page`, `CalculateContentBBox` | Demonstrates how to calculate the usable vertical space on a PDF page by subtracting margins and ... |
| [add-text-with-font-to-table-cell](./add-text-with-font-to-table-cell.cs) | Add Text with Font and Size to Table Cell | `Document`, `Page`, `Table` | Demonstrates how to insert a TextFragment with a specific font and size into a table cell, config... |
| [adjust-table-column-widths-proportionally](./adjust-table-column-widths-proportionally.cs) | Proportionally Set Table Column Widths in PDF | `Document`, `Table`, `Row` | The example loads a PDF, creates a table, calculates each column's width as a percentage of the t... |
| [apply-different-autofit-behavior-to-tables](./apply-different-autofit-behavior-to-tables.cs) | Apply Different AutoFitBehavior Settings to Multiple PDF Tab... | `Document`, `Page`, `Table` | Shows how to create several tables in a single PDF and apply distinct ColumnAdjustment settings (... |
| [apply-solid-border-to-pdf-table](./apply-solid-border-to-pdf-table.cs) | Apply Solid Border to a PDF Table | `Document`, `Page`, `Table` | Demonstrates creating a PDF document with a table and applying a solid black border around the en... |
| [auto-fit-row-height](./auto-fit-row-height.cs) | Auto-fit Row Height in PDF Table | `Document`, `Table`, `Row` | Demonstrates how to let a table row automatically adjust its height to fit its content by not fix... |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | Auto‑Fit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Demonstrates creating a PDF with a table and automatically adjusting column widths to fit the cel... |
| [batch-add-table-with-logo-to-pdfs](./batch-add-table-with-logo-to-pdfs.cs) | Batch Add Table with Logo to PDFs | `Document`, `Table`, `Image` | Demonstrates how to iterate through a folder of PDF files, create a two‑column table containing a... |
| [batch-replace-tables-in-multiple-pdfs](./batch-replace-tables-in-multiple-pdfs.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `Page`, `TableAbsorber` | Shows how to locate tables on each page of PDFs using TableAbsorber and replace them with a new t... |
| [calculate-rendered-table-width](./calculate-rendered-table-width.cs) | Calculate Rendered Table Width After Layout | `Document`, `Page`, `Table` | Shows how to create a PDF with a table using Aspose.Pdf, force layout by saving to a stream, and ... |
| [center-align-table-in-pdf](./center-align-table-in-pdf.cs) | Center Align Table in PDF | `Document`, `Table`, `Row` | Shows how to create a table in a PDF with Aspose.Pdf, set its HorizontalAlignment to Center, and ... |
| [check-pdf-table-is-broken](./check-pdf-table-is-broken.cs) | Check if PDF Table Is Broken Across Pages | `Document`, `ITaggedContent`, `TableElement` | Loads a PDF document, finds all tagged TableElement objects, and reports whether each table will ... |
| [conditional-formatting-table-cells](./conditional-formatting-table-cells.cs) | Conditional Formatting of Table Cells in PDF | `Document`, `Page`, `Table` | Demonstrates creating a PDF with a table, populating it with numeric data, and applying a backgro... |
| [count-tables-in-pdf-using-tableabsorber](./count-tables-in-pdf-using-tableabsorber.cs) | Count Tables in PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Demonstrates how to use Aspose.Pdf's TableAbsorber to detect and count tables in a PDF document. |
| ... | | | *and 67 more files* |

## Category Statistics
- Total examples: 97

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
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for working-with-tables patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->

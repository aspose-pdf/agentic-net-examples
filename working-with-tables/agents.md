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

- `using Aspose.Pdf;` (104/104 files) ← category-specific
- `using Aspose.Pdf.Text;` (81/104 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (11/104 files)
- `using Aspose.Pdf.Facades;` (5/104 files)
- `using Aspose.Pdf.LogicalStructure;` (5/104 files)
- `using Aspose.Pdf.Tagged;` (4/104 files)
- `using Aspose.Pdf.Forms;` (3/104 files)
- `using Aspose.Pdf.Annotations;` (2/104 files)
- `using System;` (104/104 files)
- `using System.IO;` (62/104 files)
- `using System.Runtime.InteropServices;` (59/104 files) ← category-specific
- `using System.Data;` (14/104 files)
- `using System.Collections.Generic;` (8/104 files)
- `using System.Linq;` (6/104 files)
- `using System.Drawing;` (1/104 files)
- `using System.Globalization;` (1/104 files)
- `using System.Text;` (1/104 files)
- `using System.Text.Json;` (1/104 files)

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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to PDF Table | `Document`, `Save`, `Page` | Demonstrates how to create a PDF table with Aspose.Pdf and insert sequential numbers into the fir... |
| [add-bullet-list-in-table-cell](./add-bullet-list-in-table-cell.cs) | Add a Bullet List Inside a Table Cell | `Document`, `Page`, `Table` | Shows how to create a bullet list within a PDF table cell by adding separate TextFragment paragra... |
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Center Text in Table Cell of a PDF | `Document`, `Save`, `Page` | Shows how to create a PDF, add a table, and place a centered paragraph (text fragment) inside a c... |
| [add-checkbox-field-in-table-cell](./add-checkbox-field-in-table-cell.cs) | Add Checkbox Form Field Inside Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a PDF with a table, adding text to a cell, and inserting a CheckboxField fo... |
| [add-footer-row-to-tagged-pdf-table](./add-footer-row-to-tagged-pdf-table.cs) | Add Footer Row to Tagged PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a table with header, body, and a footer that appears at the bottom of each pa... |
| [add-footnote-references-in-table-cells](./add-footnote-references-in-table-cells.cs) | Add Footnote References in Table Cells | `Document`, `Save`, `ITaggedContent` | Demonstrates how to insert superscript footnote references inside table cells and associate them ... |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add Hyperlink to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert a web hyperlink into a table cell by creating a LinkAnnotation and add... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to a Table Cell | `Document`, `Page`, `Table` | Shows how to create a PDF table cell containing multiple lines by adding separate TextFragment ob... |
| [add-radio-button-group-inside-table-cell](./add-radio-button-group-inside-table-cell.cs) | Add Radio Button Group Inside a Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a radio button group, defining its options, and placing the group inside a ... |
| [add-semi-transparent-background-color-to-pdf-table](./add-semi-transparent-background-color-to-pdf-table.cs) | Add Semi-Transparent Background Color to PDF Table | `Document`, `Page`, `Table` | Demonstrates how to create a table in a PDF and set a semi‑transparent background color using Asp... |
| [add-table-to-page](./add-table-to-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Demonstrates how to create a table and insert it into a specific page of a PDF using Aspose.Pdf f... |
| [add-text-to-table-cell](./add-text-to-table-cell.cs) | Add Text with Font and Size to Table Cell | `Document`, `Page`, `Table` | Demonstrates how to create a TextFragment, set its font and size, and insert it into a table cell... |
| [adjust-table-column-widths](./adjust-table-column-widths.cs) | Adjust Table Column Widths Proportionally in PDF | `Document`, `Table`, `Row` | Shows how to compute total column width, convert each width to a percentage, and assign those per... |
| [alternating-row-background-colors](./alternating-row-background-colors.cs) | Apply Alternating Row Background Colors in PDF Table | `Document`, `Save`, `Page` | Creates a PDF document with a table and demonstrates how to set alternating background colors for... |
| [apply-autofitbehavior-tables](./apply-autofitbehavior-tables.cs) | Apply Different AutoFitBehavior Settings to Tables in a PDF | `Document`, `Page`, `Table` | Demonstrates how to set distinct AutoFitBehavior values for separate tables in the same PDF docum... |
| [apply-conditional-formatting-to-pdf-table-cells](./apply-conditional-formatting-to-pdf-table-cells.cs) | Apply Conditional Formatting to PDF Table Cells | `Document`, `Save`, `Page` | Demonstrates how to color PDF table cells based on numeric values using Aspose.Pdf, applying red,... |
| [apply-double-border-to-pdf-table](./apply-double-border-to-pdf-table.cs) | Apply Double Border Style to a PDF Table | `Document`, `Table`, `GraphInfo` | Shows how to add a table to an existing PDF and set a double‑line border using GraphInfo and Bord... |
| [apply-solid-border-table](./apply-solid-border-table.cs) | Apply Solid Border to Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to set a solid border for an entire table using Aspose.Pdf for .NET. |
| [auto-adjust-row-height-in-pdf-table](./auto-adjust-row-height-in-pdf-table.cs) | Auto‑adjust Row Height in PDF Table | `Document`, `Table`, `Row` | Loads an existing PDF, adds a table, sets the row's MinRowHeight to 0 to enable automatic height ... |
| [auto-fit-table-columns-after-importing-data](./auto-fit-table-columns-after-importing-data.cs) | Auto‑Fit Table Columns After Importing Data | `Document`, `Table`, `ImportDataTable` | Shows how to load a PDF template, import a DataTable into an Aspose.Pdf.Table, automatically adju... |
| [auto-fit-table-columns](./auto-fit-table-columns.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Table`, `ColumnAdjustment` | Demonstrates setting ColumnAdjustment to AutoFitToContent so that table columns automatically res... |
| [batch-add-logo-table](./batch-add-logo-table.cs) | Batch Add Company Logo Table to PDFs | `Document`, `Table`, `Image` | Processes all PDF files in a folder, adds a two‑column table containing a company logo and name t... |
| [batch-replace-tables](./batch-replace-tables.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `TableAbsorber`, `AbsorbedTable` | Demonstrates how to locate tables in several PDF files using TableAbsorber and replace each with ... |
| [calculate-remaining-page-space](./calculate-remaining-page-space.cs) | Calculate Remaining Page Space Before Adding a Table | `Document`, `CalculateContentBBox`, `PureHeight` | Demonstrates how to compute the available vertical space on a PDF page by subtracting margins and... |
| [center-align-table](./center-align-table.cs) | Center Align Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to center a Table on a PDF page by setting its HorizontalAlignment property. |
| [check-table-broken](./check-table-broken.cs) | Check if Table Breaks Across Pages | `Document`, `Page`, `Table` | Creates a PDF with a large table, adds it to a page, and checks the Table.IsBroken property to de... |
| [count-tables-in-pdf-using-tableabsorber](./count-tables-in-pdf-using-tableabsorber.cs) | Count Tables in a PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Demonstrates how to use Aspose.Pdf's TableAbsorber to detect and count tables in a PDF document. |
| [create-modify-retrieve-table](./create-modify-retrieve-table.cs) | Create, Modify, and Retrieve Tables in PDF | `Document`, `Table`, `Row` | Demonstrates how to create a table in a PDF, save it, then extract and modify its content using T... |
| [create-pdf-table-shadow-effect-limit](./create-pdf-table-shadow-effect-limit.cs) | Create PDF Table and Explain Shadow Effect Limitation | `Document`, `Page`, `Table` | Demonstrates how to build a formatted table in a PDF using Aspose.Pdf and notes that the Table cl... |
| [create-pdf-table-with-dynamic-row-count](./create-pdf-table-with-dynamic-row-count.cs) | Create PDF Table with Dynamic Row Count | `Document`, `Table`, `Page` | Shows how to load a PDF, determine the number of rows in a DataTable, import the data into an Asp... |
| ... | | | *and 74 more files* |

## Category Statistics
- Total examples: 104

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
Updated: 2026-04-07 | Run: `20260407_184544_d6f44f`
<!-- AUTOGENERATED:END -->

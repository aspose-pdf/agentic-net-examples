---
name: working-with-tables
description: C# examples for working-with-tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-tables

> **Working with tables** in PDF using C# / .NET -- **99** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-tables** category.
This folder contains standalone C# examples for working-with-tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (98/99 files) ← category-specific
- `using Aspose.Pdf.Text;` (62/99 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (24/99 files)
- `using Aspose.Pdf.Tagged;` (24/99 files)
- `using Aspose.Pdf.Annotations;` (4/99 files)
- `using Aspose.Pdf.Drawing;` (4/99 files)
- `using Aspose.Pdf.Forms;` (3/99 files)
- `using Aspose.Pdf.Operators;` (1/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (90/99 files)
- `using System.Data;` (10/99 files)
- `using System.Linq;` (9/99 files)
- `using System.Collections.Generic;` (8/99 files)
- `using System.Globalization;` (2/99 files)
- `using System.Xml.Linq;` (2/99 files)
- `using System.Runtime.InteropServices;` (1/99 files)
- `using System.Text.Json;` (1/99 files)

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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a tagged PDF table with Aspose.Pdf and fill the first column with sequential ... |
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Add Centered Paragraph to Table Cell in PDF | `Document`, `Table`, `Row` | Demonstrates how to create a table in a PDF, add a cell, and place a centered paragraph (TextFrag... |
| [add-checkbox-form-field-in-table-cell](./add-checkbox-form-field-in-table-cell.cs) | Add Checkbox Form Field to a Table Cell | `Document`, `ITaggedContent`, `TableElement` | Demonstrates creating a tagged table in a PDF and inserting a CheckboxField form field into a spe... |
| [add-footnote-references-to-table-cells](./add-footnote-references-to-table-cells.cs) | Add Footnote References to Table Cells in PDF | `Document`, `Page`, `Table` | Shows how to insert superscript footnote numbers inside table cells and add matching footnote par... |
| [add-list-items-in-table-cell](./add-list-items-in-table-cell.cs) | Add List Inside Table Cell in PDF | `Document`, `ITaggedContent`, `TableElement` | Shows how to use Aspose.Pdf's TaggedContent API to create a table, insert a list element inside a... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to Table Cell | `Document`, `Page`, `Table` | Shows how to create a PDF with a table and insert multiline text into a cell by adding separate T... |
| [add-numbered-list-in-table-cell](./add-numbered-list-in-table-cell.cs) | Add Numbered List Inside a Table Cell (Tagged PDF) | `Document`, `ITaggedContent`, `StructureElement` | Shows how to use Aspose.Pdf's TaggedContent API to create a table in a PDF and insert an ordered ... |
| [add-radio-button-group-in-table-cell](./add-radio-button-group-in-table-cell.cs) | Add Radio Button Group Inside a Table Cell | `Document`, `Page`, `Table` | Shows how to place a group of radio button fields within a PDF table cell using Aspose.Pdf by cre... |
| [add-shadow-effect-to-table](./add-shadow-effect-to-table.cs) | Add Shadow Effect to a Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to create a table in a PDF and simulate a shadow effect using a Graph container ... |
| [add-solid-border-to-pdf-table](./add-solid-border-to-pdf-table.cs) | Apply Solid Border to PDF Table | `Document`, `Table`, `BorderInfo` | Shows how to create a table in a PDF document and set a solid black border around the entire tabl... |
| [add-table-footer-to-pdf](./add-table-footer-to-pdf.cs) | Add Footer Row to Tagged PDF Table with Automatic Page Repet... | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a table in a tagged PDF, add a footer row, and rely on Aspose.Pdf to r... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to insert a table into a chosen page of a PDF by accessing Document.Pages and adding th... |
| [add-table-with-semi-transparent-background](./add-table-with-semi-transparent-background.cs) | Add Semi-Transparent Background to Tagged PDF Table | `Document`, `ITaggedContent`, `TableElement` | Demonstrates how to create a tagged PDF table with Aspose.Pdf, set a semi‑transparent background ... |
| [add-text-fragment-to-table-cell](./add-text-fragment-to-table-cell.cs) | Add Text with Font and Size to Table Cell | `Document`, `Page`, `Table` | Demonstrates how to insert a TextFragment into a table cell and set its font and size using Aspos... |
| [adjust-table-column-widths-proportionally](./adjust-table-column-widths-proportionally.cs) | Adjust Table Column Widths Proportionally in PDF | `Document`, `ITaggedContent`, `StructureElement` | The example loads a PDF, creates a tagged table, converts absolute column widths to percentage va... |
| [apply-different-autofit-behaviors-to-tables](./apply-different-autofit-behaviors-to-tables.cs) | Apply Different AutoFitBehavior Settings to Tables in a PDF | `Document`, `Table`, `Row` | The example loads an existing PDF, creates two tables with different ColumnAdjustment settings (A... |
| [apply-double-border-to-pdf-table](./apply-double-border-to-pdf-table.cs) | Create Table with Double Border in PDF | `Document`, `ITaggedContent`, `TableElement` | Loads an existing PDF, creates a tagged table, applies a double‑like border using BorderInfo, add... |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Loads an existing PDF, creates a table with header and data rows, applies ColumnAdjustment.AutoFi... |
| [auto-fit-table-row-height](./auto-fit-table-row-height.cs) | Auto‑Fit Table Row Height in PDF | `Document`, `Table`, `Row` | Demonstrates how to create a PDF with a table whose rows automatically adjust their height to fit... |
| [batch-add-table-with-logo-to-pdfs](./batch-add-table-with-logo-to-pdfs.cs) | Batch Add Table with Company Logo to PDFs | `Document`, `Page`, `Table` | Demonstrates how to iterate through a folder of PDF files, insert a two‑column table containing a... |
| [calculate-remaining-page-space-add-table](./calculate-remaining-page-space-add-table.cs) | Calculate Remaining Page Space and Add Table to PDF | `Document`, `Page`, `TextFragment` | Loads a PDF, computes the vertical space left on a page after accounting for margins and existing... |
| [center-table-horizontally](./center-table-horizontally.cs) | Center Table Horizontally in PDF | `Document`, `Page`, `Table` | Shows how to create a PDF document with a table and align the table to the center of the page usi... |
| [check-table-break-across-pages](./check-table-break-across-pages.cs) | Determine If a PDF Table Is Broken Across Pages | `Document`, `ITaggedContent`, `TableElement` | Loads a PDF, verifies it contains tagged content, finds the first table element in the structure ... |
| [conditional-formatting-table-cells](./conditional-formatting-table-cells.cs) | Conditional Formatting of PDF Table Cells | `Document`, `Page`, `Table` | Demonstrates how to create a table in a PDF, iterate through data rows, and apply background colo... |
| [count-tables-in-pdf](./count-tables-in-pdf.cs) | Count Tables in PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Shows how to load a PDF with Aspose.Pdf, apply TableAbsorber to detect tables, and retrieve the n... |
| [create-fixed-width-table-pdf](./create-fixed-width-table-pdf.cs) | Create Fixed-Width Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to generate a PDF with a table whose width is fixed to 500 points using Aspose.Pdf. |
| [create-pdf-report-multiple-tables](./create-pdf-report-multiple-tables.cs) | Generate PDF Report with Multiple DataTables | `Document`, `Page`, `TextFragment` | Shows how to create a PDF document using Aspose.Pdf, add separate pages for each DataTable, forma... |
| [create-pdf-table-alternating-row-colors](./create-pdf-table-alternating-row-colors.cs) | Apply Alternating Row Background Colors to PDF Table | `Document`, `Table`, `Row` | Shows how to load a PDF, locate the first table, and set alternating background colors for each r... |
| [create-pdf-table-auto-fit-columns](./create-pdf-table-auto-fit-columns.cs) | Create PDF Table with Auto‑Fit Columns | `Document`, `Page`, `Table` | Demonstrates how to generate a PDF, add a table, enable auto‑fit to content for columns, populate... |
| [create-pdf-table-dynamic-row-count](./create-pdf-table-dynamic-row-count.cs) | Create PDF Table with Dynamic Row Count | `Document`, `Page`, `Table` | Demonstrates how to generate a PDF containing a table whose number of rows is determined at runti... |
| ... | | | *and 69 more files* |

## Category Statistics
- Total examples: 99

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
Updated: 2026-10-22 | Run: `20261022_020804_46c201`
<!-- AUTOGENERATED:END -->

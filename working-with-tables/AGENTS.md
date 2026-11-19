---
name: working-with-tables
description: C# examples for working-with-tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-tables

> **Working with tables** in PDF using C# / .NET -- **99** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-tables** category.
This folder contains standalone C# examples for working-with-tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (97/99 files) ← category-specific
- `using Aspose.Pdf.Text;` (54/99 files) ← category-specific
- `using Aspose.Pdf.LogicalStructure;` (29/99 files)
- `using Aspose.Pdf.Tagged;` (28/99 files)
- `using Aspose.Pdf.Drawing;` (5/99 files)
- `using Aspose.Pdf.Annotations;` (2/99 files)
- `using Aspose.Pdf.Forms;` (2/99 files)
- `using System;` (99/99 files)
- `using System.IO;` (92/99 files)
- `using System.Data;` (9/99 files)
- `using System.Collections.Generic;` (8/99 files)
- `using System.Linq;` (6/99 files)
- `using System.Reflection;` (2/99 files)
- `using System.Xml.Linq;` (2/99 files)
- `using System.Collections;` (1/99 files)
- `using System.Text;` (1/99 files)
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
| [add-auto-numbered-column-to-pdf-table](./add-auto-numbered-column-to-pdf-table.cs) | Add Auto‑Numbered Column to a PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Loads an existing PDF, creates a tagged table, and populates it with data while automatically num... |
| [add-centered-paragraph-to-table-cell](./add-centered-paragraph-to-table-cell.cs) | Add Centered Text to a PDF Table Cell | `Document`, `Page`, `Table` | Demonstrates loading an existing PDF, creating a table, adding a cell, and inserting a centered p... |
| [add-checkbox-form-field-in-table-cell](./add-checkbox-form-field-in-table-cell.cs) | Add Checkbox Form Field Inside a Table Cell | `Document`, `Page`, `StructureElement` | Demonstrates how to create a tagged PDF table and place a CheckboxField form element inside one o... |
| [add-footnote-references-to-table-cells](./add-footnote-references-to-table-cells.cs) | Add Footnote References to Table Cells in a Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | The example loads an existing PDF, creates a tagged table, inserts superscript footnote markers i... |
| [add-list-items-in-table-cell](./add-list-items-in-table-cell.cs) | Add List to Table Cell in Tagged PDF | `Document`, `ITaggedContent`, `TableElement` | Shows how to create a tagged PDF table and insert a bullet list inside a table cell using Aspose.... |
| [add-multiline-text-to-table-cell](./add-multiline-text-to-table-cell.cs) | Add Multiline Text to a Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates creating a table in a PDF, adding a header row, and inserting multiline text into a ... |
| [add-numbered-list-in-table-cell](./add-numbered-list-in-table-cell.cs) | Create Numbered List Inside a Table Cell | `Document`, `ITaggedContent`, `StructureElement` | Shows how to add a numbered list to a table cell by creating paragraph elements with list numbers... |
| [add-radio-button-group-in-table-cell](./add-radio-button-group-in-table-cell.cs) | Add Radio Button Group Inside Table Cell | `Document`, `Page`, `Table` | Demonstrates creating a radio‑button group with Aspose.Pdf.Forms.RadioButtonField, adding options... |
| [add-shadow-effect-to-table](./add-shadow-effect-to-table.cs) | Add Shadow Effect to Table in PDF | `Document`, `Page`, `Table` | Shows how to simulate a shadow for a table by drawing a semi‑transparent FloatingBox behind the t... |
| [add-solid-border-to-pdf-table](./add-solid-border-to-pdf-table.cs) | Apply Solid Border to PDF Table | `Document`, `Page`, `Table` | Creates a PDF document, adds a table, and sets a solid 1‑point border around the entire table usi... |
| [add-table-footer-to-pdf](./add-table-footer-to-pdf.cs) | Add Footer Row to Tagged PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Demonstrates how to create a table with header, body, and a footer row using the Tagged Content A... |
| [add-table-to-specific-pdf-page](./add-table-to-specific-pdf-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Shows how to insert a table into a chosen page of a PDF by accessing the page via its index and a... |
| [add-table-with-background-color](./add-table-with-background-color.cs) | Add Gradient Background to PDF Table | `Document`, `Page`, `Table` | Shows how to create a table in a PDF and apply a linear gradient brush to its background using As... |
| [add-table-with-semi-transparent-background](./add-table-with-semi-transparent-background.cs) | Add Background Color with Opacity to an Accessible PDF Table | `Document`, `ITaggedContent`, `TableElement` | Demonstrates creating an accessible table in a PDF using Aspose.Pdf's tagged content API and appl... |
| [add-text-fragment-to-table-cell](./add-text-fragment-to-table-cell.cs) | Add Text with Specific Font to a PDF Table Cell | `Document`, `Page`, `Table` | Shows how to create a table in a PDF, insert a TextFragment with a chosen font and size into a ce... |
| [adjust-table-column-widths-proportionally](./adjust-table-column-widths-proportionally.cs) | Set Proportional Column Widths in a Tagged PDF Table | `Document`, `ITaggedContent`, `StructureElement` | Shows how to calculate column widths as percentages of the total width and apply them to a TableE... |
| [apply-different-autofit-behaviors-to-tables](./apply-different-autofit-behaviors-to-tables.cs) | Apply Different AutoFitBehavior Settings to Tables in a PDF | `Document`, `Page`, `Table` | Demonstrates how to set distinct ColumnAdjustment (AutoFitToContent and AutoFitToWindow) for sepa... |
| [apply-double-border-to-pdf-table](./apply-double-border-to-pdf-table.cs) | Add Double Border Table to Tagged PDF | `Document`, `ITaggedContent`, `StructureElement` | The sample opens an existing PDF, enables tagged content, creates a table element with a double‑l... |
| [auto-fit-table-columns-to-content](./auto-fit-table-columns-to-content.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Demonstrates how to set ColumnAdjustment to AutoFitToContent on an Aspose.Pdf.Table so that colum... |
| [auto-fit-table-row-height](./auto-fit-table-row-height.cs) | Auto‑Fit Table Row Height in PDF | `Document`, `Table`, `Row` | Demonstrates how to let a table row automatically adjust its height to fit wrapped content by usi... |
| [batch-add-table-with-logo-to-pdfs](./batch-add-table-with-logo-to-pdfs.cs) | Add Company Logo Table to PDFs in Batch | `Document`, `Table`, `Row` | Shows how to iterate through a folder of PDF files, insert a table containing a logo image and co... |
| [batch-replace-tables-in-multiple-pdfs](./batch-replace-tables-in-multiple-pdfs.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `Page`, `TableAbsorber` | Demonstrates how to iterate through a folder of PDF files, locate each table on every page using ... |
| [calculate-remaining-page-space-add-table](./calculate-remaining-page-space-add-table.cs) | Add Table Within Remaining Page Space in a PDF | `Document`, `Page`, `PageInfo` | Demonstrates how to compute the usable vertical space on a PDF page after accounting for margins ... |
| [center-table-horizontally](./center-table-horizontally.cs) | Center Align Table in PDF | `Document`, `Page`, `Table` | Shows how to create a PDF document, add a table, and horizontally center the table using Aspose.P... |
| [check-table-break-across-pages](./check-table-break-across-pages.cs) | Check If a PDF Table Breaks Across Pages | `Document`, `ITaggedContent`, `TableElement` | Loads a PDF, accesses its tagged content, finds the first table element and uses the TableElement... |
| [conditional-formatting-table-cells](./conditional-formatting-table-cells.cs) | Conditional Formatting of Table Cells in PDF | `Document`, `ITaggedContent`, `StructureElement` | Shows how to create a tagged PDF table with Aspose.Pdf and apply conditional formatting by settin... |
| [count-tables-in-pdf](./count-tables-in-pdf.cs) | Count Tables in a PDF using TableAbsorber | `Document`, `TableAbsorber`, `Visit` | Shows how to load a PDF with Aspose.Pdf, apply TableAbsorber to extract table structures, and obt... |
| [create-fixed-width-table-pdf](./create-fixed-width-table-pdf.cs) | Create Fixed-Width Table in PDF | `Document`, `Page`, `Table` | Shows how to set a table's total width to 500 points by using the ColumnWidths property and add t... |
| [create-pdf-report-multiple-tables](./create-pdf-report-multiple-tables.cs) | Create PDF Report with Multiple Tables | `Document`, `Page`, `TextFragment` | Shows how to generate a PDF document using Aspose.Pdf, add titled tables from DataTable objects o... |
| [create-pdf-table-alternating-row-colors](./create-pdf-table-alternating-row-colors.cs) | Apply Alternating Row Background Colors to PDF Table | `Document`, `ITaggedContent`, `TableElement` | The example loads a tagged PDF, locates the first table, and sets alternating background colors f... |
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
Updated: 2026-11-18 | Run: `20261118_211318_ae40c2`
<!-- AUTOGENERATED:END -->

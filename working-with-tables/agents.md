---
name: Working with Tables
description: C# examples for Working with Tables using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Working with Tables

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Working with Tables** category.
This folder contains standalone C# examples for Working with Tables operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Working with Tables**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (100/100 files) ← category-specific
- `using Aspose.Pdf.Text;` (76/100 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (11/100 files)
- `using Aspose.Pdf.LogicalStructure;` (5/100 files)
- `using Aspose.Pdf.Tagged;` (5/100 files)
- `using Aspose.Pdf.Facades;` (3/100 files)
- `using Aspose.Pdf.Annotations;` (2/100 files)
- `using Aspose.Pdf.Forms;` (2/100 files)
- `using System;` (100/100 files)
- `using System.Runtime.InteropServices;` (62/100 files) ← category-specific
- `using System.IO;` (53/100 files)
- `using System.Data;` (13/100 files)
- `using System.Collections.Generic;` (8/100 files)
- `using System.Linq;` (6/100 files)
- `using System.Drawing;` (1/100 files)
- `using System.Globalization;` (1/100 files)
- `using System.Text;` (1/100 files)
- `using System.Text.Json;` (1/100 files)

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
| [add-a-background-color-to-the-table-by-setting-tab...](./add-a-background-color-to-the-table-by-setting-table.backgroundcolor-to-a-color-with-desired-opacity.cs) | Add A Background Color To The Table By Setting Table.Backgro... |  | Add A Background Color To The Table By Setting Table.Backgroundcolor To A Color With Desired Opacity |
| [add-a-cell-containing-multiline-text-by-inserting-...](./add-a-cell-containing-multiline-text-by-inserting-multiple-textfragment-objects-separated-by-line-break-fragments.cs) | Add A Cell Containing Multiline Text By Inserting Multiple T... | `TextFragment` | Add A Cell Containing Multiline Text By Inserting Multiple Textfragment Objects Separated By Line... |
| [add-a-checkbox-form-field-inside-a-cell-by-creatin...](./add-a-checkbox-form-field-inside-a-cell-by-creating-a-checkboxfield-and-placing-it-in-the-cell.cs) | Add A Checkbox Form Field Inside A Cell By Creating A Checkb... | `TextFragment` | Add A Checkbox Form Field Inside A Cell By Creating A Checkboxfield And Placing It In The Cell |
| [add-a-footer-row-to-the-table-and-configure-it-to-...](./add-a-footer-row-to-the-table-and-configure-it-to-appear-at-the-bottom-of-each-page.cs) | Add A Footer Row To The Table And Configure It To Appear At ... |  | Add A Footer Row To The Table And Configure It To Appear At The Bottom Of Each Page |
| [add-a-table-after-rotating-the-page-by-applying-a-...](./add-a-table-after-rotating-the-page-by-applying-a-rotation-transformation-to-page-before-inserting-the-table.cs) | Add A Table After Rotating The Page By Applying A Rotation T... | `BorderInfo`, `MarginInfo`, `TextFragment` | Add A Table After Rotating The Page By Applying A Rotation Transformation To Page Before Insertin... |
| [add-a-table-inside-a-form-field-by-creating-a-text...](./add-a-table-inside-a-form-field-by-creating-a-textboxfield-and-inserting-the-table-into-its-appearance.cs) | Add A Table Inside A Form Field By Creating A Textboxfield A... | `Rectangle` | Add A Table Inside A Form Field By Creating A Textboxfield And Inserting The Table Into Its Appea... |
| [add-a-table-inside-a-paragraph-by-adding-a-paragra...](./add-a-table-inside-a-paragraph-by-adding-a-paragraph-inserting-table-then-placing-it-on-the-page.cs) | Add A Table Inside A Paragraph By Adding A Paragraph Inserti... | `TextFragment` | Add A Table Inside A Paragraph By Adding A Paragraph Inserting Table Then Placing It On The Page |
| [add-an-auto-numbered-column-to-a-table-by-insertin...](./add-an-auto-numbered-column-to-a-table-by-inserting-sequential-numbers-into-the-first-cell-of-each-row.cs) | Add An Auto Numbered Column To A Table By Inserting Sequenti... | `BorderInfo`, `MarginInfo`, `TextFragment` | Add An Auto Numbered Column To A Table By Inserting Sequential Numbers Into The First Cell Of Eac... |
| [add-footnote-references-inside-cells-by-inserting-...](./add-footnote-references-inside-cells-by-inserting-superscript-numbers-and-linking-them-to-footnote-paragraphs-a-372636c8.cs) | Add Footnote References Inside Cells By Inserting Superscrip... | `BorderInfo`, `TextFragment` | Add Footnote References Inside Cells By Inserting Superscript Numbers And Linking Them To Footnot... |
| [add-hyperlink-to-table-cell](./add-hyperlink-to-table-cell.cs) | Add Hyperlink to Table Cell in PDF | `Document`, `Page`, `Table` | Demonstrates how to insert a web hyperlink into a table cell by creating a LinkAnnotation and add... |
| [add-table-to-page](./add-table-to-page.cs) | Add Table to Specific PDF Page | `Document`, `Page`, `Table` | Demonstrates how to create a table and insert it into a specific page of a PDF using Aspose.Pdf f... |
| [add-text-to-table-cell](./add-text-to-table-cell.cs) | Add Text with Font and Size to Table Cell | `Document`, `Page`, `Table` | Demonstrates how to create a TextFragment, set its font and size, and insert it into a table cell... |
| [adjust-column-widths-proportionally-by-calculating...](./adjust-column-widths-proportionally-by-calculating-total-width-and-assigning-each-column-a-percentage-of-that-total.cs) | Adjust Column Widths Proportionally By Calculating Total Wid... | `TextFragment` | Adjust Column Widths Proportionally By Calculating Total Width And Assigning Each Column A Percen... |
| [align-a-table-to-the-right-margin-by-setting-table...](./align-a-table-to-the-right-margin-by-setting-table.horizontalalignment-to-right-and-adjusting-its-left-margin.cs) | Align A Table To The Right Margin By Setting Table.Horizonta... | `TextFragment` | Align A Table To The Right Margin By Setting Table.Horizontalalignment To Right And Adjusting Its... |
| [allow-a-row-to-automatically-adjust-its-height-to-...](./allow-a-row-to-automatically-adjust-its-height-to-fit-content-by-setting-row.height-to-autofit.cs) | Allow A Row To Automatically Adjust Its Height To Fit Conten... | `TextFragment` | Allow A Row To Automatically Adjust Its Height To Fit Content By Setting Row.Height To Autofit |
| [apply-alternating-row-background-colors-by-iterati...](./apply-alternating-row-background-colors-by-iterating-rows-and-setting-cell.backgroundcolor-based-on-row-index-parity.cs) | Apply Alternating Row Background Colors By Iterating Rows An... | `BorderInfo`, `TextFragment` | Apply Alternating Row Background Colors By Iterating Rows And Setting Cell.Backgroundcolor Based ... |
| [apply-autofitbehavior-tables](./apply-autofitbehavior-tables.cs) | Apply Different AutoFitBehavior Settings to Tables in a PDF | `Document`, `Page`, `Table` | Demonstrates how to set distinct AutoFitBehavior values for separate tables in the same PDF docum... |
| [apply-conditional-formatting-to-cells-based-on-the...](./apply-conditional-formatting-to-cells-based-on-their-numeric-values-by-setting-background-color-when-thresholds-d7ec2321.cs) | Apply Conditional Formatting To Cells Based On Their Numeric... |  | Apply Conditional Formatting To Cells Based On Their Numeric Values By Setting Background Color W... |
| [apply-solid-border-table](./apply-solid-border-table.cs) | Apply Solid Border to Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to set a solid border for an entire table using Aspose.Pdf for .NET. |
| [auto-fit-table-columns](./auto-fit-table-columns.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Page`, `Table` | Demonstrates how to set ColumnAdjustment.AutoFitToContent on a Table so that column widths automa... |
| [auto-fit-table-columns__v2](./auto-fit-table-columns__v2.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Table`, `ColumnAdjustment` | Demonstrates setting ColumnAdjustment to AutoFitToContent so that table columns automatically res... |
| [auto-fit-table-columns__v3](./auto-fit-table-columns__v3.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Table`, `ColumnAdjustment` | Demonstrates setting ColumnAdjustment to AutoFitToContent so that table columns automatically res... |
| [auto-fit-table-columns__v4](./auto-fit-table-columns__v4.cs) | AutoFit Table Columns to Content in PDF | `Document`, `Table`, `ColumnAdjustment` | Demonstrates setting ColumnAdjustment to AutoFitToContent so that table columns automatically res... |
| [batch-add-logo-table](./batch-add-logo-table.cs) | Batch Add Company Logo Table to PDFs | `Document`, `Table`, `Image` | Processes all PDF files in a folder, adds a two‑column table containing a company logo and name t... |
| [batch-replace-tables](./batch-replace-tables.cs) | Batch Replace Tables in Multiple PDFs | `Document`, `TableAbsorber`, `AbsorbedTable` | Demonstrates how to locate tables in several PDF files using TableAbsorber and replace each with ... |
| [calculate-remaining-page-space](./calculate-remaining-page-space.cs) | Calculate Remaining Page Space Before Adding a Table | `Document`, `CalculateContentBBox`, `PureHeight` | Demonstrates how to compute the available vertical space on a PDF page by subtracting margins and... |
| [center-align-table](./center-align-table.cs) | Center Align Table in PDF | `Document`, `Page`, `Table` | Demonstrates how to center a Table on a PDF page by setting its HorizontalAlignment property. |
| [change-page-orientation-to-landscape-before-render...](./change-page-orientation-to-landscape-before-rendering-a-wide-table-to-ensure-it-fits-within-page-boundaries.cs) | Change Page Orientation To Landscape Before Rendering A Wide... | `BorderInfo`, `MarginInfo` | Change Page Orientation To Landscape Before Rendering A Wide Table To Ensure It Fits Within Page ... |
| [check-table-broken](./check-table-broken.cs) | Check if Table Breaks Across Pages | `Document`, `Page`, `Table` | Creates a PDF with a large table, adds it to a page, and checks the Table.IsBroken property to de... |
| [create-a-header-row-and-set-its-isheader-property-...](./create-a-header-row-and-set-its-isheader-property-to-true-so-it-repeats-on-each-new-page.cs) | Create A Header Row And Set Its Isheader Property To True So... | `TextFragment` | Create A Header Row And Set Its Isheader Property To True So It Repeats On Each New Page |
| ... | | | *and 70 more files* |

## Category Statistics
- Total examples: 100

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
- Review code examples in this folder for Working with Tables patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-02 | Run: `20260402_163244_5398f3`
<!-- AUTOGENERATED:END -->

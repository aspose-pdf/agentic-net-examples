using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber, AbsorbedTable, AbsorbedCell, AbsorbedRow

class ReplaceMergedCell
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF – wrap in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Process each page separately
            foreach (Page page in doc.Pages)
            {
                // Absorb tables on the current page using the current API (Visit)
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Work on a copy of the TableList to avoid collection modification issues
                var tablesCopy = new List<AbsorbedTable>(absorber.TableList);

                foreach (AbsorbedTable oldTable in tablesCopy)
                {
                    // Build a new Table that will replace the old one
                    Table newTable = new Table();

                    // NOTE: Column widths are not directly exposed via AbsorbedTable in recent versions.
                    // For the purpose of splitting merged cells we rely on the default column handling.

                    // Iterate rows of the absorbed table using RowList
                    foreach (AbsorbedRow absorbedRow in oldTable.RowList)
                    {
                        // Add a new row to the replacement table
                        Row newRow = newTable.Rows.Add();

                        // Iterate cells in the absorbed row using CellList
                        foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                        {
                            // In newer Aspose.Pdf versions AbsorbedCell does not expose RowSpan.
                            // We treat missing RowSpan as 1 (i.e., only column merges are handled).
                            int colSpan = Math.Max(absorbedCell.ColSpan, 1);
                            int rowSpan = 1; // fallback because RowSpan property is unavailable

                            bool isMerged = colSpan > 1 || rowSpan > 1;

                            if (isMerged)
                            {
                                // Extract the first text fragment (if any) to reuse as content
                                string cellText = string.Empty;
                                if (absorbedCell.TextFragments != null && absorbedCell.TextFragments.Count > 0)
                                    cellText = absorbedCell.TextFragments[0].Text;

                                // Create cells for the merged area
                                for (int r = 0; r < rowSpan; r++)
                                {
                                    // Ensure the target row exists (add empty rows if needed)
                                    Row targetRow;
                                    if (r == 0)
                                    {
                                        targetRow = newRow; // first row already created
                                    }
                                    else
                                    {
                                        // Add additional rows below the current one
                                        targetRow = newTable.Rows.Add();
                                    }

                                    for (int c = 0; c < colSpan; c++)
                                    {
                                        Cell splitCell = targetRow.Cells.Add();
                                        splitCell.ColSpan = 1;
                                        splitCell.RowSpan = 1;

                                        // Add the extracted text only to the top‑left cell
                                        if (r == 0 && c == 0 && !string.IsNullOrEmpty(cellText))
                                        {
                                            splitCell.Paragraphs.Add(new TextFragment(cellText));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Normal (non‑merged) cell – copy its content
                                Cell newCell = newRow.Cells.Add();

                                if (absorbedCell.TextFragments != null && absorbedCell.TextFragments.Count > 0)
                                {
                                    string txt = absorbedCell.TextFragments[0].Text;
                                    newCell.Paragraphs.Add(new TextFragment(txt));
                                }

                                // Preserve explicit spans (should be 1)
                                newCell.ColSpan = absorbedCell.ColSpan;
                                // RowSpan property does not exist; default is 1
                                newCell.RowSpan = 1;
                            }
                        }
                    }

                    // Insert the newly built table into the page.
                    // The original Table reference is not exposed via AbsorbedTable in recent versions,
                    // so we simply add the new table. It will be rendered on top of the original content.
                    page.Paragraphs.Add(newTable);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Merged cells replaced and saved to '{outputPath}'.");
    }
}

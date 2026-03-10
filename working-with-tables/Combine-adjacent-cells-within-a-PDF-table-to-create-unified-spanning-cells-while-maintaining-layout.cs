using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment, TableAbsorber, AbsorbedTable, AbsorbedRow, AbsorbedCell

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "merged_cells.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Find tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each detected table
                foreach (var absorbedTable in absorber.TableList)
                {
                    // Create a new Aspose.Pdf.Table that will replace the absorbed one
                    Table newTable = new Table();

                    // NOTE: AbsorbedTable does not expose layout properties such as ColumnAdjustment,
                    // ColumnWidths or IsBordersIncluded. If you need to preserve them, you must
                    // retrieve the information from the original Table (if you have it) or set them
                    // manually. For the purpose of merging cells we can omit these assignments.

                    // Iterate rows of the absorbed table
                    foreach (AbsorbedRow absorbedRow in absorbedTable.RowList)
                    {
                        Row newRow = new Row();

                        int cellIdx = 0;
                        while (cellIdx < absorbedRow.CellList.Count)
                        {
                            // If there is a next cell, merge the current and next cell
                            if (cellIdx < absorbedRow.CellList.Count - 1)
                            {
                                // Create a merged cell spanning two columns
                                Cell mergedCell = new Cell
                                {
                                    ColSpan = 2
                                };

                                // Concatenate text from the two adjacent cells
                                string mergedText = GetCellText(absorbedRow.CellList[cellIdx]) + " " +
                                                    GetCellText(absorbedRow.CellList[cellIdx + 1]);

                                mergedCell.Paragraphs.Add(new TextFragment(mergedText));
                                newRow.Cells.Add(mergedCell);

                                cellIdx += 2; // skip the next cell because it has been merged
                            }
                            else
                            {
                                // Last cell without a neighbour – copy as‑is
                                Cell singleCell = new Cell();
                                singleCell.Paragraphs.Add(new TextFragment(GetCellText(absorbedRow.CellList[cellIdx])));
                                newRow.Cells.Add(singleCell);
                                cellIdx++;
                            }
                        }

                        newTable.Rows.Add(newRow);
                    }

                    // Replace the original absorbed table with the newly built table
                    absorber.Replace(page, absorbedTable, newTable);
                }
            }

            // Save the modified document (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }

    // Helper: extracts concatenated text from an AbsorbedCell
    private static string GetCellText(AbsorbedCell cell)
    {
        if (cell == null) return string.Empty;
        return string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
    }
}

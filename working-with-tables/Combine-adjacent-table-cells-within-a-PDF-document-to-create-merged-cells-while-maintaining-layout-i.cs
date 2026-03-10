using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Find tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            // If no tables were found, exit
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected on the first page.");
                doc.Save(outputPath); // still save the original document
                return;
            }

            // Process the first detected table
            var absorbedTable = absorber.TableList[0];

            // Create a new Table that will replace the absorbed one
            Table newTable = new Table();

            // NOTE: AbsorbedTable does not expose ColumnWidths, so we skip preserving them.

            // Iterate through rows of the absorbed table
            for (int r = 0; r < absorbedTable.RowList.Count; r++)
            {
                // Create a new row for the replacement table
                Row newRow = new Row();

                // Example: merge the first two cells of each row
                for (int c = 0; c < absorbedTable.RowList[r].CellList.Count; c++)
                {
                    // If this is the first cell and there is a next cell, merge them
                    if (c == 0 && absorbedTable.RowList[r].CellList.Count > 1)
                    {
                        Cell mergedCell = new Cell
                        {
                            // Horizontal merge of two adjacent cells
                            ColSpan = 2
                        };

                        // Copy text from the first absorbed cell (if any)
                        var firstAbsorbedCell = absorbedTable.RowList[r].CellList[0];
                        if (firstAbsorbedCell.TextFragments.Count > 0)
                        {
                            TextFragment tf = new TextFragment(firstAbsorbedCell.TextFragments[0].Text);
                            mergedCell.Paragraphs.Add(tf);
                        }

                        newRow.Cells.Add(mergedCell);
                        // Skip the next cell because it is merged
                        c++; // extra increment to bypass the second cell
                        continue;
                    }

                    // For all other cells, copy content without merging
                    var srcAbsorbedCell = absorbedTable.RowList[r].CellList[c];
                    Cell dstCell = new Cell();

                    // Copy the first text fragment (simple copy)
                    if (srcAbsorbedCell.TextFragments.Count > 0)
                    {
                        TextFragment tf = new TextFragment(srcAbsorbedCell.TextFragments[0].Text);
                        dstCell.Paragraphs.Add(tf);
                    }

                    // Additional formatting (background, border, etc.) can be copied here if needed
                    // using properties that exist on AbsorbedCell. For simplicity we omit them.

                    newRow.Cells.Add(dstCell);
                }

                newTable.Rows.Add(newRow);
            }

            // Replace the original absorbed table with the newly built table
            absorber.Replace(doc.Pages[1], absorbedTable, newTable);

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged cells saved to '{outputPath}'.");
    }
}

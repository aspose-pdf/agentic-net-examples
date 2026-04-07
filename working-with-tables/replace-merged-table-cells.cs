using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // required by task specification

class Program
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (adjust if needed)
            Page page = doc.Pages[1];

            // Absorb tables on the selected page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Take the first absorbed table as the target to replace
            AbsorbedTable oldTable = absorber.TableList[0];

            // Determine the maximum number of columns (taking colspans into account)
            int maxColumns = GetMaxColumnCount(oldTable);

            // Create a new Table that will replace the old one
            Table newTable = new Table
            {
                // Simple equal column widths; adjust as needed for real layouts
                ColumnWidths = string.Join(" ", Enumerable.Repeat("100", maxColumns))
            };

            // Rebuild rows and cells, splitting merged cells into separate cells
            foreach (var absorbedRow in oldTable.RowList)
            {
                Row newRow = new Row();

                foreach (var absorbedCell in absorbedRow.CellList)
                {
                    int span = absorbedCell.ColSpan;

                    // If the cell spans multiple columns, create separate cells for each column
                    if (span > 1)
                    {
                        for (int i = 0; i < span; i++)
                        {
                            Cell splitCell = new Cell();

                            // Copy text fragments from the original merged cell
                            foreach (var fragment in absorbedCell.TextFragments)
                            {
                                splitCell.Paragraphs.Add(new TextFragment(fragment.Text));
                            }

                            newRow.Cells.Add(splitCell);
                        }
                    }
                    else
                    {
                        Cell cell = new Cell();

                        foreach (var fragment in absorbedCell.TextFragments)
                        {
                            cell.Paragraphs.Add(new TextFragment(fragment.Text));
                        }

                        newRow.Cells.Add(cell);
                    }
                }

                newTable.Rows.Add(newRow);
            }

            // Replace the original table with the newly constructed one
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Helper method to compute the maximum column count in a table,
    // considering cells that have a ColSpan greater than 1.
    static int GetMaxColumnCount(AbsorbedTable table)
    {
        int max = 0;
        foreach (var row in table.RowList)
        {
            int count = 0;
            foreach (var cell in row.CellList)
            {
                count += cell.ColSpan;
            }
            if (count > max) max = count;
        }
        return max;
    }
}
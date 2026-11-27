using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber and related types are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber will detect tables on each page
            TableAbsorber absorber = new TableAbsorber();

            // Pages are 1‑based in Aspose.Pdf – iterate accordingly
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                absorber.Visit(page); // Detect tables on this page
            }

            // After visiting all pages, TableList contains the detected tables
            Console.WriteLine($"Total tables detected: {absorber.TableList.Count}");

            int tableIndex = 1;
            foreach (var table in absorber.TableList) // table is of type AbsorbedTable
            {
                int columnCount = GetColumnCount(table);
                // Use the correct property name: PageNum (not PageNumber)
                Console.WriteLine($"Table {tableIndex}: Page {table.PageNum}, Rows={table.RowList.Count}, Columns={columnCount}");

                int rowIndex = 1;
                foreach (var row in table.RowList) // row is of type AbsorbedRow
                {
                    int colIndex = 1;
                    foreach (var cell in row.CellList) // cell is of type AbsorbedCell
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments) // TextFragment collection
                        {
                            cellText += fragment.Text;
                        }
                        Console.WriteLine($"  Row {rowIndex}, Col {colIndex}: {cellText}");
                        colIndex++;
                    }
                    rowIndex++;
                }
                tableIndex++;
            }
        }
    }

    // Helper to compute the maximum number of cells in any row (i.e., column count)
    private static int GetColumnCount(AbsorbedTable table)
    {
        int max = 0;
        foreach (var row in table.RowList)
        {
            if (row.CellList.Count > max)
                max = row.CellList.Count;
        }
        return max;
    }
}

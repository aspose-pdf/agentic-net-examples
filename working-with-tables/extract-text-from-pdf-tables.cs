using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Extract tables from the whole document
            tableAbsorber.Visit(doc);

            // Iterate over each absorbed table
            for (int t = 0; t < tableAbsorber.TableList.Count; t++)
            {
                var absorbedTable = tableAbsorber.TableList[t];
                Console.WriteLine($"Table {t + 1} (Page {absorbedTable.PageNum}):");

                // Iterate over rows in the current table
                for (int r = 0; r < absorbedTable.RowList.Count; r++)
                {
                    var row = absorbedTable.RowList[r];
                    Console.WriteLine($"  Row {r + 1}:");

                    // Iterate over cells in the current row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        Console.Write($"    Cell {c + 1} text: ");

                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.WriteLine(cellText);
                    }
                }
            }
        }
    }
}
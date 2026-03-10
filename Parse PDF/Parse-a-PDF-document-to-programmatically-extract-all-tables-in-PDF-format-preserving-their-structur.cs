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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over all found tables
            int tableIndex = 0;
            foreach (var absorbedTable in absorber.TableList)
            {
                tableIndex++;
                Console.WriteLine($"--- Table {tableIndex} ---");

                // Iterate rows
                foreach (var absorbedRow in absorbedTable.RowList)
                {
                    // Iterate cells in the current row
                    foreach (var absorbedCell in absorbedRow.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in absorbedCell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Output cell text separated by a tab (preserves column layout)
                        Console.Write($"{cellText}\t");
                    }

                    // End of row
                    Console.WriteLine();
                }

                Console.WriteLine(); // Blank line after each table
            }
        }
    }
}
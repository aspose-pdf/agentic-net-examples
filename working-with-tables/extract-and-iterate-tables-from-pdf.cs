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
            // Create a TableAbsorber to search for tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each table found
            foreach (var absorbedTable in absorber.TableList)
            {
                // Output basic information about the table
                Console.WriteLine($"Table found on page {absorbedTable.PageNum}");
                Console.WriteLine($"Position: {absorbedTable.Rectangle}");

                // Iterate rows and cells to access text fragments
                for (int r = 0; r < absorbedTable.RowList.Count; r++)
                {
                    var row = absorbedTable.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        foreach (var fragment in cell.TextFragments)
                        {
                            Console.WriteLine($"Row {r + 1}, Cell {c + 1}: {fragment.Text}");
                        }
                    }
                }
            }

            // No modifications are made; saving is optional
            // doc.Save("output.pdf");
        }
    }
}

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the TableAbsorber
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate through each absorbed table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} on page {table.PageNum}:");

                // Iterate through rows of the table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate through cells of the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate text from all fragments within the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.WriteLine($"  Row {r + 1}, Cell {c + 1}: \"{cellText}\"");
                    }
                }
            }
        }
    }
}
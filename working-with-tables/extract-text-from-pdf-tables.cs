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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each absorbed table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                // Iterate over rows in the current table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells in the current row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate all text fragments inside the cell
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
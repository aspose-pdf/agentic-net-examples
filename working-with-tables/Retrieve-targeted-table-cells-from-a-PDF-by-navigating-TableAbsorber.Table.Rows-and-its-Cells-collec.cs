using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber and related types are here

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

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each discovered table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                // Iterate over rows of the current table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells of the current row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Each cell may contain multiple text fragments
                        for (int f = 0; f < cell.TextFragments.Count; f++)
                        {
                            TextFragment fragment = cell.TextFragments[f];
                            Console.WriteLine(
                                $"Table {t + 1}, Row {r + 1}, Cell {c + 1}, Fragment {f + 1}: {fragment.Text}");
                        }
                    }
                }
            }
        }
    }
}
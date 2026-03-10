using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance (default constructor)
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the first page (pages are 1‑based)
            absorber.Visit(doc.Pages[1]);

            // Iterate over all found tables
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                AbsorbedTable table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1}: Page {table.PageNum}, Rectangle {table.Rectangle}");

                // Iterate over rows of the current table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    Console.WriteLine($"  Row {r + 1}: Cells {row.CellList.Count}");

                    // Iterate over cells of the current row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        Console.WriteLine($"    Cell {c + 1}: TextFragments {cell.TextFragments.Count}");

                        // Iterate over text fragments inside the cell
                        for (int f = 0; f < cell.TextFragments.Count; f++)
                        {
                            TextFragment fragment = cell.TextFragments[f];
                            Console.WriteLine($"      Fragment {f + 1}: \"{fragment.Text}\"");
                        }
                    }
                }
            }
        }
    }
}
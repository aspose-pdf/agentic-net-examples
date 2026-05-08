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
            // Create a TableAbsorber and enable the FlowEngine to get ColSpan values
            TableAbsorber absorber = new TableAbsorber
            {
                UseFlowEngine = true
            };

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                // Iterate over rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells in the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        AbsorbedCell cell = row.CellList[c];

                        // ColSpan > 1 indicates a merged (horizontally spanned) cell
                        if (cell.ColSpan > 1)
                        {
                            Console.WriteLine(
                                $"  Merged cell detected at Table {t + 1}, Row {r + 1}, Column {c + 1} " +
                                $"(ColSpan = {cell.ColSpan})");
                        }

                        // If RowSpan were available on AbsorbedCell, you would check similarly:
                        // if (cell.RowSpan > 1) { ... }
                    }
                }
            }
        }
    }
}
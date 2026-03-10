using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each detected table and output basic information
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                var table = absorber.TableList[i];

                // Row count is directly available
                int rowCount = table.RowList.Count;

                // Column count may not be exposed in all versions; derive it from the first row if present
                int columnCount = 0;
                if (rowCount > 0)
                {
                    columnCount = table.RowList[0].CellList.Count;
                }

                Console.WriteLine($"Table {i + 1}: {rowCount} rows, {columnCount} columns");

                // Print the text content of each cell
                for (int r = 0; r < rowCount; r++)
                {
                    var row = table.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        Console.WriteLine($"  Cell[{r},{c}]: {cellText}");
                    }
                }
            }

            // Save the (unchanged) PDF to preserve the original format
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
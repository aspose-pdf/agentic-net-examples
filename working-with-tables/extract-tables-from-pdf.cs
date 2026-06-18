using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance – it will search for tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Extract tables from the whole document
            tableAbsorber.Visit(doc);

            // Iterate over all found tables
            foreach (AbsorbedTable table in tableAbsorber.TableList)
            {
                // Output basic information about each table
                Console.WriteLine($"Table found on page {table.PageNum}");
                Console.WriteLine($"  Position: LLX={table.Rectangle.LLX}, LLY={table.Rectangle.LLY}, " +
                                  $"URX={table.Rectangle.URX}, URY={table.Rectangle.URY}");

                // Iterate over rows
                for (int rowIndex = 0; rowIndex < table.RowList.Count; rowIndex++)
                {
                    var row = table.RowList[rowIndex];
                    Console.WriteLine($"  Row {rowIndex + 1} (Rect: LLX={row.Rectangle.LLX}, LLY={row.Rectangle.LLY}, " +
                                      $"URX={row.Rectangle.URX}, URY={row.Rectangle.URY})");

                    // Iterate over cells in the current row
                    for (int cellIndex = 0; cellIndex < row.CellList.Count; cellIndex++)
                    {
                        var cell = row.CellList[cellIndex];
                        Console.WriteLine($"    Cell {cellIndex + 1} (Rect: LLX={cell.Rectangle.LLX}, LLY={cell.Rectangle.LLY}, " +
                                          $"URX={cell.Rectangle.URX}, URY={cell.Rectangle.URY})");

                        // Each cell may contain multiple text fragments; output their text
                        for (int fragIndex = 0; fragIndex < cell.TextFragments.Count; fragIndex++)
                        {
                            TextFragment fragment = cell.TextFragments[fragIndex];
                            Console.WriteLine($"      TextFragment {fragIndex + 1}: \"{fragment.Text}\"");
                        }
                    }
                }

                Console.WriteLine(); // Blank line between tables
            }
        }
    }
}
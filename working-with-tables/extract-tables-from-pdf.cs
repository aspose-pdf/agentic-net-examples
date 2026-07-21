using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and (optional) output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance – it will search for tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Extract tables from the whole document
            tableAbsorber.Visit(doc);

            // Iterate over all tables that were found
            foreach (AbsorbedTable table in tableAbsorber.TableList)
            {
                // Output basic information about the table
                Console.WriteLine($"Table found on page {table.PageNum}");
                Console.WriteLine($"Table bounds: LLX={table.Rectangle.LLX}, LLY={table.Rectangle.LLY}, " +
                                  $"URX={table.Rectangle.URX}, URY={table.Rectangle.URY}");

                // Iterate through rows
                foreach (AbsorbedRow row in table.RowList)
                {
                    // Iterate through cells in the current row
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // Each cell may contain multiple text fragments; concatenate their text
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.WriteLine($"  Cell at [{cell.Rectangle.LLX}, {cell.Rectangle.LLY}] contains: \"{cellText}\"");
                    }
                }
            }

            // (Optional) Save the document unchanged – demonstrates proper save usage
            doc.Save(outputPath);
            Console.WriteLine($"Document processed and saved to '{outputPath}'.");
        }
    }
}
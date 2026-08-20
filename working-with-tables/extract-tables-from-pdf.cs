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

        // Load the PDF document (load rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance (creation rule)
            TableAbsorber absorber = new TableAbsorber();

            // Absorb tables from the entire document (Visit overload for Document)
            absorber.Visit(doc);

            // Iterate over all absorbed tables
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Output basic table information
                Console.WriteLine($"Table found on page {table.PageNum}");
                Console.WriteLine($"Location: LLX={table.Rectangle.LLX}, LLY={table.Rectangle.LLY}, URX={table.Rectangle.URX}, URY={table.Rectangle.URY}");

                // Optional: iterate rows, cells, and text fragments within the table
                foreach (AbsorbedRow row in table.RowList)
                {
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            Console.WriteLine($"  Cell text: {fragment.Text}");
                        }
                    }
                }
            }

            // Save the (unchanged) document to a new file (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
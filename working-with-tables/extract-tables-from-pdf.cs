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
            // Create a TableAbsorber instance
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over all absorbed tables
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Page number where the table was found
                int pageNumber = table.PageNum;

                // Position of the table on the page
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                Console.WriteLine($"Table found on page {pageNumber}:");
                Console.WriteLine($"  Position - LLX:{rect.LLX}, LLY:{rect.LLY}, URX:{rect.URX}, URY:{rect.URY}");

                // Optionally iterate rows and cells
                foreach (AbsorbedRow row in table.RowList)
                {
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // Each cell may contain multiple text fragments
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            Console.WriteLine($"    Cell text: \"{fragment.Text}\"");
                        }
                    }
                }
            }
        }
    }
}
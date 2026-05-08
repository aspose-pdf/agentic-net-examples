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
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over all found tables
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Output basic table information
                Console.WriteLine($"Table on page {table.PageNum}: " +
                                  $"LLX={table.Rectangle.LLX}, LLY={table.Rectangle.LLY}, " +
                                  $"URX={table.Rectangle.URX}, URY={table.Rectangle.URY}");

                // Optionally iterate rows and cells to read cell text
                foreach (AbsorbedRow row in table.RowList)
                {
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        Console.WriteLine($"  Cell text: {cellText}");
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
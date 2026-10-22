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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to extract tables
            TableAbsorber absorber = new TableAbsorber();

            // Visit each page to collect tables
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            // Iterate over each absorbed table and output its rectangle coordinates
            int tableIndex = 1;
            foreach (var table in absorber.TableList)
            {
                // Rectangle provides the table bounds (points)
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                Console.WriteLine(
                    $"Table {tableIndex}: LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
                tableIndex++;
            }
        }
    }
}

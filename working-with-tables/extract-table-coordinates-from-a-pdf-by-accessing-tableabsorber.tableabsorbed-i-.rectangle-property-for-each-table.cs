using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber resides in this namespace

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];

                // Rectangle describing the table position on its page
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                // Output the coordinates (lower‑left and upper‑right corners)
                Console.WriteLine($"Table {i + 1} on page {table.PageNum}:");
                Console.WriteLine($"  LLX = {rect.LLX}, LLY = {rect.LLY}");
                Console.WriteLine($"  URX = {rect.URX}, URY = {rect.URY}");
            }
        }
    }
}
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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over all found tables
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];
                // The Rectangle property describes the table position on the page
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                Console.WriteLine($"Table {i + 1} on page {table.PageNum}:");
                Console.WriteLine($"  Lower‑Left X: {rect.LLX}");
                Console.WriteLine($"  Lower‑Left Y: {rect.LLY}");
                Console.WriteLine($"  Upper‑Right X: {rect.URX}");
                Console.WriteLine($"  Upper‑Right Y: {rect.URY}");
                Console.WriteLine();
            }
        }
    }
}
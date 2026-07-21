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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages
            absorber.Visit(doc);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables were detected in the document.");
                return;
            }

            // Iterate over each detected table and output its rectangle coordinates
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                Console.WriteLine($"Table {i + 1} (Page {table.PageNum}):");
                Console.WriteLine($"  LLX = {rect.LLX}, LLY = {rect.LLY}, URX = {rect.URX}, URY = {rect.URY}");
            }
        }
    }
}